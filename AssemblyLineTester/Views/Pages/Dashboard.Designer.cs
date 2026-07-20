using System;

using System.Drawing;

using System.Windows.Forms;

using System.Windows.Forms.DataVisualization.Charting;



namespace AssemblyLineTester.Views.Pages

{

    partial class Dashboard

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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            tblMain = new TableLayoutPanel();
            tblTop = new TableLayoutPanel();
            cardTester = new Panel();
            lblTesterSub = new Label();
            lblTesterStatus = new Label();
            lblTesterTitle = new Label();
            cardMachine = new Panel();
            lblMachineSub = new Label();
            lblMachineStatus = new Label();
            lblMachineTitle = new Label();
            cardDatabase = new Panel();
            lblDatabaseSub = new Label();
            lblDatabaseStatus = new Label();
            lblDatabaseTitle = new Label();
            cardActiveProg = new Panel();
            btnChangeProg = new Button();
            picHarness = new PictureBox();
            lblProgDetails = new Label();
            lblActiveProgTitle = new Label();
            tblMiddle = new TableLayoutPanel();
            cardSummary = new Panel();
            tblCharts = new TableLayoutPanel();
            chartHistory = new Chart();
            chartFailReason = new Chart();
            tblKpi = new TableLayoutPanel();
            kpiTotalTested = new Panel();
            lblTotalTestedVal = new Label();
            lblTotalTestedTitle = new Label();
            kpiPass = new Panel();
            lblPassVal = new Label();
            lblPassTitle = new Label();
            kpiFail = new Panel();
            lblFailVal = new Label();
            lblFailTitle = new Label();
            kpiYield = new Panel();
            lblYieldVal = new Label();
            lblYieldTitle = new Label();
            kpiCycleTime = new Panel();
            lblCycleTimeVal = new Label();
            lblCycleTimeTitle = new Label();
            cmbTimeRange = new ComboBox();
            lblSummaryTitle = new Label();
            cardHistory = new Panel();
            gridRecent = new DataGridView();
            lnkViewAll = new LinkLabel();
            lblRecentTitle = new Label();
            tblBottom = new TableLayoutPanel();
            cardModules = new Panel();
            tblModules = new TableLayoutPanel();
            pnlMod1 = new Panel();
            lblMod1Status = new Label();
            lblMod1Title = new Label();
            pnlMod2 = new Panel();
            lblMod2Status = new Label();
            lblMod2Title = new Label();
            pnlMod3 = new Panel();
            lblMod3Status = new Label();
            lblMod3Title = new Label();
            pnlMod4 = new Panel();
            lblMod4Status = new Label();
            lblMod4Title = new Label();
            lblModulesTitle = new Label();
            cardOperatingTime = new Panel();
            lblOpTimeSub = new Label();
            lblOpTimeVal = new Label();
            lblOpTimeTitle = new Label();
            cardMaintenance = new Panel();
            lblMaintSub = new Label();
            lblMaintVal = new Label();
            lblMaintTitle = new Label();
            tblMain.SuspendLayout();
            tblTop.SuspendLayout();
            cardTester.SuspendLayout();
            cardMachine.SuspendLayout();
            cardDatabase.SuspendLayout();
            cardActiveProg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picHarness).BeginInit();
            tblMiddle.SuspendLayout();
            cardSummary.SuspendLayout();
            tblCharts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartHistory).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartFailReason).BeginInit();
            tblKpi.SuspendLayout();
            kpiTotalTested.SuspendLayout();
            kpiPass.SuspendLayout();
            kpiFail.SuspendLayout();
            kpiYield.SuspendLayout();
            kpiCycleTime.SuspendLayout();
            cardHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridRecent).BeginInit();
            tblBottom.SuspendLayout();
            cardModules.SuspendLayout();
            tblModules.SuspendLayout();
            pnlMod1.SuspendLayout();
            pnlMod2.SuspendLayout();
            pnlMod3.SuspendLayout();
            pnlMod4.SuspendLayout();
            cardOperatingTime.SuspendLayout();
            cardMaintenance.SuspendLayout();
            SuspendLayout();
            // 
            // tblMain
            // 
            tblMain.ColumnCount = 1;
            tblMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblMain.Controls.Add(tblTop, 0, 0);
            tblMain.Controls.Add(tblMiddle, 0, 1);
            tblMain.Dock = DockStyle.Fill;
            tblMain.Location = new Point(0, 0);
            tblMain.Margin = new Padding(0);
            tblMain.Name = "tblMain";
            tblMain.RowCount = 2;
            tblMain.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tblMain.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            tblMain.Size = new Size(2600, 1440);
            tblMain.TabIndex = 0;
            // 
            // tblTop
            // 
            tblTop.ColumnCount = 4;
            tblTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22F));
            tblTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22F));
            tblTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22F));
            tblTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            tblTop.Controls.Add(cardTester, 0, 0);
            tblTop.Controls.Add(cardMachine, 1, 0);
            tblTop.Controls.Add(cardDatabase, 2, 0);
            tblTop.Controls.Add(cardActiveProg, 3, 0);
            tblTop.Dock = DockStyle.Fill;
            tblTop.Location = new Point(13, 13);
            tblTop.Margin = new Padding(13);
            tblTop.Name = "tblTop";
            tblTop.RowCount = 1;
            tblTop.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblTop.Size = new Size(2574, 262);
            tblTop.TabIndex = 0;
            // 
            // cardTester
            // 
            cardTester.BackColor = Color.White;
            cardTester.Controls.Add(lblTesterSub);
            cardTester.Controls.Add(lblTesterStatus);
            cardTester.Controls.Add(lblTesterTitle);
            cardTester.Dock = DockStyle.Fill;
            cardTester.Location = new Point(10, 10);
            cardTester.Margin = new Padding(10);
            cardTester.Name = "cardTester";
            cardTester.Padding = new Padding(24);
            cardTester.Size = new Size(546, 242);
            cardTester.TabIndex = 0;
            // 
            // lblTesterSub
            // 
            lblTesterSub.AutoSize = true;
            lblTesterSub.Font = new Font("Segoe UI", 9F);
            lblTesterSub.ForeColor = Color.FromArgb(100, 116, 139);
            lblTesterSub.Location = new Point(24, 160);
            lblTesterSub.Margin = new Padding(5, 0, 5, 0);
            lblTesterSub.Name = "lblTesterSub";
            lblTesterSub.Size = new Size(375, 32);
            lblTesterSub.TabIndex = 2;
            lblTesterSub.Text = "All tester modules are operational";
            // 
            // lblTesterStatus
            // 
            lblTesterStatus.AutoSize = true;
            lblTesterStatus.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTesterStatus.ForeColor = Color.FromArgb(76, 175, 80);
            lblTesterStatus.Location = new Point(21, 80);
            lblTesterStatus.Margin = new Padding(5, 0, 5, 0);
            lblTesterStatus.Name = "lblTesterStatus";
            lblTesterStatus.Size = new Size(197, 59);
            lblTesterStatus.TabIndex = 1;
            lblTesterStatus.Text = " ONLINE";
            // 
            // lblTesterTitle
            // 
            lblTesterTitle.AutoSize = true;
            lblTesterTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTesterTitle.ForeColor = Color.FromArgb(51, 65, 85);
            lblTesterTitle.Location = new Point(24, 24);
            lblTesterTitle.Margin = new Padding(5, 0, 5, 0);
            lblTesterTitle.Name = "lblTesterTitle";
            lblTesterTitle.Size = new Size(214, 37);
            lblTesterTitle.TabIndex = 0;
            lblTesterTitle.Text = "TESTER STATUS";
            // 
            // cardMachine
            // 
            cardMachine.BackColor = Color.White;
            cardMachine.Controls.Add(lblMachineSub);
            cardMachine.Controls.Add(lblMachineStatus);
            cardMachine.Controls.Add(lblMachineTitle);
            cardMachine.Dock = DockStyle.Fill;
            cardMachine.Location = new Point(576, 10);
            cardMachine.Margin = new Padding(10);
            cardMachine.Name = "cardMachine";
            cardMachine.Padding = new Padding(24);
            cardMachine.Size = new Size(546, 242);
            cardMachine.TabIndex = 1;
            // 
            // lblMachineSub
            // 
            lblMachineSub.AutoSize = true;
            lblMachineSub.Font = new Font("Segoe UI", 9F);
            lblMachineSub.ForeColor = Color.FromArgb(100, 116, 139);
            lblMachineSub.Location = new Point(24, 160);
            lblMachineSub.Margin = new Padding(5, 0, 5, 0);
            lblMachineSub.Name = "lblMachineSub";
            lblMachineSub.Size = new Size(191, 32);
            lblMachineSub.TabIndex = 2;
            lblMachineSub.Text = "No active alarms";
            // 
            // lblMachineStatus
            // 
            lblMachineStatus.AutoSize = true;
            lblMachineStatus.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblMachineStatus.ForeColor = Color.FromArgb(76, 175, 80);
            lblMachineStatus.Location = new Point(21, 80);
            lblMachineStatus.Margin = new Padding(5, 0, 5, 0);
            lblMachineStatus.Name = "lblMachineStatus";
            lblMachineStatus.Size = new Size(177, 59);
            lblMachineStatus.TabIndex = 1;
            lblMachineStatus.Text = " READY";
            // 
            // lblMachineTitle
            // 
            lblMachineTitle.AutoSize = true;
            lblMachineTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMachineTitle.ForeColor = Color.FromArgb(51, 65, 85);
            lblMachineTitle.Location = new Point(24, 24);
            lblMachineTitle.Margin = new Padding(5, 0, 5, 0);
            lblMachineTitle.Name = "lblMachineTitle";
            lblMachineTitle.Size = new Size(248, 37);
            lblMachineTitle.TabIndex = 0;
            lblMachineTitle.Text = "MACHINE STATUS";
            // 
            // cardDatabase
            // 
            cardDatabase.BackColor = Color.White;
            cardDatabase.Controls.Add(lblDatabaseSub);
            cardDatabase.Controls.Add(lblDatabaseStatus);
            cardDatabase.Controls.Add(lblDatabaseTitle);
            cardDatabase.Dock = DockStyle.Fill;
            cardDatabase.Location = new Point(1142, 10);
            cardDatabase.Margin = new Padding(10);
            cardDatabase.Name = "cardDatabase";
            cardDatabase.Padding = new Padding(24);
            cardDatabase.Size = new Size(546, 242);
            cardDatabase.TabIndex = 2;
            // 
            // lblDatabaseSub
            // 
            lblDatabaseSub.AutoSize = true;
            lblDatabaseSub.Font = new Font("Segoe UI", 9F);
            lblDatabaseSub.ForeColor = Color.FromArgb(100, 116, 139);
            lblDatabaseSub.Location = new Point(24, 160);
            lblDatabaseSub.Margin = new Padding(5, 0, 5, 0);
            lblDatabaseSub.Name = "lblDatabaseSub";
            lblDatabaseSub.Size = new Size(213, 32);
            lblDatabaseSub.TabIndex = 2;
            lblDatabaseSub.Text = "SQL Server: Online";
            // 
            // lblDatabaseStatus
            // 
            lblDatabaseStatus.AutoSize = true;
            lblDatabaseStatus.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblDatabaseStatus.ForeColor = Color.FromArgb(76, 175, 80);
            lblDatabaseStatus.Location = new Point(21, 80);
            lblDatabaseStatus.Margin = new Padding(5, 0, 5, 0);
            lblDatabaseStatus.Name = "lblDatabaseStatus";
            lblDatabaseStatus.Size = new Size(294, 59);
            lblDatabaseStatus.TabIndex = 1;
            lblDatabaseStatus.Text = " CONNECTED";
            // 
            // lblDatabaseTitle
            // 
            lblDatabaseTitle.AutoSize = true;
            lblDatabaseTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDatabaseTitle.ForeColor = Color.FromArgb(51, 65, 85);
            lblDatabaseTitle.Location = new Point(24, 24);
            lblDatabaseTitle.Margin = new Padding(5, 0, 5, 0);
            lblDatabaseTitle.Name = "lblDatabaseTitle";
            lblDatabaseTitle.Size = new Size(256, 37);
            lblDatabaseTitle.TabIndex = 0;
            lblDatabaseTitle.Text = "DATABASE STATUS";
            // 
            // cardActiveProg
            // 
            cardActiveProg.BackColor = Color.White;
            cardActiveProg.Controls.Add(btnChangeProg);
            cardActiveProg.Controls.Add(picHarness);
            cardActiveProg.Controls.Add(lblProgDetails);
            cardActiveProg.Controls.Add(lblActiveProgTitle);
            cardActiveProg.Dock = DockStyle.Fill;
            cardActiveProg.Location = new Point(1708, 10);
            cardActiveProg.Margin = new Padding(10);
            cardActiveProg.Name = "cardActiveProg";
            cardActiveProg.Padding = new Padding(24);
            cardActiveProg.Size = new Size(856, 242);
            cardActiveProg.TabIndex = 3;
            // 
            // btnChangeProg
            // 
            btnChangeProg.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnChangeProg.BackColor = Color.FromArgb(255, 193, 7);
            btnChangeProg.FlatAppearance.BorderSize = 0;
            btnChangeProg.FlatStyle = FlatStyle.Flat;
            btnChangeProg.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnChangeProg.ForeColor = Color.FromArgb(30, 41, 59);
            btnChangeProg.Location = new Point(592, 168);
            btnChangeProg.Margin = new Padding(5);
            btnChangeProg.Name = "btnChangeProg";
            btnChangeProg.Size = new Size(236, 54);
            btnChangeProg.TabIndex = 3;
            btnChangeProg.Text = "Change Program";
            btnChangeProg.UseVisualStyleBackColor = false;
            // 
            // picHarness
            // 
            picHarness.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            picHarness.BackColor = Color.FromArgb(248, 250, 252);
            picHarness.Location = new Point(592, 24);
            picHarness.Margin = new Padding(5);
            picHarness.Name = "picHarness";
            picHarness.Size = new Size(234, 127);
            picHarness.SizeMode = PictureBoxSizeMode.Zoom;
            picHarness.TabIndex = 2;
            picHarness.TabStop = false;
            // 
            // lblProgDetails
            // 
            lblProgDetails.AutoSize = true;
            lblProgDetails.Font = new Font("Segoe UI", 9F);
            lblProgDetails.ForeColor = Color.FromArgb(51, 65, 85);
            lblProgDetails.Location = new Point(24, 72);
            lblProgDetails.Margin = new Padding(5, 0, 5, 0);
            lblProgDetails.Name = "lblProgDetails";
            lblProgDetails.Size = new Size(326, 160);
            lblProgDetails.TabIndex = 1;
            lblProgDetails.Text = "Product Model : 6060\r\nProduct Type  : AC\r\nArea          : Engine Control\r\nPart Number   : WH-ENG-001\r\nECN No.       : 123.R001";
            // 
            // lblActiveProgTitle
            // 
            lblActiveProgTitle.AutoSize = true;
            lblActiveProgTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblActiveProgTitle.ForeColor = Color.FromArgb(51, 65, 85);
            lblActiveProgTitle.Location = new Point(24, 24);
            lblActiveProgTitle.Margin = new Padding(5, 0, 5, 0);
            lblActiveProgTitle.Name = "lblActiveProgTitle";
            lblActiveProgTitle.Size = new Size(254, 37);
            lblActiveProgTitle.TabIndex = 0;
            lblActiveProgTitle.Text = "ACTIVE PROGRAM";
            // 
            // tblMiddle
            // 
            tblMiddle.ColumnCount = 3;
            tblMiddle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 67.28827F));
            tblMiddle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18.06173F));
            tblMiddle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.65F));
            tblMiddle.Controls.Add(cardSummary, 0, 0);
            tblMiddle.SetRowSpan(cardSummary, 2);
            tblMiddle.Controls.Add(cardHistory, 1, 0);
            tblMiddle.SetColumnSpan(cardHistory, 2);
            tblMiddle.Controls.Add(cardOperatingTime, 1, 1);
            tblMiddle.Controls.Add(cardMaintenance, 2, 1);
            tblMiddle.Dock = DockStyle.Fill;
            tblMiddle.Location = new Point(13, 301);
            tblMiddle.Margin = new Padding(13);
            tblMiddle.Name = "tblMiddle";
            tblMiddle.RowCount = 2;
            tblMiddle.RowStyles.Add(new RowStyle(SizeType.Percent, 81.25F));
            tblMiddle.RowStyles.Add(new RowStyle(SizeType.Percent, 18.75F));
            tblMiddle.Size = new Size(2574, 910);
            tblMiddle.TabIndex = 1;
            // 
            // cardSummary
            // 
            cardSummary.BackColor = Color.White;
            cardSummary.Controls.Add(tblCharts);
            cardSummary.Controls.Add(tblKpi);
            cardSummary.Controls.Add(cmbTimeRange);
            cardSummary.Controls.Add(lblSummaryTitle);
            cardSummary.Dock = DockStyle.Fill;
            cardSummary.Location = new Point(10, 10);
            cardSummary.Margin = new Padding(10);
            cardSummary.Name = "cardSummary";
            cardSummary.Padding = new Padding(24);
            cardSummary.Size = new Size(1712, 890);
            cardSummary.TabIndex = 0;
            // 
            // tblCharts
            // 
            tblCharts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tblCharts.ColumnCount = 2;
            tblCharts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 72.48645F));
            tblCharts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27.513546F));
            tblCharts.Controls.Add(chartHistory, 0, 0);
            tblCharts.Controls.Add(chartFailReason, 1, 0);
            tblCharts.Location = new Point(24, 240);
            tblCharts.Margin = new Padding(5);
            tblCharts.Name = "tblCharts";
            tblCharts.RowCount = 1;
            tblCharts.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblCharts.Size = new Size(1661, 624);
            tblCharts.TabIndex = 3;
            // 
            // chartHistory
            // 
            chartHistory.Dock = DockStyle.Fill;
            chartHistory.Location = new Point(5, 5);
            chartHistory.Margin = new Padding(5);
            chartHistory.Name = "chartHistory";
            chartHistory.Size = new Size(1193, 614);
            chartHistory.TabIndex = 0;
            chartHistory.Text = "chartHistory";
            // 
            // chartFailReason
            // 
            chartFailReason.Dock = DockStyle.Fill;
            chartFailReason.Location = new Point(1208, 5);
            chartFailReason.Margin = new Padding(5);
            chartFailReason.Name = "chartFailReason";
            chartFailReason.Size = new Size(448, 614);
            chartFailReason.TabIndex = 1;
            chartFailReason.Text = "chartFailReason";
            // 
            // tblKpi
            // 
            tblKpi.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tblKpi.ColumnCount = 5;
            tblKpi.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tblKpi.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tblKpi.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tblKpi.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tblKpi.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tblKpi.Controls.Add(kpiTotalTested, 0, 0);
            tblKpi.Controls.Add(kpiPass, 1, 0);
            tblKpi.Controls.Add(kpiFail, 2, 0);
            tblKpi.Controls.Add(kpiYield, 3, 0);
            tblKpi.Controls.Add(kpiCycleTime, 4, 0);
            tblKpi.Location = new Point(24, 88);
            tblKpi.Margin = new Padding(5);
            tblKpi.Name = "tblKpi";
            tblKpi.RowCount = 1;
            tblKpi.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblKpi.Size = new Size(1661, 136);
            tblKpi.TabIndex = 2;
            // 
            // kpiTotalTested
            // 
            kpiTotalTested.BackColor = Color.FromArgb(248, 250, 252);
            kpiTotalTested.Controls.Add(lblTotalTestedVal);
            kpiTotalTested.Controls.Add(lblTotalTestedTitle);
            kpiTotalTested.Dock = DockStyle.Fill;
            kpiTotalTested.Location = new Point(6, 6);
            kpiTotalTested.Margin = new Padding(6);
            kpiTotalTested.Name = "kpiTotalTested";
            kpiTotalTested.Size = new Size(320, 124);
            kpiTotalTested.TabIndex = 0;
            // 
            // lblTotalTestedVal
            // 
            lblTotalTestedVal.AutoSize = true;
            lblTotalTestedVal.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTotalTestedVal.ForeColor = Color.FromArgb(37, 99, 235);
            lblTotalTestedVal.Location = new Point(13, 40);
            lblTotalTestedVal.Margin = new Padding(5, 0, 5, 0);
            lblTotalTestedVal.Name = "lblTotalTestedVal";
            lblTotalTestedVal.Size = new Size(112, 65);
            lblTotalTestedVal.TabIndex = 1;
            lblTotalTestedVal.Text = "215";
            // 
            // lblTotalTestedTitle
            // 
            lblTotalTestedTitle.AutoSize = true;
            lblTotalTestedTitle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblTotalTestedTitle.ForeColor = Color.FromArgb(37, 99, 235);
            lblTotalTestedTitle.Location = new Point(13, 13);
            lblTotalTestedTitle.Margin = new Padding(5, 0, 5, 0);
            lblTotalTestedTitle.Name = "lblTotalTestedTitle";
            lblTotalTestedTitle.Size = new Size(162, 30);
            lblTotalTestedTitle.TabIndex = 0;
            lblTotalTestedTitle.Text = "TOTAL TESTED";
            // 
            // kpiPass
            // 
            kpiPass.BackColor = Color.FromArgb(248, 250, 252);
            kpiPass.Controls.Add(lblPassVal);
            kpiPass.Controls.Add(lblPassTitle);
            kpiPass.Dock = DockStyle.Fill;
            kpiPass.Location = new Point(338, 6);
            kpiPass.Margin = new Padding(6);
            kpiPass.Name = "kpiPass";
            kpiPass.Size = new Size(320, 124);
            kpiPass.TabIndex = 1;
            // 
            // lblPassVal
            // 
            lblPassVal.AutoSize = true;
            lblPassVal.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblPassVal.ForeColor = Color.FromArgb(76, 175, 80);
            lblPassVal.Location = new Point(13, 40);
            lblPassVal.Margin = new Padding(5, 0, 5, 0);
            lblPassVal.Name = "lblPassVal";
            lblPassVal.Size = new Size(112, 65);
            lblPassVal.TabIndex = 1;
            lblPassVal.Text = "212";
            // 
            // lblPassTitle
            // 
            lblPassTitle.AutoSize = true;
            lblPassTitle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblPassTitle.ForeColor = Color.FromArgb(76, 175, 80);
            lblPassTitle.Location = new Point(13, 13);
            lblPassTitle.Margin = new Padding(5, 0, 5, 0);
            lblPassTitle.Name = "lblPassTitle";
            lblPassTitle.Size = new Size(164, 30);
            lblPassTitle.TabIndex = 0;
            lblPassTitle.Text = "PASS (98.60%)";
            // 
            // kpiFail
            // 
            kpiFail.BackColor = Color.FromArgb(248, 250, 252);
            kpiFail.Controls.Add(lblFailVal);
            kpiFail.Controls.Add(lblFailTitle);
            kpiFail.Dock = DockStyle.Fill;
            kpiFail.Location = new Point(670, 6);
            kpiFail.Margin = new Padding(6);
            kpiFail.Name = "kpiFail";
            kpiFail.Size = new Size(320, 124);
            kpiFail.TabIndex = 2;
            // 
            // lblFailVal
            // 
            lblFailVal.AutoSize = true;
            lblFailVal.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblFailVal.ForeColor = Color.FromArgb(244, 67, 54);
            lblFailVal.Location = new Point(13, 40);
            lblFailVal.Margin = new Padding(5, 0, 5, 0);
            lblFailVal.Name = "lblFailVal";
            lblFailVal.Size = new Size(56, 65);
            lblFailVal.TabIndex = 1;
            lblFailVal.Text = "3";
            // 
            // lblFailTitle
            // 
            lblFailTitle.AutoSize = true;
            lblFailTitle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblFailTitle.ForeColor = Color.FromArgb(244, 67, 54);
            lblFailTitle.Location = new Point(13, 13);
            lblFailTitle.Margin = new Padding(5, 0, 5, 0);
            lblFailTitle.Name = "lblFailTitle";
            lblFailTitle.Size = new Size(142, 30);
            lblFailTitle.TabIndex = 0;
            lblFailTitle.Text = "FAIL (1.40%)";
            // 
            // kpiYield
            // 
            kpiYield.BackColor = Color.FromArgb(248, 250, 252);
            kpiYield.Controls.Add(lblYieldVal);
            kpiYield.Controls.Add(lblYieldTitle);
            kpiYield.Dock = DockStyle.Fill;
            kpiYield.Location = new Point(1002, 6);
            kpiYield.Margin = new Padding(6);
            kpiYield.Name = "kpiYield";
            kpiYield.Size = new Size(320, 124);
            kpiYield.TabIndex = 3;
            // 
            // lblYieldVal
            // 
            lblYieldVal.AutoSize = true;
            lblYieldVal.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblYieldVal.ForeColor = Color.FromArgb(234, 179, 8);
            lblYieldVal.Location = new Point(13, 40);
            lblYieldVal.Margin = new Padding(5, 0, 5, 0);
            lblYieldVal.Name = "lblYieldVal";
            lblYieldVal.Size = new Size(195, 65);
            lblYieldVal.TabIndex = 1;
            lblYieldVal.Text = "98.60%";
            // 
            // lblYieldTitle
            // 
            lblYieldTitle.AutoSize = true;
            lblYieldTitle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblYieldTitle.ForeColor = Color.FromArgb(234, 179, 8);
            lblYieldTitle.Location = new Point(13, 13);
            lblYieldTitle.Margin = new Padding(5, 0, 5, 0);
            lblYieldTitle.Name = "lblYieldTitle";
            lblYieldTitle.Size = new Size(72, 30);
            lblYieldTitle.TabIndex = 0;
            lblYieldTitle.Text = "YIELD";
            // 
            // kpiCycleTime
            // 
            kpiCycleTime.BackColor = Color.FromArgb(248, 250, 252);
            kpiCycleTime.Controls.Add(lblCycleTimeVal);
            kpiCycleTime.Controls.Add(lblCycleTimeTitle);
            kpiCycleTime.Dock = DockStyle.Fill;
            kpiCycleTime.Location = new Point(1334, 6);
            kpiCycleTime.Margin = new Padding(6);
            kpiCycleTime.Name = "kpiCycleTime";
            kpiCycleTime.Size = new Size(321, 124);
            kpiCycleTime.TabIndex = 4;
            // 
            // lblCycleTimeVal
            // 
            lblCycleTimeVal.AutoSize = true;
            lblCycleTimeVal.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblCycleTimeVal.ForeColor = Color.FromArgb(14, 165, 233);
            lblCycleTimeVal.Location = new Point(13, 40);
            lblCycleTimeVal.Margin = new Padding(5, 0, 5, 0);
            lblCycleTimeVal.Name = "lblCycleTimeVal";
            lblCycleTimeVal.Size = new Size(208, 65);
            lblCycleTimeVal.TabIndex = 1;
            lblCycleTimeVal.Text = "16.2 sec";
            // 
            // lblCycleTimeTitle
            // 
            lblCycleTimeTitle.AutoSize = true;
            lblCycleTimeTitle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblCycleTimeTitle.ForeColor = Color.FromArgb(14, 165, 233);
            lblCycleTimeTitle.Location = new Point(13, 13);
            lblCycleTimeTitle.Margin = new Padding(5, 0, 5, 0);
            lblCycleTimeTitle.Name = "lblCycleTimeTitle";
            lblCycleTimeTitle.Size = new Size(186, 30);
            lblCycleTimeTitle.TabIndex = 0;
            lblCycleTimeTitle.Text = "AVG CYCLE TIME";
            // 
            // cmbTimeRange
            // 
            cmbTimeRange.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbTimeRange.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTimeRange.Font = new Font("Segoe UI", 9F);
            cmbTimeRange.FormattingEnabled = true;
            cmbTimeRange.Items.AddRange(new object[] { "Today" });
            cmbTimeRange.Location = new Point(1507, 21);
            cmbTimeRange.Margin = new Padding(5);
            cmbTimeRange.Name = "cmbTimeRange";
            cmbTimeRange.Size = new Size(176, 40);
            cmbTimeRange.TabIndex = 1;
            // 
            // lblSummaryTitle
            // 
            lblSummaryTitle.AutoSize = true;
            lblSummaryTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblSummaryTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblSummaryTitle.Location = new Point(24, 24);
            lblSummaryTitle.Margin = new Padding(5, 0, 5, 0);
            lblSummaryTitle.Name = "lblSummaryTitle";
            lblSummaryTitle.Size = new Size(410, 41);
            lblSummaryTitle.TabIndex = 0;
            lblSummaryTitle.Text = "SUMMARY REPORT (Today)";
            // 
            // cardHistory
            // 
            cardHistory.BackColor = Color.White;
            cardHistory.Controls.Add(gridRecent);
            cardHistory.Controls.Add(lnkViewAll);
            cardHistory.Controls.Add(lblRecentTitle);
            cardHistory.Dock = DockStyle.Fill;
            cardHistory.Location = new Point(1742, 10);
            cardHistory.Margin = new Padding(10);
            cardHistory.Name = "cardHistory";
            cardHistory.Padding = new Padding(24);
            cardHistory.Size = new Size(822, 890);
            cardHistory.TabIndex = 1;
            // 
            // gridRecent
            // 
            gridRecent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            gridRecent.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            gridRecent.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            gridRecent.DefaultCellStyle = dataGridViewCellStyle2;
            gridRecent.Location = new Point(24, 88);
            gridRecent.Margin = new Padding(5);
            gridRecent.Name = "gridRecent";
            gridRecent.RowHeadersWidth = 51;
            gridRecent.RowTemplate.Height = 35;
            gridRecent.Size = new Size(771, 776);
            gridRecent.TabIndex = 2;
            // 
            // lnkViewAll
            // 
            lnkViewAll.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lnkViewAll.AutoSize = true;
            lnkViewAll.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lnkViewAll.LinkColor = Color.FromArgb(37, 99, 235);
            lnkViewAll.Location = new Point(688, 29);
            lnkViewAll.Margin = new Padding(5, 0, 5, 0);
            lnkViewAll.Name = "lnkViewAll";
            lnkViewAll.Size = new Size(107, 32);
            lnkViewAll.TabIndex = 1;
            lnkViewAll.TabStop = true;
            lnkViewAll.Text = "View All";
            // 
            // lblRecentTitle
            // 
            lblRecentTitle.AutoSize = true;
            lblRecentTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblRecentTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblRecentTitle.Location = new Point(24, 24);
            lblRecentTitle.Margin = new Padding(5, 0, 5, 0);
            lblRecentTitle.Name = "lblRecentTitle";
            lblRecentTitle.Size = new Size(344, 41);
            lblRecentTitle.TabIndex = 0;
            lblRecentTitle.Text = "RECENT TEST HISTORY";
            // 
            // tblBottom
            // 
            tblBottom.ColumnCount = 3;
            tblBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 67.29F));
            tblBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18.06F));
            tblBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.65F));
            // tblBottom content moved
            tblBottom.Dock = DockStyle.Fill;
            tblBottom.Location = new Point(13, 1237);
            tblBottom.Margin = new Padding(13);
            tblBottom.Name = "tblBottom";
            tblBottom.RowCount = 1;
            tblBottom.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblBottom.Size = new Size(2574, 190);
            tblBottom.TabIndex = 2;
            // 
            // cardModules
            // 
            cardModules.BackColor = Color.White;
            cardModules.Controls.Add(tblModules);
            cardModules.Controls.Add(lblModulesTitle);
            cardModules.Dock = DockStyle.Fill;
            cardModules.Location = new Point(10, 10);
            cardModules.Margin = new Padding(10);
            cardModules.Name = "cardModules";
            cardModules.Padding = new Padding(24);
            cardModules.Size = new Size(1712, 170);
            cardModules.TabIndex = 0;
            // 
            // tblModules
            // 
            tblModules.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tblModules.ColumnCount = 4;
            tblModules.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblModules.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblModules.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblModules.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblModules.Controls.Add(pnlMod1, 0, 0);
            tblModules.Controls.Add(pnlMod2, 1, 0);
            tblModules.Controls.Add(pnlMod3, 2, 0);
            tblModules.Controls.Add(pnlMod4, 3, 0);
            tblModules.Location = new Point(24, 61);
            tblModules.Margin = new Padding(5);
            tblModules.Name = "tblModules";
            tblModules.RowCount = 1;
            tblModules.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblModules.Size = new Size(1661, 83);
            tblModules.TabIndex = 1;
            // 
            // pnlMod1
            // 
            pnlMod1.Controls.Add(lblMod1Status);
            pnlMod1.Controls.Add(lblMod1Title);
            pnlMod1.Dock = DockStyle.Fill;
            pnlMod1.Location = new Point(5, 5);
            pnlMod1.Margin = new Padding(5);
            pnlMod1.Name = "pnlMod1";
            pnlMod1.Size = new Size(405, 73);
            pnlMod1.TabIndex = 0;
            // 
            // lblMod1Status
            // 
            lblMod1Status.AutoSize = true;
            lblMod1Status.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMod1Status.ForeColor = Color.FromArgb(76, 175, 80);
            lblMod1Status.Location = new Point(5, 35);
            lblMod1Status.Margin = new Padding(5, 0, 5, 0);
            lblMod1Status.Name = "lblMod1Status";
            lblMod1Status.Size = new Size(110, 32);
            lblMod1Status.TabIndex = 1;
            lblMod1Status.Text = " ONLINE";
            // 
            // lblMod1Title
            // 
            lblMod1Title.AutoSize = true;
            lblMod1Title.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMod1Title.ForeColor = Color.FromArgb(71, 85, 105);
            lblMod1Title.Location = new Point(5, 3);
            lblMod1Title.Margin = new Padding(5, 0, 5, 0);
            lblMod1Title.Name = "lblMod1Title";
            lblMod1Title.Size = new Size(157, 32);
            lblMod1Title.TabIndex = 0;
            lblMod1Title.Text = "Relay Matrix";
            // 
            // pnlMod2
            // 
            pnlMod2.Controls.Add(lblMod2Status);
            pnlMod2.Controls.Add(lblMod2Title);
            pnlMod2.Dock = DockStyle.Fill;
            pnlMod2.Location = new Point(420, 5);
            pnlMod2.Margin = new Padding(5);
            pnlMod2.Name = "pnlMod2";
            pnlMod2.Size = new Size(405, 73);
            pnlMod2.TabIndex = 1;
            // 
            // lblMod2Status
            // 
            lblMod2Status.AutoSize = true;
            lblMod2Status.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMod2Status.ForeColor = Color.FromArgb(76, 175, 80);
            lblMod2Status.Location = new Point(5, 35);
            lblMod2Status.Margin = new Padding(5, 0, 5, 0);
            lblMod2Status.Name = "lblMod2Status";
            lblMod2Status.Size = new Size(110, 32);
            lblMod2Status.TabIndex = 1;
            lblMod2Status.Text = " ONLINE";
            // 
            // lblMod2Title
            // 
            lblMod2Title.AutoSize = true;
            lblMod2Title.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMod2Title.ForeColor = Color.FromArgb(71, 85, 105);
            lblMod2Title.Location = new Point(5, 3);
            lblMod2Title.Margin = new Padding(5, 0, 5, 0);
            lblMod2Title.Name = "lblMod2Title";
            lblMod2Title.Size = new Size(170, 32);
            lblMod2Title.TabIndex = 0;
            lblMod2Title.Text = "Power Supply";
            // 
            // pnlMod3
            // 
            pnlMod3.Controls.Add(lblMod3Status);
            pnlMod3.Controls.Add(lblMod3Title);
            pnlMod3.Dock = DockStyle.Fill;
            pnlMod3.Location = new Point(835, 5);
            pnlMod3.Margin = new Padding(5);
            pnlMod3.Name = "pnlMod3";
            pnlMod3.Size = new Size(405, 73);
            pnlMod3.TabIndex = 2;
            // 
            // lblMod3Status
            // 
            lblMod3Status.AutoSize = true;
            lblMod3Status.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMod3Status.ForeColor = Color.FromArgb(76, 175, 80);
            lblMod3Status.Location = new Point(5, 35);
            lblMod3Status.Margin = new Padding(5, 0, 5, 0);
            lblMod3Status.Name = "lblMod3Status";
            lblMod3Status.Size = new Size(110, 32);
            lblMod3Status.TabIndex = 1;
            lblMod3Status.Text = " ONLINE";
            // 
            // lblMod3Title
            // 
            lblMod3Title.AutoSize = true;
            lblMod3Title.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMod3Title.ForeColor = Color.FromArgb(71, 85, 105);
            lblMod3Title.Location = new Point(5, 3);
            lblMod3Title.Margin = new Padding(5, 0, 5, 0);
            lblMod3Title.Name = "lblMod3Title";
            lblMod3Title.Size = new Size(178, 32);
            lblMod3Title.TabIndex = 0;
            lblMod3Title.Text = "PLC Controller";
            // 
            // pnlMod4
            // 
            pnlMod4.Controls.Add(lblMod4Status);
            pnlMod4.Controls.Add(lblMod4Title);
            pnlMod4.Dock = DockStyle.Fill;
            pnlMod4.Location = new Point(1250, 5);
            pnlMod4.Margin = new Padding(5);
            pnlMod4.Name = "pnlMod4";
            pnlMod4.Size = new Size(406, 73);
            pnlMod4.TabIndex = 3;
            // 
            // lblMod4Status
            // 
            lblMod4Status.AutoSize = true;
            lblMod4Status.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMod4Status.ForeColor = Color.FromArgb(76, 175, 80);
            lblMod4Status.Location = new Point(5, 35);
            lblMod4Status.Margin = new Padding(5, 0, 5, 0);
            lblMod4Status.Name = "lblMod4Status";
            lblMod4Status.Size = new Size(110, 32);
            lblMod4Status.TabIndex = 1;
            lblMod4Status.Text = " ONLINE";
            // 
            // lblMod4Title
            // 
            lblMod4Title.AutoSize = true;
            lblMod4Title.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMod4Title.ForeColor = Color.FromArgb(71, 85, 105);
            lblMod4Title.Location = new Point(5, 3);
            lblMod4Title.Margin = new Padding(5, 0, 5, 0);
            lblMod4Title.Name = "lblMod4Title";
            lblMod4Title.Size = new Size(146, 32);
            lblMod4Title.TabIndex = 0;
            lblMod4Title.Text = "I/O Module";
            // 
            // lblModulesTitle
            // 
            lblModulesTitle.AutoSize = true;
            lblModulesTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblModulesTitle.ForeColor = Color.FromArgb(51, 65, 85);
            lblModulesTitle.Location = new Point(24, 19);
            lblModulesTitle.Margin = new Padding(5, 0, 5, 0);
            lblModulesTitle.Name = "lblModulesTitle";
            lblModulesTitle.Size = new Size(344, 37);
            lblModulesTitle.TabIndex = 0;
            lblModulesTitle.Text = "SYSTEM MODULE STATUS";
            // 
            // cardOperatingTime
            // 
            cardOperatingTime.BackColor = Color.White;
            cardOperatingTime.Controls.Add(lblOpTimeSub);
            cardOperatingTime.Controls.Add(lblOpTimeVal);
            cardOperatingTime.Controls.Add(lblOpTimeTitle);
            cardOperatingTime.Dock = DockStyle.Fill;
            cardOperatingTime.Location = new Point(1742, 10);
            cardOperatingTime.Margin = new Padding(10);
            cardOperatingTime.Name = "cardOperatingTime";
            cardOperatingTime.Padding = new Padding(24);
            cardOperatingTime.Size = new Size(444, 170);
            cardOperatingTime.TabIndex = 1;
            // 
            // lblOpTimeSub
            // 
            lblOpTimeSub.AutoSize = true;
            lblOpTimeSub.Font = new Font("Segoe UI", 9F);
            lblOpTimeSub.ForeColor = Color.FromArgb(100, 116, 139);
            lblOpTimeSub.Location = new Point(24, 120);
            lblOpTimeSub.Margin = new Padding(5, 0, 5, 0);
            lblOpTimeSub.Name = "lblOpTimeSub";
            lblOpTimeSub.Size = new Size(156, 32);
            lblOpTimeSub.TabIndex = 2;
            lblOpTimeSub.Text = "HH : MM : SS";
            // 
            // lblOpTimeVal
            // 
            lblOpTimeVal.AutoSize = true;
            lblOpTimeVal.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblOpTimeVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblOpTimeVal.Location = new Point(20, 51);
            lblOpTimeVal.Margin = new Padding(5, 0, 5, 0);
            lblOpTimeVal.Name = "lblOpTimeVal";
            lblOpTimeVal.Size = new Size(306, 72);
            lblOpTimeVal.TabIndex = 1;
            lblOpTimeVal.Text = "03 : 46 : 22";
            // 
            // lblOpTimeTitle
            // 
            lblOpTimeTitle.AutoSize = true;
            lblOpTimeTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblOpTimeTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblOpTimeTitle.Location = new Point(24, 19);
            lblOpTimeTitle.Margin = new Padding(5, 0, 5, 0);
            lblOpTimeTitle.Name = "lblOpTimeTitle";
            lblOpTimeTitle.Size = new Size(385, 32);
            lblOpTimeTitle.TabIndex = 0;
            lblOpTimeTitle.Text = "TOTAL OPERATING TIME (Today)";
            // 
            // cardMaintenance
            // 
            cardMaintenance.BackColor = Color.White;
            cardMaintenance.Controls.Add(lblMaintSub);
            cardMaintenance.Controls.Add(lblMaintVal);
            cardMaintenance.Controls.Add(lblMaintTitle);
            cardMaintenance.Dock = DockStyle.Fill;
            cardMaintenance.Location = new Point(2206, 10);
            cardMaintenance.Margin = new Padding(10);
            cardMaintenance.Name = "cardMaintenance";
            cardMaintenance.Padding = new Padding(24);
            cardMaintenance.Size = new Size(358, 170);
            cardMaintenance.TabIndex = 2;
            // 
            // lblMaintSub
            // 
            lblMaintSub.AutoSize = true;
            lblMaintSub.Font = new Font("Segoe UI", 9F);
            lblMaintSub.ForeColor = Color.FromArgb(100, 116, 139);
            lblMaintSub.Location = new Point(24, 120);
            lblMaintSub.Margin = new Padding(5, 0, 5, 0);
            lblMaintSub.Name = "lblMaintSub";
            lblMaintSub.Size = new Size(174, 32);
            lblMaintSub.TabIndex = 2;
            lblMaintSub.Text = "Or 6,500 Hours";
            // 
            // lblMaintVal
            // 
            lblMaintVal.AutoSize = true;
            lblMaintVal.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblMaintVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblMaintVal.Location = new Point(20, 51);
            lblMaintVal.Margin = new Padding(5, 0, 5, 0);
            lblMaintVal.Name = "lblMaintVal";
            lblMaintVal.Size = new Size(260, 72);
            lblMaintVal.TabIndex = 1;
            lblMaintVal.Text = "145 Days";
            // 
            // lblMaintTitle
            // 
            lblMaintTitle.AutoSize = true;
            lblMaintTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMaintTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblMaintTitle.Location = new Point(24, 19);
            lblMaintTitle.Margin = new Padding(5, 0, 5, 0);
            lblMaintTitle.Name = "lblMaintTitle";
            lblMaintTitle.Size = new Size(223, 32);
            lblMaintTitle.TabIndex = 0;
            lblMaintTitle.Text = "Next Maintenance";
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 245, 247);
            Controls.Add(tblMain);
            Font = new Font("Segoe UI", 9F);
            Margin = new Padding(0);
            Name = "Dashboard";
            Size = new Size(2600, 1440);
            tblMain.ResumeLayout(false);
            tblTop.ResumeLayout(false);
            cardTester.ResumeLayout(false);
            cardTester.PerformLayout();
            cardMachine.ResumeLayout(false);
            cardMachine.PerformLayout();
            cardDatabase.ResumeLayout(false);
            cardDatabase.PerformLayout();
            cardActiveProg.ResumeLayout(false);
            cardActiveProg.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picHarness).EndInit();
            tblMiddle.ResumeLayout(false);
            cardSummary.ResumeLayout(false);
            cardSummary.PerformLayout();
            tblCharts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chartHistory).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartFailReason).EndInit();
            tblKpi.ResumeLayout(false);
            kpiTotalTested.ResumeLayout(false);
            kpiTotalTested.PerformLayout();
            kpiPass.ResumeLayout(false);
            kpiPass.PerformLayout();
            kpiFail.ResumeLayout(false);
            kpiFail.PerformLayout();
            kpiYield.ResumeLayout(false);
            kpiYield.PerformLayout();
            kpiCycleTime.ResumeLayout(false);
            kpiCycleTime.PerformLayout();
            cardHistory.ResumeLayout(false);
            cardHistory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridRecent).EndInit();
            tblBottom.ResumeLayout(false);
            cardModules.ResumeLayout(false);
            cardModules.PerformLayout();
            tblModules.ResumeLayout(false);
            pnlMod1.ResumeLayout(false);
            pnlMod1.PerformLayout();
            pnlMod2.ResumeLayout(false);
            pnlMod2.PerformLayout();
            pnlMod3.ResumeLayout(false);
            pnlMod3.PerformLayout();
            pnlMod4.ResumeLayout(false);
            pnlMod4.PerformLayout();
            cardOperatingTime.ResumeLayout(false);
            cardOperatingTime.PerformLayout();
            cardMaintenance.ResumeLayout(false);
            cardMaintenance.PerformLayout();
            ResumeLayout(false);



        }



        #endregion



        private System.Windows.Forms.TableLayoutPanel tblMain;

        private System.Windows.Forms.TableLayoutPanel tblTop;

        private System.Windows.Forms.Panel cardTester;

        private System.Windows.Forms.Label lblTesterSub;

        private System.Windows.Forms.Label lblTesterStatus;

        private System.Windows.Forms.Label lblTesterTitle;

        private System.Windows.Forms.Panel cardMachine;

        private System.Windows.Forms.Label lblMachineSub;

        private System.Windows.Forms.Label lblMachineStatus;

        private System.Windows.Forms.Label lblMachineTitle;

        private System.Windows.Forms.Panel cardDatabase;

        private System.Windows.Forms.Label lblDatabaseSub;

        private System.Windows.Forms.Label lblDatabaseStatus;

        private System.Windows.Forms.Label lblDatabaseTitle;

        private System.Windows.Forms.Panel cardActiveProg;

        private System.Windows.Forms.Button btnChangeProg;

        private System.Windows.Forms.PictureBox picHarness;

        private System.Windows.Forms.Label lblProgDetails;

        private System.Windows.Forms.Label lblActiveProgTitle;

        private System.Windows.Forms.TableLayoutPanel tblMiddle;

        private System.Windows.Forms.Panel cardSummary;

        private System.Windows.Forms.ComboBox cmbTimeRange;

        private System.Windows.Forms.Label lblSummaryTitle;

        private System.Windows.Forms.TableLayoutPanel tblKpi;

        private System.Windows.Forms.Panel kpiTotalTested;

        private System.Windows.Forms.Label lblTotalTestedVal;

        private System.Windows.Forms.Label lblTotalTestedTitle;

        private System.Windows.Forms.Panel kpiPass;

        private System.Windows.Forms.Label lblPassVal;

        private System.Windows.Forms.Label lblPassTitle;

        private System.Windows.Forms.Panel kpiFail;

        private System.Windows.Forms.Label lblFailVal;

        private System.Windows.Forms.Label lblFailTitle;

        private System.Windows.Forms.Panel kpiYield;

        private System.Windows.Forms.Label lblYieldVal;

        private System.Windows.Forms.Label lblYieldTitle;

        private System.Windows.Forms.Panel kpiCycleTime;

        private System.Windows.Forms.Label lblCycleTimeVal;

        private System.Windows.Forms.Label lblCycleTimeTitle;

        private System.Windows.Forms.TableLayoutPanel tblCharts;

        private System.Windows.Forms.DataVisualization.Charting.Chart chartHistory;

        private System.Windows.Forms.DataVisualization.Charting.Chart chartFailReason;

        private System.Windows.Forms.Panel cardHistory;

        private System.Windows.Forms.DataGridView gridRecent;

        private System.Windows.Forms.LinkLabel lnkViewAll;

        private System.Windows.Forms.Label lblRecentTitle;

        private System.Windows.Forms.TableLayoutPanel tblBottom;

        private System.Windows.Forms.Panel cardModules;

        private System.Windows.Forms.TableLayoutPanel tblModules;

        private System.Windows.Forms.Panel pnlMod1;

        private System.Windows.Forms.Label lblMod1Status;

        private System.Windows.Forms.Label lblMod1Title;

        private System.Windows.Forms.Panel pnlMod2;

        private System.Windows.Forms.Label lblMod2Status;

        private System.Windows.Forms.Label lblMod2Title;

        private System.Windows.Forms.Panel pnlMod3;

        private System.Windows.Forms.Label lblMod3Status;

        private System.Windows.Forms.Label lblMod3Title;

        private System.Windows.Forms.Panel pnlMod4;

        private System.Windows.Forms.Label lblMod4Status;

        private System.Windows.Forms.Label lblMod4Title;

        private System.Windows.Forms.Label lblModulesTitle;

        private System.Windows.Forms.Panel cardOperatingTime;

        private System.Windows.Forms.Label lblOpTimeSub;

        private System.Windows.Forms.Label lblOpTimeVal;

        private System.Windows.Forms.Label lblOpTimeTitle;

        private System.Windows.Forms.Panel cardMaintenance;

        private System.Windows.Forms.Label lblMaintSub;

        private System.Windows.Forms.Label lblMaintVal;

        private System.Windows.Forms.Label lblMaintTitle;

    }

}