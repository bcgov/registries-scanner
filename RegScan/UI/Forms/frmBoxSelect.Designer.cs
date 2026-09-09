namespace RegScan
{
    partial class frmBoxSelect
    {
        private System.ComponentModel.IContainer components;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridBoxes = new System.Windows.Forms.DataGridView();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.comboStatus = new System.Windows.Forms.ComboBox();
            this.comboOpenedDate = new System.Windows.Forms.ComboBox();
            this.comboSequence = new System.Windows.Forms.ComboBox();
            this.comboSchedule = new System.Windows.Forms.ComboBox();
            this.boxObjBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBoxStatus = new System.Windows.Forms.GroupBox();
            this.groupBoxOpened = new System.Windows.Forms.GroupBox();
            this.groupBoxSequence = new System.Windows.Forms.GroupBox();
            this.groupBoxSchedule = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRefreshList = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridBoxes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.boxObjBindingSource)).BeginInit();
            this.groupBoxStatus.SuspendLayout();
            this.groupBoxOpened.SuspendLayout();
            this.groupBoxSequence.SuspendLayout();
            this.groupBoxSchedule.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridBoxes
            // 
            this.dataGridBoxes.AllowUserToAddRows = false;
            this.dataGridBoxes.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(235)))), ((int)(((byte)(233)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("BC Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(81)))), ((int)(((byte)(137)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.dataGridBoxes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridBoxes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridBoxes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("BC Sans", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridBoxes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridBoxes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("BC Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(81)))), ((int)(((byte)(137)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridBoxes.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridBoxes.Location = new System.Drawing.Point(12, 80);
            this.dataGridBoxes.MultiSelect = false;
            this.dataGridBoxes.Name = "dataGridBoxes";
            this.dataGridBoxes.ReadOnly = true;
            this.dataGridBoxes.RowHeadersVisible = false;
            this.dataGridBoxes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridBoxes.ShowCellToolTips = false;
            this.dataGridBoxes.ShowEditingIcon = false;
            this.dataGridBoxes.Size = new System.Drawing.Size(771, 361);
            this.dataGridBoxes.TabIndex = 0;
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.BackColor = System.Drawing.Color.White;
            this.btnReset.Font = new System.Drawing.Font("BC Sans Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(677, 23);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(105, 34);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "Reset Filters";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("BC Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(567, 447);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(105, 34);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.btnOk.Font = new System.Drawing.Font("BC Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(678, 447);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(105, 34);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // comboStatus
            // 
            this.comboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboStatus.Font = new System.Drawing.Font("BC Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboStatus.FormattingEnabled = true;
            this.comboStatus.Location = new System.Drawing.Point(6, 22);
            this.comboStatus.Name = "comboStatus";
            this.comboStatus.Size = new System.Drawing.Size(152, 28);
            this.comboStatus.TabIndex = 1;
            // 
            // comboOpenedDate
            // 
            this.comboOpenedDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboOpenedDate.Font = new System.Drawing.Font("BC Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboOpenedDate.FormattingEnabled = true;
            this.comboOpenedDate.Location = new System.Drawing.Point(6, 22);
            this.comboOpenedDate.Name = "comboOpenedDate";
            this.comboOpenedDate.Size = new System.Drawing.Size(152, 28);
            this.comboOpenedDate.TabIndex = 2;
            // 
            // comboSequence
            // 
            this.comboSequence.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSequence.Font = new System.Drawing.Font("BC Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboSequence.FormattingEnabled = true;
            this.comboSequence.Location = new System.Drawing.Point(6, 22);
            this.comboSequence.Name = "comboSequence";
            this.comboSequence.Size = new System.Drawing.Size(152, 28);
            this.comboSequence.TabIndex = 3;
            // 
            // comboSchedule
            // 
            this.comboSchedule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSchedule.Font = new System.Drawing.Font("BC Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboSchedule.FormattingEnabled = true;
            this.comboSchedule.Location = new System.Drawing.Point(6, 22);
            this.comboSchedule.Name = "comboSchedule";
            this.comboSchedule.Size = new System.Drawing.Size(152, 28);
            this.comboSchedule.TabIndex = 4;
            // 
            // boxObjBindingSource
            // 
            this.boxObjBindingSource.DataSource = typeof(RegScan.BoxObj);
            // 
            // groupBoxStatus
            // 
            this.groupBoxStatus.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxStatus.Controls.Add(this.comboStatus);
            this.groupBoxStatus.Font = new System.Drawing.Font("BC Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxStatus.Location = new System.Drawing.Point(0, 0);
            this.groupBoxStatus.Margin = new System.Windows.Forms.Padding(0);
            this.groupBoxStatus.Name = "groupBoxStatus";
            this.groupBoxStatus.Size = new System.Drawing.Size(164, 56);
            this.groupBoxStatus.TabIndex = 0;
            this.groupBoxStatus.TabStop = false;
            this.groupBoxStatus.Text = "Status";
            // 
            // groupBoxOpened
            // 
            this.groupBoxOpened.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxOpened.Controls.Add(this.comboOpenedDate);
            this.groupBoxOpened.Font = new System.Drawing.Font("BC Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxOpened.Location = new System.Drawing.Point(164, 0);
            this.groupBoxOpened.Margin = new System.Windows.Forms.Padding(0);
            this.groupBoxOpened.Name = "groupBoxOpened";
            this.groupBoxOpened.Size = new System.Drawing.Size(164, 56);
            this.groupBoxOpened.TabIndex = 0;
            this.groupBoxOpened.TabStop = false;
            this.groupBoxOpened.Text = "Opened";
            // 
            // groupBoxSequence
            // 
            this.groupBoxSequence.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxSequence.Controls.Add(this.comboSequence);
            this.groupBoxSequence.Font = new System.Drawing.Font("BC Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxSequence.Location = new System.Drawing.Point(328, 0);
            this.groupBoxSequence.Margin = new System.Windows.Forms.Padding(0);
            this.groupBoxSequence.Name = "groupBoxSequence";
            this.groupBoxSequence.Size = new System.Drawing.Size(164, 56);
            this.groupBoxSequence.TabIndex = 0;
            this.groupBoxSequence.TabStop = false;
            this.groupBoxSequence.Text = "Sequence";
            // 
            // groupBoxSchedule
            // 
            this.groupBoxSchedule.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxSchedule.Controls.Add(this.comboSchedule);
            this.groupBoxSchedule.Font = new System.Drawing.Font("BC Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxSchedule.Location = new System.Drawing.Point(492, 0);
            this.groupBoxSchedule.Margin = new System.Windows.Forms.Padding(0);
            this.groupBoxSchedule.Name = "groupBoxSchedule";
            this.groupBoxSchedule.Size = new System.Drawing.Size(164, 56);
            this.groupBoxSchedule.TabIndex = 0;
            this.groupBoxSchedule.TabStop = false;
            this.groupBoxSchedule.Text = "Schedule";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.groupBoxStatus);
            this.flowLayoutPanel1.Controls.Add(this.groupBoxOpened);
            this.flowLayoutPanel1.Controls.Add(this.groupBoxSequence);
            this.flowLayoutPanel1.Controls.Add(this.groupBoxSchedule);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(660, 62);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnRefreshList
            // 
            this.btnRefreshList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefreshList.Font = new System.Drawing.Font("BC Sans Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefreshList.Location = new System.Drawing.Point(12, 447);
            this.btnRefreshList.Name = "btnRefreshList";
            this.btnRefreshList.Size = new System.Drawing.Size(105, 34);
            this.btnRefreshList.TabIndex = 6;
            this.btnRefreshList.Text = "Refresh List";
            this.btnRefreshList.UseVisualStyleBackColor = true;
            this.btnRefreshList.Click += new System.EventHandler(this.btnRefreshList_Click);
            // 
            // frmBoxSelect
            // 
            this.AcceptButton = this.btnOk;
            this.ClientSize = new System.Drawing.Size(795, 493);
            this.Controls.Add(this.btnRefreshList);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.dataGridBoxes);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(811, 350);
            this.Name = "frmBoxSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change Current Box";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridBoxes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.boxObjBindingSource)).EndInit();
            this.groupBoxStatus.ResumeLayout(false);
            this.groupBoxOpened.ResumeLayout(false);
            this.groupBoxSequence.ResumeLayout(false);
            this.groupBoxSchedule.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private System.Windows.Forms.GroupBox groupBoxStatus;
        private System.Windows.Forms.GroupBox groupBoxOpened;
        private System.Windows.Forms.GroupBox groupBoxSequence;
        private System.Windows.Forms.GroupBox groupBoxSchedule;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnRefreshList;
    }
}