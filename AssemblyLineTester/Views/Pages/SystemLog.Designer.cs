namespace AssemblyLineTester.Views.Pages

{

    partial class SystemLog

    {

        private System.ComponentModel.IContainer components = null;



        protected override void Dispose(bool disposing)

        {

            if (disposing)

            {

                components?.Dispose();

            }

            base.Dispose(disposing);

        }



        #region Component Designer generated code



        private void InitializeComponent()

        {
            pnlTop = new Panel();
            logoBox = new PictureBox();
            lblTitle = new Label();
            panelFilters = new Panel();
            tblFilterInner = new TableLayoutPanel();
            tblFilterInputs = new TableLayoutPanel();
            lblDateRange = new Label();
            tblDateRange = new TableLayoutPanel();
            dtpStart = new DateTimePicker();
            lblDash = new Label();
            dtpEnd = new DateTimePicker();
            lblLogLevel = new Label();
            cmbLogLevel = new ComboBox();
            lblModule = new Label();
            cmbModule = new ComboBox();
            lblUser = new Label();
            cmbUser = new ComboBox();
            lblAction = new Label();
            cmbAction = new ComboBox();
            lblKeyword = new Label();
            txtKeyword = new TextBox();
            flpActionButtons = new FlowLayoutPanel();
            btnSearch = new Button();
            btnReset = new Button();
            mainLayout = new TableLayoutPanel();
            pnlLeft = new Panel();
            btnExport = new Button();
            btnRefresh = new Button();
            gridData = new DataGridView();
            lblTableTitle = new Label();
            lblPaginationInfo = new Label();
            flpPages = new FlowLayoutPanel();
            rightLayout = new TableLayoutPanel();
            pnlSummary = new Panel();
            tblSummaryContent = new TableLayoutPanel();
            lblSummaryTitle = new Label();
            tblSummaryBody = new TableLayoutPanel();
            pnlChartContainer = new Panel();
            tblSummaryLegend = new TableLayoutPanel();
            lblLegInfoIcon = new Label();
            lblLegInfo = new Label();
            lblLegWarnIcon = new Label();
            lblLegWarn = new Label();
            lblLegErrorIcon = new Label();
            lblLegError = new Label();
            pnlLevelStats = new Panel();
            lblLevelStatsTitle = new Label();
            pnlModuleStats = new Panel();
            lblModuleStatsTitle = new Label();
            pnlLogDetail = new Panel();
            lblLogDetailTitle = new Label();
            lblLogDetailContent = new Label();
            pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)logoBox).BeginInit();
            panelFilters.SuspendLayout();
            tblFilterInner.SuspendLayout();
            tblFilterInputs.SuspendLayout();
            tblDateRange.SuspendLayout();
            flpActionButtons.SuspendLayout();
            mainLayout.SuspendLayout();
            pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridData).BeginInit();
            rightLayout.SuspendLayout();
            pnlSummary.SuspendLayout();
            tblSummaryContent.SuspendLayout();
            tblSummaryBody.SuspendLayout();
            tblSummaryLegend.SuspendLayout();
            pnlLevelStats.SuspendLayout();
            pnlModuleStats.SuspendLayout();
            pnlLogDetail.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.FromArgb(248, 250, 252);
            pnlTop.Controls.Add(logoBox);
            pnlTop.Controls.Add(lblTitle);
            pnlTop.Controls.Add(panelFilters);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Margin = new Padding(5);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(2520, 294);
            pnlTop.TabIndex = 0;
            // 
            // logoBox
            // 
            logoBox.BackColor = Color.LightGray;
            logoBox.Location = new Point(39, 29);
            logoBox.Margin = new Padding(5);
            logoBox.Name = "logoBox";
            logoBox.Size = new Size(68, 67);
            logoBox.TabIndex = 0;
            logoBox.TabStop = false;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTitle.Location = new Point(127, 29);
            lblTitle.Margin = new Padding(5, 0, 5, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(261, 54);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "SYSTEM LOG";
            // 
            // panelFilters
            // 
            panelFilters.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelFilters.BackColor = Color.White;
            panelFilters.Controls.Add(tblFilterInner);
            panelFilters.Location = new Point(39, 120);
            panelFilters.Margin = new Padding(5);
            panelFilters.Name = "panelFilters";
            panelFilters.Padding = new Padding(16, 12, 16, 12);
            panelFilters.Size = new Size(2442, 160);
            panelFilters.TabIndex = 2;
            // 
            // tblFilterInner
            // 
            tblFilterInner.ColumnCount = 2;
            tblFilterInner.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblFilterInner.ColumnStyles.Add(new ColumnStyle());
            tblFilterInner.Controls.Add(tblFilterInputs, 0, 0);
            tblFilterInner.Controls.Add(flpActionButtons, 1, 0);
            tblFilterInner.Dock = DockStyle.Fill;
            tblFilterInner.Location = new Point(16, 12);
            tblFilterInner.Margin = new Padding(0);
            tblFilterInner.Name = "tblFilterInner";
            tblFilterInner.RowCount = 1;
            tblFilterInner.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblFilterInner.Size = new Size(2410, 136);
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
            tblFilterInputs.Controls.Add(lblLogLevel, 1, 0);
            tblFilterInputs.Controls.Add(cmbLogLevel, 1, 1);
            tblFilterInputs.Controls.Add(lblModule, 2, 0);
            tblFilterInputs.Controls.Add(cmbModule, 2, 1);
            tblFilterInputs.Controls.Add(lblUser, 3, 0);
            tblFilterInputs.Controls.Add(cmbUser, 3, 1);
            tblFilterInputs.Controls.Add(lblAction, 4, 0);
            tblFilterInputs.Controls.Add(cmbAction, 4, 1);
            tblFilterInputs.Controls.Add(lblKeyword, 5, 0);
            tblFilterInputs.Controls.Add(txtKeyword, 5, 1);
            tblFilterInputs.Dock = DockStyle.Fill;
            tblFilterInputs.Location = new Point(0, 0);
            tblFilterInputs.Margin = new Padding(0);
            tblFilterInputs.Name = "tblFilterInputs";
            tblFilterInputs.RowCount = 2;
            tblFilterInputs.RowStyles.Add(new RowStyle(SizeType.Percent, 35F));
            tblFilterInputs.RowStyles.Add(new RowStyle(SizeType.Percent, 65F));
            tblFilterInputs.Size = new Size(2092, 136);
            tblFilterInputs.TabIndex = 0;
            // 
            // lblDateRange
            // 
            lblDateRange.AutoSize = true;
            lblDateRange.Dock = DockStyle.Fill;
            lblDateRange.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblDateRange.ForeColor = Color.FromArgb(71, 85, 105);
            lblDateRange.Location = new Point(4, 0);
            lblDateRange.Margin = new Padding(4, 0, 4, 2);
            lblDateRange.Name = "lblDateRange";
            lblDateRange.Size = new Size(588, 45);
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
            tblDateRange.Location = new Point(0, 47);
            tblDateRange.Margin = new Padding(0);
            tblDateRange.Name = "tblDateRange";
            tblDateRange.RowCount = 1;
            tblDateRange.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblDateRange.Size = new Size(596, 89);
            tblDateRange.TabIndex = 0;
            // 
            // dtpStart
            // 
            dtpStart.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dtpStart.Font = new Font("Segoe UI", 9.5F);
            dtpStart.Format = DateTimePickerFormat.Short;
            dtpStart.Location = new Point(4, 24);
            dtpStart.Margin = new Padding(4);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(273, 41);
            dtpStart.TabIndex = 3;
            // 
            // lblDash
            // 
            lblDash.Anchor = AnchorStyles.None;
            lblDash.AutoSize = true;
            lblDash.Font = new Font("Segoe UI", 9.5F);
            lblDash.Location = new Point(285, 26);
            lblDash.Margin = new Padding(4, 0, 4, 0);
            lblDash.Name = "lblDash";
            lblDash.Size = new Size(25, 36);
            lblDash.TabIndex = 4;
            lblDash.Text = "-";
            lblDash.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dtpEnd
            // 
            dtpEnd.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dtpEnd.Font = new Font("Segoe UI", 9.5F);
            dtpEnd.Format = DateTimePickerFormat.Short;
            dtpEnd.Location = new Point(318, 24);
            dtpEnd.Margin = new Padding(4);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(274, 41);
            dtpEnd.TabIndex = 5;
            // 
            // lblLogLevel
            // 
            lblLogLevel.AutoSize = true;
            lblLogLevel.Dock = DockStyle.Fill;
            lblLogLevel.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblLogLevel.ForeColor = Color.FromArgb(71, 85, 105);
            lblLogLevel.Location = new Point(600, 0);
            lblLogLevel.Margin = new Padding(4, 0, 4, 2);
            lblLogLevel.Name = "lblLogLevel";
            lblLogLevel.Size = new Size(291, 45);
            lblLogLevel.TabIndex = 6;
            lblLogLevel.Text = "Log Level";
            lblLogLevel.TextAlign = ContentAlignment.BottomLeft;
            // 
            // cmbLogLevel
            // 
            cmbLogLevel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbLogLevel.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLogLevel.FlatStyle = FlatStyle.System;
            cmbLogLevel.Font = new Font("Segoe UI", 9.5F);
            cmbLogLevel.FormattingEnabled = true;
            cmbLogLevel.Items.AddRange(new object[] { "All", "INFO", "WARNING", "ERROR" });
            cmbLogLevel.Location = new Point(600, 69);
            cmbLogLevel.Margin = new Padding(4);
            cmbLogLevel.Name = "cmbLogLevel";
            cmbLogLevel.Size = new Size(291, 43);
            cmbLogLevel.TabIndex = 7;
            // 
            // lblModule
            // 
            lblModule.AutoSize = true;
            lblModule.Dock = DockStyle.Fill;
            lblModule.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblModule.ForeColor = Color.FromArgb(71, 85, 105);
            lblModule.Location = new Point(899, 0);
            lblModule.Margin = new Padding(4, 0, 4, 2);
            lblModule.Name = "lblModule";
            lblModule.Size = new Size(291, 45);
            lblModule.TabIndex = 8;
            lblModule.Text = "Module";
            lblModule.TextAlign = ContentAlignment.BottomLeft;
            // 
            // cmbModule
            // 
            cmbModule.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbModule.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbModule.FlatStyle = FlatStyle.System;
            cmbModule.Font = new Font("Segoe UI", 9.5F);
            cmbModule.FormattingEnabled = true;
            cmbModule.Items.AddRange(new object[] { "All", "Test Execution", "Communication", "Harness Database", "Database", "User Management", "System" });
            cmbModule.Location = new Point(899, 69);
            cmbModule.Margin = new Padding(4);
            cmbModule.Name = "cmbModule";
            cmbModule.Size = new Size(291, 43);
            cmbModule.TabIndex = 9;
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Dock = DockStyle.Fill;
            lblUser.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblUser.ForeColor = Color.FromArgb(71, 85, 105);
            lblUser.Location = new Point(1198, 0);
            lblUser.Margin = new Padding(4, 0, 4, 2);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(291, 45);
            lblUser.TabIndex = 10;
            lblUser.Text = "User";
            lblUser.TextAlign = ContentAlignment.BottomLeft;
            // 
            // cmbUser
            // 
            cmbUser.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbUser.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUser.FlatStyle = FlatStyle.System;
            cmbUser.Font = new Font("Segoe UI", 9.5F);
            cmbUser.FormattingEnabled = true;
            cmbUser.Items.AddRange(new object[] { "All", "System", "Fally" });
            cmbUser.Location = new Point(1198, 69);
            cmbUser.Margin = new Padding(4);
            cmbUser.Name = "cmbUser";
            cmbUser.Size = new Size(291, 43);
            cmbUser.TabIndex = 11;
            // 
            // lblAction
            // 
            lblAction.AutoSize = true;
            lblAction.Dock = DockStyle.Fill;
            lblAction.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblAction.ForeColor = Color.FromArgb(71, 85, 105);
            lblAction.Location = new Point(1497, 0);
            lblAction.Margin = new Padding(4, 0, 4, 2);
            lblAction.Name = "lblAction";
            lblAction.Size = new Size(291, 45);
            lblAction.TabIndex = 12;
            lblAction.Text = "Action";
            lblAction.TextAlign = ContentAlignment.BottomLeft;
            // 
            // cmbAction
            // 
            cmbAction.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbAction.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAction.FlatStyle = FlatStyle.System;
            cmbAction.Font = new Font("Segoe UI", 9.5F);
            cmbAction.FormattingEnabled = true;
            cmbAction.Items.AddRange(new object[] { "All", "Start Test", "Load Program", "Step Complete", "Test Complete", "Update", "Connection Lost", "Reconnected", "Login", "Backup" });
            cmbAction.Location = new Point(1497, 69);
            cmbAction.Margin = new Padding(4);
            cmbAction.Name = "cmbAction";
            cmbAction.Size = new Size(291, 43);
            cmbAction.TabIndex = 13;
            // 
            // lblKeyword
            // 
            lblKeyword.AutoSize = true;
            lblKeyword.Dock = DockStyle.Fill;
            lblKeyword.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblKeyword.ForeColor = Color.FromArgb(71, 85, 105);
            lblKeyword.Location = new Point(1796, 0);
            lblKeyword.Margin = new Padding(4, 0, 4, 2);
            lblKeyword.Name = "lblKeyword";
            lblKeyword.Size = new Size(292, 45);
            lblKeyword.TabIndex = 14;
            lblKeyword.Text = "Keyword";
            lblKeyword.TextAlign = ContentAlignment.BottomLeft;
            // 
            // txtKeyword
            // 
            txtKeyword.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtKeyword.Font = new Font("Segoe UI", 9.5F);
            txtKeyword.Location = new Point(1796, 71);
            txtKeyword.Margin = new Padding(4);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderText = "Search log...";
            txtKeyword.Size = new Size(292, 41);
            txtKeyword.TabIndex = 15;
            // 
            // flpActionButtons
            // 
            flpActionButtons.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            flpActionButtons.AutoSize = true;
            flpActionButtons.Controls.Add(btnSearch);
            flpActionButtons.Controls.Add(btnReset);
            flpActionButtons.Location = new Point(2102, 88);
            flpActionButtons.Margin = new Padding(10, 0, 0, 0);
            flpActionButtons.Name = "flpActionButtons";
            flpActionButtons.Size = new Size(308, 48);
            flpActionButtons.TabIndex = 1;
            flpActionButtons.WrapContents = false;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(250, 204, 21);
            btnSearch.Cursor = Cursors.Hand;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnSearch.ForeColor = Color.FromArgb(15, 23, 42);
            btnSearch.Location = new Point(0, 0);
            btnSearch.Margin = new Padding(0, 0, 8, 0);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(160, 48);
            btnSearch.TabIndex = 16;
            btnSearch.Text = "🔍 Search";
            btnSearch.UseVisualStyleBackColor = false;
            // 
            // btnReset
            // 
            btnReset.BackColor = Color.FromArgb(248, 250, 252);
            btnReset.Cursor = Cursors.Hand;
            btnReset.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnReset.ForeColor = Color.FromArgb(71, 85, 105);
            btnReset.Location = new Point(168, 0);
            btnReset.Margin = new Padding(0);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(140, 48);
            btnReset.TabIndex = 17;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = false;
            // 
            // mainLayout
            // 
            mainLayout.ColumnCount = 2;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            mainLayout.Controls.Add(pnlLeft, 0, 0);
            mainLayout.Controls.Add(rightLayout, 1, 0);
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Location = new Point(0, 294);
            mainLayout.Margin = new Padding(5);
            mainLayout.Name = "mainLayout";
            mainLayout.Padding = new Padding(32, 39, 32, 32);
            mainLayout.RowCount = 1;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.Size = new Size(2520, 1101);
            mainLayout.TabIndex = 1;
            // 
            // pnlLeft
            // 
            pnlLeft.BackColor = Color.White;
            pnlLeft.Controls.Add(btnExport);
            pnlLeft.Controls.Add(btnRefresh);
            pnlLeft.Controls.Add(gridData);
            pnlLeft.Controls.Add(lblTableTitle);
            pnlLeft.Controls.Add(lblPaginationInfo);
            pnlLeft.Controls.Add(flpPages);
            pnlLeft.Dock = DockStyle.Fill;
            pnlLeft.Location = new Point(40, 48);
            pnlLeft.Margin = new Padding(8, 9, 8, 9);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Size = new Size(1703, 1012);
            pnlLeft.TabIndex = 0;
            // 
            // btnExport
            // 
            btnExport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExport.BackColor = Color.White;
            btnExport.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnExport.FlatStyle = FlatStyle.Flat;
            btnExport.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnExport.ForeColor = Color.FromArgb(71, 85, 105);
            btnExport.Location = new Point(1435, 38);
            btnExport.Margin = new Padding(5);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(203, 70);
            btnExport.TabIndex = 1;
            btnExport.Text = "📤 Export";
            btnExport.UseVisualStyleBackColor = false;
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.BackColor = Color.White;
            btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.FromArgb(71, 85, 105);
            btnRefresh.Location = new Point(1212, 38);
            btnRefresh.Margin = new Padding(5);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(203, 70);
            btnRefresh.TabIndex = 0;
            btnRefresh.Text = "↻ Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            // 
            // gridData
            // 
            gridData.AllowUserToAddRows = false;
            gridData.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridData.BackgroundColor = Color.White;
            gridData.BorderStyle = BorderStyle.None;
            gridData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridData.Location = new Point(65, 136);
            gridData.Margin = new Padding(5);
            gridData.Name = "gridData";
            gridData.RowHeadersVisible = false;
            gridData.RowHeadersWidth = 82;
            gridData.Size = new Size(1573, 748);
            gridData.TabIndex = 1;
            // 
            // lblTableTitle
            // 
            lblTableTitle.AutoSize = true;
            lblTableTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTableTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTableTitle.Location = new Point(65, 45);
            lblTableTitle.Margin = new Padding(5, 0, 5, 0);
            lblTableTitle.Name = "lblTableTitle";
            lblTableTitle.Size = new Size(395, 47);
            lblTableTitle.TabIndex = 2;
            lblTableTitle.Text = "LOG LIST (Total: 1,258)";
            // 
            // lblPaginationInfo
            // 
            lblPaginationInfo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblPaginationInfo.AutoSize = true;
            lblPaginationInfo.Font = new Font("Segoe UI", 9.5F);
            lblPaginationInfo.ForeColor = Color.FromArgb(100, 116, 139);
            lblPaginationInfo.Location = new Point(65, 921);
            lblPaginationInfo.Margin = new Padding(5, 0, 5, 0);
            lblPaginationInfo.Name = "lblPaginationInfo";
            lblPaginationInfo.Size = new Size(385, 36);
            lblPaginationInfo.TabIndex = 3;
            lblPaginationInfo.Text = "Showing 1 to 12 of 1,258 entries";
            // 
            // flpPages
            // 
            flpPages.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            flpPages.AutoSize = true;
            flpPages.Location = new Point(900, 908);
            flpPages.Margin = new Padding(0);
            flpPages.Name = "flpPages";
            flpPages.Size = new Size(580, 65);
            flpPages.TabIndex = 4;
            flpPages.WrapContents = false;
            // 
            // rightLayout
            // 
            rightLayout.ColumnCount = 1;
            rightLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rightLayout.Controls.Add(pnlSummary, 0, 0);
            rightLayout.Controls.Add(pnlLevelStats, 0, 1);
            rightLayout.Controls.Add(pnlModuleStats, 0, 2);
            rightLayout.Dock = DockStyle.Fill;
            rightLayout.Location = new Point(1769, 58);
            rightLayout.Margin = new Padding(18, 19, 18, 19);
            rightLayout.Name = "rightLayout";
            rightLayout.RowCount = 3;
            rightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
            rightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
            rightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 34F));
            rightLayout.Size = new Size(701, 992);
            rightLayout.TabIndex = 1;
            // 
            // pnlSummary
            // 
            pnlSummary.BackColor = Color.White;
            pnlSummary.Controls.Add(tblSummaryContent);
            pnlSummary.Dock = DockStyle.Fill;
            pnlSummary.Location = new Point(0, 0);
            pnlSummary.Margin = new Padding(0, 0, 0, 40);
            pnlSummary.Name = "pnlSummary";
            pnlSummary.Padding = new Padding(16, 12, 16, 12);
            pnlSummary.Size = new Size(701, 287);
            pnlSummary.TabIndex = 0;
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
            tblSummaryContent.Size = new Size(669, 263);
            tblSummaryContent.TabIndex = 0;
            // 
            // lblSummaryTitle
            // 
            lblSummaryTitle.Dock = DockStyle.Fill;
            lblSummaryTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSummaryTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblSummaryTitle.Location = new Point(0, 0);
            lblSummaryTitle.Margin = new Padding(0);
            lblSummaryTitle.Name = "lblSummaryTitle";
            lblSummaryTitle.Size = new Size(669, 45);
            lblSummaryTitle.TabIndex = 0;
            lblSummaryTitle.Text = "LOG DISTRIBUTION";
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
            tblSummaryBody.Size = new Size(669, 218);
            tblSummaryBody.TabIndex = 1;
            // 
            // pnlChartContainer
            // 
            pnlChartContainer.Dock = DockStyle.Fill;
            pnlChartContainer.Location = new Point(0, 0);
            pnlChartContainer.Margin = new Padding(0);
            pnlChartContainer.Name = "pnlChartContainer";
            pnlChartContainer.Size = new Size(267, 218);
            pnlChartContainer.TabIndex = 0;
            // 
            // tblSummaryLegend
            // 
            tblSummaryLegend.ColumnCount = 2;
            tblSummaryLegend.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 45F));
            tblSummaryLegend.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblSummaryLegend.Controls.Add(lblLegInfoIcon, 0, 0);
            tblSummaryLegend.Controls.Add(lblLegInfo, 1, 0);
            tblSummaryLegend.Controls.Add(lblLegWarnIcon, 0, 1);
            tblSummaryLegend.Controls.Add(lblLegWarn, 1, 1);
            tblSummaryLegend.Controls.Add(lblLegErrorIcon, 0, 2);
            tblSummaryLegend.Controls.Add(lblLegError, 1, 2);
            tblSummaryLegend.Dock = DockStyle.Fill;
            tblSummaryLegend.Location = new Point(277, 0);
            tblSummaryLegend.Margin = new Padding(10, 0, 0, 0);
            tblSummaryLegend.Name = "tblSummaryLegend";
            tblSummaryLegend.RowCount = 3;
            tblSummaryLegend.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            tblSummaryLegend.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            tblSummaryLegend.RowStyles.Add(new RowStyle(SizeType.Percent, 33.34F));
            tblSummaryLegend.Size = new Size(392, 218);
            tblSummaryLegend.TabIndex = 1;
            // 
            // lblLegInfoIcon
            // 
            lblLegInfoIcon.Dock = DockStyle.Fill;
            lblLegInfoIcon.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblLegInfoIcon.ForeColor = Color.FromArgb(16, 185, 129);
            lblLegInfoIcon.Location = new Point(0, 0);
            lblLegInfoIcon.Margin = new Padding(0);
            lblLegInfoIcon.Name = "lblLegInfoIcon";
            lblLegInfoIcon.Size = new Size(45, 72);
            lblLegInfoIcon.TabIndex = 0;
            lblLegInfoIcon.Text = "●";
            lblLegInfoIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblLegInfo
            // 
            lblLegInfo.Dock = DockStyle.Fill;
            lblLegInfo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblLegInfo.ForeColor = Color.FromArgb(30, 41, 59);
            lblLegInfo.Location = new Point(45, 0);
            lblLegInfo.Margin = new Padding(0);
            lblLegInfo.Name = "lblLegInfo";
            lblLegInfo.Size = new Size(347, 72);
            lblLegInfo.TabIndex = 1;
            lblLegInfo.Text = "INFO : 1,148 (91.3%)";
            lblLegInfo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblLegWarnIcon
            // 
            lblLegWarnIcon.Dock = DockStyle.Fill;
            lblLegWarnIcon.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblLegWarnIcon.ForeColor = Color.FromArgb(245, 158, 11);
            lblLegWarnIcon.Location = new Point(0, 72);
            lblLegWarnIcon.Margin = new Padding(0);
            lblLegWarnIcon.Name = "lblLegWarnIcon";
            lblLegWarnIcon.Size = new Size(45, 72);
            lblLegWarnIcon.TabIndex = 2;
            lblLegWarnIcon.Text = "●";
            lblLegWarnIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblLegWarn
            // 
            lblLegWarn.Dock = DockStyle.Fill;
            lblLegWarn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblLegWarn.ForeColor = Color.FromArgb(30, 41, 59);
            lblLegWarn.Location = new Point(45, 72);
            lblLegWarn.Margin = new Padding(0);
            lblLegWarn.Name = "lblLegWarn";
            lblLegWarn.Size = new Size(347, 72);
            lblLegWarn.TabIndex = 3;
            lblLegWarn.Text = "WARNING : 84 (6.7%)";
            lblLegWarn.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblLegErrorIcon
            // 
            lblLegErrorIcon.Dock = DockStyle.Fill;
            lblLegErrorIcon.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblLegErrorIcon.ForeColor = Color.FromArgb(239, 68, 68);
            lblLegErrorIcon.Location = new Point(0, 144);
            lblLegErrorIcon.Margin = new Padding(0);
            lblLegErrorIcon.Name = "lblLegErrorIcon";
            lblLegErrorIcon.Size = new Size(45, 74);
            lblLegErrorIcon.TabIndex = 4;
            lblLegErrorIcon.Text = "●";
            lblLegErrorIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblLegError
            // 
            lblLegError.Dock = DockStyle.Fill;
            lblLegError.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblLegError.ForeColor = Color.FromArgb(30, 41, 59);
            lblLegError.Location = new Point(45, 144);
            lblLegError.Margin = new Padding(0);
            lblLegError.Name = "lblLegError";
            lblLegError.Size = new Size(347, 74);
            lblLegError.TabIndex = 5;
            lblLegError.Text = "ERROR : 26 (2.1%)";
            lblLegError.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlLevelStats
            // 
            pnlLevelStats.BackColor = Color.White;
            pnlLevelStats.Controls.Add(lblLevelStatsTitle);
            pnlLevelStats.Dock = DockStyle.Fill;
            pnlLevelStats.Location = new Point(0, 327);
            pnlLevelStats.Margin = new Padding(0, 0, 0, 40);
            pnlLevelStats.Name = "pnlLevelStats";
            pnlLevelStats.Size = new Size(701, 287);
            pnlLevelStats.TabIndex = 1;
            // 
            // lblLevelStatsTitle
            // 
            lblLevelStatsTitle.AutoSize = true;
            lblLevelStatsTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblLevelStatsTitle.Location = new Point(32, 32);
            lblLevelStatsTitle.Margin = new Padding(5, 0, 5, 0);
            lblLevelStatsTitle.Name = "lblLevelStatsTitle";
            lblLevelStatsTitle.Size = new Size(150, 37);
            lblLevelStatsTitle.TabIndex = 0;
            lblLevelStatsTitle.Text = "LOG LEVEL";
            // 
            // pnlModuleStats
            // 
            pnlModuleStats.BackColor = Color.White;
            pnlModuleStats.Controls.Add(lblModuleStatsTitle);
            pnlModuleStats.Dock = DockStyle.Fill;
            pnlModuleStats.Location = new Point(0, 654);
            pnlModuleStats.Margin = new Padding(0);
            pnlModuleStats.Name = "pnlModuleStats";
            pnlModuleStats.Size = new Size(701, 338);
            pnlModuleStats.TabIndex = 2;
            // 
            // lblModuleStatsTitle
            // 
            lblModuleStatsTitle.AutoSize = true;
            lblModuleStatsTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblModuleStatsTitle.Location = new Point(32, 32);
            lblModuleStatsTitle.Margin = new Padding(5, 0, 5, 0);
            lblModuleStatsTitle.Name = "lblModuleStatsTitle";
            lblModuleStatsTitle.Size = new Size(131, 37);
            lblModuleStatsTitle.TabIndex = 0;
            lblModuleStatsTitle.Text = "MODULE";
            // 
            // pnlLogDetail
            // 
            pnlLogDetail.BackColor = Color.White;
            pnlLogDetail.Controls.Add(lblLogDetailTitle);
            pnlLogDetail.Controls.Add(lblLogDetailContent);
            pnlLogDetail.Dock = DockStyle.Bottom;
            pnlLogDetail.Location = new Point(0, 1395);
            pnlLogDetail.Margin = new Padding(5);
            pnlLogDetail.Name = "pnlLogDetail";
            pnlLogDetail.Size = new Size(2520, 175);
            pnlLogDetail.TabIndex = 2;
            // 
            // lblLogDetailTitle
            // 
            lblLogDetailTitle.AutoSize = true;
            lblLogDetailTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblLogDetailTitle.Location = new Point(65, 32);
            lblLogDetailTitle.Margin = new Padding(5, 0, 5, 0);
            lblLogDetailTitle.Name = "lblLogDetailTitle";
            lblLogDetailTitle.Size = new Size(166, 37);
            lblLogDetailTitle.TabIndex = 0;
            lblLogDetailTitle.Text = "LOG DETAIL";
            // 
            // lblLogDetailContent
            // 
            lblLogDetailContent.AutoSize = true;
            lblLogDetailContent.Location = new Point(65, 86);
            lblLogDetailContent.Margin = new Padding(5, 0, 5, 0);
            lblLogDetailContent.Name = "lblLogDetailContent";
            lblLogDetailContent.Size = new Size(1938, 32);
            lblLogDetailContent.TabIndex = 1;
            lblLogDetailContent.Text = "Date & Time: 15 Mar 2026 10:30:20 AM   |   Log Level: INFO   |   Module: Test Execution   |   User: Fally   |   Action: Start Test   |   Description: Test started for Part Number WH-ENG-001";
            // 
            // SystemLog
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
            Controls.Add(mainLayout);
            Controls.Add(pnlLogDetail);
            Controls.Add(pnlTop);
            Margin = new Padding(10, 11, 10, 11);
            Name = "SystemLog";
            Size = new Size(2520, 1570);
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)logoBox).EndInit();
            panelFilters.ResumeLayout(false);
            tblFilterInner.ResumeLayout(false);
            tblFilterInner.PerformLayout();
            tblFilterInputs.ResumeLayout(false);
            tblFilterInputs.PerformLayout();
            tblDateRange.ResumeLayout(false);
            tblDateRange.PerformLayout();
            flpActionButtons.ResumeLayout(false);
            mainLayout.ResumeLayout(false);
            pnlLeft.ResumeLayout(false);
            pnlLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridData).EndInit();
            rightLayout.ResumeLayout(false);
            pnlSummary.ResumeLayout(false);
            tblSummaryContent.ResumeLayout(false);
            tblSummaryBody.ResumeLayout(false);
            tblSummaryLegend.ResumeLayout(false);
            pnlLevelStats.ResumeLayout(false);
            pnlLevelStats.PerformLayout();
            pnlModuleStats.ResumeLayout(false);
            pnlModuleStats.PerformLayout();
            pnlLogDetail.ResumeLayout(false);
            pnlLogDetail.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel panelFilters;
        private System.Windows.Forms.PictureBox logoBox;
        private System.Windows.Forms.Label lblTitle;

        // Filter Controls
        private System.Windows.Forms.Label lblDateRange;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label lblDash;

        private System.Windows.Forms.DateTimePicker dtpEnd;

        private System.Windows.Forms.Label lblLogLevel;

        private System.Windows.Forms.ComboBox cmbLogLevel;

        private System.Windows.Forms.Label lblModule;

        private System.Windows.Forms.ComboBox cmbModule;

        private System.Windows.Forms.Label lblUser;

        private System.Windows.Forms.ComboBox cmbUser;

        private System.Windows.Forms.Label lblAction;

        private System.Windows.Forms.ComboBox cmbAction;

        private System.Windows.Forms.Label lblKeyword;

        private System.Windows.Forms.TextBox txtKeyword;

        private System.Windows.Forms.Button btnSearch;

        private System.Windows.Forms.Button btnReset;

        private System.Windows.Forms.TableLayoutPanel tblFilterInner;
        private System.Windows.Forms.TableLayoutPanel tblFilterInputs;
        private System.Windows.Forms.TableLayoutPanel tblDateRange;
        private System.Windows.Forms.FlowLayoutPanel flpActionButtons;



        // Layout Components

        private System.Windows.Forms.TableLayoutPanel mainLayout;

        private System.Windows.Forms.Panel pnlLeft;

        private System.Windows.Forms.Button btnExport;

        private System.Windows.Forms.Button btnRefresh;

        private System.Windows.Forms.Label lblTableTitle;
        private System.Windows.Forms.DataGridView gridData;
        private System.Windows.Forms.Label lblPaginationInfo;
        private System.Windows.Forms.FlowLayoutPanel flpPages;
        // Right Side Components

        private System.Windows.Forms.TableLayoutPanel rightLayout;

        private System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.TableLayoutPanel tblSummaryContent;
        private System.Windows.Forms.TableLayoutPanel tblSummaryBody;
        private System.Windows.Forms.Panel pnlChartContainer;
        private System.Windows.Forms.TableLayoutPanel tblSummaryLegend;
        private System.Windows.Forms.Label lblLegInfoIcon;
        private System.Windows.Forms.Label lblLegInfo;
        private System.Windows.Forms.Label lblLegWarnIcon;
        private System.Windows.Forms.Label lblLegWarn;
        private System.Windows.Forms.Label lblLegErrorIcon;
        private System.Windows.Forms.Label lblLegError;
        private System.Windows.Forms.Label lblSummaryTitle;

        private System.Windows.Forms.Panel pnlLevelStats;

        private System.Windows.Forms.Label lblLevelStatsTitle;

        private System.Windows.Forms.Panel pnlModuleStats;

        private System.Windows.Forms.Label lblModuleStatsTitle;



        // Log Detail Components (Full Width Bottom)

        private System.Windows.Forms.Panel pnlLogDetail;

        private System.Windows.Forms.Label lblLogDetailTitle;

        private System.Windows.Forms.Label lblLogDetailContent;

    }

}
