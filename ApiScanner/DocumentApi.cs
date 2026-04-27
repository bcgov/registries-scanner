using AsyncRequests;
using Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PdfSharp.Pdf;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;

namespace ApiScanner
{
    public class DocumentApi
    {
        // consumerDocumentId
        public static List<JObject> getDocObjectList(string docType, string conDocId)
        {
            string resp = get(docType, conDocId);

            List<JObject> jList = new List<JObject>();
            var token = JToken.Parse(resp);

            foreach (object token2 in token)
            {
                jList.Add((JObject)token2);
            }

            return jList;
        }

        /// <summary>
        /// Used to control the returned data from attempting to download an image from a URL
        /// </summary>
        /// <param name="docURL"> URL to access previously scanned data </param>
        /// <returns> Byte[] holding image data </returns>
        /// <exception cref="Exception"> If the URL was inaccessible or timed out </exception>
        public static Byte[] getDocBytes(string docURL)
        {
            return APIRequest.download(docURL);
        }        

        /// <summary>
        /// To be used to update the scanning information of a preexisting document record.
        /// This endpoint can not be used to upload a document image to the API
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="pdfBytes"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string post(string fileName, byte[] pdfBytes, object data)
        {
            DocumentModel myData = (DocumentModel)data;
            string resp = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("consumerIdentifier", (string)myData.consumerIdentifier);
            param.Add("consumerFilename", (string)myData.consumerFilename);
            param.Add("consumerFilingDate", (DateTime)myData.consumerFilingDate);
            param.Add("consumerDocumentId", (int)myData.consumerDocumentId);

            //string endpoint = "doc/api/v1/documents/" + myData.documentClass + "/" + myData.documentType;
            //string endpoint = "doc/api/v1/scanning/" + myData.documentClass + "/" + myData.consumerDocumentId;
            string endpoint = "doc/api/v1/documents/" + myData.documentClass;

            try
            {
                resp = APIRequest.MakeKeyRequest(fileName, pdfBytes, param, endpoint, RestSharp.Method.POST);
            }
            catch (Exception e)
            {
                UtilityObj.writeLog("Error trying to POST data: " + e);
            }
            return resp;
        }

        /// <summary>
        /// Update any of the document record properties (other than document class) for an existing
        /// document identified by a document service ID. If scanning information is included in the
        /// request (and it exists), the scanning record matching the consumerDocumentId will be
        /// updated and included in the response.
        /// </summary>
        /// <param name="data">Body of request. MUST be able to be cast to DocumentModel</param>
        /// <param name="docServId">The unique identifier of an existing document service document</param>
        /// <returns></returns>
        public static string patch(object data, string docServId)
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
        /// <param name="_fileName"> Filename associated with the uploaded document </param>
        /// <param name="pdfBytes"> PDF file as a byte array</param>
        /// <param name="data"> other elements to be used in the request. Backup if _fileName is an empty string</param>
        /// <returns></returns>
        public static string put(string _fileName, byte[] pdfBytes, object data)
        {
            DocumentModel myData = (DocumentModel)data;
            string resp = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            if (_fileName != "")
            {
                // TODO: handle passing in any parameters with param. 
                param.Add("consumerFilename", (string)myData.consumerFilename);
            }
            string endpoint = "/doc/api/v1/documents/" + myData.documentServiceId;
            try
            {
                resp = APIRequest.MakeKeyRequest(myData.consumerFilename, pdfBytes, param, endpoint, RestSharp.Method.PUT);
            }
            catch (Exception e)
            {
                UtilityObj.writeLog("Error trying to PUT data: " + e);
            }
                return resp;
        }

        /// In documentation, possible expansion of application. Not currently used by scanner

        ////Documents       

        /// <summary>
        /// This endpoint should only be used if a user is checking if the current barcode is
        /// already used or not before attempting to add a new document record. 
        /// </summary>
        /// <param name="conDocId"> Barcode (consumerDocumentId) to check for existence </param>
        /// <returns> String response from API call </returns>
        public static string get(string conDocId)
        {           
            string endpoint = "/doc/api/v1/documents/verify/" + conDocId;
            string resp = APIRequest.MakeKeyRequest("", endpoint, RestSharp.Method.GET);           
            return resp;
        }

        /// REMOVE. 
        /// This endpoint and request type combo should not be used by the scanning application
        public static string delete(string docServId)
        {
            string endpoint = "/doc/api/v1/documents/" + docServId;
            string resp = APIRequest.MakeKeyRequest("", endpoint, RestSharp.Method.DELETE);
            return resp;
        }

        /// REMOVE. This is duplicated from the patch method above. 
        /// The only difference is not casting data to a DocumentModel.
        public static string patch2(object data, string docServId)
        {
            string endpoint = "/doc/api/v1/documents/" + docServId;
            string resp = APIRequest.MakeKeyRequest(data, endpoint, RestSharp.Method.PATCH);
            return resp;
        }

        /// <summary>
        /// Update any of the document record properties (other than document class) for an existing document.
        /// If scanning information is included in the request, the scanning record matching the consumerDocumentId
        /// will be updated (if it exists). This information will then be included in the response.
        /// </summary>
        /// <param name="data">
        /// body document to be included in the request. MUST be able to be cast to DocumentModel.
        /// </param>
        /// <returns>>Response from the API call</returns>
        public static string post(object data)
        {
            DocumentModel myData = (DocumentModel)data;

            string endpoint = "doc/api/v1/documents/" + myData.documentClass + "/" + myData.documentType;
            string resp = APIRequest.MakeKeyRequest(data, endpoint, RestSharp.Method.POST);
            return resp;
        }    

        ////search

        public static string getSearch()
        {
            string endpoint = "/doc/api/v1/searches";
            string resp = APIRequest.MakeKeyRequest("", endpoint, RestSharp.Method.GET);
            return resp;
        }

        //Search by docClass
        public static string getSearch(string docClass)
        {
            string endpoint = "/doc/api/v1/searches/" + docClass;
            string resp = APIRequest.MakeKeyRequest("", endpoint, RestSharp.Method.GET);
            return resp;
        }

        /// REMOVE. This is duplicated from the post method above. 
        /// The only difference is adding parameters to before making the call. 
        public static string update(string fileName, byte[] pdfBytes, object data)
        {
            DocumentModel myData = (DocumentModel)data;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("consumerIdentifier", (string)myData.consumerIdentifier);
            param.Add("consumerFilename", (string)myData.consumerFilename);
            param.Add("consumerFilingDate", (DateTime)myData.consumerFilingDate);
            param.Add("consumerDocumentId", (int)myData.consumerDocumentId);

            string endpoint = "doc/api/v1/documents/" + myData.documentClass + "/" + myData.documentType;
            string resp = APIRequest.MakeKeyRequest(fileName, pdfBytes, param, endpoint, RestSharp.Method.PATCH);

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

            string resp = APIRequest.MakeKeyRequest(fileName, pdfBytes, param, endpoint, RestSharp.Method.PATCH);

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