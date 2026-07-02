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
            lblElapsedVal = new Label();
            lblEstimatedKey = new Label();
            lblEstimatedVal = new Label();
            lblEndTimeKey = new Label();
            lblEndTimeVal = new Label();
            lblStatusRunning = new Label();
            lblTestStatusTitle = new Label();
            tblRow2 = new TableLayoutPanel();
            cardProgress = new Panel();
            tblProgressCard = new TableLayoutPanel();
            pnlSequenceSection = new Panel();
            tblSteps = new TableLayoutPanel();
            lblStep1 = new Label();
            lblStep2 = new Label();
            lblStep3 = new Label();
            lblStep4 = new Label();
            lblStep5 = new Label();
            lblSequenceTitle = new Label();
            pnlOverallStatsSection = new Panel();
            lblProgressStats = new Label();
            prgOverall = new ProgressBar();
            lblPercent = new Label();
            lblOverallProgressTitle = new Label();
            cardCurrentPoint = new Panel();
            tblCurrentPointDetails = new TableLayoutPanel();
            lblExpectedKey = new Label();
            lblExpectedVal = new Label();
            lblMeasuredKey = new Label();
            lblMeasuredVal = new Label();
            lblResultKey = new Label();
            lblResultVal = new Label();
            lblPointsRoute = new Label();
            lblCurrentPointTitle = new Label();
            tblRow3 = new TableLayoutPanel();
            cardLiveDetails = new Panel();
            gridLiveDetails = new DataGridView();
            lblLiveDetailsTitle = new Label();
            cardWiringDiagram = new Panel();
            pnlWiringPlaceholder = new Panel();
            lblWiringMockText = new Label();
            lblWiringDiagramTitle = new Label();
            tblSummaryNotes = new TableLayoutPanel();
            cardTestSummary = new Panel();
            lblSummaryText = new Label();
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

            pnlSequenceSection.SuspendLayout();
            tblSteps.SuspendLayout();
            pnlOverallStatsSection.SuspendLayout();
            cardCurrentPoint.SuspendLayout();
            tblCurrentPointDetails.SuspendLayout();
            tblRow3.SuspendLayout();
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
            cardProgress.SuspendLayout();
            tblProgressCard.SuspendLayout();
            pnlSequenceSection.SuspendLayout();
            tblSteps.SuspendLayout();
            pnlOverallStatsSection.SuspendLayout();
            cardCurrentPoint.SuspendLayout();
            tblCurrentPointDetails.SuspendLayout();
            tblRow3.SuspendLayout();
            cardLiveDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridLiveDetails).BeginInit();
            cardWiringDiagram.SuspendLayout();
            pnlWiringPlaceholder.SuspendLayout();
            tblSummaryNotes.SuspendLayout();
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
            tblMain.Controls.Add(tblRow3, 0, 3);
            tblMain.AutoScroll = true;
            tblMain.Dock = DockStyle.Fill;
            tblMain.Location = new Point(0, 0);
            tblMain.MinimumSize = new Size(1500, 0);
            tblMain.Name = "tblMain";
            tblMain.RowCount = 4;
            tblMain.RowStyles.Add(new RowStyle());
            tblMain.RowStyles.Add(new RowStyle());
            tblMain.RowStyles.Add(new RowStyle());
            tblMain.RowStyles.Add(new RowStyle());
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
            pnlTopBar.Size = new Size(2666, 155);
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
            tblTopWrapper.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tblTopWrapper.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tblTopWrapper.Size = new Size(2666, 155);
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
            lblPageHeader.Size = new Size(2656, 32);
            lblPageHeader.TabIndex = 0;
            lblPageHeader.Text = "🏠  TEST EXECUTION";
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
            tblFilterRow.Location = new Point(0, 40);
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
            pnlFilterCard.BorderStyle = BorderStyle.FixedSingle;
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
            tblFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666F));
            tblFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666F));
            tblFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666F));
            tblFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666F));
            tblFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666F));
            tblFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666F));
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
            tblFilters.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tblFilters.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblFilters.Size = new Size(1987, 113);
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
            cmbProductModel.Location = new Point(11, 46);
            cmbProductModel.Name = "cmbProductModel";
            cmbProductModel.Size = new Size(322, 36);
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
            cmbProductType.Location = new Point(339, 46);
            cmbProductType.Name = "cmbProductType";
            cmbProductType.Size = new Size(322, 36);
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
            cmbArea.Location = new Point(667, 46);
            cmbArea.Name = "cmbArea";
            cmbArea.Size = new Size(322, 36);
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
            cmbPartNumber.Location = new Point(995, 46);
            cmbPartNumber.Name = "cmbPartNumber";
            cmbPartNumber.Size = new Size(322, 36);
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
            cmbEcnNo.Location = new Point(1323, 46);
            cmbEcnNo.Name = "cmbEcnNo";
            cmbEcnNo.Size = new Size(322, 36);
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
            btnLoadProgram.Location = new Point(1658, 25);
            btnLoadProgram.Margin = new Padding(10, 3, 3, 3);
            btnLoadProgram.Name = "btnLoadProgram";
            tblFilters.SetRowSpan(btnLoadProgram, 2);
            btnLoadProgram.Size = new Size(318, 55);
            btnLoadProgram.TabIndex = 10;
            btnLoadProgram.Text = "📁 Load Test Program";
            btnLoadProgram.UseVisualStyleBackColor = false;
            // 
            // tblActionButtons
            // 
            tblActionButtons.ColumnCount = 2;
            tblActionButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblActionButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblActionButtons.Controls.Add(btnStartTest, 0, 0);
            tblActionButtons.Controls.Add(btnStopTest, 1, 0);
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
            btnStartTest.Size = new Size(327, 115);
            btnStartTest.TabIndex = 0;
            btnStartTest.Text = "▶ START TEST";
            btnStartTest.UseVisualStyleBackColor = false;
            // 
            // btnStopTest
            // 
            btnStopTest.BackColor = Color.FromArgb(211, 47, 47);
            btnStopTest.Dock = DockStyle.Fill;
            btnStopTest.FlatAppearance.BorderSize = 0;
            btnStopTest.FlatStyle = FlatStyle.Flat;
            btnStopTest.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnStopTest.ForeColor = Color.White;
            btnStopTest.Location = new Point(339, 0);
            btnStopTest.Margin = new Padding(6, 0, 0, 0);
            btnStopTest.Name = "btnStopTest";
            btnStopTest.Size = new Size(328, 115);
            btnStopTest.TabIndex = 1;
            btnStopTest.Text = "⏹ STOP TEST";
            btnStopTest.UseVisualStyleBackColor = false;
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
            tblRow1.Location = new Point(3, 168);
            tblRow1.MinimumSize = new Size(0, 440);
            tblRow1.Name = "tblRow1";
            tblRow1.RowCount = 1;
            tblRow1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblRow1.Size = new Size(2670, 489);
            tblRow1.TabIndex = 1;
            // 
            // cardMachineOverview
            // 
            cardMachineOverview.BackColor = Color.White;
            cardMachineOverview.BorderStyle = BorderStyle.FixedSingle;
            cardMachineOverview.Controls.Add(picMachineOverview);
            cardMachineOverview.Controls.Add(lblMachineOverviewTitle);
            cardMachineOverview.Dock = DockStyle.Fill;
            cardMachineOverview.Location = new Point(5, 5);
            cardMachineOverview.Margin = new Padding(5);
            cardMachineOverview.Name = "cardMachineOverview";
            cardMachineOverview.Padding = new Padding(10);
            cardMachineOverview.Size = new Size(657, 479);
            cardMachineOverview.TabIndex = 0;
            // 
            // picMachineOverview
            // 
            picMachineOverview.BackColor = Color.LightGray;
            picMachineOverview.Dock = DockStyle.Fill;
            picMachineOverview.Location = new Point(10, 56);
            picMachineOverview.Name = "picMachineOverview";
            picMachineOverview.Size = new Size(637, 413);
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
            cardAreaDetail.BorderStyle = BorderStyle.FixedSingle;
            cardAreaDetail.Controls.Add(picAreaDetail);
            cardAreaDetail.Controls.Add(lblAreaDetailTitle);
            cardAreaDetail.Dock = DockStyle.Fill;
            cardAreaDetail.Location = new Point(672, 5);
            cardAreaDetail.Margin = new Padding(5);
            cardAreaDetail.Name = "cardAreaDetail";
            cardAreaDetail.Padding = new Padding(10);
            cardAreaDetail.Size = new Size(657, 479);
            cardAreaDetail.TabIndex = 1;
            // 
            // cardHarnessConn
            // 
            cardHarnessConn.BackColor = Color.White;
            cardHarnessConn.BorderStyle = BorderStyle.FixedSingle;
            cardHarnessConn.Controls.Add(pnlHarnessImgContainer);
            cardHarnessConn.Controls.Add(lblHarnessConnTitle);
            cardHarnessConn.Dock = DockStyle.Fill;
            cardHarnessConn.Location = new Point(1339, 5);
            cardHarnessConn.Margin = new Padding(5);
            cardHarnessConn.Name = "cardHarnessConn";
            cardHarnessConn.Padding = new Padding(10);
            cardHarnessConn.Size = new Size(657, 479);
            cardHarnessConn.TabIndex = 2;
            // 
            // pnlHarnessImgContainer
            // 
            pnlHarnessImgContainer.Controls.Add(btnViewPinMap);
            pnlHarnessImgContainer.Controls.Add(picHarnessConn);
            pnlHarnessImgContainer.Dock = DockStyle.Fill;
            pnlHarnessImgContainer.Location = new Point(10, 56);
            pnlHarnessImgContainer.Name = "pnlHarnessImgContainer";
            pnlHarnessImgContainer.Size = new Size(635, 411);
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
            btnViewPinMap.Location = new Point(460, 360);
            btnViewPinMap.Name = "btnViewPinMap";
            btnViewPinMap.Size = new Size(160, 40);
            btnViewPinMap.TabIndex = 1;
            btnViewPinMap.Text = "🔍 View Pin Map";
            btnViewPinMap.UseVisualStyleBackColor = false;
            // 
            // picHarnessConn
            // 
            picHarnessConn.BackColor = Color.FromArgb(248, 250, 252);
            picHarnessConn.BorderStyle = BorderStyle.FixedSingle;
            picHarnessConn.Dock = DockStyle.Fill;
            picHarnessConn.Location = new Point(0, 0);
            picHarnessConn.Name = "picHarnessConn";
            picHarnessConn.Size = new Size(635, 411);
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
            lblHarnessConnTitle.Size = new Size(635, 46);
            lblHarnessConnTitle.TabIndex = 0;
            lblHarnessConnTitle.Text = "HARNESS CONNECTOR";
            lblHarnessConnTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cardTestStatus
            // 
            cardTestStatus.BackColor = Color.White;
            cardTestStatus.BorderStyle = BorderStyle.FixedSingle;
            cardTestStatus.Controls.Add(tblStatusLabels);
            cardTestStatus.Controls.Add(lblStatusRunning);
            cardTestStatus.Controls.Add(lblTestStatusTitle);
            cardTestStatus.Dock = DockStyle.Fill;
            cardTestStatus.Location = new Point(2006, 5);
            cardTestStatus.Margin = new Padding(5);
            cardTestStatus.Name = "cardTestStatus";
            cardTestStatus.Padding = new Padding(10);
            cardTestStatus.Size = new Size(659, 479);
            cardTestStatus.TabIndex = 3;
            // 
            // tblStatusLabels
            // 
            tblStatusLabels.ColumnCount = 2;
            tblStatusLabels.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44.5F));
            tblStatusLabels.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55.5F));
            tblStatusLabels.Controls.Add(lblStartTimeKey, 0, 0);
            tblStatusLabels.Controls.Add(lblStartTimeVal, 1, 0);
            tblStatusLabels.Controls.Add(lblElapsedKey, 0, 1);
            tblStatusLabels.Controls.Add(lblElapsedVal, 1, 1);
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
            tblStatusLabels.Size = new Size(639, 342);
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
            lblStartTimeKey.Size = new Size(275, 85);
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
            lblStartTimeVal.Location = new Point(284, 0);
            lblStartTimeVal.Name = "lblStartTimeVal";
            lblStartTimeVal.Size = new Size(352, 85);
            lblStartTimeVal.TabIndex = 1;
            lblStartTimeVal.Text = "15 Mar 2026 10:30:20 AM";
            lblStartTimeVal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblElapsedKey
            // 
            lblElapsedKey.AutoSize = true;
            lblElapsedKey.Dock = DockStyle.Fill;
            lblElapsedKey.Font = new Font("Segoe UI", 8.5F);
            lblElapsedKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblElapsedKey.Location = new Point(3, 85);
            lblElapsedKey.Name = "lblElapsedKey";
            lblElapsedKey.Size = new Size(275, 85);
            lblElapsedKey.TabIndex = 2;
            lblElapsedKey.Text = "Elapsed Time :";
            lblElapsedKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblElapsedVal
            // 
            lblElapsedVal.AutoSize = true;
            lblElapsedVal.Dock = DockStyle.Fill;
            lblElapsedVal.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblElapsedVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblElapsedVal.Location = new Point(284, 85);
            lblElapsedVal.Name = "lblElapsedVal";
            lblElapsedVal.Size = new Size(352, 85);
            lblElapsedVal.TabIndex = 3;
            lblElapsedVal.Text = "00:02:15";
            lblElapsedVal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblEstimatedKey
            // 
            lblEstimatedKey.AutoSize = true;
            lblEstimatedKey.Dock = DockStyle.Fill;
            lblEstimatedKey.Font = new Font("Segoe UI", 8.5F);
            lblEstimatedKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblEstimatedKey.Location = new Point(3, 170);
            lblEstimatedKey.Name = "lblEstimatedKey";
            lblEstimatedKey.Size = new Size(275, 85);
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
            lblEstimatedVal.Location = new Point(284, 170);
            lblEstimatedVal.Name = "lblEstimatedVal";
            lblEstimatedVal.Size = new Size(352, 85);
            lblEstimatedVal.TabIndex = 5;
            lblEstimatedVal.Text = "00:09:01";
            lblEstimatedVal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblEndTimeKey
            // 
            lblEndTimeKey.AutoSize = true;
            lblEndTimeKey.Dock = DockStyle.Fill;
            lblEndTimeKey.Font = new Font("Segoe UI", 8.5F);
            lblEndTimeKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblEndTimeKey.Location = new Point(3, 255);
            lblEndTimeKey.Name = "lblEndTimeKey";
            lblEndTimeKey.Size = new Size(275, 87);
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
            lblEndTimeVal.Location = new Point(284, 255);
            lblEndTimeVal.Name = "lblEndTimeVal";
            lblEndTimeVal.Size = new Size(352, 87);
            lblEndTimeVal.TabIndex = 7;
            lblEndTimeVal.Text = "15 Mar 2026 10:39:21 AM";
            lblEndTimeVal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblStatusRunning
            // 
            lblStatusRunning.Dock = DockStyle.Top;
            lblStatusRunning.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblStatusRunning.ForeColor = Color.FromArgb(40, 167, 69);
            lblStatusRunning.Location = new Point(10, 56);
            lblStatusRunning.Name = "lblStatusRunning";
            lblStatusRunning.Size = new Size(639, 71);
            lblStatusRunning.TabIndex = 1;
            lblStatusRunning.Text = "RUNNING";
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
            tblRow2.ColumnCount = 2;
            tblRow2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tblRow2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tblRow2.Controls.Add(cardProgress, 0, 0);
            tblRow2.Controls.Add(cardCurrentPoint, 1, 0);
            tblRow2.Dock = DockStyle.Fill;
            tblRow2.Location = new Point(3, 663);
            tblRow2.Name = "tblRow2";
            tblRow2.RowCount = 1;
            tblRow2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblRow2.Size = new Size(2670, 324);
            tblRow2.TabIndex = 2;
            // 
            // cardProgress
            // 
            cardProgress.BackColor = Color.White;
            cardProgress.BorderStyle = BorderStyle.FixedSingle;
            cardProgress.Controls.Add(tblProgressCard);
            cardProgress.Dock = DockStyle.Fill;
            cardProgress.Location = new Point(5, 5);
            cardProgress.Margin = new Padding(5);
            cardProgress.Name = "cardProgress";
            cardProgress.Padding = new Padding(10);
            cardProgress.Size = new Size(1859, 314);
            cardProgress.TabIndex = 0;
            // 
            // tblProgressCard
            // 
            tblProgressCard.ColumnCount = 2;
            tblProgressCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 72F));
            tblProgressCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
            tblProgressCard.Controls.Add(pnlSequenceSection, 0, 0);
            tblProgressCard.Controls.Add(pnlOverallStatsSection, 1, 0);
            tblProgressCard.Dock = DockStyle.Fill;
            tblProgressCard.Location = new Point(10, 10);
            tblProgressCard.Name = "tblProgressCard";
            tblProgressCard.RowCount = 1;
            tblProgressCard.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblProgressCard.Size = new Size(1839, 294);
            tblProgressCard.TabIndex = 0;
            // 
            // pnlSequenceSection
            // 
            pnlSequenceSection.Controls.Add(tblSteps);
            pnlSequenceSection.Controls.Add(lblSequenceTitle);
            pnlSequenceSection.Dock = DockStyle.Fill;
            pnlSequenceSection.Location = new Point(0, 0);
            pnlSequenceSection.Margin = new Padding(0, 0, 10, 0);
            pnlSequenceSection.Name = "pnlSequenceSection";
            pnlSequenceSection.Size = new Size(1314, 294);
            pnlSequenceSection.TabIndex = 0;
            // 
            // tblSteps
            // 
            tblSteps.ColumnCount = 5;
            tblSteps.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tblSteps.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tblSteps.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tblSteps.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tblSteps.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tblSteps.Controls.Add(lblStep1, 0, 0);
            tblSteps.Controls.Add(lblStep2, 1, 0);
            tblSteps.Controls.Add(lblStep3, 2, 0);
            tblSteps.Controls.Add(lblStep4, 3, 0);
            tblSteps.Controls.Add(lblStep5, 4, 0);
            tblSteps.Dock = DockStyle.Fill;
            tblSteps.Location = new Point(0, 43);
            tblSteps.Name = "tblSteps";
            tblSteps.RowCount = 1;
            tblSteps.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblSteps.Size = new Size(1314, 251);
            tblSteps.TabIndex = 1;
            // 
            // lblStep1
            // 
            lblStep1.BackColor = Color.FromArgb(209, 231, 221);
            lblStep1.Dock = DockStyle.Fill;
            lblStep1.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblStep1.ForeColor = Color.FromArgb(15, 81, 50);
            lblStep1.Location = new Point(3, 3);
            lblStep1.Margin = new Padding(3);
            lblStep1.Name = "lblStep1";
            lblStep1.Size = new Size(256, 245);
            lblStep1.TabIndex = 0;
            lblStep1.Text = "✔ Continuity Test\r\n\r\n(00:01:42)";
            lblStep1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblStep2
            // 
            lblStep2.BackColor = Color.FromArgb(209, 231, 221);
            lblStep2.Dock = DockStyle.Fill;
            lblStep2.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblStep2.ForeColor = Color.FromArgb(15, 81, 50);
            lblStep2.Location = new Point(265, 3);
            lblStep2.Margin = new Padding(3);
            lblStep2.Name = "lblStep2";
            lblStep2.Size = new Size(256, 245);
            lblStep2.TabIndex = 1;
            lblStep2.Text = "✔ Short Circuit\r\n\r\n(00:01:35)";
            lblStep2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblStep3
            // 
            lblStep3.BackColor = Color.FromArgb(209, 231, 221);
            lblStep3.Dock = DockStyle.Fill;
            lblStep3.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblStep3.ForeColor = Color.FromArgb(15, 81, 50);
            lblStep3.Location = new Point(527, 3);
            lblStep3.Margin = new Padding(3);
            lblStep3.Name = "lblStep3";
            lblStep3.Size = new Size(256, 245);
            lblStep3.TabIndex = 2;
            lblStep3.Text = "✔ Insulation Test\r\n\r\n(00:01:28)";
            lblStep3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblStep4
            // 
            lblStep4.BackColor = Color.FromArgb(255, 243, 205);
            lblStep4.Dock = DockStyle.Fill;
            lblStep4.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblStep4.ForeColor = Color.FromArgb(102, 77, 3);
            lblStep4.Location = new Point(789, 3);
            lblStep4.Margin = new Padding(3);
            lblStep4.Name = "lblStep4";
            lblStep4.Size = new Size(256, 245);
            lblStep4.TabIndex = 3;
            lblStep4.Text = "⏳ Functional Test\r\n\r\nIn Progress\r\n\r\n(00:02:15)";
            lblStep4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblStep5
            // 
            lblStep5.BackColor = Color.FromArgb(226, 232, 240);
            lblStep5.Dock = DockStyle.Fill;
            lblStep5.Font = new Font("Segoe UI", 8.5F);
            lblStep5.ForeColor = Color.FromArgb(71, 85, 105);
            lblStep5.Location = new Point(1051, 3);
            lblStep5.Margin = new Padding(3);
            lblStep5.Name = "lblStep5";
            lblStep5.Size = new Size(260, 245);
            lblStep5.TabIndex = 4;
            lblStep5.Text = "Final Verification\r\n(Pending)";
            lblStep5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSequenceTitle
            // 
            lblSequenceTitle.Dock = DockStyle.Top;
            lblSequenceTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSequenceTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblSequenceTitle.Location = new Point(0, 0);
            lblSequenceTitle.Name = "lblSequenceTitle";
            lblSequenceTitle.Size = new Size(1314, 43);
            lblSequenceTitle.TabIndex = 0;
            lblSequenceTitle.Text = "TEST SEQUENCE & PROGRESS";
            lblSequenceTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlOverallStatsSection
            // 
            pnlOverallStatsSection.Controls.Add(lblProgressStats);
            pnlOverallStatsSection.Controls.Add(prgOverall);
            pnlOverallStatsSection.Controls.Add(lblPercent);
            pnlOverallStatsSection.Controls.Add(lblOverallProgressTitle);
            pnlOverallStatsSection.Dock = DockStyle.Fill;
            pnlOverallStatsSection.Location = new Point(1324, 0);
            pnlOverallStatsSection.Margin = new Padding(0);
            pnlOverallStatsSection.Name = "pnlOverallStatsSection";
            pnlOverallStatsSection.Size = new Size(515, 294);
            pnlOverallStatsSection.TabIndex = 1;
            // 
            // lblProgressStats
            // 
            lblProgressStats.Dock = DockStyle.Fill;
            lblProgressStats.Font = new Font("Segoe UI", 9F);
            lblProgressStats.ForeColor = Color.FromArgb(71, 85, 105);
            lblProgressStats.Location = new Point(0, 155);
            lblProgressStats.Name = "lblProgressStats";
            lblProgressStats.Size = new Size(515, 139);
            lblProgressStats.TabIndex = 3;
            lblProgressStats.Text = "Total Steps: 5 | Completed: 2 | Remaining: 3";
            lblProgressStats.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // prgOverall
            // 
            prgOverall.Dock = DockStyle.Top;
            prgOverall.Location = new Point(0, 112);
            prgOverall.Name = "prgOverall";
            prgOverall.Size = new Size(515, 43);
            prgOverall.TabIndex = 2;
            prgOverall.Value = 24;
            // 
            // lblPercent
            // 
            lblPercent.Dock = DockStyle.Top;
            lblPercent.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblPercent.ForeColor = Color.FromArgb(13, 110, 253);
            lblPercent.Location = new Point(0, 43);
            lblPercent.Name = "lblPercent";
            lblPercent.Size = new Size(515, 69);
            lblPercent.TabIndex = 1;
            lblPercent.Text = "24%";
            lblPercent.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblOverallProgressTitle
            // 
            lblOverallProgressTitle.Dock = DockStyle.Top;
            lblOverallProgressTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblOverallProgressTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblOverallProgressTitle.Location = new Point(0, 0);
            lblOverallProgressTitle.Name = "lblOverallProgressTitle";
            lblOverallProgressTitle.Size = new Size(515, 43);
            lblOverallProgressTitle.TabIndex = 0;
            lblOverallProgressTitle.Text = "OVERALL PROGRESS";
            lblOverallProgressTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cardCurrentPoint
            // 
            cardCurrentPoint.BackColor = Color.White;

            cardCurrentPoint.BorderStyle = BorderStyle.FixedSingle;
            cardCurrentPoint.Controls.Add(tblCurrentPointDetails);
            cardCurrentPoint.Controls.Add(lblPointsRoute);
            cardCurrentPoint.Controls.Add(lblCurrentPointTitle);
            cardCurrentPoint.Dock = DockStyle.Fill;
            cardCurrentPoint.Location = new Point(1874, 5);
            cardCurrentPoint.Margin = new Padding(5);
            cardCurrentPoint.Name = "cardCurrentPoint";
            cardCurrentPoint.Padding = new Padding(10);
            cardCurrentPoint.Size = new Size(791, 314);
            cardCurrentPoint.TabIndex = 1;
            // 
            // tblCurrentPointDetails
            // 
            tblCurrentPointDetails.ColumnCount = 2;
            tblCurrentPointDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tblCurrentPointDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tblCurrentPointDetails.Controls.Add(lblExpectedKey, 0, 0);
            tblCurrentPointDetails.Controls.Add(lblExpectedVal, 1, 0);
            tblCurrentPointDetails.Controls.Add(lblMeasuredKey, 0, 1);
            tblCurrentPointDetails.Controls.Add(lblMeasuredVal, 1, 1);
            tblCurrentPointDetails.Controls.Add(lblResultKey, 0, 2);
            tblCurrentPointDetails.Controls.Add(lblResultVal, 1, 2);
            tblCurrentPointDetails.Dock = DockStyle.Fill;
            tblCurrentPointDetails.Location = new Point(10, 100);
            tblCurrentPointDetails.Name = "tblCurrentPointDetails";
            tblCurrentPointDetails.RowCount = 3;
            tblCurrentPointDetails.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            tblCurrentPointDetails.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            tblCurrentPointDetails.RowStyles.Add(new RowStyle(SizeType.Percent, 33.34F));
            tblCurrentPointDetails.Size = new Size(771, 204);
            tblCurrentPointDetails.TabIndex = 2;
            // 
            // lblExpectedKey
            // 
            lblExpectedKey.AutoSize = true;
            lblExpectedKey.Dock = DockStyle.Fill;
            lblExpectedKey.Font = new Font("Segoe UI", 9F);
            lblExpectedKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblExpectedKey.Location = new Point(3, 0);
            lblExpectedKey.Name = "lblExpectedKey";
            lblExpectedKey.Size = new Size(418, 67);
            lblExpectedKey.TabIndex = 0;
            lblExpectedKey.Text = "Expected Resistance:";
            lblExpectedKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblExpectedVal
            // 
            lblExpectedVal.AutoSize = true;
            lblExpectedVal.Dock = DockStyle.Fill;
            lblExpectedVal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblExpectedVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblExpectedVal.Location = new Point(427, 0);
            lblExpectedVal.Name = "lblExpectedVal";
            lblExpectedVal.Size = new Size(341, 67);
            lblExpectedVal.TabIndex = 1;
            lblExpectedVal.Text = "< 1.00 Ω";
            lblExpectedVal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblMeasuredKey
            // 
            lblMeasuredKey.AutoSize = true;
            lblMeasuredKey.Dock = DockStyle.Fill;
            lblMeasuredKey.Font = new Font("Segoe UI", 9F);
            lblMeasuredKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblMeasuredKey.Location = new Point(3, 67);
            lblMeasuredKey.Name = "lblMeasuredKey";
            lblMeasuredKey.Size = new Size(418, 67);
            lblMeasuredKey.TabIndex = 2;
            lblMeasuredKey.Text = "Measured Resistance:";
            lblMeasuredKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblMeasuredVal
            // 
            lblMeasuredVal.AutoSize = true;
            lblMeasuredVal.Dock = DockStyle.Fill;
            lblMeasuredVal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMeasuredVal.ForeColor = Color.FromArgb(40, 167, 69);
            lblMeasuredVal.Location = new Point(427, 67);
            lblMeasuredVal.Name = "lblMeasuredVal";
            lblMeasuredVal.Size = new Size(341, 67);
            lblMeasuredVal.TabIndex = 3;
            lblMeasuredVal.Text = "0.23 Ω";
            lblMeasuredVal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblResultKey
            // 
            lblResultKey.AutoSize = true;
            lblResultKey.Dock = DockStyle.Fill;
            lblResultKey.Font = new Font("Segoe UI", 9F);
            lblResultKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblResultKey.Location = new Point(3, 134);
            lblResultKey.Name = "lblResultKey";
            lblResultKey.Size = new Size(418, 70);
            lblResultKey.TabIndex = 4;
            lblResultKey.Text = "Result:";
            lblResultKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblResultVal
            // 
            lblResultVal.AutoSize = true;
            lblResultVal.Dock = DockStyle.Fill;
            lblResultVal.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblResultVal.ForeColor = Color.FromArgb(40, 167, 69);
            lblResultVal.Location = new Point(427, 134);
            lblResultVal.Name = "lblResultVal";
            lblResultVal.Size = new Size(341, 70);
            lblResultVal.TabIndex = 5;
            lblResultVal.Text = "PASS";
            lblResultVal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblPointsRoute
            // 
            lblPointsRoute.Dock = DockStyle.Top;
            lblPointsRoute.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblPointsRoute.ForeColor = Color.FromArgb(30, 41, 59);
            lblPointsRoute.Location = new Point(10, 53);
            lblPointsRoute.Name = "lblPointsRoute";
            lblPointsRoute.Size = new Size(771, 47);
            lblPointsRoute.TabIndex = 1;
            lblPointsRoute.Text = "Dari \"J1-23\"  ➔  Ke \"J3-16\"";
            lblPointsRoute.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblCurrentPointTitle
            // 
            lblCurrentPointTitle.Dock = DockStyle.Top;
            lblCurrentPointTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCurrentPointTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblCurrentPointTitle.Location = new Point(10, 10);
            lblCurrentPointTitle.Name = "lblCurrentPointTitle";
            lblCurrentPointTitle.Size = new Size(771, 43);
            lblCurrentPointTitle.TabIndex = 0;
            lblCurrentPointTitle.Text = "CURRENT TEST POINT";
            lblCurrentPointTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tblRow3
            // 
            tblRow3.ColumnCount = 3;
            tblRow3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblRow3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblRow3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblRow3.Controls.Add(cardLiveDetails, 0, 0);
            tblRow3.Controls.Add(cardWiringDiagram, 1, 0);
            tblRow3.Controls.Add(tblSummaryNotes, 2, 0);
            tblRow3.Dock = DockStyle.Fill;
            tblRow3.Location = new Point(3, 993);
            tblRow3.Name = "tblRow3";
            tblRow3.RowCount = 1;
            tblRow3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblRow3.Size = new Size(2670, 657);
            tblRow3.TabIndex = 3;
            // 
            // cardLiveDetails
            // 
            cardLiveDetails.BackColor = Color.White;
            cardLiveDetails.BorderStyle = BorderStyle.FixedSingle;
            cardLiveDetails.Controls.Add(gridLiveDetails);
            cardLiveDetails.Controls.Add(lblLiveDetailsTitle);
            cardLiveDetails.Dock = DockStyle.Fill;
            cardLiveDetails.Location = new Point(5, 5);
            cardLiveDetails.Margin = new Padding(5);
            cardLiveDetails.Name = "cardLiveDetails";
            cardLiveDetails.Padding = new Padding(10);
            cardLiveDetails.Size = new Size(1325, 647);
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
            gridLiveDetails.Size = new Size(1305, 565);
            gridLiveDetails.TabIndex = 1;
            // 
            // lblLiveDetailsTitle
            // 
            lblLiveDetailsTitle.Dock = DockStyle.Top;
            lblLiveDetailsTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblLiveDetailsTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblLiveDetailsTitle.Location = new Point(10, 10);
            lblLiveDetailsTitle.Name = "lblLiveDetailsTitle";
            lblLiveDetailsTitle.Size = new Size(1305, 62);
            lblLiveDetailsTitle.TabIndex = 0;
            lblLiveDetailsTitle.Text = "LIVE TEST DETAILS";
            lblLiveDetailsTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cardWiringDiagram
            // 
            cardWiringDiagram.BackColor = Color.White;
            cardWiringDiagram.BorderStyle = BorderStyle.FixedSingle;
            cardWiringDiagram.Controls.Add(pnlWiringPlaceholder);
            cardWiringDiagram.Controls.Add(lblWiringDiagramTitle);
            cardWiringDiagram.Dock = DockStyle.Fill;
            cardWiringDiagram.Location = new Point(1340, 5);
            cardWiringDiagram.Margin = new Padding(5);
            cardWiringDiagram.Name = "cardWiringDiagram";
            cardWiringDiagram.Padding = new Padding(10);
            cardWiringDiagram.Size = new Size(657, 647);
            cardWiringDiagram.TabIndex = 1;
            // 
            // pnlWiringPlaceholder
            // 
            pnlWiringPlaceholder.BackColor = Color.FromArgb(248, 250, 252);
            pnlWiringPlaceholder.Controls.Add(lblWiringMockText);
            pnlWiringPlaceholder.Dock = DockStyle.Fill;
            pnlWiringPlaceholder.Location = new Point(10, 72);
            pnlWiringPlaceholder.Name = "pnlWiringPlaceholder";
            pnlWiringPlaceholder.Size = new Size(637, 565);
            pnlWiringPlaceholder.TabIndex = 1;
            // 
            // lblWiringMockText
            // 
            lblWiringMockText.Dock = DockStyle.Fill;
            lblWiringMockText.Font = new Font("Segoe UI", 9.5F);
            lblWiringMockText.ForeColor = Color.FromArgb(100, 116, 139);
            lblWiringMockText.Location = new Point(0, 0);
            lblWiringMockText.Name = "lblWiringMockText";
            lblWiringMockText.Size = new Size(637, 565);
            lblWiringMockText.TabIndex = 0;
            lblWiringMockText.Text = "J1 to J3 Visual Wiring Map Placeholder\r\n\r\nLegend:\r\n✔ Completed\r\n⏳ Testing\r\n⏳ Pending";
            lblWiringMockText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblWiringDiagramTitle
            // 
            lblWiringDiagramTitle.Dock = DockStyle.Top;
            lblWiringDiagramTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblWiringDiagramTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblWiringDiagramTitle.Location = new Point(10, 10);
            lblWiringDiagramTitle.Name = "lblWiringDiagramTitle";
            lblWiringDiagramTitle.Size = new Size(637, 62);
            lblWiringDiagramTitle.TabIndex = 0;
            lblWiringDiagramTitle.Text = "WIRING DIAGRAM";
            lblWiringDiagramTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tblSummaryNotes
            // 
            tblSummaryNotes.ColumnCount = 1;
            tblSummaryNotes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblSummaryNotes.Controls.Add(cardTestSummary, 0, 0);
            tblSummaryNotes.Controls.Add(cardNotes, 0, 1);
            tblSummaryNotes.Dock = DockStyle.Fill;
            tblSummaryNotes.Location = new Point(2002, 0);
            tblSummaryNotes.Margin = new Padding(0);
            tblSummaryNotes.Name = "tblSummaryNotes";
            tblSummaryNotes.RowCount = 2;
            tblSummaryNotes.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            tblSummaryNotes.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            tblSummaryNotes.Size = new Size(668, 657);
            tblSummaryNotes.TabIndex = 2;
            // 
            // cardTestSummary
            // 
            cardTestSummary.BackColor = Color.White;
            cardTestSummary.BorderStyle = BorderStyle.FixedSingle;
            cardTestSummary.Controls.Add(lblSummaryText);
            cardTestSummary.Controls.Add(lblTestSummaryTitle);
            cardTestSummary.Dock = DockStyle.Fill;
            cardTestSummary.Location = new Point(5, 5);
            cardTestSummary.Margin = new Padding(5, 5, 5, 2);
            cardTestSummary.Name = "cardTestSummary";
            cardTestSummary.Padding = new Padding(10);
            cardTestSummary.Size = new Size(658, 387);
            cardTestSummary.TabIndex = 0;
            // 
            // lblSummaryText
            // 
            lblSummaryText.Dock = DockStyle.Fill;
            lblSummaryText.Font = new Font("Segoe UI", 9.2F);
            lblSummaryText.ForeColor = Color.FromArgb(51, 65, 85);
            lblSummaryText.Location = new Point(10, 56);
            lblSummaryText.Name = "lblSummaryText";
            lblSummaryText.Size = new Size(638, 321);
            lblSummaryText.TabIndex = 1;
            lblSummaryText.Text = "Continuity Test (212/212 ✔️)\r\nShort Circuit Test (212/212 ✔️)\r\nInsulation Test (212/212 ⏳)\r\nFunctional Test (64/212 ⏳)\r\nFinal Verification (0/212)";
            // 
            // lblTestSummaryTitle
            // 
            lblTestSummaryTitle.Dock = DockStyle.Top;
            lblTestSummaryTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTestSummaryTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblTestSummaryTitle.Location = new Point(10, 10);
            lblTestSummaryTitle.Name = "lblTestSummaryTitle";
            lblTestSummaryTitle.Size = new Size(638, 46);
            lblTestSummaryTitle.TabIndex = 0;
            lblTestSummaryTitle.Text = "TEST SUMMARY";
            lblTestSummaryTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cardNotes
            // 
            cardNotes.BackColor = Color.White;
            cardNotes.BorderStyle = BorderStyle.FixedSingle;
            cardNotes.Controls.Add(txtNotes);
            cardNotes.Controls.Add(lblNotesTitle);
            cardNotes.Dock = DockStyle.Fill;
            cardNotes.Location = new Point(5, 396);
            cardNotes.Margin = new Padding(5, 2, 5, 5);
            cardNotes.Name = "cardNotes";
            cardNotes.Padding = new Padding(10);
            cardNotes.Size = new Size(658, 256);
            cardNotes.TabIndex = 1;
            // 
            // txtNotes
            // 
            txtNotes.BorderStyle = BorderStyle.FixedSingle;
            txtNotes.Dock = DockStyle.Fill;
            txtNotes.Font = new Font("Segoe UI", 9.5F);
            txtNotes.Location = new Point(10, 57);
            txtNotes.Multiline = true;
            txtNotes.Name = "txtNotes";
            txtNotes.Size = new Size(638, 189);
            txtNotes.TabIndex = 1;
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
            // TestExecution
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(244, 245, 247);
            Controls.Add(tblMain);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "TestExecution";
            Size = new Size(2676, 1653);
            tblMain.ResumeLayout(false);
            pnlTopBar.ResumeLayout(false);
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
            cardProgress.ResumeLayout(false);
            tblProgressCard.ResumeLayout(false);
            pnlSequenceSection.ResumeLayout(false);
            tblSteps.ResumeLayout(false);
            pnlOverallStatsSection.ResumeLayout(false);
            cardCurrentPoint.ResumeLayout(false);
            tblCurrentPointDetails.ResumeLayout(false);
            tblCurrentPointDetails.PerformLayout();
            tblRow3.ResumeLayout(false);
            cardLiveDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridLiveDetails).EndInit();
            cardWiringDiagram.ResumeLayout(false);
            pnlWiringPlaceholder.ResumeLayout(false);
            tblSummaryNotes.ResumeLayout(false);
            cardTestSummary.ResumeLayout(false);
            cardNotes.ResumeLayout(false);
            cardNotes.PerformLayout();
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
        private System.Windows.Forms.Label lblElapsedVal;
        private System.Windows.Forms.Label lblEstimatedKey;
        private System.Windows.Forms.Label lblEstimatedVal;
        private System.Windows.Forms.Label lblEndTimeKey;
        private System.Windows.Forms.Label lblEndTimeVal;
        private System.Windows.Forms.Label lblStatusRunning;
        private System.Windows.Forms.Label lblTestStatusTitle;
        private System.Windows.Forms.TableLayoutPanel tblRow2;
        private System.Windows.Forms.Panel cardProgress;
        private System.Windows.Forms.TableLayoutPanel tblProgressCard;
        private System.Windows.Forms.Panel pnlSequenceSection;
        private System.Windows.Forms.TableLayoutPanel tblSteps;
        private System.Windows.Forms.Label lblStep1;
        private System.Windows.Forms.Label lblStep2;
        private System.Windows.Forms.Label lblStep3;
        private System.Windows.Forms.Label lblStep4;
        private System.Windows.Forms.Label lblStep5;
        private System.Windows.Forms.Label lblSequenceTitle;
        private System.Windows.Forms.Panel pnlOverallStatsSection;
        private System.Windows.Forms.Label lblProgressStats;
        private System.Windows.Forms.ProgressBar prgOverall;
        private System.Windows.Forms.Label lblPercent;
        private System.Windows.Forms.Label lblOverallProgressTitle;
        private System.Windows.Forms.Panel cardCurrentPoint;
        private System.Windows.Forms.TableLayoutPanel tblCurrentPointDetails;
        private System.Windows.Forms.Label lblExpectedKey;
        private System.Windows.Forms.Label lblExpectedVal;
        private System.Windows.Forms.Label lblMeasuredKey;
        private System.Windows.Forms.Label lblMeasuredVal;
        private System.Windows.Forms.Label lblResultKey;
        private System.Windows.Forms.Label lblResultVal;
        private System.Windows.Forms.Label lblPointsRoute;
        private System.Windows.Forms.Label lblCurrentPointTitle;
        private System.Windows.Forms.TableLayoutPanel tblRow3;
        private System.Windows.Forms.Panel cardLiveDetails;
        private System.Windows.Forms.DataGridView gridLiveDetails;
        private System.Windows.Forms.Label lblLiveDetailsTitle;
        private System.Windows.Forms.Panel cardWiringDiagram;
        private System.Windows.Forms.Panel pnlWiringPlaceholder;
        private System.Windows.Forms.Label lblWiringMockText;
        private System.Windows.Forms.Label lblWiringDiagramTitle;
        private System.Windows.Forms.TableLayoutPanel tblSummaryNotes;
        private System.Windows.Forms.Panel cardTestSummary;
        private System.Windows.Forms.Label lblSummaryText;
        private System.Windows.Forms.Label lblTestSummaryTitle;
        private System.Windows.Forms.Panel cardNotes;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label lblNotesTitle;
    }
}
