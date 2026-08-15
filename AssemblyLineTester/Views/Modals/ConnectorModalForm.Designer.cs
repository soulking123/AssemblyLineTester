using System.Drawing;
using System.Windows.Forms;

namespace AssemblyLineTester.Views.Modals
{
    partial class ConnectorModalForm
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
            lblPerf = new Label();
            txtPerf = new TextBox();
            lblPartNumber = new Label();
            txtPartNumber = new TextBox();
            lblContactType = new Label();
            txtContactType = new TextBox();
            lblContactQuantity = new Label();
            txtContactQuantity = new TextBox();
            lblWireSize = new Label();
            txtWireSize = new TextBox();
            lblCurrentRating = new Label();
            txtCurrentRating = new TextBox();
            lblMountingStyle = new Label();
            txtMountingStyle = new TextBox();
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
            pnlCustomHeader.Margin = new Padding(2, 1, 2, 1);
            pnlCustomHeader.Name = "pnlCustomHeader";
            pnlCustomHeader.Size = new Size(491, 38);
            pnlCustomHeader.TabIndex = 20;
            // 
            // lblCustomTitle
            // 
            lblCustomTitle.AutoSize = true;
            lblCustomTitle.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            lblCustomTitle.ForeColor = Color.White;
            lblCustomTitle.Location = new Point(48, 10);
            lblCustomTitle.Margin = new Padding(2, 0, 2, 0);
            lblCustomTitle.Name = "lblCustomTitle";
            lblCustomTitle.Size = new Size(188, 21);
            lblCustomTitle.TabIndex = 0;
            lblCustomTitle.Text = "ADD/EDIT CONNECTOR";
            // 
            // iconLogo
            // 
            iconLogo.BackColor = Color.Transparent;
            iconLogo.ForeColor = Color.FromArgb(250, 204, 21);
            iconLogo.IconChar = FontAwesome.Sharp.IconChar.Plug;
            iconLogo.IconColor = Color.FromArgb(250, 204, 21);
            iconLogo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconLogo.IconSize = 24;
            iconLogo.Location = new Point(13, 7);
            iconLogo.Margin = new Padding(2, 1, 2, 1);
            iconLogo.Name = "iconLogo";
            iconLogo.Size = new Size(28, 24);
            iconLogo.TabIndex = 1;
            iconLogo.TabStop = false;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 38);
            pnlHeader.Margin = new Padding(2, 1, 2, 1);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(491, 64);
            pnlHeader.TabIndex = 19;
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 9.5F);
            lblSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblSubtitle.Location = new Point(28, 36);
            lblSubtitle.Margin = new Padding(2, 0, 2, 0);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(206, 17);
            lblSubtitle.TabIndex = 0;
            lblSubtitle.Text = "Enter the connector details below.";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblTitle.Location = new Point(26, 11);
            lblTitle.Margin = new Padding(2, 0, 2, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(181, 28);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Connector Details";
            // 
            // lblPerf
            // 
            lblPerf.AutoSize = true;
            lblPerf.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblPerf.ForeColor = Color.FromArgb(51, 65, 85);
            lblPerf.Location = new Point(31, 111);
            lblPerf.Margin = new Padding(2, 0, 2, 0);
            lblPerf.Name = "lblPerf";
            lblPerf.Size = new Size(43, 17);
            lblPerf.TabIndex = 18;
            lblPerf.Text = "Perf *";
            // 
            // txtPerf
            // 
            txtPerf.Font = new Font("Segoe UI", 10.5F);
            txtPerf.Location = new Point(31, 129);
            txtPerf.Margin = new Padding(2, 1, 2, 1);
            txtPerf.Name = "txtPerf";
            txtPerf.Size = new Size(211, 26);
            txtPerf.TabIndex = 17;
            // 
            // lblPartNumber
            // 
            lblPartNumber.AutoSize = true;
            lblPartNumber.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblPartNumber.ForeColor = Color.FromArgb(51, 65, 85);
            lblPartNumber.Location = new Point(267, 111);
            lblPartNumber.Margin = new Padding(2, 0, 2, 0);
            lblPartNumber.Name = "lblPartNumber";
            lblPartNumber.Size = new Size(97, 17);
            lblPartNumber.TabIndex = 16;
            lblPartNumber.Text = "Part Number *";
            // 
            // txtPartNumber
            // 
            txtPartNumber.Font = new Font("Segoe UI", 10.5F);
            txtPartNumber.Location = new Point(267, 129);
            txtPartNumber.Margin = new Padding(2, 1, 2, 1);
            txtPartNumber.Name = "txtPartNumber";
            txtPartNumber.Size = new Size(211, 26);
            txtPartNumber.TabIndex = 15;
            // 
            // lblContactType
            // 
            lblContactType.AutoSize = true;
            lblContactType.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblContactType.ForeColor = Color.FromArgb(51, 65, 85);
            lblContactType.Location = new Point(31, 158);
            lblContactType.Margin = new Padding(2, 0, 2, 0);
            lblContactType.Name = "lblContactType";
            lblContactType.Size = new Size(98, 17);
            lblContactType.TabIndex = 14;
            lblContactType.Text = "Contact Type *";
            // 
            // txtContactType
            // 
            txtContactType.Font = new Font("Segoe UI", 10.5F);
            txtContactType.Location = new Point(31, 176);
            txtContactType.Margin = new Padding(2, 1, 2, 1);
            txtContactType.Name = "txtContactType";
            txtContactType.Size = new Size(211, 26);
            txtContactType.TabIndex = 13;
            // 
            // lblContactQuantity
            // 
            lblContactQuantity.AutoSize = true;
            lblContactQuantity.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblContactQuantity.ForeColor = Color.FromArgb(51, 65, 85);
            lblContactQuantity.Location = new Point(267, 158);
            lblContactQuantity.Margin = new Padding(2, 0, 2, 0);
            lblContactQuantity.Name = "lblContactQuantity";
            lblContactQuantity.Size = new Size(123, 17);
            lblContactQuantity.TabIndex = 12;
            lblContactQuantity.Text = "Contact Quantity *";
            // 
            // txtContactQuantity
            // 
            txtContactQuantity.Font = new Font("Segoe UI", 10.5F);
            txtContactQuantity.Location = new Point(267, 176);
            txtContactQuantity.Margin = new Padding(2, 1, 2, 1);
            txtContactQuantity.Name = "txtContactQuantity";
            txtContactQuantity.Size = new Size(211, 26);
            txtContactQuantity.TabIndex = 11;
            // 
            // lblWireSize
            // 
            lblWireSize.AutoSize = true;
            lblWireSize.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblWireSize.ForeColor = Color.FromArgb(51, 65, 85);
            lblWireSize.Location = new Point(31, 204);
            lblWireSize.Margin = new Padding(2, 0, 2, 0);
            lblWireSize.Name = "lblWireSize";
            lblWireSize.Size = new Size(75, 17);
            lblWireSize.TabIndex = 10;
            lblWireSize.Text = "Wire Size *";
            // 
            // txtWireSize
            // 
            txtWireSize.Font = new Font("Segoe UI", 10.5F);
            txtWireSize.Location = new Point(31, 223);
            txtWireSize.Margin = new Padding(2, 1, 2, 1);
            txtWireSize.Name = "txtWireSize";
            txtWireSize.Size = new Size(211, 26);
            txtWireSize.TabIndex = 9;
            // 
            // lblCurrentRating
            // 
            lblCurrentRating.AutoSize = true;
            lblCurrentRating.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblCurrentRating.ForeColor = Color.FromArgb(51, 65, 85);
            lblCurrentRating.Location = new Point(267, 204);
            lblCurrentRating.Margin = new Padding(2, 0, 2, 0);
            lblCurrentRating.Name = "lblCurrentRating";
            lblCurrentRating.Size = new Size(108, 17);
            lblCurrentRating.TabIndex = 8;
            lblCurrentRating.Text = "Current Rating *";
            // 
            // txtCurrentRating
            // 
            txtCurrentRating.Font = new Font("Segoe UI", 10.5F);
            txtCurrentRating.Location = new Point(267, 223);
            txtCurrentRating.Margin = new Padding(2, 1, 2, 1);
            txtCurrentRating.Name = "txtCurrentRating";
            txtCurrentRating.Size = new Size(211, 26);
            txtCurrentRating.TabIndex = 7;
            // 
            // lblMountingStyle
            // 
            lblMountingStyle.AutoSize = true;
            lblMountingStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblMountingStyle.ForeColor = Color.FromArgb(51, 65, 85);
            lblMountingStyle.Location = new Point(31, 251);
            lblMountingStyle.Margin = new Padding(2, 0, 2, 0);
            lblMountingStyle.Name = "lblMountingStyle";
            lblMountingStyle.Size = new Size(113, 17);
            lblMountingStyle.TabIndex = 6;
            lblMountingStyle.Text = "Mounting Style *";
            // 
            // txtMountingStyle
            // 
            txtMountingStyle.Font = new Font("Segoe UI", 10.5F);
            txtMountingStyle.Location = new Point(31, 270);
            txtMountingStyle.Margin = new Padding(2, 1, 2, 1);
            txtMountingStyle.Name = "txtMountingStyle";
            txtMountingStyle.Size = new Size(211, 26);
            txtMountingStyle.TabIndex = 5;
            // 
            // lblImage
            // 
            lblImage.AutoSize = true;
            lblImage.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblImage.ForeColor = Color.FromArgb(51, 65, 85);
            lblImage.Location = new Point(267, 251);
            lblImage.Margin = new Padding(2, 0, 2, 0);
            lblImage.MaximumSize = new Size(209, 0);
            lblImage.Name = "lblImage";
            lblImage.Size = new Size(208, 30);
            lblImage.TabIndex = 4;
            lblImage.Text = "Images (multiple allowed, max 2MB each, JPG/PNG)";
            // 
            // btnChooseImage
            // 
            btnChooseImage.Font = new Font("Segoe UI", 9F);
            btnChooseImage.Location = new Point(269, 282);
            btnChooseImage.Margin = new Padding(2, 1, 2, 1);
            btnChooseImage.Name = "btnChooseImage";
            btnChooseImage.Size = new Size(207, 29);
            btnChooseImage.TabIndex = 3;
            btnChooseImage.Text = "Choose Images...";
            // 
            // lstImages
            // 
            lstImages.Font = new Font("Segoe UI", 8.5F);
            lstImages.IntegralHeight = false;
            lstImages.Location = new Point(269, 313);
            lstImages.Margin = new Padding(2, 1, 2, 1);
            lstImages.Name = "lstImages";
            lstImages.Size = new Size(209, 94);
            lstImages.TabIndex = 2;
            // 
            // btnRemoveImage
            // 
            btnRemoveImage.Font = new Font("Segoe UI", 8.5F);
            btnRemoveImage.ForeColor = Color.FromArgb(239, 68, 68);
            btnRemoveImage.Location = new Point(267, 411);
            btnRemoveImage.Margin = new Padding(2, 1, 2, 1);
            btnRemoveImage.Name = "btnRemoveImage";
            btnRemoveImage.Size = new Size(213, 27);
            btnRemoveImage.TabIndex = 6;
            btnRemoveImage.Text = "Remove Selected";
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.FromArgb(34, 40, 49);
            pnlFooter.Controls.Add(btnCancel);
            pnlFooter.Controls.Add(btnSave);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 440);
            pnlFooter.Margin = new Padding(2, 1, 2, 1);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Size = new Size(491, 60);
            pnlFooter.TabIndex = 0;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(241, 245, 249);
            btnCancel.BorderRadius = 20;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCancel.ForeColor = Color.FromArgb(71, 85, 105);
            btnCancel.HoverBackColor = Color.FromArgb(226, 232, 240);
            btnCancel.HoverForeColor = Color.Empty;
            btnCancel.Location = new Point(284, 14);
            btnCancel.Margin = new Padding(2, 1, 2, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.NormalBackColor = Color.FromArgb(241, 245, 249);
            btnCancel.PressedBackColor = Color.FromArgb(203, 213, 225);
            btnCancel.PressedForeColor = Color.Empty;
            btnCancel.Size = new Size(92, 33);
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
            btnSave.Location = new Point(385, 14);
            btnSave.Margin = new Padding(2, 1, 2, 1);
            btnSave.Name = "btnSave";
            btnSave.PressedForeColor = Color.Empty;
            btnSave.Size = new Size(92, 33);
            btnSave.TabIndex = 1;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // ConnectorModalForm
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
            CancelButton = btnCancel;
            ClientSize = new Size(491, 500);
            Controls.Add(pnlFooter);
            Controls.Add(btnRemoveImage);
            Controls.Add(lstImages);
            Controls.Add(btnChooseImage);
            Controls.Add(lblImage);
            Controls.Add(txtMountingStyle);
            Controls.Add(lblMountingStyle);
            Controls.Add(txtCurrentRating);
            Controls.Add(lblCurrentRating);
            Controls.Add(txtWireSize);
            Controls.Add(lblWireSize);
            Controls.Add(txtContactQuantity);
            Controls.Add(lblContactQuantity);
            Controls.Add(txtContactType);
            Controls.Add(lblContactType);
            Controls.Add(txtPartNumber);
            Controls.Add(lblPartNumber);
            Controls.Add(txtPerf);
            Controls.Add(lblPerf);
            Controls.Add(pnlHeader);
            Controls.Add(pnlCustomHeader);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2, 1, 2, 1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ConnectorModalForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Connector Details";
            pnlCustomHeader.ResumeLayout(false);
            pnlCustomHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconLogo).EndInit();
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlFooter.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblPerf;
        private System.Windows.Forms.TextBox txtPerf;
        private System.Windows.Forms.Label lblPartNumber;
        private System.Windows.Forms.TextBox txtPartNumber;
        private System.Windows.Forms.Label lblContactType;
        private System.Windows.Forms.TextBox txtContactType;
        private System.Windows.Forms.Label lblContactQuantity;
        private System.Windows.Forms.TextBox txtContactQuantity;
        private System.Windows.Forms.Label lblWireSize;
        private System.Windows.Forms.TextBox txtWireSize;
        private System.Windows.Forms.Label lblCurrentRating;
        private System.Windows.Forms.TextBox txtCurrentRating;
        private System.Windows.Forms.Label lblMountingStyle;
        private System.Windows.Forms.TextBox txtMountingStyle;
        private System.Windows.Forms.Label lblImage;
        private System.Windows.Forms.Button btnChooseImage;
        private System.Windows.Forms.ListBox lstImages;
        private System.Windows.Forms.Button btnRemoveImage;

        private System.Windows.Forms.Panel pnlFooter;
        private AssemblyLineTester.Views.Controls.RoundedButton btnCancel;
        private AssemblyLineTester.Views.Controls.RoundedButton btnSave;
        private System.Windows.Forms.Panel pnlCustomHeader;
        private FontAwesome.Sharp.IconPictureBox iconLogo;
        private System.Windows.Forms.Label lblCustomTitle;
    }
}