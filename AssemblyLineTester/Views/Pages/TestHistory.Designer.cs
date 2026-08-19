namespace AssemblyLineTester.Views.Pages

{

    partial class TestHistory

    {

        private System.ComponentModel.IContainer components = null;



        protected override void Dispose(bool disposing)

        {

            if (disposing && (components != null))

            {

                components.Dispose();

            }

            base.Dispose(disposing);

        }



        #region Component Designer generated code



        private void InitializeComponent()

        {
            pnlTop = new Panel();
            pnlFilterCard = new Panel();
            tblFilterInner = new TableLayoutPanel();
            tblFilterInputs = new TableLayoutPanel();
            lblDateRange = new Label();
            tblDateRange = new TableLayoutPanel();
            dtpStart = new DateTimePicker();
            lblDash = new Label();
            dtpEnd = new DateTimePicker();
            lblProductModel = new Label();
            cmbProductModel = new ComboBox();
            lblArea = new Label();
            cmbArea = new ComboBox();
            lblResult = new Label();
            cmbResult = new ComboBox();
            lblOperator = new Label();
            txtOperator = new TextBox();
            flpActionButtons = new FlowLayoutPanel();
            btnSearch = new Button();
            btnReset = new Button();
            lblTitle = new Label();
            mainLayout = new TableLayoutPanel();
            pnlLeft = new Panel();
            tblLeftContent = new TableLayoutPanel();
            tblLeftHeader = new TableLayoutPanel();
            lblTableTitle = new Label();
            flpLeftButtons = new FlowLayoutPanel();
            btnExport = new Button();
            btnRefresh = new Button();
            gridData = new DataGridView();
            tblPagination = new TableLayoutPanel();
            flpPageNav = new FlowLayoutPanel();
            flpPageInfo = new FlowLayoutPanel();
            txtGoToPage = new TextBox();
            lblGoTo = new Label();
            lblEntries = new Label();
            cmbPageSize = new ComboBox();
            lblShow = new Label();
            rightLayout = new TableLayoutPanel();
            pnlDetail = new Panel();
            tblDetailContent = new TableLayoutPanel();
            tblDetailHeader = new TableLayoutPanel();
            lblDetailTitle = new Label();
            lblDetailBadge = new Label();
            tblDetailBody = new TableLayoutPanel();
            lblDtTestTimeK = new Label();
            lblDtTestTimeV = new Label();
            lblDtModelK = new Label();
            lblDtModelV = new Label();
            lblDtTotalK = new Label();
            lblDtTotalV = new Label();
            lblDtTypeK = new Label();
            lblDtTypeV = new Label();
            lblDtStartK = new Label();
            lblDtStartV = new Label();
            lblDtAreaK = new Label();
            lblDtAreaV = new Label();
            lblDtEndK = new Label();
            lblDtEndV = new Label();
            lblDtPartK = new Label();
            lblDtPartV = new Label();
            lblDtResultK = new Label();
            lblDtResultV = new Label();
            lblDtSerialK = new Label();
            lblDtSerialV = new Label();
            lblDtEcnK = new Label();
            lblDtEcnV = new Label();
            lblDtOpK = new Label();
            lblDtOpV = new Label();
            flpDetailFooter = new FlowLayoutPanel();
            btnViewReport = new Button();
            btnDeleteHistory = new Button();
            pnlSummary = new Panel();
            tblSummaryContent = new TableLayoutPanel();
            lblSummaryTitle = new Label();
            tblSummaryBody = new TableLayoutPanel();
            pnlChartContainer = new Panel();
            tblSummaryLegend = new TableLayoutPanel();
            lblLegPassIcon = new Label();
            lblLegPassText = new Label();
            lblLegFailIcon = new Label();
            lblLegFailText = new Label();
            lblLegProgIcon = new Label();
            lblLegProgText = new Label();
            lblLegStopIcon = new Label();
            lblLegStopText = new Label();
            pnlSteps = new Panel();
            tblStepsContent = new TableLayoutPanel();
            lblStepsTitle = new Label();
            gridSteps = new DataGridView();
            pnlTop.SuspendLayout();
            pnlFilterCard.SuspendLayout();
            tblFilterInner.SuspendLayout();
            tblFilterInputs.SuspendLayout();
            tblDateRange.SuspendLayout();
            flpActionButtons.SuspendLayout();
            mainLayout.SuspendLayout();
            pnlLeft.SuspendLayout();
            tblLeftContent.SuspendLayout();
            tblLeftHeader.SuspendLayout();
            flpLeftButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridData).BeginInit();
            tblPagination.SuspendLayout();
            flpPageInfo.SuspendLayout();
            rightLayout.SuspendLayout();
            pnlDetail.SuspendLayout();
            tblDetailContent.SuspendLayout();
            tblDetailHeader.SuspendLayout();
            tblDetailBody.SuspendLayout();
            flpDetailFooter.SuspendLayout();
            pnlSummary.SuspendLayout();
            tblSummaryContent.SuspendLayout();
            tblSummaryBody.SuspendLayout();
            tblSummaryLegend.SuspendLayout();
            pnlSteps.SuspendLayout();
            tblStepsContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridSteps).BeginInit();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.FromArgb(244, 245, 247);
            pnlTop.Controls.Add(pnlFilterCard);
            pnlTop.Controls.Add(lblTitle);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Margin = new Padding(4, 4, 4, 4);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(1225, 112);
            pnlTop.TabIndex = 0;
            // 
            // pnlFilterCard
            // 
            pnlFilterCard.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlFilterCard.BackColor = Color.White;
            pnlFilterCard.Controls.Add(tblFilterInner);
            pnlFilterCard.Location = new Point(17, 41);
            pnlFilterCard.Margin = new Padding(4, 4, 4, 4);
            pnlFilterCard.Name = "pnlFilterCard";
            pnlFilterCard.Padding = new Padding(6, 6, 6, 6);
            pnlFilterCard.Size = new Size(1190, 60);
            pnlFilterCard.TabIndex = 2;
            // 
            // tblFilterInner
            // 
            tblFilterInner.ColumnCount = 2;
            tblFilterInner.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblFilterInner.ColumnStyles.Add(new ColumnStyle());
            tblFilterInner.Controls.Add(tblFilterInputs, 0, 0);
            tblFilterInner.Controls.Add(flpActionButtons, 1, 0);
            tblFilterInner.Dock = DockStyle.Fill;
            tblFilterInner.Location = new Point(6, 6);
            tblFilterInner.Margin = new Padding(0);
            tblFilterInner.Name = "tblFilterInner";
            tblFilterInner.RowCount = 1;
            tblFilterInner.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblFilterInner.Size = new Size(1178, 48);
            tblFilterInner.TabIndex = 0;
            // 
            // tblFilterInputs
            // 
            tblFilterInputs.ColumnCount = 5;
            tblFilterInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
            tblFilterInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18F));
            tblFilterInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18F));
            tblFilterInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18F));
            tblFilterInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18F));
            tblFilterInputs.Controls.Add(lblDateRange, 0, 0);
            tblFilterInputs.Controls.Add(tblDateRange, 0, 1);
            tblFilterInputs.Controls.Add(lblProductModel, 1, 0);
            tblFilterInputs.Controls.Add(cmbProductModel, 1, 1);
            tblFilterInputs.Controls.Add(lblArea, 2, 0);
            tblFilterInputs.Controls.Add(cmbArea, 2, 1);
            tblFilterInputs.Controls.Add(lblResult, 3, 0);
            tblFilterInputs.Controls.Add(cmbResult, 3, 1);
            tblFilterInputs.Controls.Add(lblOperator, 4, 0);
            tblFilterInputs.Controls.Add(txtOperator, 4, 1);
            tblFilterInputs.Dock = DockStyle.Fill;
            tblFilterInputs.Location = new Point(0, 0);
            tblFilterInputs.Margin = new Padding(0);
            tblFilterInputs.Name = "tblFilterInputs";
            tblFilterInputs.RowCount = 2;
            tblFilterInputs.RowStyles.Add(new RowStyle(SizeType.Percent, 35F));
            tblFilterInputs.RowStyles.Add(new RowStyle(SizeType.Percent, 65F));
            tblFilterInputs.Size = new Size(1019, 48);
            tblFilterInputs.TabIndex = 0;
            // 
            // lblDateRange
            // 
            lblDateRange.AutoSize = true;
            lblDateRange.Dock = DockStyle.Fill;
            lblDateRange.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblDateRange.ForeColor = Color.FromArgb(51, 65, 85);
            lblDateRange.Location = new Point(2, 0);
            lblDateRange.Margin = new Padding(2, 0, 2, 1);
            lblDateRange.Name = "lblDateRange";
            lblDateRange.Size = new Size(286, 15);
            lblDateRange.TabIndex = 2;
            lblDateRange.Text = "Date Range";
            lblDateRange.TextAlign = ContentAlignment.BottomLeft;
            // 
            // tblDateRange
            // 
            tblDateRange.ColumnCount = 3;
            tblDateRange.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblDateRange.ColumnStyles.Add(new ColumnStyle());
            tblDateRange.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblDateRange.Controls.Add(dtpStart, 0, 0);
            tblDateRange.Controls.Add(lblDash, 1, 0);
            tblDateRange.Controls.Add(dtpEnd, 2, 0);
            tblDateRange.Dock = DockStyle.Fill;
            tblDateRange.Location = new Point(0, 16);
            tblDateRange.Margin = new Padding(0);
            tblDateRange.Name = "tblDateRange";
            tblDateRange.RowCount = 1;
            tblDateRange.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblDateRange.Size = new Size(290, 32);
            tblDateRange.TabIndex = 0;
            // 
            // dtpStart
            // 
            dtpStart.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dtpStart.CustomFormat = "";
            dtpStart.Font = new Font("Segoe UI", 9F);
            dtpStart.Format = DateTimePickerFormat.Custom;
            dtpStart.Location = new Point(2, 4);
            dtpStart.Margin = new Padding(2, 2, 2, 2);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(135, 23);
            dtpStart.TabIndex = 3;
            // 
            // lblDash
            // 
            lblDash.AutoSize = true;
            lblDash.Dock = DockStyle.Fill;
            lblDash.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDash.ForeColor = Color.FromArgb(100, 116, 139);
            lblDash.Location = new Point(139, 0);
            lblDash.Margin = new Padding(0);
            lblDash.Name = "lblDash";
            lblDash.Size = new Size(12, 32);
            lblDash.TabIndex = 4;
            lblDash.Text = "-";
            lblDash.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dtpEnd
            // 
            dtpEnd.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dtpEnd.CustomFormat = "";
            dtpEnd.Font = new Font("Segoe UI", 9F);
            dtpEnd.Format = DateTimePickerFormat.Custom;
            dtpEnd.Location = new Point(153, 4);
            dtpEnd.Margin = new Padding(2, 2, 2, 2);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(135, 23);
            dtpEnd.TabIndex = 5;
            // 
            // lblProductModel
            // 
            lblProductModel.AutoSize = true;
            lblProductModel.Dock = DockStyle.Fill;
            lblProductModel.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblProductModel.ForeColor = Color.FromArgb(51, 65, 85);
            lblProductModel.Location = new Point(292, 0);
            lblProductModel.Margin = new Padding(2, 0, 2, 1);
            lblProductModel.Name = "lblProductModel";
            lblProductModel.Size = new Size(141, 15);
            lblProductModel.TabIndex = 6;
            lblProductModel.Text = "Product Model";
            lblProductModel.TextAlign = ContentAlignment.BottomLeft;
            // 
            // cmbProductModel
            // 
            cmbProductModel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbProductModel.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProductModel.Font = new Font("Segoe UI", 9F);
            cmbProductModel.FormattingEnabled = true;
            cmbProductModel.Items.AddRange(new object[] { "All", "6060", "797F", "D11T" });
            cmbProductModel.Location = new Point(292, 20);
            cmbProductModel.Margin = new Padding(2, 2, 2, 2);
            cmbProductModel.Name = "cmbProductModel";
            cmbProductModel.Size = new Size(141, 23);
            cmbProductModel.TabIndex = 7;
            // 
            // lblProductType
            // 
            // 
            // lblArea
            // 
            lblArea.AutoSize = true;
            lblArea.Dock = DockStyle.Fill;
            lblArea.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblArea.ForeColor = Color.FromArgb(51, 65, 85);
            lblArea.Location = new Point(437, 0);
            lblArea.Margin = new Padding(2, 0, 2, 1);
            lblArea.Name = "lblArea";
            lblArea.Size = new Size(141, 15);
            lblArea.TabIndex = 10;
            lblArea.Text = "Area";
            lblArea.TextAlign = ContentAlignment.BottomLeft;
            // 
            // cmbArea
            // 
            cmbArea.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbArea.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbArea.Font = new Font("Segoe UI", 9F);
            cmbArea.FormattingEnabled = true;
            cmbArea.Items.AddRange(new object[] { "All", "Engine Control", "Cabin", "Main Frame", "Boom", "Power Module", "Lighting", "Hydraulic Control", "Monitoring System" });
            cmbArea.Location = new Point(437, 20);
            cmbArea.Margin = new Padding(2, 2, 2, 2);
            cmbArea.Name = "cmbArea";
            cmbArea.Size = new Size(141, 23);
            cmbArea.TabIndex = 11;
            // 
            // lblResult
            // 
            lblResult.AutoSize = true;
            lblResult.Dock = DockStyle.Fill;
            lblResult.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblResult.ForeColor = Color.FromArgb(51, 65, 85);
            lblResult.Location = new Point(582, 0);
            lblResult.Margin = new Padding(2, 0, 2, 1);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(141, 15);
            lblResult.TabIndex = 12;
            lblResult.Text = "Result";
            lblResult.TextAlign = ContentAlignment.BottomLeft;
            // 
            // cmbResult
            // 
            cmbResult.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbResult.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbResult.Font = new Font("Segoe UI", 9F);
            cmbResult.FormattingEnabled = true;
            cmbResult.Items.AddRange(new object[] { "All", "PASS", "FAIL" });
            cmbResult.Location = new Point(582, 20);
            cmbResult.Margin = new Padding(2, 2, 2, 2);
            cmbResult.Name = "cmbResult";
            cmbResult.Size = new Size(141, 23);
            cmbResult.TabIndex = 13;
            // 
            // lblOperator
            // 
            lblOperator.AutoSize = true;
            lblOperator.Dock = DockStyle.Fill;
            lblOperator.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblOperator.ForeColor = Color.FromArgb(51, 65, 85);
            lblOperator.Location = new Point(727, 0);
            lblOperator.Margin = new Padding(2, 0, 2, 1);
            lblOperator.Name = "lblOperator";
            lblOperator.Size = new Size(145, 15);
            lblOperator.TabIndex = 14;
            lblOperator.Text = "Operator / User";
            lblOperator.TextAlign = ContentAlignment.BottomLeft;
            // 
            // txtOperator
            // 
            txtOperator.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtOperator.Font = new Font("Segoe UI", 9F);
            txtOperator.Location = new Point(727, 20);
            txtOperator.Margin = new Padding(2, 2, 2, 2);
            txtOperator.Name = "txtOperator";
            txtOperator.PlaceholderText = "Search name or username";
            txtOperator.Size = new Size(145, 23);
            txtOperator.TabIndex = 15;
            // 
            // flpActionButtons
            // 
            flpActionButtons.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            flpActionButtons.AutoSize = true;
            flpActionButtons.Controls.Add(btnSearch);
            flpActionButtons.Controls.Add(btnReset);
            flpActionButtons.Location = new Point(1025, 18);
            flpActionButtons.Margin = new Padding(6, 0, 0, 0);
            flpActionButtons.Name = "flpActionButtons";
            flpActionButtons.Size = new Size(153, 30);
            flpActionButtons.TabIndex = 1;
            flpActionButtons.WrapContents = false;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(250, 204, 21);
            btnSearch.Cursor = Cursors.Hand;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSearch.ForeColor = Color.FromArgb(15, 23, 42);
            btnSearch.Location = new Point(0, 0);
            btnSearch.Margin = new Padding(0, 0, 4, 5);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(83, 25);
            btnSearch.TabIndex = 16;
            btnSearch.Text = "🔍 Search";
            btnSearch.UseVisualStyleBackColor = false;
            // 
            // btnReset
            // 
            btnReset.BackColor = Color.FromArgb(241, 245, 249);
            btnReset.Cursor = Cursors.Hand;
            btnReset.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnReset.ForeColor = Color.FromArgb(71, 85, 105);
            btnReset.Location = new Point(87, 0);
            btnReset.Margin = new Padding(0);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(66, 25);
            btnReset.TabIndex = 17;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = false;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 13.5F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTitle.Location = new Point(21, 9);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(177, 25);
            lblTitle.TabIndex = 1;
            lblTitle.Text = " 📜  TEST HISTORY";
            // 
            // mainLayout
            // 
            mainLayout.ColumnCount = 2;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 61.89011F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38.10989F));
            mainLayout.Controls.Add(pnlLeft, 0, 0);
            mainLayout.Controls.Add(rightLayout, 1, 0);
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Location = new Point(0, 112);
            mainLayout.Margin = new Padding(4, 4, 4, 4);
            mainLayout.Name = "mainLayout";
            mainLayout.RowCount = 1;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.Size = new Size(1225, 600);
            mainLayout.TabIndex = 1;
            // 
            // pnlLeft
            // 
            pnlLeft.BackColor = Color.White;
            pnlLeft.Controls.Add(tblLeftContent);
            pnlLeft.Dock = DockStyle.Fill;
            pnlLeft.Location = new Point(16, 14);
            pnlLeft.Margin = new Padding(16, 14, 16, 14);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Size = new Size(726, 572);
            pnlLeft.TabIndex = 0;
            // 
            // tblLeftContent
            // 
            tblLeftContent.BackColor = Color.Transparent;
            tblLeftContent.ColumnCount = 1;
            tblLeftContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblLeftContent.Controls.Add(tblLeftHeader, 0, 0);
            tblLeftContent.Controls.Add(gridData, 0, 1);
            tblLeftContent.Controls.Add(tblPagination, 0, 2);
            tblLeftContent.Dock = DockStyle.Fill;
            tblLeftContent.Location = new Point(0, 0);
            tblLeftContent.Margin = new Padding(0);
            tblLeftContent.Name = "tblLeftContent";
            tblLeftContent.RowCount = 3;
            tblLeftContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 37F));
            tblLeftContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblLeftContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            tblLeftContent.Size = new Size(726, 572);
            tblLeftContent.TabIndex = 0;
            // 
            // tblLeftHeader
            // 
            tblLeftHeader.ColumnCount = 2;
            tblLeftHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tblLeftHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tblLeftHeader.Controls.Add(lblTableTitle, 0, 0);
            tblLeftHeader.Controls.Add(flpLeftButtons, 1, 0);
            tblLeftHeader.Dock = DockStyle.Fill;
            tblLeftHeader.Location = new Point(0, 0);
            tblLeftHeader.Margin = new Padding(0);
            tblLeftHeader.Name = "tblLeftHeader";
            tblLeftHeader.Padding = new Padding(11, 0, 11, 0);
            tblLeftHeader.RowCount = 1;
            tblLeftHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblLeftHeader.Size = new Size(726, 37);
            tblLeftHeader.TabIndex = 0;
            // 
            // lblTableTitle
            // 
            lblTableTitle.Dock = DockStyle.Fill;
            lblTableTitle.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            lblTableTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblTableTitle.Location = new Point(11, 0);
            lblTableTitle.Margin = new Padding(0);
            lblTableTitle.Name = "lblTableTitle";
            lblTableTitle.Size = new Size(387, 37);
            lblTableTitle.TabIndex = 0;
            lblTableTitle.Text = "TEST HISTORY LIST (Total: 215)";
            lblTableTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // flpLeftButtons
            // 
            flpLeftButtons.Controls.Add(btnExport);
            flpLeftButtons.Controls.Add(btnRefresh);
            flpLeftButtons.Dock = DockStyle.Fill;
            flpLeftButtons.FlowDirection = FlowDirection.RightToLeft;
            flpLeftButtons.Location = new Point(398, 0);
            flpLeftButtons.Margin = new Padding(0);
            flpLeftButtons.Name = "flpLeftButtons";
            flpLeftButtons.Size = new Size(317, 37);
            flpLeftButtons.TabIndex = 1;
            flpLeftButtons.WrapContents = false;
            // 
            // btnExport
            // 
            btnExport.BackColor = Color.FromArgb(241, 245, 249);
            btnExport.Cursor = Cursors.Hand;
            btnExport.FlatAppearance.BorderColor = Color.LightGray;
            btnExport.FlatStyle = FlatStyle.Flat;
            btnExport.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnExport.ForeColor = Color.FromArgb(30, 41, 59);
            btnExport.Location = new Point(229, 5);
            btnExport.Margin = new Padding(0, 5, 0, 0);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(88, 26);
            btnExport.TabIndex = 1;
            btnExport.Text = "📤 Export";
            btnExport.UseVisualStyleBackColor = false;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(241, 245, 249);
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.FlatAppearance.BorderColor = Color.LightGray;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.FromArgb(30, 41, 59);
            btnRefresh.Location = new Point(122, 5);
            btnRefresh.Margin = new Padding(0, 5, 11, 0);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(96, 26);
            btnRefresh.TabIndex = 0;
            btnRefresh.Text = "↻ Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            // 
            // gridData
            // 
            gridData.AllowUserToAddRows = false;
            gridData.AllowUserToDeleteRows = false;
            gridData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridData.BackgroundColor = Color.White;
            gridData.BorderStyle = BorderStyle.None;
            gridData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridData.Dock = DockStyle.Fill;
            gridData.Location = new Point(11, 46);
            gridData.Margin = new Padding(11, 9, 11, 0);
            gridData.Name = "gridData";
            gridData.ReadOnly = true;
            gridData.RowHeadersVisible = false;
            gridData.RowHeadersWidth = 51;
            gridData.Size = new Size(704, 500);
            gridData.TabIndex = 1;
            // 
            // tblPagination
            // 
            tblPagination.ColumnCount = 2;
            tblPagination.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tblPagination.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tblPagination.Controls.Add(flpPageNav, 0, 0);
            tblPagination.Controls.Add(flpPageInfo, 1, 0);
            tblPagination.Dock = DockStyle.Fill;
            tblPagination.Location = new Point(0, 546);
            tblPagination.Margin = new Padding(0);
            tblPagination.Name = "tblPagination";
            tblPagination.Padding = new Padding(11, 0, 11, 0);
            tblPagination.RowCount = 1;
            tblPagination.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblPagination.Size = new Size(726, 26);
            tblPagination.TabIndex = 2;
            // 
            // flpPageNav
            // 
            flpPageNav.Dock = DockStyle.Fill;
            flpPageNav.Location = new Point(11, 0);
            flpPageNav.Margin = new Padding(0);
            flpPageNav.Name = "flpPageNav";
            flpPageNav.Size = new Size(246, 26);
            flpPageNav.TabIndex = 0;
            flpPageNav.WrapContents = false;
            // 
            // flpPageInfo
            // 
            flpPageInfo.Controls.Add(txtGoToPage);
            flpPageInfo.Controls.Add(lblGoTo);
            flpPageInfo.Controls.Add(lblEntries);
            flpPageInfo.Controls.Add(cmbPageSize);
            flpPageInfo.Controls.Add(lblShow);
            flpPageInfo.Dock = DockStyle.Fill;
            flpPageInfo.FlowDirection = FlowDirection.RightToLeft;
            flpPageInfo.Location = new Point(257, 0);
            flpPageInfo.Margin = new Padding(0);
            flpPageInfo.Name = "flpPageInfo";
            flpPageInfo.Size = new Size(458, 26);
            flpPageInfo.TabIndex = 1;
            flpPageInfo.WrapContents = false;
            // 
            // txtGoToPage
            // 
            txtGoToPage.Font = new Font("Segoe UI", 8.5F);
            txtGoToPage.Location = new Point(429, 7);
            txtGoToPage.Margin = new Padding(3, 7, 0, 0);
            txtGoToPage.Name = "txtGoToPage";
            txtGoToPage.Size = new Size(29, 23);
            txtGoToPage.TabIndex = 0;
            txtGoToPage.Text = "1";
            txtGoToPage.TextAlign = HorizontalAlignment.Center;
            // 
            // lblGoTo
            // 
            lblGoTo.AutoSize = true;
            lblGoTo.Font = new Font("Segoe UI", 8.5F);
            lblGoTo.ForeColor = Color.FromArgb(71, 85, 105);
            lblGoTo.Location = new Point(390, 8);
            lblGoTo.Margin = new Padding(8, 8, 0, 0);
            lblGoTo.Name = "lblGoTo";
            lblGoTo.Size = new Size(36, 15);
            lblGoTo.TabIndex = 1;
            lblGoTo.Text = "Go to";
            // 
            // lblEntries
            // 
            lblEntries.AutoSize = true;
            lblEntries.Font = new Font("Segoe UI", 8.5F);
            lblEntries.ForeColor = Color.FromArgb(71, 85, 105);
            lblEntries.Location = new Point(340, 8);
            lblEntries.Margin = new Padding(3, 8, 0, 0);
            lblEntries.Name = "lblEntries";
            lblEntries.Size = new Size(42, 15);
            lblEntries.TabIndex = 2;
            lblEntries.Text = "entries";
            // 
            // cmbPageSize
            // 
            cmbPageSize.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPageSize.Font = new Font("Segoe UI", 8.5F);
            cmbPageSize.FormattingEnabled = true;
            cmbPageSize.Items.AddRange(new object[] { "10", "25", "50", "100" });
            cmbPageSize.Location = new Point(300, 7);
            cmbPageSize.Margin = new Padding(3, 7, 0, 0);
            cmbPageSize.Name = "cmbPageSize";
            cmbPageSize.Size = new Size(37, 21);
            cmbPageSize.TabIndex = 3;
            // 
            // lblShow
            // 
            lblShow.AutoSize = true;
            lblShow.Font = new Font("Segoe UI", 8.5F);
            lblShow.ForeColor = Color.FromArgb(71, 85, 105);
            lblShow.Location = new Point(261, 8);
            lblShow.Margin = new Padding(0, 8, 0, 0);
            lblShow.Name = "lblShow";
            lblShow.Size = new Size(36, 15);
            lblShow.TabIndex = 4;
            lblShow.Text = "Show";
            // 
            // rightLayout
            // 
            rightLayout.ColumnCount = 1;
            rightLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rightLayout.Controls.Add(pnlDetail, 0, 0);
            rightLayout.Controls.Add(pnlSummary, 0, 1);
            rightLayout.Controls.Add(pnlSteps, 0, 2);
            rightLayout.Dock = DockStyle.Fill;
            rightLayout.Location = new Point(774, 14);
            rightLayout.Margin = new Padding(16, 14, 16, 14);
            rightLayout.Name = "rightLayout";
            rightLayout.RowCount = 3;
            rightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            rightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            rightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 35F));
            rightLayout.Size = new Size(435, 572);
            rightLayout.TabIndex = 1;
            // 
            // pnlDetail
            // 
            pnlDetail.BackColor = Color.White;
            pnlDetail.Controls.Add(tblDetailContent);
            pnlDetail.Dock = DockStyle.Fill;
            pnlDetail.Location = new Point(0, 0);
            pnlDetail.Margin = new Padding(0, 0, 0, 9);
            pnlDetail.Name = "pnlDetail";
            pnlDetail.Padding = new Padding(9, 6, 9, 6);
            pnlDetail.Size = new Size(435, 219);
            pnlDetail.TabIndex = 0;
            // 
            // tblDetailContent
            // 
            tblDetailContent.ColumnCount = 1;
            tblDetailContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblDetailContent.Controls.Add(tblDetailHeader, 0, 0);
            tblDetailContent.Controls.Add(tblDetailBody, 0, 1);
            tblDetailContent.Controls.Add(flpDetailFooter, 0, 2);
            tblDetailContent.Dock = DockStyle.Fill;
            tblDetailContent.Location = new Point(9, 6);
            tblDetailContent.Margin = new Padding(0);
            tblDetailContent.Name = "tblDetailContent";
            tblDetailContent.RowCount = 3;
            tblDetailContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 21F));
            tblDetailContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblDetailContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            tblDetailContent.Size = new Size(417, 207);
            tblDetailContent.TabIndex = 0;
            // 
            // tblDetailHeader
            // 
            tblDetailHeader.ColumnCount = 2;
            tblDetailHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tblDetailHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tblDetailHeader.Controls.Add(lblDetailTitle, 0, 0);
            tblDetailHeader.Controls.Add(lblDetailBadge, 1, 0);
            tblDetailHeader.Dock = DockStyle.Fill;
            tblDetailHeader.Location = new Point(0, 0);
            tblDetailHeader.Margin = new Padding(0);
            tblDetailHeader.Name = "tblDetailHeader";
            tblDetailHeader.RowCount = 1;
            tblDetailHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblDetailHeader.Size = new Size(417, 21);
            tblDetailHeader.TabIndex = 0;
            // 
            // lblDetailTitle
            // 
            lblDetailTitle.Dock = DockStyle.Fill;
            lblDetailTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblDetailTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblDetailTitle.Location = new Point(0, 0);
            lblDetailTitle.Margin = new Padding(0);
            lblDetailTitle.Name = "lblDetailTitle";
            lblDetailTitle.Size = new Size(271, 21);
            lblDetailTitle.TabIndex = 0;
            lblDetailTitle.Text = "TEST DETAIL";
            lblDetailTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDetailBadge
            // 
            lblDetailBadge.Anchor = AnchorStyles.Right;
            lblDetailBadge.BackColor = Color.FromArgb(22, 101, 52);
            lblDetailBadge.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblDetailBadge.ForeColor = Color.White;
            lblDetailBadge.Location = new Point(364, 0);
            lblDetailBadge.Margin = new Padding(0);
            lblDetailBadge.Name = "lblDetailBadge";
            lblDetailBadge.Size = new Size(53, 21);
            lblDetailBadge.TabIndex = 1;
            lblDetailBadge.Text = "PASS";
            lblDetailBadge.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tblDetailBody
            // 
            tblDetailBody.ColumnCount = 4;
            tblDetailBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
            tblDetailBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22F));
            tblDetailBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblDetailBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblDetailBody.Controls.Add(lblDtTestTimeK, 0, 0);
            tblDetailBody.Controls.Add(lblDtTestTimeV, 1, 0);
            tblDetailBody.Controls.Add(lblDtModelK, 0, 1);
            tblDetailBody.Controls.Add(lblDtModelV, 1, 1);
            tblDetailBody.Controls.Add(lblDtTotalK, 2, 1);
            tblDetailBody.Controls.Add(lblDtTotalV, 3, 1);
            tblDetailBody.Controls.Add(lblDtTypeK, 0, 2);
            tblDetailBody.Controls.Add(lblDtTypeV, 1, 2);
            tblDetailBody.Controls.Add(lblDtStartK, 2, 2);
            tblDetailBody.Controls.Add(lblDtStartV, 3, 2);
            tblDetailBody.Controls.Add(lblDtAreaK, 0, 3);
            tblDetailBody.Controls.Add(lblDtAreaV, 1, 3);
            tblDetailBody.Controls.Add(lblDtEndK, 2, 3);
            tblDetailBody.Controls.Add(lblDtEndV, 3, 3);
            tblDetailBody.Controls.Add(lblDtPartK, 0, 4);
            tblDetailBody.Controls.Add(lblDtPartV, 1, 4);
            tblDetailBody.Controls.Add(lblDtResultK, 2, 4);
            tblDetailBody.Controls.Add(lblDtResultV, 3, 4);
            tblDetailBody.Controls.Add(lblDtSerialK, 0, 5);
            tblDetailBody.Controls.Add(lblDtSerialV, 1, 5);
            tblDetailBody.Controls.Add(lblDtEcnK, 0, 6);
            tblDetailBody.Controls.Add(lblDtEcnV, 1, 6);
            tblDetailBody.Controls.Add(lblDtOpK, 0, 7);
            tblDetailBody.Controls.Add(lblDtOpV, 1, 7);
            tblDetailBody.Dock = DockStyle.Fill;
            tblDetailBody.Location = new Point(0, 21);
            tblDetailBody.Margin = new Padding(0);
            tblDetailBody.Name = "tblDetailBody";
            tblDetailBody.RowCount = 8;
            tblDetailBody.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tblDetailBody.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tblDetailBody.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tblDetailBody.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tblDetailBody.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tblDetailBody.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tblDetailBody.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tblDetailBody.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tblDetailBody.Size = new Size(417, 162);
            tblDetailBody.TabIndex = 1;
            // 
            // lblDtTestTimeK
            // 
            lblDtTestTimeK.Dock = DockStyle.Fill;
            lblDtTestTimeK.Font = new Font("Segoe UI", 8.8F, FontStyle.Bold);
            lblDtTestTimeK.Location = new Point(0, 0);
            lblDtTestTimeK.Margin = new Padding(0);
            lblDtTestTimeK.Name = "lblDtTestTimeK";
            lblDtTestTimeK.Size = new Size(116, 20);
            lblDtTestTimeK.TabIndex = 0;
            lblDtTestTimeK.Text = "Test Time";
            lblDtTestTimeK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtTestTimeV
            // 
            tblDetailBody.SetColumnSpan(lblDtTestTimeV, 3);
            lblDtTestTimeV.Dock = DockStyle.Fill;
            lblDtTestTimeV.Font = new Font("Segoe UI", 8.8F);
            lblDtTestTimeV.Location = new Point(116, 0);
            lblDtTestTimeV.Margin = new Padding(0);
            lblDtTestTimeV.Name = "lblDtTestTimeV";
            lblDtTestTimeV.Size = new Size(301, 20);
            lblDtTestTimeV.TabIndex = 1;
            lblDtTestTimeV.Text = ": 15 Mar 2026 10:30:20 AM";
            lblDtTestTimeV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtModelK
            // 
            lblDtModelK.Dock = DockStyle.Fill;
            lblDtModelK.Font = new Font("Segoe UI", 8.8F, FontStyle.Bold);
            lblDtModelK.Location = new Point(0, 20);
            lblDtModelK.Margin = new Padding(0);
            lblDtModelK.Name = "lblDtModelK";
            lblDtModelK.Size = new Size(116, 20);
            lblDtModelK.TabIndex = 2;
            lblDtModelK.Text = "Product Model";
            lblDtModelK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtModelV
            // 
            lblDtModelV.Dock = DockStyle.Fill;
            lblDtModelV.Font = new Font("Segoe UI", 8.8F);
            lblDtModelV.Location = new Point(116, 20);
            lblDtModelV.Margin = new Padding(0);
            lblDtModelV.Name = "lblDtModelV";
            lblDtModelV.Size = new Size(91, 20);
            lblDtModelV.TabIndex = 3;
            lblDtModelV.Text = ": 6060";
            lblDtModelV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtTotalK
            // 
            lblDtTotalK.Dock = DockStyle.Fill;
            lblDtTotalK.Font = new Font("Segoe UI", 8.8F, FontStyle.Bold);
            lblDtTotalK.Location = new Point(207, 20);
            lblDtTotalK.Margin = new Padding(0);
            lblDtTotalK.Name = "lblDtTotalK";
            lblDtTotalK.Size = new Size(104, 20);
            lblDtTotalK.TabIndex = 4;
            lblDtTotalK.Text = "Total Time";
            lblDtTotalK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtTotalV
            // 
            lblDtTotalV.Dock = DockStyle.Fill;
            lblDtTotalV.Font = new Font("Segoe UI", 8.8F);
            lblDtTotalV.Location = new Point(311, 20);
            lblDtTotalV.Margin = new Padding(0);
            lblDtTotalV.Name = "lblDtTotalV";
            lblDtTotalV.Size = new Size(106, 20);
            lblDtTotalV.TabIndex = 5;
            lblDtTotalV.Text = ": 00:09:01";
            lblDtTotalV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtTypeK
            // 
            lblDtTypeK.Dock = DockStyle.Fill;
            lblDtTypeK.Font = new Font("Segoe UI", 8.8F, FontStyle.Bold);
            lblDtTypeK.Location = new Point(0, 40);
            lblDtTypeK.Margin = new Padding(0);
            lblDtTypeK.Name = "lblDtTypeK";
            lblDtTypeK.Size = new Size(116, 20);
            lblDtTypeK.TabIndex = 6;
            lblDtTypeK.Text = "Product Type";
            lblDtTypeK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtTypeV
            // 
            lblDtTypeV.Dock = DockStyle.Fill;
            lblDtTypeV.Font = new Font("Segoe UI", 8.8F);
            lblDtTypeV.Location = new Point(116, 40);
            lblDtTypeV.Margin = new Padding(0);
            lblDtTypeV.Name = "lblDtTypeV";
            lblDtTypeV.Size = new Size(91, 20);
            lblDtTypeV.TabIndex = 7;
            lblDtTypeV.Text = ": AC";
            lblDtTypeV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtStartK
            // 
            lblDtStartK.Dock = DockStyle.Fill;
            lblDtStartK.Font = new Font("Segoe UI", 8.8F, FontStyle.Bold);
            lblDtStartK.Location = new Point(207, 40);
            lblDtStartK.Margin = new Padding(0);
            lblDtStartK.Name = "lblDtStartK";
            lblDtStartK.Size = new Size(104, 20);
            lblDtStartK.TabIndex = 8;
            lblDtStartK.Text = "Start Time";
            lblDtStartK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtStartV
            // 
            lblDtStartV.Dock = DockStyle.Fill;
            lblDtStartV.Font = new Font("Segoe UI", 8.8F);
            lblDtStartV.Location = new Point(311, 40);
            lblDtStartV.Margin = new Padding(0);
            lblDtStartV.Name = "lblDtStartV";
            lblDtStartV.Size = new Size(106, 20);
            lblDtStartV.TabIndex = 9;
            lblDtStartV.Text = ": 10:30:20 AM";
            lblDtStartV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtAreaK
            // 
            lblDtAreaK.Dock = DockStyle.Fill;
            lblDtAreaK.Font = new Font("Segoe UI", 8.8F, FontStyle.Bold);
            lblDtAreaK.Location = new Point(0, 60);
            lblDtAreaK.Margin = new Padding(0);
            lblDtAreaK.Name = "lblDtAreaK";
            lblDtAreaK.Size = new Size(116, 20);
            lblDtAreaK.TabIndex = 10;
            lblDtAreaK.Text = "Area";
            lblDtAreaK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtAreaV
            // 
            lblDtAreaV.Dock = DockStyle.Fill;
            lblDtAreaV.Font = new Font("Segoe UI", 8.8F);
            lblDtAreaV.Location = new Point(116, 60);
            lblDtAreaV.Margin = new Padding(0);
            lblDtAreaV.Name = "lblDtAreaV";
            lblDtAreaV.Size = new Size(91, 20);
            lblDtAreaV.TabIndex = 11;
            lblDtAreaV.Text = ": Engine Control";
            lblDtAreaV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtEndK
            // 
            lblDtEndK.Dock = DockStyle.Fill;
            lblDtEndK.Font = new Font("Segoe UI", 8.8F, FontStyle.Bold);
            lblDtEndK.Location = new Point(207, 60);
            lblDtEndK.Margin = new Padding(0);
            lblDtEndK.Name = "lblDtEndK";
            lblDtEndK.Size = new Size(104, 20);
            lblDtEndK.TabIndex = 12;
            lblDtEndK.Text = "End Time";
            lblDtEndK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtEndV
            // 
            lblDtEndV.Dock = DockStyle.Fill;
            lblDtEndV.Font = new Font("Segoe UI", 8.8F);
            lblDtEndV.Location = new Point(311, 60);
            lblDtEndV.Margin = new Padding(0);
            lblDtEndV.Name = "lblDtEndV";
            lblDtEndV.Size = new Size(106, 20);
            lblDtEndV.TabIndex = 13;
            lblDtEndV.Text = ": 10:39:21 AM";
            lblDtEndV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtPartK
            // 
            lblDtPartK.Dock = DockStyle.Fill;
            lblDtPartK.Font = new Font("Segoe UI", 8.8F, FontStyle.Bold);
            lblDtPartK.Location = new Point(0, 80);
            lblDtPartK.Margin = new Padding(0);
            lblDtPartK.Name = "lblDtPartK";
            lblDtPartK.Size = new Size(116, 20);
            lblDtPartK.TabIndex = 14;
            lblDtPartK.Text = "Part Number";
            lblDtPartK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtPartV
            // 
            lblDtPartV.Dock = DockStyle.Fill;
            lblDtPartV.Font = new Font("Segoe UI", 8.8F);
            lblDtPartV.Location = new Point(116, 80);
            lblDtPartV.Margin = new Padding(0);
            lblDtPartV.Name = "lblDtPartV";
            lblDtPartV.Size = new Size(91, 20);
            lblDtPartV.TabIndex = 15;
            lblDtPartV.Text = ": WH-ENG-001";
            lblDtPartV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtResultK
            // 
            lblDtResultK.Dock = DockStyle.Fill;
            lblDtResultK.Font = new Font("Segoe UI", 8.8F, FontStyle.Bold);
            lblDtResultK.Location = new Point(207, 80);
            lblDtResultK.Margin = new Padding(0);
            lblDtResultK.Name = "lblDtResultK";
            lblDtResultK.Size = new Size(104, 20);
            lblDtResultK.TabIndex = 16;
            lblDtResultK.Text = "Result";
            lblDtResultK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtResultV
            // 
            lblDtResultV.Dock = DockStyle.Fill;
            lblDtResultV.Font = new Font("Segoe UI", 8.8F, FontStyle.Bold);
            lblDtResultV.ForeColor = Color.FromArgb(22, 101, 52);
            lblDtResultV.Location = new Point(311, 80);
            lblDtResultV.Margin = new Padding(0);
            lblDtResultV.Name = "lblDtResultV";
            lblDtResultV.Size = new Size(106, 20);
            lblDtResultV.TabIndex = 17;
            lblDtResultV.Text = ": PASS";
            lblDtResultV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtSerialK
            // 
            lblDtSerialK.Dock = DockStyle.Fill;
            lblDtSerialK.Font = new Font("Segoe UI", 8.8F, FontStyle.Bold);
            lblDtSerialK.Location = new Point(0, 100);
            lblDtSerialK.Margin = new Padding(0);
            lblDtSerialK.Name = "lblDtSerialK";
            lblDtSerialK.Size = new Size(116, 20);
            lblDtSerialK.TabIndex = 18;
            lblDtSerialK.Text = "Serial Number";
            lblDtSerialK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtSerialV
            // 
            lblDtSerialV.Dock = DockStyle.Fill;
            lblDtSerialV.Font = new Font("Segoe UI", 8.8F);
            lblDtSerialV.Location = new Point(116, 100);
            lblDtSerialV.Margin = new Padding(0);
            lblDtSerialV.Name = "lblDtSerialV";
            lblDtSerialV.Size = new Size(91, 20);
            lblDtSerialV.TabIndex = 19;
            lblDtSerialV.Text = ": SN12350";
            lblDtSerialV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtEcnK
            // 
            lblDtEcnK.Dock = DockStyle.Fill;
            lblDtEcnK.Font = new Font("Segoe UI", 8.8F, FontStyle.Bold);
            lblDtEcnK.Location = new Point(0, 120);
            lblDtEcnK.Margin = new Padding(0);
            lblDtEcnK.Name = "lblDtEcnK";
            lblDtEcnK.Size = new Size(116, 20);
            lblDtEcnK.TabIndex = 20;
            lblDtEcnK.Text = "ECN No.";
            lblDtEcnK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtEcnV
            // 
            lblDtEcnV.Dock = DockStyle.Fill;
            lblDtEcnV.Font = new Font("Segoe UI", 8.8F);
            lblDtEcnV.Location = new Point(116, 120);
            lblDtEcnV.Margin = new Padding(0);
            lblDtEcnV.Name = "lblDtEcnV";
            lblDtEcnV.Size = new Size(91, 20);
            lblDtEcnV.TabIndex = 21;
            lblDtEcnV.Text = ": 123.R001";
            lblDtEcnV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtOpK
            // 
            lblDtOpK.Dock = DockStyle.Fill;
            lblDtOpK.Font = new Font("Segoe UI", 8.8F, FontStyle.Bold);
            lblDtOpK.Location = new Point(0, 140);
            lblDtOpK.Margin = new Padding(0);
            lblDtOpK.Name = "lblDtOpK";
            lblDtOpK.Size = new Size(116, 22);
            lblDtOpK.TabIndex = 22;
            lblDtOpK.Text = "Operator";
            lblDtOpK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtOpV
            // 
            lblDtOpV.Dock = DockStyle.Fill;
            lblDtOpV.Font = new Font("Segoe UI", 8.8F);
            lblDtOpV.Location = new Point(116, 140);
            lblDtOpV.Margin = new Padding(0);
            lblDtOpV.Name = "lblDtOpV";
            lblDtOpV.Size = new Size(91, 22);
            lblDtOpV.TabIndex = 23;
            lblDtOpV.Text = ": Fally";
            lblDtOpV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // flpDetailFooter
            // 
            flpDetailFooter.Controls.Add(btnViewReport);
            flpDetailFooter.Controls.Add(btnDeleteHistory);
            flpDetailFooter.Dock = DockStyle.Fill;
            flpDetailFooter.FlowDirection = FlowDirection.RightToLeft;
            flpDetailFooter.Location = new Point(0, 183);
            flpDetailFooter.Margin = new Padding(0);
            flpDetailFooter.Name = "flpDetailFooter";
            flpDetailFooter.Size = new Size(417, 24);
            flpDetailFooter.TabIndex = 2;
            // 
            // btnViewReport
            // 
            btnViewReport.BackColor = Color.FromArgb(250, 204, 21);
            btnViewReport.Cursor = Cursors.Hand;
            btnViewReport.FlatAppearance.BorderSize = 0;
            btnViewReport.FlatStyle = FlatStyle.Flat;
            btnViewReport.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnViewReport.ForeColor = Color.FromArgb(15, 23, 42);
            btnViewReport.Location = new Point(231, 0);
            btnViewReport.Margin = new Padding(0);
            btnViewReport.Name = "btnViewReport";
            btnViewReport.Size = new Size(186, 24);
            btnViewReport.TabIndex = 0;
            btnViewReport.Text = "View Test Report";
            btnViewReport.UseVisualStyleBackColor = false;
            // 
            // btnDeleteHistory
            // 
            btnDeleteHistory.BackColor = Color.FromArgb(254, 242, 242);
            btnDeleteHistory.Cursor = Cursors.Hand;
            btnDeleteHistory.Enabled = false;
            btnDeleteHistory.FlatAppearance.BorderColor = Color.FromArgb(254, 202, 202);
            btnDeleteHistory.FlatStyle = FlatStyle.Flat;
            btnDeleteHistory.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDeleteHistory.ForeColor = Color.FromArgb(239, 68, 68);
            btnDeleteHistory.Location = new Point(109, 0);
            btnDeleteHistory.Margin = new Padding(0, 0, 4, 0);
            btnDeleteHistory.Name = "btnDeleteHistory";
            btnDeleteHistory.Size = new Size(118, 24);
            btnDeleteHistory.TabIndex = 1;
            btnDeleteHistory.Text = "🗑 Delete History";
            btnDeleteHistory.UseVisualStyleBackColor = false;
            btnDeleteHistory.Click += BtnDeleteHistory_Click;
            // 
            // pnlSummary
            // 
            pnlSummary.BackColor = Color.White;
            pnlSummary.Controls.Add(tblSummaryContent);
            pnlSummary.Dock = DockStyle.Fill;
            pnlSummary.Location = new Point(0, 228);
            pnlSummary.Margin = new Padding(0, 0, 0, 9);
            pnlSummary.Name = "pnlSummary";
            pnlSummary.Padding = new Padding(9, 6, 9, 6);
            pnlSummary.Size = new Size(435, 134);
            pnlSummary.TabIndex = 1;
            // 
            // tblSummaryContent
            // 
            tblSummaryContent.ColumnCount = 1;
            tblSummaryContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblSummaryContent.Controls.Add(lblSummaryTitle, 0, 0);
            tblSummaryContent.Controls.Add(tblSummaryBody, 0, 1);
            tblSummaryContent.Dock = DockStyle.Fill;
            tblSummaryContent.Location = new Point(9, 6);
            tblSummaryContent.Margin = new Padding(0);
            tblSummaryContent.Name = "tblSummaryContent";
            tblSummaryContent.RowCount = 2;
            tblSummaryContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 21F));
            tblSummaryContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblSummaryContent.Size = new Size(417, 122);
            tblSummaryContent.TabIndex = 0;
            // 
            // lblSummaryTitle
            // 
            lblSummaryTitle.Dock = DockStyle.Fill;
            lblSummaryTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblSummaryTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblSummaryTitle.Location = new Point(0, 0);
            lblSummaryTitle.Margin = new Padding(0);
            lblSummaryTitle.Name = "lblSummaryTitle";
            lblSummaryTitle.Size = new Size(417, 21);
            lblSummaryTitle.TabIndex = 0;
            lblSummaryTitle.Text = "TEST SUMMARY";
            lblSummaryTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tblSummaryBody
            // 
            tblSummaryBody.ColumnCount = 2;
            tblSummaryBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tblSummaryBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tblSummaryBody.Controls.Add(pnlChartContainer, 0, 0);
            tblSummaryBody.Controls.Add(tblSummaryLegend, 1, 0);
            tblSummaryBody.Dock = DockStyle.Fill;
            tblSummaryBody.Location = new Point(0, 21);
            tblSummaryBody.Margin = new Padding(0);
            tblSummaryBody.Name = "tblSummaryBody";
            tblSummaryBody.RowCount = 1;
            tblSummaryBody.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblSummaryBody.Size = new Size(417, 101);
            tblSummaryBody.TabIndex = 1;
            // 
            // pnlChartContainer
            // 
            pnlChartContainer.Dock = DockStyle.Fill;
            pnlChartContainer.Location = new Point(0, 0);
            pnlChartContainer.Margin = new Padding(0);
            pnlChartContainer.Name = "pnlChartContainer";
            pnlChartContainer.Size = new Size(310, 216);
            pnlChartContainer.TabIndex = 0;
            // 
            // tblSummaryLegend
            // 
            tblSummaryLegend.ColumnCount = 2;
            tblSummaryLegend.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 27F));
            tblSummaryLegend.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblSummaryLegend.Controls.Add(lblLegPassIcon, 0, 0);
            tblSummaryLegend.Controls.Add(lblLegPassText, 1, 0);
            tblSummaryLegend.Controls.Add(lblLegFailIcon, 0, 1);
            tblSummaryLegend.Controls.Add(lblLegFailText, 1, 1);
            tblSummaryLegend.Controls.Add(lblLegProgIcon, 0, 2);
            tblSummaryLegend.Controls.Add(lblLegProgText, 1, 2);
            tblSummaryLegend.Controls.Add(lblLegStopIcon, 0, 3);
            tblSummaryLegend.Controls.Add(lblLegStopText, 1, 3);
            tblSummaryLegend.Dock = DockStyle.Fill;
            tblSummaryLegend.Location = new Point(171, 0);
            tblSummaryLegend.Margin = new Padding(5, 0, 0, 0);
            tblSummaryLegend.Name = "tblSummaryLegend";
            tblSummaryLegend.RowCount = 4;
            tblSummaryLegend.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tblSummaryLegend.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tblSummaryLegend.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tblSummaryLegend.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tblSummaryLegend.Size = new Size(246, 101);
            tblSummaryLegend.TabIndex = 1;
            // 
            // lblLegPassIcon
            // 
            lblLegPassIcon.Dock = DockStyle.Fill;
            lblLegPassIcon.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblLegPassIcon.ForeColor = Color.FromArgb(76, 175, 80);
            lblLegPassIcon.Location = new Point(0, 0);
            lblLegPassIcon.Margin = new Padding(0);
            lblLegPassIcon.Name = "lblLegPassIcon";
            lblLegPassIcon.Size = new Size(27, 25);
            lblLegPassIcon.TabIndex = 0;
            lblLegPassIcon.Text = "●";
            lblLegPassIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblLegPassText
            // 
            lblLegPassText.Dock = DockStyle.Fill;
            lblLegPassText.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblLegPassText.ForeColor = Color.FromArgb(30, 41, 59);
            lblLegPassText.Location = new Point(27, 0);
            lblLegPassText.Margin = new Padding(0);
            lblLegPassText.Name = "lblLegPassText";
            lblLegPassText.Size = new Size(219, 25);
            lblLegPassText.TabIndex = 1;
            lblLegPassText.Text = "Pass : 4 (80.00%)";
            lblLegPassText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblLegFailIcon
            // 
            lblLegFailIcon.Dock = DockStyle.Fill;
            lblLegFailIcon.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblLegFailIcon.ForeColor = Color.FromArgb(244, 67, 54);
            lblLegFailIcon.Location = new Point(0, 25);
            lblLegFailIcon.Margin = new Padding(0);
            lblLegFailIcon.Name = "lblLegFailIcon";
            lblLegFailIcon.Size = new Size(27, 25);
            lblLegFailIcon.TabIndex = 2;
            lblLegFailIcon.Text = "●";
            lblLegFailIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblLegFailText
            // 
            lblLegFailText.Dock = DockStyle.Fill;
            lblLegFailText.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblLegFailText.ForeColor = Color.FromArgb(30, 41, 59);
            lblLegFailText.Location = new Point(27, 25);
            lblLegFailText.Margin = new Padding(0);
            lblLegFailText.Name = "lblLegFailText";
            lblLegFailText.Size = new Size(219, 25);
            lblLegFailText.TabIndex = 3;
            lblLegFailText.Text = "Fail : 1 (20.00%)";
            lblLegFailText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblLegProgIcon
            // 
            lblLegProgIcon.Dock = DockStyle.Fill;
            lblLegProgIcon.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblLegProgIcon.ForeColor = Color.FromArgb(255, 193, 7);
            lblLegProgIcon.Location = new Point(0, 50);
            lblLegProgIcon.Margin = new Padding(0);
            lblLegProgIcon.Name = "lblLegProgIcon";
            lblLegProgIcon.Size = new Size(27, 25);
            lblLegProgIcon.TabIndex = 4;
            lblLegProgIcon.Text = "●";
            lblLegProgIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblLegProgText
            // 
            lblLegProgText.Dock = DockStyle.Fill;
            lblLegProgText.Font = new Font("Segoe UI", 9F);
            lblLegProgText.ForeColor = Color.FromArgb(30, 41, 59);
            lblLegProgText.Location = new Point(27, 50);
            lblLegProgText.Margin = new Padding(0);
            lblLegProgText.Name = "lblLegProgText";
            lblLegProgText.Size = new Size(219, 25);
            lblLegProgText.TabIndex = 5;
            lblLegProgText.Text = "In Progress : 0 (0.00%)";
            lblLegProgText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblLegStopIcon
            // 
            lblLegStopIcon.Dock = DockStyle.Fill;
            lblLegStopIcon.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblLegStopIcon.ForeColor = Color.FromArgb(158, 158, 158);
            lblLegStopIcon.Location = new Point(0, 75);
            lblLegStopIcon.Margin = new Padding(0);
            lblLegStopIcon.Name = "lblLegStopIcon";
            lblLegStopIcon.Size = new Size(27, 26);
            lblLegStopIcon.TabIndex = 6;
            lblLegStopIcon.Text = "●";
            lblLegStopIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblLegStopText
            // 
            lblLegStopText.Dock = DockStyle.Fill;
            lblLegStopText.Font = new Font("Segoe UI", 9F);
            lblLegStopText.ForeColor = Color.FromArgb(30, 41, 59);
            lblLegStopText.Location = new Point(27, 75);
            lblLegStopText.Margin = new Padding(0);
            lblLegStopText.Name = "lblLegStopText";
            lblLegStopText.Size = new Size(219, 26);
            lblLegStopText.TabIndex = 7;
            lblLegStopText.Text = "Stop : 0 (0.00%)";
            lblLegStopText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlSteps
            // 
            pnlSteps.BackColor = Color.White;
            pnlSteps.Controls.Add(tblStepsContent);
            pnlSteps.Dock = DockStyle.Fill;
            pnlSteps.Location = new Point(0, 371);
            pnlSteps.Margin = new Padding(0);
            pnlSteps.Name = "pnlSteps";
            pnlSteps.Padding = new Padding(9, 6, 9, 6);
            pnlSteps.Size = new Size(435, 201);
            pnlSteps.TabIndex = 2;
            // 
            // tblStepsContent
            // 
            tblStepsContent.ColumnCount = 1;
            tblStepsContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblStepsContent.Controls.Add(lblStepsTitle, 0, 0);
            tblStepsContent.Controls.Add(gridSteps, 0, 1);
            tblStepsContent.Dock = DockStyle.Fill;
            tblStepsContent.Location = new Point(9, 6);
            tblStepsContent.Margin = new Padding(0);
            tblStepsContent.Name = "tblStepsContent";
            tblStepsContent.RowCount = 2;
            tblStepsContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 21F));
            tblStepsContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblStepsContent.Size = new Size(417, 189);
            tblStepsContent.TabIndex = 0;
            // 
            // lblStepsTitle
            // 
            lblStepsTitle.Dock = DockStyle.Fill;
            lblStepsTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblStepsTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblStepsTitle.Location = new Point(0, 0);
            lblStepsTitle.Margin = new Padding(0);
            lblStepsTitle.Name = "lblStepsTitle";
            lblStepsTitle.Size = new Size(417, 21);
            lblStepsTitle.TabIndex = 0;
            lblStepsTitle.Text = "TEST STEPS RESULT";
            lblStepsTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // gridSteps
            // 
            gridSteps.AllowUserToAddRows = false;
            gridSteps.AllowUserToDeleteRows = false;
            gridSteps.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridSteps.BackgroundColor = Color.White;
            gridSteps.BorderStyle = BorderStyle.None;
            gridSteps.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridSteps.Dock = DockStyle.Fill;
            gridSteps.Location = new Point(0, 21);
            gridSteps.Margin = new Padding(0);
            gridSteps.Name = "gridSteps";
            gridSteps.ReadOnly = true;
            gridSteps.RowHeadersVisible = false;
            gridSteps.RowHeadersWidth = 51;
            gridSteps.Size = new Size(417, 168);
            gridSteps.TabIndex = 1;
            // 
            // TestHistory
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 245, 247);
            Controls.Add(mainLayout);
            Controls.Add(pnlTop);
            Margin = new Padding(9, 8, 9, 8);
            Name = "TestHistory";
            Size = new Size(1225, 712);
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            pnlFilterCard.ResumeLayout(false);
            tblFilterInner.ResumeLayout(false);
            tblFilterInner.PerformLayout();
            tblFilterInputs.ResumeLayout(false);
            tblFilterInputs.PerformLayout();
            tblDateRange.ResumeLayout(false);
            tblDateRange.PerformLayout();
            flpActionButtons.ResumeLayout(false);
            mainLayout.ResumeLayout(false);
            pnlLeft.ResumeLayout(false);
            tblLeftContent.ResumeLayout(false);
            tblLeftHeader.ResumeLayout(false);
            flpLeftButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridData).EndInit();
            tblPagination.ResumeLayout(false);
            flpPageInfo.ResumeLayout(false);
            flpPageInfo.PerformLayout();
            rightLayout.ResumeLayout(false);
            pnlDetail.ResumeLayout(false);
            tblDetailContent.ResumeLayout(false);
            tblDetailHeader.ResumeLayout(false);
            tblDetailBody.ResumeLayout(false);
            flpDetailFooter.ResumeLayout(false);
            pnlSummary.ResumeLayout(false);
            tblSummaryContent.ResumeLayout(false);
            tblSummaryBody.ResumeLayout(false);
            tblSummaryLegend.ResumeLayout(false);
            pnlSteps.ResumeLayout(false);
            tblStepsContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridSteps).EndInit();
            ResumeLayout(false);

        }



        #endregion



        private System.Windows.Forms.Panel pnlTop;

        private System.Windows.Forms.Label lblTitle;



        // Deklarasi Komponen Filter

        private System.Windows.Forms.Label lblDateRange;

        private System.Windows.Forms.DateTimePicker dtpStart;

        private System.Windows.Forms.Label lblDash;

        private System.Windows.Forms.DateTimePicker dtpEnd;

        private System.Windows.Forms.Label lblProductModel;

        private System.Windows.Forms.ComboBox cmbProductModel;

        private System.Windows.Forms.Label lblArea;

        private System.Windows.Forms.ComboBox cmbArea;

        private System.Windows.Forms.Label lblResult;

        private System.Windows.Forms.ComboBox cmbResult;

        private System.Windows.Forms.Label lblOperator;

        private System.Windows.Forms.TextBox txtOperator;

        private System.Windows.Forms.Button btnSearch;

        private System.Windows.Forms.Button btnReset;



        // Deklarasi Tambahan (Panel Kiri & Kanan)

        private System.Windows.Forms.TableLayoutPanel mainLayout;

        private System.Windows.Forms.Panel pnlLeft;

        private System.Windows.Forms.Button btnExport;

        private System.Windows.Forms.Button btnRefresh;

        private System.Windows.Forms.Label lblTableTitle;

        private System.Windows.Forms.DataGridView gridData;

        private System.Windows.Forms.TableLayoutPanel tblLeftContent;

        private System.Windows.Forms.TableLayoutPanel tblLeftHeader;

        private System.Windows.Forms.FlowLayoutPanel flpLeftButtons;

        private System.Windows.Forms.TableLayoutPanel tblPagination;

        private System.Windows.Forms.FlowLayoutPanel flpPageNav;

        private System.Windows.Forms.FlowLayoutPanel flpPageInfo;

        private System.Windows.Forms.TextBox txtGoToPage;

        private System.Windows.Forms.Label lblGoTo;

        private System.Windows.Forms.Label lblEntries;

        private System.Windows.Forms.ComboBox cmbPageSize;

        private System.Windows.Forms.Label lblShow;

        private System.Windows.Forms.TableLayoutPanel rightLayout;

        private System.Windows.Forms.Panel pnlDetail;

        private System.Windows.Forms.TableLayoutPanel tblDetailContent;

        private System.Windows.Forms.TableLayoutPanel tblDetailHeader;

        private System.Windows.Forms.Label lblDetailTitle;

        private System.Windows.Forms.Label lblDetailBadge;

        private System.Windows.Forms.TableLayoutPanel tblDetailBody;

        private System.Windows.Forms.Label lblDtTestTimeK;

        private System.Windows.Forms.Label lblDtTestTimeV;

        private System.Windows.Forms.Label lblDtModelK;

        private System.Windows.Forms.Label lblDtModelV;

        private System.Windows.Forms.Label lblDtTotalK;

        private System.Windows.Forms.Label lblDtTotalV;

        private System.Windows.Forms.Label lblDtTypeK;

        private System.Windows.Forms.Label lblDtTypeV;

        private System.Windows.Forms.Label lblDtStartK;

        private System.Windows.Forms.Label lblDtStartV;

        private System.Windows.Forms.Label lblDtAreaK;

        private System.Windows.Forms.Label lblDtAreaV;

        private System.Windows.Forms.Label lblDtEndK;

        private System.Windows.Forms.Label lblDtEndV;

        private System.Windows.Forms.Label lblDtPartK;

        private System.Windows.Forms.Label lblDtPartV;

        private System.Windows.Forms.Label lblDtResultK;

        private System.Windows.Forms.Label lblDtResultV;

        private System.Windows.Forms.Label lblDtSerialK;

        private System.Windows.Forms.Label lblDtSerialV;

        private System.Windows.Forms.Label lblDtEcnK;

        private System.Windows.Forms.Label lblDtEcnV;

        private System.Windows.Forms.Label lblDtOpK;

        private System.Windows.Forms.Label lblDtOpV;

        private System.Windows.Forms.FlowLayoutPanel flpDetailFooter;

        private System.Windows.Forms.Button btnViewReport;
        private System.Windows.Forms.Button btnDeleteHistory;

        private System.Windows.Forms.Panel pnlSummary;

        private System.Windows.Forms.TableLayoutPanel tblSummaryContent;

        private System.Windows.Forms.Label lblSummaryTitle;

        private System.Windows.Forms.TableLayoutPanel tblSummaryBody;

        private System.Windows.Forms.Panel pnlChartContainer;

        private System.Windows.Forms.TableLayoutPanel tblSummaryLegend;

        private System.Windows.Forms.Label lblLegPassIcon;

        private System.Windows.Forms.Label lblLegPassText;

        private System.Windows.Forms.Label lblLegFailIcon;

        private System.Windows.Forms.Label lblLegFailText;

        private System.Windows.Forms.Label lblLegProgIcon;

        private System.Windows.Forms.Label lblLegProgText;

        private System.Windows.Forms.Label lblLegStopIcon;

        private System.Windows.Forms.Label lblLegStopText;

        private System.Windows.Forms.Panel pnlSteps;

        private System.Windows.Forms.TableLayoutPanel tblStepsContent;

        private System.Windows.Forms.Label lblStepsTitle;

        private System.Windows.Forms.DataGridView gridSteps;

        private System.Windows.Forms.Panel pnlFilterCard;
        private System.Windows.Forms.TableLayoutPanel tblFilterInner;
        private System.Windows.Forms.TableLayoutPanel tblFilterInputs;
        private System.Windows.Forms.TableLayoutPanel tblDateRange;
        private System.Windows.Forms.FlowLayoutPanel flpActionButtons;

    }

}