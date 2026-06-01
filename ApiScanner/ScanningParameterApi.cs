using AsyncRequests;
using RestSharp;

namespace ApiScanner
{
    public class ScanningParameterApi
    {      
        static public string get()
        {
            // We aren't controlling these setting per user, anything that is set (or got) by these
            // methods will be overwritten as soon as a new user uses the application and makes any
            // change. If that feature is desired in the future it should be implemented fully.
            //string endpoint = "/doc/api/v1/scanning/parameters";
            //string resp = APIRequest.MakeKeyRequest(endpoint, Method.GET);
            return "";
        }

        static public string patch(object data)
        {
            // See notes above
            //string endpoint = "/doc/api/v1/scanning/parameters";
            //string resp = APIRequest.MakeKeyRequest(data, endpoint, Method.PATCH);
            return "";
        }   
                
    }

    public class ScannerParametersModel
    {
        public int maxPagesInBox;
        public bool useDocumentFeeder;
        public bool showTwainUi;
        public bool showTwainProgress;
        public bool useFullDuplex;
        public bool useLowResolution;
    }

}
