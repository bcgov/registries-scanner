using AppConfiguration;
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
                UserName = ConfigKeys.USERNAME;
                Password = ConfigKeys.PASSWORD;
                Host = ConfigKeys.HOST;
                Port = ConfigKeys.PORT;
                Sid = ConfigKeys.SID;

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
                TwainSDKUserName = ConfigKeys.TWAINSDKUSERNAME;
                TwainSDKEmail = ConfigKeys.TWAINSDKEMAIL;
                TwainSDKKey = ConfigKeys.TWAINSDKKEY;
            }
            catch (Exception Error)
            {
                ErrorMessage = Error.Message;
            }

        }

        #endregion
    }
}
