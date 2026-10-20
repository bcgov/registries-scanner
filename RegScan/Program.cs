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
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                new AppConfiguration.ConfigKeys(); //Initialize the configuration keys to ensure they are loaded before any other operations
                Security.EncryptExternalSection("appSettings");//Encrypt the appSettings section in App.config to secure sensitive information
                Application.Run(new frmMDIMain());
            }
            catch (Exception e)
            {
                string msg = "Hit an uncaught and unexpected error:\n" + e.Message + "\n\nIf this issue persists contact support.";
                MessageBox.Show(msg, "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UtilityObj.WriteLog(UtilityObj.error, "Hit unexpected error during runtime:\n" + e.ToString());
            }
            
        }
    }
}
