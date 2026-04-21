using System;
using System.Windows.Forms;

namespace RegScan
{
    public partial class frmEnterBarCode : Form
    {

        private BarCodeString _barCodeString;

        public frmEnterBarCode(BarCodeString _BarCodeString)
        {
            InitializeComponent();
            _barCodeString = _BarCodeString;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtBarCode.Text == "")
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
                _barCodeString.BarCode = txtBarCode.Text;
                this.Close();
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _barCodeString.BarCode = "";
            this.Close();
        }
    }
}
