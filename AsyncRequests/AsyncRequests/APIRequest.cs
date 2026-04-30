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

        /// <summary>
        /// Method to build a request to send to the DRS API.
        /// The endpoint is appended to the API URL from the environment variables
        /// </summary>
        /// <param name="endPoint">Where the request is sent. Appended to API URL</param>
        /// <param name="requestType">HTTP request type (GET, PATCH, PUT)</param>
        /// <returns>Response from API in string format.</returns>
        public static string MakeKeyRequest(string endPoint, Method requestType)
        {
            var request = new RestRequest(requestType);

            request.AddHeader("Account-Id", account_id);
            request.AddHeader("x-apikey", apikey);

            if (requestType != Method.GET)
            {
                // If any other request type is used throw a new error
                throw new Exception("Invalid request type for endpoint: " + endPoint + 
                                    ". Must use GET.");
            }
            return GetHttpResponseContent(request, endPoint);
        }

        /// <summary>
        /// Method to build a request to send to the DRS API. Any data passed in is used as the
        /// body of the request. The endpoint is appended to the API URL from the environment
        /// variables. 
        /// </summary>
        /// <param name="data">Body of the request to be sent</param>
        /// <param name="endPoint">Where the request is sent. Appended to API URL</param>
        /// <param name="requestType">HTTP request type (GET, PATCH, PUT)</param>
        /// <returns>Response from API in string format.</returns>
        public static string MakeKeyRequest(object data, string endPoint, Method requestType)
        {
            var request = new RestRequest(requestType);

            request.AddHeader("Account-Id", account_id);
            request.AddHeader("x-apikey", apikey);

            if (requestType != Method.POST && requestType != Method.PUT &&
                requestType != Method.PATCH)
            {
                // If any other request type is used throw a new error
                throw new Exception("Invalid request type for endpoint: " + endPoint +
                                    ". Must use POST, PUT, or PATCH.");
            }
            request.AddJsonBody(data);
            return GetHttpResponseContent(request, endPoint);
        }

        /// <summary>
        /// Method to build a request to send a document to the DRS API. Any key value pairs in
        /// param are set as query parameters. The endpoint is appended to the API URL from the 
        /// environment variables. 
        /// </summary>
        /// <param name="docBytes">Document image to be uploaded to DRS API</param>
        /// <param name="param">Key (String) Value (object) pairs of query parameters</param>
        /// <param name="endPoint">Where the request is sent. Appended to API UR</param>
        /// <param name="requestType">HTTP request type (GET, PATCH, PUT)</param>
        /// <returns>Response from API in string format.</returns>
        /// <exception cref="Exception"></exception>
        public static string MakeKeyRequest(byte[] docBytes, Dictionary<string, object> param, 
                                            string endPoint, Method requestType)
        {
            var request = new RestRequest(requestType);

            request.AddHeader("Account-Id", account_id);
            request.AddHeader("x-apikey", apikey);
            request.AddHeader("Content-Type", "application/pdf");

            if ( requestType != Method.PUT )
            {
                // If any other request type is used throw a new error
                throw new Exception("Invalid request type for endpoint: " + endPoint + ". Uploading documents must use PUT.");
            }
            foreach (var keyValuePair in param)
            {
                request.AddQueryParameter(keyValuePair.Key, Convert.ToString(keyValuePair.Value));
            }

            request.AddParameter("application/pdf", docBytes, ParameterType.RequestBody);

            return GetHttpResponseContent(request, endPoint);
        }

        /// <summary>
        /// Set object variables to environment variables.
        /// </summary>
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
        /// <exception cref="Exception">
        ///     Thrown if the result of the request has a bad status or was not completed
        /// </exception>
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



