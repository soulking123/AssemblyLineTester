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

            this.pnlTop = new Panel();

            this.logoBox = new PictureBox();

            this.lblTitle = new Label();

            this.lblDateRange = new Label();

            this.dtpStart = new DateTimePicker();

            this.lblDash = new Label();

            this.dtpEnd = new DateTimePicker();

            this.lblLogLevel = new Label();

            this.cmbLogLevel = new ComboBox();

            this.lblModule = new Label();

            this.cmbModule = new ComboBox();

            this.lblUser = new Label();

            this.cmbUser = new ComboBox();

            this.lblAction = new Label();

            this.cmbAction = new ComboBox();

            this.lblKeyword = new Label();

            this.txtKeyword = new TextBox();

            this.btnSearch = new Button();

            this.btnReset = new Button();

            this.mainLayout = new TableLayoutPanel();

            this.pnlLeft = new Panel();

            this.btnExport = new Button();

            this.btnRefresh = new Button();

            this.gridData = new DataGridView();

            this.lblTableTitle = new Label();

            this.rightLayout = new TableLayoutPanel();

            this.pnlSummary = new Panel();

            this.lblSummaryTitle = new Label();

            this.pnlLevelStats = new Panel();

            this.lblLevelStatsTitle = new Label();

            this.pnlModuleStats = new Panel();

            this.lblModuleStatsTitle = new Label();

            this.pnlLogDetail = new Panel();

            this.lblLogDetailTitle = new Label();

            this.lblLogDetailContent = new Label();

            this.pnlTop.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)logoBox).BeginInit();

            this.mainLayout.SuspendLayout();

            this.pnlLeft.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)gridData).BeginInit();

            this.rightLayout.SuspendLayout();

            this.pnlSummary.SuspendLayout();

            this.pnlLevelStats.SuspendLayout();

            this.pnlModuleStats.SuspendLayout();

            this.pnlLogDetail.SuspendLayout();

            this.SuspendLayout();

            // 

            // pnlTop

            // 

            this.pnlTop.BackColor = System.Drawing.Color.White;

            this.pnlTop.Controls.Add(logoBox);

            this.pnlTop.Controls.Add(lblTitle);

            this.pnlTop.Controls.Add(lblDateRange);

            this.pnlTop.Controls.Add(dtpStart);

            this.pnlTop.Controls.Add(lblDash);

            this.pnlTop.Controls.Add(dtpEnd);

            this.pnlTop.Controls.Add(lblLogLevel);

            this.pnlTop.Controls.Add(cmbLogLevel);

            this.pnlTop.Controls.Add(lblModule);

            this.pnlTop.Controls.Add(cmbModule);

            this.pnlTop.Controls.Add(lblUser);

            this.pnlTop.Controls.Add(cmbUser);

            this.pnlTop.Controls.Add(lblAction);

            this.pnlTop.Controls.Add(cmbAction);

            this.pnlTop.Controls.Add(lblKeyword);

            this.pnlTop.Controls.Add(txtKeyword);

            this.pnlTop.Controls.Add(btnSearch);

            this.pnlTop.Controls.Add(btnReset);

            this.pnlTop.Dock = DockStyle.Top;

            this.pnlTop.Location = new Point(0, 0);

            this.pnlTop.Name = "pnlTop";

            this.pnlTop.Size = new Size(2210, 212);

            this.pnlTop.TabIndex = 0;

            // 

            // logoBox

            // 

            this.logoBox.BackColor = System.Drawing.Color.LightGray;

            this.logoBox.Location = new Point(40, 20);

            this.logoBox.Name = "logoBox";

            this.logoBox.Size = new Size(45, 45);

            this.logoBox.TabIndex = 0;

            this.logoBox.TabStop = false;

            // 

            // lblTitle

            // 

            this.lblTitle.AutoSize = true;

            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);

            this.lblTitle.Location = new Point(100, 23);

            this.lblTitle.Name = "lblTitle";

            this.lblTitle.Size = new Size(285, 59);

            this.lblTitle.TabIndex = 1;

            this.lblTitle.Text = "SYSTEM LOG";

            // 

            // lblDateRange

            // 

            this.lblDateRange.AutoSize = true;

            this.lblDateRange.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);

            this.lblDateRange.Location = new Point(40, 96);

            this.lblDateRange.Name = "lblDateRange";

            this.lblDateRange.Size = new Size(146, 32);

            this.lblDateRange.TabIndex = 2;

            this.lblDateRange.Text = "Date Range";

            // 

            // dtpStart

            // 

            this.dtpStart.Format = DateTimePickerFormat.Short;

            this.dtpStart.Location = new Point(40, 138);

            this.dtpStart.Name = "dtpStart";

            this.dtpStart.Size = new Size(217, 39);

            this.dtpStart.TabIndex = 3;

            // 

            // lblDash

            // 

            this.lblDash.AutoSize = true;

            this.lblDash.Location = new Point(275, 137);

            this.lblDash.Name = "lblDash";

            this.lblDash.Size = new Size(24, 32);

            this.lblDash.TabIndex = 4;

            this.lblDash.Text = "-";

            // 

            // dtpEnd

            // 

            this.dtpEnd.Format = DateTimePickerFormat.Short;

            this.dtpEnd.Location = new Point(316, 137);

            this.dtpEnd.Name = "dtpEnd";

            this.dtpEnd.Size = new Size(228, 39);

            this.dtpEnd.TabIndex = 5;

            // 

            // lblLogLevel

            // 

            this.lblLogLevel.AutoSize = true;

            this.lblLogLevel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);

            this.lblLogLevel.Location = new Point(595, 96);

            this.lblLogLevel.Name = "lblLogLevel";

            this.lblLogLevel.Size = new Size(121, 32);

            this.lblLogLevel.TabIndex = 6;

            this.lblLogLevel.Text = "Log Level";

            // 

            // cmbLogLevel

            // 

            this.cmbLogLevel.FormattingEnabled = true;

            this.cmbLogLevel.Items.AddRange(new object[] { "All", "INFO", "WARNING", "ERROR" });

            this.cmbLogLevel.Location = new Point(595, 137);

            this.cmbLogLevel.Name = "cmbLogLevel";

            this.cmbLogLevel.Size = new Size(180, 40);

            this.cmbLogLevel.TabIndex = 7;

            this.cmbLogLevel.Text = "All";

            // 

            // lblModule

            // 

            this.lblModule.AutoSize = true;

            this.lblModule.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);

            this.lblModule.Location = new Point(815, 96);

            this.lblModule.Name = "lblModule";

            this.lblModule.Size = new Size(102, 32);

            this.lblModule.TabIndex = 8;

            this.lblModule.Text = "Module";

            // 

            // cmbModule

            // 

            this.cmbModule.FormattingEnabled = true;

            this.cmbModule.Items.AddRange(new object[] { "All", "Test Execution", "Communication" });

            this.cmbModule.Location = new Point(815, 137);

            this.cmbModule.Name = "cmbModule";

            this.cmbModule.Size = new Size(180, 40);

            this.cmbModule.TabIndex = 9;

            this.cmbModule.Text = "All";

            // 

            // lblUser

            // 

            this.lblUser.AutoSize = true;

            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);

            this.lblUser.Location = new Point(1035, 96);

            this.lblUser.Name = "lblUser";

            this.lblUser.Size = new Size(65, 32);

            this.lblUser.TabIndex = 10;

            this.lblUser.Text = "User";

            // 

            // cmbUser

            // 

            this.cmbUser.FormattingEnabled = true;

            this.cmbUser.Items.AddRange(new object[] { "All", "System", "Fally" });

            this.cmbUser.Location = new Point(1035, 136);

            this.cmbUser.Name = "cmbUser";

            this.cmbUser.Size = new Size(180, 40);

            this.cmbUser.TabIndex = 11;

            this.cmbUser.Text = "All";

            // 

            // lblAction

            // 

            this.lblAction.AutoSize = true;

            this.lblAction.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);

            this.lblAction.Location = new Point(1255, 96);

            this.lblAction.Name = "lblAction";

            this.lblAction.Size = new Size(89, 32);

            this.lblAction.TabIndex = 12;

            this.lblAction.Text = "Action";

            // 

            // cmbAction

            // 

            this.cmbAction.FormattingEnabled = true;

            this.cmbAction.Items.AddRange(new object[] { "All", "Start Test", "Load Program" });

            this.cmbAction.Location = new Point(1255, 137);

            this.cmbAction.Name = "cmbAction";

            this.cmbAction.Size = new Size(180, 40);

            this.cmbAction.TabIndex = 13;

            this.cmbAction.Text = "All";

            // 

            // lblKeyword

            // 

            this.lblKeyword.AutoSize = true;

            this.lblKeyword.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);

            this.lblKeyword.Location = new Point(1475, 96);

            this.lblKeyword.Name = "lblKeyword";

            this.lblKeyword.Size = new Size(115, 32);

            this.lblKeyword.TabIndex = 14;

            this.lblKeyword.Text = "Keyword";

            // 

            // txtKeyword

            // 

            this.txtKeyword.Location = new Point(1475, 136);

            this.txtKeyword.Name = "txtKeyword";

            this.txtKeyword.PlaceholderText = "Search log...";

            this.txtKeyword.Size = new Size(220, 39);

            this.txtKeyword.TabIndex = 15;

            // 

            // btnSearch

            // 

            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(255, 193, 7);

            this.btnSearch.FlatAppearance.BorderSize = 0;

            this.btnSearch.FlatStyle = FlatStyle.Flat;

            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);

            this.btnSearch.Location = new Point(1891, 125);

            this.btnSearch.Name = "btnSearch";

            this.btnSearch.Size = new Size(140, 50);

            this.btnSearch.TabIndex = 16;

            this.btnSearch.Text = "Search";

            this.btnSearch.UseVisualStyleBackColor = false;

            // 

            // btnReset

            // 

            this.btnReset.BackColor = System.Drawing.Color.White;

            this.btnReset.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;

            this.btnReset.FlatStyle = FlatStyle.Flat;

            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);

            this.btnReset.Location = new Point(2047, 125);

            this.btnReset.Name = "btnReset";

            this.btnReset.Size = new Size(120, 50);

            this.btnReset.TabIndex = 17;

            this.btnReset.Text = "Reset";

            this.btnReset.UseVisualStyleBackColor = false;

            // 

            // mainLayout

            // 

            this.mainLayout.ColumnCount = 2;

            this.mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));

            this.mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));

            this.mainLayout.Controls.Add(pnlLeft, 0, 0);

            this.mainLayout.Controls.Add(rightLayout, 1, 0);

            this.mainLayout.Dock = DockStyle.Fill;

            this.mainLayout.Location = new Point(0, 212);

            this.mainLayout.Name = "mainLayout";

            this.mainLayout.Padding = new Padding(32, 37, 32, 20);

            this.mainLayout.RowCount = 1;

            this.mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            this.mainLayout.Size = new Size(2210, 766);

            this.mainLayout.TabIndex = 1;

            // 

            // pnlLeft

            // 

            this.pnlLeft.BackColor = System.Drawing.Color.White;

            this.pnlLeft.Controls.Add(btnExport);

            this.pnlLeft.Controls.Add(btnRefresh);

            this.pnlLeft.Controls.Add(gridData);

            this.pnlLeft.Controls.Add(lblTableTitle);

            this.pnlLeft.Dock = DockStyle.Fill;

            this.pnlLeft.Location = new Point(43, 49);

            this.pnlLeft.Margin = new Padding(11, 12, 11, 12);

            this.pnlLeft.Name = "pnlLeft";

            this.pnlLeft.Size = new Size(1480, 685);

            this.pnlLeft.TabIndex = 0;

            // 

            // btnExport

            // 

            this.btnExport.BackColor = System.Drawing.Color.White;

            this.btnExport.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;

            this.btnExport.FlatStyle = FlatStyle.Flat;

            this.btnExport.Location = new Point(1313, 30);

            this.btnExport.Name = "btnExport";

            this.btnExport.Size = new Size(130, 45);

            this.btnExport.TabIndex = 1;

            this.btnExport.Text = "Export";

            this.btnExport.UseVisualStyleBackColor = false;

            // 

            // btnRefresh

            // 

            this.btnRefresh.BackColor = System.Drawing.Color.White;

            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;

            this.btnRefresh.FlatStyle = FlatStyle.Flat;

            this.btnRefresh.Location = new Point(1177, 30);

            this.btnRefresh.Name = "btnRefresh";

            this.btnRefresh.Size = new Size(130, 45);

            this.btnRefresh.TabIndex = 0;

            this.btnRefresh.Text = "Refresh";

            this.btnRefresh.UseVisualStyleBackColor = false;

            // 

            // gridData

            // 

            this.gridData.AllowUserToAddRows = false;

            this.gridData.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            this.gridData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.gridData.BackgroundColor = System.Drawing.Color.White;

            this.gridData.BorderStyle = BorderStyle.None;

            this.gridData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            this.gridData.Location = new Point(40, 100);

            this.gridData.Name = "gridData";

            this.gridData.RowHeadersVisible = false;

            this.gridData.RowHeadersWidth = 82;

            this.gridData.Size = new Size(1400, 545);

            this.gridData.TabIndex = 1;

            // 

            // lblTableTitle

            // 

            this.lblTableTitle.AutoSize = true;

            this.lblTableTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);

            this.lblTableTitle.Location = new Point(40, 30);

            this.lblTableTitle.Name = "lblTableTitle";

            this.lblTableTitle.Size = new Size(360, 45);

            this.lblTableTitle.TabIndex = 2;

            this.lblTableTitle.Text = "LOG LIST (Total: 1,258)";

            // 

            // rightLayout

            // 

            this.rightLayout.ColumnCount = 1;

            this.rightLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            this.rightLayout.Controls.Add(pnlSummary, 0, 0);

            this.rightLayout.Controls.Add(pnlLevelStats, 0, 1);

            this.rightLayout.Controls.Add(pnlModuleStats, 0, 2);

            this.rightLayout.Dock = DockStyle.Fill;

            this.rightLayout.Location = new Point(1545, 49);

            this.rightLayout.Margin = new Padding(11, 12, 11, 12);

            this.rightLayout.Name = "rightLayout";

            this.rightLayout.RowCount = 3;

            this.rightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));

            this.rightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));

            this.rightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 34F));

            this.rightLayout.Size = new Size(622, 685);

            this.rightLayout.TabIndex = 1;

            // 

            // pnlSummary

            // 

            this.pnlSummary.BackColor = System.Drawing.Color.White;

            this.pnlSummary.Controls.Add(lblSummaryTitle);

            this.pnlSummary.Dock = DockStyle.Fill;

            this.pnlSummary.Location = new Point(0, 0);

            this.pnlSummary.Margin = new Padding(0, 0, 0, 25);

            this.pnlSummary.Name = "pnlSummary";

            this.pnlSummary.Size = new Size(622, 201);

            this.pnlSummary.TabIndex = 0;

            // 

            // lblSummaryTitle

            // 

            this.lblSummaryTitle.AutoSize = true;

            this.lblSummaryTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);

            this.lblSummaryTitle.Location = new Point(20, 20);

            this.lblSummaryTitle.Name = "lblSummaryTitle";

            this.lblSummaryTitle.Size = new Size(216, 37);

            this.lblSummaryTitle.TabIndex = 0;

            this.lblSummaryTitle.Text = "LOG SUMMARY";

            // 

            // pnlLevelStats

            // 

            this.pnlLevelStats.BackColor = System.Drawing.Color.White;

            this.pnlLevelStats.Controls.Add(lblLevelStatsTitle);

            this.pnlLevelStats.Dock = DockStyle.Fill;

            this.pnlLevelStats.Location = new Point(0, 226);

            this.pnlLevelStats.Margin = new Padding(0, 0, 0, 25);

            this.pnlLevelStats.Name = "pnlLevelStats";

            this.pnlLevelStats.Size = new Size(622, 201);

            this.pnlLevelStats.TabIndex = 1;

            // 

            // lblLevelStatsTitle

            // 

            this.lblLevelStatsTitle.AutoSize = true;

            this.lblLevelStatsTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);

            this.lblLevelStatsTitle.Location = new Point(20, 20);

            this.lblLevelStatsTitle.Name = "lblLevelStatsTitle";

            this.lblLevelStatsTitle.Size = new Size(150, 37);

            this.lblLevelStatsTitle.TabIndex = 0;

            this.lblLevelStatsTitle.Text = "LOG LEVEL";

            // 

            // pnlModuleStats

            // 

            this.pnlModuleStats.BackColor = System.Drawing.Color.White;

            this.pnlModuleStats.Controls.Add(lblModuleStatsTitle);

            this.pnlModuleStats.Dock = DockStyle.Fill;

            this.pnlModuleStats.Location = new Point(3, 455);

            this.pnlModuleStats.Name = "pnlModuleStats";

            this.pnlModuleStats.Size = new Size(616, 227);

            this.pnlModuleStats.TabIndex = 2;

            // 

            // lblModuleStatsTitle

            // 

            this.lblModuleStatsTitle.AutoSize = true;

            this.lblModuleStatsTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);

            this.lblModuleStatsTitle.Location = new Point(20, 20);

            this.lblModuleStatsTitle.Name = "lblModuleStatsTitle";

            this.lblModuleStatsTitle.Size = new Size(131, 37);

            this.lblModuleStatsTitle.TabIndex = 0;

            this.lblModuleStatsTitle.Text = "MODULE";

            // 

            // pnlLogDetail

            // 

            this.pnlLogDetail.BackColor = System.Drawing.Color.White;

            this.pnlLogDetail.Controls.Add(lblLogDetailTitle);

            this.pnlLogDetail.Controls.Add(lblLogDetailContent);

            this.pnlLogDetail.Dock = DockStyle.Bottom;

            this.pnlLogDetail.Location = new Point(0, 978);

            this.pnlLogDetail.Name = "pnlLogDetail";

            this.pnlLogDetail.Size = new Size(2210, 180);

            this.pnlLogDetail.TabIndex = 2;

            // 

            // lblLogDetailTitle

            // 

            this.lblLogDetailTitle.AutoSize = true;

            this.lblLogDetailTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);

            this.lblLogDetailTitle.Location = new Point(40, 20);

            this.lblLogDetailTitle.Name = "lblLogDetailTitle";

            this.lblLogDetailTitle.Size = new Size(166, 37);

            this.lblLogDetailTitle.TabIndex = 0;

            this.lblLogDetailTitle.Text = "LOG DETAIL";

            // 

            // lblLogDetailContent

            // 

            this.lblLogDetailContent.AutoSize = true;

            this.lblLogDetailContent.Location = new Point(40, 75);

            this.lblLogDetailContent.Name = "lblLogDetailContent";

            this.lblLogDetailContent.Size = new Size(1938, 32);

            this.lblLogDetailContent.TabIndex = 1;

            this.lblLogDetailContent.Text = "Date & Time: 15 Mar 2026 10:30:20 AM   |   Log Level: INFO   |   Module: Test Execution   |   User: Fally   |   Action: Start Test   |   Description: Test started for Part Number WH-ENG-001";

            // 

            // SystemLog

            // 

            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);

            this.AutoScaleMode = AutoScaleMode.Font;

            this.BackColor = System.Drawing.Color.FromArgb(244, 245, 247);

            this.Controls.Add(mainLayout);

            this.Controls.Add(pnlLogDetail);

            this.Controls.Add(pnlTop);

            this.Margin = new Padding(6, 7, 6, 7);

            this.Name = "SystemLog";

            this.Size = new Size(2210, 1158);

            this.pnlTop.ResumeLayout(false);

            this.pnlTop.PerformLayout();

            ((System.ComponentModel.ISupportInitialize)logoBox).EndInit();

            this.mainLayout.ResumeLayout(false);

            this.pnlLeft.ResumeLayout(false);

            this.pnlLeft.PerformLayout();

            ((System.ComponentModel.ISupportInitialize)gridData).EndInit();

            this.rightLayout.ResumeLayout(false);

            this.pnlSummary.ResumeLayout(false);

            this.pnlSummary.PerformLayout();

            this.pnlLevelStats.ResumeLayout(false);

            this.pnlLevelStats.PerformLayout();

            this.pnlModuleStats.ResumeLayout(false);

            this.pnlModuleStats.PerformLayout();

            this.pnlLogDetail.ResumeLayout(false);

            this.pnlLogDetail.PerformLayout();

            this.ResumeLayout(false);

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