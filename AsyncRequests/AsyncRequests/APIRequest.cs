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
    public class APIRequest
    {
        // security                         
        private static string timeout;
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
            // Set configuration
            SetEnvironment();

            // Set security protocols
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        }

        /* This method is used to make API calls that require an authorization token and cache-control in the header.
         */
        public static string MakeRequest(object data, string endPoint, Method requestType)
        {
            authToken = GetAuthToken();

            var client = new RestClient(api_url + "/" + endPoint);
            var request = new RestRequest(requestType);

            request.AddHeader("Authorization", "Bearer " + authToken);
            request.AddHeader("cache-control", "no-cache");

            if (requestType == Method.POST || requestType == Method.PUT || requestType == Method.PATCH)
            {
                request.AddJsonBody(data);
            }
            return GetHttpResponseContent(request, endPoint);
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
            var request = new RestRequest(requestType);

            request.AddHeader("Account-Id", account_id);
            request.AddHeader("x-apikey", apikey);

            if (requestType == Method.POST || requestType == Method.PUT || requestType == Method.PATCH)
            {
                request.AddJsonBody(data);
            }
            return GetHttpResponseContent(request, endPoint);
        }

        public static string MakeKeyRequest(object requestBody, string queryParam, string endPoint, Method requestType)
        {
            if (api_url == null) { }

            string answer;

            string url = api_url + "/" + endPoint;
            if (!string.IsNullOrEmpty(queryParam)) { url += "?" + queryParam; }

            var client = new RestClient(url);
            var request = new RestRequest(requestType);

            request.AddHeader("Account-Id", account_id);
            request.AddHeader("x-apikey", apikey);

            if (requestType == Method.POST || requestType == Method.PUT || requestType == Method.PATCH)
            {
                if (requestBody != null)
                {
                    request.AddJsonBody(requestBody);
                }
            }

            answer = client.Execute(request).Content;

            return answer;
        }

        //Dictionary<string,object>param
        public static string MakeKeyRequest(byte[] docBytes, Dictionary<string, object> param, string endPoint, Method requestType)
        {
            var request = new RestRequest(requestType);
            string answer;

            request.AddHeader("Account-Id", account_id);
            request.AddHeader("x-apikey", apikey);
            request.AddHeader("Content-Type", "application/pdf");

            if (requestType != Method.POST && requestType != Method.PUT && requestType != Method.PATCH)
            {
                // If any other request type is used throw a new error
                throw new Exception("Invalid request type for endpoint: " + endPoint + ". Must be POST, PUT, or PATCH.");
            }
            foreach (var keyValuePair in param)
            {
                request.AddQueryParameter(keyValuePair.Key, Convert.ToString(keyValuePair.Value));
            }
            //if param.ContainsKey() { request.AddQueryParameter("consumerIdentifier", (string)param["consumerIdentifier"]); }
            //request.AddQueryParameter("consumerFilename", (string)param["consumerFilename"]);
            //request.AddQueryParameter("consumerFilingDate", Convert.ToString((DateTime)param["consumerFilingDate"]));
            //request.AddQueryParameter("consumerDocumentId", Convert.ToString((int)param["consumerDocumentId"]));

            request.AddParameter("application/pdf", docBytes, ParameterType.RequestBody);

            answer = client.Execute(request).Content;

            return answer;

        }


        private static string GetAuthToken()
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

        /// <summary>
        /// This method is used to execute the API request and handle any exceptions that may occur
        /// during the request. It takes in a RestRequest object and an endpoint string, executes
        /// the request, and returns the response content as a string. If there is an error with
        /// the request, it throws an exception with the error message. 
        /// </summary>
        /// <param name="request">Request to be executed</param>
        /// <param name="endPoint">Where the request is sent</param>
        /// <returns>Request response as a string</returns>
        /// <exception cref="Exception"></exception>
        private static string GetHttpResponseContent(RestRequest request, string endPoint)
        {
            var client = new RestClient(api_url + endPoint);
            IRestResponse response = client.Execute(request);

            // If the request was not successful (completed with a good status)
            // throw a new error. 
            if (!response.IsSuccessful)
            {
                throw new Exception("Error Message: " + 
                                     response.StatusCode.ToString() + response.StatusDescription +
                                     Environment.NewLine + response.ErrorMessage
                                    );
            }
            return response.Content;

        }

    }  //end class

} // end namespace



