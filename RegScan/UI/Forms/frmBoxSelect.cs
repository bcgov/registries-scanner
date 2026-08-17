using RegScan.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RegScan
{
    /// <summary>
    /// Modal dialog that lets the user locate and select a single <see cref="BoxObj"/>
    /// from an existing collection of boxes.
    /// <para>
    /// The dialog presents one drop-down per box field (status, opened date, sequence,
    /// schedule and box number). The drop-downs act as cascading facets: each one only
    /// offers the values that are still available given the selections already made in
    /// the other drop-downs. A grid below the facets lists every box that matches the
    /// current selection; the user chooses a box from that grid.
    /// </para>
    /// The dialog does not own or copy the source collection; it keeps a single
    /// read-only working list to avoid duplicate storage.
    /// </summary>
    partial class frmBoxSelect : Form
    {
        /// <summary>The special combo entry that represents "no filter for this field".</summary>
        private const string AnyValue = "(Any)";

        private readonly List<BoxObj> _allBoxes;

        /// <summary>
        /// When <c>true</c>, facet <c>SelectedIndexChanged</c> events are ignored. Used
        /// while the facet lists are being rebuilt to prevent re-entrant refreshes.
        /// </summary>
        private bool _suppressFacetEvents;

        /// <summary>
        /// Gets the box chosen by the user, or <c>null</c> if no valid selection was made.
        /// </summary>
        public BoxObj SelectedBox { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="frmBoxSelect"/> class.
        /// </summary>
        /// <param name="boxes">The existing collection of selectable boxes.</param>
        /// <param name="currentBox">
        /// The box to pre-select, if it is present in <paramref name="boxes"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="boxes"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="boxes"/> is empty.
        /// </exception>
        public frmBoxSelect(ICollection<BoxObj> boxes, BoxObj currentBox)
        {
            if (boxes == null)
                throw new ArgumentNullException(nameof(boxes));
            if (boxes.Count == 0)
                throw new ArgumentException("There are no boxes available to select.", nameof(boxes));

            InitializeComponent();

            // Given the collection of boxes in the parameters transfrom into a list for this form
            // removing any null entries
            _allBoxes = boxes.Where(b => b != null).ToList();

            // Build the datagrid and set columns
            BuildDataGrid();
            dataGridBoxes.DoubleClick += Grid_DoubleClick;

            // Populate facet choices and the results grid for the first time.
            RefreshFacets();
            RefreshGrid();

            // Add facet change handlers only after the initial population so the
            // handlers never run while the lists are still being built.
            comboStatus.SelectedIndexChanged += Facet_Changed;
            comboOpenedDate.SelectedIndexChanged += Facet_Changed;
            comboSequence.SelectedIndexChanged += Facet_Changed;
            comboSchedule.SelectedIndexChanged += Facet_Changed;

            PreSelect(currentBox);
        }

        #region UI construction helpers

        /// <summary>
        /// Defines the column headers and sets their approperate
        /// data source to be shown in the results grid.
        /// </summary>
        private void BuildDataGrid()
        {
            dataGridBoxes.AutoGenerateColumns = false;

            // Add the colum headers and their linked data source
            this.dataGridBoxes.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Status",
                DataPropertyName = nameof(BoxObj.Status)
            });
            this.dataGridBoxes.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Opened",
                DataPropertyName = nameof(BoxObj.OpenedDateString)
            });
            this.dataGridBoxes.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Closed",
                DataPropertyName = nameof(BoxObj.ClosedDateString)
            });
            this.dataGridBoxes.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Accession Number",
                DataPropertyName = nameof(BoxObj.AccessionNumberDashes)
            });
            this.dataGridBoxes.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Pages",
                DataPropertyName = nameof(BoxObj.PageCount)
            });
        }

        #endregion

        #region Facet + grid logic

        /// <summary>
        /// Gets the boxes that satisfy every facet except the one identified by
        /// <paramref name="excluded"/>. If a null excluded parameter is passed all facets will
        /// be applied.
        /// </summary>
        /// <param name="excluded">
        /// The facet to ignore when filtering, so its own list of available values can
        /// be computed without excluding options the user could still switch to.
        /// </param>
        private IEnumerable<BoxObj> GetMatches(ComboBox excluded)
        {
            IEnumerable<BoxObj> query = _allBoxes;

            if (excluded != comboStatus)
            {
                // Only include boxes that match the status selected 
                string status = SelectedValue(comboStatus);
                if (status != null)
                    query = query.Where(b => b.Status == status);
            }
            if (excluded != comboOpenedDate)
            {
                // Only include boxes that match the opened date selected 
                string opened = SelectedValue(comboOpenedDate);
                if (opened != null)
                    query = query.Where(b => b.OpenedDateString == opened);
            }
            if (excluded != comboSequence)
            {
                // Only include boxes that match the sequence number selected 
                string seq = SelectedValue(comboSequence);
                if (seq != null)
                    query = query.Where(b => b.SequenceNumber.ToString() == seq);
            }
            if (excluded != comboSchedule)
            {
                // Only include boxes that match the schedule number selected 
                string sched = SelectedValue(comboSchedule);
                if (sched != null)
                    query = query.Where(b => b.ScheduleNumber.ToString() == sched);
            }

            return query;
        }

        /// <summary>
        /// Rebuilds every facet's option list so each only holds values that remain viable given
        /// the selections in the other facets. Current selections are preserved when they are 
        /// still valid.
        /// </summary>
        private void RefreshFacets()
        {
            // ignore new filtering changes while applying the current changes
            _suppressFacetEvents = true;
            try
            {
                // repopulate the filtering lists based off of available options contained box list
                PopulateFacet(comboStatus, GetMatches(comboStatus).Select(b => b.Status));
                PopulateFacet(comboOpenedDate, 
                    GetMatches(comboOpenedDate).Select(b => b.OpenedDateString));
                PopulateFacet(comboSequence, 
                    GetMatches(comboSequence).Select(b => b.SequenceNumber.ToString()));
                PopulateFacet(comboSchedule, 
                    GetMatches(comboSchedule).Select(b => b.ScheduleNumber.ToString()));
            }
            finally
            {
                _suppressFacetEvents = false;
            }
        }

        /// <summary>
        /// Fills a facet combo with "(Any)" followed by the distinct, sorted set of
        /// <paramref name="values"/>, keeping the prior selection when still present.
        /// </summary>
        private static void PopulateFacet(ComboBox combo, IEnumerable<string> values)
        {
            string previous = SelectedValue(combo);

            List<string> options = values
                .Where(v => !string.IsNullOrEmpty(v))
                .Distinct()
                .OrderBy(v => v, StringComparer.OrdinalIgnoreCase)
                .ToList();
            options.Insert(0, AnyValue);

            combo.DataSource = options;

            int index = previous != null ? options.IndexOf(previous) : 0;
            combo.SelectedIndex = index >= 0 ? index : 0;
        }

        /// <summary>Rebinds the results grid to the boxes matching all facets.</summary>
        private void RefreshGrid()
        {
            List<BoxObj> matches = GetMatches(null)
                .OrderByDescending(b => b.BoxId)
                .ToList();

            BoxObj previous = CurrentGridSelection();
            dataGridBoxes.DataSource = matches;

            if (previous != null)
                SelectGridRow(matches.FindIndex(b => b.BoxId == previous.BoxId));
            else if (matches.Count > 0)
                SelectGridRow(0);
        }

        /// <summary>Returns the selected facet value, or <c>null</c> for "(Any)".</summary>
        private static string SelectedValue(ComboBox combo)
        {
            string value = combo.SelectedItem as string;
            return (value == null || value == AnyValue) ? null : value;
        }

        #endregion

        #region Selection helpers

        /// <summary>Gets the box bound to the grid's current row, or <c>null</c>.</summary>
        private BoxObj CurrentGridSelection()
        {
            return dataGridBoxes.CurrentRow != null ? dataGridBoxes.CurrentRow.DataBoundItem as BoxObj : null;
        }

        /// <summary>Selects the grid row at <paramref name="index"/> when valid.</summary>
        private void SelectGridRow(int index)
        {
            if (index < 0 || index >= dataGridBoxes.Rows.Count)
                return;
            dataGridBoxes.ClearSelection();
            dataGridBoxes.Rows[index].Selected = true;
            dataGridBoxes.CurrentCell = dataGridBoxes.Rows[index].Cells[0];
        }

        /// <summary>Highlights the supplied box in the grid when it is present.</summary>
        private void PreSelect(BoxObj currentBox)
        {
            if (currentBox == null)
                return;

            var rows = dataGridBoxes.DataSource as List<BoxObj>;
            
            if (rows == null)
                return;

            SelectGridRow(rows.FindIndex(b => b.BoxId == currentBox.BoxId));
        }

        #endregion

        #region Event handlers

        /// <summary>Re-applies the facets and refreshes the grid on any facet change.</summary>
        private void Facet_Changed(object sender, EventArgs e)
        {
            if (_suppressFacetEvents)
                return;

            RefreshFacets();
            RefreshGrid();
        }

        /// <summary>Clears every facet back to "(Any)".</summary>
        private void btnReset_Click(object sender, EventArgs e)
        {
            _suppressFacetEvents = true;
            try
            {
                comboStatus.SelectedIndex = 0;
                comboOpenedDate.SelectedIndex = 0;
                comboSequence.SelectedIndex = 0;
                comboSchedule.SelectedIndex = 0;
            }
            finally
            {
                _suppressFacetEvents = false;
            }

            RefreshFacets();
            RefreshGrid();
        }

        /// <summary>Treats a double-click on a grid row as confirming the selection.</summary>
        private void Grid_DoubleClick(object sender, EventArgs e)
        {
            if (CurrentGridSelection() != null)
                btnOk.PerformClick();
        }

        /// <summary>Validates the current selection before closing the dialog.</summary>
        private void btnOk_Click(object sender, EventArgs e)
        {
            SelectedBox = CurrentGridSelection();
            if (SelectedBox == null)
            {
                MessageBox.Show(this, "Please select a box before continuing.",
                    "No Box Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.None; // keep the dialog open
            }
            else
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Ensure that the box does not change and close the form
            SelectedBox = null;
            this.Close();
        }
        #endregion

        
    }
}
