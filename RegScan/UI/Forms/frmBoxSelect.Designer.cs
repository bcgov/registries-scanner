namespace RegScan
{
    partial class frmBoxSelect
    {
        private System.ComponentModel.IContainer components;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridBoxes = new System.Windows.Forms.DataGridView();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.comboStatus = new System.Windows.Forms.ComboBox();
            this.comboOpenedDate = new System.Windows.Forms.ComboBox();
            this.comboSequence = new System.Windows.Forms.ComboBox();
            this.comboSchedule = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblOpened = new System.Windows.Forms.Label();
            this.lblSeq = new System.Windows.Forms.Label();
            this.lblSch = new System.Windows.Forms.Label();
            this.boxObjBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridBoxes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.boxObjBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridBoxes
            // 
            this.dataGridBoxes.AllowUserToAddRows = false;
            this.dataGridBoxes.AllowUserToDeleteRows = false;
            this.dataGridBoxes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridBoxes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("BC Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridBoxes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridBoxes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridBoxes.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridBoxes.Location = new System.Drawing.Point(12, 61);
            this.dataGridBoxes.MultiSelect = false;
            this.dataGridBoxes.Name = "dataGridBoxes";
            this.dataGridBoxes.ReadOnly = true;
            this.dataGridBoxes.RowHeadersVisible = false;
            this.dataGridBoxes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridBoxes.ShowCellToolTips = false;
            this.dataGridBoxes.ShowEditingIcon = false;
            this.dataGridBoxes.Size = new System.Drawing.Size(730, 372);
            this.dataGridBoxes.TabIndex = 0;
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Font = new System.Drawing.Font("BC Sans Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(637, 21);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(105, 34);
            this.btnReset.TabIndex = 1;
            this.btnReset.Text = "Reset Filters";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("BC Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(526, 439);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(105, 34);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Font = new System.Drawing.Font("BC Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Location = new System.Drawing.Point(637, 439);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(105, 34);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // comboStatus
            // 
            this.comboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboStatus.FormattingEnabled = true;
            this.comboStatus.Location = new System.Drawing.Point(12, 34);
            this.comboStatus.Name = "comboStatus";
            this.comboStatus.Size = new System.Drawing.Size(130, 21);
            this.comboStatus.TabIndex = 4;
            // 
            // comboOpenedDate
            // 
            this.comboOpenedDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboOpenedDate.FormattingEnabled = true;
            this.comboOpenedDate.Location = new System.Drawing.Point(170, 34);
            this.comboOpenedDate.Name = "comboOpenedDate";
            this.comboOpenedDate.Size = new System.Drawing.Size(130, 21);
            this.comboOpenedDate.TabIndex = 5;
            // 
            // comboSequence
            // 
            this.comboSequence.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSequence.FormattingEnabled = true;
            this.comboSequence.Location = new System.Drawing.Point(328, 34);
            this.comboSequence.Name = "comboSequence";
            this.comboSequence.Size = new System.Drawing.Size(130, 21);
            this.comboSequence.TabIndex = 6;
            // 
            // comboSchedule
            // 
            this.comboSchedule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSchedule.FormattingEnabled = true;
            this.comboSchedule.Location = new System.Drawing.Point(486, 34);
            this.comboSchedule.Name = "comboSchedule";
            this.comboSchedule.Size = new System.Drawing.Size(130, 21);
            this.comboSchedule.TabIndex = 7;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("BC Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(12, 9);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(62, 22);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "Status:";
            // 
            // lblOpened
            // 
            this.lblOpened.AutoSize = true;
            this.lblOpened.Font = new System.Drawing.Font("BC Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOpened.Location = new System.Drawing.Point(166, 9);
            this.lblOpened.Name = "lblOpened";
            this.lblOpened.Size = new System.Drawing.Size(75, 22);
            this.lblOpened.TabIndex = 9;
            this.lblOpened.Text = "Opened:";
            // 
            // lblSeq
            // 
            this.lblSeq.AutoSize = true;
            this.lblSeq.Font = new System.Drawing.Font("BC Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeq.Location = new System.Drawing.Point(324, 9);
            this.lblSeq.Name = "lblSeq";
            this.lblSeq.Size = new System.Drawing.Size(88, 22);
            this.lblSeq.TabIndex = 10;
            this.lblSeq.Text = "Sequence:";
            // 
            // lblSch
            // 
            this.lblSch.AutoSize = true;
            this.lblSch.Font = new System.Drawing.Font("BC Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSch.Location = new System.Drawing.Point(482, 9);
            this.lblSch.Name = "lblSch";
            this.lblSch.Size = new System.Drawing.Size(83, 22);
            this.lblSch.TabIndex = 11;
            this.lblSch.Text = "Schedule:";
            // 
            // boxObjBindingSource
            // 
            this.boxObjBindingSource.DataSource = typeof(RegScan.BoxObj);
            // 
            // frmBoxSelect
            // 
            this.ClientSize = new System.Drawing.Size(754, 485);
            this.AcceptButton = this.btnOk;
            this.Controls.Add(this.lblSch);
            this.Controls.Add(this.lblSeq);
            this.Controls.Add(this.lblOpened);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.comboSchedule);
            this.Controls.Add(this.comboSequence);
            this.Controls.Add(this.comboOpenedDate);
            this.Controls.Add(this.comboStatus);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.dataGridBoxes);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(770, 350);
            this.Name = "frmBoxSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change Current Box";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridBoxes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.boxObjBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.DataGridView dataGridBoxes;
        private System.Windows.Forms.BindingSource boxObjBindingSource;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ComboBox comboStatus;
        private System.Windows.Forms.ComboBox comboOpenedDate;
        private System.Windows.Forms.ComboBox comboSequence;
        private System.Windows.Forms.ComboBox comboSchedule;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblOpened;
        private System.Windows.Forms.Label lblSeq;
        private System.Windows.Forms.Label lblSch;
    }
}