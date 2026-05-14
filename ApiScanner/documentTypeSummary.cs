using AsyncRequests;

namespace ApiScanner
{
    // In documentation, possible expansion of application. Not currently used by scanner
    public class documentTypeSummary
    {        
        /// <summary>
        /// Scanning Application should NOT use this endpoint. 
        /// This should be updated to /doc/api/v1/scanning/document-types which is currently
        /// already defined in documentTypeApi.
        /// </summary>
        /// <returns></returns>
        public string get()
        {
            // This is not used currently should be removed.
            //string endpoint = "/doc/api/v1/documents/document-types";
            //string resp = APIRequest.MakeKeyRequest("", endpoint, RestSharp.Method.GET);
            return "";
        }
    }
}
