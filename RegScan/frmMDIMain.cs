using AsyncRequests;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Utilities;
using Vintasoft.Twain;

namespace RegScan
{
    public partial class frmMDIMain : Form
    {

        #region Fields

        frmScannerDocument _scannerForm = null;

        #endregion

        public frmMDIMain()
        {            
            APIRequest api = new APIRequest();

            UtilityObj.WriteLog(UtilityObj.debug, "Initializing components");
            InitializeComponent();

            UtilityObj.WriteLog(UtilityObj.debug, "Reading app settings");
            // Get the application setting.
            AppSettingObj aso = new AppSettingObj();

            // Register Twain SDK
            UtilityObj.WriteLog(UtilityObj.debug, "Initialize twain settings");
            TwainGlobalSettings tgs = new TwainGlobalSettings();
            tgs.Register(aso.TwainSDKUserName, aso.TwainSDKEmail, aso.TwainSDKKey);


            // Set the database connection.
            UtilityObj.WriteLog(UtilityObj.debug, "Set database connection");
            // FIX DBSupport.SetConnection(DBSupport.BuildConnectionString(aso.UserName, aso.Password, aso.Host, aso.Port, aso.Sid));

            // Assign which database that is being used.
            //this.Text += " Using Database " + aso.DatabaseName;
            this.Text += " Using DRS API";
            UtilityObj.WriteLog(UtilityObj.debug, this.Text);
            
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            UtilityObj.WriteLog(UtilityObj.debug, "Make new scanner form and display it");
            _scannerForm = new frmScannerDocument();
            _scannerForm.MdiParent = this;
            _scannerForm.WindowState = FormWindowState.Maximized;
            _scannerForm.Show();
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void selectSourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                UtilityObj.WriteLog(UtilityObj.debug, 
                    "Create new device manager using vintasoft twain");
                // Create a device manager and display the built in selection dialog
                DeviceManager deviceManager = new DeviceManager();
                UtilityObj.WriteLog(UtilityObj.debug, "Open Manager");
                deviceManager.Open();
                UtilityObj.WriteLog(UtilityObj.debug, 
                    "Show default device selection dialog (choose scanner)");
                deviceManager.ShowDefaultDeviceSelectionDialog();

                // Close our device manger and ask scanning form to create a new one
                // for its use to pick up on the new selected scanner.
                UtilityObj.WriteLog(UtilityObj.debug, "Close manager after dialog");
                deviceManager.Close();
                UtilityObj.WriteLog(UtilityObj.debug, 
                    "Create new device manager based on scanner selection");
                _scannerForm.CreateTwainDeviceManager();
            }
            catch (Exception _Error)
            {
                string msg = "TWAIN Device unable to reset.";
                UtilityObj.WriteLog(UtilityObj.error, msg + Environment.NewLine +
                    _Error.ToString());
                MessageBox.Show(_Error.Message + msg + " Close and open scanning application");
            }
        }

        private void MDIMain_Load(object sender, EventArgs e)
        {
            UtilityObj.WriteLog(UtilityObj.debug, "Load New Form");
            ShowNewForm(sender, e);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form childForm = new frmFind();
            childForm.MdiParent = this;
            childForm.Text = "Find Document: ";
            childForm.WindowState = FormWindowState.Normal;
            childForm.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmAbout();
            frm.ShowDialog();
            frm.Dispose();
        }
    }
}
