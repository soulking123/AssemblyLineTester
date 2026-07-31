namespace AssemblyLineTester.Views.Pages
{
    partial class TestExecution
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
            if (disposing)
            {
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tblMain = new TableLayoutPanel();
            pnlTopBar = new Panel();
            tblTopWrapper = new TableLayoutPanel();
            lblPageHeader = new Label();
            tblFilterRow = new TableLayoutPanel();
            pnlFilterCard = new Panel();
            tblFilters = new TableLayoutPanel();
            lblFilterModel = new Label();
            cmbProductModel = new ComboBox();
            lblFilterType = new Label();
            cmbProductType = new ComboBox();
            lblFilterArea = new Label();
            cmbArea = new ComboBox();
            lblFilterPart = new Label();
            cmbPartNumber = new ComboBox();
            lblFilterEcn = new Label();
            cmbEcnNo = new ComboBox();
            btnLoadProgram = new Button();
            tblActionButtons = new TableLayoutPanel();
            btnStartTest = new Button();
            btnStopTest = new Button();
            btnSaveReport = new Button();
            tblRow1 = new TableLayoutPanel();
            cardMachineOverview = new Panel();
            picMachineOverview = new PictureBox();
            lblMachineOverviewTitle = new Label();
            cardAreaDetail = new Panel();
            picAreaDetail = new PictureBox();
            lblAreaDetailTitle = new Label();
            cardHarnessConn = new Panel();
            pnlHarnessImgContainer = new Panel();
            btnViewPinMap = new Button();
            picHarnessConn = new PictureBox();
            lblHarnessConnTitle = new Label();
            cardTestStatus = new Panel();
            tblStatusLabels = new TableLayoutPanel();
            lblStartTimeKey = new Label();
            lblStartTimeVal = new Label();
            lblElapsedKey = new Label();
            lblElapsedTimeVal = new Label();
            lblEstimatedKey = new Label();
            lblEstimatedVal = new Label();
            lblEndTimeKey = new Label();
            lblEndTimeVal = new Label();
            lblStatusRunning = new Label();
            lblTestStatusTitle = new Label();
            tblRow2 = new TableLayoutPanel();
            cardLiveDetails = new Panel();
            gridLiveDetails = new DataGridView();
            pnlLiveDetailsHeader = new Panel();
            btnViewWiringDiagram = new Button();
            lblLiveDetailsTitle = new Label();
            cardTestSummary = new Panel();
            listBoxTestSummary = new ListBox();
            lblTestSummaryTitle = new Label();
            cardNotes = new Panel();
            txtNotes = new TextBox();
            lblNotesTitle = new Label();
            tblMain.SuspendLayout();
            pnlTopBar.SuspendLayout();
            tblTopWrapper.SuspendLayout();
            tblFilterRow.SuspendLayout();
            pnlFilterCard.SuspendLayout();
            tblFilters.SuspendLayout();
            tblActionButtons.SuspendLayout();
            tblRow1.SuspendLayout();
            cardMachineOverview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picMachineOverview).BeginInit();
            cardAreaDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picAreaDetail).BeginInit();
            cardHarnessConn.SuspendLayout();
            pnlHarnessImgContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picHarnessConn).BeginInit();
            cardTestStatus.SuspendLayout();
            tblStatusLabels.SuspendLayout();
            tblRow2.SuspendLayout();
            cardLiveDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridLiveDetails).BeginInit();
            pnlLiveDetailsHeader.SuspendLayout();
            cardTestSummary.SuspendLayout();
            cardNotes.SuspendLayout();
            SuspendLayout();
            // 
            // tblMain
            // 
            tblMain.BackColor = Color.FromArgb(244, 245, 247);
            tblMain.ColumnCount = 1;
            tblMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblMain.Controls.Add(pnlTopBar, 0, 0);
            tblMain.Controls.Add(tblRow1, 0, 1);
            tblMain.Controls.Add(tblRow2, 0, 2);
            tblMain.Dock = DockStyle.Fill;
            tblMain.Location = new Point(0, 0);
            tblMain.MinimumSize = new Size(1500, 0);
            tblMain.Name = "tblMain";
            tblMain.RowCount = 3;
            tblMain.RowStyles.Add(new RowStyle());
            tblMain.RowStyles.Add(new RowStyle(SizeType.Percent, 35F));
            tblMain.RowStyles.Add(new RowStyle(SizeType.Percent, 65F));
            tblMain.Size = new Size(2676, 1653);
            tblMain.TabIndex = 0;
            // 
            // pnlTopBar
            // 
            pnlTopBar.AutoSize = true;
            pnlTopBar.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlTopBar.Controls.Add(tblTopWrapper);
            pnlTopBar.Dock = DockStyle.Fill;
            pnlTopBar.Location = new Point(5, 5);
            pnlTopBar.Margin = new Padding(5);
            pnlTopBar.Name = "pnlTopBar";
            pnlTopBar.Size = new Size(2666, 154);
            pnlTopBar.TabIndex = 0;
            // 
            // tblTopWrapper
            // 
            tblTopWrapper.AutoSize = true;
            tblTopWrapper.ColumnCount = 1;
            tblTopWrapper.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblTopWrapper.Controls.Add(lblPageHeader, 0, 0);
            tblTopWrapper.Controls.Add(tblFilterRow, 0, 1);
            tblTopWrapper.Dock = DockStyle.Fill;
            tblTopWrapper.Location = new Point(0, 0);
            tblTopWrapper.Name = "tblTopWrapper";
            tblTopWrapper.RowCount = 2;
            tblTopWrapper.RowStyles.Add(new RowStyle());
            tblTopWrapper.RowStyles.Add(new RowStyle());
            tblTopWrapper.Size = new Size(2666, 154);
            tblTopWrapper.TabIndex = 0;
            // 
            // lblPageHeader
            // 
            lblPageHeader.AutoSize = true;
            lblPageHeader.Dock = DockStyle.Fill;
            lblPageHeader.Font = new Font("Segoe UI", 13.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPageHeader.ForeColor = Color.FromArgb(30, 41, 59);
            lblPageHeader.Location = new Point(10, 0);
            lblPageHeader.Margin = new Padding(10, 0, 0, 8);
            lblPageHeader.Name = "lblPageHeader";
            lblPageHeader.Size = new Size(2656, 31);
            lblPageHeader.TabIndex = 0;
            lblPageHeader.Text = "⚡  TEST EXECUTION";
            lblPageHeader.TextAlign = ContentAlignment.BottomLeft;
            // 
            // tblFilterRow
            // 
            tblFilterRow.ColumnCount = 2;
            tblFilterRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75F));
            tblFilterRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblFilterRow.Controls.Add(pnlFilterCard, 0, 0);
            tblFilterRow.Controls.Add(tblActionButtons, 1, 0);
            tblFilterRow.Dock = DockStyle.Fill;
            tblFilterRow.Location = new Point(0, 39);
            tblFilterRow.Margin = new Padding(0);
            tblFilterRow.Name = "tblFilterRow";
            tblFilterRow.RowCount = 1;
            tblFilterRow.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblFilterRow.Size = new Size(2666, 115);
            tblFilterRow.TabIndex = 1;
            // 
            // pnlFilterCard
            // 
            pnlFilterCard.BackColor = Color.White;
            pnlFilterCard.Controls.Add(tblFilters);
            pnlFilterCard.Dock = DockStyle.Fill;
            pnlFilterCard.Location = new Point(0, 0);
            pnlFilterCard.Margin = new Padding(0, 0, 10, 0);
            pnlFilterCard.Name = "pnlFilterCard";
            pnlFilterCard.Size = new Size(1989, 115);
            pnlFilterCard.TabIndex = 0;
            // 
            // tblFilters
            // 
            tblFilters.ColumnCount = 6;
            tblFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66F));
            tblFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66F));
            tblFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66F));
            tblFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66F));
            tblFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66F));
            tblFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.7F));
            tblFilters.Controls.Add(lblFilterModel, 0, 0);
            tblFilters.Controls.Add(cmbProductModel, 0, 1);
            tblFilters.Controls.Add(lblFilterType, 1, 0);
            tblFilters.Controls.Add(cmbProductType, 1, 1);
            tblFilters.Controls.Add(lblFilterArea, 2, 0);
            tblFilters.Controls.Add(cmbArea, 2, 1);
            tblFilters.Controls.Add(lblFilterPart, 3, 0);
            tblFilters.Controls.Add(cmbPartNumber, 3, 1);
            tblFilters.Controls.Add(lblFilterEcn, 4, 0);
            tblFilters.Controls.Add(cmbEcnNo, 4, 1);
            tblFilters.Controls.Add(btnLoadProgram, 5, 0);
            tblFilters.Dock = DockStyle.Fill;
            tblFilters.Location = new Point(0, 0);
            tblFilters.Margin = new Padding(0);
            tblFilters.Name = "tblFilters";
            tblFilters.Padding = new Padding(8, 4, 8, 4);
            tblFilters.RowCount = 2;
            tblFilters.RowStyles.Add(new RowStyle());
            tblFilters.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblFilters.Size = new Size(1989, 115);
            tblFilters.TabIndex = 0;
            // 
            // lblFilterModel
            // 
            lblFilterModel.AutoSize = true;
            lblFilterModel.Dock = DockStyle.Bottom;
            lblFilterModel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFilterModel.ForeColor = Color.FromArgb(71, 85, 105);
            lblFilterModel.Location = new Point(11, 4);
            lblFilterModel.Margin = new Padding(3, 0, 3, 2);
            lblFilterModel.Name = "lblFilterModel";
            lblFilterModel.Size = new Size(322, 20);
            lblFilterModel.TabIndex = 0;
            lblFilterModel.Text = "Product Model";
            // 
            // cmbProductModel
            // 
            cmbProductModel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbProductModel.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProductModel.Font = new Font("Segoe UI", 10F);
            cmbProductModel.FormattingEnabled = true;
            cmbProductModel.Location = new Point(11, 53);
            cmbProductModel.Name = "cmbProductModel";
            cmbProductModel.Size = new Size(322, 31);
            cmbProductModel.TabIndex = 1;
            // 
            // lblFilterType
            // 
            lblFilterType.AutoSize = true;
            lblFilterType.Dock = DockStyle.Bottom;
            lblFilterType.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFilterType.ForeColor = Color.FromArgb(71, 85, 105);
            lblFilterType.Location = new Point(339, 4);
            lblFilterType.Margin = new Padding(3, 0, 3, 2);
            lblFilterType.Name = "lblFilterType";
            lblFilterType.Size = new Size(322, 20);
            lblFilterType.TabIndex = 2;
            lblFilterType.Text = "Product Type";
            // 
            // cmbProductType
            // 
            cmbProductType.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbProductType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProductType.Font = new Font("Segoe UI", 10F);
            cmbProductType.FormattingEnabled = true;
            cmbProductType.Location = new Point(339, 53);
            cmbProductType.Name = "cmbProductType";
            cmbProductType.Size = new Size(322, 31);
            cmbProductType.TabIndex = 3;
            // 
            // lblFilterArea
            // 
            lblFilterArea.AutoSize = true;
            lblFilterArea.Dock = DockStyle.Bottom;
            lblFilterArea.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFilterArea.ForeColor = Color.FromArgb(71, 85, 105);
            lblFilterArea.Location = new Point(667, 4);
            lblFilterArea.Margin = new Padding(3, 0, 3, 2);
            lblFilterArea.Name = "lblFilterArea";
            lblFilterArea.Size = new Size(322, 20);
            lblFilterArea.TabIndex = 4;
            lblFilterArea.Text = "Area";
            // 
            // cmbArea
            // 
            cmbArea.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbArea.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbArea.Font = new Font("Segoe UI", 10F);
            cmbArea.FormattingEnabled = true;
            cmbArea.Location = new Point(667, 53);
            cmbArea.Name = "cmbArea";
            cmbArea.Size = new Size(322, 31);
            cmbArea.TabIndex = 5;
            // 
            // lblFilterPart
            // 
            lblFilterPart.AutoSize = true;
            lblFilterPart.Dock = DockStyle.Bottom;
            lblFilterPart.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFilterPart.ForeColor = Color.FromArgb(71, 85, 105);
            lblFilterPart.Location = new Point(995, 4);
            lblFilterPart.Margin = new Padding(3, 0, 3, 2);
            lblFilterPart.Name = "lblFilterPart";
            lblFilterPart.Size = new Size(322, 20);
            lblFilterPart.TabIndex = 6;
            lblFilterPart.Text = "Part Number";
            // 
            // cmbPartNumber
            // 
            cmbPartNumber.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbPartNumber.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPartNumber.Font = new Font("Segoe UI", 10F);
            cmbPartNumber.FormattingEnabled = true;
            cmbPartNumber.Location = new Point(995, 53);
            cmbPartNumber.Name = "cmbPartNumber";
            cmbPartNumber.Size = new Size(322, 31);
            cmbPartNumber.TabIndex = 7;
            // 
            // lblFilterEcn
            // 
            lblFilterEcn.AutoSize = true;
            lblFilterEcn.Dock = DockStyle.Bottom;
            lblFilterEcn.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFilterEcn.ForeColor = Color.FromArgb(71, 85, 105);
            lblFilterEcn.Location = new Point(1323, 4);
            lblFilterEcn.Margin = new Padding(3, 0, 3, 2);
            lblFilterEcn.Name = "lblFilterEcn";
            lblFilterEcn.Size = new Size(322, 20);
            lblFilterEcn.TabIndex = 8;
            lblFilterEcn.Text = "ECN No.";
            // 
            // cmbEcnNo
            // 
            cmbEcnNo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbEcnNo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEcnNo.Font = new Font("Segoe UI", 10F);
            cmbEcnNo.FormattingEnabled = true;
            cmbEcnNo.Location = new Point(1323, 53);
            cmbEcnNo.Name = "cmbEcnNo";
            cmbEcnNo.Size = new Size(322, 31);
            cmbEcnNo.TabIndex = 9;
            // 
            // btnLoadProgram
            // 
            btnLoadProgram.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnLoadProgram.BackColor = Color.FromArgb(255, 193, 7);
            btnLoadProgram.FlatAppearance.BorderSize = 0;
            btnLoadProgram.FlatStyle = FlatStyle.Flat;
            btnLoadProgram.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLoadProgram.ForeColor = Color.Black;
            btnLoadProgram.Location = new Point(1658, 30);
            btnLoadProgram.Margin = new Padding(10, 3, 3, 3);
            btnLoadProgram.Name = "btnLoadProgram";
            tblFilters.SetRowSpan(btnLoadProgram, 2);
            btnLoadProgram.Size = new Size(320, 55);
            btnLoadProgram.TabIndex = 10;
            btnLoadProgram.Text = "📁 Load Test Program";
            btnLoadProgram.UseVisualStyleBackColor = false;
            // 
            // tblActionButtons
            // 
            tblActionButtons.ColumnCount = 3;
            tblActionButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            tblActionButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            tblActionButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            tblActionButtons.Controls.Add(btnStartTest, 0, 0);
            tblActionButtons.Controls.Add(btnStopTest, 1, 0);
            tblActionButtons.Controls.Add(btnSaveReport, 2, 0);
            tblActionButtons.Dock = DockStyle.Fill;
            tblActionButtons.Location = new Point(1999, 0);
            tblActionButtons.Margin = new Padding(0);
            tblActionButtons.Name = "tblActionButtons";
            tblActionButtons.RowCount = 1;
            tblActionButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblActionButtons.Size = new Size(667, 115);
            tblActionButtons.TabIndex = 1;
            // 
            // btnStartTest
            // 
            btnStartTest.BackColor = Color.FromArgb(56, 142, 60);
            btnStartTest.Dock = DockStyle.Fill;
            btnStartTest.FlatAppearance.BorderSize = 0;
            btnStartTest.FlatStyle = FlatStyle.Flat;
            btnStartTest.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnStartTest.ForeColor = Color.White;
            btnStartTest.Location = new Point(0, 0);
            btnStartTest.Margin = new Padding(0, 0, 6, 0);
            btnStartTest.Name = "btnStartTest";
            btnStartTest.Size = new Size(220, 115);
            btnStartTest.TabIndex = 0;
            btnStartTest.Text = "▶ START TEST";
            btnStartTest.UseVisualStyleBackColor = false;
            btnStartTest.Click += btnStartTest_Click;
            // 
            // btnStopTest
            // 
            btnStopTest.BackColor = Color.FromArgb(211, 47, 47);
            btnStopTest.Dock = DockStyle.Fill;
            btnStopTest.FlatAppearance.BorderSize = 0;
            btnStopTest.FlatStyle = FlatStyle.Flat;
            btnStopTest.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnStopTest.ForeColor = Color.White;
            btnStopTest.Location = new Point(229, 0);
            btnStopTest.Margin = new Padding(3, 0, 3, 0);
            btnStopTest.Name = "btnStopTest";
            btnStopTest.Size = new Size(214, 115);
            btnStopTest.TabIndex = 1;
            btnStopTest.Text = "⏹ STOP TEST";
            btnStopTest.UseVisualStyleBackColor = false;
            btnStopTest.Click += btnStopTest_Click;
            // 
            // btnSaveReport
            // 
            btnSaveReport.BackColor = Color.FromArgb(37, 99, 235);
            btnSaveReport.Dock = DockStyle.Fill;
            btnSaveReport.FlatAppearance.BorderSize = 0;
            btnSaveReport.FlatStyle = FlatStyle.Flat;
            btnSaveReport.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSaveReport.ForeColor = Color.White;
            btnSaveReport.Location = new Point(449, 0);
            btnSaveReport.Margin = new Padding(3, 0, 0, 0);
            btnSaveReport.Name = "btnSaveReport";
            btnSaveReport.Size = new Size(218, 115);
            btnSaveReport.TabIndex = 2;
            btnSaveReport.Text = "💾 SAVE";
            btnSaveReport.UseVisualStyleBackColor = false;
            // 
            // tblRow1
            // 
            tblRow1.AutoSize = true;
            tblRow1.ColumnCount = 4;
            tblRow1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblRow1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblRow1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblRow1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblRow1.Controls.Add(cardMachineOverview, 0, 0);
            tblRow1.Controls.Add(cardAreaDetail, 1, 0);
            tblRow1.Controls.Add(cardHarnessConn, 2, 0);
            tblRow1.Controls.Add(cardTestStatus, 3, 0);
            tblRow1.Dock = DockStyle.Fill;
            tblRow1.Location = new Point(3, 167);
            tblRow1.Name = "tblRow1";
            tblRow1.RowCount = 1;
            tblRow1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblRow1.Size = new Size(2670, 515);
            tblRow1.TabIndex = 1;
            // 
            // cardMachineOverview
            // 
            cardMachineOverview.BackColor = Color.White;
            cardMachineOverview.Controls.Add(picMachineOverview);
            cardMachineOverview.Controls.Add(lblMachineOverviewTitle);
            cardMachineOverview.Dock = DockStyle.Fill;
            cardMachineOverview.Location = new Point(5, 5);
            cardMachineOverview.Margin = new Padding(5);
            cardMachineOverview.Name = "cardMachineOverview";
            cardMachineOverview.Padding = new Padding(10);
            cardMachineOverview.Size = new Size(657, 505);
            cardMachineOverview.TabIndex = 0;
            // 
            // picMachineOverview
            // 
            picMachineOverview.BackColor = Color.LightGray;
            picMachineOverview.Dock = DockStyle.Fill;
            picMachineOverview.Location = new Point(10, 56);
            picMachineOverview.Name = "picMachineOverview";
            picMachineOverview.Size = new Size(637, 439);
            picMachineOverview.TabIndex = 1;
            picMachineOverview.TabStop = false;
            // 
            // lblMachineOverviewTitle
            // 
            lblMachineOverviewTitle.Dock = DockStyle.Top;
            lblMachineOverviewTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMachineOverviewTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblMachineOverviewTitle.Location = new Point(10, 10);
            lblMachineOverviewTitle.Name = "lblMachineOverviewTitle";
            lblMachineOverviewTitle.Size = new Size(637, 46);
            lblMachineOverviewTitle.TabIndex = 0;
            lblMachineOverviewTitle.Text = "MACHINE OVERVIEW";
            lblMachineOverviewTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cardAreaDetail
            // 
            cardAreaDetail.BackColor = Color.White;
            cardAreaDetail.Controls.Add(picAreaDetail);
            cardAreaDetail.Controls.Add(lblAreaDetailTitle);
            cardAreaDetail.Dock = DockStyle.Fill;
            cardAreaDetail.Location = new Point(672, 5);
            cardAreaDetail.Margin = new Padding(5);
            cardAreaDetail.Name = "cardAreaDetail";
            cardAreaDetail.Padding = new Padding(10);
            cardAreaDetail.Size = new Size(657, 505);
            cardAreaDetail.TabIndex = 1;
            // 
            // picAreaDetail
            // 
            picAreaDetail.BackColor = Color.LightGray;
            picAreaDetail.Dock = DockStyle.Fill;
            picAreaDetail.Location = new Point(10, 56);
            picAreaDetail.Name = "picAreaDetail";
            picAreaDetail.Size = new Size(637, 439);
            picAreaDetail.TabIndex = 1;
            picAreaDetail.TabStop = false;
            // 
            // lblAreaDetailTitle
            // 
            lblAreaDetailTitle.Dock = DockStyle.Top;
            lblAreaDetailTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAreaDetailTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblAreaDetailTitle.Location = new Point(10, 10);
            lblAreaDetailTitle.Name = "lblAreaDetailTitle";
            lblAreaDetailTitle.Size = new Size(637, 46);
            lblAreaDetailTitle.TabIndex = 0;
            lblAreaDetailTitle.Text = "AREA DETAIL";
            lblAreaDetailTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cardHarnessConn
            // 
            cardHarnessConn.BackColor = Color.White;
            cardHarnessConn.Controls.Add(pnlHarnessImgContainer);
            cardHarnessConn.Controls.Add(lblHarnessConnTitle);
            cardHarnessConn.Dock = DockStyle.Fill;
            cardHarnessConn.Location = new Point(1339, 5);
            cardHarnessConn.Margin = new Padding(5);
            cardHarnessConn.Name = "cardHarnessConn";
            cardHarnessConn.Padding = new Padding(10);
            cardHarnessConn.Size = new Size(657, 505);
            cardHarnessConn.TabIndex = 2;
            // 
            // pnlHarnessImgContainer
            // 
            pnlHarnessImgContainer.Controls.Add(btnViewPinMap);
            pnlHarnessImgContainer.Controls.Add(picHarnessConn);
            pnlHarnessImgContainer.Dock = DockStyle.Fill;
            pnlHarnessImgContainer.Location = new Point(10, 56);
            pnlHarnessImgContainer.Name = "pnlHarnessImgContainer";
            pnlHarnessImgContainer.Size = new Size(637, 439);
            pnlHarnessImgContainer.TabIndex = 1;
            // 
            // btnViewPinMap
            // 
            btnViewPinMap.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnViewPinMap.BackColor = Color.FromArgb(37, 99, 235);
            btnViewPinMap.Cursor = Cursors.Hand;
            btnViewPinMap.FlatAppearance.BorderSize = 0;
            btnViewPinMap.FlatStyle = FlatStyle.Flat;
            btnViewPinMap.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnViewPinMap.ForeColor = Color.White;
            btnViewPinMap.Location = new Point(462, 388);
            btnViewPinMap.Name = "btnViewPinMap";
            btnViewPinMap.Size = new Size(160, 40);
            btnViewPinMap.TabIndex = 1;
            btnViewPinMap.Text = "🔍 View Pin Map";
            btnViewPinMap.UseVisualStyleBackColor = false;
            // 
            // picHarnessConn
            // 
            picHarnessConn.BackColor = Color.FromArgb(248, 250, 252);
            picHarnessConn.Dock = DockStyle.Fill;
            picHarnessConn.Location = new Point(0, 0);
            picHarnessConn.Name = "picHarnessConn";
            picHarnessConn.Size = new Size(637, 439);
            picHarnessConn.SizeMode = PictureBoxSizeMode.Zoom;
            picHarnessConn.TabIndex = 0;
            picHarnessConn.TabStop = false;
            // 
            // lblHarnessConnTitle
            // 
            lblHarnessConnTitle.Dock = DockStyle.Top;
            lblHarnessConnTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHarnessConnTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblHarnessConnTitle.Location = new Point(10, 10);
            lblHarnessConnTitle.Name = "lblHarnessConnTitle";
            lblHarnessConnTitle.Size = new Size(637, 46);
            lblHarnessConnTitle.TabIndex = 0;
            lblHarnessConnTitle.Text = "HARNESS CONNECTOR";
            lblHarnessConnTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cardTestStatus
            // 
            cardTestStatus.BackColor = Color.White;
            cardTestStatus.Controls.Add(tblStatusLabels);
            cardTestStatus.Controls.Add(lblStatusRunning);
            cardTestStatus.Controls.Add(lblTestStatusTitle);
            cardTestStatus.Dock = DockStyle.Fill;
            cardTestStatus.Location = new Point(2006, 5);
            cardTestStatus.Margin = new Padding(5);
            cardTestStatus.Name = "cardTestStatus";
            cardTestStatus.Padding = new Padding(10);
            cardTestStatus.Size = new Size(659, 505);
            cardTestStatus.TabIndex = 3;
            // 
            // tblStatusLabels
            // 
            tblStatusLabels.ColumnCount = 2;
            tblStatusLabels.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tblStatusLabels.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tblStatusLabels.Controls.Add(lblStartTimeKey, 0, 0);
            tblStatusLabels.Controls.Add(lblStartTimeVal, 1, 0);
            tblStatusLabels.Controls.Add(lblElapsedKey, 0, 1);
            tblStatusLabels.Controls.Add(lblElapsedTimeVal, 1, 1);
            tblStatusLabels.Controls.Add(lblEstimatedKey, 0, 2);
            tblStatusLabels.Controls.Add(lblEstimatedVal, 1, 2);
            tblStatusLabels.Controls.Add(lblEndTimeKey, 0, 3);
            tblStatusLabels.Controls.Add(lblEndTimeVal, 1, 3);
            tblStatusLabels.Dock = DockStyle.Fill;
            tblStatusLabels.Location = new Point(10, 127);
            tblStatusLabels.Name = "tblStatusLabels";
            tblStatusLabels.RowCount = 4;
            tblStatusLabels.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tblStatusLabels.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tblStatusLabels.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tblStatusLabels.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tblStatusLabels.Size = new Size(639, 368);
            tblStatusLabels.TabIndex = 2;
            // 
            // lblStartTimeKey
            // 
            lblStartTimeKey.AutoSize = true;
            lblStartTimeKey.Dock = DockStyle.Fill;
            lblStartTimeKey.Font = new Font("Segoe UI", 8.5F);
            lblStartTimeKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblStartTimeKey.Location = new Point(3, 0);
            lblStartTimeKey.Name = "lblStartTimeKey";
            lblStartTimeKey.Size = new Size(249, 92);
            lblStartTimeKey.TabIndex = 0;
            lblStartTimeKey.Text = "Start Time :";
            lblStartTimeKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblStartTimeVal
            // 
            lblStartTimeVal.AutoSize = true;
            lblStartTimeVal.Dock = DockStyle.Fill;
            lblStartTimeVal.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblStartTimeVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblStartTimeVal.Location = new Point(258, 0);
            lblStartTimeVal.Name = "lblStartTimeVal";
            lblStartTimeVal.Size = new Size(378, 92);
            lblStartTimeVal.TabIndex = 1;
            lblStartTimeVal.Text = "15 Mar 2026 10:30:20 AM";
            lblStartTimeVal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblElapsedKey
            // 
            lblElapsedKey.AutoSize = true;
            lblElapsedKey.Dock = DockStyle.Fill;
            lblElapsedKey.Font = new Font("Segoe UI", 8.5F);
            lblElapsedKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblElapsedKey.Location = new Point(3, 92);
            lblElapsedKey.Name = "lblElapsedKey";
            lblElapsedKey.Size = new Size(249, 92);
            lblElapsedKey.TabIndex = 2;
            lblElapsedKey.Text = "Elapsed Time :";
            lblElapsedKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblElapsedTimeVal
            // 
            lblElapsedTimeVal.AutoSize = true;
            lblElapsedTimeVal.Dock = DockStyle.Fill;
            lblElapsedTimeVal.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblElapsedTimeVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblElapsedTimeVal.Location = new Point(258, 92);
            lblElapsedTimeVal.Name = "lblElapsedTimeVal";
            lblElapsedTimeVal.Size = new Size(378, 92);
            lblElapsedTimeVal.TabIndex = 3;
            lblElapsedTimeVal.Text = "00:02:15";
            lblElapsedTimeVal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblEstimatedKey
            // 
            lblEstimatedKey.AutoSize = true;
            lblEstimatedKey.Dock = DockStyle.Fill;
            lblEstimatedKey.Font = new Font("Segoe UI", 8.5F);
            lblEstimatedKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblEstimatedKey.Location = new Point(3, 184);
            lblEstimatedKey.Name = "lblEstimatedKey";
            lblEstimatedKey.Size = new Size(249, 92);
            lblEstimatedKey.TabIndex = 4;
            lblEstimatedKey.Text = "Estimated Time :";
            lblEstimatedKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblEstimatedVal
            // 
            lblEstimatedVal.AutoSize = true;
            lblEstimatedVal.Dock = DockStyle.Fill;
            lblEstimatedVal.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblEstimatedVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblEstimatedVal.Location = new Point(258, 184);
            lblEstimatedVal.Name = "lblEstimatedVal";
            lblEstimatedVal.Size = new Size(378, 92);
            lblEstimatedVal.TabIndex = 5;
            lblEstimatedVal.Text = "00:09:01";
            lblEstimatedVal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblEndTimeKey
            // 
            lblEndTimeKey.AutoSize = true;
            lblEndTimeKey.Dock = DockStyle.Fill;
            lblEndTimeKey.Font = new Font("Segoe UI", 8.5F);
            lblEndTimeKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblEndTimeKey.Location = new Point(3, 276);
            lblEndTimeKey.Name = "lblEndTimeKey";
            lblEndTimeKey.Size = new Size(249, 92);
            lblEndTimeKey.TabIndex = 6;
            lblEndTimeKey.Text = "End Time (Est.) :";
            lblEndTimeKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblEndTimeVal
            // 
            lblEndTimeVal.AutoSize = true;
            lblEndTimeVal.Dock = DockStyle.Fill;
            lblEndTimeVal.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblEndTimeVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblEndTimeVal.Location = new Point(258, 276);
            lblEndTimeVal.Name = "lblEndTimeVal";
            lblEndTimeVal.Size = new Size(378, 92);
            lblEndTimeVal.TabIndex = 7;
            lblEndTimeVal.Text = "15 Mar 2026 10:39:21 AM";
            lblEndTimeVal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblStatusRunning
            // 
            lblStatusRunning.Dock = DockStyle.Top;
            lblStatusRunning.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblStatusRunning.ForeColor = Color.Gray;
            lblStatusRunning.Location = new Point(10, 56);
            lblStatusRunning.Name = "lblStatusRunning";
            lblStatusRunning.Size = new Size(639, 71);
            lblStatusRunning.TabIndex = 1;
            lblStatusRunning.Text = "Idle";
            lblStatusRunning.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTestStatusTitle
            // 
            lblTestStatusTitle.Dock = DockStyle.Top;
            lblTestStatusTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTestStatusTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblTestStatusTitle.Location = new Point(10, 10);
            lblTestStatusTitle.Name = "lblTestStatusTitle";
            lblTestStatusTitle.Size = new Size(639, 46);
            lblTestStatusTitle.TabIndex = 0;
            lblTestStatusTitle.Text = "TEST STATUS";
            lblTestStatusTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tblRow2
            // 
            tblRow2.ColumnCount = 3;
            tblRow2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblRow2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblRow2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblRow2.Controls.Add(cardLiveDetails, 0, 0);
            tblRow2.Controls.Add(cardTestSummary, 1, 0);
            tblRow2.Controls.Add(cardNotes, 2, 0);
            tblRow2.Dock = DockStyle.Fill;
            tblRow2.Location = new Point(3, 688);
            tblRow2.Name = "tblRow2";
            tblRow2.RowCount = 1;
            tblRow2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblRow2.Size = new Size(2670, 962);
            tblRow2.TabIndex = 2;
            // 
            // cardLiveDetails
            // 
            cardLiveDetails.BackColor = Color.White;
            cardLiveDetails.Controls.Add(gridLiveDetails);
            cardLiveDetails.Controls.Add(pnlLiveDetailsHeader);
            cardLiveDetails.Dock = DockStyle.Fill;
            cardLiveDetails.Location = new Point(5, 5);
            cardLiveDetails.Margin = new Padding(5);
            cardLiveDetails.Name = "cardLiveDetails";
            cardLiveDetails.Padding = new Padding(10);
            cardLiveDetails.Size = new Size(1325, 952);
            cardLiveDetails.TabIndex = 0;
            // 
            // gridLiveDetails
            // 
            gridLiveDetails.AllowUserToAddRows = false;
            gridLiveDetails.AllowUserToDeleteRows = false;
            gridLiveDetails.BackgroundColor = Color.White;
            gridLiveDetails.BorderStyle = BorderStyle.None;
            gridLiveDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridLiveDetails.Dock = DockStyle.Fill;
            gridLiveDetails.Location = new Point(10, 72);
            gridLiveDetails.Name = "gridLiveDetails";
            gridLiveDetails.ReadOnly = true;
            gridLiveDetails.RowHeadersVisible = false;
            gridLiveDetails.RowHeadersWidth = 82;
            gridLiveDetails.Size = new Size(1305, 870);
            gridLiveDetails.TabIndex = 1;
            // 
            // pnlLiveDetailsHeader
            // 
            pnlLiveDetailsHeader.Controls.Add(btnViewWiringDiagram);
            pnlLiveDetailsHeader.Controls.Add(lblLiveDetailsTitle);
            pnlLiveDetailsHeader.Dock = DockStyle.Top;
            pnlLiveDetailsHeader.Location = new Point(10, 10);
            pnlLiveDetailsHeader.Name = "pnlLiveDetailsHeader";
            pnlLiveDetailsHeader.Size = new Size(1305, 62);
            pnlLiveDetailsHeader.TabIndex = 0;
            // 
            // btnViewWiringDiagram
            // 
            btnViewWiringDiagram.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnViewWiringDiagram.BackColor = Color.FromArgb(37, 99, 235);
            btnViewWiringDiagram.Cursor = Cursors.Hand;
            btnViewWiringDiagram.FlatAppearance.BorderSize = 0;
            btnViewWiringDiagram.FlatStyle = FlatStyle.Flat;
            btnViewWiringDiagram.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnViewWiringDiagram.ForeColor = Color.White;
            btnViewWiringDiagram.Location = new Point(1110, 12);
            btnViewWiringDiagram.Name = "btnViewWiringDiagram";
            btnViewWiringDiagram.Size = new Size(185, 38);
            btnViewWiringDiagram.TabIndex = 1;
            btnViewWiringDiagram.Text = "🔌 Wiring Preview";
            btnViewWiringDiagram.UseVisualStyleBackColor = false;
            // 
            // lblLiveDetailsTitle
            // 
            lblLiveDetailsTitle.Dock = DockStyle.Fill;
            lblLiveDetailsTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblLiveDetailsTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblLiveDetailsTitle.Location = new Point(0, 0);
            lblLiveDetailsTitle.Name = "lblLiveDetailsTitle";
            lblLiveDetailsTitle.Size = new Size(1305, 62);
            lblLiveDetailsTitle.TabIndex = 0;
            lblLiveDetailsTitle.Text = "LIVE TEST DETAILS";
            lblLiveDetailsTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cardTestSummary
            // 
            cardTestSummary.BackColor = Color.White;
            cardTestSummary.Controls.Add(listBoxTestSummary);
            cardTestSummary.Controls.Add(lblTestSummaryTitle);
            cardTestSummary.Dock = DockStyle.Fill;
            cardTestSummary.Location = new Point(1340, 5);
            cardTestSummary.Margin = new Padding(5, 5, 5, 2);
            cardTestSummary.Name = "cardTestSummary";
            cardTestSummary.Padding = new Padding(10);
            cardTestSummary.Size = new Size(657, 955);
            cardTestSummary.TabIndex = 0;
            // 
            // listBoxTestSummary
            // 
            listBoxTestSummary.FormattingEnabled = true;
            listBoxTestSummary.HorizontalScrollbar = true;
            listBoxTestSummary.Location = new Point(12, 59);
            listBoxTestSummary.Name = "listBoxTestSummary";
            listBoxTestSummary.ScrollAlwaysVisible = true;
            listBoxTestSummary.Size = new Size(372, 444);
            listBoxTestSummary.TabIndex = 2;
            // 
            // lblTestSummaryTitle
            // 
            lblTestSummaryTitle.Dock = DockStyle.Top;
            lblTestSummaryTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTestSummaryTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblTestSummaryTitle.Location = new Point(10, 10);
            lblTestSummaryTitle.Name = "lblTestSummaryTitle";
            lblTestSummaryTitle.Size = new Size(637, 46);
            lblTestSummaryTitle.TabIndex = 0;
            lblTestSummaryTitle.Text = "TEST SUMMARY";
            lblTestSummaryTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cardNotes
            // 
            cardNotes.BackColor = Color.White;
            cardNotes.Controls.Add(txtNotes);
            cardNotes.Controls.Add(lblNotesTitle);
            cardNotes.Dock = DockStyle.Fill;
            cardNotes.Location = new Point(2007, 2);
            cardNotes.Margin = new Padding(5, 2, 5, 5);
            cardNotes.Name = "cardNotes";
            cardNotes.Padding = new Padding(10);
            cardNotes.Size = new Size(658, 955);
            cardNotes.TabIndex = 1;
            // 
            // lblNotesTitle
            // 
            lblNotesTitle.Dock = DockStyle.Top;
            lblNotesTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNotesTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblNotesTitle.Location = new Point(10, 10);
            lblNotesTitle.Name = "lblNotesTitle";
            lblNotesTitle.Size = new Size(638, 47);
            lblNotesTitle.TabIndex = 0;
            lblNotesTitle.Text = "NOTES";
            lblNotesTitle.TextAlign = ContentAlignment.MiddleLeft;
            //
            // txtNotes
            //
            txtNotes.BorderStyle = BorderStyle.FixedSingle;
            txtNotes.Dock = DockStyle.Fill;
            txtNotes.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNotes.ForeColor = Color.FromArgb(30, 41, 59);
            txtNotes.Location = new Point(10, 57);
            txtNotes.MaxLength = 2000;
            txtNotes.Multiline = true;
            txtNotes.Name = "txtNotes";
            txtNotes.ScrollBars = ScrollBars.Vertical;
            txtNotes.Size = new Size(638, 888);
            txtNotes.TabIndex = 1;
            // 
            // TestExecution
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(244, 245, 247);
            Controls.Add(tblMain);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "TestExecution";
            Size = new Size(2676, 1653);
            tblMain.ResumeLayout(false);
            tblMain.PerformLayout();
            pnlTopBar.ResumeLayout(false);
            pnlTopBar.PerformLayout();
            tblTopWrapper.ResumeLayout(false);
            tblTopWrapper.PerformLayout();
            tblFilterRow.ResumeLayout(false);
            pnlFilterCard.ResumeLayout(false);
            tblFilters.ResumeLayout(false);
            tblFilters.PerformLayout();
            tblActionButtons.ResumeLayout(false);
            tblRow1.ResumeLayout(false);
            cardMachineOverview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picMachineOverview).EndInit();
            cardAreaDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picAreaDetail).EndInit();
            cardHarnessConn.ResumeLayout(false);
            pnlHarnessImgContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picHarnessConn).EndInit();
            cardTestStatus.ResumeLayout(false);
            tblStatusLabels.ResumeLayout(false);
            tblStatusLabels.PerformLayout();
            tblRow2.ResumeLayout(false);
            cardLiveDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridLiveDetails).EndInit();
            pnlLiveDetailsHeader.ResumeLayout(false);
            cardTestSummary.ResumeLayout(false);
            cardNotes.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblMain;
        private System.Windows.Forms.Panel pnlTopBar;
        private System.Windows.Forms.TableLayoutPanel tblTopWrapper;
        private System.Windows.Forms.Label lblPageHeader;
        private System.Windows.Forms.TableLayoutPanel tblFilterRow;
        private System.Windows.Forms.Panel pnlFilterCard;
        private System.Windows.Forms.TableLayoutPanel tblFilters;
        private System.Windows.Forms.Label lblFilterModel;
        private System.Windows.Forms.ComboBox cmbProductModel;
        private System.Windows.Forms.Label lblFilterType;
        private System.Windows.Forms.ComboBox cmbProductType;
        private System.Windows.Forms.Label lblFilterArea;
        private System.Windows.Forms.ComboBox cmbArea;
        private System.Windows.Forms.Label lblFilterPart;
        private System.Windows.Forms.ComboBox cmbPartNumber;
        private System.Windows.Forms.Label lblFilterEcn;
        private System.Windows.Forms.ComboBox cmbEcnNo;
        private System.Windows.Forms.Button btnLoadProgram;
        private System.Windows.Forms.TableLayoutPanel tblActionButtons;
        private System.Windows.Forms.Button btnStartTest;
        private System.Windows.Forms.Button btnStopTest;
        private System.Windows.Forms.Button btnSaveReport;
        private System.Windows.Forms.TableLayoutPanel tblRow1;
        private System.Windows.Forms.Panel cardMachineOverview;
        private System.Windows.Forms.PictureBox picMachineOverview;
        private System.Windows.Forms.Label lblMachineOverviewTitle;
        private System.Windows.Forms.Panel cardAreaDetail;
        private System.Windows.Forms.PictureBox picAreaDetail;
        private System.Windows.Forms.Label lblAreaDetailTitle;
        private System.Windows.Forms.Panel cardHarnessConn;
        private System.Windows.Forms.Panel pnlHarnessImgContainer;
        private System.Windows.Forms.Button btnViewPinMap;
        private System.Windows.Forms.PictureBox picHarnessConn;
        private System.Windows.Forms.Label lblHarnessConnTitle;
        private System.Windows.Forms.Panel cardTestStatus;
        private System.Windows.Forms.TableLayoutPanel tblStatusLabels;
        private System.Windows.Forms.Label lblStartTimeKey;
        private System.Windows.Forms.Label lblStartTimeVal;
        private System.Windows.Forms.Label lblElapsedKey;
        private System.Windows.Forms.Label lblElapsedTimeVal;
        private System.Windows.Forms.Label lblEstimatedKey;
        private System.Windows.Forms.Label lblEstimatedVal;
        private System.Windows.Forms.Label lblEndTimeKey;
        private System.Windows.Forms.Label lblEndTimeVal;
        private System.Windows.Forms.Label lblStatusRunning;
        private System.Windows.Forms.Label lblTestStatusTitle;
        private System.Windows.Forms.TableLayoutPanel tblRow2;
        private System.Windows.Forms.Panel cardLiveDetails;
        private System.Windows.Forms.Panel pnlLiveDetailsHeader;
        private System.Windows.Forms.Button btnViewWiringDiagram;
        private System.Windows.Forms.DataGridView gridLiveDetails;
        private System.Windows.Forms.Label lblLiveDetailsTitle;
        private System.Windows.Forms.Panel cardTestSummary;
        private System.Windows.Forms.Label lblTestSummaryTitle;
        private System.Windows.Forms.Panel cardNotes;
        private System.Windows.Forms.Label lblNotesTitle;
        private System.Windows.Forms.TextBox txtNotes;
        private ListBox listBoxTestSummary;
    }
}