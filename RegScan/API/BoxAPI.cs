using Newtonsoft.Json.Linq;
using System;

namespace RegScan
{
    /// <summary>
    /// This static class is used as the bridge between the application and the DRS API.
    /// Using pre-defined HTML request functions within APIRequest.cs.
    /// </summary>
    public class BoxAPI
    {
        /// <summary>
        /// Get and return a string of all the box records from DRS API
        /// </summary>
        /// <returns>String HTML response containing a list of BoxObjs</returns>
        public static string GetAllBoxes()
        {
            string endpoint = "/doc/api/v1/scanning/boxes";

            return APIRequest.MakeKeyRequest(endpoint, RestSharp.Method.Get);
        }

        /// <summary>
        /// Given a string Accession Number put a request to the DRS API to get the highest
        /// possible BatchID. 
        /// </summary>
        /// <param name="accessionNumber">
        /// A string version of the accession number associated with a particular box
        /// </param>
        /// <returns>The current highest BatchID for the box</returns>
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

        /// <summary>
        /// Used to modify a box record stored within the DRS API. This will have to be used
        /// when a box is closed to add the closed date. 
        /// </summary>
        /// <param name="box">BoxObj to be updated</param>
        /// <returns>HTTP response in string format</returns>
        public static string UpdateBox(BoxObj box)
        {
            string endpoint = "/doc/api/v1/scanning/boxes";

            // transform the boxObj to the scanningBox Schema
            CreateBoxModel newBox = new CreateBoxModel(box);

            return APIRequest.MakeKeyRequest(newBox, endpoint, RestSharp.Method.Patch);
        }

        /// <summary>
        /// Used to add a new box record to the DRS API. 
        /// </summary>
        /// <param name="box">The box Obj to add</param>
        /// <returns>HTTP response in string format</returns>
        public static string CreateBox(BoxObj box)
        {
            string endpoint = "/doc/api/v1/scanning/boxes";

            // transform the boxObj to the scanningBox Schema
            CreateBoxModel newBox = new CreateBoxModel(box);

            return APIRequest.MakeKeyRequest((object)newBox, endpoint, RestSharp.Method.Post);
        }
    }

    /// <summary>
    /// Used to create requests for boxes with the DRS API. The matching schema is:
    /// {
    ///   "boxNumber": <int>,
    ///   "openedDate": <str>,
    ///   "pageCount": <int>,
    ///   "scheduleNumber": <int>,
    ///   "sequenceNumber": <int>
    ///   }
    ///   BoxIDs are only used after the box is created. Close dates are only set on close.
    /// </summary>
    public class CreateBoxModel
    {
        public int? boxId;
        public int boxNumber;
        public DateTimeOffset openedDate;
        public DateTimeOffset? closedDate;
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

            if (inBox.IsClosed)
                closedDate = inBox.ClosedDate;
            // The default for unset ints is 0
            if (inBox.BoxId != 0)
                boxId = inBox.BoxId;
        }
    }
}
