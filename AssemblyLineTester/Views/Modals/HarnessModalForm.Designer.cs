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
            pnlCustomHeader.Margin = new Padding(3, 2, 3, 2);
            pnlCustomHeader.Name = "pnlCustomHeader";
            pnlCustomHeader.Size = new Size(824, 60);
            pnlCustomHeader.TabIndex = 0;
            // 
            // lblCustomTitle
            // 
            lblCustomTitle.AutoSize = true;
            lblCustomTitle.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            lblCustomTitle.ForeColor = Color.White;
            lblCustomTitle.Location = new Point(78, 16);
            lblCustomTitle.Name = "lblCustomTitle";
            lblCustomTitle.Size = new Size(164, 21);
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
            iconLogo.IconSize = 34;
            iconLogo.Location = new Point(21, 14);
            iconLogo.Margin = new Padding(3, 2, 3, 2);
            iconLogo.Name = "iconLogo";
            iconLogo.Size = new Size(42, 34);
            iconLogo.TabIndex = 1;
            iconLogo.TabStop = false;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 60);
            pnlHeader.Margin = new Padding(3, 2, 3, 2);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(824, 82);
            pnlHeader.TabIndex = 1;
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 9.5F);
            lblSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblSubtitle.Location = new Point(45, 49);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(262, 17);
            lblSubtitle.TabIndex = 0;
            lblSubtitle.Text = "Fill in product harness configuration details.";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblTitle.Location = new Point(43, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(225, 28);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Harness Specifications";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblName.ForeColor = Color.FromArgb(51, 65, 85);
            lblName.Location = new Point(50, 161);
            lblName.Name = "lblName";
            lblName.Size = new Size(107, 17);
            lblName.TabIndex = 15;
            lblName.Text = "Harness Name *";
            // 
            // txtName
            // 
            txtName.Font = new Font("Segoe UI", 10.5F);
            txtName.Location = new Point(50, 184);
            txtName.Margin = new Padding(3, 2, 3, 2);
            txtName.Name = "txtName";
            txtName.Size = new Size(340, 26);
            txtName.TabIndex = 2;
            // 
            // lblProductModel
            // 
            lblProductModel.AutoSize = true;
            lblProductModel.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblProductModel.ForeColor = Color.FromArgb(51, 65, 85);
            lblProductModel.Location = new Point(434, 161);
            lblProductModel.Name = "lblProductModel";
            lblProductModel.Size = new Size(109, 17);
            lblProductModel.TabIndex = 14;
            lblProductModel.Text = "Product Model *";
            // 
            // txtProductModel
            // 
            txtProductModel.Font = new Font("Segoe UI", 10.5F);
            txtProductModel.Location = new Point(434, 184);
            txtProductModel.Margin = new Padding(3, 2, 3, 2);
            txtProductModel.Name = "txtProductModel";
            txtProductModel.Size = new Size(340, 26);
            txtProductModel.TabIndex = 3;
            // 
            // lblProductType
            // 
            lblProductType.AutoSize = true;
            lblProductType.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblProductType.ForeColor = Color.FromArgb(51, 65, 85);
            lblProductType.Location = new Point(50, 229);
            lblProductType.Name = "lblProductType";
            lblProductType.Size = new Size(89, 17);
            lblProductType.TabIndex = 13;
            lblProductType.Text = "Product Type";
            // 
            // txtProductType
            // 
            txtProductType.Font = new Font("Segoe UI", 10.5F);
            txtProductType.Location = new Point(50, 251);
            txtProductType.Margin = new Padding(3, 2, 3, 2);
            txtProductType.Name = "txtProductType";
            txtProductType.Size = new Size(340, 26);
            txtProductType.TabIndex = 4;
            // 
            // lblArea
            // 
            lblArea.AutoSize = true;
            lblArea.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblArea.ForeColor = Color.FromArgb(51, 65, 85);
            lblArea.Location = new Point(434, 229);
            lblArea.Name = "lblArea";
            lblArea.Size = new Size(36, 17);
            lblArea.TabIndex = 12;
            lblArea.Text = "Area";
            // 
            // txtArea
            // 
            txtArea.Font = new Font("Segoe UI", 10.5F);
            txtArea.Location = new Point(434, 251);
            txtArea.Margin = new Padding(3, 2, 3, 2);
            txtArea.Name = "txtArea";
            txtArea.Size = new Size(340, 26);
            txtArea.TabIndex = 5;
            // 
            // lblConnectors
            // 
            lblConnectors.AutoSize = true;
            lblConnectors.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblConnectors.ForeColor = Color.FromArgb(51, 65, 85);
            lblConnectors.Location = new Point(50, 296);
            lblConnectors.Name = "lblConnectors";
            lblConnectors.Size = new Size(224, 17);
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
            pnlConnectorPicker.Location = new Point(50, 319);
            pnlConnectorPicker.Margin = new Padding(3, 2, 3, 2);
            pnlConnectorPicker.MinimumSize = new Size(340, 32);
            pnlConnectorPicker.Name = "pnlConnectorPicker";
            pnlConnectorPicker.Size = new Size(340, 32);
            pnlConnectorPicker.TabIndex = 6;
            // 
            // flpConnectorChips
            // 
            flpConnectorChips.BackColor = Color.White;
            flpConnectorChips.Cursor = Cursors.Hand;
            flpConnectorChips.Dock = DockStyle.Fill;
            flpConnectorChips.Location = new Point(0, 0);
            flpConnectorChips.Margin = new Padding(3, 2, 3, 2);
            flpConnectorChips.Name = "flpConnectorChips";
            flpConnectorChips.Padding = new Padding(5, 5, 21, 5);
            flpConnectorChips.Size = new Size(338, 30);
            flpConnectorChips.TabIndex = 0;
            // 
            // lblConnectorArrow
            // 
            lblConnectorArrow.Cursor = Cursors.Hand;
            lblConnectorArrow.Font = new Font("Segoe UI", 8F);
            lblConnectorArrow.ForeColor = Color.FromArgb(100, 116, 139);
            lblConnectorArrow.Location = new Point(317, 9);
            lblConnectorArrow.Name = "lblConnectorArrow";
            lblConnectorArrow.Size = new Size(17, 15);
            lblConnectorArrow.TabIndex = 1;
            lblConnectorArrow.Text = "▼";
            lblConnectorArrow.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblImage
            // 
            lblImage.AutoSize = true;
            lblImage.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblImage.ForeColor = Color.FromArgb(51, 65, 85);
            lblImage.Location = new Point(434, 296);
            lblImage.MaximumSize = new Size(339, 0);
            lblImage.Name = "lblImage";
            lblImage.Size = new Size(331, 17);
            lblImage.TabIndex = 10;
            lblImage.Text = "Images (multiple allowed, max 2MB each, JPG/PNG)";
            // 
            // btnChooseImage
            // 
            btnChooseImage.Font = new Font("Segoe UI", 9F);
            btnChooseImage.Location = new Point(434, 332);
            btnChooseImage.Margin = new Padding(3, 2, 3, 2);
            btnChooseImage.Name = "btnChooseImage";
            btnChooseImage.Size = new Size(339, 31);
            btnChooseImage.TabIndex = 7;
            btnChooseImage.Text = "Choose Images...";
            // 
            // lstImages
            // 
            lstImages.Font = new Font("Segoe UI", 8.5F);
            lstImages.IntegralHeight = false;
            lstImages.Location = new Point(434, 379);
            lstImages.Margin = new Padding(3, 2, 3, 2);
            lstImages.Name = "lstImages";
            lstImages.Size = new Size(340, 145);
            lstImages.TabIndex = 8;
            // 
            // btnRemoveImage
            // 
            btnRemoveImage.Font = new Font("Segoe UI", 8.5F);
            btnRemoveImage.ForeColor = Color.FromArgb(239, 68, 68);
            btnRemoveImage.Location = new Point(434, 530);
            btnRemoveImage.Margin = new Padding(3, 2, 3, 2);
            btnRemoveImage.Name = "btnRemoveImage";
            btnRemoveImage.Size = new Size(339, 29);
            btnRemoveImage.TabIndex = 9;
            btnRemoveImage.Text = "Remove Selected";
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.FromArgb(34, 40, 49);
            pnlFooter.Controls.Add(btnCancel);
            pnlFooter.Controls.Add(btnSave);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 580);
            pnlFooter.Margin = new Padding(3, 2, 3, 2);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Size = new Size(824, 75);
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
            btnCancel.Location = new Point(462, 16);
            btnCancel.Margin = new Padding(3, 2, 3, 2);
            btnCancel.Name = "btnCancel";
            btnCancel.NormalBackColor = Color.FromArgb(241, 245, 249);
            btnCancel.PressedBackColor = Color.FromArgb(203, 213, 225);
            btnCancel.PressedForeColor = Color.Empty;
            btnCancel.Size = new Size(150, 41);
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
            btnSave.Location = new Point(626, 16);
            btnSave.Margin = new Padding(3, 2, 3, 2);
            btnSave.Name = "btnSave";
            btnSave.PressedForeColor = Color.Empty;
            btnSave.Size = new Size(150, 41);
            btnSave.TabIndex = 1;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // HarnessModalForm
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
            CancelButton = btnCancel;
            ClientSize = new Size(824, 655);
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
            Margin = new Padding(3, 2, 3, 2);
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