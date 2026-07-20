using System.Configuration;
using System.Security.Policy;

namespace AppConfiguration
{
    /// <summary>
    /// This class is used to store the keys for configuration values.  
    /// The values are read from the app.config file and stored in these properties.
    /// This allows for easy access to configuration values throughout the application as well as
    /// allowing intellisense and compile time checking.
    /// </summary>
    /// <remarks>
    /// Instead of using ConfigurationManager.AppSettings["PORT"] throughout the code, we can
    /// use ConfigKeys.PORT which is more readable and less error prone.
    /// </remarks>
    public class ConfigKeys
    {
        public static string PORT{ get; set; } = string.Empty;
        public static string ENV { get; set; } = string.Empty;
        public static string USERNAME { get; set; } = string.Empty;
        public static string PASSWORD { get; set; } = string.Empty;
        public static string HOST { get; set; } = string.Empty;
        public static string SID { get; set; } = string.Empty;
        public static string AUTH_TIMEOUT { get; set; } = string.Empty;
        public static string AUTH_SVC_URL { get; set; } = string.Empty;
        public static string CLIENT_ID { get; set; } = string.Empty;    
        public static string CLIENT_ACCOUNT { get; set; } = string.Empty;
        public static string ACCOUNT_ID { get; set; } = string.Empty;
        public static string APIKEY { get; set; } = string.Empty; 
        public static string API_URL { get; set; } = string.Empty;
        public static string TWAINSDKUSERNAME { get; set; } = string.Empty;
        public static string TWAINSDKEMAIL { get; set; } = string.Empty;
        public static string TWAINSDKKEY { get; set; } = string.Empty;
        public static string LOGPATH { get; set; } = string.Empty;

        /// <summary>
        /// Load all configuration values from sensitive.config and store as class properties.
        /// Any other project within the solution can referenec these values as required.
        /// </summary>
        public ConfigKeys()
        {
            APIKEY = ConfigurationManager.AppSettings[nameof(ConfigKeys.APIKEY)];
            ACCOUNT_ID = ConfigurationManager.AppSettings[nameof(ConfigKeys.ACCOUNT_ID)];
            AUTH_SVC_URL = ConfigurationManager.AppSettings[nameof(ConfigKeys.AUTH_SVC_URL)];
            AUTH_TIMEOUT = ConfigurationManager.AppSettings[nameof(ConfigKeys.AUTH_TIMEOUT)];
            CLIENT_ID = ConfigurationManager.AppSettings[nameof(ConfigKeys.CLIENT_ID)];
            CLIENT_ACCOUNT = ConfigurationManager.AppSettings[nameof(ConfigKeys.CLIENT_ACCOUNT)];
            API_URL = ConfigurationManager.AppSettings[nameof(ConfigKeys.API_URL)];

            USERNAME = ConfigurationManager.AppSettings[nameof(ConfigKeys.USERNAME)];
            PASSWORD = ConfigurationManager.AppSettings[nameof(ConfigKeys.PASSWORD)];
            HOST = ConfigurationManager.AppSettings[nameof(ConfigKeys.HOST)];
            PORT = ConfigurationManager.AppSettings[nameof(ConfigKeys.PORT)];
            SID = ConfigurationManager.AppSettings[nameof(ConfigKeys.SID)];
            ENV = ConfigurationManager.AppSettings[nameof(ConfigKeys.ENV)];

            TWAINSDKUSERNAME = ConfigurationManager.AppSettings[nameof(ConfigKeys.TWAINSDKUSERNAME)];
            TWAINSDKEMAIL = ConfigurationManager.AppSettings[nameof(ConfigKeys.TWAINSDKEMAIL)];
            TWAINSDKKEY = ConfigurationManager.AppSettings[nameof(ConfigKeys.TWAINSDKKEY)];

            LOGPATH = ConfigurationManager.AppSettings[nameof(ConfigKeys.LOGPATH)];
        }

    }
}
