using BarCodeScanner;
using Utilities;
using System.Drawing;
using System;

namespace RegScan
{
    class BarCodeObj
    {
        /// <summary>
        /// Used to specify what barcode type(s) to detect.
        /// </summary>
        public enum BarcodeType
        {
            /// <summary>Not specified</summary>
            None = 0,
            /// <summary>Code39</summary>
            Code39 = 1,
            /// <summary>EAN/UPC</summary>
            EAN = 2,
            /// <summary>Code128</summary>
            Code128 = 4,
            /// <summary>Use BarcodeType.All for all supported types</summary>
            All = Code39 | EAN | Code128

            // Note: Extend this enum with new types numbered as 8, 16, 32 ... ,
            //       so that we can use bitwise logic: All = Code39 | EAN | <your favorite type here> | ...
        }

        /// <summary>
        /// Used to specify whether to scan a page in vertical direction,
        /// horizontally, or both.
        /// </summary>
        public enum ScanDirection
        {
            /// <summary>Scan top-to-bottom</summary>
            Vertical = 1,
            /// <summary>Scan left-to-right</summary>
            Horizontal = 2
        }

        /// <summary>
        /// parse a given bitmap image for a barcode. The first barcode that was found in the image
        /// is returned. 
        /// </summary>
        /// <param name="bitmapImage">Scanned image</param>
        /// <returns>First barcode found in the bitmapImage</returns>
        static public string ScanForBarcode(Bitmap bitmapImage)
        {
            var barcodes = new System.Collections.ArrayList();
            string barCode = null;
            try
            {
                // Scan the first page for Barcodes
                barcodes = BarCodeObj.Scan(bitmapImage);
                // get the first barcode from the list
                barCode = (string)barcodes[0];
            }
            catch (Exception e)
            {
                // Log the error.
                UtilityObj.WriteLog(UtilityObj.error, "Error trying to scan for barcodes: " +
                    e.ToString());
            }

            return barCode;
        }

        /// <summary>
        /// Scan the image for barcode type 39
        /// </summary>
        /// <param name="_BMP">Image to scan.</param>
        /// <returns>collection of bar code</returns>
        static private System.Collections.ArrayList Scan(Bitmap _BMP)
        {
            var barcodes = new System.Collections.ArrayList();

            BarCodeScanner.BarCodeImageScanner.ScanPage(ref barcodes, _BMP, 100,
                BarCodeScanner.BarCodeImageScanner.ScanDirection.Vertical,
                BarCodeImageScanner.BarcodeType.Code39);

            return barcodes;
        }
    }
}
