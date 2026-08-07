using ClosedXML.Excel;
using static System.Windows.Forms.MonthCalendar;

namespace AssemblyLineTester.Views.Pages

{

    partial class Reports

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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            tblMain = new TableLayoutPanel();
            pnlTopHeader = new Panel();
            lblPageHeader = new Label();
            tblLeftCenterRight = new TableLayoutPanel();
            splitMainTopBottom = new SplitContainer();
            splitLeftRight = new SplitContainer();
            tblColLeft = new TableLayoutPanel();
            cardFilters = new Panel();
            tblFilterControls = new TableLayoutPanel();
            lblDateRange = new Label();
            tblDateRange = new TableLayoutPanel();
            dtpStart = new DateTimePicker();
            lblDash = new Label();
            dtpEnd = new DateTimePicker();
            lblFilters = new Label();
            tblFilterGrid1 = new TableLayoutPanel();
            lblFilterModel = new Label();
            lblFilterType = new Label();
            cmbModel = new ComboBox();
            cmbType = new ComboBox();
            tblFilterGrid2 = new TableLayoutPanel();
            lblFilterArea = new Label();
            lblFilterOperator = new Label();
            cmbArea = new ComboBox();
            cmbOperator = new ComboBox();
            tblFilterGrid3 = new TableLayoutPanel();
            lblFilterResult = new Label();
            lblFilterPart = new Label();
            cmbResult = new ComboBox();
            txtPartNumber = new TextBox();
            tblFilterButtons = new TableLayoutPanel();
            btnGenerate = new Button();
            btnReset = new Button();
            pnlSeparator = new Panel();
            lblSummaryTitle = new Label();
            tblSummaryCards = new TableLayoutPanel();
            pnlSumTotal = new Panel();
            lblSumTotalKey = new Label();
            lblSumTotalVal = new Label();
            pnlSumPass = new Panel();
            lblSumPassKey = new Label();
            lblSumPassVal = new Label();
            pnlSumFail = new Panel();
            lblSumFailKey = new Label();
            lblSumFailVal = new Label();
            cardReportList = new Panel();
            tblCenter = new TableLayoutPanel();
            lblReportListTitle = new Label();
            gridReports = new DataGridView();
            colNo = new DataGridViewTextBoxColumn();
            colReportName = new DataGridViewTextBoxColumn();
            colReportType = new DataGridViewTextBoxColumn();
            colDateGenerated = new DataGridViewTextBoxColumn();
            colGeneratedBy = new DataGridViewTextBoxColumn();
            colSize = new DataGridViewTextBoxColumn();
            colAction = new DataGridViewTextBoxColumn();
            tblPagination = new TableLayoutPanel();
            lblPageInfo = new Label();
            flpPageButtons = new FlowLayoutPanel();
            btnPageLast = new Button();
            btnPageNext = new Button();
            btnPageDots = new Button();
            btnPage2 = new Button();
            btnPage1 = new Button();
            btnPagePrev = new Button();
            btnPageFirst = new Button();
            cardTrend = new Panel();
            lblTrendTitle = new Label();
            tblColRight = new TableLayoutPanel();
            cardPreviewTop = new Panel();
            flpPreviewButtons = new TableLayoutPanel();
            btnExportPDF = new Button();
            btnExportExcel = new Button();
            btnPrint = new Button();
            btnEditReport = new Button();
            btnDeleteReport = new Button();
            btnCancelReportEdit = new Button();
            btnSaveReportEdit = new Button();
            lblPreviewTitle = new Label();
            pnlPreviewContainer = new Panel();
            cardPaper = new Panel();
            tblPaperContent = new TableLayoutPanel();
            tblPaperHeader = new TableLayoutPanel();
            flpPaperLogo = new FlowLayoutPanel();
            lblLogoBadge = new Label();
            lblPaperTitle = new Label();
            lblPaperMeta = new Label();
            pnlGenInfoBox = new Panel();
            tblGenGrid = new TableLayoutPanel();
            lblGenModelKey = new Label();
            lblGenModelVal = new Label();
            lblGenEcnKey = new Label();
            lblGenEcnVal = new Label();
            lblGenTypeKey = new Label();
            lblGenTypeVal = new Label();
            lblGenRevKey = new Label();
            lblGenRevVal = new Label();
            lblGenAreaKey = new Label();
            lblGenAreaVal = new Label();
            lblGenOpKey = new Label();
            lblGenOpVal = new Label();
            lblGenPartKey = new Label();
            lblGenPartVal = new Label();
            lblGenTimeKey = new Label();
            lblGenTimeVal = new Label();
            lblGenSnKey = new Label();
            lblGenSnVal = new Label();
            lblGenResKey = new Label();
            lblGenResVal = new Label();
            lblGenTitle = new Label();
            pnlSummaryTableContainer = new Panel();
            tblTestSummary = new TableLayoutPanel();
            lblSumH1 = new Label();
            lblSumH2 = new Label();
            lblSumH3 = new Label();
            lblSumH4 = new Label();
            lblSumH5 = new Label();
            lblR1C1 = new Label();
            lblR1C2 = new Label();
            lblR1C3 = new Label();
            lblR1C4 = new Label();
            lblR1C5 = new Label();
            lblR2C1 = new Label();
            lblR2C2 = new Label();
            lblR2C3 = new Label();
            lblR2C4 = new Label();
            lblR2C5 = new Label();
            lblR3C1 = new Label();
            lblR3C2 = new Label();
            lblR3C3 = new Label();
            lblR3C4 = new Label();
            lblR3C5 = new Label();
            lblR4C1 = new Label();
            lblR4C2 = new Label();
            lblR4C3 = new Label();
            lblR4C4 = new Label();
            lblR4C5 = new Label();
            lblR5C1 = new Label();
            lblR5C2 = new Label();
            lblR5C3 = new Label();
            lblR5C4 = new Label();
            lblR5C5 = new Label();
            lblSumTableTitle = new Label();
            pnlResultBox = new TableLayoutPanel();
            lblResultTitle = new Label();
            lblPassBox = new Label();
            pnlNotesBox = new TableLayoutPanel();
            lblNotesTitle = new Label();
            pnlNotesCard = new Panel();
            lblNotesContent = new Label();
            lblReportType = new Label();
            cmbReportType = new ComboBox();
            pnlSumProg = new Panel();
            lblSumProgKey = new Label();
            lblSumProgVal = new Label();
            tblMain.SuspendLayout();
            pnlTopHeader.SuspendLayout();
            tblLeftCenterRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitMainTopBottom).BeginInit();
            splitMainTopBottom.Panel1.SuspendLayout();
            splitMainTopBottom.Panel2.SuspendLayout();
            splitMainTopBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitLeftRight).BeginInit();
            splitLeftRight.Panel1.SuspendLayout();
            splitLeftRight.Panel2.SuspendLayout();
            splitLeftRight.SuspendLayout();
            tblColLeft.SuspendLayout();
            cardFilters.SuspendLayout();
            tblFilterControls.SuspendLayout();
            tblDateRange.SuspendLayout();
            tblFilterGrid1.SuspendLayout();
            tblFilterGrid2.SuspendLayout();
            tblFilterGrid3.SuspendLayout();
            tblFilterButtons.SuspendLayout();
            tblSummaryCards.SuspendLayout();
            pnlSumTotal.SuspendLayout();
            pnlSumPass.SuspendLayout();
            pnlSumFail.SuspendLayout();
            cardReportList.SuspendLayout();
            tblCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridReports).BeginInit();
            tblPagination.SuspendLayout();
            flpPageButtons.SuspendLayout();
            cardTrend.SuspendLayout();
            tblColRight.SuspendLayout();
            cardPreviewTop.SuspendLayout();
            flpPreviewButtons.SuspendLayout();
            pnlPreviewContainer.SuspendLayout();
            cardPaper.SuspendLayout();
            tblPaperContent.SuspendLayout();
            tblPaperHeader.SuspendLayout();
            flpPaperLogo.SuspendLayout();
            pnlGenInfoBox.SuspendLayout();
            tblGenGrid.SuspendLayout();
            pnlSummaryTableContainer.SuspendLayout();
            tblTestSummary.SuspendLayout();
            pnlResultBox.SuspendLayout();
            pnlNotesBox.SuspendLayout();
            pnlNotesCard.SuspendLayout();
            pnlSumProg.SuspendLayout();
            SuspendLayout();
            // 
            // tblMain
            // 
            tblMain.AutoScroll = true;
            tblMain.ColumnCount = 1;
            tblMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblMain.Controls.Add(pnlTopHeader, 0, 0);
            tblMain.Controls.Add(tblLeftCenterRight, 0, 1);
            tblMain.Dock = DockStyle.Fill;
            tblMain.Location = new Point(0, 0);
            tblMain.Margin = new Padding(6, 4, 6, 4);
            tblMain.Name = "tblMain";
            tblMain.RowCount = 2;
            tblMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 94F));
            tblMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblMain.Size = new Size(4000, 2451);
            tblMain.TabIndex = 0;
            // 
            // pnlTopHeader
            // 
            pnlTopHeader.Controls.Add(lblPageHeader);
            pnlTopHeader.Dock = DockStyle.Fill;
            pnlTopHeader.Location = new Point(17, 9);
            pnlTopHeader.Margin = new Padding(17, 9, 17, 0);
            pnlTopHeader.Name = "pnlTopHeader";
            pnlTopHeader.Size = new Size(3966, 85);
            pnlTopHeader.TabIndex = 0;
            // 
            // lblPageHeader
            // 
            lblPageHeader.Dock = DockStyle.Left;
            lblPageHeader.Font = new Font("Segoe UI", 13.5F, FontStyle.Bold);
            lblPageHeader.ForeColor = Color.FromArgb(30, 41, 59);
            lblPageHeader.Location = new Point(0, 0);
            lblPageHeader.Margin = new Padding(6, 0, 6, 0);
            lblPageHeader.Name = "lblPageHeader";
            lblPageHeader.Size = new Size(488, 85);
            lblPageHeader.TabIndex = 0;
            lblPageHeader.Text = "📑  REPORTS";
            lblPageHeader.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tblLeftCenterRight
            // 
            tblLeftCenterRight.ColumnCount = 2;
            tblLeftCenterRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tblLeftCenterRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tblLeftCenterRight.Controls.Add(splitMainTopBottom, 0, 0);
            tblLeftCenterRight.Controls.Add(tblColRight, 1, 0);
            tblLeftCenterRight.Dock = DockStyle.Fill;
            tblLeftCenterRight.Location = new Point(17, 103);
            tblLeftCenterRight.Margin = new Padding(17, 9, 17, 17);
            tblLeftCenterRight.Name = "tblLeftCenterRight";
            tblLeftCenterRight.RowCount = 1;
            tblLeftCenterRight.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblLeftCenterRight.Size = new Size(3966, 2331);
            tblLeftCenterRight.TabIndex = 1;
            // 
            // splitMainTopBottom
            // 
            splitMainTopBottom.Dock = DockStyle.Fill;
            splitMainTopBottom.FixedPanel = FixedPanel.Panel1;
            splitMainTopBottom.Location = new Point(0, 0);
            splitMainTopBottom.Margin = new Padding(0);
            splitMainTopBottom.Name = "splitMainTopBottom";
            splitMainTopBottom.Orientation = Orientation.Horizontal;
            // 
            // splitMainTopBottom.Panel1
            // 
            splitMainTopBottom.Panel1.Controls.Add(splitLeftRight);
            splitMainTopBottom.Panel1MinSize = 475;
            // 
            // splitMainTopBottom.Panel2
            // 
            splitMainTopBottom.Panel2.Controls.Add(cardTrend);
            splitMainTopBottom.Size = new Size(2776, 2331);
            splitMainTopBottom.SplitterDistance = 1013;
            splitMainTopBottom.SplitterWidth = 11;
            splitMainTopBottom.TabIndex = 0;
            // 
            // splitLeftRight
            // 
            splitLeftRight.Dock = DockStyle.Fill;
            splitLeftRight.Location = new Point(0, 0);
            splitLeftRight.Margin = new Padding(0);
            splitLeftRight.Name = "splitLeftRight";
            // 
            // splitLeftRight.Panel1
            // 
            splitLeftRight.Panel1.Controls.Add(tblColLeft);
            // 
            // splitLeftRight.Panel2
            // 
            splitLeftRight.Panel2.Controls.Add(cardReportList);
            splitLeftRight.Size = new Size(2776, 1013);
            splitLeftRight.SplitterDistance = 975;
            splitLeftRight.SplitterWidth = 11;
            splitLeftRight.TabIndex = 0;
            // 
            // tblColLeft
            // 
            tblColLeft.ColumnCount = 1;
            tblColLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblColLeft.Controls.Add(cardFilters, 0, 0);
            tblColLeft.Dock = DockStyle.Fill;
            tblColLeft.Location = new Point(0, 0);
            tblColLeft.Margin = new Padding(0, 0, 13, 0);
            tblColLeft.Name = "tblColLeft";
            tblColLeft.RowCount = 1;
            tblColLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblColLeft.Size = new Size(975, 1013);
            tblColLeft.TabIndex = 0;
            // 
            // cardFilters
            // 
            cardFilters.AutoScroll = true;
            cardFilters.BackColor = Color.White;
            cardFilters.Controls.Add(tblFilterControls);
            cardFilters.Dock = DockStyle.Fill;
            cardFilters.Location = new Point(0, 0);
            cardFilters.Margin = new Padding(0);
            cardFilters.Name = "cardFilters";
            cardFilters.Padding = new Padding(20, 19, 20, 19);
            cardFilters.Size = new Size(975, 1013);
            cardFilters.TabIndex = 0;
            // 
            // tblFilterControls
            // 
            tblFilterControls.ColumnCount = 1;
            tblFilterControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblFilterControls.Controls.Add(lblDateRange, 0, 2);
            tblFilterControls.Controls.Add(tblDateRange, 0, 3);
            tblFilterControls.Controls.Add(lblFilters, 0, 4);
            tblFilterControls.Controls.Add(tblFilterGrid1, 0, 5);
            tblFilterControls.Controls.Add(tblFilterGrid2, 0, 6);
            tblFilterControls.Controls.Add(tblFilterGrid3, 0, 7);
            tblFilterControls.Controls.Add(tblFilterButtons, 0, 8);
            tblFilterControls.Controls.Add(pnlSeparator, 0, 9);
            tblFilterControls.Controls.Add(lblSummaryTitle, 0, 10);
            tblFilterControls.Controls.Add(tblSummaryCards, 0, 11);
            tblFilterControls.Dock = DockStyle.Fill;
            tblFilterControls.Location = new Point(20, 19);
            tblFilterControls.Margin = new Padding(6, 4, 6, 4);
            tblFilterControls.Name = "tblFilterControls";
            tblFilterControls.RowCount = 12;
            tblFilterControls.RowStyles.Add(new RowStyle());
            tblFilterControls.RowStyles.Add(new RowStyle());
            tblFilterControls.RowStyles.Add(new RowStyle());
            tblFilterControls.RowStyles.Add(new RowStyle());
            tblFilterControls.RowStyles.Add(new RowStyle());
            tblFilterControls.RowStyles.Add(new RowStyle());
            tblFilterControls.RowStyles.Add(new RowStyle());
            tblFilterControls.RowStyles.Add(new RowStyle());
            tblFilterControls.RowStyles.Add(new RowStyle());
            tblFilterControls.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tblFilterControls.RowStyles.Add(new RowStyle());
            tblFilterControls.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblFilterControls.Size = new Size(935, 975);
            tblFilterControls.TabIndex = 0;
            // 
            // lblDateRange
            // 
            lblDateRange.AutoSize = true;
            lblDateRange.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblDateRange.ForeColor = Color.FromArgb(71, 85, 105);
            lblDateRange.Location = new Point(0, 0);
            lblDateRange.Margin = new Padding(0, 0, 0, 6);
            lblDateRange.Name = "lblDateRange";
            lblDateRange.Size = new Size(153, 31);
            lblDateRange.TabIndex = 2;
            lblDateRange.Text = "DATE RANGE";
            // 
            // tblDateRange
            // 
            tblDateRange.ColumnCount = 3;
            tblDateRange.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblDateRange.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 24F));
            tblDateRange.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblDateRange.Controls.Add(dtpStart, 0, 0);
            tblDateRange.Controls.Add(lblDash, 1, 0);
            tblDateRange.Controls.Add(dtpEnd, 2, 0);
            tblDateRange.Dock = DockStyle.Fill;
            tblDateRange.Location = new Point(0, 37);
            tblDateRange.Margin = new Padding(0, 0, 0, 13);
            tblDateRange.Name = "tblDateRange";
            tblDateRange.RowCount = 1;
            tblDateRange.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblDateRange.Size = new Size(935, 68);
            tblDateRange.TabIndex = 3;
            // 
            // dtpStart
            // 
            dtpStart.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dtpStart.CustomFormat = "dd Mar yyyy";
            dtpStart.Font = new Font("Segoe UI", 9F);
            dtpStart.Format = DateTimePickerFormat.Custom;
            dtpStart.Location = new Point(7, 14);
            dtpStart.Margin = new Padding(7, 9, 7, 9);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(441, 39);
            dtpStart.TabIndex = 0;
            // 
            // lblDash
            // 
            lblDash.AutoSize = true;
            lblDash.Dock = DockStyle.Fill;
            lblDash.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDash.ForeColor = Color.FromArgb(100, 116, 139);
            lblDash.Location = new Point(455, 0);
            lblDash.Margin = new Padding(0);
            lblDash.Name = "lblDash";
            lblDash.Size = new Size(24, 68);
            lblDash.TabIndex = 1;
            lblDash.Text = "-";
            lblDash.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dtpEnd
            // 
            dtpEnd.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dtpEnd.CustomFormat = "dd Mar yyyy";
            dtpEnd.Font = new Font("Segoe UI", 9F);
            dtpEnd.Format = DateTimePickerFormat.Custom;
            dtpEnd.Location = new Point(486, 14);
            dtpEnd.Margin = new Padding(7, 9, 7, 9);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(442, 39);
            dtpEnd.TabIndex = 2;
            // 
            // lblFilters
            // 
            lblFilters.AutoSize = true;
            lblFilters.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblFilters.ForeColor = Color.FromArgb(71, 85, 105);
            lblFilters.Location = new Point(0, 118);
            lblFilters.Margin = new Padding(0, 0, 0, 6);
            lblFilters.Name = "lblFilters";
            lblFilters.Size = new Size(96, 31);
            lblFilters.TabIndex = 4;
            lblFilters.Text = "FILTERS";
            // 
            // tblFilterGrid1
            // 
            tblFilterGrid1.AutoSize = true;
            tblFilterGrid1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tblFilterGrid1.ColumnCount = 2;
            tblFilterGrid1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblFilterGrid1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblFilterGrid1.Controls.Add(lblFilterModel, 0, 0);
            tblFilterGrid1.Controls.Add(lblFilterType, 1, 0);
            tblFilterGrid1.Controls.Add(cmbModel, 0, 1);
            tblFilterGrid1.Controls.Add(cmbType, 1, 1);
            tblFilterGrid1.Dock = DockStyle.Fill;
            tblFilterGrid1.Location = new Point(0, 155);
            tblFilterGrid1.Margin = new Padding(0, 0, 0, 11);
            tblFilterGrid1.Name = "tblFilterGrid1";
            tblFilterGrid1.RowCount = 2;
            tblFilterGrid1.RowStyles.Add(new RowStyle());
            tblFilterGrid1.RowStyles.Add(new RowStyle());
            tblFilterGrid1.Size = new Size(935, 72);
            tblFilterGrid1.TabIndex = 5;
            // 
            // lblFilterModel
            // 
            lblFilterModel.AutoSize = true;
            lblFilterModel.Font = new Font("Segoe UI", 8F);
            lblFilterModel.ForeColor = Color.FromArgb(100, 116, 139);
            lblFilterModel.Location = new Point(0, 0);
            lblFilterModel.Margin = new Padding(0, 0, 0, 2);
            lblFilterModel.Name = "lblFilterModel";
            lblFilterModel.Size = new Size(157, 30);
            lblFilterModel.TabIndex = 0;
            lblFilterModel.Text = "Product Model";
            // 
            // lblFilterType
            // 
            lblFilterType.AutoSize = true;
            lblFilterType.Font = new Font("Segoe UI", 8F);
            lblFilterType.ForeColor = Color.FromArgb(100, 116, 139);
            lblFilterType.Location = new Point(473, 0);
            lblFilterType.Margin = new Padding(6, 0, 0, 2);
            lblFilterType.Name = "lblFilterType";
            lblFilterType.Size = new Size(141, 30);
            lblFilterType.TabIndex = 1;
            lblFilterType.Text = "Product Type";
            // 
            // cmbModel
            // 
            cmbModel.Dock = DockStyle.Fill;
            cmbModel.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbModel.Font = new Font("Segoe UI", 9F);
            cmbModel.FormattingEnabled = true;
            cmbModel.Items.AddRange(new object[] { "All Models", "Model X-100", "Model Y-200", "Model Z-300" });
            cmbModel.Location = new Point(0, 32);
            cmbModel.Margin = new Padding(0, 0, 6, 0);
            cmbModel.Name = "cmbModel";
            cmbModel.Size = new Size(461, 40);
            cmbModel.TabIndex = 2;
            // 
            // cmbType
            // 
            cmbType.Dock = DockStyle.Fill;
            cmbType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbType.Font = new Font("Segoe UI", 9F);
            cmbType.FormattingEnabled = true;
            cmbType.Items.AddRange(new object[] { "All Types", "Standard", "Premium", "Industrial" });
            cmbType.Location = new Point(473, 32);
            cmbType.Margin = new Padding(6, 0, 0, 0);
            cmbType.Name = "cmbType";
            cmbType.Size = new Size(462, 40);
            cmbType.TabIndex = 3;
            // 
            // tblFilterGrid2
            // 
            tblFilterGrid2.AutoSize = true;
            tblFilterGrid2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tblFilterGrid2.ColumnCount = 2;
            tblFilterGrid2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblFilterGrid2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblFilterGrid2.Controls.Add(lblFilterArea, 0, 0);
            tblFilterGrid2.Controls.Add(lblFilterOperator, 1, 0);
            tblFilterGrid2.Controls.Add(cmbArea, 0, 1);
            tblFilterGrid2.Controls.Add(cmbOperator, 1, 1);
            tblFilterGrid2.Dock = DockStyle.Fill;
            tblFilterGrid2.Location = new Point(0, 238);
            tblFilterGrid2.Margin = new Padding(0, 0, 0, 15);
            tblFilterGrid2.Name = "tblFilterGrid2";
            tblFilterGrid2.RowCount = 2;
            tblFilterGrid2.RowStyles.Add(new RowStyle());
            tblFilterGrid2.RowStyles.Add(new RowStyle());
            tblFilterGrid2.Size = new Size(935, 72);
            tblFilterGrid2.TabIndex = 7;
            // 
            // lblFilterArea
            // 
            lblFilterArea.AutoSize = true;
            lblFilterArea.Font = new Font("Segoe UI", 8F);
            lblFilterArea.ForeColor = Color.FromArgb(100, 116, 139);
            lblFilterArea.Location = new Point(0, 0);
            lblFilterArea.Margin = new Padding(0, 0, 0, 2);
            lblFilterArea.Name = "lblFilterArea";
            lblFilterArea.Size = new Size(58, 30);
            lblFilterArea.TabIndex = 0;
            lblFilterArea.Text = "Area";
            // 
            // lblFilterOperator
            // 
            lblFilterOperator.AutoSize = true;
            lblFilterOperator.Font = new Font("Segoe UI", 8F);
            lblFilterOperator.ForeColor = Color.FromArgb(100, 116, 139);
            lblFilterOperator.Location = new Point(473, 0);
            lblFilterOperator.Margin = new Padding(6, 0, 0, 2);
            lblFilterOperator.Name = "lblFilterOperator";
            lblFilterOperator.Size = new Size(102, 30);
            lblFilterOperator.TabIndex = 1;
            lblFilterOperator.Text = "Operator";
            // 
            // cmbArea
            // 
            cmbArea.Dock = DockStyle.Fill;
            cmbArea.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbArea.Font = new Font("Segoe UI", 9F);
            cmbArea.FormattingEnabled = true;
            cmbArea.Items.AddRange(new object[] { "All Areas", "Assembly Bay A", "Inspection Line 1", "Packaging Station" });
            cmbArea.Location = new Point(0, 32);
            cmbArea.Margin = new Padding(0, 0, 6, 0);
            cmbArea.Name = "cmbArea";
            cmbArea.Size = new Size(461, 40);
            cmbArea.TabIndex = 2;
            // 
            // cmbOperator
            // 
            cmbOperator.Dock = DockStyle.Fill;
            cmbOperator.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOperator.Font = new Font("Segoe UI", 9F);
            cmbOperator.FormattingEnabled = true;
            cmbOperator.Items.AddRange(new object[] { "All Operators", "John Doe (OP-101)", "Jane Smith (OP-102)", "Robert Johnson (OP-103)" });
            cmbOperator.Location = new Point(473, 32);
            cmbOperator.Margin = new Padding(6, 0, 0, 0);
            cmbOperator.Name = "cmbOperator";
            cmbOperator.Size = new Size(462, 40);
            cmbOperator.TabIndex = 3;
            // 
            // tblFilterGrid3
            // 
            tblFilterGrid3.AutoSize = true;
            tblFilterGrid3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tblFilterGrid3.ColumnCount = 2;
            tblFilterGrid3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblFilterGrid3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblFilterGrid3.Controls.Add(lblFilterResult, 0, 0);
            tblFilterGrid3.Controls.Add(lblFilterPart, 1, 0);
            tblFilterGrid3.Controls.Add(cmbResult, 0, 1);
            tblFilterGrid3.Controls.Add(txtPartNumber, 1, 1);
            tblFilterGrid3.Dock = DockStyle.Fill;
            tblFilterGrid3.Location = new Point(0, 325);
            tblFilterGrid3.Margin = new Padding(0, 0, 0, 11);
            tblFilterGrid3.Name = "tblFilterGrid3";
            tblFilterGrid3.RowCount = 2;
            tblFilterGrid3.RowStyles.Add(new RowStyle());
            tblFilterGrid3.RowStyles.Add(new RowStyle());
            tblFilterGrid3.Size = new Size(935, 72);
            tblFilterGrid3.TabIndex = 5;
            // 
            // lblFilterResult
            // 
            lblFilterResult.AutoSize = true;
            lblFilterResult.Font = new Font("Segoe UI", 8F);
            lblFilterResult.ForeColor = Color.FromArgb(100, 116, 139);
            lblFilterResult.Location = new Point(0, 0);
            lblFilterResult.Margin = new Padding(0, 0, 0, 2);
            lblFilterResult.Name = "lblFilterResult";
            lblFilterResult.Size = new Size(70, 30);
            lblFilterResult.TabIndex = 0;
            lblFilterResult.Text = "Result";
            // 
            // lblFilterPart
            // 
            lblFilterPart.AutoSize = true;
            lblFilterPart.Font = new Font("Segoe UI", 8F);
            lblFilterPart.ForeColor = Color.FromArgb(100, 116, 139);
            lblFilterPart.Location = new Point(473, 0);
            lblFilterPart.Margin = new Padding(6, 0, 0, 2);
            lblFilterPart.Name = "lblFilterPart";
            lblFilterPart.Size = new Size(136, 30);
            lblFilterPart.TabIndex = 1;
            lblFilterPart.Text = "Part Number";
            // 
            // cmbResult
            // 
            cmbResult.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbResult.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbResult.Font = new Font("Segoe UI", 9F);
            cmbResult.FormattingEnabled = true;
            cmbResult.Items.AddRange(new object[] { "All Results", "PASS Only", "FAIL Only" });
            cmbResult.Location = new Point(0, 32);
            cmbResult.Margin = new Padding(0, 0, 6, 0);
            cmbResult.Name = "cmbResult";
            cmbResult.Size = new Size(461, 40);
            cmbResult.TabIndex = 2;
            // 
            // txtPartNumber
            // 
            txtPartNumber.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtPartNumber.Font = new Font("Segoe UI", 9F);
            txtPartNumber.Location = new Point(473, 32);
            txtPartNumber.Margin = new Padding(6, 0, 0, 0);
            txtPartNumber.Name = "txtPartNumber";
            txtPartNumber.PlaceholderText = "Search by Part Number...";
            txtPartNumber.Size = new Size(462, 39);
            txtPartNumber.TabIndex = 3;
            // 
            // tblFilterButtons
            // 
            tblFilterButtons.ColumnCount = 2;
            tblFilterButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tblFilterButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tblFilterButtons.Controls.Add(btnGenerate, 0, 0);
            tblFilterButtons.Controls.Add(btnReset, 1, 0);
            tblFilterButtons.Dock = DockStyle.Fill;
            tblFilterButtons.Location = new Point(0, 408);
            tblFilterButtons.Margin = new Padding(0);
            tblFilterButtons.Name = "tblFilterButtons";
            tblFilterButtons.RowCount = 1;
            tblFilterButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblFilterButtons.Size = new Size(935, 66);
            tblFilterButtons.TabIndex = 8;
            // 
            // btnGenerate
            // 
            btnGenerate.BackColor = Color.FromArgb(255, 193, 7);
            btnGenerate.Dock = DockStyle.Fill;
            btnGenerate.FlatAppearance.BorderSize = 0;
            btnGenerate.FlatStyle = FlatStyle.Flat;
            btnGenerate.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnGenerate.ForeColor = Color.Black;
            btnGenerate.Location = new Point(0, 0);
            btnGenerate.Margin = new Padding(0, 0, 6, 0);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(601, 66);
            btnGenerate.TabIndex = 0;
            btnGenerate.Text = "Generate Report";
            btnGenerate.UseVisualStyleBackColor = false;
            // 
            // btnReset
            // 
            btnReset.BackColor = Color.FromArgb(241, 245, 249);
            btnReset.Dock = DockStyle.Fill;
            btnReset.FlatAppearance.BorderSize = 0;
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnReset.ForeColor = Color.FromArgb(51, 65, 85);
            btnReset.Location = new Point(613, 0);
            btnReset.Margin = new Padding(6, 0, 0, 0);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(322, 66);
            btnReset.TabIndex = 1;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = false;
            // 
            // pnlSeparator
            // 
            pnlSeparator.BackColor = Color.FromArgb(226, 232, 240);
            pnlSeparator.Dock = DockStyle.Fill;
            pnlSeparator.Location = new Point(0, 491);
            pnlSeparator.Margin = new Padding(0, 17, 0, 17);
            pnlSeparator.Name = "pnlSeparator";
            pnlSeparator.Size = new Size(935, 1);
            pnlSeparator.TabIndex = 9;
            // 
            // lblSummaryTitle
            // 
            lblSummaryTitle.Dock = DockStyle.Top;
            lblSummaryTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSummaryTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblSummaryTitle.Location = new Point(0, 506);
            lblSummaryTitle.Margin = new Padding(0, 0, 0, 11);
            lblSummaryTitle.Name = "lblSummaryTitle";
            lblSummaryTitle.Size = new Size(935, 41);
            lblSummaryTitle.TabIndex = 10;
            lblSummaryTitle.Text = "SUMMARY (08 Mar 2026 - 15 Mar 2026)";
            // 
            // tblSummaryCards
            // 
            tblSummaryCards.ColumnCount = 2;
            tblSummaryCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblSummaryCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblSummaryCards.Controls.Add(pnlSumTotal, 0, 0);
            tblSummaryCards.Controls.Add(pnlSumPass, 0, 1);
            tblSummaryCards.Controls.Add(pnlSumFail, 1, 1);
            tblSummaryCards.Dock = DockStyle.Fill;
            tblSummaryCards.Location = new Point(6, 562);
            tblSummaryCards.Margin = new Padding(6, 4, 6, 4);
            tblSummaryCards.Name = "tblSummaryCards";
            tblSummaryCards.RowCount = 2;
            tblSummaryCards.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tblSummaryCards.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tblSummaryCards.Size = new Size(923, 409);
            tblSummaryCards.TabIndex = 1;
            // 
            // pnlSumTotal
            // 
            pnlSumTotal.BackColor = Color.FromArgb(248, 250, 252);
            tblSummaryCards.SetColumnSpan(pnlSumTotal, 2);
            pnlSumTotal.Controls.Add(lblSumTotalKey);
            pnlSumTotal.Controls.Add(lblSumTotalVal);
            pnlSumTotal.Dock = DockStyle.Fill;
            pnlSumTotal.Location = new Point(0, 0);
            pnlSumTotal.Margin = new Padding(0, 0, 0, 6);
            pnlSumTotal.Name = "pnlSumTotal";
            pnlSumTotal.Padding = new Padding(17, 23, 17, 23);
            pnlSumTotal.Size = new Size(923, 198);
            pnlSumTotal.TabIndex = 0;
            // 
            // lblSumTotalKey
            // 
            lblSumTotalKey.Dock = DockStyle.Top;
            lblSumTotalKey.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblSumTotalKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblSumTotalKey.Location = new Point(17, 23);
            lblSumTotalKey.Margin = new Padding(6, 0, 6, 0);
            lblSumTotalKey.Name = "lblSumTotalKey";
            lblSumTotalKey.Size = new Size(889, 26);
            lblSumTotalKey.TabIndex = 0;
            lblSumTotalKey.Text = "TOTAL TESTED";
            lblSumTotalKey.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSumTotalVal
            // 
            lblSumTotalVal.Dock = DockStyle.Fill;
            lblSumTotalVal.Font = new Font("Segoe UI", 13.5F, FontStyle.Bold);
            lblSumTotalVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblSumTotalVal.Location = new Point(17, 23);
            lblSumTotalVal.Margin = new Padding(11, 0, 11, 0);
            lblSumTotalVal.Name = "lblSumTotalVal";
            lblSumTotalVal.Size = new Size(889, 152);
            lblSumTotalVal.TabIndex = 1;
            lblSumTotalVal.Text = "215";
            lblSumTotalVal.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlSumPass
            // 
            pnlSumPass.BackColor = Color.FromArgb(236, 253, 245);
            pnlSumPass.Controls.Add(lblSumPassKey);
            pnlSumPass.Controls.Add(lblSumPassVal);
            pnlSumPass.Dock = DockStyle.Fill;
            pnlSumPass.Location = new Point(0, 217);
            pnlSumPass.Margin = new Padding(0, 13, 11, 0);
            pnlSumPass.Name = "pnlSumPass";
            pnlSumPass.Padding = new Padding(17, 23, 17, 23);
            pnlSumPass.Size = new Size(450, 192);
            pnlSumPass.TabIndex = 1;
            // 
            // lblSumPassKey
            // 
            lblSumPassKey.Dock = DockStyle.Top;
            lblSumPassKey.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblSumPassKey.ForeColor = Color.FromArgb(6, 95, 70);
            lblSumPassKey.Location = new Point(17, 23);
            lblSumPassKey.Margin = new Padding(6, 0, 6, 0);
            lblSumPassKey.Name = "lblSumPassKey";
            lblSumPassKey.Size = new Size(416, 26);
            lblSumPassKey.TabIndex = 0;
            lblSumPassKey.Text = "PASS";
            lblSumPassKey.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSumPassVal
            // 
            lblSumPassVal.Dock = DockStyle.Fill;
            lblSumPassVal.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblSumPassVal.ForeColor = Color.FromArgb(4, 120, 87);
            lblSumPassVal.Location = new Point(17, 23);
            lblSumPassVal.Margin = new Padding(11, 0, 11, 0);
            lblSumPassVal.Name = "lblSumPassVal";
            lblSumPassVal.Size = new Size(416, 146);
            lblSumPassVal.TabIndex = 1;
            lblSumPassVal.Text = "212 (98.60%)";
            lblSumPassVal.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlSumFail
            // 
            pnlSumFail.BackColor = Color.FromArgb(254, 242, 242);
            pnlSumFail.Controls.Add(lblSumFailKey);
            pnlSumFail.Controls.Add(lblSumFailVal);
            pnlSumFail.Dock = DockStyle.Fill;
            pnlSumFail.Location = new Point(472, 217);
            pnlSumFail.Margin = new Padding(11, 13, 0, 0);
            pnlSumFail.Name = "pnlSumFail";
            pnlSumFail.Padding = new Padding(17, 23, 17, 23);
            pnlSumFail.Size = new Size(451, 192);
            pnlSumFail.TabIndex = 2;
            // 
            // lblSumFailKey
            // 
            lblSumFailKey.Dock = DockStyle.Top;
            lblSumFailKey.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblSumFailKey.ForeColor = Color.FromArgb(153, 27, 27);
            lblSumFailKey.Location = new Point(17, 23);
            lblSumFailKey.Margin = new Padding(6, 0, 6, 0);
            lblSumFailKey.Name = "lblSumFailKey";
            lblSumFailKey.Size = new Size(417, 26);
            lblSumFailKey.TabIndex = 0;
            lblSumFailKey.Text = "FAIL";
            lblSumFailKey.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSumFailVal
            // 
            lblSumFailVal.Dock = DockStyle.Fill;
            lblSumFailVal.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblSumFailVal.ForeColor = Color.FromArgb(220, 38, 38);
            lblSumFailVal.Location = new Point(17, 23);
            lblSumFailVal.Margin = new Padding(11, 0, 11, 0);
            lblSumFailVal.Name = "lblSumFailVal";
            lblSumFailVal.Size = new Size(417, 146);
            lblSumFailVal.TabIndex = 1;
            lblSumFailVal.Text = "3 (1.40%)";
            lblSumFailVal.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cardReportList
            // 
            cardReportList.BackColor = Color.White;
            cardReportList.Controls.Add(tblCenter);
            cardReportList.Dock = DockStyle.Fill;
            cardReportList.Location = new Point(0, 0);
            cardReportList.Margin = new Padding(0, 0, 13, 0);
            cardReportList.Name = "cardReportList";
            cardReportList.Padding = new Padding(20, 19, 20, 19);
            cardReportList.Size = new Size(1790, 1013);
            cardReportList.TabIndex = 1;
            // 
            // tblCenter
            // 
            tblCenter.ColumnCount = 1;
            tblCenter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblCenter.Controls.Add(lblReportListTitle, 0, 0);
            tblCenter.Controls.Add(gridReports, 0, 1);
            tblCenter.Controls.Add(tblPagination, 0, 2);
            tblCenter.Dock = DockStyle.Fill;
            tblCenter.Location = new Point(20, 19);
            tblCenter.Margin = new Padding(6, 4, 6, 4);
            tblCenter.Name = "tblCenter";
            tblCenter.RowCount = 3;
            tblCenter.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            tblCenter.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblCenter.RowStyles.Add(new RowStyle(SizeType.Absolute, 66F));
            tblCenter.Size = new Size(1750, 975);
            tblCenter.TabIndex = 0;
            // 
            // lblReportListTitle
            // 
            lblReportListTitle.Dock = DockStyle.Top;
            lblReportListTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblReportListTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblReportListTitle.Location = new Point(6, 0);
            lblReportListTitle.Margin = new Padding(6, 0, 6, 0);
            lblReportListTitle.Name = "lblReportListTitle";
            lblReportListTitle.Size = new Size(1738, 55);
            lblReportListTitle.TabIndex = 0;
            lblReportListTitle.Text = "REPORT LIST (Total: 18)";
            lblReportListTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // gridReports
            // 
            gridReports.AllowUserToAddRows = false;
            gridReports.AllowUserToDeleteRows = false;
            gridReports.AllowUserToOrderColumns = true;
            gridReports.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridReports.BackgroundColor = Color.White;
            gridReports.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(248, 250, 252);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(71, 85, 105);
            dataGridViewCellStyle1.Padding = new Padding(10, 4, 10, 4);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(248, 250, 252);
            dataGridViewCellStyle1.SelectionForeColor = Color.FromArgb(71, 85, 105);
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            gridReports.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            gridReports.ColumnHeadersHeight = 48;
            gridReports.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            gridReports.Columns.AddRange(new DataGridViewColumn[] { colNo, colReportName, colReportType, colDateGenerated, colGeneratedBy, colSize, colAction });
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = Color.White;
            dataGridViewCellStyle7.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle7.ForeColor = Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle7.Padding = new Padding(8, 4, 8, 4);
            dataGridViewCellStyle7.SelectionBackColor = Color.FromArgb(241, 245, 249);
            dataGridViewCellStyle7.SelectionForeColor = Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.False;
            gridReports.DefaultCellStyle = dataGridViewCellStyle7;
            gridReports.Dock = DockStyle.Fill;
            gridReports.EnableHeadersVisualStyles = false;
            gridReports.GridColor = Color.FromArgb(203, 213, 225);
            gridReports.Location = new Point(0, 83);
            gridReports.Margin = new Padding(0, 28, 0, 28);
            gridReports.MinimumSize = new Size(929, 747);
            gridReports.Name = "gridReports";
            gridReports.ReadOnly = true;
            gridReports.RowHeadersVisible = false;
            gridReports.RowHeadersWidth = 82;
            gridReports.RowTemplate.Height = 44;
            gridReports.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridReports.Size = new Size(1750, 798);
            gridReports.TabIndex = 1;
            // 
            // colNo
            // 
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colNo.DefaultCellStyle = dataGridViewCellStyle2;
            colNo.FillWeight = 25F;
            colNo.HeaderText = "No.";
            colNo.MinimumWidth = 40;
            colNo.Name = "colNo";
            colNo.ReadOnly = true;
            // 
            // colReportName
            // 
            colReportName.FillWeight = 140F;
            colReportName.HeaderText = "Report Name";
            colReportName.MinimumWidth = 180;
            colReportName.Name = "colReportName";
            colReportName.ReadOnly = true;
            // 
            // colReportType
            // 
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colReportType.DefaultCellStyle = dataGridViewCellStyle3;
            colReportType.FillWeight = 70F;
            colReportType.HeaderText = "Report Type";
            colReportType.MinimumWidth = 90;
            colReportType.Name = "colReportType";
            colReportType.ReadOnly = true;
            // 
            // colDateGenerated
            // 
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colDateGenerated.DefaultCellStyle = dataGridViewCellStyle4;
            colDateGenerated.FillWeight = 95F;
            colDateGenerated.HeaderText = "Date Generated";
            colDateGenerated.MinimumWidth = 130;
            colDateGenerated.Name = "colDateGenerated";
            colDateGenerated.ReadOnly = true;
            // 
            // colGeneratedBy
            // 
            colGeneratedBy.FillWeight = 75F;
            colGeneratedBy.HeaderText = "Generated By";
            colGeneratedBy.MinimumWidth = 100;
            colGeneratedBy.Name = "colGeneratedBy";
            colGeneratedBy.ReadOnly = true;
            // 
            // colSize
            // 
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSize.DefaultCellStyle = dataGridViewCellStyle5;
            colSize.FillWeight = 45F;
            colSize.HeaderText = "Size";
            colSize.MinimumWidth = 60;
            colSize.Name = "colSize";
            colSize.ReadOnly = true;
            // 
            // colAction
            // 
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colAction.DefaultCellStyle = dataGridViewCellStyle6;
            colAction.FillWeight = 125F;
            colAction.HeaderText = "Action";
            colAction.MinimumWidth = 160;
            colAction.Name = "colAction";
            colAction.ReadOnly = true;
            // 
            // tblPagination
            // 
            tblPagination.ColumnCount = 2;
            tblPagination.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblPagination.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblPagination.Controls.Add(lblPageInfo, 0, 0);
            tblPagination.Controls.Add(flpPageButtons, 1, 0);
            tblPagination.Dock = DockStyle.Fill;
            tblPagination.Location = new Point(0, 909);
            tblPagination.Margin = new Padding(0);
            tblPagination.Name = "tblPagination";
            tblPagination.RowCount = 1;
            tblPagination.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblPagination.Size = new Size(1750, 66);
            tblPagination.TabIndex = 2;
            // 
            // lblPageInfo
            // 
            lblPageInfo.Dock = DockStyle.Fill;
            lblPageInfo.Font = new Font("Segoe UI", 8.5F);
            lblPageInfo.ForeColor = Color.FromArgb(100, 116, 139);
            lblPageInfo.Location = new Point(6, 0);
            lblPageInfo.Margin = new Padding(6, 0, 6, 0);
            lblPageInfo.Name = "lblPageInfo";
            lblPageInfo.Size = new Size(863, 66);
            lblPageInfo.TabIndex = 0;
            lblPageInfo.Text = "Showing 1 to 10 of 18 entries";
            lblPageInfo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // flpPageButtons
            // 
            flpPageButtons.Controls.Add(btnPageLast);
            flpPageButtons.Controls.Add(btnPageNext);
            flpPageButtons.Controls.Add(btnPageDots);
            flpPageButtons.Controls.Add(btnPage2);
            flpPageButtons.Controls.Add(btnPage1);
            flpPageButtons.Controls.Add(btnPagePrev);
            flpPageButtons.Controls.Add(btnPageFirst);
            flpPageButtons.Dock = DockStyle.Fill;
            flpPageButtons.FlowDirection = FlowDirection.RightToLeft;
            flpPageButtons.Location = new Point(875, 4);
            flpPageButtons.Margin = new Padding(0, 4, 0, 0);
            flpPageButtons.Name = "flpPageButtons";
            flpPageButtons.Size = new Size(875, 62);
            flpPageButtons.TabIndex = 1;
            // 
            // btnPageLast
            // 
            btnPageLast.BackColor = Color.FromArgb(241, 245, 249);
            btnPageLast.FlatAppearance.BorderSize = 0;
            btnPageLast.FlatStyle = FlatStyle.Flat;
            btnPageLast.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnPageLast.Location = new Point(810, 0);
            btnPageLast.Margin = new Padding(4, 0, 0, 0);
            btnPageLast.Name = "btnPageLast";
            btnPageLast.Size = new Size(65, 62);
            btnPageLast.TabIndex = 0;
            btnPageLast.Text = ">>";
            btnPageLast.UseVisualStyleBackColor = false;
            // 
            // btnPageNext
            // 
            btnPageNext.BackColor = Color.FromArgb(241, 245, 249);
            btnPageNext.FlatAppearance.BorderSize = 0;
            btnPageNext.FlatStyle = FlatStyle.Flat;
            btnPageNext.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnPageNext.Location = new Point(745, 0);
            btnPageNext.Margin = new Padding(4, 0, 0, 0);
            btnPageNext.Name = "btnPageNext";
            btnPageNext.Size = new Size(61, 70);
            btnPageNext.TabIndex = 1;
            btnPageNext.Text = ">";
            btnPageNext.UseVisualStyleBackColor = false;
            // 
            // btnPageDots
            // 
            btnPageDots.BackColor = Color.White;
            btnPageDots.FlatAppearance.BorderSize = 0;
            btnPageDots.FlatStyle = FlatStyle.Flat;
            btnPageDots.Font = new Font("Segoe UI", 8.5F);
            btnPageDots.Location = new Point(687, 0);
            btnPageDots.Margin = new Padding(4, 0, 0, 0);
            btnPageDots.Name = "btnPageDots";
            btnPageDots.Size = new Size(54, 47);
            btnPageDots.TabIndex = 2;
            btnPageDots.Text = "...";
            btnPageDots.UseVisualStyleBackColor = false;
            // 
            // btnPage2
            // 
            btnPage2.BackColor = Color.FromArgb(241, 245, 249);
            btnPage2.FlatAppearance.BorderSize = 0;
            btnPage2.FlatStyle = FlatStyle.Flat;
            btnPage2.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnPage2.Location = new Point(625, 0);
            btnPage2.Margin = new Padding(4, 0, 0, 0);
            btnPage2.Name = "btnPage2";
            btnPage2.Size = new Size(58, 62);
            btnPage2.TabIndex = 3;
            btnPage2.Text = "2";
            btnPage2.UseVisualStyleBackColor = false;
            // 
            // btnPage1
            // 
            btnPage1.BackColor = Color.FromArgb(30, 41, 59);
            btnPage1.FlatAppearance.BorderSize = 0;
            btnPage1.FlatStyle = FlatStyle.Flat;
            btnPage1.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnPage1.ForeColor = Color.White;
            btnPage1.Location = new Point(563, 0);
            btnPage1.Margin = new Padding(4, 0, 0, 0);
            btnPage1.Name = "btnPage1";
            btnPage1.Size = new Size(58, 62);
            btnPage1.TabIndex = 4;
            btnPage1.Text = "1";
            btnPage1.UseVisualStyleBackColor = false;
            // 
            // btnPagePrev
            // 
            btnPagePrev.BackColor = Color.FromArgb(241, 245, 249);
            btnPagePrev.FlatAppearance.BorderSize = 0;
            btnPagePrev.FlatStyle = FlatStyle.Flat;
            btnPagePrev.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnPagePrev.Location = new Point(498, 0);
            btnPagePrev.Margin = new Padding(4, 0, 0, 0);
            btnPagePrev.Name = "btnPagePrev";
            btnPagePrev.Size = new Size(61, 62);
            btnPagePrev.TabIndex = 5;
            btnPagePrev.Text = "<";
            btnPagePrev.UseVisualStyleBackColor = false;
            // 
            // btnPageFirst
            // 
            btnPageFirst.BackColor = Color.FromArgb(241, 245, 249);
            btnPageFirst.FlatAppearance.BorderSize = 0;
            btnPageFirst.FlatStyle = FlatStyle.Flat;
            btnPageFirst.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnPageFirst.Location = new Point(429, 0);
            btnPageFirst.Margin = new Padding(4, 0, 0, 0);
            btnPageFirst.Name = "btnPageFirst";
            btnPageFirst.Size = new Size(65, 62);
            btnPageFirst.TabIndex = 6;
            btnPageFirst.Text = "<<";
            btnPageFirst.UseVisualStyleBackColor = false;
            // 
            // cardTrend
            // 
            cardTrend.BackColor = Color.White;
            cardTrend.Controls.Add(lblTrendTitle);
            cardTrend.Dock = DockStyle.Fill;
            cardTrend.Location = new Point(0, 0);
            cardTrend.Margin = new Padding(0);
            cardTrend.Name = "cardTrend";
            cardTrend.Padding = new Padding(20, 19, 20, 19);
            cardTrend.Size = new Size(2776, 1307);
            cardTrend.TabIndex = 2;
            // 
            // lblTrendTitle
            // 
            lblTrendTitle.Dock = DockStyle.Top;
            lblTrendTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblTrendTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblTrendTitle.Location = new Point(20, 19);
            lblTrendTitle.Margin = new Padding(6, 0, 6, 0);
            lblTrendTitle.Name = "lblTrendTitle";
            lblTrendTitle.Size = new Size(2736, 43);
            lblTrendTitle.TabIndex = 0;
            lblTrendTitle.Text = "RESULT TREND";
            // 
            // tblColRight
            // 
            tblColRight.ColumnCount = 1;
            tblColRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblColRight.Controls.Add(cardPreviewTop, 0, 0);
            tblColRight.Controls.Add(pnlPreviewContainer, 0, 1);
            tblColRight.Dock = DockStyle.Fill;
            tblColRight.Location = new Point(2792, 0);
            tblColRight.Margin = new Padding(16, 0, 0, 0);
            tblColRight.Name = "tblColRight";
            tblColRight.RowCount = 2;
            tblColRight.RowStyles.Add(new RowStyle());
            tblColRight.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblColRight.Size = new Size(1174, 2331);
            tblColRight.TabIndex = 2;
            // 
            // cardPreviewTop
            // 
            cardPreviewTop.AutoSize = true;
            cardPreviewTop.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            cardPreviewTop.BackColor = Color.White;
            cardPreviewTop.Controls.Add(flpPreviewButtons);
            cardPreviewTop.Controls.Add(lblPreviewTitle);
            cardPreviewTop.Dock = DockStyle.Fill;
            cardPreviewTop.Location = new Point(0, 0);
            cardPreviewTop.Margin = new Padding(0, 0, 0, 11);
            cardPreviewTop.Name = "cardPreviewTop";
            cardPreviewTop.Padding = new Padding(20, 19, 20, 19);
            cardPreviewTop.Size = new Size(1174, 329);
            cardPreviewTop.TabIndex = 0;
            // 
            // flpPreviewButtons
            // 
            flpPreviewButtons.AutoSize = true;
            flpPreviewButtons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flpPreviewButtons.BackColor = Color.White;
            flpPreviewButtons.ColumnCount = 6;
            flpPreviewButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666F));
            flpPreviewButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666F));
            flpPreviewButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666F));
            flpPreviewButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666F));
            flpPreviewButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666F));
            flpPreviewButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666F));
            flpPreviewButtons.Controls.Add(btnExportPDF, 0, 0);
            flpPreviewButtons.Controls.Add(btnExportExcel, 2, 0);
            flpPreviewButtons.Controls.Add(btnPrint, 4, 0);
            flpPreviewButtons.Controls.Add(btnEditReport, 0, 1);
            flpPreviewButtons.Controls.Add(btnDeleteReport, 3, 1);
            flpPreviewButtons.Controls.Add(btnCancelReportEdit, 0, 2);
            flpPreviewButtons.Controls.Add(btnSaveReportEdit, 3, 2);
            flpPreviewButtons.Dock = DockStyle.Top;
            flpPreviewButtons.Location = new Point(20, 64);
            flpPreviewButtons.Margin = new Padding(0);
            flpPreviewButtons.Name = "flpPreviewButtons";
            flpPreviewButtons.RowCount = 3;
            flpPreviewButtons.RowStyles.Add(new RowStyle());
            flpPreviewButtons.RowStyles.Add(new RowStyle());
            flpPreviewButtons.RowStyles.Add(new RowStyle());
            flpPreviewButtons.Size = new Size(1134, 246);
            flpPreviewButtons.TabIndex = 1;
            // 
            // btnExportPDF
            // 
            btnExportPDF.BackColor = Color.White;
            flpPreviewButtons.SetColumnSpan(btnExportPDF, 2);
            btnExportPDF.Dock = DockStyle.Fill;
            btnExportPDF.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnExportPDF.FlatStyle = FlatStyle.Flat;
            btnExportPDF.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnExportPDF.ForeColor = Color.FromArgb(220, 53, 69);
            btnExportPDF.Location = new Point(7, 9);
            btnExportPDF.Margin = new Padding(7, 9, 7, 9);
            btnExportPDF.Name = "btnExportPDF";
            btnExportPDF.Size = new Size(364, 62);
            btnExportPDF.TabIndex = 0;
            btnExportPDF.Text = "Export PDF";
            btnExportPDF.UseVisualStyleBackColor = false;
            // 
            // btnExportExcel
            // 
            btnExportExcel.BackColor = Color.FromArgb(241, 245, 249);
            flpPreviewButtons.SetColumnSpan(btnExportExcel, 2);
            btnExportExcel.Dock = DockStyle.Fill;
            btnExportExcel.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnExportExcel.FlatStyle = FlatStyle.Flat;
            btnExportExcel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnExportExcel.ForeColor = Color.FromArgb(40, 167, 69);
            btnExportExcel.Location = new Point(385, 9);
            btnExportExcel.Margin = new Padding(7, 9, 7, 9);
            btnExportExcel.Name = "btnExportExcel";
            btnExportExcel.Size = new Size(364, 62);
            btnExportExcel.TabIndex = 1;
            btnExportExcel.Text = "Export Excel";
            btnExportExcel.UseVisualStyleBackColor = false;
            // 
            // btnPrint
            // 
            btnPrint.BackColor = Color.FromArgb(241, 245, 249);
            flpPreviewButtons.SetColumnSpan(btnPrint, 2);
            btnPrint.Dock = DockStyle.Fill;
            btnPrint.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnPrint.FlatStyle = FlatStyle.Flat;
            btnPrint.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnPrint.ForeColor = Color.Black;
            btnPrint.Location = new Point(763, 9);
            btnPrint.Margin = new Padding(7, 9, 7, 9);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(364, 62);
            btnPrint.TabIndex = 2;
            btnPrint.Text = "Print";
            btnPrint.UseVisualStyleBackColor = false;
            // 
            // btnEditReport
            // 
            btnEditReport.BackColor = Color.FromArgb(37, 99, 235);
            flpPreviewButtons.SetColumnSpan(btnEditReport, 3);
            btnEditReport.Dock = DockStyle.Fill;
            btnEditReport.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnEditReport.FlatStyle = FlatStyle.Flat;
            btnEditReport.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEditReport.ForeColor = Color.White;
            btnEditReport.Location = new Point(7, 89);
            btnEditReport.Margin = new Padding(7, 9, 7, 9);
            btnEditReport.Name = "btnEditReport";
            btnEditReport.Size = new Size(553, 62);
            btnEditReport.TabIndex = 3;
            btnEditReport.Text = "Edit";
            btnEditReport.UseVisualStyleBackColor = false;
            // 
            // btnDeleteReport
            // 
            btnDeleteReport.BackColor = Color.FromArgb(220, 38, 38);
            flpPreviewButtons.SetColumnSpan(btnDeleteReport, 3);
            btnDeleteReport.Dock = DockStyle.Fill;
            btnDeleteReport.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnDeleteReport.FlatStyle = FlatStyle.Flat;
            btnDeleteReport.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDeleteReport.ForeColor = Color.White;
            btnDeleteReport.Location = new Point(574, 89);
            btnDeleteReport.Margin = new Padding(7, 9, 7, 9);
            btnDeleteReport.Name = "btnDeleteReport";
            btnDeleteReport.Size = new Size(553, 62);
            btnDeleteReport.TabIndex = 4;
            btnDeleteReport.Text = "Delete";
            btnDeleteReport.UseVisualStyleBackColor = false;
            // 
            // btnCancelReportEdit
            // 
            btnCancelReportEdit.BackColor = Color.FromArgb(100, 116, 139);
            flpPreviewButtons.SetColumnSpan(btnCancelReportEdit, 3);
            btnCancelReportEdit.Dock = DockStyle.Fill;
            btnCancelReportEdit.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnCancelReportEdit.FlatStyle = FlatStyle.Flat;
            btnCancelReportEdit.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCancelReportEdit.ForeColor = Color.White;
            btnCancelReportEdit.Location = new Point(7, 169);
            btnCancelReportEdit.Margin = new Padding(7, 9, 7, 9);
            btnCancelReportEdit.Name = "btnCancelReportEdit";
            btnCancelReportEdit.Size = new Size(553, 68);
            btnCancelReportEdit.TabIndex = 6;
            btnCancelReportEdit.Text = "Cancel";
            btnCancelReportEdit.UseVisualStyleBackColor = false;
            btnCancelReportEdit.Visible = false;
            // 
            // btnSaveReportEdit
            // 
            btnSaveReportEdit.BackColor = Color.FromArgb(22, 163, 74);
            flpPreviewButtons.SetColumnSpan(btnSaveReportEdit, 3);
            btnSaveReportEdit.Dock = DockStyle.Fill;
            btnSaveReportEdit.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnSaveReportEdit.FlatStyle = FlatStyle.Flat;
            btnSaveReportEdit.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSaveReportEdit.ForeColor = Color.White;
            btnSaveReportEdit.Location = new Point(574, 169);
            btnSaveReportEdit.Margin = new Padding(7, 9, 7, 9);
            btnSaveReportEdit.Name = "btnSaveReportEdit";
            btnSaveReportEdit.Size = new Size(553, 68);
            btnSaveReportEdit.TabIndex = 5;
            btnSaveReportEdit.Text = "Save";
            btnSaveReportEdit.UseVisualStyleBackColor = false;
            btnSaveReportEdit.Visible = false;
            // 
            // lblPreviewTitle
            // 
            lblPreviewTitle.Dock = DockStyle.Top;
            lblPreviewTitle.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            lblPreviewTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblPreviewTitle.Location = new Point(20, 19);
            lblPreviewTitle.Margin = new Padding(6, 0, 6, 0);
            lblPreviewTitle.Name = "lblPreviewTitle";
            lblPreviewTitle.Size = new Size(1134, 45);
            lblPreviewTitle.TabIndex = 0;
            lblPreviewTitle.Text = "REPORT PREVIEW";
            lblPreviewTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlPreviewContainer
            // 
            pnlPreviewContainer.AutoScroll = true;
            pnlPreviewContainer.BackColor = Color.White;
            pnlPreviewContainer.Controls.Add(cardPaper);
            pnlPreviewContainer.Dock = DockStyle.Fill;
            pnlPreviewContainer.Location = new Point(0, 340);
            pnlPreviewContainer.Margin = new Padding(0);
            pnlPreviewContainer.Name = "pnlPreviewContainer";
            pnlPreviewContainer.Padding = new Padding(28, 35, 28, 35);
            pnlPreviewContainer.Size = new Size(1174, 1991);
            pnlPreviewContainer.TabIndex = 1;
            // 
            // cardPaper
            // 
            cardPaper.AutoScroll = true;
            cardPaper.BackColor = Color.White;
            cardPaper.BorderStyle = BorderStyle.FixedSingle;
            cardPaper.Controls.Add(tblPaperContent);
            cardPaper.Dock = DockStyle.Fill;
            cardPaper.Location = new Point(28, 35);
            cardPaper.Margin = new Padding(6, 4, 6, 4);
            cardPaper.Name = "cardPaper";
            cardPaper.Padding = new Padding(26, 34, 26, 34);
            cardPaper.Size = new Size(1118, 1921);
            cardPaper.TabIndex = 0;
            // 
            // tblPaperContent
            // 
            tblPaperContent.ColumnCount = 1;
            tblPaperContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblPaperContent.Controls.Add(tblPaperHeader, 0, 0);
            tblPaperContent.Controls.Add(pnlGenInfoBox, 0, 1);
            tblPaperContent.Controls.Add(pnlSummaryTableContainer, 0, 2);
            tblPaperContent.Controls.Add(pnlResultBox, 0, 3);
            tblPaperContent.Controls.Add(pnlNotesBox, 0, 4);
            tblPaperContent.Dock = DockStyle.Fill;
            tblPaperContent.Location = new Point(26, 34);
            tblPaperContent.Margin = new Padding(6, 4, 6, 4);
            tblPaperContent.Name = "tblPaperContent";
            tblPaperContent.RowCount = 5;
            tblPaperContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 105F));
            tblPaperContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 382F));
            tblPaperContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblPaperContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            tblPaperContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 230F));
            tblPaperContent.Size = new Size(1064, 1851);
            tblPaperContent.TabIndex = 0;
            // 
            // tblPaperHeader
            // 
            tblPaperHeader.ColumnCount = 2;
            tblPaperHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblPaperHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblPaperHeader.Controls.Add(flpPaperLogo, 0, 0);
            tblPaperHeader.Controls.Add(lblPaperMeta, 1, 0);
            tblPaperHeader.Dock = DockStyle.Fill;
            tblPaperHeader.Location = new Point(0, 0);
            tblPaperHeader.Margin = new Padding(0, 0, 0, 17);
            tblPaperHeader.Name = "tblPaperHeader";
            tblPaperHeader.RowCount = 1;
            tblPaperHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblPaperHeader.Size = new Size(1064, 88);
            tblPaperHeader.TabIndex = 0;
            // 
            // flpPaperLogo
            // 
            flpPaperLogo.Controls.Add(lblLogoBadge);
            flpPaperLogo.Controls.Add(lblPaperTitle);
            flpPaperLogo.Dock = DockStyle.Fill;
            flpPaperLogo.Location = new Point(0, 0);
            flpPaperLogo.Margin = new Padding(0);
            flpPaperLogo.Name = "flpPaperLogo";
            flpPaperLogo.Size = new Size(532, 88);
            flpPaperLogo.TabIndex = 0;
            flpPaperLogo.WrapContents = false;
            // 
            // lblLogoBadge
            // 
            lblLogoBadge.BackColor = Color.FromArgb(30, 41, 59);
            lblLogoBadge.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblLogoBadge.ForeColor = Color.White;
            lblLogoBadge.Location = new Point(0, 6);
            lblLogoBadge.Margin = new Padding(0, 6, 17, 0);
            lblLogoBadge.Name = "lblLogoBadge";
            lblLogoBadge.Size = new Size(89, 64);
            lblLogoBadge.TabIndex = 0;
            lblLogoBadge.Text = "CAT";
            lblLogoBadge.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblPaperTitle
            // 
            lblPaperTitle.AutoSize = true;
            lblPaperTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblPaperTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblPaperTitle.Location = new Point(106, 13);
            lblPaperTitle.Margin = new Padding(0, 13, 0, 0);
            lblPaperTitle.Name = "lblPaperTitle";
            lblPaperTitle.Size = new Size(223, 45);
            lblPaperTitle.TabIndex = 1;
            lblPaperTitle.Text = "TEST REPORT";
            // 
            // lblPaperMeta
            // 
            lblPaperMeta.Dock = DockStyle.Fill;
            lblPaperMeta.Font = new Font("Segoe UI", 8F);
            lblPaperMeta.ForeColor = Color.FromArgb(71, 85, 105);
            lblPaperMeta.Location = new Point(538, 0);
            lblPaperMeta.Margin = new Padding(6, 0, 6, 0);
            lblPaperMeta.Name = "lblPaperMeta";
            lblPaperMeta.Size = new Size(520, 88);
            lblPaperMeta.TabIndex = 1;
            lblPaperMeta.Text = "Report No.: TR-20260315-103020\r\nDate: 15 Mar 2026\r\nPage: 1 of 2";
            lblPaperMeta.TextAlign = ContentAlignment.TopRight;
            // 
            // pnlGenInfoBox
            // 
            pnlGenInfoBox.BackColor = Color.FromArgb(248, 250, 252);
            pnlGenInfoBox.Controls.Add(tblGenGrid);
            pnlGenInfoBox.Controls.Add(lblGenTitle);
            pnlGenInfoBox.Dock = DockStyle.Fill;
            pnlGenInfoBox.Location = new Point(0, 105);
            pnlGenInfoBox.Margin = new Padding(0, 0, 0, 17);
            pnlGenInfoBox.Name = "pnlGenInfoBox";
            pnlGenInfoBox.Padding = new Padding(32, 36, 32, 36);
            pnlGenInfoBox.Size = new Size(1064, 365);
            pnlGenInfoBox.TabIndex = 1;
            // 
            // tblGenGrid
            // 
            tblGenGrid.ColumnCount = 4;
            tblGenGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblGenGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblGenGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblGenGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblGenGrid.Controls.Add(lblGenModelKey, 0, 0);
            tblGenGrid.Controls.Add(lblGenModelVal, 1, 0);
            tblGenGrid.Controls.Add(lblGenEcnKey, 2, 0);
            tblGenGrid.Controls.Add(lblGenEcnVal, 3, 0);
            tblGenGrid.Controls.Add(lblGenTypeKey, 0, 1);
            tblGenGrid.Controls.Add(lblGenTypeVal, 1, 1);
            tblGenGrid.Controls.Add(lblGenRevKey, 2, 1);
            tblGenGrid.Controls.Add(lblGenRevVal, 3, 1);
            tblGenGrid.Controls.Add(lblGenAreaKey, 0, 2);
            tblGenGrid.Controls.Add(lblGenAreaVal, 1, 2);
            tblGenGrid.Controls.Add(lblGenOpKey, 2, 2);
            tblGenGrid.Controls.Add(lblGenOpVal, 3, 2);
            tblGenGrid.Controls.Add(lblGenPartKey, 0, 3);
            tblGenGrid.Controls.Add(lblGenPartVal, 1, 3);
            tblGenGrid.Controls.Add(lblGenTimeKey, 2, 3);
            tblGenGrid.Controls.Add(lblGenTimeVal, 3, 3);
            tblGenGrid.Controls.Add(lblGenSnKey, 0, 4);
            tblGenGrid.Controls.Add(lblGenSnVal, 1, 4);
            tblGenGrid.Controls.Add(lblGenResKey, 2, 4);
            tblGenGrid.Controls.Add(lblGenResVal, 3, 4);
            tblGenGrid.Dock = DockStyle.Fill;
            tblGenGrid.Location = new Point(32, 100);
            tblGenGrid.Margin = new Padding(6, 4, 6, 4);
            tblGenGrid.Name = "tblGenGrid";
            tblGenGrid.RowCount = 5;
            tblGenGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tblGenGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tblGenGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tblGenGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tblGenGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tblGenGrid.Size = new Size(1000, 229);
            tblGenGrid.TabIndex = 1;
            // 
            // lblGenModelKey
            // 
            lblGenModelKey.Dock = DockStyle.Fill;
            lblGenModelKey.Font = new Font("Segoe UI", 8F);
            lblGenModelKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblGenModelKey.Location = new Point(6, 0);
            lblGenModelKey.Margin = new Padding(6, 0, 6, 0);
            lblGenModelKey.Name = "lblGenModelKey";
            lblGenModelKey.Size = new Size(238, 45);
            lblGenModelKey.TabIndex = 0;
            lblGenModelKey.Text = "Product Model:";
            lblGenModelKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGenModelVal
            // 
            lblGenModelVal.Dock = DockStyle.Fill;
            lblGenModelVal.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblGenModelVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblGenModelVal.Location = new Point(256, 0);
            lblGenModelVal.Margin = new Padding(6, 0, 6, 0);
            lblGenModelVal.Name = "lblGenModelVal";
            lblGenModelVal.Size = new Size(238, 45);
            lblGenModelVal.TabIndex = 1;
            lblGenModelVal.Text = "6060";
            lblGenModelVal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGenEcnKey
            // 
            lblGenEcnKey.Dock = DockStyle.Fill;
            lblGenEcnKey.Font = new Font("Segoe UI", 8F);
            lblGenEcnKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblGenEcnKey.Location = new Point(506, 0);
            lblGenEcnKey.Margin = new Padding(6, 0, 6, 0);
            lblGenEcnKey.Name = "lblGenEcnKey";
            lblGenEcnKey.Size = new Size(238, 45);
            lblGenEcnKey.TabIndex = 2;
            lblGenEcnKey.Text = "ECN No:";
            lblGenEcnKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGenEcnVal
            // 
            lblGenEcnVal.Dock = DockStyle.Fill;
            lblGenEcnVal.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblGenEcnVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblGenEcnVal.Location = new Point(756, 0);
            lblGenEcnVal.Margin = new Padding(6, 0, 6, 0);
            lblGenEcnVal.Name = "lblGenEcnVal";
            lblGenEcnVal.Size = new Size(238, 45);
            lblGenEcnVal.TabIndex = 3;
            lblGenEcnVal.Text = "123.R001";
            lblGenEcnVal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGenTypeKey
            // 
            lblGenTypeKey.Dock = DockStyle.Fill;
            lblGenTypeKey.Font = new Font("Segoe UI", 8F);
            lblGenTypeKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblGenTypeKey.Location = new Point(6, 45);
            lblGenTypeKey.Margin = new Padding(6, 0, 6, 0);
            lblGenTypeKey.Name = "lblGenTypeKey";
            lblGenTypeKey.Size = new Size(238, 45);
            lblGenTypeKey.TabIndex = 4;
            lblGenTypeKey.Text = "Product Type:";
            lblGenTypeKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGenTypeVal
            // 
            lblGenTypeVal.Dock = DockStyle.Fill;
            lblGenTypeVal.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblGenTypeVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblGenTypeVal.Location = new Point(256, 45);
            lblGenTypeVal.Margin = new Padding(6, 0, 6, 0);
            lblGenTypeVal.Name = "lblGenTypeVal";
            lblGenTypeVal.Size = new Size(238, 45);
            lblGenTypeVal.TabIndex = 5;
            lblGenTypeVal.Text = "AC";
            lblGenTypeVal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGenRevKey
            // 
            lblGenRevKey.Dock = DockStyle.Fill;
            lblGenRevKey.Font = new Font("Segoe UI", 8F);
            lblGenRevKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblGenRevKey.Location = new Point(506, 45);
            lblGenRevKey.Margin = new Padding(6, 0, 6, 0);
            lblGenRevKey.Name = "lblGenRevKey";
            lblGenRevKey.Size = new Size(238, 45);
            lblGenRevKey.TabIndex = 6;
            lblGenRevKey.Text = "Revision:";
            lblGenRevKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGenRevVal
            // 
            lblGenRevVal.Dock = DockStyle.Fill;
            lblGenRevVal.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblGenRevVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblGenRevVal.Location = new Point(756, 45);
            lblGenRevVal.Margin = new Padding(6, 0, 6, 0);
            lblGenRevVal.Name = "lblGenRevVal";
            lblGenRevVal.Size = new Size(238, 45);
            lblGenRevVal.TabIndex = 7;
            lblGenRevVal.Text = "A";
            lblGenRevVal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGenAreaKey
            // 
            lblGenAreaKey.Dock = DockStyle.Fill;
            lblGenAreaKey.Font = new Font("Segoe UI", 8F);
            lblGenAreaKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblGenAreaKey.Location = new Point(6, 90);
            lblGenAreaKey.Margin = new Padding(6, 0, 6, 0);
            lblGenAreaKey.Name = "lblGenAreaKey";
            lblGenAreaKey.Size = new Size(238, 45);
            lblGenAreaKey.TabIndex = 8;
            lblGenAreaKey.Text = "Area:";
            lblGenAreaKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGenAreaVal
            // 
            lblGenAreaVal.Dock = DockStyle.Fill;
            lblGenAreaVal.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblGenAreaVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblGenAreaVal.Location = new Point(256, 90);
            lblGenAreaVal.Margin = new Padding(6, 0, 6, 0);
            lblGenAreaVal.Name = "lblGenAreaVal";
            lblGenAreaVal.Size = new Size(238, 45);
            lblGenAreaVal.TabIndex = 9;
            lblGenAreaVal.Text = "Engine Control";
            lblGenAreaVal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGenOpKey
            // 
            lblGenOpKey.Dock = DockStyle.Fill;
            lblGenOpKey.Font = new Font("Segoe UI", 8F);
            lblGenOpKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblGenOpKey.Location = new Point(506, 90);
            lblGenOpKey.Margin = new Padding(6, 0, 6, 0);
            lblGenOpKey.Name = "lblGenOpKey";
            lblGenOpKey.Size = new Size(238, 45);
            lblGenOpKey.TabIndex = 10;
            lblGenOpKey.Text = "Operator:";
            lblGenOpKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGenOpVal
            // 
            lblGenOpVal.Dock = DockStyle.Fill;
            lblGenOpVal.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblGenOpVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblGenOpVal.Location = new Point(756, 90);
            lblGenOpVal.Margin = new Padding(6, 0, 6, 0);
            lblGenOpVal.Name = "lblGenOpVal";
            lblGenOpVal.Size = new Size(238, 45);
            lblGenOpVal.TabIndex = 11;
            lblGenOpVal.Text = "Fally";
            lblGenOpVal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGenPartKey
            // 
            lblGenPartKey.Dock = DockStyle.Fill;
            lblGenPartKey.Font = new Font("Segoe UI", 8F);
            lblGenPartKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblGenPartKey.Location = new Point(6, 135);
            lblGenPartKey.Margin = new Padding(6, 0, 6, 0);
            lblGenPartKey.Name = "lblGenPartKey";
            lblGenPartKey.Size = new Size(238, 45);
            lblGenPartKey.TabIndex = 12;
            lblGenPartKey.Text = "Part Number:";
            lblGenPartKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGenPartVal
            // 
            lblGenPartVal.Dock = DockStyle.Fill;
            lblGenPartVal.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblGenPartVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblGenPartVal.Location = new Point(256, 135);
            lblGenPartVal.Margin = new Padding(6, 0, 6, 0);
            lblGenPartVal.Name = "lblGenPartVal";
            lblGenPartVal.Size = new Size(238, 45);
            lblGenPartVal.TabIndex = 13;
            lblGenPartVal.Text = "WH-ENG-001";
            lblGenPartVal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGenTimeKey
            // 
            lblGenTimeKey.Dock = DockStyle.Fill;
            lblGenTimeKey.Font = new Font("Segoe UI", 8F);
            lblGenTimeKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblGenTimeKey.Location = new Point(506, 135);
            lblGenTimeKey.Margin = new Padding(6, 0, 6, 0);
            lblGenTimeKey.Name = "lblGenTimeKey";
            lblGenTimeKey.Size = new Size(238, 45);
            lblGenTimeKey.TabIndex = 14;
            lblGenTimeKey.Text = "Test Time:";
            lblGenTimeKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGenTimeVal
            // 
            lblGenTimeVal.Dock = DockStyle.Fill;
            lblGenTimeVal.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblGenTimeVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblGenTimeVal.Location = new Point(756, 135);
            lblGenTimeVal.Margin = new Padding(6, 0, 6, 0);
            lblGenTimeVal.Name = "lblGenTimeVal";
            lblGenTimeVal.Size = new Size(238, 45);
            lblGenTimeVal.TabIndex = 15;
            lblGenTimeVal.Text = "00:09:01";
            lblGenTimeVal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGenSnKey
            // 
            lblGenSnKey.Dock = DockStyle.Fill;
            lblGenSnKey.Font = new Font("Segoe UI", 8F);
            lblGenSnKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblGenSnKey.Location = new Point(6, 180);
            lblGenSnKey.Margin = new Padding(6, 0, 6, 0);
            lblGenSnKey.Name = "lblGenSnKey";
            lblGenSnKey.Size = new Size(238, 49);
            lblGenSnKey.TabIndex = 16;
            lblGenSnKey.Text = "Serial Number:";
            lblGenSnKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGenSnVal
            // 
            lblGenSnVal.Dock = DockStyle.Fill;
            lblGenSnVal.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblGenSnVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblGenSnVal.Location = new Point(256, 180);
            lblGenSnVal.Margin = new Padding(6, 0, 6, 0);
            lblGenSnVal.Name = "lblGenSnVal";
            lblGenSnVal.Size = new Size(238, 49);
            lblGenSnVal.TabIndex = 17;
            lblGenSnVal.Text = "SN12350";
            lblGenSnVal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGenResKey
            // 
            lblGenResKey.Dock = DockStyle.Fill;
            lblGenResKey.Font = new Font("Segoe UI", 8F);
            lblGenResKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblGenResKey.Location = new Point(506, 180);
            lblGenResKey.Margin = new Padding(6, 0, 6, 0);
            lblGenResKey.Name = "lblGenResKey";
            lblGenResKey.Size = new Size(238, 49);
            lblGenResKey.TabIndex = 18;
            lblGenResKey.Text = "Result:";
            lblGenResKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGenResVal
            // 
            lblGenResVal.Dock = DockStyle.Fill;
            lblGenResVal.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblGenResVal.ForeColor = Color.FromArgb(4, 120, 87);
            lblGenResVal.Location = new Point(756, 180);
            lblGenResVal.Margin = new Padding(6, 0, 6, 0);
            lblGenResVal.Name = "lblGenResVal";
            lblGenResVal.Size = new Size(238, 49);
            lblGenResVal.TabIndex = 19;
            lblGenResVal.Text = "PASS";
            lblGenResVal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGenTitle
            // 
            lblGenTitle.Dock = DockStyle.Top;
            lblGenTitle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblGenTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblGenTitle.Location = new Point(32, 36);
            lblGenTitle.Margin = new Padding(6, 0, 6, 0);
            lblGenTitle.Name = "lblGenTitle";
            lblGenTitle.Size = new Size(1000, 64);
            lblGenTitle.TabIndex = 0;
            lblGenTitle.Text = "GENERAL INFORMATION";
            // 
            // pnlSummaryTableContainer
            // 
            pnlSummaryTableContainer.AutoScroll = true;
            pnlSummaryTableContainer.Controls.Add(tblTestSummary);
            pnlSummaryTableContainer.Controls.Add(lblSumTableTitle);
            pnlSummaryTableContainer.Dock = DockStyle.Fill;
            pnlSummaryTableContainer.Location = new Point(0, 504);
            pnlSummaryTableContainer.Margin = new Padding(0, 17, 0, 17);
            pnlSummaryTableContainer.Name = "pnlSummaryTableContainer";
            pnlSummaryTableContainer.Size = new Size(1064, 1000);
            pnlSummaryTableContainer.TabIndex = 2;
            // 
            // tblTestSummary
            // 
            tblTestSummary.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tblTestSummary.ColumnCount = 5;
            tblTestSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36F));
            tblTestSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F));
            tblTestSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F));
            tblTestSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F));
            tblTestSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F));
            tblTestSummary.Controls.Add(lblSumH1, 0, 0);
            tblTestSummary.Controls.Add(lblSumH2, 1, 0);
            tblTestSummary.Controls.Add(lblSumH3, 2, 0);
            tblTestSummary.Controls.Add(lblSumH4, 3, 0);
            tblTestSummary.Controls.Add(lblSumH5, 4, 0);
            tblTestSummary.Controls.Add(lblR1C1, 0, 1);
            tblTestSummary.Controls.Add(lblR1C2, 1, 1);
            tblTestSummary.Controls.Add(lblR1C3, 2, 1);
            tblTestSummary.Controls.Add(lblR1C4, 3, 1);
            tblTestSummary.Controls.Add(lblR1C5, 4, 1);
            tblTestSummary.Controls.Add(lblR2C1, 0, 2);
            tblTestSummary.Controls.Add(lblR2C2, 1, 2);
            tblTestSummary.Controls.Add(lblR2C3, 2, 2);
            tblTestSummary.Controls.Add(lblR2C4, 3, 2);
            tblTestSummary.Controls.Add(lblR2C5, 4, 2);
            tblTestSummary.Controls.Add(lblR3C1, 0, 3);
            tblTestSummary.Controls.Add(lblR3C2, 1, 3);
            tblTestSummary.Controls.Add(lblR3C3, 2, 3);
            tblTestSummary.Controls.Add(lblR3C4, 3, 3);
            tblTestSummary.Controls.Add(lblR3C5, 4, 3);
            tblTestSummary.Controls.Add(lblR4C1, 0, 4);
            tblTestSummary.Controls.Add(lblR4C2, 1, 4);
            tblTestSummary.Controls.Add(lblR4C3, 2, 4);
            tblTestSummary.Controls.Add(lblR4C4, 3, 4);
            tblTestSummary.Controls.Add(lblR4C5, 4, 4);
            tblTestSummary.Controls.Add(lblR5C1, 0, 5);
            tblTestSummary.Controls.Add(lblR5C2, 1, 5);
            tblTestSummary.Controls.Add(lblR5C3, 2, 5);
            tblTestSummary.Controls.Add(lblR5C4, 3, 5);
            tblTestSummary.Controls.Add(lblR5C5, 4, 5);
            tblTestSummary.Dock = DockStyle.Top;
            tblTestSummary.Location = new Point(0, 62);
            tblTestSummary.Margin = new Padding(6, 4, 6, 4);
            tblTestSummary.MinimumSize = new Size(600, 0);
            tblTestSummary.Name = "tblTestSummary";
            tblTestSummary.RowCount = 6;
            tblTestSummary.RowStyles.Add(new RowStyle(SizeType.Absolute, 51F));
            tblTestSummary.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tblTestSummary.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tblTestSummary.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tblTestSummary.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tblTestSummary.RowStyles.Add(new RowStyle(SizeType.Absolute, 51F));
            tblTestSummary.Size = new Size(1064, 290);
            tblTestSummary.TabIndex = 1;
            // 
            // lblSumH1
            // 
            lblSumH1.BackColor = Color.FromArgb(241, 245, 249);
            lblSumH1.Dock = DockStyle.Fill;
            lblSumH1.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblSumH1.Location = new Point(1, 1);
            lblSumH1.Margin = new Padding(0);
            lblSumH1.Name = "lblSumH1";
            lblSumH1.Size = new Size(380, 51);
            lblSumH1.TabIndex = 0;
            lblSumH1.Text = "Test Type";
            lblSumH1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblSumH2
            // 
            lblSumH2.BackColor = Color.FromArgb(241, 245, 249);
            lblSumH2.Dock = DockStyle.Fill;
            lblSumH2.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblSumH2.Location = new Point(382, 1);
            lblSumH2.Margin = new Padding(0);
            lblSumH2.Name = "lblSumH2";
            lblSumH2.Size = new Size(169, 51);
            lblSumH2.TabIndex = 1;
            lblSumH2.Text = "Total";
            lblSumH2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSumH3
            // 
            lblSumH3.BackColor = Color.FromArgb(241, 245, 249);
            lblSumH3.Dock = DockStyle.Fill;
            lblSumH3.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblSumH3.Location = new Point(552, 1);
            lblSumH3.Margin = new Padding(0);
            lblSumH3.Name = "lblSumH3";
            lblSumH3.Size = new Size(169, 51);
            lblSumH3.TabIndex = 2;
            lblSumH3.Text = "Pass";
            lblSumH3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSumH4
            // 
            lblSumH4.BackColor = Color.FromArgb(241, 245, 249);
            lblSumH4.Dock = DockStyle.Fill;
            lblSumH4.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblSumH4.Location = new Point(722, 1);
            lblSumH4.Margin = new Padding(0);
            lblSumH4.Name = "lblSumH4";
            lblSumH4.Size = new Size(169, 51);
            lblSumH4.TabIndex = 3;
            lblSumH4.Text = "Fail";
            lblSumH4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSumH5
            // 
            lblSumH5.BackColor = Color.FromArgb(241, 245, 249);
            lblSumH5.Dock = DockStyle.Fill;
            lblSumH5.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblSumH5.Location = new Point(892, 1);
            lblSumH5.Margin = new Padding(0);
            lblSumH5.Name = "lblSumH5";
            lblSumH5.Size = new Size(171, 51);
            lblSumH5.TabIndex = 4;
            lblSumH5.Text = "Pass Rate";
            lblSumH5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblR1C1
            // 
            lblR1C1.Dock = DockStyle.Fill;
            lblR1C1.Font = new Font("Segoe UI", 8F);
            lblR1C1.Location = new Point(1, 53);
            lblR1C1.Margin = new Padding(0);
            lblR1C1.Name = "lblR1C1";
            lblR1C1.Size = new Size(380, 45);
            lblR1C1.TabIndex = 5;
            lblR1C1.Text = "Continuity Test";
            lblR1C1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblR1C2
            // 
            lblR1C2.Dock = DockStyle.Fill;
            lblR1C2.Font = new Font("Segoe UI", 8F);
            lblR1C2.Location = new Point(382, 53);
            lblR1C2.Margin = new Padding(0);
            lblR1C2.Name = "lblR1C2";
            lblR1C2.Size = new Size(169, 45);
            lblR1C2.TabIndex = 6;
            lblR1C2.Text = "62";
            lblR1C2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblR1C3
            // 
            lblR1C3.Dock = DockStyle.Fill;
            lblR1C3.Font = new Font("Segoe UI", 8F);
            lblR1C3.Location = new Point(552, 53);
            lblR1C3.Margin = new Padding(0);
            lblR1C3.Name = "lblR1C3";
            lblR1C3.Size = new Size(169, 45);
            lblR1C3.TabIndex = 7;
            lblR1C3.Text = "62";
            lblR1C3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblR1C4
            // 
            lblR1C4.Dock = DockStyle.Fill;
            lblR1C4.Font = new Font("Segoe UI", 8F);
            lblR1C4.Location = new Point(722, 53);
            lblR1C4.Margin = new Padding(0);
            lblR1C4.Name = "lblR1C4";
            lblR1C4.Size = new Size(169, 45);
            lblR1C4.TabIndex = 8;
            lblR1C4.Text = "0";
            lblR1C4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblR1C5
            // 
            lblR1C5.Dock = DockStyle.Fill;
            lblR1C5.Font = new Font("Segoe UI", 8F);
            lblR1C5.Location = new Point(892, 53);
            lblR1C5.Margin = new Padding(0);
            lblR1C5.Name = "lblR1C5";
            lblR1C5.Size = new Size(171, 45);
            lblR1C5.TabIndex = 9;
            lblR1C5.Text = "100.00%";
            lblR1C5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblR2C1
            // 
            lblR2C1.Dock = DockStyle.Fill;
            lblR2C1.Font = new Font("Segoe UI", 8F);
            lblR2C1.Location = new Point(1, 99);
            lblR2C1.Margin = new Padding(0);
            lblR2C1.Name = "lblR2C1";
            lblR2C1.Size = new Size(380, 45);
            lblR2C1.TabIndex = 10;
            lblR2C1.Text = "Short Circuit Test";
            lblR2C1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblR2C2
            // 
            lblR2C2.Dock = DockStyle.Fill;
            lblR2C2.Font = new Font("Segoe UI", 8F);
            lblR2C2.Location = new Point(382, 99);
            lblR2C2.Margin = new Padding(0);
            lblR2C2.Name = "lblR2C2";
            lblR2C2.Size = new Size(169, 45);
            lblR2C2.TabIndex = 11;
            lblR2C2.Text = "62";
            lblR2C2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblR2C3
            // 
            lblR2C3.Dock = DockStyle.Fill;
            lblR2C3.Font = new Font("Segoe UI", 8F);
            lblR2C3.Location = new Point(552, 99);
            lblR2C3.Margin = new Padding(0);
            lblR2C3.Name = "lblR2C3";
            lblR2C3.Size = new Size(169, 45);
            lblR2C3.TabIndex = 12;
            lblR2C3.Text = "62";
            lblR2C3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblR2C4
            // 
            lblR2C4.Dock = DockStyle.Fill;
            lblR2C4.Font = new Font("Segoe UI", 8F);
            lblR2C4.Location = new Point(722, 99);
            lblR2C4.Margin = new Padding(0);
            lblR2C4.Name = "lblR2C4";
            lblR2C4.Size = new Size(169, 45);
            lblR2C4.TabIndex = 13;
            lblR2C4.Text = "0";
            lblR2C4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblR2C5
            // 
            lblR2C5.Dock = DockStyle.Fill;
            lblR2C5.Font = new Font("Segoe UI", 8F);
            lblR2C5.Location = new Point(892, 99);
            lblR2C5.Margin = new Padding(0);
            lblR2C5.Name = "lblR2C5";
            lblR2C5.Size = new Size(171, 45);
            lblR2C5.TabIndex = 14;
            lblR2C5.Text = "100.00%";
            lblR2C5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblR3C1
            // 
            lblR3C1.Dock = DockStyle.Fill;
            lblR3C1.Font = new Font("Segoe UI", 8F);
            lblR3C1.Location = new Point(1, 145);
            lblR3C1.Margin = new Padding(0);
            lblR3C1.Name = "lblR3C1";
            lblR3C1.Size = new Size(380, 45);
            lblR3C1.TabIndex = 15;
            lblR3C1.Text = "Insulation Test";
            lblR3C1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblR3C2
            // 
            lblR3C2.Dock = DockStyle.Fill;
            lblR3C2.Font = new Font("Segoe UI", 8F);
            lblR3C2.Location = new Point(382, 145);
            lblR3C2.Margin = new Padding(0);
            lblR3C2.Name = "lblR3C2";
            lblR3C2.Size = new Size(169, 45);
            lblR3C2.TabIndex = 16;
            lblR3C2.Text = "62";
            lblR3C2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblR3C3
            // 
            lblR3C3.Dock = DockStyle.Fill;
            lblR3C3.Font = new Font("Segoe UI", 8F);
            lblR3C3.Location = new Point(552, 145);
            lblR3C3.Margin = new Padding(0);
            lblR3C3.Name = "lblR3C3";
            lblR3C3.Size = new Size(169, 45);
            lblR3C3.TabIndex = 17;
            lblR3C3.Text = "61";
            lblR3C3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblR3C4
            // 
            lblR3C4.Dock = DockStyle.Fill;
            lblR3C4.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblR3C4.ForeColor = Color.FromArgb(220, 38, 38);
            lblR3C4.Location = new Point(722, 145);
            lblR3C4.Margin = new Padding(0);
            lblR3C4.Name = "lblR3C4";
            lblR3C4.Size = new Size(169, 45);
            lblR3C4.TabIndex = 18;
            lblR3C4.Text = "1";
            lblR3C4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblR3C5
            // 
            lblR3C5.Dock = DockStyle.Fill;
            lblR3C5.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblR3C5.ForeColor = Color.FromArgb(220, 38, 38);
            lblR3C5.Location = new Point(892, 145);
            lblR3C5.Margin = new Padding(0);
            lblR3C5.Name = "lblR3C5";
            lblR3C5.Size = new Size(171, 45);
            lblR3C5.TabIndex = 19;
            lblR3C5.Text = "98.39%";
            lblR3C5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblR4C1
            // 
            lblR4C1.Dock = DockStyle.Fill;
            lblR4C1.Font = new Font("Segoe UI", 8F);
            lblR4C1.Location = new Point(1, 191);
            lblR4C1.Margin = new Padding(0);
            lblR4C1.Name = "lblR4C1";
            lblR4C1.Size = new Size(380, 45);
            lblR4C1.TabIndex = 20;
            lblR4C1.Text = "Functional Test";
            lblR4C1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblR4C2
            // 
            lblR4C2.Dock = DockStyle.Fill;
            lblR4C2.Font = new Font("Segoe UI", 8F);
            lblR4C2.Location = new Point(382, 191);
            lblR4C2.Margin = new Padding(0);
            lblR4C2.Name = "lblR4C2";
            lblR4C2.Size = new Size(169, 45);
            lblR4C2.TabIndex = 21;
            lblR4C2.Text = "62";
            lblR4C2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblR4C3
            // 
            lblR4C3.Dock = DockStyle.Fill;
            lblR4C3.Font = new Font("Segoe UI", 8F);
            lblR4C3.Location = new Point(552, 191);
            lblR4C3.Margin = new Padding(0);
            lblR4C3.Name = "lblR4C3";
            lblR4C3.Size = new Size(169, 45);
            lblR4C3.TabIndex = 22;
            lblR4C3.Text = "62";
            lblR4C3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblR4C4
            // 
            lblR4C4.Dock = DockStyle.Fill;
            lblR4C4.Font = new Font("Segoe UI", 8F);
            lblR4C4.Location = new Point(722, 191);
            lblR4C4.Margin = new Padding(0);
            lblR4C4.Name = "lblR4C4";
            lblR4C4.Size = new Size(169, 45);
            lblR4C4.TabIndex = 23;
            lblR4C4.Text = "0";
            lblR4C4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblR4C5
            // 
            lblR4C5.Dock = DockStyle.Fill;
            lblR4C5.Font = new Font("Segoe UI", 8F);
            lblR4C5.Location = new Point(892, 191);
            lblR4C5.Margin = new Padding(0);
            lblR4C5.Name = "lblR4C5";
            lblR4C5.Size = new Size(171, 45);
            lblR4C5.TabIndex = 24;
            lblR4C5.Text = "100.00%";
            lblR4C5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblR5C1
            // 
            lblR5C1.BackColor = Color.FromArgb(248, 250, 252);
            lblR5C1.Dock = DockStyle.Fill;
            lblR5C1.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblR5C1.Location = new Point(1, 237);
            lblR5C1.Margin = new Padding(0);
            lblR5C1.Name = "lblR5C1";
            lblR5C1.Size = new Size(380, 52);
            lblR5C1.TabIndex = 25;
            lblR5C1.Text = "Total";
            lblR5C1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblR5C2
            // 
            lblR5C2.BackColor = Color.FromArgb(248, 250, 252);
            lblR5C2.Dock = DockStyle.Fill;
            lblR5C2.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblR5C2.Location = new Point(382, 237);
            lblR5C2.Margin = new Padding(0);
            lblR5C2.Name = "lblR5C2";
            lblR5C2.Size = new Size(169, 52);
            lblR5C2.TabIndex = 26;
            lblR5C2.Text = "310";
            lblR5C2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblR5C3
            // 
            lblR5C3.BackColor = Color.FromArgb(248, 250, 252);
            lblR5C3.Dock = DockStyle.Fill;
            lblR5C3.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblR5C3.Location = new Point(552, 237);
            lblR5C3.Margin = new Padding(0);
            lblR5C3.Name = "lblR5C3";
            lblR5C3.Size = new Size(169, 52);
            lblR5C3.TabIndex = 27;
            lblR5C3.Text = "309";
            lblR5C3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblR5C4
            // 
            lblR5C4.BackColor = Color.FromArgb(248, 250, 252);
            lblR5C4.Dock = DockStyle.Fill;
            lblR5C4.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblR5C4.ForeColor = Color.FromArgb(220, 38, 38);
            lblR5C4.Location = new Point(722, 237);
            lblR5C4.Margin = new Padding(0);
            lblR5C4.Name = "lblR5C4";
            lblR5C4.Size = new Size(169, 52);
            lblR5C4.TabIndex = 28;
            lblR5C4.Text = "1";
            lblR5C4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblR5C5
            // 
            lblR5C5.BackColor = Color.FromArgb(248, 250, 252);
            lblR5C5.Dock = DockStyle.Fill;
            lblR5C5.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblR5C5.Location = new Point(892, 237);
            lblR5C5.Margin = new Padding(0);
            lblR5C5.Name = "lblR5C5";
            lblR5C5.Size = new Size(171, 52);
            lblR5C5.TabIndex = 29;
            lblR5C5.Text = "99.68%";
            lblR5C5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSumTableTitle
            // 
            lblSumTableTitle.Dock = DockStyle.Top;
            lblSumTableTitle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblSumTableTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblSumTableTitle.Location = new Point(0, 0);
            lblSumTableTitle.Margin = new Padding(6, 0, 6, 0);
            lblSumTableTitle.Name = "lblSumTableTitle";
            lblSumTableTitle.Size = new Size(1064, 62);
            lblSumTableTitle.TabIndex = 0;
            lblSumTableTitle.Text = "TEST RESULT SUMMARY";
            // 
            // pnlResultBox
            // 
            pnlResultBox.ColumnCount = 2;
            pnlResultBox.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 267F));
            pnlResultBox.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            pnlResultBox.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            pnlResultBox.Controls.Add(lblResultTitle, 0, 0);
            pnlResultBox.Controls.Add(lblPassBox, 1, 0);
            pnlResultBox.Dock = DockStyle.Fill;
            pnlResultBox.Location = new Point(6, 1536);
            pnlResultBox.Margin = new Padding(6, 15, 6, 15);
            pnlResultBox.Name = "pnlResultBox";
            pnlResultBox.RowCount = 1;
            pnlResultBox.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            pnlResultBox.Size = new Size(1052, 70);
            pnlResultBox.TabIndex = 3;
            // 
            // lblResultTitle
            // 
            lblResultTitle.Dock = DockStyle.Fill;
            lblResultTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblResultTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblResultTitle.Location = new Point(0, 0);
            lblResultTitle.Margin = new Padding(0);
            lblResultTitle.Name = "lblResultTitle";
            lblResultTitle.Size = new Size(267, 70);
            lblResultTitle.TabIndex = 0;
            lblResultTitle.Text = "RESULT :";
            lblResultTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblPassBox
            // 
            lblPassBox.BackColor = Color.FromArgb(236, 253, 245);
            lblPassBox.Dock = DockStyle.Fill;
            lblPassBox.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblPassBox.ForeColor = Color.FromArgb(4, 120, 87);
            lblPassBox.Location = new Point(282, 6);
            lblPassBox.Margin = new Padding(15, 6, 15, 0);
            lblPassBox.Name = "lblPassBox";
            lblPassBox.Size = new Size(755, 64);
            lblPassBox.TabIndex = 1;
            lblPassBox.Text = "PASS";
            lblPassBox.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlNotesBox
            // 
            pnlNotesBox.ColumnCount = 1;
            pnlNotesBox.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            pnlNotesBox.Controls.Add(lblNotesTitle, 0, 0);
            pnlNotesBox.Controls.Add(pnlNotesCard, 0, 1);
            pnlNotesBox.Dock = DockStyle.Fill;
            pnlNotesBox.Location = new Point(6, 1621);
            pnlNotesBox.Margin = new Padding(6, 0, 6, 0);
            pnlNotesBox.Name = "pnlNotesBox";
            pnlNotesBox.RowCount = 2;
            pnlNotesBox.RowStyles.Add(new RowStyle(SizeType.Absolute, 41F));
            pnlNotesBox.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            pnlNotesBox.Size = new Size(1052, 230);
            pnlNotesBox.TabIndex = 4;
            // 
            // lblNotesTitle
            // 
            lblNotesTitle.Dock = DockStyle.Fill;
            lblNotesTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblNotesTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblNotesTitle.Location = new Point(0, 0);
            lblNotesTitle.Margin = new Padding(0);
            lblNotesTitle.Name = "lblNotesTitle";
            lblNotesTitle.Size = new Size(1052, 41);
            lblNotesTitle.TabIndex = 0;
            lblNotesTitle.Text = "NOTES";
            lblNotesTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlNotesCard
            // 
            pnlNotesCard.BackColor = Color.White;
            pnlNotesCard.Controls.Add(lblNotesContent);
            pnlNotesCard.Dock = DockStyle.Fill;
            pnlNotesCard.Location = new Point(0, 60);
            pnlNotesCard.Margin = new Padding(0, 19, 0, 0);
            pnlNotesCard.Name = "pnlNotesCard";
            pnlNotesCard.Size = new Size(1052, 170);
            pnlNotesCard.TabIndex = 1;
            // 
            // lblNotesContent
            // 
            lblNotesContent.Dock = DockStyle.Fill;
            lblNotesContent.Font = new Font("Segoe UI", 9.5F);
            lblNotesContent.ForeColor = Color.FromArgb(71, 85, 105);
            lblNotesContent.Location = new Point(0, 0);
            lblNotesContent.Margin = new Padding(0);
            lblNotesContent.Name = "lblNotesContent";
            lblNotesContent.Size = new Size(1052, 170);
            lblNotesContent.TabIndex = 0;
            lblNotesContent.Text = "- All test parameters are within specification.";
            // 
            // lblReportType
            // 
            lblReportType.AutoSize = true;
            lblReportType.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblReportType.ForeColor = Color.FromArgb(71, 85, 105);
            lblReportType.Location = new Point(0, 0);
            lblReportType.Margin = new Padding(0, 0, 0, 6);
            lblReportType.Name = "lblReportType";
            lblReportType.Size = new Size(159, 31);
            lblReportType.TabIndex = 0;
            lblReportType.Text = "REPORT TYPE";
            // 
            // cmbReportType
            // 
            cmbReportType.Dock = DockStyle.Fill;
            cmbReportType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbReportType.Font = new Font("Segoe UI", 9.5F);
            cmbReportType.FormattingEnabled = true;
            cmbReportType.Items.AddRange(new object[] { "Test Report", "Summary Report" });
            cmbReportType.Location = new Point(0, 37);
            cmbReportType.Margin = new Padding(0, 0, 0, 13);
            cmbReportType.Name = "cmbReportType";
            cmbReportType.Size = new Size(934, 44);
            cmbReportType.TabIndex = 1;
            // 
            // pnlSumProg
            // 
            pnlSumProg.BackColor = Color.FromArgb(255, 251, 235);
            pnlSumProg.Controls.Add(lblSumProgKey);
            pnlSumProg.Controls.Add(lblSumProgVal);
            pnlSumProg.Dock = DockStyle.Fill;
            pnlSumProg.Location = new Point(472, 171);
            pnlSumProg.Margin = new Padding(11, 13, 0, 0);
            pnlSumProg.Name = "pnlSumProg";
            pnlSumProg.Padding = new Padding(17, 23, 17, 23);
            pnlSumProg.Size = new Size(450, 145);
            pnlSumProg.TabIndex = 3;
            // 
            // lblSumProgKey
            // 
            lblSumProgKey.Dock = DockStyle.Top;
            lblSumProgKey.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblSumProgKey.ForeColor = Color.FromArgb(146, 64, 14);
            lblSumProgKey.Location = new Point(17, 23);
            lblSumProgKey.Margin = new Padding(6, 0, 6, 0);
            lblSumProgKey.Name = "lblSumProgKey";
            lblSumProgKey.Size = new Size(416, 26);
            lblSumProgKey.TabIndex = 0;
            lblSumProgKey.Text = "IN PROGRESS";
            lblSumProgKey.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSumProgVal
            // 
            lblSumProgVal.Dock = DockStyle.Fill;
            lblSumProgVal.Font = new Font("Segoe UI", 13.5F, FontStyle.Bold);
            lblSumProgVal.ForeColor = Color.FromArgb(217, 119, 6);
            lblSumProgVal.Location = new Point(17, 23);
            lblSumProgVal.Margin = new Padding(11, 0, 11, 0);
            lblSumProgVal.Name = "lblSumProgVal";
            lblSumProgVal.Size = new Size(416, 99);
            lblSumProgVal.TabIndex = 1;
            lblSumProgVal.Text = "0";
            lblSumProgVal.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Reports
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoScrollMinSize = new Size(1300, 800);
            BackColor = Color.FromArgb(244, 245, 247);
            Controls.Add(tblMain);
            Font = new Font("Segoe UI", 9F);
            Margin = new Padding(6, 4, 6, 4);
            Name = "Reports";
            Size = new Size(4000, 2451);
            tblMain.ResumeLayout(false);
            pnlTopHeader.ResumeLayout(false);
            tblLeftCenterRight.ResumeLayout(false);
            splitMainTopBottom.Panel1.ResumeLayout(false);
            splitMainTopBottom.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMainTopBottom).EndInit();
            splitMainTopBottom.ResumeLayout(false);
            splitLeftRight.Panel1.ResumeLayout(false);
            splitLeftRight.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitLeftRight).EndInit();
            splitLeftRight.ResumeLayout(false);
            tblColLeft.ResumeLayout(false);
            cardFilters.ResumeLayout(false);
            tblFilterControls.ResumeLayout(false);
            tblFilterControls.PerformLayout();
            tblDateRange.ResumeLayout(false);
            tblDateRange.PerformLayout();
            tblFilterGrid1.ResumeLayout(false);
            tblFilterGrid1.PerformLayout();
            tblFilterGrid2.ResumeLayout(false);
            tblFilterGrid2.PerformLayout();
            tblFilterGrid3.ResumeLayout(false);
            tblFilterGrid3.PerformLayout();
            tblFilterButtons.ResumeLayout(false);
            tblSummaryCards.ResumeLayout(false);
            pnlSumTotal.ResumeLayout(false);
            pnlSumPass.ResumeLayout(false);
            pnlSumFail.ResumeLayout(false);
            cardReportList.ResumeLayout(false);
            tblCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridReports).EndInit();
            tblPagination.ResumeLayout(false);
            flpPageButtons.ResumeLayout(false);
            cardTrend.ResumeLayout(false);
            tblColRight.ResumeLayout(false);
            tblColRight.PerformLayout();
            cardPreviewTop.ResumeLayout(false);
            cardPreviewTop.PerformLayout();
            flpPreviewButtons.ResumeLayout(false);
            pnlPreviewContainer.ResumeLayout(false);
            cardPaper.ResumeLayout(false);
            tblPaperContent.ResumeLayout(false);
            tblPaperHeader.ResumeLayout(false);
            flpPaperLogo.ResumeLayout(false);
            flpPaperLogo.PerformLayout();
            pnlGenInfoBox.ResumeLayout(false);
            tblGenGrid.ResumeLayout(false);
            pnlSummaryTableContainer.ResumeLayout(false);
            tblTestSummary.ResumeLayout(false);
            pnlResultBox.ResumeLayout(false);
            pnlNotesBox.ResumeLayout(false);
            pnlNotesCard.ResumeLayout(false);
            pnlSumProg.ResumeLayout(false);
            ResumeLayout(false);



        }



        #endregion




        private System.Windows.Forms.TableLayoutPanel tblMain;

        private System.Windows.Forms.Panel pnlTopHeader;

        private System.Windows.Forms.Label lblPageHeader;

        private System.Windows.Forms.SplitContainer splitMainTopBottom;

        private System.Windows.Forms.TableLayoutPanel tblLeftCenterRight;

        private System.Windows.Forms.Panel cardPreviewTop;

        private System.Windows.Forms.SplitContainer splitLeftRight;

        private System.Windows.Forms.TableLayoutPanel tblColLeft;

        private System.Windows.Forms.Panel cardFilters;

        private System.Windows.Forms.TableLayoutPanel tblFilterControls;

        private System.Windows.Forms.Label lblReportType;

        private System.Windows.Forms.ComboBox cmbReportType;

        private System.Windows.Forms.Label lblDateRange;

        private System.Windows.Forms.TableLayoutPanel tblDateRange;

        private System.Windows.Forms.DateTimePicker dtpStart;

        private System.Windows.Forms.Label lblDash;

        private System.Windows.Forms.DateTimePicker dtpEnd;

        private System.Windows.Forms.Label lblFilters;

        private System.Windows.Forms.TableLayoutPanel tblFilterGrid1;

        private System.Windows.Forms.Label lblFilterModel;

        private System.Windows.Forms.Label lblFilterType;

        private System.Windows.Forms.ComboBox cmbModel;

        private System.Windows.Forms.ComboBox cmbType;

        private System.Windows.Forms.TableLayoutPanel tblFilterGrid2;

        private System.Windows.Forms.Label lblFilterArea;

        private System.Windows.Forms.Label lblFilterOperator;

        private System.Windows.Forms.ComboBox cmbArea;

        private System.Windows.Forms.ComboBox cmbOperator;

        private System.Windows.Forms.TableLayoutPanel tblFilterGrid3;

        private System.Windows.Forms.Label lblFilterResult;

        private System.Windows.Forms.Label lblFilterPart;

        private System.Windows.Forms.ComboBox cmbResult;

        private System.Windows.Forms.TextBox txtPartNumber;

        private System.Windows.Forms.TableLayoutPanel tblFilterButtons;

        private System.Windows.Forms.Button btnGenerate;

        private System.Windows.Forms.Button btnReset;

        private System.Windows.Forms.Panel pnlSeparator;

        private System.Windows.Forms.Label lblSummaryTitle;

        private System.Windows.Forms.TableLayoutPanel tblSummaryCards;

        private System.Windows.Forms.Panel pnlSumTotal;

        private System.Windows.Forms.Label lblSumTotalVal;

        private System.Windows.Forms.Label lblSumTotalKey;

        private System.Windows.Forms.Panel pnlSumPass;

        private System.Windows.Forms.Label lblSumPassVal;

        private System.Windows.Forms.Label lblSumPassKey;

        private System.Windows.Forms.Panel pnlSumFail;

        private System.Windows.Forms.Label lblSumFailVal;

        private System.Windows.Forms.Label lblSumFailKey;

        private System.Windows.Forms.Panel pnlSumProg;

        private System.Windows.Forms.Label lblSumProgVal;

        private System.Windows.Forms.Label lblSumProgKey;

        private System.Windows.Forms.Panel cardTrend;

        private System.Windows.Forms.Label lblTrendTitle;

        private System.Windows.Forms.Panel cardReportList;

        private System.Windows.Forms.TableLayoutPanel tblCenter;

        private System.Windows.Forms.Label lblReportListTitle;

        private System.Windows.Forms.DataGridView gridReports;

        private System.Windows.Forms.DataGridViewTextBoxColumn colNo;

        private System.Windows.Forms.DataGridViewTextBoxColumn colReportName;

        private System.Windows.Forms.DataGridViewTextBoxColumn colReportType;

        private System.Windows.Forms.DataGridViewTextBoxColumn colDateGenerated;

        private System.Windows.Forms.DataGridViewTextBoxColumn colGeneratedBy;

        private System.Windows.Forms.DataGridViewTextBoxColumn colSize;

        private System.Windows.Forms.DataGridViewTextBoxColumn colAction;

        private System.Windows.Forms.TableLayoutPanel tblPagination;

        private System.Windows.Forms.Label lblPageInfo;

        private System.Windows.Forms.FlowLayoutPanel flpPageButtons;

        private System.Windows.Forms.Button btnPageLast;

        private System.Windows.Forms.Button btnPageNext;

        private System.Windows.Forms.Button btnPageDots;

        private System.Windows.Forms.Button btnPage2;

        private System.Windows.Forms.Button btnPage1;

        private System.Windows.Forms.Button btnPagePrev;

        private System.Windows.Forms.Button btnPageFirst;

        private System.Windows.Forms.TableLayoutPanel tblColRight;

        private System.Windows.Forms.Label lblPreviewTitle;

        private System.Windows.Forms.TableLayoutPanel flpPreviewButtons;

        private System.Windows.Forms.Button btnPrint;

        private System.Windows.Forms.Button btnExportExcel;

        private System.Windows.Forms.Button btnExportPDF;

        private System.Windows.Forms.Panel pnlPreviewContainer;

        private System.Windows.Forms.Button btnEditReport;

        private System.Windows.Forms.Button btnDeleteReport;

        private System.Windows.Forms.Button btnSaveReportEdit;

        private System.Windows.Forms.Button btnCancelReportEdit;

        private System.Windows.Forms.Panel cardPaper;

        private System.Windows.Forms.TableLayoutPanel tblPaperContent;

        private System.Windows.Forms.TableLayoutPanel tblPaperHeader;

        private System.Windows.Forms.FlowLayoutPanel flpPaperLogo;

        private System.Windows.Forms.Label lblLogoBadge;

        private System.Windows.Forms.Label lblPaperTitle;

        private System.Windows.Forms.Label lblPaperMeta;

        private System.Windows.Forms.Panel pnlGenInfoBox;

        private System.Windows.Forms.Label lblGenTitle;

        private System.Windows.Forms.TableLayoutPanel tblGenGrid;

        private System.Windows.Forms.Label lblGenModelKey;

        private System.Windows.Forms.Label lblGenModelVal;

        private System.Windows.Forms.Label lblGenEcnKey;

        private System.Windows.Forms.Label lblGenEcnVal;

        private System.Windows.Forms.Label lblGenTypeKey;

        private System.Windows.Forms.Label lblGenTypeVal;

        private System.Windows.Forms.Label lblGenRevKey;

        private System.Windows.Forms.Label lblGenRevVal;

        private System.Windows.Forms.Label lblGenAreaKey;

        private System.Windows.Forms.Label lblGenAreaVal;

        private System.Windows.Forms.Label lblGenOpKey;

        private System.Windows.Forms.Label lblGenOpVal;

        private System.Windows.Forms.Label lblGenPartKey;

        private System.Windows.Forms.Label lblGenPartVal;

        private System.Windows.Forms.Label lblGenTimeKey;

        private System.Windows.Forms.Label lblGenTimeVal;

        private System.Windows.Forms.Label lblGenSnKey;

        private System.Windows.Forms.Label lblGenSnVal;

        private System.Windows.Forms.Label lblGenResKey;

        private System.Windows.Forms.Label lblGenResVal;

        private System.Windows.Forms.Panel pnlSummaryTableContainer;

        private System.Windows.Forms.Label lblSumTableTitle;

        private System.Windows.Forms.TableLayoutPanel tblTestSummary;

        private System.Windows.Forms.Label lblSumH1;

        private System.Windows.Forms.Label lblSumH2;

        private System.Windows.Forms.Label lblSumH3;

        private System.Windows.Forms.Label lblSumH4;

        private System.Windows.Forms.Label lblSumH5;

        private System.Windows.Forms.Label lblR1C1;

        private System.Windows.Forms.Label lblR1C2;

        private System.Windows.Forms.Label lblR1C3;

        private System.Windows.Forms.Label lblR1C4;

        private System.Windows.Forms.Label lblR1C5;

        private System.Windows.Forms.Label lblR2C1;

        private System.Windows.Forms.Label lblR2C2;

        private System.Windows.Forms.Label lblR2C3;

        private System.Windows.Forms.Label lblR2C4;

        private System.Windows.Forms.Label lblR2C5;

        private System.Windows.Forms.Label lblR3C1;

        private System.Windows.Forms.Label lblR3C2;

        private System.Windows.Forms.Label lblR3C3;

        private System.Windows.Forms.Label lblR3C4;

        private System.Windows.Forms.Label lblR3C5;

        private System.Windows.Forms.Label lblR4C1;

        private System.Windows.Forms.Label lblR4C2;

        private System.Windows.Forms.Label lblR4C3;

        private System.Windows.Forms.Label lblR4C4;

        private System.Windows.Forms.Label lblR4C5;

        private System.Windows.Forms.Label lblR5C1;

        private System.Windows.Forms.Label lblR5C2;

        private System.Windows.Forms.Label lblR5C3;

        private System.Windows.Forms.Label lblR5C4;

        private System.Windows.Forms.Label lblR5C5;

        private System.Windows.Forms.TableLayoutPanel pnlResultBox;

        private System.Windows.Forms.Label lblResultTitle;

        private System.Windows.Forms.Label lblPassBox;

        private System.Windows.Forms.TableLayoutPanel pnlNotesBox;

        private System.Windows.Forms.Label lblNotesTitle;

        private System.Windows.Forms.Panel pnlNotesCard;

        private System.Windows.Forms.Label lblNotesContent;

    }

}
