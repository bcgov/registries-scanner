using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Google.Cloud.SecretManager.V1;
using Google.Protobuf.Compiler;
using System;
using System.Configuration;
using System.IdentityModel;
using System.Threading.Tasks;

namespace AppConfiguration
{
    public enum KeyStorage
    {
        AppConfig,
        AzureKeyVault,
        GCPSecretManager
    }
    /* This class is used to store the keys for configuration values.  
     * The values are either read from the app.config file or Azure and stored in these properties.  
     * This allows for easy access to configuration values throughout the application as well as allowing intellisense and compile time checking.
     * i.e. instead of using ConfigurationManager.AppSettings["PORT"] throughout the code, we can use ConfigKeys.PORT which is more readable and less error prone.
     * */
    public class ConfigKeys
    {
        #region Public Keys
        public static string PORT { get; set; } = string.Empty;
        public static string ENV { get; set; } = string.Empty;
        public static string HOST { get; set; } = string.Empty;
        public static string SID { get; set; } = string.Empty;
        public static string AUTH_TIMEOUT { get; set; } = string.Empty;
        public static string AUTH_SVC_URL { get; set; } = string.Empty;
        public static string API_URL { get; set; } = string.Empty;
        public static string GOOGLE_PROJECT_ID { get; set; } = string.Empty;
        public static string AZURE_KEYVAULTNAME { get; set; } = string.Empty;
        public static string AZURE_TENANT_ID { get; set; } = string.Empty;
        public static string AZURE_CLIENT_ID { get; set; } = string.Empty;
        #endregion

        #region Private Keys
        public static string USERNAME { get; set; } = string.Empty;
        public static string PASSWORD { get; set; } = string.Empty;
        public static string CLIENT_ID { get; set; } = string.Empty;
        public static string CLIENT_ACCOUNT { get; set; } = string.Empty;
        public static string ACCOUNT_ID { get; set; } = string.Empty;
        public static string APIKEY { get; set; } = string.Empty;
        public static string TWAINSDKUSERNAME { get; set; } = string.Empty;
        public static string TWAINSDKEMAIL { get; set; } = string.Empty;
        public static string TWAINSDKKEY { get; set; } = string.Empty;
        #endregion

        private static SecretClient AzureClient { get; set; } = null;
        private static SecretManagerServiceClient GoogleClient { get; set; } = null;
        public ConfigKeys()
        { LoadFromAppConfig(); }
        /* Constructor that takes a boolean parameter to determine whether to load configuration values from Azure Key Vault or from the app.config file.
         */
        public ConfigKeys(KeyStorage keyStorage)
        {
            switch (keyStorage)
            {
                case KeyStorage.AppConfig:
                    LoadFromAppConfig();
                    break;
                case KeyStorage.AzureKeyVault:
                    LoadFromAzureKeyVault();
                    break;
                case KeyStorage.GCPSecretManager:
                    LoadFromGCPSecretManager();
                    break;
                default:
                    LoadFromAppConfig();
                    break;
            }

        }
        /* This method can be used to load configuration values from the app.config file. 
         * It uses the ConfigurationManager.AppSettings to retrieve the values and store them in the corresponding properties of the ConfigKeys class.
         */
        public void LoadFromAppConfig()
        {
            PORT = ConfigurationManager.AppSettings[nameof(ConfigKeys.PORT)];
            ENV = ConfigurationManager.AppSettings[nameof(ConfigKeys.ENV)];
            HOST = ConfigurationManager.AppSettings[nameof(ConfigKeys.HOST)];
            SID = ConfigurationManager.AppSettings[nameof(ConfigKeys.SID)];
            AUTH_TIMEOUT = ConfigurationManager.AppSettings[nameof(ConfigKeys.AUTH_TIMEOUT)];
            AUTH_SVC_URL = ConfigurationManager.AppSettings[nameof(ConfigKeys.AUTH_SVC_URL)];
            API_URL = ConfigurationManager.AppSettings[nameof(ConfigKeys.API_URL)];

            USERNAME = ConfigurationManager.AppSettings[nameof(ConfigKeys.USERNAME)];
            PASSWORD = ConfigurationManager.AppSettings[nameof(ConfigKeys.PASSWORD)];
            CLIENT_ID = ConfigurationManager.AppSettings[nameof(ConfigKeys.CLIENT_ID)];
            CLIENT_ACCOUNT = ConfigurationManager.AppSettings[nameof(ConfigKeys.CLIENT_ACCOUNT)];
            ACCOUNT_ID = ConfigurationManager.AppSettings[nameof(ConfigKeys.ACCOUNT_ID)];
            APIKEY = ConfigurationManager.AppSettings[nameof(ConfigKeys.APIKEY)];

            TWAINSDKUSERNAME = ConfigurationManager.AppSettings[nameof(ConfigKeys.TWAINSDKUSERNAME)];
            TWAINSDKEMAIL = ConfigurationManager.AppSettings[nameof(ConfigKeys.TWAINSDKEMAIL)];
            TWAINSDKKEY = ConfigurationManager.AppSettings[nameof(ConfigKeys.TWAINSDKKEY)];
        }
        #region Azure Key Vault
        /* This method can be used to load configuration values from Azure Key Vault. 
         * It first authenticates to Azure Key Vault and then retrieves the secrets using the SecretClient instance. 
         * The retrieved secrets are then stored in the corresponding properties of the ConfigKeys class.
         */
        public async void LoadFromAzureKeyVault()
        {

            PORT = ConfigurationManager.AppSettings[nameof(ConfigKeys.PORT)];
            ENV = ConfigurationManager.AppSettings[nameof(ConfigKeys.ENV)];
            HOST = ConfigurationManager.AppSettings[nameof(ConfigKeys.HOST)];
            SID = ConfigurationManager.AppSettings[nameof(ConfigKeys.SID)];
            AUTH_TIMEOUT = ConfigurationManager.AppSettings[nameof(ConfigKeys.AUTH_TIMEOUT)];
            AUTH_SVC_URL = ConfigurationManager.AppSettings[nameof(ConfigKeys.AUTH_SVC_URL)];
            API_URL = ConfigurationManager.AppSettings[nameof(ConfigKeys.API_URL)];

            AZURE_KEYVAULTNAME = ConfigurationManager.AppSettings[nameof(ConfigKeys.AZURE_KEYVAULTNAME)];
            AZURE_TENANT_ID = ConfigurationManager.AppSettings[nameof(ConfigKeys.AZURE_TENANT_ID)];
            AZURE_CLIENT_ID = ConfigurationManager.AppSettings[nameof(ConfigKeys.AZURE_CLIENT_ID)];



            if (await AzureAuthenticate())
            {
                //Authenticate to Azure Key Vault and set the SecretClient instance

                var _APIKEY = await AzureClient.GetSecretAsync(nameof(ConfigKeys.APIKEY));
                APIKEY = _APIKEY.Value.Value;
                var _ACCOUNT_ID = await AzureClient.GetSecretAsync(nameof(ConfigKeys.ACCOUNT_ID));
                ACCOUNT_ID = _ACCOUNT_ID.Value.Value;
                var _CLIENT_ID = await AzureClient.GetSecretAsync(nameof(ConfigKeys.CLIENT_ID));
                CLIENT_ID = _CLIENT_ID.Value.Value;
                var _CLIENT_ACCOUNT = await AzureClient.GetSecretAsync(nameof(ConfigKeys.CLIENT_ACCOUNT));
                CLIENT_ACCOUNT = _CLIENT_ACCOUNT.Value.Value;
                var _USERNAME = await AzureClient.GetSecretAsync(nameof(ConfigKeys.USERNAME));
                USERNAME = _USERNAME.Value.Value;
                var _PASSWORD = await AzureClient.GetSecretAsync(nameof(ConfigKeys.PASSWORD));
                PASSWORD = _PASSWORD.Value.Value;
                var _TWAINSDKUSERNAME = await AzureClient.GetSecretAsync(nameof(ConfigKeys.TWAINSDKUSERNAME));
                TWAINSDKUSERNAME = _TWAINSDKUSERNAME.Value.Value;
                var _TWAINSDKEMAIL = await AzureClient.GetSecretAsync(nameof(ConfigKeys.TWAINSDKEMAIL));
                TWAINSDKEMAIL = _TWAINSDKEMAIL.Value.Value;
                var _TWAINSDKKEY = await AzureClient.GetSecretAsync(nameof(ConfigKeys.TWAINSDKKEY));
                TWAINSDKKEY = _TWAINSDKKEY.Value.Value;
            }

        }
        // This method can be used to authenticate to Azure Key Vault. 
        // With DefaultAzureCredential, authentication is handled automatically based on the environment.
        // DefaultAzureCredential will try:
        // 1. Azure CLI login (az login)
        // 2. Visual Studio / VS Code login
        public async Task<bool> AzureAuthenticate()
        {
            var kvUri = $"https://{AZURE_KEYVAULTNAME}.vault.azure.net";
            var isAuthorized = false;

            // First, try to authenticate silently (without user interaction).
            DefaultAzureCredentialOptions silentOptions = new DefaultAzureCredentialOptions
            {
                //ExcludeEnvironmentCredential = true,
                ExcludeManagedIdentityCredential = true,
                ExcludeInteractiveBrowserCredential = true // No popup here
            };

            DefaultAzureCredential silentCredential = new DefaultAzureCredential(silentOptions);

            AzureClient = new SecretClient(new Uri(kvUri), silentCredential);

            if (await HasAccessAsync(AzureClient, nameof(ConfigKeys.APIKEY)))
            {
                return true;
            }
            // If silent authentication fails, fall back to interactive browser authentication, which will prompt the user to log in via a browser window.

            TokenCachePersistenceOptions persistenceOptions = new TokenCachePersistenceOptions
            {
                Name = "RegistryScanner_Cache"
            };
            InteractiveBrowserCredentialOptions interactiveOptions = new InteractiveBrowserCredentialOptions
            {
                TenantId = AZURE_TENANT_ID,
                ClientId = AZURE_CLIENT_ID,
                TokenCachePersistenceOptions = persistenceOptions
            };
            InteractiveBrowserCredential interactiveCredential = new InteractiveBrowserCredential(interactiveOptions);

            AzureClient = new SecretClient(new Uri(kvUri), interactiveCredential);

            if (await HasAccessAsync(AzureClient, nameof(ConfigKeys.APIKEY)))
            {
                return true;
            }

            if (!isAuthorized)
            {
                throw new Azure.RequestFailedException("Failed to authenticate to Azure Key Vault. Please ensure you have the correct permissions and try again.");
            }

            return isAuthorized;
        }
        private async Task<bool> HasAccessAsync(SecretClient client, string secretProbeName)
        {
            try
            {
                await client.GetSecretAsync(secretProbeName);
                return true;
            }
            catch (Azure.RequestFailedException ex) when (ex.Status == 401 || ex.Status == 403)
            {
                return false;
            }
        }
        #endregion
        #region Google Cloud Secret Manager
        public void LoadFromGCPSecretManager()
        {
            PORT = ConfigurationManager.AppSettings[nameof(ConfigKeys.PORT)];
            ENV = ConfigurationManager.AppSettings[nameof(ConfigKeys.ENV)];
            HOST = ConfigurationManager.AppSettings[nameof(ConfigKeys.HOST)];
            SID = ConfigurationManager.AppSettings[nameof(ConfigKeys.SID)];
            AUTH_TIMEOUT = ConfigurationManager.AppSettings[nameof(ConfigKeys.AUTH_TIMEOUT)];
            AUTH_SVC_URL = ConfigurationManager.AppSettings[nameof(ConfigKeys.AUTH_SVC_URL)];
            API_URL = ConfigurationManager.AppSettings[nameof(ConfigKeys.API_URL)];
            GOOGLE_PROJECT_ID = ConfigurationManager.AppSettings[nameof(ConfigKeys.GOOGLE_PROJECT_ID)];

            USERNAME = GetGoogleSecretValue(nameof(ConfigKeys.USERNAME));
            PASSWORD = GetGoogleSecretValue(nameof(ConfigKeys.PASSWORD));
            CLIENT_ID = GetGoogleSecretValue(nameof(ConfigKeys.CLIENT_ID));
            CLIENT_ACCOUNT = GetGoogleSecretValue(nameof(ConfigKeys.CLIENT_ACCOUNT));
            ACCOUNT_ID = GetGoogleSecretValue(nameof(ConfigKeys.ACCOUNT_ID));
            APIKEY = GetGoogleSecretValue(nameof(ConfigKeys.APIKEY));

            TWAINSDKUSERNAME = GetGoogleSecretValue(nameof(ConfigKeys.TWAINSDKUSERNAME));
            TWAINSDKEMAIL = GetGoogleSecretValue(nameof(ConfigKeys.TWAINSDKEMAIL));
            TWAINSDKKEY = GetGoogleSecretValue(nameof(ConfigKeys.TWAINSDKKEY));

        }
        /* This method can be used to retrieve a secret value from Google Cloud Secret Manager. 
        It creates a SecretManagerServiceClient instance, constructs the secret name using the provided secretId and the project ID, and then accesses the secret version to retrieve the secret value.
        The application uses Application Default Credentials with a Workload Identity Federation configuration file. 
        Authentication is performed via AD FS using Windows Integrated Authentication through WWAuth, and authorization is enforced by service account impersonation. 
        No static credentials or secrets are embedded in source code or binaries.
         */
        public static string GetGoogleSecretValue(string secretId)
        {
            GoogleClient = SecretManagerServiceClient.Create(); // thread-safe single instance [1](https://docs.cloud.google.com/dotnet/docs/reference/Google.Cloud.SecretManager.V1/latest)[2](https://docs.cloud.google.com/secret-manager/docs/samples/secretmanager-access-secret-version)

            var name = new SecretVersionName(GOOGLE_PROJECT_ID, secretId, "latest"); // [2](https://docs.cloud.google.com/secret-manager/docs/samples/secretmanager-access-secret-version)
            var result = GoogleClient.AccessSecretVersion(name);                   // [2](https://docs.cloud.google.com/secret-manager/docs/samples/secretmanager-access-secret-version)[3](https://docs.cloud.google.com/dotnet/docs/reference/Google.Cloud.SecretManager.V1/latest/Google.Cloud.SecretManager.V1.SecretManagerServiceClient)
            return result.Payload.Data.ToStringUtf8();                        // [2](https://docs.cloud.google.com/secret-manager/docs/samples/secretmanager-access-secret-version)
        }
        #endregion
    }
}
