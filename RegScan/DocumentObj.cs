using ApiScanner;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using PdfSharp.Drawing.BarCodes;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.PerformanceData;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace RegScan
{
    public class DocumentObj
    {
        #region Properties and Constructors
        private DocumentModel ApiDocModel = new DocumentModel();

        //DRS
        private string _documentURL;
        private string _documentClass;
        private string _documentServiceId;
        //private string _documentExists;
        private string _documentTypeDescription;
        private DateTime _consumerFilingDate;
        private string _consumerIdentifier;
        private int _consumerReferenceId;
        //private int _consumerDocumentId;
        JObject scanInfo;

        //private long _documentId = UtilityObj.NOID;
        //private string _documentId = "";
        private string _legalEntityKey;
        //private string _ownerTypeCode;
        private string _documentTypeCode;
        private string _author;
        private string _description;
        private string _fileName;
        private string _fileExtension;
        private bool _isScanned;
        //private bool _isPurged;
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
        //private int _eventId;
        //private bool _eventIdIsNull;
        private string _scannerId;
        private DateTime _scannedDate;

        private Boolean _replaceRecordFlag = false;
       

        // Calculated.
        private BoxObj _boxObj = null;        

        private PdfDocument _pdfDocument = null;
        private List<Bitmap> _imageList = new List<Bitmap>();

        // From document table
        public string DocumentURL { get { return _documentURL; } }
        public string DocumentServiceId { get { return _documentServiceId; } }
        public string LegalEntityKey { get { return _legalEntityKey; } }
        //public string OwnerTypeCode { get { return _ownerTypeCode; } }
        public string DocumentTypeCode { get { return _documentTypeCode; } }
        public string Author { get { return _author; } }
        public string Description { get { return _description; } set { _description = value; } }
        public string FileName { get { return _fileName; } }
        public string FileExtension { get { return _fileExtension; } }
        public bool IsScanned { get { return _isScanned; } }
        //public bool IsPurged { get { return _isPurged; } }
        public DateTime CreateDateTime { get { return _createDateTime; } }
        public string BarCode { get { return _barCode.ToString(); } }
        public int PageCount { get { return _pageCount; } set { _pageCount = value; } }
        public long AccessionNumber { get { return long.Parse(AccessionNumberString); } }
        public string AccessionNumberString { 
            get { return string.Concat(_sequenceNumber.ToString().PadLeft(2, '0'), 
                                       _scheduleNumber.ToString().PadLeft(4, '0'), 
                                       _boxNumber.ToString().PadLeft(4, '0')); } }
        public string AccessionNumberText { 
            get { return string.Concat(_sequenceNumber.ToString().PadLeft(2, '0'), "-",
                                       _scheduleNumber.ToString().PadLeft(4, '0'), "-",
                                       _boxNumber.ToString().PadLeft(4, '0')); } }
        public int BatchId { get { return _batchId; } set { _batchId = value; } }
        public int VersionNumber { get { return _versionNumber; } set { _versionNumber = value; } }
        public string ScannerId { get { return _scannerId; } set { _scannerId = value; } }
        public DateTime ScannedDate { get { return _scannedDate; } set { _scannedDate = value; } }

        public string Owner { get { return _owner; } }
        public BoxObj Box { get { return _boxObj; } set { _boxObj = value; } }

        public Boolean UpdateRecord { 
            get { return _replaceRecordFlag; } set { _replaceRecordFlag = value; } }

        // Calculated
        public PdfDocument PDFDocument { 
            get { return _pdfDocument; } set { _pdfDocument = value; } }
        public List<Bitmap> ImageList { get { return _imageList; } set { _imageList = value; } }

        public int SequenceNumber { 
            get { return int.Parse(AccessionNumberString.Substring(0, 2)); } }
        public int ScheduleNumber { 
            get { return int.Parse(AccessionNumberString.Substring(2, 4)); } }
        public int BoxNumber { 
            get { return int.Parse(AccessionNumberString.Substring(6, 4)); } }

        public int PagesInBox { get { return _boxObj == null ? 0 : _boxObj.PageCount; } }
        public int PdfPages { get { return _pdfDocument.PageCount; } }
        public string FQDocType { get { return _documentTypeDescription; } }

        public string Error = "";

        public DocumentObj()
        {
            _boxObj = new BoxObj();
        }

        #endregion

        #region dml
               
        /// <summary>
        /// Handles the logic of updating a current record with the scanned document.
        /// </summary>
        private void Update()
        {
            CopyToModel();

            byte[] pdfBytes = PDFObj.ConvertPdfToByteArray(_pdfDocument);
            
            string resp = DocumentApi.uploadDocument(pdfBytes, ApiDocModel);
            if (resp.Contains("errorMessage"))
            {
                MessageBox.Show("ERROR, scanned image failed to load into database. " + 
                                "Current data for " + BarCode + " may be inaccurate.");
                UtilityObj.writeLog("Scanned document Image failed PUT to update old image.");
                Environment.Exit(0);
            }

            UpdateBoxPageCount();
        }

        /// <summary>
        /// Controls the flow of logic between inserting or updatating a document record.
        /// If the _replaceFlag is true, (there is an existing document) 
        ///     -> Update: replace the existing document.
        /// If the _replaceFlag is false, (no existing document).
        ///     -> Insert: create a new record and post the document. Currently the scanning
        ///        application does not have the ability to create a new document record through
        ///        DRS API. This functionality could be added in the future.
        /// </summary>
        public void UpdateInsert()
        {
            // Set some values before database requests.
            _isScanned = true;
            _fileExtension = "PDF";
            _fileName = _legalEntityKey + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss");

            if (_replaceRecordFlag)
                Update();
            // The scanning application should never create a new record through the DRS API.
            else
            {
                MessageBox.Show("ERROR Unable to create a new record for document with barcode: " +
                                 BarCode + ". Please try again once document has been indexed.");
                UtilityObj.writeLog("No existing record for barcode: " + BarCode);
                Environment.Exit(0);
            }

            // Once done reset flag
            _replaceRecordFlag = false;
        }

        private void UpdateBoxPageCount()
        {
            // Update Box Page Count
            _boxObj.PageCount += _pageCount;
            _boxObj.UpdatePageCount();
        }

        #endregion

        #region Utility 
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


        /// <summary>
        /// NOTE - Truthfully I am not sure what the intent is here. This method is only called if
        /// there is a preexisting version of this document and the user indicates they would like
        /// to create a new version of the document. This would not suggest that neither the 
        /// document class should be changed nor a new box should be created. 
        /// </summary>
        public void SetToNew()
        {
            // _documentId = "" indicates a database insert, instead of an update.
            _replaceRecordFlag = true;

            //if (_documentClass == "SOCIETY")
            //{
            //    _ownerTypeCode = "SOC";
            //}
            //else
            //{
            //    _ownerTypeCode = _documentClass;
            //}
            
            //// Get the latest open box ... if one is not found, then one will be created.            
            //_boxObj = BoxObj.Find(_ownerTypeCode);
            //_accessionNumber = GetAccessionNumber(_boxObj);
            //_batchId = 0;                           // Batch Id is reset to zero.
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
        ///     List of DocumentObjs for each returned record matching the _BarCode
        /// </returns>
        static public List<DocumentObj> Find(string BarCode)
        {           
            ErrorMessage = "";
            List<DocumentObj> documentList = new List<DocumentObj>();

            // This value will hold the information we need for the document. 
            // The Call to `getDocObjectList()` is not necessary.
            string resp = DocumentApi.searchByBarcode(BarCode);

            if (resp.Contains("errorMessage"))
            {
                resp = "";       
                ErrorMessage = "No Documents Found For Barcode: " + BarCode;
                UtilityObj.writeLog(ErrorMessage);
            }         
            else
            {

                var resultList = (JObject)JToken.Parse(resp);

                // If there are no results are returned from the API or if they are not formatted
                // as expected -> return an empty list and log an error.
                if (!resultList.ContainsKey("resultCount") || !resultList.ContainsKey("results"))
                {
                    UtilityObj.writeLog("Unexpected return result from API.");
                    return null;
                }
                if (resultList["resultCount"].ToString() != "1")
                {
                    // We need exactly one result. 
                    UtilityObj.writeLog("API did not return exactly one value.");
                    return null;
                }

                DocumentObj docObj;
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
                    copyFromModel(docObj, betterResultList);
                    documentList.Add(docObj);
                }
            }

            UtilityObj.writeLog("Return Ordered docs");

            // Return list ordered by Version Number Descending.
            return documentList.OrderByDescending(l => l.VersionNumber).ToList();

        }

        /// <summary>
        /// Take the elements of the DocumentObj and 
        /// </summary>
        public void CopyToModel()
        {
            //ApiScanModel.accessionNumber = _accessionNumber;
            //ApiScanModel.author = _authorId;
            //ApiScanModel.batchId = _batchId;
            //ApiScanModel.createDateTime = _createDateTime;
            //ApiScanModel.pagecount = _pageCount;
            //ApiScanModel.scannedDate = _scannedDate;

            ApiDocModel.author = _author;
            ApiDocModel.consumerDocumentId = _barCode;
            ApiDocModel.consumerFilename = _fileName;
            ApiDocModel.consumerFilingDate = DateTime.Now;
            ApiDocModel.consumerIdentifier = _consumerIdentifier;
            ApiDocModel.consumerReferenceId = "";
            ApiDocModel.createDateTime = _createDateTime;
            ApiDocModel.documentClass = _documentClass;
            ApiDocModel.documentExists = "";
            ApiDocModel.documentServiceId = _documentServiceId;
            ApiDocModel.documentType = _documentTypeCode;
            ApiDocModel.documentTypeDescription = "";
            ApiDocModel.documentURL = "";
            //ApiDocModel.scanningInformation = ApiScanModel;
          
        }
        
        public void copyToModel(JObject temp, ScanningInfoModel scanInfo )
        {
            if (temp == null || scanInfo == null) return;

            if (temp["accessionNumber"] != null) { scanInfo.accessionNumber = (long)temp["accessionNumber"]; }
            if (temp["authorId"] != null) { scanInfo.author = (string)temp["authorId"]; }
            if (temp["scannedDate"] != null) { scanInfo.scannedDate = (DateTime)temp["scannedDate"]; }
            if (temp["batchId"] != null) { scanInfo.batchId = (int)temp["batchId"]; }
            if (temp["createDateTime"] != null) { scanInfo.createDateTime = (DateTime)temp["createDateTime"]; }
            if (temp["pageCount"] != null) { scanInfo.pagecount = (int)temp["pageCount"]; }

            if (temp["author"] != null) { ApiDocModel.author = (string)temp["author"]; }
            if (temp["consumerDocumentId"] != null) { ApiDocModel.consumerDocumentId = (int)temp["consumerDocumentId"]; }
            if (temp["consumerFilename"] != null) { ApiDocModel.consumerFilename = (string)temp["consumerFilename"]; }
            if (temp["consumerFilingDate"] != null) { ApiDocModel.consumerFilingDate = (DateTime)temp["consumerFilingDate"]; }
            if (temp["consumerIdentifier"] != null) { ApiDocModel.consumerIdentifier = (string)temp["consumerIdentifier"]; }
            if (temp["consumerReferenceId"] != null) { ApiDocModel.consumerReferenceId = ""; }
            if (temp["createDateTime"] != null) { ApiDocModel.createDateTime = (DateTime)temp["createDateTime"]; }
            if (temp["documentClass"] != null) { ApiDocModel.documentClass = (string)temp["documentClass"]; }
            if (temp["documentExists"] != null) { ApiDocModel.documentExists = (string)temp["documentExists"]; }
            if (temp["documentId"] != null) { ApiDocModel.documentServiceId = (string)temp["documentId"]; }
            
            if (temp["documentType"] != null) { ApiDocModel.documentType = (string)temp["documentTypeCode"]; }
            //if (temp["documentType"] != null) { ApiDocModel.documentType = (string)temp["documentType"]; }

            if (temp["documentTypeDescription"] != null) { ApiDocModel.documentTypeDescription = (string)temp["documentTypeDescription"]; }
            if (temp["documentURL"] != null) { ApiDocModel.documentURL = (string)temp["documentURL"]; } 
                 
            ApiDocModel.scanningInformation = scanInfo;
        }

        
        /// <summary>
        /// Copy elements from a JObject to a DocumentObj. Include checks for elements that are not
        /// guaranteed to be returned from DRS API
        /// </summary>
        /// <param name="docObj"> 
        ///     DocumentObj that we want to hold the current documents information
        /// </param>
        /// <param name="jDoc"> JObject of items returned from DRS API </param>
        static public void copyFromModel(DocumentObj docObj, JObject jDoc)
        {
            if (docObj == null || jDoc == null)
            {
                UtilityObj.writeLog("Error copying from model, docObj or jDoc was null.");
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
                if (dataCheck(jDoc["consumerReferenceId"].ToString()))
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
                docObj.scanInfo = (JObject)jDoc["scanningInformation"];

                if (docObj.scanInfo.ContainsKey("accessionNumber")) {
                    // The Accession Number is returned as a string with the following form:
                    // "12-3456-7890" - From this we can extract the following
                    // sequence number <12>, schedule number <3456> and box number <7890>.
                    string accNumb = (string)docObj.scanInfo["accessionNumber"];
                    string[] accNumbersSplit = accNumb.Split('-');
                    docObj._sequenceNumber = int.Parse(accNumbersSplit[0]);
                    docObj._scheduleNumber = int.Parse(accNumbersSplit[1]);
                    docObj._boxNumber = int.Parse(accNumbersSplit[2]);
                }

                // Another author field, save as owner
                if (docObj.scanInfo.ContainsKey("author"))
                    { docObj._owner = (string)docObj.scanInfo["author"]; }
                
                // Batch ID that the scan was processed with
                if (docObj.scanInfo.ContainsKey("batchId"))
                    { docObj._batchId = (int)docObj.scanInfo["batchId"];}

                // Number of pages expected in the document
                if (docObj.scanInfo.ContainsKey("pageCount"))
                    { docObj._pageCount = (int)docObj.scanInfo["pageCount"];}

                // The date of the consumer application document scan date
                if (docObj.scanInfo.ContainsKey("scanDateTime"))
                    { docObj._scannedDate = (DateTime)docObj.scanInfo["scanDateTime"];}             
            }
        }        

        static private Boolean dataCheck(string jDoc)
        {
            if (jDoc == null || jDoc.ToString() == "") { return false; }
            return true;
        }

        #endregion      

    }
}
