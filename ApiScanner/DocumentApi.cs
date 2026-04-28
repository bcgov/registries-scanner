using AsyncRequests;
using System;
using System.Collections.Generic;


namespace ApiScanner
{
    public class DocumentApi
    {
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
        public static string updateDocumentRecord(object data, string docServId)
        {
            DocumentModel myData = (DocumentModel)data;

            string endpoint = "doc/api/v1/documents/" + docServId;
            string resp = APIRequest.MakeKeyRequest(data, endpoint, RestSharp.Method.PATCH);
            return resp;
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
        public static string uploadDocument(byte[] pdfBytes, object data)
        {
            DocumentModel myData = (DocumentModel)data;
            string resp = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            if (myData.consumerFilename != "")
            {
                // TODO: handle passing in any parameters with param. Ticket #33043
                param.Add("consumerFilename", (string)myData.consumerFilename);
            }
            string endpoint = "/doc/api/v1/documents/" + myData.documentServiceId;
            try
            {
                resp = APIRequest.MakeKeyRequest(pdfBytes, param, endpoint, RestSharp.Method.PUT);
            }
            catch (Exception e)
            {
                UtilityObj.writeLog("Error trying to PUT data: " + e);
            }
                return resp;
        }

        ////search

        /// <summary>
        /// With the given barcode use the DRS API to search for a matching document record.
        /// </summary>
        /// <param name="barcode">
        ///     Cant be null or empty string. Used in query parameter for search.
        /// </param>
        /// <returns>string response from search endpoint</returns>
        /// <exception cref="Exception">If barcode is empty or null throw an error</exception>
        public static string searchByBarcode(string barcode)
        {
            string endpoint = "/doc/api/v1/searches";

            if (!string.IsNullOrEmpty(barcode)) { endpoint += "?consumerDocumentId=" + barcode; }
            else
            {
                UtilityObj.writeLog("ERR: Document is either null or empty. " + 
                        "Cannot hit search endpoint.");
                throw new Exception("Unable to process search without Barcode. Please try again.");
            }
            
            string resp = APIRequest.MakeKeyRequest("", endpoint, RestSharp.Method.GET);

            return resp;
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
            
            string resp = APIRequest.MakeKeyRequest("", endpoint, RestSharp.Method.GET);
            return resp;
        }
    
    }

    public class DocumentModel
    {
        public string author;
        public int consumerDocumentId;
        public string consumerFilename;
        public DateTime consumerFilingDate;
        public string consumerIdentifier;
        public string consumerReferenceId;
        public DateTime createDateTime;
        public string documentClass;
        public string documentExists;
        public string documentServiceId;
        public string documentType;
        public string documentTypeDescription;
        public string documentURL;
        public object scanningInformation;
    }

}