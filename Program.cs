using System;
using System.Linq;
using System.Windows.Forms;

namespace Patcher
{
    static class Program
    {
        // Allow only one instance running.
        static readonly System.Threading.Mutex mutex = new System.Threading.Mutex(false, "7f3f621809043c6f59e2ec85d6b3c48d");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!mutex.WaitOne(TimeSpan.FromSeconds(2), false))
            {
                MessageBox.Show("Another instance is already running.", "", MessageBoxButtons.OK);
                return;
            }

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Patcher());
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }
    }
}
