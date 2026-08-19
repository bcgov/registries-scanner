using Newtonsoft.Json.Linq;
using System;

namespace RegScan
{
    public class BoxAPI
    {
        public static string GetAllBoxes()
        {
            string endpoint = "/doc/api/v1/scanning/boxes";

            return APIRequest.MakeKeyRequest(endpoint, RestSharp.Method.Get);
        }

        public static int GetBatchID(string accessionNumber)
        {
            int batchID = 0;
            string endpoint = String.Concat("/doc/api/v1/scanning/batchid/", accessionNumber);

            var payload = APIRequest.MakeKeyRequest(endpoint, RestSharp.Method.Get);

            if (!string.IsNullOrEmpty(payload))
            {
                var obj = (JObject)JToken.Parse(payload);
                if (obj.ContainsKey("batchId"))
                    batchID = obj["batchId"].Value<int>();
            }
            return batchID;
        }

        public static string UpdateBox(BoxObj box)
        {
            string endpoint = "/doc/api/v1/scanning/boxes";

            return APIRequest.MakeKeyRequest(box, endpoint, RestSharp.Method.Patch);
        }
    }
}
