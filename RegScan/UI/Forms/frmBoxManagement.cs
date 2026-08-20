using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RegScan
{
    partial class frmBoxManagement : Form
    {
        private BoxObj _currentBox;
        private int _batchID;

        /// <summary>
        /// Get and display information about the application to the user in a new form.
        /// </summary>
        public frmBoxManagement()
        {
            // Load UI components
            InitializeComponent();
            
            try
            {
                // Set the list of boxes 
                BoxObj.RefreshBoxList();
                // From the box list set _current box to be the most recently opened box
                _currentBox = GetMostRecentBox();
            }
            catch (TypeAccessException err)
            {
                string msg = "Unable to get list of boxes from API. " +
                    "See more information below:\n " + err.Message;
                string title = "Error Setting Box List";
                MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                UtilityObj.WriteLog(UtilityObj.error, err.ToString());
            }
            
            // Get the highest Batch ID for the current box
            RefreshBatchId();
            // Update the fields of the form
            UpdateFields();
        }

        public BoxObj GetMostRecentBox()
        {
            // Get the box that is has the highest BoxID and is open
            // If there are no open boxes default to the highest BoxID
            return BoxObj.Boxes.Cast<BoxObj>()
                .OrderByDescending(b => !b.IsClosed)  // open boxes first
                .ThenByDescending(b => b.BoxId)       // then highest BoxId
                .FirstOrDefault();
        }

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

            // If the boxes is closed show the closed date. If the box is not yet closed show
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
        }

        /// <summary>
        /// Gets the box currently selected in this form. Downstream document scanning
        /// and indexing processes can read this value to associate work with a box.
        /// </summary>
        public BoxObj SelectedBox
        {
            get { return _currentBox; }
        }

        /// <summary>
        /// Prompts the user to choose a different box from the existing
        /// <see cref="_boxes"/> collection and refreshes the form to reflect it.
        /// </summary>
        private void btnChangeBox_Click(object sender, EventArgs e)
        {
            // Guard against an empty or unpopulated collection.
            if (BoxObj.Boxes == null || BoxObj.Boxes.Count == 0)
            {
                MessageBox.Show(this, "There are no boxes available to select.",
                    "Change Box", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

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
        /// Because the result is the max BatchID for a given box we can set the _maxBatchID
        /// </summary>
        private void RefreshBatchId()
        {
            var ret = BoxAPI.GetBatchID(_currentBox.AccessionNumber);
            _batchID = ret;
            _currentBox.MaxBatchID = ret;
        }

        private void editBatchID_Click(object sender, EventArgs e)
        {
            int batchID = frmEnterText.ManualBatchID();
            
            if (batchID != _batchID)
            {
                textBoxBatchID.Text = batchID.ToString();
                _batchID = batchID;
            }
        }

        private void btnCloseBox_Click(object sender, EventArgs e)
        {
            bool wasClosed = _currentBox.IsClosed;
            string msg;
            string title;
            MessageBoxIcon icon;

            // if the box is already closed we dont need to update it
            if (wasClosed)
            {
                msg = "Current box is closed. Would you like to switch to a different box?";
                title = "Box Closed";
                icon = MessageBoxIcon.Information;
            }
            // Show the current box info to the user to confirm
            else
            {
                _currentBox.ClosedDate = DateTime.Now;
                msg = "Please confirm the following:\n\n" + _currentBox.InfoString;
                title = "Closing Box";
                icon = MessageBoxIcon.Warning;
            }
            
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
                    var ret = BoxAPI.UpdateBox(_currentBox);
                    // Update the box in the list of boxes
                    BoxObj.UpdateBoxInList(_currentBox);
                    // Update form contents
                    UpdateFields();
                    
                    // Inform user
                    MessageBox.Show("Box has closed successfully!", "Box Closed",
                        MessageBoxButtons.OK);
                }
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
    }

}