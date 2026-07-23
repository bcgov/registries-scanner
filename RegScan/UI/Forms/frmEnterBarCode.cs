using System;
using System.Windows.Forms;

namespace RegScan
{
    /// <summary>
    /// A form used to prompt a user to enter a new barcode string manually.
    /// If the user cancles or the 
    /// </summary>
    public partial class frmEnterBarCode : Form
    {

        private string _barCodeString;
        public string EnteredBarcode { get { return _barCodeString; } }

        public frmEnterBarCode()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles making a request to the user to manually enter a barcode and returns the entered value.
        /// </summary>
        /// <param name="message"> String displayed in the message box </param>
        /// <returns> string of characters entered by the user </returns>
        public static string ManualBarcode(string message)
        {
            string retString = null;
            // Display window to user.
            if (MessageBox.Show(message, "Missing/ Not Found Barcode", 
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // User has indicated they wish to enter a barcode manually. Display a new form.
                var frm = new frmEnterBarCode();
                frm.ShowDialog();
                retString = frm.EnteredBarcode;
            }

            return retString;
        }

        /// <summary>
        /// Catch the event when the user selects "Okay" on the form.
        /// Check if the entered barcode is not empty or null before setting _barCodeString
        /// </summary>
        /// <param name="sender">btnOk</param>
        /// <param name="e">User selecting Okay button</param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBarCode.Text))
            { 
                _barCodeString = txtBarCode.Text;
                this.Close();
            }
        }

        /// <summary>
        /// Handle the event that the user cancels the form. Ensure the _barCodeString is an empty
        /// string and close the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            _barCodeString = "";
            this.Close();
        }
    }
}
