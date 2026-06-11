using ApiScanner;
using Utilities;
using Newtonsoft.Json.Linq;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace RegScan
{
    public class DocumentObj
    {
        #region Properties and Constructors

        //DRS
        private string _documentURL;
        private string _documentClass;
        private string _documentServiceId;
        private string _documentTypeDescription;
        private DateTime _consumerFilingDate;
        private string _consumerIdentifier;
        private int _consumerReferenceId;

        private string _legalEntityKey;
        private string _documentTypeCode;
        private string _author;
        private string _description;
        private string _fileName;
        private bool _isScanned;
        private DateTime _createDateTime;

        // Scanning Information
        private int _barCode;
        private int _pageCount;
        private int _sequenceNumber;
        private int _scheduleNumber;
        private int _boxNumber;
        private int _batchId;
        private int _versionNumber;
        private string _owner;
        private string _scannerId;
        private DateTime _scannedDate;

        private Boolean _replaceRecordFlag = false;
       

        // Calculated.
        private BoxObj _boxObj = null;        

        private PdfDocument _pdfDocument = null;
        private List<Bitmap> _imageList = new List<Bitmap>();

        // Public variables other classes can use
        // Document Information Form
        public string BarCode { get { return _barCode.ToString(); } }
        public string Owner { get { return _owner; } }
        public string LegalEntityKey { get { return _legalEntityKey; } }
        public string ConsumerFilingDateString { 
            get { return _consumerFilingDate.ToString("MMMM dd, yyyy"); } }
        public int PageCount { get { return _pageCount; } set { _pageCount = value; } }
        public string DocumentClass { get { return _documentClass; } }
        public string DocumentType {  get { return _documentTypeCode; } }
        public string DocTypeDesc { get { return _documentTypeDescription; } }
        public string Description { get { return _description; } set { _description = value; } }
        public string DocumentURL { get { return _documentURL; } }
        public string DocumentServiceId { get { return _documentServiceId; } }
        // Accession Number Elements
        public string SequenceNumberString { 
            get { return _sequenceNumber.ToString().PadLeft(2, '0'); } }
        public string ScheduleNumberString {
            get { return _scheduleNumber.ToString().PadLeft(4, '0'); } }
        public string BoxNumberString {
            get { return _boxNumber.ToString().PadLeft(4, '0'); } }
        public int SequenceNumber {
            get { return _sequenceNumber; } set { _sequenceNumber = value; } }
        public int ScheduleNumber {
            get { return _scheduleNumber; } set { _scheduleNumber = value; } }
        public int BoxNumber {
            get { return _boxNumber; } set { _boxNumber = value; } }
        public string AccessionNumberString { 
            get { return string.Concat(_sequenceNumber.ToString().PadLeft(2, '0'), 
                                       _scheduleNumber.ToString().PadLeft(4, '0'), 
                                       _boxNumber.ToString().PadLeft(4, '0')); } }
        public string AccessionNumberText { 
            get { return string.Concat(_sequenceNumber.ToString().PadLeft(2, '0'), "-",
                                       _scheduleNumber.ToString().PadLeft(4, '0'), "-",
                                       _boxNumber.ToString().PadLeft(4, '0')); } }
        public long AccessionNumber { get { return long.Parse(AccessionNumberString); } }
        
        public bool IsScanned { get { return _isScanned; } }
        public int BatchId { get { return _batchId; } set { _batchId = value; } }
        public int VersionNumber { get { return _versionNumber; } set { _versionNumber = value; } }
        public string ScannerId { get { return _scannerId; } set { _scannerId = value; } }
        public DateTime ScannedDate { get { return _scannedDate; } set { _scannedDate = value; } }

        public BoxObj Box { get { return _boxObj; } set { _boxObj = value; } }

        public Boolean UpdateRecord { 
            get { return _replaceRecordFlag; } set { _replaceRecordFlag = value; } }

        // Calculated
        public PdfDocument PDFDocument { 
            get { return _pdfDocument; } set { _pdfDocument = value; } }
        public List<Bitmap> ImageList { get { return _imageList; } set { _imageList = value; } }

        public string Error = "";

        public DocumentObj()
        {
            _boxObj = new BoxObj();
        }

        #endregion

        #region dml

        /// <summary>
        /// Handles the logic of uploading the scanned document to the DRS API.
        /// </summary>
        /// <param name="fileName">Name to use for the document in DRS API</param>
        private void UploadImage(string fileName)
        {
            byte[] pdfBytes = PDFObj.ConvertPdfToByteArray(_pdfDocument);

            // TODO - we should not exit, we should show the error then handle it gracefully
            try
            {
                // upload the scanned image
                string uploadResp = DocumentApi.uploadDocument(
                    pdfBytes, fileName, _documentServiceId);
            }
            catch (Exception e)
            {
                MessageBox.Show("Scanned image failed to load into database. " +
                                "Current data for " + BarCode + " may be inaccurate.");
                UtilityObj.WriteLog(UtilityObj.error,
                    "Scanned document Image failed PUT of scanned image." + e.ToString());
                Environment.Exit(0);
            }
        }

        private void UpdateDocumentRecord(DocumentModel apiDocModel)
        { 
            try
            {
                // update the document record
                string updateResp = DocumentApi.updateDocumentRecord(
                    apiDocModel, _documentServiceId);
            }
            catch (Exception e)
            {
                MessageBox.Show("Unable to update document record." +
                                "Current data for " + BarCode + " may be inaccurate.");
                UtilityObj.WriteLog(UtilityObj.error,
                    "Scanned document Image failed PATCH to update document record.\n" +
                    e.ToString());
                Environment.Exit(0);
            }

        }

        /// <summary>
        /// Controls the flow of logic between inserting or updating a document record.
        /// If the _replaceFlag is true, (there is an existing document) 
        ///     -> Update: replace the existing document.
        /// If the _replaceFlag is false, (no existing document).
        ///     -> Insert: create a new record and post the document. Currently the scanning
        ///        application does not have the ability to create a new document record through
        ///        DRS API. This functionality could be added in the future.
        /// </summary>
        /// <param name="updateRecordFlag">
        /// Determines if the applicaiton will update the record and upload image. If this is true
        /// the scanning app shows a warning. This is because at this time the scanning applicaiton
        /// is not intended to create new records. 
        /// </param>
        public void UpdateInsert(bool updateRecordFlag)
        {
            // Set some values before database requests.
            _isScanned = true;

            if (_replaceRecordFlag)
            {
                // Object to hold the document record information
                DocumentModel apiDocModel = new DocumentModel();
                // Copy local information to obj
                CopyToModel(apiDocModel);

                UploadImage(apiDocModel.consumerFilename);

                // Only update the record if there was a change made to one of the fields
                if (updateRecordFlag)
                {
                    UpdateDocumentRecord(apiDocModel);
                }
            }
            // The scanning application should never create a new record through the DRS API.
            else
            {
                MessageBox.Show("Unable to create a new record for document with barcode: " +
                                 BarCode + ". Please try again once document has been indexed.");
                UtilityObj.WriteLog(UtilityObj.error, "No existing record for barcode: " + BarCode);
                Environment.Exit(0);
            }

            // Once done reset flag
            _replaceRecordFlag = false;
        }

        #endregion

        #region Utility 
        // TODO - this should be in the utility class, and need to be reworked.
        //     Reading and writing to FileIO is not optimal.
        public void ConvertPDFToImageList()
        {
            // The document will be null if this is a new scan
            if (_pdfDocument == null)
                return;

            string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";
            File.WriteAllBytes(fileName, PDFObj.ConvertPdfToByteArray(_pdfDocument));
            var pdf = new Cyotek.GhostScript.PdfConversion.Pdf2Image(fileName);
            _imageList = pdf.GetImages().ToList();
            File.Delete(fileName);
        }

        #endregion

        #region Static Find, Select and utility methods
        static public string ErrorMessage = "";


        /// <summary>
        /// Given a barcode request record details. Store the returned items into a list of
        /// DocumentObjs
        /// </summary>
        /// <param name="BarCode"> 8-digit barcode string </param>
        /// <returns> 
        ///     a DocumentObj with a result of the API call for the given barcode.
        /// </returns>
        static public List<DocumentObj> Find(string BarCode)
        {
            List<DocumentObj> documentList = new List<DocumentObj>();
            DocumentObj docObj;

            // This value will hold the information we need for the document. 
            // The Call to `getDocObjectList()` is not necessary.
            string resp = DocumentApi.searchByBarcode(BarCode);
       
            if (!string.IsNullOrEmpty(resp))
            {
                var resultList = (JObject)JToken.Parse(resp);

                // If there are no results are returned from the API or if they are not formatted
                // as expected -> return an empty list and log an error.
                if (!resultList.ContainsKey("resultCount") || !resultList.ContainsKey("results") )
                {
                    UtilityObj.WriteLog(UtilityObj.error, "Unexpected return result from API.");
                    return null;
                }
                // Checking if we get more than one result.
                else if (resultList["resultCount"].ToString() != "1")
                {
                    UtilityObj.WriteLog(UtilityObj.warn, "Got more than one result from DRS API.");
                }

                
                // The barcode should be unique and only one item (Document Record) in the results
                // This loop will catch any unexpected results and save them to the documentList
                foreach (JObject record in resultList["results"])
                {
                    // The first API call doesn't provide all the fields we want to display
                    // Call search again with the document class and unique identifier
                    var docClass = record["documentClass"].ToString();
                    var queries = new Dictionary<string, string>() { 
                        { "documentServiceId", record["documentServiceId"].ToString() } 
                    };
                    var betterResponse = DocumentApi.getSearch(docClass, queries);

                    // This does return a list but the documentClass documentServiceId pairing is
                    //     unique -> only one element
                    var betterResultList = (JObject)JToken.Parse(betterResponse)[0];
                    docObj = new DocumentObj();
                    // Take the elements from the returned JObject and save them to a DocumentObj
                    CopyFromModel(docObj, betterResultList);
                    documentList.Add(docObj);
                }
            }

            // Return the 'list' of records 
            return documentList;
        }

        /// <summary>
        /// Assign values from this DocumentObj to the DocumentModel passed in. 
        /// </summary>
        /// <param name="model">Model to add vaules to</param>
        /// <remarks>
        /// Currently we dont want to update any of the items commented out at the end of the
        /// function. These can be uncommented if we want to send them in the API request. 
        /// </remarks>
        public void CopyToModel(DocumentModel model)
        {
            // get the current time
            DateTime currentTime = DateTime.Now;

            // Build the scanning information model
            DocumentModel.ScanningInformation scanInfo = new DocumentModel.ScanningInformation();
            
            // Currently these are the only fields we want to update from the scanning application
            scanInfo.consumerDocumentId = _barCode.ToString();
            scanInfo.scanDateTime = _scannedDate.ToString("yyyy-MM-dd");
            scanInfo.accessionNumber = AccessionNumberString;
            scanInfo.author = _scannerId;
            scanInfo.batchId = null;
            scanInfo.pageCount = _pageCount;
            scanInfo.documentClass = _documentClass;

            model.description = _description;
            model.scanningInformation = scanInfo;
            
            // Build the UpdateDocument model 
            //model.consumerDocumentId = _barCode.ToString();
            //model.consumerIdentifier = _consumerIdentifier;
            //model.consumerFilingDate = _consumerFilingDate.ToString();
            //model.documentType = _documentTypeCode;
            //model.documentClass = _documentClass;
            //model.consumerReferenceId = _consumerReferenceId.ToString();
        }

        /// <summary>
        /// Uses a regex string will match and group digits based on their placement. Because the
        /// API returns a string value we we have to assume that it may not be well formatted. 
        /// If accNumb does not match the regex pattern there will be issues. See more about the
        /// Regex pattern used in the "<remarks>" section
        /// </summary>
        /// <param name="docObj">Object to add the Accession Number values to</param>
        /// <param name="accNumb">String to be parsed for individual numbers</param>
        /// <remarks>
        /// The Regex pattern @"^(\d{0,2}).?(\d{4}).?(\d{4})$" can be read as:
        ///   @ verbatim text string. Wont interoperate 'escape' characters
        ///   ^ matches the start of the string (limits time and effort from 'greedy' algorithm)
        ///   (\d{0,2}) Group 1 matches 0 to 2 digits
        ///   .? matches 0 or 1 of any character
        ///   (\d{4}) Group 2 matches exactly 4 characters
        ///   (\d{4}) Group 3 matches exactly 4 characters
        ///   $ matches the end of the string
        /// So the following strings will match and result in the following groups:
        ///   "11-2222-3333" -> group 1 = "11", group 2 = "2222", group 3 = "3333"
        ///   "11 2222 3333" -> group 1 = "11", group 2 = "2222", group 3 = "3333"
        ///   "1122223333"   -> group 1 = "11", group 2 = "2222", group 3 = "3333"
        ///   "122223333"    -> group 1 = "1",  group 2 = "2222", group 3 = "3333"
        /// </remarks>
        static public bool SetAccessionNumbers(DocumentObj docObj, string accNumb)
        {
            bool success = false;
            if (accNumb.Length >= 8)
            {
                string pattern = @"^(\d{0,2}).?(\d{4}).?(\d{4})$";

                var match = System.Text.RegularExpressions.Regex.Match(accNumb, pattern);

                // If the input matched our pattern we can parse the groups
                if (match.Success)
                {
                    docObj._sequenceNumber = int.Parse(match.Groups[1].Value);
                    docObj._scheduleNumber = int.Parse(match.Groups[2].Value);
                    docObj._boxNumber = int.Parse(match.Groups[3].Value);
                    success = true;
                }
            }
            return success;
            
        }
        
        /// <summary>
        /// Copy elements from a JObject to a DocumentObj. Include checks for elements that are not
        /// guaranteed to be returned from DRS API
        /// </summary>
        /// <param name="docObj"> 
        ///     DocumentObj that we want to hold the current documents information
        /// </param>
        /// <param name="jDoc"> JObject of items returned from DRS API </param>
        static public void CopyFromModel(DocumentObj docObj, JObject jDoc)
        {
            if (docObj == null || jDoc == null)
            {
                UtilityObj.WriteLog(UtilityObj.error, "Unable to copy from model, objet null.");
                throw new Exception("Attempting to pull data from a null object."); 
            }

            // The unique identifier of the document record
            if (jDoc.ContainsKey("documentServiceId"))
                { docObj._documentServiceId = (string)jDoc["documentServiceId"]; }

            // This author and the scanningInformation.author may be the same values
            if (jDoc.ContainsKey("author")) { docObj._author = (string)jDoc["author"]; }

            // Consumer information
            // - identifier of one[+] document(s) associated with the consumer application entity
            if (jDoc.ContainsKey("consumerDocumentId"))
                { docObj._barCode = (int)jDoc["consumerDocumentId"]; }
            // - Either a singular consumerFileName is returned, or a list of consumerFileNames
            if (jDoc.ContainsKey("consumerFilename"))
                { docObj._fileName = (string)jDoc["consumerFilename"]; }
            else if (jDoc.ContainsKey("consumerFilenames")) 
            {
                // Convert to an array type and add all names to a string sep by a comma
                JArray filenameList = (JArray)jDoc["consumerFileNames"];
                foreach (string filename in filenameList)
                {
                    docObj._fileName += (string)filename;
                    if (filename != (string)filenameList.Last())
                    {
                        docObj._fileName += ", ";
                    }
                }
            }
            // - The DateTime of application/ filing
            if (jDoc.ContainsKey("consumerFilingDateTime")) 
                { docObj._consumerFilingDate = (DateTime)jDoc["consumerFilingDateTime"]; }
            // - Identifier of the entity (BC Company or a manufactured home) in filing
            if (jDoc.ContainsKey("consumerIdentifier")) {
                docObj._legalEntityKey = (string)jDoc["consumerIdentifier"];
                docObj._consumerIdentifier = (string)jDoc["consumerIdentifier"];
            }
            // - Unique identifier for the consumer application transaction/filing/registration
            if (jDoc.ContainsKey("consumerReferenceId"))
            {
                if (!string.IsNullOrEmpty((jDoc["consumerReferenceId"].ToString())))
                {
                    docObj._consumerReferenceId = 
                        Int32.Parse(jDoc["consumerReferenceId"].ToString());
                }
            }

            // Generated by the system, the date and time the document is saved/uploaded.
            if (jDoc.ContainsKey("createDateTime")) 
                { docObj._createDateTime = (DateTime)jDoc["createDateTime"]; }

            // Document Class, Type, and type description
            if (jDoc.ContainsKey("documentClass"))
                { docObj._documentClass = (string)jDoc["documentClass"]; }
            if (jDoc.ContainsKey("documentType"))
                { docObj._documentTypeCode = (string)jDoc["documentType"]; }
            if (jDoc.ContainsKey("documentTypeDescription"))
                { docObj._documentTypeDescription = (string)jDoc["documentTypeDescription"]; }

            // Check if there is a previous document uploaded for this record
            if (jDoc.ContainsKey("documentURL"))
                { docObj._documentURL = (string)jDoc["documentURL"]; }

            // Description, or notes of the document record
            if (jDoc.ContainsKey("description"))
                { docObj._description = (string)jDoc["description"]; }

            // Scanning information [if present] is in a nested object
            if (jDoc.ContainsKey("scanningInformation"))
            {
                // Make the nested object easier to parse
                JObject scanInfo = (JObject)jDoc["scanningInformation"];

                // Another author field, save as owner
                if (scanInfo.ContainsKey("author"))
                    { docObj._owner = (string)scanInfo["author"]; }
                
                // Batch ID that the scan was processed with
                if (scanInfo.ContainsKey("batchId"))
                    { docObj._batchId = (int)scanInfo["batchId"];}

                // Number of pages expected in the document
                if (scanInfo.ContainsKey("pageCount"))
                    { docObj._pageCount = (int)scanInfo["pageCount"];}

                // The date of the consumer application document scan date
                if (scanInfo.ContainsKey("scanDateTime"))
                    { docObj._scannedDate = (DateTime)scanInfo["scanDateTime"];}        
                
                // Try processing the Accession Number last, this is the area tha may have issues.
                if (scanInfo.ContainsKey("accessionNumber")) {
                    string inAccNumber = (string)scanInfo["accessionNumber"];
                    SetAccessionNumbers(docObj, inAccNumber);
                }
            }
        }

        #endregion      

    }
}
