using System.Drawing;
using System.Windows.Forms;

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
            pnlButtons = new Panel();
            btnExport = new Button();
            btnReset = new Button();
            btnImport = new Button();
            btnRemove = new Button();
            btnAdd = new Button();
            btnTotalNet = new Label();
            pnlHeaderTop = new Panel();
            lblHeaderTop = new Label();
            pnlBottomBox = new Panel();
            pnlPreviewScroll = new Panel();
            picPreview = new PictureBox();
            pnlHeaderBottom = new Panel();
            lblHeaderBottom = new Label();
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
            splitContainerMain.Panel1.SuspendLayout();
            splitContainerMain.Panel2.SuspendLayout();
            splitContainerMain.SuspendLayout();
            pnlTopBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridConfig).BeginInit();
            pnlButtons.SuspendLayout();
            pnlHeaderTop.SuspendLayout();
            pnlBottomBox.SuspendLayout();
            pnlPreviewScroll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picPreview).BeginInit();
            pnlHeaderBottom.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainerMain
            // 
            splitContainerMain.BackColor = Color.FromArgb(203, 213, 225);
            splitContainerMain.Dock = DockStyle.Fill;
            splitContainerMain.Location = new Point(24, 24);
            splitContainerMain.Margin = new Padding(5, 5, 5, 5);
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
            splitContainerMain.Size = new Size(2357, 1392);
            splitContainerMain.SplitterDistance = 671;
            splitContainerMain.SplitterWidth = 13;
            splitContainerMain.TabIndex = 0;
            // 
            // pnlTopBox
            // 
            pnlTopBox.BackColor = Color.White;
            pnlTopBox.BorderStyle = BorderStyle.FixedSingle;
            pnlTopBox.Controls.Add(gridConfig);
            pnlTopBox.Controls.Add(pnlButtons);
            pnlTopBox.Controls.Add(pnlHeaderTop);
            pnlTopBox.Dock = DockStyle.Fill;
            pnlTopBox.Location = new Point(0, 0);
            pnlTopBox.Margin = new Padding(0);
            pnlTopBox.Name = "pnlTopBox";
            pnlTopBox.Size = new Size(2357, 671);
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
            gridConfig.Location = new Point(0, 170);
            gridConfig.Margin = new Padding(5, 5, 5, 5);
            gridConfig.Name = "gridConfig";
            gridConfig.RowHeadersVisible = false;
            gridConfig.RowHeadersWidth = 82;
            gridConfig.RowTemplate.Height = 38;
            gridConfig.Size = new Size(2355, 499);
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
            pnlButtons.Margin = new Padding(5, 5, 5, 5);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(2355, 96);
            pnlButtons.TabIndex = 1;
            // 
            // btnExport
            // 
            btnExport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExport.BackColor = Color.FromArgb(203, 213, 225);
            btnExport.FlatAppearance.BorderSize = 0;
            btnExport.FlatStyle = FlatStyle.Flat;
            btnExport.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnExport.ForeColor = Color.FromArgb(30, 41, 59);
            btnExport.Location = new Point(2114, 16);
            btnExport.Margin = new Padding(5, 5, 5, 5);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(211, 64);
            btnExport.TabIndex = 5;
            btnExport.Text = "Export Config";
            btnExport.UseVisualStyleBackColor = false;
            // 
            // btnReset
            // 
            btnReset.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnReset.BackColor = Color.FromArgb(203, 213, 225);
            btnReset.FlatAppearance.BorderSize = 0;
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnReset.ForeColor = Color.FromArgb(220, 38, 38);
            btnReset.Location = new Point(1879, 16);
            btnReset.Margin = new Padding(5, 5, 5, 5);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(211, 64);
            btnReset.TabIndex = 4;
            btnReset.Text = "Reset Config";
            btnReset.UseVisualStyleBackColor = false;
            // 
            // btnImport
            // 
            btnImport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnImport.BackColor = Color.FromArgb(203, 213, 225);
            btnImport.FlatAppearance.BorderSize = 0;
            btnImport.FlatStyle = FlatStyle.Flat;
            btnImport.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnImport.ForeColor = Color.FromArgb(30, 41, 59);
            btnImport.Location = new Point(1643, 16);
            btnImport.Margin = new Padding(5, 5, 5, 5);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(211, 64);
            btnImport.TabIndex = 3;
            btnImport.Text = "Import Config";
            btnImport.UseVisualStyleBackColor = false;
            // 
            // btnRemove
            // 
            btnRemove.BackColor = Color.FromArgb(239, 68, 68);
            btnRemove.FlatAppearance.BorderSize = 0;
            btnRemove.FlatStyle = FlatStyle.Flat;
            btnRemove.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            btnRemove.ForeColor = Color.White;
            btnRemove.Location = new Point(352, 13);
            btnRemove.Margin = new Padding(5, 5, 5, 5);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(89, 70);
            btnRemove.TabIndex = 2;
            btnRemove.Text = "-";
            btnRemove.TextAlign = ContentAlignment.TopCenter;
            btnRemove.UseVisualStyleBackColor = false;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(34, 197, 94);
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(244, 13);
            btnAdd.Margin = new Padding(5, 5, 5, 5);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(89, 70);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "+";
            btnAdd.TextAlign = ContentAlignment.TopCenter;
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnTotalNet
            // 
            btnTotalNet.BackColor = Color.FromArgb(203, 213, 225);
            btnTotalNet.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnTotalNet.ForeColor = Color.FromArgb(30, 41, 59);
            btnTotalNet.Location = new Point(24, 16);
            btnTotalNet.Margin = new Padding(5, 0, 5, 0);
            btnTotalNet.Name = "btnTotalNet";
            btnTotalNet.Size = new Size(187, 64);
            btnTotalNet.TabIndex = 0;
            btnTotalNet.Text = "Total Net: 4";
            btnTotalNet.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlHeaderTop
            // 
            pnlHeaderTop.BackColor = Color.FromArgb(203, 213, 225);
            pnlHeaderTop.Controls.Add(lblHeaderTop);
            pnlHeaderTop.Dock = DockStyle.Top;
            pnlHeaderTop.Location = new Point(0, 0);
            pnlHeaderTop.Margin = new Padding(5, 5, 5, 5);
            pnlHeaderTop.Name = "pnlHeaderTop";
            pnlHeaderTop.Size = new Size(2355, 74);
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
            lblHeaderTop.Size = new Size(2355, 74);
            lblHeaderTop.TabIndex = 0;
            lblHeaderTop.Text = "Automated Test Configuration";
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
            pnlBottomBox.Size = new Size(2357, 708);
            pnlBottomBox.TabIndex = 1;
            // 
            // pnlPreviewScroll
            // 
            pnlPreviewScroll.AutoScroll = true;
            pnlPreviewScroll.BackColor = Color.FromArgb(248, 250, 252);
            pnlPreviewScroll.Controls.Add(picPreview);
            pnlPreviewScroll.Dock = DockStyle.Fill;
            pnlPreviewScroll.Location = new Point(0, 74);
            pnlPreviewScroll.Margin = new Padding(5, 5, 5, 5);
            pnlPreviewScroll.Name = "pnlPreviewScroll";
            pnlPreviewScroll.Size = new Size(2355, 632);
            pnlPreviewScroll.TabIndex = 1;
            // 
            // picPreview
            // 
            picPreview.BackColor = Color.FromArgb(248, 250, 252);
            picPreview.Location = new Point(0, 0);
            picPreview.Margin = new Padding(5, 5, 5, 5);
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
            pnlHeaderBottom.Margin = new Padding(5, 5, 5, 5);
            pnlHeaderBottom.Name = "pnlHeaderBottom";
            pnlHeaderBottom.Size = new Size(2355, 74);
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
            lblHeaderBottom.Size = new Size(2355, 74);
            lblHeaderBottom.TabIndex = 0;
            lblHeaderBottom.Text = "Connection Preview";
            lblHeaderBottom.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // AutomatedTestConfigModalForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
            ClientSize = new Size(2405, 1440);
            Controls.Add(splitContainerMain);
            Font = new Font("Segoe UI", 9F);
            Margin = new Padding(5, 5, 5, 5);
            MinimizeBox = false;
            Name = "AutomatedTestConfigModalForm";
            Padding = new Padding(24, 24, 24, 24);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Automated Test Configuration";
            splitContainerMain.Panel1.ResumeLayout(false);
            splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).EndInit();
            splitContainerMain.ResumeLayout(false);
            pnlTopBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridConfig).EndInit();
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
        private System.Windows.Forms.Label btnTotalNet;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnExport;
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
    }
}
