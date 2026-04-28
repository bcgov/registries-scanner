using ApiScanner;
using Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace RegScan
{
    /// <summary>
    /// Class <c>DocTypeObj</c> is used to store Document Types.
    /// This information is obtained from an API call and stored in a class variable list of DocTypeObjs. 
    /// 
    /// Taylor's Notes - 
    /// I believe that this is not a great practice - it would be better to store a list of 
    /// DocTypeObjs outside this class or rename and re configure this class to intentionally 
    /// store a list of objects. 
    /// If Document Types and Classes are not updated frequently then it may be beneficial to
    /// store this data in a file or database rather than making an API call to get this information.
    /// 
    /// </summary>
    public class DocTypeObj
    {
        private string _code;
        private string _description;
        private bool _isActive;
        private string _applicationId;

        public string Code { get { return _code; } }
        public string Description { get { return _description; } }
        public bool IsActive { get { return _isActive; } }
        public string ApplicationId { get { return _applicationId; } }

        public string FQDescription { get { return Code + " -> " + Description; } }

        /// <summary>
        /// Constructor for DocTypeObj. Holds information for a specific document type. 
        /// To see detailed Information on document types and classes see the DRS API documentation 
        ///     - https://okagqp-test-bcregrestricted.apigee.io/docs/docserviceproxy/1/types/documentClass
        /// </summary>
        /// <param name="_Code">API value `documentType` provides the short code for document type</param>
        /// <param name="_Description">API value `documentTypeDescription` a short description of the type</param>
        /// <param name="_IsActive">API value `active`. Used to indicate if the type is active or depreciated</param>
        /// <param name="_ApplicationId">API value `applicationId` provides the short document class code</param>
        public DocTypeObj(string _Code, string _Description, bool _IsActive, string _ApplicationId)
        {
            _code = _Code;
            _description = _Description;
            _isActive = _IsActive;
            _applicationId = _ApplicationId;
        }

        public DocTypeObj()
        { }

        /// <summary>
        /// This list holds DocTypeObjs. It is a member of the DocTypeObj class. 
        /// This creates a nested relationship where each DocTypeObj has a list of DocTypeObjs. This is not ideal and should be refactored.
        /// </summary>
        static private List<DocTypeObj> _list = new List<DocTypeObj>();
        /// <summary>
        /// Currently this method is not being called anywhere in the application. If it is not necessary it should be removed.
        /// </summary>
        /// <param name="_Code"></param>
        /// <returns></returns>
        static public DocTypeObj Find(string _Code)
        {
            if (_list.Count == 0)
                Refresh();

            try
            {
                return _list.Where(c => c.Code == _Code).First();
            }
            catch
            {
                return new DocTypeObj(_Code, "Description Not Found!", false, "NA");
            }
        }       


        /// <summary>
        /// This method only exists to call another method. 
        /// I dont believe that this is necessary and the call to `SetListFromApi()` can be made directly where needed.
        /// </summary>
        static public void Refresh()
        {
            SetListFromApi();
        }

        /// <summary>
        /// This method calls the API to get document type information and creates DocTypeObjs to store that information in the class variable list of DocTypeObjs.
        /// The endpoint hit within `documentTypeApi.get()` returns a list of all document types. This endpoint does not need to be hit more than once.
        /// </summary>
        static private void SetListFromApi()
        {
            string resp = documentTypeApi.get();

            if (resp == "") { return; }
            if (resp.Contains("errorMessage"))
            {
                MessageBox.Show("Error: " + resp);
                Application.Exit();
            }

            if (JsonParser.FromJson(resp).Count > 0)
            {
                _list.Clear();

                // Because we are checking if this value has any elements before this we are doing this work twice.
                // This conversion should happen outside this if statement and stored. 
                var respArray = JsonParser.FromJson(resp).ElementAt(0);
                List<object> docTypes = (List<object>)respArray.Value;

                foreach (Dictionary<string, object> record in docTypes)
                {
                    // It may be better here to create the DocTypeObj directly from the API response rather than parsing the information out of the record and then creating a DocTypeObj.
                    _list.Add(new DocTypeObj(Convert.ToString(record.ElementAt(2).Value), Convert.ToString(record.ElementAt(3).Value), Convert.ToBoolean(record.ElementAt(0).Value),
                                            Convert.ToString(record.ElementAt(1).Value)));
                }
            }
        }
    }
}
