using ApiScanner;
using Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Data;

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

        private ScannerParametersModel ApiModel = new ScannerParametersModel();

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

        public void Update()
        {
            copyToModel();
            string resp = ScanningParameterApi.patch(ApiModel);          
        }      

        public void copyToModel()
        {
            ApiModel.maxPagesInBox = MaxPagesInBox;
            ApiModel.useDocumentFeeder = UseDocumentFeeder;
            ApiModel.showTwainUi = ShowTwainUI;
            ApiModel.showTwainProgress = ShowProgressIndicatorUI;
            ApiModel.useFullDuplex = UseDuplex;
            ApiModel.useLowResolution = BlackAndWhiteCheckBox;
        }           
    }
}
