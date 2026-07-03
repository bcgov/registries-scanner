using AsyncRequests;
using PdfSharp.Drawing.BarCodes;
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

            InitializeComponent();

            // Get the application setting.
            AppSettingObj aso = new AppSettingObj();

            // Register Twain SDK
            TwainGlobalSettings tgs = new TwainGlobalSettings();
            tgs.Register(aso.TwainSDKUserName, aso.TwainSDKEmail, aso.TwainSDKKey);
        }


        private void selectSourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Create a device manager and display the built in selection dialog
                DeviceManager deviceManager = new DeviceManager();
                
                deviceManager.Open();
                
                deviceManager.ShowDefaultDeviceSelectionDialog();

                // Close our device manger and ask scanning form to create a new one
                // for its use to pick up on the new selected scanner.
                deviceManager.Close();
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

        public void ChildFormClosing(object sender, FormClosingEventArgs e)
        {
            // Ask the user if they would like to start a new scanning session.
            // If they select no the main form will close and exit
            if (e.CloseReason == CloseReason.UserClosing)
            {
                string message = "Do you want to load a new scanning session?";
                if (MessageBox.Show(message, "Restart Application",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // start a new frmScannerDocument
                    this.MDIMain_Load(this, EventArgs.Empty);
                }
                else
                {
                    this.Close();
                    this.Dispose();
                }
            }

        }

        public void MDIMain_Load(object sender, EventArgs e)
        {
            _scannerForm = new frmScannerDocument();
            _scannerForm.MdiParent = this;
            _scannerForm.WindowState = FormWindowState.Maximized;
            // Catch and handle child form closing
            _scannerForm.FormClosing += new FormClosingEventHandler(ChildFormClosing);
            _scannerForm.Show();
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If there isnt a scanning form already open start one
            if (_scannerForm == null)
            {
                MDIMain_Load(this, EventArgs.Empty);
            }
            string message = "Please manually enter a barcode.";
            var barCode = frmEnterBarCode.ManualBarcode(message);
            var gotDoc = _scannerForm.GetBarcode(barCode);
            if (!gotDoc)
            {
                MessageBox.Show("Unable to get a document record for the entered barcode.");
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmAbout();
            frm.ShowDialog();
            frm.Dispose();
        }
    }
}
