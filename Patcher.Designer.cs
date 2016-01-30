namespace Patcher
{
    partial class Patcher
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Patcher));
            this.webBrowserPatch = new System.Windows.Forms.WebBrowser();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.progressBarPatch = new System.Windows.Forms.ProgressBar();
            this.labelPatch = new System.Windows.Forms.Label();
            this.updateFileList = new System.ComponentModel.BackgroundWorker();
            this.downloadFiles = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // webBrowserPatch
            // 
            this.webBrowserPatch.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.webBrowserPatch.AllowNavigation = false;
            this.webBrowserPatch.AllowWebBrowserDrop = false;
            this.webBrowserPatch.IsWebBrowserContextMenuEnabled = false;
            this.webBrowserPatch.Location = new System.Drawing.Point(12, 12);
            this.webBrowserPatch.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserPatch.Name = "webBrowserPatch";
            this.webBrowserPatch.ScrollBarsEnabled = false;
            this.webBrowserPatch.Size = new System.Drawing.Size(410, 279);
            this.webBrowserPatch.TabIndex = 1;
            // 
            // buttonPlay
            // 
            this.buttonPlay.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.buttonPlay.Location = new System.Drawing.Point(347, 297);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(75, 23);
            this.buttonPlay.TabIndex = 2;
            this.buttonPlay.Text = "Start";
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.buttonClose.Location = new System.Drawing.Point(347, 326);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // progressBarPatch
            // 
            this.progressBarPatch.Location = new System.Drawing.Point(12, 297);
            this.progressBarPatch.Name = "progressBarPatch";
            this.progressBarPatch.Size = new System.Drawing.Size(329, 23);
            this.progressBarPatch.TabIndex = 4;
            // 
            // labelPatch
            // 
            this.labelPatch.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPatch.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelPatch.Location = new System.Drawing.Point(12, 326);
            this.labelPatch.Name = "labelPatch";
            this.labelPatch.Size = new System.Drawing.Size(329, 23);
            this.labelPatch.TabIndex = 5;
            this.labelPatch.Text = "Checking Files";
            this.labelPatch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // updateFileList
            // 
            this.updateFileList.DoWork += new System.ComponentModel.DoWorkEventHandler(this.updateFileList_DoWork);
            this.updateFileList.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.updateFileList_RunWorkerCompleted);
            // 
            // downloadFiles
            // 
            this.downloadFiles.Tick += new System.EventHandler(this.downloadFiles_Tick);
            // 
            // Patcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.ClientSize = new System.Drawing.Size(434, 361);
            this.Controls.Add(this.labelPatch);
            this.Controls.Add(this.progressBarPatch);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.webBrowserPatch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Patcher";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Client Patcher";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowserPatch;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.ProgressBar progressBarPatch;
        private System.Windows.Forms.Label labelPatch;
        private System.ComponentModel.BackgroundWorker updateFileList;
        private System.Windows.Forms.Timer downloadFiles;
    }
}

