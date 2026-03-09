using System;
using System.Windows.Forms;
using AsyncRequests;

namespace RegScan
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Security.EncryptExternalSection("appSettings");//Encrypt the appSettings section in App.config to secure sensitive information
            Application.Run(new frmMDIMain());
        }
    }
}
