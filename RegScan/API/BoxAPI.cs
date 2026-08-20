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

        public static string CreateBox(BoxObj box)
        {
            string endpoint = "/doc/api/v1/scanning/boxes";

            // transform the boxObj to the scanningBox Schema
            CreateBoxModel newBox = new CreateBoxModel(box);

            return APIRequest.MakeKeyRequest((object)newBox, endpoint, RestSharp.Method.Post);
        }
    }

    /// <summary>
    /// Used to create the POST request for boxes with the DRS API. The matching schema is:
    /// {
    ///   "boxNumber": <int>,
    ///   "openedDate": <str>,
    ///   "pageCount": <int>,
    ///   "scheduleNumber": <int>,
    ///   "sequenceNumber": <int>
    ///   }
    /// </summary>
    public class CreateBoxModel
    {
        public int boxNumber;
        public DateTimeOffset openedDate;
        public int pageCount;
        public int sequenceNumber;
        public int scheduleNumber;

        /// <summary>
        /// Given a BoxObj transform into the DRS API scanningBox obj
        /// </summary>
        /// <param name="inBox"></param>
        public CreateBoxModel(BoxObj inBox)
        {
            boxNumber = inBox.BoxNumber;
            openedDate = inBox.OpenedDate;
            pageCount = inBox.PageCount;
            sequenceNumber = inBox.SequenceNumber;
            scheduleNumber = inBox.ScheduleNumber;
        }
    }
}
