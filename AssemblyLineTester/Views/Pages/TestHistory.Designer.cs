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
            logoBox = new PictureBox();
            lblTitle = new Label();
            lblDateRange = new Label();
            dtpStart = new DateTimePicker();
            lblDash = new Label();
            dtpEnd = new DateTimePicker();
            lblProductModel = new Label();
            cmbProductModel = new ComboBox();
            lblProductType = new Label();
            cmbProductType = new ComboBox();
            lblArea = new Label();
            cmbArea = new ComboBox();
            lblResult = new Label();
            cmbResult = new ComboBox();
            lblOperator = new Label();
            cmbOperator = new ComboBox();
            btnSearch = new Button();
            btnReset = new Button();
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
            btnFirst = new Button();
            btnPrev = new Button();
            btnP1 = new Button();
            btnP2 = new Button();
            btnP3 = new Button();
            btnP4 = new Button();
            btnP5 = new Button();
            lblDots = new Label();
            btnP22 = new Button();
            btnNext = new Button();
            btnLast = new Button();
            flpPageInfo = new FlowLayoutPanel();
            lblShow = new Label();
            lblEntries = new Label();
            txtGoToPage = new TextBox();
            cmbPageSize = new ComboBox();
            lblGoTo = new Label();
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
            ((System.ComponentModel.ISupportInitialize)logoBox).BeginInit();
            mainLayout.SuspendLayout();
            pnlLeft.SuspendLayout();
            tblLeftContent.SuspendLayout();
            tblLeftHeader.SuspendLayout();
            flpLeftButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridData).BeginInit();
            tblPagination.SuspendLayout();
            flpPageNav.SuspendLayout();
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
            pnlTop.BackColor = Color.White;
            pnlTop.Controls.Add(logoBox);
            pnlTop.Controls.Add(lblTitle);
            pnlTop.Controls.Add(lblDateRange);
            pnlTop.Controls.Add(dtpStart);
            pnlTop.Controls.Add(lblDash);
            pnlTop.Controls.Add(dtpEnd);
            pnlTop.Controls.Add(lblProductModel);
            pnlTop.Controls.Add(cmbProductModel);
            pnlTop.Controls.Add(lblProductType);
            pnlTop.Controls.Add(cmbProductType);
            pnlTop.Controls.Add(lblArea);
            pnlTop.Controls.Add(cmbArea);
            pnlTop.Controls.Add(lblResult);
            pnlTop.Controls.Add(cmbResult);
            pnlTop.Controls.Add(lblOperator);
            pnlTop.Controls.Add(cmbOperator);
            pnlTop.Controls.Add(btnSearch);
            pnlTop.Controls.Add(btnReset);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Margin = new Padding(5);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(3726, 339);
            pnlTop.TabIndex = 0;
            // 
            // logoBox
            // 
            logoBox.BackColor = Color.LightGray;
            logoBox.Location = new Point(65, 32);
            logoBox.Margin = new Padding(5);
            logoBox.Name = "logoBox";
            logoBox.Size = new Size(73, 72);
            logoBox.TabIndex = 0;
            logoBox.TabStop = false;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(162, 37);
            lblTitle.Margin = new Padding(5, 0, 5, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(315, 59);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "TEST HISTORY";
            // 
            // lblDateRange
            // 
            lblDateRange.AutoSize = true;
            lblDateRange.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDateRange.Location = new Point(65, 154);
            lblDateRange.Margin = new Padding(5, 0, 5, 0);
            lblDateRange.Name = "lblDateRange";
            lblDateRange.Size = new Size(146, 32);
            lblDateRange.TabIndex = 2;
            lblDateRange.Text = "Date Range";
            // 
            // dtpStart
            // 
            dtpStart.Format = DateTimePickerFormat.Short;
            dtpStart.Location = new Point(65, 221);
            dtpStart.Margin = new Padding(5);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(350, 39);
            dtpStart.TabIndex = 3;
            // 
            // lblDash
            // 
            lblDash.AutoSize = true;
            lblDash.Location = new Point(447, 219);
            lblDash.Margin = new Padding(5, 0, 5, 0);
            lblDash.Name = "lblDash";
            lblDash.Size = new Size(24, 32);
            lblDash.TabIndex = 4;
            lblDash.Text = "-";
            // 
            // dtpEnd
            // 
            dtpEnd.Format = DateTimePickerFormat.Short;
            dtpEnd.Location = new Point(514, 219);
            dtpEnd.Margin = new Padding(5);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(368, 39);
            dtpEnd.TabIndex = 5;
            // 
            // lblProductModel
            // 
            lblProductModel.AutoSize = true;
            lblProductModel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblProductModel.Location = new Point(967, 154);
            lblProductModel.Margin = new Padding(5, 0, 5, 0);
            lblProductModel.Name = "lblProductModel";
            lblProductModel.Size = new Size(185, 32);
            lblProductModel.TabIndex = 6;
            lblProductModel.Text = "Product Model";
            // 
            // cmbProductModel
            // 
            cmbProductModel.FormattingEnabled = true;
            cmbProductModel.Items.AddRange(new object[] { "All", "6060" });
            cmbProductModel.Location = new Point(967, 219);
            cmbProductModel.Margin = new Padding(5);
            cmbProductModel.Name = "cmbProductModel";
            cmbProductModel.Size = new Size(290, 40);
            cmbProductModel.TabIndex = 7;
            cmbProductModel.Text = "All";
            // 
            // lblProductType
            // 
            lblProductType.AutoSize = true;
            lblProductType.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblProductType.Location = new Point(1324, 154);
            lblProductType.Margin = new Padding(5, 0, 5, 0);
            lblProductType.Name = "lblProductType";
            lblProductType.Size = new Size(166, 32);
            lblProductType.TabIndex = 8;
            lblProductType.Text = "Product Type";
            // 
            // cmbProductType
            // 
            cmbProductType.FormattingEnabled = true;
            cmbProductType.Items.AddRange(new object[] { "All", "AC" });
            cmbProductType.Location = new Point(1324, 219);
            cmbProductType.Margin = new Padding(5);
            cmbProductType.Name = "cmbProductType";
            cmbProductType.Size = new Size(290, 40);
            cmbProductType.TabIndex = 9;
            cmbProductType.Text = "All";
            // 
            // lblArea
            // 
            lblArea.AutoSize = true;
            lblArea.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblArea.Location = new Point(1682, 154);
            lblArea.Margin = new Padding(5, 0, 5, 0);
            lblArea.Name = "lblArea";
            lblArea.Size = new Size(67, 32);
            lblArea.TabIndex = 10;
            lblArea.Text = "Area";
            // 
            // cmbArea
            // 
            cmbArea.FormattingEnabled = true;
            cmbArea.Items.AddRange(new object[] { "All", "Engine Control" });
            cmbArea.Location = new Point(1682, 218);
            cmbArea.Margin = new Padding(5);
            cmbArea.Name = "cmbArea";
            cmbArea.Size = new Size(306, 40);
            cmbArea.TabIndex = 11;
            cmbArea.Text = "All";
            // 
            // lblResult
            // 
            lblResult.AutoSize = true;
            lblResult.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblResult.Location = new Point(2039, 154);
            lblResult.Margin = new Padding(5, 0, 5, 0);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(84, 32);
            lblResult.TabIndex = 12;
            lblResult.Text = "Result";
            // 
            // cmbResult
            // 
            cmbResult.FormattingEnabled = true;
            cmbResult.Items.AddRange(new object[] { "All", "PASS", "FAIL" });
            cmbResult.Location = new Point(2039, 219);
            cmbResult.Margin = new Padding(5);
            cmbResult.Name = "cmbResult";
            cmbResult.Size = new Size(290, 40);
            cmbResult.TabIndex = 13;
            cmbResult.Text = "All";
            // 
            // lblOperator
            // 
            lblOperator.AutoSize = true;
            lblOperator.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblOperator.Location = new Point(2363, 154);
            lblOperator.Margin = new Padding(5, 0, 5, 0);
            lblOperator.Name = "lblOperator";
            lblOperator.Size = new Size(117, 32);
            lblOperator.TabIndex = 14;
            lblOperator.Text = "Operator";
            // 
            // cmbOperator
            // 
            cmbOperator.FormattingEnabled = true;
            cmbOperator.Items.AddRange(new object[] { "All", "Fally" });
            cmbOperator.Location = new Point(2363, 218);
            cmbOperator.Margin = new Padding(5);
            cmbOperator.Name = "cmbOperator";
            cmbOperator.Size = new Size(290, 40);
            cmbOperator.TabIndex = 15;
            cmbOperator.Text = "All";
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(255, 193, 7);
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSearch.Location = new Point(3097, 210);
            btnSearch.Margin = new Padding(5);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(228, 80);
            btnSearch.TabIndex = 16;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = false;
            // 
            // btnReset
            // 
            btnReset.BackColor = Color.White;
            btnReset.FlatAppearance.BorderColor = Color.LightGray;
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnReset.Location = new Point(3349, 206);
            btnReset.Margin = new Padding(5);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(195, 80);
            btnReset.TabIndex = 17;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = false;
            // 
            // mainLayout
            // 
            mainLayout.ColumnCount = 2;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65.63481F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34.3651848F));
            mainLayout.Controls.Add(pnlLeft, 0, 0);
            mainLayout.Controls.Add(rightLayout, 1, 0);
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Location = new Point(0, 339);
            mainLayout.Margin = new Padding(5);
            mainLayout.Name = "mainLayout";
            mainLayout.Padding = new Padding(10, 12, 10, 12);
            mainLayout.RowCount = 1;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.Size = new Size(3726, 2634);
            mainLayout.TabIndex = 1;
            // 
            // pnlLeft
            // 
            pnlLeft.BackColor = Color.White;
            pnlLeft.Controls.Add(tblLeftContent);
            pnlLeft.Dock = DockStyle.Fill;
            pnlLeft.Location = new Point(28, 31);
            pnlLeft.Margin = new Padding(18, 19, 18, 19);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Size = new Size(2396, 2572);
            pnlLeft.TabIndex = 0;
            // 
            // tblLeftContent
            // 
            tblLeftContent.BackColor = Color.White;
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
            tblLeftContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 96F));
            tblLeftContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblLeftContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 96F));
            tblLeftContent.Size = new Size(2396, 2572);
            tblLeftContent.TabIndex = 0;
            // 
            // tblLeftHeader
            // 
            tblLeftHeader.ColumnCount = 2;
            tblLeftHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblLeftHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblLeftHeader.Controls.Add(lblTableTitle, 0, 0);
            tblLeftHeader.Controls.Add(flpLeftButtons, 1, 0);
            tblLeftHeader.Dock = DockStyle.Fill;
            tblLeftHeader.Location = new Point(0, 0);
            tblLeftHeader.Margin = new Padding(0);
            tblLeftHeader.Name = "tblLeftHeader";
            tblLeftHeader.Padding = new Padding(26, 0, 26, 0);
            tblLeftHeader.RowCount = 1;
            tblLeftHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblLeftHeader.Size = new Size(2396, 96);
            tblLeftHeader.TabIndex = 0;
            // 
            // lblTableTitle
            // 
            lblTableTitle.Dock = DockStyle.Fill;
            lblTableTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTableTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblTableTitle.Location = new Point(31, 8);
            lblTableTitle.Margin = new Padding(5, 8, 5, 8);
            lblTableTitle.Name = "lblTableTitle";
            lblTableTitle.Size = new Size(1162, 80);
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
            flpLeftButtons.Location = new Point(1198, 0);
            flpLeftButtons.Margin = new Padding(0);
            flpLeftButtons.Name = "flpLeftButtons";
            flpLeftButtons.Size = new Size(1172, 96);
            flpLeftButtons.TabIndex = 1;
            flpLeftButtons.WrapContents = false;
            // 
            // btnExport
            // 
            btnExport.BackColor = Color.White;
            btnExport.Cursor = Cursors.Hand;
            btnExport.FlatAppearance.BorderColor = Color.LightGray;
            btnExport.FlatStyle = FlatStyle.Flat;
            btnExport.Font = new Font("Segoe UI", 9.5F);
            btnExport.ForeColor = Color.FromArgb(30, 41, 59);
            btnExport.Location = new Point(826, 8);
            btnExport.Margin = new Padding(0, 8, 0, 16);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(346, 63);
            btnExport.TabIndex = 1;
            btnExport.Text = "Export";
            btnExport.UseVisualStyleBackColor = false;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.White;
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.FlatAppearance.BorderColor = Color.LightGray;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9.5F);
            btnRefresh.ForeColor = Color.FromArgb(30, 41, 59);
            btnRefresh.Location = new Point(533, 8);
            btnRefresh.Margin = new Padding(0, 8, 0, 16);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(293, 63);
            btnRefresh.TabIndex = 0;
            btnRefresh.Text = "Refresh";
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
            gridData.Location = new Point(26, 96);
            gridData.Margin = new Padding(26, 0, 26, 0);
            gridData.Name = "gridData";
            gridData.ReadOnly = true;
            gridData.RowHeadersVisible = false;
            gridData.RowHeadersWidth = 51;
            gridData.Size = new Size(2344, 2380);
            gridData.TabIndex = 1;
            // 
            // tblPagination
            // 
            tblPagination.ColumnCount = 2;
            tblPagination.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tblPagination.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tblPagination.Controls.Add(flpPageNav, 0, 0);
            tblPagination.Controls.Add(flpPageInfo, 1, 0);
            tblPagination.Dock = DockStyle.Fill;
            tblPagination.Location = new Point(0, 2476);
            tblPagination.Margin = new Padding(0);
            tblPagination.Name = "tblPagination";
            tblPagination.Padding = new Padding(26, 0, 26, 0);
            tblPagination.RowCount = 1;
            tblPagination.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblPagination.Size = new Size(2396, 96);
            tblPagination.TabIndex = 2;
            // 
            // flpPageNav
            // 
            flpPageNav.Controls.Add(btnFirst);
            flpPageNav.Controls.Add(btnPrev);
            flpPageNav.Controls.Add(btnP1);
            flpPageNav.Controls.Add(btnP2);
            flpPageNav.Controls.Add(btnP3);
            flpPageNav.Controls.Add(btnP4);
            flpPageNav.Controls.Add(btnP5);
            flpPageNav.Controls.Add(lblDots);
            flpPageNav.Controls.Add(btnP22);
            flpPageNav.Controls.Add(btnNext);
            flpPageNav.Controls.Add(btnLast);
            flpPageNav.Dock = DockStyle.Fill;
            flpPageNav.Location = new Point(26, 0);
            flpPageNav.Margin = new Padding(0);
            flpPageNav.Name = "flpPageNav";
            flpPageNav.Size = new Size(1406, 96);
            flpPageNav.TabIndex = 0;
            flpPageNav.WrapContents = false;
            // 
            // btnFirst
            // 
            btnFirst.BackColor = Color.White;
            btnFirst.Cursor = Cursors.Hand;
            btnFirst.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            btnFirst.FlatStyle = FlatStyle.Flat;
            btnFirst.Font = new Font("Segoe UI", 9F);
            btnFirst.ForeColor = Color.FromArgb(71, 85, 105);
            btnFirst.Location = new Point(0, 19);
            btnFirst.Margin = new Padding(0, 19, 6, 19);
            btnFirst.Name = "btnFirst";
            btnFirst.Size = new Size(58, 66);
            btnFirst.TabIndex = 0;
            btnFirst.Text = "|<";
            btnFirst.UseVisualStyleBackColor = false;
            // 
            // btnPrev
            // 
            btnPrev.BackColor = Color.White;
            btnPrev.Cursor = Cursors.Hand;
            btnPrev.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            btnPrev.FlatStyle = FlatStyle.Flat;
            btnPrev.Font = new Font("Segoe UI", 9F);
            btnPrev.ForeColor = Color.FromArgb(71, 85, 105);
            btnPrev.Location = new Point(64, 19);
            btnPrev.Margin = new Padding(0, 19, 6, 19);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(58, 66);
            btnPrev.TabIndex = 1;
            btnPrev.Text = "<";
            btnPrev.UseVisualStyleBackColor = false;
            // 
            // btnP1
            // 
            btnP1.BackColor = Color.FromArgb(255, 193, 7);
            btnP1.Cursor = Cursors.Hand;
            btnP1.FlatAppearance.BorderSize = 0;
            btnP1.FlatStyle = FlatStyle.Flat;
            btnP1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnP1.ForeColor = Color.Black;
            btnP1.Location = new Point(128, 19);
            btnP1.Margin = new Padding(0, 19, 6, 19);
            btnP1.Name = "btnP1";
            btnP1.Size = new Size(58, 66);
            btnP1.TabIndex = 2;
            btnP1.Text = "1";
            btnP1.UseVisualStyleBackColor = false;
            // 
            // btnP2
            // 
            btnP2.BackColor = Color.White;
            btnP2.Cursor = Cursors.Hand;
            btnP2.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            btnP2.FlatStyle = FlatStyle.Flat;
            btnP2.Font = new Font("Segoe UI", 9F);
            btnP2.ForeColor = Color.FromArgb(71, 85, 105);
            btnP2.Location = new Point(192, 19);
            btnP2.Margin = new Padding(0, 19, 6, 19);
            btnP2.Name = "btnP2";
            btnP2.Size = new Size(58, 66);
            btnP2.TabIndex = 3;
            btnP2.Text = "2";
            btnP2.UseVisualStyleBackColor = false;
            // 
            // btnP3
            // 
            btnP3.BackColor = Color.White;
            btnP3.Cursor = Cursors.Hand;
            btnP3.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            btnP3.FlatStyle = FlatStyle.Flat;
            btnP3.Font = new Font("Segoe UI", 9F);
            btnP3.ForeColor = Color.FromArgb(71, 85, 105);
            btnP3.Location = new Point(256, 19);
            btnP3.Margin = new Padding(0, 19, 6, 19);
            btnP3.Name = "btnP3";
            btnP3.Size = new Size(58, 66);
            btnP3.TabIndex = 4;
            btnP3.Text = "3";
            btnP3.UseVisualStyleBackColor = false;
            // 
            // btnP4
            // 
            btnP4.BackColor = Color.White;
            btnP4.Cursor = Cursors.Hand;
            btnP4.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            btnP4.FlatStyle = FlatStyle.Flat;
            btnP4.Font = new Font("Segoe UI", 9F);
            btnP4.ForeColor = Color.FromArgb(71, 85, 105);
            btnP4.Location = new Point(320, 19);
            btnP4.Margin = new Padding(0, 19, 6, 19);
            btnP4.Name = "btnP4";
            btnP4.Size = new Size(58, 66);
            btnP4.TabIndex = 5;
            btnP4.Text = "4";
            btnP4.UseVisualStyleBackColor = false;
            // 
            // btnP5
            // 
            btnP5.BackColor = Color.White;
            btnP5.Cursor = Cursors.Hand;
            btnP5.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            btnP5.FlatStyle = FlatStyle.Flat;
            btnP5.Font = new Font("Segoe UI", 9F);
            btnP5.ForeColor = Color.FromArgb(71, 85, 105);
            btnP5.Location = new Point(384, 19);
            btnP5.Margin = new Padding(0, 19, 6, 19);
            btnP5.Name = "btnP5";
            btnP5.Size = new Size(58, 66);
            btnP5.TabIndex = 6;
            btnP5.Text = "5";
            btnP5.UseVisualStyleBackColor = false;
            // 
            // lblDots
            // 
            lblDots.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDots.ForeColor = Color.FromArgb(100, 116, 139);
            lblDots.Location = new Point(448, 19);
            lblDots.Margin = new Padding(0, 19, 6, 19);
            lblDots.Name = "lblDots";
            lblDots.Size = new Size(42, 66);
            lblDots.TabIndex = 7;
            lblDots.Text = "...";
            lblDots.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnP22
            // 
            btnP22.BackColor = Color.White;
            btnP22.Cursor = Cursors.Hand;
            btnP22.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            btnP22.FlatStyle = FlatStyle.Flat;
            btnP22.Font = new Font("Segoe UI", 9F);
            btnP22.ForeColor = Color.FromArgb(71, 85, 105);
            btnP22.Location = new Point(496, 19);
            btnP22.Margin = new Padding(0, 19, 6, 19);
            btnP22.Name = "btnP22";
            btnP22.Size = new Size(58, 66);
            btnP22.TabIndex = 8;
            btnP22.Text = "22";
            btnP22.UseVisualStyleBackColor = false;
            // 
            // btnNext
            // 
            btnNext.BackColor = Color.White;
            btnNext.Cursor = Cursors.Hand;
            btnNext.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            btnNext.FlatStyle = FlatStyle.Flat;
            btnNext.Font = new Font("Segoe UI", 9F);
            btnNext.ForeColor = Color.FromArgb(71, 85, 105);
            btnNext.Location = new Point(560, 19);
            btnNext.Margin = new Padding(0, 19, 6, 19);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(58, 77);
            btnNext.TabIndex = 9;
            btnNext.Text = ">";
            btnNext.UseVisualStyleBackColor = false;
            // 
            // btnLast
            // 
            btnLast.BackColor = Color.White;
            btnLast.Cursor = Cursors.Hand;
            btnLast.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            btnLast.FlatStyle = FlatStyle.Flat;
            btnLast.Font = new Font("Segoe UI", 9F);
            btnLast.ForeColor = Color.FromArgb(71, 85, 105);
            btnLast.Location = new Point(624, 19);
            btnLast.Margin = new Padding(0, 19, 6, 19);
            btnLast.Name = "btnLast";
            btnLast.Size = new Size(58, 77);
            btnLast.TabIndex = 10;
            btnLast.Text = ">>";
            btnLast.UseVisualStyleBackColor = false;
            // 
            // flpPageInfo
            // 
            flpPageInfo.Controls.Add(lblShow);
            flpPageInfo.Controls.Add(lblEntries);
            flpPageInfo.Controls.Add(txtGoToPage);
            flpPageInfo.Controls.Add(cmbPageSize);
            flpPageInfo.Controls.Add(lblGoTo);
            flpPageInfo.Dock = DockStyle.Fill;
            flpPageInfo.FlowDirection = FlowDirection.RightToLeft;
            flpPageInfo.Location = new Point(1432, 0);
            flpPageInfo.Margin = new Padding(0);
            flpPageInfo.Name = "flpPageInfo";
            flpPageInfo.Size = new Size(938, 96);
            flpPageInfo.TabIndex = 1;
            flpPageInfo.WrapContents = false;
            // 
            // lblShow
            // 
            lblShow.AutoSize = true;
            lblShow.Font = new Font("Segoe UI", 9F);
            lblShow.ForeColor = Color.FromArgb(71, 85, 105);
            lblShow.Location = new Point(866, 27);
            lblShow.Margin = new Padding(0, 27, 0, 0);
            lblShow.Name = "lblShow";
            lblShow.Size = new Size(72, 32);
            lblShow.TabIndex = 4;
            lblShow.Text = "Show";
            // 
            // lblEntries
            // 
            lblEntries.AutoSize = true;
            lblEntries.Font = new Font("Segoe UI", 9F);
            lblEntries.ForeColor = Color.FromArgb(71, 85, 105);
            lblEntries.Location = new Point(780, 27);
            lblEntries.Margin = new Padding(10, 27, 0, 0);
            lblEntries.Name = "lblEntries";
            lblEntries.Size = new Size(86, 32);
            lblEntries.TabIndex = 2;
            lblEntries.Text = "entries";
            // 
            // txtGoToPage
            // 
            txtGoToPage.Font = new Font("Segoe UI", 9F);
            txtGoToPage.Location = new Point(701, 22);
            txtGoToPage.Margin = new Padding(10, 22, 0, 0);
            txtGoToPage.Name = "txtGoToPage";
            txtGoToPage.Size = new Size(69, 39);
            txtGoToPage.TabIndex = 0;
            txtGoToPage.Text = "1";
            txtGoToPage.TextAlign = HorizontalAlignment.Center;
            // 
            // cmbPageSize
            // 
            cmbPageSize.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPageSize.Font = new Font("Segoe UI", 8F);
            cmbPageSize.FormattingEnabled = true;
            cmbPageSize.Items.AddRange(new object[] { "10", "25", "50", "100" });
            cmbPageSize.Location = new Point(567, 22);
            cmbPageSize.Margin = new Padding(10, 22, 0, 0);
            cmbPageSize.Name = "cmbPageSize";
            cmbPageSize.Size = new Size(124, 38);
            cmbPageSize.TabIndex = 3;
            // 
            // lblGoTo
            // 
            lblGoTo.AutoSize = true;
            lblGoTo.Font = new Font("Segoe UI", 9F);
            lblGoTo.ForeColor = Color.FromArgb(71, 85, 105);
            lblGoTo.Location = new Point(484, 27);
            lblGoTo.Margin = new Padding(20, 27, 0, 0);
            lblGoTo.Name = "lblGoTo";
            lblGoTo.Size = new Size(73, 32);
            lblGoTo.TabIndex = 1;
            lblGoTo.Text = "Go to";
            // 
            // rightLayout
            // 
            rightLayout.ColumnCount = 1;
            rightLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rightLayout.Controls.Add(pnlDetail, 0, 0);
            rightLayout.Controls.Add(pnlSummary, 0, 1);
            rightLayout.Controls.Add(pnlSteps, 0, 2);
            rightLayout.Dock = DockStyle.Fill;
            rightLayout.Location = new Point(2460, 31);
            rightLayout.Margin = new Padding(18, 19, 18, 19);
            rightLayout.Name = "rightLayout";
            rightLayout.RowCount = 3;
            rightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            rightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            rightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 35F));
            rightLayout.Size = new Size(1238, 2572);
            rightLayout.TabIndex = 1;
            // 
            // pnlDetail
            // 
            pnlDetail.BackColor = Color.White;
            pnlDetail.BorderStyle = BorderStyle.FixedSingle;
            pnlDetail.Controls.Add(tblDetailContent);
            pnlDetail.Dock = DockStyle.Fill;
            pnlDetail.Location = new Point(0, 0);
            pnlDetail.Margin = new Padding(0, 0, 0, 32);
            pnlDetail.Name = "pnlDetail";
            pnlDetail.Padding = new Padding(24);
            pnlDetail.Size = new Size(1238, 996);
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
            tblDetailContent.Location = new Point(24, 24);
            tblDetailContent.Margin = new Padding(0);
            tblDetailContent.Name = "tblDetailContent";
            tblDetailContent.RowCount = 3;
            tblDetailContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
            tblDetailContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblDetailContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            tblDetailContent.Size = new Size(1188, 946);
            tblDetailContent.TabIndex = 0;
            // 
            // tblDetailHeader
            // 
            tblDetailHeader.ColumnCount = 2;
            tblDetailHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tblDetailHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tblDetailHeader.Controls.Add(lblDetailTitle, 0, 0);
            tblDetailHeader.Controls.Add(lblDetailBadge, 1, 0);
            tblDetailHeader.Dock = DockStyle.Fill;
            tblDetailHeader.Location = new Point(0, 0);
            tblDetailHeader.Margin = new Padding(0);
            tblDetailHeader.Name = "tblDetailHeader";
            tblDetailHeader.RowCount = 1;
            tblDetailHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblDetailHeader.Size = new Size(1188, 64);
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
            lblDetailTitle.Size = new Size(712, 64);
            lblDetailTitle.TabIndex = 0;
            lblDetailTitle.Text = "TEST DETAIL";
            lblDetailTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDetailBadge
            // 
            lblDetailBadge.Anchor = AnchorStyles.Right;
            lblDetailBadge.AutoSize = true;
            lblDetailBadge.BackColor = Color.FromArgb(76, 175, 80);
            lblDetailBadge.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDetailBadge.ForeColor = Color.White;
            lblDetailBadge.Location = new Point(1077, 10);
            lblDetailBadge.Margin = new Padding(0);
            lblDetailBadge.Name = "lblDetailBadge";
            lblDetailBadge.Padding = new Padding(20, 6, 20, 6);
            lblDetailBadge.Size = new Size(111, 44);
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
            tblDetailBody.Location = new Point(0, 64);
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
            tblDetailBody.Size = new Size(1188, 802);
            tblDetailBody.TabIndex = 1;
            // 
            // lblDtTestTimeK
            // 
            lblDtTestTimeK.Dock = DockStyle.Fill;
            lblDtTestTimeK.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDtTestTimeK.Location = new Point(0, 0);
            lblDtTestTimeK.Margin = new Padding(0);
            lblDtTestTimeK.Name = "lblDtTestTimeK";
            lblDtTestTimeK.Size = new Size(332, 100);
            lblDtTestTimeK.TabIndex = 0;
            lblDtTestTimeK.Text = "Test Time";
            lblDtTestTimeK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtTestTimeV
            // 
            tblDetailBody.SetColumnSpan(lblDtTestTimeV, 3);
            lblDtTestTimeV.Dock = DockStyle.Fill;
            lblDtTestTimeV.Font = new Font("Segoe UI", 9F);
            lblDtTestTimeV.Location = new Point(332, 0);
            lblDtTestTimeV.Margin = new Padding(0);
            lblDtTestTimeV.Name = "lblDtTestTimeV";
            lblDtTestTimeV.Size = new Size(856, 100);
            lblDtTestTimeV.TabIndex = 1;
            lblDtTestTimeV.Text = ": 15 Mar 2026 10:30:20 AM";
            lblDtTestTimeV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtModelK
            // 
            lblDtModelK.Dock = DockStyle.Fill;
            lblDtModelK.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDtModelK.Location = new Point(0, 100);
            lblDtModelK.Margin = new Padding(0);
            lblDtModelK.Name = "lblDtModelK";
            lblDtModelK.Size = new Size(332, 100);
            lblDtModelK.TabIndex = 2;
            lblDtModelK.Text = "Product Model";
            lblDtModelK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtModelV
            // 
            lblDtModelV.Dock = DockStyle.Fill;
            lblDtModelV.Font = new Font("Segoe UI", 9F);
            lblDtModelV.Location = new Point(332, 100);
            lblDtModelV.Margin = new Padding(0);
            lblDtModelV.Name = "lblDtModelV";
            lblDtModelV.Size = new Size(261, 100);
            lblDtModelV.TabIndex = 3;
            lblDtModelV.Text = ": 6060";
            lblDtModelV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtTotalK
            // 
            lblDtTotalK.Dock = DockStyle.Fill;
            lblDtTotalK.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDtTotalK.Location = new Point(593, 100);
            lblDtTotalK.Margin = new Padding(0);
            lblDtTotalK.Name = "lblDtTotalK";
            lblDtTotalK.Size = new Size(297, 100);
            lblDtTotalK.TabIndex = 4;
            lblDtTotalK.Text = "Total Time";
            lblDtTotalK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtTotalV
            // 
            lblDtTotalV.Dock = DockStyle.Fill;
            lblDtTotalV.Font = new Font("Segoe UI", 9F);
            lblDtTotalV.Location = new Point(890, 100);
            lblDtTotalV.Margin = new Padding(0);
            lblDtTotalV.Name = "lblDtTotalV";
            lblDtTotalV.Size = new Size(298, 100);
            lblDtTotalV.TabIndex = 5;
            lblDtTotalV.Text = ": 00:09:01";
            lblDtTotalV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtTypeK
            // 
            lblDtTypeK.Dock = DockStyle.Fill;
            lblDtTypeK.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDtTypeK.Location = new Point(0, 200);
            lblDtTypeK.Margin = new Padding(0);
            lblDtTypeK.Name = "lblDtTypeK";
            lblDtTypeK.Size = new Size(332, 100);
            lblDtTypeK.TabIndex = 6;
            lblDtTypeK.Text = "Product Type";
            lblDtTypeK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtTypeV
            // 
            lblDtTypeV.Dock = DockStyle.Fill;
            lblDtTypeV.Font = new Font("Segoe UI", 9F);
            lblDtTypeV.Location = new Point(332, 200);
            lblDtTypeV.Margin = new Padding(0);
            lblDtTypeV.Name = "lblDtTypeV";
            lblDtTypeV.Size = new Size(261, 100);
            lblDtTypeV.TabIndex = 7;
            lblDtTypeV.Text = ": AC";
            lblDtTypeV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtStartK
            // 
            lblDtStartK.Dock = DockStyle.Fill;
            lblDtStartK.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDtStartK.Location = new Point(593, 200);
            lblDtStartK.Margin = new Padding(0);
            lblDtStartK.Name = "lblDtStartK";
            lblDtStartK.Size = new Size(297, 100);
            lblDtStartK.TabIndex = 8;
            lblDtStartK.Text = "Start Time";
            lblDtStartK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtStartV
            // 
            lblDtStartV.Dock = DockStyle.Fill;
            lblDtStartV.Font = new Font("Segoe UI", 9F);
            lblDtStartV.Location = new Point(890, 200);
            lblDtStartV.Margin = new Padding(0);
            lblDtStartV.Name = "lblDtStartV";
            lblDtStartV.Size = new Size(298, 100);
            lblDtStartV.TabIndex = 9;
            lblDtStartV.Text = ": 10:30:20 AM";
            lblDtStartV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtAreaK
            // 
            lblDtAreaK.Dock = DockStyle.Fill;
            lblDtAreaK.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDtAreaK.Location = new Point(0, 300);
            lblDtAreaK.Margin = new Padding(0);
            lblDtAreaK.Name = "lblDtAreaK";
            lblDtAreaK.Size = new Size(332, 100);
            lblDtAreaK.TabIndex = 10;
            lblDtAreaK.Text = "Area";
            lblDtAreaK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtAreaV
            // 
            lblDtAreaV.Dock = DockStyle.Fill;
            lblDtAreaV.Font = new Font("Segoe UI", 9F);
            lblDtAreaV.Location = new Point(332, 300);
            lblDtAreaV.Margin = new Padding(0);
            lblDtAreaV.Name = "lblDtAreaV";
            lblDtAreaV.Size = new Size(261, 100);
            lblDtAreaV.TabIndex = 11;
            lblDtAreaV.Text = ": Engine Control";
            lblDtAreaV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtEndK
            // 
            lblDtEndK.Dock = DockStyle.Fill;
            lblDtEndK.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDtEndK.Location = new Point(593, 300);
            lblDtEndK.Margin = new Padding(0);
            lblDtEndK.Name = "lblDtEndK";
            lblDtEndK.Size = new Size(297, 100);
            lblDtEndK.TabIndex = 12;
            lblDtEndK.Text = "End Time";
            lblDtEndK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtEndV
            // 
            lblDtEndV.Dock = DockStyle.Fill;
            lblDtEndV.Font = new Font("Segoe UI", 9F);
            lblDtEndV.Location = new Point(890, 300);
            lblDtEndV.Margin = new Padding(0);
            lblDtEndV.Name = "lblDtEndV";
            lblDtEndV.Size = new Size(298, 100);
            lblDtEndV.TabIndex = 13;
            lblDtEndV.Text = ": 10:39:21 AM";
            lblDtEndV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtPartK
            // 
            lblDtPartK.Dock = DockStyle.Fill;
            lblDtPartK.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDtPartK.Location = new Point(0, 400);
            lblDtPartK.Margin = new Padding(0);
            lblDtPartK.Name = "lblDtPartK";
            lblDtPartK.Size = new Size(332, 100);
            lblDtPartK.TabIndex = 14;
            lblDtPartK.Text = "Part Number";
            lblDtPartK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtPartV
            // 
            lblDtPartV.Dock = DockStyle.Fill;
            lblDtPartV.Font = new Font("Segoe UI", 9F);
            lblDtPartV.Location = new Point(332, 400);
            lblDtPartV.Margin = new Padding(0);
            lblDtPartV.Name = "lblDtPartV";
            lblDtPartV.Size = new Size(261, 100);
            lblDtPartV.TabIndex = 15;
            lblDtPartV.Text = ": WH-ENG-001";
            lblDtPartV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtResultK
            // 
            lblDtResultK.Dock = DockStyle.Fill;
            lblDtResultK.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDtResultK.Location = new Point(593, 400);
            lblDtResultK.Margin = new Padding(0);
            lblDtResultK.Name = "lblDtResultK";
            lblDtResultK.Size = new Size(297, 100);
            lblDtResultK.TabIndex = 16;
            lblDtResultK.Text = "Result";
            lblDtResultK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtResultV
            // 
            lblDtResultV.Dock = DockStyle.Fill;
            lblDtResultV.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDtResultV.ForeColor = Color.FromArgb(22, 101, 52);
            lblDtResultV.Location = new Point(890, 400);
            lblDtResultV.Margin = new Padding(0);
            lblDtResultV.Name = "lblDtResultV";
            lblDtResultV.Size = new Size(298, 100);
            lblDtResultV.TabIndex = 17;
            lblDtResultV.Text = ": PASS";
            lblDtResultV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtSerialK
            // 
            lblDtSerialK.Dock = DockStyle.Fill;
            lblDtSerialK.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDtSerialK.Location = new Point(0, 500);
            lblDtSerialK.Margin = new Padding(0);
            lblDtSerialK.Name = "lblDtSerialK";
            lblDtSerialK.Size = new Size(332, 100);
            lblDtSerialK.TabIndex = 18;
            lblDtSerialK.Text = "Serial Number";
            lblDtSerialK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtSerialV
            // 
            lblDtSerialV.Dock = DockStyle.Fill;
            lblDtSerialV.Font = new Font("Segoe UI", 9F);
            lblDtSerialV.Location = new Point(332, 500);
            lblDtSerialV.Margin = new Padding(0);
            lblDtSerialV.Name = "lblDtSerialV";
            lblDtSerialV.Size = new Size(261, 100);
            lblDtSerialV.TabIndex = 19;
            lblDtSerialV.Text = ": SN12350";
            lblDtSerialV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtEcnK
            // 
            lblDtEcnK.Dock = DockStyle.Fill;
            lblDtEcnK.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDtEcnK.Location = new Point(0, 600);
            lblDtEcnK.Margin = new Padding(0);
            lblDtEcnK.Name = "lblDtEcnK";
            lblDtEcnK.Size = new Size(332, 100);
            lblDtEcnK.TabIndex = 20;
            lblDtEcnK.Text = "ECN No.";
            lblDtEcnK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtEcnV
            // 
            lblDtEcnV.Dock = DockStyle.Fill;
            lblDtEcnV.Font = new Font("Segoe UI", 9F);
            lblDtEcnV.Location = new Point(332, 600);
            lblDtEcnV.Margin = new Padding(0);
            lblDtEcnV.Name = "lblDtEcnV";
            lblDtEcnV.Size = new Size(261, 100);
            lblDtEcnV.TabIndex = 21;
            lblDtEcnV.Text = ": 123.R001";
            lblDtEcnV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtOpK
            // 
            lblDtOpK.Dock = DockStyle.Fill;
            lblDtOpK.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDtOpK.Location = new Point(0, 700);
            lblDtOpK.Margin = new Padding(0);
            lblDtOpK.Name = "lblDtOpK";
            lblDtOpK.Size = new Size(332, 102);
            lblDtOpK.TabIndex = 22;
            lblDtOpK.Text = "Operator";
            lblDtOpK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtOpV
            // 
            lblDtOpV.Dock = DockStyle.Fill;
            lblDtOpV.Font = new Font("Segoe UI", 9F);
            lblDtOpV.Location = new Point(332, 700);
            lblDtOpV.Margin = new Padding(0);
            lblDtOpV.Name = "lblDtOpV";
            lblDtOpV.Size = new Size(261, 102);
            lblDtOpV.TabIndex = 23;
            lblDtOpV.Text = ": Fally";
            lblDtOpV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // flpDetailFooter
            // 
            flpDetailFooter.Controls.Add(btnViewReport);
            flpDetailFooter.Dock = DockStyle.Fill;
            flpDetailFooter.FlowDirection = FlowDirection.RightToLeft;
            flpDetailFooter.Location = new Point(0, 866);
            flpDetailFooter.Margin = new Padding(0);
            flpDetailFooter.Name = "flpDetailFooter";
            flpDetailFooter.Size = new Size(1188, 80);
            flpDetailFooter.TabIndex = 2;
            // 
            // btnViewReport
            // 
            btnViewReport.BackColor = Color.FromArgb(255, 193, 7);
            btnViewReport.Cursor = Cursors.Hand;
            btnViewReport.FlatAppearance.BorderSize = 0;
            btnViewReport.FlatStyle = FlatStyle.Flat;
            btnViewReport.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnViewReport.ForeColor = Color.Black;
            btnViewReport.Location = new Point(905, 8);
            btnViewReport.Margin = new Padding(0, 8, 0, 0);
            btnViewReport.Name = "btnViewReport";
            btnViewReport.Size = new Size(283, 72);
            btnViewReport.TabIndex = 0;
            btnViewReport.Text = "View Test Report";
            btnViewReport.UseVisualStyleBackColor = false;
            // 
            // pnlSummary
            // 
            pnlSummary.BackColor = Color.White;
            pnlSummary.BorderStyle = BorderStyle.FixedSingle;
            pnlSummary.Controls.Add(tblSummaryContent);
            pnlSummary.Dock = DockStyle.Fill;
            pnlSummary.Location = new Point(0, 1028);
            pnlSummary.Margin = new Padding(0, 0, 0, 32);
            pnlSummary.Name = "pnlSummary";
            pnlSummary.Padding = new Padding(24);
            pnlSummary.Size = new Size(1238, 611);
            pnlSummary.TabIndex = 1;
            // 
            // tblSummaryContent
            // 
            tblSummaryContent.ColumnCount = 1;
            tblSummaryContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblSummaryContent.Controls.Add(lblSummaryTitle, 0, 0);
            tblSummaryContent.Controls.Add(tblSummaryBody, 0, 1);
            tblSummaryContent.Dock = DockStyle.Fill;
            tblSummaryContent.Location = new Point(24, 24);
            tblSummaryContent.Margin = new Padding(0);
            tblSummaryContent.Name = "tblSummaryContent";
            tblSummaryContent.RowCount = 2;
            tblSummaryContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
            tblSummaryContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblSummaryContent.Size = new Size(1188, 561);
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
            lblSummaryTitle.Size = new Size(1188, 64);
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
            tblSummaryBody.Location = new Point(0, 64);
            tblSummaryBody.Margin = new Padding(0);
            tblSummaryBody.Name = "tblSummaryBody";
            tblSummaryBody.RowCount = 1;
            tblSummaryBody.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblSummaryBody.Size = new Size(1188, 497);
            tblSummaryBody.TabIndex = 1;
            // 
            // pnlChartContainer
            // 
            pnlChartContainer.Dock = DockStyle.Fill;
            pnlChartContainer.Location = new Point(0, 0);
            pnlChartContainer.Margin = new Padding(0);
            pnlChartContainer.Name = "pnlChartContainer";
            pnlChartContainer.Size = new Size(475, 497);
            pnlChartContainer.TabIndex = 0;
            // 
            // tblSummaryLegend
            // 
            tblSummaryLegend.ColumnCount = 2;
            tblSummaryLegend.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 49F));
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
            tblSummaryLegend.Location = new Point(488, 0);
            tblSummaryLegend.Margin = new Padding(13, 0, 0, 0);
            tblSummaryLegend.Name = "tblSummaryLegend";
            tblSummaryLegend.RowCount = 4;
            tblSummaryLegend.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tblSummaryLegend.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tblSummaryLegend.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tblSummaryLegend.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tblSummaryLegend.Size = new Size(700, 497);
            tblSummaryLegend.TabIndex = 1;
            // 
            // lblLegPassIcon
            // 
            lblLegPassIcon.Dock = DockStyle.Fill;
            lblLegPassIcon.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblLegPassIcon.ForeColor = Color.FromArgb(76, 175, 80);
            lblLegPassIcon.Location = new Point(0, 0);
            lblLegPassIcon.Margin = new Padding(0);
            lblLegPassIcon.Name = "lblLegPassIcon";
            lblLegPassIcon.Size = new Size(49, 124);
            lblLegPassIcon.TabIndex = 0;
            lblLegPassIcon.Text = "-";
            lblLegPassIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblLegPassText
            // 
            lblLegPassText.Dock = DockStyle.Fill;
            lblLegPassText.Font = new Font("Segoe UI", 9F);
            lblLegPassText.ForeColor = Color.FromArgb(30, 41, 59);
            lblLegPassText.Location = new Point(49, 0);
            lblLegPassText.Margin = new Padding(0);
            lblLegPassText.Name = "lblLegPassText";
            lblLegPassText.Size = new Size(651, 124);
            lblLegPassText.TabIndex = 1;
            lblLegPassText.Text = "Pass : 4 (80.00%)";
            lblLegPassText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblLegFailIcon
            // 
            lblLegFailIcon.Dock = DockStyle.Fill;
            lblLegFailIcon.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblLegFailIcon.ForeColor = Color.FromArgb(244, 67, 54);
            lblLegFailIcon.Location = new Point(0, 124);
            lblLegFailIcon.Margin = new Padding(0);
            lblLegFailIcon.Name = "lblLegFailIcon";
            lblLegFailIcon.Size = new Size(49, 124);
            lblLegFailIcon.TabIndex = 2;
            lblLegFailIcon.Text = "-";
            lblLegFailIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblLegFailText
            // 
            lblLegFailText.Dock = DockStyle.Fill;
            lblLegFailText.Font = new Font("Segoe UI", 9F);
            lblLegFailText.ForeColor = Color.FromArgb(30, 41, 59);
            lblLegFailText.Location = new Point(49, 124);
            lblLegFailText.Margin = new Padding(0);
            lblLegFailText.Name = "lblLegFailText";
            lblLegFailText.Size = new Size(651, 124);
            lblLegFailText.TabIndex = 3;
            lblLegFailText.Text = "Fail : 1 (20.00%)";
            lblLegFailText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblLegProgIcon
            // 
            lblLegProgIcon.Dock = DockStyle.Fill;
            lblLegProgIcon.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblLegProgIcon.ForeColor = Color.FromArgb(255, 193, 7);
            lblLegProgIcon.Location = new Point(0, 248);
            lblLegProgIcon.Margin = new Padding(0);
            lblLegProgIcon.Name = "lblLegProgIcon";
            lblLegProgIcon.Size = new Size(49, 124);
            lblLegProgIcon.TabIndex = 4;
            lblLegProgIcon.Text = "-";
            lblLegProgIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblLegProgText
            // 
            lblLegProgText.Dock = DockStyle.Fill;
            lblLegProgText.Font = new Font("Segoe UI", 9F);
            lblLegProgText.ForeColor = Color.FromArgb(30, 41, 59);
            lblLegProgText.Location = new Point(49, 248);
            lblLegProgText.Margin = new Padding(0);
            lblLegProgText.Name = "lblLegProgText";
            lblLegProgText.Size = new Size(651, 124);
            lblLegProgText.TabIndex = 5;
            lblLegProgText.Text = "In Progress : 0 (0.00%)";
            lblLegProgText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblLegStopIcon
            // 
            lblLegStopIcon.Dock = DockStyle.Fill;
            lblLegStopIcon.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblLegStopIcon.ForeColor = Color.FromArgb(158, 158, 158);
            lblLegStopIcon.Location = new Point(0, 372);
            lblLegStopIcon.Margin = new Padding(0);
            lblLegStopIcon.Name = "lblLegStopIcon";
            lblLegStopIcon.Size = new Size(49, 125);
            lblLegStopIcon.TabIndex = 6;
            lblLegStopIcon.Text = "-";
            lblLegStopIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblLegStopText
            // 
            lblLegStopText.Dock = DockStyle.Fill;
            lblLegStopText.Font = new Font("Segoe UI", 9F);
            lblLegStopText.ForeColor = Color.FromArgb(30, 41, 59);
            lblLegStopText.Location = new Point(49, 372);
            lblLegStopText.Margin = new Padding(0);
            lblLegStopText.Name = "lblLegStopText";
            lblLegStopText.Size = new Size(651, 125);
            lblLegStopText.TabIndex = 7;
            lblLegStopText.Text = "Stop : 0 (0.00%)";
            lblLegStopText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlSteps
            // 
            pnlSteps.BackColor = Color.White;
            pnlSteps.BorderStyle = BorderStyle.FixedSingle;
            pnlSteps.Controls.Add(tblStepsContent);
            pnlSteps.Dock = DockStyle.Fill;
            pnlSteps.Location = new Point(0, 1671);
            pnlSteps.Margin = new Padding(0, 0, 0, 32);
            pnlSteps.Name = "pnlSteps";
            pnlSteps.Padding = new Padding(24);
            pnlSteps.Size = new Size(1238, 869);
            pnlSteps.TabIndex = 2;
            // 
            // tblStepsContent
            // 
            tblStepsContent.ColumnCount = 1;
            tblStepsContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblStepsContent.Controls.Add(lblStepsTitle, 0, 0);
            tblStepsContent.Controls.Add(gridSteps, 0, 1);
            tblStepsContent.Dock = DockStyle.Fill;
            tblStepsContent.Location = new Point(24, 24);
            tblStepsContent.Margin = new Padding(0);
            tblStepsContent.Name = "tblStepsContent";
            tblStepsContent.RowCount = 2;
            tblStepsContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
            tblStepsContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblStepsContent.Size = new Size(1188, 819);
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
            lblStepsTitle.Size = new Size(1188, 64);
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
            gridSteps.Location = new Point(0, 64);
            gridSteps.Margin = new Padding(0);
            gridSteps.Name = "gridSteps";
            gridSteps.ReadOnly = true;
            gridSteps.RowHeadersVisible = false;
            gridSteps.RowHeadersWidth = 51;
            gridSteps.Size = new Size(1188, 755);
            gridSteps.TabIndex = 1;
            // 
            // TestHistory
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 245, 247);
            Controls.Add(mainLayout);
            Controls.Add(pnlTop);
            Margin = new Padding(10, 11, 10, 11);
            Name = "TestHistory";
            Size = new Size(3726, 2973);
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)logoBox).EndInit();
            mainLayout.ResumeLayout(false);
            pnlLeft.ResumeLayout(false);
            tblLeftContent.ResumeLayout(false);
            tblLeftHeader.ResumeLayout(false);
            flpLeftButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridData).EndInit();
            tblPagination.ResumeLayout(false);
            flpPageNav.ResumeLayout(false);
            flpPageInfo.ResumeLayout(false);
            flpPageInfo.PerformLayout();
            rightLayout.ResumeLayout(false);
            pnlDetail.ResumeLayout(false);
            tblDetailContent.ResumeLayout(false);
            tblDetailHeader.ResumeLayout(false);
            tblDetailHeader.PerformLayout();
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

        private System.Windows.Forms.PictureBox logoBox;

        private System.Windows.Forms.Label lblTitle;



        // Deklarasi Komponen Filter

        private System.Windows.Forms.Label lblDateRange;

        private System.Windows.Forms.DateTimePicker dtpStart;

        private System.Windows.Forms.Label lblDash;

        private System.Windows.Forms.DateTimePicker dtpEnd;

        private System.Windows.Forms.Label lblProductModel;

        private System.Windows.Forms.ComboBox cmbProductModel;

        private System.Windows.Forms.Label lblProductType;

        private System.Windows.Forms.ComboBox cmbProductType;

        private System.Windows.Forms.Label lblArea;

        private System.Windows.Forms.ComboBox cmbArea;

        private System.Windows.Forms.Label lblResult;

        private System.Windows.Forms.ComboBox cmbResult;

        private System.Windows.Forms.Label lblOperator;

        private System.Windows.Forms.ComboBox cmbOperator;

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

        private System.Windows.Forms.Button btnFirst;

        private System.Windows.Forms.Button btnPrev;

        private System.Windows.Forms.Button btnP1;

        private System.Windows.Forms.Button btnP2;

        private System.Windows.Forms.Button btnP3;

        private System.Windows.Forms.Button btnP4;

        private System.Windows.Forms.Button btnP5;

        private System.Windows.Forms.Label lblDots;

        private System.Windows.Forms.Button btnP22;

        private System.Windows.Forms.Button btnNext;

        private System.Windows.Forms.Button btnLast;

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

    }

}