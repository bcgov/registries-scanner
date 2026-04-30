using AsyncRequests;

namespace ApiScanner
{
    public class ScanningAuthorApi 
    {     
        /// <summary>
        /// Gets a list of all the authors. It does not appear to use this information anywhere else.
        /// </summary>
        /// <returns></returns>
        static public string get()
        {
            //string endpoint = "/doc/api/v1/scanning/authors";
            //string resp = APIRequest.MakeKeyRequest("", endpoint, RestSharp.Method.GET);
            return "";
        }
    }

    public class AuthorModel
    {
        public string authorId;
        public string jobTitle;
        public string firstName;
        public string lastName;
        public string phoneNumber;
        public string email;
    }

}
