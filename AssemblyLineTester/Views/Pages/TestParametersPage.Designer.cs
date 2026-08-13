namespace AssemblyLineTester.Views.Pages
{
    partial class TestParametersPage
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
            btnLearnConnection = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnStartAll = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnStartSelected = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnAbort = new AssemblyLineTester.Views.Controls.RoundedButton();
            splitContainerMain = new SplitContainer();
            pnlLeftBox = new Panel();
            gridConfig = new DataGridView();
            colNo = new DataGridViewTextBoxColumn();
            colNetId = new DataGridViewTextBoxColumn();
            colHarnessGroup = new DataGridViewTextBoxColumn();
            colColor = new DataGridViewTextBoxColumn();
            colGauge = new DataGridViewTextBoxColumn();
            colFromAddr = new DataGridViewTextBoxColumn();
            colFromLabel = new DataGridViewTextBoxColumn();
            colToAddr = new DataGridViewTextBoxColumn();
            colToLabel = new DataGridViewTextBoxColumn();
            colTestType = new DataGridViewComboBoxColumn();
            colLowerLimit = new DataGridViewTextBoxColumn();
            colUpperLimit = new DataGridViewTextBoxColumn();
            colExpectedValue = new DataGridViewTextBoxColumn();
            colTolerance = new DataGridViewTextBoxColumn();
            colUnit = new DataGridViewTextBoxColumn();
            pnlConfigInfo = new Panel();
            tblConfigInfo = new TableLayoutPanel();
            tblCellConfigName = new TableLayoutPanel();
            lblConfigName = new Label();
            txtConfigName = new TextBox();
            tblCellProductModel = new TableLayoutPanel();
            lblProductModel = new Label();
            txtProductModel = new ComboBox();
            tblCellConnRes = new TableLayoutPanel();
            lblConnectionResistance = new Label();
            txtConnectionResistance = new TextBox();
            tblCellCompRes = new TableLayoutPanel();
            lblComponentResistance = new Label();
            txtComponentResistance = new TextBox();
            tblCellNote = new TableLayoutPanel();
            lblNote = new Label();
            txtNote = new TextBox();
            tblCellInsRes = new TableLayoutPanel();
            lblInsulationResistance = new Label();
            txtInsulationResistance = new TextBox();
            pnlConfigInfoHeader = new Panel();
            lblConfigInfoHeader = new Label();
            pnlButtons = new Panel();
            tblButtons = new TableLayoutPanel();
            flpButtonsLeft = new FlowLayoutPanel();
            btnTotalNet = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnAdd = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnRemove = new AssemblyLineTester.Views.Controls.RoundedButton();
            flpButtonsRight = new FlowLayoutPanel();
            btnExport = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnReset = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnImport = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnImportExcel = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnTemplate = new AssemblyLineTester.Views.Controls.RoundedButton();
            pnlHeaderTop = new Panel();
            lblHeaderTop = new Label();
            pnlRightBox = new Panel();
            pnlPreviewScroll = new Panel();
            picPreview = new PictureBox();
            pnlHeaderBottom = new Panel();
            lblHeaderBottom = new Label();
            btnComplex = new Button();
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
            splitContainerMain.Panel1.SuspendLayout();
            splitContainerMain.Panel2.SuspendLayout();
            splitContainerMain.SuspendLayout();
            pnlLeftBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridConfig).BeginInit();
            pnlConfigInfo.SuspendLayout();
            tblConfigInfo.SuspendLayout();
            tblCellConfigName.SuspendLayout();
            tblCellProductModel.SuspendLayout();
            tblCellConnRes.SuspendLayout();
            tblCellCompRes.SuspendLayout();
            tblCellNote.SuspendLayout();
            tblCellInsRes.SuspendLayout();
            pnlConfigInfoHeader.SuspendLayout();
            pnlButtons.SuspendLayout();
            tblButtons.SuspendLayout();
            flpButtonsLeft.SuspendLayout();
            flpButtonsRight.SuspendLayout();
            pnlHeaderTop.SuspendLayout();
            pnlRightBox.SuspendLayout();
            pnlPreviewScroll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picPreview).BeginInit();
            pnlHeaderBottom.SuspendLayout();
            SuspendLayout();
            // 
            // btnLearnConnection
            // 
            btnLearnConnection.BackColor = Color.FromArgb(203, 213, 225);
            btnLearnConnection.BorderRadius = 14;
            btnLearnConnection.Dock = DockStyle.Fill;
            btnLearnConnection.FlatAppearance.BorderSize = 0;
            btnLearnConnection.FlatStyle = FlatStyle.Flat;
            btnLearnConnection.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLearnConnection.ForeColor = Color.Black;
            btnLearnConnection.HoverBackColor = Color.FromArgb(148, 163, 184);
            btnLearnConnection.HoverForeColor = Color.Empty;
            btnLearnConnection.Location = new Point(12, 137);
            btnLearnConnection.Margin = new Padding(12, 6, 12, 6);
            btnLearnConnection.Name = "btnLearnConnection";
            btnLearnConnection.NormalBackColor = Color.FromArgb(203, 213, 225);
            btnLearnConnection.PressedBackColor = Color.FromArgb(100, 116, 139);
            btnLearnConnection.PressedForeColor = Color.Empty;
            btnLearnConnection.Size = new Size(140, 30);
            btnLearnConnection.TabIndex = 0;
            btnLearnConnection.Text = "Learn Connection";
            btnLearnConnection.UseVisualStyleBackColor = false;
            btnLearnConnection.Click += btnLearnConnection_Click;
            // 
            // btnStartAll
            // 
            btnStartAll.BackColor = Color.FromArgb(203, 213, 225);
            btnStartAll.BorderRadius = 14;
            btnStartAll.Dock = DockStyle.Fill;
            btnStartAll.FlatAppearance.BorderSize = 0;
            btnStartAll.FlatStyle = FlatStyle.Flat;
            btnStartAll.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnStartAll.ForeColor = Color.Black;
            btnStartAll.HoverBackColor = Color.FromArgb(148, 163, 184);
            btnStartAll.HoverForeColor = Color.Empty;
            btnStartAll.Location = new Point(176, 137);
            btnStartAll.Margin = new Padding(12, 6, 12, 6);
            btnStartAll.Name = "btnStartAll";
            btnStartAll.NormalBackColor = Color.FromArgb(203, 213, 225);
            btnStartAll.PressedBackColor = Color.FromArgb(100, 116, 139);
            btnStartAll.PressedForeColor = Color.Empty;
            btnStartAll.Size = new Size(140, 30);
            btnStartAll.TabIndex = 1;
            btnStartAll.Text = "Start";
            btnStartAll.UseVisualStyleBackColor = false;
            btnStartAll.Click += btnStartAll_Click;
            // 
            // btnStartSelected
            // 
            btnStartSelected.BackColor = Color.FromArgb(203, 213, 225);
            btnStartSelected.BorderRadius = 14;
            btnStartSelected.Dock = DockStyle.Fill;
            btnStartSelected.FlatAppearance.BorderSize = 0;
            btnStartSelected.FlatStyle = FlatStyle.Flat;
            btnStartSelected.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnStartSelected.ForeColor = Color.Black;
            btnStartSelected.HoverBackColor = Color.FromArgb(148, 163, 184);
            btnStartSelected.HoverForeColor = Color.Empty;
            btnStartSelected.Location = new Point(340, 137);
            btnStartSelected.Margin = new Padding(12, 6, 12, 6);
            btnStartSelected.Name = "btnStartSelected";
            btnStartSelected.NormalBackColor = Color.FromArgb(203, 213, 225);
            btnStartSelected.PressedBackColor = Color.FromArgb(100, 116, 139);
            btnStartSelected.PressedForeColor = Color.Empty;
            btnStartSelected.Size = new Size(140, 30);
            btnStartSelected.TabIndex = 2;
            btnStartSelected.Text = "Start Selected";
            btnStartSelected.UseVisualStyleBackColor = false;
            btnStartSelected.Click += btnStartSelected_Click;
            // 
            // btnAbort
            // 
            btnAbort.BackColor = Color.FromArgb(203, 213, 225);
            btnAbort.BorderRadius = 14;
            btnAbort.Dock = DockStyle.Fill;
            btnAbort.FlatAppearance.BorderSize = 0;
            btnAbort.FlatStyle = FlatStyle.Flat;
            btnAbort.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAbort.ForeColor = Color.Black;
            btnAbort.HoverBackColor = Color.FromArgb(148, 163, 184);
            btnAbort.HoverForeColor = Color.Empty;
            btnAbort.Location = new Point(504, 137);
            btnAbort.Margin = new Padding(12, 6, 12, 6);
            btnAbort.Name = "btnAbort";
            btnAbort.NormalBackColor = Color.FromArgb(203, 213, 225);
            btnAbort.PressedBackColor = Color.FromArgb(100, 116, 139);
            btnAbort.PressedForeColor = Color.Empty;
            btnAbort.Size = new Size(141, 30);
            btnAbort.TabIndex = 3;
            btnAbort.Text = "Abort";
            btnAbort.UseVisualStyleBackColor = false;
            btnAbort.Click += btnAbort_Click;
            // 
            // splitContainerMain
            // 
            splitContainerMain.BackColor = Color.FromArgb(203, 213, 225);
            splitContainerMain.Dock = DockStyle.Fill;
            splitContainerMain.IsSplitterFixed = true;
            splitContainerMain.Location = new Point(9, 9);
            splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            splitContainerMain.Panel1.BackColor = Color.FromArgb(248, 250, 252);
            splitContainerMain.Panel1.Controls.Add(pnlLeftBox);
            splitContainerMain.Panel1MinSize = 400;
            // 
            // splitContainerMain.Panel2
            // 
            splitContainerMain.Panel2.BackColor = Color.FromArgb(248, 250, 252);
            splitContainerMain.Panel2.Controls.Add(pnlRightBox);
            splitContainerMain.Panel2MinSize = 400;
            splitContainerMain.Size = new Size(1241, 832);
            splitContainerMain.SplitterDistance = 677;
            splitContainerMain.SplitterWidth = 7;
            splitContainerMain.TabIndex = 0;
            // 
            // pnlLeftBox
            // 
            pnlLeftBox.BackColor = Color.White;
            pnlLeftBox.BorderStyle = BorderStyle.FixedSingle;
            pnlLeftBox.Controls.Add(gridConfig);
            pnlLeftBox.Controls.Add(pnlConfigInfo);
            pnlLeftBox.Controls.Add(pnlConfigInfoHeader);
            pnlLeftBox.Controls.Add(pnlButtons);
            pnlLeftBox.Controls.Add(pnlHeaderTop);
            pnlLeftBox.Dock = DockStyle.Fill;
            pnlLeftBox.Location = new Point(0, 0);
            pnlLeftBox.Margin = new Padding(0);
            pnlLeftBox.Name = "pnlLeftBox";
            pnlLeftBox.Size = new Size(677, 832);
            pnlLeftBox.TabIndex = 0;
            // 
            // gridConfig
            // 
            gridConfig.AllowUserToAddRows = false;
            gridConfig.AllowUserToDeleteRows = false;
            gridConfig.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridConfig.BackgroundColor = Color.White;
            gridConfig.BorderStyle = BorderStyle.None;
            gridConfig.CausesValidation = false;
            gridConfig.ColumnHeadersHeight = 45;
            gridConfig.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            gridConfig.Columns.AddRange(new DataGridViewColumn[] { colNo, colHarnessGroup, colColor, colGauge, colNetId, colFromAddr, colFromLabel, colToAddr, colToLabel, colTestType, colLowerLimit, colUpperLimit, colExpectedValue, colTolerance, colUnit });
            gridConfig.Dock = DockStyle.Fill;
            gridConfig.EnableHeadersVisualStyles = false;
            gridConfig.Location = new Point(0, 283);
            gridConfig.Name = "gridConfig";
            gridConfig.RowHeadersVisible = false;
            gridConfig.RowHeadersWidth = 82;
            gridConfig.RowTemplate.Height = 38;
            gridConfig.Size = new Size(675, 547);
            gridConfig.TabIndex = 4;
            // 
            // colNo
            // 
            colNo.HeaderText = "No.";
            colNo.MinimumWidth = 44;
            colNo.FillWeight = 4F;
            colNo.Name = "colNo";
            colNo.ReadOnly = true;
            // 
            // colColor
            // 
            colColor.HeaderText = "Color";
            colColor.MinimumWidth = 70;
            colColor.FillWeight = 8F;
            colColor.Name = "colColor";
            // 
            // colGauge
            // 
            colGauge.HeaderText = "Gauge";
            colGauge.MinimumWidth = 70;
            colGauge.FillWeight = 8F;
            colGauge.Name = "colGauge";
            // 
            // colNetId
            // 
            colNetId.HeaderText = "Net-ID";
            colNetId.MinimumWidth = 90;
            colNetId.FillWeight = 12F;
            colNetId.Name = "colNetId";
            // 
            // colHarnessGroup
            // 
            colHarnessGroup.HeaderText = "Harness-Group";
            colHarnessGroup.MinimumWidth = 110;
            colHarnessGroup.FillWeight = 16F;
            colHarnessGroup.Name = "colHarnessGroup";
            // 
            // colFromAddr
            // 
            colFromAddr.HeaderText = "From";
            colFromAddr.MinimumWidth = 52;
            colFromAddr.FillWeight = 5F;
            colFromAddr.Name = "colFromAddr";
            // 
            // colFromLabel
            // 
            colFromLabel.HeaderText = "Net-From\nLabel";
            colFromLabel.MinimumWidth = 95;
            colFromLabel.FillWeight = 13F;
            colFromLabel.Name = "colFromLabel";
            // 
            // colToAddr
            // 
            colToAddr.HeaderText = "To";
            colToAddr.MinimumWidth = 52;
            colToAddr.FillWeight = 5F;
            colToAddr.Name = "colToAddr";
            // 
            // colToLabel
            // 
            colToLabel.HeaderText = "Net-To\nLabel";
            colToLabel.MinimumWidth = 95;
            colToLabel.FillWeight = 13F;
            colToLabel.Name = "colToLabel";
            // 
            // colTestType
            // 
            colTestType.FlatStyle = FlatStyle.Flat;
            colTestType.HeaderText = "Test Type";
            colTestType.Items.AddRange(new object[] { "WIRE", "4WIRE", "TWISTED PAIR", "SIGNATURE WIRE", "RESISTOR", "4RESISTOR", "CAPACITOR", "DIODE", "SIGNATURE DIODE", "OPEN", "CLOSE", "LINK", "PROBE", "COMMENT", "LUA COMPONENT" });
            colTestType.MinimumWidth = 115;
            colTestType.FillWeight = 15F;
            colTestType.Name = "colTestType";
            // 
            // colLowerLimit
            // 
            colLowerLimit.HeaderText = "Lower\nLimit";
            colLowerLimit.MinimumWidth = 62;
            colLowerLimit.FillWeight = 7F;
            colLowerLimit.Name = "colLowerLimit";
            colLowerLimit.ReadOnly = true;
            // 
            // colUpperLimit
            // 
            colUpperLimit.HeaderText = "Upper\nLimit";
            colUpperLimit.MinimumWidth = 62;
            colUpperLimit.FillWeight = 7F;
            colUpperLimit.Name = "colUpperLimit";
            colUpperLimit.ReadOnly = true;
            // 
            // colExpectedValue
            // 
            colExpectedValue.HeaderText = "Expected\nValue";
            colExpectedValue.MinimumWidth = 78;
            colExpectedValue.FillWeight = 9F;
            colExpectedValue.Name = "colExpectedValue";
            // 
            // colTolerance
            // 
            colTolerance.HeaderText = "Tolerance";
            colTolerance.MinimumWidth = 70;
            colTolerance.FillWeight = 8F;
            colTolerance.Name = "colTolerance";
            // 
            // colUnit
            // 
            colUnit.HeaderText = "Measurement";
            colUnit.MinimumWidth = 88;
            colUnit.FillWeight = 10F;
            colUnit.Name = "colUnit";
            colUnit.Resizable = DataGridViewTriState.True;
            colUnit.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // pnlConfigInfo
            // 
            pnlConfigInfo.AutoSize = true;
            pnlConfigInfo.BackColor = Color.White;
            pnlConfigInfo.CausesValidation = false;
            pnlConfigInfo.Controls.Add(tblConfigInfo);
            pnlConfigInfo.Dock = DockStyle.Top;
            pnlConfigInfo.Location = new Point(0, 98);
            pnlConfigInfo.Name = "pnlConfigInfo";
            pnlConfigInfo.Padding = new Padding(9, 6, 9, 6);
            pnlConfigInfo.Size = new Size(675, 185);
            pnlConfigInfo.TabIndex = 3;
            // 
            // tblConfigInfo
            // 
            tblConfigInfo.AutoSize = true;
            tblConfigInfo.BackColor = Color.White;
            tblConfigInfo.CausesValidation = false;
            tblConfigInfo.ColumnCount = 4;
            tblConfigInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblConfigInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblConfigInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblConfigInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblConfigInfo.Controls.Add(tblCellConfigName, 0, 0);
            tblConfigInfo.Controls.Add(tblCellProductModel, 2, 0);
            tblConfigInfo.Controls.Add(tblCellConnRes, 0, 1);
            tblConfigInfo.Controls.Add(tblCellInsRes, 2, 1);
            tblConfigInfo.Controls.Add(tblCellCompRes, 0, 2);
            tblConfigInfo.Controls.Add(tblCellNote, 2, 2);
            tblConfigInfo.Controls.Add(btnLearnConnection, 0, 3);
            tblConfigInfo.Controls.Add(btnStartAll, 1, 3);
            tblConfigInfo.Controls.Add(btnStartSelected, 2, 3);
            tblConfigInfo.Controls.Add(btnAbort, 3, 3);
            tblConfigInfo.Dock = DockStyle.Fill;
            tblConfigInfo.Location = new Point(9, 6);
            tblConfigInfo.Margin = new Padding(0);
            tblConfigInfo.Name = "tblConfigInfo";
            tblConfigInfo.RowCount = 4;
            tblConfigInfo.RowStyles.Add(new RowStyle());
            tblConfigInfo.RowStyles.Add(new RowStyle());
            tblConfigInfo.RowStyles.Add(new RowStyle());
            tblConfigInfo.RowStyles.Add(new RowStyle());
            tblConfigInfo.Size = new Size(657, 173);
            tblConfigInfo.TabIndex = 0;
            // 
            // tblCellConfigName
            // 
            tblCellConfigName.AutoSize = true;
            tblCellConfigName.ColumnCount = 1;
            tblConfigInfo.SetColumnSpan(tblCellConfigName, 2);
            tblCellConfigName.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblCellConfigName.Controls.Add(lblConfigName, 0, 0);
            tblCellConfigName.Controls.Add(txtConfigName, 0, 1);
            tblCellConfigName.Dock = DockStyle.Fill;
            tblCellConfigName.Location = new Point(6, 4);
            tblCellConfigName.Margin = new Padding(6, 4, 15, 8);
            tblCellConfigName.Name = "tblCellConfigName";
            tblCellConfigName.RowCount = 2;
            tblCellConfigName.RowStyles.Add(new RowStyle());
            tblCellConfigName.RowStyles.Add(new RowStyle());
            tblCellConfigName.Size = new Size(307, 56);
            tblCellConfigName.TabIndex = 0;
            // 
            // lblConfigName
            // 
            lblConfigName.AutoSize = true;
            lblConfigName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblConfigName.ForeColor = Color.FromArgb(100, 116, 139);
            lblConfigName.Location = new Point(0, 0);
            lblConfigName.Margin = new Padding(0, 0, 0, 6);
            lblConfigName.Name = "lblConfigName";
            lblConfigName.Size = new Size(101, 20);
            lblConfigName.TabIndex = 0;
            lblConfigName.Text = "Config Name";
            // 
            // txtConfigName
            // 
            txtConfigName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtConfigName.CausesValidation = false;
            txtConfigName.Font = new Font("Segoe UI", 10F);
            txtConfigName.Location = new Point(0, 26);
            txtConfigName.Margin = new Padding(0);
            txtConfigName.Name = "txtConfigName";
            txtConfigName.Size = new Size(307, 30);
            txtConfigName.TabIndex = 1;
            txtConfigName.Text = "Main Harness Test";
            // 
            // tblCellProductModel
            // 
            tblCellProductModel.AutoSize = true;
            tblCellProductModel.ColumnCount = 1;
            tblConfigInfo.SetColumnSpan(tblCellProductModel, 2);
            tblCellProductModel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblCellProductModel.Controls.Add(lblProductModel, 0, 0);
            tblCellProductModel.Controls.Add(txtProductModel, 0, 1);
            tblCellProductModel.Dock = DockStyle.Fill;
            tblCellProductModel.Location = new Point(343, 4);
            tblCellProductModel.Margin = new Padding(15, 4, 6, 8);
            tblCellProductModel.Name = "tblCellProductModel";
            tblCellProductModel.RowCount = 2;
            tblCellProductModel.RowStyles.Add(new RowStyle());
            tblCellProductModel.RowStyles.Add(new RowStyle());
            tblCellProductModel.Size = new Size(308, 56);
            tblCellProductModel.TabIndex = 1;
            // 
            // lblProductModel
            // 
            lblProductModel.AutoSize = true;
            lblProductModel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblProductModel.ForeColor = Color.FromArgb(100, 116, 139);
            lblProductModel.Location = new Point(0, 0);
            lblProductModel.Margin = new Padding(0, 0, 0, 6);
            lblProductModel.Name = "lblProductModel";
            lblProductModel.Size = new Size(112, 20);
            lblProductModel.TabIndex = 0;
            lblProductModel.Text = "Product Model";
            // 
            // 
            // txtProductModel
            // 
            txtProductModel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtProductModel.CausesValidation = false;
            txtProductModel.Font = new Font("Segoe UI", 10F);
            txtProductModel.Location = new Point(0, 26);
            txtProductModel.Margin = new Padding(0);
            txtProductModel.Name = "txtProductModel";
            txtProductModel.Size = new Size(308, 30);
            txtProductModel.TabIndex = 1;
            txtProductModel.Text = "6060";
            // 
            // tblCellConnRes
            // 
            tblCellConnRes.AutoSize = true;
            tblCellConnRes.ColumnCount = 1;
            tblConfigInfo.SetColumnSpan(tblCellConnRes, 2);
            tblCellConnRes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblCellConnRes.Controls.Add(lblConnectionResistance, 0, 0);
            tblCellConnRes.Controls.Add(txtConnectionResistance, 0, 1);
            tblCellConnRes.Dock = DockStyle.Fill;
            tblCellConnRes.Location = new Point(4, 70);
            tblCellConnRes.Margin = new Padding(4, 2, 9, 5);
            tblCellConnRes.Name = "tblCellConnRes";
            tblCellConnRes.RowCount = 2;
            tblCellConnRes.RowStyles.Add(new RowStyle());
            tblCellConnRes.RowStyles.Add(new RowStyle());
            tblCellConnRes.Size = new Size(315, 56);
            tblCellConnRes.TabIndex = 2;
            // 
            // lblConnectionResistance
            // 
            lblConnectionResistance.AutoSize = true;
            lblConnectionResistance.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblConnectionResistance.ForeColor = Color.FromArgb(100, 116, 139);
            lblConnectionResistance.Location = new Point(0, 0);
            lblConnectionResistance.Margin = new Padding(0, 0, 0, 6);
            lblConnectionResistance.Name = "lblConnectionResistance";
            lblConnectionResistance.Size = new Size(207, 20);
            lblConnectionResistance.TabIndex = 0;
            lblConnectionResistance.Text = "Connection Resistance (mΩ)";
            // 
            // txtConnectionResistance
            // 
            txtConnectionResistance.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtConnectionResistance.Font = new Font("Segoe UI", 10F);
            txtConnectionResistance.Location = new Point(0, 26);
            txtConnectionResistance.Margin = new Padding(0);
            txtConnectionResistance.Name = "txtConnectionResistance";
            txtConnectionResistance.Size = new Size(315, 30);
            txtConnectionResistance.TabIndex = 1;
            txtConnectionResistance.Text = "100";
            // 
            // tblCellCompRes
            // 
            tblConfigInfo.SetColumnSpan(tblCellCompRes, 2);
            tblCellCompRes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblCellCompRes.Controls.Add(lblComponentResistance, 0, 0);
            tblCellCompRes.Controls.Add(txtComponentResistance, 0, 1);
            tblCellCompRes.Dock = DockStyle.Fill;
            tblCellCompRes.Location = new Point(4, 136);
            tblCellCompRes.Margin = new Padding(4, 2, 9, 5);
            tblCellCompRes.Name = "tblCellCompRes";
            tblCellCompRes.RowCount = 2;
            tblCellCompRes.RowStyles.Add(new RowStyle());
            tblCellCompRes.RowStyles.Add(new RowStyle());
            tblCellCompRes.Size = new Size(315, 56);
            tblCellCompRes.TabIndex = 4;
            // 
            // lblComponentResistance
            // 
            lblComponentResistance.AutoSize = true;
            lblComponentResistance.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblComponentResistance.ForeColor = Color.FromArgb(100, 116, 139);
            lblComponentResistance.Location = new Point(0, 0);
            lblComponentResistance.Margin = new Padding(0, 0, 0, 6);
            lblComponentResistance.Name = "lblComponentResistance";
            lblComponentResistance.Size = new Size(207, 20);
            lblComponentResistance.TabIndex = 0;
            lblComponentResistance.Text = "Component Resistance (mΩ)";
            // 
            // txtComponentResistance
            // 
            txtComponentResistance.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtComponentResistance.Font = new Font("Segoe UI", 10F);
            txtComponentResistance.Location = new Point(0, 26);
            txtComponentResistance.Margin = new Padding(0);
            txtComponentResistance.Name = "txtComponentResistance";
            txtComponentResistance.Size = new Size(315, 30);
            txtComponentResistance.TabIndex = 1;
            txtComponentResistance.Text = "N/A";
            txtComponentResistance.ReadOnly = true;
            // 
            // tblCellNote
            //
            tblConfigInfo.SetColumnSpan(tblCellNote, 2);
            tblCellNote.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblCellNote.Controls.Add(lblNote, 0, 0);
            tblCellNote.Controls.Add(txtNote, 0, 1);
            tblCellNote.Dock = DockStyle.Fill;
            tblCellNote.Location = new Point(337, 136);
            tblCellNote.Margin = new Padding(9, 2, 4, 5);
            tblCellNote.Name = "tblCellNote";
            tblCellNote.RowCount = 2;
            tblCellNote.RowStyles.Add(new RowStyle());
            tblCellNote.RowStyles.Add(new RowStyle());
            tblCellNote.Size = new Size(316, 56);
            tblCellNote.TabIndex = 5;
            //
            // lblNote
            //
            lblNote.AutoSize = true;
            lblNote.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblNote.ForeColor = Color.FromArgb(100, 116, 139);
            lblNote.Location = new Point(0, 0);
            lblNote.Margin = new Padding(0, 0, 0, 6);
            lblNote.Name = "lblNote";
            lblNote.Size = new Size(207, 20);
            lblNote.TabIndex = 0;
            lblNote.Text = "Note";
            //
            // txtNote
            //
            txtNote.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtNote.CausesValidation = false;
            txtNote.Font = new Font("Segoe UI", 10F);
            txtNote.Location = new Point(0, 26);
            txtNote.Margin = new Padding(0);
            txtNote.Name = "txtNote";
            txtNote.PlaceholderText = "Optional note for this configuration";
            txtNote.Size = new Size(316, 30);
            txtNote.TabIndex = 1;
            //
            // tblCellInsRes
            //
            tblConfigInfo.SetColumnSpan(tblCellInsRes, 2);
            tblCellInsRes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblCellInsRes.Controls.Add(lblInsulationResistance, 0, 0);
            tblCellInsRes.Controls.Add(txtInsulationResistance, 0, 1);
            tblCellInsRes.Dock = DockStyle.Fill;
            tblCellInsRes.Location = new Point(337, 70);
            tblCellInsRes.Margin = new Padding(9, 2, 4, 5);
            tblCellInsRes.Name = "tblCellInsRes";
            tblCellInsRes.RowCount = 2;
            tblCellInsRes.RowStyles.Add(new RowStyle());
            tblCellInsRes.RowStyles.Add(new RowStyle());
            tblCellInsRes.Size = new Size(316, 56);
            tblCellInsRes.TabIndex = 3;
            // 
            // lblInsulationResistance
            // 
            lblInsulationResistance.AutoSize = true;
            lblInsulationResistance.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblInsulationResistance.ForeColor = Color.FromArgb(100, 116, 139);
            lblInsulationResistance.Location = new Point(0, 0);
            lblInsulationResistance.Margin = new Padding(0, 0, 0, 4);
            lblInsulationResistance.Name = "lblInsulationResistance";
            lblInsulationResistance.Size = new Size(198, 20);
            lblInsulationResistance.TabIndex = 0;
            lblInsulationResistance.Text = "Insulation Resistance (MΩ)";
            // 
            // txtInsulationResistance
            // 
            txtInsulationResistance.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtInsulationResistance.CausesValidation = false;
            txtInsulationResistance.Font = new Font("Segoe UI", 10F);
            txtInsulationResistance.Location = new Point(0, 25);
            txtInsulationResistance.Margin = new Padding(0);
            txtInsulationResistance.Name = "txtInsulationResistance";
            txtInsulationResistance.Size = new Size(316, 30);
            txtInsulationResistance.TabIndex = 1;
            txtInsulationResistance.Text = "500";
            // 
            // pnlConfigInfoHeader
            // 
            pnlConfigInfoHeader.BackColor = Color.FromArgb(203, 213, 225);
            pnlConfigInfoHeader.Controls.Add(lblConfigInfoHeader);
            pnlConfigInfoHeader.Dock = DockStyle.Top;
            pnlConfigInfoHeader.Location = new Point(0, 73);
            pnlConfigInfoHeader.Name = "pnlConfigInfoHeader";
            pnlConfigInfoHeader.Size = new Size(675, 25);
            pnlConfigInfoHeader.TabIndex = 2;
            // 
            // lblConfigInfoHeader
            // 
            lblConfigInfoHeader.Dock = DockStyle.Fill;
            lblConfigInfoHeader.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblConfigInfoHeader.ForeColor = Color.FromArgb(30, 41, 59);
            lblConfigInfoHeader.Location = new Point(0, 0);
            lblConfigInfoHeader.Name = "lblConfigInfoHeader";
            lblConfigInfoHeader.Padding = new Padding(6, 0, 0, 0);
            lblConfigInfoHeader.Size = new Size(675, 25);
            lblConfigInfoHeader.TabIndex = 0;
            lblConfigInfoHeader.Text = "Config Summary";
            lblConfigInfoHeader.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlButtons
            // 
            pnlButtons.BackColor = Color.FromArgb(248, 250, 252);
            pnlButtons.Controls.Add(tblButtons);
            pnlButtons.Dock = DockStyle.Top;
            pnlButtons.Location = new Point(0, 35);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(675, 58);
            pnlButtons.TabIndex = 1;
            // 
            // tblButtons
            // 
            tblButtons.BackColor = Color.FromArgb(248, 250, 252);
            tblButtons.ColumnCount = 2;
            tblButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblButtons.Controls.Add(flpButtonsLeft, 0, 0);
            tblButtons.Controls.Add(flpButtonsRight, 1, 0);
            tblButtons.Dock = DockStyle.Fill;
            tblButtons.Location = new Point(0, 0);
            tblButtons.Margin = new Padding(0);
            tblButtons.Name = "tblButtons";
            tblButtons.RowCount = 1;
            tblButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblButtons.Size = new Size(675, 58);
            tblButtons.TabIndex = 0;
            // 
            // flpButtonsLeft
            // 
            flpButtonsLeft.BackColor = Color.FromArgb(248, 250, 252);
            flpButtonsLeft.Controls.Add(btnTotalNet);
            flpButtonsLeft.Controls.Add(btnAdd);
            flpButtonsLeft.Controls.Add(btnRemove);
            flpButtonsLeft.Controls.Add(btnComplex);
            flpButtonsLeft.Dock = DockStyle.Fill;
            flpButtonsLeft.Location = new Point(0, 0);
            flpButtonsLeft.Margin = new Padding(0);
            flpButtonsLeft.Name = "flpButtonsLeft";
            flpButtonsLeft.Padding = new Padding(9, 6, 0, 18);
            flpButtonsLeft.Size = new Size(337, 58);
            flpButtonsLeft.TabIndex = 0;
            flpButtonsLeft.WrapContents = false;
            // 
            // btnTotalNet
            // 
            btnTotalNet.BackColor = Color.FromArgb(203, 213, 225);
            btnTotalNet.BorderRadius = 14;
            btnTotalNet.FlatAppearance.BorderSize = 0;
            btnTotalNet.FlatStyle = FlatStyle.Flat;
            btnTotalNet.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnTotalNet.ForeColor = Color.FromArgb(30, 41, 59);
            btnTotalNet.HoverBackColor = Color.FromArgb(203, 213, 225);
            btnTotalNet.HoverForeColor = Color.Empty;
            btnTotalNet.Location = new Point(9, 5);
            btnTotalNet.Margin = new Padding(0, 0, 6, 0);
            btnTotalNet.Name = "btnTotalNet";
            btnTotalNet.NormalBackColor = Color.FromArgb(203, 213, 225);
            btnTotalNet.PressedBackColor = Color.FromArgb(203, 213, 225);
            btnTotalNet.PressedForeColor = Color.Empty;
            btnTotalNet.Size = new Size(136, 28);
            btnTotalNet.TabIndex = 0;
            btnTotalNet.Text = "Total Net: 4";
            btnTotalNet.UseVisualStyleBackColor = false;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(34, 197, 94);
            btnAdd.BorderRadius = 14;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnAdd.ForeColor = Color.White;
            btnAdd.HoverBackColor = Color.FromArgb(22, 163, 74);
            btnAdd.HoverForeColor = Color.Empty;
            btnAdd.Location = new Point(151, 5);
            btnAdd.Margin = new Padding(0, 0, 6, 0);
            btnAdd.Name = "btnAdd";
            btnAdd.NormalBackColor = Color.FromArgb(34, 197, 94);
            btnAdd.PressedBackColor = Color.FromArgb(21, 128, 61);
            btnAdd.PressedForeColor = Color.Empty;
            btnAdd.Size = new Size(37, 28);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "+";
            btnAdd.TextAlign = ContentAlignment.TopCenter;
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnRemove
            // 
            btnRemove.BackColor = Color.FromArgb(239, 68, 68);
            btnRemove.BorderRadius = 14;
            btnRemove.FlatAppearance.BorderSize = 0;
            btnRemove.FlatStyle = FlatStyle.Flat;
            btnRemove.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnRemove.ForeColor = Color.White;
            btnRemove.HoverBackColor = Color.FromArgb(220, 38, 38);
            btnRemove.HoverForeColor = Color.Empty;
            btnRemove.Location = new Point(194, 5);
            btnRemove.Margin = new Padding(0);
            btnRemove.Name = "btnRemove";
            btnRemove.NormalBackColor = Color.FromArgb(239, 68, 68);
            btnRemove.PressedBackColor = Color.FromArgb(185, 28, 28);
            btnRemove.PressedForeColor = Color.Empty;
            btnRemove.Size = new Size(36, 28);
            btnRemove.TabIndex = 2;
            btnRemove.Text = "-";
            btnRemove.UseVisualStyleBackColor = false;
            // 
            // flpButtonsRight
            // 
            flpButtonsRight.BackColor = Color.FromArgb(248, 250, 252);
            flpButtonsRight.Controls.Add(btnExport);
            flpButtonsRight.Controls.Add(btnReset);
            flpButtonsRight.Controls.Add(btnImport);
            flpButtonsRight.Controls.Add(btnImportExcel);
            flpButtonsRight.Controls.Add(btnTemplate);
            flpButtonsRight.Dock = DockStyle.Fill;
            flpButtonsRight.FlowDirection = FlowDirection.RightToLeft;
            flpButtonsRight.Location = new Point(337, 0);
            flpButtonsRight.Margin = new Padding(0);
            flpButtonsRight.Name = "flpButtonsRight";
            flpButtonsRight.Padding = new Padding(0, 6, 9, 18);
            flpButtonsRight.WrapContents = false;
            flpButtonsRight.Size = new Size(338, 58);
            flpButtonsRight.TabIndex = 1;
            flpButtonsRight.WrapContents = false;
            // 
            // btnExport
            // 
            btnExport.BackColor = Color.FromArgb(203, 213, 225);
            btnExport.BorderRadius = 14;
            btnExport.FlatAppearance.BorderSize = 0;
            btnExport.FlatStyle = FlatStyle.Flat;
            btnExport.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnExport.ForeColor = Color.FromArgb(30, 41, 59);
            btnExport.HoverBackColor = Color.FromArgb(148, 163, 184);
            btnExport.HoverForeColor = Color.Empty;
            btnExport.Margin = new Padding(6, 0, 0, 0);
            btnExport.Name = "btnExport";
            btnExport.NormalBackColor = Color.FromArgb(203, 213, 225);
            btnExport.PressedBackColor = Color.FromArgb(100, 116, 139);
            btnExport.PressedForeColor = Color.Empty;
            btnExport.AutoSize = true;
            btnExport.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnExport.Padding = new Padding(14, 4, 14, 4);
            btnExport.TabIndex = 5;
            btnExport.Text = "Save";
            btnExport.UseVisualStyleBackColor = false;
            // 
            // btnReset
            // 
            btnReset.BackColor = Color.FromArgb(203, 213, 225);
            btnReset.BorderRadius = 14;
            btnReset.FlatAppearance.BorderSize = 0;
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnReset.ForeColor = Color.FromArgb(220, 38, 38);
            btnReset.HoverBackColor = Color.FromArgb(254, 226, 226);
            btnReset.HoverForeColor = Color.Empty;
            btnReset.Margin = new Padding(6, 0, 0, 0);
            btnReset.Name = "btnReset";
            btnReset.NormalBackColor = Color.FromArgb(203, 213, 225);
            btnReset.PressedBackColor = Color.FromArgb(254, 202, 202);
            btnReset.PressedForeColor = Color.Empty;
            btnReset.AutoSize = true;
            btnReset.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnReset.Padding = new Padding(14, 4, 14, 4);
            btnReset.TabIndex = 4;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = false;
            // 
            // btnImport
            // 
            btnImport.BackColor = Color.FromArgb(203, 213, 225);
            btnImport.BorderRadius = 14;
            btnImport.FlatAppearance.BorderSize = 0;
            btnImport.FlatStyle = FlatStyle.Flat;
            btnImport.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnImport.ForeColor = Color.FromArgb(30, 41, 59);
            btnImport.HoverBackColor = Color.FromArgb(148, 163, 184);
            btnImport.HoverForeColor = Color.Empty;
            btnImport.Margin = new Padding(6, 0, 0, 0);
            btnImport.Name = "btnImport";
            btnImport.NormalBackColor = Color.FromArgb(203, 213, 225);
            btnImport.PressedBackColor = Color.FromArgb(100, 116, 139);
            btnImport.PressedForeColor = Color.Empty;
            btnImport.AutoSize = true;
            btnImport.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnImport.Padding = new Padding(14, 4, 14, 4);
            btnImport.TabIndex = 3;
            btnImport.Text = "Edit";
            //
            // btnImportExcel
            //
            btnImportExcel.BackColor = Color.FromArgb(203, 213, 225);
            btnImportExcel.BorderRadius = 14;
            btnImportExcel.FlatAppearance.BorderSize = 0;
            btnImportExcel.FlatStyle = FlatStyle.Flat;
            btnImportExcel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnImportExcel.ForeColor = Color.FromArgb(30, 41, 59);
            btnImportExcel.HoverBackColor = Color.FromArgb(148, 163, 184);
            btnImportExcel.HoverForeColor = Color.Empty;
            btnImportExcel.Margin = new Padding(6, 0, 0, 0);
            btnImportExcel.Name = "btnImportExcel";
            btnImportExcel.NormalBackColor = Color.FromArgb(203, 213, 225);
            btnImportExcel.PressedBackColor = Color.FromArgb(100, 116, 139);
            btnImportExcel.PressedForeColor = Color.Empty;
            btnImportExcel.AutoSize = true;
            btnImportExcel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnImportExcel.Padding = new Padding(14, 4, 14, 4);
            btnImportExcel.TabIndex = 4;
            btnImportExcel.Text = "Import";
            //
            // btnTemplate
            //
            btnTemplate.BackColor = Color.FromArgb(203, 213, 225);
            btnTemplate.BorderRadius = 14;
            btnTemplate.FlatAppearance.BorderSize = 0;
            btnTemplate.FlatStyle = FlatStyle.Flat;
            btnTemplate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnTemplate.ForeColor = Color.FromArgb(30, 41, 59);
            btnTemplate.HoverBackColor = Color.FromArgb(148, 163, 184);
            btnTemplate.HoverForeColor = Color.Empty;
            btnTemplate.Margin = new Padding(6, 0, 0, 0);
            btnTemplate.Name = "btnTemplate";
            btnTemplate.NormalBackColor = Color.FromArgb(203, 213, 225);
            btnTemplate.PressedBackColor = Color.FromArgb(100, 116, 139);
            btnTemplate.PressedForeColor = Color.Empty;
            btnTemplate.AutoSize = true;
            btnTemplate.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnTemplate.Padding = new Padding(14, 4, 14, 4);
            btnTemplate.TabIndex = 5;
            btnTemplate.Text = "Template";
            btnImport.UseVisualStyleBackColor = false;
            // 
            // pnlHeaderTop
            // 
            pnlHeaderTop.BackColor = Color.FromArgb(203, 213, 225);
            pnlHeaderTop.Controls.Add(lblHeaderTop);
            pnlHeaderTop.Dock = DockStyle.Top;
            pnlHeaderTop.Location = new Point(0, 0);
            pnlHeaderTop.Name = "pnlHeaderTop";
            pnlHeaderTop.Size = new Size(675, 35);
            pnlHeaderTop.TabIndex = 0;
            // 
            // lblHeaderTop
            // 
            lblHeaderTop.Dock = DockStyle.Fill;
            lblHeaderTop.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblHeaderTop.ForeColor = Color.FromArgb(30, 41, 59);
            lblHeaderTop.Location = new Point(0, 0);
            lblHeaderTop.Name = "lblHeaderTop";
            lblHeaderTop.Size = new Size(675, 35);
            lblHeaderTop.TabIndex = 0;
            lblHeaderTop.Text = "Net List & Harness Configuration";
            lblHeaderTop.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlRightBox
            // 
            pnlRightBox.BackColor = Color.White;
            pnlRightBox.BorderStyle = BorderStyle.FixedSingle;
            pnlRightBox.Controls.Add(pnlPreviewScroll);
            pnlRightBox.Controls.Add(pnlHeaderBottom);
            pnlRightBox.Dock = DockStyle.Fill;
            pnlRightBox.Location = new Point(0, 0);
            pnlRightBox.Margin = new Padding(0);
            pnlRightBox.Name = "pnlRightBox";
            pnlRightBox.Size = new Size(557, 832);
            pnlRightBox.TabIndex = 1;
            // 
            // pnlPreviewScroll
            // 
            pnlPreviewScroll.AutoScroll = true;
            pnlPreviewScroll.BackColor = Color.FromArgb(248, 250, 252);
            pnlPreviewScroll.Controls.Add(picPreview);
            pnlPreviewScroll.Dock = DockStyle.Fill;
            pnlPreviewScroll.Location = new Point(0, 35);
            pnlPreviewScroll.Name = "pnlPreviewScroll";
            pnlPreviewScroll.Size = new Size(555, 795);
            pnlPreviewScroll.TabIndex = 1;
            // 
            // picPreview
            // 
            picPreview.BackColor = Color.FromArgb(248, 250, 252);
            picPreview.Location = new Point(0, 0);
            picPreview.Name = "picPreview";
            picPreview.Size = new Size(551, 781);
            picPreview.TabIndex = 1;
            picPreview.TabStop = false;
            // 
            // pnlHeaderBottom
            // 
            pnlHeaderBottom.BackColor = Color.FromArgb(203, 213, 225);
            pnlHeaderBottom.Controls.Add(lblHeaderBottom);
            pnlHeaderBottom.Dock = DockStyle.Top;
            pnlHeaderBottom.Location = new Point(0, 0);
            pnlHeaderBottom.Name = "pnlHeaderBottom";
            pnlHeaderBottom.Size = new Size(555, 35);
            pnlHeaderBottom.TabIndex = 0;
            // 
            // lblHeaderBottom
            // 
            lblHeaderBottom.Dock = DockStyle.Fill;
            lblHeaderBottom.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblHeaderBottom.ForeColor = Color.FromArgb(30, 41, 59);
            lblHeaderBottom.Location = new Point(0, 0);
            lblHeaderBottom.Name = "lblHeaderBottom";
            lblHeaderBottom.Size = new Size(555, 35);
            lblHeaderBottom.TabIndex = 0;
            lblHeaderBottom.Text = "Live Schematic Diagram Preview";
            lblHeaderBottom.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnComplex
            // 
            btnComplex.Location = new Point(233, 8);
            btnComplex.Name = "btnComplex";
            btnComplex.Size = new Size(94, 29);
            btnComplex.TabIndex = 3;
            btnComplex.Text = "Temporary";
            btnComplex.UseVisualStyleBackColor = true;
            btnComplex.Click += btnComplex_Click;
            // 
            // TestParametersPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
            Controls.Add(splitContainerMain);
            Margin = new Padding(0);
            Name = "TestParametersPage";
            Padding = new Padding(9);
            Size = new Size(1259, 850);
            splitContainerMain.Panel1.ResumeLayout(false);
            splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).EndInit();
            splitContainerMain.ResumeLayout(false);
            pnlLeftBox.ResumeLayout(false);
            pnlLeftBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridConfig).EndInit();
            pnlConfigInfo.ResumeLayout(false);
            pnlConfigInfo.PerformLayout();
            tblConfigInfo.ResumeLayout(false);
            tblConfigInfo.PerformLayout();
            tblCellConfigName.ResumeLayout(false);
            tblCellConfigName.PerformLayout();
            tblCellProductModel.ResumeLayout(false);
            tblCellProductModel.PerformLayout();
            tblCellConnRes.ResumeLayout(false);
            tblCellConnRes.PerformLayout();
            tblCellCompRes.ResumeLayout(false);
            tblCellCompRes.PerformLayout();
            tblCellNote.ResumeLayout(false);
            tblCellNote.PerformLayout();
            tblCellInsRes.ResumeLayout(false);
            tblCellInsRes.PerformLayout();
            pnlConfigInfoHeader.ResumeLayout(false);
            pnlButtons.ResumeLayout(false);
            tblButtons.ResumeLayout(false);
            flpButtonsLeft.ResumeLayout(false);
            flpButtonsRight.ResumeLayout(false);
            pnlHeaderTop.ResumeLayout(false);
            pnlRightBox.ResumeLayout(false);
            pnlPreviewScroll.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picPreview).EndInit();
            pnlHeaderBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private AssemblyLineTester.Views.Controls.RoundedButton btnLearnConnection;
        private AssemblyLineTester.Views.Controls.RoundedButton btnStartAll;
        private AssemblyLineTester.Views.Controls.RoundedButton btnStartSelected;
        private AssemblyLineTester.Views.Controls.RoundedButton btnAbort;

        private SplitContainer splitContainerMain;
        private Panel pnlLeftBox;
        private DataGridView gridConfig;
        private DataGridViewTextBoxColumn colNetId;
        private DataGridViewTextBoxColumn colHarnessGroup;
        private DataGridViewTextBoxColumn colColor;
        private DataGridViewTextBoxColumn colGauge;
        private DataGridViewTextBoxColumn colFromLabel;
        private DataGridViewTextBoxColumn colToLabel;
        private DataGridViewComboBoxColumn colTestType;
        private DataGridViewTextBoxColumn colLowerLimit;
        private DataGridViewTextBoxColumn colUpperLimit;
        private DataGridViewTextBoxColumn colExpectedValue;
        private DataGridViewTextBoxColumn colTolerance;
        private Panel pnlConfigInfo;
        private TableLayoutPanel tblConfigInfo;
        private TableLayoutPanel tblCellConfigName;
        private TableLayoutPanel tblCellProductModel;
        private TableLayoutPanel tblCellConnRes;
        private TableLayoutPanel tblCellCompRes;
        private TableLayoutPanel tblCellNote;
        private TableLayoutPanel tblCellInsRes;
        private Label lblConfigName;
        private Label lblProductModel;
        private Label lblConnectionResistance;
        private Label lblComponentResistance;
        private Label lblNote;
        private Label lblInsulationResistance;
        internal TextBox txtConfigName;
        internal ComboBox txtProductModel;
        internal TextBox txtConnectionResistance;
        internal TextBox txtComponentResistance;
        internal TextBox txtNote;
        internal TextBox txtInsulationResistance;
        private Panel pnlConfigInfoHeader;
        private Label lblConfigInfoHeader;
        private Panel pnlButtons;
        private TableLayoutPanel tblButtons;
        private FlowLayoutPanel flpButtonsLeft;
        private FlowLayoutPanel flpButtonsRight;
        private AssemblyLineTester.Views.Controls.RoundedButton btnExport;
        private AssemblyLineTester.Views.Controls.RoundedButton btnReset;
        private AssemblyLineTester.Views.Controls.RoundedButton btnImport;
        private AssemblyLineTester.Views.Controls.RoundedButton btnImportExcel;
        private AssemblyLineTester.Views.Controls.RoundedButton btnTemplate;
        private AssemblyLineTester.Views.Controls.RoundedButton btnRemove;
        private AssemblyLineTester.Views.Controls.RoundedButton btnAdd;
        private AssemblyLineTester.Views.Controls.RoundedButton btnTotalNet;
        private Panel pnlHeaderTop;
        private Label lblHeaderTop;
        private Panel pnlRightBox;
        private Panel pnlPreviewScroll;
        private PictureBox picPreview;
        private Panel pnlHeaderBottom;
        private Label lblHeaderBottom;
        private DataGridViewTextBoxColumn colNo;
        private DataGridViewTextBoxColumn colFromAddr;
        private DataGridViewTextBoxColumn colToAddr;
        private DataGridViewTextBoxColumn colUnit;
        private Button btnComplex;
    }
}