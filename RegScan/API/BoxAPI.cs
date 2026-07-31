using PdfSharp.Drawing.BarCodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegScan
{
    public class BoxAPI
    {
        public static string GetAllBoxes()
        {
            string endpoint = "/doc/api/v1/scanning/boxes";

            return APIRequest.MakeKeyRequest(endpoint, RestSharp.Method.Get);
        }
    }
}
