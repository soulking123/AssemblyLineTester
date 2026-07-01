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
            rightLayout = new TableLayoutPanel();
            pnlSummary = new Panel();
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
            mainLayout.SuspendLayout();
            pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridData).BeginInit();
            rightLayout.SuspendLayout();
            pnlSummary.SuspendLayout();
            pnlLevelStats.SuspendLayout();
            pnlModuleStats.SuspendLayout();
            pnlLogDetail.SuspendLayout();
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
            pnlTop.Controls.Add(lblLogLevel);
            pnlTop.Controls.Add(cmbLogLevel);
            pnlTop.Controls.Add(lblModule);
            pnlTop.Controls.Add(cmbModule);
            pnlTop.Controls.Add(lblUser);
            pnlTop.Controls.Add(cmbUser);
            pnlTop.Controls.Add(lblAction);
            pnlTop.Controls.Add(cmbAction);
            pnlTop.Controls.Add(lblKeyword);
            pnlTop.Controls.Add(txtKeyword);
            pnlTop.Controls.Add(btnSearch);
            pnlTop.Controls.Add(btnReset);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(2210, 212);
            pnlTop.TabIndex = 0;
            // 
            // logoBox
            // 
            logoBox.BackColor = Color.LightGray;
            logoBox.Location = new Point(40, 20);
            logoBox.Name = "logoBox";
            logoBox.Size = new Size(45, 45);
            logoBox.TabIndex = 0;
            logoBox.TabStop = false;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(100, 23);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(285, 59);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "SYSTEM LOG";
            // 
            // lblDateRange
            // 
            lblDateRange.AutoSize = true;
            lblDateRange.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDateRange.Location = new Point(40, 96);
            lblDateRange.Name = "lblDateRange";
            lblDateRange.Size = new Size(146, 32);
            lblDateRange.TabIndex = 2;
            lblDateRange.Text = "Date Range";
            // 
            // dtpStart
            // 
            dtpStart.Format = DateTimePickerFormat.Short;
            dtpStart.Location = new Point(40, 138);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(217, 39);
            dtpStart.TabIndex = 3;
            // 
            // lblDash
            // 
            lblDash.AutoSize = true;
            lblDash.Location = new Point(275, 137);
            lblDash.Name = "lblDash";
            lblDash.Size = new Size(24, 32);
            lblDash.TabIndex = 4;
            lblDash.Text = "-";
            // 
            // dtpEnd
            // 
            dtpEnd.Format = DateTimePickerFormat.Short;
            dtpEnd.Location = new Point(316, 137);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(228, 39);
            dtpEnd.TabIndex = 5;
            // 
            // lblLogLevel
            // 
            lblLogLevel.AutoSize = true;
            lblLogLevel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblLogLevel.Location = new Point(595, 96);
            lblLogLevel.Name = "lblLogLevel";
            lblLogLevel.Size = new Size(121, 32);
            lblLogLevel.TabIndex = 6;
            lblLogLevel.Text = "Log Level";
            // 
            // cmbLogLevel
            // 
            cmbLogLevel.FormattingEnabled = true;
            cmbLogLevel.Items.AddRange(new object[] { "All", "INFO", "WARNING", "ERROR" });
            cmbLogLevel.Location = new Point(595, 137);
            cmbLogLevel.Name = "cmbLogLevel";
            cmbLogLevel.Size = new Size(180, 40);
            cmbLogLevel.TabIndex = 7;
            cmbLogLevel.Text = "All";
            // 
            // lblModule
            // 
            lblModule.AutoSize = true;
            lblModule.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblModule.Location = new Point(815, 96);
            lblModule.Name = "lblModule";
            lblModule.Size = new Size(102, 32);
            lblModule.TabIndex = 8;
            lblModule.Text = "Module";
            // 
            // cmbModule
            // 
            cmbModule.FormattingEnabled = true;
            cmbModule.Items.AddRange(new object[] { "All", "Test Execution", "Communication" });
            cmbModule.Location = new Point(815, 137);
            cmbModule.Name = "cmbModule";
            cmbModule.Size = new Size(180, 40);
            cmbModule.TabIndex = 9;
            cmbModule.Text = "All";
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblUser.Location = new Point(1035, 96);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(65, 32);
            lblUser.TabIndex = 10;
            lblUser.Text = "User";
            // 
            // cmbUser
            // 
            cmbUser.FormattingEnabled = true;
            cmbUser.Items.AddRange(new object[] { "All", "System", "Fally" });
            cmbUser.Location = new Point(1035, 136);
            cmbUser.Name = "cmbUser";
            cmbUser.Size = new Size(180, 40);
            cmbUser.TabIndex = 11;
            cmbUser.Text = "All";
            // 
            // lblAction
            // 
            lblAction.AutoSize = true;
            lblAction.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAction.Location = new Point(1255, 96);
            lblAction.Name = "lblAction";
            lblAction.Size = new Size(89, 32);
            lblAction.TabIndex = 12;
            lblAction.Text = "Action";
            // 
            // cmbAction
            // 
            cmbAction.FormattingEnabled = true;
            cmbAction.Items.AddRange(new object[] { "All", "Start Test", "Load Program" });
            cmbAction.Location = new Point(1255, 137);
            cmbAction.Name = "cmbAction";
            cmbAction.Size = new Size(180, 40);
            cmbAction.TabIndex = 13;
            cmbAction.Text = "All";
            // 
            // lblKeyword
            // 
            lblKeyword.AutoSize = true;
            lblKeyword.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblKeyword.Location = new Point(1475, 96);
            lblKeyword.Name = "lblKeyword";
            lblKeyword.Size = new Size(115, 32);
            lblKeyword.TabIndex = 14;
            lblKeyword.Text = "Keyword";
            // 
            // txtKeyword
            // 
            txtKeyword.Location = new Point(1475, 136);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderText = "Search log...";
            txtKeyword.Size = new Size(220, 39);
            txtKeyword.TabIndex = 15;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(255, 193, 7);
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSearch.Location = new Point(1891, 125);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(140, 50);
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
            btnReset.Location = new Point(2047, 125);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(120, 50);
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
            mainLayout.Location = new Point(0, 212);
            mainLayout.Name = "mainLayout";
            mainLayout.Padding = new Padding(32, 37, 32, 20);
            mainLayout.RowCount = 1;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.Size = new Size(2210, 766);
            mainLayout.TabIndex = 1;
            // 
            // pnlLeft
            // 
            pnlLeft.BackColor = Color.White;
            pnlLeft.Controls.Add(btnExport);
            pnlLeft.Controls.Add(btnRefresh);
            pnlLeft.Controls.Add(gridData);
            pnlLeft.Controls.Add(lblTableTitle);
            pnlLeft.Dock = DockStyle.Fill;
            pnlLeft.Location = new Point(43, 49);
            pnlLeft.Margin = new Padding(11, 12, 11, 12);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Size = new Size(1480, 685);
            pnlLeft.TabIndex = 0;
            // 
            // btnExport
            // 
            btnExport.BackColor = Color.White;
            btnExport.FlatAppearance.BorderColor = Color.LightGray;
            btnExport.FlatStyle = FlatStyle.Flat;
            btnExport.Location = new Point(1313, 30);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(130, 45);
            btnExport.TabIndex = 1;
            btnExport.Text = "Export";
            btnExport.UseVisualStyleBackColor = false;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.White;
            btnRefresh.FlatAppearance.BorderColor = Color.LightGray;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Location = new Point(1177, 30);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(130, 45);
            btnRefresh.TabIndex = 0;
            btnRefresh.Text = "Refresh";
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
            gridData.Location = new Point(40, 100);
            gridData.Name = "gridData";
            gridData.RowHeadersVisible = false;
            gridData.RowHeadersWidth = 82;
            gridData.Size = new Size(1400, 545);
            gridData.TabIndex = 1;
            // 
            // lblTableTitle
            // 
            lblTableTitle.AutoSize = true;
            lblTableTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTableTitle.Location = new Point(40, 30);
            lblTableTitle.Name = "lblTableTitle";
            lblTableTitle.Size = new Size(360, 45);
            lblTableTitle.TabIndex = 2;
            lblTableTitle.Text = "LOG LIST (Total: 1,258)";
            // 
            // rightLayout
            // 
            rightLayout.ColumnCount = 1;
            rightLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rightLayout.Controls.Add(pnlSummary, 0, 0);
            rightLayout.Controls.Add(pnlLevelStats, 0, 1);
            rightLayout.Controls.Add(pnlModuleStats, 0, 2);
            rightLayout.Dock = DockStyle.Fill;
            rightLayout.Location = new Point(1545, 49);
            rightLayout.Margin = new Padding(11, 12, 11, 12);
            rightLayout.Name = "rightLayout";
            rightLayout.RowCount = 3;
            rightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
            rightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
            rightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 34F));
            rightLayout.Size = new Size(622, 685);
            rightLayout.TabIndex = 1;
            // 
            // pnlSummary
            // 
            pnlSummary.BackColor = Color.White;
            pnlSummary.Controls.Add(lblSummaryTitle);
            pnlSummary.Dock = DockStyle.Fill;
            pnlSummary.Location = new Point(0, 0);
            pnlSummary.Margin = new Padding(0, 0, 0, 25);
            pnlSummary.Name = "pnlSummary";
            pnlSummary.Size = new Size(622, 201);
            pnlSummary.TabIndex = 0;
            // 
            // lblSummaryTitle
            // 
            lblSummaryTitle.AutoSize = true;
            lblSummaryTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSummaryTitle.Location = new Point(20, 20);
            lblSummaryTitle.Name = "lblSummaryTitle";
            lblSummaryTitle.Size = new Size(216, 37);
            lblSummaryTitle.TabIndex = 0;
            lblSummaryTitle.Text = "LOG SUMMARY";
            // 
            // pnlLevelStats
            // 
            pnlLevelStats.BackColor = Color.White;
            pnlLevelStats.Controls.Add(lblLevelStatsTitle);
            pnlLevelStats.Dock = DockStyle.Fill;
            pnlLevelStats.Location = new Point(0, 226);
            pnlLevelStats.Margin = new Padding(0, 0, 0, 25);
            pnlLevelStats.Name = "pnlLevelStats";
            pnlLevelStats.Size = new Size(622, 201);
            pnlLevelStats.TabIndex = 1;
            // 
            // lblLevelStatsTitle
            // 
            lblLevelStatsTitle.AutoSize = true;
            lblLevelStatsTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblLevelStatsTitle.Location = new Point(20, 20);
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
            pnlModuleStats.Location = new Point(3, 455);
            pnlModuleStats.Name = "pnlModuleStats";
            pnlModuleStats.Size = new Size(616, 227);
            pnlModuleStats.TabIndex = 2;
            // 
            // lblModuleStatsTitle
            // 
            lblModuleStatsTitle.AutoSize = true;
            lblModuleStatsTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblModuleStatsTitle.Location = new Point(20, 20);
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
            pnlLogDetail.Location = new Point(0, 978);
            pnlLogDetail.Name = "pnlLogDetail";
            pnlLogDetail.Size = new Size(2210, 180);
            pnlLogDetail.TabIndex = 2;
            // 
            // lblLogDetailTitle
            // 
            lblLogDetailTitle.AutoSize = true;
            lblLogDetailTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblLogDetailTitle.Location = new Point(40, 20);
            lblLogDetailTitle.Name = "lblLogDetailTitle";
            lblLogDetailTitle.Size = new Size(166, 37);
            lblLogDetailTitle.TabIndex = 0;
            lblLogDetailTitle.Text = "LOG DETAIL";
            // 
            // lblLogDetailContent
            // 
            lblLogDetailContent.AutoSize = true;
            lblLogDetailContent.Location = new Point(40, 75);
            lblLogDetailContent.Name = "lblLogDetailContent";
            lblLogDetailContent.Size = new Size(1938, 32);
            lblLogDetailContent.TabIndex = 1;
            lblLogDetailContent.Text = "Date & Time: 15 Mar 2026 10:30:20 AM   |   Log Level: INFO   |   Module: Test Execution   |   User: Fally   |   Action: Start Test   |   Description: Test started for Part Number WH-ENG-001";
            // 
            // SystemLog
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 245, 247);
            Controls.Add(mainLayout);
            Controls.Add(pnlLogDetail);
            Controls.Add(pnlTop);
            Margin = new Padding(6, 7, 6, 7);
            Name = "SystemLog";
            Size = new Size(2210, 1158);
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)logoBox).EndInit();
            mainLayout.ResumeLayout(false);
            pnlLeft.ResumeLayout(false);
            pnlLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridData).EndInit();
            rightLayout.ResumeLayout(false);
            pnlSummary.ResumeLayout(false);
            pnlSummary.PerformLayout();
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

        // Right Side Components
        private System.Windows.Forms.TableLayoutPanel rightLayout;
        private System.Windows.Forms.Panel pnlSummary;
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