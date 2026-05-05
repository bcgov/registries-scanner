using AsyncRequests;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
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
        /// Current active batch number.
        /// </summary>
        BatchObj _currentBatchId = null;

        /// <summary>
        /// Options that control functions of application.
        /// </summary>
        //private OptionsObj _options = null;

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
        /// image0 is the first page of the document and possibly contains a barcode.
        /// </summary>
        private Bitmap image0;

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

            UtilityObj.createFolder("Images");

            // Set scanner defaults.
            _defaultSetting = new ScannerSettingObj();
            SetSettingValues();
            useAdfCheckBox_CheckedChanged(new object(), new EventArgs());

            // Load combo boxes
            cBoxOrientation.Items.Add(PdfSharp.PageOrientation.Landscape.ToString());
            cBoxOrientation.Items.Add(PdfSharp.PageOrientation.Portrait.ToString());
            cBoxOrientation.SelectedIndex = -1;

            cBoxPageSize.Items.Add(PdfSharp.PageSize.Letter.ToString());
            cBoxPageSize.Items.Add(PdfSharp.PageSize.Legal.ToString());
            cBoxPageSize.SelectedIndex = -1;

            // Create a path to where debugging logs should be stored. 
            string scannerDir = "c:\\scanner25";
            string scannerFile = "vstwain.log";
            string scannerPath = String.Concat(scannerDir, "\\", scannerFile);

            UtilityObj.createFolder(scannerDir);
            UtilityObj.createFile(scannerPath);

            // Set up debugging for the TWAIN SDK
            TwainEnvironment.EnableDebugging(scannerPath);
            TwainEnvironment.DebugLevel = DebugLevel.Debug;

            // create TWAIN device manager
            CreateTwainDeviceManager();
        }

        public void SetSettingValues()
        {
            useAdfCheckBox.Checked = _defaultSetting.UseDocumentFeeder;
            useDuplexCheckBox.Checked = _defaultSetting.UseDuplex;
            useUICheckBox.Checked = _defaultSetting.ShowTwainUI;
            showProgressIndicatorUICheckBox.Checked = _defaultSetting.ShowProgressIndicatorUI;
            ckBoxLowResolution.Checked = _defaultSetting.BlackAndWhiteCheckBox;
        }

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
            catch { }
        }

        #region Scanning Session
        /// <summary>
        /// Process the scanned image as a bitmap.
        /// </summary>
        /// <param name="_Image"> Bitmap image from scan process </param>
        private void ProcessScan(Bitmap _Image)
        {
            UtilityObj.writeLog("ProcessScan: Saving Scan");

            // Set the first image to image0 for barcode scanning and display purposes.
            // NOTE - It may be possible to use _Image and image0 interchangeably and eliminate the need for image0.
            if (image0 == null)
            {
                image0 = _Image;
            }

            string fileName = "Images\\Bitmap_image" + Convert.ToString(_scanSessionFileList.Count) + ".bmp";
            UtilityObj.saveImageAsFile(fileName, _Image);

            // Save the created filename to a list of scanned files for this session.
            _scanSessionFileList.Add(fileName);

            UtilityObj.writeLog("Scan List size: " + Convert.ToString(_scanSessionFileList.Count));
        }

        /// <summary>
        /// Handles making a request to the user to manually enter a barcode and returns the entered value.
        /// </summary>
        /// <param name="message"> String displayed in the message box </param>
        /// <returns> string of characters entered by the user </returns>
        private string ManualBarcode(string message)
        {
            string enteredBarcode = "";

            // Display window to user.
            if (MessageBox.Show(message, "Missing/ Not Found Barcode", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                // User has indicated they wish to enter a barcode manually. Display a new form.
                var barCodeString = new BarCodeString();
                var frm = new frmEnterBarCode(barCodeString);
                frm.ShowDialog();
                enteredBarcode = barCodeString.BarCode;
            }

            return enteredBarcode;
        }

        /// <summary>
        /// Once the scan is complete process all scanned images and save to a local folder.
        /// Pages are checked for barcodes and any existing document records. The scans are then
        /// passed along to be shown to the user.
        /// </summary>
        private void ProcessCompleted()
        {
            UtilityObj.writeLog("Scanning finished");

            // Enable the form.
            Enabled = true;

            // If there is no scanned image (user might have clicked the close button)
            if (_scanSessionFileList.Count == 0)
                return;

            UtilityObj.writeLog("Fix Checking for barcode");
            // Scan for Barcodes
            var barcodes = BarCodeObj.Scan(image0);

            // What follows here is a lot of if else statements. I believe breaking the components
            // out into methods then only using this as the control to the flow.
            //    Components: get barcode, get record, handle versions, display document
            // This could also be accomplished by drawing out the process before rewriting.

            // IF we have a new document.
            if (_currentDocument == null)
            {
                UtilityObj.writeLog("Fix Current Doc is null");

                // IF no barcodes were returned from BarCodeObj.Scan() 
                string barCode = "";
                if (barcodes.Count == 0)
                {
                    UtilityObj.writeLog("add barcode manually");
                    // Display the image.
                    SetImage(image0);

                    // Ask if a barcode should be entered manually.
                    barCode = ManualBarcode(
                        "No barcode found. Do you want to manually enter the barcode?");
                }
                else
                {
                    UtilityObj.writeLog("Fix barcode found=" + barcodes[0].ToString());
                    // Assume the first bar code found is the correct one.
                    // NOTE - If we are making this assumption BarcodeObj.Scan()
                    // can be optimized to only find one barcode. 
                    barCode = barcodes[0].ToString();
                }

                UtilityObj.writeLog("Fix Set List of Docs");
                // List to hold the documents.
                List<DocumentObj> docs = null;

                // If the barcode was set in the checks above
                if (!string.IsNullOrEmpty(barCode))
                {
                    UtilityObj.writeLog("Fix Barcode not null");
                    bool documentsFound = false;
                    // Unclear what 'neos' means. Used here to denote that API calls need to be
                    // made to get a record associated with the found barcode.
                    bool neos = true;
                    while (neos)
                    {
                        // Call to DRS API for existing document records for this barcode.
                        // There may be more than one document (known as versions)
                        docs = DocumentObj.Find(barCode);

                        // IF no documents were found
                        if (docs.Count == 0)
                        {
                            UtilityObj.writeLog("Fix No existing docs, SetImage(image0)");
                            SetImage(image0);

                            // Ask if a barcode should be entered manually.
                            string message = "No documents found for barcode " + barCode + 
                                ". Do you want to manually enter the barcode?";
                            barCode = ManualBarcode(message);

                            if (barCode == "")
                            {
                                // Move on to next step.
                                neos = false;
                            }
                        }
                        else
                        {                           
                            // Indicate document(s) found and move onto next step.
                            documentsFound = true;
                            neos = false;
                        }
                    }            

                    if (documentsFound)
                    {
                        UtilityObj.writeLog("Fix Set current doc to docs[0]");
                        // The first document is the latest and is the one that will be displayed
                        _currentDocument = docs[0];

                        // Display the warning if there was some sort of error getting the document
                        if (_currentDocument.Error != "")
                            MessageBox.Show("Warning an error was found -> " + 
                                _currentDocument.Error);
                                            
                        // Will only be true if it is an existing scan and
                        // new version is not required.
                        bool cancelScan = false;

                        // We have a record from DRS API that matches the barcode.
                        // This means we will be updating the record.
                        _currentDocument.UpdateRecord = true;

                        // IF document has already been scanned.
                        //if (_currentDocument.IsScanned)
                        if (_currentDocument.DocumentURL != "")
                        {
                            UtilityObj.writeLog("Document barcode already exists.");
                            // Turn off buttons for new box number.
                            btnNewBox.Enabled = false;

                            // Ask if this is a new version.
                            if (MessageBox.Show("Document with barcode " + barCode + 
                                " has already been scanned. Do you want to create a new version?", 
                                "Document Already Scanned", MessageBoxButtons.YesNo, 
                                MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                            {
                                // If a new version, then update the version number
                                // TODO - get previous version number from filename. Then add one.
                                // This will always be 0++ = 1
                                _currentDocument.VersionNumber++;
                                // We don't want to clear the metadata from the current document
                                // just indicate that there is a new version of the document image
                                // _currentDocument.SetToNew();
                            }
                            else
                                // Cancel this scan if a new version is not required.
                                cancelScan = true;
                        }
                        else
                        {
                            // Check for box full and display a message if it is full.
                            if ((_currentDocument.PageCount + _currentDocument.PagesInBox) > 
                                    _defaultSetting.MaxPagesInBox)
                                MessageBox.Show("Warning - Current Box will exceed limit of " +
                                    _defaultSetting.MaxPagesInBox.ToString() +
                                    " pages, after these pages are added");
                        }

                        // IF scan is to be cancelled
                        if (cancelScan)
                        {
                            // Set current document back to null.
                            _currentDocument = null;
                            UtilityObj.deleteFolder("Images");
                        }
                        else
                        {
                            // IF the current batch id is null
                            // TODO - look into what this should actually be doing
                            // This is not set on app start. Will always be null on first scan.
                            if (_currentBatchId == null)
                            {
                                // If the current document's batch is assigned.
                                if (_currentDocument.BatchId != 0)
                                {
                                    // Create a batch object and assign current's document's values
                                    _currentBatchId = new BatchObj();
                                    _currentBatchId.AccessionNumber = 
                                        _currentDocument.AccessionNumber;
                                    _currentBatchId.BatchId = _currentDocument.BatchId;
                                }
                                else
                                {
                                    // Get the next batch id for this accession and assign
                                    // it to the document.
                                    _currentBatchId = BatchObj.GetNextBatchId(
                                        _currentDocument.AccessionNumber);
                                    _currentDocument.BatchId = _currentBatchId.BatchId;
                                }
                            }
                            else
                            {
                                UtilityObj.writeLog("Fix No batchid");
                                // IF the accession number has changed,
                                // get the next batch id and assign it.
                                if (_currentBatchId.AccessionNumber != 
                                        _currentDocument.AccessionNumber)
                                    _currentBatchId = BatchObj.GetNextBatchId(
                                        _currentDocument.AccessionNumber);

                                // If the current document's batch is not assigned.
                                if (_currentDocument.BatchId == 0)
                                    _currentDocument.BatchId = _currentBatchId.BatchId;
                            }

                            UtilityObj.writeLog("Fix Display image");
                            // Display the document.
                            SetForm();

                            // Transfer images to our list of images for this document.
                            _scannedImageList.Clear();

                            // FOREACH of the pages scanned in this session.
                            foreach (var imageFile in _scanSessionFileList)
                            {
                                Bitmap image = UtilityObj.readFileAsImage(imageFile);
                                // go to next page if this one cant be read
                                if (image == null) { continue; }
                                // Calculate the page size and add the image to the scanned list.
                                var pageSize = ImageObj.GetPageSize(image.Width, image.Height);

                                _scannedImageList.Add(
                                    new ImageObj(image, 
                                        PdfSharp.PageOrientation.Portrait, pageSize));
                                image = null;
                            }

                            UtilityObj.writeLog("Fix SetImageNav");
                            // Display the first document.
                            _currentImageIndex = 0;
                            SetImageNav();
                        }                        
                    }
                    // NOTE Create a new record for the scanned image logic could be added here
                    // _currentDocument.SetToNew();
                }
                else
                {                    
                    // Clear Image
                    imageBox.Image = null;
                }                
            }

            // This else is entered when there is at least one page that has already been scanned
            // in this session.
            else
            {
                // IF we have a barcode and it doesn't match the previous barcode
                if (barcodes.Count != 0 && barcodes[0].ToString() != _currentDocument.BarCode)
                {
                    // Display a warning message.
                    string title = "Warning: Double Barcode";
                    string msg = "A barcode was found on this page.\n.";
                    string comp = "First barcode: " + barcodes[0].ToString() + 
                                  "\nSecond Barcode: " + _currentDocument.BarCode;
                    MessageBox.Show(msg + comp, title);
                }

                // Save the images.
                foreach (var imageFile in _scanSessionFileList)
                {
                    var image = UtilityObj.readFileAsImage(imageFile);
                    // go to next page if this one cant be read
                    if (image == null) { continue; }
                    _currentImageIndex++;
                    var pageSize = ImageObj.GetPageSize(image.Width, image.Height);
                    _scannedImageList.Add(
                        new ImageObj(image, PdfSharp.PageOrientation.Portrait, pageSize));
                }

                SetImageNav();

            }            
        }

        #endregion

        #region Image Display.

        /// <summary>
        ///  Sets the image on the image navigation panel.
        /// </summary>
        private void SetImageNav()
        {
            if (_currentImageIndex == -1)
                return;
            UpdateImageDisplay();
        }

        /// <summary>
        ///  Display next image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNextImage_Click(object sender, EventArgs e)
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
        private void btnPreviousImage_Click(object sender, EventArgs e)
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
            if (_currentImageIndex > -1 && _currentImageIndex < _scannedImageList.Count)
            {
                // we have already read in all the images, select the one from the index to display
                var image = _scannedImageList[_currentImageIndex].Image;
                // display error if this one cant be read
                if (image == null) 
                { 
                    MessageBox.Show("Error loading image for page " + Convert.ToString(_currentImageIndex + 1));
                    return;
                }
                SetImage(image);
                SetOrientation(_scannedImageList[_currentImageIndex].Orientation.ToString());
                SetPageSize(_scannedImageList[_currentImageIndex].PageSize.ToString());
            }

            lbPagesScanned.Text = "Pages Scanned: " + _scannedImageList.Count().ToString();
            string display = (_currentImageIndex + 1).ToString();
            lbDisplayImage.Text = display + " of " + _scannedImageList.Count.ToString();
            if (_currentImageIndex != 0)
                btnDeleteImage.Visible = true;
            else
                btnDeleteImage.Visible = false;

            btnViewAsPDF.Enabled = true;
            btnSharpen.Enabled = true;
            btnPrintBatchLabel.Enabled = true;
            btnNewBox.Enabled = true;
            cBoxOrientation.Enabled = true;
            cBoxPageSize.Enabled = true;
        }

        #endregion
        #region click events.
        private void btnRotate_Click(object sender, EventArgs e)
        {
            // Rotate current image and 
            _scannedImageList[_currentImageIndex].Rotate();
            UpdateImageDisplay();
        }

        /// <summary>
        /// View the scanned pages as a PDF.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewAsPDF_Click(object sender, EventArgs e)
        {
            // Nothing to view if no document
            if (_currentDocument == null)
                return;

            // Create a PDF document.
            var pdf = new PdfDocument();
            int updateCount = ((int)100 / _scannedImageList.Count) / 2;
            progressBar.Value = updateCount;
            progressBar.Visible = true;
            Application.DoEvents();

            UtilityObj.writeLog(Convert.ToString(_scannedImageList.Count) + " btnViewAsPDF_Click Scanner  Objects");

            // FOREACH image create a new page.
            foreach (var bp in _scannedImageList)
            {
                // Create a new page and add in the image.
                var pdfPage = new PdfPage();
                pdfPage.Size = bp.PageSize;
                pdfPage.Orientation = bp.Orientation;
                pdf.AddPage(pdfPage);
                var xgr = XGraphics.FromPdfPage(pdfPage);
                var img = XImage.FromGdiPlusImage(bp.Image);
                xgr.DrawImage(img, 0, 0);

                // Update progress bar
                txtMessage.Text += "\r\n" + "Page Size: " + pdfPage.Size.ToString();
                progressBar.Value += updateCount;
                Application.DoEvents();

            }

            _tempFileNameList.Add(PDFObj.DisplayPdf(pdf));
            progressBar.Visible = false;

        }

        /// <summary>
        /// Method called when a user selects the 'Save' button on the main form.
        /// Given an image create a new page and 'draw' the image to that page. The page is added
        /// to the given PDF document. We should verify the form and its contents, then make a
        /// call to push the document and all related metadata to the DRS API.
        /// </summary>
        /// <param name="pdf">PDF Document to add image to</param>
        /// <param name="scannedImage">Image to be added to PDF document</param>
        private void imageToPDF(PdfDocument pdf, ImageObj scannedImage)
        {
            // Create a new page and with the document scans specifications
            var pdfPage = new PdfPage();
            pdfPage.Size = scannedImage.PageSize;
            pdfPage.Orientation = scannedImage.Orientation;

            // Add the new page to the PDF document
            pdf.AddPage(pdfPage);

            // Make the PDF Page a drawable canvas
            var xgr = XGraphics.FromPdfPage(pdfPage);
            // Turn the scanned document into an XImage
            var img = XImage.FromGdiPlusImage(scannedImage.Image);

            // Get the PDF Pages 
            double pageWidth = pdfPage.Width.Point;
            double pageHeight = pdfPage.Height.Point;

            // Maintain aspect ratio, fit within page, centered
            // Get the shape of the scanned document and PDF page
            double imgAspect = (double)scannedImage.Image.Width / scannedImage.Image.Height;
            double pageAspect = pageWidth / pageHeight;

            double drawWidth, drawHeight, drawX, drawY;
            // if the images' shape is larger than the pages' shape 
            if (imgAspect > pageAspect)
            {
                // limit the width to the width of the page
                drawWidth = pageWidth;
                // the height is set to the width of the page / the images' shape
                //  -> Pw / (Iw / Ih) -> Pw * Ih / Iw
                //  -> The images aspect ratio scaled to the pages width
                drawHeight = pageWidth / imgAspect;
                // 
                drawX = 0;
                // 
                drawY = (pageHeight - drawHeight) / 2;
            }
            // if the pages' shape is larger than (or equal to) the images' shape 
            else
            {
                // set the height to the pages height
                drawHeight = pageHeight;
                // set the width to the height of the page * the images' shape
                //  -> Ph * (Iw / Ih) -> Ph * Ih / Iw
                //  -> The images aspect ratio scaled to the pages height
                drawWidth = pageHeight * imgAspect;
                //
                drawX = (pageWidth - drawWidth) / 2;
                //
                drawY = 0;
            }

            xgr.DrawImage(img, drawX, drawY, drawWidth, drawHeight);
           
        }

        /// <summary>
        ///  Save the document to the database.
        /// </summary>
        /// <param name="sender"> Save Scan Button </param>
        /// <param name="e"> Events from user </param>
        private void btnSave_Click(object sender, EventArgs e)
        {

            // Validation
            if (_currentDocument == null)
                return;

            if (_currentDocument.PageCount != _scannedImageList.Count)
            {
                var usr_rsp = MessageBox.Show("Number of pages scanned does not match the " +
                    "expected number of pages. Do you still wish to save?", "Page Mismatch",
                    MessageBoxButtons.YesNo);
                if ( usr_rsp == System.Windows.Forms.DialogResult.No)
                    return;
            }

            // Create a PDF document.
            var pdf = new PdfDocument();

            // Percentage updated based on number of images to process
            int updateCount = (int)100 / _scannedImageList.Count;
            progressBar.Value = 0;
            progressBar.Visible = true;

            // This is discouraged in Microsoft Docs.
            Application.DoEvents();

            UtilityObj.writeLog(_scannedImageList.Count.ToString() + " _scannedImageList Objects");

            // FOREACH image convert to a PDF page and add to the PDF document
            foreach (var bp in _scannedImageList)
            {
                try
                {
                    imageToPDF(pdf, bp);
                }
                catch (Exception ex)
                {
                    string img_num = _scannedImageList.IndexOf(bp).ToString() +
                                     " of " + _scannedImageList.Count();
                    UtilityObj.writeLog("Unable to process image " + img_num + " to PDF page.\n" +
                                         ex.ToString());
                }
                
                // Update progress bar
                progressBar.Value += updateCount;
                // This is discouraged in Microsoft Docs.
                Application.DoEvents();
            }

            // Update the document.
            _currentDocument.PDFDocument = pdf;
            _currentDocument.Description = txtDocumentDescription.Text;
            _currentDocument.PageCount = _scannedImageList.Count;
            _currentDocument.ScannerId = Environment.UserName;
            _currentDocument.ScannedDate = DateTime.Now;
            _currentDocument.ImageList = _scannedImageList.Select(i => i.Image).ToList();
            _currentDocument.UpdateInsert();

            // Reset the document and display.
            ResetDocument();
            progressBar.Visible = false;

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
        /// Create a new box number.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewBox_Click(object sender, EventArgs e)
        {
            CreateNewBoxNumber();
        }

        /// <summary>
        /// Print the batch label.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintBatchLabel_Click(object sender, EventArgs e)
        {

            // IF no document or batch, then don't proceed.
            if (_currentDocument == null || _currentBatchId == null)
                return;

            // Display the batch form.
            var frm = new frmBatchPrint(_currentBatchId, true);
            frm.ShowDialog();

            // If the batch ID was changed
            if (_currentBatchId.BatchId != int.Parse(txtBatchNumber.Text))
            {
                // Then update on the form and in the document object.
                txtBatchNumber.Text = _currentBatchId.BatchId.ToString();
                _currentDocument.BatchId = _currentBatchId.BatchId;
            }
        }

        /// <summary>
        ///  Delete the current image from teh list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteImage_Click(object sender, EventArgs e)
        {
            // Confirm this image is to be deleted.
            if (MessageBox.Show("Are you sure you want to delete this page?", "Confirm Deletion", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                _scannedImageList.RemoveAt(_currentImageIndex);
                _currentImageIndex--;
                UpdateImageDisplay();
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

            // Scan button is set as the focus.
            btnScanPage.Focus();
        }

        /// <summary>
        /// The orientation was changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cBoxOrientation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBoxOrientation.Text == PdfSharp.PageOrientation.Landscape.ToString())
                _scannedImageList[_currentImageIndex].Orientation = PdfSharp.PageOrientation.Landscape;
            else if (cBoxOrientation.Text == PdfSharp.PageOrientation.Portrait.ToString())
                _scannedImageList[_currentImageIndex].Orientation = PdfSharp.PageOrientation.Portrait;
        }

        /// <summary>
        /// The Page size was changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cBoxPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBoxPageSize.Text == PdfSharp.PageSize.Letter.ToString())
                _scannedImageList[_currentImageIndex].PageSize = PdfSharp.PageSize.Letter;
            else if (cBoxPageSize.Text == PdfSharp.PageSize.Legal.ToString())
                _scannedImageList[_currentImageIndex].PageSize = PdfSharp.PageSize.Legal;
        }
        #endregion

        #region house keeping methods

        /// <summary>
        /// Reset the document.
        /// NOTE - This is not necessary. An new instance of this class can be used instead of
        /// attempting to clear an old version. 
        /// </summary>
        private void ResetDocument()
        {
            // Clear document.
            _currentDocument = null;
            _scannedImageList.Clear();
            _currentImageIndex = -1;

            // Clear the document.
            txtBarCode.Text = "";
            txtDocumentId.Text = "";
            txtLegalEntityKey.Text = "";
            txtOwner.Text = "";
            txtDocumentDescription.Text = "";
            txtDocumentType.Text = "";
            txtVersionNumber.Text = "";
            txtPagesInDocument.Text = "";
            txtBatchNumber.Text = "";
            txtAccessionNumber.Text = "";
            txtPagesInBox.Text = "";
            imageBox.Image = null;
            heightLabel.Text = "";
            widthLabel.Text = "";
            lbDisplayImage.Text = "0 of 0";
            lbPagesScanned.Text = "Pages Scanned: 0";
            btnDeleteImage.Visible = false;
            btnNewBox.Enabled = false;
            btnPrintBatchLabel.Enabled = false;
            btnViewAsPDF.Enabled = false;
            btnSharpen.Enabled = false;
            cBoxOrientation.Enabled = false;
            cBoxOrientation.SelectedIndex = -1;
            cBoxPageSize.Enabled = false;

            // Delete any temporary FileNames.
            try
            {
                foreach (var fileName in _tempFileNameList)
                    File.Delete(fileName);
                _tempFileNameList.Clear();
            }
            catch { }

            txtMessage.Text = "";
            imageBox.Image = null;
        }

        /// <summary>
        ///  Set the current selected Orientation in the combo box.
        /// </summary>
        /// <param name="_Orientation"></param>
        private void SetOrientation(string _Orientation)
        {

            for (int i = 0; i < cBoxOrientation.Items.Count; i++)
            {
                if (cBoxOrientation.Items[i].ToString() == _Orientation)
                    cBoxOrientation.SelectedIndex = i;
            }
        }

        /// <summary>
        /// Set the current selected Page Size in the combo box.
        /// </summary>
        /// <param name="_PageSize"></param>
        private void SetPageSize(string _PageSize)
        {
            for (int i = 0; i < cBoxPageSize.Items.Count; i++)
            {
                if (cBoxPageSize.Items[i].ToString() == _PageSize)
                    cBoxPageSize.SelectedIndex = i;
            }
        }

        /// <summary>
        /// Create a new box number.
        /// </summary>
        private void CreateNewBoxNumber()
        {
            // Don't proceed if no current document.
            if (_currentDocument == null)
                return;

            // Create a copy of our box.
            var box = new BoxObj();
            BoxObj.CopyBox(_currentDocument.Box, box);

            // Make use of our existing form to create the new box.
            var frm = new frmBox();
            var status = frm.SetBoxNumber(box);
            if (status != "")
            {
                MessageBox.Show(status);
                frm.Dispose();
            }
            else
            {
                // Display the form.
                frm.ShowDialog();

                // IF a new box was created.
                if (!CompareBoxes(box, _currentDocument.Box))
                {
                    // Reset everything on this end.
                    _currentDocument.Box = box;
                    _currentBatchId.AccessionNumber = _currentDocument.AccessionNumber;
                    _currentBatchId.BatchId = 1;
                    _currentDocument.BatchId = _currentBatchId.BatchId;
                    txtBatchNumber.Text = _currentBatchId.BatchId.ToString();
                    txtAccessionNumber.Text = box.AccessionNumber;
                    txtPagesInBox.Text = box.PageCount.ToString();
                }
            }

        }

        /// <summary>
        /// Compare two boxes to see if they are the same
        /// </summary>
        /// <param name="_Box1"></param>
        /// <param name="_Box2"></param>
        /// <returns>False if they are not the same.</returns>
        private bool CompareBoxes(BoxObj _Box1, BoxObj _Box2)
        {
            bool result = false;

            if (_Box1.SequenceNumber == _Box2.SequenceNumber && _Box1.ScheduleNumber == _Box2.ScheduleNumber && _Box1.BoxNumber == _Box2.BoxNumber)
                result = true;

            return result;
        }

        /// <summary>
        /// Sets the image in the image box.
        /// </summary>
        /// <param name="_Image"></param>
        protected void SetImage(Bitmap _Image)
        {
            imageBox.SizeToFit = true;
            imageBox.Image = _Image;
            imageBox.SizeToFit = false;
        }

        /// <summary>
        /// Called if a record is found for the scanned document.
        /// Form fields will be populated with the document's properties.
        /// </summary>
        protected void SetForm()
        {
            txtBarCode.Text = _currentDocument.BarCode;
            txtDocumentId.Text = _currentDocument.DocumentId == "" ? "[not assigned]" : _currentDocument.DocumentId.ToString();
            txtLegalEntityKey.Text = _currentDocument.LegalEntityKey;
            txtOwner.Text = _currentDocument.Owner;
            txtDocumentDescription.Text = _currentDocument.Description;
            txtDocumentType.Text = _currentDocument.FQDocType;
            txtVersionNumber.Text = _currentDocument.VersionNumber.ToString();
            txtPagesInDocument.Text = _currentDocument.PageCount.ToString();
            txtBatchNumber.Text = _currentDocument.BatchId.ToString();
            txtAccessionNumber.Text = _currentDocument.AccessionNumberText;
            txtPagesInBox.Text = _currentDocument.PagesInBox.ToString();
        }
        #endregion



        #region  Image Zoom Event Handlers

        /// <summary>
        /// Image was scrolled.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageBox_Scroll(object sender, ScrollEventArgs e)
        {
            this.UpdateStatusBar();
        }

        /// <summary>
        /// Update the status bar for the image.
        /// </summary>
        private void UpdateStatusBar()
        {
            positionToolStripStatusLabel.Text = imageBox.AutoScrollPosition.ToString();
            imageSizeToolStripStatusLabel.Text = imageBox.GetImageViewPort().ToString();
            zoomToolStripStatusLabel.Text = string.Format("{0}%", imageBox.Zoom);
        }

        /// <summary>
        /// Image was zoomed in or out.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageBox_ZoomChanged(object sender, EventArgs e)
        {
            this.UpdateStatusBar();
        }

        /// <summary>
        /// Image was resized.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageBox_Resize(object sender, EventArgs e)
        {
            this.UpdateStatusBar();
        }

        #endregion  Event Handlers

        #region Scanning Session.

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
            catch
            {
                MessageBox.Show("Error, scanner not found. Is the scanner turned on?", "TWAIN device error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            // image acquisition must be canceled because application's form is closing
            //if (_cancelTransferBecauseFormIsClosing)
            //{
            //    // cancel image acquisition
            //    _currentDevice.CancelTransfer();
            //    return;
            //}

            // set the progress bar value to the current completion level of the image acquisition.
            progressBar.Value = (int)e.Progress;

            //UtilityObj.writeLog(Convert.ToString(progressBar.Value) + " device_ImageAcquiringProgress");

            // Catch when the process is complete and clear the progress bar.
            if (progressBar.Value == 100)
            {
                progressBar.Value = 0;
            }
        }

        /// <summary>
        /// Hit once the device_ImageAcquiringProgress() is complete and the image is acquired.
        /// </summary>
        /// <param name="sender"> Vintasoft TWAIN Device </param>
        /// <param name="e"> Holds the image and it's properties </param>
        private void device_ImageAcquired(object sender, ImageAcquiredEventArgs e)
        {
            // image acquisition must be canceled because application's form is closing
            //if (_cancelTransferBecauseFormIsClosing)
            //{
            //    // cancel image acquisition
            //    _currentDevice.CancelTransfer();
            //    return;
            //}

            // Transform the acquired image into a Bitmap.
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
            UtilityObj.writeLog("device_AsyncEvent");
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

            // specify that image acquisition is finished
            //_isImageAcquiring = false;

            // Clear program bar.
            progressBar.Visible = false;

            // process the scanned images.
            ProcessCompleted();
        }

        /// <summary>
        /// Called when user selects the 'Scan' button. Sets up and starts the scanning session.
        /// </summary>
        /// <param name="sender"> "Scan" button on Main Form </param>
        /// <param name="e"> Mouse events that may need to be handled </param>
        private void btnScanPage_Click(object sender, EventArgs e)
        {            
            UtilityObj.deleteFolder("Images");
            _scanSessionFileList.Clear();
            image0 = null;
            UtilityObj.createFolder("Images");

            progressBar.Visible = true;

            try
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

                    MessageBox.Show("Error with scanner. Is " + _currentDevice.Info.ProductName + " turned on?\n\n" + ex.Message, "TWAIN device error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                // If the device does not support duplex scanning, then this will fail and be caught by the exception. 
                // NOTE - currently the catch is not handling the scenario instead just buffing the error. Logs/ warnings to user should be shown.
                //    There could also be a check before attempting to set the value to determine support.
                //    The device currently does not support this feature and the catch is always hit.
                if (_currentDevice.DocumentFeeder.Enabled)
                {
                    _currentDevice.DocumentFeeder.DuplexEnabled = useDuplexCheckBox.Checked;
                }
            }
            catch (TwainDeviceCapabilityException)
            {
                MessageBox.Show("Scanning device is not compatible with the request.", "TWAIN device error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            catch(Exception ex2)
            {
                MessageBox.Show(ex2.Message);
                return;
            }

            UtilityObj.writeLog("Checking for asynchronous scanning...");
            // if device supports asynchronous events
            if (_currentDevice.IsAsyncEventsSupported)
            {
                try
                {
                    UtilityObj.writeLog("Device supports asynchronous scanning");
                    // enable all asynchronous events supported by device
                    _currentDevice.AsyncEvents = _currentDevice.GetSupportedAsyncEvents();
                }
                catch
                {
                }
            }


            try
            {
                UtilityObj.writeLog("Start image acquisition");

                // start image acquisition process
                _currentDevice.Acquire();

                UtilityObj.writeLog("End of image acquisition");
            }
            catch (Vintasoft.Twain.TwainException ex)
            {
                UtilityObj.writeLog("Image acquisition error: " + ex);
                MessageBox.Show(ex.Message, "TWAIN device", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        #endregion

    }
}
