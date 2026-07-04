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
            lblDateRange = new Label();
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
            btnSearch = new Button();
            btnReset = new Button();
            mainLayout = new TableLayoutPanel();
            pnlLeft = new Panel();
            btnExport = new Button();
            btnRefresh = new Button();
            gridData = new DataGridView();
            lblTableTitle = new Label();
            lblPaginationInfo = new Label();
            btnPagePrev = new Button();
            btnPage1 = new Button();
            btnPage2 = new Button();
            btnPage3 = new Button();
            btnPageNext = new Button();
            rightLayout = new TableLayoutPanel();
            pnlSummary = new Panel();
            tblSummaryContent = new TableLayoutPanel();
            tblSummaryBody = new TableLayoutPanel();
            pnlChartContainer = new Panel();
            tblSummaryLegend = new TableLayoutPanel();
            lblLegInfoIcon = new Label();
            lblLegInfo = new Label();
            lblLegWarnIcon = new Label();
            lblLegWarn = new Label();
            lblLegErrorIcon = new Label();
            lblLegError = new Label();
            lblSummaryTitle = new Label();
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
            panelFilters.Controls.Add(lblDateRange);
            panelFilters.Controls.Add(dtpStart);
            panelFilters.Controls.Add(lblDash);
            panelFilters.Controls.Add(dtpEnd);
            panelFilters.Controls.Add(lblLogLevel);
            panelFilters.Controls.Add(cmbLogLevel);
            panelFilters.Controls.Add(lblModule);
            panelFilters.Controls.Add(cmbModule);
            panelFilters.Controls.Add(lblUser);
            panelFilters.Controls.Add(cmbUser);
            panelFilters.Controls.Add(lblAction);
            panelFilters.Controls.Add(cmbAction);
            panelFilters.Controls.Add(lblKeyword);
            panelFilters.Controls.Add(txtKeyword);
            panelFilters.Controls.Add(btnSearch);
            panelFilters.Controls.Add(btnReset);
            panelFilters.Location = new Point(39, 120);
            panelFilters.Margin = new Padding(5);
            panelFilters.Name = "panelFilters";
            panelFilters.Size = new Size(2442, 160);
            panelFilters.TabIndex = 2;
            // 
            // lblDateRange
            // 
            lblDateRange.AutoSize = true;
            lblDateRange.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblDateRange.ForeColor = Color.FromArgb(71, 85, 105);
            lblDateRange.Location = new Point(32, 29);
            lblDateRange.Margin = new Padding(5, 0, 5, 0);
            lblDateRange.Name = "lblDateRange";
            lblDateRange.Size = new Size(137, 31);
            lblDateRange.TabIndex = 2;
            lblDateRange.Text = "Date Range";
            // 
            // dtpStart
            // 
            dtpStart.Font = new Font("Segoe UI", 9.5F);
            dtpStart.Format = DateTimePickerFormat.Short;
            dtpStart.Location = new Point(32, 90);
            dtpStart.Margin = new Padding(5);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(200, 41);
            dtpStart.TabIndex = 3;
            // 
            // lblDash
            // 
            lblDash.AutoSize = true;
            lblDash.Font = new Font("Segoe UI", 9.5F);
            lblDash.Location = new Point(244, 96);
            lblDash.Margin = new Padding(5, 0, 5, 0);
            lblDash.Name = "lblDash";
            lblDash.Size = new Size(25, 36);
            lblDash.TabIndex = 4;
            lblDash.Text = "-";
            // 
            // dtpEnd
            // 
            dtpEnd.Font = new Font("Segoe UI", 9.5F);
            dtpEnd.Format = DateTimePickerFormat.Short;
            dtpEnd.Location = new Point(280, 90);
            dtpEnd.Margin = new Padding(5);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(200, 41);
            dtpEnd.TabIndex = 5;
            // 
            // lblLogLevel
            // 
            lblLogLevel.AutoSize = true;
            lblLogLevel.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblLogLevel.ForeColor = Color.FromArgb(71, 85, 105);
            lblLogLevel.Location = new Point(515, 29);
            lblLogLevel.Margin = new Padding(5, 0, 5, 0);
            lblLogLevel.Name = "lblLogLevel";
            lblLogLevel.Size = new Size(115, 31);
            lblLogLevel.TabIndex = 6;
            lblLogLevel.Text = "Log Level";
            // 
            // cmbLogLevel
            // 
            cmbLogLevel.FlatStyle = FlatStyle.System;
            cmbLogLevel.Font = new Font("Segoe UI", 9.5F);
            cmbLogLevel.FormattingEnabled = true;
            cmbLogLevel.Items.AddRange(new object[] { "All", "INFO", "WARNING", "ERROR" });
            cmbLogLevel.Location = new Point(515, 90);
            cmbLogLevel.Margin = new Padding(5);
            cmbLogLevel.Name = "cmbLogLevel";
            cmbLogLevel.Size = new Size(238, 43);
            cmbLogLevel.TabIndex = 7;
            cmbLogLevel.Text = "All";
            // 
            // lblModule
            // 
            lblModule.AutoSize = true;
            lblModule.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblModule.ForeColor = Color.FromArgb(71, 85, 105);
            lblModule.Location = new Point(791, 29);
            lblModule.Margin = new Padding(5, 0, 5, 0);
            lblModule.Name = "lblModule";
            lblModule.Size = new Size(97, 31);
            lblModule.TabIndex = 8;
            lblModule.Text = "Module";
            // 
            // cmbModule
            // 
            cmbModule.FlatStyle = FlatStyle.System;
            cmbModule.Font = new Font("Segoe UI", 9.5F);
            cmbModule.FormattingEnabled = true;
            cmbModule.Items.AddRange(new object[] { "All", "Test Execution", "Communication", "Harness Database", "Database", "User Management", "System" });
            cmbModule.Location = new Point(791, 90);
            cmbModule.Margin = new Padding(5);
            cmbModule.Name = "cmbModule";
            cmbModule.Size = new Size(244, 43);
            cmbModule.TabIndex = 9;
            cmbModule.Text = "All";
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblUser.ForeColor = Color.FromArgb(71, 85, 105);
            lblUser.Location = new Point(1066, 30);
            lblUser.Margin = new Padding(5, 0, 5, 0);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(62, 31);
            lblUser.TabIndex = 10;
            lblUser.Text = "User";
            // 
            // cmbUser
            // 
            cmbUser.FlatStyle = FlatStyle.System;
            cmbUser.Font = new Font("Segoe UI", 9.5F);
            cmbUser.FormattingEnabled = true;
            cmbUser.Items.AddRange(new object[] { "All", "System", "Fally" });
            cmbUser.Location = new Point(1066, 91);
            cmbUser.Margin = new Padding(5);
            cmbUser.Name = "cmbUser";
            cmbUser.Size = new Size(170, 43);
            cmbUser.TabIndex = 11;
            cmbUser.Text = "All";
            // 
            // lblAction
            // 
            lblAction.AutoSize = true;
            lblAction.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblAction.ForeColor = Color.FromArgb(71, 85, 105);
            lblAction.Location = new Point(1263, 30);
            lblAction.Margin = new Padding(5, 0, 5, 0);
            lblAction.Name = "lblAction";
            lblAction.Size = new Size(85, 31);
            lblAction.TabIndex = 12;
            lblAction.Text = "Action";
            // 
            // cmbAction
            // 
            cmbAction.FlatStyle = FlatStyle.System;
            cmbAction.Font = new Font("Segoe UI", 9.5F);
            cmbAction.FormattingEnabled = true;
            cmbAction.Items.AddRange(new object[] { "All", "Start Test", "Load Program", "Step Complete", "Test Complete", "Update", "Connection Lost", "Reconnected", "Login", "Backup" });
            cmbAction.Location = new Point(1263, 91);
            cmbAction.Margin = new Padding(5);
            cmbAction.Name = "cmbAction";
            cmbAction.Size = new Size(248, 43);
            cmbAction.TabIndex = 13;
            cmbAction.Text = "All";
            // 
            // lblKeyword
            // 
            lblKeyword.AutoSize = true;
            lblKeyword.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblKeyword.ForeColor = Color.FromArgb(71, 85, 105);
            lblKeyword.Location = new Point(1539, 29);
            lblKeyword.Margin = new Padding(5, 0, 5, 0);
            lblKeyword.Name = "lblKeyword";
            lblKeyword.Size = new Size(108, 31);
            lblKeyword.TabIndex = 14;
            lblKeyword.Text = "Keyword";
            // 
            // txtKeyword
            // 
            txtKeyword.Font = new Font("Segoe UI", 9.5F);
            txtKeyword.Location = new Point(1539, 90);
            txtKeyword.Margin = new Padding(5);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderText = "Search log...";
            txtKeyword.Size = new Size(351, 41);
            txtKeyword.TabIndex = 15;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(250, 204, 21);
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnSearch.ForeColor = Color.FromArgb(15, 23, 42);
            btnSearch.Location = new Point(1916, 87);
            btnSearch.Margin = new Padding(5);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(160, 44);
            btnSearch.TabIndex = 16;
            btnSearch.Text = "🔍 Search";
            btnSearch.UseVisualStyleBackColor = false;
            // 
            // btnReset
            // 
            btnReset.BackColor = Color.FromArgb(248, 250, 252);
            btnReset.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnReset.ForeColor = Color.FromArgb(71, 85, 105);
            btnReset.Location = new Point(2096, 87);
            btnReset.Margin = new Padding(5);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(140, 44);
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
            pnlLeft.Controls.Add(btnPagePrev);
            pnlLeft.Controls.Add(btnPage1);
            pnlLeft.Controls.Add(btnPage2);
            pnlLeft.Controls.Add(btnPage3);
            pnlLeft.Controls.Add(btnPageNext);
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
            // btnPagePrev
            // 
            btnPagePrev.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnPagePrev.BackColor = Color.White;
            btnPagePrev.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnPagePrev.FlatStyle = FlatStyle.Flat;
            btnPagePrev.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnPagePrev.ForeColor = Color.FromArgb(71, 85, 105);
            btnPagePrev.Location = new Point(947, 908);
            btnPagePrev.Margin = new Padding(5);
            btnPagePrev.Name = "btnPagePrev";
            btnPagePrev.Size = new Size(138, 61);
            btnPagePrev.TabIndex = 4;
            btnPagePrev.Text = "Previous";
            btnPagePrev.UseVisualStyleBackColor = false;
            // 
            // btnPage1
            // 
            btnPage1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnPage1.BackColor = Color.FromArgb(37, 99, 235);
            btnPage1.FlatAppearance.BorderColor = Color.FromArgb(37, 99, 235);
            btnPage1.FlatStyle = FlatStyle.Flat;
            btnPage1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnPage1.ForeColor = Color.White;
            btnPage1.Location = new Point(1098, 908);
            btnPage1.Margin = new Padding(5);
            btnPage1.Name = "btnPage1";
            btnPage1.Size = new Size(62, 61);
            btnPage1.TabIndex = 5;
            btnPage1.Text = "1";
            btnPage1.UseVisualStyleBackColor = false;
            // 
            // btnPage2
            // 
            btnPage2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnPage2.BackColor = Color.White;
            btnPage2.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnPage2.FlatStyle = FlatStyle.Flat;
            btnPage2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnPage2.ForeColor = Color.FromArgb(71, 85, 105);
            btnPage2.Location = new Point(1173, 908);
            btnPage2.Margin = new Padding(5);
            btnPage2.Name = "btnPage2";
            btnPage2.Size = new Size(62, 61);
            btnPage2.TabIndex = 6;
            btnPage2.Text = "2";
            btnPage2.UseVisualStyleBackColor = false;
            // 
            // btnPage3
            // 
            btnPage3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnPage3.BackColor = Color.White;
            btnPage3.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnPage3.FlatStyle = FlatStyle.Flat;
            btnPage3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnPage3.ForeColor = Color.FromArgb(71, 85, 105);
            btnPage3.Location = new Point(1248, 908);
            btnPage3.Margin = new Padding(5);
            btnPage3.Name = "btnPage3";
            btnPage3.Size = new Size(62, 61);
            btnPage3.TabIndex = 7;
            btnPage3.Text = "3";
            btnPage3.UseVisualStyleBackColor = false;
            // 
            // btnPageNext
            // 
            btnPageNext.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnPageNext.BackColor = Color.White;
            btnPageNext.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnPageNext.FlatStyle = FlatStyle.Flat;
            btnPageNext.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnPageNext.ForeColor = Color.FromArgb(71, 85, 105);
            btnPageNext.Location = new Point(1323, 908);
            btnPageNext.Margin = new Padding(5);
            btnPageNext.Name = "btnPageNext";
            btnPageNext.Size = new Size(138, 61);
            btnPageNext.TabIndex = 8;
            btnPageNext.Text = "Next";
            btnPageNext.UseVisualStyleBackColor = false;
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
            pnlModuleStats.Location = new Point(5, 659);
            pnlModuleStats.Margin = new Padding(5);
            pnlModuleStats.Name = "pnlModuleStats";
            pnlModuleStats.Size = new Size(691, 328);
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
            panelFilters.PerformLayout();
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



        // Layout Components

        private System.Windows.Forms.TableLayoutPanel mainLayout;

        private System.Windows.Forms.Panel pnlLeft;

        private System.Windows.Forms.Button btnExport;

        private System.Windows.Forms.Button btnRefresh;

        private System.Windows.Forms.Label lblTableTitle;
        private System.Windows.Forms.DataGridView gridData;
        private System.Windows.Forms.Label lblPaginationInfo;
        private System.Windows.Forms.Button btnPagePrev;
        private System.Windows.Forms.Button btnPage1;
        private System.Windows.Forms.Button btnPage2;
        private System.Windows.Forms.Button btnPage3;
        private System.Windows.Forms.Button btnPageNext;
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
