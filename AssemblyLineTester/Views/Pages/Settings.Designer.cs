namespace AssemblyLineTester.Views.Pages

{

    partial class Settings

    {

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)

        {

            if (disposing) { components?.Dispose(); }

            base.Dispose(disposing);

        }

        #region Component Designer generated code

        private void InitializeComponent()

        {
            tblMain = new TableLayoutPanel();
            lblHeader = new Label();
            flpTabs = new FlowLayoutPanel();
            lblTabGen = new Label();
            lblTabTest = new Label();
            lblTabHw = new Label();
            lblTabUsers = new Label();
            lblTabNotif = new Label();
            lblTabBackup = new Label();
            lblTabSysInfo = new Label();
            tblContent = new TableLayoutPanel();
            tblLeftColumn = new TableLayoutPanel();
            cardGeneral = new Panel();
            tblGenCard = new TableLayoutPanel();
            lblGenTitle = new Label();
            lblCompName = new Label();
            lblSysName = new Label();
            txtCompanyName = new TextBox();
            txtSystemName = new TextBox();
            lblLang = new Label();
            lblDateFormat = new Label();
            lblTimeFormat = new Label();
            cmbLang = new ComboBox();
            cmbDateFormat = new ComboBox();
            cmbTimeFormat = new ComboBox();
            lblAutoLogout = new Label();
            chkConfirmExit = new CheckBox();
            chkDarkMode = new CheckBox();
            cmbAutoLogout = new ComboBox();
            cardTestSettings = new Panel();
            tblTestCard = new TableLayoutPanel();
            lblTestTitle = new Label();
            lblTestType = new Label();
            lblDefArea = new Label();
            lblDefOp = new Label();
            cmbTestType = new ComboBox();
            cmbDefaultArea = new ComboBox();
            cmbDefaultOp = new ComboBox();
            lblResLow = new Label();
            lblResHigh = new Label();
            lblResTol = new Label();
            txtResLow = new TextBox();
            txtResHigh = new TextBox();
            txtResTol = new TextBox();
            chkAutoStart = new CheckBox();
            chkBuzzer = new CheckBox();
            chkPauseFail = new CheckBox();
            cardFileSettings = new Panel();
            tblFileCard = new TableLayoutPanel();
            lblFileTitle = new Label();
            lblDataStorage = new Label();
            lblReportStorage = new Label();
            tblDataBrowse = new TableLayoutPanel();
            txtDataPath = new TextBox();
            btnBrowseData = new Button();
            tblReportBrowse = new TableLayoutPanel();
            txtReportPath = new TextBox();
            btnBrowseReport = new Button();
            lblLogRet = new Label();
            lblMaxSize = new Label();
            txtLogRet = new TextBox();
            txtMaxSize = new TextBox();
            chkAutoDelete = new CheckBox();
            cardAppSettings = new Panel();
            tblAppCard = new TableLayoutPanel();
            lblAppTitle = new Label();
            chkCheckUpdate = new CheckBox();
            lblRefresh = new Label();
            chkMinTray = new CheckBox();
            cmbRefresh = new ComboBox();
            chkSoundNotif = new CheckBox();
            chkConfirmDelete = new CheckBox();
            chkAuditTrail = new CheckBox();
            tblRightColumn = new TableLayoutPanel();
            cardSystemStatus = new Panel();
            tblStatusCard = new TableLayoutPanel();
            lblStatusTitle = new Label();
            tblStatusGrid = new TableLayoutPanel();
            lblStKeyApp = new Label();
            lblStValApp = new Label();
            lblStKeyDb = new Label();
            lblStValDb = new Label();
            lblStKeyFile = new Label();
            lblStValFile = new Label();
            lblStKeyLic = new Label();
            lblStValLic = new Label();
            lblStKeyStart = new Label();
            lblStValStart = new Label();
            lblStKeyUptime = new Label();
            lblStValUptime = new Label();
            cardLicenseInfo = new Panel();
            tblLicenseCard = new TableLayoutPanel();
            lblLicenseTitle = new Label();
            tblLicenseGrid = new TableLayoutPanel();
            lblLicKeyType = new Label();
            lblLicValType = new Label();
            lblLicKeyKey = new Label();
            lblLicValKey = new Label();
            lblLicKeyFrom = new Label();
            lblLicValFrom = new Label();
            lblLicKeyTo = new Label();
            lblLicValTo = new Label();
            lblLicKeyToOrg = new Label();
            lblLicValToOrg = new Label();
            btnManageLicense = new Button();
            cardSystemTime = new Panel();
            tblTimeCard = new TableLayoutPanel();
            lblTimeTitle = new Label();
            tblTimeGrid = new TableLayoutPanel();
            lblTimeKeyDate = new Label();
            lblTimeValDate = new Label();
            lblTimeKeyTime = new Label();
            lblTimeValTime = new Label();
            lblTimeKeyZone = new Label();
            cmbTimeZone = new ComboBox();
            btnSyncTime = new Button();
            tblMain.SuspendLayout();
            flpTabs.SuspendLayout();
            tblContent.SuspendLayout();
            tblLeftColumn.SuspendLayout();
            cardGeneral.SuspendLayout();
            tblGenCard.SuspendLayout();
            cardTestSettings.SuspendLayout();
            tblTestCard.SuspendLayout();
            cardFileSettings.SuspendLayout();
            tblFileCard.SuspendLayout();
            tblDataBrowse.SuspendLayout();
            tblReportBrowse.SuspendLayout();
            cardAppSettings.SuspendLayout();
            tblAppCard.SuspendLayout();
            tblRightColumn.SuspendLayout();
            cardSystemStatus.SuspendLayout();
            tblStatusCard.SuspendLayout();
            tblStatusGrid.SuspendLayout();
            cardLicenseInfo.SuspendLayout();
            tblLicenseCard.SuspendLayout();
            tblLicenseGrid.SuspendLayout();
            cardSystemTime.SuspendLayout();
            tblTimeCard.SuspendLayout();
            tblTimeGrid.SuspendLayout();
            SuspendLayout();
            // 
            // tblMain
            // 
            tblMain.AutoScroll = true;
            tblMain.ColumnCount = 1;
            tblMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblMain.Controls.Add(lblHeader, 0, 0);
            tblMain.Controls.Add(flpTabs, 0, 1);
            tblMain.Controls.Add(tblContent, 0, 2);
            tblMain.Dock = DockStyle.Fill;
            tblMain.Location = new Point(0, 0);
            tblMain.Margin = new Padding(8);
            tblMain.Name = "tblMain";
            tblMain.RowCount = 3;
            tblMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
            tblMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
            tblMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblMain.Size = new Size(2046, 1461);
            tblMain.TabIndex = 0;
            // 
            // lblHeader
            // 
            lblHeader.Dock = DockStyle.Top;
            lblHeader.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblHeader.ForeColor = Color.FromArgb(30, 41, 59);
            lblHeader.Location = new Point(39, 21);
            lblHeader.Margin = new Padding(39, 21, 39, 13);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(1968, 56);
            lblHeader.TabIndex = 0;
            lblHeader.Text = "⚙️  SETTINGS";
            lblHeader.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // flpTabs
            // 
            flpTabs.Controls.Add(lblTabGen);
            flpTabs.Controls.Add(lblTabTest);
            flpTabs.Controls.Add(lblTabHw);
            flpTabs.Controls.Add(lblTabUsers);
            flpTabs.Controls.Add(lblTabNotif);
            flpTabs.Controls.Add(lblTabBackup);
            flpTabs.Controls.Add(lblTabSysInfo);
            flpTabs.Dock = DockStyle.Fill;
            flpTabs.Location = new Point(39, 90);
            flpTabs.Margin = new Padding(39, 0, 39, 0);
            flpTabs.Name = "flpTabs";
            flpTabs.Size = new Size(1968, 90);
            flpTabs.TabIndex = 1;
            // lblTabGen
            // 
            lblTabGen.AutoSize = true;
            lblTabGen.Cursor = Cursors.Hand;
            lblTabGen.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblTabGen.ForeColor = Color.FromArgb(30, 41, 59);
            lblTabGen.Location = new Point(0, 21);
            lblTabGen.Margin = new Padding(0, 21, 67, 0);
            lblTabGen.Name = "lblTabGen";
            lblTabGen.Size = new Size(111, 38);
            lblTabGen.TabIndex = 0;
            lblTabGen.Text = "General";
            // 
            // lblTabTest
            // 
            lblTabTest.AutoSize = true;
            lblTabTest.Cursor = Cursors.Hand;
            lblTabTest.Font = new Font("Segoe UI", 10.5F);
            lblTabTest.ForeColor = Color.FromArgb(100, 116, 139);
            lblTabTest.Location = new Point(379, 21);
            lblTabTest.Margin = new Padding(0, 21, 67, 0);
            lblTabTest.Name = "lblTabTest";
            lblTabTest.Size = new Size(211, 38);
            lblTabTest.TabIndex = 1;
            lblTabTest.Text = "Test Parameters";
            // 
            // lblTabHw
            // 
            lblTabHw.AutoSize = true;
            lblTabHw.Cursor = Cursors.Hand;
            lblTabHw.Font = new Font("Segoe UI", 10.5F);
            lblTabHw.ForeColor = Color.FromArgb(100, 116, 139);
            lblTabHw.Location = new Point(657, 21);
            lblTabHw.Margin = new Padding(0, 21, 67, 0);
            lblTabHw.Name = "lblTabHw";
            lblTabHw.Size = new Size(313, 38);
            lblTabHw.TabIndex = 2;
            lblTabHw.Text = "Hardware Configuration";
            // 
            // lblTabUsers
            // 
            lblTabUsers.AutoSize = true;
            lblTabUsers.Cursor = Cursors.Hand;
            lblTabUsers.Font = new Font("Segoe UI", 10.5F);
            lblTabUsers.ForeColor = Color.FromArgb(100, 116, 139);
            lblTabUsers.Location = new Point(1037, 21);
            lblTabUsers.Margin = new Padding(0, 21, 67, 0);
            lblTabUsers.Name = "lblTabUsers";
            lblTabUsers.Size = new Size(167, 38);
            lblTabUsers.TabIndex = 3;
            lblTabUsers.Text = "Users & Roles";
            // 
            // lblTabNotif
            // 
            lblTabNotif.AutoSize = true;
            lblTabNotif.Cursor = Cursors.Hand;
            lblTabNotif.Font = new Font("Segoe UI", 10.5F);
            lblTabNotif.ForeColor = Color.FromArgb(100, 116, 139);
            lblTabNotif.Location = new Point(1271, 21);
            lblTabNotif.Margin = new Padding(0, 21, 67, 0);
            lblTabNotif.Name = "lblTabNotif";
            lblTabNotif.Size = new Size(173, 38);
            lblTabNotif.TabIndex = 4;
            lblTabNotif.Text = "Notifications";
            // 
            // lblTabBackup
            // 
            lblTabBackup.AutoSize = true;
            lblTabBackup.Cursor = Cursors.Hand;
            lblTabBackup.Font = new Font("Segoe UI", 10.5F);
            lblTabBackup.ForeColor = Color.FromArgb(100, 116, 139);
            lblTabBackup.Location = new Point(1511, 21);
            lblTabBackup.Margin = new Padding(0, 21, 67, 0);
            lblTabBackup.Name = "lblTabBackup";
            lblTabBackup.Size = new Size(215, 38);
            lblTabBackup.TabIndex = 5;
            lblTabBackup.Text = "Backup & Restore";
            // 
            // lblTabSysInfo
            // 
            lblTabSysInfo.AutoSize = true;
            lblTabSysInfo.Cursor = Cursors.Hand;
            lblTabSysInfo.Font = new Font("Segoe UI", 10.5F);
            lblTabSysInfo.ForeColor = Color.FromArgb(100, 116, 139);
            lblTabSysInfo.Location = new Point(0, 119);
            lblTabSysInfo.Margin = new Padding(0, 21, 67, 0);
            lblTabSysInfo.Name = "lblTabSysInfo";
            lblTabSysInfo.Size = new Size(257, 38);
            lblTabSysInfo.TabIndex = 6;
            lblTabSysInfo.Text = "System Information";
            // 
            // tblContent
            // 
            tblContent.AutoScroll = true;
            tblContent.ColumnCount = 2;
            tblContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tblContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tblContent.Controls.Add(tblLeftColumn, 0, 0);
            tblContent.Controls.Add(tblRightColumn, 1, 0);
            tblContent.Dock = DockStyle.Fill;
            tblContent.Location = new Point(26, 180);
            tblContent.Margin = new Padding(26, 0, 26, 0);
            tblContent.Name = "tblContent";
            tblContent.RowCount = 1;
            tblContent.RowStyles.Add(new RowStyle());
            tblContent.Size = new Size(1994, 1281);
            tblContent.TabIndex = 2;
            // 
            // tblLeftColumn
            // 
            tblLeftColumn.AutoSize = true;
            tblLeftColumn.ColumnCount = 1;
            tblLeftColumn.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblLeftColumn.Controls.Add(cardGeneral, 0, 0);
            tblLeftColumn.Controls.Add(cardTestSettings, 0, 1);
            tblLeftColumn.Controls.Add(cardFileSettings, 0, 2);
            tblLeftColumn.Controls.Add(cardAppSettings, 0, 3);
            tblLeftColumn.Dock = DockStyle.Fill;
            tblLeftColumn.Location = new Point(13, 13);
            tblLeftColumn.Margin = new Padding(13);
            tblLeftColumn.Name = "tblLeftColumn";
            tblLeftColumn.RowCount = 4;
            tblLeftColumn.RowStyles.Add(new RowStyle());
            tblLeftColumn.RowStyles.Add(new RowStyle());
            tblLeftColumn.RowStyles.Add(new RowStyle());
            tblLeftColumn.RowStyles.Add(new RowStyle());
            tblLeftColumn.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tblLeftColumn.Size = new Size(1270, 1404);
            tblLeftColumn.TabIndex = 0;
            // 
            // cardGeneral
            // 
            cardGeneral.AutoSize = true;
            cardGeneral.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            cardGeneral.BackColor = Color.White;
            cardGeneral.Controls.Add(tblGenCard);
            cardGeneral.Dock = DockStyle.Fill;
            cardGeneral.Location = new Point(0, 0);
            cardGeneral.Margin = new Padding(0, 0, 0, 19);
            cardGeneral.Name = "cardGeneral";
            cardGeneral.Size = new Size(1270, 372);
            cardGeneral.TabIndex = 0;
            // 
            // tblGenCard
            // 
            tblGenCard.AutoSize = true;
            tblGenCard.ColumnCount = 6;
            tblGenCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666F));
            tblGenCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666F));
            tblGenCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666F));
            tblGenCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666F));
            tblGenCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666F));
            tblGenCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.67F));
            tblGenCard.Controls.Add(lblGenTitle, 0, 0);
            tblGenCard.Controls.Add(lblCompName, 0, 1);
            tblGenCard.Controls.Add(lblSysName, 3, 1);
            tblGenCard.Controls.Add(txtCompanyName, 0, 2);
            tblGenCard.Controls.Add(txtSystemName, 3, 2);
            tblGenCard.Controls.Add(lblLang, 0, 3);
            tblGenCard.Controls.Add(lblDateFormat, 2, 3);
            tblGenCard.Controls.Add(lblTimeFormat, 4, 3);
            tblGenCard.Controls.Add(cmbLang, 0, 4);
            tblGenCard.Controls.Add(cmbDateFormat, 2, 4);
            tblGenCard.Controls.Add(cmbTimeFormat, 4, 4);
            tblGenCard.Controls.Add(lblAutoLogout, 0, 5);
            tblGenCard.Controls.Add(chkConfirmExit, 2, 5);
            tblGenCard.Controls.Add(chkDarkMode, 4, 5);
            tblGenCard.Controls.Add(cmbAutoLogout, 0, 6);
            tblGenCard.Dock = DockStyle.Top;
            tblGenCard.Location = new Point(0, 0);
            tblGenCard.Margin = new Padding(8);
            tblGenCard.Name = "tblGenCard";
            tblGenCard.Padding = new Padding(15, 0, 15, 15);
            tblGenCard.RowCount = 7;
            tblGenCard.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tblGenCard.RowStyles.Add(new RowStyle());
            tblGenCard.RowStyles.Add(new RowStyle());
            tblGenCard.RowStyles.Add(new RowStyle());
            tblGenCard.RowStyles.Add(new RowStyle());
            tblGenCard.RowStyles.Add(new RowStyle());
            tblGenCard.RowStyles.Add(new RowStyle());
            tblGenCard.Size = new Size(1270, 372);
            tblGenCard.TabIndex = 0;
            // 
            // lblGenTitle
            // 
            tblGenCard.SetColumnSpan(lblGenTitle, 6);
            lblGenTitle.Dock = DockStyle.Fill;
            lblGenTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblGenTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblGenTitle.Location = new Point(15, 0);
            lblGenTitle.Margin = new Padding(0, 0, 0, 14);
            lblGenTitle.Name = "lblGenTitle";
            lblGenTitle.Size = new Size(1240, 36);
            lblGenTitle.TabIndex = 0;
            lblGenTitle.Text = "GENERAL SETTINGS";
            lblGenTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblCompName
            // 
            lblCompName.AutoSize = true;
            tblGenCard.SetColumnSpan(lblCompName, 3);
            lblCompName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCompName.ForeColor = Color.FromArgb(51, 65, 85);
            lblCompName.Location = new Point(15, 60);
            lblCompName.Margin = new Padding(0, 10, 0, 4);
            lblCompName.Name = "lblCompName";
            lblCompName.Size = new Size(196, 32);
            lblCompName.TabIndex = 1;
            lblCompName.Text = "Company Name";
            // 
            // lblSysName
            // 
            lblSysName.AutoSize = true;
            tblGenCard.SetColumnSpan(lblSysName, 3);
            lblSysName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSysName.ForeColor = Color.FromArgb(51, 65, 85);
            lblSysName.Location = new Point(648, 60);
            lblSysName.Margin = new Padding(15, 10, 0, 4);
            lblSysName.Name = "lblSysName";
            lblSysName.Size = new Size(168, 32);
            lblSysName.TabIndex = 2;
            lblSysName.Text = "System Name";
            // 
            // txtCompanyName
            // 
            txtCompanyName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tblGenCard.SetColumnSpan(txtCompanyName, 3);
            txtCompanyName.Font = new Font("Segoe UI", 9.5F);
            txtCompanyName.Location = new Point(15, 96);
            txtCompanyName.Margin = new Padding(0, 0, 15, 14);
            txtCompanyName.Name = "txtCompanyName";
            txtCompanyName.Size = new Size(603, 41);
            txtCompanyName.TabIndex = 3;
            txtCompanyName.Text = "Caterpillar Inc.";
            // 
            // txtSystemName
            // 
            txtSystemName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tblGenCard.SetColumnSpan(txtSystemName, 3);
            txtSystemName.Font = new Font("Segoe UI", 9.5F);
            txtSystemName.Location = new Point(648, 96);
            txtSystemName.Margin = new Padding(15, 0, 0, 14);
            txtSystemName.Name = "txtSystemName";
            txtSystemName.Size = new Size(607, 41);
            txtSystemName.TabIndex = 4;
            txtSystemName.Text = "Assembly Line Test Rig #4";
            // 
            // lblLang
            // 
            lblLang.AutoSize = true;
            tblGenCard.SetColumnSpan(lblLang, 2);
            lblLang.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblLang.ForeColor = Color.FromArgb(51, 65, 85);
            lblLang.Location = new Point(15, 161);
            lblLang.Margin = new Padding(0, 10, 0, 4);
            lblLang.Name = "lblLang";
            lblLang.Size = new Size(125, 32);
            lblLang.TabIndex = 5;
            lblLang.Text = "Language";
            // 
            // lblDateFormat
            // 
            lblDateFormat.AutoSize = true;
            tblGenCard.SetColumnSpan(lblDateFormat, 2);
            lblDateFormat.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDateFormat.ForeColor = Color.FromArgb(51, 65, 85);
            lblDateFormat.Location = new Point(437, 161);
            lblDateFormat.Margin = new Padding(10, 10, 0, 4);
            lblDateFormat.Name = "lblDateFormat";
            lblDateFormat.Size = new Size(155, 32);
            lblDateFormat.TabIndex = 6;
            lblDateFormat.Text = "Date Format";
            // 
            // lblTimeFormat
            // 
            lblTimeFormat.AutoSize = true;
            tblGenCard.SetColumnSpan(lblTimeFormat, 2);
            lblTimeFormat.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTimeFormat.ForeColor = Color.FromArgb(51, 65, 85);
            lblTimeFormat.Location = new Point(849, 161);
            lblTimeFormat.Margin = new Padding(10, 10, 0, 4);
            lblTimeFormat.Name = "lblTimeFormat";
            lblTimeFormat.Size = new Size(158, 32);
            lblTimeFormat.TabIndex = 7;
            lblTimeFormat.Text = "Time Format";
            // 
            // cmbLang
            // 
            cmbLang.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tblGenCard.SetColumnSpan(cmbLang, 2);
            cmbLang.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLang.FlatStyle = FlatStyle.Flat;
            cmbLang.Font = new Font("Segoe UI", 9.5F);
            cmbLang.Items.AddRange(new object[] { "English", "Spanish", "German", "Japanese" });
            cmbLang.Location = new Point(15, 197);
            cmbLang.Margin = new Padding(0, 0, 10, 14);
            cmbLang.Name = "cmbLang";
            cmbLang.Size = new Size(402, 43);
            cmbLang.TabIndex = 8;
            // 
            // cmbDateFormat
            // 
            cmbDateFormat.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tblGenCard.SetColumnSpan(cmbDateFormat, 2);
            cmbDateFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDateFormat.FlatStyle = FlatStyle.Flat;
            cmbDateFormat.Font = new Font("Segoe UI", 9.5F);
            cmbDateFormat.Items.AddRange(new object[] { "DD MMM YYYY", "YYYY-MM-DD", "MM/DD/YYYY" });
            cmbDateFormat.Location = new Point(437, 197);
            cmbDateFormat.Margin = new Padding(10, 0, 10, 14);
            cmbDateFormat.Name = "cmbDateFormat";
            cmbDateFormat.Size = new Size(392, 43);
            cmbDateFormat.TabIndex = 9;
            // 
            // cmbTimeFormat
            // 
            cmbTimeFormat.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tblGenCard.SetColumnSpan(cmbTimeFormat, 2);
            cmbTimeFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTimeFormat.FlatStyle = FlatStyle.Flat;
            cmbTimeFormat.Font = new Font("Segoe UI", 9.5F);
            cmbTimeFormat.Items.AddRange(new object[] { "12 Hour (AM/PM)", "24 Hour" });
            cmbTimeFormat.Location = new Point(849, 197);
            cmbTimeFormat.Margin = new Padding(10, 0, 0, 14);
            cmbTimeFormat.Name = "cmbTimeFormat";
            cmbTimeFormat.Size = new Size(406, 43);
            cmbTimeFormat.TabIndex = 10;
            // 
            // lblAutoLogout
            // 
            lblAutoLogout.AutoSize = true;
            tblGenCard.SetColumnSpan(lblAutoLogout, 2);
            lblAutoLogout.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAutoLogout.ForeColor = Color.FromArgb(51, 65, 85);
            lblAutoLogout.Location = new Point(15, 264);
            lblAutoLogout.Margin = new Padding(0, 10, 0, 4);
            lblAutoLogout.Name = "lblAutoLogout";
            lblAutoLogout.Size = new Size(158, 32);
            lblAutoLogout.TabIndex = 11;
            lblAutoLogout.Text = "Auto Logout";
            // 
            // chkConfirmExit
            // 
            chkConfirmExit.AutoSize = true;
            chkConfirmExit.Checked = true;
            chkConfirmExit.CheckState = CheckState.Checked;
            tblGenCard.SetColumnSpan(chkConfirmExit, 2);
            chkConfirmExit.Font = new Font("Segoe UI", 9.5F);
            chkConfirmExit.ForeColor = Color.FromArgb(30, 41, 59);
            chkConfirmExit.Location = new Point(437, 274);
            chkConfirmExit.Margin = new Padding(10, 20, 10, 10);
            chkConfirmExit.Name = "chkConfirmExit";
            tblGenCard.SetRowSpan(chkConfirmExit, 2);
            chkConfirmExit.Size = new Size(392, 40);
            chkConfirmExit.TabIndex = 12;
            chkConfirmExit.Text = "Show confirmation before exit";
            // 
            // chkDarkMode
            // 
            chkDarkMode.AutoSize = true;
            tblGenCard.SetColumnSpan(chkDarkMode, 2);
            chkDarkMode.Font = new Font("Segoe UI", 9.5F);
            chkDarkMode.ForeColor = Color.FromArgb(30, 41, 59);
            chkDarkMode.Location = new Point(849, 274);
            chkDarkMode.Margin = new Padding(10, 20, 10, 10);
            chkDarkMode.Name = "chkDarkMode";
            tblGenCard.SetRowSpan(chkDarkMode, 2);
            chkDarkMode.Size = new Size(253, 40);
            chkDarkMode.TabIndex = 13;
            chkDarkMode.Text = "Enable dark mode";
            // 
            // cmbAutoLogout
            // 
            cmbAutoLogout.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tblGenCard.SetColumnSpan(cmbAutoLogout, 2);
            cmbAutoLogout.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAutoLogout.FlatStyle = FlatStyle.Flat;
            cmbAutoLogout.Font = new Font("Segoe UI", 9.5F);
            cmbAutoLogout.Items.AddRange(new object[] { "30 Minutes", "15 Minutes", "1 Hour", "Never" });
            cmbAutoLogout.Location = new Point(15, 300);
            cmbAutoLogout.Margin = new Padding(0, 0, 10, 14);
            cmbAutoLogout.Name = "cmbAutoLogout";
            cmbAutoLogout.Size = new Size(402, 43);
            cmbAutoLogout.TabIndex = 14;
            // 
            // cardTestSettings
            // 
            cardTestSettings.AutoSize = true;
            cardTestSettings.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            cardTestSettings.BackColor = Color.White;
            cardTestSettings.Controls.Add(tblTestCard);
            cardTestSettings.Dock = DockStyle.Fill;
            cardTestSettings.Location = new Point(0, 391);
            cardTestSettings.Margin = new Padding(0, 0, 0, 19);
            cardTestSettings.Name = "cardTestSettings";
            cardTestSettings.Size = new Size(1270, 329);
            cardTestSettings.TabIndex = 1;
            // 
            // tblTestCard
            // 
            tblTestCard.AutoSize = true;
            tblTestCard.ColumnCount = 3;
            tblTestCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tblTestCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tblTestCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
            tblTestCard.Controls.Add(lblTestTitle, 0, 0);
            tblTestCard.Controls.Add(lblTestType, 0, 1);
            tblTestCard.Controls.Add(lblDefArea, 1, 1);
            tblTestCard.Controls.Add(lblDefOp, 2, 1);
            tblTestCard.Controls.Add(cmbTestType, 0, 2);
            tblTestCard.Controls.Add(cmbDefaultArea, 1, 2);
            tblTestCard.Controls.Add(cmbDefaultOp, 2, 2);
            tblTestCard.Controls.Add(lblResLow, 0, 3);
            tblTestCard.Controls.Add(lblResHigh, 1, 3);
            tblTestCard.Controls.Add(lblResTol, 2, 3);
            tblTestCard.Controls.Add(txtResLow, 0, 4);
            tblTestCard.Controls.Add(txtResHigh, 1, 4);
            tblTestCard.Controls.Add(txtResTol, 2, 4);
            tblTestCard.Controls.Add(chkAutoStart, 0, 5);
            tblTestCard.Controls.Add(chkBuzzer, 1, 5);
            tblTestCard.Controls.Add(chkPauseFail, 2, 5);
            tblTestCard.Dock = DockStyle.Top;
            tblTestCard.Location = new Point(0, 0);
            tblTestCard.Margin = new Padding(8);
            tblTestCard.Name = "tblTestCard";
            tblTestCard.Padding = new Padding(15, 0, 15, 15);
            tblTestCard.RowCount = 6;
            tblTestCard.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tblTestCard.RowStyles.Add(new RowStyle());
            tblTestCard.RowStyles.Add(new RowStyle());
            tblTestCard.RowStyles.Add(new RowStyle());
            tblTestCard.RowStyles.Add(new RowStyle());
            tblTestCard.RowStyles.Add(new RowStyle());
            tblTestCard.Size = new Size(1270, 329);
            tblTestCard.TabIndex = 0;
            // 
            // lblTestTitle
            // 
            tblTestCard.SetColumnSpan(lblTestTitle, 3);
            lblTestTitle.Dock = DockStyle.Fill;
            lblTestTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTestTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTestTitle.Location = new Point(15, 0);
            lblTestTitle.Margin = new Padding(0, 0, 0, 14);
            lblTestTitle.Name = "lblTestTitle";
            lblTestTitle.Size = new Size(1240, 36);
            lblTestTitle.TabIndex = 0;
            lblTestTitle.Text = "DEFAULT TEST SETTINGS";
            lblTestTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTestType
            // 
            lblTestType.AutoSize = true;
            lblTestType.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTestType.ForeColor = Color.FromArgb(51, 65, 85);
            lblTestType.Location = new Point(15, 60);
            lblTestType.Margin = new Padding(0, 10, 0, 4);
            lblTestType.Name = "lblTestType";
            lblTestType.Size = new Size(159, 32);
            lblTestType.TabIndex = 1;
            lblTestType.Text = "Default Type";
            // 
            // lblDefArea
            // 
            lblDefArea.AutoSize = true;
            lblDefArea.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDefArea.ForeColor = Color.FromArgb(51, 65, 85);
            lblDefArea.Location = new Point(443, 60);
            lblDefArea.Margin = new Padding(15, 10, 0, 4);
            lblDefArea.Name = "lblDefArea";
            lblDefArea.Size = new Size(158, 32);
            lblDefArea.TabIndex = 2;
            lblDefArea.Text = "Default Area";
            // 
            // lblDefOp
            // 
            lblDefOp.AutoSize = true;
            lblDefOp.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDefOp.ForeColor = Color.FromArgb(51, 65, 85);
            lblDefOp.Location = new Point(856, 60);
            lblDefOp.Margin = new Padding(15, 10, 0, 4);
            lblDefOp.Name = "lblDefOp";
            lblDefOp.Size = new Size(208, 32);
            lblDefOp.TabIndex = 3;
            lblDefOp.Text = "Default Operator";
            // 
            // cmbTestType
            // 
            cmbTestType.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbTestType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTestType.FlatStyle = FlatStyle.Flat;
            cmbTestType.Font = new Font("Segoe UI", 9.5F);
            cmbTestType.Items.AddRange(new object[] { "Standard Electrical Test", "Automated Sequence", "Quick Diagnostics", "Burn-in Cycle" });
            cmbTestType.Location = new Point(15, 96);
            cmbTestType.Margin = new Padding(0, 0, 15, 14);
            cmbTestType.Name = "cmbTestType";
            cmbTestType.Size = new Size(398, 43);
            cmbTestType.TabIndex = 4;
            // 
            // cmbDefaultArea
            // 
            cmbDefaultArea.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbDefaultArea.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDefaultArea.FlatStyle = FlatStyle.Flat;
            cmbDefaultArea.Font = new Font("Segoe UI", 9.5F);
            cmbDefaultArea.Items.AddRange(new object[] { "Zone A - Final Assembly", "Zone B - High Voltage", "Zone C - Packaging" });
            cmbDefaultArea.Location = new Point(443, 96);
            cmbDefaultArea.Margin = new Padding(15, 0, 15, 14);
            cmbDefaultArea.Name = "cmbDefaultArea";
            cmbDefaultArea.Size = new Size(383, 43);
            cmbDefaultArea.TabIndex = 5;
            // 
            // cmbDefaultOp
            // 
            cmbDefaultOp.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbDefaultOp.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDefaultOp.FlatStyle = FlatStyle.Flat;
            cmbDefaultOp.Font = new Font("Segoe UI", 9.5F);
            cmbDefaultOp.Items.AddRange(new object[] { "Current User", "Operator 1", "Supervisor", "Admin" });
            cmbDefaultOp.Location = new Point(856, 96);
            cmbDefaultOp.Margin = new Padding(15, 0, 0, 14);
            cmbDefaultOp.Name = "cmbDefaultOp";
            cmbDefaultOp.Size = new Size(399, 43);
            cmbDefaultOp.TabIndex = 6;
            // 
            // lblResLow
            // 
            lblResLow.AutoSize = true;
            lblResLow.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblResLow.ForeColor = Color.FromArgb(51, 65, 85);
            lblResLow.Location = new Point(15, 163);
            lblResLow.Margin = new Padding(0, 10, 0, 4);
            lblResLow.Name = "lblResLow";
            lblResLow.Size = new Size(339, 32);
            lblResLow.TabIndex = 7;
            lblResLow.Text = "Expected Resistance Low (Ω)";
            // 
            // lblResHigh
            // 
            lblResHigh.AutoSize = true;
            lblResHigh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblResHigh.ForeColor = Color.FromArgb(51, 65, 85);
            lblResHigh.Location = new Point(443, 163);
            lblResHigh.Margin = new Padding(15, 10, 0, 4);
            lblResHigh.Name = "lblResHigh";
            lblResHigh.Size = new Size(347, 32);
            lblResHigh.TabIndex = 8;
            lblResHigh.Text = "Expected Resistance High (Ω)";
            // 
            // lblResTol
            // 
            lblResTol.AutoSize = true;
            lblResTol.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblResTol.ForeColor = Color.FromArgb(51, 65, 85);
            lblResTol.Location = new Point(856, 163);
            lblResTol.Margin = new Padding(15, 10, 0, 4);
            lblResTol.Name = "lblResTol";
            lblResTol.Size = new Size(261, 32);
            lblResTol.TabIndex = 9;
            lblResTol.Text = "Default Tolerance (%)";
            // 
            // txtResLow
            // 
            txtResLow.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtResLow.Font = new Font("Segoe UI", 9.5F);
            txtResLow.Location = new Point(15, 199);
            txtResLow.Margin = new Padding(0, 0, 15, 14);
            txtResLow.Name = "txtResLow";
            txtResLow.Size = new Size(398, 41);
            txtResLow.TabIndex = 10;
            txtResLow.Text = "1.00";
            // 
            // txtResHigh
            // 
            txtResHigh.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtResHigh.Font = new Font("Segoe UI", 9.5F);
            txtResHigh.Location = new Point(443, 199);
            txtResHigh.Margin = new Padding(15, 0, 15, 14);
            txtResHigh.Name = "txtResHigh";
            txtResHigh.Size = new Size(383, 41);
            txtResHigh.TabIndex = 11;
            txtResHigh.Text = "10.00";
            // 
            // txtResTol
            // 
            txtResTol.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtResTol.Font = new Font("Segoe UI", 9.5F);
            txtResTol.Location = new Point(856, 199);
            txtResTol.Margin = new Padding(15, 0, 0, 14);
            txtResTol.Name = "txtResTol";
            txtResTol.Size = new Size(399, 41);
            txtResTol.TabIndex = 12;
            txtResTol.Text = "5.0";
            // 
            // chkAutoStart
            // 
            chkAutoStart.AutoSize = true;
            chkAutoStart.Checked = true;
            chkAutoStart.CheckState = CheckState.Checked;
            chkAutoStart.Font = new Font("Segoe UI", 9.5F);
            chkAutoStart.ForeColor = Color.FromArgb(30, 41, 59);
            chkAutoStart.Location = new Point(15, 264);
            chkAutoStart.Margin = new Padding(0, 10, 10, 10);
            chkAutoStart.Name = "chkAutoStart";
            chkAutoStart.Size = new Size(403, 40);
            chkAutoStart.TabIndex = 13;
            chkAutoStart.Text = "Auto start next test after completion";
            // 
            // chkBuzzer
            // 
            chkBuzzer.AutoSize = true;
            chkBuzzer.Checked = true;
            chkBuzzer.CheckState = CheckState.Checked;
            chkBuzzer.Font = new Font("Segoe UI", 9.5F);
            chkBuzzer.ForeColor = Color.FromArgb(30, 41, 59);
            chkBuzzer.Location = new Point(443, 264);
            chkBuzzer.Margin = new Padding(15, 10, 10, 10);
            chkBuzzer.Name = "chkBuzzer";
            chkBuzzer.Size = new Size(325, 40);
            chkBuzzer.TabIndex = 14;
            chkBuzzer.Text = "Buzzer on test complete";
            // 
            // chkPauseFail
            // 
            chkPauseFail.AutoSize = true;
            chkPauseFail.Checked = true;
            chkPauseFail.CheckState = CheckState.Checked;
            chkPauseFail.Font = new Font("Segoe UI", 9.5F);
            chkPauseFail.ForeColor = Color.FromArgb(30, 41, 59);
            chkPauseFail.Location = new Point(856, 264);
            chkPauseFail.Margin = new Padding(15, 10, 0, 10);
            chkPauseFail.Name = "chkPauseFail";
            chkPauseFail.Size = new Size(241, 40);
            chkPauseFail.TabIndex = 15;
            chkPauseFail.Text = "Pause on test fail";
            // 
            // cardFileSettings
            // 
            cardFileSettings.AutoSize = true;
            cardFileSettings.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            cardFileSettings.BackColor = Color.White;
            cardFileSettings.Controls.Add(tblFileCard);
            cardFileSettings.Dock = DockStyle.Fill;
            cardFileSettings.Location = new Point(0, 739);
            cardFileSettings.Margin = new Padding(0, 0, 0, 30);
            cardFileSettings.Name = "cardFileSettings";
            cardFileSettings.Size = new Size(1270, 327);
            cardFileSettings.TabIndex = 2;
            // 
            // tblFileCard
            // 
            tblFileCard.AutoSize = true;
            tblFileCard.ColumnCount = 2;
            tblFileCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblFileCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblFileCard.Controls.Add(lblFileTitle, 0, 0);
            tblFileCard.Controls.Add(lblDataStorage, 0, 1);
            tblFileCard.Controls.Add(lblReportStorage, 1, 1);
            tblFileCard.Controls.Add(tblDataBrowse, 0, 2);
            tblFileCard.Controls.Add(tblReportBrowse, 1, 2);
            tblFileCard.Controls.Add(lblLogRet, 0, 3);
            tblFileCard.Controls.Add(lblMaxSize, 1, 3);
            tblFileCard.Controls.Add(txtLogRet, 0, 4);
            tblFileCard.Controls.Add(txtMaxSize, 1, 4);
            tblFileCard.Controls.Add(chkAutoDelete, 0, 5);
            tblFileCard.Dock = DockStyle.Top;
            tblFileCard.Location = new Point(0, 0);
            tblFileCard.Margin = new Padding(8);
            tblFileCard.Name = "tblFileCard";
            tblFileCard.Padding = new Padding(15, 0, 15, 15);
            tblFileCard.RowCount = 6;
            tblFileCard.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tblFileCard.RowStyles.Add(new RowStyle());
            tblFileCard.RowStyles.Add(new RowStyle());
            tblFileCard.RowStyles.Add(new RowStyle());
            tblFileCard.RowStyles.Add(new RowStyle());
            tblFileCard.RowStyles.Add(new RowStyle());
            tblFileCard.Size = new Size(1270, 327);
            tblFileCard.TabIndex = 0;
            // 
            // lblFileTitle
            // 
            tblFileCard.SetColumnSpan(lblFileTitle, 2);
            lblFileTitle.Dock = DockStyle.Fill;
            lblFileTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblFileTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblFileTitle.Location = new Point(15, 0);
            lblFileTitle.Margin = new Padding(0, 0, 0, 14);
            lblFileTitle.Name = "lblFileTitle";
            lblFileTitle.Size = new Size(1240, 36);
            lblFileTitle.TabIndex = 0;
            lblFileTitle.Text = "FILE & DATA SETTINGS";
            lblFileTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDataStorage
            // 
            lblDataStorage.AutoSize = true;
            lblDataStorage.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDataStorage.ForeColor = Color.FromArgb(51, 65, 85);
            lblDataStorage.Location = new Point(15, 60);
            lblDataStorage.Margin = new Padding(0, 10, 0, 4);
            lblDataStorage.Name = "lblDataStorage";
            lblDataStorage.Size = new Size(218, 32);
            lblDataStorage.TabIndex = 1;
            lblDataStorage.Text = "Data Storage Path";
            // 
            // lblReportStorage
            // 
            lblReportStorage.AutoSize = true;
            lblReportStorage.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblReportStorage.ForeColor = Color.FromArgb(51, 65, 85);
            lblReportStorage.Location = new Point(650, 60);
            lblReportStorage.Margin = new Padding(15, 10, 0, 4);
            lblReportStorage.Name = "lblReportStorage";
            lblReportStorage.Size = new Size(243, 32);
            lblReportStorage.TabIndex = 2;
            lblReportStorage.Text = "Report Storage Path";
            // 
            // tblDataBrowse
            // 
            tblDataBrowse.AutoSize = true;
            tblDataBrowse.ColumnCount = 2;
            tblDataBrowse.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblDataBrowse.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            tblDataBrowse.Controls.Add(txtDataPath, 0, 0);
            tblDataBrowse.Controls.Add(btnBrowseData, 1, 0);
            tblDataBrowse.Dock = DockStyle.Fill;
            tblDataBrowse.Location = new Point(15, 96);
            tblDataBrowse.Margin = new Padding(0, 0, 15, 14);
            tblDataBrowse.Name = "tblDataBrowse";
            tblDataBrowse.RowCount = 1;
            tblDataBrowse.RowStyles.Add(new RowStyle());
            tblDataBrowse.Size = new Size(605, 41);
            tblDataBrowse.TabIndex = 3;
            // 
            // txtDataPath
            // 
            txtDataPath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtDataPath.Font = new Font("Segoe UI", 9.5F);
            txtDataPath.Location = new Point(0, 0);
            txtDataPath.Margin = new Padding(0, 0, 10, 0);
            txtDataPath.Name = "txtDataPath";
            txtDataPath.Size = new Size(485, 41);
            txtDataPath.TabIndex = 0;
            txtDataPath.Text = "C:\\Data\\TestResults";
            // 
            // btnBrowseData
            // 
            btnBrowseData.Anchor = AnchorStyles.None;
            btnBrowseData.BackColor = Color.FromArgb(241, 245, 249);
            btnBrowseData.Cursor = Cursors.Hand;
            btnBrowseData.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnBrowseData.FlatStyle = FlatStyle.Flat;
            btnBrowseData.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnBrowseData.ForeColor = Color.FromArgb(51, 65, 85);
            btnBrowseData.Location = new Point(495, 0);
            btnBrowseData.Margin = new Padding(0);
            btnBrowseData.Name = "btnBrowseData";
            btnBrowseData.Size = new Size(110, 41);
            btnBrowseData.TabIndex = 1;
            btnBrowseData.Text = "Browse...";
            btnBrowseData.UseVisualStyleBackColor = false;
            // 
            // tblReportBrowse
            // 
            tblReportBrowse.AutoSize = true;
            tblReportBrowse.ColumnCount = 2;
            tblReportBrowse.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblReportBrowse.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            tblReportBrowse.Controls.Add(txtReportPath, 0, 0);
            tblReportBrowse.Controls.Add(btnBrowseReport, 1, 0);
            tblReportBrowse.Dock = DockStyle.Fill;
            tblReportBrowse.Location = new Point(650, 96);
            tblReportBrowse.Margin = new Padding(15, 0, 0, 14);
            tblReportBrowse.Name = "tblReportBrowse";
            tblReportBrowse.RowCount = 1;
            tblReportBrowse.RowStyles.Add(new RowStyle());
            tblReportBrowse.Size = new Size(605, 41);
            tblReportBrowse.TabIndex = 4;
            // 
            // txtReportPath
            // 
            txtReportPath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtReportPath.Font = new Font("Segoe UI", 9.5F);
            txtReportPath.Location = new Point(0, 0);
            txtReportPath.Margin = new Padding(0, 0, 10, 0);
            txtReportPath.Name = "txtReportPath";
            txtReportPath.Size = new Size(485, 41);
            txtReportPath.TabIndex = 0;
            txtReportPath.Text = "C:\\Reports\\Production";
            // 
            // btnBrowseReport
            // 
            btnBrowseReport.Anchor = AnchorStyles.None;
            btnBrowseReport.BackColor = Color.FromArgb(241, 245, 249);
            btnBrowseReport.Cursor = Cursors.Hand;
            btnBrowseReport.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnBrowseReport.FlatStyle = FlatStyle.Flat;
            btnBrowseReport.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnBrowseReport.ForeColor = Color.FromArgb(51, 65, 85);
            btnBrowseReport.Location = new Point(495, 0);
            btnBrowseReport.Margin = new Padding(0);
            btnBrowseReport.Name = "btnBrowseReport";
            btnBrowseReport.Size = new Size(110, 41);
            btnBrowseReport.TabIndex = 1;
            btnBrowseReport.Text = "Browse...";
            btnBrowseReport.UseVisualStyleBackColor = false;
            // 
            // lblLogRet
            // 
            lblLogRet.AutoSize = true;
            lblLogRet.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblLogRet.ForeColor = Color.FromArgb(51, 65, 85);
            lblLogRet.Location = new Point(15, 161);
            lblLogRet.Margin = new Padding(0, 10, 0, 4);
            lblLogRet.Name = "lblLogRet";
            lblLogRet.Size = new Size(254, 32);
            lblLogRet.TabIndex = 5;
            lblLogRet.Text = "Log Retention (Days)";
            // 
            // lblMaxSize
            // 
            lblMaxSize.AutoSize = true;
            lblMaxSize.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMaxSize.ForeColor = Color.FromArgb(51, 65, 85);
            lblMaxSize.Location = new Point(650, 161);
            lblMaxSize.Margin = new Padding(15, 10, 0, 4);
            lblMaxSize.Name = "lblMaxSize";
            lblMaxSize.Size = new Size(227, 32);
            lblMaxSize.TabIndex = 6;
            lblMaxSize.Text = "Max Log Size (MB)";
            // 
            // txtLogRet
            // 
            txtLogRet.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtLogRet.Font = new Font("Segoe UI", 9.5F);
            txtLogRet.Location = new Point(15, 197);
            txtLogRet.Margin = new Padding(0, 0, 15, 14);
            txtLogRet.Name = "txtLogRet";
            txtLogRet.Size = new Size(605, 41);
            txtLogRet.TabIndex = 7;
            txtLogRet.Text = "30";
            // 
            // txtMaxSize
            // 
            txtMaxSize.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtMaxSize.Font = new Font("Segoe UI", 9.5F);
            txtMaxSize.Location = new Point(650, 197);
            txtMaxSize.Margin = new Padding(15, 0, 0, 14);
            txtMaxSize.Name = "txtMaxSize";
            txtMaxSize.Size = new Size(605, 41);
            txtMaxSize.TabIndex = 8;
            txtMaxSize.Text = "100";
            // 
            // chkAutoDelete
            // 
            chkAutoDelete.AutoSize = true;
            chkAutoDelete.Checked = true;
            chkAutoDelete.CheckState = CheckState.Checked;
            tblFileCard.SetColumnSpan(chkAutoDelete, 2);
            chkAutoDelete.Font = new Font("Segoe UI", 9.5F);
            chkAutoDelete.ForeColor = Color.FromArgb(30, 41, 59);
            chkAutoDelete.Location = new Point(15, 262);
            chkAutoDelete.Margin = new Padding(0, 10, 10, 10);
            chkAutoDelete.Name = "chkAutoDelete";
            chkAutoDelete.Size = new Size(474, 40);
            chkAutoDelete.TabIndex = 9;
            chkAutoDelete.Text = "Auto delete logs older than retention";
            // 
            // cardAppSettings
            // 
            cardAppSettings.AutoSize = true;
            cardAppSettings.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            cardAppSettings.BackColor = Color.White;
            cardAppSettings.Controls.Add(tblAppCard);
            cardAppSettings.Dock = DockStyle.Fill;
            cardAppSettings.Location = new Point(0, 1096);
            cardAppSettings.Margin = new Padding(0);
            cardAppSettings.Name = "cardAppSettings";
            cardAppSettings.Size = new Size(1270, 308);
            cardAppSettings.TabIndex = 3;
            // 
            // tblAppCard
            // 
            tblAppCard.AutoSize = true;
            tblAppCard.ColumnCount = 2;
            tblAppCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblAppCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblAppCard.Controls.Add(lblAppTitle, 0, 0);
            tblAppCard.Controls.Add(chkCheckUpdate, 0, 1);
            tblAppCard.Controls.Add(lblRefresh, 1, 1);
            tblAppCard.Controls.Add(chkMinTray, 0, 2);
            tblAppCard.Controls.Add(cmbRefresh, 1, 2);
            tblAppCard.Controls.Add(chkSoundNotif, 0, 3);
            tblAppCard.Controls.Add(chkConfirmDelete, 1, 3);
            tblAppCard.Controls.Add(chkAuditTrail, 1, 4);
            tblAppCard.Dock = DockStyle.Top;
            tblAppCard.Location = new Point(0, 0);
            tblAppCard.Margin = new Padding(8);
            tblAppCard.Name = "tblAppCard";
            tblAppCard.Padding = new Padding(15, 0, 15, 15);
            tblAppCard.RowCount = 5;
            tblAppCard.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tblAppCard.RowStyles.Add(new RowStyle());
            tblAppCard.RowStyles.Add(new RowStyle());
            tblAppCard.RowStyles.Add(new RowStyle());
            tblAppCard.RowStyles.Add(new RowStyle());
            tblAppCard.Size = new Size(1270, 305);
            tblAppCard.TabIndex = 0;
            // 
            // lblAppTitle
            // 
            tblAppCard.SetColumnSpan(lblAppTitle, 2);
            lblAppTitle.Dock = DockStyle.Fill;
            lblAppTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblAppTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblAppTitle.Location = new Point(15, 0);
            lblAppTitle.Margin = new Padding(0, 0, 0, 14);
            lblAppTitle.Name = "lblAppTitle";
            lblAppTitle.Size = new Size(1240, 36);
            lblAppTitle.TabIndex = 0;
            lblAppTitle.Text = "APPLICATION SETTINGS";
            lblAppTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // chkCheckUpdate
            // 
            chkCheckUpdate.AutoSize = true;
            chkCheckUpdate.Checked = true;
            chkCheckUpdate.CheckState = CheckState.Checked;
            chkCheckUpdate.Font = new Font("Segoe UI", 9.5F);
            chkCheckUpdate.ForeColor = Color.FromArgb(30, 41, 59);
            chkCheckUpdate.Location = new Point(15, 60);
            chkCheckUpdate.Margin = new Padding(0, 10, 10, 10);
            chkCheckUpdate.Name = "chkCheckUpdate";
            chkCheckUpdate.Size = new Size(474, 40);
            chkCheckUpdate.TabIndex = 1;
            chkCheckUpdate.Text = "Check for software update on startup";
            // 
            // lblRefresh
            // 
            lblRefresh.AutoSize = true;
            lblRefresh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblRefresh.ForeColor = Color.FromArgb(30, 41, 59);
            lblRefresh.Location = new Point(650, 60);
            lblRefresh.Margin = new Padding(15, 10, 0, 4);
            lblRefresh.Name = "lblRefresh";
            lblRefresh.Size = new Size(297, 32);
            lblRefresh.TabIndex = 2;
            lblRefresh.Text = "Refresh dashboard every";
            // 
            // chkMinTray
            // 
            chkMinTray.AutoSize = true;
            chkMinTray.Font = new Font("Segoe UI", 9.5F);
            chkMinTray.ForeColor = Color.FromArgb(30, 41, 59);
            chkMinTray.Location = new Point(15, 120);
            chkMinTray.Margin = new Padding(0, 10, 10, 10);
            chkMinTray.Name = "chkMinTray";
            chkMinTray.Size = new Size(320, 40);
            chkMinTray.TabIndex = 3;
            chkMinTray.Text = "Minimize to system tray";
            // 
            // cmbRefresh
            // 
            cmbRefresh.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbRefresh.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRefresh.Font = new Font("Segoe UI", 9.5F);
            cmbRefresh.Items.AddRange(new object[] { "10 Seconds", "30 Seconds", "1 Minute", "5 Minutes" });
            cmbRefresh.Location = new Point(650, 111);
            cmbRefresh.Margin = new Padding(15, 0, 0, 14);
            cmbRefresh.Name = "cmbRefresh";
            cmbRefresh.Size = new Size(605, 43);
            cmbRefresh.TabIndex = 4;
            // 
            // chkSoundNotif
            // 
            chkSoundNotif.AutoSize = true;
            chkSoundNotif.Checked = true;
            chkSoundNotif.CheckState = CheckState.Checked;
            chkSoundNotif.Font = new Font("Segoe UI", 9.5F);
            chkSoundNotif.ForeColor = Color.FromArgb(30, 41, 59);
            chkSoundNotif.Location = new Point(15, 180);
            chkSoundNotif.Margin = new Padding(0, 10, 10, 10);
            chkSoundNotif.Name = "chkSoundNotif";
            chkSoundNotif.Size = new Size(348, 40);
            chkSoundNotif.TabIndex = 5;
            chkSoundNotif.Text = "Enable sound notifications";
            // 
            // chkConfirmDelete
            // 
            chkConfirmDelete.AutoSize = true;
            chkConfirmDelete.Checked = true;
            chkConfirmDelete.CheckState = CheckState.Checked;
            chkConfirmDelete.Font = new Font("Segoe UI", 9.5F);
            chkConfirmDelete.ForeColor = Color.FromArgb(30, 41, 59);
            chkConfirmDelete.Location = new Point(650, 180);
            chkConfirmDelete.Margin = new Padding(15, 10, 10, 10);
            chkConfirmDelete.Name = "chkConfirmDelete";
            chkConfirmDelete.Size = new Size(378, 40);
            chkConfirmDelete.TabIndex = 6;
            chkConfirmDelete.Text = "Confirm before deleting data";
            // 
            // chkAuditTrail
            // 
            chkAuditTrail.AutoSize = true;
            chkAuditTrail.Checked = true;
            chkAuditTrail.CheckState = CheckState.Checked;
            chkAuditTrail.Font = new Font("Segoe UI", 9.5F);
            chkAuditTrail.ForeColor = Color.FromArgb(30, 41, 59);
            chkAuditTrail.Location = new Point(650, 240);
            chkAuditTrail.Margin = new Padding(15, 10, 10, 10);
            chkAuditTrail.Name = "chkAuditTrail";
            chkAuditTrail.Size = new Size(238, 40);
            chkAuditTrail.TabIndex = 7;
            chkAuditTrail.Text = "Enable audit trail";
            // 
            // tblRightColumn
            // 
            tblRightColumn.AutoSize = true;
            tblRightColumn.ColumnCount = 1;
            tblRightColumn.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblRightColumn.Controls.Add(cardSystemStatus, 0, 0);
            tblRightColumn.Controls.Add(cardLicenseInfo, 0, 1);
            tblRightColumn.Controls.Add(cardSystemTime, 0, 2);
            tblRightColumn.Dock = DockStyle.Fill;
            tblRightColumn.Location = new Point(1309, 13);
            tblRightColumn.Margin = new Padding(13);
            tblRightColumn.Name = "tblRightColumn";
            tblRightColumn.RowCount = 3;
            tblRightColumn.RowStyles.Add(new RowStyle());
            tblRightColumn.RowStyles.Add(new RowStyle());
            tblRightColumn.RowStyles.Add(new RowStyle());
            tblRightColumn.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tblRightColumn.Size = new Size(672, 1404);
            tblRightColumn.TabIndex = 1;
            // 
            // cardSystemStatus
            // 
            cardSystemStatus.AutoSize = true;
            cardSystemStatus.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            cardSystemStatus.BackColor = Color.White;
            cardSystemStatus.Controls.Add(tblStatusCard);
            cardSystemStatus.Dock = DockStyle.Fill;
            cardSystemStatus.Location = new Point(0, 0);
            cardSystemStatus.Margin = new Padding(0, 0, 0, 30);
            cardSystemStatus.Name = "cardSystemStatus";
            cardSystemStatus.Padding = new Padding(11, 0, 11, 0);
            cardSystemStatus.Size = new Size(672, 444);
            cardSystemStatus.TabIndex = 0;
            // 
            // tblStatusCard
            // 
            tblStatusCard.AutoSize = true;
            tblStatusCard.ColumnCount = 1;
            tblStatusCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblStatusCard.Controls.Add(lblStatusTitle, 0, 0);
            tblStatusCard.Controls.Add(tblStatusGrid, 0, 1);
            tblStatusCard.Dock = DockStyle.Top;
            tblStatusCard.Location = new Point(11, 0);
            tblStatusCard.Margin = new Padding(8);
            tblStatusCard.Name = "tblStatusCard";
            tblStatusCard.RowCount = 2;
            tblStatusCard.RowStyles.Add(new RowStyle(SizeType.Absolute, 82F));
            tblStatusCard.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblStatusCard.Size = new Size(650, 444);
            tblStatusCard.TabIndex = 0;
            // 
            // lblStatusTitle
            // 
            lblStatusTitle.Dock = DockStyle.Fill;
            lblStatusTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblStatusTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblStatusTitle.Location = new Point(8, 0);
            lblStatusTitle.Margin = new Padding(8, 0, 8, 0);
            lblStatusTitle.Name = "lblStatusTitle";
            lblStatusTitle.Size = new Size(634, 82);
            lblStatusTitle.TabIndex = 0;
            lblStatusTitle.Text = "SYSTEM STATUS";
            lblStatusTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tblStatusGrid
            // 
            tblStatusGrid.ColumnCount = 2;
            tblStatusGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblStatusGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblStatusGrid.Controls.Add(lblStKeyApp, 0, 0);
            tblStatusGrid.Controls.Add(lblStValApp, 1, 0);
            tblStatusGrid.Controls.Add(lblStKeyDb, 0, 1);
            tblStatusGrid.Controls.Add(lblStValDb, 1, 1);
            tblStatusGrid.Controls.Add(lblStKeyFile, 0, 2);
            tblStatusGrid.Controls.Add(lblStValFile, 1, 2);
            tblStatusGrid.Controls.Add(lblStKeyLic, 0, 3);
            tblStatusGrid.Controls.Add(lblStValLic, 1, 3);
            tblStatusGrid.Controls.Add(lblStKeyStart, 0, 4);
            tblStatusGrid.Controls.Add(lblStValStart, 1, 4);
            tblStatusGrid.Controls.Add(lblStKeyUptime, 0, 5);
            tblStatusGrid.Controls.Add(lblStValUptime, 1, 5);
            tblStatusGrid.Dock = DockStyle.Fill;
            tblStatusGrid.Location = new Point(0, 82);
            tblStatusGrid.Margin = new Padding(0);
            tblStatusGrid.Name = "tblStatusGrid";
            tblStatusGrid.RowCount = 6;
            tblStatusGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66F));
            tblStatusGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66F));
            tblStatusGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66F));
            tblStatusGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66F));
            tblStatusGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66F));
            tblStatusGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 16.7F));
            tblStatusGrid.Size = new Size(650, 362);
            tblStatusGrid.TabIndex = 1;
            // 
            // lblStKeyApp
            // 
            lblStKeyApp.Dock = DockStyle.Fill;
            lblStKeyApp.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblStKeyApp.ForeColor = Color.FromArgb(30, 41, 59);
            lblStKeyApp.Location = new Point(8, 0);
            lblStKeyApp.Margin = new Padding(8, 0, 8, 0);
            lblStKeyApp.Name = "lblStKeyApp";
            lblStKeyApp.Size = new Size(309, 60);
            lblStKeyApp.TabIndex = 0;
            lblStKeyApp.Text = "Application Status";
            lblStKeyApp.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblStValApp
            // 
            lblStValApp.Dock = DockStyle.Fill;
            lblStValApp.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblStValApp.ForeColor = Color.FromArgb(22, 163, 74);
            lblStValApp.Location = new Point(333, 0);
            lblStValApp.Margin = new Padding(8, 0, 8, 0);
            lblStValApp.Name = "lblStValApp";
            lblStValApp.Size = new Size(309, 60);
            lblStValApp.TabIndex = 1;
            lblStValApp.Text = "Running";
            lblStValApp.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblStKeyDb
            // 
            lblStKeyDb.Dock = DockStyle.Fill;
            lblStKeyDb.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblStKeyDb.ForeColor = Color.FromArgb(30, 41, 59);
            lblStKeyDb.Location = new Point(8, 60);
            lblStKeyDb.Margin = new Padding(8, 0, 8, 0);
            lblStKeyDb.Name = "lblStKeyDb";
            lblStKeyDb.Size = new Size(309, 60);
            lblStKeyDb.TabIndex = 2;
            lblStKeyDb.Text = "Database Connection";
            lblStKeyDb.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblStValDb
            // 
            lblStValDb.Dock = DockStyle.Fill;
            lblStValDb.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblStValDb.ForeColor = Color.FromArgb(22, 163, 74);
            lblStValDb.Location = new Point(333, 60);
            lblStValDb.Margin = new Padding(8, 0, 8, 0);
            lblStValDb.Name = "lblStValDb";
            lblStValDb.Size = new Size(309, 60);
            lblStValDb.TabIndex = 3;
            lblStValDb.Text = "Connected";
            lblStValDb.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblStKeyFile
            // 
            lblStKeyFile.Dock = DockStyle.Fill;
            lblStKeyFile.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblStKeyFile.ForeColor = Color.FromArgb(30, 41, 59);
            lblStKeyFile.Location = new Point(8, 120);
            lblStKeyFile.Margin = new Padding(8, 0, 8, 0);
            lblStKeyFile.Name = "lblStKeyFile";
            lblStKeyFile.Size = new Size(309, 60);
            lblStKeyFile.TabIndex = 4;
            lblStKeyFile.Text = "File Storage";
            lblStKeyFile.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblStValFile
            // 
            lblStValFile.Dock = DockStyle.Fill;
            lblStValFile.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblStValFile.ForeColor = Color.FromArgb(22, 163, 74);
            lblStValFile.Location = new Point(333, 120);
            lblStValFile.Margin = new Padding(8, 0, 8, 0);
            lblStValFile.Name = "lblStValFile";
            lblStValFile.Size = new Size(309, 60);
            lblStValFile.TabIndex = 5;
            lblStValFile.Text = "Healthy";
            lblStValFile.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblStKeyLic
            // 
            lblStKeyLic.Dock = DockStyle.Fill;
            lblStKeyLic.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblStKeyLic.ForeColor = Color.FromArgb(30, 41, 59);
            lblStKeyLic.Location = new Point(8, 180);
            lblStKeyLic.Margin = new Padding(8, 0, 8, 0);
            lblStKeyLic.Name = "lblStKeyLic";
            lblStKeyLic.Size = new Size(309, 60);
            lblStKeyLic.TabIndex = 6;
            lblStKeyLic.Text = "License Status";
            lblStKeyLic.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblStValLic
            // 
            lblStValLic.Dock = DockStyle.Fill;
            lblStValLic.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblStValLic.ForeColor = Color.FromArgb(22, 163, 74);
            lblStValLic.Location = new Point(333, 180);
            lblStValLic.Margin = new Padding(8, 0, 8, 0);
            lblStValLic.Name = "lblStValLic";
            lblStValLic.Size = new Size(309, 60);
            lblStValLic.TabIndex = 7;
            lblStValLic.Text = "Valid";
            lblStValLic.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblStKeyStart
            // 
            lblStKeyStart.Dock = DockStyle.Fill;
            lblStKeyStart.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblStKeyStart.ForeColor = Color.FromArgb(30, 41, 59);
            lblStKeyStart.Location = new Point(8, 240);
            lblStKeyStart.Margin = new Padding(8, 0, 8, 0);
            lblStKeyStart.Name = "lblStKeyStart";
            lblStKeyStart.Size = new Size(309, 60);
            lblStKeyStart.TabIndex = 8;
            lblStKeyStart.Text = "Last System Start";
            lblStKeyStart.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblStValStart
            // 
            lblStValStart.Dock = DockStyle.Fill;
            lblStValStart.Font = new Font("Segoe UI", 9.5F);
            lblStValStart.ForeColor = Color.FromArgb(71, 85, 105);
            lblStValStart.Location = new Point(333, 240);
            lblStValStart.Margin = new Padding(8, 0, 8, 0);
            lblStValStart.Name = "lblStValStart";
            lblStValStart.Size = new Size(309, 60);
            lblStValStart.TabIndex = 9;
            lblStValStart.Text = "15 Mar 2026 08:15:22 AM";
            lblStValStart.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblStKeyUptime
            // 
            lblStKeyUptime.Dock = DockStyle.Fill;
            lblStKeyUptime.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblStKeyUptime.ForeColor = Color.FromArgb(30, 41, 59);
            lblStKeyUptime.Location = new Point(8, 300);
            lblStKeyUptime.Margin = new Padding(8, 0, 8, 0);
            lblStKeyUptime.Name = "lblStKeyUptime";
            lblStKeyUptime.Size = new Size(309, 62);
            lblStKeyUptime.TabIndex = 10;
            lblStKeyUptime.Text = "Uptime";
            lblStKeyUptime.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblStValUptime
            // 
            lblStValUptime.Dock = DockStyle.Fill;
            lblStValUptime.Font = new Font("Segoe UI", 9.5F);
            lblStValUptime.ForeColor = Color.FromArgb(71, 85, 105);
            lblStValUptime.Location = new Point(333, 300);
            lblStValUptime.Margin = new Padding(8, 0, 8, 0);
            lblStValUptime.Name = "lblStValUptime";
            lblStValUptime.Size = new Size(309, 62);
            lblStValUptime.TabIndex = 11;
            lblStValUptime.Text = "02:15:23";
            lblStValUptime.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cardLicenseInfo
            // 
            cardLicenseInfo.AutoSize = true;
            cardLicenseInfo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            cardLicenseInfo.BackColor = Color.White;
            cardLicenseInfo.Controls.Add(tblLicenseCard);
            cardLicenseInfo.Dock = DockStyle.Fill;
            cardLicenseInfo.Location = new Point(0, 474);
            cardLicenseInfo.Margin = new Padding(0, 0, 0, 30);
            cardLicenseInfo.Name = "cardLicenseInfo";
            cardLicenseInfo.Padding = new Padding(11, 0, 11, 0);
            cardLicenseInfo.Size = new Size(672, 419);
            cardLicenseInfo.TabIndex = 1;
            // 
            // tblLicenseCard
            // 
            tblLicenseCard.AutoSize = true;
            tblLicenseCard.ColumnCount = 1;
            tblLicenseCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblLicenseCard.Controls.Add(lblLicenseTitle, 0, 0);
            tblLicenseCard.Controls.Add(tblLicenseGrid, 0, 1);
            tblLicenseCard.Controls.Add(btnManageLicense, 0, 2);
            tblLicenseCard.Dock = DockStyle.Top;
            tblLicenseCard.Location = new Point(11, 0);
            tblLicenseCard.Margin = new Padding(8);
            tblLicenseCard.Name = "tblLicenseCard";
            tblLicenseCard.RowCount = 3;
            tblLicenseCard.RowStyles.Add(new RowStyle(SizeType.Absolute, 82F));
            tblLicenseCard.RowStyles.Add(new RowStyle());
            tblLicenseCard.RowStyles.Add(new RowStyle());
            tblLicenseCard.Size = new Size(650, 419);
            tblLicenseCard.TabIndex = 0;
            // 
            // lblLicenseTitle
            // 
            lblLicenseTitle.Dock = DockStyle.Fill;
            lblLicenseTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblLicenseTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblLicenseTitle.Location = new Point(8, 0);
            lblLicenseTitle.Margin = new Padding(8, 0, 8, 0);
            lblLicenseTitle.Name = "lblLicenseTitle";
            lblLicenseTitle.Size = new Size(634, 82);
            lblLicenseTitle.TabIndex = 0;
            lblLicenseTitle.Text = "LICENSE INFORMATION";
            lblLicenseTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tblLicenseGrid
            // 
            tblLicenseGrid.ColumnCount = 2;
            tblLicenseGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tblLicenseGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tblLicenseGrid.Controls.Add(lblLicKeyType, 0, 0);
            tblLicenseGrid.Controls.Add(lblLicValType, 1, 0);
            tblLicenseGrid.Controls.Add(lblLicKeyKey, 0, 1);
            tblLicenseGrid.Controls.Add(lblLicValKey, 1, 1);
            tblLicenseGrid.Controls.Add(lblLicKeyFrom, 0, 2);
            tblLicenseGrid.Controls.Add(lblLicValFrom, 1, 2);
            tblLicenseGrid.Controls.Add(lblLicKeyTo, 0, 3);
            tblLicenseGrid.Controls.Add(lblLicValTo, 1, 3);
            tblLicenseGrid.Controls.Add(lblLicKeyToOrg, 0, 4);
            tblLicenseGrid.Controls.Add(lblLicValToOrg, 1, 4);
            tblLicenseGrid.Dock = DockStyle.Fill;
            tblLicenseGrid.Location = new Point(8, 90);
            tblLicenseGrid.Margin = new Padding(8);
            tblLicenseGrid.Name = "tblLicenseGrid";
            tblLicenseGrid.RowCount = 5;
            tblLicenseGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tblLicenseGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tblLicenseGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tblLicenseGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tblLicenseGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tblLicenseGrid.Size = new Size(634, 248);
            tblLicenseGrid.TabIndex = 1;
            // 
            // lblLicKeyType
            // 
            lblLicKeyType.Dock = DockStyle.Fill;
            lblLicKeyType.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblLicKeyType.ForeColor = Color.FromArgb(30, 41, 59);
            lblLicKeyType.Location = new Point(8, 0);
            lblLicKeyType.Margin = new Padding(8, 0, 8, 0);
            lblLicKeyType.Name = "lblLicKeyType";
            lblLicKeyType.Size = new Size(269, 49);
            lblLicKeyType.TabIndex = 0;
            lblLicKeyType.Text = "License Type";
            lblLicKeyType.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblLicValType
            // 
            lblLicValType.Dock = DockStyle.Fill;
            lblLicValType.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblLicValType.ForeColor = Color.FromArgb(16, 185, 129);
            lblLicValType.Location = new Point(293, 0);
            lblLicValType.Margin = new Padding(8, 0, 8, 0);
            lblLicValType.Name = "lblLicValType";
            lblLicValType.Size = new Size(333, 49);
            lblLicValType.TabIndex = 1;
            lblLicValType.Text = "Enterprise Professional";
            lblLicValType.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblLicKeyKey
            // 
            lblLicKeyKey.Dock = DockStyle.Fill;
            lblLicKeyKey.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblLicKeyKey.ForeColor = Color.FromArgb(30, 41, 59);
            lblLicKeyKey.Location = new Point(8, 49);
            lblLicKeyKey.Margin = new Padding(8, 0, 8, 0);
            lblLicKeyKey.Name = "lblLicKeyKey";
            lblLicKeyKey.Size = new Size(269, 49);
            lblLicKeyKey.TabIndex = 2;
            lblLicKeyKey.Text = "Machine ID";
            lblLicKeyKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblLicValKey
            // 
            lblLicValKey.Dock = DockStyle.Fill;
            lblLicValKey.Font = new Font("Segoe UI", 9.5F);
            lblLicValKey.ForeColor = Color.FromArgb(71, 85, 105);
            lblLicValKey.Location = new Point(293, 49);
            lblLicValKey.Margin = new Padding(8, 0, 8, 0);
            lblLicValKey.Name = "lblLicValKey";
            lblLicValKey.Size = new Size(333, 49);
            lblLicValKey.TabIndex = 3;
            lblLicValKey.Text = "XXXX-XXXX-XXXX-9821";
            lblLicValKey.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblLicKeyFrom
            // 
            lblLicKeyFrom.Dock = DockStyle.Fill;
            lblLicKeyFrom.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblLicKeyFrom.ForeColor = Color.FromArgb(30, 41, 59);
            lblLicKeyFrom.Location = new Point(8, 98);
            lblLicKeyFrom.Margin = new Padding(8, 0, 8, 0);
            lblLicKeyFrom.Name = "lblLicKeyFrom";
            lblLicKeyFrom.Size = new Size(269, 49);
            lblLicKeyFrom.TabIndex = 4;
            lblLicKeyFrom.Text = "Valid From";
            lblLicKeyFrom.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblLicValFrom
            // 
            lblLicValFrom.Dock = DockStyle.Fill;
            lblLicValFrom.Font = new Font("Segoe UI", 9.5F);
            lblLicValFrom.ForeColor = Color.FromArgb(71, 85, 105);
            lblLicValFrom.Location = new Point(293, 98);
            lblLicValFrom.Margin = new Padding(8, 0, 8, 0);
            lblLicValFrom.Name = "lblLicValFrom";
            lblLicValFrom.Size = new Size(333, 49);
            lblLicValFrom.TabIndex = 5;
            lblLicValFrom.Text = "01 Jan 2026";
            lblLicValFrom.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblLicKeyTo
            // 
            lblLicKeyTo.Dock = DockStyle.Fill;
            lblLicKeyTo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblLicKeyTo.ForeColor = Color.FromArgb(30, 41, 59);
            lblLicKeyTo.Location = new Point(8, 147);
            lblLicKeyTo.Margin = new Padding(8, 0, 8, 0);
            lblLicKeyTo.Name = "lblLicKeyTo";
            lblLicKeyTo.Size = new Size(269, 49);
            lblLicKeyTo.TabIndex = 6;
            lblLicKeyTo.Text = "Valid To";
            lblLicKeyTo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblLicValTo
            // 
            lblLicValTo.Dock = DockStyle.Fill;
            lblLicValTo.Font = new Font("Segoe UI", 9.5F);
            lblLicValTo.ForeColor = Color.FromArgb(71, 85, 105);
            lblLicValTo.Location = new Point(293, 147);
            lblLicValTo.Margin = new Padding(8, 0, 8, 0);
            lblLicValTo.Name = "lblLicValTo";
            lblLicValTo.Size = new Size(333, 49);
            lblLicValTo.TabIndex = 7;
            lblLicValTo.Text = "31 Dec 2026";
            lblLicValTo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblLicKeyToOrg
            // 
            lblLicKeyToOrg.Dock = DockStyle.Fill;
            lblLicKeyToOrg.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblLicKeyToOrg.ForeColor = Color.FromArgb(30, 41, 59);
            lblLicKeyToOrg.Location = new Point(8, 196);
            lblLicKeyToOrg.Margin = new Padding(8, 0, 8, 0);
            lblLicKeyToOrg.Name = "lblLicKeyToOrg";
            lblLicKeyToOrg.Size = new Size(269, 52);
            lblLicKeyToOrg.TabIndex = 8;
            lblLicKeyToOrg.Text = "Licensed To";
            lblLicKeyToOrg.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblLicValToOrg
            // 
            lblLicValToOrg.Dock = DockStyle.Fill;
            lblLicValToOrg.Font = new Font("Segoe UI", 9.5F);
            lblLicValToOrg.ForeColor = Color.FromArgb(71, 85, 105);
            lblLicValToOrg.Location = new Point(293, 196);
            lblLicValToOrg.Margin = new Padding(8, 0, 8, 0);
            lblLicValToOrg.Name = "lblLicValToOrg";
            lblLicValToOrg.Size = new Size(333, 52);
            lblLicValToOrg.TabIndex = 9;
            lblLicValToOrg.Text = "Acme Corporation";
            lblLicValToOrg.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnManageLicense
            // 
            btnManageLicense.Anchor = AnchorStyles.None;
            btnManageLicense.BackColor = Color.FromArgb(255, 193, 7);
            btnManageLicense.FlatAppearance.BorderSize = 0;
            btnManageLicense.FlatStyle = FlatStyle.Flat;
            btnManageLicense.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnManageLicense.ForeColor = Color.Black;
            btnManageLicense.Location = new Point(61, 354);
            btnManageLicense.Margin = new Padding(8);
            btnManageLicense.Name = "btnManageLicense";
            btnManageLicense.Size = new Size(528, 57);
            btnManageLicense.TabIndex = 10;
            btnManageLicense.Text = "Manage License";
            btnManageLicense.UseVisualStyleBackColor = false;
            // 
            // cardSystemTime
            // 
            cardSystemTime.AutoSize = true;
            cardSystemTime.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            cardSystemTime.BackColor = Color.White;
            cardSystemTime.Controls.Add(tblTimeCard);
            cardSystemTime.Dock = DockStyle.Fill;
            cardSystemTime.Location = new Point(0, 923);
            cardSystemTime.Margin = new Padding(0);
            cardSystemTime.Name = "cardSystemTime";
            cardSystemTime.Padding = new Padding(11, 0, 11, 0);
            cardSystemTime.Size = new Size(672, 481);
            cardSystemTime.TabIndex = 2;
            // 
            // tblTimeCard
            // 
            tblTimeCard.AutoSize = true;
            tblTimeCard.ColumnCount = 1;
            tblTimeCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblTimeCard.Controls.Add(lblTimeTitle, 0, 0);
            tblTimeCard.Controls.Add(tblTimeGrid, 0, 1);
            tblTimeCard.Controls.Add(btnSyncTime, 0, 2);
            tblTimeCard.Dock = DockStyle.Top;
            tblTimeCard.Location = new Point(11, 0);
            tblTimeCard.Margin = new Padding(8);
            tblTimeCard.Name = "tblTimeCard";
            tblTimeCard.RowCount = 3;
            tblTimeCard.RowStyles.Add(new RowStyle(SizeType.Absolute, 51F));
            tblTimeCard.RowStyles.Add(new RowStyle());
            tblTimeCard.RowStyles.Add(new RowStyle());
            tblTimeCard.Size = new Size(650, 326);
            tblTimeCard.TabIndex = 0;
            // 
            // lblTimeTitle
            // 
            lblTimeTitle.Dock = DockStyle.Fill;
            lblTimeTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTimeTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblTimeTitle.Location = new Point(8, 0);
            lblTimeTitle.Margin = new Padding(8, 0, 8, 0);
            lblTimeTitle.Name = "lblTimeTitle";
            lblTimeTitle.Size = new Size(634, 51);
            lblTimeTitle.TabIndex = 0;
            lblTimeTitle.Text = "SYSTEM TIME";
            lblTimeTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tblTimeGrid
            // 
            tblTimeGrid.AutoSize = true;
            tblTimeGrid.ColumnCount = 2;
            tblTimeGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38F));
            tblTimeGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 62F));
            tblTimeGrid.Controls.Add(lblTimeKeyDate, 0, 0);
            tblTimeGrid.Controls.Add(lblTimeValDate, 1, 0);
            tblTimeGrid.Controls.Add(lblTimeKeyTime, 0, 1);
            tblTimeGrid.Controls.Add(lblTimeValTime, 1, 1);
            tblTimeGrid.Controls.Add(lblTimeKeyZone, 0, 2);
            tblTimeGrid.Controls.Add(cmbTimeZone, 1, 2);
            tblTimeGrid.Dock = DockStyle.Fill;
            tblTimeGrid.Location = new Point(0, 51);
            tblTimeGrid.Margin = new Padding(0);
            tblTimeGrid.Name = "tblTimeGrid";
            tblTimeGrid.RowCount = 3;
            tblTimeGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            tblTimeGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            tblTimeGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            tblTimeGrid.Size = new Size(650, 180);
            tblTimeGrid.TabIndex = 1;
            // 
            // lblTimeKeyDate
            // 
            lblTimeKeyDate.Dock = DockStyle.Fill;
            lblTimeKeyDate.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblTimeKeyDate.ForeColor = Color.FromArgb(30, 41, 59);
            lblTimeKeyDate.Location = new Point(8, 0);
            lblTimeKeyDate.Margin = new Padding(8, 0, 8, 0);
            lblTimeKeyDate.Name = "lblTimeKeyDate";
            lblTimeKeyDate.Size = new Size(231, 60);
            lblTimeKeyDate.TabIndex = 0;
            lblTimeKeyDate.Text = "Current Date";
            lblTimeKeyDate.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTimeValDate
            // 
            lblTimeValDate.Dock = DockStyle.Fill;
            lblTimeValDate.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblTimeValDate.ForeColor = Color.FromArgb(30, 41, 59);
            lblTimeValDate.Location = new Point(255, 0);
            lblTimeValDate.Margin = new Padding(8, 0, 8, 0);
            lblTimeValDate.Name = "lblTimeValDate";
            lblTimeValDate.Size = new Size(387, 60);
            lblTimeValDate.TabIndex = 1;
            lblTimeValDate.Text = "15 Mar 2026";
            lblTimeValDate.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTimeKeyTime
            // 
            lblTimeKeyTime.Dock = DockStyle.Fill;
            lblTimeKeyTime.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblTimeKeyTime.ForeColor = Color.FromArgb(30, 41, 59);
            lblTimeKeyTime.Location = new Point(8, 60);
            lblTimeKeyTime.Margin = new Padding(8, 0, 8, 0);
            lblTimeKeyTime.Name = "lblTimeKeyTime";
            lblTimeKeyTime.Size = new Size(231, 60);
            lblTimeKeyTime.TabIndex = 2;
            lblTimeKeyTime.Text = "Current Time";
            lblTimeKeyTime.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTimeValTime
            // 
            lblTimeValTime.Dock = DockStyle.Fill;
            lblTimeValTime.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblTimeValTime.ForeColor = Color.FromArgb(37, 99, 235);
            lblTimeValTime.Location = new Point(255, 60);
            lblTimeValTime.Margin = new Padding(8, 0, 8, 0);
            lblTimeValTime.Name = "lblTimeValTime";
            lblTimeValTime.Size = new Size(387, 60);
            lblTimeValTime.TabIndex = 3;
            lblTimeValTime.Text = "10:30:45 AM";
            lblTimeValTime.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTimeKeyZone
            // 
            lblTimeKeyZone.Dock = DockStyle.Fill;
            lblTimeKeyZone.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblTimeKeyZone.ForeColor = Color.FromArgb(30, 41, 59);
            lblTimeKeyZone.Location = new Point(8, 120);
            lblTimeKeyZone.Margin = new Padding(8, 0, 8, 0);
            lblTimeKeyZone.Name = "lblTimeKeyZone";
            lblTimeKeyZone.Size = new Size(231, 60);
            lblTimeKeyZone.TabIndex = 4;
            lblTimeKeyZone.Text = "Time Zone";
            lblTimeKeyZone.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbTimeZone
            // 
            cmbTimeZone.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbTimeZone.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTimeZone.Font = new Font("Segoe UI", 9.5F);
            cmbTimeZone.Items.AddRange(new object[] { "(UTC+07:00) Bangkok, Hanoi, Jakarta", "(UTC+00:00) Dublin, Edinburgh, Lisbon, London", "(UTC-05:00) Eastern Time (US & Canada)" });
            cmbTimeZone.Location = new Point(255, 128);
            cmbTimeZone.Margin = new Padding(8);
            cmbTimeZone.Name = "cmbTimeZone";
            cmbTimeZone.Size = new Size(387, 43);
            cmbTimeZone.TabIndex = 5;
            // 
            // btnSyncTime
            // 
            btnSyncTime.Anchor = AnchorStyles.None;
            btnSyncTime.BackColor = Color.FromArgb(255, 193, 7);
            tblTimeCard.SetColumnSpan(btnSyncTime, 2);
            btnSyncTime.FlatAppearance.BorderSize = 0;
            btnSyncTime.FlatStyle = FlatStyle.Flat;
            btnSyncTime.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnSyncTime.ForeColor = Color.Black;
            btnSyncTime.Location = new Point(66, 246);
            btnSyncTime.Margin = new Padding(8, 15, 8, 15);
            btnSyncTime.Name = "btnSyncTime";
            btnSyncTime.Size = new Size(518, 65);
            btnSyncTime.TabIndex = 6;
            btnSyncTime.Text = "Sync with System Time";
            btnSyncTime.UseVisualStyleBackColor = false;
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(244, 245, 247);
            Controls.Add(tblMain);
            Margin = new Padding(8);
            Name = "Settings";
            Size = new Size(2046, 1461);
            tblMain.ResumeLayout(false);
            flpTabs.ResumeLayout(false);
            flpTabs.PerformLayout();
            tblContent.ResumeLayout(false);
            tblContent.PerformLayout();
            tblLeftColumn.ResumeLayout(false);
            tblLeftColumn.PerformLayout();
            cardGeneral.ResumeLayout(false);
            cardGeneral.PerformLayout();
            tblGenCard.ResumeLayout(false);
            tblGenCard.PerformLayout();
            cardTestSettings.ResumeLayout(false);
            cardTestSettings.PerformLayout();
            tblTestCard.ResumeLayout(false);
            tblTestCard.PerformLayout();
            cardFileSettings.ResumeLayout(false);
            cardFileSettings.PerformLayout();
            tblFileCard.ResumeLayout(false);
            tblFileCard.PerformLayout();
            tblDataBrowse.ResumeLayout(false);
            tblDataBrowse.PerformLayout();
            tblReportBrowse.ResumeLayout(false);
            tblReportBrowse.PerformLayout();
            cardAppSettings.ResumeLayout(false);
            cardAppSettings.PerformLayout();
            tblAppCard.ResumeLayout(false);
            tblAppCard.PerformLayout();
            tblRightColumn.ResumeLayout(false);
            tblRightColumn.PerformLayout();
            cardSystemStatus.ResumeLayout(false);
            cardSystemStatus.PerformLayout();
            tblStatusCard.ResumeLayout(false);
            tblStatusGrid.ResumeLayout(false);
            cardLicenseInfo.ResumeLayout(false);
            cardLicenseInfo.PerformLayout();
            tblLicenseCard.ResumeLayout(false);
            tblLicenseGrid.ResumeLayout(false);
            cardSystemTime.ResumeLayout(false);
            cardSystemTime.PerformLayout();
            tblTimeCard.ResumeLayout(false);
            tblTimeCard.PerformLayout();
            tblTimeGrid.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblMain;

        private System.Windows.Forms.Label lblHeader;

        private System.Windows.Forms.FlowLayoutPanel flpTabs;

        private System.Windows.Forms.Label lblTabGen;

        private System.Windows.Forms.Label lblTabTest;

        private System.Windows.Forms.Label lblTabHw;

        private System.Windows.Forms.Label lblTabUsers;

        private System.Windows.Forms.Label lblTabNotif;

        private System.Windows.Forms.Label lblTabBackup;

        private System.Windows.Forms.Label lblTabSysInfo;

        private System.Windows.Forms.TableLayoutPanel tblContent;

        private System.Windows.Forms.TableLayoutPanel tblLeftColumn;

        private System.Windows.Forms.TableLayoutPanel tblRightColumn;

        private System.Windows.Forms.Panel cardGeneral;

        private System.Windows.Forms.TableLayoutPanel tblGenCard;

        private System.Windows.Forms.Label lblGenTitle;
        private System.Windows.Forms.Label lblCompName;
        private System.Windows.Forms.TextBox txtCompanyName;
        private System.Windows.Forms.Label lblSysName;
        private System.Windows.Forms.TextBox txtSystemName;

        private System.Windows.Forms.Label lblLang;
        private System.Windows.Forms.ComboBox cmbLang;
        private System.Windows.Forms.Label lblDateFormat;
        private System.Windows.Forms.ComboBox cmbDateFormat;
        private System.Windows.Forms.Label lblTimeFormat;
        private System.Windows.Forms.ComboBox cmbTimeFormat;

        private System.Windows.Forms.Label lblAutoLogout;
        private System.Windows.Forms.ComboBox cmbAutoLogout;

        private System.Windows.Forms.CheckBox chkConfirmExit;

        private System.Windows.Forms.CheckBox chkDarkMode;

        private System.Windows.Forms.Panel cardTestSettings;

        private System.Windows.Forms.TableLayoutPanel tblTestCard;

        private System.Windows.Forms.Label lblTestTitle;

        private System.Windows.Forms.Label lblTestType;
        private System.Windows.Forms.ComboBox cmbTestType;
        private System.Windows.Forms.Label lblDefArea;
        private System.Windows.Forms.ComboBox cmbDefaultArea;
        private System.Windows.Forms.Label lblDefOp;
        private System.Windows.Forms.ComboBox cmbDefaultOp;

        private System.Windows.Forms.Label lblResLow;
        private System.Windows.Forms.TextBox txtResLow;
        private System.Windows.Forms.Label lblResHigh;
        private System.Windows.Forms.TextBox txtResHigh;
        private System.Windows.Forms.Label lblResTol;
        private System.Windows.Forms.TextBox txtResTol;

        private System.Windows.Forms.CheckBox chkAutoStart;

        private System.Windows.Forms.CheckBox chkBuzzer;

        private System.Windows.Forms.CheckBox chkPauseFail;

        private System.Windows.Forms.Panel cardFileSettings;

        private System.Windows.Forms.TableLayoutPanel tblFileCard;

        private System.Windows.Forms.Label lblFileTitle;

        private System.Windows.Forms.Label lblDataStorage;
        private System.Windows.Forms.TableLayoutPanel tblDataBrowse;
        private System.Windows.Forms.TextBox txtDataPath;
        private System.Windows.Forms.Button btnBrowseData;
        private System.Windows.Forms.Label lblReportStorage;
        private System.Windows.Forms.TableLayoutPanel tblReportBrowse;
        private System.Windows.Forms.TextBox txtReportPath;
        private System.Windows.Forms.Button btnBrowseReport;

        private System.Windows.Forms.Label lblLogRet;
        private System.Windows.Forms.TextBox txtLogRet;
        private System.Windows.Forms.Label lblMaxSize;
        private System.Windows.Forms.TextBox txtMaxSize;

        private System.Windows.Forms.CheckBox chkAutoDelete;

        private System.Windows.Forms.Panel cardAppSettings;

        private System.Windows.Forms.TableLayoutPanel tblAppCard;

        private System.Windows.Forms.Label lblAppTitle;

        private System.Windows.Forms.CheckBox chkCheckUpdate;

        private System.Windows.Forms.CheckBox chkMinTray;

        private System.Windows.Forms.CheckBox chkSoundNotif;

        private System.Windows.Forms.Label lblRefresh;
        private System.Windows.Forms.ComboBox cmbRefresh;

        private System.Windows.Forms.CheckBox chkConfirmDelete;

        private System.Windows.Forms.CheckBox chkAuditTrail;

        private System.Windows.Forms.Panel cardSystemStatus;

        private System.Windows.Forms.TableLayoutPanel tblStatusCard;

        private System.Windows.Forms.Label lblStatusTitle;

        private System.Windows.Forms.TableLayoutPanel tblStatusGrid;

        private System.Windows.Forms.Label lblStKeyApp;

        private System.Windows.Forms.Label lblStValApp;

        private System.Windows.Forms.Label lblStKeyDb;

        private System.Windows.Forms.Label lblStValDb;

        private System.Windows.Forms.Label lblStKeyFile;

        private System.Windows.Forms.Label lblStValFile;

        private System.Windows.Forms.Label lblStKeyLic;

        private System.Windows.Forms.Label lblStValLic;

        private System.Windows.Forms.Label lblStKeyStart;

        private System.Windows.Forms.Label lblStValStart;

        private System.Windows.Forms.Label lblStKeyUptime;

        private System.Windows.Forms.Label lblStValUptime;

        private System.Windows.Forms.Panel cardLicenseInfo;

        private System.Windows.Forms.TableLayoutPanel tblLicenseCard;

        private System.Windows.Forms.Label lblLicenseTitle;

        private System.Windows.Forms.TableLayoutPanel tblLicenseGrid;

        private System.Windows.Forms.Label lblLicKeyType;

        private System.Windows.Forms.Label lblLicValType;

        private System.Windows.Forms.Label lblLicKeyKey;

        private System.Windows.Forms.Label lblLicValKey;

        private System.Windows.Forms.Label lblLicKeyFrom;

        private System.Windows.Forms.Label lblLicValFrom;

        private System.Windows.Forms.Label lblLicKeyTo;

        private System.Windows.Forms.Label lblLicValTo;

        private System.Windows.Forms.Label lblLicKeyToOrg;

        private System.Windows.Forms.Label lblLicValToOrg;

        private System.Windows.Forms.Button btnManageLicense;

        private System.Windows.Forms.Panel cardSystemTime;

        private System.Windows.Forms.TableLayoutPanel tblTimeCard;

        private System.Windows.Forms.Label lblTimeTitle;

        private System.Windows.Forms.TableLayoutPanel tblTimeGrid;

        private System.Windows.Forms.Label lblTimeKeyDate;

        private System.Windows.Forms.Label lblTimeValDate;

        private System.Windows.Forms.Label lblTimeKeyTime;

        private System.Windows.Forms.Label lblTimeValTime;

        private System.Windows.Forms.Label lblTimeKeyZone;
        private System.Windows.Forms.ComboBox cmbTimeZone;

        private System.Windows.Forms.Button btnSyncTime;

    }

}