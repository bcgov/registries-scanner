using AppConfiguration;
using PdfSharp.Pdf;
using RegScan.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Vintasoft.Twain;

namespace RegScan
{
    public partial class frmScannerDocument : Form
    {
        #region consts

        /// <summary>
        /// Const values. Set to match values in MDIMain Form exactly. If you update here please
        /// ensure the paring values in frmMDIMain are updated. 
        /// </summary>
        const int _closeRequestRefresh = 1;
        const int _closeAutoRefresh = 2;
        const int _closeAndExit = 3;

        #endregion

        #region Fields

        /// <summary>
        /// Current active document.
        /// </summary>
        private static DocumentObj _currentDocument = null;

        /// <summary>
        /// Used to control how the parent form will handle the closing of this form
        /// </summary>
        public static int CloseReason { get; set; } = 0;

        /// <summary>
        /// Default setting for scanning parameters.
        /// </summary>
        private ScannerSettingObj _defaultSetting;

        /// <summary>
        /// List of all scanned images for the current document.
        /// </summary>
        private List<ImageObj> _scannedImageList = new List<ImageObj>();

        /// <summary>
        /// Index of current image being displayed.
        /// </summary>
        private int _currentImageIndex = -1;

        /// <summary>
        /// This is the list of bitmap images returned from the scanner from one scan session.
        ///     This may be one image or multiple images. 
        /// </summary>
        private List<string> _scanSessionFileList = new List<string>();

        /// <summary>
        /// _image0 is the first page of the document and possibly contains a barcode.
        /// </summary>
        private Bitmap _image0;

        /// <summary>
        /// PDF files created during viewing that can be deleted when form closes.
        /// </summary>
        List<string> _tempFileNameList = new List<string>();

        /// <summary>
        /// TWAIN device manager.
        /// </summary>
        DeviceManager _deviceManager = null;

        /// <summary>
        /// Current device.
        /// </summary>
        Device _currentDevice;

        public static DocumentObj CurrentDocument { get { return _currentDocument; } }

        #endregion
        public frmScannerDocument()
        {
            InitializeComponent();
            LoadControls();

            UtilityObj.CreateFolder(Path.GetTempPath() + "Images");

            // Set scanner defaults.
            _defaultSetting = new ScannerSettingObj();
            SetSettingValues();
            useAdfCheckBox_CheckedChanged(new object(), new EventArgs());

            // Create a path to where debugging logs should be stored.
            string scannerFile = "vstwain.log";
            string scannerPath = String.Concat(ConfigKeys.LOGPATH, "\\", scannerFile);

            UtilityObj.CreateFolder(ConfigKeys.LOGPATH);
            UtilityObj.CreateFile(scannerPath);

            // Set up debugging for the TWAIN SDK
            if ( !TwainEnvironment.IsDebuggingEnabled )
            {
                TwainEnvironment.EnableDebugging(scannerPath);
                TwainEnvironment.DebugLevel = DebugLevel.Debug;
            }
            

            // create TWAIN device manager
            CreateTwainDeviceManager();
        }

        /// <summary>
        /// Using the default settings set by ScannerSettingObj init function, set the checkboxes
        /// at the top of the form.
        /// </summary>
        public void SetSettingValues()
        {
            if (_defaultSetting.CanUseDocumentFeeder)
            {
                useAdfCheckBox.Enabled = true;
                useAdfCheckBox.Checked = _defaultSetting.UseDocumentFeeder;
            }
            else 
            {
                useAdfCheckBox.Enabled = false;

            }

            if (_defaultSetting.CanUseDuplex)
            {
                useDuplexCheckBox.Enabled = true;
                useDuplexCheckBox.Checked = _defaultSetting.UseDuplex;
            }
            else
            {
                useDuplexCheckBox.Enabled = false;
            }

            useUICheckBox.Checked = _defaultSetting.ShowTwainUI;
            showProgressIndicatorUICheckBox.Checked = _defaultSetting.ShowProgressIndicatorUI;
        }

        #region Scanning Session

        /// <summary>
        /// When the form is closed (by an error or user actions) this method is used to clean the
        /// temporary files, in app memory, and any connections to external devices. 
        /// This function will trigger the ChildFormClosing() function in frmMDIMain.cs.
        /// </summary>
        /// <remarks>
        /// Close() methods will ensure that the object in question is stopped 
        /// Dispose() methods will release resources associated with the object and allow garbage
        /// collection.
        /// </remarks>
        public void CloseForm()
        {
            // Clean up the temp folder for storing scanned images.
            UtilityObj.DeleteFolder("Images");

            if (TwainEnvironment.IsDebuggingEnabled)
            {
                TwainEnvironment.DisableDebugging();
            }
            
            // Close the device
            if (_currentDevice != null)
            {
                if (_currentDevice.State > DeviceState.Enabled)
                {
                    _currentDevice.CancelTransfer();
                }
                // Stop catching device events
                UnsubscribeFromDeviceEvents(_currentDevice);
                
                if (_currentDevice.State != DeviceState.Closed)
                    _currentDevice.Close();
            }

            // Close the device manager
            if (_deviceManager.State != DeviceManagerState.Closed)
            {
                _deviceManager.Close();
                _deviceManager.Dispose();
            }

            UpdateImageDisplay();

            // Close the form and dispose of it to allow garbage collection
            this.Close();
        }

        /// <summary>
        /// Process the scanned image as a bitmap.
        /// </summary>
        /// <param name="image"> Bitmap image from scan process </param>
        private void ProcessScan(Bitmap image)
        {
            // Set the first image to _image0 for barcode scanning and display purposes.
            // NOTE - It may be possible to use image and _image0 interchangeably and
            //     eliminate the need for _image0.
            if (_image0 == null)
            {
                _image0 = new Bitmap(image);
            }

            int fileCount = _scanSessionFileList.Count;

            string fileNumber = fileCount > 0 ? 
                fileCount.ToString() : "";
            string fileName = "Images\\Bitmap_image" + fileNumber + ".bmp";
            UtilityObj.SaveImageAsFile(fileName, image);

            // Save the created filename to a list of scanned files for this session.
            _scanSessionFileList.Add(fileName);
            
            // Create a new ImageObj and add to the list of scanned images
            _scannedImageList.Add(new ImageObj(image, PdfSharp.PageOrientation.Portrait,
                ImageObj.GetPageSize(image.Width, image.Height)));
        }

        /// <summary>
        /// Used to prompt the user with a message that they can reply "Yes" or "No" to. 
        /// The response is parsed and returned as a bool reflecting how the user responded.
        /// </summary>
        /// <param name="msg">Message to display to the user inside the Message Box </param>
        /// <param name="title">Title of the MessageBox</param>
        /// <returns>True if the user indicates "Yes" else false. </returns>
        public bool GetUserInput(string msg, string title)
        {
            bool userAffirm = false;
            // Ask the user if they would like to try again
            DialogResult usrInput = MessageBox.Show(msg + System.Environment.NewLine + 
                "Would you like to try again?", title, MessageBoxButtons.YesNo);
            
            userAffirm = usrInput == DialogResult.Yes ? true : false;
            return userAffirm;
        }

        /// <summary>
        /// Given a string attempt to retrieve an associated document record from DRS API.
        /// If the string is empty, or there was an issue ask the user to enter a barcode manually.
        /// </summary>
        /// <param name="barCode">
        /// A string containing a barcode, if empty or null the user can enter a barcode manually
        /// </param>
        /// <returns>True if a document record was found. False if not</returns>
        public bool GetBarcode(string barCode)
        {
            DocumentObj doc = null;
            bool documentSet = false;
            bool checkBarcode = true;

            // Loop to get a document record for a given barcode
            while (checkBarcode)
            {
                if (string.IsNullOrEmpty(barCode))
                {
                    // Ask if a barcode should be entered manually.
                    string message = "Do you want to manually enter a barcode?";
                    barCode = frmEnterText.ManualBarcode(message);
                }
                // Call to DRS API for existing document records for this barcode.
                // There may be more than one document (known as versions)
                try
                {
                    List<DocumentObj> resp = DocumentObj.Find(barCode);
                    // if there is more than one document record select the first
                    if (resp.Count >= 1)
                        doc = resp[0];
                }
                catch (ArgumentNullException ane)
                {
                    // This exception is thrown if the barcode is not passed into the controller
                    // correctly or if it is an empty string.
                    UtilityObj.WriteLog(UtilityObj.error, ane.ToString());
                    // continue the process
                    doc = null;
                    checkBarcode = GetUserInput(ane.Message, "Empty Barcode");
                }
                catch (ArgumentException ae)
                {
                    // This type of exception is thrown when the accession number is not in an
                    // expected format. This message box will display the number to the user
                    // and warn them to verify it.
                    UtilityObj.WriteLog(UtilityObj.error, ae.ToString());
                    MessageBox.Show(ae.Message + "\nPlease copy the accession number listed" +
                        "for verification as it will not be displayed in the application.", 
                        "Unexpected Number Format");
                    // continue the process
                    checkBarcode = false;
                }
                catch (Exception e)
                {
                    string msg = "Hit error trying to find record for barcode: " + barCode;
                    // log the issue
                    UtilityObj.WriteLog(UtilityObj.error, msg +
                        System.Environment.NewLine + e.ToString());

                    // Ask the user if they would like to try again 
                    checkBarcode = GetUserInput(msg, "Missing/ Not Found Barcode");

                    // start loop over with a request for a new barcode
                    barCode = null;
                    doc = null;
                }

                // If documents were found continue to next step
                if (doc != null)
                {
                    checkBarcode = false;
                    documentSet = true;
                    SetDocument(doc);
                }
            }
            
            return documentSet;
        }

        /// <summary>
        /// Given a documentObj update the _currentDocument and update the form
        /// </summary>
        /// <param name="inDoc">Document information to be displayed</param>
        public void SetDocument(DocumentObj inDoc)
        {
            // The first document is the latest and is the one that will be displayed
            _currentDocument = inDoc;

            // We have a record from DRS API that matches the barcode.
            // This means we will be updating the record.
            _currentDocument.UpdateRecord = true;

            // Display the document.
            SetForm();
        }

        /// <summary>
        /// Attempts to find a barcode on the first page scanned then tries to request information
        /// the DRS API on the barcode. If the barcode is unable to be read by the `BarCodeObj` the
        /// user will be prompted to enter a barcode manually. This is looped until a barcode is
        /// acquired or until the user indicates they do not want to enter a barcode; in that case
        /// the form is reset.
        /// </summary>
        private void ProcessFirstPage()
        {
            // Control on if the document is set
            bool gotDocument = false;

            // Scan the first page for Barcodes
            string barCode = BarCodeObj.ScanForBarcode(_image0);

            gotDocument = GetBarcode(barCode);

            if (gotDocument)
            {
                // Will only be true if it is an existing scan and
                // the user indicates a new version is not required.
                bool cancelScan = false;

                // IF document has already been scanned.
                if (! string.IsNullOrEmpty(_currentDocument.DocumentURL))
                {
                    // Ask if this is a new version.
                    string msg = "Document with barcode " + barCode + " has already been" +
                        " scanned.\nDo you want to replace the previous version?";
                    if (MessageBox.Show(msg, "Document Already Scanned", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // If a new version, then update the version number
                        // TODO - get previous version number from filename. Then add one.
                        // This will always be 0++ = 1
                        _currentDocument.VersionNumber++;
                    }
                    else
                    {
                        // Cancel this scan if a new version is not required.
                        cancelScan = true;
                    }
                }

                // IF scan is to be cancelled
                if (cancelScan)
                {
                    // Set current document back to null.
                    CloseReason = _closeRequestRefresh;
                    CloseForm();
                    return;
                }
                else
                {
                    // Display the first document.
                    _currentImageIndex = 0;
                    // Enable buttons 
                    btnCancelScan.Enabled = true;
                    btnSave.Enabled = true;
                    btnRotateImg.Enabled = true;
                    btnImagePDF.Enabled = true;
                }
            }

            // NOTE Create a new record for the scanned image logic could be added here

            if (_currentDocument != null)
            {
                // if the barcode was not set
                if (string.IsNullOrEmpty(_currentDocument.BarCode))
                {
                    // Clear Image
                    imageBox.Image = null;
                }
            }
        }

        /// <summary>
        /// Once the scan is complete check pages for barcodes and any existing document records.
        /// The scans are then passed along to be shown to the user.
        /// </summary>
        private void ProcessCompleted()
        {
            // If there is no scanned image (user might have clicked the close button)
            if (_scanSessionFileList.Count == 0)
            {
                String msg = "No images scanned. Please ensure the document is loaded and " +
                    "try again. Do you want to reload the scanning session?";
                String caption = "Scanning Error";
                DialogResult res; 
                res = MessageBox.Show(msg, caption, MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    ResetDocument();
                    CloseReason = _closeAutoRefresh;
                    CloseForm();
                    return;
                }
                else
                {
                    ResetDocument();
                    return;
                }
            }

            // IF we have a new document process the first page
            if (_currentDocument == null)
            {
                ProcessFirstPage();
            } 
            // Here we are adding an additional page to the document. We just need to adjust the index
            else {  _currentImageIndex = _scanSessionFileList.Count - 1; }

            SetImageNav();
            
            // Enable the form.
            Enabled = true;
                     
        }


        /// <summary>
        /// All steps used to hide the progress bar, show scanner controls and enable the form
        /// </summary>
        private void hideProgressBar()
        {
            // Show scanning options when processing scan is complete
            useUICheckBox.Enabled = true;
            useAdfCheckBox.Enabled = _defaultSetting.CanUseDocumentFeeder;
            useDuplexCheckBox.Enabled = _defaultSetting.CanUseDuplex;
            ckBoxLowResolution.Enabled = true;
            showProgressIndicatorUICheckBox.Enabled = true;

            // hide the progress bar
            progressBar.Visible = false;
            progressBar.SendToBack();
            progressBar.Enabled = false;

            // Allow the user to interact with the form
            Enabled = true;
        }

        /// <summary>
        /// All steps used to show the progress bar, hide scanner controls and disable the form
        /// </summary>
        private void showProgressBar()
        {
            // Get the forms current size
            var screenSize = this.ClientSize;
            // set progress bar size (width based on screen size)
            this.progressBar.Height = 43;
            this.progressBar.Width = screenSize.Width / 2;
            // set progress bar location (centered)
            this.progressBar.Location = new Point(
                (screenSize.Width - progressBar.Width) / 2 ,
                (screenSize.Height - progressBar.Height) / 2 );
            
            // Disable interaction with the form
            Enabled = false;

            // Hide scanning options when processing scan
            useUICheckBox.Enabled = false;
            useAdfCheckBox.Enabled = false;
            useDuplexCheckBox.Enabled = false;
            ckBoxLowResolution.Enabled = false;
            showProgressIndicatorUICheckBox.Enabled = false;

            // show the progress bar
            progressBar.Enabled = true;
            progressBar.BringToFront();
            progressBar.Visible = true;
            progressBar.Value = 0;
        }

        #endregion

        #region Image Display.

        /// <summary>
        ///  Sets the image on the image navigation panel.
        /// </summary>
        private void SetImageNav()
        {
            if (_currentImageIndex != -1)
                UpdateImageDisplay();
        }

        /// <summary>
        ///  Display next image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnlNextImage_Click(object sender, EventArgs e)
        {
            if (_currentImageIndex + 2 > _scannedImageList.Count)
                return;
            ++_currentImageIndex;
            UpdateImageDisplay();
        }

        /// <summary>
        /// Display previous image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrevImage_Click(object sender, EventArgs e)
        {
            if (_currentImageIndex - 1 < 0)
                return;
            --_currentImageIndex;
            UpdateImageDisplay();
        }

        /// <summary>
        /// Update the image display.
        /// </summary>
        private void UpdateImageDisplay()
        {
            var totalScanned = _scannedImageList.Count;
            if (_currentImageIndex > -1 && _currentImageIndex < totalScanned)
            {
                // we have already read in all the images, select the one from the index to display
                var image = _scannedImageList[_currentImageIndex].Image;
                // display error if this one cant be read
                if (image == null)
                {
                    MessageBox.Show("Error loading image for page " +
                        (_currentImageIndex + 1).ToString());
                    return;
                }
                SetImage(image);
            }

            //Update current and total page labels in the status label
            lblCurImage.Text = (_currentImageIndex + 1).ToString();
            lblTotalImage.Text = totalScanned.ToString();

            // Check the total number of pages against the expected number
            if (_currentDocument != null)
            {
                if (totalScanned > _currentDocument.PageCount)
                {
                    lblTotalImage.BackColor = Theme.WarningBackground;
                    txtPagesInDocument.BackColor = Theme.WarningBackgroundLight;
                }
                else
                {
                    lblTotalImage.BackColor = Color.Transparent;
                    txtPagesInDocument.BackColor = Theme.Disabled;
                }
            }

            // Disable/ enable buttons based on the current image index
            // Fist page - can't delete, no previous image, only go to next image if there is one
            if (_currentImageIndex == 0)
            {
                btnDeleteImage.Enabled = false;
                btnPrevImage.Enabled = false;
                if (totalScanned > 1)
                    btnNextImage.Enabled = true;
                else
                    btnNextImage.Enabled = false;
            }
            // Last page - can be deleted, can go to previous image, no next image
            else if (_currentImageIndex == totalScanned - 1)
            {
                btnDeleteImage.Enabled = true;
                btnPrevImage.Enabled = true;
                btnNextImage.Enabled = false;
            }
            // This will be the default for any page that isn't the last or first
            // Can be deleted and can navigate to next or previous images
            else
            {
                btnDeleteImage.Enabled = true;
                btnPrevImage.Enabled = true;
                btnNextImage.Enabled = true;
            }
        }

        #endregion

        #region house keeping methods

        /// <summary>
        /// Reset the document form fields. 
        /// Best if used with CloseForm() to ensure that in-app memory and temporary files are 
        /// properly cleaned and connections to devices are reset. 
        /// </summary>
        private void ResetDocument()
        {
            // Clear the document form
            txtBarCode.Text = "";
            txtLegalEntityKey.Text = "";
            txtIndexer.Text = "";
            txtDocumentClass.Text = "";
            txtDocumentType.Text = "";
            txtPagesInDocument.Text = "";
            txtDocumentNotes.Text = "";
            maskedTextBoxSequenceNumber.Text = "";
            maskedTextBoxScheduleNumber.Text = "";
            maskedTextBoxBoxNumber.Text = "";

            // ensure back colours are reset
            SetBackgroundAccFields(Theme.Disabled);

            // Disable notes
            txtDocumentNotes.Enabled = false;

            // Delete any temporary FileNames.
            try
            {
                foreach (var fileName in _tempFileNameList)
                    File.Delete(fileName);
                _tempFileNameList.Clear();
            }
            catch (Exception e)
            {
                UtilityObj.WriteLog(UtilityObj.warn,
                    "Unable to delete all images in file list. Next scanning session may not run as" +
                    " expected:\n" + e.ToString());
                MessageBox.Show("Warning: The previous session did not clear as expected. There may" +
                    " be carry-over images that persist. Please try 'Reject Scan' process again or" +
                    " restarting this application.", "Scan Session Failed to Clear");
            }

            // Clear document image
            _currentDocument = null;
            _image0 = null;
            _scannedImageList.Clear();
            _currentImageIndex = -1;

            // Set buttons related to image to a disabled state
            btnCancelScan.Enabled = false;
            btnSave.Enabled = false;
            btnDeleteImage.Enabled = false;
            btnRotateImg.Enabled = false;
            btnImagePDF.Enabled = false;
            btnPrevImage.Enabled = false;
            btnNextImage.Enabled = false;

            // reset img numbers
            lblCurImage.Text = "0";
            lblTotalImage.Text = "0";

            imageBox.Image = null;
        }

        /// <summary>
        /// Sets the image in the image box.
        /// </summary>
        /// <param name="image"></param>
        protected void SetImage(Bitmap image)
        {
            imageBox.SizeToFit = true;
            imageBox.Image = image;
            imageBox.SizeToFit = false;
        }

        
        /// <summary>
        /// Given a background colour update all accession number fields to that colour
        /// </summary>
        /// <param name="updatedBackground">colour value for background</param>
        public void SetBackgroundAccFields(Color updatedBackground)
        {
            maskedTextBoxSequenceNumber.BackColor = updatedBackground;
            maskedTextBoxScheduleNumber.BackColor = updatedBackground;
            maskedTextBoxBoxNumber.BackColor = updatedBackground;
        }
        
        /// <summary>
        /// Set the document fields for accession numbers based on the document record
        /// If these values differ from the current working box inform the user and change the
        /// background of the fields.
        /// </summary>
        protected void SetDocumentAccessionForm()
        {
            if (_currentDocument.AccSet)
            {
                maskedTextBoxSequenceNumber.Text = _currentDocument.AccessionNumber.SequenceString;
                maskedTextBoxScheduleNumber.Text = _currentDocument.AccessionNumber.ScheduleString;
                maskedTextBoxBoxNumber.Text = _currentDocument.AccessionNumber.BoxString;

                // Check if the Accession Number matches the current box
                // If there is a discrepancy between the current working box and the
                // accession number for this document show the user a warning.
                if (!AccessionNumberObj.CompareAccessionObsj(
                    _currentDocument.AccessionNumber,
                    frmBoxManagement.SelectedBox.AccessionNumber))
                {
                    // Set the background of the fields for this form
                    SetBackgroundAccFields(Theme.WarningBackgroundLight);

                    // Warn the user
                    string msg = "The current working box does not match the accession number " +
                        "for this document record.\nPlease verify the current working box and " +
                        "Document Record information.\nDocument Record: " +
                        _currentDocument.AccessionNumber.TextDashes + "\nCurrent Box: " +
                        frmBoxManagement.SelectedBox.AccessionNumber.TextDashes;
                    string title = "Mismatched Accession Numbers";
                    MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            // if it was not set show not available string to users
            else
            {
                string notAva = "N/A";
                maskedTextBoxSequenceNumber.Text = notAva;
                maskedTextBoxScheduleNumber.Text = notAva;
                maskedTextBoxBoxNumber.Text = notAva;
            }
            
        }

        /// <summary>
        /// Called if a record is found for the scanned document.
        /// Form fields will be populated with the document's properties.
        /// </summary>
        protected void SetForm()
        {
            // Document Record Details
            txtBarCode.Text = _currentDocument.BarCode;
            txtLegalEntityKey.Text = _currentDocument.LegalEntityKey;
            txtIndexer.Text = _currentDocument.ScannerId;
            txtFilingDate.Text = _currentDocument.ConsumerFilingDateString;
            txtPagesInDocument.Text = _currentDocument.PageCount.ToString();

            // Record Classification
            txtDocumentClass.Text = _currentDocument.DocumentClass;
            txtDocumentType.Text = string.Concat(
                _currentDocument.DocumentType, " -> ", _currentDocument.DocTypeDesc);

            // Set Accession number values 
            SetDocumentAccessionForm();

            // Document Notes / Description
            txtDocumentNotes.Enabled = true;
            txtDocumentNotes.Text = _currentDocument.Description;
        }
        
        /// <summary>
        /// Add a progress bar to the form. If this is added through the UI it will be within
        /// another control. We want it to stay in front of all controls. 
        /// </summary>
        protected void LoadControls()
        {
            // Create the progress bar
            this.progressBar = new System.Windows.Forms.ProgressBar()
            {
                Name = "progressBar",
                Visible = false
            };
            // add to form
            this.Controls.Add(progressBar);
        }

        #endregion

        #region Scanner Methods

        /// <summary>
        /// If there is not a current device manager create one.
        /// </summary>
        public void CreateTwainDeviceManager()
        {
            try
            {
                if (_deviceManager != null)
                    _deviceManager.Close();
                _deviceManager = new DeviceManager(this, CountryCode.Canada, LanguageType.EnglishCanadian);
                //Set the twain DSM path to the local folder if using 64 bit
                //_deviceManager.TwainDllPath = Directory.GetCurrentDirectory() + "\\TWAINDSM.dll";
            }
            // TODO - Make this catch do something.
            catch { }
        }

        /// <summary>
        /// Subscribe to the device events.
        /// </summary>
        private void SubscribeToDeviceEvents(Device device)
        {
            try
            {
                device.ImageAcquiringProgress += new 
                    EventHandler<ImageAcquiringProgressEventArgs>(device_ImageAcquiringProgress);
                device.ImageAcquired += new 
                    EventHandler<ImageAcquiredEventArgs>(device_ImageAcquired);
                device.ScanFailed += new EventHandler<ScanFailedEventArgs>(device_ScanFailed);
                device.AsyncEvent += new EventHandler<DeviceAsyncEventArgs>(device_AsyncEvent);
                device.ScanFinished += new EventHandler(device_ScanFinished);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error, scanner not found. Is the scanner turned on?",
                    "TWAIN device error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UtilityObj.WriteLog(UtilityObj.error, 
                    "Unable to Subscribe to Device Events" + e.ToString());
                // TODO - handle error gracefully instead of exiting
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Unsubscribe from the device events.
        /// </summary>
        private void UnsubscribeFromDeviceEvents(Device device)
        {
            device.ImageAcquiringProgress -= new 
                EventHandler<ImageAcquiringProgressEventArgs>(device_ImageAcquiringProgress);
            device.ImageAcquired -= new EventHandler<ImageAcquiredEventArgs>(device_ImageAcquired);
            device.ScanFailed -= new EventHandler<ScanFailedEventArgs>(device_ScanFailed);
            device.AsyncEvent -= new EventHandler<DeviceAsyncEventArgs>(device_AsyncEvent);
            device.ScanFinished -= new EventHandler(device_ScanFinished);
        }

        /// <summary>
        /// This method is hit after the scan process has been completed to start the process of acquiring the image.
        /// </summary>
        /// <param name="sender"> Vintasoft TWAIN Device </param>
        /// <param name="e"> The image processing event. Tracks progress and canceling events </param>
        private void device_ImageAcquiringProgress(object sender, ImageAcquiringProgressEventArgs e)
        {
            // set the progress bar value to the current completion level of the image acquisition.
            progressBar.Value = (int)e.Progress;
        }

        /// <summary>
        /// Hit once the device_ImageAcquiringProgress() is complete and the image is acquired.
        /// </summary>
        /// <param name="sender"> Vintasoft TWAIN Device </param>
        /// <param name="e"> Holds the image and it's properties </param>
        private void device_ImageAcquired(object sender, ImageAcquiredEventArgs e)
        {
            // Transform the acquired image into a Bitmap variable.
            Bitmap acquiredImage = new Bitmap(e.Image.GetAsBitmap());
            
            // Method call to process the bitmap
            ProcessScan(acquiredImage);
            
        }

        /// <summary>
        /// Device is running in async mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void device_AsyncEvent(object sender, DeviceAsyncEventArgs e)
        {
            switch (e.DeviceEvent)
            {
                case DeviceEventId.PaperJam:
                    MessageBox.Show("Paper is jammed.");
                    break;

                case DeviceEventId.CheckDeviceOnline:
                    MessageBox.Show("Check that device is online.");
                    break;

                case DeviceEventId.CheckBattery:
                    MessageBox.Show(string.Format("DeviceEvent: Device={0}, Event={1}, BatteryMinutes={2}, BatteryPercentage={3}",
                        e.DeviceName, e.DeviceEvent, e.BatteryMinutes, e.BatteryPercentage));
                    break;

                case DeviceEventId.CheckPowerSupply:
                    MessageBox.Show(string.Format("DeviceEvent: Device={0}, Event={1}, PowerSupply={2}",
                        e.DeviceName, e.DeviceEvent, e.PowerSupply));
                    break;

                case DeviceEventId.CheckResolution:
                    MessageBox.Show(string.Format("DeviceEvent: Device={0}, Event={1}, Resolution={2}",
                        e.DeviceName, e.DeviceEvent, e.Resolution));
                    break;

                case DeviceEventId.CheckFlash:
                    MessageBox.Show(string.Format("DeviceEvent: Device={0}, Event={1}, FlashUsed={2}",
                        e.DeviceName, e.DeviceEvent, e.FlashUsed));
                    break;

                case DeviceEventId.CheckAutomaticCapture:
                    MessageBox.Show(string.Format("DeviceEvent: Device={0}, Event={1}, AutomaticCapture={2}, TimeBeforeFirstCapture={3}, TimeBetweenCaptures={4}",
                        e.DeviceName, e.DeviceEvent, e.AutomaticCapture, e.TimeBeforeFirstCapture, e.TimeBetweenCaptures));
                    break;

                default:
                    MessageBox.Show(string.Format("DeviceEvent: Device={0}, Event={1}",
                        e.DeviceName, e.DeviceEvent));
                    break;
            }

            // if device is enabled or transferring images
            if (_currentDevice.State >= DeviceState.Enabled)
                return;

            // close the device
            _currentDevice.Close();
        }

        /// <summary>
        /// Scan failed event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void device_ScanFailed(object sender, ScanFailedEventArgs e)
        {
            // show error message
            MessageBox.Show(e.ErrorString, "Scan failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Scan session and image acquisition and processing events have finished.
        /// </summary>
        /// <param name="sender"> Vintasoft TWAIN Device </param>
        /// <param name="e"> Application Event Arguments - Empty </param>
        private void device_ScanFinished(object sender, EventArgs e)
        {
            // close the device
            _currentDevice.Close();

            // process the scanned images.
            ProcessCompleted();

            // Clear program bar.
            hideProgressBar();
        }

        /// <summary>
        /// Initialize the connection to the connected scanner. If a connection is already
        /// established it will be closed and a new connection started. 
        /// </summary>
        private void setUpScanner()
        {
            // Open device manager, if not open.
            if (_deviceManager.State == DeviceManagerState.Closed)
                _deviceManager.Open();

            if (_currentDevice != null)
                // unsubscribe from the device events
                UnsubscribeFromDeviceEvents(_currentDevice);

            // Get and set the current device
            Device device = _deviceManager.DefaultDevice;
            _currentDevice = device;

            // subscribe to the device events
            SubscribeToDeviceEvents(_currentDevice);

            // set the image acquisition parameters
            _currentDevice.ShowUI = useUICheckBox.Checked;
            _currentDevice.ShowIndicators = showProgressIndicatorUICheckBox.Checked;
            _currentDevice.ModalUI = false;
            _currentDevice.DisableAfterAcquire = false;
            _currentDevice.TransferMode = TransferMode.Memory;

            try
            {
                // open the device
                _currentDevice.Open();
            }
            catch (Vintasoft.Twain.TwainException ex)
            {
                // specify that image acquisition is finished
                //_isImageAcquiring = false;
                MessageBox.Show("Error with scanner. Is " + _currentDevice.Info.ProductName +
                    " turned on?\n\n" + ex.Message, "TWAIN device error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            // set device capabilities
            // unit of measure
            if (_currentDevice.UnitOfMeasure != UnitOfMeasure.Inches)
                _currentDevice.UnitOfMeasure = UnitOfMeasure.Inches;

            // resolution
            if (ckBoxLowResolution.Checked)
                _currentDevice.Resolution = new Resolution(400, 400);
            else
                _currentDevice.Resolution = new Resolution(600, 600);

            // Enable the automatic document feeder if the device supports that feature and
            // the user has indicated that it should be used.
            if (_defaultSetting.CanUseDocumentFeeder && useAdfCheckBox.Checked)
            {
                _currentDevice.DocumentFeeder.Enabled = true;
                // If we are going to use the ADF ensure a document is loaded
                // (if the device is able to detect it)
                if (_currentDevice.DocumentFeeder.PaperDetectable && 
                    !_currentDevice.DocumentFeeder.Loaded)
                {
                    MessageBox.Show("No Document loaded in Auto Feeder. Please load document " +
                        "into auto feeder or flatbed and try again.\n\n", "Automatic Document" +
                        " Feeder", MessageBoxButtons.OK);
                    return;
                }
            }
            
            // Duplex (double sided page scanning) is set by the "Use Duplex" checkbox.
            // If the device supports duplex scanning, and the user has indicated that it should
            // be used attempt to enable it.
            if (_currentDevice.DocumentFeeder.Enabled && _defaultSetting.CanUseDuplex)
            {
                _currentDevice.DocumentFeeder.DuplexEnabled = useDuplexCheckBox.Checked;
            }
            // if device supports asynchronous events
            if (_currentDevice.IsAsyncEventsSupported)
            {
                // enable all asynchronous events supported by device
                _currentDevice.AsyncEvents = _currentDevice.GetSupportedAsyncEvents();
            }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Ensure the scanning device is open and use the form fields to configure scanning
        /// options. 
        /// </summary>
        /// <param name="sender"> "Scan" button on Main Form </param>
        /// <param name="e"> Mouse events that may need to be handled </param>
        private void btnScanPage_Click(object sender, EventArgs e)
        {
            // If we are starting a new scanning session we want to ensure any files created in
            // past sessions are cleared.
            if (_currentDocument == null)
            {
                UtilityObj.DeleteFolder("Images");
                _scanSessionFileList.Clear();
                UtilityObj.CreateFolder("Images");
            }

            _image0 = null;
            showProgressBar();
            if (testCheckBox.Checked)
            {
                Bitmap image = UtilityObj.ReadFileAsImage(String.Concat(ConfigKeys.LOGPATH, "\\", "temp.bmp"));
                ProcessScan(image);
                // process the scanned images.
                ProcessCompleted();

                // Clear program bar.
                hideProgressBar();
            }
            else
            {
                try
                {
                    setUpScanner();
                }
                catch (TwainDeviceCapabilityException)
                {
                    MessageBox.Show("Scanning device is not compatible with the request.",
                        "TWAIN device error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }
                catch (Exception ex2)
                {
                    MessageBox.Show(ex2.Message);
                    return;
                }

                try
                {
                    // start image acquisition process
                    _currentDevice.Acquire();
                }
                catch (Vintasoft.Twain.TwainException ex)
                {
                    UtilityObj.WriteLog(UtilityObj.error, "Image acquisition error: " + ex);
                    MessageBox.Show(ex.Message, "TWAIN device", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            
        }

        private void btnRotateImg_Click(object sender, EventArgs e)
        {
            // TODO - currently this doesn't actually rotate the image it mirrors it?
            _scannedImageList[_currentImageIndex].Rotate();
            UpdateImageDisplay();
        }

        /// <summary>
        /// View the scanned pages as a PDF.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImagePDF_Click(object sender, EventArgs e)
        {
            // Nothing to view if no document
            if (_currentDocument == null)
                return;

            int updateCount = ((int)100 / _scannedImageList.Count);
            showProgressBar();
            Application.DoEvents();

            // TODO - investigate how to do this asynchronously
            // Create the PDF Document to open in default PDF viewing app
            PdfDocument pdf = PDFObj.ImageListToPdf(_scannedImageList, progressBar);

            _tempFileNameList.Add(PDFObj.DisplayPdf(pdf));
            hideProgressBar();

        }

        /// <summary>
        ///  Save the document to the database.
        /// </summary>
        /// <param name="sender"> Save Scan Button </param>
        /// <param name="e"> Events from user </param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            bool hitErr = false;
            string er = "While attempting to save the document the following issue(s) were hit:\n";
            PdfDocument pdf = null;

            // Validation
            if (_currentDocument == null)
                return;

            if (!AccessionNumberObj.CompareAccessionObsj(
                _currentDocument.AccessionNumber, frmBoxManagement.SelectedBox.AccessionNumber))
            {
                hitErr = true;
                er += "\nCurrent working box does not match Document Record Accession " +
                    "Number.\n    Please change the current working box or update the record " +
                    "in DRS\n";
            }

            if (_currentDocument.PageCount != _scannedImageList.Count)
            {
                hitErr = true;
                er += "\nNumber of pages scanned does not match the expected number of pages.\n";
            }

            if (hitErr)
            {
                er += "\n\nPlease resolve the issue(s) listed above before attempting to save.";
                MessageBox.Show(er, "Save Errors", MessageBoxButtons.OK);
                return;
            }

            // Show progress bar and disable form elements 
            showProgressBar();

            // TODO: This is discouraged in Microsoft Docs.
            Application.DoEvents();

            try
            {
                pdf = PDFObj.ImageListToPdf(_scannedImageList, progressBar);
            }
            catch (Exception ex)
            {
                UtilityObj.WriteLog(UtilityObj.error, "Unable to process images to PDF.\n" +
                    ex.ToString());
            }
            // TODO - This is discouraged in Microsoft Docs.
            Application.DoEvents();


            // Update the document.
            _currentDocument.PDFDocument = pdf;
            _currentDocument.ScannerId = Environment.UserName;
            _currentDocument.ScannedDate = DateTime.Now;

            try
            {
                // Update document. Flag set based on if there were any updates to the fields
                _currentDocument.UpdateInsert();
            }
            catch (ApplicationException ae)
            {
                MessageBox.Show("There was an error in the Saving process. Please try again, or " +
                    "if there have been multiple failures please contact support.\n" + ae.Message,
                    "Unable to Process Save");
            }
            catch (ArgumentException age)
            {
                MessageBox.Show(age.Message);
            }

            // Reset the document and display.
            ResetDocument();
            hideProgressBar();
            CloseReason = _closeAutoRefresh;
            CloseForm();
            return;
        }

        /// <summary>
        /// Cancel the scan.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelScan_Click(object sender, EventArgs e)
        {
            // Reset the document and clear the display.
            ResetDocument();
            CloseReason = _closeAutoRefresh;
            CloseForm();
            return;
        }

        /// <summary>
        /// Automated Document Feeder checkbox clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void useAdfCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // If adf, then allow duplex.
            if (useAdfCheckBox.Checked && _defaultSetting.CanUseDuplex)
                useDuplexCheckBox.Enabled = true;
            else
            {
                // otherwise ensure not checked or can be checked.
                useDuplexCheckBox.Enabled = false;
                useDuplexCheckBox.Checked = false;
            }
        }

        /// <summary>
        /// Fires when this forms becomes active.
        /// </summary>
        /// <param name="sender">Opening form event</param>
        /// <param name="e">Event Data</param>
        private void frmScanDocument_Activated(object sender, EventArgs e)
        {
            // Form is maximized.
            this.WindowState = FormWindowState.Maximized;

            // Reset the form to default state
            ResetDocument();

            // Scan button is set as the focus.
            btnScanPage.Focus();
        }

        /// <summary>
        /// Invoked when the user selects "delete image" button. Confirm and remove the image
        /// currently displayed. Once finished show the image that came before the deleted image.
        /// </summary>
        /// <param name="sender">Delete Image Button</param>
        /// <param name="e">Event data</param>
        private void btnDeleteImage_Click(object sender, EventArgs e)
        {
            // Confirm this image is to be deleted.
            if (MessageBox.Show("Are you sure you want to delete this page?", "Confirm Deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _scannedImageList.RemoveAt(_currentImageIndex);
                _currentImageIndex--;
                UpdateImageDisplay();
            }
        }

        #endregion

    }
}
