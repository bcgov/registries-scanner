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

    
    }
}
