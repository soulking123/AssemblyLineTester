namespace AssemblyLineTester.Views.Pages
{
    partial class HarnessDatabase
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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            splitContainerMain = new SplitContainer();
            tableLayoutPanelLeft = new TableLayoutPanel();
            lblListTitle = new Label();
            dataGridViewList = new DataGridView();
            colNo = new DataGridViewTextBoxColumn();
            colProductModel = new DataGridViewTextBoxColumn();
            colProductType = new DataGridViewTextBoxColumn();
            colArea = new DataGridViewTextBoxColumn();
            colPartNumber = new DataGridViewTextBoxColumn();
            colLastUpdated = new DataGridViewTextBoxColumn();
            tblPagination = new TableLayoutPanel();
            flpPages = new FlowLayoutPanel();
            flpShowEntries = new FlowLayoutPanel();
            lblShowEntries2 = new Label();
            cmbShowEntries = new ComboBox();
            lblShowEntries1 = new Label();
            tableLayoutPanelRight = new TableLayoutPanel();
            panelDetailCard = new TableLayoutPanel();
            panelDetailTop = new Panel();
            lblDetailTitle = new Label();
            flpNavigation = new FlowLayoutPanel();
            lblNavGeneral = new Label();
            lblNavConnector = new Label();
            lblNavWiring = new Label();
            panelTabContent = new Panel();
            tableLayoutPanelBottomRight = new TableLayoutPanel();
            panelCardSpecs = new Panel();
            tblSpecsContent = new TableLayoutPanel();
            lblSpecsTitle = new Label();
            panelCardTestSummary = new Panel();
            tblTestSummaryContent = new TableLayoutPanel();
            lblTestSummaryTitle = new Label();
            tabTestParams = new TabPage();
            tabHistory = new TabPage();
            lblTitle = new Label();
            tableLayoutPanelMain = new TableLayoutPanel();
            panelFilters = new Panel();
            lblProductModel = new Label();
            cmbProductModel = new ComboBox();
            lblProductType = new Label();
            cmbProductType = new ComboBox();
            lblArea = new Label();
            cmbArea = new ComboBox();
            lblPartNumber = new Label();
            txtPartNumber = new TextBox();
            lblStatus = new Label();
            cmbStatus = new ComboBox();
            btnSearch = new Button();
            btnReset = new Button();
            btnAddHarness = new Button();
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
            splitContainerMain.Panel1.SuspendLayout();
            splitContainerMain.Panel2.SuspendLayout();
            splitContainerMain.SuspendLayout();
            tableLayoutPanelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewList).BeginInit();
            tblPagination.SuspendLayout();
            flpShowEntries.SuspendLayout();
            tableLayoutPanelRight.SuspendLayout();
            panelDetailCard.SuspendLayout();
            panelDetailTop.SuspendLayout();
            flpNavigation.SuspendLayout();
            tableLayoutPanelBottomRight.SuspendLayout();
            panelCardSpecs.SuspendLayout();
            panelCardTestSummary.SuspendLayout();
            tableLayoutPanelMain.SuspendLayout();
            panelFilters.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainerMain
            // 
            splitContainerMain.Dock = DockStyle.Fill;
            splitContainerMain.IsSplitterFixed = true;
            splitContainerMain.Location = new Point(36, 269);
            splitContainerMain.Margin = new Padding(10);
            splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            splitContainerMain.Panel1.Controls.Add(tableLayoutPanelLeft);
            // 
            // splitContainerMain.Panel2
            // 
            splitContainerMain.Panel2.Controls.Add(tableLayoutPanelRight);
            splitContainerMain.Size = new Size(5245, 2815);
            splitContainerMain.SplitterDistance = 1923;
            splitContainerMain.SplitterWidth = 11;
            splitContainerMain.TabIndex = 3;
            // 
            // tableLayoutPanelLeft
            // 
            tableLayoutPanelLeft.BackColor = Color.White;
            tableLayoutPanelLeft.ColumnCount = 1;
            tableLayoutPanelLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelLeft.Controls.Add(lblListTitle, 0, 0);
            tableLayoutPanelLeft.Controls.Add(dataGridViewList, 0, 1);
            tableLayoutPanelLeft.Controls.Add(tblPagination, 0, 2);
            tableLayoutPanelLeft.Dock = DockStyle.Fill;
            tableLayoutPanelLeft.Location = new Point(0, 0);
            tableLayoutPanelLeft.Margin = new Padding(0);
            tableLayoutPanelLeft.Name = "tableLayoutPanelLeft";
            tableLayoutPanelLeft.Padding = new Padding(4);
            tableLayoutPanelLeft.RowCount = 3;
            tableLayoutPanelLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            tableLayoutPanelLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 114F));
            tableLayoutPanelLeft.Size = new Size(1923, 2815);
            tableLayoutPanelLeft.TabIndex = 0;
            // 
            // lblListTitle
            // 
            lblListTitle.AutoSize = true;
            lblListTitle.Dock = DockStyle.Fill;
            lblListTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblListTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblListTitle.Location = new Point(4, 4);
            lblListTitle.Margin = new Padding(0);
            lblListTitle.Name = "lblListTitle";
            lblListTitle.Padding = new Padding(26, 0, 0, 0);
            lblListTitle.Size = new Size(1915, 80);
            lblListTitle.TabIndex = 0;
            lblListTitle.Text = "HARNESSES LIST (Total: 58)";
            lblListTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dataGridViewList
            // 
            dataGridViewList.AllowUserToAddRows = false;
            dataGridViewList.AllowUserToDeleteRows = false;
            dataGridViewList.BackgroundColor = Color.White;
            dataGridViewList.BorderStyle = BorderStyle.None;
            dataGridViewList.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(248, 250, 252);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(71, 85, 105);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(248, 250, 252);
            dataGridViewCellStyle1.SelectionForeColor = Color.FromArgb(71, 85, 105);
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridViewList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewList.Columns.AddRange(new DataGridViewColumn[] { colNo, colProductModel, colProductType, colArea, colPartNumber, colLastUpdated });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(239, 246, 255);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridViewList.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewList.Dock = DockStyle.Fill;
            dataGridViewList.EnableHeadersVisualStyles = false;
            dataGridViewList.GridColor = Color.FromArgb(203, 213, 225);
            dataGridViewList.Location = new Point(30, 84);
            dataGridViewList.Margin = new Padding(26, 0, 26, 0);
            dataGridViewList.Name = "dataGridViewList";
            dataGridViewList.ReadOnly = true;
            dataGridViewList.RowHeadersVisible = false;
            dataGridViewList.RowHeadersWidth = 82;
            dataGridViewList.RowTemplate.Height = 36;
            dataGridViewList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewList.Size = new Size(1863, 2613);
            dataGridViewList.TabIndex = 1;
            // 
            // colNo
            // 
            colNo.FillWeight = 30F;
            colNo.HeaderText = "No.";
            colNo.MinimumWidth = 35;
            colNo.Name = "colNo";
            colNo.ReadOnly = true;
            colNo.Width = 200;
            // 
            // colProductModel
            // 
            colProductModel.FillWeight = 60F;
            colProductModel.HeaderText = "Product\r\nModel";
            colProductModel.MinimumWidth = 65;
            colProductModel.Name = "colProductModel";
            colProductModel.ReadOnly = true;
            colProductModel.Width = 200;
            // 
            // colProductType
            // 
            colProductType.FillWeight = 55F;
            colProductType.HeaderText = "Product\r\nType";
            colProductType.MinimumWidth = 60;
            colProductType.Name = "colProductType";
            colProductType.ReadOnly = true;
            colProductType.Width = 200;
            // 
            // colArea
            // 
            colArea.FillWeight = 220F;
            colArea.HeaderText = "Area";
            colArea.MinimumWidth = 160;
            colArea.Name = "colArea";
            colArea.ReadOnly = true;
            colArea.Width = 200;
            // 
            // colPartNumber
            // 
            colPartNumber.FillWeight = 160F;
            colPartNumber.HeaderText = "Part\r\nNumber";
            colPartNumber.MinimumWidth = 130;
            colPartNumber.Name = "colPartNumber";
            colPartNumber.ReadOnly = true;
            colPartNumber.Width = 200;
            // 
            // colLastUpdated
            // 
            colLastUpdated.FillWeight = 95F;
            colLastUpdated.HeaderText = "Last\r\nUpdated";
            colLastUpdated.MinimumWidth = 90;
            colLastUpdated.Name = "colLastUpdated";
            colLastUpdated.ReadOnly = true;
            colLastUpdated.Width = 200;
            // 
            // tblPagination
            // 
            tblPagination.ColumnCount = 2;
            tblPagination.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tblPagination.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tblPagination.Controls.Add(flpPages, 0, 0);
            tblPagination.Controls.Add(flpShowEntries, 1, 0);
            tblPagination.Dock = DockStyle.Fill;
            tblPagination.Location = new Point(30, 2697);
            tblPagination.Margin = new Padding(26, 0, 26, 0);
            tblPagination.Name = "tblPagination";
            tblPagination.RowCount = 1;
            tblPagination.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblPagination.Size = new Size(1863, 114);
            tblPagination.TabIndex = 2;
            // 
            // flpPages
            // 
            flpPages.Dock = DockStyle.Fill;
            flpPages.Location = new Point(0, 0);
            flpPages.Margin = new Padding(0);
            flpPages.Name = "flpPages";
            flpPages.Size = new Size(652, 114);
            flpPages.TabIndex = 0;
            // 
            // flpShowEntries
            // 
            flpShowEntries.Controls.Add(lblShowEntries2);
            flpShowEntries.Controls.Add(cmbShowEntries);
            flpShowEntries.Controls.Add(lblShowEntries1);
            flpShowEntries.Dock = DockStyle.Fill;
            flpShowEntries.FlowDirection = FlowDirection.RightToLeft;
            flpShowEntries.Location = new Point(652, 0);
            flpShowEntries.Margin = new Padding(0);
            flpShowEntries.Name = "flpShowEntries";
            flpShowEntries.Size = new Size(1211, 114);
            flpShowEntries.TabIndex = 1;
            flpShowEntries.WrapContents = false;
            // 
            // lblShowEntries2
            // 
            lblShowEntries2.AutoSize = true;
            lblShowEntries2.Font = new Font("Segoe UI", 9F);
            lblShowEntries2.ForeColor = Color.FromArgb(71, 85, 105);
            lblShowEntries2.Location = new Point(1125, 19);
            lblShowEntries2.Margin = new Padding(6, 19, 0, 0);
            lblShowEntries2.Name = "lblShowEntries2";
            lblShowEntries2.Size = new Size(86, 32);
            lblShowEntries2.TabIndex = 0;
            lblShowEntries2.Text = "entries";
            // 
            // cmbShowEntries
            // 
            cmbShowEntries.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbShowEntries.Font = new Font("Segoe UI", 9F);
            cmbShowEntries.FormattingEnabled = true;
            cmbShowEntries.Items.AddRange(new object[] { "10", "25", "50" });
            cmbShowEntries.Location = new Point(1015, 13);
            cmbShowEntries.Margin = new Padding(6, 13, 6, 0);
            cmbShowEntries.Name = "cmbShowEntries";
            cmbShowEntries.Size = new Size(98, 40);
            cmbShowEntries.TabIndex = 1;
            // 
            // lblShowEntries1
            // 
            lblShowEntries1.AutoSize = true;
            lblShowEntries1.Font = new Font("Segoe UI", 9F);
            lblShowEntries1.ForeColor = Color.FromArgb(71, 85, 105);
            lblShowEntries1.Location = new Point(931, 19);
            lblShowEntries1.Margin = new Padding(0, 19, 6, 0);
            lblShowEntries1.Name = "lblShowEntries1";
            lblShowEntries1.Size = new Size(72, 32);
            lblShowEntries1.TabIndex = 2;
            lblShowEntries1.Text = "Show";
            // 
            // tableLayoutPanelRight
            // 
            tableLayoutPanelRight.ColumnCount = 1;
            tableLayoutPanelRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelRight.Controls.Add(panelDetailCard, 0, 0);
            tableLayoutPanelRight.Controls.Add(tableLayoutPanelBottomRight, 0, 1);
            tableLayoutPanelRight.Dock = DockStyle.Fill;
            tableLayoutPanelRight.Location = new Point(0, 0);
            tableLayoutPanelRight.Margin = new Padding(0);
            tableLayoutPanelRight.Name = "tableLayoutPanelRight";
            tableLayoutPanelRight.RowCount = 2;
            tableLayoutPanelRight.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            tableLayoutPanelRight.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            tableLayoutPanelRight.Size = new Size(3311, 2815);
            tableLayoutPanelRight.TabIndex = 0;
            // 
            // panelDetailCard
            // 
            panelDetailCard.BackColor = Color.White;
            panelDetailCard.ColumnCount = 1;
            panelDetailCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            panelDetailCard.Controls.Add(panelDetailTop, 0, 0);
            panelDetailCard.Controls.Add(panelTabContent, 0, 1);
            panelDetailCard.Dock = DockStyle.Fill;
            panelDetailCard.Location = new Point(0, 0);
            panelDetailCard.Margin = new Padding(0, 0, 0, 26);
            panelDetailCard.Name = "panelDetailCard";
            panelDetailCard.RowCount = 2;
            panelDetailCard.RowStyles.Add(new RowStyle(SizeType.Absolute, 86F));
            panelDetailCard.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            panelDetailCard.Size = new Size(3311, 1941);
            panelDetailCard.TabIndex = 0;
            // 
            // panelDetailTop
            // 
            panelDetailTop.BackColor = Color.Transparent;
            panelDetailTop.Controls.Add(lblDetailTitle);
            panelDetailTop.Controls.Add(flpNavigation);
            panelDetailTop.Dock = DockStyle.Fill;
            panelDetailTop.Location = new Point(0, 0);
            panelDetailTop.Margin = new Padding(0);
            panelDetailTop.Name = "panelDetailTop";
            panelDetailTop.Size = new Size(3311, 86);
            panelDetailTop.TabIndex = 0;
            // 
            // lblDetailTitle
            // 
            lblDetailTitle.AutoSize = true;
            lblDetailTitle.BackColor = Color.Transparent;
            lblDetailTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblDetailTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblDetailTitle.Location = new Point(26, 21);
            lblDetailTitle.Margin = new Padding(0);
            lblDetailTitle.Name = "lblDetailTitle";
            lblDetailTitle.Size = new Size(318, 45);
            lblDetailTitle.TabIndex = 0;
            lblDetailTitle.Text = "HARNESSES DETAIL";
            // 
            // flpNavigation
            // 
            flpNavigation.Controls.Add(lblNavGeneral);
            flpNavigation.Controls.Add(lblNavConnector);
            flpNavigation.Controls.Add(lblNavWiring);
            flpNavigation.Location = new Point(400, 20);
            flpNavigation.Name = "flpNavigation";
            flpNavigation.Size = new Size(1000, 50);
            flpNavigation.TabIndex = 1;
            // 
            // lblNavGeneral
            // 
            lblNavGeneral.AutoSize = true;
            lblNavGeneral.Cursor = Cursors.Hand;
            lblNavGeneral.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblNavGeneral.ForeColor = Color.FromArgb(30, 41, 59);
            lblNavGeneral.Location = new Point(0, 5);
            lblNavGeneral.Margin = new Padding(0, 5, 40, 0);
            lblNavGeneral.Name = "lblNavGeneral";
            lblNavGeneral.Size = new Size(118, 38);
            lblNavGeneral.TabIndex = 0;
            lblNavGeneral.Text = "General";
            // 
            // lblNavConnector
            // 
            lblNavConnector.AutoSize = true;
            lblNavConnector.Cursor = Cursors.Hand;
            lblNavConnector.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblNavConnector.ForeColor = Color.FromArgb(30, 41, 59);
            lblNavConnector.Location = new Point(158, 5);
            lblNavConnector.Margin = new Padding(0, 5, 40, 0);
            lblNavConnector.Name = "lblNavConnector";
            lblNavConnector.Size = new Size(234, 38);
            lblNavConnector.TabIndex = 1;
            lblNavConnector.Text = "Connector && Pin";
            // 
            // lblNavWiring
            // 
            lblNavWiring.AutoSize = true;
            lblNavWiring.Cursor = Cursors.Hand;
            lblNavWiring.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblNavWiring.ForeColor = Color.FromArgb(30, 41, 59);
            lblNavWiring.Location = new Point(432, 5);
            lblNavWiring.Margin = new Padding(0, 5, 40, 0);
            lblNavWiring.Name = "lblNavWiring";
            lblNavWiring.Size = new Size(340, 38);
            lblNavWiring.TabIndex = 2;
            lblNavWiring.Text = "Wiring && Test Parameter";
            // 
            // panelTabContent
            // 
            panelTabContent.Dock = DockStyle.Fill;
            panelTabContent.Location = new Point(20, 86);
            panelTabContent.Margin = new Padding(20, 0, 20, 19);
            panelTabContent.Name = "panelTabContent";
            panelTabContent.Size = new Size(3271, 1891);
            panelTabContent.TabIndex = 1;
            // 
            // tableLayoutPanelBottomRight
            // 
            tableLayoutPanelBottomRight.ColumnCount = 2;
            tableLayoutPanelBottomRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tableLayoutPanelBottomRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tableLayoutPanelBottomRight.Controls.Add(panelCardSpecs, 0, 0);
            tableLayoutPanelBottomRight.Controls.Add(panelCardTestSummary, 1, 0);
            tableLayoutPanelBottomRight.Dock = DockStyle.Fill;
            tableLayoutPanelBottomRight.Location = new Point(0, 1996);
            tableLayoutPanelBottomRight.Margin = new Padding(0);
            tableLayoutPanelBottomRight.Name = "tableLayoutPanelBottomRight";
            tableLayoutPanelBottomRight.RowCount = 1;
            tableLayoutPanelBottomRight.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelBottomRight.Size = new Size(3311, 819);
            tableLayoutPanelBottomRight.TabIndex = 2;
            // 
            // panelCardSpecs
            // 
            panelCardSpecs.BackColor = Color.White;
            panelCardSpecs.Controls.Add(tblSpecsContent);
            panelCardSpecs.Controls.Add(lblSpecsTitle);
            panelCardSpecs.Dock = DockStyle.Fill;
            panelCardSpecs.Location = new Point(0, 0);
            panelCardSpecs.Margin = new Padding(0, 0, 16, 0);
            panelCardSpecs.Name = "panelCardSpecs";
            panelCardSpecs.Padding = new Padding(24, 70, 24, 24);
            panelCardSpecs.Size = new Size(1805, 819);
            panelCardSpecs.TabIndex = 0;
            // 
            // tblSpecsContent
            // 
            tblSpecsContent.ColumnCount = 4;
            tblSpecsContent.ColumnStyles.Add(new ColumnStyle());
            tblSpecsContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tblSpecsContent.ColumnStyles.Add(new ColumnStyle());
            tblSpecsContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tblSpecsContent.Dock = DockStyle.Fill;
            tblSpecsContent.Location = new Point(24, 70);
            tblSpecsContent.Margin = new Padding(0);
            tblSpecsContent.Name = "tblSpecsContent";
            tblSpecsContent.RowCount = 4;
            tblSpecsContent.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tblSpecsContent.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tblSpecsContent.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tblSpecsContent.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tblSpecsContent.Size = new Size(1757, 725);
            tblSpecsContent.TabIndex = 1;
            // 
            // lblSpecsTitle
            // 
            lblSpecsTitle.AutoSize = true;
            lblSpecsTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblSpecsTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblSpecsTitle.Location = new Point(24, 11);
            lblSpecsTitle.Name = "lblSpecsTitle";
            lblSpecsTitle.Size = new Size(383, 41);
            lblSpecsTitle.TabIndex = 0;
            lblSpecsTitle.Text = "HARNESS SPECIFICATION";
            // 
            // panelCardTestSummary
            // 
            panelCardTestSummary.BackColor = Color.White;
            panelCardTestSummary.Controls.Add(tblTestSummaryContent);
            panelCardTestSummary.Controls.Add(lblTestSummaryTitle);
            panelCardTestSummary.Dock = DockStyle.Fill;
            panelCardTestSummary.Location = new Point(1821, 0);
            panelCardTestSummary.Margin = new Padding(0);
            panelCardTestSummary.Name = "panelCardTestSummary";
            panelCardTestSummary.Padding = new Padding(24, 70, 24, 24);
            panelCardTestSummary.Size = new Size(1490, 819);
            panelCardTestSummary.TabIndex = 1;
            // 
            // tblTestSummaryContent
            // 
            tblTestSummaryContent.ColumnCount = 2;
            tblTestSummaryContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tblTestSummaryContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tblTestSummaryContent.Dock = DockStyle.Fill;
            tblTestSummaryContent.Location = new Point(24, 70);
            tblTestSummaryContent.Margin = new Padding(0);
            tblTestSummaryContent.Name = "tblTestSummaryContent";
            tblTestSummaryContent.RowCount = 4;
            tblTestSummaryContent.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tblTestSummaryContent.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tblTestSummaryContent.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tblTestSummaryContent.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tblTestSummaryContent.Size = new Size(1442, 725);
            tblTestSummaryContent.TabIndex = 1;
            // 
            // lblTestSummaryTitle
            // 
            lblTestSummaryTitle.AutoSize = true;
            lblTestSummaryTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTestSummaryTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTestSummaryTitle.Location = new Point(24, 20);
            lblTestSummaryTitle.Name = "lblTestSummaryTitle";
            lblTestSummaryTitle.Size = new Size(458, 41);
            lblTestSummaryTitle.TabIndex = 0;
            lblTestSummaryTitle.Text = "TEST SUMMARY (This Harness)";
            // 
            // tabTestParams
            // 
            tabTestParams.Location = new Point(8, 49);
            tabTestParams.Margin = new Padding(10);
            tabTestParams.Name = "tabTestParams";
            tabTestParams.Size = new Size(3255, 1779);
            tabTestParams.TabIndex = 3;
            tabTestParams.Text = "Test Parameters";
            tabTestParams.UseVisualStyleBackColor = true;
            // 
            // tabHistory
            // 
            tabHistory.Location = new Point(8, 49);
            tabHistory.Margin = new Padding(10);
            tabHistory.Name = "tabHistory";
            tabHistory.Size = new Size(3255, 1779);
            tabHistory.TabIndex = 4;
            tabHistory.Text = "History";
            tabHistory.UseVisualStyleBackColor = true;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTitle.Location = new Point(36, 19);
            lblTitle.Margin = new Padding(10, 0, 10, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(5245, 88);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "🗄️  HARNESSES DATABASE";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.ColumnCount = 1;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Controls.Add(lblTitle, 0, 0);
            tableLayoutPanelMain.Controls.Add(panelFilters, 0, 1);
            tableLayoutPanelMain.Controls.Add(splitContainerMain, 0, 2);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.Location = new Point(0, 0);
            tableLayoutPanelMain.Margin = new Padding(0);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.Padding = new Padding(26, 19, 26, 26);
            tableLayoutPanelMain.RowCount = 3;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 88F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 152F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Size = new Size(5317, 3120);
            tableLayoutPanelMain.TabIndex = 0;
            // 
            // panelFilters
            // 
            panelFilters.BackColor = Color.White;
            panelFilters.Controls.Add(lblProductModel);
            panelFilters.Controls.Add(cmbProductModel);
            panelFilters.Controls.Add(lblProductType);
            panelFilters.Controls.Add(cmbProductType);
            panelFilters.Controls.Add(lblArea);
            panelFilters.Controls.Add(cmbArea);
            panelFilters.Controls.Add(lblPartNumber);
            panelFilters.Controls.Add(txtPartNumber);
            panelFilters.Controls.Add(lblStatus);
            panelFilters.Controls.Add(cmbStatus);
            panelFilters.Controls.Add(btnSearch);
            panelFilters.Controls.Add(btnReset);
            panelFilters.Controls.Add(btnAddHarness);
            panelFilters.Dock = DockStyle.Fill;
            panelFilters.Location = new Point(26, 107);
            panelFilters.Margin = new Padding(0, 0, 0, 26);
            panelFilters.Name = "panelFilters";
            panelFilters.Size = new Size(5265, 126);
            panelFilters.TabIndex = 1;
            // 
            // lblProductModel
            // 
            lblProductModel.AutoSize = true;
            lblProductModel.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblProductModel.ForeColor = Color.FromArgb(71, 85, 105);
            lblProductModel.Location = new Point(32, 19);
            lblProductModel.Margin = new Padding(0);
            lblProductModel.Name = "lblProductModel";
            lblProductModel.Size = new Size(174, 31);
            lblProductModel.TabIndex = 5;
            lblProductModel.Text = "Product Model";
            // 
            // cmbProductModel
            // 
            cmbProductModel.FlatStyle = FlatStyle.System;
            cmbProductModel.Font = new Font("Segoe UI", 9.5F);
            cmbProductModel.FormattingEnabled = true;
            cmbProductModel.Location = new Point(32, 58);
            cmbProductModel.Margin = new Padding(0);
            cmbProductModel.Name = "cmbProductModel";
            cmbProductModel.Size = new Size(258, 43);
            cmbProductModel.TabIndex = 0;
            // 
            // lblProductType
            // 
            lblProductType.AutoSize = true;
            lblProductType.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblProductType.ForeColor = Color.FromArgb(71, 85, 105);
            lblProductType.Location = new Point(317, 19);
            lblProductType.Margin = new Padding(0);
            lblProductType.Name = "lblProductType";
            lblProductType.Size = new Size(155, 31);
            lblProductType.TabIndex = 6;
            lblProductType.Text = "Product Type";
            // 
            // cmbProductType
            // 
            cmbProductType.FlatStyle = FlatStyle.System;
            cmbProductType.Font = new Font("Segoe UI", 9.5F);
            cmbProductType.FormattingEnabled = true;
            cmbProductType.Location = new Point(317, 58);
            cmbProductType.Margin = new Padding(0);
            cmbProductType.Name = "cmbProductType";
            cmbProductType.Size = new Size(258, 43);
            cmbProductType.TabIndex = 1;
            // 
            // lblArea
            // 
            lblArea.AutoSize = true;
            lblArea.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblArea.ForeColor = Color.FromArgb(71, 85, 105);
            lblArea.Location = new Point(601, 19);
            lblArea.Margin = new Padding(0);
            lblArea.Name = "lblArea";
            lblArea.Size = new Size(63, 31);
            lblArea.TabIndex = 7;
            lblArea.Text = "Area";
            // 
            // cmbArea
            // 
            cmbArea.FlatStyle = FlatStyle.System;
            cmbArea.Font = new Font("Segoe UI", 9.5F);
            cmbArea.FormattingEnabled = true;
            cmbArea.Location = new Point(601, 58);
            cmbArea.Margin = new Padding(0);
            cmbArea.Name = "cmbArea";
            cmbArea.Size = new Size(290, 43);
            cmbArea.TabIndex = 2;
            // 
            // lblPartNumber
            // 
            lblPartNumber.AutoSize = true;
            lblPartNumber.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblPartNumber.ForeColor = Color.FromArgb(71, 85, 105);
            lblPartNumber.Location = new Point(918, 19);
            lblPartNumber.Margin = new Padding(0);
            lblPartNumber.Name = "lblPartNumber";
            lblPartNumber.Size = new Size(152, 31);
            lblPartNumber.TabIndex = 8;
            lblPartNumber.Text = "Part Number";
            // 
            // txtPartNumber
            // 
            txtPartNumber.Font = new Font("Segoe UI", 9.5F);
            txtPartNumber.Location = new Point(918, 58);
            txtPartNumber.Margin = new Padding(0);
            txtPartNumber.Name = "txtPartNumber";
            txtPartNumber.PlaceholderText = "Search part number";
            txtPartNumber.Size = new Size(339, 41);
            txtPartNumber.TabIndex = 3;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblStatus.ForeColor = Color.FromArgb(71, 85, 105);
            lblStatus.Location = new Point(1284, 19);
            lblStatus.Margin = new Padding(0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(80, 31);
            lblStatus.TabIndex = 9;
            lblStatus.Text = "Status";
            // 
            // cmbStatus
            // 
            cmbStatus.FlatStyle = FlatStyle.System;
            cmbStatus.Font = new Font("Segoe UI", 9.5F);
            cmbStatus.FormattingEnabled = true;
            cmbStatus.Location = new Point(1284, 58);
            cmbStatus.Margin = new Padding(0);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(225, 43);
            cmbStatus.TabIndex = 4;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(250, 204, 21);
            btnSearch.Cursor = Cursors.Hand;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSearch.ForeColor = Color.FromArgb(15, 23, 42);
            btnSearch.Location = new Point(1536, 54);
            btnSearch.Margin = new Padding(0);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(154, 53);
            btnSearch.TabIndex = 0;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = false;
            // 
            // btnReset
            // 
            btnReset.BackColor = Color.FromArgb(241, 245, 249);
            btnReset.Cursor = Cursors.Hand;
            btnReset.FlatAppearance.BorderSize = 0;
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnReset.ForeColor = Color.FromArgb(51, 65, 85);
            btnReset.Location = new Point(1706, 54);
            btnReset.Margin = new Padding(0);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(138, 53);
            btnReset.TabIndex = 1;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = false;
            // 
            // btnAddHarness
            // 
            btnAddHarness.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddHarness.BackColor = Color.FromArgb(250, 204, 21);
            btnAddHarness.Cursor = Cursors.Hand;
            btnAddHarness.FlatAppearance.BorderSize = 0;
            btnAddHarness.FlatStyle = FlatStyle.Flat;
            btnAddHarness.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnAddHarness.ForeColor = Color.FromArgb(15, 23, 42);
            btnAddHarness.Location = new Point(4925, 50);
            btnAddHarness.Margin = new Padding(0);
            btnAddHarness.Name = "btnAddHarness";
            btnAddHarness.Size = new Size(309, 61);
            btnAddHarness.TabIndex = 10;
            btnAddHarness.Text = "+ Add New Harness";
            btnAddHarness.UseVisualStyleBackColor = false;
            // 
            // HarnessDatabase
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 245, 247);
            Controls.Add(tableLayoutPanelMain);
            Margin = new Padding(10);
            Name = "HarnessDatabase";
            Size = new Size(5317, 3120);
            splitContainerMain.Panel1.ResumeLayout(false);
            splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).EndInit();
            splitContainerMain.ResumeLayout(false);
            tableLayoutPanelLeft.ResumeLayout(false);
            tableLayoutPanelLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewList).EndInit();
            tblPagination.ResumeLayout(false);
            flpShowEntries.ResumeLayout(false);
            flpShowEntries.PerformLayout();
            tableLayoutPanelRight.ResumeLayout(false);
            panelDetailCard.ResumeLayout(false);
            panelDetailTop.ResumeLayout(false);
            panelDetailTop.PerformLayout();
            flpNavigation.ResumeLayout(false);
            flpNavigation.PerformLayout();
            tableLayoutPanelBottomRight.ResumeLayout(false);
            panelCardSpecs.ResumeLayout(false);
            panelCardSpecs.PerformLayout();
            panelCardTestSummary.ResumeLayout(false);
            panelCardTestSummary.PerformLayout();
            tableLayoutPanelMain.ResumeLayout(false);
            tableLayoutPanelMain.PerformLayout();
            panelFilters.ResumeLayout(false);
            panelFilters.PerformLayout();
            ResumeLayout(false);

        }

        public void InitializeCustomStyles()
        {
            if (!DesignMode && System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                dataGridViewList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
                dataGridViewList.ScrollBars = System.Windows.Forms.ScrollBars.Both;

                foreach (System.Windows.Forms.DataGridViewColumn col in dataGridViewList.Columns)
                {
                    col.Resizable = System.Windows.Forms.DataGridViewTriState.True;
                }

                if (dataGridViewList.Columns["colNo"] is System.Windows.Forms.DataGridViewColumn colNo)
                {
                    colNo.MinimumWidth = 50;
                    colNo.Width = 60;
                }
                if (dataGridViewList.Columns["colProductModel"] is System.Windows.Forms.DataGridViewColumn colProductModel)
                {
                    colProductModel.MinimumWidth = 95;
                    colProductModel.Width = 115;
                }
                if (dataGridViewList.Columns["colProductType"] is System.Windows.Forms.DataGridViewColumn colProductType)
                {
                    colProductType.MinimumWidth = 90;
                    colProductType.Width = 105;
                }
                if (dataGridViewList.Columns["colArea"] is System.Windows.Forms.DataGridViewColumn colArea)
                {
                    colArea.MinimumWidth = 170;
                    colArea.Width = 220;
                }
                if (dataGridViewList.Columns["colPartNumber"] is System.Windows.Forms.DataGridViewColumn colPartNumber)
                {
                    colPartNumber.MinimumWidth = 145;
                    colPartNumber.Width = 175;
                }
                if (dataGridViewList.Columns["colEcnNo"] is System.Windows.Forms.DataGridViewColumn colEcnNo)
                {
                    colEcnNo.MinimumWidth = 105;
                    colEcnNo.Width = 125;
                }
                if (dataGridViewList.Columns["colRevision"] is System.Windows.Forms.DataGridViewColumn colRevision)
                {
                    colRevision.MinimumWidth = 55;
                    colRevision.Width = 65;
                }
                if (dataGridViewList.Columns["colStatus"] is System.Windows.Forms.DataGridViewColumn colStatus)
                {
                    colStatus.MinimumWidth = 90;
                    colStatus.Width = 115;
                }
                if (dataGridViewList.Columns["colLastUpdated"] is System.Windows.Forms.DataGridViewColumn colLastUpdated)
                {
                    colLastUpdated.MinimumWidth = 140;
                    colLastUpdated.Width = 160;
                    colLastUpdated.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
                }
            }

            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(panelFilters, 12, System.Drawing.Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(tableLayoutPanelLeft, 12, System.Drawing.Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(panelDetailCard, 12, System.Drawing.Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(panelCardSpecs, 12, System.Drawing.Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(panelCardTestSummary, 12, System.Drawing.Color.FromArgb(34, 40, 49), 2.5f);

            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnSearch, 10, System.Drawing.Color.FromArgb(250, 204, 21), System.Drawing.Color.FromArgb(254, 216, 53), System.Drawing.Color.FromArgb(234, 186, 10), System.Drawing.Color.Transparent);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnReset, 10, System.Drawing.Color.FromArgb(241, 245, 249), System.Drawing.Color.FromArgb(226, 232, 240), System.Drawing.Color.FromArgb(203, 213, 225), System.Drawing.Color.Transparent);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnAddHarness, 10, System.Drawing.Color.FromArgb(250, 204, 21), System.Drawing.Color.FromArgb(254, 216, 53), System.Drawing.Color.FromArgb(234, 186, 10), System.Drawing.Color.Transparent);
        }

        #endregion

        private SplitContainer splitContainerMain;
        private TableLayoutPanel tableLayoutPanelLeft;
        private Label lblListTitle;
        private DataGridView dataGridViewList;
        private DataGridViewTextBoxColumn colNo;
        private DataGridViewTextBoxColumn colProductModel;
        private DataGridViewTextBoxColumn colProductType;
        private DataGridViewTextBoxColumn colArea;
        private DataGridViewTextBoxColumn colPartNumber;
        private DataGridViewTextBoxColumn colLastUpdated;
        private TableLayoutPanel tableLayoutPanelRight;
        private TableLayoutPanel panelDetailCard;
        private Panel panelDetailTop;
        private Label lblDetailTitle;
        private System.Windows.Forms.Panel panelTabContent;
        private FlowLayoutPanel flpNavigation;
        private Label lblNavGeneral;
        private Label lblNavConnector;
        private Label lblNavWiring;
        
        
        
        private TabPage tabTestParams;
        private TabPage tabHistory;
        private TableLayoutPanel tableLayoutPanelBottomRight;
        private Panel panelCardSpecs;
        private Label lblSpecsTitle;
        private TableLayoutPanel tblSpecsContent;
        private Panel panelCardTestSummary;
        private Label lblTestSummaryTitle;
        private TableLayoutPanel tblTestSummaryContent;
        private Label lblTitle;
        private TableLayoutPanel tableLayoutPanelMain;
        private Panel panelFilters;
        private Label lblStatus;
        private Button btnReset;
        private Label lblPartNumber;
        private Button btnSearch;
        private Label lblArea;
        private Label lblProductType;
        private Label lblProductModel;
        private ComboBox cmbStatus;
        private TextBox txtPartNumber;
        private ComboBox cmbArea;
        private ComboBox cmbProductType;
        private ComboBox cmbProductModel;
        private Button btnAddHarness;
        private TableLayoutPanel tblPagination;
        private FlowLayoutPanel flpPages;
        private FlowLayoutPanel flpShowEntries;
        private Label lblShowEntries1;
        private ComboBox cmbShowEntries;
        private Label lblShowEntries2;

    }
}
