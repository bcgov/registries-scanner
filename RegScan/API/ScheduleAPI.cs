using System;

namespace RegScan
{
    public class ScheduleAPI
    {
        
        /// <summary>
        /// Query the DRS API for all available Sequence and Schedule numbers.
        /// </summary>
        /// <returns>String response from the API</returns>
        public static string GetSchedules()
        {
            string endpoint = "/doc/api/v1/scanning/schedules";

            return APIRequest.MakeKeyRequest(endpoint, RestSharp.Method.Get);
        }
    }
}
