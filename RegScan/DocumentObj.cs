using Newtonsoft.Json.Linq;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;

namespace RegScan
{
    /// <summary>
    /// The DocumentObj class is used to hold the information for a document record. The properties
    /// of the class are matched to the DRS APIs documentSummary schema as closely as possible.
    /// </summary>
    public class DocumentObj
    {

        #region Document Record Properties
        
        // DRS APIs unique identifier for this document record. 
        private string _documentServiceId;
        
        // Consumer Document ID (Barcode) used to identify a document with a document record
        private int _barCode;
        public string BarCode { get { return _barCode.ToString(); } }
        
        // Consumer Identifier/ Legal Entity. Used to identify the entity (ex. company, mobile home
        // , etc) associated with the document record
        private string _legalEntityKey;
        public string LegalEntityKey { get { return _legalEntityKey; } }
        
        // Consumer File Name. Used to hold the new name of the file
        private string _fileName;

        // Document Type. Used as the subcategory of the document type.
        private string _documentTypeCode;
        public string DocumentType {  get { return _documentTypeCode; } }

        // Document Type Description. Used as the long form/description of the document type.
        private string _documentTypeDescription;
        public string DocTypeDesc { get { return _documentTypeDescription; } }

        // Document Class. Used as the category of the document type.
        private string _documentClass;
        public string DocumentClass { get { return _documentClass; } }

        // Create Date Time. The Date and time that the document record is saved/uploaded
        private DateTime _createDateTime;

        // Consumer Filing Date Time. The datetime of the consumer application registration/filing
        private DateTime _consumerFilingDate;
        public string ConsumerFilingDateString { 
            get { return _consumerFilingDate.ToString("MMM dd, yyyy"); } }

        // Document URL. Only set after saving current document or if the document record has a
        // version of the document already uploaded.
        private string _documentURL;
        public string DocumentURL { get { return _documentURL; } }

        // Holds notes/comments about the document record.
        private string _description;
        public string Description { get { return _description; } set { _description = value; } }

        // Not listed in DRS API documentation. However; sometimes this is returned.
        // If not in Scanning Information it will hold the filing authors information.
        private string _author;

        // Consumer Reference ID for the consumer application transaction/filing/registration
        private int _consumerReferenceId;

        #endregion

        #region Scanning Information Properties

        // Scanning Information

        // consumer Document ID is also listed within the ScanningInformation object. This should
        // be the same as the Barcode above.

        // ScanDateTime. The date of the consumer application document scan date. Time is alway
        // set to 12:00pm in the local time zone.
        private DateTime _scannedDate;
        public DateTime ScannedDate { get { return _scannedDate; } set { _scannedDate = value; } }

        // Document Class is listed here again. It will be the same as the Document Class above.

        // Used to catch if the Accession Number has been set for a particular document record
        private bool _accSet = false;
        public bool AccSet { get { return _accSet; } }

        // Accession Number Values. 
        private AccessionNumberObj _accNumber; 

        public AccessionNumberObj AccessionNumber { 
            get { return _accNumber; } set { _accNumber = value; } }

        // batch number -> not used in this app

        // Number of pages expected in the document.
        private int _pageCount;
        public int PageCount { get { return _pageCount; } set { _pageCount = value; } }

        private int _versionNumber;
        public int VersionNumber { get { return _versionNumber; } set { _versionNumber = value; } }

        // This author field May be empty when requesting a document record (if it hasn't been
        // scanned.) Will be set to the current users IDIR when the document is saved.
        private string _scannerIDIR;
        public string ScannerId { get { return _scannerIDIR; } set { _scannerIDIR = value; } }

        #endregion
        #region Other Properties

        // As far as I can tell this value is set to false, used as a flag, then reset to false.
        // There are no places currently that it can be set to true.
        // If this app was able to create new document records this flag may be useful.
        private bool _updateRecord = false;
        public bool UpdateRecord { 
            get { return _updateRecord; } set { _updateRecord = value; } }
       
        // Used when a PDF is created of the scanned images.
        private PdfDocument _pdfDocument = null;
        public PdfDocument PDFDocument { 
            get { return _pdfDocument; } set { _pdfDocument = value; } }

        #endregion

        public DocumentObj()
        {
        }

        #region Updating Functions

        /// <summary>
        /// Handles the logic of uploading the scanned document to the DRS API.
        /// </summary>
        /// <param name="fileName">Name to use for the document in DRS API</param>
        private void UploadImage(string fileName)
        {
            byte[] pdfBytes = PDFObj.ConvertPdfToByteArray(_pdfDocument);

            try
            {
                // upload the scanned image
                string uploadResp = DocumentApi.UploadDocument(
                    pdfBytes, fileName, _documentServiceId);
            }
            catch (Exception e)
            {
                string msg = "Scanned image failed to load into database. " +
                                "Current data for " + BarCode + " may be inaccurate.";
                UtilityObj.WriteLog(UtilityObj.error,
                    "Scanned document Image failed PUT of scanned image." + e.ToString());
                throw new ApplicationException(msg);
            }
        }

        /// <summary>
        /// Used when the document record needs to be updated.
        /// </summary>
        /// <param name="apiDocModel">Document Model with updated information</param>
        /// <exception cref="ApplicationException">
        /// Thrown if an error is hit while trying to send the updated information to DRS API
        /// </exception>
        private void UpdateDocumentRecord(DocumentModel apiDocModel)
        { 
            try
            {
                // update the document record
                string updateResp = DocumentApi.UpdateDocumentRecord(
                    apiDocModel, _documentServiceId);
            }
            catch (Exception e)
            {
                string msg = "Unable to update document record." +
                                "Current data for " + BarCode + " may be inaccurate.";
                UtilityObj.WriteLog(UtilityObj.error,
                    "Scanned document Image failed PATCH to update document record.\n" +
                    e.ToString());
                throw new ApplicationException(msg);
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
        /// Determines if the application will update the record and upload image. If this is true
        /// the scanning app shows a warning. This is because at this time the scanning application
        /// is not intended to create new records. 
        /// </param>
        public void UpdateInsert(bool updateRecordFlag)
        {

            if (_updateRecord)
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
                string msg = "Unable to create a new record for document with barcode: " +
                                 BarCode + ". Please try again once document has been indexed.";
                UtilityObj.WriteLog(UtilityObj.error, "No existing record for barcode: " + BarCode);
                throw new ArgumentException(msg);
            }

            // Once done reset flag
            _updateRecord = false;
        }

        #endregion

        #region Static Find, Select and utility methods
        
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
            string resp = DocumentApi.SearchByBarcode(BarCode);
       
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
                    var betterResponse = DocumentApi.GetSearch(docClass, queries);

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
        /// <param name="model">Model to add values to</param>
        /// <remarks>
        /// Currently we don't want to update any of the items commented out at the end of the
        /// function. These can be uncommented if we want to send them in the API request. 
        /// </remarks>
        public void CopyToModel(DocumentModel model)
        {
            // get the current time
            DateTime currentTime = DateTime.Now;

            // Build the scanning information model
            DocumentModel.ScanningInformation scanInfo = new DocumentModel.ScanningInformation();

            // Current box information
            BoxObj current = frmBoxManagement.SelectedBox;
            
            // Currently these are the only fields we want to update from the scanning application
            scanInfo.consumerDocumentId = _barCode.ToString();
            scanInfo.scanDateTime = _scannedDate.ToString("yyyy-MM-dd");
            scanInfo.accessionNumber = AccessionNumber.Text;
            scanInfo.author = _scannerIDIR;
            scanInfo.batchId = null;
            scanInfo.pageCount = _pageCount;
            scanInfo.documentClass = _documentClass;

            model.description = _description;
            model.scanningInformation = scanInfo;
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
                { docObj._barCode = jDoc["consumerDocumentId"].Value<int>(); }
            // - Either a singular consumerFileName is returned, or a list of consumerFileNames
            if (jDoc.ContainsKey("consumerFilename"))
                { docObj._fileName = (string)jDoc["consumerFilename"]; }
            else if (jDoc.ContainsKey("consumerFilenames")) 
            {
                // Convert to an array type and add all names to a string sep by a comma
                JArray filenameList = (JArray)jDoc["consumerFilenames"];
                // Get the first filename from the list.
                docObj._fileName = (string)filenameList[0];
            }
            // - The DateTime of application/ filing
            if (jDoc.ContainsKey("consumerFilingDateTime")) 
                { docObj._consumerFilingDate = (DateTime)jDoc["consumerFilingDateTime"]; }
            // - Identifier of the entity (BC Company or a manufactured home) in filing
            if (jDoc.ContainsKey("consumerIdentifier")) {
                docObj._legalEntityKey = (string)jDoc["consumerIdentifier"];
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
                    { docObj._scannerIDIR = (string)scanInfo["author"]; }

                // Number of pages expected in the document
                if (scanInfo.ContainsKey("pageCount"))
                    { docObj._pageCount = scanInfo["pageCount"].Value<int>(); }

                // The date of the consumer application document scan date
                if (scanInfo.ContainsKey("scanDateTime"))
                    { docObj._scannedDate = (DateTime)scanInfo["scanDateTime"];}        
                
                // Try processing the Accession Number last, this is the area that may have issues.
                if (scanInfo.ContainsKey("accessionNumber")) {
                    string inAccNumber = (string)scanInfo["accessionNumber"];
                    // set Accession Number and flag 
                    docObj.AccessionNumber = new AccessionNumberObj(inAccNumber);
                    docObj._accSet = true;
                }
            }
        }

        #endregion      

    }
}
