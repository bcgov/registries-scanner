using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace RegScan
{
    class PDFObj
    {
        static public string ErrorMessage = "";
        static public string ConvertImagesToPdf(List<Bitmap> _Images)
        {
            string ErrorMessage = "";
            string fileName = Path.GetTempFileName().Replace(".tmp", ".pdf");

            try
            {
                // Create and Save the document, then close it.
                var pdf = ImagesToPdf(_Images);
                pdf.Save(fileName);
                pdf.Close();

                // Now let ADOBE PDF Viewer display it.
                System.Diagnostics.Process.Start(fileName);

            }
            catch (Exception _Error)
            {
                ErrorMessage = _Error.Message;
                fileName = "";
            }

            return fileName;
        }

        // Display PDF file.
        static public string DisplayPdf(PdfDocument _PDF)
        {
            string ErrorMessage = "";
            string fileName = Path.GetTempFileName().Replace(".tmp", ".pdf");
            try
            {
                // Create and Save the document, then close it.
                _PDF.Save(fileName);
                _PDF.Close();

                // Now let ADOBE PDF Viewer display it.
                System.Diagnostics.Process.Start(fileName);

            }
            catch (Exception _Error)
            {
                ErrorMessage = _Error.Message;
                fileName = "";
            }

            return fileName;
        }

        // Convert PDF to byte array.
        static public byte[] ConvertPdfToByteArray(PdfDocument _PDF)
        {
            // Save pdf file to memory stream.
            MemoryStream ms = new MemoryStream();
            _PDF.Save(ms, true);
            return ms.ToArray();
        }

        // Creates a PDF file and adds the images to it.
        static public PdfDocument ImagesToPdf(List<Bitmap> images)
        {
            // Create a PDF document.
            var pdf = new PdfDocument();

            // FOREACH image create a new page.
            foreach (var bp in images)
            {
                // Create a new page and add in the image.
                var pdfPage = new PdfPage();
                pdf.AddPage(pdfPage);
                var xgr = XGraphics.FromPdfPage(pdfPage);
                var img = XImage.FromGdiPlusImage(bp);
                xgr.DrawImage(img, 0, 0);

            }

            return pdf;
        }

        static public PdfDocument AddImageToPdf(ImageObj img, PdfDocument pdf)
        {
            // Create a new page and with the document scans specifications
            var pdfPage = new PdfPage();
            pdfPage.Size = img.PageSize;
            pdfPage.Orientation = img.Orientation;

            // Add the new page to the PDF document
            pdf.AddPage(pdfPage);

            // Make the PDF Page a drawable canvas
            var xgr = XGraphics.FromPdfPage(pdfPage);
            // Turn the scanned document into an XImage
            var ximg = XImage.FromGdiPlusImage(img.Image);

            // Get the PDF Pages 
            double pageWidth = pdfPage.Width.Point;
            double pageHeight = pdfPage.Height.Point;

            // Maintain aspect ratio, fit within page, centered
            // Get the shape of the scanned document and PDF page
            double imgAspect = (double)img.Image.Width / img.Image.Height;
            double pageAspect = pageWidth / pageHeight;

            double drawWidth, drawHeight, drawX, drawY;
            // if the images' shape is larger than the pages' shape 
            if (imgAspect > pageAspect)
            {
                // limit the width to the width of the page
                drawWidth = pageWidth;
                // the height is set to the width of the page / the images' shape
                //  -> Pw / (Iw / Ih) -> Pw * Ih / Iw
                //  -> The images aspect ratio scaled to the pages width
                drawHeight = pageWidth / imgAspect;
                // 
                drawX = 0;
                // 
                drawY = (pageHeight - drawHeight) / 2;
            }
            // if the pages' shape is larger than (or equal to) the images' shape 
            else
            {
                // set the height to the pages height
                drawHeight = pageHeight;
                // set the width to the height of the page * the images' shape
                //  -> Ph * (Iw / Ih) -> Ph * Ih / Iw
                //  -> The images aspect ratio scaled to the pages height
                drawWidth = pageHeight * imgAspect;
                //
                drawX = (pageWidth - drawWidth) / 2;
                //
                drawY = 0;
            }

            xgr.DrawImage(ximg, drawX, drawY, drawWidth, drawHeight);

            return pdf;
        }

        /// <summary>
        /// Method called when a user selects the 'Save' button on the main form.
        /// Given a list of images create a new PDF document adding 'drawn' images as pages.
        /// Return the PDF Document.
        /// </summary>
        /// <param name="images">List of ImageObjs to be added to PDF document</param>
        /// <returns>
        ///    A PDF Document containing a page for each of the images in the list
        /// </returns>
        static public PdfDocument ImageListToPdf(List<ImageObj> images, ProgressBar progressBar)
        {
            PdfDocument pdfDoc = new PdfDocument();

            // Percentage updated based on number of images to process
            int updateCount = (int)100 / images.Count;

            foreach (var curImage in images)
            {
                pdfDoc = AddImageToPdf(curImage, pdfDoc);

                // Update progress bar
                progressBar.Value += updateCount;
            }

            return pdfDoc;
        }
    }
}
