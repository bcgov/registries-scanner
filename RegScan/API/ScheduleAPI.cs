using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegScan
{
    public class ScheduleAPI
    {
        public static string GetSchedules()
        {
            string endpoint = "/doc/api/v1/scanning/schedules";

            return APIRequest.MakeKeyRequest(endpoint, RestSharp.Method.Get);
        }
    }
}
