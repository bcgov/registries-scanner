using ApiScanner;
using Utilities;
using Newtonsoft.Json.Linq;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Linq.Expressions;

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
            get { return _sequenceNumber; } }
        public int ScheduleNumber {
            get { return _scheduleNumber; } }
        public int BoxNumber {
            get { return _boxNumber; } }
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
        /// Handles the logic of updating a current record with the scanned document.
        /// </summary>
        private void Update()
        {
            // Object to hold the document record information
            DocumentModel apiDocModel = new DocumentModel();
            // Copy local information to obj
            CopyToModel(apiDocModel);

            byte[] pdfBytes = PDFObj.ConvertPdfToByteArray(_pdfDocument);

            // TODO - we should not exit, we should show the error then handle it gracefully
            try
            {
                // upload the scanned image
                string uploadResp = DocumentApi.uploadDocument(
                    pdfBytes, apiDocModel.consumerFilename, _documentServiceId);
            }
            catch (Exception e)
            {
                MessageBox.Show("Scanned image failed to load into database. " + 
                                "Current data for " + BarCode + " may be inaccurate.");
                UtilityObj.WriteLog(UtilityObj.error, 
                    "Scanned document Image failed PUT of scanned image." + e.ToString());
                Environment.Exit(0);
            }

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
                    "Scanned document Image failed PATCH to update document record." +
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
        public void UpdateInsert()
        {
            // Set some values before database requests.
            _isScanned = true;

            if (_replaceRecordFlag)
                Update();
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
            List<DocumentObj> documentList = new List<DocumentObj>();

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

            UtilityObj.WriteLog(UtilityObj.debug, "Return Ordered docs");

            // Return list ordered by Version Number Descending.
            return documentList.OrderByDescending(l => l.VersionNumber).ToList();

        }

        public void CopyToModel(DocumentModel model)
        {
            // get the current time
            DateTime currentTime = DateTime.Now;

            // Build the scanning information model
            DocumentModel.ScanningInformation scanInfo = new DocumentModel.ScanningInformation();
            scanInfo.consumerDocumentId = _barCode.ToString();
            scanInfo.scanDateTime = currentTime.ToString();
            scanInfo.documentClass = _documentClass;
            scanInfo.accessionNumber = AccessionNumberString;
            scanInfo.batchId = null;
            scanInfo.pageCount = _pageCount;
            scanInfo.author = _author;

            // Build the UpdateDocument model
            model.consumerDocumentId = _barCode.ToString();
            model.consumerIdentifier = _consumerIdentifier;
            model.consumerFilename = _legalEntityKey + currentTime.ToString("yyyy_MM_dd_hh_mm_ss");
            model.consumerFilingDate = _consumerFilingDate.ToString();
            model.description = _description;
            model.documentType = _documentTypeCode;
            model.documentClass = _documentClass;
            model.consumerReferenceId = _consumerReferenceId.ToString();
            model.scanInfo = scanInfo;
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
                JObject scanInfo = (JObject)jDoc["scanningInformation"];

                if (scanInfo.ContainsKey("accessionNumber")) {
                    // The Accession Number is returned as a string with the following form:
                    // "12-3456-7890" - From this we can extract the following
                    // sequence number <12>, schedule number <3456> and box number <7890>.
                    string accNumb = (string)scanInfo["accessionNumber"];
                    string[] accNumbersSplit = accNumb.Split('-');
                    docObj._sequenceNumber = int.Parse(accNumbersSplit[0]);
                    docObj._scheduleNumber = int.Parse(accNumbersSplit[1]);
                    docObj._boxNumber = int.Parse(accNumbersSplit[2]);
                }

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
            }
        }        

        static private Boolean dataCheck(string jDoc)
        {
            if (string.IsNullOrEmpty(jDoc)) { return false; }
            return true;
        }

        #endregion      

    }
}
