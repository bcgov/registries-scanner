
namespace RegScan
{
    public class ScannerSettingObj
    {
        
        public int MaxPagesInBox;
        public bool UseDocumentFeeder;
        public bool ShowTwainUI;
        public bool ShowProgressIndicatorUI;
        public bool UseDuplex;
        public bool BlackAndWhiteCheckBox;
        public bool ShouldTransferAllPages;
        public bool AutoRotateCheckBox;
        public bool AutoDetectBorderCheckBox;
        public bool checkBoxArea;

        public ScannerSettingObj()
        {
            // Because we have default values that can be used we dont need to call the API to get
            // this information. If there was a way to tie settings to a specific user the API
            // might be more useful. 
            //load();

            // Default values
            MaxPagesInBox = 300;
            UseDocumentFeeder = false;
            ShowTwainUI = true;
            ShowProgressIndicatorUI = true;
            UseDuplex = false;
            BlackAndWhiteCheckBox = true;
            ShouldTransferAllPages = true;
            AutoRotateCheckBox = true;
            AutoDetectBorderCheckBox = false;
            checkBoxArea = false;
        }             
    }
}
