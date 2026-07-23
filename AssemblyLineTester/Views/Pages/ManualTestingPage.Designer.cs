namespace AssemblyLineTester.Views.Pages
{
    partial class ManualTestingPage
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
            btnStart = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnStartSelected = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnAbort = new AssemblyLineTester.Views.Controls.RoundedButton();
            splitContainerMain = new SplitContainer();
            pnlLeftBox = new Panel();
            gridConfig = new DataGridView();
            colNo = new DataGridViewTextBoxColumn();
            colFromAddr = new DataGridViewTextBoxColumn();
            colToAddr = new DataGridViewTextBoxColumn();
            colUnit = new DataGridViewComboBoxColumn();
            pnlConfigInfo = new Panel();
            tblConfigInfo = new TableLayoutPanel();
            tblCellConnRes = new TableLayoutPanel();
            lblConnectionResistance = new Label();
            txtConnectionResistance = new TextBox();
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
            pnlHeaderTop = new Panel();
            lblHeaderTop = new Label();
            pnlRightBox = new Panel();
            pnlPreviewScroll = new Panel();
            picPreview = new PictureBox();
            pnlHeaderBottom = new Panel();
            lblHeaderBottom = new Label();
            colNetId = new DataGridViewTextBoxColumn();
            colHarnessGroup = new DataGridViewTextBoxColumn();
            colFromLabel = new DataGridViewTextBoxColumn();
            colToLabel = new DataGridViewTextBoxColumn();
            colTestType = new DataGridViewComboBoxColumn();
            colLowerLimit = new DataGridViewTextBoxColumn();
            colUpperLimit = new DataGridViewTextBoxColumn();
            colExpectedValue = new DataGridViewTextBoxColumn();
            colTolerance = new DataGridViewTextBoxColumn();
            tblCellConfigName = new TableLayoutPanel();
            lblConfigName = new Label();
            txtConfigName = new TextBox();
            tblCellProductModel = new TableLayoutPanel();
            lblProductModel = new Label();
            txtProductModel = new TextBox();
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
            splitContainerMain.Panel1.SuspendLayout();
            splitContainerMain.Panel2.SuspendLayout();
            splitContainerMain.SuspendLayout();
            pnlLeftBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridConfig).BeginInit();
            pnlConfigInfo.SuspendLayout();
            tblConfigInfo.SuspendLayout();
            tblCellConnRes.SuspendLayout();
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
            tblCellConfigName.SuspendLayout();
            tblCellProductModel.SuspendLayout();
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
            btnLearnConnection.ForeColor = Color.FromArgb(30, 41, 59);
            btnLearnConnection.HoverBackColor = Color.FromArgb(148, 163, 184);
            btnLearnConnection.HoverForeColor = Color.Empty;
            btnLearnConnection.Location = new Point(20, 10);
            btnLearnConnection.Margin = new Padding(20, 10, 20, 10);
            btnLearnConnection.Name = "btnLearnConnection";
            btnLearnConnection.NormalBackColor = Color.FromArgb(203, 213, 225);
            btnLearnConnection.PressedBackColor = Color.FromArgb(100, 116, 139);
            btnLearnConnection.PressedForeColor = Color.Empty;
            btnLearnConnection.Size = new Size(227, 48);
            btnLearnConnection.TabIndex = 0;
            btnLearnConnection.Text = "Learn Connection";
            btnLearnConnection.UseVisualStyleBackColor = false;
            // 
            // btnStart
            // 
            btnStart.BackColor = Color.FromArgb(34, 197, 94);
            btnStart.BorderRadius = 14;
            btnStart.Dock = DockStyle.Fill;
            btnStart.FlatAppearance.BorderSize = 0;
            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnStart.ForeColor = Color.White;
            btnStart.HoverBackColor = Color.FromArgb(22, 163, 74);
            btnStart.HoverForeColor = Color.Empty;
            btnStart.Location = new Point(287, 10);
            btnStart.Margin = new Padding(20, 10, 20, 10);
            btnStart.Name = "btnStart";
            btnStart.NormalBackColor = Color.FromArgb(34, 197, 94);
            btnStart.PressedBackColor = Color.FromArgb(21, 128, 61);
            btnStart.PressedForeColor = Color.Empty;
            btnStart.Size = new Size(227, 48);
            btnStart.TabIndex = 1;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = false;
            // 
            // btnStartSelected
            // 
            btnStartSelected.BackColor = Color.FromArgb(245, 158, 11);
            btnStartSelected.BorderRadius = 14;
            btnStartSelected.Dock = DockStyle.Fill;
            btnStartSelected.FlatAppearance.BorderSize = 0;
            btnStartSelected.FlatStyle = FlatStyle.Flat;
            btnStartSelected.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnStartSelected.ForeColor = Color.White;
            btnStartSelected.HoverBackColor = Color.FromArgb(217, 119, 6);
            btnStartSelected.HoverForeColor = Color.Empty;
            btnStartSelected.Location = new Point(554, 10);
            btnStartSelected.Margin = new Padding(20, 10, 20, 10);
            btnStartSelected.Name = "btnStartSelected";
            btnStartSelected.NormalBackColor = Color.FromArgb(245, 158, 11);
            btnStartSelected.PressedBackColor = Color.FromArgb(180, 83, 9);
            btnStartSelected.PressedForeColor = Color.Empty;
            btnStartSelected.Size = new Size(227, 48);
            btnStartSelected.TabIndex = 2;
            btnStartSelected.Text = "Start Selected";
            btnStartSelected.UseVisualStyleBackColor = false;
            // 
            // btnAbort
            // 
            btnAbort.BackColor = Color.FromArgb(239, 68, 68);
            btnAbort.BorderRadius = 14;
            btnAbort.Dock = DockStyle.Fill;
            btnAbort.FlatAppearance.BorderSize = 0;
            btnAbort.FlatStyle = FlatStyle.Flat;
            btnAbort.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAbort.ForeColor = Color.White;
            btnAbort.HoverBackColor = Color.FromArgb(220, 38, 38);
            btnAbort.HoverForeColor = Color.Empty;
            btnAbort.Location = new Point(821, 10);
            btnAbort.Margin = new Padding(20, 10, 20, 10);
            btnAbort.Name = "btnAbort";
            btnAbort.NormalBackColor = Color.FromArgb(239, 68, 68);
            btnAbort.PressedBackColor = Color.FromArgb(185, 28, 28);
            btnAbort.PressedForeColor = Color.Empty;
            btnAbort.Size = new Size(227, 48);
            btnAbort.TabIndex = 3;
            btnAbort.Text = "Abort";
            btnAbort.UseVisualStyleBackColor = false;
            // 
            // splitContainerMain
            // 
            splitContainerMain.BackColor = Color.FromArgb(203, 213, 225);
            splitContainerMain.Dock = DockStyle.Fill;
            splitContainerMain.IsSplitterFixed = true;
            splitContainerMain.Location = new Point(15, 15);
            splitContainerMain.Margin = new Padding(5);
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
            splitContainerMain.Size = new Size(2016, 1330);
            splitContainerMain.SplitterDistance = 1100;
            splitContainerMain.SplitterWidth = 12;
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
            pnlLeftBox.Size = new Size(1100, 1330);
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
            gridConfig.Columns.AddRange(new DataGridViewColumn[] { colNo, colFromAddr, colToAddr, colUnit });
            gridConfig.Dock = DockStyle.Fill;
            gridConfig.EnableHeadersVisualStyles = false;
            gridConfig.Location = new Point(0, 337);
            gridConfig.Margin = new Padding(5);
            gridConfig.Name = "gridConfig";
            gridConfig.RowHeadersVisible = false;
            gridConfig.RowHeadersWidth = 82;
            gridConfig.RowTemplate.Height = 38;
            gridConfig.Size = new Size(1098, 991);
            gridConfig.TabIndex = 4;
            // 
            // colNo
            // 
            colNo.HeaderText = "No.";
            colNo.MinimumWidth = 50;
            colNo.Name = "colNo";
            colNo.ReadOnly = true;
            // 
            // colFromAddr
            // 
            colFromAddr.HeaderText = "From";
            colFromAddr.MinimumWidth = 90;
            colFromAddr.Name = "colFromAddr";
            // 
            // colToAddr
            // 
            colToAddr.HeaderText = "To";
            colToAddr.MinimumWidth = 90;
            colToAddr.Name = "colToAddr";
            // 
            // colUnit
            // 
            colUnit.FlatStyle = FlatStyle.Flat;
            colUnit.HeaderText = "Measurement";
            colUnit.Items.AddRange(new object[] { "Ω", "mΩ", "kΩ", "MΩ", "GΩ", "F", "μF", "nF", "pF", "V", "mV", "kV", "A", "mA", "Hz", "ms" });
            colUnit.MinimumWidth = 110;
            colUnit.Name = "colUnit";
            // 
            // pnlConfigInfo
            // 
            pnlConfigInfo.AutoSize = true;
            pnlConfigInfo.BackColor = Color.White;
            pnlConfigInfo.CausesValidation = false;
            pnlConfigInfo.Controls.Add(tblConfigInfo);
            pnlConfigInfo.Dock = DockStyle.Top;
            pnlConfigInfo.Location = new Point(0, 156);
            pnlConfigInfo.Margin = new Padding(5);
            pnlConfigInfo.Name = "pnlConfigInfo";
            pnlConfigInfo.Padding = new Padding(15, 10, 15, 10);
            pnlConfigInfo.Size = new Size(1098, 181);
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
            tblConfigInfo.Controls.Add(btnLearnConnection, 0, 0);
            tblConfigInfo.Controls.Add(btnStart, 1, 0);
            tblConfigInfo.Controls.Add(btnStartSelected, 2, 0);
            tblConfigInfo.Controls.Add(btnAbort, 3, 0);
            tblConfigInfo.Controls.Add(tblCellConnRes, 0, 1);
            tblConfigInfo.Controls.Add(tblCellInsRes, 2, 1);
            tblConfigInfo.Dock = DockStyle.Fill;
            tblConfigInfo.Location = new Point(15, 10);
            tblConfigInfo.Margin = new Padding(0);
            tblConfigInfo.Name = "tblConfigInfo";
            tblConfigInfo.RowCount = 2;
            tblConfigInfo.RowStyles.Add(new RowStyle());
            tblConfigInfo.RowStyles.Add(new RowStyle());
            tblConfigInfo.Size = new Size(1068, 161);
            tblConfigInfo.TabIndex = 0;
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
            tblCellConnRes.Location = new Point(6, 72);
            tblCellConnRes.Margin = new Padding(6, 4, 15, 8);
            tblCellConnRes.Name = "tblCellConnRes";
            tblCellConnRes.RowCount = 2;
            tblCellConnRes.RowStyles.Add(new RowStyle());
            tblCellConnRes.RowStyles.Add(new RowStyle());
            tblCellConnRes.Size = new Size(513, 81);
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
            lblConnectionResistance.Size = new Size(336, 32);
            lblConnectionResistance.TabIndex = 0;
            lblConnectionResistance.Text = "Connection Resistance (mΩ)";
            // 
            // txtConnectionResistance
            // 
            txtConnectionResistance.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtConnectionResistance.CausesValidation = false;
            txtConnectionResistance.Font = new Font("Segoe UI", 10F);
            txtConnectionResistance.Location = new Point(0, 38);
            txtConnectionResistance.Margin = new Padding(0);
            txtConnectionResistance.Name = "txtConnectionResistance";
            txtConnectionResistance.Size = new Size(513, 43);
            txtConnectionResistance.TabIndex = 1;
            txtConnectionResistance.Text = "2.5";
            // 
            // tblCellInsRes
            // 
            tblCellInsRes.AutoSize = true;
            tblCellInsRes.ColumnCount = 1;
            tblConfigInfo.SetColumnSpan(tblCellInsRes, 2);
            tblCellInsRes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblCellInsRes.Controls.Add(lblInsulationResistance, 0, 0);
            tblCellInsRes.Controls.Add(txtInsulationResistance, 0, 1);
            tblCellInsRes.Dock = DockStyle.Fill;
            tblCellInsRes.Location = new Point(549, 72);
            tblCellInsRes.Margin = new Padding(15, 4, 6, 8);
            tblCellInsRes.Name = "tblCellInsRes";
            tblCellInsRes.RowCount = 2;
            tblCellInsRes.RowStyles.Add(new RowStyle());
            tblCellInsRes.RowStyles.Add(new RowStyle());
            tblCellInsRes.Size = new Size(513, 81);
            tblCellInsRes.TabIndex = 3;
            // 
            // lblInsulationResistance
            // 
            lblInsulationResistance.AutoSize = true;
            lblInsulationResistance.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblInsulationResistance.ForeColor = Color.FromArgb(100, 116, 139);
            lblInsulationResistance.Location = new Point(0, 0);
            lblInsulationResistance.Margin = new Padding(0, 0, 0, 6);
            lblInsulationResistance.Name = "lblInsulationResistance";
            lblInsulationResistance.Size = new Size(321, 32);
            lblInsulationResistance.TabIndex = 0;
            lblInsulationResistance.Text = "Insulation Resistance (MΩ)";
            // 
            // txtInsulationResistance
            // 
            txtInsulationResistance.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtInsulationResistance.CausesValidation = false;
            txtInsulationResistance.Font = new Font("Segoe UI", 10F);
            txtInsulationResistance.Location = new Point(0, 38);
            txtInsulationResistance.Margin = new Padding(0);
            txtInsulationResistance.Name = "txtInsulationResistance";
            txtInsulationResistance.Size = new Size(513, 43);
            txtInsulationResistance.TabIndex = 1;
            txtInsulationResistance.Text = "500";
            // 
            // pnlConfigInfoHeader
            // 
            pnlConfigInfoHeader.BackColor = Color.FromArgb(203, 213, 225);
            pnlConfigInfoHeader.Controls.Add(lblConfigInfoHeader);
            pnlConfigInfoHeader.Dock = DockStyle.Top;
            pnlConfigInfoHeader.Location = new Point(0, 116);
            pnlConfigInfoHeader.Margin = new Padding(5);
            pnlConfigInfoHeader.Name = "pnlConfigInfoHeader";
            pnlConfigInfoHeader.Size = new Size(1098, 40);
            pnlConfigInfoHeader.TabIndex = 2;
            // 
            // lblConfigInfoHeader
            // 
            lblConfigInfoHeader.Dock = DockStyle.Fill;
            lblConfigInfoHeader.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblConfigInfoHeader.ForeColor = Color.FromArgb(30, 41, 59);
            lblConfigInfoHeader.Location = new Point(0, 0);
            lblConfigInfoHeader.Margin = new Padding(5, 0, 5, 0);
            lblConfigInfoHeader.Name = "lblConfigInfoHeader";
            lblConfigInfoHeader.Padding = new Padding(10, 0, 0, 0);
            lblConfigInfoHeader.Size = new Size(1098, 40);
            lblConfigInfoHeader.TabIndex = 0;
            lblConfigInfoHeader.Text = "Config Summary";
            lblConfigInfoHeader.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlButtons
            // 
            pnlButtons.BackColor = Color.FromArgb(248, 250, 252);
            pnlButtons.Controls.Add(tblButtons);
            pnlButtons.Dock = DockStyle.Top;
            pnlButtons.Location = new Point(0, 56);
            pnlButtons.Margin = new Padding(5);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(1098, 60);
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
            tblButtons.Size = new Size(1098, 60);
            tblButtons.TabIndex = 0;
            // 
            // flpButtonsLeft
            // 
            flpButtonsLeft.BackColor = Color.FromArgb(248, 250, 252);
            flpButtonsLeft.Controls.Add(btnTotalNet);
            flpButtonsLeft.Controls.Add(btnAdd);
            flpButtonsLeft.Controls.Add(btnRemove);
            flpButtonsLeft.Dock = DockStyle.Fill;
            flpButtonsLeft.Location = new Point(0, 0);
            flpButtonsLeft.Margin = new Padding(0);
            flpButtonsLeft.Name = "flpButtonsLeft";
            flpButtonsLeft.Padding = new Padding(15, 8, 0, 8);
            flpButtonsLeft.Size = new Size(549, 60);
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
            btnTotalNet.Location = new Point(15, 8);
            btnTotalNet.Margin = new Padding(0, 0, 10, 0);
            btnTotalNet.Name = "btnTotalNet";
            btnTotalNet.NormalBackColor = Color.FromArgb(203, 213, 225);
            btnTotalNet.PressedBackColor = Color.FromArgb(203, 213, 225);
            btnTotalNet.PressedForeColor = Color.Empty;
            btnTotalNet.Size = new Size(221, 44);
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
            btnAdd.Location = new Point(246, 8);
            btnAdd.Margin = new Padding(0, 0, 10, 0);
            btnAdd.Name = "btnAdd";
            btnAdd.NormalBackColor = Color.FromArgb(34, 197, 94);
            btnAdd.PressedBackColor = Color.FromArgb(21, 128, 61);
            btnAdd.PressedForeColor = Color.Empty;
            btnAdd.Size = new Size(60, 44);
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
            btnRemove.Location = new Point(316, 8);
            btnRemove.Margin = new Padding(0);
            btnRemove.Name = "btnRemove";
            btnRemove.NormalBackColor = Color.FromArgb(239, 68, 68);
            btnRemove.PressedBackColor = Color.FromArgb(185, 28, 28);
            btnRemove.PressedForeColor = Color.Empty;
            btnRemove.Size = new Size(59, 44);
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
            flpButtonsRight.Dock = DockStyle.Fill;
            flpButtonsRight.FlowDirection = FlowDirection.RightToLeft;
            flpButtonsRight.Location = new Point(549, 0);
            flpButtonsRight.Margin = new Padding(0);
            flpButtonsRight.Name = "flpButtonsRight";
            flpButtonsRight.Padding = new Padding(0, 8, 15, 8);
            flpButtonsRight.Size = new Size(549, 60);
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
            btnExport.Location = new Point(351, 8);
            btnExport.Margin = new Padding(10, 0, 0, 0);
            btnExport.Name = "btnExport";
            btnExport.NormalBackColor = Color.FromArgb(203, 213, 225);
            btnExport.PressedBackColor = Color.FromArgb(100, 116, 139);
            btnExport.PressedForeColor = Color.Empty;
            btnExport.Size = new Size(183, 44);
            btnExport.TabIndex = 5;
            btnExport.Text = "Save Config";
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
            btnReset.Location = new Point(173, 8);
            btnReset.Margin = new Padding(10, 0, 0, 0);
            btnReset.Name = "btnReset";
            btnReset.NormalBackColor = Color.FromArgb(203, 213, 225);
            btnReset.PressedBackColor = Color.FromArgb(254, 202, 202);
            btnReset.PressedForeColor = Color.Empty;
            btnReset.Size = new Size(168, 44);
            btnReset.TabIndex = 4;
            btnReset.Text = "Reset Config";
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
            btnImport.Location = new Point(-21, 8);
            btnImport.Margin = new Padding(0);
            btnImport.Name = "btnImport";
            btnImport.NormalBackColor = Color.FromArgb(203, 213, 225);
            btnImport.PressedBackColor = Color.FromArgb(100, 116, 139);
            btnImport.PressedForeColor = Color.Empty;
            btnImport.Size = new Size(184, 44);
            btnImport.TabIndex = 3;
            btnImport.Text = "Edit Config";
            btnImport.UseVisualStyleBackColor = false;
            // 
            // pnlHeaderTop
            // 
            pnlHeaderTop.BackColor = Color.FromArgb(203, 213, 225);
            pnlHeaderTop.Controls.Add(lblHeaderTop);
            pnlHeaderTop.Dock = DockStyle.Top;
            pnlHeaderTop.Location = new Point(0, 0);
            pnlHeaderTop.Margin = new Padding(5);
            pnlHeaderTop.Name = "pnlHeaderTop";
            pnlHeaderTop.Size = new Size(1098, 56);
            pnlHeaderTop.TabIndex = 0;
            // 
            // lblHeaderTop
            // 
            lblHeaderTop.Dock = DockStyle.Fill;
            lblHeaderTop.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblHeaderTop.ForeColor = Color.FromArgb(30, 41, 59);
            lblHeaderTop.Location = new Point(0, 0);
            lblHeaderTop.Margin = new Padding(5, 0, 5, 0);
            lblHeaderTop.Name = "lblHeaderTop";
            lblHeaderTop.Size = new Size(1098, 56);
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
            pnlRightBox.Size = new Size(904, 1330);
            pnlRightBox.TabIndex = 1;
            // 
            // pnlPreviewScroll
            // 
            pnlPreviewScroll.AutoScroll = true;
            pnlPreviewScroll.BackColor = Color.FromArgb(248, 250, 252);
            pnlPreviewScroll.Controls.Add(picPreview);
            pnlPreviewScroll.Dock = DockStyle.Fill;
            pnlPreviewScroll.Location = new Point(0, 56);
            pnlPreviewScroll.Margin = new Padding(5);
            pnlPreviewScroll.Name = "pnlPreviewScroll";
            pnlPreviewScroll.Size = new Size(902, 1272);
            pnlPreviewScroll.TabIndex = 1;
            // 
            // picPreview
            // 
            picPreview.BackColor = Color.FromArgb(248, 250, 252);
            picPreview.Location = new Point(0, 0);
            picPreview.Margin = new Padding(5);
            picPreview.Name = "picPreview";
            picPreview.Size = new Size(895, 1250);
            picPreview.TabIndex = 1;
            picPreview.TabStop = false;
            // 
            // pnlHeaderBottom
            // 
            pnlHeaderBottom.BackColor = Color.FromArgb(203, 213, 225);
            pnlHeaderBottom.Controls.Add(lblHeaderBottom);
            pnlHeaderBottom.Dock = DockStyle.Top;
            pnlHeaderBottom.Location = new Point(0, 0);
            pnlHeaderBottom.Margin = new Padding(5);
            pnlHeaderBottom.Name = "pnlHeaderBottom";
            pnlHeaderBottom.Size = new Size(902, 56);
            pnlHeaderBottom.TabIndex = 0;
            // 
            // lblHeaderBottom
            // 
            lblHeaderBottom.Dock = DockStyle.Fill;
            lblHeaderBottom.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblHeaderBottom.ForeColor = Color.FromArgb(30, 41, 59);
            lblHeaderBottom.Location = new Point(0, 0);
            lblHeaderBottom.Margin = new Padding(5, 0, 5, 0);
            lblHeaderBottom.Name = "lblHeaderBottom";
            lblHeaderBottom.Size = new Size(902, 56);
            lblHeaderBottom.TabIndex = 0;
            lblHeaderBottom.Text = "Live Schematic Diagram Preview";
            lblHeaderBottom.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // colNetId
            // 
            colNetId.HeaderText = "Net-ID";
            colNetId.MinimumWidth = 110;
            colNetId.Name = "colNetId";
            colNetId.Width = 130;
            // 
            // colHarnessGroup
            // 
            colHarnessGroup.HeaderText = "Harness-Group";
            colHarnessGroup.MinimumWidth = 130;
            colHarnessGroup.Name = "colHarnessGroup";
            colHarnessGroup.Width = 160;
            // 
            // colFromLabel
            // 
            colFromLabel.HeaderText = "Net-From\nLabel";
            colFromLabel.MinimumWidth = 100;
            colFromLabel.Name = "colFromLabel";
            colFromLabel.Width = 120;
            // 
            // colToLabel
            // 
            colToLabel.HeaderText = "Net-To\nLabel";
            colToLabel.MinimumWidth = 100;
            colToLabel.Name = "colToLabel";
            colToLabel.Width = 120;
            // 
            // colTestType
            // 
            colTestType.FlatStyle = FlatStyle.Flat;
            colTestType.HeaderText = "Test Type";
            colTestType.Items.AddRange(new object[] { "Resistance", "Capacitance", "Continuity", "Insulation", "Short Circuit", "Functional", "Voltage" });
            colTestType.MinimumWidth = 120;
            colTestType.Name = "colTestType";
            colTestType.Width = 150;
            // 
            // colLowerLimit
            // 
            colLowerLimit.HeaderText = "Lower\nLimit";
            colLowerLimit.MinimumWidth = 80;
            colLowerLimit.Name = "colLowerLimit";
            colLowerLimit.ReadOnly = true;
            colLowerLimit.Width = 95;
            // 
            // colUpperLimit
            // 
            colUpperLimit.HeaderText = "Upper\nLimit";
            colUpperLimit.MinimumWidth = 80;
            colUpperLimit.Name = "colUpperLimit";
            colUpperLimit.ReadOnly = true;
            colUpperLimit.Width = 95;
            // 
            // colExpectedValue
            // 
            colExpectedValue.HeaderText = "Expected\nValue";
            colExpectedValue.MinimumWidth = 110;
            colExpectedValue.Name = "colExpectedValue";
            colExpectedValue.Width = 140;
            // 
            // colTolerance
            // 
            colTolerance.HeaderText = "Tolerance";
            colTolerance.MinimumWidth = 90;
            colTolerance.Name = "colTolerance";
            colTolerance.Width = 110;
            // 
            // tblCellConfigName
            // 
            tblCellConfigName.AutoSize = true;
            tblCellConfigName.ColumnCount = 1;
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
            tblCellConfigName.Size = new Size(513, 81);
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
            lblConfigName.Size = new Size(164, 32);
            lblConfigName.TabIndex = 0;
            lblConfigName.Text = "Config Name";
            // 
            // txtConfigName
            // 
            txtConfigName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtConfigName.CausesValidation = false;
            txtConfigName.Font = new Font("Segoe UI", 10F);
            txtConfigName.Location = new Point(0, 38);
            txtConfigName.Margin = new Padding(0);
            txtConfigName.Name = "txtConfigName";
            txtConfigName.Size = new Size(513, 43);
            txtConfigName.TabIndex = 1;
            txtConfigName.Text = "Main Harness Test";
            // 
            // tblCellProductModel
            // 
            tblCellProductModel.AutoSize = true;
            tblCellProductModel.ColumnCount = 1;
            tblCellProductModel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblCellProductModel.Controls.Add(lblProductModel, 0, 0);
            tblCellProductModel.Controls.Add(txtProductModel, 0, 1);
            tblCellProductModel.Dock = DockStyle.Fill;
            tblCellProductModel.Location = new Point(549, 4);
            tblCellProductModel.Margin = new Padding(15, 4, 6, 8);
            tblCellProductModel.Name = "tblCellProductModel";
            tblCellProductModel.RowCount = 2;
            tblCellProductModel.RowStyles.Add(new RowStyle());
            tblCellProductModel.RowStyles.Add(new RowStyle());
            tblCellProductModel.Size = new Size(513, 81);
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
            lblProductModel.Size = new Size(185, 32);
            lblProductModel.TabIndex = 0;
            lblProductModel.Text = "Product Model";
            // 
            // txtProductModel
            // 
            txtProductModel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtProductModel.CausesValidation = false;
            txtProductModel.Font = new Font("Segoe UI", 10F);
            txtProductModel.Location = new Point(0, 38);
            txtProductModel.Margin = new Padding(0);
            txtProductModel.Name = "txtProductModel";
            txtProductModel.Size = new Size(513, 43);
            txtProductModel.TabIndex = 1;
            txtProductModel.Text = "6060";
            // 
            // ManualTestingPage
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
            Controls.Add(splitContainerMain);
            Margin = new Padding(0);
            Name = "ManualTestingPage";
            Padding = new Padding(15);
            Size = new Size(2046, 1360);
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
            tblCellConnRes.ResumeLayout(false);
            tblCellConnRes.PerformLayout();
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
            tblCellConfigName.ResumeLayout(false);
            tblCellConfigName.PerformLayout();
            tblCellProductModel.ResumeLayout(false);
            tblCellProductModel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private AssemblyLineTester.Views.Controls.RoundedButton btnLearnConnection;
        private AssemblyLineTester.Views.Controls.RoundedButton btnStart;
        private AssemblyLineTester.Views.Controls.RoundedButton btnStartSelected;
        private AssemblyLineTester.Views.Controls.RoundedButton btnAbort;

        private SplitContainer splitContainerMain;
        private Panel pnlLeftBox;
        private DataGridView gridConfig;
        private DataGridViewTextBoxColumn colNo;
        private DataGridViewTextBoxColumn colNetId;
        private DataGridViewTextBoxColumn colHarnessGroup;
        private DataGridViewTextBoxColumn colFromAddr;
        private DataGridViewTextBoxColumn colFromLabel;
        private DataGridViewTextBoxColumn colToAddr;
        private DataGridViewTextBoxColumn colToLabel;
        private DataGridViewComboBoxColumn colTestType;
        private DataGridViewTextBoxColumn colLowerLimit;
        private DataGridViewTextBoxColumn colUpperLimit;
        private DataGridViewTextBoxColumn colExpectedValue;
        private DataGridViewTextBoxColumn colTolerance;
        private DataGridViewComboBoxColumn colUnit;
        private Panel pnlConfigInfo;
        private TableLayoutPanel tblConfigInfo;
        private TableLayoutPanel tblCellConfigName;
        private TableLayoutPanel tblCellProductModel;
        private TableLayoutPanel tblCellConnRes;
        private TableLayoutPanel tblCellInsRes;
        private Label lblConfigName;
        private Label lblProductModel;
        private Label lblConnectionResistance;
        private Label lblInsulationResistance;
        internal TextBox txtConfigName;
        internal TextBox txtProductModel;
        internal TextBox txtConnectionResistance;
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
    }
}