
using System;
using System.Windows.Forms;
using Vintasoft.Twain;

namespace RegScan
{
    public class ScannerConnectionObj
    {

        private static DeviceManager _deviceManager;
        private Device _currentScanner;

        // Default values for scanner settings.
        private const bool _defaultUseDocumentFeeder = false;
        private const bool _defaultUseDuplex = false;
        private bool _showTwainUI = true;
        private bool _showProgress = true;

        // Indicators if certain settings can be used by the current scanner
        private bool _canUseDocumentFeeder;
        private bool _canUseDuplex;

        // Publicly available properties
        public static DeviceManager DeviceManager { get { return _deviceManager; } }
        public Device CurrentScanner { get { return _currentScanner; } }

        public bool UseDocumentFeeder { get { return _canUseDocumentFeeder && _defaultUseDocumentFeeder; } }
        public bool UseDuplex { get { return _canUseDuplex && _defaultUseDuplex; } }
        public bool ShowTwainUI { get { return _showTwainUI; } set { _showTwainUI = value; } }
        public bool ShowProgressIndicatorUI
        {
            get { return _showProgress; }
            set { _showProgress = value; }
        }

        public bool CanUseDocumentFeeder { get { return _canUseDocumentFeeder; } }
        public bool CanUseDuplex { get { return _canUseDuplex; } }

        public ScannerConnectionObj(Form currentForm)
        {
            // On init get the default devices capabilities
            CheckDeviceCapabilities(currentForm);
        }

        public static DeviceManager GetDeviceManager(Form currentForm)
        {
            OpenDeviceManager(currentForm);

            // return the device manager
            return _deviceManager;
        }

        /// <summary>
        /// If not already created, create and open a connection to a device manager.
        /// </summary>
        public static void OpenDeviceManager(Form currentForm)
        {
            // ensure we have a device manager and it is running
            if (_deviceManager == null)
            {
                _deviceManager = new DeviceManager(currentForm, CountryCode.Canada, LanguageType.EnglishCanadian);
            }
            if (_deviceManager.State != DeviceManagerState.Opened)
            {
                _deviceManager.Open();
            }
        }

        /// <summary>
        /// Used when a task with device manager is complete. Ensures connections are disposed of
        /// and memory can be reallocated.
        /// </summary>
        public void CloseDeviceManager()
        {
            if (_deviceManager != null && _deviceManager.State == DeviceManagerState.Opened)
            {
                _deviceManager.Close();
                _deviceManager.Dispose();
            }
        }

        /// <summary>
        /// Get the default device for the current _deviceManager and open a connection.
        /// </summary>
        public void OpenScanner()
        {
            try
            {
                if (_deviceManager == null || _deviceManager.Devices.Count == 0)
                    throw new InvalidOperationException("No scanning devices are available.");

                // get the default device and open it
                _currentScanner = _deviceManager.DefaultDevice
                    ?? throw new InvalidOperationException("No default scanner configured.");
                _currentScanner.Open();
            }
            catch (Exception e)
            {
                // this should maybe catch the weird error and give us more information
                UtilityObj.WriteLog(UtilityObj.error, e.ToString());
            }
            
        }

        /// <summary>
        /// Given a list of device capabilities return true if the Auto Feeder is included
        /// </summary>
        /// <param name="capabilities">List of device capabilities</param>
        /// <returns>True if the device can use an auto feeder, false otherwise</returns>
        public bool CheckADF(DeviceCapabilityCollection capabilities)
        {
            // Determine if the scanner can use the automatic document feeder
            Vintasoft.Twain.DeviceCapability autoFeederCap = capabilities.Find(
                Vintasoft.Twain.DeviceCapabilityId.AutoFeed);
            return autoFeederCap != null;
        }

        /// <summary>
        /// Given a list of device capabilities return true if Duplex (double sided) scanning is 
        /// included
        /// </summary>
        /// <param name="capabilities">List of device capabilities</param>
        /// <returns>True if the device can use an duplex features, false otherwise</returns>
        public bool CheckDuplex(DeviceCapabilityCollection capabilities)
        {
            // Determine if the scanner can scan double sided documents.
            Vintasoft.Twain.DeviceCapability duplexEnabledCap = capabilities.Find(
                Vintasoft.Twain.DeviceCapabilityId.DuplexEnabled);
            return duplexEnabledCap != null;
        }

        /// <summary>
        /// Get and Set CanUse properties based on the default devices list of capabilities.
        /// </summary>
        public void CheckDeviceCapabilities(Form form)
        {
            // Ensure the device manager is open
            OpenDeviceManager(form);
            // get the default device and open it
            OpenScanner();

            // get list of capabilities for the device
            DeviceCapabilityCollection deviceCapabilities = _currentScanner.Capabilities;

            // Set the device capabilities based on the collection from the device 
            _canUseDocumentFeeder = CheckADF(deviceCapabilities);
            _canUseDuplex = CheckDuplex(deviceCapabilities);
        }
    }
}
