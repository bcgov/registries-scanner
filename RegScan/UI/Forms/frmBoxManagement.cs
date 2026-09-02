using RegScan.UI;
using System;
using System.Linq;
using System.Windows.Forms;

namespace RegScan
{
    public partial class frmBoxManagement : Form
    {
        private static BoxObj _currentBox;
        private static int _batchID;

        public static int BatchID {  get {  return _batchID; } }

        /// <summary>
        /// Get and display information about the current working box to the user
        /// </summary>
        public frmBoxManagement()
        {
            // Load UI components
            InitializeComponent();
            
            try
            {
                // Set the list of boxes when the application opens
                BoxObj.RefreshBoxList();
                // From the box list set _currentBox to be the most recently opened box
                _currentBox = GetMostRecentBox();
                
                // Get the highest Batch ID for the current box
                RefreshBatchId();
                // Update the fields of the form
                UpdateFields();
            }
            catch (TypeAccessException err)
            {
                string msg = "Unable to get list of boxes from API. " +
                    "See more information below:\n " + err.Message;
                string title = "Error Setting Box List";
                MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                UtilityObj.WriteLog(UtilityObj.error, err.ToString());
            }
            
        }

        /// <summary>
        /// Gets the box currently selected in this form. Downstream document scanning
        /// and indexing processes can read this value to associate work with a box.
        /// </summary>
        public static BoxObj SelectedBox
        {
            get { return _currentBox; }
        }

        /// <summary>
        /// Used if there are updates to the box record (page count, close date) that
        /// need to be captured by DRS API.
        /// </summary>
        /// <exception cref="ApplicationException">
        /// If an error is hit while attempting to update the box record - clean and rethrow
        /// </exception>
        public void UpdateCurrentBox()
        {
            try
            {
                BoxAPI.UpdateBox(_currentBox);
            }
            catch (Exception e)
            {
                string msg = "Unable to update box record. Current data for \n" +
                                SelectedBox.InfoString + "\nmay be inaccurate.";
                UtilityObj.WriteLog(UtilityObj.error,
                    "Scanned document Image failed PATCH to update box record.\n" +
                    e.ToString());
                throw new ApplicationException(msg);
            }

            // After the box is updated ensure the fields and record match
            UpdateFields();
            BoxObj.UpdateBoxInList(_currentBox);

            
        }

        /// <summary>
        /// Get the box that is has the highest BoxID and is open.
        /// If there are no open boxes default to the highest BoxID
        /// </summary>
        /// <returns>The BoxObj that is currently open and most recently opened</returns>
        public BoxObj GetMostRecentBox()
        {
            // Order by open boxes first, then by BoxIDs 
            return BoxObj.Boxes.Cast<BoxObj>()
                .OrderByDescending(b => !b.IsClosed) 
                .ThenByDescending(b => b.BoxId) 
                .FirstOrDefault();
        }

        /// <summary>
        /// Update the Text Box fields to match the current boxes fields. 
        /// </summary>
        public void UpdateFields()
        {
            // TODO - add check for if _currentBox is null,
            // if yes ask the user if they want to find a box or if they want to open a new box
            // TODO - check if the found values are too long for the fields. 
            maskedTextBoxSequenceNumber.Text = _currentBox.SequenceNumber.ToString().Trim();
            maskedTextBoxScheduleNumber.Text = _currentBox.ScheduleNumber.ToString().Trim();
            maskedTextBoxBoxNumber.Text = _currentBox.BoxNumber.ToString().Trim();
            textBoxOpenedDate.Text = _currentBox.OpenedDateString;
            textBoxClosedDate.Text = _currentBox.ClosedDateString;
            textBoxPageCount.Text = _currentBox.PageCount.ToString();
            textBoxBatchID.Text = _batchID.ToString();

            // If the box is closed show the closed date. If the box is not yet closed show
            // the button to close the box.
            if (!_currentBox.IsClosed)
            {
                textBoxClosedDate.Visible = false;
                btnCloseBox.Visible = true;
            }
            else
            {
                textBoxClosedDate.Visible = true;
                btnCloseBox.Visible = false;
            }

            DocumentObj current = frmScannerDocument.CurrentDocument;

            // Check if there is a Current Document set with an accession number
            if ( current != null && current.AccSet)
            {
                // Check if the Accession number matches the current document if not warn the user
                if(!AccessionNumberObj.CompareAccessionObsj(
                    current.AccessionNumber, _currentBox.AccessionNumber))
                {
                    string msg = "The Accession Number of the selected box does not match " +
                        "the current document. The document can not be saved to this box. " + 
                        "\nPlease check the Accession Numbers.\n" + 
                        "\nCurrent Box: " + current.AccessionNumber.TextDashes + 
                        "\nCurrent Document: " + _currentBox.AccessionNumber.TextDashes;
                    MessageBox.Show(msg, "Accession Number Mismatch", MessageBoxButtons.OK);
                    frmMDIMain.ScannerForm.SetBackgroundAccFields(Theme.WarningBackgroundLight);
                }
                else
                {
                    // ensure all fields are their normal background
                    frmMDIMain.ScannerForm.SetBackgroundAccFields(Theme.Disabled);
                }
            }
            
        }

        /// <summary>
        /// Prompts the user to choose a different box from the existing
        /// <see cref="_boxes"/> collection and refreshes the form to reflect it.
        /// </summary>
        private void btnChangeBox_Click(object sender, EventArgs e)
        {
            // Warn user about an empty or unpopulated collection.
            if (BoxObj.Boxes == null || BoxObj.Boxes.Count == 0)
            {
                MessageBox.Show(this, "There are no boxes available to select.",
                    "Change Box", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Open a new form to allow the user to select a different box 
            using (var dialog = new frmBoxSelect(_currentBox))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return; // user cancelled; leave the current box unchanged.

                BoxObj selected = dialog.SelectedBox;
                if (selected == null)
                    return; // no valid selection; preserve existing state.

                _currentBox = selected;
                RefreshBatchId();
                UpdateFields();
            }
        }

        /// <summary>
        /// Recalculates the batch for the current box by querying the DRS API.
        /// The result is the max BatchID for a given box
        /// </summary>
        private void RefreshBatchId()
        {
            var ret = BoxAPI.GetBatchID(_currentBox.AccessionNumber.Text);
            _batchID = ret;
            _currentBox.MaxBatchID = ret;
        }

        /// <summary>
        /// If the user wants to update the batch ID open a dialog to capture their input
        /// </summary>
        /// <param name="sender">User action selection of the edit icon</param>
        /// <param name="e">Event Data</param>
        private void editBatchID_Click(object sender, EventArgs e)
        {
            int batchID = frmEnterText.ManualBatchID();
            
            // if the batch ID has been changed update the form field and var
            if (batchID != _batchID)
            {
                textBoxBatchID.Text = batchID.ToString();
                _batchID = batchID;
            }
        }

        /// <summary>
        /// Handle the flow of a user closing a box. Confirm and validate fields before putting a
        /// request into DRS API.
        /// </summary>
        /// <param name="sender">Close Box Button</param>
        /// <param name="e">Event Data</param>
        private void btnCloseBox_Click(object sender, EventArgs e)
        {
            // Capture if the box was closed before updating fields
            bool wasClosed = _currentBox.IsClosed;
            string msg;
            string title;
            MessageBoxIcon icon;

            // If the box is already closed we don't need to update it - but check if the user
            // would like to open a different box
            if (wasClosed)
            {
                // set dialog fields
                msg = "Current box is closed. Would you like to switch to a different box?";
                title = "Box Closed";
                icon = MessageBoxIcon.Information;
            }
            // Show the current box info to the user to confirm
            else
            {
                // set the dialog fields
                _currentBox.ClosedDate = DateTime.Now;
                msg = "Please confirm the following:\n\n" + _currentBox.InfoString;
                title = "Closing Box";
                icon = MessageBoxIcon.Warning;
            }
            
            // Show the dialog to the user. The form data will be based on above checks
            var result = MessageBox.Show(msg, title, MessageBoxButtons.OKCancel, icon);

            // if the user cancels ensure the box is closed. No further action is required
            if (result == DialogResult.Cancel)
            { 
                _currentBox.ReopenBox();
                return;
            }
            // if the box was already closed and the user wants to switch boxes start that process
            else if (wasClosed && result == DialogResult.OK)
            {
                btnChangeBox.Select();
                btnChangeBox_Click(sender, e);
                return;
            }
            // if the box was not closed and the user confirmed the information close it
            else if (!wasClosed && result == DialogResult.OK)
            {
                try
                {
                    // Update the box record in DRS API
                    UpdateCurrentBox();
                    // Update the box in the list of boxes
                    BoxObj.UpdateBoxInList(_currentBox);
                    // Update form contents
                    UpdateFields();
                    
                    // Inform user
                    MessageBox.Show("Box has closed successfully!", "Box Closed",
                        MessageBoxButtons.OK);
                }
                // If an error was caught show to user and 'reopen' the box. 
                catch (Exception err)
                {
                    _currentBox.ReopenBox();
                    var errStr = "Hit unexpected issue while attempting to close box. " +
                        "Please review the following error message.\n" + err.Message;
                    MessageBox.Show(errStr, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    UtilityObj.WriteLog(UtilityObj.error, err.ToString());
                }
            }
        }

        /// <summary>
        /// Launches the "Create New Box" workflow. A new <see cref="BoxObj"/> is seeded with the
        /// default values of the current box, presented to the user for review and editing, and
        /// created through the DRS API. On success the newly created box becomes the current box.
        /// </summary>
        /// <param name="sender">The open/create box button.</param>
        /// <param name="e">Event data.</param>
        private void btnOpenBox_Click(object sender, EventArgs e)
        {
            // Use the current box as the source of default values for the new box.
            BoxObj defaults = _currentBox ?? new BoxObj();

            using (var dialog = new frmBoxOpen(defaults))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return; // user cancelled or creation failed; leave state unchanged.

                BoxObj created = dialog.NewBox;
                if (created == null)
                    return; // no valid box was created; preserve existing state.

                // Adopt the newly created box as the current box and refresh the form.
                _currentBox = created;
                RefreshBatchId();
                UpdateFields();
            }
        }
    }

}