using System.Windows.Forms;

namespace RegScan
{
    partial class frmBoxManagement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBoxManagement));
            this.CurrentBoxLabel = new System.Windows.Forms.Label();
            this.flowLayoutPanelOpened = new System.Windows.Forms.FlowLayoutPanel();
            this.BoxOpenDateLabel = new System.Windows.Forms.Label();
            this.textBoxOpenedDate = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelSequence = new System.Windows.Forms.FlowLayoutPanel();
            this.SequenceLabel = new System.Windows.Forms.Label();
            this.maskedTextBoxSequenceNumber = new System.Windows.Forms.MaskedTextBox();
            this.flowLayoutPanelSchedule = new System.Windows.Forms.FlowLayoutPanel();
            this.ScheduleLabel = new System.Windows.Forms.Label();
            this.maskedTextBoxScheduleNumber = new System.Windows.Forms.MaskedTextBox();
            this.flowLayoutPanelBoxNumber = new System.Windows.Forms.FlowLayoutPanel();
            this.boxNumberLabel = new System.Windows.Forms.Label();
            this.maskedTextBoxBoxNumber = new System.Windows.Forms.MaskedTextBox();
            this.flowLayoutPanelClosed = new System.Windows.Forms.FlowLayoutPanel();
            this.BoxCloseDateLabel = new System.Windows.Forms.Label();
            this.btnCloseBox = new System.Windows.Forms.Button();
            this.textBoxClosedDate = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel10 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel11 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel12 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxAccessionNumber = new System.Windows.Forms.GroupBox();
            this.groupBoxDates = new System.Windows.Forms.GroupBox();
            this.btnOpenBox = new System.Windows.Forms.Button();
            this.groupBoxPageCount = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanelTotalPages = new System.Windows.Forms.FlowLayoutPanel();
            this.BoxTotalPagesLabel = new System.Windows.Forms.Label();
            this.textBoxPageCount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxBatchID = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanelCurrentBatchID = new System.Windows.Forms.FlowLayoutPanel();
            this.editBatchID = new System.Windows.Forms.Label();
            this.textBoxBatchID = new System.Windows.Forms.TextBox();
            this.btnChangeBox = new System.Windows.Forms.Button();
            this.flowLayoutPanelBoxMaintinence = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelBoxMaintButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.lblBoxMaintenance = new System.Windows.Forms.Label();
            this.flowLayoutPanelOpened.SuspendLayout();
            this.flowLayoutPanelSequence.SuspendLayout();
            this.flowLayoutPanelSchedule.SuspendLayout();
            this.flowLayoutPanelBoxNumber.SuspendLayout();
            this.flowLayoutPanelClosed.SuspendLayout();
            this.groupBoxAccessionNumber.SuspendLayout();
            this.groupBoxDates.SuspendLayout();
            this.groupBoxPageCount.SuspendLayout();
            this.flowLayoutPanelTotalPages.SuspendLayout();
            this.groupBoxBatchID.SuspendLayout();
            this.flowLayoutPanelCurrentBatchID.SuspendLayout();
            this.flowLayoutPanelBoxMaintinence.SuspendLayout();
            this.flowLayoutPanelBoxMaintButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // CurrentBoxLabel
            // 
            this.CurrentBoxLabel.AutoSize = true;
            this.CurrentBoxLabel.Font = new System.Drawing.Font("BC Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentBoxLabel.ForeColor = System.Drawing.Color.Black;
            this.CurrentBoxLabel.Location = new System.Drawing.Point(11, 11);
            this.CurrentBoxLabel.Margin = new System.Windows.Forms.Padding(2);
            this.CurrentBoxLabel.Name = "CurrentBoxLabel";
            this.CurrentBoxLabel.Size = new System.Drawing.Size(0, 26);
            this.CurrentBoxLabel.TabIndex = 0;
            // 
            // flowLayoutPanelOpened
            // 
            this.flowLayoutPanelOpened.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanelOpened.Controls.Add(this.BoxOpenDateLabel);
            this.flowLayoutPanelOpened.Controls.Add(this.textBoxOpenedDate);
            this.flowLayoutPanelOpened.Location = new System.Drawing.Point(7, 29);
            this.flowLayoutPanelOpened.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanelOpened.MinimumSize = new System.Drawing.Size(155, 71);
            this.flowLayoutPanelOpened.Name = "flowLayoutPanelOpened";
            this.flowLayoutPanelOpened.Padding = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanelOpened.Size = new System.Drawing.Size(155, 71);
            this.flowLayoutPanelOpened.TabIndex = 101;
            // 
            // BoxOpenDateLabel
            // 
            this.BoxOpenDateLabel.AutoSize = true;
            this.BoxOpenDateLabel.Font = new System.Drawing.Font("BC Sans Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BoxOpenDateLabel.Location = new System.Drawing.Point(7, 4);
            this.BoxOpenDateLabel.Name = "BoxOpenDateLabel";
            this.BoxOpenDateLabel.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.BoxOpenDateLabel.Size = new System.Drawing.Size(47, 26);
            this.BoxOpenDateLabel.TabIndex = 99;
            this.BoxOpenDateLabel.Text = "Open";
            // 
            // textBoxOpenedDate
            // 
            this.textBoxOpenedDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(235)))), ((int)(((byte)(233)))));
            this.textBoxOpenedDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxOpenedDate.Font = new System.Drawing.Font("BC Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxOpenedDate.Location = new System.Drawing.Point(7, 33);
            this.textBoxOpenedDate.MinimumSize = new System.Drawing.Size(133, 29);
            this.textBoxOpenedDate.Name = "textBoxOpenedDate";
            this.textBoxOpenedDate.Size = new System.Drawing.Size(141, 29);
            this.textBoxOpenedDate.TabIndex = 100;
            this.textBoxOpenedDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel4.ForeColor = System.Drawing.Color.Black;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(3, 118);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.flowLayoutPanel4.Size = new System.Drawing.Size(0, 0);
            this.flowLayoutPanel4.TabIndex = 103;
            // 
            // flowLayoutPanelSequence
            // 
            this.flowLayoutPanelSequence.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanelSequence.Controls.Add(this.SequenceLabel);
            this.flowLayoutPanelSequence.Controls.Add(this.maskedTextBoxSequenceNumber);
            this.flowLayoutPanelSequence.Location = new System.Drawing.Point(7, 29);
            this.flowLayoutPanelSequence.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanelSequence.MinimumSize = new System.Drawing.Size(100, 71);
            this.flowLayoutPanelSequence.Name = "flowLayoutPanelSequence";
            this.flowLayoutPanelSequence.Padding = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanelSequence.Size = new System.Drawing.Size(115, 71);
            this.flowLayoutPanelSequence.TabIndex = 101;
            // 
            // SequenceLabel
            // 
            this.SequenceLabel.AutoSize = true;
            this.SequenceLabel.Font = new System.Drawing.Font("BC Sans Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SequenceLabel.Location = new System.Drawing.Point(7, 4);
            this.SequenceLabel.Name = "SequenceLabel";
            this.SequenceLabel.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.SequenceLabel.Size = new System.Drawing.Size(75, 26);
            this.SequenceLabel.TabIndex = 99;
            this.SequenceLabel.Text = "Sequence";
            // 
            // maskedTextBoxSequenceNumber
            // 
            this.maskedTextBoxSequenceNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(235)))), ((int)(((byte)(233)))));
            this.maskedTextBoxSequenceNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.maskedTextBoxSequenceNumber.Font = new System.Drawing.Font("BC Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maskedTextBoxSequenceNumber.Location = new System.Drawing.Point(8, 34);
            this.maskedTextBoxSequenceNumber.Margin = new System.Windows.Forms.Padding(4);
            this.maskedTextBoxSequenceNumber.Mask = "00";
            this.maskedTextBoxSequenceNumber.MaximumSize = new System.Drawing.Size(141, 29);
            this.maskedTextBoxSequenceNumber.MinimumSize = new System.Drawing.Size(74, 29);
            this.maskedTextBoxSequenceNumber.Name = "maskedTextBoxSequenceNumber";
            this.maskedTextBoxSequenceNumber.PromptChar = ' ';
            this.maskedTextBoxSequenceNumber.ResetOnSpace = false;
            this.maskedTextBoxSequenceNumber.Size = new System.Drawing.Size(95, 29);
            this.maskedTextBoxSequenceNumber.TabIndex = 98;
            this.maskedTextBoxSequenceNumber.Text = "00";
            this.maskedTextBoxSequenceNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // flowLayoutPanelSchedule
            // 
            this.flowLayoutPanelSchedule.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanelSchedule.Controls.Add(this.ScheduleLabel);
            this.flowLayoutPanelSchedule.Controls.Add(this.maskedTextBoxScheduleNumber);
            this.flowLayoutPanelSchedule.Location = new System.Drawing.Point(131, 29);
            this.flowLayoutPanelSchedule.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanelSchedule.MinimumSize = new System.Drawing.Size(100, 71);
            this.flowLayoutPanelSchedule.Name = "flowLayoutPanelSchedule";
            this.flowLayoutPanelSchedule.Padding = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanelSchedule.Size = new System.Drawing.Size(115, 71);
            this.flowLayoutPanelSchedule.TabIndex = 101;
            // 
            // ScheduleLabel
            // 
            this.ScheduleLabel.AutoSize = true;
            this.ScheduleLabel.Font = new System.Drawing.Font("BC Sans Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScheduleLabel.Location = new System.Drawing.Point(7, 4);
            this.ScheduleLabel.Name = "ScheduleLabel";
            this.ScheduleLabel.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.ScheduleLabel.Size = new System.Drawing.Size(70, 26);
            this.ScheduleLabel.TabIndex = 99;
            this.ScheduleLabel.Text = "Schedule";
            // 
            // maskedTextBoxScheduleNumber
            // 
            this.maskedTextBoxScheduleNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(235)))), ((int)(((byte)(233)))));
            this.maskedTextBoxScheduleNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.maskedTextBoxScheduleNumber.Font = new System.Drawing.Font("BC Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maskedTextBoxScheduleNumber.Location = new System.Drawing.Point(8, 34);
            this.maskedTextBoxScheduleNumber.Margin = new System.Windows.Forms.Padding(4);
            this.maskedTextBoxScheduleNumber.Mask = "0000";
            this.maskedTextBoxScheduleNumber.MaximumSize = new System.Drawing.Size(141, 29);
            this.maskedTextBoxScheduleNumber.MinimumSize = new System.Drawing.Size(74, 29);
            this.maskedTextBoxScheduleNumber.Name = "maskedTextBoxScheduleNumber";
            this.maskedTextBoxScheduleNumber.PromptChar = ' ';
            this.maskedTextBoxScheduleNumber.ResetOnSpace = false;
            this.maskedTextBoxScheduleNumber.Size = new System.Drawing.Size(95, 29);
            this.maskedTextBoxScheduleNumber.TabIndex = 100;
            this.maskedTextBoxScheduleNumber.Text = "0000";
            this.maskedTextBoxScheduleNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // flowLayoutPanelBoxNumber
            // 
            this.flowLayoutPanelBoxNumber.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanelBoxNumber.Controls.Add(this.boxNumberLabel);
            this.flowLayoutPanelBoxNumber.Controls.Add(this.maskedTextBoxBoxNumber);
            this.flowLayoutPanelBoxNumber.Location = new System.Drawing.Point(255, 29);
            this.flowLayoutPanelBoxNumber.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanelBoxNumber.MinimumSize = new System.Drawing.Size(100, 71);
            this.flowLayoutPanelBoxNumber.Name = "flowLayoutPanelBoxNumber";
            this.flowLayoutPanelBoxNumber.Padding = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanelBoxNumber.Size = new System.Drawing.Size(115, 71);
            this.flowLayoutPanelBoxNumber.TabIndex = 102;
            // 
            // boxNumberLabel
            // 
            this.boxNumberLabel.AutoSize = true;
            this.boxNumberLabel.Font = new System.Drawing.Font("BC Sans Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boxNumberLabel.Location = new System.Drawing.Point(7, 4);
            this.boxNumberLabel.Name = "boxNumberLabel";
            this.boxNumberLabel.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.boxNumberLabel.Size = new System.Drawing.Size(95, 26);
            this.boxNumberLabel.TabIndex = 99;
            this.boxNumberLabel.Text = "Box Number";
            // 
            // maskedTextBoxBoxNumber
            // 
            this.maskedTextBoxBoxNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(235)))), ((int)(((byte)(233)))));
            this.maskedTextBoxBoxNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.maskedTextBoxBoxNumber.Font = new System.Drawing.Font("BC Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maskedTextBoxBoxNumber.Location = new System.Drawing.Point(8, 34);
            this.maskedTextBoxBoxNumber.Margin = new System.Windows.Forms.Padding(4);
            this.maskedTextBoxBoxNumber.Mask = "0000";
            this.maskedTextBoxBoxNumber.MaximumSize = new System.Drawing.Size(141, 29);
            this.maskedTextBoxBoxNumber.MinimumSize = new System.Drawing.Size(74, 29);
            this.maskedTextBoxBoxNumber.Name = "maskedTextBoxBoxNumber";
            this.maskedTextBoxBoxNumber.PromptChar = ' ';
            this.maskedTextBoxBoxNumber.ResetOnSpace = false;
            this.maskedTextBoxBoxNumber.Size = new System.Drawing.Size(95, 29);
            this.maskedTextBoxBoxNumber.TabIndex = 113;
            this.maskedTextBoxBoxNumber.Text = "0000";
            this.maskedTextBoxBoxNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // flowLayoutPanelClosed
            // 
            this.flowLayoutPanelClosed.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanelClosed.Controls.Add(this.BoxCloseDateLabel);
            this.flowLayoutPanelClosed.Controls.Add(this.btnCloseBox);
            this.flowLayoutPanelClosed.Controls.Add(this.textBoxClosedDate);
            this.flowLayoutPanelClosed.Location = new System.Drawing.Point(170, 29);
            this.flowLayoutPanelClosed.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanelClosed.MinimumSize = new System.Drawing.Size(155, 71);
            this.flowLayoutPanelClosed.Name = "flowLayoutPanelClosed";
            this.flowLayoutPanelClosed.Padding = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanelClosed.Size = new System.Drawing.Size(155, 71);
            this.flowLayoutPanelClosed.TabIndex = 102;
            // 
            // BoxCloseDateLabel
            // 
            this.BoxCloseDateLabel.AutoSize = true;
            this.BoxCloseDateLabel.Font = new System.Drawing.Font("BC Sans Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BoxCloseDateLabel.Location = new System.Drawing.Point(7, 4);
            this.BoxCloseDateLabel.Name = "BoxCloseDateLabel";
            this.BoxCloseDateLabel.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.BoxCloseDateLabel.Size = new System.Drawing.Size(45, 26);
            this.BoxCloseDateLabel.TabIndex = 99;
            this.BoxCloseDateLabel.Text = "Close";
            // 
            // btnCloseBox
            // 
            this.btnCloseBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCloseBox.Font = new System.Drawing.Font("BC Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCloseBox.ForeColor = System.Drawing.Color.Black;
            this.btnCloseBox.Location = new System.Drawing.Point(7, 33);
            this.btnCloseBox.MinimumSize = new System.Drawing.Size(133, 29);
            this.btnCloseBox.Name = "btnCloseBox";
            this.btnCloseBox.Size = new System.Drawing.Size(141, 29);
            this.btnCloseBox.TabIndex = 113;
            this.btnCloseBox.Text = "Close Box";
            this.btnCloseBox.UseVisualStyleBackColor = true;
            this.btnCloseBox.Visible = false;
            // 
            // textBoxClosedDate
            // 
            this.textBoxClosedDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(235)))), ((int)(((byte)(233)))));
            this.textBoxClosedDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxClosedDate.Font = new System.Drawing.Font("BC Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxClosedDate.Location = new System.Drawing.Point(7, 68);
            this.textBoxClosedDate.MinimumSize = new System.Drawing.Size(133, 29);
            this.textBoxClosedDate.Name = "textBoxClosedDate";
            this.textBoxClosedDate.Size = new System.Drawing.Size(141, 29);
            this.textBoxClosedDate.TabIndex = 101;
            this.textBoxClosedDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // flowLayoutPanel10
            // 
            this.flowLayoutPanel10.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel10.Location = new System.Drawing.Point(9, 118);
            this.flowLayoutPanel10.Name = "flowLayoutPanel10";
            this.flowLayoutPanel10.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.flowLayoutPanel10.Size = new System.Drawing.Size(0, 0);
            this.flowLayoutPanel10.TabIndex = 104;
            // 
            // flowLayoutPanel11
            // 
            this.flowLayoutPanel11.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel11.Location = new System.Drawing.Point(15, 118);
            this.flowLayoutPanel11.Name = "flowLayoutPanel11";
            this.flowLayoutPanel11.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.flowLayoutPanel11.Size = new System.Drawing.Size(0, 0);
            this.flowLayoutPanel11.TabIndex = 105;
            // 
            // flowLayoutPanel12
            // 
            this.flowLayoutPanel12.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel12.Location = new System.Drawing.Point(974, 3);
            this.flowLayoutPanel12.Name = "flowLayoutPanel12";
            this.flowLayoutPanel12.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.flowLayoutPanel12.Size = new System.Drawing.Size(0, 0);
            this.flowLayoutPanel12.TabIndex = 106;
            // 
            // groupBoxAccessionNumber
            // 
            this.groupBoxAccessionNumber.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxAccessionNumber.Controls.Add(this.flowLayoutPanelBoxNumber);
            this.groupBoxAccessionNumber.Controls.Add(this.flowLayoutPanelSchedule);
            this.groupBoxAccessionNumber.Controls.Add(this.flowLayoutPanelSequence);
            this.groupBoxAccessionNumber.Font = new System.Drawing.Font("BC Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxAccessionNumber.Location = new System.Drawing.Point(0, 3);
            this.groupBoxAccessionNumber.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.groupBoxAccessionNumber.Name = "groupBoxAccessionNumber";
            this.groupBoxAccessionNumber.Size = new System.Drawing.Size(377, 109);
            this.groupBoxAccessionNumber.TabIndex = 109;
            this.groupBoxAccessionNumber.TabStop = false;
            this.groupBoxAccessionNumber.Text = "Accession Number";
            // 
            // groupBoxDates
            // 
            this.groupBoxDates.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxDates.Controls.Add(this.flowLayoutPanelClosed);
            this.groupBoxDates.Controls.Add(this.flowLayoutPanelOpened);
            this.groupBoxDates.Font = new System.Drawing.Font("BC Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxDates.Location = new System.Drawing.Point(377, 3);
            this.groupBoxDates.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.groupBoxDates.Name = "groupBoxDates";
            this.groupBoxDates.Size = new System.Drawing.Size(332, 109);
            this.groupBoxDates.TabIndex = 110;
            this.groupBoxDates.TabStop = false;
            this.groupBoxDates.Text = "Dates";
            // 
            // btnOpenBox
            // 
            this.btnOpenBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnOpenBox.Font = new System.Drawing.Font("BC Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenBox.ForeColor = System.Drawing.Color.Black;
            this.btnOpenBox.Location = new System.Drawing.Point(3, 3);
            this.btnOpenBox.Name = "btnOpenBox";
            this.btnOpenBox.Size = new System.Drawing.Size(136, 31);
            this.btnOpenBox.TabIndex = 114;
            this.btnOpenBox.Text = "Open Box";
            this.btnOpenBox.UseVisualStyleBackColor = true;
            // 
            // groupBoxPageCount
            // 
            this.groupBoxPageCount.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxPageCount.Controls.Add(this.flowLayoutPanelTotalPages);
            this.groupBoxPageCount.Font = new System.Drawing.Font("BC Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxPageCount.Location = new System.Drawing.Point(709, 3);
            this.groupBoxPageCount.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.groupBoxPageCount.Name = "groupBoxPageCount";
            this.groupBoxPageCount.Size = new System.Drawing.Size(131, 109);
            this.groupBoxPageCount.TabIndex = 111;
            this.groupBoxPageCount.TabStop = false;
            this.groupBoxPageCount.Text = "Page Count";
            // 
            // flowLayoutPanelTotalPages
            // 
            this.flowLayoutPanelTotalPages.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanelTotalPages.Controls.Add(this.BoxTotalPagesLabel);
            this.flowLayoutPanelTotalPages.Controls.Add(this.textBoxPageCount);
            this.flowLayoutPanelTotalPages.Location = new System.Drawing.Point(7, 29);
            this.flowLayoutPanelTotalPages.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanelTotalPages.MaximumSize = new System.Drawing.Size(155, 71);
            this.flowLayoutPanelTotalPages.MinimumSize = new System.Drawing.Size(115, 71);
            this.flowLayoutPanelTotalPages.Name = "flowLayoutPanelTotalPages";
            this.flowLayoutPanelTotalPages.Padding = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanelTotalPages.Size = new System.Drawing.Size(115, 71);
            this.flowLayoutPanelTotalPages.TabIndex = 113;
            // 
            // BoxTotalPagesLabel
            // 
            this.BoxTotalPagesLabel.AutoSize = true;
            this.BoxTotalPagesLabel.Font = new System.Drawing.Font("BC Sans Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BoxTotalPagesLabel.Location = new System.Drawing.Point(7, 4);
            this.BoxTotalPagesLabel.Name = "BoxTotalPagesLabel";
            this.BoxTotalPagesLabel.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.BoxTotalPagesLabel.Size = new System.Drawing.Size(41, 26);
            this.BoxTotalPagesLabel.TabIndex = 99;
            this.BoxTotalPagesLabel.Text = "Total";
            // 
            // textBoxPageCount
            // 
            this.textBoxPageCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(235)))), ((int)(((byte)(233)))));
            this.textBoxPageCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPageCount.Font = new System.Drawing.Font("BC Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPageCount.Location = new System.Drawing.Point(7, 33);
            this.textBoxPageCount.MinimumSize = new System.Drawing.Size(95, 29);
            this.textBoxPageCount.Name = "textBoxPageCount";
            this.textBoxPageCount.Size = new System.Drawing.Size(95, 29);
            this.textBoxPageCount.TabIndex = 101;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("BC Sans Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 4);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.label2.Size = new System.Drawing.Size(61, 26);
            this.label2.TabIndex = 99;
            this.label2.Text = "Current";
            // 
            // groupBoxBatchID
            // 
            this.groupBoxBatchID.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxBatchID.Controls.Add(this.flowLayoutPanelCurrentBatchID);
            this.groupBoxBatchID.Font = new System.Drawing.Font("BC Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxBatchID.Location = new System.Drawing.Point(840, 3);
            this.groupBoxBatchID.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.groupBoxBatchID.Name = "groupBoxBatchID";
            this.groupBoxBatchID.Size = new System.Drawing.Size(131, 109);
            this.groupBoxBatchID.TabIndex = 112;
            this.groupBoxBatchID.TabStop = false;
            this.groupBoxBatchID.Text = "Batch ID";
            // 
            // flowLayoutPanelCurrentBatchID
            // 
            this.flowLayoutPanelCurrentBatchID.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanelCurrentBatchID.Controls.Add(this.label2);
            this.flowLayoutPanelCurrentBatchID.Controls.Add(this.editBatchID);
            this.flowLayoutPanelCurrentBatchID.Controls.Add(this.textBoxBatchID);
            this.flowLayoutPanelCurrentBatchID.Location = new System.Drawing.Point(7, 29);
            this.flowLayoutPanelCurrentBatchID.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanelCurrentBatchID.MaximumSize = new System.Drawing.Size(155, 79);
            this.flowLayoutPanelCurrentBatchID.MinimumSize = new System.Drawing.Size(115, 71);
            this.flowLayoutPanelCurrentBatchID.Name = "flowLayoutPanelCurrentBatchID";
            this.flowLayoutPanelCurrentBatchID.Padding = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanelCurrentBatchID.Size = new System.Drawing.Size(115, 71);
            this.flowLayoutPanelCurrentBatchID.TabIndex = 102;
            // 
            // editBatchID
            // 
            this.editBatchID.Image = ((System.Drawing.Image)(resources.GetObject("editBatchID.Image")));
            this.editBatchID.Location = new System.Drawing.Point(74, 4);
            this.editBatchID.Name = "editBatchID";
            this.editBatchID.Size = new System.Drawing.Size(26, 26);
            this.editBatchID.TabIndex = 102;
            // 
            // textBoxBatchID
            // 
            this.textBoxBatchID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxBatchID.Font = new System.Drawing.Font("BC Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxBatchID.Location = new System.Drawing.Point(7, 33);
            this.textBoxBatchID.MinimumSize = new System.Drawing.Size(95, 29);
            this.textBoxBatchID.Name = "textBoxBatchID";
            this.textBoxBatchID.Size = new System.Drawing.Size(95, 29);
            this.textBoxBatchID.TabIndex = 101;
            // 
            // btnChangeBox
            // 
            this.btnChangeBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnChangeBox.Font = new System.Drawing.Font("BC Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangeBox.ForeColor = System.Drawing.Color.Black;
            this.btnChangeBox.Location = new System.Drawing.Point(3, 40);
            this.btnChangeBox.Name = "btnChangeBox";
            this.btnChangeBox.Size = new System.Drawing.Size(136, 31);
            this.btnChangeBox.TabIndex = 115;
            this.btnChangeBox.Text = "Change Box";
            this.btnChangeBox.UseVisualStyleBackColor = true;
            this.btnChangeBox.Click += new System.EventHandler(this.btnChangeBox_Click);
            // 
            // flowLayoutPanelBoxMaintinence
            // 
            this.flowLayoutPanelBoxMaintinence.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanelBoxMaintinence.Controls.Add(this.groupBoxAccessionNumber);
            this.flowLayoutPanelBoxMaintinence.Controls.Add(this.groupBoxDates);
            this.flowLayoutPanelBoxMaintinence.Controls.Add(this.groupBoxPageCount);
            this.flowLayoutPanelBoxMaintinence.Controls.Add(this.groupBoxBatchID);
            this.flowLayoutPanelBoxMaintinence.Controls.Add(this.flowLayoutPanel12);
            this.flowLayoutPanelBoxMaintinence.Controls.Add(this.flowLayoutPanel4);
            this.flowLayoutPanelBoxMaintinence.Controls.Add(this.flowLayoutPanel10);
            this.flowLayoutPanelBoxMaintinence.Controls.Add(this.flowLayoutPanel11);
            this.flowLayoutPanelBoxMaintinence.Location = new System.Drawing.Point(192, 11);
            this.flowLayoutPanelBoxMaintinence.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanelBoxMaintinence.Name = "flowLayoutPanelBoxMaintinence";
            this.flowLayoutPanelBoxMaintinence.Size = new System.Drawing.Size(982, 116);
            this.flowLayoutPanelBoxMaintinence.TabIndex = 117;
            // 
            // flowLayoutPanelBoxMaintButtons
            // 
            this.flowLayoutPanelBoxMaintButtons.Controls.Add(this.btnOpenBox);
            this.flowLayoutPanelBoxMaintButtons.Controls.Add(this.btnChangeBox);
            this.flowLayoutPanelBoxMaintButtons.Location = new System.Drawing.Point(16, 43);
            this.flowLayoutPanelBoxMaintButtons.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanelBoxMaintButtons.Name = "flowLayoutPanelBoxMaintButtons";
            this.flowLayoutPanelBoxMaintButtons.Size = new System.Drawing.Size(142, 84);
            this.flowLayoutPanelBoxMaintButtons.TabIndex = 116;
            // 
            // lblBoxMaintenance
            // 
            this.lblBoxMaintenance.AutoSize = true;
            this.lblBoxMaintenance.Font = new System.Drawing.Font("BC Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBoxMaintenance.Location = new System.Drawing.Point(12, 9);
            this.lblBoxMaintenance.Name = "lblBoxMaintenance";
            this.lblBoxMaintenance.Size = new System.Drawing.Size(175, 26);
            this.lblBoxMaintenance.TabIndex = 118;
            this.lblBoxMaintenance.Text = "Box Maintenance";
            // 
            // frmBoxManagement
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(249)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1194, 135);
            this.Controls.Add(this.lblBoxMaintenance);
            this.Controls.Add(this.flowLayoutPanelBoxMaintinence);
            this.Controls.Add(this.CurrentBoxLabel);
            this.Controls.Add(this.flowLayoutPanelBoxMaintButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1194, 135);
            this.Name = "frmBoxManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.flowLayoutPanelOpened.ResumeLayout(false);
            this.flowLayoutPanelOpened.PerformLayout();
            this.flowLayoutPanelSequence.ResumeLayout(false);
            this.flowLayoutPanelSequence.PerformLayout();
            this.flowLayoutPanelSchedule.ResumeLayout(false);
            this.flowLayoutPanelSchedule.PerformLayout();
            this.flowLayoutPanelBoxNumber.ResumeLayout(false);
            this.flowLayoutPanelBoxNumber.PerformLayout();
            this.flowLayoutPanelClosed.ResumeLayout(false);
            this.flowLayoutPanelClosed.PerformLayout();
            this.groupBoxAccessionNumber.ResumeLayout(false);
            this.groupBoxDates.ResumeLayout(false);
            this.groupBoxPageCount.ResumeLayout(false);
            this.flowLayoutPanelTotalPages.ResumeLayout(false);
            this.flowLayoutPanelTotalPages.PerformLayout();
            this.groupBoxBatchID.ResumeLayout(false);
            this.flowLayoutPanelCurrentBatchID.ResumeLayout(false);
            this.flowLayoutPanelCurrentBatchID.PerformLayout();
            this.flowLayoutPanelBoxMaintinence.ResumeLayout(false);
            this.flowLayoutPanelBoxMaintButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label CurrentBoxLabel;
        private FlowLayoutPanel flowLayoutPanelOpened;
        private Label BoxOpenDateLabel;
        private FlowLayoutPanel flowLayoutPanel4;
        private FlowLayoutPanel flowLayoutPanelSequence;
        private Label SequenceLabel;
        private MaskedTextBox maskedTextBoxSequenceNumber;
        private FlowLayoutPanel flowLayoutPanelSchedule;
        private Label ScheduleLabel;
        private FlowLayoutPanel flowLayoutPanelBoxNumber;
        private Label boxNumberLabel;
        private FlowLayoutPanel flowLayoutPanelClosed;
        private Label BoxCloseDateLabel;
        private FlowLayoutPanel flowLayoutPanel10;
        private FlowLayoutPanel flowLayoutPanel11;
        private FlowLayoutPanel flowLayoutPanel12;
        private GroupBox groupBoxAccessionNumber;
        private GroupBox groupBoxDates;
        private GroupBox groupBoxPageCount;
        private GroupBox groupBoxBatchID;
        private FlowLayoutPanel flowLayoutPanelTotalPages;
        private Label label2;
        private FlowLayoutPanel flowLayoutPanelCurrentBatchID;
        private Button btnCloseBox;
        private Button btnOpenBox;
        private MaskedTextBox maskedTextBoxScheduleNumber;
        private MaskedTextBox maskedTextBoxBoxNumber;
        private FlowLayoutPanel flowLayoutPanelBoxMaintinence;
        private TextBox textBoxOpenedDate;
        private Label BoxTotalPagesLabel;
        private TextBox textBoxClosedDate;
        private TextBox textBoxPageCount;
        private TextBox textBoxBatchID;
        private Label editBatchID;
        private Button btnChangeBox;
        private FlowLayoutPanel flowLayoutPanelBoxMaintButtons;
        private Label lblBoxMaintenance;
    }
}
