namespace RegScan
{
    partial class frmBoxOpen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBoxOpen));
            this.groupBoxAccessionNumber = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanelBoxNumber = new System.Windows.Forms.FlowLayoutPanel();
            this.boxNumberLabel = new System.Windows.Forms.Label();
            this.maskedTextBoxBoxNumber = new System.Windows.Forms.MaskedTextBox();
            this.flowLayoutPanelSequence = new System.Windows.Forms.FlowLayoutPanel();
            this.SequenceLabel = new System.Windows.Forms.Label();
            this.comboBoxSeqSch = new System.Windows.Forms.ComboBox();
            this.groupBoxDates = new System.Windows.Forms.GroupBox();
            this.textBoxOpenedDate = new System.Windows.Forms.TextBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBoxAccessionNumber.SuspendLayout();
            this.flowLayoutPanelBoxNumber.SuspendLayout();
            this.flowLayoutPanelSequence.SuspendLayout();
            this.groupBoxDates.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxAccessionNumber
            // 
            this.groupBoxAccessionNumber.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxAccessionNumber.Controls.Add(this.flowLayoutPanelBoxNumber);
            this.groupBoxAccessionNumber.Controls.Add(this.flowLayoutPanelSequence);
            resources.ApplyResources(this.groupBoxAccessionNumber, "groupBoxAccessionNumber");
            this.groupBoxAccessionNumber.Name = "groupBoxAccessionNumber";
            this.groupBoxAccessionNumber.TabStop = false;
            // 
            // flowLayoutPanelBoxNumber
            // 
            this.flowLayoutPanelBoxNumber.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanelBoxNumber.Controls.Add(this.boxNumberLabel);
            this.flowLayoutPanelBoxNumber.Controls.Add(this.maskedTextBoxBoxNumber);
            resources.ApplyResources(this.flowLayoutPanelBoxNumber, "flowLayoutPanelBoxNumber");
            this.flowLayoutPanelBoxNumber.Name = "flowLayoutPanelBoxNumber";
            // 
            // boxNumberLabel
            // 
            resources.ApplyResources(this.boxNumberLabel, "boxNumberLabel");
            this.boxNumberLabel.Name = "boxNumberLabel";
            // 
            // maskedTextBoxBoxNumber
            // 
            this.maskedTextBoxBoxNumber.BackColor = System.Drawing.Color.White;
            this.maskedTextBoxBoxNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.maskedTextBoxBoxNumber, "maskedTextBoxBoxNumber");
            this.maskedTextBoxBoxNumber.Name = "maskedTextBoxBoxNumber";
            this.maskedTextBoxBoxNumber.ResetOnSpace = false;
            // 
            // flowLayoutPanelSequence
            // 
            this.flowLayoutPanelSequence.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanelSequence.Controls.Add(this.SequenceLabel);
            this.flowLayoutPanelSequence.Controls.Add(this.comboBoxSeqSch);
            resources.ApplyResources(this.flowLayoutPanelSequence, "flowLayoutPanelSequence");
            this.flowLayoutPanelSequence.Name = "flowLayoutPanelSequence";
            // 
            // SequenceLabel
            // 
            resources.ApplyResources(this.SequenceLabel, "SequenceLabel");
            this.SequenceLabel.Name = "SequenceLabel";
            // 
            // comboBoxSeqSch
            // 
            resources.ApplyResources(this.comboBoxSeqSch, "comboBoxSeqSch");
            this.comboBoxSeqSch.FormattingEnabled = true;
            this.comboBoxSeqSch.Name = "comboBoxSeqSch";
            // 
            // groupBoxDates
            // 
            this.groupBoxDates.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxDates.Controls.Add(this.textBoxOpenedDate);
            resources.ApplyResources(this.groupBoxDates, "groupBoxDates");
            this.groupBoxDates.Name = "groupBoxDates";
            this.groupBoxDates.TabStop = false;
            // 
            // textBoxOpenedDate
            // 
            this.textBoxOpenedDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(235)))), ((int)(((byte)(233)))));
            this.textBoxOpenedDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.textBoxOpenedDate, "textBoxOpenedDate");
            this.textBoxOpenedDate.Name = "textBoxOpenedDate";
            this.textBoxOpenedDate.ReadOnly = true;
            // 
            // btnCreate
            // 
            this.btnCreate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            resources.ApplyResources(this.btnCreate, "btnCreate");
            this.btnCreate.ForeColor = System.Drawing.Color.White;
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.UseVisualStyleBackColor = false;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmBoxOpen
            // 
            this.AcceptButton = this.btnCreate;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancel;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.groupBoxDates);
            this.Controls.Add(this.groupBoxAccessionNumber);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBoxOpen";
            this.groupBoxAccessionNumber.ResumeLayout(false);
            this.flowLayoutPanelBoxNumber.ResumeLayout(false);
            this.flowLayoutPanelBoxNumber.PerformLayout();
            this.flowLayoutPanelSequence.ResumeLayout(false);
            this.flowLayoutPanelSequence.PerformLayout();
            this.groupBoxDates.ResumeLayout(false);
            this.groupBoxDates.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBoxAccessionNumber;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelBoxNumber;
        private System.Windows.Forms.Label boxNumberLabel;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxBoxNumber;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSequence;
        private System.Windows.Forms.Label SequenceLabel;
        private System.Windows.Forms.GroupBox groupBoxDates;
        private System.Windows.Forms.TextBox textBoxOpenedDate;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox comboBoxSeqSch;
    }
}
