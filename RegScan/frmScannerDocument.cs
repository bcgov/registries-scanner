using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Utilities;
using Vintasoft.Twain;

namespace RegScan
{
    public partial class frmScannerDocument : Form
    {

        #region Fields

        /// <summary>
        /// Current active document.
        /// </summary>
        DocumentObj _currentDocument = null;

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

        #endregion
        public frmScannerDocument()
        {
            InitializeComponent();

            UtilityObj.CreateFolder(Path.GetTempPath() + "Images");

            // Set scanner defaults.
            _defaultSetting = new ScannerSettingObj();
            SetSettingValues();
            useAdfCheckBox_CheckedChanged(new object(), new EventArgs());

            // Create a path to where debugging logs should be stored. 
            string scannerDir = "c:\\scanner25";
            string scannerFile = "vstwain.log";
            string scannerPath = String.Concat(scannerDir, "\\", scannerFile);

            UtilityObj.CreateFolder(scannerDir);
            UtilityObj.CreateFile(scannerPath);

            // Set up debugging for the TWAIN SDK
            TwainEnvironment.EnableDebugging(scannerPath);
            TwainEnvironment.DebugLevel = DebugLevel.Debug;

            // create TWAIN device manager
            CreateTwainDeviceManager();
        }

        /// <summary>
        /// Using the default settings set by ScannerSettingObj init function, set the checkboxes
        /// at the top of the form.
        /// </summary>
        public void SetSettingValues()
        {
            useAdfCheckBox.Checked = _defaultSetting.UseDocumentFeeder;
            useDuplexCheckBox.Checked = _defaultSetting.UseDuplex;
            useUICheckBox.Checked = _defaultSetting.ShowTwainUI;
            showProgressIndicatorUICheckBox.Checked = _defaultSetting.ShowProgressIndicatorUI;
            ckBoxLowResolution.Checked = _defaultSetting.BlackAndWhiteCheckBox;
        }



        #region Scanning Session

        /// <summary>
        /// When the form is closed (by an error or user actions) this method is used to clean the
        /// temporary files, in app memory, and any connections to externam devices. 
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
            this.Dispose();
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
                    barCode = frmEnterBarCode.ManualBarcode(message);
                }
                // Call to DRS API for existing document records for this barcode.
                // There may be more than one document (known as versions)
                try
                {
                    List<DocumentObj> resp = DocumentObj.Find(barCode);
                    if (resp.Count >= 1)
                        doc = resp[0];
                }
                catch (ArgumentNullException ane)
                {
                    // This exception is thrown if the barcode is not passed into the controller
                    // correctly or if it is an empty string.
                    UtilityObj.WriteLog(UtilityObj.error, ane.ToString());
                    MessageBox.Show(ane.Message, "Unable to Process Request. Barcode can not be empty.");
                    // continue the process
                    doc = null;
                    checkBarcode = false;
                }
                catch (ArgumentException ae)
                {
                    // This type of exception is thrown when the accession number is not in an
                    // expected format. This message box will display the number to the user
                    // and warn them to verify it.
                    UtilityObj.WriteLog(UtilityObj.error, ae.ToString());
                    MessageBox.Show(ae.Message + "\nPlease copy the number listed for " +
                        "verification as it will not be displayed in the application.");
                    // continue the process
                    doc = null;
                    checkBarcode = false;
                }
                catch (Exception e)
                {
                    string msg = "Hit error trying to find record for barcode: " + barCode;
                    // log the issue
                    UtilityObj.WriteLog(UtilityObj.error, msg +
                        System.Environment.NewLine + e.ToString());
                    // Ask the user if they would like to try again 
                    if (MessageBox.Show(msg + System.Environment.NewLine +
                        "Would you like to try again?", "Missing/ Not Found Barcode",
                        MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        // start loop over with a request for a new barcode
                        barCode = null;
                    }
                    else
                    {
                        // move on without a barcode
                        doc = null;
                        checkBarcode = false;
                    }
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
            
            // TODO -> better error handling here
            // Display the warning if there was some sort of error getting the document
            if (! string.IsNullOrEmpty(_currentDocument.Error))
                MessageBox.Show("Warning an error was found -> " +
                    _currentDocument.Error);

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
                    if (MessageBox.Show("Document with barcode " + barCode +
                        " has already been scanned. Do you want to create a new version?",
                        "Document Already Scanned", MessageBoxButtons.YesNo,
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
                    CloseForm();
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
                return;

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
            useAdfCheckBox.Enabled = true;
            useDuplexCheckBox.Enabled = true;
            useUICheckBox.Enabled = true;
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
            // Disable interaction with the form
            Enabled = false;

            // Hide scanning options when processing scan
            useUICheckBox.Enabled = false;
            useAdfCheckBox.Enabled = false;
            useDuplexCheckBox.Enabled = false;
            useUICheckBox.Enabled = false;
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

            // Disable/ enable buttons based on the current image index
            // Fist page - can't delete, no previous image, only go to next image if there is one
            if (_currentImageIndex == 0)
            {
                btnDeleteImage.Enabled = false;
                btnPrevImage.Enabled = false;
                if (totalScanned > 1)
                    btnlNextImage.Enabled = true;
                else
                    btnlNextImage.Enabled = false;
            }
            // Last page - can be deleted, can go to previous image, no next image
            else if (_currentImageIndex == totalScanned - 1)
            {
                btnDeleteImage.Enabled = true;
                btnPrevImage.Enabled = true;
                btnlNextImage.Enabled = false;
            }
            // This will be the default for any page that isnt the last or first
            // Can be deleted and can navigate to next or previous images
            else
            {
                btnDeleteImage.Enabled = true;
                btnPrevImage.Enabled = true;
                btnlNextImage.Enabled = true;
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
            maskSeqNumber.Text = "";
            txtFilingDate.Text = "";
            maskSchNumber.Text = "";
            maskBoxNumber.Text = "";
            txtDocumentNotes.Text = "";

            // Disable Accession Number fields and notes
            maskSeqNumber.Enabled = false;
            maskSchNumber.Enabled = false;
            maskBoxNumber.Enabled = false;
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
            btnlNextImage.Enabled = false;

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
        /// Called if a record is found for the scanned document.
        /// Form fields will be populated with the document's properties.
        /// </summary>
        protected void SetForm()
        {
            // Document Record Details
            txtBarCode.Text = _currentDocument.BarCode;
            txtLegalEntityKey.Text = _currentDocument.LegalEntityKey;
            txtIndexer.Text = _currentDocument.Owner;
            txtFilingDate.Text = _currentDocument.ConsumerFilingDateString;
            txtPagesInDocument.Text = _currentDocument.PageCount.ToString();

            // Record Classification
            txtDocumentClass.Text = _currentDocument.DocumentClass;
            txtDocumentType.Text = string.Concat(
                _currentDocument.DocumentType, " -> ", _currentDocument.DocTypeDesc);

            // Accession Number
            maskSeqNumber.Enabled = true;
            maskSchNumber.Enabled = true;
            maskBoxNumber.Enabled = true;
            maskSeqNumber.Text = _currentDocument.SequenceNumberString;
            maskSchNumber.Text = _currentDocument.ScheduleNumberString;
            maskBoxNumber.Text = _currentDocument.BoxNumberString;

            // Document Notes / Description
            txtDocumentNotes.Enabled = true;
            txtDocumentNotes.Text = _currentDocument.Description;
        }
        #endregion

        #region Scanner Methods

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
                device.ImageAcquiringProgress += new EventHandler<ImageAcquiringProgressEventArgs>(device_ImageAcquiringProgress);
                device.ImageAcquired += new EventHandler<ImageAcquiredEventArgs>(device_ImageAcquired);
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
            device.ImageAcquiringProgress -= new EventHandler<ImageAcquiringProgressEventArgs>(device_ImageAcquiringProgress);
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
        /// Initialize the connection to the conencted scanner. If a connection is already
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

            // Use the "Use Automatic Document Feeder" checkbox to set if the ADF if used.
            // NOTE - There could be a test implemented here to check if the device supports
            //     an ADF and if not, show a warning to the user that it can not be used.
            _currentDevice.DocumentFeeder.Enabled = useAdfCheckBox.Checked;

            // Duplex (double sided page scanning) is set by the "Use Duplex" checkbox.
            // If the device does not support duplex scanning, then this will fail and be
            // caught by the exception. 
            // NOTE - currently the catch is not handling the scenario instead just buffing the
            //    error. Logs/ warnings to user should be shown. There could also be a check
            //    before attempting to set the value to determine support. The device currently
            //    does not support this feature and the catch is always hit.
            if (_currentDevice.DocumentFeeder.Enabled)
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

        private void btnRotateImg_Click(object sender, EventArgs e)
        {
            // TODO - currently this doesnt actually rotate the image it mirrors it?
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
            bool descChange = false;
            bool accNChange = false;
            PdfDocument pdf = null;

            // Validation
            if (_currentDocument == null)
                return;

            if (_currentDocument.PageCount != _scannedImageList.Count)
            {
                var usr_rsp = MessageBox.Show("Number of pages scanned does not match the " +
                    "expected number of pages. Do you still wish to save?", "Page Mismatch",
                    MessageBoxButtons.YesNo);
                if (usr_rsp == System.Windows.Forms.DialogResult.No)
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

            // Get the form fields we want to update
            // If the description field changes we can use one endpoint
            // If it isnt updated we have to use a different endpoint.
            if (string.Equals(_currentDocument.Description ?? string.Empty, 
                txtDocumentNotes.Text, StringComparison.Ordinal))
            {
                descChange = true;
                _currentDocument.Description = txtDocumentNotes.Text;
            }

            try
            {
                // If any of the fields are missing values
                if (string.IsNullOrEmpty(maskSeqNumber.Text) ||
                    string.IsNullOrEmpty(maskSchNumber.Text) ||
                    string.IsNullOrEmpty(maskBoxNumber.Text))
                    throw new FormatException();
                // or if there is an error trying to parse them as ints
                var seqNumber = Int32.Parse(maskSeqNumber.Text);
                var schNumber = Int32.Parse(maskSchNumber.Text);
                var boxNumber = Int32.Parse(maskBoxNumber.Text);

                // we only need to update the values if there were changes made
                if (seqNumber != _currentDocument.SequenceNumber ||
                    schNumber != _currentDocument.ScheduleNumber ||
                    boxNumber != _currentDocument.BoxNumber)
                {
                    _currentDocument.SequenceNumber = seqNumber;
                    _currentDocument.ScheduleNumber = schNumber;
                    _currentDocument.BoxNumber = boxNumber;
                    accNChange = true;
                }

            }
            // Catch the formatting error
            catch (FormatException ex)
            {
                MessageBox.Show("Unable to parse one of the form fields. Please verify all fields and try again.", "Unable to Update");
                UtilityObj.WriteLog(UtilityObj.error, "Couldn't process form fields to int.\n" + ex.ToString());
                hideProgressBar();
                return;
            }

            try
            {
                _currentDocument.UpdateInsert(descChange || accNChange);
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
            CloseForm();
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
            CloseForm();
        }

        /// <summary>
        /// Automated Document Feeder checkbox clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void useAdfCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // If adf, then allow duplex.
            if (useAdfCheckBox.Checked)
                useDuplexCheckBox.Enabled = true;
            else
            {
                // otherwise insure not checked or can be checked.
                useDuplexCheckBox.Enabled = false;
                useDuplexCheckBox.Checked = false;
            }
        }

        /// <summary>
        /// Fires when this forms becomes active.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmScanDocument_Activated(object sender, EventArgs e)
        {
            // Form is maximized.
            this.WindowState = FormWindowState.Maximized;

            // Reset the form to default state
            ResetDocument();

            // Scan button is set as the focus.
            btnScanPage.Focus();
        }

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
