namespace AssemblyLineTester
{
    partial class Dashboard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            tblMain = new TableLayoutPanel();
            tblTop = new TableLayoutPanel();
            cardTester = new Panel();
            lblTesterTitle = new Label();
            lblTesterStatus = new Label();
            cardMachine = new Panel();
            lblMachineTitle = new Label();
            lblMachineStatus = new Label();
            cardActiveProg = new Panel();
            lblActiveProgTitle = new Label();
            lblProgDetails = new Label();
            btnChangeProg = new Button();
            cardDatabase = new Panel();
            lblDatabaseTitle = new Label();
            lblDatabaseStatus = new Label();
            tblMiddle = new TableLayoutPanel();
            cardSummary = new Panel();
            lblSummaryTitle = new Label();
            cmbSummaryDate = new ComboBox();
            tblSummaryContent = new TableLayoutPanel();
            tblKpiCards = new TableLayoutPanel();
            pnlKpiPass = new Panel();
            lblKpiPassNum = new Label();
            lblKpiPassText = new Label();
            pnlKpiFail = new Panel();
            lblKpiFailNum = new Label();
            lblKpiFailText = new Label();
            pnlKpiYield = new Panel();
            lblKpiYieldNum = new Label();
            lblKpiYieldText = new Label();
            pnlKpiTime = new Panel();
            lblKpiTimeNum = new Label();
            lblKpiTimeText = new Label();
            pnlKpiTotal = new Panel();
            lblKpiTotalNum = new Label();
            lblKpiTotalText = new Label();
            tblCharts = new TableLayoutPanel();
            pnlBarChart = new Panel();
            lblBarChartMock = new Label();
            pnlDonutChart = new Panel();
            lblDonutTitle = new Label();
            lblDonutLegendMock = new Label();
            cardRecent = new Panel();
            lblRecentTitle = new Label();
            gridRecent = new DataGridView();
            tblBottom = new TableLayoutPanel();
            cardSystemMod = new Panel();
            lblSystemModTitle = new Label();
            tblModuleGrid = new TableLayoutPanel();
            pnlRelay = new Panel();
            lblIconRelay = new Label();
            lblTitleRelay = new Label();
            lblStatusRelay = new Label();
            pnlPower = new Panel();
            lblIconPower = new Label();
            lblTitlePower = new Label();
            lblStatusPower = new Label();
            pnlPLC = new Panel();
            lblIconPLC = new Label();
            lblTitlePLC = new Label();
            lblStatusPLC = new Label();
            pnlIO = new Panel();
            lblIconIO = new Label();
            lblTitleIO = new Label();
            lblStatusIO = new Label();
            cardOpTime = new Panel();
            lblOpTime = new Label();
            lblTimeValue = new Label();
            cardNextMaint = new Panel();
            lblNextMaintTitle = new Label();
            lblNextMaintValue = new Label();
            lblNextMaintSub = new Label();
            tblMain.SuspendLayout();
            tblTop.SuspendLayout();
            cardTester.SuspendLayout();
            cardMachine.SuspendLayout();
            cardActiveProg.SuspendLayout();
            cardDatabase.SuspendLayout();
            tblMiddle.SuspendLayout();
            cardSummary.SuspendLayout();
            tblSummaryContent.SuspendLayout();
            tblKpiCards.SuspendLayout();
            pnlKpiPass.SuspendLayout();
            pnlKpiFail.SuspendLayout();
            pnlKpiYield.SuspendLayout();
            pnlKpiTime.SuspendLayout();
            pnlKpiTotal.SuspendLayout();
            tblCharts.SuspendLayout();
            pnlBarChart.SuspendLayout();
            pnlDonutChart.SuspendLayout();
            cardRecent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridRecent).BeginInit();
            tblBottom.SuspendLayout();
            cardSystemMod.SuspendLayout();
            tblModuleGrid.SuspendLayout();
            pnlRelay.SuspendLayout();
            pnlPower.SuspendLayout();
            pnlPLC.SuspendLayout();
            pnlIO.SuspendLayout();
            cardOpTime.SuspendLayout();
            cardNextMaint.SuspendLayout();
            SuspendLayout();
            // 
            // tblMain
            // 
            tblMain.ColumnCount = 1;
            tblMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblMain.Controls.Add(tblTop, 0, 0);
            tblMain.Controls.Add(tblMiddle, 0, 1);
            tblMain.Controls.Add(tblBottom, 0, 2);
            tblMain.Dock = DockStyle.Fill;
            tblMain.Location = new Point(0, 0);
            tblMain.Name = "tblMain";
            tblMain.Padding = new Padding(15);
            tblMain.RowCount = 3;
            tblMain.RowStyles.Add(new RowStyle(SizeType.Percent, 22F));
            tblMain.RowStyles.Add(new RowStyle(SizeType.Percent, 63F));
            tblMain.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tblMain.Size = new Size(2204, 1652);
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
            tblTop.Controls.Add(cardActiveProg, 3, 0);
            tblTop.Controls.Add(cardDatabase, 2, 0);
            tblTop.Dock = DockStyle.Fill;
            tblTop.Location = new Point(15, 15);
            tblTop.Margin = new Padding(0);
            tblTop.Name = "tblTop";
            tblTop.RowCount = 1;
            tblTop.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblTop.Size = new Size(2174, 356);
            tblTop.TabIndex = 0;
            // 
            // cardTester
            // 
            cardTester.BackColor = Color.White;
            cardTester.Controls.Add(lblTesterTitle);
            cardTester.Controls.Add(lblTesterStatus);
            cardTester.Dock = DockStyle.Fill;
            cardTester.Location = new Point(5, 5);
            cardTester.Margin = new Padding(5);
            cardTester.Name = "cardTester";
            cardTester.Size = new Size(468, 346);
            cardTester.TabIndex = 0;
            // 
            // lblTesterTitle
            // 
            lblTesterTitle.AutoSize = true;
            lblTesterTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTesterTitle.Location = new Point(20, 20);
            lblTesterTitle.Name = "lblTesterTitle";
            lblTesterTitle.Size = new Size(256, 45);
            lblTesterTitle.TabIndex = 0;
            lblTesterTitle.Text = "TESTER STATUS";
            // 
            // lblTesterStatus
            // 
            lblTesterStatus.AutoSize = true;
            lblTesterStatus.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTesterStatus.ForeColor = Color.Green;
            lblTesterStatus.Location = new Point(20, 70);
            lblTesterStatus.Name = "lblTesterStatus";
            lblTesterStatus.Size = new Size(195, 51);
            lblTesterStatus.TabIndex = 1;
            lblTesterStatus.Text = "● ONLINE";
            // 
            // cardMachine
            // 
            cardMachine.BackColor = Color.White;
            cardMachine.Controls.Add(lblMachineTitle);
            cardMachine.Controls.Add(lblMachineStatus);
            cardMachine.Dock = DockStyle.Fill;
            cardMachine.Location = new Point(483, 5);
            cardMachine.Margin = new Padding(5);
            cardMachine.Name = "cardMachine";
            cardMachine.Size = new Size(468, 346);
            cardMachine.TabIndex = 1;
            // 
            // lblMachineTitle
            // 
            lblMachineTitle.AutoSize = true;
            lblMachineTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblMachineTitle.Location = new Point(20, 20);
            lblMachineTitle.Name = "lblMachineTitle";
            lblMachineTitle.Size = new Size(296, 45);
            lblMachineTitle.TabIndex = 0;
            lblMachineTitle.Text = "MACHINE STATUS";
            // 
            // lblMachineStatus
            // 
            lblMachineStatus.AutoSize = true;
            lblMachineStatus.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblMachineStatus.ForeColor = Color.Green;
            lblMachineStatus.Location = new Point(20, 70);
            lblMachineStatus.Name = "lblMachineStatus";
            lblMachineStatus.Size = new Size(179, 51);
            lblMachineStatus.TabIndex = 1;
            lblMachineStatus.Text = "● READY";
            // 
            // cardActiveProg
            // 
            cardActiveProg.BackColor = Color.White;
            cardActiveProg.Controls.Add(lblActiveProgTitle);
            cardActiveProg.Controls.Add(lblProgDetails);
            cardActiveProg.Controls.Add(btnChangeProg);
            cardActiveProg.Dock = DockStyle.Fill;
            cardActiveProg.Location = new Point(1439, 5);
            cardActiveProg.Margin = new Padding(5);
            cardActiveProg.Name = "cardActiveProg";
            cardActiveProg.Size = new Size(730, 346);
            cardActiveProg.TabIndex = 3;
            // 
            // lblActiveProgTitle
            // 
            lblActiveProgTitle.AutoSize = true;
            lblActiveProgTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblActiveProgTitle.Location = new Point(20, 20);
            lblActiveProgTitle.Name = "lblActiveProgTitle";
            lblActiveProgTitle.Size = new Size(302, 45);
            lblActiveProgTitle.TabIndex = 0;
            lblActiveProgTitle.Text = "ACTIVE PROGRAM";
            // 
            // lblProgDetails
            // 
            lblProgDetails.AutoSize = true;
            lblProgDetails.Font = new Font("Segoe UI", 9F);
            lblProgDetails.Location = new Point(20, 75);
            lblProgDetails.Name = "lblProgDetails";
            lblProgDetails.Size = new Size(312, 160);
            lblProgDetails.TabIndex = 1;
            lblProgDetails.Text = "Product Model : 6060\nProduct Type : AC\nArea : Engine Control\nPart Number : WH-ENG-001\nECN No. : 123.R001";
            // 
            // btnChangeProg
            // 
            btnChangeProg.BackColor = Color.FromArgb(255, 193, 7);
            btnChangeProg.FlatAppearance.BorderSize = 0;
            btnChangeProg.FlatStyle = FlatStyle.Flat;
            btnChangeProg.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnChangeProg.Location = new Point(522, 272);
            btnChangeProg.Name = "btnChangeProg";
            btnChangeProg.Size = new Size(180, 50);
            btnChangeProg.TabIndex = 2;
            btnChangeProg.Text = "Change Program";
            btnChangeProg.UseVisualStyleBackColor = false;
            // 
            // cardDatabase
            // 
            cardDatabase.BackColor = Color.White;
            cardDatabase.Controls.Add(lblDatabaseTitle);
            cardDatabase.Controls.Add(lblDatabaseStatus);
            cardDatabase.Dock = DockStyle.Fill;
            cardDatabase.Location = new Point(961, 5);
            cardDatabase.Margin = new Padding(5);
            cardDatabase.Name = "cardDatabase";
            cardDatabase.Size = new Size(468, 346);
            cardDatabase.TabIndex = 2;
            // 
            // lblDatabaseTitle
            // 
            lblDatabaseTitle.AutoSize = true;
            lblDatabaseTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblDatabaseTitle.Location = new Point(20, 20);
            lblDatabaseTitle.Name = "lblDatabaseTitle";
            lblDatabaseTitle.Size = new Size(309, 45);
            lblDatabaseTitle.TabIndex = 0;
            lblDatabaseTitle.Text = "DATABASE STATUS";
            // 
            // lblDatabaseStatus
            // 
            lblDatabaseStatus.AutoSize = true;
            lblDatabaseStatus.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblDatabaseStatus.ForeColor = Color.Green;
            lblDatabaseStatus.Location = new Point(20, 70);
            lblDatabaseStatus.Name = "lblDatabaseStatus";
            lblDatabaseStatus.Size = new Size(282, 51);
            lblDatabaseStatus.TabIndex = 1;
            lblDatabaseStatus.Text = "● CONNECTED";
            // 
            // tblMiddle
            // 
            tblMiddle.ColumnCount = 2;
            tblMiddle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tblMiddle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tblMiddle.Controls.Add(cardSummary, 0, 0);
            tblMiddle.Controls.Add(cardRecent, 1, 0);
            tblMiddle.Dock = DockStyle.Fill;
            tblMiddle.Location = new Point(15, 371);
            tblMiddle.Margin = new Padding(0);
            tblMiddle.Name = "tblMiddle";
            tblMiddle.RowCount = 1;
            tblMiddle.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblMiddle.Size = new Size(2174, 1021);
            tblMiddle.TabIndex = 1;
            // 
            // cardSummary
            // 
            cardSummary.BackColor = Color.White;
            cardSummary.Controls.Add(lblSummaryTitle);
            cardSummary.Controls.Add(cmbSummaryDate);
            cardSummary.Controls.Add(tblSummaryContent);
            cardSummary.Dock = DockStyle.Fill;
            cardSummary.Location = new Point(5, 5);
            cardSummary.Margin = new Padding(5);
            cardSummary.Name = "cardSummary";
            cardSummary.Padding = new Padding(20, 70, 20, 20);
            cardSummary.Size = new Size(1185, 1011);
            cardSummary.TabIndex = 0;
            // 
            // lblSummaryTitle
            // 
            lblSummaryTitle.AutoSize = true;
            lblSummaryTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblSummaryTitle.Location = new Point(33, 9);
            lblSummaryTitle.Name = "lblSummaryTitle";
            lblSummaryTitle.Size = new Size(514, 51);
            lblSummaryTitle.TabIndex = 0;
            lblSummaryTitle.Text = "SUMMARY REPORT (Today)";
            // 
            // cmbSummaryDate
            // 
            cmbSummaryDate.FormattingEnabled = true;
            cmbSummaryDate.Items.AddRange(new object[] { "Today", "This Week", "This Month" });
            cmbSummaryDate.Location = new Point(1404, 20);
            cmbSummaryDate.Name = "cmbSummaryDate";
            cmbSummaryDate.Size = new Size(150, 40);
            cmbSummaryDate.TabIndex = 1;
            cmbSummaryDate.Text = "Today";
            // 
            // tblSummaryContent
            // 
            tblSummaryContent.ColumnCount = 1;
            tblSummaryContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblSummaryContent.Controls.Add(tblKpiCards, 0, 0);
            tblSummaryContent.Controls.Add(tblCharts, 0, 1);
            tblSummaryContent.Dock = DockStyle.Fill;
            tblSummaryContent.Location = new Point(20, 70);
            tblSummaryContent.Name = "tblSummaryContent";
            tblSummaryContent.RowCount = 2;
            tblSummaryContent.RowStyles.Add(new RowStyle(SizeType.Percent, 35F));
            tblSummaryContent.RowStyles.Add(new RowStyle(SizeType.Percent, 65F));
            tblSummaryContent.Size = new Size(1145, 921);
            tblSummaryContent.TabIndex = 2;
            // 
            // tblKpiCards
            // 
            tblKpiCards.ColumnCount = 5;
            tblKpiCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tblKpiCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tblKpiCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tblKpiCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tblKpiCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tblKpiCards.Controls.Add(pnlKpiPass, 1, 0);
            tblKpiCards.Controls.Add(pnlKpiFail, 2, 0);
            tblKpiCards.Controls.Add(pnlKpiYield, 3, 0);
            tblKpiCards.Controls.Add(pnlKpiTime, 4, 0);
            tblKpiCards.Controls.Add(pnlKpiTotal, 0, 0);
            tblKpiCards.Dock = DockStyle.Fill;
            tblKpiCards.Location = new Point(3, 3);
            tblKpiCards.Name = "tblKpiCards";
            tblKpiCards.RowCount = 1;
            tblKpiCards.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblKpiCards.Size = new Size(1139, 316);
            tblKpiCards.TabIndex = 0;
            // 
            // pnlKpiPass
            // 
            pnlKpiPass.BackColor = Color.White;
            pnlKpiPass.BorderStyle = BorderStyle.FixedSingle;
            pnlKpiPass.Controls.Add(lblKpiPassNum);
            pnlKpiPass.Controls.Add(lblKpiPassText);
            pnlKpiPass.Dock = DockStyle.Fill;
            pnlKpiPass.Location = new Point(232, 5);
            pnlKpiPass.Margin = new Padding(5);
            pnlKpiPass.Name = "pnlKpiPass";
            pnlKpiPass.Size = new Size(217, 306);
            pnlKpiPass.TabIndex = 0;
            // 
            // lblKpiPassNum
            // 
            lblKpiPassNum.AutoSize = true;
            lblKpiPassNum.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblKpiPassNum.Location = new Point(10, 20);
            lblKpiPassNum.Name = "lblKpiPassNum";
            lblKpiPassNum.Size = new Size(100, 59);
            lblKpiPassNum.TabIndex = 0;
            lblKpiPassNum.Text = "212";
            // 
            // lblKpiPassText
            // 
            lblKpiPassText.AutoSize = true;
            lblKpiPassText.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblKpiPassText.ForeColor = Color.ForestGreen;
            lblKpiPassText.Location = new Point(10, 79);
            lblKpiPassText.Name = "lblKpiPassText";
            lblKpiPassText.Size = new Size(131, 74);
            lblKpiPassText.TabIndex = 1;
            lblKpiPassText.Text = "PASS\n(98.60%)";
            // 
            // pnlKpiFail
            // 
            pnlKpiFail.BackColor = Color.White;
            pnlKpiFail.BorderStyle = BorderStyle.FixedSingle;
            pnlKpiFail.Controls.Add(lblKpiFailNum);
            pnlKpiFail.Controls.Add(lblKpiFailText);
            pnlKpiFail.Dock = DockStyle.Fill;
            pnlKpiFail.Location = new Point(459, 5);
            pnlKpiFail.Margin = new Padding(5);
            pnlKpiFail.Name = "pnlKpiFail";
            pnlKpiFail.Size = new Size(217, 306);
            pnlKpiFail.TabIndex = 1;
            // 
            // lblKpiFailNum
            // 
            lblKpiFailNum.AutoSize = true;
            lblKpiFailNum.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblKpiFailNum.Location = new Point(10, 20);
            lblKpiFailNum.Name = "lblKpiFailNum";
            lblKpiFailNum.Size = new Size(50, 59);
            lblKpiFailNum.TabIndex = 0;
            lblKpiFailNum.Text = "3";
            // 
            // lblKpiFailText
            // 
            lblKpiFailText.AutoSize = true;
            lblKpiFailText.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblKpiFailText.ForeColor = Color.Crimson;
            lblKpiFailText.Location = new Point(10, 79);
            lblKpiFailText.Name = "lblKpiFailText";
            lblKpiFailText.Size = new Size(115, 74);
            lblKpiFailText.TabIndex = 1;
            lblKpiFailText.Text = "FAIL\n(1.40%)";
            // 
            // pnlKpiYield
            // 
            pnlKpiYield.BackColor = Color.White;
            pnlKpiYield.BorderStyle = BorderStyle.FixedSingle;
            pnlKpiYield.Controls.Add(lblKpiYieldNum);
            pnlKpiYield.Controls.Add(lblKpiYieldText);
            pnlKpiYield.Dock = DockStyle.Fill;
            pnlKpiYield.Location = new Point(686, 5);
            pnlKpiYield.Margin = new Padding(5);
            pnlKpiYield.Name = "pnlKpiYield";
            pnlKpiYield.Size = new Size(217, 306);
            pnlKpiYield.TabIndex = 2;
            // 
            // lblKpiYieldNum
            // 
            lblKpiYieldNum.AutoSize = true;
            lblKpiYieldNum.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblKpiYieldNum.Location = new Point(10, 25);
            lblKpiYieldNum.Name = "lblKpiYieldNum";
            lblKpiYieldNum.Size = new Size(153, 51);
            lblKpiYieldNum.TabIndex = 0;
            lblKpiYieldNum.Text = "98.60%";
            // 
            // lblKpiYieldText
            // 
            lblKpiYieldText.AutoSize = true;
            lblKpiYieldText.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblKpiYieldText.ForeColor = Color.Goldenrod;
            lblKpiYieldText.Location = new Point(10, 79);
            lblKpiYieldText.Name = "lblKpiYieldText";
            lblKpiYieldText.Size = new Size(90, 37);
            lblKpiYieldText.TabIndex = 1;
            lblKpiYieldText.Text = "YIELD";
            // 
            // pnlKpiTime
            // 
            pnlKpiTime.BackColor = Color.White;
            pnlKpiTime.BorderStyle = BorderStyle.FixedSingle;
            pnlKpiTime.Controls.Add(lblKpiTimeNum);
            pnlKpiTime.Controls.Add(lblKpiTimeText);
            pnlKpiTime.Dock = DockStyle.Fill;
            pnlKpiTime.Location = new Point(913, 5);
            pnlKpiTime.Margin = new Padding(5);
            pnlKpiTime.Name = "pnlKpiTime";
            pnlKpiTime.Size = new Size(221, 306);
            pnlKpiTime.TabIndex = 3;
            // 
            // lblKpiTimeNum
            // 
            lblKpiTimeNum.AutoSize = true;
            lblKpiTimeNum.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblKpiTimeNum.Location = new Point(10, 25);
            lblKpiTimeNum.Name = "lblKpiTimeNum";
            lblKpiTimeNum.Size = new Size(164, 51);
            lblKpiTimeNum.TabIndex = 0;
            lblKpiTimeNum.Text = "16.2 sec";
            // 
            // lblKpiTimeText
            // 
            lblKpiTimeText.AutoSize = true;
            lblKpiTimeText.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblKpiTimeText.ForeColor = Color.RoyalBlue;
            lblKpiTimeText.Location = new Point(10, 84);
            lblKpiTimeText.Name = "lblKpiTimeText";
            lblKpiTimeText.Size = new Size(204, 32);
            lblKpiTimeText.TabIndex = 1;
            lblKpiTimeText.Text = "AVG CYCLE TIME";
            // 
            // pnlKpiTotal
            // 
            pnlKpiTotal.BackColor = Color.FromArgb(248, 250, 255);
            pnlKpiTotal.BorderStyle = BorderStyle.FixedSingle;
            pnlKpiTotal.Controls.Add(lblKpiTotalNum);
            pnlKpiTotal.Controls.Add(lblKpiTotalText);
            pnlKpiTotal.Dock = DockStyle.Fill;
            pnlKpiTotal.Location = new Point(5, 5);
            pnlKpiTotal.Margin = new Padding(5);
            pnlKpiTotal.Name = "pnlKpiTotal";
            pnlKpiTotal.Size = new Size(217, 306);
            pnlKpiTotal.TabIndex = 4;
            // 
            // lblKpiTotalNum
            // 
            lblKpiTotalNum.AutoSize = true;
            lblKpiTotalNum.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblKpiTotalNum.Location = new Point(4, 20);
            lblKpiTotalNum.Name = "lblKpiTotalNum";
            lblKpiTotalNum.Size = new Size(100, 59);
            lblKpiTotalNum.TabIndex = 0;
            lblKpiTotalNum.Text = "215";
            // 
            // lblKpiTotalText
            // 
            lblKpiTotalText.AutoSize = true;
            lblKpiTotalText.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblKpiTotalText.ForeColor = Color.RoyalBlue;
            lblKpiTotalText.Location = new Point(4, 79);
            lblKpiTotalText.Name = "lblKpiTotalText";
            lblKpiTotalText.Size = new Size(200, 37);
            lblKpiTotalText.TabIndex = 1;
            lblKpiTotalText.Text = "TOTAL TESTED";
            // 
            // tblCharts
            // 
            tblCharts.ColumnCount = 2;
            tblCharts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tblCharts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tblCharts.Controls.Add(pnlBarChart, 0, 0);
            tblCharts.Controls.Add(pnlDonutChart, 1, 0);
            tblCharts.Dock = DockStyle.Fill;
            tblCharts.Location = new Point(3, 325);
            tblCharts.Name = "tblCharts";
            tblCharts.RowCount = 1;
            tblCharts.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblCharts.Size = new Size(1139, 593);
            tblCharts.TabIndex = 1;
            // 
            // pnlBarChart
            // 
            pnlBarChart.BorderStyle = BorderStyle.FixedSingle;
            pnlBarChart.Controls.Add(lblBarChartMock);
            pnlBarChart.Dock = DockStyle.Fill;
            pnlBarChart.Location = new Point(5, 5);
            pnlBarChart.Margin = new Padding(5);
            pnlBarChart.Name = "pnlBarChart";
            pnlBarChart.Size = new Size(730, 583);
            pnlBarChart.TabIndex = 0;
            // 
            // lblBarChartMock
            // 
            lblBarChartMock.AutoSize = true;
            lblBarChartMock.Font = new Font("Segoe UI", 10F);
            lblBarChartMock.ForeColor = Color.Gray;
            lblBarChartMock.Location = new Point(20, 20);
            lblBarChartMock.Name = "lblBarChartMock";
            lblBarChartMock.Size = new Size(433, 111);
            lblBarChartMock.TabIndex = 0;
            lblBarChartMock.Text = "■ Pass    ■ Fail\n\n[ Area Bar Chart (09 Mar - 15 Mar) ]";
            // 
            // pnlDonutChart
            // 
            pnlDonutChart.BorderStyle = BorderStyle.FixedSingle;
            pnlDonutChart.Controls.Add(lblDonutTitle);
            pnlDonutChart.Controls.Add(lblDonutLegendMock);
            pnlDonutChart.Dock = DockStyle.Fill;
            pnlDonutChart.Location = new Point(745, 5);
            pnlDonutChart.Margin = new Padding(5);
            pnlDonutChart.Name = "pnlDonutChart";
            pnlDonutChart.Size = new Size(389, 583);
            pnlDonutChart.TabIndex = 1;
            // 
            // lblDonutTitle
            // 
            lblDonutTitle.AutoSize = true;
            lblDonutTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblDonutTitle.Location = new Point(20, 20);
            lblDonutTitle.Name = "lblDonutTitle";
            lblDonutTitle.Size = new Size(365, 41);
            lblDonutTitle.TabIndex = 0;
            lblDonutTitle.Text = "FAIL BY REASON (Today)";
            // 
            // lblDonutLegendMock
            // 
            lblDonutLegendMock.AutoSize = true;
            lblDonutLegendMock.Font = new Font("Segoe UI", 10F);
            lblDonutLegendMock.Location = new Point(20, 70);
            lblDonutLegendMock.Name = "lblDonutLegendMock";
            lblDonutLegendMock.Size = new Size(341, 185);
            lblDonutLegendMock.TabIndex = 1;
            lblDonutLegendMock.Text = "[ Donut Chart: 3 Total Fail ]\n\n■ Continuity Fail: 2 (66.7%)\n■ Insulation Fail: 1 (33.3%)\n■ Short Circuit: 0 (0%)";
            // 
            // cardRecent
            // 
            cardRecent.BackColor = Color.White;
            cardRecent.Controls.Add(lblRecentTitle);
            cardRecent.Controls.Add(gridRecent);
            cardRecent.Dock = DockStyle.Fill;
            cardRecent.Location = new Point(1200, 5);
            cardRecent.Margin = new Padding(5);
            cardRecent.Name = "cardRecent";
            cardRecent.Size = new Size(969, 1011);
            cardRecent.TabIndex = 1;
            // 
            // lblRecentTitle
            // 
            lblRecentTitle.AutoSize = true;
            lblRecentTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblRecentTitle.Location = new Point(20, 20);
            lblRecentTitle.Name = "lblRecentTitle";
            lblRecentTitle.Size = new Size(365, 45);
            lblRecentTitle.TabIndex = 0;
            lblRecentTitle.Text = "RECENT TEST HISTORY";
            // 
            // gridRecent
            // 
            gridRecent.AllowUserToAddRows = false;
            gridRecent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridRecent.BackgroundColor = Color.White;
            gridRecent.BorderStyle = BorderStyle.None;
            gridRecent.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridRecent.Location = new Point(20, 68);
            gridRecent.Name = "gridRecent";
            gridRecent.RowHeadersVisible = false;
            gridRecent.RowHeadersWidth = 82;
            gridRecent.Size = new Size(921, 923);
            gridRecent.TabIndex = 1;
            // 
            // tblBottom
            // 
            tblBottom.ColumnCount = 3;
            tblBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tblBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.5F));
            tblBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.5F));
            tblBottom.Controls.Add(cardSystemMod, 0, 0);
            tblBottom.Controls.Add(cardOpTime, 1, 0);
            tblBottom.Controls.Add(cardNextMaint, 2, 0);
            tblBottom.Dock = DockStyle.Fill;
            tblBottom.Location = new Point(15, 1392);
            tblBottom.Margin = new Padding(0);
            tblBottom.Name = "tblBottom";
            tblBottom.RowCount = 1;
            tblBottom.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblBottom.Size = new Size(2174, 245);
            tblBottom.TabIndex = 2;
            // 
            // cardSystemMod
            // 
            cardSystemMod.BackColor = Color.White;
            cardSystemMod.Controls.Add(lblSystemModTitle);
            cardSystemMod.Controls.Add(tblModuleGrid);
            cardSystemMod.Dock = DockStyle.Fill;
            cardSystemMod.Location = new Point(5, 5);
            cardSystemMod.Margin = new Padding(5);
            cardSystemMod.Name = "cardSystemMod";
            cardSystemMod.Size = new Size(1185, 235);
            cardSystemMod.TabIndex = 0;
            // 
            // lblSystemModTitle
            // 
            lblSystemModTitle.AutoSize = true;
            lblSystemModTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblSystemModTitle.Location = new Point(20, 7);
            lblSystemModTitle.Name = "lblSystemModTitle";
            lblSystemModTitle.Size = new Size(411, 45);
            lblSystemModTitle.TabIndex = 0;
            lblSystemModTitle.Text = "SYSTEM MODULE STATUS";
            // 
            // tblModuleGrid
            // 
            tblModuleGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tblModuleGrid.ColumnCount = 4;
            tblModuleGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblModuleGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblModuleGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblModuleGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblModuleGrid.Controls.Add(pnlRelay, 0, 0);
            tblModuleGrid.Controls.Add(pnlPower, 1, 0);
            tblModuleGrid.Controls.Add(pnlPLC, 2, 0);
            tblModuleGrid.Controls.Add(pnlIO, 3, 0);
            tblModuleGrid.Location = new Point(20, 60);
            tblModuleGrid.Name = "tblModuleGrid";
            tblModuleGrid.RowCount = 1;
            tblModuleGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblModuleGrid.Size = new Size(1138, 155);
            tblModuleGrid.TabIndex = 1;
            // 
            // pnlRelay
            // 
            pnlRelay.Controls.Add(lblIconRelay);
            pnlRelay.Controls.Add(lblTitleRelay);
            pnlRelay.Controls.Add(lblStatusRelay);
            pnlRelay.Dock = DockStyle.Fill;
            pnlRelay.Location = new Point(0, 0);
            pnlRelay.Margin = new Padding(0);
            pnlRelay.Name = "pnlRelay";
            pnlRelay.Size = new Size(284, 155);
            pnlRelay.TabIndex = 0;
            // 
            // lblIconRelay
            // 
            lblIconRelay.Dock = DockStyle.Top;
            lblIconRelay.Font = new Font("Segoe UI", 18F);
            lblIconRelay.Location = new Point(0, 0);
            lblIconRelay.Name = "lblIconRelay";
            lblIconRelay.Size = new Size(284, 85);
            lblIconRelay.TabIndex = 0;
            lblIconRelay.Text = "🔀";
            lblIconRelay.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTitleRelay
            // 
            lblTitleRelay.Dock = DockStyle.Bottom;
            lblTitleRelay.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTitleRelay.Location = new Point(0, 78);
            lblTitleRelay.Name = "lblTitleRelay";
            lblTitleRelay.Size = new Size(284, 47);
            lblTitleRelay.TabIndex = 1;
            lblTitleRelay.Text = "Relay Matrix";
            lblTitleRelay.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblStatusRelay
            // 
            lblStatusRelay.Dock = DockStyle.Bottom;
            lblStatusRelay.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblStatusRelay.ForeColor = Color.ForestGreen;
            lblStatusRelay.Location = new Point(0, 125);
            lblStatusRelay.Name = "lblStatusRelay";
            lblStatusRelay.Size = new Size(284, 30);
            lblStatusRelay.TabIndex = 2;
            lblStatusRelay.Text = "● ONLINE";
            lblStatusRelay.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlPower
            // 
            pnlPower.Controls.Add(lblIconPower);
            pnlPower.Controls.Add(lblTitlePower);
            pnlPower.Controls.Add(lblStatusPower);
            pnlPower.Dock = DockStyle.Fill;
            pnlPower.Location = new Point(284, 0);
            pnlPower.Margin = new Padding(0);
            pnlPower.Name = "pnlPower";
            pnlPower.Size = new Size(284, 155);
            pnlPower.TabIndex = 1;
            // 
            // lblIconPower
            // 
            lblIconPower.Dock = DockStyle.Top;
            lblIconPower.Font = new Font("Segoe UI", 20F);
            lblIconPower.Location = new Point(0, 0);
            lblIconPower.Name = "lblIconPower";
            lblIconPower.Size = new Size(284, 85);
            lblIconPower.TabIndex = 0;
            lblIconPower.Text = "\u23fb";
            lblIconPower.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTitlePower
            // 
            lblTitlePower.Dock = DockStyle.Bottom;
            lblTitlePower.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTitlePower.Location = new Point(0, 78);
            lblTitlePower.Name = "lblTitlePower";
            lblTitlePower.Size = new Size(284, 47);
            lblTitlePower.TabIndex = 1;
            lblTitlePower.Text = "Power Supply";
            lblTitlePower.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblStatusPower
            // 
            lblStatusPower.Dock = DockStyle.Bottom;
            lblStatusPower.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblStatusPower.ForeColor = Color.ForestGreen;
            lblStatusPower.Location = new Point(0, 125);
            lblStatusPower.Name = "lblStatusPower";
            lblStatusPower.Size = new Size(284, 30);
            lblStatusPower.TabIndex = 2;
            lblStatusPower.Text = "● ONLINE";
            lblStatusPower.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlPLC
            // 
            pnlPLC.Controls.Add(lblIconPLC);
            pnlPLC.Controls.Add(lblTitlePLC);
            pnlPLC.Controls.Add(lblStatusPLC);
            pnlPLC.Dock = DockStyle.Fill;
            pnlPLC.Location = new Point(568, 0);
            pnlPLC.Margin = new Padding(0);
            pnlPLC.Name = "pnlPLC";
            pnlPLC.Size = new Size(284, 155);
            pnlPLC.TabIndex = 2;
            // 
            // lblIconPLC
            // 
            lblIconPLC.Dock = DockStyle.Top;
            lblIconPLC.Font = new Font("Segoe UI", 20F);
            lblIconPLC.Location = new Point(0, 0);
            lblIconPLC.Name = "lblIconPLC";
            lblIconPLC.Size = new Size(284, 85);
            lblIconPLC.TabIndex = 0;
            lblIconPLC.Text = "📟";
            lblIconPLC.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTitlePLC
            // 
            lblTitlePLC.Dock = DockStyle.Bottom;
            lblTitlePLC.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTitlePLC.Location = new Point(0, 78);
            lblTitlePLC.Name = "lblTitlePLC";
            lblTitlePLC.Size = new Size(284, 47);
            lblTitlePLC.TabIndex = 1;
            lblTitlePLC.Text = "PLC Controller";
            lblTitlePLC.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblStatusPLC
            // 
            lblStatusPLC.Dock = DockStyle.Bottom;
            lblStatusPLC.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblStatusPLC.ForeColor = Color.ForestGreen;
            lblStatusPLC.Location = new Point(0, 125);
            lblStatusPLC.Name = "lblStatusPLC";
            lblStatusPLC.Size = new Size(284, 30);
            lblStatusPLC.TabIndex = 2;
            lblStatusPLC.Text = "● ONLINE";
            lblStatusPLC.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlIO
            // 
            pnlIO.Controls.Add(lblIconIO);
            pnlIO.Controls.Add(lblTitleIO);
            pnlIO.Controls.Add(lblStatusIO);
            pnlIO.Dock = DockStyle.Fill;
            pnlIO.Location = new Point(852, 0);
            pnlIO.Margin = new Padding(0);
            pnlIO.Name = "pnlIO";
            pnlIO.Size = new Size(286, 155);
            pnlIO.TabIndex = 3;
            // 
            // lblIconIO
            // 
            lblIconIO.Dock = DockStyle.Top;
            lblIconIO.Font = new Font("Segoe UI", 20F);
            lblIconIO.Location = new Point(0, 0);
            lblIconIO.Name = "lblIconIO";
            lblIconIO.Size = new Size(286, 85);
            lblIconIO.TabIndex = 0;
            lblIconIO.Text = "🎛️";
            lblIconIO.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTitleIO
            // 
            lblTitleIO.Dock = DockStyle.Bottom;
            lblTitleIO.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTitleIO.Location = new Point(0, 78);
            lblTitleIO.Name = "lblTitleIO";
            lblTitleIO.Size = new Size(286, 47);
            lblTitleIO.TabIndex = 1;
            lblTitleIO.Text = "I/O Module";
            lblTitleIO.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblStatusIO
            // 
            lblStatusIO.Dock = DockStyle.Bottom;
            lblStatusIO.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblStatusIO.ForeColor = Color.ForestGreen;
            lblStatusIO.Location = new Point(0, 125);
            lblStatusIO.Name = "lblStatusIO";
            lblStatusIO.Size = new Size(286, 30);
            lblStatusIO.TabIndex = 2;
            lblStatusIO.Text = "● ONLINE";
            lblStatusIO.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cardOpTime
            // 
            cardOpTime.BackColor = Color.White;
            cardOpTime.Controls.Add(lblOpTime);
            cardOpTime.Controls.Add(lblTimeValue);
            cardOpTime.Dock = DockStyle.Fill;
            cardOpTime.Location = new Point(1200, 5);
            cardOpTime.Margin = new Padding(5);
            cardOpTime.Name = "cardOpTime";
            cardOpTime.Size = new Size(479, 235);
            cardOpTime.TabIndex = 1;
            // 
            // lblOpTime
            // 
            lblOpTime.AutoSize = true;
            lblOpTime.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblOpTime.Location = new Point(20, 15);
            lblOpTime.Name = "lblOpTime";
            lblOpTime.Size = new Size(328, 37);
            lblOpTime.TabIndex = 0;
            lblOpTime.Text = "TOTAL OPERATING TIME";
            // 
            // lblTimeValue
            // 
            lblTimeValue.AutoSize = true;
            lblTimeValue.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblTimeValue.Location = new Point(15, 50);
            lblTimeValue.Name = "lblTimeValue";
            lblTimeValue.Size = new Size(333, 78);
            lblTimeValue.TabIndex = 1;
            lblTimeValue.Text = "03 : 46 : 22";
            // 
            // cardNextMaint
            // 
            cardNextMaint.BackColor = Color.White;
            cardNextMaint.Controls.Add(lblNextMaintTitle);
            cardNextMaint.Controls.Add(lblNextMaintValue);
            cardNextMaint.Controls.Add(lblNextMaintSub);
            cardNextMaint.Dock = DockStyle.Fill;
            cardNextMaint.Location = new Point(1689, 5);
            cardNextMaint.Margin = new Padding(5);
            cardNextMaint.Name = "cardNextMaint";
            cardNextMaint.Size = new Size(480, 235);
            cardNextMaint.TabIndex = 2;
            // 
            // lblNextMaintTitle
            // 
            lblNextMaintTitle.AutoSize = true;
            lblNextMaintTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNextMaintTitle.Location = new Point(20, 15);
            lblNextMaintTitle.Name = "lblNextMaintTitle";
            lblNextMaintTitle.Size = new Size(292, 37);
            lblNextMaintTitle.TabIndex = 0;
            lblNextMaintTitle.Text = "NEXT MAINTENANCE";
            // 
            // lblNextMaintValue
            // 
            lblNextMaintValue.AutoSize = true;
            lblNextMaintValue.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblNextMaintValue.Location = new Point(15, 45);
            lblNextMaintValue.Name = "lblNextMaintValue";
            lblNextMaintValue.Size = new Size(285, 78);
            lblNextMaintValue.TabIndex = 1;
            lblNextMaintValue.Text = "145 Days";
            // 
            // lblNextMaintSub
            // 
            lblNextMaintSub.AutoSize = true;
            lblNextMaintSub.Font = new Font("Segoe UI", 9F);
            lblNextMaintSub.ForeColor = Color.Gray;
            lblNextMaintSub.Location = new Point(20, 90);
            lblNextMaintSub.Name = "lblNextMaintSub";
            lblNextMaintSub.Size = new Size(174, 32);
            lblNextMaintSub.TabIndex = 2;
            lblNextMaintSub.Text = "Or 6,500 Hours";
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 245, 247);
            Controls.Add(tblMain);
            Name = "Dashboard";
            Size = new Size(2204, 1652);
            tblMain.ResumeLayout(false);
            tblTop.ResumeLayout(false);
            cardTester.ResumeLayout(false);
            cardTester.PerformLayout();
            cardMachine.ResumeLayout(false);
            cardMachine.PerformLayout();
            cardActiveProg.ResumeLayout(false);
            cardActiveProg.PerformLayout();
            cardDatabase.ResumeLayout(false);
            cardDatabase.PerformLayout();
            tblMiddle.ResumeLayout(false);
            cardSummary.ResumeLayout(false);
            cardSummary.PerformLayout();
            tblSummaryContent.ResumeLayout(false);
            tblKpiCards.ResumeLayout(false);
            pnlKpiPass.ResumeLayout(false);
            pnlKpiPass.PerformLayout();
            pnlKpiFail.ResumeLayout(false);
            pnlKpiFail.PerformLayout();
            pnlKpiYield.ResumeLayout(false);
            pnlKpiYield.PerformLayout();
            pnlKpiTime.ResumeLayout(false);
            pnlKpiTime.PerformLayout();
            pnlKpiTotal.ResumeLayout(false);
            pnlKpiTotal.PerformLayout();
            tblCharts.ResumeLayout(false);
            pnlBarChart.ResumeLayout(false);
            pnlBarChart.PerformLayout();
            pnlDonutChart.ResumeLayout(false);
            pnlDonutChart.PerformLayout();
            cardRecent.ResumeLayout(false);
            cardRecent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridRecent).EndInit();
            tblBottom.ResumeLayout(false);
            cardSystemMod.ResumeLayout(false);
            cardSystemMod.PerformLayout();
            tblModuleGrid.ResumeLayout(false);
            pnlRelay.ResumeLayout(false);
            pnlPower.ResumeLayout(false);
            pnlPLC.ResumeLayout(false);
            pnlIO.ResumeLayout(false);
            cardOpTime.ResumeLayout(false);
            cardOpTime.PerformLayout();
            cardNextMaint.ResumeLayout(false);
            cardNextMaint.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblMain;
        private System.Windows.Forms.TableLayoutPanel tblTop;
        private System.Windows.Forms.Panel cardTester;
        private System.Windows.Forms.Label lblTesterTitle;
        private System.Windows.Forms.Label lblTesterStatus;
        private System.Windows.Forms.Panel cardMachine;
        private System.Windows.Forms.Label lblMachineTitle;
        private System.Windows.Forms.Label lblMachineStatus;
        private System.Windows.Forms.Panel cardDatabase;
        private System.Windows.Forms.Label lblDatabaseTitle;
        private System.Windows.Forms.Label lblDatabaseStatus;
        private System.Windows.Forms.Panel cardActiveProg;
        private System.Windows.Forms.Label lblActiveProgTitle;
        private System.Windows.Forms.Label lblProgDetails;
        private System.Windows.Forms.Button btnChangeProg;
        private System.Windows.Forms.TableLayoutPanel tblMiddle;
        private System.Windows.Forms.Panel cardSummary;
        private System.Windows.Forms.Label lblSummaryTitle;
        private System.Windows.Forms.ComboBox cmbSummaryDate;
        private System.Windows.Forms.TableLayoutPanel tblSummaryContent;
        private System.Windows.Forms.Panel pnlKpiTotal;
        private System.Windows.Forms.Label lblKpiTotalNum;
        private System.Windows.Forms.Label lblKpiTotalText;
        private System.Windows.Forms.Panel pnlKpiPass;
        private System.Windows.Forms.Label lblKpiPassNum;
        private System.Windows.Forms.Label lblKpiPassText;
        private System.Windows.Forms.Panel pnlKpiFail;
        private System.Windows.Forms.Label lblKpiFailNum;
        private System.Windows.Forms.Label lblKpiFailText;
        private System.Windows.Forms.Panel pnlKpiYield;
        private System.Windows.Forms.Label lblKpiYieldNum;
        private System.Windows.Forms.Label lblKpiYieldText;
        private System.Windows.Forms.Panel pnlKpiTime;
        private System.Windows.Forms.Label lblKpiTimeNum;
        private System.Windows.Forms.Label lblKpiTimeText;
        private System.Windows.Forms.Panel pnlBarChart;
        private System.Windows.Forms.Label lblBarChartMock;
        private System.Windows.Forms.Panel pnlDonutChart;
        private System.Windows.Forms.Label lblDonutTitle;
        private System.Windows.Forms.Label lblDonutLegendMock;
        private System.Windows.Forms.Panel cardRecent;
        private System.Windows.Forms.Label lblRecentTitle;
        private System.Windows.Forms.DataGridView gridRecent;
        private System.Windows.Forms.TableLayoutPanel tblBottom;
        private System.Windows.Forms.Panel cardSystemMod;
        private System.Windows.Forms.Label lblSystemModTitle;
        private System.Windows.Forms.TableLayoutPanel tblModuleGrid;
        private System.Windows.Forms.Panel cardOpTime;
        private System.Windows.Forms.Label lblOpTime;
        private System.Windows.Forms.Label lblTimeValue;
        private System.Windows.Forms.Panel cardNextMaint;
        private System.Windows.Forms.Label lblNextMaintTitle;
        private System.Windows.Forms.Label lblNextMaintValue;
        private System.Windows.Forms.Label lblNextMaintSub;

        // 4 Kolom Modul Status
        private System.Windows.Forms.Panel pnlRelay;
        private System.Windows.Forms.Label lblIconRelay;
        private System.Windows.Forms.Label lblTitleRelay;
        private System.Windows.Forms.Label lblStatusRelay;
        private System.Windows.Forms.Panel pnlPower;
        private System.Windows.Forms.Label lblIconPower;
        private System.Windows.Forms.Label lblTitlePower;
        private System.Windows.Forms.Label lblStatusPower;
        private System.Windows.Forms.Panel pnlPLC;
        private System.Windows.Forms.Label lblIconPLC;
        private System.Windows.Forms.Label lblTitlePLC;
        private System.Windows.Forms.Label lblStatusPLC;
        private System.Windows.Forms.Panel pnlIO;
        private System.Windows.Forms.Label lblIconIO;
        private System.Windows.Forms.Label lblTitleIO;
        private System.Windows.Forms.Label lblStatusIO;
        private TableLayoutPanel tblKpiCards;
        private TableLayoutPanel tblCharts;
    }
}