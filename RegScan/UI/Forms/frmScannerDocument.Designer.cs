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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtDocumentNotes = new System.Windows.Forms.TextBox();
            this.panelRejectSaveScan = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancelScan = new System.Windows.Forms.Button();
            this.panelDocClass = new System.Windows.Forms.FlowLayoutPanel();
            this.lblDocumentClass = new System.Windows.Forms.Label();
            this.txtDocumentClass = new System.Windows.Forms.TextBox();
            this.panelDocType = new System.Windows.Forms.FlowLayoutPanel();
            this.lblDocumentType = new System.Windows.Forms.Label();
            this.txtDocumentType = new System.Windows.Forms.TextBox();
            this.panelBarcode = new System.Windows.Forms.FlowLayoutPanel();
            this.lblBarcode = new System.Windows.Forms.Label();
            this.txtBarCode = new System.Windows.Forms.TextBox();
            this.panelLegalEntity = new System.Windows.Forms.FlowLayoutPanel();
            this.lblLegalEntity = new System.Windows.Forms.Label();
            this.txtLegalEntityKey = new System.Windows.Forms.TextBox();
            this.panelPages = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTotalPages = new System.Windows.Forms.Label();
            this.txtPagesInDocument = new System.Windows.Forms.TextBox();
            this.panelFilingDate = new System.Windows.Forms.FlowLayoutPanel();
            this.lblFilingDate = new System.Windows.Forms.Label();
            this.txtFilingDate = new System.Windows.Forms.TextBox();
            this.panelIndexer = new System.Windows.Forms.FlowLayoutPanel();
            this.lblIndexer = new System.Windows.Forms.Label();
            this.txtIndexer = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRotateImg = new System.Windows.Forms.Button();
            this.btnImagePDF = new System.Windows.Forms.Button();
            this.panelImageControl = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblCurImage = new System.Windows.Forms.Label();
            this.btnPrevImage = new System.Windows.Forms.Button();
            this.lblDisplay = new System.Windows.Forms.Label();
            this.lblTotalImage = new System.Windows.Forms.Label();
            this.btnNextImage = new System.Windows.Forms.Button();
            this.lblDisplayImgOf = new System.Windows.Forms.Label();
            this.btnDeleteImage = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.imageBox = new RegScan.ImageBox();
            this.btnScanPage = new System.Windows.Forms.Button();
            this.panelScanningOptions = new System.Windows.Forms.FlowLayoutPanel();
            this.testCheckBox = new System.Windows.Forms.CheckBox();
            this.useDuplexCheckBox = new System.Windows.Forms.CheckBox();
            this.useAdfCheckBox = new System.Windows.Forms.CheckBox();
            this.showProgressIndicatorUICheckBox = new System.Windows.Forms.CheckBox();
            this.ckBoxLowResolution = new System.Windows.Forms.CheckBox();
            this.useUICheckBox = new System.Windows.Forms.CheckBox();
            this.groupBoxScanningOptions = new System.Windows.Forms.GroupBox();
            this.groupBoxDocumentRecord = new System.Windows.Forms.GroupBox();
            this.groupBoxRecordClassification = new System.Windows.Forms.GroupBox();
            this.groupBoxNotes = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelRejectSaveScan.SuspendLayout();
            this.panelDocClass.SuspendLayout();
            this.panelDocType.SuspendLayout();
            this.panelBarcode.SuspendLayout();
            this.panelLegalEntity.SuspendLayout();
            this.panelPages.SuspendLayout();
            this.panelFilingDate.SuspendLayout();
            this.panelIndexer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelImageControl.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelScanningOptions.SuspendLayout();
            this.groupBoxScanningOptions.SuspendLayout();
            this.groupBoxDocumentRecord.SuspendLayout();
            this.groupBoxRecordClassification.SuspendLayout();
            this.groupBoxNotes.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panelRejectSaveScan);
            this.splitContainer1.Panel1.Controls.Add(this.groupBoxScanningOptions);
            this.splitContainer1.Panel1.Controls.Add(this.groupBoxDocumentRecord);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.imageBox);
            this.splitContainer1.Size = new System.Drawing.Size(1186, 888);
            this.splitContainer1.SplitterDistance = 520;
            this.splitContainer1.TabIndex = 67;
            // 
            // txtDocumentNotes
            // 
            this.txtDocumentNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDocumentNotes.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDocumentNotes.Font = new System.Drawing.Font("BC Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDocumentNotes.Location = new System.Drawing.Point(8, 28);
            this.txtDocumentNotes.MaximumSize = new System.Drawing.Size(514, 142);
            this.txtDocumentNotes.MinimumSize = new System.Drawing.Size(167, 79);
            this.txtDocumentNotes.Multiline = true;
            this.txtDocumentNotes.Name = "txtDocumentNotes";
            this.txtDocumentNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDocumentNotes.Size = new System.Drawing.Size(488, 104);
            this.txtDocumentNotes.TabIndex = 104;
            // 
            // panelRejectSaveScan
            // 
            this.panelRejectSaveScan.ColumnCount = 2;
            this.panelRejectSaveScan.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panelRejectSaveScan.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panelRejectSaveScan.Controls.Add(this.btnSave, 1, 0);
            this.panelRejectSaveScan.Controls.Add(this.btnCancelScan, 0, 0);
            this.panelRejectSaveScan.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelRejectSaveScan.Location = new System.Drawing.Point(0, 838);
            this.panelRejectSaveScan.Margin = new System.Windows.Forms.Padding(2);
            this.panelRejectSaveScan.Name = "panelRejectSaveScan";
            this.panelRejectSaveScan.RowCount = 1;
            this.panelRejectSaveScan.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelRejectSaveScan.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.panelRejectSaveScan.Size = new System.Drawing.Size(520, 50);
            this.panelRejectSaveScan.TabIndex = 60;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Green;
            this.btnSave.Location = new System.Drawing.Point(322, 5);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(136, 40);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save Scan";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancelScan
            // 
            this.btnCancelScan.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancelScan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(62)))), ((int)(((byte)(57)))));
            this.btnCancelScan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelScan.ForeColor = System.Drawing.Color.White;
            this.btnCancelScan.Location = new System.Drawing.Point(62, 5);
            this.btnCancelScan.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnCancelScan.Name = "btnCancelScan";
            this.btnCancelScan.Size = new System.Drawing.Size(136, 40);
            this.btnCancelScan.TabIndex = 4;
            this.btnCancelScan.Text = "Reject Scan";
            this.btnCancelScan.UseVisualStyleBackColor = false;
            this.btnCancelScan.Click += new System.EventHandler(this.btnCancelScan_Click);
            // 
            // panelDocClass
            // 
            this.panelDocClass.Controls.Add(this.lblDocumentClass);
            this.panelDocClass.Controls.Add(this.txtDocumentClass);
            this.panelDocClass.Location = new System.Drawing.Point(7, 33);
            this.panelDocClass.Margin = new System.Windows.Forms.Padding(4);
            this.panelDocClass.MaximumSize = new System.Drawing.Size(167, 79);
            this.panelDocClass.MinimumSize = new System.Drawing.Size(155, 79);
            this.panelDocClass.Name = "panelDocClass";
            this.panelDocClass.Padding = new System.Windows.Forms.Padding(4);
            this.panelDocClass.Size = new System.Drawing.Size(155, 79);
            this.panelDocClass.TabIndex = 109;
            // 
            // lblDocumentClass
            // 
            this.lblDocumentClass.AutoSize = true;
            this.lblDocumentClass.Font = new System.Drawing.Font("BC Sans Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocumentClass.Location = new System.Drawing.Point(7, 4);
            this.lblDocumentClass.Name = "lblDocumentClass";
            this.lblDocumentClass.Size = new System.Drawing.Size(82, 20);
            this.lblDocumentClass.TabIndex = 68;
            this.lblDocumentClass.Text = "Entity Type";
            // 
            // txtDocumentClass
            // 
            this.txtDocumentClass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(235)))), ((int)(((byte)(233)))));
            this.txtDocumentClass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDocumentClass.Font = new System.Drawing.Font("BC Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDocumentClass.Location = new System.Drawing.Point(7, 27);
            this.txtDocumentClass.MaximumSize = new System.Drawing.Size(310, 29);
            this.txtDocumentClass.MinimumSize = new System.Drawing.Size(141, 29);
            this.txtDocumentClass.Name = "txtDocumentClass";
            this.txtDocumentClass.ReadOnly = true;
            this.txtDocumentClass.Size = new System.Drawing.Size(141, 29);
            this.txtDocumentClass.TabIndex = 65;
            this.txtDocumentClass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panelDocType
            // 
            this.panelDocType.Controls.Add(this.lblDocumentType);
            this.panelDocType.Controls.Add(this.txtDocumentType);
            this.panelDocType.Location = new System.Drawing.Point(168, 33);
            this.panelDocType.Margin = new System.Windows.Forms.Padding(2);
            this.panelDocType.MinimumSize = new System.Drawing.Size(155, 79);
            this.panelDocType.Name = "panelDocType";
            this.panelDocType.Padding = new System.Windows.Forms.Padding(4);
            this.panelDocType.Size = new System.Drawing.Size(324, 79);
            this.panelDocType.TabIndex = 113;
            // 
            // lblDocumentType
            // 
            this.lblDocumentType.AutoSize = true;
            this.lblDocumentType.Font = new System.Drawing.Font("BC Sans Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocumentType.Location = new System.Drawing.Point(7, 4);
            this.lblDocumentType.Name = "lblDocumentType";
            this.lblDocumentType.Size = new System.Drawing.Size(116, 20);
            this.lblDocumentType.TabIndex = 69;
            this.lblDocumentType.Text = "Document Type";
            // 
            // txtDocumentType
            // 
            this.txtDocumentType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(235)))), ((int)(((byte)(233)))));
            this.txtDocumentType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDocumentType.Font = new System.Drawing.Font("BC Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDocumentType.Location = new System.Drawing.Point(7, 27);
            this.txtDocumentType.MaximumSize = new System.Drawing.Size(310, 29);
            this.txtDocumentType.MinimumSize = new System.Drawing.Size(141, 29);
            this.txtDocumentType.Name = "txtDocumentType";
            this.txtDocumentType.ReadOnly = true;
            this.txtDocumentType.Size = new System.Drawing.Size(310, 29);
            this.txtDocumentType.TabIndex = 70;
            this.txtDocumentType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panelBarcode
            // 
            this.panelBarcode.Controls.Add(this.lblBarcode);
            this.panelBarcode.Controls.Add(this.txtBarCode);
            this.panelBarcode.Location = new System.Drawing.Point(7, 33);
            this.panelBarcode.Margin = new System.Windows.Forms.Padding(4);
            this.panelBarcode.MinimumSize = new System.Drawing.Size(155, 79);
            this.panelBarcode.Name = "panelBarcode";
            this.panelBarcode.Padding = new System.Windows.Forms.Padding(4);
            this.panelBarcode.Size = new System.Drawing.Size(155, 79);
            this.panelBarcode.TabIndex = 119;
            // 
            // lblBarcode
            // 
            this.lblBarcode.AutoSize = true;
            this.lblBarcode.Font = new System.Drawing.Font("BC Sans Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBarcode.Location = new System.Drawing.Point(7, 4);
            this.lblBarcode.Name = "lblBarcode";
            this.lblBarcode.Size = new System.Drawing.Size(65, 20);
            this.lblBarcode.TabIndex = 66;
            this.lblBarcode.Text = "Barcode";
            // 
            // txtBarCode
            // 
            this.txtBarCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(235)))), ((int)(((byte)(233)))));
            this.txtBarCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBarCode.Font = new System.Drawing.Font("BC Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBarCode.Location = new System.Drawing.Point(7, 27);
            this.txtBarCode.MaximumSize = new System.Drawing.Size(310, 29);
            this.txtBarCode.MinimumSize = new System.Drawing.Size(141, 29);
            this.txtBarCode.Name = "txtBarCode";
            this.txtBarCode.ReadOnly = true;
            this.txtBarCode.Size = new System.Drawing.Size(141, 29);
            this.txtBarCode.TabIndex = 67;
            this.txtBarCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panelLegalEntity
            // 
            this.panelLegalEntity.Controls.Add(this.lblLegalEntity);
            this.panelLegalEntity.Controls.Add(this.txtLegalEntityKey);
            this.panelLegalEntity.Location = new System.Drawing.Point(171, 33);
            this.panelLegalEntity.Margin = new System.Windows.Forms.Padding(2);
            this.panelLegalEntity.MinimumSize = new System.Drawing.Size(155, 79);
            this.panelLegalEntity.Name = "panelLegalEntity";
            this.panelLegalEntity.Padding = new System.Windows.Forms.Padding(4);
            this.panelLegalEntity.Size = new System.Drawing.Size(324, 79);
            this.panelLegalEntity.TabIndex = 114;
            // 
            // lblLegalEntity
            // 
            this.lblLegalEntity.AutoSize = true;
            this.lblLegalEntity.Font = new System.Drawing.Font("BC Sans Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLegalEntity.Location = new System.Drawing.Point(7, 4);
            this.lblLegalEntity.Name = "lblLegalEntity";
            this.lblLegalEntity.Size = new System.Drawing.Size(66, 20);
            this.lblLegalEntity.TabIndex = 83;
            this.lblLegalEntity.Text = "Entity ID";
            // 
            // txtLegalEntityKey
            // 
            this.txtLegalEntityKey.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(235)))), ((int)(((byte)(233)))));
            this.txtLegalEntityKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLegalEntityKey.Font = new System.Drawing.Font("BC Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLegalEntityKey.Location = new System.Drawing.Point(7, 27);
            this.txtLegalEntityKey.MaximumSize = new System.Drawing.Size(310, 29);
            this.txtLegalEntityKey.MinimumSize = new System.Drawing.Size(141, 29);
            this.txtLegalEntityKey.Name = "txtLegalEntityKey";
            this.txtLegalEntityKey.ReadOnly = true;
            this.txtLegalEntityKey.Size = new System.Drawing.Size(310, 29);
            this.txtLegalEntityKey.TabIndex = 84;
            this.txtLegalEntityKey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panelPages
            // 
            this.panelPages.Controls.Add(this.lblTotalPages);
            this.panelPages.Controls.Add(this.txtPagesInDocument);
            this.panelPages.Location = new System.Drawing.Point(7, 120);
            this.panelPages.Margin = new System.Windows.Forms.Padding(4);
            this.panelPages.MinimumSize = new System.Drawing.Size(155, 79);
            this.panelPages.Name = "panelPages";
            this.panelPages.Padding = new System.Windows.Forms.Padding(4);
            this.panelPages.Size = new System.Drawing.Size(155, 79);
            this.panelPages.TabIndex = 118;
            // 
            // lblTotalPages
            // 
            this.lblTotalPages.AutoSize = true;
            this.lblTotalPages.Font = new System.Drawing.Font("BC Sans Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPages.Location = new System.Drawing.Point(7, 4);
            this.lblTotalPages.Name = "lblTotalPages";
            this.lblTotalPages.Size = new System.Drawing.Size(86, 20);
            this.lblTotalPages.TabIndex = 73;
            this.lblTotalPages.Text = "Total Pages";
            // 
            // txtPagesInDocument
            // 
            this.txtPagesInDocument.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(235)))), ((int)(((byte)(233)))));
            this.txtPagesInDocument.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPagesInDocument.Font = new System.Drawing.Font("BC Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPagesInDocument.Location = new System.Drawing.Point(7, 27);
            this.txtPagesInDocument.MaximumSize = new System.Drawing.Size(310, 29);
            this.txtPagesInDocument.MinimumSize = new System.Drawing.Size(141, 29);
            this.txtPagesInDocument.Name = "txtPagesInDocument";
            this.txtPagesInDocument.ReadOnly = true;
            this.txtPagesInDocument.Size = new System.Drawing.Size(141, 29);
            this.txtPagesInDocument.TabIndex = 74;
            this.txtPagesInDocument.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panelFilingDate
            // 
            this.panelFilingDate.Controls.Add(this.lblFilingDate);
            this.panelFilingDate.Controls.Add(this.txtFilingDate);
            this.panelFilingDate.Location = new System.Drawing.Point(171, 120);
            this.panelFilingDate.Margin = new System.Windows.Forms.Padding(4);
            this.panelFilingDate.MinimumSize = new System.Drawing.Size(155, 79);
            this.panelFilingDate.Name = "panelFilingDate";
            this.panelFilingDate.Padding = new System.Windows.Forms.Padding(4);
            this.panelFilingDate.Size = new System.Drawing.Size(155, 79);
            this.panelFilingDate.TabIndex = 117;
            // 
            // lblFilingDate
            // 
            this.lblFilingDate.AutoSize = true;
            this.lblFilingDate.Font = new System.Drawing.Font("BC Sans Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilingDate.Location = new System.Drawing.Point(7, 4);
            this.lblFilingDate.Name = "lblFilingDate";
            this.lblFilingDate.Size = new System.Drawing.Size(80, 20);
            this.lblFilingDate.TabIndex = 101;
            this.lblFilingDate.Text = "Filing Date";
            // 
            // txtFilingDate
            // 
            this.txtFilingDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(235)))), ((int)(((byte)(233)))));
            this.txtFilingDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilingDate.Font = new System.Drawing.Font("BC Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilingDate.Location = new System.Drawing.Point(7, 27);
            this.txtFilingDate.MaximumSize = new System.Drawing.Size(310, 29);
            this.txtFilingDate.MinimumSize = new System.Drawing.Size(141, 29);
            this.txtFilingDate.Name = "txtFilingDate";
            this.txtFilingDate.ReadOnly = true;
            this.txtFilingDate.Size = new System.Drawing.Size(141, 29);
            this.txtFilingDate.TabIndex = 102;
            this.txtFilingDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panelIndexer
            // 
            this.panelIndexer.Controls.Add(this.lblIndexer);
            this.panelIndexer.Controls.Add(this.txtIndexer);
            this.panelIndexer.Location = new System.Drawing.Point(340, 120);
            this.panelIndexer.Margin = new System.Windows.Forms.Padding(4);
            this.panelIndexer.MinimumSize = new System.Drawing.Size(155, 79);
            this.panelIndexer.Name = "panelIndexer";
            this.panelIndexer.Padding = new System.Windows.Forms.Padding(4);
            this.panelIndexer.Size = new System.Drawing.Size(155, 79);
            this.panelIndexer.TabIndex = 116;
            // 
            // lblIndexer
            // 
            this.lblIndexer.AutoSize = true;
            this.lblIndexer.Font = new System.Drawing.Font("BC Sans Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIndexer.Location = new System.Drawing.Point(7, 4);
            this.lblIndexer.Name = "lblIndexer";
            this.lblIndexer.Size = new System.Drawing.Size(56, 20);
            this.lblIndexer.TabIndex = 85;
            this.lblIndexer.Text = "Author";
            // 
            // txtIndexer
            // 
            this.txtIndexer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(235)))), ((int)(((byte)(233)))));
            this.txtIndexer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIndexer.Font = new System.Drawing.Font("BC Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIndexer.Location = new System.Drawing.Point(7, 27);
            this.txtIndexer.MaximumSize = new System.Drawing.Size(310, 29);
            this.txtIndexer.MinimumSize = new System.Drawing.Size(141, 29);
            this.txtIndexer.Name = "txtIndexer";
            this.txtIndexer.ReadOnly = true;
            this.txtIndexer.Size = new System.Drawing.Size(141, 29);
            this.txtIndexer.TabIndex = 86;
            this.txtIndexer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRotateImg);
            this.panel1.Controls.Add(this.btnImagePDF);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(298, 3);
            this.panel1.MaximumSize = new System.Drawing.Size(354, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(349, 37);
            this.panel1.TabIndex = 60;
            // 
            // btnRotateImg
            // 
            this.btnRotateImg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnRotateImg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRotateImg.ForeColor = System.Drawing.Color.Black;
            this.btnRotateImg.Location = new System.Drawing.Point(20, 3);
            this.btnRotateImg.Name = "btnRotateImg";
            this.btnRotateImg.Size = new System.Drawing.Size(136, 31);
            this.btnRotateImg.TabIndex = 5;
            this.btnRotateImg.Text = "Rotate Image";
            this.btnRotateImg.UseVisualStyleBackColor = true;
            this.btnRotateImg.Click += new System.EventHandler(this.btnRotateImg_Click);
            // 
            // btnImagePDF
            // 
            this.btnImagePDF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnImagePDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImagePDF.ForeColor = System.Drawing.Color.Black;
            this.btnImagePDF.Location = new System.Drawing.Point(186, 3);
            this.btnImagePDF.Name = "btnImagePDF";
            this.btnImagePDF.Size = new System.Drawing.Size(136, 31);
            this.btnImagePDF.TabIndex = 6;
            this.btnImagePDF.Text = "View as PDF";
            this.btnImagePDF.UseVisualStyleBackColor = true;
            this.btnImagePDF.Click += new System.EventHandler(this.btnImagePDF_Click);
            // 
            // panelImageControl
            // 
            this.panelImageControl.ColumnCount = 7;
            this.panelImageControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.panelImageControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.panelImageControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.panelImageControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.panelImageControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.panelImageControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.panelImageControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.panelImageControl.Controls.Add(this.panel1, 3, 0);
            this.panelImageControl.Controls.Add(this.tableLayoutPanel2, 5, 0);
            this.panelImageControl.Controls.Add(this.btnDeleteImage, 1, 0);
            this.panelImageControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelImageControl.Location = new System.Drawing.Point(1, 889);
            this.panelImageControl.Margin = new System.Windows.Forms.Padding(1);
            this.panelImageControl.MinimumSize = new System.Drawing.Size(708, 43);
            this.panelImageControl.Name = "panelImageControl";
            this.panelImageControl.RowCount = 1;
            this.panelImageControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelImageControl.Size = new System.Drawing.Size(1184, 43);
            this.panelImageControl.TabIndex = 93;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.11864F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.71186F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.49153F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.71186F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.474576F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.49153F));
            this.tableLayoutPanel2.Controls.Add(this.lblCurImage, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnPrevImage, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblDisplay, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblTotalImage, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnNextImage, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblDisplayImgOf, 4, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(710, 1);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel2.MaximumSize = new System.Drawing.Size(400, 43);
            this.tableLayoutPanel2.MinimumSize = new System.Drawing.Size(300, 43);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(400, 43);
            this.tableLayoutPanel2.TabIndex = 94;
            // 
            // lblCurImage
            // 
            this.lblCurImage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCurImage.AutoSize = true;
            this.lblCurImage.Font = new System.Drawing.Font("BC Sans", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurImage.Location = new System.Drawing.Point(179, 11);
            this.lblCurImage.Name = "lblCurImage";
            this.lblCurImage.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lblCurImage.Size = new System.Drawing.Size(34, 20);
            this.lblCurImage.TabIndex = 96;
            this.lblCurImage.Text = "0";
            this.lblCurImage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPrevImage
            // 
            this.btnPrevImage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnPrevImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrevImage.ForeColor = System.Drawing.Color.Black;
            this.btnPrevImage.Location = new System.Drawing.Point(111, 3);
            this.btnPrevImage.Name = "btnPrevImage";
            this.btnPrevImage.Size = new System.Drawing.Size(33, 37);
            this.btnPrevImage.TabIndex = 7;
            this.btnPrevImage.Text = "<";
            this.btnPrevImage.UseVisualStyleBackColor = true;
            this.btnPrevImage.Click += new System.EventHandler(this.btnPrevImage_Click);
            // 
            // lblDisplay
            // 
            this.lblDisplay.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDisplay.AutoSize = true;
            this.lblDisplay.Font = new System.Drawing.Font("BC Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisplay.Location = new System.Drawing.Point(5, 10);
            this.lblDisplay.Name = "lblDisplay";
            this.lblDisplay.Size = new System.Drawing.Size(100, 22);
            this.lblDisplay.TabIndex = 7;
            this.lblDisplay.Text = "Displaying:";
            this.lblDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalImage
            // 
            this.lblTotalImage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTotalImage.AutoSize = true;
            this.lblTotalImage.Font = new System.Drawing.Font("BC Sans", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalImage.Location = new System.Drawing.Point(350, 11);
            this.lblTotalImage.Name = "lblTotalImage";
            this.lblTotalImage.Size = new System.Drawing.Size(18, 20);
            this.lblTotalImage.TabIndex = 96;
            this.lblTotalImage.Text = "0";
            this.lblTotalImage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnNextImage
            // 
            this.btnNextImage.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnNextImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNextImage.ForeColor = System.Drawing.Color.Black;
            this.btnNextImage.Location = new System.Drawing.Point(251, 3);
            this.btnNextImage.Name = "btnNextImage";
            this.btnNextImage.Size = new System.Drawing.Size(31, 37);
            this.btnNextImage.TabIndex = 97;
            this.btnNextImage.Text = ">";
            this.btnNextImage.UseVisualStyleBackColor = true;
            this.btnNextImage.Click += new System.EventHandler(this.btnlNextImage_Click);
            // 
            // lblDisplayImgOf
            // 
            this.lblDisplayImgOf.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDisplayImgOf.AutoSize = true;
            this.lblDisplayImgOf.Font = new System.Drawing.Font("BC Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisplayImgOf.Location = new System.Drawing.Point(288, 10);
            this.lblDisplayImgOf.Name = "lblDisplayImgOf";
            this.lblDisplayImgOf.Size = new System.Drawing.Size(26, 22);
            this.lblDisplayImgOf.TabIndex = 95;
            this.lblDisplayImgOf.Text = "of";
            this.lblDisplayImgOf.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDeleteImage
            // 
            this.btnDeleteImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnDeleteImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteImage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(62)))), ((int)(((byte)(57)))));
            this.btnDeleteImage.Location = new System.Drawing.Point(79, 3);
            this.btnDeleteImage.Name = "btnDeleteImage";
            this.btnDeleteImage.Size = new System.Drawing.Size(136, 37);
            this.btnDeleteImage.TabIndex = 4;
            this.btnDeleteImage.Text = "Delete Image";
            this.btnDeleteImage.UseVisualStyleBackColor = true;
            this.btnDeleteImage.Click += new System.EventHandler(this.btnDeleteImage_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panelImageControl, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1186, 933);
            this.tableLayoutPanel1.TabIndex = 94;
            // 
            // imageBox
            // 
            this.imageBox.AutoScroll = true;
            this.imageBox.AutoSize = false;
            this.imageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBox.Location = new System.Drawing.Point(0, 0);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(662, 888);
            this.imageBox.TabIndex = 0;
            // 
            // btnScanPage
            // 
            this.btnScanPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnScanPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.btnScanPage.Font = new System.Drawing.Font("BC Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnScanPage.ForeColor = System.Drawing.Color.White;
            this.btnScanPage.Location = new System.Drawing.Point(354, 58);
            this.btnScanPage.MaximumSize = new System.Drawing.Size(153, 52);
            this.btnScanPage.MinimumSize = new System.Drawing.Size(153, 52);
            this.btnScanPage.Name = "btnScanPage";
            this.btnScanPage.Size = new System.Drawing.Size(153, 52);
            this.btnScanPage.TabIndex = 5;
            this.btnScanPage.Text = "Scan";
            this.btnScanPage.UseVisualStyleBackColor = false;
            this.btnScanPage.Click += new System.EventHandler(this.btnScanPage_Click);
            // 
            // panelScanningOptions
            // 
            this.panelScanningOptions.Controls.Add(this.useUICheckBox);
            this.panelScanningOptions.Controls.Add(this.ckBoxLowResolution);
            this.panelScanningOptions.Controls.Add(this.showProgressIndicatorUICheckBox);
            this.panelScanningOptions.Controls.Add(this.useAdfCheckBox);
            this.panelScanningOptions.Controls.Add(this.useDuplexCheckBox);
            this.panelScanningOptions.Controls.Add(this.testCheckBox);
            this.panelScanningOptions.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelScanningOptions.Location = new System.Drawing.Point(13, 25);
            this.panelScanningOptions.Margin = new System.Windows.Forms.Padding(0);
            this.panelScanningOptions.Name = "panelScanningOptions";
            this.panelScanningOptions.Size = new System.Drawing.Size(258, 103);
            this.panelScanningOptions.TabIndex = 36;
            // 
            // testCheckBox
            // 
            this.testCheckBox.AutoSize = true;
            this.testCheckBox.Font = new System.Drawing.Font("BC Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.testCheckBox.Location = new System.Drawing.Point(124, 74);
            this.testCheckBox.Name = "testCheckBox";
            this.testCheckBox.Size = new System.Drawing.Size(77, 21);
            this.testCheckBox.TabIndex = 35;
            this.testCheckBox.Text = "TESTING";
            this.testCheckBox.UseVisualStyleBackColor = true;
            this.testCheckBox.Visible = false;
            // 
            // useDuplexCheckBox
            // 
            this.useDuplexCheckBox.AutoSize = true;
            this.useDuplexCheckBox.Font = new System.Drawing.Font("BC Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.useDuplexCheckBox.Location = new System.Drawing.Point(124, 47);
            this.useDuplexCheckBox.MaximumSize = new System.Drawing.Size(127, 38);
            this.useDuplexCheckBox.Name = "useDuplexCheckBox";
            this.useDuplexCheckBox.Size = new System.Drawing.Size(116, 21);
            this.useDuplexCheckBox.TabIndex = 34;
            this.useDuplexCheckBox.Text = "Scan Both Sides";
            this.useDuplexCheckBox.UseVisualStyleBackColor = true;
            // 
            // useAdfCheckBox
            // 
            this.useAdfCheckBox.AutoSize = true;
            this.useAdfCheckBox.Font = new System.Drawing.Font("BC Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.useAdfCheckBox.Location = new System.Drawing.Point(124, 3);
            this.useAdfCheckBox.MinimumSize = new System.Drawing.Size(127, 38);
            this.useAdfCheckBox.Name = "useAdfCheckBox";
            this.useAdfCheckBox.Size = new System.Drawing.Size(127, 38);
            this.useAdfCheckBox.TabIndex = 29;
            this.useAdfCheckBox.Text = "Use Automatic\r\nDocument Feeder";
            this.useAdfCheckBox.UseVisualStyleBackColor = true;
            this.useAdfCheckBox.CheckedChanged += new System.EventHandler(this.useAdfCheckBox_CheckedChanged);
            // 
            // showProgressIndicatorUICheckBox
            // 
            this.showProgressIndicatorUICheckBox.AutoSize = true;
            this.showProgressIndicatorUICheckBox.Checked = true;
            this.showProgressIndicatorUICheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showProgressIndicatorUICheckBox.Font = new System.Drawing.Font("BC Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showProgressIndicatorUICheckBox.Location = new System.Drawing.Point(3, 57);
            this.showProgressIndicatorUICheckBox.Name = "showProgressIndicatorUICheckBox";
            this.showProgressIndicatorUICheckBox.Size = new System.Drawing.Size(110, 21);
            this.showProgressIndicatorUICheckBox.TabIndex = 33;
            this.showProgressIndicatorUICheckBox.Text = "Show Progress";
            this.showProgressIndicatorUICheckBox.UseVisualStyleBackColor = true;
            // 
            // ckBoxLowResolution
            // 
            this.ckBoxLowResolution.AutoSize = true;
            this.ckBoxLowResolution.Checked = true;
            this.ckBoxLowResolution.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckBoxLowResolution.Font = new System.Drawing.Font("BC Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckBoxLowResolution.Location = new System.Drawing.Point(3, 30);
            this.ckBoxLowResolution.Name = "ckBoxLowResolution";
            this.ckBoxLowResolution.Size = new System.Drawing.Size(110, 21);
            this.ckBoxLowResolution.TabIndex = 31;
            this.ckBoxLowResolution.Text = "Low Resolution";
            this.ckBoxLowResolution.UseVisualStyleBackColor = true;
            // 
            // useUICheckBox
            // 
            this.useUICheckBox.AutoSize = true;
            this.useUICheckBox.Checked = true;
            this.useUICheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.useUICheckBox.Font = new System.Drawing.Font("BC Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.useUICheckBox.Location = new System.Drawing.Point(3, 3);
            this.useUICheckBox.Name = "useUICheckBox";
            this.useUICheckBox.Size = new System.Drawing.Size(115, 21);
            this.useUICheckBox.TabIndex = 30;
            this.useUICheckBox.Text = "Show Advanced";
            this.useUICheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBoxScanningOptions
            // 
            this.groupBoxScanningOptions.Controls.Add(this.btnScanPage);
            this.groupBoxScanningOptions.Controls.Add(this.panelScanningOptions);
            this.groupBoxScanningOptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxScanningOptions.Font = new System.Drawing.Font("BC Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxScanningOptions.Location = new System.Drawing.Point(0, 0);
            this.groupBoxScanningOptions.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxScanningOptions.Name = "groupBoxScanningOptions";
            this.groupBoxScanningOptions.Size = new System.Drawing.Size(520, 144);
            this.groupBoxScanningOptions.TabIndex = 0;
            this.groupBoxScanningOptions.TabStop = false;
            this.groupBoxScanningOptions.Text = "Scanning Options";
            // 
            // groupBoxDocumentRecord
            // 
            this.groupBoxDocumentRecord.Controls.Add(this.groupBoxNotes);
            this.groupBoxDocumentRecord.Controls.Add(this.panelIndexer);
            this.groupBoxDocumentRecord.Controls.Add(this.groupBoxRecordClassification);
            this.groupBoxDocumentRecord.Controls.Add(this.panelFilingDate);
            this.groupBoxDocumentRecord.Controls.Add(this.panelPages);
            this.groupBoxDocumentRecord.Controls.Add(this.panelLegalEntity);
            this.groupBoxDocumentRecord.Controls.Add(this.panelBarcode);
            this.groupBoxDocumentRecord.Font = new System.Drawing.Font("BC Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxDocumentRecord.Location = new System.Drawing.Point(10, 201);
            this.groupBoxDocumentRecord.Name = "groupBoxDocumentRecord";
            this.groupBoxDocumentRecord.Size = new System.Drawing.Size(507, 489);
            this.groupBoxDocumentRecord.TabIndex = 1;
            this.groupBoxDocumentRecord.TabStop = false;
            this.groupBoxDocumentRecord.Text = "Document Record";
            // 
            // groupBoxRecordClassification
            // 
            this.groupBoxRecordClassification.Controls.Add(this.panelDocType);
            this.groupBoxRecordClassification.Controls.Add(this.panelDocClass);
            this.groupBoxRecordClassification.Font = new System.Drawing.Font("BC Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxRecordClassification.Location = new System.Drawing.Point(3, 206);
            this.groupBoxRecordClassification.Name = "groupBoxRecordClassification";
            this.groupBoxRecordClassification.Size = new System.Drawing.Size(501, 125);
            this.groupBoxRecordClassification.TabIndex = 2;
            this.groupBoxRecordClassification.TabStop = false;
            this.groupBoxRecordClassification.Text = "Record Classification";
            // 
            // groupBoxNotes
            // 
            this.groupBoxNotes.Controls.Add(this.txtDocumentNotes);
            this.groupBoxNotes.Font = new System.Drawing.Font("BC Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxNotes.Location = new System.Drawing.Point(3, 337);
            this.groupBoxNotes.Name = "groupBoxNotes";
            this.groupBoxNotes.Size = new System.Drawing.Size(501, 143);
            this.groupBoxNotes.TabIndex = 0;
            this.groupBoxNotes.TabStop = false;
            this.groupBoxNotes.Text = "Document Notes";
            // 
            // frmScannerDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1194, 941);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1194, 941);
            this.Name = "frmScannerDocument";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Document Scanner";
            this.Activated += new System.EventHandler(this.frmScanDocument_Activated);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelRejectSaveScan.ResumeLayout(false);
            this.panelDocClass.ResumeLayout(false);
            this.panelDocClass.PerformLayout();
            this.panelDocType.ResumeLayout(false);
            this.panelDocType.PerformLayout();
            this.panelBarcode.ResumeLayout(false);
            this.panelBarcode.PerformLayout();
            this.panelLegalEntity.ResumeLayout(false);
            this.panelLegalEntity.PerformLayout();
            this.panelPages.ResumeLayout(false);
            this.panelPages.PerformLayout();
            this.panelFilingDate.ResumeLayout(false);
            this.panelFilingDate.PerformLayout();
            this.panelIndexer.ResumeLayout(false);
            this.panelIndexer.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panelImageControl.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelScanningOptions.ResumeLayout(false);
            this.panelScanningOptions.PerformLayout();
            this.groupBoxScanningOptions.ResumeLayout(false);
            this.groupBoxDocumentRecord.ResumeLayout(false);
            this.groupBoxRecordClassification.ResumeLayout(false);
            this.groupBoxNotes.ResumeLayout(false);
            this.groupBoxNotes.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.TextBox txtDocumentNotes;
        private System.Windows.Forms.FlowLayoutPanel panelDocClass;
        private System.Windows.Forms.Label lblDocumentClass;
        private System.Windows.Forms.TextBox txtDocumentClass;
        private System.Windows.Forms.FlowLayoutPanel panelDocType;
        private System.Windows.Forms.Label lblDocumentType;
        private System.Windows.Forms.TextBox txtDocumentType;
        private System.Windows.Forms.FlowLayoutPanel panelPages;
        private System.Windows.Forms.Label lblTotalPages;
        private System.Windows.Forms.TextBox txtPagesInDocument;
        private System.Windows.Forms.FlowLayoutPanel panelFilingDate;
        private System.Windows.Forms.Label lblFilingDate;
        private System.Windows.Forms.TextBox txtFilingDate;
        private System.Windows.Forms.FlowLayoutPanel panelIndexer;
        private System.Windows.Forms.Label lblIndexer;
        private System.Windows.Forms.TextBox txtIndexer;
        private System.Windows.Forms.FlowLayoutPanel panelLegalEntity;
        private System.Windows.Forms.Label lblLegalEntity;
        private System.Windows.Forms.TextBox txtLegalEntityKey;
        private System.Windows.Forms.FlowLayoutPanel panelBarcode;
        private System.Windows.Forms.Label lblBarcode;
        private System.Windows.Forms.TextBox txtBarCode;
        private ImageBox imageBox;
        private System.Windows.Forms.TableLayoutPanel panelImageControl;
        private System.Windows.Forms.Label lblCurImage;
        private System.Windows.Forms.Button btnPrevImage;
        private System.Windows.Forms.Button btnImagePDF;
        private System.Windows.Forms.Button btnRotateImg;
        private System.Windows.Forms.Button btnNextImage;
        private System.Windows.Forms.Label lblDisplay;
        private System.Windows.Forms.Button btnDeleteImage;
        private System.Windows.Forms.Label lblTotalImage;
        private System.Windows.Forms.Label lblDisplayImgOf;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnCancelScan;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TableLayoutPanel panelRejectSaveScan;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBoxDocumentRecord;
        private System.Windows.Forms.GroupBox groupBoxScanningOptions;
        private System.Windows.Forms.Button btnScanPage;
        private System.Windows.Forms.FlowLayoutPanel panelScanningOptions;
        private System.Windows.Forms.CheckBox useUICheckBox;
        private System.Windows.Forms.CheckBox ckBoxLowResolution;
        private System.Windows.Forms.CheckBox showProgressIndicatorUICheckBox;
        private System.Windows.Forms.CheckBox useAdfCheckBox;
        private System.Windows.Forms.CheckBox useDuplexCheckBox;
        private System.Windows.Forms.CheckBox testCheckBox;
        private System.Windows.Forms.GroupBox groupBoxRecordClassification;
        private System.Windows.Forms.GroupBox groupBoxNotes;
    }
}