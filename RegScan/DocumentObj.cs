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
        private string _documentExists;
        private string _documentTypeDescription;
        private DateTime _consumerFilingDate;
        private string _consumerIdentifier;
        private int _consumerReferenceId;
        private int _consumerDocumentId;
        JObject scanInfo;
        object scanDoc;

        //private long _documentId = UtilityObj.NOID;
        private string _documentId = "";
        private string _legalEntityKey;
        private string _ownerTypeCode;
        private string _documentTypeCode;
        private string _authorId;
        private string _description;
        private string _fileName;
        private string _fileExtension;
        private bool _isScanned;
        private bool _isPurged;
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
        private int _eventId;
        private bool _eventIdIsNull;
        private string _scannerId;
        private DateTime _scannedDate;

        private Boolean _replaceRecordFlag = false;
       

        // Calculated.
        private BoxObj _boxObj = null;        

        private PdfDocument _pdfDocument = null;
        private List<Bitmap> _imageList = new List<Bitmap>();

        // From document table
        public string DocumentURL { get { return _documentURL; } }
        public string DocumentId { get { return _documentId; } } //not used in data tables any longer
        public string LegalEntityKey { get { return _legalEntityKey; } }
        public string OwnerTypeCode { get { return _ownerTypeCode; } }
        public string DocumentTypeCode { get { return _documentTypeCode; } }
        public string AuthorId { get { return _authorId; } }
        public string Description { get { return _description; } set { _description = value; } }
        public string FileName { get { return _fileName; } }
        public string FileExtension { get { return _fileExtension; } }
        public bool IsScanned { get { return _isScanned; } }
        public bool IsPurged { get { return _isPurged; } }
        public DateTime CreateDateTime { get { return _createDateTime; } }
        public string BarCode { get { return _barCode.ToString(); } }
        public int PageCount { get { return _pageCount; } set { _pageCount = value; } }
        public long AccessionNumber { get { return long.Parse(AccessionNumberString); } }
        public string AccessionNumberString { get { return string.Concat(_sequenceNumber.ToString().PadLeft(2, '0'), _scheduleNumber.ToString().PadLeft(4, '0'), _boxNumber.ToString().PadLeft(4, '0')); } }
        public string AccessionNumberText { get { return string.Concat(_sequenceNumber.ToString().PadLeft(2, '0'), "-", _scheduleNumber.ToString().PadLeft(4, '0'), "-", _boxNumber.ToString().PadLeft(4, '0')); } }
        public int BatchId { get { return _batchId; } set { _batchId = value; } }
        public int VersionNumber { get { return _versionNumber; } set { _versionNumber = value; } }
        public string ScannerId { get { return _scannerId; } set { _scannerId = value; } }
        public DateTime ScannedDate { get { return _scannedDate; } set { _scannedDate = value; } }

        public string Owner { get { return _owner; } }
        public BoxObj Box { get { return _boxObj; } set { _boxObj = value; } }

        public Boolean UpdateRecord { get { return _replaceRecordFlag; } set { _replaceRecordFlag = value; } }

        // Calculated
        public PdfDocument PDFDocument { get { return _pdfDocument; } set { _pdfDocument = value; } }
        public List<Bitmap> ImageList { get { return _imageList; } set { _imageList = value; } }

        public int SequenceNumber { get { return int.Parse(AccessionNumberString.Substring(0, 2)); } }
        public int ScheduleNumber { get { return int.Parse(AccessionNumberString.Substring(2, 4)); } }
        public int BoxNumber { get { return int.Parse(AccessionNumberString.Substring(6, 4)); } }

        public int PagesInBox { get { return _boxObj == null ? 0 : _boxObj.PageCount; } }
        public int PdfPages { get { return _pdfDocument.PageCount; } }
        public string FQDocType { get { return DocTypeObj.Find(_documentTypeCode).FQDescription; } }

        public string Error = "";

        public DocumentObj()
        {
            _boxObj = new BoxObj();
        }

        #endregion

        #region dml
               
        private void Insert()
        {
            copyToModel();            
            byte[] pdfBytes = PDFObj.ConvertPdfToByteArray(_pdfDocument);
            string resp = DocumentApi.post(_fileName, pdfBytes, ApiDocModel);
            UpdateBoxPageCount();
        }
               
        /// <summary>
        /// Handles the logic of updating a current record with the scanned document.
        /// </summary>
        private void Update()
        {
            copyToModel();

            byte[] pdfBytes = PDFObj.ConvertPdfToByteArray(_pdfDocument);
            
            string resp = DocumentApi.put(_fileName, pdfBytes, ApiDocModel);
            if (resp.Contains("errorMessage"))
            {
                MessageBox.Show("ERROR, scanned image failed to laod into database. Current data for " + BarCode + " may be inaccurrate.");
                UtilityObj.writeLog("Scanned document Image failed PUT to update old image.");
                Environment.Exit(0);
            }

            UpdateBoxPageCount();
        }

        /// <summary>
        /// Controls the flow of logic between inserting a new document and updating an existing document.
        /// If the _replaceFlag is true, (there is an existing document) -> Update: replace the existing document.
        /// If the _replaceFlag is false, (no existing document). Insert: create a new record and post the document.
        ///     Previously _replaceFlag would have been false for any record without a DocumentURL and inserted
        ///                                             true for any record with a DocumentURL and updated.
        /// </summary>
        public void UpdateInsert()
        {
            // Set some values before database requests.
            _isScanned = true;
            _fileExtension = "PDF";
            _fileName = _legalEntityKey + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss");                    
            
            // Previously would have been false for any record without a DocumentURL and inserted - any with would be an update.
            // That logic is faulty because the endpoints are the same in both cases and should be handled the same
            // We want to only do an insert if there was no existing record for the barcode.
            if (_replaceRecordFlag)
                Update();
            else
                Insert();

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
        /// NOTE - Truthfully I am not sure what the intent is here. This method is only called if there is a preexisting
        /// version of this document and the user indicates they would like to create a new version of the document. This
        /// would not suggest that the document class should be changed nor a new box should be created. 
        /// </summary>
        public void SetToNew()
        {
            //_documentId = "";          // _documentId = "" indicates a database insert, instead of an update.
            _replaceRecordFlag = true;

            //if (_documentClass == "SOCIETY")
            //{
            //    _ownerTypeCode = "SOC";
            //}
            //else
            //{
            //    _ownerTypeCode = _documentClass;
            //}
            
            //// Get the lates open box ... if one is not found, then one will be created.            
            //_boxObj = BoxObj.Find(_ownerTypeCode);
            //_accessionNumber = GetAccessionNumber(_boxObj);
            //_batchId = 0;                           // Batch Id is reset to zero.
        }
        #endregion

        #region Static Find, Select and utility methods
        static public string ErrorMessage = "";


        //  _BarCode  == conDocId
        static public List<DocumentObj> Find(string _BarCode)
        {           
            ErrorMessage = "";
            List<DocumentObj> documentList = new List<DocumentObj>();

            string resp = DocumentApi.get(_BarCode);

            if (resp.Contains("errorMessage"))
            {
                resp = "";
                ErrorMessage = "No Documents Found For Barcode: " + _BarCode;
                UtilityObj.writeLog(ErrorMessage);
            }         
            else
            {
                var resultList = JToken.Parse(resp);
                //string docClass = (string)token[0]["documentClass"];
                //List<JObject> docList = DocumentApi.getDocObjectList(docClass, _BarCode);

                UtilityObj.writeLog("Fix3 API returned docList, Adding docs to list");
                foreach (JObject record in resultList)
                    documentList.Add(ExtractDocument(record));
            }

            UtilityObj.writeLog("Return Ordered docs");

            // Return list ordered by Version Number Descending.
            return documentList.OrderByDescending(l => l.VersionNumber).ToList();

        }


        // Select the documents that match this barcode.
        static private DocumentObj ExtractDocument(JObject jDoc)
        {           
            UtilityObj.writeLog("Extract jDoc and scanning information.");

            DocumentObj docObj = new DocumentObj();
            // Updated copyFromModel to do the work copying scanning information (if returned) as well as the record information.
            // Much of the logic below is not required. 
            copyFromModel(docObj, jDoc);

            if (jDoc.ContainsKey("scanningInformation")) {
                // If there is already scanning information for the record it is likely that it has already been scanned at least once
                
                UtilityObj.writeLog("Extract jDoc and scanning information.");

                // If Accession Number
                //if (docObj.AccessionNumber == 0 )
                //{
                //    // Get the latest open box ... if one is not found, then one will be created.
                //    docObj._boxObj = BoxObj.Find(docObj._ownerTypeCode);

                //    // IF the box was not created
                //    if (docObj._boxObj == null)
                //    {
                //        docObj.Error = BoxObj.ERROR_MESSAGE;
                //    }
                //    else
                //        docObj.AccessionNumber = GetAccessionNumber(docObj._boxObj);
                //}
                //else
                //{
                //    // Get the existing box from the accession number.
                //    string an = docObj._accessionNumber.ToString().PadLeft(BoxObj.ACCESSION_NUMBER_LENGTH, '0');
                //    docObj._boxObj = BoxObj.Find(int.Parse(an.Substring(0, 2)), int.Parse(an.Substring(2, 4)), int.Parse(an.Substring(6, 4)));
                //    docObj._accessionNumber = Convert.ToInt64(docObj._accessionNumber);
                //}                               
            }
            else
            {
                UtilityObj.writeLog("No Scanning Information for Barcode " + docObj._consumerDocumentId);
            }

            // If a previous version of the document exists make a request to download the stored copy
            if (docObj._documentURL != "")
            {
                // TODO: check if this works... Should the old document ever be shown?
                // If not why do we download it?
                Byte[] docBytes = DocumentApi.getDocBytes(docObj._documentURL);

                if (docBytes != null && docBytes.Length > 2000)
                {
                    docObj._pdfDocument = PDFObj.ConvertByteArrayToPDF(docBytes);
                }
            }
            return docObj;
        }
              

        // IF the document does not contain an assesion number, then get one based on the ower type code.
        static public long GetAccessionNumber(BoxObj _Box)
        {
            return long.Parse(_Box.SequenceNumber.ToString().PadLeft(2, '0') +
                              _Box.ScheduleNumber.ToString().PadLeft(4, '0') +
                              _Box.BoxNumber.ToString().PadLeft(4, '0'));

        }

        public void copyToModel()
        {
            //ApiScanModel.accessionNumber = _accessionNumber;
            //ApiScanModel.author = _authorId;
            //ApiScanModel.batchId = _batchId;
            //ApiScanModel.createDateTime = _createDateTime;
            //ApiScanModel.pagecount = _pageCount;
            //ApiScanModel.scannedDate = _scannedDate;

            ApiDocModel.author = _authorId;
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
        /// Copy elements from a JObject to a DocumentObj. Include checks for elements that are not guarenteed to be reurned from DRS API
        /// </summary>
        /// <param name="docObj"> DocumentObj that we want to hold the current documents information </param>
        /// <param name="jDoc"> JObject of items returned from DRS API </param>
        static public void copyFromModel(DocumentObj docObj, JObject jDoc)
        {
            if (jDoc.ContainsKey("author")) { docObj._authorId = (string)jDoc["author"]; }            
            if (jDoc.ContainsKey("consumerDocumentId")) { docObj._barCode = (int)jDoc["consumerDocumentId"]; }            
            if (jDoc.ContainsKey("consumerFilename")) { docObj._fileName = (string)jDoc["consumerFilename"]; }

            if (jDoc.ContainsKey("consumerIdentifier")) {
                docObj._legalEntityKey = (string)jDoc["consumerIdentifier"];
                docObj._consumerIdentifier = (string)jDoc["consumerIdentifier"];
            }

            if ( dataCheck(jDoc["consumerReferenceId"].ToString())) {
                docObj._consumerReferenceId = Int32.Parse(jDoc["consumerReferenceId"].ToString());
            }
            if (jDoc.ContainsKey("createDateTime")) { docObj._createDateTime = (DateTime)jDoc["createDateTime"]; }

            if (jDoc.ContainsKey("documentClass")) {
                if (jDoc.ContainsKey("documentTypeDescription")) { docObj._description = (string)jDoc["documentTypeDescription"]; }
                else { docObj._description = (string)jDoc["documentClass"]; }
                docObj._documentClass = (string)jDoc["documentClass"];
            }

            if (jDoc.ContainsKey("documentServiceId")) { docObj._documentServiceId = (string)jDoc["documentServiceId"]; }
            if (jDoc.ContainsKey("documentType")) { docObj._documentTypeCode = (string)jDoc["documentType"]; }
            if (jDoc.ContainsKey("documentTypeDescription")) { docObj._documentTypeDescription = (string)jDoc["documentTypeDescription"]; }

            // Check if there is a previous document uploaded for this record
            if (jDoc.ContainsKey("documentURL")) { docObj._documentURL = (string)jDoc["documentURL"]; }
            else if (jDoc.ContainsKey("consumerFilename") && docObj._documentServiceId != "")
            {
                // If the optional documentURL parameter is not present but there is a consumerFileName
                // There is another way we can attempt to get the document URL.
                docObj._documentURL = GetDocURL(docObj);
            }

            if (jDoc.ContainsKey("scanningInformation"))
            {
                // Make the nested object easier to parse
                docObj.scanInfo = (JObject)jDoc["scanningInformation"];
                //docObj.ApiScanModel = (ScanningInfoModel)docObj.scanInfo;

                if (docObj.scanInfo.ContainsKey("accessionNumber")) {
                    // The Accession Number is returned as a string with the following form: "12-3456-7890"
                    // From this we can get the sequence number (12), schedule number (3456) and box number (7890).
                    string accNumb = (string)docObj.scanInfo["accessionNumber"];
                    string[] accNumbersSplit = accNumb.Split('-');
                    docObj._sequenceNumber = int.Parse(accNumbersSplit[0]);
                    docObj._scheduleNumber = int.Parse(accNumbersSplit[1]);
                    docObj._boxNumber = int.Parse(accNumbersSplit[2]);
                }

                if (docObj.scanInfo.ContainsKey("author")) { docObj._owner = (string)docObj.scanInfo["author"]; }
                
                if (docObj.scanInfo.ContainsKey("batchId")) { docObj._batchId = (int)docObj.scanInfo["batchId"];}
                                
                if (docObj.scanInfo.ContainsKey("pagecount")) { docObj._pageCount = (int)docObj.scanInfo["pagecount"];}
                
                if (docObj.scanInfo.ContainsKey("scanDateTime")) { docObj._scannedDate = (DateTime)docObj.scanInfo["scanDateTime"];}
                                       
            }
        }        

        static private Boolean dataCheck(string jDoc)
        {
            if (jDoc == null || jDoc.ToString() == "") { return false; }
            return true;
        }

        /// <summary>
        /// Using this objects _documentServiceId, attempt to get the document URL from the API.
        /// If successful, return the document URL. If not, return an empty string and log an error.
        /// </summary>
        /// <param name="thisDoc"> The current object we want to get a URL for </param>
        /// <returns> A temporary URL to access the digital document, or an empty string </returns>
        static public String GetDocURL(DocumentObj thisDoc)
        {
            string resp = "";
            if (thisDoc._documentServiceId == "")
            {
                // If there is no document identifier we cant use the endpoint to request a documentURL.
                thisDoc.Error = "Document Service ID Not Found For Document with Barcode: " + thisDoc._barCode;
                UtilityObj.writeLog(thisDoc.Error);
            }
            else
            {
                // request a URL for the document.
                resp = DocumentApi.getDocumentURL(thisDoc._documentServiceId);

                if (resp.Contains("errorMessage"))
                {
                    thisDoc.Error = "Error attempting to request URL for document with Barcode: " + thisDoc._barCode;
                    UtilityObj.writeLog(thisDoc.Error + "\n" + resp);
                    resp = "";
                }
                else
                {
                    // Get the result of the API call as a JObject to parse
                    var result = (JObject)JToken.Parse(resp);

                    if (result.ContainsKey("url"))
                    {
                        // Set the returned string to be the returned document URL
                        resp = (string)result["url"];
                    }
                    else
                    {
                        thisDoc.Error = "Document URL Not Found For Document Service Id: " + thisDoc._documentServiceId;
                        UtilityObj.writeLog(thisDoc.Error + "\n" + resp);
                        resp = "";
                    }
                }
            }

            return resp;
        }

        #endregion      

    }
}
