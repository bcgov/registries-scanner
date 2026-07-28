using BarCodeScanner;
using System.Drawing;
using System;

namespace RegScan
{
    class BarCodeObj
    {
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
