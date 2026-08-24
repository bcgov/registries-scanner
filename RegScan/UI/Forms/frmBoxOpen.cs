using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RegScan
{
    /// <summary>
    /// Modal dialog that lets the user create a brand new <see cref="BoxObj"/>.
    /// The dialog is seeded with the default values of the box currently selected in
    /// <see cref="frmBoxManagement"/>. All object fields are confirmed by the user, then validated
    /// internally before a request to create a new box is sent to the API through 
    /// <see cref="BoxAPI.CreateBox"/>.
    /// </summary>
    partial class frmBoxOpen : Form
    {
        private BoxObj _newBox;
        
        /// <summary>
        /// Gets the box that was successfully created, or <c>null</c> when the user cancelled
        /// or creation failed.
        /// </summary>
        public BoxObj NewBox { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="frmBoxOpen"/> class.
        /// </summary>
        /// <param name="defaults">
        /// The box whose default values are used to seed the new box. This is typically the
        /// current box shown in <see cref="frmBoxManagement"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="defaults"/> is <c>null</c>.
        /// </exception>
        public frmBoxOpen(BoxObj defaults)
        {
            if (defaults == null)
                throw new ArgumentNullException(nameof(defaults));

            InitializeComponent();

            // Create a new box from the default values - applying standard updates
            _newBox = BoxObj.CreateFromDefaults(defaults);

            // Seed the editable fields with the supplied default values so the user can review
            // and adjust them before creating the box.
            // TODO - add event handlers to the fields to validate and update background colour on change
            maskedTextBoxSequenceNumber.Text = _newBox.AccessionNumber.SequenceString.Trim();
            maskedTextBoxScheduleNumber.Text = _newBox.AccessionNumber.ScheduleString.Trim();
            maskedTextBoxBoxNumber.Text = _newBox.AccessionNumber.BoxNumber.ToString().Trim();

            // The opened date cannot be edited.
            textBoxOpenedDate.Text = _newBox.OpenedDateString;
        }

        /// <summary>
        /// Validates the accession number fields entered by the user.
        /// </summary>
        /// <param name="sequence">Parsed sequence number when validation succeeds.</param>
        /// <param name="schedule">Parsed schedule number when validation succeeds.</param>
        /// <param name="boxNumber">Parsed box number when validation succeeds.</param>
        /// <returns>
        /// <c>true</c> when every field contains a valid, non-zero numeric value; otherwise
        /// <c>false</c>.
        /// </returns>
        private bool TryValidateFields(out int sequence, out int schedule, out int boxNumber)
        {
            var errors = new List<string>();

            //TODO - update background colour of fields based on if they are acceptable
            if (!TryParseField(maskedTextBoxSequenceNumber.Text, out sequence))
            {
                errors.Add("Sequence number must be a numeric value greater than zero.");
            }
                
            if (!TryParseField(maskedTextBoxScheduleNumber.Text, out schedule))
            {
                errors.Add("Schedule number must be a numeric value greater than zero.");
            }
                
            if (!TryParseField(maskedTextBoxBoxNumber.Text, out boxNumber))
            {
                errors.Add("Box number must be a numeric value greater than zero.");
            }
                

            if (errors.Count > 0)
            {
                MessageBox.Show(this,
                    "Please correct the following before creating the box:\n\n\t" +
                    string.Join("\n\t", errors),
                    "Invalid Box Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Update the new box to match the entered values
            _newBox.SequenceNumber = sequence;
            _newBox.ScheduleNumber = schedule;
            _newBox.BoxNumber = boxNumber;

            return true;
        }

        /// <summary>
        /// Attempts to parse a text value into a non-zero integer.
        /// </summary>
        /// <param name="text">The raw text entered by the user.</param>
        /// <param name="value">The parsed value when successful; otherwise zero.</param>
        /// <returns><c>true</c> when the text is a non-zero integer; otherwise <c>false</c>.</returns>
        private static bool TryParseField(string text, out int value)
        {
            string trimmed = (text ?? string.Empty).Trim();

            return int.TryParse(trimmed, out value) && value > 0;
        }

        /// <summary>
        /// Validates the entered values, submits the new box to the DRS API, and closes the
        /// dialog on success.
        /// </summary>
        /// <param name="sender">The create button.</param>
        /// <param name="e">Event data.</param>
        private void btnCreate_Click(object sender, EventArgs e)
        {
            // If the fields are unable to be parsed into ints keep the dialog open 
            // so the user can try again
            if (!TryValidateFields(out int sequence, out int schedule, out int boxNumber))
            {
                DialogResult = DialogResult.None;
                return;
            }

            // Confirm the details with the user before contacting the API.
            var confirm = MessageBox.Show(this,
                "Please confirm the new box details:\n\n" + _newBox.InfoString,
                "Create New Box", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (confirm != DialogResult.OK)
            {
                // if the request is not confirmed keep th  dialog open
                DialogResult = DialogResult.None;
                return;
            }

            try
            {
                // Put a request into the DRS API to create a new box record.
                string response = BoxAPI.CreateBox(_newBox);

                if (string.IsNullOrEmpty(response))
                    throw new ApplicationException("The API returned an empty response.");

                // Update the new box with any server-assigned values (such as the box id).
                BoxObj createdBox =
                    Newtonsoft.Json.JsonConvert.DeserializeObject<BoxObj>(response) ?? _newBox;

                // Keep the in-memory list in sync so the new box is immediately available.
                BoxObj.AddBoxToList(createdBox);

                NewBox = createdBox;

                //Inform the user of the success 
                MessageBox.Show(this, "Box created successfully!", "Box Created",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception err)
            {
                //if an error was caught show the warning to the user. Keep the dialog open
                NewBox = null;
                string msg = "There was an error creating the box. Please try again, or contact " +
                    "support if the problem persists.\n\n" + err.Message;
                MessageBox.Show(this, msg, "Unable to Create Box", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                UtilityObj.WriteLog(UtilityObj.error, err.ToString());
            }
        }

        /// <summary>
        /// Cancels the creation workflow without contacting the API.
        /// </summary>
        /// <param name="sender">The cancel button.</param>
        /// <param name="e">Event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            NewBox = null;
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
