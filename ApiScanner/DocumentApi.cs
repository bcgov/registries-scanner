using AsyncRequests;
using Utilities;
using System;
using System.Collections.Generic;


namespace ApiScanner
{
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
        /// <param name="docServId">The unique identifier of an existing document service document</param>
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
                UtilityObj.WriteLog(UtilityObj.error, "Error trying to PUT data: " + e);
            }
                return resp;
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

        //Search by docClass
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

        /// REMOVE. This is duplicated from the post method above. 
        /// The only difference is adding parameters to before making the call. 
        //Update pdf document
        public static string update(string fileName, byte[] pdfBytes, object data)
        {
            DocumentModel myData = (DocumentModel)data;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("consumerIdentifier", (string)myData.consumerIdentifier);
            param.Add("consumerFilename", (string)myData.consumerFilename);
            param.Add("consumerFilingDate", (DateTime)myData.consumerFilingDate);
            param.Add("consumerDocumentId", (int)myData.consumerDocumentId);

            string endpoint = "doc/api/v1/documents/" + myData.documentClass + "/" + myData.documentType;
            string resp = APIRequest.MakeKeyRequest(pdfBytes, param, endpoint, RestSharp.Method.PATCH);

            return resp;
        }

        /// REMOVE. This is duplicated from the post method above. 
        /// The only difference is adding parameters to before making the call. 
        public static string patch(string fileName, byte[] pdfBytes, object data)
        {
            DocumentModel myData = (DocumentModel)data;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("consumerIdentifier", (string)myData.consumerIdentifier);
            param.Add("consumerFilename", (string)myData.consumerFilename);
            param.Add("consumerFilingDate", (DateTime)myData.consumerFilingDate);
            param.Add("consumerDocumentId", (int)myData.consumerDocumentId);

            //string endpoint = "doc/api/v1/documents/" + myData.documentClass + "/" + myData.consumerDocumentId;
            string endpoint = "doc/api/v1/scanning/" + myData.documentClass + "/" + myData.consumerDocumentId;

            string resp = APIRequest.MakeKeyRequest(pdfBytes, param, endpoint, RestSharp.Method.PATCH);

            return resp;
        }

        /// REMOVE. This endpoint was depreciated a long time ago. Should NOT be used. 
        public static string get(string docType, string conDocId)
        {
            string endpoint = "/doc/api/v1/business/" + docType + "?consumerDocumentId=" + conDocId;
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