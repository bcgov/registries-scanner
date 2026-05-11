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
            this.txtIndexer = new System.Windows.Forms.TextBox();
            this.IndexerLabel = new System.Windows.Forms.Label();
            this.txtLegalEntityKey = new System.Windows.Forms.TextBox();
            this.txtLegalEntityLabel = new System.Windows.Forms.Label();
            this.txtAccessionNumber = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPagesInDocument = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDocumentType = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDocumentClass = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBarCode = new System.Windows.Forms.TextBox();
            this.showProgressIndicatorUICheckBox = new System.Windows.Forms.CheckBox();
            this.useUICheckBox = new System.Windows.Forms.CheckBox();
            this.ckBoxLowResolution = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.heightLabel = new System.Windows.Forms.Label();
            this.pnlNextPreviosImage = new System.Windows.Forms.Panel();
            this.btnDeleteImage = new System.Windows.Forms.Button();
            this.lbImageDisplay = new System.Windows.Forms.Label();
            this.btnPreviousImage = new System.Windows.Forms.Button();
            this.btnNextImage = new System.Windows.Forms.Button();
            this.lbDisplayImage = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnCancelScan = new System.Windows.Forms.Button();
            this.btnViewAsPDF = new System.Windows.Forms.Button();
            this.btnSharpen = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupScan = new System.Windows.Forms.Panel();
            this.useDuplexCheckBox = new System.Windows.Forms.CheckBox();
            this.useAdfCheckBox = new System.Windows.Forms.CheckBox();
            this.btnScanPage = new System.Windows.Forms.Button();
            this.txtBarcodeLabel = new System.Windows.Forms.Label();
            this.imageBox = new RegScan.ImageBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.pnlNextPreviosImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupScan.SuspendLayout();
            this.imageBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtIndexer
            // 
            this.txtIndexer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(235)))), ((int)(((byte)(233)))));
            this.txtIndexer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIndexer.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtIndexer.Location = new System.Drawing.Point(16, 184);
            this.txtIndexer.Name = "txtIndexer";
            this.txtIndexer.ReadOnly = true;
            this.txtIndexer.Size = new System.Drawing.Size(232, 20);
            this.txtIndexer.TabIndex = 86;
            // 
            // IndexerLabel
            // 
            this.IndexerLabel.AutoSize = true;
            this.IndexerLabel.BackColor = System.Drawing.Color.Transparent;
            this.IndexerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IndexerLabel.ForeColor = System.Drawing.Color.Black;
            this.IndexerLabel.Location = new System.Drawing.Point(16, 160);
            this.IndexerLabel.Name = "IndexerLabel";
            this.IndexerLabel.Size = new System.Drawing.Size(126, 17);
            this.IndexerLabel.TabIndex = 85;
            this.IndexerLabel.Text = "Indexing Author:";
            // 
            // txtLegalEntityKey
            // 
            this.txtLegalEntityKey.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(235)))), ((int)(((byte)(233)))));
            this.txtLegalEntityKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLegalEntityKey.Location = new System.Drawing.Point(272, 128);
            this.txtLegalEntityKey.Name = "txtLegalEntityKey";
            this.txtLegalEntityKey.ReadOnly = true;
            this.txtLegalEntityKey.Size = new System.Drawing.Size(232, 20);
            this.txtLegalEntityKey.TabIndex = 84;
            // 
            // txtLegalEntityLabel
            // 
            this.txtLegalEntityLabel.AutoSize = true;
            this.txtLegalEntityLabel.BackColor = System.Drawing.Color.Transparent;
            this.txtLegalEntityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLegalEntityLabel.ForeColor = System.Drawing.Color.Black;
            this.txtLegalEntityLabel.Location = new System.Drawing.Point(272, 104);
            this.txtLegalEntityLabel.Name = "txtLegalEntityLabel";
            this.txtLegalEntityLabel.Size = new System.Drawing.Size(99, 17);
            this.txtLegalEntityLabel.TabIndex = 83;
            this.txtLegalEntityLabel.Text = "Legal Entity:";
            // 
            // txtAccessionNumber
            // 
            this.txtAccessionNumber.BackColor = System.Drawing.Color.White;
            this.txtAccessionNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAccessionNumber.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtAccessionNumber.Location = new System.Drawing.Point(136, 288);
            this.txtAccessionNumber.Name = "txtAccessionNumber";
            this.txtAccessionNumber.ReadOnly = true;
            this.txtAccessionNumber.Size = new System.Drawing.Size(110, 20);
            this.txtAccessionNumber.TabIndex = 78;
            this.txtAccessionNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(16, 272);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 34);
            this.label8.TabIndex = 77;
            this.label8.Text = "Accession\r\nNumber:";
            // 
            // txtPagesInDocument
            // 
            this.txtPagesInDocument.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(235)))), ((int)(((byte)(233)))));
            this.txtPagesInDocument.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPagesInDocument.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtPagesInDocument.Location = new System.Drawing.Point(272, 184);
            this.txtPagesInDocument.Name = "txtPagesInDocument";
            this.txtPagesInDocument.ReadOnly = true;
            this.txtPagesInDocument.Size = new System.Drawing.Size(232, 20);
            this.txtPagesInDocument.TabIndex = 74;
            this.txtPagesInDocument.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(272, 160);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(153, 17);
            this.label6.TabIndex = 73;
            this.label6.Text = "Pages In Document:";
            // 
            // txtDocumentType
            // 
            this.txtDocumentType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(235)))), ((int)(((byte)(233)))));
            this.txtDocumentType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDocumentType.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtDocumentType.Location = new System.Drawing.Point(264, 232);
            this.txtDocumentType.Name = "txtDocumentType";
            this.txtDocumentType.ReadOnly = true;
            this.txtDocumentType.Size = new System.Drawing.Size(110, 20);
            this.txtDocumentType.TabIndex = 70;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(264, 216);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 16);
            this.label4.TabIndex = 69;
            this.label4.Text = "Type";
            // 
            // txtDocumentClass
            // 
            this.txtDocumentClass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(235)))), ((int)(((byte)(233)))));
            this.txtDocumentClass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDocumentClass.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtDocumentClass.Location = new System.Drawing.Point(136, 232);
            this.txtDocumentClass.Name = "txtDocumentClass";
            this.txtDocumentClass.ReadOnly = true;
            this.txtDocumentClass.Size = new System.Drawing.Size(110, 20);
            this.txtDocumentClass.TabIndex = 65;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(16, 216);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 34);
            this.label3.TabIndex = 68;
            this.label3.Text = "Document\r\nInformation:";
            // 
            // txtBarCode
            // 
            this.txtBarCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(235)))), ((int)(((byte)(233)))));
            this.txtBarCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBarCode.Location = new System.Drawing.Point(16, 128);
            this.txtBarCode.Name = "txtBarCode";
            this.txtBarCode.ReadOnly = true;
            this.txtBarCode.Size = new System.Drawing.Size(232, 20);
            this.txtBarCode.TabIndex = 67;
            this.txtBarCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // showProgressIndicatorUICheckBox
            // 
            this.showProgressIndicatorUICheckBox.AutoSize = true;
            this.showProgressIndicatorUICheckBox.Checked = true;
            this.showProgressIndicatorUICheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showProgressIndicatorUICheckBox.Location = new System.Drawing.Point(224, 56);
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
            this.useUICheckBox.Location = new System.Drawing.Point(16, 32);
            this.useUICheckBox.Name = "useUICheckBox";
            this.useUICheckBox.Size = new System.Drawing.Size(141, 17);
            this.useUICheckBox.TabIndex = 30;
            this.useUICheckBox.Text = "Show Advanced Setting";
            this.useUICheckBox.UseVisualStyleBackColor = true;
            // 
            // ckBoxLowResolution
            // 
            this.ckBoxLowResolution.AutoSize = true;
            this.ckBoxLowResolution.Checked = true;
            this.ckBoxLowResolution.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckBoxLowResolution.Location = new System.Drawing.Point(16, 56);
            this.ckBoxLowResolution.Name = "ckBoxLowResolution";
            this.ckBoxLowResolution.Size = new System.Drawing.Size(99, 17);
            this.ckBoxLowResolution.TabIndex = 31;
            this.ckBoxLowResolution.Text = "Low Resolution";
            this.ckBoxLowResolution.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.heightLabel);
            this.panel2.Controls.Add(this.progressBar);
            this.panel2.Controls.Add(this.btnCancelScan);
            this.panel2.Controls.Add(this.btnViewAsPDF);
            this.panel2.Controls.Add(this.btnSharpen);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Location = new System.Drawing.Point(-1, 419);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(521, 181);
            this.panel2.TabIndex = 91;
            // 
            // heightLabel
            // 
            this.heightLabel.AutoSize = true;
            this.heightLabel.Location = new System.Drawing.Point(426, 186);
            this.heightLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Size = new System.Drawing.Size(38, 13);
            this.heightLabel.TabIndex = 62;
            this.heightLabel.Text = "Height";
            // 
            // pnlNextPreviosImage
            // 
            this.pnlNextPreviosImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(248)))), ((int)(((byte)(254)))));
            this.pnlNextPreviosImage.Controls.Add(this.btnDeleteImage);
            this.pnlNextPreviosImage.Controls.Add(this.lbImageDisplay);
            this.pnlNextPreviosImage.Controls.Add(this.btnPreviousImage);
            this.pnlNextPreviosImage.Controls.Add(this.btnNextImage);
            this.pnlNextPreviosImage.Controls.Add(this.lbDisplayImage);
            this.pnlNextPreviosImage.Location = new System.Drawing.Point(0, 544);
            this.pnlNextPreviosImage.Name = "pnlNextPreviosImage";
            this.pnlNextPreviosImage.Size = new System.Drawing.Size(509, 51);
            this.pnlNextPreviosImage.TabIndex = 60;
            // 
            // btnDeleteImage
            // 
            this.btnDeleteImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteImage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(62)))), ((int)(((byte)(57)))));
            this.btnDeleteImage.Location = new System.Drawing.Point(368, 8);
            this.btnDeleteImage.Name = "btnDeleteImage";
            this.btnDeleteImage.Size = new System.Drawing.Size(136, 38);
            this.btnDeleteImage.TabIndex = 50;
            this.btnDeleteImage.Text = "Delete Page";
            this.btnDeleteImage.UseVisualStyleBackColor = true;
            this.btnDeleteImage.Visible = false;
            this.btnDeleteImage.Click += new System.EventHandler(this.btnDeleteImage_Click);
            // 
            // lbImageDisplay
            // 
            this.lbImageDisplay.AutoSize = true;
            this.lbImageDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbImageDisplay.Location = new System.Drawing.Point(16, 16);
            this.lbImageDisplay.Name = "lbImageDisplay";
            this.lbImageDisplay.Size = new System.Drawing.Size(58, 17);
            this.lbImageDisplay.TabIndex = 17;
            this.lbImageDisplay.Text = "Pages:";
            // 
            // btnPreviousImage
            // 
            this.btnPreviousImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPreviousImage.ForeColor = System.Drawing.Color.Blue;
            this.btnPreviousImage.Location = new System.Drawing.Point(104, 16);
            this.btnPreviousImage.Name = "btnPreviousImage";
            this.btnPreviousImage.Size = new System.Drawing.Size(34, 21);
            this.btnPreviousImage.TabIndex = 16;
            this.btnPreviousImage.Text = "<";
            this.btnPreviousImage.UseVisualStyleBackColor = true;
            this.btnPreviousImage.Click += new System.EventHandler(this.btnPreviousImage_Click);
            // 
            // btnNextImage
            // 
            this.btnNextImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNextImage.ForeColor = System.Drawing.Color.Blue;
            this.btnNextImage.Location = new System.Drawing.Point(216, 16);
            this.btnNextImage.Name = "btnNextImage";
            this.btnNextImage.Size = new System.Drawing.Size(34, 21);
            this.btnNextImage.TabIndex = 15;
            this.btnNextImage.Text = ">";
            this.btnNextImage.UseVisualStyleBackColor = true;
            this.btnNextImage.Click += new System.EventHandler(this.btnNextImage_Click);
            // 
            // lbDisplayImage
            // 
            this.lbDisplayImage.AutoSize = true;
            this.lbDisplayImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDisplayImage.Location = new System.Drawing.Point(152, 16);
            this.lbDisplayImage.Name = "lbDisplayImage";
            this.lbDisplayImage.Size = new System.Drawing.Size(55, 20);
            this.lbDisplayImage.TabIndex = 14;
            this.lbDisplayImage.Text = "0 of 0";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(16, 55);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(278, 23);
            this.progressBar.TabIndex = 59;
            this.progressBar.Visible = false;
            // 
            // btnCancelScan
            // 
            this.btnCancelScan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(62)))), ((int)(((byte)(57)))));
            this.btnCancelScan.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(62)))), ((int)(((byte)(57)))));
            this.btnCancelScan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelScan.ForeColor = System.Drawing.Color.White;
            this.btnCancelScan.Location = new System.Drawing.Point(200, 136);
            this.btnCancelScan.Name = "btnCancelScan";
            this.btnCancelScan.Size = new System.Drawing.Size(136, 37);
            this.btnCancelScan.TabIndex = 4;
            this.btnCancelScan.Text = "Reject Scan";
            this.btnCancelScan.UseVisualStyleBackColor = false;
            this.btnCancelScan.Click += new System.EventHandler(this.btnCancelScan_Click);
            // 
            // btnViewAsPDF
            // 
            this.btnViewAsPDF.Enabled = false;
            this.btnViewAsPDF.Location = new System.Drawing.Point(315, 55);
            this.btnViewAsPDF.Name = "btnViewAsPDF";
            this.btnViewAsPDF.Size = new System.Drawing.Size(80, 23);
            this.btnViewAsPDF.TabIndex = 56;
            this.btnViewAsPDF.Text = "View PDF";
            this.btnViewAsPDF.UseVisualStyleBackColor = true;
            this.btnViewAsPDF.Click += new System.EventHandler(this.btnViewAsPDF_Click);
            // 
            // btnSharpen
            // 
            this.btnSharpen.Enabled = false;
            this.btnSharpen.Location = new System.Drawing.Point(315, 28);
            this.btnSharpen.Name = "btnSharpen";
            this.btnSharpen.Size = new System.Drawing.Size(80, 23);
            this.btnSharpen.TabIndex = 55;
            this.btnSharpen.Text = "Rotate";
            this.btnSharpen.UseVisualStyleBackColor = true;
            this.btnSharpen.Click += new System.EventHandler(this.btnRotate_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(129)))), ((int)(((byte)(74)))));
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(129)))), ((int)(((byte)(74)))));
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(368, 136);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(136, 37);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save Scan";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(16, 360);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessage.Size = new System.Drawing.Size(368, 40);
            this.txtMessage.TabIndex = 63;
            this.txtMessage.Visible = false;
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
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.label13);
            this.splitContainer1.Panel1.Controls.Add(this.label11);
            this.splitContainer1.Panel1.Controls.Add(this.label9);
            this.splitContainer1.Panel1.Controls.Add(this.textBox3);
            this.splitContainer1.Panel1.Controls.Add(this.textBox2);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.textBox1);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            this.splitContainer1.Panel1.Controls.Add(this.groupScan);
            this.splitContainer1.Panel1.Controls.Add(this.txtMessage);
            this.splitContainer1.Panel1.Controls.Add(this.txtIndexer);
            this.splitContainer1.Panel1.Controls.Add(this.IndexerLabel);
            this.splitContainer1.Panel1.Controls.Add(this.txtLegalEntityKey);
            this.splitContainer1.Panel1.Controls.Add(this.txtLegalEntityLabel);
            this.splitContainer1.Panel1.Controls.Add(this.txtAccessionNumber);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.txtPagesInDocument);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.txtDocumentType);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.txtDocumentClass);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.txtBarCode);
            this.splitContainer1.Panel1.Controls.Add(this.txtBarcodeLabel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.imageBox);
            this.splitContainer1.Size = new System.Drawing.Size(1034, 691);
            this.splitContainer1.SplitterDistance = 513;
            this.splitContainer1.TabIndex = 67;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(16, 336);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 17);
            this.label1.TabIndex = 101;
            this.label1.Text = "Description:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(392, 272);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(81, 16);
            this.label13.TabIndex = 100;
            this.label13.Text = "Box Number";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(264, 272);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(115, 16);
            this.label11.TabIndex = 99;
            this.label11.Text = "Schedule Number";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(136, 272);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(120, 16);
            this.label9.TabIndex = 98;
            this.label9.Text = "Sequence Number";
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.White;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox3.Location = new System.Drawing.Point(392, 288);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(110, 20);
            this.textBox3.TabIndex = 97;
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.White;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox2.Location = new System.Drawing.Point(264, 288);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(110, 20);
            this.textBox2.TabIndex = 96;
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(392, 216);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 16);
            this.label7.TabIndex = 95;
            this.label7.Text = "Description";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(136, 216);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 16);
            this.label2.TabIndex = 94;
            this.label2.Text = "Class";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(235)))), ((int)(((byte)(233)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox1.Location = new System.Drawing.Point(392, 232);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(110, 20);
            this.textBox1.TabIndex = 93;
            // 
            // groupScan
            // 
            this.groupScan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(249)))), ((int)(((byte)(248)))));
            this.groupScan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.groupScan.Controls.Add(this.useDuplexCheckBox);
            this.groupScan.Controls.Add(this.useAdfCheckBox);
            this.groupScan.Controls.Add(this.showProgressIndicatorUICheckBox);
            this.groupScan.Controls.Add(this.useUICheckBox);
            this.groupScan.Controls.Add(this.ckBoxLowResolution);
            this.groupScan.Controls.Add(this.btnScanPage);
            this.groupScan.ForeColor = System.Drawing.Color.Black;
            this.groupScan.Location = new System.Drawing.Point(8, 8);
            this.groupScan.Name = "groupScan";
            this.groupScan.Size = new System.Drawing.Size(496, 88);
            this.groupScan.TabIndex = 88;
            // 
            // useDuplexCheckBox
            // 
            this.useDuplexCheckBox.AutoSize = true;
            this.useDuplexCheckBox.Location = new System.Drawing.Point(224, 8);
            this.useDuplexCheckBox.Name = "useDuplexCheckBox";
            this.useDuplexCheckBox.Size = new System.Drawing.Size(105, 17);
            this.useDuplexCheckBox.TabIndex = 34;
            this.useDuplexCheckBox.Text = "Scan Both Sides";
            this.useDuplexCheckBox.UseVisualStyleBackColor = true;
            // 
            // useAdfCheckBox
            // 
            this.useAdfCheckBox.AutoSize = true;
            this.useAdfCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.useAdfCheckBox.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.useAdfCheckBox.Location = new System.Drawing.Point(16, 8);
            this.useAdfCheckBox.Name = "useAdfCheckBox";
            this.useAdfCheckBox.Size = new System.Drawing.Size(183, 17);
            this.useAdfCheckBox.TabIndex = 29;
            this.useAdfCheckBox.Text = "Use Automatic Document Feeder";
            this.useAdfCheckBox.UseVisualStyleBackColor = false;
            this.useAdfCheckBox.CheckedChanged += new System.EventHandler(this.useAdfCheckBox_CheckedChanged);
            // 
            // btnScanPage
            // 
            this.btnScanPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.btnScanPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnScanPage.ForeColor = System.Drawing.Color.White;
            this.btnScanPage.Location = new System.Drawing.Point(368, 8);
            this.btnScanPage.Name = "btnScanPage";
            this.btnScanPage.Padding = new System.Windows.Forms.Padding(2);
            this.btnScanPage.Size = new System.Drawing.Size(111, 63);
            this.btnScanPage.TabIndex = 1;
            this.btnScanPage.Text = "Scan";
            this.btnScanPage.UseVisualStyleBackColor = false;
            this.btnScanPage.Click += new System.EventHandler(this.btnScanPage_Click);
            // 
            // txtBarcodeLabel
            // 
            this.txtBarcodeLabel.AutoSize = true;
            this.txtBarcodeLabel.BackColor = System.Drawing.Color.Transparent;
            this.txtBarcodeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBarcodeLabel.ForeColor = System.Drawing.Color.Black;
            this.txtBarcodeLabel.Location = new System.Drawing.Point(16, 104);
            this.txtBarcodeLabel.Name = "txtBarcodeLabel";
            this.txtBarcodeLabel.Size = new System.Drawing.Size(73, 17);
            this.txtBarcodeLabel.TabIndex = 66;
            this.txtBarcodeLabel.Text = "Barcode:";
            // 
            // imageBox
            // 
            this.imageBox.AutoScroll = true;
            this.imageBox.AutoSize = false;
            this.imageBox.Controls.Add(this.pnlNextPreviosImage);
            this.imageBox.Controls.Add(this.panel1);
            this.imageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBox.Location = new System.Drawing.Point(0, 0);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(517, 691);
            this.imageBox.TabIndex = 0;
            this.imageBox.ZoomChanged += new System.EventHandler(this.imageBox_ZoomChanged);
            this.imageBox.Scroll += new System.Windows.Forms.ScrollEventHandler(this.imageBox_Scroll);
            this.imageBox.Resize += new System.EventHandler(this.imageBox_Resize);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(0, 504);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(512, 93);
            this.panel1.TabIndex = 92;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(426, 186);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 62;
            this.label5.Text = "Height";
            // 
            // frmScannerDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 691);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmScannerDocument";
            this.Text = "Document Scanner";
            this.Activated += new System.EventHandler(this.frmScanDocument_Activated);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlNextPreviosImage.ResumeLayout(false);
            this.pnlNextPreviosImage.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupScan.ResumeLayout(false);
            this.groupScan.PerformLayout();
            this.imageBox.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox txtIndexer;
        private System.Windows.Forms.Label IndexerLabel;
        private System.Windows.Forms.TextBox txtLegalEntityKey;
        private System.Windows.Forms.Label txtLegalEntityLabel;
        private System.Windows.Forms.TextBox txtAccessionNumber;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPagesInDocument;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDocumentType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDocumentClass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBarCode;
        private System.Windows.Forms.CheckBox showProgressIndicatorUICheckBox;
        private ImageBox imageBox;
        private System.Windows.Forms.CheckBox useUICheckBox;
        private System.Windows.Forms.CheckBox ckBoxLowResolution;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label heightLabel;
        private System.Windows.Forms.Panel pnlNextPreviosImage;
        private System.Windows.Forms.Button btnDeleteImage;
        private System.Windows.Forms.Label lbImageDisplay;
        private System.Windows.Forms.Button btnPreviousImage;
        private System.Windows.Forms.Button btnNextImage;
        private System.Windows.Forms.Label lbDisplayImage;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnCancelScan;
        private System.Windows.Forms.Button btnViewAsPDF;
        private System.Windows.Forms.Button btnSharpen;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel groupScan;
        private System.Windows.Forms.Button btnScanPage;
        private System.Windows.Forms.Label txtBarcodeLabel;
        private System.Windows.Forms.CheckBox useDuplexCheckBox;
        private System.Windows.Forms.CheckBox useAdfCheckBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
    }
}