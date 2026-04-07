using System;
using System.Windows.Forms;
using AppConfiguration;
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
            _ = new ConfigKeys(KeyStorage.AppConfig); //Initialize the configuration keys to ensure they are loaded before any other operations
            Security.EncryptExternalSection("appSettings");//Encrypt the appSettings section in App.config to secure sensitive information
            Application.Run(new frmMDIMain());
        }
    }
}
