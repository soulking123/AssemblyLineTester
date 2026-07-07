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
            lblProductType = new Label();
            cmbProductType = new ComboBox();
            lblArea = new Label();
            cmbArea = new ComboBox();
            lblResult = new Label();
            cmbResult = new ComboBox();
            lblOperator = new Label();
            cmbOperator = new ComboBox();
            flpActionButtons = new FlowLayoutPanel();
            btnSearch = new Button();
            btnReset = new Button();
            logoBox = new PictureBox();
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
            ((System.ComponentModel.ISupportInitialize)logoBox).BeginInit();
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
            pnlTop.BackColor = Color.FromArgb(248, 250, 252);
            pnlTop.Controls.Add(pnlFilterCard);
            pnlTop.Controls.Add(logoBox);
            pnlTop.Controls.Add(lblTitle);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Margin = new Padding(8);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(2275, 240);
            pnlTop.TabIndex = 0;
            // 
            // pnlFilterCard
            // 
            pnlFilterCard.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlFilterCard.BackColor = Color.White;
            pnlFilterCard.Controls.Add(tblFilterInner);
            pnlFilterCard.Location = new Point(32, 88);
            pnlFilterCard.Margin = new Padding(8);
            pnlFilterCard.Name = "pnlFilterCard";
            pnlFilterCard.Padding = new Padding(12);
            pnlFilterCard.Size = new Size(2210, 128);
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
            tblFilterInner.Location = new Point(12, 12);
            tblFilterInner.Margin = new Padding(0);
            tblFilterInner.Name = "tblFilterInner";
            tblFilterInner.RowCount = 1;
            tblFilterInner.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblFilterInner.Size = new Size(2186, 104);
            tblFilterInner.TabIndex = 0;
            // 
            // tblFilterInputs
            // 
            tblFilterInputs.ColumnCount = 6;
            tblFilterInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28.5F));
            tblFilterInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.3F));
            tblFilterInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.3F));
            tblFilterInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.3F));
            tblFilterInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.3F));
            tblFilterInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.3F));
            tblFilterInputs.Controls.Add(lblDateRange, 0, 0);
            tblFilterInputs.Controls.Add(tblDateRange, 0, 1);
            tblFilterInputs.Controls.Add(lblProductModel, 1, 0);
            tblFilterInputs.Controls.Add(cmbProductModel, 1, 1);
            tblFilterInputs.Controls.Add(lblProductType, 2, 0);
            tblFilterInputs.Controls.Add(cmbProductType, 2, 1);
            tblFilterInputs.Controls.Add(lblArea, 3, 0);
            tblFilterInputs.Controls.Add(cmbArea, 3, 1);
            tblFilterInputs.Controls.Add(lblResult, 4, 0);
            tblFilterInputs.Controls.Add(cmbResult, 4, 1);
            tblFilterInputs.Controls.Add(lblOperator, 5, 0);
            tblFilterInputs.Controls.Add(cmbOperator, 5, 1);
            tblFilterInputs.Dock = DockStyle.Fill;
            tblFilterInputs.Location = new Point(0, 0);
            tblFilterInputs.Margin = new Padding(0);
            tblFilterInputs.Name = "tblFilterInputs";
            tblFilterInputs.RowCount = 2;
            tblFilterInputs.RowStyles.Add(new RowStyle(SizeType.Percent, 35F));
            tblFilterInputs.RowStyles.Add(new RowStyle(SizeType.Percent, 65F));
            tblFilterInputs.Size = new Size(1890, 104);
            tblFilterInputs.TabIndex = 0;
            // 
            // lblDateRange
            // 
            lblDateRange.AutoSize = true;
            lblDateRange.Dock = DockStyle.Fill;
            lblDateRange.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblDateRange.ForeColor = Color.FromArgb(51, 65, 85);
            lblDateRange.Location = new Point(4, 0);
            lblDateRange.Margin = new Padding(4, 0, 4, 2);
            lblDateRange.Name = "lblDateRange";
            lblDateRange.Size = new Size(530, 34);
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
            tblDateRange.Location = new Point(0, 36);
            tblDateRange.Margin = new Padding(0);
            tblDateRange.Name = "tblDateRange";
            tblDateRange.RowCount = 1;
            tblDateRange.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblDateRange.Size = new Size(538, 68);
            tblDateRange.TabIndex = 0;
            // 
            // dtpStart
            // 
            dtpStart.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dtpStart.CustomFormat = "dd Mar yyyy";
            dtpStart.Font = new Font("Segoe UI", 9F);
            dtpStart.Format = DateTimePickerFormat.Custom;
            dtpStart.Location = new Point(4, 14);
            dtpStart.Margin = new Padding(4);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(249, 39);
            dtpStart.TabIndex = 3;
            // 
            // lblDash
            // 
            lblDash.AutoSize = true;
            lblDash.Dock = DockStyle.Fill;
            lblDash.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDash.ForeColor = Color.FromArgb(100, 116, 139);
            lblDash.Location = new Point(257, 0);
            lblDash.Margin = new Padding(0);
            lblDash.Name = "lblDash";
            lblDash.Size = new Size(24, 68);
            lblDash.TabIndex = 4;
            lblDash.Text = "-";
            lblDash.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dtpEnd
            // 
            dtpEnd.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dtpEnd.CustomFormat = "dd Mar yyyy";
            dtpEnd.Font = new Font("Segoe UI", 9F);
            dtpEnd.Format = DateTimePickerFormat.Custom;
            dtpEnd.Location = new Point(285, 14);
            dtpEnd.Margin = new Padding(4);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(249, 39);
            dtpEnd.TabIndex = 5;
            // 
            // lblProductModel
            // 
            lblProductModel.AutoSize = true;
            lblProductModel.Dock = DockStyle.Fill;
            lblProductModel.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblProductModel.ForeColor = Color.FromArgb(51, 65, 85);
            lblProductModel.Location = new Point(542, 0);
            lblProductModel.Margin = new Padding(4, 0, 4, 2);
            lblProductModel.Name = "lblProductModel";
            lblProductModel.Size = new Size(262, 34);
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
            cmbProductModel.Location = new Point(542, 50);
            cmbProductModel.Margin = new Padding(4);
            cmbProductModel.Name = "cmbProductModel";
            cmbProductModel.Size = new Size(262, 40);
            cmbProductModel.TabIndex = 7;
            // 
            // lblProductType
            // 
            lblProductType.AutoSize = true;
            lblProductType.Dock = DockStyle.Fill;
            lblProductType.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblProductType.ForeColor = Color.FromArgb(51, 65, 85);
            lblProductType.Location = new Point(812, 0);
            lblProductType.Margin = new Padding(4, 0, 4, 2);
            lblProductType.Name = "lblProductType";
            lblProductType.Size = new Size(262, 34);
            lblProductType.TabIndex = 8;
            lblProductType.Text = "Product Type";
            lblProductType.TextAlign = ContentAlignment.BottomLeft;
            // 
            // cmbProductType
            // 
            cmbProductType.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbProductType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProductType.Font = new Font("Segoe UI", 9F);
            cmbProductType.FormattingEnabled = true;
            cmbProductType.Items.AddRange(new object[] { "All", "AC", "DC" });
            cmbProductType.Location = new Point(812, 50);
            cmbProductType.Margin = new Padding(4);
            cmbProductType.Name = "cmbProductType";
            cmbProductType.Size = new Size(262, 40);
            cmbProductType.TabIndex = 9;
            // 
            // lblArea
            // 
            lblArea.AutoSize = true;
            lblArea.Dock = DockStyle.Fill;
            lblArea.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblArea.ForeColor = Color.FromArgb(51, 65, 85);
            lblArea.Location = new Point(1082, 0);
            lblArea.Margin = new Padding(4, 0, 4, 2);
            lblArea.Name = "lblArea";
            lblArea.Size = new Size(262, 34);
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
            cmbArea.Location = new Point(1082, 50);
            cmbArea.Margin = new Padding(4);
            cmbArea.Name = "cmbArea";
            cmbArea.Size = new Size(262, 40);
            cmbArea.TabIndex = 11;
            // 
            // lblResult
            // 
            lblResult.AutoSize = true;
            lblResult.Dock = DockStyle.Fill;
            lblResult.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblResult.ForeColor = Color.FromArgb(51, 65, 85);
            lblResult.Location = new Point(1352, 0);
            lblResult.Margin = new Padding(4, 0, 4, 2);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(262, 34);
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
            cmbResult.Location = new Point(1352, 50);
            cmbResult.Margin = new Padding(4);
            cmbResult.Name = "cmbResult";
            cmbResult.Size = new Size(262, 40);
            cmbResult.TabIndex = 13;
            // 
            // lblOperator
            // 
            lblOperator.AutoSize = true;
            lblOperator.Dock = DockStyle.Fill;
            lblOperator.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblOperator.ForeColor = Color.FromArgb(51, 65, 85);
            lblOperator.Location = new Point(1622, 0);
            lblOperator.Margin = new Padding(4, 0, 4, 2);
            lblOperator.Name = "lblOperator";
            lblOperator.Size = new Size(264, 34);
            lblOperator.TabIndex = 14;
            lblOperator.Text = "Operator";
            lblOperator.TextAlign = ContentAlignment.BottomLeft;
            // 
            // cmbOperator
            // 
            cmbOperator.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbOperator.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOperator.Font = new Font("Segoe UI", 9F);
            cmbOperator.FormattingEnabled = true;
            cmbOperator.Items.AddRange(new object[] { "All", "Fally", "Hendra", "Budi" });
            cmbOperator.Location = new Point(1622, 50);
            cmbOperator.Margin = new Padding(4);
            cmbOperator.Name = "cmbOperator";
            cmbOperator.Size = new Size(264, 40);
            cmbOperator.TabIndex = 15;
            // 
            // flpActionButtons
            // 
            flpActionButtons.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            flpActionButtons.AutoSize = true;
            flpActionButtons.Controls.Add(btnSearch);
            flpActionButtons.Controls.Add(btnReset);
            flpActionButtons.Location = new Point(1902, 51);
            flpActionButtons.Margin = new Padding(12, 0, 0, 0);
            flpActionButtons.Name = "flpActionButtons";
            flpActionButtons.Size = new Size(284, 53);
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
            btnSearch.Margin = new Padding(0, 0, 8, 0);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(154, 53);
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
            btnReset.Location = new Point(162, 0);
            btnReset.Margin = new Padding(0);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(122, 53);
            btnReset.TabIndex = 17;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = false;
            // 
            // logoBox
            // 
            logoBox.BackColor = Color.Transparent;
            logoBox.Location = new Point(39, 19);
            logoBox.Margin = new Padding(8);
            logoBox.Name = "logoBox";
            logoBox.Size = new Size(58, 58);
            logoBox.TabIndex = 0;
            logoBox.TabStop = false;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTitle.Location = new Point(107, 19);
            lblTitle.Margin = new Padding(8, 0, 8, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(291, 54);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "TEST HISTORY";
            // 
            // mainLayout
            // 
            mainLayout.ColumnCount = 2;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 61.89011F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38.10989F));
            mainLayout.Controls.Add(pnlLeft, 0, 0);
            mainLayout.Controls.Add(rightLayout, 1, 0);
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Location = new Point(0, 240);
            mainLayout.Margin = new Padding(8);
            mainLayout.Name = "mainLayout";
            mainLayout.RowCount = 1;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.Size = new Size(2275, 1280);
            mainLayout.TabIndex = 1;
            // 
            // pnlLeft
            // 
            pnlLeft.BackColor = Color.White;
            pnlLeft.Controls.Add(tblLeftContent);
            pnlLeft.Dock = DockStyle.Fill;
            pnlLeft.Location = new Point(29, 30);
            pnlLeft.Margin = new Padding(29, 30, 29, 30);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Size = new Size(1350, 1220);
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
            tblLeftContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 79F));
            tblLeftContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblLeftContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            tblLeftContent.Size = new Size(1350, 1220);
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
            tblLeftHeader.Padding = new Padding(20, 0, 20, 0);
            tblLeftHeader.RowCount = 1;
            tblLeftHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblLeftHeader.Size = new Size(1350, 79);
            tblLeftHeader.TabIndex = 0;
            // 
            // lblTableTitle
            // 
            lblTableTitle.Dock = DockStyle.Fill;
            lblTableTitle.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            lblTableTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblTableTitle.Location = new Point(20, 0);
            lblTableTitle.Margin = new Padding(0);
            lblTableTitle.Name = "lblTableTitle";
            lblTableTitle.Size = new Size(720, 79);
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
            flpLeftButtons.Location = new Point(740, 0);
            flpLeftButtons.Margin = new Padding(0);
            flpLeftButtons.Name = "flpLeftButtons";
            flpLeftButtons.Size = new Size(590, 79);
            flpLeftButtons.TabIndex = 1;
            flpLeftButtons.WrapContents = false;
            // 
            // btnExport
            // 
            btnExport.BackColor = Color.White;
            btnExport.Cursor = Cursors.Hand;
            btnExport.FlatAppearance.BorderColor = Color.LightGray;
            btnExport.FlatStyle = FlatStyle.Flat;
            btnExport.Font = new Font("Segoe UI", 9F);
            btnExport.ForeColor = Color.FromArgb(30, 41, 59);
            btnExport.Location = new Point(427, 10);
            btnExport.Margin = new Padding(0, 10, 0, 0);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(163, 55);
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
            btnRefresh.Font = new Font("Segoe UI", 9F);
            btnRefresh.ForeColor = Color.FromArgb(30, 41, 59);
            btnRefresh.Location = new Point(229, 10);
            btnRefresh.Margin = new Padding(0, 10, 20, 0);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(178, 55);
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
            gridData.Location = new Point(20, 99);
            gridData.Margin = new Padding(20, 20, 20, 0);
            gridData.Name = "gridData";
            gridData.ReadOnly = true;
            gridData.RowHeadersVisible = false;
            gridData.RowHeadersWidth = 51;
            gridData.Size = new Size(1310, 1066);
            gridData.TabIndex = 1;
            // 
            // tblPagination
            // 
            tblPagination.ColumnCount = 2;
            tblPagination.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tblPagination.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tblPagination.Controls.Add(flpPageNav, 0, 0);
            tblPagination.Controls.Add(flpPageInfo, 1, 0);
            tblPagination.Dock = DockStyle.Fill;
            tblPagination.Location = new Point(0, 1165);
            tblPagination.Margin = new Padding(0);
            tblPagination.Name = "tblPagination";
            tblPagination.Padding = new Padding(20, 0, 20, 0);
            tblPagination.RowCount = 1;
            tblPagination.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblPagination.Size = new Size(1350, 55);
            tblPagination.TabIndex = 2;
            // 
            // flpPageNav
            // 
            flpPageNav.Dock = DockStyle.Fill;
            flpPageNav.Location = new Point(20, 0);
            flpPageNav.Margin = new Padding(0);
            flpPageNav.Name = "flpPageNav";
            flpPageNav.Size = new Size(720, 55);
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
            flpPageInfo.Location = new Point(740, 0);
            flpPageInfo.Margin = new Padding(0);
            flpPageInfo.Name = "flpPageInfo";
            flpPageInfo.Size = new Size(590, 55);
            flpPageInfo.TabIndex = 1;
            flpPageInfo.WrapContents = false;
            // 
            // txtGoToPage
            // 
            txtGoToPage.Font = new Font("Segoe UI", 8.5F);
            txtGoToPage.Location = new Point(540, 14);
            txtGoToPage.Margin = new Padding(6, 14, 0, 0);
            txtGoToPage.Name = "txtGoToPage";
            txtGoToPage.Size = new Size(50, 38);
            txtGoToPage.TabIndex = 0;
            txtGoToPage.Text = "1";
            txtGoToPage.TextAlign = HorizontalAlignment.Center;
            // 
            // lblGoTo
            // 
            lblGoTo.AutoSize = true;
            lblGoTo.Font = new Font("Segoe UI", 8.5F);
            lblGoTo.ForeColor = Color.FromArgb(71, 85, 105);
            lblGoTo.Location = new Point(464, 17);
            lblGoTo.Margin = new Padding(15, 17, 0, 0);
            lblGoTo.Name = "lblGoTo";
            lblGoTo.Size = new Size(70, 31);
            lblGoTo.TabIndex = 1;
            lblGoTo.Text = "Go to";
            // 
            // lblEntries
            // 
            lblEntries.AutoSize = true;
            lblEntries.Font = new Font("Segoe UI", 8.5F);
            lblEntries.ForeColor = Color.FromArgb(71, 85, 105);
            lblEntries.Location = new Point(366, 17);
            lblEntries.Margin = new Padding(6, 17, 0, 0);
            lblEntries.Name = "lblEntries";
            lblEntries.Size = new Size(83, 31);
            lblEntries.TabIndex = 2;
            lblEntries.Text = "entries";
            // 
            // cmbPageSize
            // 
            cmbPageSize.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPageSize.Font = new Font("Segoe UI", 8.5F);
            cmbPageSize.FormattingEnabled = true;
            cmbPageSize.Items.AddRange(new object[] { "10", "25", "50", "100" });
            cmbPageSize.Location = new Point(295, 14);
            cmbPageSize.Margin = new Padding(6, 14, 0, 0);
            cmbPageSize.Name = "cmbPageSize";
            cmbPageSize.Size = new Size(65, 39);
            cmbPageSize.TabIndex = 3;
            // 
            // lblShow
            // 
            lblShow.AutoSize = true;
            lblShow.Font = new Font("Segoe UI", 8.5F);
            lblShow.ForeColor = Color.FromArgb(71, 85, 105);
            lblShow.Location = new Point(220, 17);
            lblShow.Margin = new Padding(0, 17, 0, 0);
            lblShow.Name = "lblShow";
            lblShow.Size = new Size(69, 31);
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
            rightLayout.Location = new Point(1437, 30);
            rightLayout.Margin = new Padding(29, 30, 29, 30);
            rightLayout.Name = "rightLayout";
            rightLayout.RowCount = 3;
            rightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            rightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            rightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 35F));
            rightLayout.Size = new Size(809, 1220);
            rightLayout.TabIndex = 1;
            // 
            // pnlDetail
            // 
            pnlDetail.BackColor = Color.White;
            pnlDetail.BorderStyle = BorderStyle.FixedSingle;
            pnlDetail.Controls.Add(tblDetailContent);
            pnlDetail.Dock = DockStyle.Fill;
            pnlDetail.Location = new Point(0, 0);
            pnlDetail.Margin = new Padding(0, 0, 0, 20);
            pnlDetail.Name = "pnlDetail";
            pnlDetail.Padding = new Padding(16, 12, 16, 12);
            pnlDetail.Size = new Size(809, 468);
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
            tblDetailContent.Location = new Point(16, 12);
            tblDetailContent.Margin = new Padding(0);
            tblDetailContent.Name = "tblDetailContent";
            tblDetailContent.RowCount = 3;
            tblDetailContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tblDetailContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblDetailContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            tblDetailContent.Size = new Size(775, 442);
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
            tblDetailHeader.Size = new Size(775, 45);
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
            lblDetailTitle.Size = new Size(503, 45);
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
            lblDetailBadge.Location = new Point(676, 0);
            lblDetailBadge.Margin = new Padding(0);
            lblDetailBadge.Name = "lblDetailBadge";
            lblDetailBadge.Size = new Size(99, 45);
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
            tblDetailBody.Location = new Point(0, 45);
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
            tblDetailBody.Size = new Size(775, 345);
            tblDetailBody.TabIndex = 1;
            // 
            // lblDtTestTimeK
            // 
            lblDtTestTimeK.Dock = DockStyle.Fill;
            lblDtTestTimeK.Font = new Font("Segoe UI", 8.8F, FontStyle.Bold);
            lblDtTestTimeK.Location = new Point(0, 0);
            lblDtTestTimeK.Margin = new Padding(0);
            lblDtTestTimeK.Name = "lblDtTestTimeK";
            lblDtTestTimeK.Size = new Size(217, 43);
            lblDtTestTimeK.TabIndex = 0;
            lblDtTestTimeK.Text = "Test Time";
            lblDtTestTimeK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtTestTimeV
            // 
            tblDetailBody.SetColumnSpan(lblDtTestTimeV, 3);
            lblDtTestTimeV.Dock = DockStyle.Fill;
            lblDtTestTimeV.Font = new Font("Segoe UI", 8.8F);
            lblDtTestTimeV.Location = new Point(217, 0);
            lblDtTestTimeV.Margin = new Padding(0);
            lblDtTestTimeV.Name = "lblDtTestTimeV";
            lblDtTestTimeV.Size = new Size(558, 43);
            lblDtTestTimeV.TabIndex = 1;
            lblDtTestTimeV.Text = ": 15 Mar 2026 10:30:20 AM";
            lblDtTestTimeV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtModelK
            // 
            lblDtModelK.Dock = DockStyle.Fill;
            lblDtModelK.Font = new Font("Segoe UI", 8.8F, FontStyle.Bold);
            lblDtModelK.Location = new Point(0, 43);
            lblDtModelK.Margin = new Padding(0);
            lblDtModelK.Name = "lblDtModelK";
            lblDtModelK.Size = new Size(217, 43);
            lblDtModelK.TabIndex = 2;
            lblDtModelK.Text = "Product Model";
            lblDtModelK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtModelV
            // 
            lblDtModelV.Dock = DockStyle.Fill;
            lblDtModelV.Font = new Font("Segoe UI", 8.8F);
            lblDtModelV.Location = new Point(217, 43);
            lblDtModelV.Margin = new Padding(0);
            lblDtModelV.Name = "lblDtModelV";
            lblDtModelV.Size = new Size(170, 43);
            lblDtModelV.TabIndex = 3;
            lblDtModelV.Text = ": 6060";
            lblDtModelV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtTotalK
            // 
            lblDtTotalK.Dock = DockStyle.Fill;
            lblDtTotalK.Font = new Font("Segoe UI", 8.8F, FontStyle.Bold);
            lblDtTotalK.Location = new Point(387, 43);
            lblDtTotalK.Margin = new Padding(0);
            lblDtTotalK.Name = "lblDtTotalK";
            lblDtTotalK.Size = new Size(193, 43);
            lblDtTotalK.TabIndex = 4;
            lblDtTotalK.Text = "Total Time";
            lblDtTotalK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtTotalV
            // 
            lblDtTotalV.Dock = DockStyle.Fill;
            lblDtTotalV.Font = new Font("Segoe UI", 8.8F);
            lblDtTotalV.Location = new Point(580, 43);
            lblDtTotalV.Margin = new Padding(0);
            lblDtTotalV.Name = "lblDtTotalV";
            lblDtTotalV.Size = new Size(195, 43);
            lblDtTotalV.TabIndex = 5;
            lblDtTotalV.Text = ": 00:09:01";
            lblDtTotalV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtTypeK
            // 
            lblDtTypeK.Dock = DockStyle.Fill;
            lblDtTypeK.Font = new Font("Segoe UI", 8.8F, FontStyle.Bold);
            lblDtTypeK.Location = new Point(0, 86);
            lblDtTypeK.Margin = new Padding(0);
            lblDtTypeK.Name = "lblDtTypeK";
            lblDtTypeK.Size = new Size(217, 43);
            lblDtTypeK.TabIndex = 6;
            lblDtTypeK.Text = "Product Type";
            lblDtTypeK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtTypeV
            // 
            lblDtTypeV.Dock = DockStyle.Fill;
            lblDtTypeV.Font = new Font("Segoe UI", 8.8F);
            lblDtTypeV.Location = new Point(217, 86);
            lblDtTypeV.Margin = new Padding(0);
            lblDtTypeV.Name = "lblDtTypeV";
            lblDtTypeV.Size = new Size(170, 43);
            lblDtTypeV.TabIndex = 7;
            lblDtTypeV.Text = ": AC";
            lblDtTypeV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtStartK
            // 
            lblDtStartK.Dock = DockStyle.Fill;
            lblDtStartK.Font = new Font("Segoe UI", 8.8F, FontStyle.Bold);
            lblDtStartK.Location = new Point(387, 86);
            lblDtStartK.Margin = new Padding(0);
            lblDtStartK.Name = "lblDtStartK";
            lblDtStartK.Size = new Size(193, 43);
            lblDtStartK.TabIndex = 8;
            lblDtStartK.Text = "Start Time";
            lblDtStartK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtStartV
            // 
            lblDtStartV.Dock = DockStyle.Fill;
            lblDtStartV.Font = new Font("Segoe UI", 8.8F);
            lblDtStartV.Location = new Point(580, 86);
            lblDtStartV.Margin = new Padding(0);
            lblDtStartV.Name = "lblDtStartV";
            lblDtStartV.Size = new Size(195, 43);
            lblDtStartV.TabIndex = 9;
            lblDtStartV.Text = ": 10:30:20 AM";
            lblDtStartV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtAreaK
            // 
            lblDtAreaK.Dock = DockStyle.Fill;
            lblDtAreaK.Font = new Font("Segoe UI", 8.8F, FontStyle.Bold);
            lblDtAreaK.Location = new Point(0, 129);
            lblDtAreaK.Margin = new Padding(0);
            lblDtAreaK.Name = "lblDtAreaK";
            lblDtAreaK.Size = new Size(217, 43);
            lblDtAreaK.TabIndex = 10;
            lblDtAreaK.Text = "Area";
            lblDtAreaK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtAreaV
            // 
            lblDtAreaV.Dock = DockStyle.Fill;
            lblDtAreaV.Font = new Font("Segoe UI", 8.8F);
            lblDtAreaV.Location = new Point(217, 129);
            lblDtAreaV.Margin = new Padding(0);
            lblDtAreaV.Name = "lblDtAreaV";
            lblDtAreaV.Size = new Size(170, 43);
            lblDtAreaV.TabIndex = 11;
            lblDtAreaV.Text = ": Engine Control";
            lblDtAreaV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtEndK
            // 
            lblDtEndK.Dock = DockStyle.Fill;
            lblDtEndK.Font = new Font("Segoe UI", 8.8F, FontStyle.Bold);
            lblDtEndK.Location = new Point(387, 129);
            lblDtEndK.Margin = new Padding(0);
            lblDtEndK.Name = "lblDtEndK";
            lblDtEndK.Size = new Size(193, 43);
            lblDtEndK.TabIndex = 12;
            lblDtEndK.Text = "End Time";
            lblDtEndK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtEndV
            // 
            lblDtEndV.Dock = DockStyle.Fill;
            lblDtEndV.Font = new Font("Segoe UI", 8.8F);
            lblDtEndV.Location = new Point(580, 129);
            lblDtEndV.Margin = new Padding(0);
            lblDtEndV.Name = "lblDtEndV";
            lblDtEndV.Size = new Size(195, 43);
            lblDtEndV.TabIndex = 13;
            lblDtEndV.Text = ": 10:39:21 AM";
            lblDtEndV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtPartK
            // 
            lblDtPartK.Dock = DockStyle.Fill;
            lblDtPartK.Font = new Font("Segoe UI", 8.8F, FontStyle.Bold);
            lblDtPartK.Location = new Point(0, 172);
            lblDtPartK.Margin = new Padding(0);
            lblDtPartK.Name = "lblDtPartK";
            lblDtPartK.Size = new Size(217, 43);
            lblDtPartK.TabIndex = 14;
            lblDtPartK.Text = "Part Number";
            lblDtPartK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtPartV
            // 
            lblDtPartV.Dock = DockStyle.Fill;
            lblDtPartV.Font = new Font("Segoe UI", 8.8F);
            lblDtPartV.Location = new Point(217, 172);
            lblDtPartV.Margin = new Padding(0);
            lblDtPartV.Name = "lblDtPartV";
            lblDtPartV.Size = new Size(170, 43);
            lblDtPartV.TabIndex = 15;
            lblDtPartV.Text = ": WH-ENG-001";
            lblDtPartV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtResultK
            // 
            lblDtResultK.Dock = DockStyle.Fill;
            lblDtResultK.Font = new Font("Segoe UI", 8.8F, FontStyle.Bold);
            lblDtResultK.Location = new Point(387, 172);
            lblDtResultK.Margin = new Padding(0);
            lblDtResultK.Name = "lblDtResultK";
            lblDtResultK.Size = new Size(193, 43);
            lblDtResultK.TabIndex = 16;
            lblDtResultK.Text = "Result";
            lblDtResultK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtResultV
            // 
            lblDtResultV.Dock = DockStyle.Fill;
            lblDtResultV.Font = new Font("Segoe UI", 8.8F, FontStyle.Bold);
            lblDtResultV.ForeColor = Color.FromArgb(22, 101, 52);
            lblDtResultV.Location = new Point(580, 172);
            lblDtResultV.Margin = new Padding(0);
            lblDtResultV.Name = "lblDtResultV";
            lblDtResultV.Size = new Size(195, 43);
            lblDtResultV.TabIndex = 17;
            lblDtResultV.Text = ": PASS";
            lblDtResultV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtSerialK
            // 
            lblDtSerialK.Dock = DockStyle.Fill;
            lblDtSerialK.Font = new Font("Segoe UI", 8.8F, FontStyle.Bold);
            lblDtSerialK.Location = new Point(0, 215);
            lblDtSerialK.Margin = new Padding(0);
            lblDtSerialK.Name = "lblDtSerialK";
            lblDtSerialK.Size = new Size(217, 43);
            lblDtSerialK.TabIndex = 18;
            lblDtSerialK.Text = "Serial Number";
            lblDtSerialK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtSerialV
            // 
            lblDtSerialV.Dock = DockStyle.Fill;
            lblDtSerialV.Font = new Font("Segoe UI", 8.8F);
            lblDtSerialV.Location = new Point(217, 215);
            lblDtSerialV.Margin = new Padding(0);
            lblDtSerialV.Name = "lblDtSerialV";
            lblDtSerialV.Size = new Size(170, 43);
            lblDtSerialV.TabIndex = 19;
            lblDtSerialV.Text = ": SN12350";
            lblDtSerialV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtEcnK
            // 
            lblDtEcnK.Dock = DockStyle.Fill;
            lblDtEcnK.Font = new Font("Segoe UI", 8.8F, FontStyle.Bold);
            lblDtEcnK.Location = new Point(0, 258);
            lblDtEcnK.Margin = new Padding(0);
            lblDtEcnK.Name = "lblDtEcnK";
            lblDtEcnK.Size = new Size(217, 43);
            lblDtEcnK.TabIndex = 20;
            lblDtEcnK.Text = "ECN No.";
            lblDtEcnK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtEcnV
            // 
            lblDtEcnV.Dock = DockStyle.Fill;
            lblDtEcnV.Font = new Font("Segoe UI", 8.8F);
            lblDtEcnV.Location = new Point(217, 258);
            lblDtEcnV.Margin = new Padding(0);
            lblDtEcnV.Name = "lblDtEcnV";
            lblDtEcnV.Size = new Size(170, 43);
            lblDtEcnV.TabIndex = 21;
            lblDtEcnV.Text = ": 123.R001";
            lblDtEcnV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtOpK
            // 
            lblDtOpK.Dock = DockStyle.Fill;
            lblDtOpK.Font = new Font("Segoe UI", 8.8F, FontStyle.Bold);
            lblDtOpK.Location = new Point(0, 301);
            lblDtOpK.Margin = new Padding(0);
            lblDtOpK.Name = "lblDtOpK";
            lblDtOpK.Size = new Size(217, 44);
            lblDtOpK.TabIndex = 22;
            lblDtOpK.Text = "Operator";
            lblDtOpK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDtOpV
            // 
            lblDtOpV.Dock = DockStyle.Fill;
            lblDtOpV.Font = new Font("Segoe UI", 8.8F);
            lblDtOpV.Location = new Point(217, 301);
            lblDtOpV.Margin = new Padding(0);
            lblDtOpV.Name = "lblDtOpV";
            lblDtOpV.Size = new Size(170, 44);
            lblDtOpV.TabIndex = 23;
            lblDtOpV.Text = ": Fally";
            lblDtOpV.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // flpDetailFooter
            // 
            flpDetailFooter.Controls.Add(btnViewReport);
            flpDetailFooter.Dock = DockStyle.Fill;
            flpDetailFooter.FlowDirection = FlowDirection.RightToLeft;
            flpDetailFooter.Location = new Point(0, 390);
            flpDetailFooter.Margin = new Padding(0);
            flpDetailFooter.Name = "flpDetailFooter";
            flpDetailFooter.Size = new Size(775, 52);
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
            btnViewReport.Location = new Point(429, 0);
            btnViewReport.Margin = new Padding(0);
            btnViewReport.Name = "btnViewReport";
            btnViewReport.Size = new Size(346, 52);
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
            pnlSummary.Location = new Point(0, 488);
            pnlSummary.Margin = new Padding(0, 0, 0, 20);
            pnlSummary.Name = "pnlSummary";
            pnlSummary.Padding = new Padding(16, 12, 16, 12);
            pnlSummary.Size = new Size(809, 285);
            pnlSummary.TabIndex = 1;
            // 
            // tblSummaryContent
            // 
            tblSummaryContent.ColumnCount = 1;
            tblSummaryContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblSummaryContent.Controls.Add(lblSummaryTitle, 0, 0);
            tblSummaryContent.Controls.Add(tblSummaryBody, 0, 1);
            tblSummaryContent.Dock = DockStyle.Fill;
            tblSummaryContent.Location = new Point(16, 12);
            tblSummaryContent.Margin = new Padding(0);
            tblSummaryContent.Name = "tblSummaryContent";
            tblSummaryContent.RowCount = 2;
            tblSummaryContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tblSummaryContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblSummaryContent.Size = new Size(775, 259);
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
            lblSummaryTitle.Size = new Size(775, 45);
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
            tblSummaryBody.Location = new Point(0, 45);
            tblSummaryBody.Margin = new Padding(0);
            tblSummaryBody.Name = "tblSummaryBody";
            tblSummaryBody.RowCount = 1;
            tblSummaryBody.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblSummaryBody.Size = new Size(775, 214);
            tblSummaryBody.TabIndex = 1;
            // 
            // pnlChartContainer
            // 
            pnlChartContainer.Dock = DockStyle.Fill;
            pnlChartContainer.Location = new Point(0, 0);
            pnlChartContainer.Margin = new Padding(0);
            pnlChartContainer.Name = "pnlChartContainer";
            pnlChartContainer.Size = new Size(310, 214);
            pnlChartContainer.TabIndex = 0;
            // 
            // tblSummaryLegend
            // 
            tblSummaryLegend.ColumnCount = 2;
            tblSummaryLegend.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
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
            tblSummaryLegend.Location = new Point(320, 0);
            tblSummaryLegend.Margin = new Padding(10, 0, 0, 0);
            tblSummaryLegend.Name = "tblSummaryLegend";
            tblSummaryLegend.RowCount = 4;
            tblSummaryLegend.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tblSummaryLegend.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tblSummaryLegend.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tblSummaryLegend.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tblSummaryLegend.Size = new Size(455, 214);
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
            lblLegPassIcon.Size = new Size(50, 53);
            lblLegPassIcon.TabIndex = 0;
            lblLegPassIcon.Text = "●";
            lblLegPassIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblLegPassText
            // 
            lblLegPassText.Dock = DockStyle.Fill;
            lblLegPassText.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblLegPassText.ForeColor = Color.FromArgb(30, 41, 59);
            lblLegPassText.Location = new Point(50, 0);
            lblLegPassText.Margin = new Padding(0);
            lblLegPassText.Name = "lblLegPassText";
            lblLegPassText.Size = new Size(405, 53);
            lblLegPassText.TabIndex = 1;
            lblLegPassText.Text = "Pass : 4 (80.00%)";
            lblLegPassText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblLegFailIcon
            // 
            lblLegFailIcon.Dock = DockStyle.Fill;
            lblLegFailIcon.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblLegFailIcon.ForeColor = Color.FromArgb(244, 67, 54);
            lblLegFailIcon.Location = new Point(0, 53);
            lblLegFailIcon.Margin = new Padding(0);
            lblLegFailIcon.Name = "lblLegFailIcon";
            lblLegFailIcon.Size = new Size(50, 53);
            lblLegFailIcon.TabIndex = 2;
            lblLegFailIcon.Text = "●";
            lblLegFailIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblLegFailText
            // 
            lblLegFailText.Dock = DockStyle.Fill;
            lblLegFailText.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblLegFailText.ForeColor = Color.FromArgb(30, 41, 59);
            lblLegFailText.Location = new Point(50, 53);
            lblLegFailText.Margin = new Padding(0);
            lblLegFailText.Name = "lblLegFailText";
            lblLegFailText.Size = new Size(405, 53);
            lblLegFailText.TabIndex = 3;
            lblLegFailText.Text = "Fail : 1 (20.00%)";
            lblLegFailText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblLegProgIcon
            // 
            lblLegProgIcon.Dock = DockStyle.Fill;
            lblLegProgIcon.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblLegProgIcon.ForeColor = Color.FromArgb(255, 193, 7);
            lblLegProgIcon.Location = new Point(0, 106);
            lblLegProgIcon.Margin = new Padding(0);
            lblLegProgIcon.Name = "lblLegProgIcon";
            lblLegProgIcon.Size = new Size(50, 53);
            lblLegProgIcon.TabIndex = 4;
            lblLegProgIcon.Text = "●";
            lblLegProgIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblLegProgText
            // 
            lblLegProgText.Dock = DockStyle.Fill;
            lblLegProgText.Font = new Font("Segoe UI", 9F);
            lblLegProgText.ForeColor = Color.FromArgb(30, 41, 59);
            lblLegProgText.Location = new Point(50, 106);
            lblLegProgText.Margin = new Padding(0);
            lblLegProgText.Name = "lblLegProgText";
            lblLegProgText.Size = new Size(405, 53);
            lblLegProgText.TabIndex = 5;
            lblLegProgText.Text = "In Progress : 0 (0.00%)";
            lblLegProgText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblLegStopIcon
            // 
            lblLegStopIcon.Dock = DockStyle.Fill;
            lblLegStopIcon.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblLegStopIcon.ForeColor = Color.FromArgb(158, 158, 158);
            lblLegStopIcon.Location = new Point(0, 159);
            lblLegStopIcon.Margin = new Padding(0);
            lblLegStopIcon.Name = "lblLegStopIcon";
            lblLegStopIcon.Size = new Size(50, 55);
            lblLegStopIcon.TabIndex = 6;
            lblLegStopIcon.Text = "●";
            lblLegStopIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblLegStopText
            // 
            lblLegStopText.Dock = DockStyle.Fill;
            lblLegStopText.Font = new Font("Segoe UI", 9F);
            lblLegStopText.ForeColor = Color.FromArgb(30, 41, 59);
            lblLegStopText.Location = new Point(50, 159);
            lblLegStopText.Margin = new Padding(0);
            lblLegStopText.Name = "lblLegStopText";
            lblLegStopText.Size = new Size(405, 55);
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
            pnlSteps.Location = new Point(0, 793);
            pnlSteps.Margin = new Padding(0);
            pnlSteps.Name = "pnlSteps";
            pnlSteps.Padding = new Padding(16, 12, 16, 12);
            pnlSteps.Size = new Size(809, 427);
            pnlSteps.TabIndex = 2;
            // 
            // tblStepsContent
            // 
            tblStepsContent.ColumnCount = 1;
            tblStepsContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblStepsContent.Controls.Add(lblStepsTitle, 0, 0);
            tblStepsContent.Controls.Add(gridSteps, 0, 1);
            tblStepsContent.Dock = DockStyle.Fill;
            tblStepsContent.Location = new Point(16, 12);
            tblStepsContent.Margin = new Padding(0);
            tblStepsContent.Name = "tblStepsContent";
            tblStepsContent.RowCount = 2;
            tblStepsContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tblStepsContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblStepsContent.Size = new Size(775, 401);
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
            lblStepsTitle.Size = new Size(775, 45);
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
            gridSteps.Location = new Point(0, 45);
            gridSteps.Margin = new Padding(0);
            gridSteps.Name = "gridSteps";
            gridSteps.ReadOnly = true;
            gridSteps.RowHeadersVisible = false;
            gridSteps.RowHeadersWidth = 51;
            gridSteps.Size = new Size(775, 356);
            gridSteps.TabIndex = 1;
            // 
            // TestHistory
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 245, 247);
            Controls.Add(mainLayout);
            Controls.Add(pnlTop);
            Margin = new Padding(16, 18, 16, 18);
            Name = "TestHistory";
            Size = new Size(2275, 1520);
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
            ((System.ComponentModel.ISupportInitialize)logoBox).EndInit();
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

        private System.Windows.Forms.Panel pnlFilterCard;
        private System.Windows.Forms.TableLayoutPanel tblFilterInner;
        private System.Windows.Forms.TableLayoutPanel tblFilterInputs;
        private System.Windows.Forms.TableLayoutPanel tblDateRange;
        private System.Windows.Forms.FlowLayoutPanel flpActionButtons;

    }

}
