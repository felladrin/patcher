using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace Patcher
{
    public sealed partial class Patcher : Form
    {
        ///////////////////
        // SYSTEM CONFIG //
        ///////////////////

        private const string windowName = "Client Patcher";
        private const string mainApp = "client.exe";
        private const string webpage = "http://example.com/index.html";
        private static string host = "http://felladrin.com/game/files/";
        private const string updateList = "http://felladrin.com/game/filelist.php";
        private const string fullAppURL = "http://felladrin.com/game/game.zip";

        //////////////////
        // OTHER FIELDS //
        //////////////////

        private static WebClient wc = new WebClient();
        private static string[] listarray = new string[1023];
        private static int counter = 0;
        private static int i = 0;
        private static long length;
        private static string fileBeingUpdated;
        private static string fullAppFile;

        //////////////////////////////
        // DRAGABLE BORDERLESS FORM //
        //////////////////////////////

        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT)
                m.Result = (IntPtr)HTCAPTION;
        }

        /////////////////////////
        // DEFAULT CONSTRUCTOR //
        /////////////////////////

        public Patcher()
        {
            InitializeComponent();

            this.Text = windowName;

            buttonPlay.Enabled = false;

            wc.DownloadFileCompleted += new AsyncCompletedEventHandler(wc_DownloadFileCompleted);

            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);

            webBrowserPatch.Navigate(webpage);

			if (host[host.Length - 1] != '/')
				host += "/";

            var uri = new Uri(fullAppURL);
            fullAppFile = System.IO.Path.GetFileName(uri.LocalPath);

            updateFileList.RunWorkerAsync();
        }

        private void updateFileList_DoWork(object sender, DoWorkEventArgs e)
        {
            Stream stream = null;
            try
            {
                stream = wc.OpenRead(updateList);
            }
            catch (WebException)
            {
                MessageBox.Show("Update list not found! Try again later.");
                Application.Exit();
            }
            var reader = new StreamReader(stream);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                listarray[counter] = line;
                counter++;
            }
        }

        private void updateFileList_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            downloadFiles.Start();
        }

        private void downloadFiles_Tick(object sender, EventArgs e)
        {
            if (!wc.IsBusy)
            {
                if (i < counter)
                {
                    if (File.Exists(mainApp))
                    {
                        var request = (HttpWebRequest)WebRequest.Create(host + listarray[i]);

                        try
                        {
                            var response = (HttpWebResponse)request.GetResponse();
                            response.Close();
                            length = response.ContentLength;
                        }
                        catch (WebException)
                        {
                            downloadFiles.Stop();
                            MessageBox.Show("The file " + listarray[i] + " was not found on the server.");
                            Application.Exit();
                        }

                        labelPatch.Text = "Checking " + listarray[i] + ".";

                        var slashcount = listarray[i].Count(c => c == '/');

                        if (slashcount != 0)
                        {
                            string s = Path.GetDirectoryName(listarray[i]);
                            if (!Directory.Exists(s))
                                Directory.CreateDirectory(s);
                        }

                        if (File.Exists(listarray[i]))
                        {
                            var fi = new FileInfo(listarray[i]);

                            if (fi.Length != length)
                            {
                                labelPatch.Text = listarray[i] + " is outdated";
                                fileBeingUpdated = listarray[i];
                                wc.DownloadFileAsync(new Uri(host + listarray[i]), listarray[i]);
                            }
                            else
                            {
                                labelPatch.Text = listarray[i] + " is updated";
                            }
                        }
                        else
                        {
                            labelPatch.Text = listarray[i] + " is outdated";
                            fileBeingUpdated = listarray[i];
                            wc.DownloadFileAsync(new Uri(host + listarray[i]), listarray[i]);
                        }

                        i++;
                    }
                    else
                    {
                        var request = (HttpWebRequest)WebRequest.Create(fullAppURL);

                        try
                        {
                            var response = (HttpWebResponse)request.GetResponse();
                            response.Close();
                            length = response.ContentLength;
                        }
                        catch (WebException)
                        {
                            downloadFiles.Stop();
                            MessageBox.Show("The file " + fullAppFile + " was not found on the server.");
                            Application.Exit();
                        }

                        fileBeingUpdated = fullAppFile;
                        wc.DownloadFileAsync(new Uri(fullAppURL), fullAppFile);

                        i = counter;
                    }
                }
                else
                {
                    downloadFiles.Stop();

                    if (File.Exists(fullAppFile))
                    {
                        try
                        {
                            // Open an existing zip file for reading
                            ZipStorer zip = ZipStorer.Open(fullAppFile, FileAccess.Read);

                            // Read the central directory collection
                            List<ZipStorer.ZipFileEntry> dir = zip.ReadCentralDir();

                            bool success = false;

                            // Extract all files in target directory
                            foreach (ZipStorer.ZipFileEntry entry in dir)
                            {
                                success = zip.ExtractFile(entry, "./" + entry.FilenameInZip);
                            }

                            zip.Close();

                            if (success)
                            {
                                File.Delete(fullAppFile);
                            }
                        }
                        catch (InvalidDataException)
                        {
                            Console.WriteLine("Error: Invalid or not supported Zip file.");
                        }
                        catch
                        {
                            Console.WriteLine("Error while processing source file.");
                        }

                        i = 0;
                        downloadFiles.Start();
                    }
                    else
                    {
                        labelPatch.Text = "Patch process completed!";
                        buttonPlay.Enabled = true;
                    }
                }
            }
        }

        private void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString()) / 1048576;
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString()) / 1048576;

            progressBarPatch.Value = e.ProgressPercentage;

            labelPatch.Text = fileBeingUpdated + " [" + String.Format("{0:0.0}", bytesIn) + " MB / " + String.Format("{0:0.0}", totalBytes) + " MB]";
        }

        private void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            progressBarPatch.Value = 100;
            labelPatch.Text = "Unpacking " + fileBeingUpdated;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            if (File.Exists(mainApp))
            {
                System.Diagnostics.Process.Start(mainApp);
            }
            else
            {
                MessageBox.Show("Error: " + mainApp + " was not patched correctly.");
            }
            Application.Exit();
        }
    }
}
