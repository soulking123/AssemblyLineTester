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
            settingSystemInfo = new SettingSystemInfo();
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
            lblTotalTestPoint = new Label();
            lblDefTestType = new Label();
            lblDefResistorVal = new Label();
            lblDefMaxWireRes = new Label();
            txtTotalTestPoint = new TextBox();
            cmbDefTestType = new ComboBox();
            txtDefResistorVal = new TextBox();
            txtDefMaxWireRes = new TextBox();
            lblDefCapacitorTol = new Label();
            lblDefResistorTol = new Label();
            lblDefWireInsRes = new Label();
            lblDefCapacitorVal = new Label();
            txtDefCapacitorTol = new TextBox();
            txtDefResistorTol = new TextBox();
            txtDefWireInsRes = new TextBox();
            txtDefCapacitorVal = new TextBox();
            lblTestTypeAvail = new Label();
            lblLearnConn = new Label();
            lblTestResultCol = new Label();
            flpTestTypeAvail = new FlowLayoutPanel();
            flpLearnConn = new FlowLayoutPanel();
            chkLearnWire = new CheckBox();
            chkLearnCapacitor = new CheckBox();
            chkLearnTwistedPair = new CheckBox();
            chkLearnResistance = new CheckBox();
            chkLearnDiode = new CheckBox();
            lblDataStorage = new Label();
            lblReportStorage = new Label();
            tblDataBrowse = new TableLayoutPanel();
            txtDataPath = new TextBox();
            btnBrowseData = new AssemblyLineTester.Views.Controls.RoundedButton();
            tblReportBrowse = new TableLayoutPanel();
            txtReportPath = new TextBox();
            btnBrowseReport = new AssemblyLineTester.Views.Controls.RoundedButton();
            flpTestResultCol = new FlowLayoutPanel();
            chkResNetId = new CheckBox();
            chkResHarnessGrp = new CheckBox();
            chkResFrom = new CheckBox();
            chkResNetFromLbl = new CheckBox();
            chkResTo = new CheckBox();
            chkResNetToLbl = new CheckBox();
            chkResTestType = new CheckBox();
            chkResLowerLimit = new CheckBox();
            chkResUpperLimit = new CheckBox();
            chkResExpectedVal = new CheckBox();
            chkResTolerance = new CheckBox();
            chkResMeasurement = new CheckBox();
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
            tblMain.SuspendLayout();
            flpTabs.SuspendLayout();
            tblContent.SuspendLayout();
            tblLeftColumn.SuspendLayout();
            cardGeneral.SuspendLayout();
            tblGenCard.SuspendLayout();
            cardTestSettings.SuspendLayout();
            tblTestCard.SuspendLayout();
            flpLearnConn.SuspendLayout();
            tblDataBrowse.SuspendLayout();
            tblReportBrowse.SuspendLayout();
            flpTestResultCol.SuspendLayout();
            cardAppSettings.SuspendLayout();
            tblAppCard.SuspendLayout();
            SuspendLayout();
            // 
            // settingSystemInfo
            // 
            settingSystemInfo.Dock = DockStyle.Fill;
            settingSystemInfo.Location = new Point(1283, 13);
            settingSystemInfo.Margin = new Padding(13);
            settingSystemInfo.Name = "settingSystemInfo";
            settingSystemInfo.Size = new Size(698, 1255);
            settingSystemInfo.TabIndex = 1;
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
            flpTabs.Controls.Add(lblTabUsers);
            flpTabs.Controls.Add(lblTabBackup);
            flpTabs.Controls.Add(lblTabSysInfo);
            flpTabs.Dock = DockStyle.Fill;
            flpTabs.Location = new Point(39, 90);
            flpTabs.Margin = new Padding(39, 0, 39, 0);
            flpTabs.Name = "flpTabs";
            flpTabs.Size = new Size(1968, 90);
            flpTabs.TabIndex = 1;
            // 
            // lblTabGen
            // 
            lblTabGen.AutoSize = true;
            lblTabGen.Cursor = Cursors.Hand;
            lblTabGen.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblTabGen.ForeColor = Color.FromArgb(30, 41, 59);
            lblTabGen.Location = new Point(0, 21);
            lblTabGen.Margin = new Padding(0, 21, 67, 0);
            lblTabGen.Name = "lblTabGen";
            lblTabGen.Size = new Size(118, 38);
            lblTabGen.TabIndex = 0;
            lblTabGen.Text = "General";
            // 
            // lblTabTest
            // 
            lblTabTest.AutoSize = true;
            lblTabTest.Cursor = Cursors.Hand;
            lblTabTest.Font = new Font("Segoe UI", 10.5F);
            lblTabTest.ForeColor = Color.FromArgb(100, 116, 139);
            lblTabTest.Location = new Point(185, 21);
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
            lblTabHw.Location = new Point(463, 21);
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
            lblTabUsers.Location = new Point(843, 21);
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
            lblTabNotif.Location = new Point(1077, 21);
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
            lblTabBackup.Location = new Point(1317, 21);
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
            lblTabSysInfo.Location = new Point(1599, 21);
            lblTabSysInfo.Margin = new Padding(0, 21, 67, 0);
            lblTabSysInfo.Name = "lblTabSysInfo";
            lblTabSysInfo.Size = new Size(257, 38);
            lblTabSysInfo.TabIndex = 6;
            lblTabSysInfo.Text = "System Information";
            // 
            // tblContent
            // 
            tblContent.AutoScroll = true;
            tblContent.ColumnCount = 1;
            tblContent.ColumnStyles.Add(new ColumnStyle());
            tblContent.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tblContent.Controls.Add(tblLeftColumn, 0, 0);
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
            tblLeftColumn.Controls.Add(cardAppSettings, 0, 3);
            tblLeftColumn.Dock = DockStyle.Fill;
            tblLeftColumn.Location = new Point(13, 13);
            tblLeftColumn.Margin = new Padding(13);
            tblLeftColumn.Name = "tblLeftColumn";
            tblLeftColumn.RowCount = 4;
            tblLeftColumn.RowStyles.Add(new RowStyle());
            tblLeftColumn.RowStyles.Add(new RowStyle());
            tblLeftColumn.RowStyles.Add(new RowStyle());
            tblLeftColumn.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tblLeftColumn.Size = new Size(1968, 1255);
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
            cardGeneral.Size = new Size(1968, 372);
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
            tblGenCard.Size = new Size(1968, 372);
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
            lblGenTitle.Size = new Size(1938, 36);
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
            lblSysName.Location = new Point(996, 60);
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
            txtCompanyName.Size = new Size(951, 41);
            txtCompanyName.TabIndex = 3;
            txtCompanyName.Text = "Caterpillar Inc.";
            // 
            // txtSystemName
            // 
            txtSystemName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tblGenCard.SetColumnSpan(txtSystemName, 3);
            txtSystemName.Font = new Font("Segoe UI", 9.5F);
            txtSystemName.Location = new Point(996, 96);
            txtSystemName.Margin = new Padding(15, 0, 0, 14);
            txtSystemName.Name = "txtSystemName";
            txtSystemName.Size = new Size(957, 41);
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
            lblDateFormat.Location = new Point(669, 161);
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
            lblTimeFormat.Location = new Point(1313, 161);
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
            cmbLang.Size = new Size(634, 43);
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
            cmbDateFormat.Location = new Point(669, 197);
            cmbDateFormat.Margin = new Padding(10, 0, 10, 14);
            cmbDateFormat.Name = "cmbDateFormat";
            cmbDateFormat.Size = new Size(624, 43);
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
            cmbTimeFormat.Location = new Point(1313, 197);
            cmbTimeFormat.Margin = new Padding(10, 0, 0, 14);
            cmbTimeFormat.Name = "cmbTimeFormat";
            cmbTimeFormat.Size = new Size(640, 43);
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
            chkConfirmExit.Location = new Point(669, 274);
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
            chkDarkMode.Location = new Point(1313, 274);
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
            cmbAutoLogout.Size = new Size(634, 43);
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
            cardTestSettings.Size = new Size(1968, 730);
            cardTestSettings.TabIndex = 1;
            // 
            // tblTestCard
            // 
            tblTestCard.AutoSize = true;
            tblTestCard.ColumnCount = 4;
            tblTestCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblTestCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblTestCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblTestCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblTestCard.Controls.Add(lblTestTitle, 0, 0);
            tblTestCard.Controls.Add(lblTotalTestPoint, 0, 1);
            tblTestCard.Controls.Add(lblDefTestType, 1, 1);
            tblTestCard.Controls.Add(lblDefResistorVal, 2, 1);
            tblTestCard.Controls.Add(lblDefMaxWireRes, 3, 1);
            tblTestCard.Controls.Add(txtTotalTestPoint, 0, 2);
            tblTestCard.Controls.Add(cmbDefTestType, 1, 2);
            tblTestCard.Controls.Add(txtDefResistorVal, 2, 2);
            tblTestCard.Controls.Add(txtDefMaxWireRes, 3, 2);
            tblTestCard.Controls.Add(lblDefCapacitorTol, 0, 3);
            tblTestCard.Controls.Add(lblDefResistorTol, 1, 3);
            tblTestCard.Controls.Add(lblDefWireInsRes, 2, 3);
            tblTestCard.Controls.Add(lblDefCapacitorVal, 3, 3);
            tblTestCard.Controls.Add(txtDefCapacitorTol, 0, 4);
            tblTestCard.Controls.Add(txtDefResistorTol, 1, 4);
            tblTestCard.Controls.Add(txtDefWireInsRes, 2, 4);
            tblTestCard.Controls.Add(txtDefCapacitorVal, 3, 4);
            tblTestCard.Controls.Add(lblTestTypeAvail, 0, 5);
            tblTestCard.Controls.Add(lblLearnConn, 1, 5);
            tblTestCard.Controls.Add(lblTestResultCol, 2, 5);
            tblTestCard.Controls.Add(flpTestTypeAvail, 0, 6);
            tblTestCard.Controls.Add(flpLearnConn, 1, 6);
            tblTestCard.Controls.Add(lblDataStorage, 0, 7);
            tblTestCard.Controls.Add(lblReportStorage, 2, 7);
            tblTestCard.Controls.Add(tblDataBrowse, 0, 8);
            tblTestCard.Controls.Add(tblReportBrowse, 2, 8);
            tblTestCard.Controls.Add(flpTestResultCol, 2, 6);
            tblTestCard.Dock = DockStyle.Top;
            tblTestCard.Location = new Point(0, 0);
            tblTestCard.Margin = new Padding(8);
            tblTestCard.Name = "tblTestCard";
            tblTestCard.Padding = new Padding(15, 0, 15, 15);
            tblTestCard.RowCount = 9;
            tblTestCard.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tblTestCard.RowStyles.Add(new RowStyle());
            tblTestCard.RowStyles.Add(new RowStyle());
            tblTestCard.RowStyles.Add(new RowStyle());
            tblTestCard.RowStyles.Add(new RowStyle());
            tblTestCard.RowStyles.Add(new RowStyle());
            tblTestCard.RowStyles.Add(new RowStyle());
            tblTestCard.RowStyles.Add(new RowStyle());
            tblTestCard.RowStyles.Add(new RowStyle());
            tblTestCard.Size = new Size(1968, 730);
            tblTestCard.TabIndex = 0;
            // 
            // lblTestTitle
            // 
            lblTestTitle.Dock = DockStyle.Fill;
            lblTestTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTestTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTestTitle.Location = new Point(15, 0);
            lblTestTitle.Margin = new Padding(0, 0, 0, 14);
            lblTestTitle.Name = "lblTestTitle";
            lblTestTitle.Size = new Size(484, 36);
            lblTestTitle.TabIndex = 0;
            lblTestTitle.Text = "DEFAULT TEST SETTINGS";
            lblTestTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTotalTestPoint
            // 
            lblTotalTestPoint.AutoSize = true;
            lblTotalTestPoint.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotalTestPoint.ForeColor = Color.FromArgb(51, 65, 85);
            lblTotalTestPoint.Location = new Point(15, 60);
            lblTotalTestPoint.Margin = new Padding(0, 10, 0, 4);
            lblTotalTestPoint.Name = "lblTotalTestPoint";
            lblTotalTestPoint.Size = new Size(300, 32);
            lblTotalTestPoint.TabIndex = 1;
            lblTotalTestPoint.Text = "Total Test Point Available";
            // 
            // lblDefTestType
            // 
            lblDefTestType.AutoSize = true;
            lblDefTestType.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDefTestType.ForeColor = Color.FromArgb(51, 65, 85);
            lblDefTestType.Location = new Point(499, 60);
            lblDefTestType.Margin = new Padding(0, 10, 0, 4);
            lblDefTestType.Name = "lblDefTestType";
            lblDefTestType.Size = new Size(211, 32);
            lblDefTestType.TabIndex = 2;
            lblDefTestType.Text = "Default Test Type";
            // 
            // lblDefResistorVal
            // 
            lblDefResistorVal.AutoSize = true;
            lblDefResistorVal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDefResistorVal.ForeColor = Color.FromArgb(51, 65, 85);
            lblDefResistorVal.Location = new Point(983, 60);
            lblDefResistorVal.Margin = new Padding(0, 10, 0, 4);
            lblDefResistorVal.Name = "lblDefResistorVal";
            lblDefResistorVal.Size = new Size(308, 32);
            lblDefResistorVal.TabIndex = 3;
            lblDefResistorVal.Text = "Default Resistor Value (Ω)";
            // 
            // lblDefMaxWireRes
            // 
            lblDefMaxWireRes.AutoSize = true;
            lblDefMaxWireRes.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDefMaxWireRes.ForeColor = Color.FromArgb(51, 65, 85);
            lblDefMaxWireRes.Location = new Point(1467, 60);
            lblDefMaxWireRes.Margin = new Padding(0, 10, 0, 4);
            lblDefMaxWireRes.Name = "lblDefMaxWireRes";
            lblDefMaxWireRes.Size = new Size(384, 32);
            lblDefMaxWireRes.TabIndex = 4;
            lblDefMaxWireRes.Text = "Default Max Wire Resistance (Ω)";
            // 
            // txtTotalTestPoint
            // 
            txtTotalTestPoint.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtTotalTestPoint.Font = new Font("Segoe UI", 9.5F);
            txtTotalTestPoint.Location = new Point(15, 97);
            txtTotalTestPoint.Margin = new Padding(0, 0, 15, 14);
            txtTotalTestPoint.Name = "txtTotalTestPoint";
            txtTotalTestPoint.Size = new Size(469, 41);
            txtTotalTestPoint.TabIndex = 5;
            // 
            // cmbDefTestType
            // 
            cmbDefTestType.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbDefTestType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDefTestType.FlatStyle = FlatStyle.Flat;
            cmbDefTestType.Font = new Font("Segoe UI", 9.5F);
            cmbDefTestType.Location = new Point(499, 96);
            cmbDefTestType.Margin = new Padding(0, 0, 15, 14);
            cmbDefTestType.Name = "cmbDefTestType";
            cmbDefTestType.Size = new Size(469, 43);
            cmbDefTestType.TabIndex = 6;
            // 
            // txtDefResistorVal
            // 
            txtDefResistorVal.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtDefResistorVal.Font = new Font("Segoe UI", 9.5F);
            txtDefResistorVal.Location = new Point(983, 97);
            txtDefResistorVal.Margin = new Padding(0, 0, 15, 14);
            txtDefResistorVal.Name = "txtDefResistorVal";
            txtDefResistorVal.Size = new Size(469, 41);
            txtDefResistorVal.TabIndex = 7;
            // 
            // txtDefMaxWireRes
            // 
            txtDefMaxWireRes.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtDefMaxWireRes.Font = new Font("Segoe UI", 9.5F);
            txtDefMaxWireRes.Location = new Point(1467, 97);
            txtDefMaxWireRes.Margin = new Padding(0, 0, 15, 14);
            txtDefMaxWireRes.Name = "txtDefMaxWireRes";
            txtDefMaxWireRes.Size = new Size(471, 41);
            txtDefMaxWireRes.TabIndex = 8;
            // 
            // lblDefCapacitorTol
            // 
            lblDefCapacitorTol.AutoSize = true;
            lblDefCapacitorTol.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDefCapacitorTol.ForeColor = Color.FromArgb(51, 65, 85);
            lblDefCapacitorTol.Location = new Point(15, 163);
            lblDefCapacitorTol.Margin = new Padding(0, 10, 0, 4);
            lblDefCapacitorTol.Name = "lblDefCapacitorTol";
            lblDefCapacitorTol.Size = new Size(377, 32);
            lblDefCapacitorTol.TabIndex = 17;
            lblDefCapacitorTol.Text = "Default Capacitor Tolerance (%)";
            // 
            // lblDefResistorTol
            // 
            lblDefResistorTol.AutoSize = true;
            lblDefResistorTol.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDefResistorTol.ForeColor = Color.FromArgb(51, 65, 85);
            lblDefResistorTol.Location = new Point(499, 163);
            lblDefResistorTol.Margin = new Padding(0, 10, 0, 4);
            lblDefResistorTol.Name = "lblDefResistorTol";
            lblDefResistorTol.Size = new Size(359, 32);
            lblDefResistorTol.TabIndex = 10;
            lblDefResistorTol.Text = "Default Resistor Tolerance (%)";
            // 
            // lblDefWireInsRes
            // 
            lblDefWireInsRes.AutoSize = true;
            lblDefWireInsRes.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDefWireInsRes.ForeColor = Color.FromArgb(51, 65, 85);
            lblDefWireInsRes.Location = new Point(983, 163);
            lblDefWireInsRes.Margin = new Padding(0, 10, 0, 4);
            lblDefWireInsRes.Name = "lblDefWireInsRes";
            lblDefWireInsRes.Size = new Size(400, 32);
            lblDefWireInsRes.TabIndex = 11;
            lblDefWireInsRes.Text = "Default Wire Insulation Res. (MΩ)";
            // 
            // lblDefCapacitorVal
            // 
            lblDefCapacitorVal.AutoSize = true;
            lblDefCapacitorVal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDefCapacitorVal.ForeColor = Color.FromArgb(51, 65, 85);
            lblDefCapacitorVal.Location = new Point(1467, 163);
            lblDefCapacitorVal.Margin = new Padding(0, 10, 0, 4);
            lblDefCapacitorVal.Name = "lblDefCapacitorVal";
            lblDefCapacitorVal.Size = new Size(335, 32);
            lblDefCapacitorVal.TabIndex = 12;
            lblDefCapacitorVal.Text = "Default Capacitor Value (µF)";
            // 
            // txtDefCapacitorTol
            // 
            txtDefCapacitorTol.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtDefCapacitorTol.Font = new Font("Segoe UI", 9.5F);
            txtDefCapacitorTol.Location = new Point(15, 199);
            txtDefCapacitorTol.Margin = new Padding(0, 0, 15, 14);
            txtDefCapacitorTol.Name = "txtDefCapacitorTol";
            txtDefCapacitorTol.Size = new Size(469, 41);
            txtDefCapacitorTol.TabIndex = 20;
            // 
            // txtDefResistorTol
            // 
            txtDefResistorTol.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtDefResistorTol.Font = new Font("Segoe UI", 9.5F);
            txtDefResistorTol.Location = new Point(499, 199);
            txtDefResistorTol.Margin = new Padding(0, 0, 15, 14);
            txtDefResistorTol.Name = "txtDefResistorTol";
            txtDefResistorTol.Size = new Size(469, 41);
            txtDefResistorTol.TabIndex = 14;
            // 
            // txtDefWireInsRes
            // 
            txtDefWireInsRes.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtDefWireInsRes.Font = new Font("Segoe UI", 9.5F);
            txtDefWireInsRes.Location = new Point(983, 199);
            txtDefWireInsRes.Margin = new Padding(0, 0, 15, 14);
            txtDefWireInsRes.Name = "txtDefWireInsRes";
            txtDefWireInsRes.Size = new Size(469, 41);
            txtDefWireInsRes.TabIndex = 15;
            // 
            // txtDefCapacitorVal
            // 
            txtDefCapacitorVal.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtDefCapacitorVal.Font = new Font("Segoe UI", 9.5F);
            txtDefCapacitorVal.Location = new Point(1467, 199);
            txtDefCapacitorVal.Margin = new Padding(0, 0, 15, 14);
            txtDefCapacitorVal.Name = "txtDefCapacitorVal";
            txtDefCapacitorVal.Size = new Size(471, 41);
            txtDefCapacitorVal.TabIndex = 16;
            // 
            // lblTestTypeAvail
            // 
            lblTestTypeAvail.AutoSize = true;
            lblTestTypeAvail.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTestTypeAvail.ForeColor = Color.FromArgb(51, 65, 85);
            lblTestTypeAvail.Location = new Point(15, 264);
            lblTestTypeAvail.Margin = new Padding(0, 10, 0, 4);
            lblTestTypeAvail.Name = "lblTestTypeAvail";
            lblTestTypeAvail.Size = new Size(231, 32);
            lblTestTypeAvail.TabIndex = 9;
            lblTestTypeAvail.Text = "Test Type Available";
            // 
            // lblLearnConn
            // 
            lblLearnConn.AutoSize = true;
            lblLearnConn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblLearnConn.ForeColor = Color.FromArgb(51, 65, 85);
            lblLearnConn.Location = new Point(499, 264);
            lblLearnConn.Margin = new Padding(0, 10, 0, 4);
            lblLearnConn.Name = "lblLearnConn";
            lblLearnConn.Size = new Size(215, 32);
            lblLearnConn.TabIndex = 18;
            lblLearnConn.Text = "Learn Connection";
            // 
            // lblTestResultCol
            // 
            lblTestResultCol.AutoSize = true;
            tblTestCard.SetColumnSpan(lblTestResultCol, 2);
            lblTestResultCol.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTestResultCol.ForeColor = Color.FromArgb(51, 65, 85);
            lblTestResultCol.Location = new Point(983, 264);
            lblTestResultCol.Margin = new Padding(0, 10, 0, 4);
            lblTestResultCol.Name = "lblTestResultCol";
            lblTestResultCol.Size = new Size(232, 32);
            lblTestResultCol.TabIndex = 19;
            lblTestResultCol.Text = "Test Result Column";
            // 
            // flpTestTypeAvail
            // 
            flpTestTypeAvail.AutoSize = true;
            flpTestTypeAvail.FlowDirection = FlowDirection.TopDown;
            flpTestTypeAvail.Location = new Point(15, 300);
            flpTestTypeAvail.Margin = new Padding(0, 0, 15, 14);
            flpTestTypeAvail.Name = "flpTestTypeAvail";
            flpTestTypeAvail.Size = new Size(0, 0);
            flpTestTypeAvail.TabIndex = 13;
            // 
            // flpLearnConn
            // 
            flpLearnConn.AutoSize = true;
            flpLearnConn.Controls.Add(chkLearnWire);
            flpLearnConn.Controls.Add(chkLearnCapacitor);
            flpLearnConn.Controls.Add(chkLearnTwistedPair);
            flpLearnConn.Controls.Add(chkLearnResistance);
            flpLearnConn.Controls.Add(chkLearnDiode);
            flpLearnConn.FlowDirection = FlowDirection.TopDown;
            flpLearnConn.Location = new Point(499, 300);
            flpLearnConn.Margin = new Padding(0, 0, 15, 14);
            flpLearnConn.Name = "flpLearnConn";
            flpLearnConn.Size = new Size(183, 250);
            flpLearnConn.TabIndex = 21;
            // 
            // chkLearnWire
            // 
            chkLearnWire.AutoSize = true;
            chkLearnWire.Font = new Font("Segoe UI", 9.5F);
            chkLearnWire.ForeColor = Color.FromArgb(30, 41, 59);
            chkLearnWire.Location = new Point(0, 5);
            chkLearnWire.Margin = new Padding(0, 5, 0, 5);
            chkLearnWire.Name = "chkLearnWire";
            chkLearnWire.Size = new Size(100, 40);
            chkLearnWire.TabIndex = 0;
            chkLearnWire.Text = "Wire";
            chkLearnWire.UseVisualStyleBackColor = true;
            // 
            // chkLearnCapacitor
            // 
            chkLearnCapacitor.AutoSize = true;
            chkLearnCapacitor.Font = new Font("Segoe UI", 9.5F);
            chkLearnCapacitor.ForeColor = Color.FromArgb(30, 41, 59);
            chkLearnCapacitor.Location = new Point(0, 55);
            chkLearnCapacitor.Margin = new Padding(0, 5, 0, 5);
            chkLearnCapacitor.Name = "chkLearnCapacitor";
            chkLearnCapacitor.Size = new Size(155, 40);
            chkLearnCapacitor.TabIndex = 1;
            chkLearnCapacitor.Text = "Capacitor";
            chkLearnCapacitor.UseVisualStyleBackColor = true;
            // 
            // chkLearnTwistedPair
            // 
            chkLearnTwistedPair.AutoSize = true;
            chkLearnTwistedPair.Font = new Font("Segoe UI", 9.5F);
            chkLearnTwistedPair.ForeColor = Color.FromArgb(30, 41, 59);
            chkLearnTwistedPair.Location = new Point(0, 105);
            chkLearnTwistedPair.Margin = new Padding(0, 5, 0, 5);
            chkLearnTwistedPair.Name = "chkLearnTwistedPair";
            chkLearnTwistedPair.Size = new Size(183, 40);
            chkLearnTwistedPair.TabIndex = 2;
            chkLearnTwistedPair.Text = "Twisted Pair";
            chkLearnTwistedPair.UseVisualStyleBackColor = true;
            // 
            // chkLearnResistance
            // 
            chkLearnResistance.AutoSize = true;
            chkLearnResistance.Font = new Font("Segoe UI", 9.5F);
            chkLearnResistance.ForeColor = Color.FromArgb(30, 41, 59);
            chkLearnResistance.Location = new Point(0, 155);
            chkLearnResistance.Margin = new Padding(0, 5, 0, 5);
            chkLearnResistance.Name = "chkLearnResistance";
            chkLearnResistance.Size = new Size(167, 40);
            chkLearnResistance.TabIndex = 3;
            chkLearnResistance.Text = "Resistance";
            chkLearnResistance.UseVisualStyleBackColor = true;
            // 
            // chkLearnDiode
            // 
            chkLearnDiode.AutoSize = true;
            chkLearnDiode.Font = new Font("Segoe UI", 9.5F);
            chkLearnDiode.ForeColor = Color.FromArgb(30, 41, 59);
            chkLearnDiode.Location = new Point(0, 205);
            chkLearnDiode.Margin = new Padding(0, 5, 0, 5);
            chkLearnDiode.Name = "chkLearnDiode";
            chkLearnDiode.Size = new Size(115, 40);
            chkLearnDiode.TabIndex = 4;
            chkLearnDiode.Text = "Diode";
            chkLearnDiode.UseVisualStyleBackColor = true;
            // 
            // lblDataStorage
            // 
            lblDataStorage.AutoSize = true;
            tblTestCard.SetColumnSpan(lblDataStorage, 2);
            lblDataStorage.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDataStorage.ForeColor = Color.FromArgb(51, 65, 85);
            lblDataStorage.Location = new Point(15, 624);
            lblDataStorage.Margin = new Padding(0, 10, 0, 4);
            lblDataStorage.Name = "lblDataStorage";
            lblDataStorage.Size = new Size(218, 32);
            lblDataStorage.TabIndex = 1;
            lblDataStorage.Text = "Data Storage Path";
            // 
            // lblReportStorage
            // 
            lblReportStorage.AutoSize = true;
            tblTestCard.SetColumnSpan(lblReportStorage, 2);
            lblReportStorage.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblReportStorage.ForeColor = Color.FromArgb(51, 65, 85);
            lblReportStorage.Location = new Point(998, 624);
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
            tblTestCard.SetColumnSpan(tblDataBrowse, 2);
            tblDataBrowse.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblDataBrowse.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            tblDataBrowse.Controls.Add(txtDataPath, 0, 0);
            tblDataBrowse.Controls.Add(btnBrowseData, 1, 0);
            tblDataBrowse.Dock = DockStyle.Fill;
            tblDataBrowse.Location = new Point(15, 660);
            tblDataBrowse.Margin = new Padding(0, 0, 15, 14);
            tblDataBrowse.Name = "tblDataBrowse";
            tblDataBrowse.RowCount = 1;
            tblDataBrowse.RowStyles.Add(new RowStyle());
            tblDataBrowse.Size = new Size(953, 41);
            tblDataBrowse.TabIndex = 3;
            // 
            // txtDataPath
            // 
            txtDataPath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtDataPath.Font = new Font("Segoe UI", 9.5F);
            txtDataPath.Location = new Point(0, 0);
            txtDataPath.Margin = new Padding(0, 0, 10, 0);
            txtDataPath.Name = "txtDataPath";
            txtDataPath.ReadOnly = true;
            txtDataPath.Size = new Size(833, 41);
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
            btnBrowseData.HoverForeColor = Color.Empty;
            btnBrowseData.Location = new Point(843, 0);
            btnBrowseData.Margin = new Padding(0);
            btnBrowseData.Name = "btnBrowseData";
            btnBrowseData.PressedForeColor = Color.Empty;
            btnBrowseData.Size = new Size(110, 41);
            btnBrowseData.TabIndex = 1;
            btnBrowseData.Text = "Browse";
            btnBrowseData.UseVisualStyleBackColor = false;
            // 
            // tblReportBrowse
            // 
            tblReportBrowse.AutoSize = true;
            tblReportBrowse.ColumnCount = 2;
            tblTestCard.SetColumnSpan(tblReportBrowse, 2);
            tblReportBrowse.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblReportBrowse.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            tblReportBrowse.Controls.Add(txtReportPath, 0, 0);
            tblReportBrowse.Controls.Add(btnBrowseReport, 1, 0);
            tblReportBrowse.Dock = DockStyle.Fill;
            tblReportBrowse.Location = new Point(998, 660);
            tblReportBrowse.Margin = new Padding(15, 0, 0, 14);
            tblReportBrowse.Name = "tblReportBrowse";
            tblReportBrowse.RowCount = 1;
            tblReportBrowse.RowStyles.Add(new RowStyle());
            tblReportBrowse.Size = new Size(955, 41);
            tblReportBrowse.TabIndex = 4;
            // 
            // txtReportPath
            // 
            txtReportPath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtReportPath.Font = new Font("Segoe UI", 9.5F);
            txtReportPath.Location = new Point(0, 0);
            txtReportPath.Margin = new Padding(0, 0, 10, 0);
            txtReportPath.Name = "txtReportPath";
            txtReportPath.ReadOnly = true;
            txtReportPath.Size = new Size(835, 41);
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
            btnBrowseReport.HoverForeColor = Color.Empty;
            btnBrowseReport.Location = new Point(845, 0);
            btnBrowseReport.Margin = new Padding(0);
            btnBrowseReport.Name = "btnBrowseReport";
            btnBrowseReport.PressedForeColor = Color.Empty;
            btnBrowseReport.Size = new Size(110, 41);
            btnBrowseReport.TabIndex = 1;
            btnBrowseReport.Text = "Browse";
            btnBrowseReport.UseVisualStyleBackColor = false;
            // 
            // flpTestResultCol
            // 
            flpTestResultCol.AutoSize = true;
            tblTestCard.SetColumnSpan(flpTestResultCol, 2);
            flpTestResultCol.Controls.Add(chkResNetId);
            flpTestResultCol.Controls.Add(chkResHarnessGrp);
            flpTestResultCol.Controls.Add(chkResFrom);
            flpTestResultCol.Controls.Add(chkResNetFromLbl);
            flpTestResultCol.Controls.Add(chkResTo);
            flpTestResultCol.Controls.Add(chkResNetToLbl);
            flpTestResultCol.Controls.Add(chkResTestType);
            flpTestResultCol.Controls.Add(chkResLowerLimit);
            flpTestResultCol.Controls.Add(chkResUpperLimit);
            flpTestResultCol.Controls.Add(chkResExpectedVal);
            flpTestResultCol.Controls.Add(chkResTolerance);
            flpTestResultCol.Controls.Add(chkResMeasurement);
            flpTestResultCol.FlowDirection = FlowDirection.TopDown;
            flpTestResultCol.Location = new Point(983, 300);
            flpTestResultCol.Margin = new Padding(0, 0, 15, 14);
            flpTestResultCol.MaximumSize = new Size(800, 300);
            flpTestResultCol.Name = "flpTestResultCol";
            flpTestResultCol.Size = new Size(442, 300);
            flpTestResultCol.TabIndex = 22;
            // 
            // chkResNetId
            // 
            chkResNetId.AutoSize = true;
            chkResNetId.Font = new Font("Segoe UI", 9.5F);
            chkResNetId.ForeColor = Color.FromArgb(30, 41, 59);
            chkResNetId.Location = new Point(0, 5);
            chkResNetId.Margin = new Padding(0, 5, 0, 5);
            chkResNetId.Name = "chkResNetId";
            chkResNetId.Size = new Size(124, 40);
            chkResNetId.TabIndex = 0;
            chkResNetId.Text = "Net-ID";
            chkResNetId.UseVisualStyleBackColor = true;
            // 
            // chkResHarnessGrp
            // 
            chkResHarnessGrp.AutoSize = true;
            chkResHarnessGrp.Font = new Font("Segoe UI", 9.5F);
            chkResHarnessGrp.ForeColor = Color.FromArgb(30, 41, 59);
            chkResHarnessGrp.Location = new Point(0, 55);
            chkResHarnessGrp.Margin = new Padding(0, 5, 0, 5);
            chkResHarnessGrp.Name = "chkResHarnessGrp";
            chkResHarnessGrp.Size = new Size(217, 40);
            chkResHarnessGrp.TabIndex = 1;
            chkResHarnessGrp.Text = "Harness Group";
            chkResHarnessGrp.UseVisualStyleBackColor = true;
            // 
            // chkResFrom
            // 
            chkResFrom.AutoSize = true;
            chkResFrom.Font = new Font("Segoe UI", 9.5F);
            chkResFrom.ForeColor = Color.FromArgb(30, 41, 59);
            chkResFrom.Location = new Point(0, 105);
            chkResFrom.Margin = new Padding(0, 5, 0, 5);
            chkResFrom.Name = "chkResFrom";
            chkResFrom.Size = new Size(106, 40);
            chkResFrom.TabIndex = 2;
            chkResFrom.Text = "From";
            chkResFrom.UseVisualStyleBackColor = true;
            // 
            // chkResNetFromLbl
            // 
            chkResNetFromLbl.AutoSize = true;
            chkResNetFromLbl.Font = new Font("Segoe UI", 9.5F);
            chkResNetFromLbl.ForeColor = Color.FromArgb(30, 41, 59);
            chkResNetFromLbl.Location = new Point(0, 155);
            chkResNetFromLbl.Margin = new Padding(0, 5, 0, 5);
            chkResNetFromLbl.Name = "chkResNetFromLbl";
            chkResNetFromLbl.Size = new Size(222, 40);
            chkResNetFromLbl.TabIndex = 3;
            chkResNetFromLbl.Text = "Net From Label";
            chkResNetFromLbl.UseVisualStyleBackColor = true;
            // 
            // chkResTo
            // 
            chkResTo.AutoSize = true;
            chkResTo.Font = new Font("Segoe UI", 9.5F);
            chkResTo.ForeColor = Color.FromArgb(30, 41, 59);
            chkResTo.Location = new Point(0, 205);
            chkResTo.Margin = new Padding(0, 5, 0, 5);
            chkResTo.Name = "chkResTo";
            chkResTo.Size = new Size(73, 40);
            chkResTo.TabIndex = 4;
            chkResTo.Text = "To";
            chkResTo.UseVisualStyleBackColor = true;
            // 
            // chkResNetToLbl
            // 
            chkResNetToLbl.AutoSize = true;
            chkResNetToLbl.Font = new Font("Segoe UI", 9.5F);
            chkResNetToLbl.ForeColor = Color.FromArgb(30, 41, 59);
            chkResNetToLbl.Location = new Point(0, 255);
            chkResNetToLbl.Margin = new Padding(0, 5, 0, 5);
            chkResNetToLbl.Name = "chkResNetToLbl";
            chkResNetToLbl.Size = new Size(189, 40);
            chkResNetToLbl.TabIndex = 5;
            chkResNetToLbl.Text = "Net To Label";
            chkResNetToLbl.UseVisualStyleBackColor = true;
            // 
            // chkResTestType
            // 
            chkResTestType.AutoSize = true;
            chkResTestType.Font = new Font("Segoe UI", 9.5F);
            chkResTestType.ForeColor = Color.FromArgb(30, 41, 59);
            chkResTestType.Location = new Point(222, 5);
            chkResTestType.Margin = new Padding(0, 5, 0, 5);
            chkResTestType.Name = "chkResTestType";
            chkResTestType.Size = new Size(154, 40);
            chkResTestType.TabIndex = 6;
            chkResTestType.Text = "Test Type";
            chkResTestType.UseVisualStyleBackColor = true;
            // 
            // chkResLowerLimit
            // 
            chkResLowerLimit.AutoSize = true;
            chkResLowerLimit.Font = new Font("Segoe UI", 9.5F);
            chkResLowerLimit.ForeColor = Color.FromArgb(30, 41, 59);
            chkResLowerLimit.Location = new Point(222, 55);
            chkResLowerLimit.Margin = new Padding(0, 5, 0, 5);
            chkResLowerLimit.Name = "chkResLowerLimit";
            chkResLowerLimit.Size = new Size(178, 40);
            chkResLowerLimit.TabIndex = 7;
            chkResLowerLimit.Text = "Lower Limit";
            chkResLowerLimit.UseVisualStyleBackColor = true;
            // 
            // chkResUpperLimit
            // 
            chkResUpperLimit.AutoSize = true;
            chkResUpperLimit.Font = new Font("Segoe UI", 9.5F);
            chkResUpperLimit.ForeColor = Color.FromArgb(30, 41, 59);
            chkResUpperLimit.Location = new Point(222, 105);
            chkResUpperLimit.Margin = new Padding(0, 5, 0, 5);
            chkResUpperLimit.Name = "chkResUpperLimit";
            chkResUpperLimit.Size = new Size(180, 40);
            chkResUpperLimit.TabIndex = 8;
            chkResUpperLimit.Text = "Upper Limit";
            chkResUpperLimit.UseVisualStyleBackColor = true;
            // 
            // chkResExpectedVal
            // 
            chkResExpectedVal.AutoSize = true;
            chkResExpectedVal.Font = new Font("Segoe UI", 9.5F);
            chkResExpectedVal.ForeColor = Color.FromArgb(30, 41, 59);
            chkResExpectedVal.Location = new Point(222, 155);
            chkResExpectedVal.Margin = new Padding(0, 5, 0, 5);
            chkResExpectedVal.Name = "chkResExpectedVal";
            chkResExpectedVal.Size = new Size(220, 40);
            chkResExpectedVal.TabIndex = 9;
            chkResExpectedVal.Text = "Expected Value";
            chkResExpectedVal.UseVisualStyleBackColor = true;
            // 
            // chkResTolerance
            // 
            chkResTolerance.AutoSize = true;
            chkResTolerance.Font = new Font("Segoe UI", 9.5F);
            chkResTolerance.ForeColor = Color.FromArgb(30, 41, 59);
            chkResTolerance.Location = new Point(222, 205);
            chkResTolerance.Margin = new Padding(0, 5, 0, 5);
            chkResTolerance.Name = "chkResTolerance";
            chkResTolerance.Size = new Size(156, 40);
            chkResTolerance.TabIndex = 10;
            chkResTolerance.Text = "Tolerance";
            chkResTolerance.UseVisualStyleBackColor = true;
            // 
            // chkResMeasurement
            // 
            chkResMeasurement.AutoSize = true;
            chkResMeasurement.Font = new Font("Segoe UI", 9.5F);
            chkResMeasurement.ForeColor = Color.FromArgb(30, 41, 59);
            chkResMeasurement.Location = new Point(222, 255);
            chkResMeasurement.Margin = new Padding(0, 5, 0, 5);
            chkResMeasurement.Name = "chkResMeasurement";
            chkResMeasurement.Size = new Size(206, 40);
            chkResMeasurement.TabIndex = 11;
            chkResMeasurement.Text = "Measurement";
            chkResMeasurement.UseVisualStyleBackColor = true;
            // 
            // cardAppSettings
            // 
            cardAppSettings.AutoSize = true;
            cardAppSettings.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            cardAppSettings.BackColor = Color.White;
            cardAppSettings.Controls.Add(tblAppCard);
            cardAppSettings.Dock = DockStyle.Fill;
            cardAppSettings.Location = new Point(0, 1140);
            cardAppSettings.Margin = new Padding(0);
            cardAppSettings.Name = "cardAppSettings";
            cardAppSettings.Size = new Size(1968, 115);
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
            tblAppCard.Size = new Size(1968, 305);
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
            lblAppTitle.Size = new Size(1938, 36);
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
            lblRefresh.Location = new Point(999, 60);
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
            cmbRefresh.Location = new Point(999, 111);
            cmbRefresh.Margin = new Padding(15, 0, 0, 14);
            cmbRefresh.Name = "cmbRefresh";
            cmbRefresh.Size = new Size(954, 43);
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
            chkConfirmDelete.Location = new Point(999, 180);
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
            chkAuditTrail.Location = new Point(999, 240);
            chkAuditTrail.Margin = new Padding(15, 10, 10, 10);
            chkAuditTrail.Name = "chkAuditTrail";
            chkAuditTrail.Size = new Size(238, 40);
            chkAuditTrail.TabIndex = 7;
            chkAuditTrail.Text = "Enable audit trail";
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
            flpLearnConn.ResumeLayout(false);
            flpLearnConn.PerformLayout();
            tblDataBrowse.ResumeLayout(false);
            tblDataBrowse.PerformLayout();
            tblReportBrowse.ResumeLayout(false);
            tblReportBrowse.PerformLayout();
            flpTestResultCol.ResumeLayout(false);
            flpTestResultCol.PerformLayout();
            cardAppSettings.ResumeLayout(false);
            cardAppSettings.PerformLayout();
            tblAppCard.ResumeLayout(false);
            tblAppCard.PerformLayout();
            ResumeLayout(false);

        }

        private SettingSystemInfo settingSystemInfo;
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


        private System.Windows.Forms.Label lblDataStorage;
        private System.Windows.Forms.TableLayoutPanel tblDataBrowse;
        private System.Windows.Forms.TextBox txtDataPath;
        private Controls.RoundedButton btnBrowseData;
        private System.Windows.Forms.Label lblReportStorage;
        private System.Windows.Forms.TableLayoutPanel tblReportBrowse;
        private System.Windows.Forms.TextBox txtReportPath;
        private Controls.RoundedButton btnBrowseReport;


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


    
        private System.Windows.Forms.Label lblTotalTestPoint;
        private System.Windows.Forms.Label lblDefTestType;
        private System.Windows.Forms.Label lblDefResistorVal;
        private System.Windows.Forms.Label lblDefMaxWireRes;
        private System.Windows.Forms.Label lblTestTypeAvail;
        private System.Windows.Forms.Label lblDefResistorTol;
        private System.Windows.Forms.Label lblDefWireInsRes;
        private System.Windows.Forms.Label lblDefCapacitorVal;
        private System.Windows.Forms.Label lblDefCapacitorTol;
        private System.Windows.Forms.Label lblLearnConn;
        private System.Windows.Forms.Label lblTestResultCol;

        private System.Windows.Forms.TextBox txtTotalTestPoint;
        private System.Windows.Forms.ComboBox cmbDefTestType;
        private System.Windows.Forms.TextBox txtDefResistorVal;
        private System.Windows.Forms.TextBox txtDefMaxWireRes;
        private System.Windows.Forms.TextBox txtDefResistorTol;
        private System.Windows.Forms.TextBox txtDefWireInsRes;
        private System.Windows.Forms.TextBox txtDefCapacitorVal;
        private System.Windows.Forms.TextBox txtDefCapacitorTol;

        private System.Windows.Forms.FlowLayoutPanel flpTestTypeAvail;
        private System.Windows.Forms.FlowLayoutPanel flpLearnConn;
        private System.Windows.Forms.FlowLayoutPanel flpTestResultCol;

        private System.Windows.Forms.CheckBox chkLearnWire;
        private System.Windows.Forms.CheckBox chkLearnCapacitor;
        private System.Windows.Forms.CheckBox chkLearnTwistedPair;
        private System.Windows.Forms.CheckBox chkLearnResistance;
        private System.Windows.Forms.CheckBox chkLearnDiode;

        private System.Windows.Forms.CheckBox chkResNetId;
        private System.Windows.Forms.CheckBox chkResHarnessGrp;
        private System.Windows.Forms.CheckBox chkResFrom;
        private System.Windows.Forms.CheckBox chkResNetFromLbl;
        private System.Windows.Forms.CheckBox chkResTo;
        private System.Windows.Forms.CheckBox chkResNetToLbl;
        private System.Windows.Forms.CheckBox chkResTestType;
        private System.Windows.Forms.CheckBox chkResLowerLimit;
        private System.Windows.Forms.CheckBox chkResUpperLimit;
        private System.Windows.Forms.CheckBox chkResExpectedVal;
        private System.Windows.Forms.CheckBox chkResTolerance;
        private System.Windows.Forms.CheckBox chkResMeasurement;
}

}