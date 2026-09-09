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
            lblTabUsers = new Label();
            lblTabBackup = new Label();
            lblTabSysInfo = new Label();
            tblContent = new Panel();
            tblLeftColumn = new TableLayoutPanel();
            tblTopRowContainer = new TableLayoutPanel();
            cardGeneral = new Panel();
            tblGenCard = new TableLayoutPanel();
            lblGenTitle = new Label();
            lblCompanyName = new Label();
            lblSysName = new Label();
            txtCompanyName = new TextBox();
            txtSysName = new TextBox();
            lblDataStorage = new Label();
            lblReportStorage = new Label();
            tblDataBrowse = new TableLayoutPanel();
            txtDataPath = new TextBox();
            btnBrowseData = new AssemblyLineTester.Views.Controls.RoundedButton();
            tblReportBrowse = new TableLayoutPanel();
            txtReportPath = new TextBox();
            btnBrowseReport = new AssemblyLineTester.Views.Controls.RoundedButton();
            cardMaintenanceState = new Panel();
            tblMaintCard = new TableLayoutPanel();
            lblMaintTitle = new Label();
            flpMaintHeaderActions = new FlowLayoutPanel();
            btnEditMaintenance = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnResetMaintenance = new AssemblyLineTester.Views.Controls.RoundedButton();
            lblRemainingDays = new Label();
            lblRemainingHours = new Label();
            txtRemainingDays = new TextBox();
            txtRemainingHours = new MaskedTextBox();
            pnlMaintInfo = new Panel();
            tblMaintInfo = new TableLayoutPanel();
            lblOperatingTimeTodayTitle = new Label();
            lblOperatingTimeTodayVal = new Label();
            lblLastUsageTitle = new Label();
            lblLastUsageVal = new Label();
            flpMaintActions = new FlowLayoutPanel();
            btnUpdateMaintenance = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnCancelMaintenance = new AssemblyLineTester.Views.Controls.RoundedButton();
            lblEditHint = new Label();
            cardTestSettings = new Panel();
            tblTestCard = new TableLayoutPanel();
            lblTestTitle = new Label();
            lblTotalTestPoint = new Label();
            txtTotalTestPoint = new TextBox();
            lblDefTestType = new Label();
            cmbDefTestType = new ComboBox();
            lblDefResistorVal = new Label();
            txtDefResistorVal = new TextBox();
            lblDefResistorTol = new Label();
            txtDefResistorTol = new TextBox();
            lblDefCapacitorVal = new Label();
            txtDefCapacitorVal = new TextBox();
            lblDefCapacitorTol = new Label();
            txtDefCapacitorTol = new TextBox();
            lblDefWireInsRes = new Label();
            txtDefWireInsRes = new TextBox();
            lblDefMaxWireRes = new Label();
            txtDefMaxWireRes = new TextBox();
            lblTestTypeAvail = new Label();
            lblLearnConn = new Label();
            lblTestResultCol = new Label();
            flpTestTypeAvail = new FlowLayoutPanel();
            flpLearnConn = new FlowLayoutPanel();
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
            lblTabTest = new Label();
            lblTabHw = new Label();
            lblTabNotif = new Label();
            tblMain.SuspendLayout();
            flpTabs.SuspendLayout();
            tblContent.SuspendLayout();
            tblLeftColumn.SuspendLayout();
            tblTopRowContainer.SuspendLayout();
            cardGeneral.SuspendLayout();
            tblGenCard.SuspendLayout();
            tblDataBrowse.SuspendLayout();
            tblReportBrowse.SuspendLayout();
            cardMaintenanceState.SuspendLayout();
            tblMaintCard.SuspendLayout();
            flpMaintHeaderActions.SuspendLayout();
            pnlMaintInfo.SuspendLayout();
            tblMaintInfo.SuspendLayout();
            flpMaintActions.SuspendLayout();
            cardTestSettings.SuspendLayout();
            tblTestCard.SuspendLayout();
            flpTestResultCol.SuspendLayout();
            cardAppSettings.SuspendLayout();
            tblAppCard.SuspendLayout();
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
            tblMain.Margin = new Padding(4, 4, 4, 4);
            tblMain.Name = "tblMain";
            tblMain.RowCount = 3;
            tblMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            tblMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            tblMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblMain.Size = new Size(1102, 685);
            tblMain.TabIndex = 0;
            // 
            // lblHeader
            // 
            lblHeader.Dock = DockStyle.Top;
            lblHeader.Font = new Font("Segoe UI", 13.5F, FontStyle.Bold);
            lblHeader.ForeColor = Color.FromArgb(30, 41, 59);
            lblHeader.Location = new Point(21, 10);
            lblHeader.Margin = new Padding(21, 10, 21, 6);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(1060, 26);
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
            flpTabs.Location = new Point(21, 42);
            flpTabs.Margin = new Padding(21, 0, 21, 0);
            flpTabs.Name = "flpTabs";
            flpTabs.Size = new Size(1060, 42);
            flpTabs.TabIndex = 1;
            // 
            // lblTabGen
            // 
            lblTabGen.AutoSize = true;
            lblTabGen.Cursor = Cursors.Hand;
            lblTabGen.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblTabGen.ForeColor = Color.FromArgb(30, 41, 59);
            lblTabGen.Location = new Point(0, 10);
            lblTabGen.Margin = new Padding(0, 10, 36, 0);
            lblTabGen.Name = "lblTabGen";
            lblTabGen.Size = new Size(61, 19);
            lblTabGen.TabIndex = 0;
            lblTabGen.Text = "General";
            // 
            // lblTabUsers
            // 
            lblTabUsers.AutoSize = true;
            lblTabUsers.Cursor = Cursors.Hand;
            lblTabUsers.Font = new Font("Segoe UI", 10.5F);
            lblTabUsers.ForeColor = Color.FromArgb(100, 116, 139);
            lblTabUsers.Location = new Point(97, 10);
            lblTabUsers.Margin = new Padding(0, 10, 36, 0);
            lblTabUsers.Name = "lblTabUsers";
            lblTabUsers.Size = new Size(83, 19);
            lblTabUsers.TabIndex = 3;
            lblTabUsers.Text = "Users & Roles";
            // 
            // lblTabBackup
            // 
            lblTabBackup.AutoSize = true;
            lblTabBackup.Cursor = Cursors.Hand;
            lblTabBackup.Font = new Font("Segoe UI", 10.5F);
            lblTabBackup.ForeColor = Color.FromArgb(100, 116, 139);
            lblTabBackup.Location = new Point(216, 10);
            lblTabBackup.Margin = new Padding(0, 10, 36, 0);
            lblTabBackup.Name = "lblTabBackup";
            lblTabBackup.Size = new Size(107, 19);
            lblTabBackup.TabIndex = 5;
            lblTabBackup.Text = "Backup & Restore";
            // 
            // lblTabSysInfo
            // 
            lblTabSysInfo.AutoSize = true;
            lblTabSysInfo.Cursor = Cursors.Hand;
            lblTabSysInfo.Font = new Font("Segoe UI", 10.5F);
            lblTabSysInfo.ForeColor = Color.FromArgb(100, 116, 139);
            lblTabSysInfo.Location = new Point(359, 10);
            lblTabSysInfo.Margin = new Padding(0, 10, 36, 0);
            lblTabSysInfo.Name = "lblTabSysInfo";
            lblTabSysInfo.Size = new Size(129, 19);
            lblTabSysInfo.TabIndex = 6;
            lblTabSysInfo.Text = "System Information";
            // 
            // tblContent
            // 
            tblContent.AutoScroll = true;
            tblContent.Controls.Add(tblLeftColumn);
            tblContent.Dock = DockStyle.Fill;
            tblContent.Location = new Point(14, 84);
            tblContent.Margin = new Padding(14, 0, 14, 0);
            tblContent.Name = "tblContent";
            tblContent.Size = new Size(1074, 601);
            tblContent.TabIndex = 2;
            // 
            // tblLeftColumn
            // 
            tblLeftColumn.AutoSize = true;
            tblLeftColumn.ColumnCount = 1;
            tblLeftColumn.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblLeftColumn.Controls.Add(tblTopRowContainer, 0, 0);
            tblLeftColumn.Controls.Add(cardTestSettings, 0, 1);
            tblLeftColumn.Controls.Add(cardAppSettings, 0, 3);
            tblLeftColumn.Dock = DockStyle.Top;
            tblLeftColumn.Location = new Point(0, 0);
            tblLeftColumn.Margin = new Padding(7, 6, 7, 6);
            tblLeftColumn.Name = "tblLeftColumn";
            tblLeftColumn.RowCount = 4;
            tblLeftColumn.RowStyles.Add(new RowStyle());
            tblLeftColumn.RowStyles.Add(new RowStyle());
            tblLeftColumn.RowStyles.Add(new RowStyle());
            tblLeftColumn.RowStyles.Add(new RowStyle());
            tblLeftColumn.Size = new Size(1057, 635);
            tblLeftColumn.TabIndex = 0;
            // 
            // tblTopRowContainer
            // 
            tblTopRowContainer.AutoSize = true;
            tblTopRowContainer.ColumnCount = 2;
            tblTopRowContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblTopRowContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblTopRowContainer.Controls.Add(cardGeneral, 0, 0);
            tblTopRowContainer.Controls.Add(cardMaintenanceState, 1, 0);
            tblTopRowContainer.Dock = DockStyle.Fill;
            tblTopRowContainer.Location = new Point(0, 0);
            tblTopRowContainer.Margin = new Padding(0, 0, 0, 9);
            tblTopRowContainer.Name = "tblTopRowContainer";
            tblTopRowContainer.RowCount = 1;
            tblTopRowContainer.RowStyles.Add(new RowStyle());
            tblTopRowContainer.Size = new Size(1057, 171);
            tblTopRowContainer.TabIndex = 0;
            // 
            // cardGeneral
            // 
            cardGeneral.AutoSize = true;
            cardGeneral.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            cardGeneral.BackColor = Color.White;
            cardGeneral.Controls.Add(tblGenCard);
            cardGeneral.Dock = DockStyle.Fill;
            cardGeneral.Location = new Point(0, 0);
            cardGeneral.Margin = new Padding(0, 0, 5, 0);
            cardGeneral.Name = "cardGeneral";
            cardGeneral.Size = new Size(523, 171);
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
            tblGenCard.Controls.Add(lblCompanyName, 0, 1);
            tblGenCard.Controls.Add(lblSysName, 3, 1);
            tblGenCard.Controls.Add(txtCompanyName, 0, 2);
            tblGenCard.Controls.Add(txtSysName, 3, 2);
            tblGenCard.Controls.Add(lblDataStorage, 0, 3);
            tblGenCard.Controls.Add(lblReportStorage, 3, 3);
            tblGenCard.Controls.Add(tblDataBrowse, 0, 4);
            tblGenCard.Controls.Add(tblReportBrowse, 3, 4);
            tblGenCard.Dock = DockStyle.Top;
            tblGenCard.Location = new Point(0, 0);
            tblGenCard.Margin = new Padding(4, 4, 4, 4);
            tblGenCard.Name = "tblGenCard";
            tblGenCard.Padding = new Padding(8, 0, 8, 7);
            tblGenCard.RowCount = 5;
            tblGenCard.RowStyles.Add(new RowStyle(SizeType.Absolute, 23F));
            tblGenCard.RowStyles.Add(new RowStyle());
            tblGenCard.RowStyles.Add(new RowStyle());
            tblGenCard.RowStyles.Add(new RowStyle());
            tblGenCard.RowStyles.Add(new RowStyle());
            tblGenCard.Size = new Size(523, 136);
            tblGenCard.TabIndex = 0;
            // 
            // lblGenTitle
            // 
            tblGenCard.SetColumnSpan(lblGenTitle, 6);
            lblGenTitle.Dock = DockStyle.Fill;
            lblGenTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblGenTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblGenTitle.Location = new Point(8, 0);
            lblGenTitle.Margin = new Padding(0, 0, 0, 7);
            lblGenTitle.Name = "lblGenTitle";
            lblGenTitle.Size = new Size(507, 16);
            lblGenTitle.TabIndex = 0;
            lblGenTitle.Text = "GENERAL SETTINGS";
            lblGenTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblCompanyName
            // 
            lblCompanyName.AutoSize = true;
            tblGenCard.SetColumnSpan(lblCompanyName, 3);
            lblCompanyName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCompanyName.ForeColor = Color.FromArgb(51, 65, 85);
            lblCompanyName.Location = new Point(8, 28);
            lblCompanyName.Margin = new Padding(0, 5, 0, 2);
            lblCompanyName.Name = "lblCompanyName";
            lblCompanyName.Size = new Size(94, 15);
            lblCompanyName.TabIndex = 1;
            lblCompanyName.Text = "Company Name";
            // 
            // lblSysName
            // 
            lblSysName.AutoSize = true;
            tblGenCard.SetColumnSpan(lblSysName, 3);
            lblSysName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSysName.ForeColor = Color.FromArgb(51, 65, 85);
            lblSysName.Location = new Point(268, 28);
            lblSysName.Margin = new Padding(8, 5, 0, 2);
            lblSysName.Name = "lblSysName";
            lblSysName.Size = new Size(84, 15);
            lblSysName.TabIndex = 2;
            lblSysName.Text = "System Name";
            // 
            // txtCompanyName
            // 
            txtCompanyName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tblGenCard.SetColumnSpan(txtCompanyName, 3);
            txtCompanyName.Font = new Font("Segoe UI", 9.5F);
            txtCompanyName.Location = new Point(8, 45);
            txtCompanyName.Margin = new Padding(0, 0, 8, 7);
            txtCompanyName.Name = "txtCompanyName";
            txtCompanyName.Size = new Size(244, 24);
            txtCompanyName.TabIndex = 3;
            txtCompanyName.Text = "Caterpillar Inc.";
            // 
            // txtSysName
            // 
            txtSysName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tblGenCard.SetColumnSpan(txtSysName, 3);
            txtSysName.Font = new Font("Segoe UI", 9.5F);
            txtSysName.Location = new Point(268, 45);
            txtSysName.Margin = new Padding(8, 0, 0, 7);
            txtSysName.Name = "txtSysName";
            txtSysName.Size = new Size(247, 24);
            txtSysName.TabIndex = 4;
            txtSysName.Text = "Assembly Line Test Rig #4";
            // 
            // lblDataStorage
            // 
            lblDataStorage.AutoSize = true;
            tblGenCard.SetColumnSpan(lblDataStorage, 3);
            lblDataStorage.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDataStorage.ForeColor = Color.FromArgb(51, 65, 85);
            lblDataStorage.Location = new Point(8, 81);
            lblDataStorage.Margin = new Padding(0, 5, 0, 2);
            lblDataStorage.Name = "lblDataStorage";
            lblDataStorage.Size = new Size(108, 15);
            lblDataStorage.TabIndex = 1;
            lblDataStorage.Text = "Data Storage Path";
            // 
            // lblReportStorage
            // 
            lblReportStorage.AutoSize = true;
            tblGenCard.SetColumnSpan(lblReportStorage, 3);
            lblReportStorage.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblReportStorage.ForeColor = Color.FromArgb(51, 65, 85);
            lblReportStorage.Location = new Point(268, 81);
            lblReportStorage.Margin = new Padding(8, 5, 0, 2);
            lblReportStorage.Name = "lblReportStorage";
            lblReportStorage.Size = new Size(121, 15);
            lblReportStorage.TabIndex = 2;
            lblReportStorage.Text = "Report Storage Path";
            // 
            // tblDataBrowse
            // 
            tblDataBrowse.AutoSize = true;
            tblDataBrowse.ColumnCount = 2;
            tblGenCard.SetColumnSpan(tblDataBrowse, 3);
            tblDataBrowse.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblDataBrowse.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 59F));
            tblDataBrowse.Controls.Add(txtDataPath, 0, 0);
            tblDataBrowse.Controls.Add(btnBrowseData, 1, 0);
            tblDataBrowse.Dock = DockStyle.Fill;
            tblDataBrowse.Location = new Point(8, 98);
            tblDataBrowse.Margin = new Padding(0, 0, 8, 7);
            tblDataBrowse.Name = "tblDataBrowse";
            tblDataBrowse.RowCount = 1;
            tblDataBrowse.RowStyles.Add(new RowStyle());
            tblDataBrowse.Size = new Size(244, 24);
            tblDataBrowse.TabIndex = 3;
            // 
            // txtDataPath
            // 
            txtDataPath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtDataPath.Font = new Font("Segoe UI", 9.5F);
            txtDataPath.Location = new Point(0, 0);
            txtDataPath.Margin = new Padding(0, 0, 5, 0);
            txtDataPath.Name = "txtDataPath";
            txtDataPath.ReadOnly = true;
            txtDataPath.Size = new Size(180, 24);
            txtDataPath.TabIndex = 0;
            txtDataPath.Text = "C:\\Data\\TestResults";
            // 
            // btnBrowseData
            // 
            btnBrowseData.Anchor = AnchorStyles.None;
            btnBrowseData.BackColor = Color.FromArgb(241, 245, 249);
            btnBrowseData.Cursor = Cursors.Hand;
            btnBrowseData.FlatStyle = FlatStyle.Flat;
            btnBrowseData.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnBrowseData.ForeColor = Color.FromArgb(51, 65, 85);
            btnBrowseData.HoverForeColor = Color.Empty;
            btnBrowseData.Location = new Point(185, 2);
            btnBrowseData.Margin = new Padding(0);
            btnBrowseData.Name = "btnBrowseData";
            btnBrowseData.PressedForeColor = Color.Empty;
            btnBrowseData.Size = new Size(59, 19);
            btnBrowseData.TabIndex = 1;
            btnBrowseData.Text = "Browse";
            btnBrowseData.UseVisualStyleBackColor = false;
            // 
            // tblReportBrowse
            // 
            tblReportBrowse.AutoSize = true;
            tblReportBrowse.ColumnCount = 2;
            tblGenCard.SetColumnSpan(tblReportBrowse, 3);
            tblReportBrowse.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblReportBrowse.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 59F));
            tblReportBrowse.Controls.Add(txtReportPath, 0, 0);
            tblReportBrowse.Controls.Add(btnBrowseReport, 1, 0);
            tblReportBrowse.Dock = DockStyle.Fill;
            tblReportBrowse.Location = new Point(268, 98);
            tblReportBrowse.Margin = new Padding(8, 0, 0, 7);
            tblReportBrowse.Name = "tblReportBrowse";
            tblReportBrowse.RowCount = 1;
            tblReportBrowse.RowStyles.Add(new RowStyle());
            tblReportBrowse.Size = new Size(247, 24);
            tblReportBrowse.TabIndex = 4;
            // 
            // txtReportPath
            // 
            txtReportPath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtReportPath.Font = new Font("Segoe UI", 9.5F);
            txtReportPath.Location = new Point(0, 0);
            txtReportPath.Margin = new Padding(0, 0, 5, 0);
            txtReportPath.Name = "txtReportPath";
            txtReportPath.ReadOnly = true;
            txtReportPath.Size = new Size(183, 24);
            txtReportPath.TabIndex = 0;
            txtReportPath.Text = "C:\\Reports\\Production";
            // 
            // btnBrowseReport
            // 
            btnBrowseReport.Anchor = AnchorStyles.None;
            btnBrowseReport.BackColor = Color.FromArgb(241, 245, 249);
            btnBrowseReport.Cursor = Cursors.Hand;
            btnBrowseReport.FlatStyle = FlatStyle.Flat;
            btnBrowseReport.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnBrowseReport.ForeColor = Color.FromArgb(51, 65, 85);
            btnBrowseReport.HoverForeColor = Color.Empty;
            btnBrowseReport.Location = new Point(188, 2);
            btnBrowseReport.Margin = new Padding(0);
            btnBrowseReport.Name = "btnBrowseReport";
            btnBrowseReport.PressedForeColor = Color.Empty;
            btnBrowseReport.Size = new Size(59, 19);
            btnBrowseReport.TabIndex = 1;
            btnBrowseReport.Text = "Browse";
            btnBrowseReport.UseVisualStyleBackColor = false;
            // 
            // cardMaintenanceState
            // 
            cardMaintenanceState.AutoSize = true;
            cardMaintenanceState.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            cardMaintenanceState.BackColor = Color.White;
            cardMaintenanceState.Controls.Add(tblMaintCard);
            cardMaintenanceState.Dock = DockStyle.Fill;
            cardMaintenanceState.Location = new Point(533, 0);
            cardMaintenanceState.Margin = new Padding(5, 0, 0, 0);
            cardMaintenanceState.Name = "cardMaintenanceState";
            cardMaintenanceState.Size = new Size(524, 171);
            cardMaintenanceState.TabIndex = 1;
            // 
            // tblMaintCard
            // 
            tblMaintCard.AutoSize = true;
            tblMaintCard.ColumnCount = 4;
            tblMaintCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblMaintCard.ColumnStyles.Add(new ColumnStyle());
            tblMaintCard.ColumnStyles.Add(new ColumnStyle());
            tblMaintCard.ColumnStyles.Add(new ColumnStyle());
            tblMaintCard.Controls.Add(lblMaintTitle, 0, 0);
            tblMaintCard.Controls.Add(flpMaintHeaderActions, 2, 0);
            tblMaintCard.Controls.Add(lblRemainingDays, 0, 1);
            tblMaintCard.Controls.Add(lblRemainingHours, 2, 1);
            tblMaintCard.Controls.Add(txtRemainingDays, 0, 2);
            tblMaintCard.Controls.Add(txtRemainingHours, 2, 2);
            tblMaintCard.Controls.Add(pnlMaintInfo, 0, 3);
            tblMaintCard.Controls.Add(flpMaintActions, 0, 4);
            tblMaintCard.Dock = DockStyle.Top;
            tblMaintCard.Location = new Point(0, 0);
            tblMaintCard.Margin = new Padding(4, 4, 4, 4);
            tblMaintCard.Name = "tblMaintCard";
            tblMaintCard.Padding = new Padding(8, 0, 8, 7);
            tblMaintCard.RowCount = 5;
            tblMaintCard.RowStyles.Add(new RowStyle(SizeType.Absolute, 23F));
            tblMaintCard.RowStyles.Add(new RowStyle());
            tblMaintCard.RowStyles.Add(new RowStyle());
            tblMaintCard.RowStyles.Add(new RowStyle());
            tblMaintCard.RowStyles.Add(new RowStyle());
            tblMaintCard.Size = new Size(524, 171);
            tblMaintCard.TabIndex = 0;
            // 
            // lblMaintTitle
            // 
            lblMaintTitle.AutoSize = true;
            tblMaintCard.SetColumnSpan(lblMaintTitle, 2);
            lblMaintTitle.Dock = DockStyle.Fill;
            lblMaintTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblMaintTitle.ForeColor = Color.FromArgb(51, 65, 85);
            lblMaintTitle.Location = new Point(10, 0);
            lblMaintTitle.Margin = new Padding(2, 0, 2, 0);
            lblMaintTitle.Name = "lblMaintTitle";
            lblMaintTitle.Size = new Size(240, 23);
            lblMaintTitle.TabIndex = 0;
            lblMaintTitle.Text = "MAINTENANCE STATE";
            lblMaintTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // flpMaintHeaderActions
            // 
            flpMaintHeaderActions.AutoSize = true;
            tblMaintCard.SetColumnSpan(flpMaintHeaderActions, 2);
            flpMaintHeaderActions.Controls.Add(btnEditMaintenance);
            flpMaintHeaderActions.Controls.Add(btnResetMaintenance);
            flpMaintHeaderActions.Dock = DockStyle.Right;
            flpMaintHeaderActions.FlowDirection = FlowDirection.RightToLeft;
            flpMaintHeaderActions.Location = new Point(376, 0);
            flpMaintHeaderActions.Margin = new Padding(0);
            flpMaintHeaderActions.Name = "flpMaintHeaderActions";
            flpMaintHeaderActions.Size = new Size(140, 23);
            flpMaintHeaderActions.TabIndex = 9;
            // 
            // btnEditMaintenance
            // 
            btnEditMaintenance.BackColor = Color.FromArgb(253, 230, 138);
            btnEditMaintenance.BorderRadius = 8;
            btnEditMaintenance.FlatStyle = FlatStyle.Flat;
            btnEditMaintenance.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEditMaintenance.ForeColor = Color.FromArgb(69, 26, 3);
            btnEditMaintenance.HoverForeColor = Color.Empty;
            btnEditMaintenance.Location = new Point(75, 2);
            btnEditMaintenance.Margin = new Padding(5, 2, 0, 2);
            btnEditMaintenance.Name = "btnEditMaintenance";
            btnEditMaintenance.PressedForeColor = Color.Empty;
            btnEditMaintenance.Size = new Size(65, 19);
            btnEditMaintenance.TabIndex = 2;
            btnEditMaintenance.Text = "✏️ Edit";
            btnEditMaintenance.UseVisualStyleBackColor = false;
            // 
            // btnResetMaintenance
            // 
            btnResetMaintenance.BackColor = Color.FromArgb(239, 246, 255);
            btnResetMaintenance.BorderRadius = 8;
            btnResetMaintenance.FlatStyle = FlatStyle.Flat;
            btnResetMaintenance.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnResetMaintenance.ForeColor = Color.FromArgb(30, 58, 138);
            btnResetMaintenance.HoverForeColor = Color.Empty;
            btnResetMaintenance.Location = new Point(5, 2);
            btnResetMaintenance.Margin = new Padding(5, 2, 0, 2);
            btnResetMaintenance.Name = "btnResetMaintenance";
            btnResetMaintenance.PressedForeColor = Color.Empty;
            btnResetMaintenance.Size = new Size(65, 19);
            btnResetMaintenance.TabIndex = 1;
            btnResetMaintenance.Text = "Reset";
            btnResetMaintenance.UseVisualStyleBackColor = false;
            // 
            // lblRemainingDays
            // 
            lblRemainingDays.AutoSize = true;
            tblMaintCard.SetColumnSpan(lblRemainingDays, 2);
            lblRemainingDays.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblRemainingDays.ForeColor = Color.FromArgb(100, 116, 139);
            lblRemainingDays.Location = new Point(8, 28);
            lblRemainingDays.Margin = new Padding(0, 5, 0, 2);
            lblRemainingDays.Name = "lblRemainingDays";
            lblRemainingDays.Size = new Size(93, 15);
            lblRemainingDays.TabIndex = 3;
            lblRemainingDays.Text = "Remaining days";
            // 
            // lblRemainingHours
            // 
            lblRemainingHours.AutoSize = true;
            tblMaintCard.SetColumnSpan(lblRemainingHours, 2);
            lblRemainingHours.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblRemainingHours.ForeColor = Color.FromArgb(100, 116, 139);
            lblRemainingHours.Location = new Point(260, 28);
            lblRemainingHours.Margin = new Padding(8, 5, 0, 2);
            lblRemainingHours.Name = "lblRemainingHours";
            lblRemainingHours.Size = new Size(100, 15);
            lblRemainingHours.TabIndex = 4;
            lblRemainingHours.Text = "Remaining hours";
            // 
            // txtRemainingDays
            // 
            txtRemainingDays.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tblMaintCard.SetColumnSpan(txtRemainingDays, 2);
            txtRemainingDays.Font = new Font("Segoe UI", 9.5F);
            txtRemainingDays.Location = new Point(8, 45);
            txtRemainingDays.Margin = new Padding(0, 0, 8, 7);
            txtRemainingDays.Name = "txtRemainingDays";
            txtRemainingDays.ReadOnly = true;
            txtRemainingDays.Size = new Size(236, 24);
            txtRemainingDays.TabIndex = 5;
            txtRemainingDays.Text = "180";
            // 
            // txtRemainingHours
            // 
            txtRemainingHours.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tblMaintCard.SetColumnSpan(txtRemainingHours, 2);
            txtRemainingHours.Font = new Font("Segoe UI", 9.5F);
            txtRemainingHours.InsertKeyMode = InsertKeyMode.Overwrite;
            txtRemainingHours.Location = new Point(260, 45);
            txtRemainingHours.Margin = new Padding(8, 0, 0, 7);
            txtRemainingHours.Mask = "0000:00:00";
            txtRemainingHours.Name = "txtRemainingHours";
            txtRemainingHours.ReadOnly = true;
            txtRemainingHours.Size = new Size(256, 24);
            txtRemainingHours.TabIndex = 6;
            txtRemainingHours.Text = "00240000";
            // 
            // pnlMaintInfo
            // 
            pnlMaintInfo.AutoSize = true;
            pnlMaintInfo.BackColor = Color.FromArgb(248, 250, 252);
            tblMaintCard.SetColumnSpan(pnlMaintInfo, 4);
            pnlMaintInfo.Controls.Add(tblMaintInfo);
            pnlMaintInfo.Dock = DockStyle.Fill;
            pnlMaintInfo.Location = new Point(8, 83);
            pnlMaintInfo.Margin = new Padding(0, 7, 0, 7);
            pnlMaintInfo.Name = "pnlMaintInfo";
            pnlMaintInfo.Padding = new Padding(8, 7, 8, 7);
            pnlMaintInfo.Size = new Size(508, 48);
            pnlMaintInfo.TabIndex = 7;
            // 
            // tblMaintInfo
            // 
            tblMaintInfo.AutoSize = true;
            tblMaintInfo.ColumnCount = 2;
            tblMaintInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblMaintInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblMaintInfo.Controls.Add(lblOperatingTimeTodayTitle, 0, 0);
            tblMaintInfo.Controls.Add(lblOperatingTimeTodayVal, 1, 0);
            tblMaintInfo.Controls.Add(lblLastUsageTitle, 0, 1);
            tblMaintInfo.Controls.Add(lblLastUsageVal, 1, 1);
            tblMaintInfo.Dock = DockStyle.Fill;
            tblMaintInfo.Location = new Point(8, 7);
            tblMaintInfo.Margin = new Padding(2, 1, 2, 1);
            tblMaintInfo.Name = "tblMaintInfo";
            tblMaintInfo.RowCount = 2;
            tblMaintInfo.RowStyles.Add(new RowStyle());
            tblMaintInfo.RowStyles.Add(new RowStyle());
            tblMaintInfo.Size = new Size(492, 34);
            tblMaintInfo.TabIndex = 0;
            // 
            // lblOperatingTimeTodayTitle
            // 
            lblOperatingTimeTodayTitle.AutoSize = true;
            lblOperatingTimeTodayTitle.Font = new Font("Segoe UI", 9F);
            lblOperatingTimeTodayTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblOperatingTimeTodayTitle.Location = new Point(0, 0);
            lblOperatingTimeTodayTitle.Margin = new Padding(0, 0, 0, 4);
            lblOperatingTimeTodayTitle.Name = "lblOperatingTimeTodayTitle";
            lblOperatingTimeTodayTitle.Size = new Size(120, 15);
            lblOperatingTimeTodayTitle.TabIndex = 0;
            lblOperatingTimeTodayTitle.Text = "Operating time today";
            // 
            // lblOperatingTimeTodayVal
            // 
            lblOperatingTimeTodayVal.AutoSize = true;
            lblOperatingTimeTodayVal.Dock = DockStyle.Right;
            lblOperatingTimeTodayVal.Font = new Font("Segoe UI", 9F);
            lblOperatingTimeTodayVal.ForeColor = Color.FromArgb(51, 65, 85);
            lblOperatingTimeTodayVal.Location = new Point(443, 0);
            lblOperatingTimeTodayVal.Margin = new Padding(0, 0, 0, 4);
            lblOperatingTimeTodayVal.Name = "lblOperatingTimeTodayVal";
            lblOperatingTimeTodayVal.Size = new Size(49, 15);
            lblOperatingTimeTodayVal.TabIndex = 1;
            lblOperatingTimeTodayVal.Text = "00:00:00";
            // 
            // lblLastUsageTitle
            // 
            lblLastUsageTitle.AutoSize = true;
            lblLastUsageTitle.Font = new Font("Segoe UI", 9F);
            lblLastUsageTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblLastUsageTitle.Location = new Point(0, 19);
            lblLastUsageTitle.Margin = new Padding(0);
            lblLastUsageTitle.Name = "lblLastUsageTitle";
            lblLastUsageTitle.Size = new Size(62, 15);
            lblLastUsageTitle.TabIndex = 2;
            lblLastUsageTitle.Text = "Last usage";
            // 
            // lblLastUsageVal
            // 
            lblLastUsageVal.AutoSize = true;
            lblLastUsageVal.Dock = DockStyle.Right;
            lblLastUsageVal.Font = new Font("Segoe UI", 9F);
            lblLastUsageVal.ForeColor = Color.FromArgb(51, 65, 85);
            lblLastUsageVal.Location = new Point(426, 19);
            lblLastUsageVal.Margin = new Padding(0);
            lblLastUsageVal.Name = "lblLastUsageVal";
            lblLastUsageVal.Size = new Size(66, 15);
            lblLastUsageVal.TabIndex = 3;
            lblLastUsageVal.Text = "01 Jan 2026";
            // 
            // flpMaintActions
            // 
            flpMaintActions.AutoSize = true;
            tblMaintCard.SetColumnSpan(flpMaintActions, 4);
            flpMaintActions.Controls.Add(btnUpdateMaintenance);
            flpMaintActions.Controls.Add(btnCancelMaintenance);
            flpMaintActions.Controls.Add(lblEditHint);
            flpMaintActions.Dock = DockStyle.Right;
            flpMaintActions.FlowDirection = FlowDirection.RightToLeft;
            flpMaintActions.Location = new Point(117, 139);
            flpMaintActions.Margin = new Padding(2, 1, 2, 1);
            flpMaintActions.Name = "flpMaintActions";
            flpMaintActions.Size = new Size(397, 24);
            flpMaintActions.TabIndex = 8;
            // 
            // btnUpdateMaintenance
            // 
            btnUpdateMaintenance.BackColor = Color.FromArgb(253, 230, 138);
            btnUpdateMaintenance.BorderRadius = 8;
            btnUpdateMaintenance.FlatStyle = FlatStyle.Flat;
            btnUpdateMaintenance.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnUpdateMaintenance.ForeColor = Color.FromArgb(69, 26, 3);
            btnUpdateMaintenance.HoverForeColor = Color.Empty;
            btnUpdateMaintenance.Location = new Point(316, 1);
            btnUpdateMaintenance.Margin = new Padding(5, 1, 0, 1);
            btnUpdateMaintenance.Name = "btnUpdateMaintenance";
            btnUpdateMaintenance.PressedForeColor = Color.Empty;
            btnUpdateMaintenance.Size = new Size(81, 22);
            btnUpdateMaintenance.TabIndex = 1;
            btnUpdateMaintenance.Text = "🔄 Update";
            btnUpdateMaintenance.UseVisualStyleBackColor = false;
            btnUpdateMaintenance.Visible = false;
            // 
            // btnCancelMaintenance
            // 
            btnCancelMaintenance.BackColor = Color.White;
            btnCancelMaintenance.BorderRadius = 8;
            btnCancelMaintenance.FlatStyle = FlatStyle.Flat;
            btnCancelMaintenance.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCancelMaintenance.ForeColor = Color.FromArgb(51, 65, 85);
            btnCancelMaintenance.HoverForeColor = Color.Empty;
            btnCancelMaintenance.Location = new Point(231, 1);
            btnCancelMaintenance.Margin = new Padding(5, 1, 5, 1);
            btnCancelMaintenance.Name = "btnCancelMaintenance";
            btnCancelMaintenance.PressedForeColor = Color.Empty;
            btnCancelMaintenance.Size = new Size(75, 22);
            btnCancelMaintenance.TabIndex = 0;
            btnCancelMaintenance.Text = "Cancel";
            btnCancelMaintenance.UseVisualStyleBackColor = false;
            btnCancelMaintenance.Visible = false;
            // 
            // lblEditHint
            // 
            lblEditHint.Anchor = AnchorStyles.Right;
            lblEditHint.AutoSize = true;
            lblEditHint.Font = new Font("Segoe UI", 8.5F);
            lblEditHint.ForeColor = Color.FromArgb(148, 163, 184);
            lblEditHint.Location = new Point(2, 4);
            lblEditHint.Margin = new Padding(2, 0, 2, 0);
            lblEditHint.Name = "lblEditHint";
            lblEditHint.Size = new Size(222, 15);
            lblEditHint.TabIndex = 2;
            lblEditHint.Text = "Update and Cancel appear only after Edit";
            lblEditHint.Visible = false;
            // 
            // cardTestSettings
            // 
            cardTestSettings.AutoSize = true;
            cardTestSettings.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            cardTestSettings.BackColor = Color.White;
            cardTestSettings.Controls.Add(tblTestCard);
            cardTestSettings.Dock = DockStyle.Fill;
            cardTestSettings.Location = new Point(0, 180);
            cardTestSettings.Margin = new Padding(0, 0, 0, 9);
            cardTestSettings.Name = "cardTestSettings";
            cardTestSettings.Size = new Size(1057, 291);
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
            tblTestCard.Controls.Add(txtTotalTestPoint, 0, 2);
            tblTestCard.Controls.Add(lblDefTestType, 1, 1);
            tblTestCard.Controls.Add(cmbDefTestType, 1, 2);
            tblTestCard.Controls.Add(lblDefResistorVal, 2, 1);
            tblTestCard.Controls.Add(txtDefResistorVal, 2, 2);
            tblTestCard.Controls.Add(lblDefResistorTol, 3, 1);
            tblTestCard.Controls.Add(txtDefResistorTol, 3, 2);
            tblTestCard.Controls.Add(lblDefCapacitorVal, 0, 3);
            tblTestCard.Controls.Add(txtDefCapacitorVal, 0, 4);
            tblTestCard.Controls.Add(lblDefCapacitorTol, 1, 3);
            tblTestCard.Controls.Add(txtDefCapacitorTol, 1, 4);
            tblTestCard.Controls.Add(lblDefWireInsRes, 2, 3);
            tblTestCard.Controls.Add(txtDefWireInsRes, 2, 4);
            tblTestCard.Controls.Add(lblDefMaxWireRes, 3, 3);
            tblTestCard.Controls.Add(txtDefMaxWireRes, 3, 4);
            tblTestCard.Controls.Add(lblTestTypeAvail, 0, 5);
            tblTestCard.Controls.Add(lblLearnConn, 1, 5);
            tblTestCard.Controls.Add(lblTestResultCol, 2, 5);
            tblTestCard.Controls.Add(flpTestTypeAvail, 0, 6);
            tblTestCard.Controls.Add(flpLearnConn, 1, 6);
            tblTestCard.Controls.Add(flpTestResultCol, 2, 6);
            tblTestCard.Dock = DockStyle.Top;
            tblTestCard.Location = new Point(0, 0);
            tblTestCard.Margin = new Padding(4, 4, 4, 4);
            tblTestCard.Name = "tblTestCard";
            tblTestCard.Padding = new Padding(8, 0, 8, 7);
            tblTestCard.RowCount = 7;
            tblTestCard.RowStyles.Add(new RowStyle(SizeType.Absolute, 23F));
            tblTestCard.RowStyles.Add(new RowStyle());
            tblTestCard.RowStyles.Add(new RowStyle());
            tblTestCard.RowStyles.Add(new RowStyle());
            tblTestCard.RowStyles.Add(new RowStyle());
            tblTestCard.RowStyles.Add(new RowStyle());
            tblTestCard.RowStyles.Add(new RowStyle());
            tblTestCard.Size = new Size(1057, 291);
            tblTestCard.TabIndex = 0;
            // 
            // lblTestTitle
            // 
            lblTestTitle.Dock = DockStyle.Fill;
            lblTestTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTestTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTestTitle.Location = new Point(8, 0);
            lblTestTitle.Margin = new Padding(0, 0, 0, 7);
            lblTestTitle.Name = "lblTestTitle";
            lblTestTitle.Size = new Size(260, 16);
            lblTestTitle.TabIndex = 0;
            lblTestTitle.Text = "DEFAULT TEST SETTINGS";
            lblTestTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTotalTestPoint
            // 
            lblTotalTestPoint.AutoSize = true;
            lblTotalTestPoint.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotalTestPoint.ForeColor = Color.FromArgb(51, 65, 85);
            lblTotalTestPoint.Location = new Point(8, 28);
            lblTotalTestPoint.Margin = new Padding(0, 5, 0, 2);
            lblTotalTestPoint.Name = "lblTotalTestPoint";
            lblTotalTestPoint.Size = new Size(145, 15);
            lblTotalTestPoint.TabIndex = 1;
            lblTotalTestPoint.Text = "Total Test Point Available";
            // 
            // txtTotalTestPoint
            // 
            txtTotalTestPoint.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtTotalTestPoint.Font = new Font("Segoe UI", 9.5F);
            txtTotalTestPoint.Location = new Point(8, 45);
            txtTotalTestPoint.Margin = new Padding(0, 0, 8, 7);
            txtTotalTestPoint.Name = "txtTotalTestPoint";
            txtTotalTestPoint.Size = new Size(252, 24);
            txtTotalTestPoint.TabIndex = 5;
            // 
            // lblDefTestType
            // 
            lblDefTestType.AutoSize = true;
            lblDefTestType.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDefTestType.ForeColor = Color.FromArgb(51, 65, 85);
            lblDefTestType.Location = new Point(268, 28);
            lblDefTestType.Margin = new Padding(0, 5, 0, 2);
            lblDefTestType.Name = "lblDefTestType";
            lblDefTestType.Size = new Size(59, 15);
            lblDefTestType.TabIndex = 2;
            lblDefTestType.Text = "Test Type";
            // 
            // cmbDefTestType
            // 
            cmbDefTestType.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbDefTestType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDefTestType.FlatStyle = FlatStyle.Flat;
            cmbDefTestType.Font = new Font("Segoe UI", 9.5F);
            cmbDefTestType.Location = new Point(268, 45);
            cmbDefTestType.Margin = new Padding(0, 0, 8, 7);
            cmbDefTestType.Name = "cmbDefTestType";
            cmbDefTestType.Size = new Size(252, 25);
            cmbDefTestType.TabIndex = 6;
            // 
            // lblDefResistorVal
            // 
            lblDefResistorVal.AutoSize = true;
            lblDefResistorVal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDefResistorVal.ForeColor = Color.FromArgb(51, 65, 85);
            lblDefResistorVal.Location = new Point(528, 28);
            lblDefResistorVal.Margin = new Padding(0, 5, 0, 2);
            lblDefResistorVal.Name = "lblDefResistorVal";
            lblDefResistorVal.Size = new Size(105, 15);
            lblDefResistorVal.TabIndex = 3;
            lblDefResistorVal.Text = "Resistor Value (Ω)";
            // 
            // txtDefResistorVal
            // 
            txtDefResistorVal.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtDefResistorVal.Font = new Font("Segoe UI", 9.5F);
            txtDefResistorVal.Location = new Point(528, 45);
            txtDefResistorVal.Margin = new Padding(0, 0, 8, 7);
            txtDefResistorVal.Name = "txtDefResistorVal";
            txtDefResistorVal.Size = new Size(252, 24);
            txtDefResistorVal.TabIndex = 7;
            // 
            // lblDefResistorTol
            // 
            lblDefResistorTol.AutoSize = true;
            lblDefResistorTol.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDefResistorTol.ForeColor = Color.FromArgb(51, 65, 85);
            lblDefResistorTol.Location = new Point(788, 28);
            lblDefResistorTol.Margin = new Padding(0, 5, 0, 2);
            lblDefResistorTol.Name = "lblDefResistorTol";
            lblDefResistorTol.Size = new Size(130, 15);
            lblDefResistorTol.TabIndex = 10;
            lblDefResistorTol.Text = "Resistor Tolerance (%)";
            // 
            // txtDefResistorTol
            // 
            txtDefResistorTol.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtDefResistorTol.Font = new Font("Segoe UI", 9.5F);
            txtDefResistorTol.Location = new Point(788, 45);
            txtDefResistorTol.Margin = new Padding(0, 0, 8, 7);
            txtDefResistorTol.Name = "txtDefResistorTol";
            txtDefResistorTol.Size = new Size(253, 24);
            txtDefResistorTol.TabIndex = 14;
            // 
            // lblDefCapacitorVal
            // 
            lblDefCapacitorVal.AutoSize = true;
            lblDefCapacitorVal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDefCapacitorVal.ForeColor = Color.FromArgb(51, 65, 85);
            lblDefCapacitorVal.Location = new Point(8, 82);
            lblDefCapacitorVal.Margin = new Padding(0, 5, 0, 2);
            lblDefCapacitorVal.Name = "lblDefCapacitorVal";
            lblDefCapacitorVal.Size = new Size(117, 15);
            lblDefCapacitorVal.TabIndex = 12;
            lblDefCapacitorVal.Text = "Capacitor Value (F)";
            // 
            // txtDefCapacitorVal
            // 
            txtDefCapacitorVal.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtDefCapacitorVal.Font = new Font("Segoe UI", 9.5F);
            txtDefCapacitorVal.Location = new Point(8, 99);
            txtDefCapacitorVal.Margin = new Padding(0, 0, 8, 7);
            txtDefCapacitorVal.Name = "txtDefCapacitorVal";
            txtDefCapacitorVal.Size = new Size(252, 24);
            txtDefCapacitorVal.TabIndex = 16;
            // 
            // lblDefCapacitorTol
            // 
            lblDefCapacitorTol.AutoSize = true;
            lblDefCapacitorTol.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDefCapacitorTol.ForeColor = Color.FromArgb(51, 65, 85);
            lblDefCapacitorTol.Location = new Point(268, 82);
            lblDefCapacitorTol.Margin = new Padding(0, 5, 0, 2);
            lblDefCapacitorTol.Name = "lblDefCapacitorTol";
            lblDefCapacitorTol.Size = new Size(137, 15);
            lblDefCapacitorTol.TabIndex = 17;
            lblDefCapacitorTol.Text = "Capacitor Tolerance (%)";
            // 
            // txtDefCapacitorTol
            // 
            txtDefCapacitorTol.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtDefCapacitorTol.Font = new Font("Segoe UI", 9.5F);
            txtDefCapacitorTol.Location = new Point(268, 99);
            txtDefCapacitorTol.Margin = new Padding(0, 0, 8, 7);
            txtDefCapacitorTol.Name = "txtDefCapacitorTol";
            txtDefCapacitorTol.Size = new Size(252, 24);
            txtDefCapacitorTol.TabIndex = 20;
            // 
            // lblDefWireInsRes
            // 
            lblDefWireInsRes.AutoSize = true;
            lblDefWireInsRes.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDefWireInsRes.ForeColor = Color.FromArgb(51, 65, 85);
            lblDefWireInsRes.Location = new Point(528, 82);
            lblDefWireInsRes.Margin = new Padding(0, 5, 0, 2);
            lblDefWireInsRes.Name = "lblDefWireInsRes";
            lblDefWireInsRes.Size = new Size(172, 15);
            lblDefWireInsRes.TabIndex = 11;
            lblDefWireInsRes.Text = "Min Wire Insulation Res. (MΩ)";
            // 
            // txtDefWireInsRes
            // 
            txtDefWireInsRes.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtDefWireInsRes.Font = new Font("Segoe UI", 9.5F);
            txtDefWireInsRes.Location = new Point(528, 99);
            txtDefWireInsRes.Margin = new Padding(0, 0, 8, 7);
            txtDefWireInsRes.Name = "txtDefWireInsRes";
            txtDefWireInsRes.Size = new Size(252, 24);
            txtDefWireInsRes.TabIndex = 15;
            // 
            // lblDefMaxWireRes
            // 
            lblDefMaxWireRes.AutoSize = true;
            lblDefMaxWireRes.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDefMaxWireRes.ForeColor = Color.FromArgb(51, 65, 85);
            lblDefMaxWireRes.Location = new Point(788, 82);
            lblDefMaxWireRes.Margin = new Padding(0, 5, 0, 2);
            lblDefMaxWireRes.Name = "lblDefMaxWireRes";
            lblDefMaxWireRes.Size = new Size(143, 15);
            lblDefMaxWireRes.TabIndex = 4;
            lblDefMaxWireRes.Text = "Max Wire Resistance (Ω)";
            // 
            // txtDefMaxWireRes
            // 
            txtDefMaxWireRes.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtDefMaxWireRes.Font = new Font("Segoe UI", 9.5F);
            txtDefMaxWireRes.Location = new Point(788, 99);
            txtDefMaxWireRes.Margin = new Padding(0, 0, 8, 7);
            txtDefMaxWireRes.Name = "txtDefMaxWireRes";
            txtDefMaxWireRes.Size = new Size(253, 24);
            txtDefMaxWireRes.TabIndex = 8;
            // 
            // lblTestTypeAvail
            // 
            lblTestTypeAvail.AutoSize = true;
            lblTestTypeAvail.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTestTypeAvail.ForeColor = Color.FromArgb(51, 65, 85);
            lblTestTypeAvail.Location = new Point(8, 135);
            lblTestTypeAvail.Margin = new Padding(0, 5, 0, 2);
            lblTestTypeAvail.Name = "lblTestTypeAvail";
            lblTestTypeAvail.Size = new Size(112, 15);
            lblTestTypeAvail.TabIndex = 9;
            lblTestTypeAvail.Text = "Test Type Available";
            // 
            // lblLearnConn
            // 
            lblLearnConn.AutoSize = true;
            lblLearnConn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblLearnConn.ForeColor = Color.FromArgb(51, 65, 85);
            lblLearnConn.Location = new Point(268, 135);
            lblLearnConn.Margin = new Padding(0, 5, 0, 2);
            lblLearnConn.Name = "lblLearnConn";
            lblLearnConn.Size = new Size(104, 15);
            lblLearnConn.TabIndex = 18;
            lblLearnConn.Text = "Learn Connection";
            // 
            // lblTestResultCol
            // 
            lblTestResultCol.AutoSize = true;
            tblTestCard.SetColumnSpan(lblTestResultCol, 2);
            lblTestResultCol.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTestResultCol.ForeColor = Color.FromArgb(51, 65, 85);
            lblTestResultCol.Location = new Point(528, 135);
            lblTestResultCol.Margin = new Padding(0, 5, 0, 2);
            lblTestResultCol.Name = "lblTestResultCol";
            lblTestResultCol.Size = new Size(113, 15);
            lblTestResultCol.TabIndex = 19;
            lblTestResultCol.Text = "Test Result Column";
            // 
            // flpTestTypeAvail
            // 
            flpTestTypeAvail.AutoSize = true;
            flpTestTypeAvail.FlowDirection = FlowDirection.TopDown;
            flpTestTypeAvail.Location = new Point(8, 152);
            flpTestTypeAvail.Margin = new Padding(0, 0, 8, 7);
            flpTestTypeAvail.MaximumSize = new Size(248, 188);
            flpTestTypeAvail.Name = "flpTestTypeAvail";
            flpTestTypeAvail.Size = new Size(0, 0);
            flpTestTypeAvail.TabIndex = 13;
            // 
            // flpLearnConn
            // 
            flpLearnConn.AutoSize = true;
            flpLearnConn.FlowDirection = FlowDirection.TopDown;
            flpLearnConn.Location = new Point(268, 152);
            flpLearnConn.Margin = new Padding(0, 0, 8, 7);
            flpLearnConn.Name = "flpLearnConn";
            flpLearnConn.Size = new Size(0, 0);
            flpLearnConn.TabIndex = 21;
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
            flpTestResultCol.Location = new Point(528, 152);
            flpTestResultCol.Margin = new Padding(0, 0, 8, 7);
            flpTestResultCol.MaximumSize = new Size(431, 141);
            flpTestResultCol.Name = "flpTestResultCol";
            flpTestResultCol.Size = new Size(339, 125);
            flpTestResultCol.TabIndex = 22;
            // 
            // chkResNetId
            // 
            chkResNetId.AutoSize = true;
            chkResNetId.Font = new Font("Segoe UI", 9.5F);
            chkResNetId.ForeColor = Color.FromArgb(30, 41, 59);
            chkResNetId.Location = new Point(0, 2);
            chkResNetId.Margin = new Padding(0, 2, 0, 2);
            chkResNetId.Name = "chkResNetId";
            chkResNetId.Size = new Size(65, 21);
            chkResNetId.TabIndex = 0;
            chkResNetId.Text = "Net-ID";
            chkResNetId.UseVisualStyleBackColor = true;
            // 
            // chkResHarnessGrp
            // 
            chkResHarnessGrp.AutoSize = true;
            chkResHarnessGrp.Font = new Font("Segoe UI", 9.5F);
            chkResHarnessGrp.ForeColor = Color.FromArgb(30, 41, 59);
            chkResHarnessGrp.Location = new Point(0, 27);
            chkResHarnessGrp.Margin = new Padding(0, 2, 0, 2);
            chkResHarnessGrp.Name = "chkResHarnessGrp";
            chkResHarnessGrp.Size = new Size(115, 21);
            chkResHarnessGrp.TabIndex = 1;
            chkResHarnessGrp.Text = "Harness Group";
            chkResHarnessGrp.UseVisualStyleBackColor = true;
            // 
            // chkResFrom
            // 
            chkResFrom.AutoSize = true;
            chkResFrom.Font = new Font("Segoe UI", 9.5F);
            chkResFrom.ForeColor = Color.FromArgb(30, 41, 59);
            chkResFrom.Location = new Point(0, 52);
            chkResFrom.Margin = new Padding(0, 2, 0, 2);
            chkResFrom.Name = "chkResFrom";
            chkResFrom.Size = new Size(57, 21);
            chkResFrom.TabIndex = 2;
            chkResFrom.Text = "From";
            chkResFrom.UseVisualStyleBackColor = true;
            // 
            // chkResNetFromLbl
            // 
            chkResNetFromLbl.AutoSize = true;
            chkResNetFromLbl.Font = new Font("Segoe UI", 9.5F);
            chkResNetFromLbl.ForeColor = Color.FromArgb(30, 41, 59);
            chkResNetFromLbl.Location = new Point(0, 77);
            chkResNetFromLbl.Margin = new Padding(0, 2, 0, 2);
            chkResNetFromLbl.Name = "chkResNetFromLbl";
            chkResNetFromLbl.Size = new Size(117, 21);
            chkResNetFromLbl.TabIndex = 3;
            chkResNetFromLbl.Text = "Net From Label";
            chkResNetFromLbl.UseVisualStyleBackColor = true;
            // 
            // chkResTo
            // 
            chkResTo.AutoSize = true;
            chkResTo.Font = new Font("Segoe UI", 9.5F);
            chkResTo.ForeColor = Color.FromArgb(30, 41, 59);
            chkResTo.Location = new Point(0, 102);
            chkResTo.Margin = new Padding(0, 2, 0, 2);
            chkResTo.Name = "chkResTo";
            chkResTo.Size = new Size(41, 21);
            chkResTo.TabIndex = 4;
            chkResTo.Text = "To";
            chkResTo.UseVisualStyleBackColor = true;
            // 
            // chkResNetToLbl
            // 
            chkResNetToLbl.AutoSize = true;
            chkResNetToLbl.Font = new Font("Segoe UI", 9.5F);
            chkResNetToLbl.ForeColor = Color.FromArgb(30, 41, 59);
            chkResNetToLbl.Location = new Point(117, 2);
            chkResNetToLbl.Margin = new Padding(0, 2, 0, 2);
            chkResNetToLbl.Name = "chkResNetToLbl";
            chkResNetToLbl.Size = new Size(101, 21);
            chkResNetToLbl.TabIndex = 5;
            chkResNetToLbl.Text = "Net To Label";
            chkResNetToLbl.UseVisualStyleBackColor = true;
            // 
            // chkResTestType
            // 
            chkResTestType.AutoSize = true;
            chkResTestType.Font = new Font("Segoe UI", 9.5F);
            chkResTestType.ForeColor = Color.FromArgb(30, 41, 59);
            chkResTestType.Location = new Point(117, 27);
            chkResTestType.Margin = new Padding(0, 2, 0, 2);
            chkResTestType.Name = "chkResTestType";
            chkResTestType.Size = new Size(81, 21);
            chkResTestType.TabIndex = 6;
            chkResTestType.Text = "Test Type";
            chkResTestType.UseVisualStyleBackColor = true;
            // 
            // chkResLowerLimit
            // 
            chkResLowerLimit.AutoSize = true;
            chkResLowerLimit.Font = new Font("Segoe UI", 9.5F);
            chkResLowerLimit.ForeColor = Color.FromArgb(30, 41, 59);
            chkResLowerLimit.Location = new Point(117, 52);
            chkResLowerLimit.Margin = new Padding(0, 2, 0, 2);
            chkResLowerLimit.Name = "chkResLowerLimit";
            chkResLowerLimit.Size = new Size(93, 21);
            chkResLowerLimit.TabIndex = 7;
            chkResLowerLimit.Text = "Lower Limit";
            chkResLowerLimit.UseVisualStyleBackColor = true;
            // 
            // chkResUpperLimit
            // 
            chkResUpperLimit.AutoSize = true;
            chkResUpperLimit.Font = new Font("Segoe UI", 9.5F);
            chkResUpperLimit.ForeColor = Color.FromArgb(30, 41, 59);
            chkResUpperLimit.Location = new Point(117, 77);
            chkResUpperLimit.Margin = new Padding(0, 2, 0, 2);
            chkResUpperLimit.Name = "chkResUpperLimit";
            chkResUpperLimit.Size = new Size(95, 21);
            chkResUpperLimit.TabIndex = 8;
            chkResUpperLimit.Text = "Upper Limit";
            chkResUpperLimit.UseVisualStyleBackColor = true;
            // 
            // chkResExpectedVal
            // 
            chkResExpectedVal.AutoSize = true;
            chkResExpectedVal.Font = new Font("Segoe UI", 9.5F);
            chkResExpectedVal.ForeColor = Color.FromArgb(30, 41, 59);
            chkResExpectedVal.Location = new Point(117, 102);
            chkResExpectedVal.Margin = new Padding(0, 2, 0, 2);
            chkResExpectedVal.Name = "chkResExpectedVal";
            chkResExpectedVal.Size = new Size(115, 21);
            chkResExpectedVal.TabIndex = 9;
            chkResExpectedVal.Text = "Expected Value";
            chkResExpectedVal.UseVisualStyleBackColor = true;
            // 
            // chkResTolerance
            // 
            chkResTolerance.AutoSize = true;
            chkResTolerance.Font = new Font("Segoe UI", 9.5F);
            chkResTolerance.ForeColor = Color.FromArgb(30, 41, 59);
            chkResTolerance.Location = new Point(232, 2);
            chkResTolerance.Margin = new Padding(0, 2, 0, 2);
            chkResTolerance.Name = "chkResTolerance";
            chkResTolerance.Size = new Size(83, 21);
            chkResTolerance.TabIndex = 10;
            chkResTolerance.Text = "Tolerance";
            chkResTolerance.UseVisualStyleBackColor = true;
            // 
            // chkResMeasurement
            // 
            chkResMeasurement.AutoSize = true;
            chkResMeasurement.Font = new Font("Segoe UI", 9.5F);
            chkResMeasurement.ForeColor = Color.FromArgb(30, 41, 59);
            chkResMeasurement.Location = new Point(232, 27);
            chkResMeasurement.Margin = new Padding(0, 2, 0, 2);
            chkResMeasurement.Name = "chkResMeasurement";
            chkResMeasurement.Size = new Size(107, 21);
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
            cardAppSettings.Location = new Point(0, 480);
            cardAppSettings.Margin = new Padding(0);
            cardAppSettings.Name = "cardAppSettings";
            cardAppSettings.Size = new Size(1057, 155);
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
            tblAppCard.Margin = new Padding(4, 4, 4, 4);
            tblAppCard.Name = "tblAppCard";
            tblAppCard.Padding = new Padding(8, 0, 8, 7);
            tblAppCard.RowCount = 5;
            tblAppCard.RowStyles.Add(new RowStyle(SizeType.Absolute, 23F));
            tblAppCard.RowStyles.Add(new RowStyle());
            tblAppCard.RowStyles.Add(new RowStyle());
            tblAppCard.RowStyles.Add(new RowStyle());
            tblAppCard.RowStyles.Add(new RowStyle());
            tblAppCard.Size = new Size(1057, 155);
            tblAppCard.TabIndex = 0;
            // 
            // lblAppTitle
            // 
            tblAppCard.SetColumnSpan(lblAppTitle, 2);
            lblAppTitle.Dock = DockStyle.Fill;
            lblAppTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblAppTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblAppTitle.Location = new Point(8, 0);
            lblAppTitle.Margin = new Padding(0, 0, 0, 7);
            lblAppTitle.Name = "lblAppTitle";
            lblAppTitle.Size = new Size(1041, 16);
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
            chkCheckUpdate.Location = new Point(8, 28);
            chkCheckUpdate.Margin = new Padding(0, 5, 5, 5);
            chkCheckUpdate.Name = "chkCheckUpdate";
            chkCheckUpdate.Size = new Size(245, 21);
            chkCheckUpdate.TabIndex = 1;
            chkCheckUpdate.Text = "Check for software update on startup";
            // 
            // lblRefresh
            // 
            lblRefresh.AutoSize = true;
            lblRefresh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblRefresh.ForeColor = Color.FromArgb(30, 41, 59);
            lblRefresh.Location = new Point(536, 28);
            lblRefresh.Margin = new Padding(8, 5, 0, 2);
            lblRefresh.Name = "lblRefresh";
            lblRefresh.Size = new Size(146, 15);
            lblRefresh.TabIndex = 2;
            lblRefresh.Text = "Refresh dashboard every";
            // 
            // chkMinTray
            // 
            chkMinTray.AutoSize = true;
            chkMinTray.Font = new Font("Segoe UI", 9.5F);
            chkMinTray.ForeColor = Color.FromArgb(30, 41, 59);
            chkMinTray.Location = new Point(8, 59);
            chkMinTray.Margin = new Padding(0, 5, 5, 5);
            chkMinTray.Name = "chkMinTray";
            chkMinTray.Size = new Size(165, 21);
            chkMinTray.TabIndex = 3;
            chkMinTray.Text = "Minimize to system tray";
            // 
            // cmbRefresh
            // 
            cmbRefresh.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbRefresh.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRefresh.Font = new Font("Segoe UI", 9.5F);
            cmbRefresh.Items.AddRange(new object[] { "10 Seconds", "30 Seconds", "1 Minute", "5 Minutes" });
            cmbRefresh.Location = new Point(536, 54);
            cmbRefresh.Margin = new Padding(8, 0, 0, 7);
            cmbRefresh.Name = "cmbRefresh";
            cmbRefresh.Size = new Size(513, 25);
            cmbRefresh.TabIndex = 4;
            // 
            // chkSoundNotif
            // 
            chkSoundNotif.AutoSize = true;
            chkSoundNotif.Checked = true;
            chkSoundNotif.CheckState = CheckState.Checked;
            chkSoundNotif.Font = new Font("Segoe UI", 9.5F);
            chkSoundNotif.ForeColor = Color.FromArgb(30, 41, 59);
            chkSoundNotif.Location = new Point(8, 91);
            chkSoundNotif.Margin = new Padding(0, 5, 5, 5);
            chkSoundNotif.Name = "chkSoundNotif";
            chkSoundNotif.Size = new Size(180, 21);
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
            chkConfirmDelete.Location = new Point(536, 91);
            chkConfirmDelete.Margin = new Padding(8, 5, 5, 5);
            chkConfirmDelete.Name = "chkConfirmDelete";
            chkConfirmDelete.Size = new Size(197, 21);
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
            chkAuditTrail.Location = new Point(536, 122);
            chkAuditTrail.Margin = new Padding(8, 5, 5, 5);
            chkAuditTrail.Name = "chkAuditTrail";
            chkAuditTrail.Size = new Size(125, 21);
            chkAuditTrail.TabIndex = 7;
            chkAuditTrail.Text = "Enable audit trail";
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
            // Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(244, 245, 247);
            Controls.Add(tblMain);
            Margin = new Padding(4, 4, 4, 4);
            Name = "Settings";
            Size = new Size(1102, 685);
            tblMain.ResumeLayout(false);
            flpTabs.ResumeLayout(false);
            flpTabs.PerformLayout();
            tblContent.ResumeLayout(false);
            tblContent.PerformLayout();
            tblLeftColumn.ResumeLayout(false);
            tblLeftColumn.PerformLayout();
            tblTopRowContainer.ResumeLayout(false);
            tblTopRowContainer.PerformLayout();
            cardGeneral.ResumeLayout(false);
            cardGeneral.PerformLayout();
            tblGenCard.ResumeLayout(false);
            tblGenCard.PerformLayout();
            tblDataBrowse.ResumeLayout(false);
            tblDataBrowse.PerformLayout();
            tblReportBrowse.ResumeLayout(false);
            tblReportBrowse.PerformLayout();
            cardMaintenanceState.ResumeLayout(false);
            cardMaintenanceState.PerformLayout();
            tblMaintCard.ResumeLayout(false);
            tblMaintCard.PerformLayout();
            flpMaintHeaderActions.ResumeLayout(false);
            pnlMaintInfo.ResumeLayout(false);
            pnlMaintInfo.PerformLayout();
            tblMaintInfo.ResumeLayout(false);
            tblMaintInfo.PerformLayout();
            flpMaintActions.ResumeLayout(false);
            flpMaintActions.PerformLayout();
            cardTestSettings.ResumeLayout(false);
            cardTestSettings.PerformLayout();
            tblTestCard.ResumeLayout(false);
            tblTestCard.PerformLayout();
            flpTestResultCol.ResumeLayout(false);
            flpTestResultCol.PerformLayout();
            cardAppSettings.ResumeLayout(false);
            cardAppSettings.PerformLayout();
            tblAppCard.ResumeLayout(false);
            tblAppCard.PerformLayout();
            ResumeLayout(false);

        }

        private SettingSystemInfo? settingSystemInfo;
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

        private System.Windows.Forms.Panel tblContent;

        private System.Windows.Forms.TableLayoutPanel tblLeftColumn;

        private System.Windows.Forms.Panel cardGeneral;
        private System.Windows.Forms.TableLayoutPanel tblTopRowContainer;
        private System.Windows.Forms.Panel cardMaintenanceState;
        private System.Windows.Forms.TableLayoutPanel tblMaintCard;
        private System.Windows.Forms.Label lblMaintTitle;
        private Controls.RoundedButton btnEditMaintenance;
        private Controls.RoundedButton btnResetMaintenance;
        private System.Windows.Forms.Label lblRemainingDays;
        private System.Windows.Forms.Label lblRemainingHours;
        private System.Windows.Forms.TextBox txtRemainingDays;
        private System.Windows.Forms.MaskedTextBox txtRemainingHours;
        private System.Windows.Forms.Panel pnlMaintInfo;
        private System.Windows.Forms.TableLayoutPanel tblMaintInfo;
        private System.Windows.Forms.Label lblOperatingTimeTodayTitle;
        private System.Windows.Forms.Label lblOperatingTimeTodayVal;
        private System.Windows.Forms.Label lblLastUsageTitle;
        private System.Windows.Forms.Label lblLastUsageVal;
        private System.Windows.Forms.FlowLayoutPanel flpMaintActions;
        private System.Windows.Forms.FlowLayoutPanel flpMaintHeaderActions;
        private System.Windows.Forms.Label lblEditHint;
        private Controls.RoundedButton btnCancelMaintenance;
        private Controls.RoundedButton btnUpdateMaintenance;


        private System.Windows.Forms.TableLayoutPanel tblGenCard;

        private System.Windows.Forms.Label lblGenTitle;
        private System.Windows.Forms.Label lblCompanyName;
        private System.Windows.Forms.TextBox txtCompanyName;
        private System.Windows.Forms.Label lblSysName;
        private System.Windows.Forms.TextBox txtSysName;

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
