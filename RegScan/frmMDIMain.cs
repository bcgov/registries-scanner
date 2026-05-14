using AsyncRequests;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
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

            UtilityObj.writeLog("Initializing components");
            InitializeComponent();

            UtilityObj.writeLog("Reading app settings");
            // Get the application setting.
            AppSettingObj aso = new AppSettingObj();

            // Register Twain SDK
            UtilityObj.writeLog("Initialize twain settings");
            TwainGlobalSettings tgs = new TwainGlobalSettings();
            tgs.Register(aso.TwainSDKUserName, aso.TwainSDKEmail, aso.TwainSDKKey);


            // Set the database connection.
            UtilityObj.writeLog("Set database connection");
            // FIX DBSupport.SetConnection(DBSupport.BuildConnectionString(aso.UserName, aso.Password, aso.Host, aso.Port, aso.Sid));

            // Assign which database that is being used.
            //this.Text += " Using Database " + aso.DatabaseName;
            this.Text += " Using DRS API";
            UtilityObj.writeLog(this.Text);

            // Load up look up lists now.
            // These API calls will not work as expected. They are hitting DRS API endpoints and
            // storing the returned values as local variables. However; because there is no obj
            // of each type to contain this values we can not guarentee that they will be set
            // correctly or that the values will be accessable in the future. I have not hit errors
            // from commenting these lines out yet but I am leaving them here in the event that
            // they are required.
            //DocTypeObj.Refresh();
            //AuthorObj.Refresh();
            //List<AuthorObj> mylist = AuthorObj._list;
            //OwnerTypeObj.Refresh();
            //BoxObj.Refresh();
                           

        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            UtilityObj.writeLog("Make new scanner form and display it");
            _scannerForm = new frmScannerDocument();
            _scannerForm.MdiParent = this;
            _scannerForm.WindowState = FormWindowState.Maximized;
            _scannerForm.Show();
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void selectSourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                UtilityObj.writeLog("Create new device manager using vintasoft twain");
                // Create a device manager and display the built in selection dialog
                DeviceManager deviceManager = new DeviceManager();
                UtilityObj.writeLog("Open Manager");
                deviceManager.Open();
                UtilityObj.writeLog("Show default device selection dialog (choose scanner)");
                deviceManager.ShowDefaultDeviceSelectionDialog();

                // Close our device manger and ask scanning form to create a new one for its use to pick up on the new selected scanner.
                UtilityObj.writeLog("Close manager after dialog");
                deviceManager.Close();
                UtilityObj.writeLog("Create new device manager based on scanner selection");
                _scannerForm.CreateTwainDeviceManager();
            }
            catch (Exception _Error)
            {
                UtilityObj.writeLog("unable to reset device");
                MessageBox.Show(_Error.Message + " ... unable to reset device for scanning form. Close and open scanning application");
            }
        }

        private void boxMaintenanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmBox();
            frm.ShowDialog();
        }

        private void scannerSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmScannerSetting();
            frm.ShowDialog();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmOptions();
            frm.ShowDialog();
        }

        private void MDIMain_Load(object sender, EventArgs e)
        {
            UtilityObj.writeLog("Load New Form");
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

        private void printSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmSetPrinterDefault();
            frm.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmAbout();
            frm.ShowDialog();
            frm.Dispose();
        }
    }
}
