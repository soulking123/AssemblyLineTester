namespace AssemblyLineTester.Views.Pages.Tabs
{
    partial class HarnessGeneralTab
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

        private void InitializeComponent()
        {
            tableLayoutPanelGeneral = new TableLayoutPanel();
            panelGeneralInfo = new Panel();
            tblGenInfo = new TableLayoutPanel();
            panelGeneralTop = new FlowLayoutPanel();
            btnEditGeneral = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnDeleteHarness = new AssemblyLineTester.Views.Controls.RoundedButton();
            panelImages = new Panel();
            tblImages = new TableLayoutPanel();
            lblHarnessImage = new Label();
            picHarnessImage = new PictureBox();
            btnChangeHarnessImage = new AssemblyLineTester.Views.Controls.RoundedButton();
            lblMachineArea = new Label();
            picMachineArea = new PictureBox();
            btnChangeMachineImage = new AssemblyLineTester.Views.Controls.RoundedButton();
            tableLayoutPanelGeneral.SuspendLayout();
            panelGeneralInfo.SuspendLayout();
            panelGeneralTop.SuspendLayout();
            panelImages.SuspendLayout();
            tblImages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picHarnessImage).BeginInit();
            picHarnessImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picMachineArea).BeginInit();
            picMachineArea.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanelGeneral
            // 
            tableLayoutPanelGeneral.ColumnCount = 2;
            tableLayoutPanelGeneral.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelGeneral.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelGeneral.Controls.Add(panelGeneralInfo, 0, 0);
            tableLayoutPanelGeneral.Controls.Add(panelImages, 1, 0);
            tableLayoutPanelGeneral.Dock = DockStyle.Fill;
            tableLayoutPanelGeneral.Location = new Point(0, 0);
            tableLayoutPanelGeneral.Margin = new Padding(0);
            tableLayoutPanelGeneral.Name = "tableLayoutPanelGeneral";
            tableLayoutPanelGeneral.RowCount = 1;
            tableLayoutPanelGeneral.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelGeneral.Size = new Size(1625, 960);
            tableLayoutPanelGeneral.TabIndex = 0;
            // 
            // panelGeneralInfo
            // 
            panelGeneralInfo.Controls.Add(tblGenInfo);
            panelGeneralInfo.Controls.Add(panelGeneralTop);
            panelGeneralInfo.Dock = DockStyle.Fill;
            panelGeneralInfo.Location = new Point(0, 0);
            panelGeneralInfo.Margin = new Padding(0, 0, 26, 0);
            panelGeneralInfo.Name = "panelGeneralInfo";
            panelGeneralInfo.Size = new Size(786, 960);
            panelGeneralInfo.TabIndex = 0;
            // 
            // tblGenInfo
            // 
            tblGenInfo.AutoScroll = true;
            tblGenInfo.ColumnCount = 2;
            tblGenInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tblGenInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tblGenInfo.Dock = DockStyle.Fill;
            tblGenInfo.Location = new Point(0, 66);
            tblGenInfo.Margin = new Padding(0);
            tblGenInfo.Name = "tblGenInfo";
            tblGenInfo.RowCount = 12;
            tblGenInfo.RowStyles.Add(new RowStyle());
            tblGenInfo.RowStyles.Add(new RowStyle());
            tblGenInfo.RowStyles.Add(new RowStyle());
            tblGenInfo.RowStyles.Add(new RowStyle());
            tblGenInfo.RowStyles.Add(new RowStyle());
            tblGenInfo.RowStyles.Add(new RowStyle());
            tblGenInfo.RowStyles.Add(new RowStyle());
            tblGenInfo.RowStyles.Add(new RowStyle());
            tblGenInfo.RowStyles.Add(new RowStyle());
            tblGenInfo.RowStyles.Add(new RowStyle());
            tblGenInfo.RowStyles.Add(new RowStyle());
            tblGenInfo.RowStyles.Add(new RowStyle());
            tblGenInfo.Size = new Size(786, 894);
            tblGenInfo.TabIndex = 0;
            // 
            // panelGeneralTop
            // 
            panelGeneralTop.AutoSize = true;
            panelGeneralTop.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panelGeneralTop.Controls.Add(btnEditGeneral);
            panelGeneralTop.Controls.Add(btnDeleteHarness);
            panelGeneralTop.Dock = DockStyle.Top;
            panelGeneralTop.Location = new Point(0, 0);
            panelGeneralTop.Name = "panelGeneralTop";
            panelGeneralTop.Size = new Size(786, 66);
            panelGeneralTop.TabIndex = 1;
            // 
            // btnEditGeneral
            // 
            btnEditGeneral.NormalBackColor = Color.FromArgb(248, 250, 252);
            btnEditGeneral.HoverBackColor = Color.FromArgb(241, 245, 249);
            btnEditGeneral.PressedBackColor = Color.FromArgb(226, 232, 240);
            btnEditGeneral.BorderRadius = 8;
            btnEditGeneral.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnEditGeneral.FlatStyle = FlatStyle.Flat;
            btnEditGeneral.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEditGeneral.Location = new Point(3, 3);
            btnEditGeneral.Margin = new Padding(3, 3, 10, 3);
            btnEditGeneral.Name = "btnEditGeneral";
            btnEditGeneral.Size = new Size(240, 60);
            btnEditGeneral.TabIndex = 1;
            btnEditGeneral.Text = "✏ Edit General";
            btnEditGeneral.UseVisualStyleBackColor = false;
            // 
            // btnDeleteHarness
            // 
            btnDeleteHarness.NormalBackColor = Color.FromArgb(254, 242, 242);
            btnDeleteHarness.HoverBackColor = Color.FromArgb(254, 226, 226);
            btnDeleteHarness.PressedBackColor = Color.FromArgb(252, 165, 165);
            btnDeleteHarness.BorderRadius = 8;
            btnDeleteHarness.FlatAppearance.BorderColor = Color.FromArgb(254, 202, 202);
            btnDeleteHarness.FlatStyle = FlatStyle.Flat;
            btnDeleteHarness.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDeleteHarness.ForeColor = Color.FromArgb(239, 68, 68);
            btnDeleteHarness.Location = new Point(256, 3);
            btnDeleteHarness.Margin = new Padding(3, 3, 10, 3);
            btnDeleteHarness.Name = "btnDeleteHarness";
            btnDeleteHarness.Size = new Size(240, 60);
            btnDeleteHarness.TabIndex = 0;
            btnDeleteHarness.Text = "🗑 Delete";
            btnDeleteHarness.UseVisualStyleBackColor = false;
            // 
            // panelImages
            // 
            panelImages.Controls.Add(tblImages);
            panelImages.Dock = DockStyle.Fill;
            panelImages.Location = new Point(838, 0);
            panelImages.Margin = new Padding(26, 0, 0, 0);
            panelImages.Name = "panelImages";
            panelImages.Size = new Size(787, 960);
            panelImages.TabIndex = 1;
            // 
            // tblImages
            // 
            tblImages.ColumnCount = 1;
            tblImages.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblImages.Controls.Add(lblHarnessImage, 0, 0);
            tblImages.Controls.Add(picHarnessImage, 0, 1);
            tblImages.Controls.Add(lblMachineArea, 0, 2);
            tblImages.Controls.Add(picMachineArea, 0, 3);
            tblImages.Dock = DockStyle.Fill;
            tblImages.Location = new Point(0, 0);
            tblImages.Margin = new Padding(0);
            tblImages.Name = "tblImages";
            tblImages.RowCount = 4;
            tblImages.RowStyles.Add(new RowStyle(SizeType.Absolute, 51F));
            tblImages.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tblImages.RowStyles.Add(new RowStyle(SizeType.Absolute, 51F));
            tblImages.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tblImages.Size = new Size(787, 960);
            tblImages.TabIndex = 0;
            // 
            // lblHarnessImage
            // 
            lblHarnessImage.AutoSize = true;
            lblHarnessImage.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblHarnessImage.ForeColor = Color.FromArgb(51, 65, 85);
            lblHarnessImage.Location = new Point(0, 0);
            lblHarnessImage.Margin = new Padding(0);
            lblHarnessImage.Name = "lblHarnessImage";
            lblHarnessImage.Size = new Size(225, 36);
            lblHarnessImage.TabIndex = 1;
            lblHarnessImage.Text = "HARNESS IMAGE";
            // 
            // picHarnessImage
            // 
            picHarnessImage.BackColor = Color.FromArgb(248, 250, 252);
            picHarnessImage.Controls.Add(btnChangeHarnessImage);
            picHarnessImage.Dock = DockStyle.Fill;
            picHarnessImage.Location = new Point(0, 51);
            picHarnessImage.Margin = new Padding(0, 0, 0, 26);
            picHarnessImage.Name = "picHarnessImage";
            picHarnessImage.Size = new Size(787, 403);
            picHarnessImage.TabIndex = 0;
            picHarnessImage.TabStop = false;
            // 
            // btnChangeHarnessImage
            // 
            btnChangeHarnessImage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnChangeHarnessImage.NormalBackColor = Color.FromArgb(248, 250, 252);
            btnChangeHarnessImage.HoverBackColor = Color.FromArgb(241, 245, 249);
            btnChangeHarnessImage.PressedBackColor = Color.FromArgb(226, 232, 240);
            btnChangeHarnessImage.BorderRadius = 8;
            btnChangeHarnessImage.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnChangeHarnessImage.FlatStyle = FlatStyle.Flat;
            btnChangeHarnessImage.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnChangeHarnessImage.Location = new Point(1697, 10);
            btnChangeHarnessImage.Name = "btnChangeHarnessImage";
            btnChangeHarnessImage.Size = new Size(180, 40);
            btnChangeHarnessImage.TabIndex = 0;
            btnChangeHarnessImage.Text = "📷 Change Image";
            btnChangeHarnessImage.UseVisualStyleBackColor = false;
            btnChangeHarnessImage.Visible = false;
            // 
            // lblMachineArea
            // 
            lblMachineArea.AutoSize = true;
            lblMachineArea.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblMachineArea.ForeColor = Color.FromArgb(51, 65, 85);
            lblMachineArea.Location = new Point(0, 480);
            lblMachineArea.Margin = new Padding(0);
            lblMachineArea.Name = "lblMachineArea";
            lblMachineArea.Size = new Size(174, 36);
            lblMachineArea.TabIndex = 3;
            lblMachineArea.Text = "CONNECTOR";
            // 
            // picMachineArea
            // 
            picMachineArea.BackColor = Color.FromArgb(248, 250, 252);
            picMachineArea.Controls.Add(btnChangeMachineImage);
            picMachineArea.Dock = DockStyle.Fill;
            picMachineArea.Location = new Point(0, 531);
            picMachineArea.Margin = new Padding(0, 0, 0, 26);
            picMachineArea.Name = "picMachineArea";
            picMachineArea.Size = new Size(787, 403);
            picMachineArea.TabIndex = 2;
            picMachineArea.TabStop = false;
            // 
            // btnChangeMachineImage
            // 
            btnChangeMachineImage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnChangeMachineImage.NormalBackColor = Color.FromArgb(248, 250, 252);
            btnChangeMachineImage.HoverBackColor = Color.FromArgb(241, 245, 249);
            btnChangeMachineImage.PressedBackColor = Color.FromArgb(226, 232, 240);
            btnChangeMachineImage.BorderRadius = 8;
            btnChangeMachineImage.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnChangeMachineImage.FlatStyle = FlatStyle.Flat;
            btnChangeMachineImage.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnChangeMachineImage.Location = new Point(1697, 10);
            btnChangeMachineImage.Name = "btnChangeMachineImage";
            btnChangeMachineImage.Size = new Size(180, 40);
            btnChangeMachineImage.TabIndex = 0;
            btnChangeMachineImage.Text = "📷 Change Image";
            btnChangeMachineImage.UseVisualStyleBackColor = false;
            btnChangeMachineImage.Visible = false;
            // 
            // HarnessGeneralTab
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanelGeneral);
            Name = "HarnessGeneralTab";
            Size = new Size(1625, 960);
            tableLayoutPanelGeneral.ResumeLayout(false);
            panelGeneralInfo.ResumeLayout(false);
            panelGeneralInfo.PerformLayout();
            panelGeneralTop.ResumeLayout(false);
            panelImages.ResumeLayout(false);
            tblImages.ResumeLayout(false);
            tblImages.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picHarnessImage).EndInit();
            picHarnessImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picMachineArea).EndInit();
            picMachineArea.ResumeLayout(false);
            ResumeLayout(false);
        }
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelGeneral;
        private System.Windows.Forms.Panel panelGeneralInfo;
        private System.Windows.Forms.FlowLayoutPanel panelGeneralTop;
        private AssemblyLineTester.Views.Controls.RoundedButton btnEditGeneral;
        private AssemblyLineTester.Views.Controls.RoundedButton btnDeleteHarness;
        private System.Windows.Forms.TableLayoutPanel tblGenInfo;
        private System.Windows.Forms.TableLayoutPanel tblImages;
        private System.Windows.Forms.Panel panelImages;
        private System.Windows.Forms.Label lblMachineArea;
        private System.Windows.Forms.PictureBox picMachineArea;
        private AssemblyLineTester.Views.Controls.RoundedButton btnChangeMachineImage;
        private System.Windows.Forms.Label lblHarnessImage;
        private System.Windows.Forms.PictureBox picHarnessImage;
        private AssemblyLineTester.Views.Controls.RoundedButton btnChangeHarnessImage;
    }
}
