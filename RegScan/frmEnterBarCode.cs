using System;
using System.Windows.Forms;

namespace RegScan
{
    public partial class frmEnterBarCode : Form
    {

        private string _barCodeString;

        /// <summary>
        /// Handles making a request to the user to manually enter a barcode and returns the entered value.
        /// </summary>
        /// <param name="message"> String displayed in the message box </param>
        /// <returns> string of characters entered by the user </returns>
        public static string ManualBarcode(string message)
        {
            string enteredBarcode = null;

            // Display window to user.
            if (MessageBox.Show(message, "Missing/ Not Found Barcode", 
                MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                // User has indicated they wish to enter a barcode manually. Display a new form.
                var frm = new frmEnterBarCode(enteredBarcode);
                frm.ShowDialog();
            }

            return enteredBarcode;
        }

        public frmEnterBarCode(string _BarCodeString)
        {
            InitializeComponent();
            _barCodeString = _BarCodeString;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBarCode.Text))
                return;

            var document = DocumentObj.Find(txtBarCode.Text);

            // NOTE - There is not an instance of this method being used which means that the class
            // variable ErrorMessage will not be held in a standard manner - it may work as intended
            // but it is just as likely to never hold an error message.
            // This method should be refactored to return an error message or throw an exception if there is an error.
            if (DocumentObj.ErrorMessage != "")
                MessageBox.Show(DocumentObj.ErrorMessage);
            else
            {
                _barCodeString = txtBarCode.Text;
                this.Close();
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _barCodeString = "";
            this.Close();
        }
    }
}
