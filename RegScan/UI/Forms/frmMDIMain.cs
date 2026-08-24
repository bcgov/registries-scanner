using AppConfiguration;
using System;
using System.Windows.Forms;
using Vintasoft.Twain;

namespace RegScan
{
    

    /// <summary>
    /// frmMDIMain is the main window of the application. 
    /// This window controls 
    ///   - child window (frmScannerDocument) launching and state
    ///   - all operations from the menu bar
    /// </summary>
    /// <remarks>
    /// MDI -> Multiple Document Interface. An application that can have multiple child windows 
    ///   showing their own documents.
    /// </remarks>
    public partial class frmMDIMain : Form
    {
        #region consts

        /// <summary>
        /// Const values. Set to match values in frmScannerDocument Form exactly. If you update
        /// here please ensure the paring values in frmScannerDocument are also updated. 
        /// </summary>
        const int _closeRequestRefresh = 1;
        const int _closeAutoRefresh = 2;
        const int _closeAndExit = 3;

        #endregion
        #region Fields

        // The current child frmScannerDocument form 
        frmScannerDocument _scannerForm = null;
        frmBoxManagement _boxForm = null;

        #endregion

        public frmMDIMain()
        {
            // Init the AsyncRequest to ensure API settings are established before any API calls
            APIRequest.SetAPISettings();

            InitializeComponent();

            // Register Twain SDK
            TwainGlobalSettings tgs = new TwainGlobalSettings();
            tgs.Register(ConfigKeys.TWAINSDKUSERNAME, ConfigKeys.TWAINSDKEMAIL, ConfigKeys.TWAINSDKKEY);
        }

        /// <summary>
        /// Event handler for catching when a frmScannerDocument form closes. If the user indicates
        /// that they would like to load a new session a new form is created, if not this form will
        /// close and the application exits.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// If the form is closed for any non-user related event (i.e. OS shutdown, TaskManager 
        /// force close, etc) this function will not attempt to ask the user if they would like to
        /// load a new session. -> The application will exit.
        /// </remarks>
        public void ChildFormClosing(object sender, FormClosingEventArgs e)
        {   
            // Ask the user if they would like to start a new scanning session.
            // If they select no the main form will close and exit
            if (sender.Equals(_scannerForm))
            {
                if (frmScannerDocument.CloseReason != _closeAndExit)
                {
                    bool refresh = frmScannerDocument.CloseReason == _closeAutoRefresh;

                    // only request user input on a refresh if the form was closed unexpectedly.
                    if (!refresh)
                    {
                        string msg = "Do you want to load a new scanning session?";
                        refresh = MessageBox.Show(msg, "Restart Application",
                            MessageBoxButtons.YesNo) == DialogResult.Yes; 
                    } 
                    // if auto refresh was set, or the user indicated they wanted to refresh
                    if(refresh)
                    {
                        _scannerForm.FormClosing -= new FormClosingEventHandler(ChildFormClosing);
                        frmScannerDocument temp = _scannerForm;
                        temp.Hide();
                        // start a new frmScannerDocument
                        LoadScanningForm(this, EventArgs.Empty);
                        temp.Close();
                        return;
                    }
                }
                else
                {
                    this.Close();
                    this.Dispose();
                    return;
                }
            }
            // If the form is closed for any other reason and not sent from the scanner form
            // (frmScannerDocument) then it will not be caught and will just close.
        }

        /// <summary>
        /// Handles creating a new scanning child form. If the form closes automatically catch it
        /// and process with ChildFormClosing(). 
        /// </summary>
        /// <param name="sender">Any control that launches a new child form</param>
        /// <param name="e">Any even that launches a new child form</param>
        public void LoadScanningForm(object sender, EventArgs e)
        {
            _scannerForm = new frmScannerDocument();
            //_scannerForm.MdiParent = this;
            _scannerForm.TopLevel = false;
            _scannerForm.FormBorderStyle = FormBorderStyle.None;
            _scannerForm.Dock = DockStyle.Fill;
            this.Controls.Add(_scannerForm);
            
            // Catch and handle child form closing
            //_scannerForm.FormClosing += new FormClosingEventHandler(ChildFormClosing);
            _scannerForm.Show();
        }

        /// <summary>
        /// Handles creating a new box child form.
        /// </summary>
        /// <param name="sender">Any control that launches a new child form</param>
        /// <param name="e">Any even that launches a new child form</param>
        public void LoadBoxForm(object sender, EventArgs e)
        {
            _boxForm = new frmBoxManagement();
            _boxForm.TopLevel = false;
            _boxForm.FormBorderStyle = FormBorderStyle.None;
            _boxForm.Dock = DockStyle.Top;
            this.Controls.Add(_boxForm);
            _boxForm.Show();
        }

        /// <summary>
        /// Handles creating child forms.
        /// </summary>
        /// <param name="sender">Any control that launches a new child form</param>
        /// <param name="e">Any even that launches a new child form</param>
        public void MDIMain_Load(object sender, EventArgs e)
        {
            // Init child forms
            LoadBoxForm(sender, e);
            LoadScanningForm(sender, e);

            this.menuStrip.BringToFront();
            _boxForm.BringToFront();
            _scannerForm.BringToFront();
        }

        #region Menu Item Event Handlers

        /// <summary>
        /// Allow the user to select a scanning device from the list of available options.
        /// </summary>
        /// <param name="sender">Menu Item Bar</param>
        /// <param name="e">Event -> User selecting "Select Source"</param>
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
            catch (Exception err)
            {
                string msg = "TWAIN Device unable to reset.";
                UtilityObj.WriteLog(UtilityObj.error, msg + Environment.NewLine +
                    err.ToString());
                MessageBox.Show(err.Message + msg + " Close and open scanning application");
            }
        }

        /// <summary>
        /// Handles the Find menu item being selected. 
        /// The frmEnterText form will return a barcode string from the user, which then can be
        /// used to search for a document record. If one is found the record information will be
        /// displayed in the frmScannerDocument form. 
        /// If there is an error finding the document show an error message to the user.
        /// </summary>
        /// <param name="sender">Menu Item Bar</param>
        /// <param name="e">Event -> User selecting "Find"</param>
        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If there isn't a scanning form already open start one
            if (_scannerForm == null)
            {
                MDIMain_Load(this, EventArgs.Empty);
            }
            string message = "Please manually enter a barcode.";
            var barCode = frmEnterText.ManualBarcode(message);
            var gotDoc = _scannerForm.GetBarcode(barCode);
            if (!gotDoc)
            {
                MessageBox.Show("Unable to get a document record for the entered barcode.");
            }
        }

        /// <summary>
        /// Used to open and display the About Form (frmAbout). Only called if the user selects the
        /// "About" menu item from the menu bar. 
        /// </summary>
        /// <param name="sender">Menu Item Bar</param>
        /// <param name="e">Event -> User selecting "About"</param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmAbout();
            frm.ShowDialog();
            frm.Dispose();
        }

        #endregion

    }
}
