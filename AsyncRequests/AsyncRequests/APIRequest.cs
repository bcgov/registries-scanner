using AppConfiguration;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;


namespace AsyncRequests
{
    // Class to facilitate API calls
    // uses Base64Encoding to scramble api-key value in app.config
    //
    public class APIRequest
    {
        // security                         
        private string timeout;
        private static string client_id;
        private static string client_secret;
        private static string apikey;
        private static string account_id;
        private static string authToken;
        private static string token_url;

        private static string api_url;    

        public static Method method;

        public APIRequest()
        {
            // Get configuration
            SetEnvironment();

            // Set security protocols
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        }        
      

        public static string MakeRequest(object data, string endPoint, Method requestType)
        {
            authToken = _GetAuthToken();

            var client = new RestClient(api_url + "/" + endPoint);
            var request = new RestRequest(requestType);

            request.AddHeader("Authorization", "Bearer " + authToken);
            request.AddHeader("cache-control", "no-cache");
            
            if (requestType == Method.POST || requestType == Method.PUT || requestType == Method.PATCH)
            {
                request.AddJsonBody(data);
                string answer = client.Execute(request).Content;
                return answer;
            }

            if (requestType == Method.GET)
            {
                string answer = client.Execute(request).Content;
                return answer;
            }

            if (requestType == Method.DELETE)
            {
                string answer = client.Execute(request).Content;
                return answer;
            }

            return "";
        }      


        public static string MakeKeyRequest(object data, string endPoint, Method requestType)
        {
            if( api_url == null ) { }                  

            var client = new RestClient(api_url + "/" + endPoint);
            var request = new RestRequest(requestType);

            request.AddHeader("Account-Id", account_id);
            request.AddHeader("x-apikey", apikey);

            if (requestType == Method.POST || requestType == Method.PUT || requestType == Method.PATCH)
            {
                request.AddJsonBody(data);                
                string answer = client.Execute(request).Content;
                return answer;
            }

            if (requestType == Method.GET)
            {
                string answer = client.Execute(request).Content;
                return answer;
            }

            if (requestType == Method.DELETE)
            {
                string answer = client.Execute(request).Content;
                return answer;
            }

            return "";
        }


        //Dictionary<string,object>param
        public static string MakeKeyRequest(string fileName, byte[] docBytes, Dictionary<string, object> param, string endPoint, Method requestType)
        {
            if (api_url == null) { }

            var client = new RestClient(api_url + endPoint);
            var request = new RestRequest(requestType);
            string answer = "";

            request.AddHeader("Account-Id", account_id);
            request.AddHeader("x-apikey", apikey);
            request.AddHeader("Content-Type", "application/pdf");

            if (requestType == Method.POST || requestType == Method.PUT || requestType == Method.PATCH)
            {
                foreach (var keyValuePair in param) {
                    request.AddQueryParameter(keyValuePair.Key, Convert.ToString(keyValuePair.Value));
                }
                //if param.ContainsKey() { request.AddQueryParameter("consumerIdentifier", (string)param["consumerIdentifier"]); }
                //request.AddQueryParameter("consumerFilename", (string)param["consumerFilename"]);
                //request.AddQueryParameter("consumerFilingDate", Convert.ToString((DateTime)param["consumerFilingDate"]));
                //request.AddQueryParameter("consumerDocumentId", Convert.ToString((int)param["consumerDocumentId"]));

                request.AddParameter("application/pdf", docBytes, ParameterType.RequestBody);

                answer = client.Execute(request).Content;
            }

            return answer;

        }


        public static byte[] download(string url)
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            byte[] response = client.DownloadData(request);
            string x = Encoding.Default.GetString(response);
            return response;
        }


        private static string _GetAuthToken()
        {
            var client = new RestClient(token_url);
            var request = new RestRequest(Method.POST);

            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            string credentials = "grant_type=client_credentials&client_id=" + client_id + "&client_secret=" + client_secret;
            request.AddParameter("application/x-www-form-urlencoded", credentials, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            IDictionary<string, object> json = Json.JsonParser.FromJson(response.Content);

            return Json.JsonParser.ToJson(json);
        }

        private void SetEnvironment()
        {
            apikey = ConfigKeys.APIKEY;
            api_url = ConfigKeys.API_URL;
            account_id = ConfigKeys.ACCOUNT_ID;
            token_url = ConfigKeys.AUTH_SVC_URL;
            timeout = ConfigKeys.AUTH_TIMEOUT;
            client_id = ConfigKeys.CLIENT_ID;
            client_secret = ConfigKeys.CLIENT_ACCOUNT;
        }

    }  //end class

} // end namespace



