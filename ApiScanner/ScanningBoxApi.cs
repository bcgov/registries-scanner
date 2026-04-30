using AsyncRequests;
using System;

namespace ApiScanner
{
    public class ScanningBoxApi
    {        
        // Refresh
        static public string get()
        {
            //string endpoint = "/doc/api/v1/scanning/boxes";
            //string resp = APIRequest.MakeKeyRequest("", endpoint, RestSharp.Method.GET);
            return "";
        }

        // UpdatePageCount + CloseOpen  
        static public string patch(object data)
        {
            string endpoint = "/doc/api/v1/scanning/boxes";
            //string resp = APIRequest.MakeKeyRequest(data, endpoint, RestSharp.Method.PATCH);
            // mocked return data 
            string resp = "{\r\n  \"boxId\": 1,\r\n  \"boxNumber\": 21,\r\n  \"closedDate\": " +
                "\"2024-09-05T19:00:00+00:00\",\r\n  \"openedDate\": " +
                "\"2024-09-01T19:00:00+00:00\",\r\n  \"pageCount\": 340,\r\n" +
                "  \"scheduleNumber\": 2000000,\r\n  \"sequenceNumber\": 1000000\r\n}";
            return resp;
        }

        // Insert
        static public string post(object data)
        {
            //string endpoint = "/doc/api/v1/scanning/boxes";
            //string resp = APIRequest.MakeKeyRequest(data, endpoint, RestSharp.Method.POST);
            return "";
        }

        // List
        static public string get(string seqNum, string schedNum)
        {
            //string endpoint = "/doc/api/v1/scanning/boxes/" + seqNum + "/" + schedNum;
            //string resp = APIRequest.MakeKeyRequest("", endpoint, RestSharp.Method.GET);
            return "";
        }
    }

    public class BoxModel
    {
        public int boxId;
        public int sequenceNumber;
        public int scheduleNumber;
        public int boxNumber;
        public DateTime openedDate;
        public DateTime closedDate;
        public int pageCount;
    }

}
