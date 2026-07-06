using System;
using System.Windows.Forms;

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
            new AppConfiguration.ConfigKeys(); //Initialize the configuration keys to ensure they are loaded before any other operations
            Security.EncryptExternalSection("appSettings");//Encrypt the appSettings section in App.config to secure sensitive information
            Application.Run(new frmMDIMain());
        }
    }
}
