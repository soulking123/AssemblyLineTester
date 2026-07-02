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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HarnessDatabase));
            splitContainerMain = new SplitContainer();
            tableLayoutPanelLeft = new TableLayoutPanel();
            lblListTitle = new Label();
            dataGridViewList = new DataGridView();
            colNo = new DataGridViewTextBoxColumn();
            colProductModel = new DataGridViewTextBoxColumn();
            colProductType = new DataGridViewTextBoxColumn();
            colArea = new DataGridViewTextBoxColumn();
            colPartNumber = new DataGridViewTextBoxColumn();
            colEcnNo = new DataGridViewTextBoxColumn();
            colRevision = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            colLastUpdated = new DataGridViewTextBoxColumn();
            tableLayoutPanelRight = new TableLayoutPanel();
            panelDetailTop = new Panel();
            btnDelete = new Button();
            btnEdit = new Button();
            lblDetailTitle = new Label();
            tabControlDetail = new TabControl();
            tabGeneral = new TabPage();
            tableLayoutPanelGeneral = new TableLayoutPanel();
            panelGeneralInfo = new Panel();
            lblGenInfo = new Label();
            panelImages = new Panel();
            lblMachineArea = new Label();
            picMachineArea = new PictureBox();
            lblHarnessImage = new Label();
            picHarnessImage = new PictureBox();
            tabConnector = new TabPage();
            tabWiring = new TabPage();
            tabTestParams = new TabPage();
            tabHistory = new TabPage();
            tableLayoutPanelBottomRight = new TableLayoutPanel();
            groupBoxSpecs = new GroupBox();
            lblSpecsContent = new Label();
            groupBoxTestSummary = new GroupBox();
            lblTestSummaryContent = new Label();
            lblTitle = new Label();
            tableLayoutPanelMain = new TableLayoutPanel();
            panelFilters = new Panel();
            lblStatus = new Label();
            btnReset = new Button();
            lblPartNumber = new Label();
            btnSearch = new Button();
            lblArea = new Label();
            lblProductType = new Label();
            lblProductModel = new Label();
            cmbStatus = new ComboBox();
            txtPartNumber = new TextBox();
            cmbArea = new ComboBox();
            cmbProductType = new ComboBox();
            cmbProductModel = new ComboBox();
            btnAddHarness = new Button();
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
            splitContainerMain.Panel1.SuspendLayout();
            splitContainerMain.Panel2.SuspendLayout();
            splitContainerMain.SuspendLayout();
            tableLayoutPanelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewList).BeginInit();
            tableLayoutPanelRight.SuspendLayout();
            panelDetailTop.SuspendLayout();
            tabControlDetail.SuspendLayout();
            tabGeneral.SuspendLayout();
            tableLayoutPanelGeneral.SuspendLayout();
            panelGeneralInfo.SuspendLayout();
            panelImages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picMachineArea).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picHarnessImage).BeginInit();
            tableLayoutPanelBottomRight.SuspendLayout();
            groupBoxSpecs.SuspendLayout();
            groupBoxTestSummary.SuspendLayout();
            tableLayoutPanelMain.SuspendLayout();
            panelFilters.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainerMain
            // 
            splitContainerMain.Dock = DockStyle.Fill;
            splitContainerMain.Location = new Point(6, 219);
            splitContainerMain.Margin = new Padding(6);
            splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            splitContainerMain.Panel1.Controls.Add(tableLayoutPanelLeft);
            splitContainerMain.Panel1.Padding = new Padding(19, 21, 19, 21);
            // 
            // splitContainerMain.Panel2
            // 
            splitContainerMain.Panel2.Controls.Add(tableLayoutPanelRight);
            splitContainerMain.Panel2.Padding = new Padding(19, 21, 19, 21);
            splitContainerMain.Size = new Size(3260, 1725);
            splitContainerMain.SplitterDistance = 1196;
            splitContainerMain.SplitterWidth = 7;
            splitContainerMain.TabIndex = 3;
            // 
            // tableLayoutPanelLeft
            // 
            tableLayoutPanelLeft.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanelLeft.ColumnCount = 1;
            tableLayoutPanelLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelLeft.Controls.Add(lblListTitle, 0, 0);
            tableLayoutPanelLeft.Controls.Add(dataGridViewList, 0, 1);
            tableLayoutPanelLeft.Dock = DockStyle.Fill;
            tableLayoutPanelLeft.Location = new Point(19, 21);
            tableLayoutPanelLeft.Margin = new Padding(6);
            tableLayoutPanelLeft.Name = "tableLayoutPanelLeft";
            tableLayoutPanelLeft.RowCount = 2;
            tableLayoutPanelLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
            tableLayoutPanelLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelLeft.Size = new Size(1158, 1683);
            tableLayoutPanelLeft.TabIndex = 0;
            // 
            // lblListTitle
            // 
            lblListTitle.AutoSize = true;
            lblListTitle.Dock = DockStyle.Fill;
            lblListTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblListTitle.Location = new Point(6, 0);
            lblListTitle.Margin = new Padding(6, 0, 6, 0);
            lblListTitle.Name = "lblListTitle";
            lblListTitle.Size = new Size(1144, 64);
            lblListTitle.TabIndex = 0;
            lblListTitle.Text = "HARNESS LIST (Total: 58)";
            lblListTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dataGridViewList
            // 
            dataGridViewList.AllowUserToAddRows = false;
            dataGridViewList.AllowUserToDeleteRows = false;
            dataGridViewList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewList.Columns.AddRange(new DataGridViewColumn[] { colNo, colProductModel, colProductType, colArea, colPartNumber, colEcnNo, colRevision, colStatus, colLastUpdated });
            dataGridViewList.Dock = DockStyle.Fill;
            dataGridViewList.Location = new Point(6, 70);
            dataGridViewList.Margin = new Padding(6);
            dataGridViewList.Name = "dataGridViewList";
            dataGridViewList.ReadOnly = true;
            dataGridViewList.RowHeadersVisible = false;
            dataGridViewList.RowHeadersWidth = 82;
            dataGridViewList.RowTemplate.Height = 25;
            dataGridViewList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewList.Size = new Size(1144, 1605);
            dataGridViewList.TabIndex = 1;
            // 
            // colNo
            // 
            colNo.HeaderText = "No.";
            colNo.MinimumWidth = 10;
            colNo.Name = "colNo";
            colNo.ReadOnly = true;
            // 
            // colProductModel
            // 
            colProductModel.HeaderText = "Product Model";
            colProductModel.MinimumWidth = 10;
            colProductModel.Name = "colProductModel";
            colProductModel.ReadOnly = true;
            // 
            // colProductType
            // 
            colProductType.HeaderText = "Product Type";
            colProductType.MinimumWidth = 10;
            colProductType.Name = "colProductType";
            colProductType.ReadOnly = true;
            // 
            // colArea
            // 
            colArea.HeaderText = "Area";
            colArea.MinimumWidth = 10;
            colArea.Name = "colArea";
            colArea.ReadOnly = true;
            // 
            // colPartNumber
            // 
            colPartNumber.HeaderText = "Part Number";
            colPartNumber.MinimumWidth = 10;
            colPartNumber.Name = "colPartNumber";
            colPartNumber.ReadOnly = true;
            // 
            // colEcnNo
            // 
            colEcnNo.HeaderText = "ECN No.";
            colEcnNo.MinimumWidth = 10;
            colEcnNo.Name = "colEcnNo";
            colEcnNo.ReadOnly = true;
            // 
            // colRevision
            // 
            colRevision.HeaderText = "Revision";
            colRevision.MinimumWidth = 10;
            colRevision.Name = "colRevision";
            colRevision.ReadOnly = true;
            // 
            // colStatus
            // 
            colStatus.HeaderText = "Status";
            colStatus.MinimumWidth = 10;
            colStatus.Name = "colStatus";
            colStatus.ReadOnly = true;
            // 
            // colLastUpdated
            // 
            colLastUpdated.HeaderText = "Last Updated";
            colLastUpdated.MinimumWidth = 10;
            colLastUpdated.Name = "colLastUpdated";
            colLastUpdated.ReadOnly = true;
            // 
            // tableLayoutPanelRight
            // 
            tableLayoutPanelRight.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanelRight.ColumnCount = 1;
            tableLayoutPanelRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelRight.Controls.Add(panelDetailTop, 0, 0);
            tableLayoutPanelRight.Controls.Add(tabControlDetail, 0, 1);
            tableLayoutPanelRight.Controls.Add(tableLayoutPanelBottomRight, 0, 2);
            tableLayoutPanelRight.Dock = DockStyle.Fill;
            tableLayoutPanelRight.Location = new Point(19, 21);
            tableLayoutPanelRight.Margin = new Padding(6);
            tableLayoutPanelRight.Name = "tableLayoutPanelRight";
            tableLayoutPanelRight.RowCount = 3;
            tableLayoutPanelRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 85F));
            tableLayoutPanelRight.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            tableLayoutPanelRight.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            tableLayoutPanelRight.Size = new Size(2019, 1683);
            tableLayoutPanelRight.TabIndex = 0;
            // 
            // panelDetailTop
            // 
            panelDetailTop.Controls.Add(btnDelete);
            panelDetailTop.Controls.Add(btnEdit);
            panelDetailTop.Controls.Add(lblDetailTitle);
            panelDetailTop.Dock = DockStyle.Fill;
            panelDetailTop.Location = new Point(6, 6);
            panelDetailTop.Margin = new Padding(6);
            panelDetailTop.Name = "panelDetailTop";
            panelDetailTop.Size = new Size(2005, 73);
            panelDetailTop.TabIndex = 0;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDelete.Location = new Point(1800, 11);
            btnDelete.Margin = new Padding(6);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(139, 49);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            btnEdit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEdit.Location = new Point(1650, 11);
            btnEdit.Margin = new Padding(6);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(139, 49);
            btnEdit.TabIndex = 1;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = true;
            // 
            // lblDetailTitle
            // 
            lblDetailTitle.AutoSize = true;
            lblDetailTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDetailTitle.Location = new Point(7, 17);
            lblDetailTitle.Margin = new Padding(6, 0, 6, 0);
            lblDetailTitle.Name = "lblDetailTitle";
            lblDetailTitle.Size = new Size(237, 37);
            lblDetailTitle.TabIndex = 0;
            lblDetailTitle.Text = "HARNESS DETAIL";
            // 
            // tabControlDetail
            // 
            tabControlDetail.Controls.Add(tabGeneral);
            tabControlDetail.Controls.Add(tabConnector);
            tabControlDetail.Controls.Add(tabWiring);
            tabControlDetail.Controls.Add(tabTestParams);
            tabControlDetail.Controls.Add(tabHistory);
            tabControlDetail.Dock = DockStyle.Fill;
            tabControlDetail.Location = new Point(6, 91);
            tabControlDetail.Margin = new Padding(6);
            tabControlDetail.Name = "tabControlDetail";
            tabControlDetail.SelectedIndex = 0;
            tabControlDetail.Size = new Size(2005, 1105);
            tabControlDetail.TabIndex = 1;
            // 
            // tabGeneral
            // 
            tabGeneral.Controls.Add(tableLayoutPanelGeneral);
            tabGeneral.Location = new Point(8, 46);
            tabGeneral.Margin = new Padding(6);
            tabGeneral.Name = "tabGeneral";
            tabGeneral.Padding = new Padding(6);
            tabGeneral.Size = new Size(1989, 1051);
            tabGeneral.TabIndex = 0;
            tabGeneral.Text = "General";
            tabGeneral.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelGeneral
            // 
            tableLayoutPanelGeneral.ColumnCount = 2;
            tableLayoutPanelGeneral.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelGeneral.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelGeneral.Controls.Add(panelGeneralInfo, 0, 0);
            tableLayoutPanelGeneral.Controls.Add(panelImages, 1, 0);
            tableLayoutPanelGeneral.Dock = DockStyle.Fill;
            tableLayoutPanelGeneral.Location = new Point(6, 6);
            tableLayoutPanelGeneral.Margin = new Padding(6);
            tableLayoutPanelGeneral.Name = "tableLayoutPanelGeneral";
            tableLayoutPanelGeneral.RowCount = 1;
            tableLayoutPanelGeneral.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelGeneral.Size = new Size(1977, 1039);
            tableLayoutPanelGeneral.TabIndex = 0;
            // 
            // panelGeneralInfo
            // 
            panelGeneralInfo.Controls.Add(lblGenInfo);
            panelGeneralInfo.Dock = DockStyle.Fill;
            panelGeneralInfo.Location = new Point(6, 6);
            panelGeneralInfo.Margin = new Padding(6);
            panelGeneralInfo.Name = "panelGeneralInfo";
            panelGeneralInfo.Size = new Size(976, 1027);
            panelGeneralInfo.TabIndex = 0;
            // 
            // lblGenInfo
            // 
            lblGenInfo.AutoSize = true;
            lblGenInfo.Location = new Point(19, 21);
            lblGenInfo.Margin = new Padding(6, 0, 6, 0);
            lblGenInfo.Name = "lblGenInfo";
            lblGenInfo.Size = new Size(397, 416);
            lblGenInfo.TabIndex = 0;
            lblGenInfo.Text = resources.GetString("lblGenInfo.Text");
            // 
            // panelImages
            // 
            panelImages.Controls.Add(lblMachineArea);
            panelImages.Controls.Add(picMachineArea);
            panelImages.Controls.Add(lblHarnessImage);
            panelImages.Controls.Add(picHarnessImage);
            panelImages.Dock = DockStyle.Fill;
            panelImages.Location = new Point(994, 6);
            panelImages.Margin = new Padding(6);
            panelImages.Name = "panelImages";
            panelImages.Size = new Size(977, 1027);
            panelImages.TabIndex = 1;
            // 
            // lblMachineArea
            // 
            lblMachineArea.AutoSize = true;
            lblMachineArea.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMachineArea.Location = new Point(7, 384);
            lblMachineArea.Margin = new Padding(6, 0, 6, 0);
            lblMachineArea.Name = "lblMachineArea";
            lblMachineArea.Size = new Size(348, 32);
            lblMachineArea.TabIndex = 3;
            lblMachineArea.Text = "ASSOCIATED MACHINE AREA";
            // 
            // picMachineArea
            // 
            picMachineArea.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            picMachineArea.BackColor = SystemColors.ControlLight;
            picMachineArea.BorderStyle = BorderStyle.FixedSingle;
            picMachineArea.Location = new Point(7, 427);
            picMachineArea.Margin = new Padding(6);
            picMachineArea.Name = "picMachineArea";
            picMachineArea.Size = new Size(915, 104);
            picMachineArea.SizeMode = PictureBoxSizeMode.Zoom;
            picMachineArea.TabIndex = 2;
            picMachineArea.TabStop = false;
            // 
            // lblHarnessImage
            // 
            lblHarnessImage.AutoSize = true;
            lblHarnessImage.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblHarnessImage.Location = new Point(7, 21);
            lblHarnessImage.Margin = new Padding(6, 0, 6, 0);
            lblHarnessImage.Name = "lblHarnessImage";
            lblHarnessImage.Size = new Size(208, 32);
            lblHarnessImage.TabIndex = 1;
            lblHarnessImage.Text = "HARNESS IMAGE";
            // 
            // picHarnessImage
            // 
            picHarnessImage.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            picHarnessImage.BackColor = SystemColors.ControlLight;
            picHarnessImage.BorderStyle = BorderStyle.FixedSingle;
            picHarnessImage.Location = new Point(7, 64);
            picHarnessImage.Margin = new Padding(6);
            picHarnessImage.Name = "picHarnessImage";
            picHarnessImage.Size = new Size(915, 104);
            picHarnessImage.SizeMode = PictureBoxSizeMode.Zoom;
            picHarnessImage.TabIndex = 0;
            picHarnessImage.TabStop = false;
            // 
            // tabConnector
            // 
            tabConnector.Location = new Point(8, 46);
            tabConnector.Margin = new Padding(6);
            tabConnector.Name = "tabConnector";
            tabConnector.Padding = new Padding(6);
            tabConnector.Size = new Size(1369, 1052);
            tabConnector.TabIndex = 1;
            tabConnector.Text = "Connector & Pin";
            tabConnector.UseVisualStyleBackColor = true;
            // 
            // tabWiring
            // 
            tabWiring.Location = new Point(8, 46);
            tabWiring.Margin = new Padding(6);
            tabWiring.Name = "tabWiring";
            tabWiring.Size = new Size(1369, 1052);
            tabWiring.TabIndex = 2;
            tabWiring.Text = "Wiring Diagram";
            tabWiring.UseVisualStyleBackColor = true;
            // 
            // tabTestParams
            // 
            tabTestParams.Location = new Point(8, 46);
            tabTestParams.Margin = new Padding(6);
            tabTestParams.Name = "tabTestParams";
            tabTestParams.Size = new Size(1369, 1052);
            tabTestParams.TabIndex = 3;
            tabTestParams.Text = "Test Parameters";
            tabTestParams.UseVisualStyleBackColor = true;
            // 
            // tabHistory
            // 
            tabHistory.Location = new Point(8, 46);
            tabHistory.Margin = new Padding(6);
            tabHistory.Name = "tabHistory";
            tabHistory.Size = new Size(1369, 1052);
            tabHistory.TabIndex = 4;
            tabHistory.Text = "History";
            tabHistory.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelBottomRight
            // 
            tableLayoutPanelBottomRight.ColumnCount = 2;
            tableLayoutPanelBottomRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayoutPanelBottomRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanelBottomRight.Controls.Add(groupBoxSpecs, 0, 0);
            tableLayoutPanelBottomRight.Controls.Add(groupBoxTestSummary, 1, 0);
            tableLayoutPanelBottomRight.Dock = DockStyle.Fill;
            tableLayoutPanelBottomRight.Location = new Point(6, 1208);
            tableLayoutPanelBottomRight.Margin = new Padding(6);
            tableLayoutPanelBottomRight.Name = "tableLayoutPanelBottomRight";
            tableLayoutPanelBottomRight.RowCount = 1;
            tableLayoutPanelBottomRight.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelBottomRight.Size = new Size(2005, 467);
            tableLayoutPanelBottomRight.TabIndex = 2;
            // 
            // groupBoxSpecs
            // 
            groupBoxSpecs.Controls.Add(lblSpecsContent);
            groupBoxSpecs.Dock = DockStyle.Fill;
            groupBoxSpecs.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBoxSpecs.Location = new Point(6, 6);
            groupBoxSpecs.Margin = new Padding(6);
            groupBoxSpecs.Name = "groupBoxSpecs";
            groupBoxSpecs.Padding = new Padding(6);
            groupBoxSpecs.Size = new Size(1191, 455);
            groupBoxSpecs.TabIndex = 0;
            groupBoxSpecs.TabStop = false;
            groupBoxSpecs.Text = "HARNESS SPECIFICATION";
            // 
            // lblSpecsContent
            // 
            lblSpecsContent.AutoSize = true;
            lblSpecsContent.Font = new Font("Segoe UI", 9F);
            lblSpecsContent.Location = new Point(19, 53);
            lblSpecsContent.Margin = new Padding(6, 0, 6, 0);
            lblSpecsContent.Name = "lblSpecsContent";
            lblSpecsContent.Size = new Size(405, 288);
            lblSpecsContent.TabIndex = 0;
            lblSpecsContent.Text = "Total Connector : 6\nTotal Pin : 48\nTotal Wire : 62\nTotal Branch : 12\n\nMin Wire Size : 0.50 mm²\nMax Wire Size : 4.00 mm²\nNominal Voltage : 24 VDC\nOperating Temperature : -40 ~ 85 °C";
            // 
            // groupBoxTestSummary
            // 
            groupBoxTestSummary.Controls.Add(lblTestSummaryContent);
            groupBoxTestSummary.Dock = DockStyle.Fill;
            groupBoxTestSummary.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBoxTestSummary.Location = new Point(1209, 6);
            groupBoxTestSummary.Margin = new Padding(6);
            groupBoxTestSummary.Name = "groupBoxTestSummary";
            groupBoxTestSummary.Padding = new Padding(6);
            groupBoxTestSummary.Size = new Size(790, 455);
            groupBoxTestSummary.TabIndex = 1;
            groupBoxTestSummary.TabStop = false;
            groupBoxTestSummary.Text = "TEST SUMMARY (This Harness)";
            // 
            // lblTestSummaryContent
            // 
            lblTestSummaryContent.AutoSize = true;
            lblTestSummaryContent.Font = new Font("Segoe UI", 9F);
            lblTestSummaryContent.Location = new Point(19, 53);
            lblTestSummaryContent.Margin = new Padding(6, 0, 6, 0);
            lblTestSummaryContent.Name = "lblTestSummaryContent";
            lblTestSummaryContent.Size = new Size(392, 128);
            lblTestSummaryContent.TabIndex = 0;
            lblTestSummaryContent.Text = "Total Tested : 215\nPass : 212 (98.60%)\nFail : 3 (1.40%)\nLast Tested : 15 Mar 2026 10:30 AM";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(6, 0);
            lblTitle.Margin = new Padding(6, 0, 6, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(3260, 85);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "HARNESS DATABASE";
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
            tableLayoutPanelMain.Margin = new Padding(6);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 3;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 85F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 128F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanelMain.Size = new Size(3272, 1950);
            tableLayoutPanelMain.TabIndex = 0;
            // 
            // panelFilters
            // 
            panelFilters.Controls.Add(lblStatus);
            panelFilters.Controls.Add(btnReset);
            panelFilters.Controls.Add(lblPartNumber);
            panelFilters.Controls.Add(btnSearch);
            panelFilters.Controls.Add(lblArea);
            panelFilters.Controls.Add(lblProductType);
            panelFilters.Controls.Add(lblProductModel);
            panelFilters.Controls.Add(cmbStatus);
            panelFilters.Controls.Add(txtPartNumber);
            panelFilters.Controls.Add(cmbArea);
            panelFilters.Controls.Add(cmbProductType);
            panelFilters.Controls.Add(cmbProductModel);
            panelFilters.Controls.Add(btnAddHarness);
            panelFilters.Dock = DockStyle.Fill;
            panelFilters.Location = new Point(6, 91);
            panelFilters.Margin = new Padding(6);
            panelFilters.Name = "panelFilters";
            panelFilters.Size = new Size(3260, 116);
            panelFilters.TabIndex = 1;
            panelFilters.BorderStyle = BorderStyle.FixedSingle;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(1170, 11);
            lblStatus.Margin = new Padding(6, 0, 6, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(78, 32);
            lblStatus.TabIndex = 9;
            lblStatus.Text = "Status";
            // 
            // btnReset
            // 
            btnReset.Location = new Point(1626, 49);
            btnReset.Margin = new Padding(6);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(139, 49);
            btnReset.TabIndex = 1;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = true;
            // 
            // lblPartNumber
            // 
            lblPartNumber.AutoSize = true;
            lblPartNumber.Location = new Point(873, 11);
            lblPartNumber.Margin = new Padding(6, 0, 6, 0);
            lblPartNumber.Name = "lblPartNumber";
            lblPartNumber.Size = new Size(149, 32);
            lblPartNumber.TabIndex = 8;
            lblPartNumber.Text = "Part Number";
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.Gold;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSearch.Location = new Point(1475, 49);
            btnSearch.Margin = new Padding(6);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(139, 49);
            btnSearch.TabIndex = 0;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = false;
            //
            // btnAddHarness
            //
            btnAddHarness.BackColor = Color.Gold;
            btnAddHarness.FlatStyle = FlatStyle.Flat;
            btnAddHarness.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAddHarness.Location = new Point(1775, 49);
            btnAddHarness.Margin = new Padding(6);
            btnAddHarness.Name = "btnAddHarness";
            btnAddHarness.Size = new Size(250, 49);
            btnAddHarness.TabIndex = 10; BorderStyle = BorderStyle.FixedSingle;
            btnAddHarness.Text = "+ Add New Harness";
            btnAddHarness.UseVisualStyleBackColor = false;
            // 
            // lblArea
            // 
            lblArea.AutoSize = true;
            lblArea.Location = new Point(594, 11);
            lblArea.Margin = new Padding(6, 0, 6, 0);
            lblArea.Name = "lblArea";
            lblArea.Size = new Size(62, 32);
            lblArea.TabIndex = 7;
            lblArea.Text = "Area";
            // 
            // lblProductType
            // 
            lblProductType.AutoSize = true;
            lblProductType.Location = new Point(316, 11);
            lblProductType.Margin = new Padding(6, 0, 6, 0);
            lblProductType.Name = "lblProductType";
            lblProductType.Size = new Size(154, 32);
            lblProductType.TabIndex = 6;
            lblProductType.Text = "Product Type";
            // 
            // lblProductModel
            // 
            lblProductModel.AutoSize = true;
            lblProductModel.Location = new Point(37, 11);
            lblProductModel.Margin = new Padding(6, 0, 6, 0);
            lblProductModel.Name = "lblProductModel";
            lblProductModel.Size = new Size(172, 32);
            lblProductModel.TabIndex = 5;
            lblProductModel.Text = "Product Model";
            // 
            // cmbStatus
            // 
            cmbStatus.FormattingEnabled = true;
            cmbStatus.Location = new Point(1170, 54);
            cmbStatus.Margin = new Padding(6);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(221, 40);
            cmbStatus.TabIndex = 4;
            // 
            // txtPartNumber
            // 
            txtPartNumber.Location = new Point(873, 54);
            txtPartNumber.Margin = new Padding(6);
            txtPartNumber.Name = "txtPartNumber";
            txtPartNumber.PlaceholderText = "Search part number";
            txtPartNumber.Size = new Size(269, 39);
            txtPartNumber.TabIndex = 3;
            // 
            // cmbArea
            // 
            cmbArea.FormattingEnabled = true;
            cmbArea.Location = new Point(594, 54);
            cmbArea.Margin = new Padding(6);
            cmbArea.Name = "cmbArea";
            cmbArea.Size = new Size(221, 40);
            cmbArea.TabIndex = 2;
            // 
            // cmbProductType
            // 
            cmbProductType.FormattingEnabled = true;
            cmbProductType.Location = new Point(316, 54);
            cmbProductType.Margin = new Padding(6);
            cmbProductType.Name = "cmbProductType";
            cmbProductType.Size = new Size(221, 40);
            cmbProductType.TabIndex = 1;
            // 
            // cmbProductModel
            // 
            cmbProductModel.FormattingEnabled = true;
            cmbProductModel.Location = new Point(37, 54);
            cmbProductModel.Margin = new Padding(6);
            cmbProductModel.Name = "cmbProductModel";
            cmbProductModel.Size = new Size(221, 40);
            cmbProductModel.TabIndex = 0;
            // 
            // HarnessDatabase
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(tableLayoutPanelMain);
            Margin = new Padding(6);
            Name = "HarnessDatabase";
            Size = new Size(3272, 1950);
            splitContainerMain.Panel1.ResumeLayout(false);
            splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).EndInit();
            splitContainerMain.ResumeLayout(false);
            tableLayoutPanelLeft.ResumeLayout(false);
            tableLayoutPanelLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewList).EndInit();
            tableLayoutPanelRight.ResumeLayout(false);
            panelDetailTop.ResumeLayout(false);
            panelDetailTop.PerformLayout();
            tabControlDetail.ResumeLayout(false);
            tabGeneral.ResumeLayout(false);
            tableLayoutPanelGeneral.ResumeLayout(false);
            panelGeneralInfo.ResumeLayout(false);
            panelGeneralInfo.PerformLayout();
            panelImages.ResumeLayout(false);
            panelImages.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picMachineArea).EndInit();
            ((System.ComponentModel.ISupportInitialize)picHarnessImage).EndInit();
            tableLayoutPanelBottomRight.ResumeLayout(false);
            groupBoxSpecs.ResumeLayout(false);
            groupBoxSpecs.PerformLayout();
            groupBoxTestSummary.ResumeLayout(false);
            groupBoxTestSummary.PerformLayout();
            tableLayoutPanelMain.ResumeLayout(false);
            tableLayoutPanelMain.PerformLayout();
            panelFilters.ResumeLayout(false);
            panelFilters.PerformLayout();
            ResumeLayout(false);

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
        private DataGridViewTextBoxColumn colEcnNo;
        private DataGridViewTextBoxColumn colRevision;
        private DataGridViewTextBoxColumn colStatus;
        private DataGridViewTextBoxColumn colLastUpdated;
        private TableLayoutPanel tableLayoutPanelRight;
        private Panel panelDetailTop;
        private Button btnDelete;
        private Button btnEdit;
        private Label lblDetailTitle;
        private TabControl tabControlDetail;
        private TabPage tabGeneral;
        private TableLayoutPanel tableLayoutPanelGeneral;
        private Panel panelGeneralInfo;
        private Label lblGenInfo;
        private Panel panelImages;
        private Label lblMachineArea;
        private PictureBox picMachineArea;
        private Label lblHarnessImage;
        private PictureBox picHarnessImage;
        private TabPage tabConnector;
        private TabPage tabWiring;
        private TabPage tabTestParams;
        private TabPage tabHistory;
        private TableLayoutPanel tableLayoutPanelBottomRight;
        private GroupBox groupBoxSpecs;
        private Label lblSpecsContent;
        private GroupBox groupBoxTestSummary;
        private Label lblTestSummaryContent;
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
    }
}
