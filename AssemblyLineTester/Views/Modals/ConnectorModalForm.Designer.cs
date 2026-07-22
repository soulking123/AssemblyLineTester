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
            btnClose = new FontAwesome.Sharp.IconButton();
            pnlHeader = new Panel();
            lblSubtitle = new Label();
            lblTitle = new Label();
            lblPerf = new Label();
            txtPerf = new TextBox();
            lblPartNumber = new Label();
            txtPartNumber = new TextBox();
            lblContactType = new Label();
            cmbContactType = new ComboBox();
            lblContactQuality = new Label();
            cmbContactQuality = new ComboBox();
            lblWireSize = new Label();
            cmbWireSize = new ComboBox();
            lblCurrentRating = new Label();
            cmbCurrentRating = new ComboBox();
            lblMountingStyle = new Label();
            cmbMountingStyle = new ComboBox();
            lblImage = new Label();
            btnChooseImage = new Button();
            lblImagePath = new Label();
            picPreview = new PictureBox();
            pnlFooter = new Panel();
            btnCancel = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnSave = new AssemblyLineTester.Views.Controls.RoundedButton();
            pnlCustomHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconLogo).BeginInit();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picPreview).BeginInit();
            pnlFooter.SuspendLayout();
            SuspendLayout();
            // 
            // pnlCustomHeader
            // 
            pnlCustomHeader.BackColor = Color.FromArgb(34, 40, 49);
            pnlCustomHeader.Controls.Add(lblCustomTitle);
            pnlCustomHeader.Controls.Add(iconLogo);
            pnlCustomHeader.Controls.Add(btnClose);
            pnlCustomHeader.Dock = DockStyle.Top;
            pnlCustomHeader.Location = new Point(0, 0);
            pnlCustomHeader.Name = "pnlCustomHeader";
            pnlCustomHeader.Size = new Size(942, 80);
            pnlCustomHeader.TabIndex = 20;
            // 
            // lblCustomTitle
            // 
            lblCustomTitle.AutoSize = true;
            lblCustomTitle.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            lblCustomTitle.ForeColor = Color.White;
            lblCustomTitle.Location = new Point(89, 22);
            lblCustomTitle.Name = "lblCustomTitle";
            lblCustomTitle.Size = new Size(365, 42);
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
            iconLogo.IconSize = 51;
            iconLogo.Location = new Point(24, 14);
            iconLogo.Name = "iconLogo";
            iconLogo.Size = new Size(52, 51);
            iconLogo.TabIndex = 1;
            iconLogo.TabStop = false;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.BackColor = Color.Transparent;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.IconChar = FontAwesome.Sharp.IconChar.Close;
            btnClose.IconColor = Color.FromArgb(203, 213, 225);
            btnClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnClose.IconSize = 24;
            btnClose.Location = new Point(861, 0);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(81, 80);
            btnClose.TabIndex = 2;
            btnClose.UseVisualStyleBackColor = false;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 80);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(942, 136);
            pnlHeader.TabIndex = 19;
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 9.5F);
            lblSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblSubtitle.Location = new Point(52, 77);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(406, 36);
            lblSubtitle.TabIndex = 0;
            lblSubtitle.Text = "Enter the connector details below.";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblTitle.Location = new Point(49, 24);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(358, 54);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Connector Details";
            // 
            // lblPerf
            // 
            lblPerf.AutoSize = true;
            lblPerf.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblPerf.ForeColor = Color.FromArgb(51, 65, 85);
            lblPerf.Location = new Point(57, 236);
            lblPerf.Name = "lblPerf";
            lblPerf.Size = new Size(84, 36);
            lblPerf.TabIndex = 18;
            lblPerf.Text = "Perf *";
            // 
            // txtPerf
            // 
            txtPerf.Font = new Font("Segoe UI", 10.5F);
            txtPerf.Location = new Point(57, 275);
            txtPerf.Name = "txtPerf";
            txtPerf.Size = new Size(388, 45);
            txtPerf.TabIndex = 17;
            // 
            // lblPartNumber
            // 
            lblPartNumber.AutoSize = true;
            lblPartNumber.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblPartNumber.ForeColor = Color.FromArgb(51, 65, 85);
            lblPartNumber.Location = new Point(496, 236);
            lblPartNumber.Name = "lblPartNumber";
            lblPartNumber.Size = new Size(192, 36);
            lblPartNumber.TabIndex = 16;
            lblPartNumber.Text = "Part Number *";
            // 
            // txtPartNumber
            // 
            txtPartNumber.Font = new Font("Segoe UI", 10.5F);
            txtPartNumber.Location = new Point(496, 275);
            txtPartNumber.Name = "txtPartNumber";
            txtPartNumber.Size = new Size(388, 45);
            txtPartNumber.TabIndex = 15;
            // 
            // lblContactType
            // 
            lblContactType.AutoSize = true;
            lblContactType.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblContactType.ForeColor = Color.FromArgb(51, 65, 85);
            lblContactType.Location = new Point(57, 336);
            lblContactType.Name = "lblContactType";
            lblContactType.Size = new Size(193, 36);
            lblContactType.TabIndex = 14;
            lblContactType.Text = "Contact Type *";
            // 
            // cmbContactType
            // 
            cmbContactType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbContactType.Font = new Font("Segoe UI", 10.5F);
            cmbContactType.Location = new Point(57, 375);
            cmbContactType.Name = "cmbContactType";
            cmbContactType.Size = new Size(388, 46);
            cmbContactType.TabIndex = 13;
            // 
            // lblContactQuality
            // 
            lblContactQuality.AutoSize = true;
            lblContactQuality.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblContactQuality.ForeColor = Color.FromArgb(51, 65, 85);
            lblContactQuality.Location = new Point(496, 336);
            lblContactQuality.Name = "lblContactQuality";
            lblContactQuality.Size = new Size(223, 36);
            lblContactQuality.TabIndex = 12;
            lblContactQuality.Text = "Contact Quality *";
            // 
            // cmbContactQuality
            // 
            cmbContactQuality.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbContactQuality.Font = new Font("Segoe UI", 10.5F);
            cmbContactQuality.Location = new Point(496, 375);
            cmbContactQuality.Name = "cmbContactQuality";
            cmbContactQuality.Size = new Size(388, 46);
            cmbContactQuality.TabIndex = 11;
            // 
            // lblWireSize
            // 
            lblWireSize.AutoSize = true;
            lblWireSize.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblWireSize.ForeColor = Color.FromArgb(51, 65, 85);
            lblWireSize.Location = new Point(57, 436);
            lblWireSize.Name = "lblWireSize";
            lblWireSize.Size = new Size(146, 36);
            lblWireSize.TabIndex = 10;
            lblWireSize.Text = "Wire Size *";
            // 
            // cmbWireSize
            // 
            cmbWireSize.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbWireSize.Font = new Font("Segoe UI", 10.5F);
            cmbWireSize.Location = new Point(57, 475);
            cmbWireSize.Name = "cmbWireSize";
            cmbWireSize.Size = new Size(388, 46);
            cmbWireSize.TabIndex = 9;
            // 
            // lblCurrentRating
            // 
            lblCurrentRating.AutoSize = true;
            lblCurrentRating.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblCurrentRating.ForeColor = Color.FromArgb(51, 65, 85);
            lblCurrentRating.Location = new Point(496, 436);
            lblCurrentRating.Name = "lblCurrentRating";
            lblCurrentRating.Size = new Size(213, 36);
            lblCurrentRating.TabIndex = 8;
            lblCurrentRating.Text = "Current Rating *";
            // 
            // cmbCurrentRating
            // 
            cmbCurrentRating.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCurrentRating.Font = new Font("Segoe UI", 10.5F);
            cmbCurrentRating.Location = new Point(496, 475);
            cmbCurrentRating.Name = "cmbCurrentRating";
            cmbCurrentRating.Size = new Size(388, 46);
            cmbCurrentRating.TabIndex = 7;
            // 
            // lblMountingStyle
            // 
            lblMountingStyle.AutoSize = true;
            lblMountingStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblMountingStyle.ForeColor = Color.FromArgb(51, 65, 85);
            lblMountingStyle.Location = new Point(57, 536);
            lblMountingStyle.Name = "lblMountingStyle";
            lblMountingStyle.Size = new Size(222, 36);
            lblMountingStyle.TabIndex = 6;
            lblMountingStyle.Text = "Mounting Style *";
            // 
            // cmbMountingStyle
            // 
            cmbMountingStyle.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMountingStyle.Font = new Font("Segoe UI", 10.5F);
            cmbMountingStyle.Location = new Point(52, 575);
            cmbMountingStyle.Name = "cmbMountingStyle";
            cmbMountingStyle.Size = new Size(388, 46);
            cmbMountingStyle.TabIndex = 5;
            // 
            // lblImage
            // 
            lblImage.AutoSize = true;
            lblImage.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblImage.ForeColor = Color.FromArgb(51, 65, 85);
            lblImage.Location = new Point(496, 536);
            lblImage.Name = "lblImage";
            lblImage.Size = new Size(362, 36);
            lblImage.TabIndex = 4;
            lblImage.Text = "Image (Max 2MB, JPG/PNG)";
            // 
            // btnChooseImage
            // 
            btnChooseImage.Font = new Font("Segoe UI", 9F);
            btnChooseImage.Location = new Point(500, 575);
            btnChooseImage.Name = "btnChooseImage";
            btnChooseImage.Size = new Size(150, 45);
            btnChooseImage.TabIndex = 3;
            btnChooseImage.Text = "Choose Image";
            // 
            // lblImagePath
            // 
            lblImagePath.AutoSize = true;
            lblImagePath.Font = new Font("Segoe UI", 8F);
            lblImagePath.ForeColor = Color.Gray;
            lblImagePath.Location = new Point(500, 629);
            lblImagePath.Name = "lblImagePath";
            lblImagePath.Size = new Size(151, 30);
            lblImagePath.TabIndex = 2;
            lblImagePath.Text = "No file chosen";
            // 
            // picPreview
            // 
            picPreview.BorderStyle = BorderStyle.FixedSingle;
            picPreview.Location = new Point(664, 575);
            picPreview.Name = "picPreview";
            picPreview.Size = new Size(220, 150);
            picPreview.SizeMode = PictureBoxSizeMode.Zoom;
            picPreview.TabIndex = 1;
            picPreview.TabStop = false;
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.FromArgb(34, 40, 49);
            pnlFooter.Controls.Add(btnCancel);
            pnlFooter.Controls.Add(btnSave);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 800);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Size = new Size(942, 128);
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
            btnCancel.Location = new Point(528, 29);
            btnCancel.Name = "btnCancel";
            btnCancel.NormalBackColor = Color.FromArgb(241, 245, 249);
            btnCancel.PressedBackColor = Color.FromArgb(203, 213, 225);
            btnCancel.PressedForeColor = Color.Empty;
            btnCancel.Size = new Size(171, 70);
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
            btnSave.Location = new Point(715, 29);
            btnSave.Name = "btnSave";
            btnSave.PressedForeColor = Color.Empty;
            btnSave.Size = new Size(171, 70);
            btnSave.TabIndex = 1;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // ConnectorModalForm
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
            CancelButton = btnCancel;
            ClientSize = new Size(942, 928);
            Controls.Add(pnlFooter);
            Controls.Add(picPreview);
            Controls.Add(lblImagePath);
            Controls.Add(btnChooseImage);
            Controls.Add(lblImage);
            Controls.Add(cmbMountingStyle);
            Controls.Add(lblMountingStyle);
            Controls.Add(cmbCurrentRating);
            Controls.Add(lblCurrentRating);
            Controls.Add(cmbWireSize);
            Controls.Add(lblWireSize);
            Controls.Add(cmbContactQuality);
            Controls.Add(lblContactQuality);
            Controls.Add(cmbContactType);
            Controls.Add(lblContactType);
            Controls.Add(txtPartNumber);
            Controls.Add(lblPartNumber);
            Controls.Add(txtPerf);
            Controls.Add(lblPerf);
            Controls.Add(pnlHeader);
            Controls.Add(pnlCustomHeader);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.None;
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
            ((System.ComponentModel.ISupportInitialize)picPreview).EndInit();
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
        private System.Windows.Forms.ComboBox cmbContactType;
        private System.Windows.Forms.Label lblContactQuality;
        private System.Windows.Forms.ComboBox cmbContactQuality;
        private System.Windows.Forms.Label lblWireSize;
        private System.Windows.Forms.ComboBox cmbWireSize;
        private System.Windows.Forms.Label lblCurrentRating;
        private System.Windows.Forms.ComboBox cmbCurrentRating;
        private System.Windows.Forms.Label lblMountingStyle;
        private System.Windows.Forms.ComboBox cmbMountingStyle;
        private System.Windows.Forms.Label lblImage;
        private System.Windows.Forms.Button btnChooseImage;
        private System.Windows.Forms.Label lblImagePath;
        private System.Windows.Forms.PictureBox picPreview;
        
        private System.Windows.Forms.Panel pnlFooter;
        private AssemblyLineTester.Views.Controls.RoundedButton btnCancel;
        private AssemblyLineTester.Views.Controls.RoundedButton btnSave;
        private System.Windows.Forms.Panel pnlCustomHeader;
        private FontAwesome.Sharp.IconPictureBox iconLogo;
        private System.Windows.Forms.Label lblCustomTitle;
        private FontAwesome.Sharp.IconButton btnClose;
    }
}
