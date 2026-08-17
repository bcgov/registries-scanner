using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace RegScan
{
    partial class frmBoxManagement : Form
    {
        private BoxObj _currentBox;
        private ICollection<BoxObj> _boxes;
        private int _batchID;

        /// <summary>
        /// Get and display information about the application to the user in a new form.
        /// </summary>
        public frmBoxManagement()
        {
            // Load UI components
            InitializeComponent();
            // Pull latest box information from API
            SetBoxes();
            // Update the fields of the form
            UpdateFields();
        }

        public void SetBoxes()
        {
            // empty datetime fields will default to this date
            var defaultTime = new DateTime(1900, 1, 2);

            string boxList = BoxAPI.GetAllBoxes();
            if (string.IsNullOrEmpty(boxList))
            {
                throw new TypeAccessException("API returned null or empty string.");
            }

            // update the JSON payload to an iterable list
            _boxes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BoxObj>>(boxList);

            // Get the box that is has the highest BoxID and is open
            // If there are no open boxes default to the highest BoxID
            _currentBox = _boxes.Cast<BoxObj>()
                .OrderByDescending(b => !b.IsClosed)  // open boxes first
                .ThenByDescending(b => b.BoxId)                    // then highest BoxId
                .FirstOrDefault();

            // Get the highest batchID for the most recent box
            RefreshBatchId();
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
            if (_boxes == null || _boxes.Count == 0)
            {
                MessageBox.Show(this, "There are no boxes available to select.",
                    "Change Box", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dialog = new frmBoxSelect(_boxes, _currentBox))
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
        /// Recalculates the batch identifier for the current box.
        /// </summary>
        private void RefreshBatchId()
        {
            _batchID = BoxAPI.GetBatchID(_currentBox.AccessionNumber);
        }
    }

}