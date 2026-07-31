using System.Drawing;
using System.Windows.Forms;

namespace AssemblyLineTester.Views.Modals
{
    partial class HarnessModalForm
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
            pnlCustomHeader = new Panel();
            lblCustomTitle = new Label();
            iconLogo = new FontAwesome.Sharp.IconPictureBox();
            pnlHeader = new Panel();
            lblSubtitle = new Label();
            lblTitle = new Label();
            lblName = new Label();
            txtName = new TextBox();
            lblProductModel = new Label();
            txtProductModel = new TextBox();
            lblProductType = new Label();
            txtProductType = new TextBox();
            lblArea = new Label();
            txtArea = new TextBox();
            lblConnectors = new Label();
            pnlConnectorPicker = new Panel();
            flpConnectorChips = new FlowLayoutPanel();
            lblConnectorArrow = new Label();
            lblImage = new Label();
            btnChooseImage = new Button();
            lstImages = new ListBox();
            btnRemoveImage = new Button();
            pnlFooter = new Panel();
            btnCancel = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnSave = new AssemblyLineTester.Views.Controls.RoundedButton();
            pnlCustomHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconLogo).BeginInit();
            pnlHeader.SuspendLayout();
            pnlConnectorPicker.SuspendLayout();
            pnlFooter.SuspendLayout();
            SuspendLayout();
            // 
            // pnlCustomHeader
            // 
            pnlCustomHeader.BackColor = Color.FromArgb(34, 40, 49);
            pnlCustomHeader.Controls.Add(lblCustomTitle);
            pnlCustomHeader.Controls.Add(iconLogo);
            pnlCustomHeader.Dock = DockStyle.Top;
            pnlCustomHeader.Location = new Point(0, 0);
            pnlCustomHeader.Margin = new Padding(5, 5, 5, 5);
            pnlCustomHeader.Name = "pnlCustomHeader";
            pnlCustomHeader.Size = new Size(1531, 128);
            pnlCustomHeader.TabIndex = 0;
            // 
            // lblCustomTitle
            // 
            lblCustomTitle.AutoSize = true;
            lblCustomTitle.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            lblCustomTitle.ForeColor = Color.White;
            lblCustomTitle.Location = new Point(145, 35);
            lblCustomTitle.Margin = new Padding(5, 0, 5, 0);
            lblCustomTitle.Name = "lblCustomTitle";
            lblCustomTitle.Size = new Size(318, 42);
            lblCustomTitle.TabIndex = 0;
            lblCustomTitle.Text = "ADD/EDIT HARNESS";
            // 
            // iconLogo
            // 
            iconLogo.BackColor = Color.Transparent;
            iconLogo.ForeColor = Color.FromArgb(250, 204, 21);
            iconLogo.IconChar = FontAwesome.Sharp.IconChar.DiagramProject;
            iconLogo.IconColor = Color.FromArgb(250, 204, 21);
            iconLogo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconLogo.IconSize = 72;
            iconLogo.Location = new Point(39, 29);
            iconLogo.Margin = new Padding(5, 5, 5, 5);
            iconLogo.Name = "iconLogo";
            iconLogo.Size = new Size(78, 72);
            iconLogo.TabIndex = 1;
            iconLogo.TabStop = false;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 128);
            pnlHeader.Margin = new Padding(5, 5, 5, 5);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1531, 176);
            pnlHeader.TabIndex = 1;
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 9.5F);
            lblSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblSubtitle.Location = new Point(84, 104);
            lblSubtitle.Margin = new Padding(5, 0, 5, 0);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(513, 36);
            lblSubtitle.TabIndex = 0;
            lblSubtitle.Text = "Fill in product harness configuration details.";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblTitle.Location = new Point(80, 32);
            lblTitle.Margin = new Padding(5, 0, 5, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(444, 54);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Harness Specifications";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblName.ForeColor = Color.FromArgb(51, 65, 85);
            lblName.Location = new Point(93, 344);
            lblName.Margin = new Padding(5, 0, 5, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(210, 36);
            lblName.TabIndex = 15;
            lblName.Text = "Harness Name *";
            // 
            // txtName
            // 
            txtName.Font = new Font("Segoe UI", 10.5F);
            txtName.Location = new Point(93, 392);
            txtName.Margin = new Padding(5, 5, 5, 5);
            txtName.Name = "txtName";
            txtName.Size = new Size(628, 45);
            txtName.TabIndex = 2;
            // 
            // lblProductModel
            // 
            lblProductModel.AutoSize = true;
            lblProductModel.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblProductModel.ForeColor = Color.FromArgb(51, 65, 85);
            lblProductModel.Location = new Point(806, 344);
            lblProductModel.Margin = new Padding(5, 0, 5, 0);
            lblProductModel.Name = "lblProductModel";
            lblProductModel.Size = new Size(215, 36);
            lblProductModel.TabIndex = 14;
            lblProductModel.Text = "Product Model *";
            // 
            // txtProductModel
            // 
            txtProductModel.Font = new Font("Segoe UI", 10.5F);
            txtProductModel.Location = new Point(806, 392);
            txtProductModel.Margin = new Padding(5, 5, 5, 5);
            txtProductModel.Name = "txtProductModel";
            txtProductModel.Size = new Size(628, 45);
            txtProductModel.TabIndex = 3;
            // 
            // lblProductType
            // 
            lblProductType.AutoSize = true;
            lblProductType.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblProductType.ForeColor = Color.FromArgb(51, 65, 85);
            lblProductType.Location = new Point(93, 488);
            lblProductType.Margin = new Padding(5, 0, 5, 0);
            lblProductType.Name = "lblProductType";
            lblProductType.Size = new Size(176, 36);
            lblProductType.TabIndex = 13;
            lblProductType.Text = "Product Type";
            // 
            // txtProductType
            // 
            txtProductType.Font = new Font("Segoe UI", 10.5F);
            txtProductType.Location = new Point(93, 536);
            txtProductType.Margin = new Padding(5, 5, 5, 5);
            txtProductType.Name = "txtProductType";
            txtProductType.Size = new Size(628, 45);
            txtProductType.TabIndex = 4;
            // 
            // lblArea
            // 
            lblArea.AutoSize = true;
            lblArea.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblArea.ForeColor = Color.FromArgb(51, 65, 85);
            lblArea.Location = new Point(806, 488);
            lblArea.Margin = new Padding(5, 0, 5, 0);
            lblArea.Name = "lblArea";
            lblArea.Size = new Size(71, 36);
            lblArea.TabIndex = 12;
            lblArea.Text = "Area";
            // 
            // txtArea
            // 
            txtArea.Font = new Font("Segoe UI", 10.5F);
            txtArea.Location = new Point(806, 536);
            txtArea.Margin = new Padding(5, 5, 5, 5);
            txtArea.Name = "txtArea";
            txtArea.Size = new Size(628, 45);
            txtArea.TabIndex = 5;
            // 
            // lblConnectors
            // 
            lblConnectors.AutoSize = true;
            lblConnectors.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblConnectors.ForeColor = Color.FromArgb(51, 65, 85);
            lblConnectors.Location = new Point(93, 632);
            lblConnectors.Margin = new Padding(5, 0, 5, 0);
            lblConnectors.Name = "lblConnectors";
            lblConnectors.Size = new Size(439, 36);
            lblConnectors.TabIndex = 11;
            lblConnectors.Text = "Harness Connector(s) - click to add";
            // 
            // pnlConnectorPicker
            // 
            pnlConnectorPicker.BackColor = Color.White;
            pnlConnectorPicker.BorderStyle = BorderStyle.FixedSingle;
            pnlConnectorPicker.Controls.Add(flpConnectorChips);
            pnlConnectorPicker.Controls.Add(lblConnectorArrow);
            pnlConnectorPicker.Cursor = Cursors.Hand;
            pnlConnectorPicker.Location = new Point(93, 680);
            pnlConnectorPicker.Margin = new Padding(5, 5, 5, 5);
            pnlConnectorPicker.MinimumSize = new Size(629, 66);
            pnlConnectorPicker.Name = "pnlConnectorPicker";
            pnlConnectorPicker.Size = new Size(629, 66);
            pnlConnectorPicker.TabIndex = 6;
            // 
            // flpConnectorChips
            // 
            flpConnectorChips.BackColor = Color.White;
            flpConnectorChips.Cursor = Cursors.Hand;
            flpConnectorChips.Dock = DockStyle.Fill;
            flpConnectorChips.Location = new Point(0, 0);
            flpConnectorChips.Margin = new Padding(5, 5, 5, 5);
            flpConnectorChips.Name = "flpConnectorChips";
            flpConnectorChips.Padding = new Padding(10, 10, 39, 10);
            flpConnectorChips.Size = new Size(627, 64);
            flpConnectorChips.TabIndex = 0;
            // 
            // lblConnectorArrow
            // 
            lblConnectorArrow.Cursor = Cursors.Hand;
            lblConnectorArrow.Font = new Font("Segoe UI", 8F);
            lblConnectorArrow.ForeColor = Color.FromArgb(100, 116, 139);
            lblConnectorArrow.Location = new Point(588, 19);
            lblConnectorArrow.Margin = new Padding(5, 0, 5, 0);
            lblConnectorArrow.Name = "lblConnectorArrow";
            lblConnectorArrow.Size = new Size(32, 32);
            lblConnectorArrow.TabIndex = 1;
            lblConnectorArrow.Text = "▼";
            lblConnectorArrow.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblImage
            // 
            lblImage.AutoSize = true;
            lblImage.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblImage.ForeColor = Color.FromArgb(51, 65, 85);
            lblImage.Location = new Point(806, 632);
            lblImage.Margin = new Padding(5, 0, 5, 0);
            lblImage.MaximumSize = new Size(630, 0);
            lblImage.Name = "lblImage";
            lblImage.Size = new Size(529, 72);
            lblImage.TabIndex = 10;
            lblImage.Text = "Images (multiple allowed, max 2MB each, JPG/PNG)";
            // 
            // btnChooseImage
            // 
            btnChooseImage.Font = new Font("Segoe UI", 9F);
            btnChooseImage.Location = new Point(806, 709);
            btnChooseImage.Margin = new Padding(5, 5, 5, 5);
            btnChooseImage.Name = "btnChooseImage";
            btnChooseImage.Size = new Size(630, 67);
            btnChooseImage.TabIndex = 7;
            btnChooseImage.Text = "Choose Images...";
            // 
            // lstImages
            // 
            lstImages.Font = new Font("Segoe UI", 8.5F);
            lstImages.IntegralHeight = false;
            lstImages.Location = new Point(806, 808);
            lstImages.Margin = new Padding(5, 5, 5, 5);
            lstImages.Name = "lstImages";
            lstImages.Size = new Size(628, 305);
            lstImages.TabIndex = 8;
            // 
            // btnRemoveImage
            // 
            btnRemoveImage.Font = new Font("Segoe UI", 8.5F);
            btnRemoveImage.ForeColor = Color.FromArgb(239, 68, 68);
            btnRemoveImage.Location = new Point(806, 1131);
            btnRemoveImage.Margin = new Padding(5, 5, 5, 5);
            btnRemoveImage.Name = "btnRemoveImage";
            btnRemoveImage.Size = new Size(630, 61);
            btnRemoveImage.TabIndex = 9;
            btnRemoveImage.Text = "Remove Selected";
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.FromArgb(34, 40, 49);
            pnlFooter.Controls.Add(btnCancel);
            pnlFooter.Controls.Add(btnSave);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 1248);
            pnlFooter.Margin = new Padding(5, 5, 5, 5);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Size = new Size(1531, 160);
            pnlFooter.TabIndex = 8;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(241, 245, 249);
            btnCancel.BorderRadius = 20;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCancel.ForeColor = Color.FromArgb(71, 85, 105);
            btnCancel.HoverBackColor = Color.FromArgb(226, 232, 240);
            btnCancel.HoverForeColor = Color.Empty;
            btnCancel.Location = new Point(858, 35);
            btnCancel.Margin = new Padding(5, 5, 5, 5);
            btnCancel.Name = "btnCancel";
            btnCancel.NormalBackColor = Color.FromArgb(241, 245, 249);
            btnCancel.PressedBackColor = Color.FromArgb(203, 213, 225);
            btnCancel.PressedForeColor = Color.Empty;
            btnCancel.Size = new Size(278, 88);
            btnCancel.TabIndex = 0;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(250, 204, 21);
            btnSave.BorderRadius = 20;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnSave.ForeColor = Color.FromArgb(15, 23, 42);
            btnSave.HoverForeColor = Color.Empty;
            btnSave.Location = new Point(1162, 35);
            btnSave.Margin = new Padding(5, 5, 5, 5);
            btnSave.Name = "btnSave";
            btnSave.PressedForeColor = Color.Empty;
            btnSave.Size = new Size(278, 88);
            btnSave.TabIndex = 1;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // HarnessModalForm
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
            CancelButton = btnCancel;
            ClientSize = new Size(1531, 1408);
            Controls.Add(pnlFooter);
            Controls.Add(btnRemoveImage);
            Controls.Add(lstImages);
            Controls.Add(btnChooseImage);
            Controls.Add(lblImage);
            Controls.Add(pnlConnectorPicker);
            Controls.Add(lblConnectors);
            Controls.Add(txtArea);
            Controls.Add(lblArea);
            Controls.Add(txtProductType);
            Controls.Add(lblProductType);
            Controls.Add(txtProductModel);
            Controls.Add(lblProductModel);
            Controls.Add(txtName);
            Controls.Add(lblName);
            Controls.Add(pnlHeader);
            Controls.Add(pnlCustomHeader);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(5, 5, 5, 5);
            Name = "HarnessModalForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Harness Details";
            pnlCustomHeader.ResumeLayout(false);
            pnlCustomHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconLogo).EndInit();
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlConnectorPicker.ResumeLayout(false);
            pnlFooter.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private Panel pnlCustomHeader;
        private Label lblCustomTitle;
        private FontAwesome.Sharp.IconPictureBox iconLogo;
        private Panel pnlHeader;
        private Label lblTitle;
        private Label lblSubtitle;
        private Label lblName;
        private TextBox txtName;
        private Label lblProductModel;
        private TextBox txtProductModel;
        private Label lblProductType;
        private TextBox txtProductType;
        private Label lblArea;
        private TextBox txtArea;
        private Label lblConnectors;
        private Panel pnlConnectorPicker;
        private FlowLayoutPanel flpConnectorChips;
        private Label lblConnectorArrow;
        private Label lblImage;
        private Button btnChooseImage;
        private ListBox lstImages;
        private Button btnRemoveImage;
        private Panel pnlFooter;
        private AssemblyLineTester.Views.Controls.RoundedButton btnCancel;
        private AssemblyLineTester.Views.Controls.RoundedButton btnSave;
    }
}