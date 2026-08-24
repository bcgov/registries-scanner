using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RegScan
{
    /// <summary>
    /// Modal dialog that lets the user create a brand new <see cref="BoxObj"/>.
    /// The dialog is seeded with the default values of the box currently selected in
    /// <see cref="frmBoxManagement"/> so the user has a sensible starting point. All accession
    /// number fields (sequence, schedule, and box number) are presented for review and can be
    /// edited before the box is submitted to the DRS API through <see cref="BoxAPI.CreateBox"/>.
    /// The title bar and heading make it clear that a new box is being created rather than an
    /// existing box being edited.
    /// </summary>
    partial class frmBoxCreate : Form
    {
        private BoxObj _newBox;
        
        /// <summary>
        /// Gets the box that was successfully created, or <c>null</c> when the user cancelled
        /// or creation failed.
        /// </summary>
        public BoxObj CreatedBox { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="frmBoxCreate"/> class.
        /// </summary>
        /// <param name="defaults">
        /// The box whose default values are used to seed the new box. This is typically the
        /// current box shown in <see cref="frmBoxManagement"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="defaults"/> is <c>null</c>.
        /// </exception>
        public frmBoxCreate(BoxObj defaults)
        {
            if (defaults == null)
                throw new ArgumentNullException(nameof(defaults));

            InitializeComponent();

            // Create a new box from the default values - applying standard updates
            _newBox = BoxObj.CreateFromDefaults(defaults);

            // Seed the editable fields with the supplied default values so the user can review
            // and adjust them before creating the box.
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

            if (!TryParseField(maskedTextBoxSequenceNumber.Text, "Sequence", out sequence))
                errors.Add("Sequence number must be a numeric value greater than zero.");
            if (!TryParseField(maskedTextBoxScheduleNumber.Text, "Schedule", out schedule))
                errors.Add("Schedule number must be a numeric value greater than zero.");
            if (!TryParseField(maskedTextBoxBoxNumber.Text, "Box Number", out boxNumber))
                errors.Add("Box number must be a numeric value greater than zero.");

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
        /// Attempts to parse a masked text box value into a non-zero integer.
        /// </summary>
        /// <param name="text">The raw text entered by the user.</param>
        /// <param name="fieldName">The friendly field name used for logging.</param>
        /// <param name="value">The parsed value when successful; otherwise zero.</param>
        /// <returns><c>true</c> when the text is a non-zero integer; otherwise <c>false</c>.</returns>
        private static bool TryParseField(string text, string fieldName, out int value)
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
            if (!TryValidateFields(out int sequence, out int schedule, out int boxNumber))
            {
                DialogResult = DialogResult.None; // keep the dialog open
                return;
            }

            // Confirm the details with the user before contacting the API.
            var confirm = MessageBox.Show(this,
                "Please confirm the new box details:\n\n" + _newBox.InfoString,
                "Create New Box", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (confirm != DialogResult.OK)
            {
                DialogResult = DialogResult.None;
                return;
            }

            try
            {
                string response = BoxAPI.CreateBox(_newBox);

                if (string.IsNullOrEmpty(response))
                    throw new ApplicationException("The API returned an empty response.");

                // Update the new box with any server-assigned values (such as the box id).
                BoxObj createdBox =
                    Newtonsoft.Json.JsonConvert.DeserializeObject<BoxObj>(response) ?? _newBox;

                // Keep the in-memory list in sync so the new box is immediately available.
                BoxObj.AddBoxToList(createdBox);

                CreatedBox = createdBox;

                MessageBox.Show(this, "Box created successfully!", "Box Created",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception err)
            {
                CreatedBox = null;
                string msg = "There was an error creating the box. Please try again, or contact " +
                    "support if the problem persists.\n\n" + err.Message;
                MessageBox.Show(this, msg, "Unable to Create Box", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                UtilityObj.WriteLog(UtilityObj.error, err.ToString());
                DialogResult = DialogResult.None; // keep the dialog open for another attempt
            }
        }

        /// <summary>
        /// Cancels the creation workflow without contacting the API.
        /// </summary>
        /// <param name="sender">The cancel button.</param>
        /// <param name="e">Event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            CreatedBox = null;
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
