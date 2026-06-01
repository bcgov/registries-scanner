namespace RegScan
{
    partial class frmScannerDocument
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
            this.useDuplexCheckBox = new System.Windows.Forms.CheckBox();
            this.useAdfCheckBox = new System.Windows.Forms.CheckBox();
            this.txtIndexer = new System.Windows.Forms.TextBox();
            this.lblIndexer = new System.Windows.Forms.Label();
            this.txtLegalEntityKey = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSeqNumber = new System.Windows.Forms.TextBox();
            this.txtPagesInDocument = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDocumentType = new System.Windows.Forms.TextBox();
            this.lblDocumentType = new System.Windows.Forms.Label();
            this.txtDocumentClass = new System.Windows.Forms.TextBox();
            this.lblDocumentClass = new System.Windows.Forms.Label();
            this.txtBarCode = new System.Windows.Forms.TextBox();
            this.showProgressIndicatorUICheckBox = new System.Windows.Forms.CheckBox();
            this.useUICheckBox = new System.Windows.Forms.CheckBox();
            this.ckBoxLowResolution = new System.Windows.Forms.CheckBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnCancelScan = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblDocRecord = new System.Windows.Forms.Label();
            this.txtFilingDate = new System.Windows.Forms.TextBox();
            this.lblFilingDate = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lblDocNotes = new System.Windows.Forms.Label();
            this.lblBoxNumber = new System.Windows.Forms.Label();
            this.lblSchNumber = new System.Windows.Forms.Label();
            this.lblSeqNumber = new System.Windows.Forms.Label();
            this.txtBoxNumber = new System.Windows.Forms.TextBox();
            this.txtSchNumber = new System.Windows.Forms.TextBox();
            this.lblAccessionNumber = new System.Windows.Forms.Label();
            this.lblRecordClass = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.rotateImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewDocumentAsPDFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusLblDisplayImage = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBtnPrevImage = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLblCurImage = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLblOf = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLblTotalImage = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLblNextImage = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBtnDeleteImage = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnScanPage = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.imageBox = new RegScan.ImageBox();
            this.txtDocumentNotes = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // useDuplexCheckBox
            // 
            this.useDuplexCheckBox.AutoSize = true;
            this.useDuplexCheckBox.Location = new System.Drawing.Point(144, 42);
            this.useDuplexCheckBox.Name = "useDuplexCheckBox";
            this.useDuplexCheckBox.Size = new System.Drawing.Size(105, 17);
            this.useDuplexCheckBox.TabIndex = 34;
            this.useDuplexCheckBox.Text = "Scan Both Sides";
            this.useDuplexCheckBox.UseVisualStyleBackColor = true;
            // 
            // useAdfCheckBox
            // 
            this.useAdfCheckBox.AutoSize = true;
            this.useAdfCheckBox.Location = new System.Drawing.Point(144, 6);
            this.useAdfCheckBox.Name = "useAdfCheckBox";
            this.useAdfCheckBox.Size = new System.Drawing.Size(111, 30);
            this.useAdfCheckBox.TabIndex = 29;
            this.useAdfCheckBox.Text = "Use Automatic\r\nDocument Feeder";
            this.useAdfCheckBox.UseVisualStyleBackColor = true;
            this.useAdfCheckBox.CheckedChanged += new System.EventHandler(this.useAdfCheckBox_CheckedChanged);
            // 
            // txtIndexer
            // 
            this.txtIndexer.BackColor = System.Drawing.SystemColors.Info;
            this.txtIndexer.Location = new System.Drawing.Point(182, 144);
            this.txtIndexer.Name = "txtIndexer";
            this.txtIndexer.ReadOnly = true;
            this.txtIndexer.Size = new System.Drawing.Size(141, 20);
            this.txtIndexer.TabIndex = 86;
            // 
            // lblIndexer
            // 
            this.lblIndexer.AutoSize = true;
            this.lblIndexer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIndexer.Location = new System.Drawing.Point(182, 125);
            this.lblIndexer.Name = "lblIndexer";
            this.lblIndexer.Size = new System.Drawing.Size(51, 16);
            this.lblIndexer.TabIndex = 85;
            this.lblIndexer.Text = "Indexer";
            // 
            // txtLegalEntityKey
            // 
            this.txtLegalEntityKey.BackColor = System.Drawing.SystemColors.Info;
            this.txtLegalEntityKey.Location = new System.Drawing.Point(21, 187);
            this.txtLegalEntityKey.Name = "txtLegalEntityKey";
            this.txtLegalEntityKey.ReadOnly = true;
            this.txtLegalEntityKey.Size = new System.Drawing.Size(141, 20);
            this.txtLegalEntityKey.TabIndex = 84;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(21, 168);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 16);
            this.label10.TabIndex = 83;
            this.label10.Text = "Legal Entity";
            // 
            // txtSeqNumber
            // 
            this.txtSeqNumber.BackColor = System.Drawing.SystemColors.Info;
            this.txtSeqNumber.Location = new System.Drawing.Point(21, 350);
            this.txtSeqNumber.Name = "txtSeqNumber";
            this.txtSeqNumber.ReadOnly = true;
            this.txtSeqNumber.Size = new System.Drawing.Size(141, 20);
            this.txtSeqNumber.TabIndex = 78;
            this.txtSeqNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPagesInDocument
            // 
            this.txtPagesInDocument.BackColor = System.Drawing.SystemColors.Info;
            this.txtPagesInDocument.Location = new System.Drawing.Point(182, 187);
            this.txtPagesInDocument.Name = "txtPagesInDocument";
            this.txtPagesInDocument.ReadOnly = true;
            this.txtPagesInDocument.Size = new System.Drawing.Size(141, 20);
            this.txtPagesInDocument.TabIndex = 74;
            this.txtPagesInDocument.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(182, 168);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(127, 16);
            this.label6.TabIndex = 73;
            this.label6.Text = "Pages In Document:";
            // 
            // txtDocumentType
            // 
            this.txtDocumentType.BackColor = System.Drawing.SystemColors.Info;
            this.txtDocumentType.Location = new System.Drawing.Point(182, 267);
            this.txtDocumentType.Name = "txtDocumentType";
            this.txtDocumentType.ReadOnly = true;
            this.txtDocumentType.Size = new System.Drawing.Size(141, 20);
            this.txtDocumentType.TabIndex = 70;
            // 
            // lblDocumentType
            // 
            this.lblDocumentType.AutoSize = true;
            this.lblDocumentType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocumentType.Location = new System.Drawing.Point(183, 248);
            this.lblDocumentType.Name = "lblDocumentType";
            this.lblDocumentType.Size = new System.Drawing.Size(39, 16);
            this.lblDocumentType.TabIndex = 69;
            this.lblDocumentType.Text = "Type";
            // 
            // txtDocumentClass
            // 
            this.txtDocumentClass.BackColor = System.Drawing.SystemColors.Info;
            this.txtDocumentClass.Location = new System.Drawing.Point(21, 267);
            this.txtDocumentClass.Name = "txtDocumentClass";
            this.txtDocumentClass.ReadOnly = true;
            this.txtDocumentClass.Size = new System.Drawing.Size(141, 20);
            this.txtDocumentClass.TabIndex = 65;
            // 
            // lblDocumentClass
            // 
            this.lblDocumentClass.AutoSize = true;
            this.lblDocumentClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocumentClass.Location = new System.Drawing.Point(21, 248);
            this.lblDocumentClass.Name = "lblDocumentClass";
            this.lblDocumentClass.Size = new System.Drawing.Size(41, 16);
            this.lblDocumentClass.TabIndex = 68;
            this.lblDocumentClass.Text = "Class";
            // 
            // txtBarCode
            // 
            this.txtBarCode.BackColor = System.Drawing.SystemColors.Info;
            this.txtBarCode.Location = new System.Drawing.Point(21, 144);
            this.txtBarCode.Name = "txtBarCode";
            this.txtBarCode.ReadOnly = true;
            this.txtBarCode.Size = new System.Drawing.Size(141, 20);
            this.txtBarCode.TabIndex = 67;
            this.txtBarCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // showProgressIndicatorUICheckBox
            // 
            this.showProgressIndicatorUICheckBox.AutoSize = true;
            this.showProgressIndicatorUICheckBox.Checked = true;
            this.showProgressIndicatorUICheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showProgressIndicatorUICheckBox.Location = new System.Drawing.Point(291, 13);
            this.showProgressIndicatorUICheckBox.Name = "showProgressIndicatorUICheckBox";
            this.showProgressIndicatorUICheckBox.Size = new System.Drawing.Size(97, 17);
            this.showProgressIndicatorUICheckBox.TabIndex = 33;
            this.showProgressIndicatorUICheckBox.Text = "Show Progress";
            this.showProgressIndicatorUICheckBox.UseVisualStyleBackColor = true;
            // 
            // useUICheckBox
            // 
            this.useUICheckBox.AutoSize = true;
            this.useUICheckBox.Checked = true;
            this.useUICheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.useUICheckBox.Location = new System.Drawing.Point(3, 13);
            this.useUICheckBox.Name = "useUICheckBox";
            this.useUICheckBox.Size = new System.Drawing.Size(105, 17);
            this.useUICheckBox.TabIndex = 30;
            this.useUICheckBox.Text = "Show Advanced";
            this.useUICheckBox.UseVisualStyleBackColor = true;
            // 
            // ckBoxLowResolution
            // 
            this.ckBoxLowResolution.AutoSize = true;
            this.ckBoxLowResolution.Checked = true;
            this.ckBoxLowResolution.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckBoxLowResolution.Location = new System.Drawing.Point(3, 42);
            this.ckBoxLowResolution.Name = "ckBoxLowResolution";
            this.ckBoxLowResolution.Size = new System.Drawing.Size(99, 17);
            this.ckBoxLowResolution.TabIndex = 31;
            this.ckBoxLowResolution.Text = "Low Resolution";
            this.ckBoxLowResolution.UseVisualStyleBackColor = true;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(3, 6);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(464, 23);
            this.progressBar.TabIndex = 59;
            this.progressBar.Visible = false;
            // 
            // btnCancelScan
            // 
            this.btnCancelScan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelScan.Location = new System.Drawing.Point(186, 521);
            this.btnCancelScan.Name = "btnCancelScan";
            this.btnCancelScan.Size = new System.Drawing.Size(136, 37);
            this.btnCancelScan.TabIndex = 4;
            this.btnCancelScan.Text = "Reject Scan";
            this.btnCancelScan.UseVisualStyleBackColor = true;
            this.btnCancelScan.Click += new System.EventHandler(this.btnCancelScan_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Green;
            this.btnSave.Location = new System.Drawing.Point(348, 521);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(136, 37);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save Scan";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtDocumentNotes);
            this.splitContainer1.Panel1.Controls.Add(this.lblDocRecord);
            this.splitContainer1.Panel1.Controls.Add(this.txtFilingDate);
            this.splitContainer1.Panel1.Controls.Add(this.lblFilingDate);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.lblDocNotes);
            this.splitContainer1.Panel1.Controls.Add(this.lblBoxNumber);
            this.splitContainer1.Panel1.Controls.Add(this.btnCancelScan);
            this.splitContainer1.Panel1.Controls.Add(this.lblSchNumber);
            this.splitContainer1.Panel1.Controls.Add(this.lblSeqNumber);
            this.splitContainer1.Panel1.Controls.Add(this.txtBoxNumber);
            this.splitContainer1.Panel1.Controls.Add(this.txtSchNumber);
            this.splitContainer1.Panel1.Controls.Add(this.btnSave);
            this.splitContainer1.Panel1.Controls.Add(this.lblAccessionNumber);
            this.splitContainer1.Panel1.Controls.Add(this.lblRecordClass);
            this.splitContainer1.Panel1.Controls.Add(this.statusStrip1);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Controls.Add(this.txtIndexer);
            this.splitContainer1.Panel1.Controls.Add(this.lblIndexer);
            this.splitContainer1.Panel1.Controls.Add(this.txtLegalEntityKey);
            this.splitContainer1.Panel1.Controls.Add(this.label10);
            this.splitContainer1.Panel1.Controls.Add(this.txtSeqNumber);
            this.splitContainer1.Panel1.Controls.Add(this.txtPagesInDocument);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.txtDocumentType);
            this.splitContainer1.Panel1.Controls.Add(this.lblDocumentType);
            this.splitContainer1.Panel1.Controls.Add(this.txtDocumentClass);
            this.splitContainer1.Panel1.Controls.Add(this.lblDocumentClass);
            this.splitContainer1.Panel1.Controls.Add(this.txtBarCode);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.imageBox);
            this.splitContainer1.Size = new System.Drawing.Size(1034, 602);
            this.splitContainer1.SplitterDistance = 514;
            this.splitContainer1.TabIndex = 67;
            // 
            // lblDocRecord
            // 
            this.lblDocRecord.AutoSize = true;
            this.lblDocRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocRecord.Location = new System.Drawing.Point(13, 91);
            this.lblDocRecord.Name = "lblDocRecord";
            this.lblDocRecord.Size = new System.Drawing.Size(137, 17);
            this.lblDocRecord.TabIndex = 103;
            this.lblDocRecord.Text = "Document Record";
            // 
            // txtFilingDate
            // 
            this.txtFilingDate.BackColor = System.Drawing.SystemColors.Info;
            this.txtFilingDate.Location = new System.Drawing.Point(343, 144);
            this.txtFilingDate.Name = "txtFilingDate";
            this.txtFilingDate.ReadOnly = true;
            this.txtFilingDate.Size = new System.Drawing.Size(141, 20);
            this.txtFilingDate.TabIndex = 102;
            // 
            // lblFilingDate
            // 
            this.lblFilingDate.AutoSize = true;
            this.lblFilingDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilingDate.Location = new System.Drawing.Point(343, 125);
            this.lblFilingDate.Name = "lblFilingDate";
            this.lblFilingDate.Size = new System.Drawing.Size(71, 16);
            this.lblFilingDate.TabIndex = 101;
            this.lblFilingDate.Text = "Filing Date";
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(404, 481);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 23);
            this.button1.TabIndex = 67;
            this.button1.Text = "Add Note";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // lblDocNotes
            // 
            this.lblDocNotes.AutoSize = true;
            this.lblDocNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocNotes.Location = new System.Drawing.Point(9, 385);
            this.lblDocNotes.Name = "lblDocNotes";
            this.lblDocNotes.Size = new System.Drawing.Size(132, 17);
            this.lblDocNotes.TabIndex = 100;
            this.lblDocNotes.Text = "Document Notes:";
            // 
            // lblBoxNumber
            // 
            this.lblBoxNumber.AutoSize = true;
            this.lblBoxNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBoxNumber.Location = new System.Drawing.Point(343, 331);
            this.lblBoxNumber.Name = "lblBoxNumber";
            this.lblBoxNumber.Size = new System.Drawing.Size(30, 16);
            this.lblBoxNumber.TabIndex = 99;
            this.lblBoxNumber.Text = "Box";
            // 
            // lblSchNumber
            // 
            this.lblSchNumber.AutoSize = true;
            this.lblSchNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSchNumber.Location = new System.Drawing.Point(183, 331);
            this.lblSchNumber.Name = "lblSchNumber";
            this.lblSchNumber.Size = new System.Drawing.Size(64, 16);
            this.lblSchNumber.TabIndex = 98;
            this.lblSchNumber.Text = "Schedule";
            // 
            // lblSeqNumber
            // 
            this.lblSeqNumber.AutoSize = true;
            this.lblSeqNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeqNumber.Location = new System.Drawing.Point(21, 331);
            this.lblSeqNumber.Name = "lblSeqNumber";
            this.lblSeqNumber.Size = new System.Drawing.Size(69, 16);
            this.lblSeqNumber.TabIndex = 97;
            this.lblSeqNumber.Text = "Sequence";
            // 
            // txtBoxNumber
            // 
            this.txtBoxNumber.BackColor = System.Drawing.SystemColors.Info;
            this.txtBoxNumber.Location = new System.Drawing.Point(343, 350);
            this.txtBoxNumber.Name = "txtBoxNumber";
            this.txtBoxNumber.ReadOnly = true;
            this.txtBoxNumber.Size = new System.Drawing.Size(141, 20);
            this.txtBoxNumber.TabIndex = 96;
            this.txtBoxNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtSchNumber
            // 
            this.txtSchNumber.BackColor = System.Drawing.SystemColors.Info;
            this.txtSchNumber.Location = new System.Drawing.Point(182, 350);
            this.txtSchNumber.Name = "txtSchNumber";
            this.txtSchNumber.ReadOnly = true;
            this.txtSchNumber.Size = new System.Drawing.Size(141, 20);
            this.txtSchNumber.TabIndex = 95;
            this.txtSchNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblAccessionNumber
            // 
            this.lblAccessionNumber.AutoSize = true;
            this.lblAccessionNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccessionNumber.Location = new System.Drawing.Point(9, 304);
            this.lblAccessionNumber.Name = "lblAccessionNumber";
            this.lblAccessionNumber.Size = new System.Drawing.Size(147, 17);
            this.lblAccessionNumber.TabIndex = 94;
            this.lblAccessionNumber.Text = "Accession Number:";
            // 
            // lblRecordClass
            // 
            this.lblRecordClass.AutoSize = true;
            this.lblRecordClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordClass.Location = new System.Drawing.Point(9, 219);
            this.lblRecordClass.Name = "lblRecordClass";
            this.lblRecordClass.Size = new System.Drawing.Size(161, 17);
            this.lblRecordClass.TabIndex = 93;
            this.lblRecordClass.Text = "Record Classification";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButton1,
            this.statusLblDisplayImage,
            this.statusBtnPrevImage,
            this.statusLblCurImage,
            this.statusLblOf,
            this.statusLblTotalImage,
            this.statusLblNextImage,
            this.statusBtnDeleteImage});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 576);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(514, 26);
            this.statusStrip1.TabIndex = 92;
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rotateImageToolStripMenuItem,
            this.viewDocumentAsPDFToolStripMenuItem});
            this.toolStripSplitButton1.Font = new System.Drawing.Font("BC Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Margin = new System.Windows.Forms.Padding(3, 2, 10, 0);
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.toolStripSplitButton1.Size = new System.Drawing.Size(112, 21);
            this.toolStripSplitButton1.Text = "Image Options";
            // 
            // rotateImageToolStripMenuItem
            // 
            this.rotateImageToolStripMenuItem.Name = "rotateImageToolStripMenuItem";
            this.rotateImageToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.rotateImageToolStripMenuItem.Text = "Rotate Image";
            // 
            // viewDocumentAsPDFToolStripMenuItem
            // 
            this.viewDocumentAsPDFToolStripMenuItem.Name = "viewDocumentAsPDFToolStripMenuItem";
            this.viewDocumentAsPDFToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.viewDocumentAsPDFToolStripMenuItem.Text = "View Document as PDF";
            // 
            // statusLblDisplayImage
            // 
            this.statusLblDisplayImage.Font = new System.Drawing.Font("BC Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLblDisplayImage.Margin = new System.Windows.Forms.Padding(10, 3, 3, 2);
            this.statusLblDisplayImage.Name = "statusLblDisplayImage";
            this.statusLblDisplayImage.Size = new System.Drawing.Size(76, 17);
            this.statusLblDisplayImage.Text = "Displaying:";
            // 
            // statusBtnPrevImage
            // 
            this.statusBtnPrevImage.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusBtnPrevImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusBtnPrevImage.Font = new System.Drawing.Font("BC Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusBtnPrevImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.statusBtnPrevImage.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
            this.statusBtnPrevImage.Name = "statusBtnPrevImage";
            this.statusBtnPrevImage.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.statusBtnPrevImage.Size = new System.Drawing.Size(25, 21);
            this.statusBtnPrevImage.Text = "<";
            this.statusBtnPrevImage.Click += new System.EventHandler(this.statusBtnPrevImage_Click);
            // 
            // statusLblCurImage
            // 
            this.statusLblCurImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusLblCurImage.Font = new System.Drawing.Font("BC Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLblCurImage.Name = "statusLblCurImage";
            this.statusLblCurImage.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.statusLblCurImage.Size = new System.Drawing.Size(21, 17);
            this.statusLblCurImage.Text = "0";
            // 
            // statusLblOf
            // 
            this.statusLblOf.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusLblOf.Font = new System.Drawing.Font("BC Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLblOf.Name = "statusLblOf";
            this.statusLblOf.Size = new System.Drawing.Size(19, 17);
            this.statusLblOf.Text = "of";
            // 
            // statusLblTotalImage
            // 
            this.statusLblTotalImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusLblTotalImage.Font = new System.Drawing.Font("BC Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLblTotalImage.Name = "statusLblTotalImage";
            this.statusLblTotalImage.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.statusLblTotalImage.Size = new System.Drawing.Size(21, 17);
            this.statusLblTotalImage.Text = "0";
            // 
            // statusLblNextImage
            // 
            this.statusLblNextImage.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusLblNextImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusLblNextImage.Font = new System.Drawing.Font("BC Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLblNextImage.Margin = new System.Windows.Forms.Padding(0, 3, 10, 2);
            this.statusLblNextImage.Name = "statusLblNextImage";
            this.statusLblNextImage.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.statusLblNextImage.Size = new System.Drawing.Size(25, 21);
            this.statusLblNextImage.Text = ">";
            this.statusLblNextImage.Click += new System.EventHandler(this.statusLblNextImage_Click);
            // 
            // statusBtnDeleteImage
            // 
            this.statusBtnDeleteImage.ActiveLinkColor = System.Drawing.Color.Black;
            this.statusBtnDeleteImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(62)))), ((int)(((byte)(57)))));
            this.statusBtnDeleteImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.statusBtnDeleteImage.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusBtnDeleteImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusBtnDeleteImage.Font = new System.Drawing.Font("BC Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusBtnDeleteImage.ForeColor = System.Drawing.Color.White;
            this.statusBtnDeleteImage.Margin = new System.Windows.Forms.Padding(10, 3, 0, 2);
            this.statusBtnDeleteImage.Name = "statusBtnDeleteImage";
            this.statusBtnDeleteImage.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.statusBtnDeleteImage.Size = new System.Drawing.Size(99, 21);
            this.statusBtnDeleteImage.Text = "Delete Image";
            this.statusBtnDeleteImage.Click += new System.EventHandler(this.statusBtnDeleteImage_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.useDuplexCheckBox);
            this.panel1.Controls.Add(this.useAdfCheckBox);
            this.panel1.Controls.Add(this.showProgressIndicatorUICheckBox);
            this.panel1.Controls.Add(this.useUICheckBox);
            this.panel1.Controls.Add(this.ckBoxLowResolution);
            this.panel1.Controls.Add(this.btnScanPage);
            this.panel1.Controls.Add(this.progressBar);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(472, 75);
            this.panel1.TabIndex = 88;
            // 
            // btnScanPage
            // 
            this.btnScanPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnScanPage.ForeColor = System.Drawing.Color.Blue;
            this.btnScanPage.Location = new System.Drawing.Point(291, 42);
            this.btnScanPage.Name = "btnScanPage";
            this.btnScanPage.Size = new System.Drawing.Size(175, 26);
            this.btnScanPage.TabIndex = 1;
            this.btnScanPage.Text = "Scan";
            this.btnScanPage.UseVisualStyleBackColor = true;
            this.btnScanPage.Click += new System.EventHandler(this.btnScanPage_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 16);
            this.label1.TabIndex = 66;
            this.label1.Text = "Barcode";
            // 
            // imageBox
            // 
            this.imageBox.AutoScroll = true;
            this.imageBox.AutoSize = false;
            this.imageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBox.Location = new System.Drawing.Point(0, 0);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(516, 602);
            this.imageBox.TabIndex = 0;
            // 
            // txtDocumentNotes
            // 
            this.txtDocumentNotes.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDocumentNotes.Location = new System.Drawing.Point(21, 405);
            this.txtDocumentNotes.Multiline = true;
            this.txtDocumentNotes.Name = "txtDocumentNotes";
            this.txtDocumentNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDocumentNotes.Size = new System.Drawing.Size(463, 70);
            this.txtDocumentNotes.TabIndex = 104;
            // 
            // frmScannerDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 602);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmScannerDocument";
            this.Text = "Document Scanner";
            this.Activated += new System.EventHandler(this.frmScanDocument_Activated);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox useDuplexCheckBox;
        private System.Windows.Forms.CheckBox useAdfCheckBox;
        private System.Windows.Forms.TextBox txtIndexer;
        private System.Windows.Forms.Label lblIndexer;
        private System.Windows.Forms.TextBox txtLegalEntityKey;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtSeqNumber;
        private System.Windows.Forms.TextBox txtPagesInDocument;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDocumentType;
        private System.Windows.Forms.Label lblDocumentType;
        private System.Windows.Forms.TextBox txtDocumentClass;
        private System.Windows.Forms.Label lblDocumentClass;
        private System.Windows.Forms.TextBox txtBarCode;
        private System.Windows.Forms.CheckBox showProgressIndicatorUICheckBox;
        private ImageBox imageBox;
        private System.Windows.Forms.CheckBox useUICheckBox;
        private System.Windows.Forms.CheckBox ckBoxLowResolution;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnCancelScan;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnScanPage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label lblRecordClass;
        private System.Windows.Forms.Label lblAccessionNumber;
        private System.Windows.Forms.TextBox txtBoxNumber;
        private System.Windows.Forms.TextBox txtSchNumber;
        private System.Windows.Forms.Label lblSchNumber;
        private System.Windows.Forms.Label lblSeqNumber;
        private System.Windows.Forms.Label lblBoxNumber;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblDocNotes;
        private System.Windows.Forms.Label lblDocRecord;
        private System.Windows.Forms.TextBox txtFilingDate;
        private System.Windows.Forms.Label lblFilingDate;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem viewDocumentAsPDFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotateImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel statusLblDisplayImage;
        private System.Windows.Forms.ToolStripStatusLabel statusBtnPrevImage;
        private System.Windows.Forms.ToolStripStatusLabel statusLblCurImage;
        private System.Windows.Forms.ToolStripStatusLabel statusLblOf;
        private System.Windows.Forms.ToolStripStatusLabel statusLblTotalImage;
        private System.Windows.Forms.ToolStripStatusLabel statusLblNextImage;
        private System.Windows.Forms.ToolStripStatusLabel statusBtnDeleteImage;
        private System.Windows.Forms.TextBox txtDocumentNotes;
    }
}