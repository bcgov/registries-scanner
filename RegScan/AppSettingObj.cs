using System;
using System.Configuration;

namespace RegScan
{
    class AppSettingObj
    {
        #region Fields

        // Oracle database access.
        public string UserName = "";
        public string Password = "";
        public string Host = "";
        public string Port = "";
        public string Sid = "";
        public string DatabaseName = "";

        // Twain SDK access.
        public string TwainSDKUserName = "";
        public string TwainSDKEmail = "";
        public string TwainSDKKey = "";

        public string ErrorMessage = "";
        #endregion

        #region Constructors
        public AppSettingObj()
        {
            try
            {

                // Oracle configuration.
                UserName = ConfigurationManager.AppSettings["UserName"];
                Password = ConfigurationManager.AppSettings["Password"];
                Host = ConfigurationManager.AppSettings["Host"];
                Port = ConfigurationManager.AppSettings["Port"];
                Sid = ConfigurationManager.AppSettings["Sid"];

                // IF the user name not supplied, then use default.
                if (UserName == "*")
                {
                    UserName = "CORRESPONDENCE";
                }

                // If the password is not supplied.
                if (Password == "*")
                {
                    // IF this is production, assign production password.
                    if (Sid.Substring(Sid.Length - 1, 1).ToUpper() == "P")
                        Password = "p4scorr3sd";
                    else
                        // Assign test/devlopment password
                        Password = "roadbl0ck";
                }

                // Set the database name based on the last character.
                switch (Sid.Substring(Sid.Length - 1, 1).ToUpper())
                {
                    case "D":
                        DatabaseName = "Development";
                        break;
                    case "T":
                        DatabaseName = "Test";
                        break;
                    case "P":
                        DatabaseName = "Production";
                        break;
                }

                // Twain SDK
                TwainSDKUserName = ConfigurationManager.AppSettings["TwainSDKUserName"];
                TwainSDKEmail = ConfigurationManager.AppSettings["TwainSDKEmail"];
                TwainSDKKey = ConfigurationManager.AppSettings["TwainSDKKey"];
            }
            catch (Exception Error)
            {
                ErrorMessage = Error.Message;
            }

        }

        #endregion
    }
}
