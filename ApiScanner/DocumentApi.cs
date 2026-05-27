using AsyncRequests;
using Utilities;
using System;
using System.Collections.Generic;


namespace ApiScanner
{
    /// <summary>
    /// The DocumentApi class is used as a controller for any API calls to the DRS API.
    /// Currently this applicaiton should only be making calls for the following:
    ///   - Requesting information on a document record
    ///     - searchByBarcode -> Using BarcodeId as a query parameter
    ///     - getSearch -> [documentClass in the path] and DocumentServiceId as a query parmater
    ///   - Updating/ adding data associated with a document record
    ///     - updateDocumentRecord -> update metadata 
    ///     - uploadDocument -> Uploading a PDF 
    /// </summary>
    public class DocumentApi
    {
        #region Updating Document and Record Endpoints
        /// <summary>
        /// Update any of the document record properties (other than document class) for an existing
        /// document identified by a document service ID. If scanning information is included in the
        /// request (and it exists), the scanning record matching the consumerDocumentId will be
        /// updated and included in the response.
        /// </summary>
        /// <param name="data">Body of request. MUST be able to be cast to DocumentModel</param>
        /// <param name="docServId">
        ///     The unique identifier of an existing document service document
        /// </param>
        /// <returns></returns>
        public static string updateDocumentRecord(DocumentModel data, string docServId)
        {
            // TODO - update how we pass information back to the API 
            //     - verification & checking that the data should be changed

            string endpoint = "doc/api/v1/documents/" + docServId;
            return APIRequest.MakeKeyRequest((object)data, endpoint, RestSharp.Method.PATCH);
        }

        /// <summary>
        /// Add or replace the existing document record specified by the document service ID with
        /// the payload document. A required consumerFilename parameter must be submitted with the
        /// request. If scanning information matching the consumerDocumentId exists then it will
        /// be included in the response.
        /// </summary>
        /// <param name="pdfBytes"> PDF file as a byte array</param>
        /// <param name="data"> 
        ///     other elements to be used in the request. Backup if _fileName is an empty string
        /// </param>
        /// <returns>String representation of the result fromt the request</returns>
        public static string uploadDocument(byte[] pdfBytes, string filename, string docServiceId)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(filename))
            {
                // TODO: handle passing in any parameters with param. Ticket #33043
                param.Add("consumerFilename", filename);
            }
            string endpoint = "/doc/api/v1/documents/" + docServiceId;

            return APIRequest.MakeKeyRequest(pdfBytes, param, endpoint, RestSharp.Method.PUT);

        }
        #endregion

        #region Search Endpoints

        /// <summary>
        /// With the given barcode use the DRS API to search for a matching document record.
        /// </summary>
        /// <param name="barcode"> 
        ///     Can't be null or empty string. Used in query parameter for search.
        /// </param>
        /// <returns>string response from search endpoint</returns>
        /// <exception cref="Exception">If barcode is empty or null throw an error</exception>
        public static string searchByBarcode(string barcode)
        {
            string endpoint = "/doc/api/v1/searches";

            if (!string.IsNullOrEmpty(barcode)) { endpoint += "?consumerDocumentId=" + barcode; }
            else
            {
                UtilityObj.WriteLog(UtilityObj.error, "Document is either null or empty. " + 
                        "Cannot hit search endpoint.");
                throw new Exception("Unable to process search without Barcode. Please try again.");
            }

            return APIRequest.MakeKeyRequest(endpoint, RestSharp.Method.GET);
        }

        /// <summary>
        /// Use the second serach endpoint (with a document class) to get more information on the
        /// record. Any additional queries are used to refine the search as query paramaters.
        /// </summary>
        /// <param name="docClass">The class of the document record</param>
        /// <param name="queries">
        ///     Any additional queries (documentServiceID or consumerId) to get a unique result
        /// </param>
        /// <returns>string representation of the result of the query</returns>
        public static string getSearch(string docClass, Dictionary<string, string> queries)
        {
            string endpoint = "/doc/api/v1/searches/" + docClass;
            
            foreach (KeyValuePair<string, string> kvp in queries)
            {
                endpoint += "?" + kvp.Key + "=" + kvp.Value;
            }
            
            return APIRequest.MakeKeyRequest(endpoint, RestSharp.Method.GET);
        }

        #endregion

    }

    public class DocumentModel
    {
        public string consumerDocumentId;
        public string consumerIdentifier;
        public string consumerFilename;
        public string consumerFilingDate;
        public string description;
        public string documentType;
        public string documentClass;
        public string consumerReferenceId;
        public ScanningInformation scanInfo;
        public class ScanningInformation
        {
            public string consumerDocumentId;
            public string scanDateTime;
            public string documentClass;
            public string accessionNumber;
            public string batchId;
            public int pageCount;
            public string author;
        }
    }
}

}