namespace AssemblyLineTester.Views.Pages
{
    partial class TestExecution
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
            if (disposing)
            {
                components?.Dispose();
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
            tblMain = new TableLayoutPanel();
            pnlTopBar = new Panel();
            tblTopWrapper = new TableLayoutPanel();
            lblPageHeader = new Label();
            tblActionButtons = new TableLayoutPanel();
            btnLoadProgram = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnStartTest = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnStopTest = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnSaveReport = new AssemblyLineTester.Views.Controls.RoundedButton();
            tblRow1 = new TableLayoutPanel();
            cardMachineOverview = new Panel();
            picMachineOverview = new PictureBox();
            lblMachineOverviewTitle = new Label();
            cardHarnessConn = new Panel();
            pnlHarnessImgContainer = new Panel();
            btnViewPinMap = new AssemblyLineTester.Views.Controls.RoundedButton();
            picHarnessConn = new PictureBox();
            lblHarnessConnTitle = new Label();
            cardAreaDetail = new Panel();
            picAreaDetail = new PictureBox();
            lblAreaDetailTitle = new Label();
            cardTestStatus = new Panel();
            tblStatusLabels = new TableLayoutPanel();
            lblStartTimeKey = new Label();
            lblStartTimeVal = new Label();
            lblElapsedKey = new Label();
            lblElapsedTimeVal = new Label();
            lblEndTimeKey = new Label();
            lblEndTimeVal = new Label();
            lblStatusRunning = new Label();
            lblTestStatusTitle = new Label();
            tblRow2 = new TableLayoutPanel();
            cardLiveDetails = new Panel();
            gridLiveDetails = new DataGridView();
            pnlLiveDetailsHeader = new Panel();
            btnViewWiringDiagram = new AssemblyLineTester.Views.Controls.RoundedButton();
            lblLiveDetailsTitle = new Label();
            cardTestSummary = new Panel();
            listBoxTestSummary = new ListBox();
            lblTestSummaryTitle = new Label();
            cardNotes = new Panel();
            txtNotes = new TextBox();
            lblNotesTitle = new Label();
            tblMain.SuspendLayout();
            pnlTopBar.SuspendLayout();
            tblTopWrapper.SuspendLayout();
            tblActionButtons.SuspendLayout();
            tblRow1.SuspendLayout();
            cardMachineOverview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picMachineOverview).BeginInit();
            cardHarnessConn.SuspendLayout();
            pnlHarnessImgContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picHarnessConn).BeginInit();
            cardAreaDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picAreaDetail).BeginInit();
            cardTestStatus.SuspendLayout();
            tblStatusLabels.SuspendLayout();
            tblRow2.SuspendLayout();
            cardLiveDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridLiveDetails).BeginInit();
            pnlLiveDetailsHeader.SuspendLayout();
            cardTestSummary.SuspendLayout();
            cardNotes.SuspendLayout();
            SuspendLayout();
            // 
            // tblMain
            // 
            tblMain.BackColor = Color.FromArgb(244, 245, 247);
            tblMain.ColumnCount = 1;
            tblMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblMain.Controls.Add(pnlTopBar, 0, 0);
            tblMain.Controls.Add(tblRow1, 0, 1);
            tblMain.Controls.Add(tblRow2, 0, 2);
            tblMain.Dock = DockStyle.Fill;
            tblMain.Location = new Point(0, 0);
            tblMain.MinimumSize = new Size(1500, 0);
            tblMain.Name = "tblMain";
            tblMain.RowCount = 3;
            tblMain.RowStyles.Add(new RowStyle());
            tblMain.RowStyles.Add(new RowStyle(SizeType.Percent, 35F));
            tblMain.RowStyles.Add(new RowStyle(SizeType.Percent, 65F));
            tblMain.Size = new Size(2676, 1653);
            tblMain.TabIndex = 0;
            // 
            // pnlTopBar
            // 
            pnlTopBar.AutoSize = true;
            pnlTopBar.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlTopBar.Controls.Add(tblTopWrapper);
            pnlTopBar.Dock = DockStyle.Fill;
            pnlTopBar.Location = new Point(5, 5);
            pnlTopBar.Margin = new Padding(5);
            pnlTopBar.Name = "pnlTopBar";
            pnlTopBar.Size = new Size(2666, 60);
            pnlTopBar.TabIndex = 0;
            // 
            // tblTopWrapper
            // 
            tblTopWrapper.AutoSize = true;
            tblTopWrapper.ColumnCount = 2;
            tblTopWrapper.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblTopWrapper.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblTopWrapper.Controls.Add(lblPageHeader, 0, 0);
            tblTopWrapper.Controls.Add(tblActionButtons, 1, 0);
            tblTopWrapper.Dock = DockStyle.Fill;
            tblTopWrapper.Location = new Point(0, 0);
            tblTopWrapper.Name = "tblTopWrapper";
            tblTopWrapper.RowCount = 1;
            tblTopWrapper.RowStyles.Add(new RowStyle());
            tblTopWrapper.Size = new Size(2666, 60);
            tblTopWrapper.TabIndex = 0;
            // 
            // lblPageHeader
            // 
            lblPageHeader.AutoSize = true;
            lblPageHeader.Dock = DockStyle.Fill;
            lblPageHeader.Font = new Font("Segoe UI", 13.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPageHeader.ForeColor = Color.FromArgb(30, 41, 59);
            lblPageHeader.Location = new Point(10, 0);
            lblPageHeader.Margin = new Padding(10, 0, 0, 8);
            lblPageHeader.Name = "lblPageHeader";
            lblPageHeader.Size = new Size(1323, 52);
            lblPageHeader.TabIndex = 0;
            lblPageHeader.Text = "⚡  TEST EXECUTION";
            lblPageHeader.TextAlign = ContentAlignment.BottomLeft;
            // 
            // tblActionButtons
            // 
            tblActionButtons.ColumnCount = 4;
            tblActionButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 320F));
            tblActionButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220F));
            tblActionButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220F));
            tblActionButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220F));
            tblActionButtons.Controls.Add(btnLoadProgram, 0, 0);
            tblActionButtons.Controls.Add(btnStartTest, 1, 0);
            tblActionButtons.Controls.Add(btnStopTest, 2, 0);
            tblActionButtons.Controls.Add(btnSaveReport, 3, 0);
            tblActionButtons.Dock = DockStyle.Right;
            tblActionButtons.Location = new Point(1686, 0);
            tblActionButtons.Margin = new Padding(0);
            tblActionButtons.Name = "tblActionButtons";
            tblActionButtons.RowCount = 1;
            tblActionButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblActionButtons.Size = new Size(980, 60);
            tblActionButtons.TabIndex = 1;
            // 
            // btnLoadProgram
            // 
            btnLoadProgram.BackColor = Color.FromArgb(255, 193, 7);
            btnLoadProgram.Dock = DockStyle.Fill;
            btnLoadProgram.FlatAppearance.BorderSize = 0;
            btnLoadProgram.FlatStyle = FlatStyle.Flat;
            btnLoadProgram.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLoadProgram.ForeColor = Color.Black;
            btnLoadProgram.HoverForeColor = Color.Empty;
            btnLoadProgram.Location = new Point(0, 0);
            btnLoadProgram.Margin = new Padding(0, 0, 6, 0);
            btnLoadProgram.Name = "btnLoadProgram";
            btnLoadProgram.NormalBackColor = Color.FromArgb(255, 193, 7);
            btnLoadProgram.PressedForeColor = Color.Empty;
            btnLoadProgram.Size = new Size(314, 60);
            btnLoadProgram.TabIndex = 10;
            btnLoadProgram.Text = "📁 Load Test Program";
            btnLoadProgram.UseVisualStyleBackColor = false;
            // 
            // btnStartTest
            // 
            btnStartTest.BackColor = Color.FromArgb(56, 142, 60);
            btnStartTest.Dock = DockStyle.Fill;
            btnStartTest.FlatAppearance.BorderSize = 0;
            btnStartTest.FlatStyle = FlatStyle.Flat;
            btnStartTest.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnStartTest.ForeColor = Color.White;
            btnStartTest.HoverForeColor = Color.Empty;
            btnStartTest.Location = new Point(320, 0);
            btnStartTest.Margin = new Padding(0, 0, 6, 0);
            btnStartTest.Name = "btnStartTest";
            btnStartTest.NormalBackColor = Color.FromArgb(56, 142, 60);
            btnStartTest.PressedForeColor = Color.Empty;
            btnStartTest.Size = new Size(214, 60);
            btnStartTest.TabIndex = 0;
            btnStartTest.Text = "▶ START TEST";
            btnStartTest.UseVisualStyleBackColor = false;
            btnStartTest.Click += BtnStartTest_Click;
            // 
            // btnStopTest
            // 
            btnStopTest.BackColor = Color.FromArgb(211, 47, 47);
            btnStopTest.Dock = DockStyle.Fill;
            btnStopTest.FlatAppearance.BorderSize = 0;
            btnStopTest.FlatStyle = FlatStyle.Flat;
            btnStopTest.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnStopTest.ForeColor = Color.White;
            btnStopTest.HoverForeColor = Color.Empty;
            btnStopTest.Location = new Point(543, 0);
            btnStopTest.Margin = new Padding(3, 0, 3, 0);
            btnStopTest.Name = "btnStopTest";
            btnStopTest.NormalBackColor = Color.FromArgb(211, 47, 47);
            btnStopTest.PressedForeColor = Color.Empty;
            btnStopTest.Size = new Size(214, 60);
            btnStopTest.TabIndex = 1;
            btnStopTest.Text = "⏹ STOP TEST";
            btnStopTest.UseVisualStyleBackColor = false;
            btnStopTest.Click += BtnStopTest_Click;
            // 
            // btnSaveReport
            // 
            btnSaveReport.BackColor = Color.FromArgb(37, 99, 235);
            btnSaveReport.Dock = DockStyle.Fill;
            btnSaveReport.FlatAppearance.BorderSize = 0;
            btnSaveReport.FlatStyle = FlatStyle.Flat;
            btnSaveReport.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSaveReport.ForeColor = Color.White;
            btnSaveReport.HoverForeColor = Color.Empty;
            btnSaveReport.Location = new Point(763, 0);
            btnSaveReport.Margin = new Padding(3, 0, 0, 0);
            btnSaveReport.Name = "btnSaveReport";
            btnSaveReport.NormalBackColor = Color.FromArgb(37, 99, 235);
            btnSaveReport.PressedForeColor = Color.Empty;
            btnSaveReport.Size = new Size(217, 60);
            btnSaveReport.TabIndex = 2;
            btnSaveReport.Text = "💾 SAVE";
            btnSaveReport.UseVisualStyleBackColor = false;
            // 
            // tblRow1
            // 
            tblRow1.AutoSize = true;
            tblRow1.ColumnCount = 4;
            tblRow1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblRow1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblRow1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblRow1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblRow1.Controls.Add(cardMachineOverview, 0, 0);
            tblRow1.Controls.Add(cardHarnessConn, 1, 0);
            tblRow1.Controls.Add(cardAreaDetail, 2, 0);
            tblRow1.Controls.Add(cardTestStatus, 3, 0);
            tblRow1.Dock = DockStyle.Fill;
            tblRow1.Location = new Point(3, 73);
            tblRow1.Name = "tblRow1";
            tblRow1.RowCount = 1;
            tblRow1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblRow1.Size = new Size(2670, 548);
            tblRow1.TabIndex = 1;
            // 
            // cardMachineOverview
            // 
            cardMachineOverview.BackColor = Color.White;
            cardMachineOverview.Controls.Add(picMachineOverview);
            cardMachineOverview.Controls.Add(lblMachineOverviewTitle);
            cardMachineOverview.Dock = DockStyle.Fill;
            cardMachineOverview.Location = new Point(5, 5);
            cardMachineOverview.Margin = new Padding(5);
            cardMachineOverview.Name = "cardMachineOverview";
            cardMachineOverview.Padding = new Padding(10);
            cardMachineOverview.Size = new Size(657, 538);
            cardMachineOverview.TabIndex = 0;
            // 
            // picMachineOverview
            // 
            picMachineOverview.BackColor = Color.FromArgb(248, 250, 252);
            picMachineOverview.Dock = DockStyle.Fill;
            picMachineOverview.Location = new Point(10, 56);
            picMachineOverview.Name = "picMachineOverview";
            picMachineOverview.Size = new Size(637, 472);
            picMachineOverview.SizeMode = PictureBoxSizeMode.Zoom;
            picMachineOverview.TabIndex = 1;
            picMachineOverview.TabStop = false;
            // 
            // lblMachineOverviewTitle
            // 
            lblMachineOverviewTitle.Dock = DockStyle.Top;
            lblMachineOverviewTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMachineOverviewTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblMachineOverviewTitle.Location = new Point(10, 10);
            lblMachineOverviewTitle.Name = "lblMachineOverviewTitle";
            lblMachineOverviewTitle.Size = new Size(637, 46);
            lblMachineOverviewTitle.TabIndex = 0;
            lblMachineOverviewTitle.Text = "MACHINE OVERVIEW";
            lblMachineOverviewTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cardHarnessConn
            // 
            cardHarnessConn.BackColor = Color.White;
            cardHarnessConn.Controls.Add(pnlHarnessImgContainer);
            cardHarnessConn.Controls.Add(lblHarnessConnTitle);
            cardHarnessConn.Dock = DockStyle.Fill;
            cardHarnessConn.Location = new Point(672, 5);
            cardHarnessConn.Margin = new Padding(5);
            cardHarnessConn.Name = "cardHarnessConn";
            cardHarnessConn.Padding = new Padding(10);
            cardHarnessConn.Size = new Size(657, 538);
            cardHarnessConn.TabIndex = 1;
            // 
            // pnlHarnessImgContainer
            // 
            pnlHarnessImgContainer.Controls.Add(btnViewPinMap);
            pnlHarnessImgContainer.Controls.Add(picHarnessConn);
            pnlHarnessImgContainer.Dock = DockStyle.Fill;
            pnlHarnessImgContainer.Location = new Point(10, 56);
            pnlHarnessImgContainer.Name = "pnlHarnessImgContainer";
            pnlHarnessImgContainer.Size = new Size(637, 472);
            pnlHarnessImgContainer.TabIndex = 1;
            // 
            // btnViewPinMap
            // 
            btnViewPinMap.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnViewPinMap.BackColor = Color.FromArgb(37, 99, 235);
            btnViewPinMap.Cursor = Cursors.Hand;
            btnViewPinMap.FlatAppearance.BorderSize = 0;
            btnViewPinMap.FlatStyle = FlatStyle.Flat;
            btnViewPinMap.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnViewPinMap.ForeColor = Color.White;
            btnViewPinMap.HoverForeColor = Color.Empty;
            btnViewPinMap.Location = new Point(369, 421);
            btnViewPinMap.Name = "btnViewPinMap";
            btnViewPinMap.NormalBackColor = Color.FromArgb(37, 99, 235);
            btnViewPinMap.PressedForeColor = Color.Empty;
            btnViewPinMap.Size = new Size(253, 40);
            btnViewPinMap.TabIndex = 1;
            btnViewPinMap.Text = "View Pin Map";
            btnViewPinMap.UseVisualStyleBackColor = false;
            // 
            // picHarnessConn
            // 
            picHarnessConn.BackColor = Color.FromArgb(248, 250, 252);
            picHarnessConn.Dock = DockStyle.Fill;
            picHarnessConn.Location = new Point(0, 0);
            picHarnessConn.Name = "picHarnessConn";
            picHarnessConn.Size = new Size(637, 472);
            picHarnessConn.SizeMode = PictureBoxSizeMode.Zoom;
            picHarnessConn.TabIndex = 0;
            picHarnessConn.TabStop = false;
            // 
            // lblHarnessConnTitle
            // 
            lblHarnessConnTitle.Dock = DockStyle.Top;
            lblHarnessConnTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHarnessConnTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblHarnessConnTitle.Location = new Point(10, 10);
            lblHarnessConnTitle.Name = "lblHarnessConnTitle";
            lblHarnessConnTitle.Size = new Size(637, 46);
            lblHarnessConnTitle.TabIndex = 0;
            lblHarnessConnTitle.Text = "HARNESS CONNECTOR";
            lblHarnessConnTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cardAreaDetail
            // 
            cardAreaDetail.BackColor = Color.White;
            cardAreaDetail.Controls.Add(picAreaDetail);
            cardAreaDetail.Controls.Add(lblAreaDetailTitle);
            cardAreaDetail.Dock = DockStyle.Fill;
            cardAreaDetail.Location = new Point(1339, 5);
            cardAreaDetail.Margin = new Padding(5);
            cardAreaDetail.Name = "cardAreaDetail";
            cardAreaDetail.Padding = new Padding(10);
            cardAreaDetail.Size = new Size(657, 538);
            cardAreaDetail.TabIndex = 2;
            // 
            // picAreaDetail
            // 
            picAreaDetail.BackColor = Color.FromArgb(248, 250, 252);
            picAreaDetail.Dock = DockStyle.Fill;
            picAreaDetail.Location = new Point(10, 56);
            picAreaDetail.Name = "picAreaDetail";
            picAreaDetail.Size = new Size(637, 472);
            picAreaDetail.TabIndex = 1;
            picAreaDetail.TabStop = false;
            // 
            // lblAreaDetailTitle
            // 
            lblAreaDetailTitle.Dock = DockStyle.Top;
            lblAreaDetailTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAreaDetailTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblAreaDetailTitle.Location = new Point(10, 10);
            lblAreaDetailTitle.Name = "lblAreaDetailTitle";
            lblAreaDetailTitle.Size = new Size(637, 46);
            lblAreaDetailTitle.TabIndex = 0;
            lblAreaDetailTitle.Text = "AREA DETAIL";
            lblAreaDetailTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cardTestStatus
            // 
            cardTestStatus.BackColor = Color.White;
            cardTestStatus.Controls.Add(tblStatusLabels);
            cardTestStatus.Controls.Add(lblStatusRunning);
            cardTestStatus.Controls.Add(lblTestStatusTitle);
            cardTestStatus.Dock = DockStyle.Fill;
            cardTestStatus.Location = new Point(2006, 5);
            cardTestStatus.Margin = new Padding(5);
            cardTestStatus.Name = "cardTestStatus";
            cardTestStatus.Padding = new Padding(10);
            cardTestStatus.Size = new Size(659, 538);
            cardTestStatus.TabIndex = 3;
            // 
            // tblStatusLabels
            // 
            tblStatusLabels.ColumnCount = 2;
            tblStatusLabels.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tblStatusLabels.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tblStatusLabels.Controls.Add(lblStartTimeKey, 0, 0);
            tblStatusLabels.Controls.Add(lblStartTimeVal, 1, 0);
            tblStatusLabels.Controls.Add(lblElapsedKey, 0, 1);
            tblStatusLabels.Controls.Add(lblElapsedTimeVal, 1, 1);
            tblStatusLabels.Controls.Add(lblEndTimeKey, 0, 3);
            tblStatusLabels.Controls.Add(lblEndTimeVal, 1, 3);
            tblStatusLabels.Dock = DockStyle.Fill;
            tblStatusLabels.Location = new Point(10, 197);
            tblStatusLabels.Name = "tblStatusLabels";
            tblStatusLabels.RowCount = 4;
            tblStatusLabels.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tblStatusLabels.RowStyles.Add(new RowStyle(SizeType.Percent, 47.513813F));
            tblStatusLabels.RowStyles.Add(new RowStyle(SizeType.Percent, 2.20994473F));
            tblStatusLabels.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tblStatusLabels.Size = new Size(639, 331);
            tblStatusLabels.TabIndex = 2;
            // 
            // lblStartTimeKey
            // 
            lblStartTimeKey.AutoSize = true;
            lblStartTimeKey.Dock = DockStyle.Fill;
            lblStartTimeKey.Font = new Font("Segoe UI", 15F);
            lblStartTimeKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblStartTimeKey.Location = new Point(3, 0);
            lblStartTimeKey.Name = "lblStartTimeKey";
            lblStartTimeKey.Size = new Size(249, 82);
            lblStartTimeKey.TabIndex = 0;
            lblStartTimeKey.Text = "Start Time :";
            lblStartTimeKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblStartTimeVal
            // 
            lblStartTimeVal.AutoSize = true;
            lblStartTimeVal.Dock = DockStyle.Fill;
            lblStartTimeVal.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblStartTimeVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblStartTimeVal.Location = new Point(258, 0);
            lblStartTimeVal.Name = "lblStartTimeVal";
            lblStartTimeVal.Size = new Size(378, 82);
            lblStartTimeVal.TabIndex = 1;
            lblStartTimeVal.Text = "15 Mar 2026 10:30:20 AM";
            lblStartTimeVal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblElapsedKey
            // 
            lblElapsedKey.AutoSize = true;
            lblElapsedKey.Dock = DockStyle.Fill;
            lblElapsedKey.Font = new Font("Segoe UI", 15F);
            lblElapsedKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblElapsedKey.Location = new Point(3, 82);
            lblElapsedKey.Name = "lblElapsedKey";
            lblElapsedKey.Size = new Size(249, 157);
            lblElapsedKey.TabIndex = 2;
            lblElapsedKey.Text = "Elapsed Time :";
            lblElapsedKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblElapsedTimeVal
            // 
            lblElapsedTimeVal.AutoSize = true;
            lblElapsedTimeVal.Dock = DockStyle.Fill;
            lblElapsedTimeVal.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblElapsedTimeVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblElapsedTimeVal.Location = new Point(258, 82);
            lblElapsedTimeVal.Name = "lblElapsedTimeVal";
            lblElapsedTimeVal.Size = new Size(378, 157);
            lblElapsedTimeVal.TabIndex = 3;
            lblElapsedTimeVal.Text = "00:02:15";
            lblElapsedTimeVal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblEndTimeKey
            // 
            lblEndTimeKey.AutoSize = true;
            lblEndTimeKey.Dock = DockStyle.Fill;
            lblEndTimeKey.Font = new Font("Segoe UI", 15F);
            lblEndTimeKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblEndTimeKey.Location = new Point(3, 246);
            lblEndTimeKey.Name = "lblEndTimeKey";
            lblEndTimeKey.Size = new Size(249, 85);
            lblEndTimeKey.TabIndex = 6;
            lblEndTimeKey.Text = "End Time (Est.) :";
            lblEndTimeKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblEndTimeVal
            // 
            lblEndTimeVal.AutoSize = true;
            lblEndTimeVal.Dock = DockStyle.Fill;
            lblEndTimeVal.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblEndTimeVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblEndTimeVal.Location = new Point(258, 246);
            lblEndTimeVal.Name = "lblEndTimeVal";
            lblEndTimeVal.Size = new Size(378, 85);
            lblEndTimeVal.TabIndex = 7;
            lblEndTimeVal.Text = "15 Mar 2026 10:39:21 AM";
            lblEndTimeVal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblStatusRunning
            // 
            lblStatusRunning.Dock = DockStyle.Top;
            lblStatusRunning.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblStatusRunning.ForeColor = Color.Gray;
            lblStatusRunning.Location = new Point(10, 56);
            lblStatusRunning.Name = "lblStatusRunning";
            lblStatusRunning.Size = new Size(639, 141);
            lblStatusRunning.TabIndex = 1;
            lblStatusRunning.Text = "Idle";
            lblStatusRunning.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTestStatusTitle
            // 
            lblTestStatusTitle.Dock = DockStyle.Top;
            lblTestStatusTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTestStatusTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblTestStatusTitle.Location = new Point(10, 10);
            lblTestStatusTitle.Name = "lblTestStatusTitle";
            lblTestStatusTitle.Size = new Size(639, 46);
            lblTestStatusTitle.TabIndex = 0;
            lblTestStatusTitle.Text = "TEST STATUS";
            lblTestStatusTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tblRow2
            // 
            tblRow2.ColumnCount = 2;
            tblRow2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75F));
            tblRow2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblRow2.Controls.Add(cardLiveDetails, 0, 0);
            tblRow2.Controls.Add(cardTestSummary, 1, 0);
            tblRow2.Controls.Add(cardNotes, 1, 1);
            tblRow2.Dock = DockStyle.Fill;
            tblRow2.Location = new Point(3, 627);
            tblRow2.Name = "tblRow2";
            tblRow2.RowCount = 2;
            tblRow2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tblRow2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tblRow2.Size = new Size(2670, 1023);
            tblRow2.TabIndex = 2;
            // 
            // cardLiveDetails
            // 
            cardLiveDetails.BackColor = Color.White;
            cardLiveDetails.Controls.Add(gridLiveDetails);
            cardLiveDetails.Controls.Add(pnlLiveDetailsHeader);
            cardLiveDetails.Dock = DockStyle.Fill;
            cardLiveDetails.Location = new Point(3, 3);
            cardLiveDetails.Name = "cardLiveDetails";
            cardLiveDetails.Padding = new Padding(10);
            tblRow2.SetRowSpan(cardLiveDetails, 2);
            cardLiveDetails.Size = new Size(1996, 1017);
            cardLiveDetails.TabIndex = 0;
            // 
            // gridLiveDetails
            // 
            gridLiveDetails.AllowUserToAddRows = false;
            gridLiveDetails.AllowUserToDeleteRows = false;
            gridLiveDetails.BackgroundColor = Color.White;
            gridLiveDetails.BorderStyle = BorderStyle.None;
            gridLiveDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridLiveDetails.Dock = DockStyle.Fill;
            gridLiveDetails.Location = new Point(10, 72);
            gridLiveDetails.Name = "gridLiveDetails";
            gridLiveDetails.ReadOnly = true;
            gridLiveDetails.RowHeadersVisible = false;
            gridLiveDetails.RowHeadersWidth = 82;
            gridLiveDetails.Size = new Size(1976, 935);
            gridLiveDetails.TabIndex = 1;
            // 
            // pnlLiveDetailsHeader
            // 
            pnlLiveDetailsHeader.Controls.Add(btnViewWiringDiagram);
            pnlLiveDetailsHeader.Controls.Add(lblLiveDetailsTitle);
            pnlLiveDetailsHeader.Dock = DockStyle.Top;
            pnlLiveDetailsHeader.Location = new Point(10, 10);
            pnlLiveDetailsHeader.Name = "pnlLiveDetailsHeader";
            pnlLiveDetailsHeader.Size = new Size(1976, 62);
            pnlLiveDetailsHeader.TabIndex = 0;
            // 
            // btnViewWiringDiagram
            // 
            btnViewWiringDiagram.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnViewWiringDiagram.BackColor = Color.FromArgb(37, 99, 235);
            btnViewWiringDiagram.Cursor = Cursors.Hand;
            btnViewWiringDiagram.FlatAppearance.BorderSize = 0;
            btnViewWiringDiagram.FlatStyle = FlatStyle.Flat;
            btnViewWiringDiagram.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnViewWiringDiagram.ForeColor = Color.White;
            btnViewWiringDiagram.HoverForeColor = Color.Empty;
            btnViewWiringDiagram.Location = new Point(1707, 12);
            btnViewWiringDiagram.Name = "btnViewWiringDiagram";
            btnViewWiringDiagram.NormalBackColor = Color.FromArgb(37, 99, 235);
            btnViewWiringDiagram.PressedForeColor = Color.Empty;
            btnViewWiringDiagram.Size = new Size(259, 38);
            btnViewWiringDiagram.TabIndex = 1;
            btnViewWiringDiagram.Text = "Wiring Preview";
            btnViewWiringDiagram.UseVisualStyleBackColor = false;
            // 
            // lblLiveDetailsTitle
            // 
            lblLiveDetailsTitle.Dock = DockStyle.Fill;
            lblLiveDetailsTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblLiveDetailsTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblLiveDetailsTitle.Location = new Point(0, 0);
            lblLiveDetailsTitle.Name = "lblLiveDetailsTitle";
            lblLiveDetailsTitle.Size = new Size(1976, 62);
            lblLiveDetailsTitle.TabIndex = 0;
            lblLiveDetailsTitle.Text = "LIVE TEST DETAILS";
            lblLiveDetailsTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cardTestSummary
            // 
            cardTestSummary.BackColor = Color.White;
            cardTestSummary.Controls.Add(listBoxTestSummary);
            cardTestSummary.Controls.Add(lblTestSummaryTitle);
            cardTestSummary.Dock = DockStyle.Fill;
            cardTestSummary.Location = new Point(2005, 3);
            cardTestSummary.Name = "cardTestSummary";
            cardTestSummary.Padding = new Padding(10);
            cardTestSummary.Size = new Size(662, 505);
            cardTestSummary.TabIndex = 0;
            // 
            // listBoxTestSummary
            // 
            listBoxTestSummary.BackColor = Color.FromArgb(248, 250, 252);
            listBoxTestSummary.Dock = DockStyle.Fill;
            listBoxTestSummary.FormattingEnabled = true;
            listBoxTestSummary.HorizontalScrollbar = true;
            listBoxTestSummary.Location = new Point(10, 56);
            listBoxTestSummary.Name = "listBoxTestSummary";
            listBoxTestSummary.ScrollAlwaysVisible = true;
            listBoxTestSummary.Size = new Size(642, 439);
            listBoxTestSummary.TabIndex = 2;
            // 
            // lblTestSummaryTitle
            // 
            lblTestSummaryTitle.Dock = DockStyle.Top;
            lblTestSummaryTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTestSummaryTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblTestSummaryTitle.Location = new Point(10, 10);
            lblTestSummaryTitle.Name = "lblTestSummaryTitle";
            lblTestSummaryTitle.Size = new Size(642, 46);
            lblTestSummaryTitle.TabIndex = 0;
            lblTestSummaryTitle.Text = "TEST SUMMARY";
            lblTestSummaryTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cardNotes
            // 
            cardNotes.BackColor = Color.White;
            cardNotes.Controls.Add(txtNotes);
            cardNotes.Controls.Add(lblNotesTitle);
            cardNotes.Dock = DockStyle.Fill;
            cardNotes.Location = new Point(2005, 514);
            cardNotes.Name = "cardNotes";
            cardNotes.Padding = new Padding(10);
            cardNotes.Size = new Size(662, 506);
            cardNotes.TabIndex = 1;
            // 
            // txtNotes
            // 
            txtNotes.BorderStyle = BorderStyle.FixedSingle;
            txtNotes.Dock = DockStyle.Fill;
            txtNotes.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNotes.ForeColor = Color.FromArgb(30, 41, 59);
            txtNotes.Location = new Point(10, 57);
            txtNotes.MaxLength = 2000;
            txtNotes.Multiline = true;
            txtNotes.Name = "txtNotes";
            txtNotes.ScrollBars = ScrollBars.Vertical;
            txtNotes.Size = new Size(642, 439);
            txtNotes.TabIndex = 1;
            // 
            // lblNotesTitle
            // 
            lblNotesTitle.Dock = DockStyle.Top;
            lblNotesTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNotesTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblNotesTitle.Location = new Point(10, 10);
            lblNotesTitle.Name = "lblNotesTitle";
            lblNotesTitle.Size = new Size(642, 47);
            lblNotesTitle.TabIndex = 0;
            lblNotesTitle.Text = "NOTES";
            lblNotesTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TestExecution
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(244, 245, 247);
            Controls.Add(tblMain);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "TestExecution";
            Size = new Size(2676, 1653);
            tblMain.ResumeLayout(false);
            tblMain.PerformLayout();
            pnlTopBar.ResumeLayout(false);
            pnlTopBar.PerformLayout();
            tblTopWrapper.ResumeLayout(false);
            tblTopWrapper.PerformLayout();
            tblActionButtons.ResumeLayout(false);
            tblRow1.ResumeLayout(false);
            cardMachineOverview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picMachineOverview).EndInit();
            cardHarnessConn.ResumeLayout(false);
            pnlHarnessImgContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picHarnessConn).EndInit();
            cardAreaDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picAreaDetail).EndInit();
            cardTestStatus.ResumeLayout(false);
            tblStatusLabels.ResumeLayout(false);
            tblStatusLabels.PerformLayout();
            tblRow2.ResumeLayout(false);
            cardLiveDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridLiveDetails).EndInit();
            pnlLiveDetailsHeader.ResumeLayout(false);
            cardTestSummary.ResumeLayout(false);
            cardNotes.ResumeLayout(false);
            cardNotes.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblMain;
        private System.Windows.Forms.Panel pnlTopBar;
        private System.Windows.Forms.TableLayoutPanel tblTopWrapper;
        private System.Windows.Forms.Label lblPageHeader;
        private AssemblyLineTester.Views.Controls.RoundedButton btnLoadProgram;
        private System.Windows.Forms.TableLayoutPanel tblActionButtons;
        private AssemblyLineTester.Views.Controls.RoundedButton btnStartTest;
        private AssemblyLineTester.Views.Controls.RoundedButton btnStopTest;
        private AssemblyLineTester.Views.Controls.RoundedButton btnSaveReport;
        private System.Windows.Forms.TableLayoutPanel tblRow1;
        private System.Windows.Forms.Panel cardMachineOverview;
        private System.Windows.Forms.PictureBox picMachineOverview;
        private System.Windows.Forms.Label lblMachineOverviewTitle;
        private System.Windows.Forms.Panel cardAreaDetail;
        private System.Windows.Forms.PictureBox picAreaDetail;
        private System.Windows.Forms.Label lblAreaDetailTitle;
        private System.Windows.Forms.Panel cardHarnessConn;
        private System.Windows.Forms.Panel pnlHarnessImgContainer;
        private AssemblyLineTester.Views.Controls.RoundedButton btnViewPinMap;
        private System.Windows.Forms.PictureBox picHarnessConn;
        private System.Windows.Forms.Label lblHarnessConnTitle;
        private System.Windows.Forms.Panel cardTestStatus;
        private System.Windows.Forms.TableLayoutPanel tblStatusLabels;
        private System.Windows.Forms.Label lblStartTimeKey;
        private System.Windows.Forms.Label lblStartTimeVal;
        private System.Windows.Forms.Label lblElapsedKey;
        private System.Windows.Forms.Label lblElapsedTimeVal;
        private System.Windows.Forms.Label lblEndTimeKey;
        private System.Windows.Forms.Label lblEndTimeVal;
        private System.Windows.Forms.Label lblStatusRunning;
        private System.Windows.Forms.Label lblTestStatusTitle;
        private System.Windows.Forms.TableLayoutPanel tblRow2;
        private System.Windows.Forms.Panel cardLiveDetails;
        private System.Windows.Forms.Panel pnlLiveDetailsHeader;
        private AssemblyLineTester.Views.Controls.RoundedButton btnViewWiringDiagram;
        private System.Windows.Forms.DataGridView gridLiveDetails;
        private System.Windows.Forms.Label lblLiveDetailsTitle;
        private System.Windows.Forms.Panel cardTestSummary;
        private System.Windows.Forms.Label lblTestSummaryTitle;
        private System.Windows.Forms.Panel cardNotes;
        private System.Windows.Forms.Label lblNotesTitle;
        private System.Windows.Forms.TextBox txtNotes;
        private ListBox listBoxTestSummary;
    }
}
