using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace AssemblyLineTester.Views.Modals
{
    partial class AutomatedTestConfigModalForm
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlCustomHeader = new Panel();
            lblCustomTitle = new Label();
            iconLogo = new IconPictureBox();
            btnMinimize = new IconButton();
            btnMaximize = new IconButton();
            btnClose = new IconButton();
            pnlBodyContainer = new Panel();
            splitContainerMain = new SplitContainer();
            pnlTopBox = new Panel();
            gridConfig = new DataGridView();
            colNo = new DataGridViewTextBoxColumn();
            colNetId = new DataGridViewTextBoxColumn();
            colHarnessGroup = new DataGridViewTextBoxColumn();
            colFromAddr = new DataGridViewTextBoxColumn();
            colFromLabel = new DataGridViewTextBoxColumn();
            colToAddr = new DataGridViewTextBoxColumn();
            colToLabel = new DataGridViewTextBoxColumn();
            colTestType = new DataGridViewComboBoxColumn();
            colLowerLimit = new DataGridViewTextBoxColumn();
            colUpperLimit = new DataGridViewTextBoxColumn();
            colUnit = new DataGridViewComboBoxColumn();
            gridConfigInfo = new DataGridView();
            colConfigName = new DataGridViewTextBoxColumn();
            colProductModel = new DataGridViewTextBoxColumn();
            colDescription = new DataGridViewTextBoxColumn();
            pnlConfigInfoHeader = new Panel();
            lblConfigInfoHeader = new Label();
            pnlButtons = new Panel();
            btnExport = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnReset = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnImport = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnRemove = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnAdd = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnTotalNet = new AssemblyLineTester.Views.Controls.RoundedButton();
            pnlHeaderTop = new Panel();
            lblHeaderTop = new Label();
            pnlBottomBox = new Panel();
            pnlPreviewScroll = new Panel();
            picPreview = new PictureBox();
            pnlHeaderBottom = new Panel();
            lblHeaderBottom = new Label();
            pnlCustomHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconLogo).BeginInit();
            pnlBodyContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
            splitContainerMain.Panel1.SuspendLayout();
            splitContainerMain.Panel2.SuspendLayout();
            splitContainerMain.SuspendLayout();
            pnlTopBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridConfig).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridConfigInfo).BeginInit();
            pnlConfigInfoHeader.SuspendLayout();
            pnlButtons.SuspendLayout();
            pnlHeaderTop.SuspendLayout();
            pnlBottomBox.SuspendLayout();
            pnlPreviewScroll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picPreview).BeginInit();
            pnlHeaderBottom.SuspendLayout();
            SuspendLayout();
            // 
            // pnlCustomHeader
            // 
            pnlCustomHeader.BackColor = Color.FromArgb(34, 40, 49);
            pnlCustomHeader.Controls.Add(lblCustomTitle);
            pnlCustomHeader.Controls.Add(iconLogo);
            pnlCustomHeader.Controls.Add(btnMinimize);
            pnlCustomHeader.Controls.Add(btnMaximize);
            pnlCustomHeader.Controls.Add(btnClose);
            pnlCustomHeader.Dock = DockStyle.Top;
            pnlCustomHeader.Location = new Point(1, 1);
            pnlCustomHeader.Margin = new Padding(0);
            pnlCustomHeader.Name = "pnlCustomHeader";
            pnlCustomHeader.Padding = new Padding(15, 0, 10, 0);
            pnlCustomHeader.Size = new Size(2403, 56);
            pnlCustomHeader.TabIndex = 2;
            // 
            // lblCustomTitle
            // 
            lblCustomTitle.AutoSize = true;
            lblCustomTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCustomTitle.ForeColor = SystemColors.ControlLightLight;
            lblCustomTitle.Location = new Point(73, 2);
            lblCustomTitle.Name = "lblCustomTitle";
            lblCustomTitle.Size = new Size(389, 45);
            lblCustomTitle.TabIndex = 5;
            lblCustomTitle.Text = "CREATE TEST PROGRAM";
            // 
            // iconLogo
            // 
            iconLogo.BackColor = Color.Transparent;
            iconLogo.Dock = DockStyle.Left;
            iconLogo.ForeColor = Color.FromArgb(56, 189, 248);
            iconLogo.IconChar = IconChar.NetworkWired;
            iconLogo.IconColor = Color.FromArgb(56, 189, 248);
            iconLogo.IconFont = IconFont.Auto;
            iconLogo.IconSize = 45;
            iconLogo.Location = new Point(15, 0);
            iconLogo.Margin = new Padding(0);
            iconLogo.Name = "iconLogo";
            iconLogo.Size = new Size(45, 56);
            iconLogo.SizeMode = PictureBoxSizeMode.CenterImage;
            iconLogo.TabIndex = 0;
            iconLogo.TabStop = false;
            // 
            // btnMinimize
            // 
            btnMinimize.BackColor = Color.Transparent;
            btnMinimize.Dock = DockStyle.Right;
            btnMinimize.FlatAppearance.BorderSize = 0;
            btnMinimize.FlatStyle = FlatStyle.Flat;
            btnMinimize.IconChar = IconChar.WindowMinimize;
            btnMinimize.IconColor = Color.FromArgb(203, 213, 225);
            btnMinimize.IconFont = IconFont.Auto;
            btnMinimize.IconSize = 20;
            btnMinimize.Location = new Point(2225, 0);
            btnMinimize.Margin = new Padding(0);
            btnMinimize.Name = "btnMinimize";
            btnMinimize.Size = new Size(56, 56);
            btnMinimize.TabIndex = 2;
            btnMinimize.UseVisualStyleBackColor = false;
            // 
            // btnMaximize
            // 
            btnMaximize.BackColor = Color.Transparent;
            btnMaximize.Dock = DockStyle.Right;
            btnMaximize.FlatAppearance.BorderSize = 0;
            btnMaximize.FlatStyle = FlatStyle.Flat;
            btnMaximize.IconChar = IconChar.WindowMaximize;
            btnMaximize.IconColor = Color.FromArgb(203, 213, 225);
            btnMaximize.IconFont = IconFont.Auto;
            btnMaximize.IconSize = 20;
            btnMaximize.Location = new Point(2281, 0);
            btnMaximize.Margin = new Padding(0);
            btnMaximize.Name = "btnMaximize";
            btnMaximize.Size = new Size(56, 56);
            btnMaximize.TabIndex = 3;
            btnMaximize.UseVisualStyleBackColor = false;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.Transparent;
            btnClose.Dock = DockStyle.Right;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.IconChar = IconChar.Close;
            btnClose.IconColor = Color.FromArgb(203, 213, 225);
            btnClose.IconFont = IconFont.Auto;
            btnClose.IconSize = 22;
            btnClose.Location = new Point(2337, 0);
            btnClose.Margin = new Padding(0);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(56, 56);
            btnClose.TabIndex = 4;
            btnClose.UseVisualStyleBackColor = false;
            // 
            // pnlBodyContainer
            // 
            pnlBodyContainer.BackColor = Color.FromArgb(248, 250, 252);
            pnlBodyContainer.Controls.Add(splitContainerMain);
            pnlBodyContainer.Dock = DockStyle.Fill;
            pnlBodyContainer.Location = new Point(1, 57);
            pnlBodyContainer.Margin = new Padding(0);
            pnlBodyContainer.Name = "pnlBodyContainer";
            pnlBodyContainer.Padding = new Padding(20);
            pnlBodyContainer.Size = new Size(2403, 1382);
            pnlBodyContainer.TabIndex = 3;
            // 
            // splitContainerMain
            // 
            splitContainerMain.BackColor = Color.FromArgb(203, 213, 225);
            splitContainerMain.Dock = DockStyle.Fill;
            splitContainerMain.Location = new Point(20, 20);
            splitContainerMain.Margin = new Padding(5);
            splitContainerMain.Name = "splitContainerMain";
            splitContainerMain.Orientation = Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1
            // 
            splitContainerMain.Panel1.BackColor = Color.FromArgb(248, 250, 252);
            splitContainerMain.Panel1.Controls.Add(pnlTopBox);
            splitContainerMain.Panel1MinSize = 200;
            // 
            // splitContainerMain.Panel2
            // 
            splitContainerMain.Panel2.BackColor = Color.FromArgb(248, 250, 252);
            splitContainerMain.Panel2.Controls.Add(pnlBottomBox);
            splitContainerMain.Panel2MinSize = 200;
            splitContainerMain.Size = new Size(2363, 1342);
            splitContainerMain.SplitterDistance = 646;
            splitContainerMain.SplitterWidth = 13;
            splitContainerMain.TabIndex = 0;
            // 
            // pnlTopBox
            // 
            pnlTopBox.BackColor = Color.White;
            pnlTopBox.BorderStyle = BorderStyle.FixedSingle;
            pnlTopBox.Controls.Add(gridConfig);
            pnlTopBox.Controls.Add(gridConfigInfo);
            pnlTopBox.Controls.Add(pnlConfigInfoHeader);
            pnlTopBox.Controls.Add(pnlButtons);
            pnlTopBox.Controls.Add(pnlHeaderTop);
            pnlTopBox.Dock = DockStyle.Fill;
            pnlTopBox.Location = new Point(0, 0);
            pnlTopBox.Margin = new Padding(0);
            pnlTopBox.Name = "pnlTopBox";
            pnlTopBox.Size = new Size(2363, 646);
            pnlTopBox.TabIndex = 0;
            // 
            // gridConfig
            // 
            gridConfig.AllowUserToAddRows = false;
            gridConfig.AllowUserToDeleteRows = false;
            gridConfig.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridConfig.BackgroundColor = Color.White;
            gridConfig.BorderStyle = BorderStyle.None;
            gridConfig.ColumnHeadersHeight = 45;
            gridConfig.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            gridConfig.Columns.AddRange(new DataGridViewColumn[] { colNo, colNetId, colHarnessGroup, colFromAddr, colFromLabel, colToAddr, colToLabel, colTestType, colLowerLimit, colUpperLimit, colUnit });
            gridConfig.Dock = DockStyle.Fill;
            gridConfig.EnableHeadersVisualStyles = false;
            gridConfig.Location = new Point(0, 368);
            gridConfig.Margin = new Padding(5);
            gridConfig.Name = "gridConfig";
            gridConfig.RowHeadersVisible = false;
            gridConfig.RowHeadersWidth = 82;
            gridConfig.RowTemplate.Height = 38;
            gridConfig.Size = new Size(2361, 276);
            gridConfig.TabIndex = 2;
            // 
            // colNo
            // 
            colNo.FillWeight = 40F;
            colNo.HeaderText = "No.";
            colNo.MinimumWidth = 45;
            colNo.Name = "colNo";
            colNo.ReadOnly = true;
            // 
            // colNetId
            // 
            colNetId.FillWeight = 110F;
            colNetId.HeaderText = "Net-ID";
            colNetId.MinimumWidth = 100;
            colNetId.Name = "colNetId";
            // 
            // colHarnessGroup
            // 
            colHarnessGroup.FillWeight = 130F;
            colHarnessGroup.HeaderText = "Harness-Group";
            colHarnessGroup.MinimumWidth = 120;
            colHarnessGroup.Name = "colHarnessGroup";
            // 
            // colFromAddr
            // 
            colFromAddr.FillWeight = 80F;
            colFromAddr.HeaderText = "Net-From\nAddress";
            colFromAddr.MinimumWidth = 70;
            colFromAddr.Name = "colFromAddr";
            // 
            // colFromLabel
            // 
            colFromLabel.FillWeight = 90F;
            colFromLabel.HeaderText = "Net-From\nLabel";
            colFromLabel.MinimumWidth = 80;
            colFromLabel.Name = "colFromLabel";
            // 
            // colToAddr
            // 
            colToAddr.FillWeight = 80F;
            colToAddr.HeaderText = "Net-To\nAddress";
            colToAddr.MinimumWidth = 70;
            colToAddr.Name = "colToAddr";
            // 
            // colToLabel
            // 
            colToLabel.FillWeight = 90F;
            colToLabel.HeaderText = "Net-To\nLabel";
            colToLabel.MinimumWidth = 80;
            colToLabel.Name = "colToLabel";
            // 
            // colTestType
            // 
            colTestType.FillWeight = 130F;
            colTestType.FlatStyle = FlatStyle.Flat;
            colTestType.HeaderText = "Test Type";
            colTestType.Items.AddRange(new object[] { "Resistance", "Capacitance", "Continuity", "Insulation", "Short Circuit", "Functional", "Voltage" });
            colTestType.MinimumWidth = 110;
            colTestType.Name = "colTestType";
            // 
            // colLowerLimit
            // 
            colLowerLimit.FillWeight = 70F;
            colLowerLimit.HeaderText = "Lower\nLimit";
            colLowerLimit.MinimumWidth = 60;
            colLowerLimit.Name = "colLowerLimit";
            // 
            // colUpperLimit
            // 
            colUpperLimit.FillWeight = 70F;
            colUpperLimit.HeaderText = "Upper\nLimit";
            colUpperLimit.MinimumWidth = 60;
            colUpperLimit.Name = "colUpperLimit";
            // 
            // colUnit
            // 
            colUnit.FillWeight = 110F;
            colUnit.FlatStyle = FlatStyle.Flat;
            colUnit.HeaderText = "Measurement\nUnit";
            colUnit.Items.AddRange(new object[] { "Ω", "mΩ", "kΩ", "MΩ", "GΩ", "F", "μF", "nF", "pF", "V", "mV", "kV", "A", "mA", "Hz", "ms" });
            colUnit.MinimumWidth = 100;
            colUnit.Name = "colUnit";
            // 
            // gridConfigInfo
            // 
            gridConfigInfo.AllowUserToAddRows = false;
            gridConfigInfo.AllowUserToDeleteRows = false;
            gridConfigInfo.AllowUserToResizeRows = false;
            gridConfigInfo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridConfigInfo.BackgroundColor = Color.White;
            gridConfigInfo.BorderStyle = BorderStyle.None;
            gridConfigInfo.ColumnHeadersHeight = 40;
            gridConfigInfo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            gridConfigInfo.Columns.AddRange(new DataGridViewColumn[] { colConfigName, colProductModel, colDescription });
            gridConfigInfo.Dock = DockStyle.Top;
            gridConfigInfo.EditMode = DataGridViewEditMode.EditProgrammatically;
            gridConfigInfo.EnableHeadersVisualStyles = false;
            gridConfigInfo.Location = new Point(0, 218);
            gridConfigInfo.Margin = new Padding(5);
            gridConfigInfo.MultiSelect = false;
            gridConfigInfo.Name = "gridConfigInfo";
            gridConfigInfo.ReadOnly = true;
            gridConfigInfo.RowHeadersVisible = false;
            gridConfigInfo.RowHeadersWidth = 82;
            gridConfigInfo.RowTemplate.Height = 35;
            gridConfigInfo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridConfigInfo.Size = new Size(2361, 150);
            gridConfigInfo.TabIndex = 4;
            // 
            // colConfigName
            // 
            colConfigName.FillWeight = 120F;
            colConfigName.HeaderText = "Config Name";
            colConfigName.MinimumWidth = 150;
            colConfigName.Name = "colConfigName";
            colConfigName.ReadOnly = true;
            // 
            // colProductModel
            // 
            colProductModel.FillWeight = 80F;
            colProductModel.HeaderText = "Product Model";
            colProductModel.MinimumWidth = 120;
            colProductModel.Name = "colProductModel";
            colProductModel.ReadOnly = true;
            // 
            // colDescription
            // 
            colDescription.FillWeight = 200F;
            colDescription.HeaderText = "Description";
            colDescription.MinimumWidth = 200;
            colDescription.Name = "colDescription";
            colDescription.ReadOnly = true;
            // 
            // pnlConfigInfoHeader
            // 
            pnlConfigInfoHeader.BackColor = Color.FromArgb(203, 213, 225);
            pnlConfigInfoHeader.Controls.Add(lblConfigInfoHeader);
            pnlConfigInfoHeader.Dock = DockStyle.Top;
            pnlConfigInfoHeader.Location = new Point(0, 170);
            pnlConfigInfoHeader.Margin = new Padding(5);
            pnlConfigInfoHeader.Name = "pnlConfigInfoHeader";
            pnlConfigInfoHeader.Size = new Size(2361, 48);
            pnlConfigInfoHeader.TabIndex = 3;
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
            lblConfigInfoHeader.Size = new Size(2361, 48);
            lblConfigInfoHeader.TabIndex = 0;
            lblConfigInfoHeader.Text = "Config Summary";
            lblConfigInfoHeader.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlButtons
            // 
            pnlButtons.BackColor = Color.FromArgb(248, 250, 252);
            pnlButtons.Controls.Add(btnExport);
            pnlButtons.Controls.Add(btnReset);
            pnlButtons.Controls.Add(btnImport);
            pnlButtons.Controls.Add(btnRemove);
            pnlButtons.Controls.Add(btnAdd);
            pnlButtons.Controls.Add(btnTotalNet);
            pnlButtons.Dock = DockStyle.Top;
            pnlButtons.Location = new Point(0, 74);
            pnlButtons.Margin = new Padding(5);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(2361, 96);
            pnlButtons.TabIndex = 1;
            // 
            // btnExport
            // 
            btnExport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExport.BackColor = Color.FromArgb(203, 213, 225);
            btnExport.BorderRadius = 18;
            btnExport.FlatAppearance.BorderSize = 0;
            btnExport.FlatStyle = FlatStyle.Flat;
            btnExport.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnExport.ForeColor = Color.FromArgb(30, 41, 59);
            btnExport.HoverBackColor = Color.FromArgb(148, 163, 184);
            btnExport.Location = new Point(2120, 16);
            btnExport.Margin = new Padding(5);
            btnExport.Name = "btnExport";
            btnExport.NormalBackColor = Color.FromArgb(203, 213, 225);
            btnExport.PressedBackColor = Color.FromArgb(100, 116, 139);
            btnExport.Size = new Size(211, 64);
            btnExport.TabIndex = 5;
            btnExport.Text = "Export Config";
            btnExport.UseVisualStyleBackColor = false;
            // 
            // btnReset
            // 
            btnReset.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnReset.BackColor = Color.FromArgb(203, 213, 225);
            btnReset.BorderRadius = 18;
            btnReset.FlatAppearance.BorderSize = 0;
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnReset.ForeColor = Color.FromArgb(220, 38, 38);
            btnReset.HoverBackColor = Color.FromArgb(254, 226, 226);
            btnReset.Location = new Point(1885, 16);
            btnReset.Margin = new Padding(5);
            btnReset.Name = "btnReset";
            btnReset.NormalBackColor = Color.FromArgb(203, 213, 225);
            btnReset.PressedBackColor = Color.FromArgb(254, 202, 202);
            btnReset.Size = new Size(211, 64);
            btnReset.TabIndex = 4;
            btnReset.Text = "Reset Config";
            btnReset.UseVisualStyleBackColor = false;
            // 
            // btnImport
            // 
            btnImport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnImport.BackColor = Color.FromArgb(203, 213, 225);
            btnImport.BorderRadius = 18;
            btnImport.FlatAppearance.BorderSize = 0;
            btnImport.FlatStyle = FlatStyle.Flat;
            btnImport.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnImport.ForeColor = Color.FromArgb(30, 41, 59);
            btnImport.HoverBackColor = Color.FromArgb(148, 163, 184);
            btnImport.Location = new Point(1649, 16);
            btnImport.Margin = new Padding(5);
            btnImport.Name = "btnImport";
            btnImport.NormalBackColor = Color.FromArgb(203, 213, 225);
            btnImport.PressedBackColor = Color.FromArgb(100, 116, 139);
            btnImport.Size = new Size(211, 64);
            btnImport.TabIndex = 3;
            btnImport.Text = "Import Config";
            btnImport.UseVisualStyleBackColor = false;
            // 
            // btnRemove
            // 
            btnRemove.BackColor = Color.FromArgb(239, 68, 68);
            btnRemove.BorderRadius = 18;
            btnRemove.FlatAppearance.BorderSize = 0;
            btnRemove.FlatStyle = FlatStyle.Flat;
            btnRemove.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            btnRemove.ForeColor = Color.White;
            btnRemove.HoverBackColor = Color.FromArgb(248, 113, 113);
            btnRemove.Location = new Point(352, 13);
            btnRemove.Margin = new Padding(5);
            btnRemove.Name = "btnRemove";
            btnRemove.NormalBackColor = Color.FromArgb(239, 68, 68);
            btnRemove.PressedBackColor = Color.FromArgb(220, 38, 38);
            btnRemove.Size = new Size(89, 70);
            btnRemove.TabIndex = 2;
            btnRemove.Text = "-";
            btnRemove.UseVisualStyleBackColor = false;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(34, 197, 94);
            btnAdd.BorderRadius = 18;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            btnAdd.ForeColor = Color.White;
            btnAdd.HoverBackColor = Color.FromArgb(74, 222, 128);
            btnAdd.Location = new Point(244, 13);
            btnAdd.Margin = new Padding(5);
            btnAdd.Name = "btnAdd";
            btnAdd.NormalBackColor = Color.FromArgb(34, 197, 94);
            btnAdd.PressedBackColor = Color.FromArgb(22, 163, 74);
            btnAdd.Size = new Size(89, 70);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "+";
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnTotalNet
            // 
            btnTotalNet.BackColor = Color.FromArgb(203, 213, 225);
            btnTotalNet.BorderRadius = 18;
            btnTotalNet.FlatAppearance.BorderSize = 0;
            btnTotalNet.FlatStyle = FlatStyle.Flat;
            btnTotalNet.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnTotalNet.ForeColor = Color.FromArgb(30, 41, 59);
            btnTotalNet.HoverBackColor = Color.FromArgb(203, 213, 225);
            btnTotalNet.Location = new Point(24, 16);
            btnTotalNet.Margin = new Padding(5, 0, 5, 0);
            btnTotalNet.Name = "btnTotalNet";
            btnTotalNet.NormalBackColor = Color.FromArgb(203, 213, 225);
            btnTotalNet.PressedBackColor = Color.FromArgb(203, 213, 225);
            btnTotalNet.Size = new Size(187, 64);
            btnTotalNet.TabIndex = 0;
            btnTotalNet.Text = "Total Net: 4";
            btnTotalNet.UseVisualStyleBackColor = false;
            // 
            // pnlHeaderTop
            // 
            pnlHeaderTop.BackColor = Color.FromArgb(203, 213, 225);
            pnlHeaderTop.Controls.Add(lblHeaderTop);
            pnlHeaderTop.Dock = DockStyle.Top;
            pnlHeaderTop.Location = new Point(0, 0);
            pnlHeaderTop.Margin = new Padding(5);
            pnlHeaderTop.Name = "pnlHeaderTop";
            pnlHeaderTop.Size = new Size(2361, 74);
            pnlHeaderTop.TabIndex = 0;
            // 
            // lblHeaderTop
            // 
            lblHeaderTop.Dock = DockStyle.Fill;
            lblHeaderTop.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblHeaderTop.ForeColor = Color.FromArgb(30, 41, 59);
            lblHeaderTop.Location = new Point(0, 0);
            lblHeaderTop.Margin = new Padding(5, 0, 5, 0);
            lblHeaderTop.Name = "lblHeaderTop";
            lblHeaderTop.Size = new Size(2361, 74);
            lblHeaderTop.TabIndex = 0;
            lblHeaderTop.Text = "Net List & Harness Configuration";
            lblHeaderTop.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlBottomBox
            // 
            pnlBottomBox.BackColor = Color.White;
            pnlBottomBox.BorderStyle = BorderStyle.FixedSingle;
            pnlBottomBox.Controls.Add(pnlPreviewScroll);
            pnlBottomBox.Controls.Add(pnlHeaderBottom);
            pnlBottomBox.Dock = DockStyle.Fill;
            pnlBottomBox.Location = new Point(0, 0);
            pnlBottomBox.Margin = new Padding(0);
            pnlBottomBox.Name = "pnlBottomBox";
            pnlBottomBox.Size = new Size(2363, 683);
            pnlBottomBox.TabIndex = 1;
            // 
            // pnlPreviewScroll
            // 
            pnlPreviewScroll.AutoScroll = true;
            pnlPreviewScroll.BackColor = Color.FromArgb(248, 250, 252);
            pnlPreviewScroll.Controls.Add(picPreview);
            pnlPreviewScroll.Dock = DockStyle.Fill;
            pnlPreviewScroll.Location = new Point(0, 74);
            pnlPreviewScroll.Margin = new Padding(5);
            pnlPreviewScroll.Name = "pnlPreviewScroll";
            pnlPreviewScroll.Size = new Size(2361, 607);
            pnlPreviewScroll.TabIndex = 1;
            // 
            // picPreview
            // 
            picPreview.BackColor = Color.FromArgb(248, 250, 252);
            picPreview.Location = new Point(0, 0);
            picPreview.Margin = new Padding(5);
            picPreview.Name = "picPreview";
            picPreview.Size = new Size(2353, 960);
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
            pnlHeaderBottom.Size = new Size(2361, 74);
            pnlHeaderBottom.TabIndex = 0;
            // 
            // lblHeaderBottom
            // 
            lblHeaderBottom.Dock = DockStyle.Fill;
            lblHeaderBottom.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblHeaderBottom.ForeColor = Color.FromArgb(30, 41, 59);
            lblHeaderBottom.Location = new Point(0, 0);
            lblHeaderBottom.Margin = new Padding(5, 0, 5, 0);
            lblHeaderBottom.Name = "lblHeaderBottom";
            lblHeaderBottom.Size = new Size(2361, 74);
            lblHeaderBottom.TabIndex = 0;
            lblHeaderBottom.Text = "Connection Preview";
            lblHeaderBottom.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // AutomatedTestConfigModalForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(51, 65, 85);
            ClientSize = new Size(2405, 1440);
            Controls.Add(pnlBodyContainer);
            Controls.Add(pnlCustomHeader);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(5);
            MinimizeBox = false;
            Name = "AutomatedTestConfigModalForm";
            Padding = new Padding(1);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Automated Test Configuration";
            pnlCustomHeader.ResumeLayout(false);
            pnlCustomHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconLogo).EndInit();
            pnlBodyContainer.ResumeLayout(false);
            splitContainerMain.Panel1.ResumeLayout(false);
            splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).EndInit();
            splitContainerMain.ResumeLayout(false);
            pnlTopBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridConfig).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridConfigInfo).EndInit();
            pnlConfigInfoHeader.ResumeLayout(false);
            pnlButtons.ResumeLayout(false);
            pnlHeaderTop.ResumeLayout(false);
            pnlBottomBox.ResumeLayout(false);
            pnlPreviewScroll.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picPreview).EndInit();
            pnlHeaderBottom.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.Panel pnlTopBox;
        private System.Windows.Forms.Panel pnlHeaderTop;
        private System.Windows.Forms.Label lblHeaderTop;
        private System.Windows.Forms.Panel pnlButtons;
        private AssemblyLineTester.Views.Controls.RoundedButton btnTotalNet;
        private AssemblyLineTester.Views.Controls.RoundedButton btnAdd;
        private AssemblyLineTester.Views.Controls.RoundedButton btnRemove;
        private AssemblyLineTester.Views.Controls.RoundedButton btnImport;
        private AssemblyLineTester.Views.Controls.RoundedButton btnReset;
        private AssemblyLineTester.Views.Controls.RoundedButton btnExport;
        private System.Windows.Forms.DataGridView gridConfig;
        private System.Windows.Forms.Panel pnlBottomBox;
        private System.Windows.Forms.Panel pnlHeaderBottom;
        private System.Windows.Forms.Label lblHeaderBottom;
        private System.Windows.Forms.Panel pnlPreviewScroll;
        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNetId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHarnessGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFromAddr;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFromLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colToAddr;
        private System.Windows.Forms.DataGridViewTextBoxColumn colToLabel;
        private System.Windows.Forms.DataGridViewComboBoxColumn colTestType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLowerLimit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUpperLimit;
        private System.Windows.Forms.DataGridViewComboBoxColumn colUnit;
        private System.Windows.Forms.Panel pnlConfigInfoHeader;
        private System.Windows.Forms.Label lblConfigInfoHeader;
        private System.Windows.Forms.DataGridView gridConfigInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colConfigName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.Panel pnlCustomHeader;
        private FontAwesome.Sharp.IconPictureBox iconLogo;
        private FontAwesome.Sharp.IconButton btnClose;
        private FontAwesome.Sharp.IconButton btnMaximize;
        private FontAwesome.Sharp.IconButton btnMinimize;
        private System.Windows.Forms.Panel pnlBodyContainer;
        private Label lblCustomTitle;
    }
}
