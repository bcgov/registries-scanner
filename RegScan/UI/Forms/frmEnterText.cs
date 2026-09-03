using System;
using System.Windows.Forms;

namespace RegScan
{
    /// <summary>
    /// A form used to prompt a user to enter a string manually.
    /// If the user cancels the form ensure no value is stored. 
    /// </summary>
    public partial class frmEnterText : Form
    {

        private string _textString;
        private int _enteredInt;
        public string EnteredText { get { return _textString; } }
        public int EnteredInt { get { return _enteredInt; } }

        public frmEnterText(string labelText)
        {
            InitializeComponent();
            textLabel.Text = labelText;
        }

        /// <summary>
        /// Reset all form fields
        /// </summary>
        private void ClearForm()
        {
            // clear any values set
            _enteredInt = new int();
            _textString = "";
            txtBox.Text = "";
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
                var frm = new frmEnterText("Barcode:");
                frm.ShowDialog();
                retString = frm.EnteredText;
            }

            return retString;
        }

        /// <summary>
        /// Handles making a request to the user to manually enter a batch ID and returns the entered value.
        /// </summary>
        /// <returns> string of characters entered by the user </returns>
        public static int ManualBatchID()
        {
            // Display a new form to get manual Batch ID
            var frm = new frmEnterText("Batch ID:");
            frm.ShowDialog();

            return frm.EnteredInt;
        }

        /// <summary>
        /// Attempt to parse the user input to an int. If there are errors show the warning
        /// to the user.
        /// </summary>
        /// <returns></returns>
        private bool ValidateInt()
        {
            bool isInt = false;
            try
            {
                _enteredInt = Int32.Parse(_textString);
                isInt = true;
            }
            catch (FormatException)
            {
                var msg = "Value must be numeric characters only (0-9). " +
                    "Please try again or select cancel.";
                MessageBox.Show(msg, "Value Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return isInt;
        }

        /// <summary>
        /// Catch the event when the user selects "Okay" on the form.
        /// Check if the entered barcode is not empty or null before setting _textString
        /// </summary>
        /// <param name="sender">btnOk</param>
        /// <param name="e">User selecting Okay button</param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            // Check if the entered value is empty or null
            if (!string.IsNullOrEmpty(txtBox.Text) )
            {
                _textString = txtBox.Text;
            }
            else
            {
                ClearForm();
                // show message to user on why nothing is happening
                var msg = "Please enter a numeric value or select cancel.";
                MessageBox.Show(msg, "Value Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            // Check if the entered value can be convereted to an int.
            // This may need to be updated if this form is ever used to capture non numeric strings
            if (ValidateInt())
                this.Close();
            else
                ClearForm();
            
                
        }

        /// <summary>
        /// Handle the event that the user cancels the form. Ensure the _textString is an empty
        /// string and close the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            _textString = "";
            this.Close();
        }
    }
}
