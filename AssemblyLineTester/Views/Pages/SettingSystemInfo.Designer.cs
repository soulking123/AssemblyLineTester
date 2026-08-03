namespace AssemblyLineTester.Views.Pages
{
    partial class SettingSystemInfo
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
            if (disposing && (components != null))
            {
                components.Dispose();
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
            // tblRightColumn
            // 
            tblRightColumn.AutoSize = true;
            tblRightColumn.ColumnCount = 3;
            tblRightColumn.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tblRightColumn.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tblRightColumn.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tblRightColumn.Controls.Add(cardSystemStatus, 0, 0);
            tblRightColumn.Controls.Add(cardLicenseInfo, 1, 0);
            tblRightColumn.Controls.Add(cardSystemTime, 2, 0);
            tblRightColumn.Dock = DockStyle.Fill;
            tblRightColumn.Location = new Point(0, 0);
            tblRightColumn.Margin = new Padding(0);
            tblRightColumn.Name = "tblRightColumn";
            tblRightColumn.RowCount = 2;
            tblRightColumn.RowStyles.Add(new RowStyle(SizeType.Percent, 40.47809F));
            tblRightColumn.RowStyles.Add(new RowStyle(SizeType.Percent, 59.52191F));
            tblRightColumn.Size = new Size(1910, 1255);
            tblRightColumn.TabIndex = 1;
            // 
            // cardSystemStatus
            // 
            cardSystemStatus.AutoSize = true;
            cardSystemStatus.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            cardSystemStatus.BackColor = Color.White;
            cardSystemStatus.Controls.Add(tblStatusCard);
            cardSystemStatus.Dock = DockStyle.Fill;
            cardSystemStatus.Location = new Point(20, 20);
            cardSystemStatus.Margin = new Padding(20, 20, 10, 20);
            cardSystemStatus.Name = "cardSystemStatus";
            cardSystemStatus.Padding = new Padding(11, 0, 11, 0);
            cardSystemStatus.Size = new Size(606, 468);
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
            tblStatusCard.Size = new Size(584, 444);
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
            lblStatusTitle.Size = new Size(568, 82);
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
            tblStatusGrid.Size = new Size(584, 362);
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
            lblStKeyApp.Size = new Size(276, 60);
            lblStKeyApp.TabIndex = 0;
            lblStKeyApp.Text = "Application Status";
            lblStKeyApp.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblStValApp
            // 
            lblStValApp.Dock = DockStyle.Fill;
            lblStValApp.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblStValApp.ForeColor = Color.FromArgb(22, 163, 74);
            lblStValApp.Location = new Point(300, 0);
            lblStValApp.Margin = new Padding(8, 0, 8, 0);
            lblStValApp.Name = "lblStValApp";
            lblStValApp.Size = new Size(276, 60);
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
            lblStKeyDb.Size = new Size(276, 60);
            lblStKeyDb.TabIndex = 2;
            lblStKeyDb.Text = "Database Connection";
            lblStKeyDb.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblStValDb
            // 
            lblStValDb.Dock = DockStyle.Fill;
            lblStValDb.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblStValDb.ForeColor = Color.FromArgb(22, 163, 74);
            lblStValDb.Location = new Point(300, 60);
            lblStValDb.Margin = new Padding(8, 0, 8, 0);
            lblStValDb.Name = "lblStValDb";
            lblStValDb.Size = new Size(276, 60);
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
            lblStKeyFile.Size = new Size(276, 60);
            lblStKeyFile.TabIndex = 4;
            lblStKeyFile.Text = "File Storage";
            lblStKeyFile.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblStValFile
            // 
            lblStValFile.Dock = DockStyle.Fill;
            lblStValFile.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblStValFile.ForeColor = Color.FromArgb(22, 163, 74);
            lblStValFile.Location = new Point(300, 120);
            lblStValFile.Margin = new Padding(8, 0, 8, 0);
            lblStValFile.Name = "lblStValFile";
            lblStValFile.Size = new Size(276, 60);
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
            lblStKeyLic.Size = new Size(276, 60);
            lblStKeyLic.TabIndex = 6;
            lblStKeyLic.Text = "License Status";
            lblStKeyLic.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblStValLic
            // 
            lblStValLic.Dock = DockStyle.Fill;
            lblStValLic.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblStValLic.ForeColor = Color.FromArgb(22, 163, 74);
            lblStValLic.Location = new Point(300, 180);
            lblStValLic.Margin = new Padding(8, 0, 8, 0);
            lblStValLic.Name = "lblStValLic";
            lblStValLic.Size = new Size(276, 60);
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
            lblStKeyStart.Size = new Size(276, 60);
            lblStKeyStart.TabIndex = 8;
            lblStKeyStart.Text = "Last System Start";
            lblStKeyStart.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblStValStart
            // 
            lblStValStart.Dock = DockStyle.Fill;
            lblStValStart.Font = new Font("Segoe UI", 9.5F);
            lblStValStart.ForeColor = Color.FromArgb(71, 85, 105);
            lblStValStart.Location = new Point(300, 240);
            lblStValStart.Margin = new Padding(8, 0, 8, 0);
            lblStValStart.Name = "lblStValStart";
            lblStValStart.Size = new Size(276, 60);
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
            lblStKeyUptime.Size = new Size(276, 62);
            lblStKeyUptime.TabIndex = 10;
            lblStKeyUptime.Text = "Uptime";
            lblStKeyUptime.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblStValUptime
            // 
            lblStValUptime.Dock = DockStyle.Fill;
            lblStValUptime.Font = new Font("Segoe UI", 9.5F);
            lblStValUptime.ForeColor = Color.FromArgb(71, 85, 105);
            lblStValUptime.Location = new Point(300, 300);
            lblStValUptime.Margin = new Padding(8, 0, 8, 0);
            lblStValUptime.Name = "lblStValUptime";
            lblStValUptime.Size = new Size(276, 62);
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
            cardLicenseInfo.Location = new Point(646, 20);
            cardLicenseInfo.Margin = new Padding(10, 20, 10, 20);
            cardLicenseInfo.Name = "cardLicenseInfo";
            cardLicenseInfo.Padding = new Padding(11, 0, 11, 0);
            cardLicenseInfo.Size = new Size(616, 468);
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
            tblLicenseCard.Size = new Size(594, 419);
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
            lblLicenseTitle.Size = new Size(578, 82);
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
            tblLicenseGrid.Size = new Size(578, 248);
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
            lblLicKeyType.Size = new Size(244, 49);
            lblLicKeyType.TabIndex = 0;
            lblLicKeyType.Text = "License Type";
            lblLicKeyType.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblLicValType
            // 
            lblLicValType.Dock = DockStyle.Fill;
            lblLicValType.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblLicValType.ForeColor = Color.FromArgb(16, 185, 129);
            lblLicValType.Location = new Point(268, 0);
            lblLicValType.Margin = new Padding(8, 0, 8, 0);
            lblLicValType.Name = "lblLicValType";
            lblLicValType.Size = new Size(302, 49);
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
            lblLicKeyKey.Size = new Size(244, 49);
            lblLicKeyKey.TabIndex = 2;
            lblLicKeyKey.Text = "License Key";
            lblLicKeyKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblLicValKey
            // 
            lblLicValKey.Dock = DockStyle.Fill;
            lblLicValKey.Font = new Font("Segoe UI", 9.5F);
            lblLicValKey.ForeColor = Color.FromArgb(71, 85, 105);
            lblLicValKey.Location = new Point(268, 49);
            lblLicValKey.Margin = new Padding(8, 0, 8, 0);
            lblLicValKey.Name = "lblLicValKey";
            lblLicValKey.Size = new Size(302, 49);
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
            lblLicKeyFrom.Size = new Size(244, 49);
            lblLicKeyFrom.TabIndex = 4;
            lblLicKeyFrom.Text = "Valid From";
            lblLicKeyFrom.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblLicValFrom
            // 
            lblLicValFrom.Dock = DockStyle.Fill;
            lblLicValFrom.Font = new Font("Segoe UI", 9.5F);
            lblLicValFrom.ForeColor = Color.FromArgb(71, 85, 105);
            lblLicValFrom.Location = new Point(268, 98);
            lblLicValFrom.Margin = new Padding(8, 0, 8, 0);
            lblLicValFrom.Name = "lblLicValFrom";
            lblLicValFrom.Size = new Size(302, 49);
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
            lblLicKeyTo.Size = new Size(244, 49);
            lblLicKeyTo.TabIndex = 6;
            lblLicKeyTo.Text = "Valid To";
            lblLicKeyTo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblLicValTo
            // 
            lblLicValTo.Dock = DockStyle.Fill;
            lblLicValTo.Font = new Font("Segoe UI", 9.5F);
            lblLicValTo.ForeColor = Color.FromArgb(71, 85, 105);
            lblLicValTo.Location = new Point(268, 147);
            lblLicValTo.Margin = new Padding(8, 0, 8, 0);
            lblLicValTo.Name = "lblLicValTo";
            lblLicValTo.Size = new Size(302, 49);
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
            lblLicKeyToOrg.Size = new Size(244, 52);
            lblLicKeyToOrg.TabIndex = 8;
            lblLicKeyToOrg.Text = "Licensed To";
            lblLicKeyToOrg.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblLicValToOrg
            // 
            lblLicValToOrg.Dock = DockStyle.Fill;
            lblLicValToOrg.Font = new Font("Segoe UI", 9.5F);
            lblLicValToOrg.ForeColor = Color.FromArgb(71, 85, 105);
            lblLicValToOrg.Location = new Point(268, 196);
            lblLicValToOrg.Margin = new Padding(8, 0, 8, 0);
            lblLicValToOrg.Name = "lblLicValToOrg";
            lblLicValToOrg.Size = new Size(302, 52);
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
            btnManageLicense.Location = new Point(33, 354);
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
            cardSystemTime.Location = new Point(1282, 20);
            cardSystemTime.Margin = new Padding(10, 20, 20, 20);
            cardSystemTime.Name = "cardSystemTime";
            cardSystemTime.Padding = new Padding(11, 0, 11, 0);
            cardSystemTime.Size = new Size(608, 468);
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
            tblTimeCard.RowStyles.Add(new RowStyle(SizeType.Absolute, 82F));
            tblTimeCard.RowStyles.Add(new RowStyle());
            tblTimeCard.RowStyles.Add(new RowStyle());
            tblTimeCard.Size = new Size(586, 357);
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
            lblTimeTitle.Size = new Size(570, 82);
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
            tblTimeGrid.Location = new Point(0, 82);
            tblTimeGrid.Margin = new Padding(0);
            tblTimeGrid.Name = "tblTimeGrid";
            tblTimeGrid.RowCount = 3;
            tblTimeGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            tblTimeGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            tblTimeGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            tblTimeGrid.Size = new Size(586, 180);
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
            lblTimeKeyDate.Size = new Size(206, 60);
            lblTimeKeyDate.TabIndex = 0;
            lblTimeKeyDate.Text = "Current Date";
            lblTimeKeyDate.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTimeValDate
            // 
            lblTimeValDate.Dock = DockStyle.Fill;
            lblTimeValDate.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblTimeValDate.ForeColor = Color.FromArgb(30, 41, 59);
            lblTimeValDate.Location = new Point(230, 0);
            lblTimeValDate.Margin = new Padding(8, 0, 8, 0);
            lblTimeValDate.Name = "lblTimeValDate";
            lblTimeValDate.Size = new Size(348, 60);
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
            lblTimeKeyTime.Size = new Size(206, 60);
            lblTimeKeyTime.TabIndex = 2;
            lblTimeKeyTime.Text = "Current Time";
            lblTimeKeyTime.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTimeValTime
            // 
            lblTimeValTime.Dock = DockStyle.Fill;
            lblTimeValTime.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblTimeValTime.ForeColor = Color.FromArgb(37, 99, 235);
            lblTimeValTime.Location = new Point(230, 60);
            lblTimeValTime.Margin = new Padding(8, 0, 8, 0);
            lblTimeValTime.Name = "lblTimeValTime";
            lblTimeValTime.Size = new Size(348, 60);
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
            lblTimeKeyZone.Size = new Size(206, 60);
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
            cmbTimeZone.Location = new Point(230, 128);
            cmbTimeZone.Margin = new Padding(8);
            cmbTimeZone.Name = "cmbTimeZone";
            cmbTimeZone.Size = new Size(348, 43);
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
            btnSyncTime.Location = new Point(34, 277);
            btnSyncTime.Margin = new Padding(8, 15, 8, 15);
            btnSyncTime.Name = "btnSyncTime";
            btnSyncTime.Size = new Size(518, 65);
            btnSyncTime.TabIndex = 6;
            btnSyncTime.Text = "Sync with System Time";
            btnSyncTime.UseVisualStyleBackColor = false;
            // 
            // SettingSystemInfo
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tblRightColumn);
            Name = "SettingSystemInfo";
            Size = new Size(1910, 1255);
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
            PerformLayout();

        }

        private System.Windows.Forms.TableLayoutPanel tblRightColumn;

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
        #endregion
    }
}
