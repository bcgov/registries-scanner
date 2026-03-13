
using System.Configuration;

namespace RegScan
{
    class Security
    {
        /*
         * This class is used to encrypt and decrypt the appSettings section in App.config to secure sensitive information such as API keys and credentials.
         * It uses the DataProtectionConfigurationProvider which is a built-in provider that uses Windows Data Protection API (DPAPI) to encrypt and decrypt the configuration sections.
         * The encryption and decryption is applied to the external file specified in the 'configSource' attribute of the appSettings section in App.config.
         * The ForceSave property is set to true to ensure that changes are saved to the external file.
         */

        // Encrypts the specified section in the main app.config file, which will apply protection to the external file specified in 'configSource'.
        public static void EncryptExternalSection(string sectionName)
        {
            // Open the main app.config
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConfigurationSection section = config.GetSection(sectionName);

            if (section != null && !section.SectionInformation.IsProtected)
            {
                // This will apply protection to the file specified in 'configSource'
                section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");

                // Ensure changes are forced to the external file
                section.SectionInformation.ForceSave = true;
                config.Save(ConfigurationSaveMode.Modified);
            }
        }
        // Decrypts the specified section in the main app.config file, which will apply unprotection to the external file specified in 'configSource'.
        public static void DecryptExternalSection(string sectionName)
        {
            // Open the main app.config
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConfigurationSection section = config.GetSection(sectionName);

            if (section != null && section.SectionInformation.IsProtected)
            {
                // This will apply unprotection to the file specified in 'configSource'
                section.SectionInformation.UnprotectSection();

                // Ensure changes are forced to the external file
                section.SectionInformation.ForceSave = true;
                config.Save(ConfigurationSaveMode.Modified);
            }
        }

    }
}
