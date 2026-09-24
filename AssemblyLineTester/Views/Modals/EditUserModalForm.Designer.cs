using System.Drawing;
using System.Windows.Forms;

namespace AssemblyLineTester.Views.Modals
{
    partial class EditUserModalForm
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
            iconLogo = new FontAwesome.Sharp.IconPictureBox();
            pnlHeader = new Panel();
            lblSubtitle = new Label();
            lblTitle = new Label();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblCompanyId = new Label();
            txtCompanyId = new TextBox();
            lblFullName = new Label();
            txtFullName = new TextBox();
            lblRole = new Label();
            cmbRole = new ComboBox();
            lblDepartment = new Label();
            cmbDepartment = new ComboBox();
            lblStatus = new Label();
            cmbStatus = new ComboBox();
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
            pnlCustomHeader.Margin = new Padding(5);
            pnlCustomHeader.Name = "pnlCustomHeader";
            pnlCustomHeader.Size = new Size(942, 80);
            pnlCustomHeader.TabIndex = 0;
            //
            // lblCustomTitle
            //
            lblCustomTitle.AutoSize = true;
            lblCustomTitle.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            lblCustomTitle.ForeColor = Color.White;
            lblCustomTitle.Location = new Point(89, 22);
            lblCustomTitle.Margin = new Padding(5, 0, 5, 0);
            lblCustomTitle.Name = "lblCustomTitle";
            lblCustomTitle.Size = new Size(298, 42);
            lblCustomTitle.TabIndex = 1;
            lblCustomTitle.Text = "EDIT SYSTEM USER";
            //
            // iconLogo
            //
            iconLogo.BackColor = Color.Transparent;
            iconLogo.ForeColor = Color.FromArgb(56, 189, 248);
            iconLogo.IconChar = FontAwesome.Sharp.IconChar.UserEdit;
            iconLogo.IconColor = Color.FromArgb(56, 189, 248);
            iconLogo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconLogo.IconSize = 51;
            iconLogo.Location = new Point(24, 14);
            iconLogo.Margin = new Padding(5);
            iconLogo.Name = "iconLogo";
            iconLogo.Size = new Size(52, 51);
            iconLogo.SizeMode = PictureBoxSizeMode.CenterImage;
            iconLogo.TabIndex = 0;
            iconLogo.TabStop = false;
            //
            // pnlHeader
            //
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 80);
            pnlHeader.Margin = new Padding(5);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(942, 136);
            pnlHeader.TabIndex = 0;
            //
            // lblSubtitle
            //
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 9.5F);
            lblSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblSubtitle.Location = new Point(52, 77);
            lblSubtitle.Margin = new Padding(5, 0, 5, 0);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(528, 36);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Update this account's details and role access.";
            //
            // lblTitle
            //
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblTitle.Location = new Point(49, 24);
            lblTitle.Margin = new Padding(5, 0, 5, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(344, 54);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Edit System User";
            //
            // lblUsername
            //
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblUsername.ForeColor = Color.FromArgb(51, 65, 85);
            lblUsername.Location = new Point(57, 256);
            lblUsername.Margin = new Padding(5, 0, 5, 0);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(156, 36);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "Username *";
            //
            // txtUsername
            //
            txtUsername.Font = new Font("Segoe UI", 10.5F);
            txtUsername.Location = new Point(57, 296);
            txtUsername.Margin = new Padding(5);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(388, 45);
            txtUsername.TabIndex = 2;
            //
            // lblCompanyId
            //
            lblCompanyId.AutoSize = true;
            lblCompanyId.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblCompanyId.ForeColor = Color.FromArgb(51, 65, 85);
            lblCompanyId.Location = new Point(496, 256);
            lblCompanyId.Margin = new Padding(5, 0, 5, 0);
            lblCompanyId.Name = "lblCompanyId";
            lblCompanyId.Size = new Size(165, 36);
            lblCompanyId.TabIndex = 3;
            lblCompanyId.Text = "Company ID";
            //
            // txtCompanyId
            //
            txtCompanyId.Font = new Font("Segoe UI", 10.5F);
            txtCompanyId.Location = new Point(496, 296);
            txtCompanyId.Margin = new Padding(5);
            txtCompanyId.Name = "txtCompanyId";
            txtCompanyId.Size = new Size(388, 45);
            txtCompanyId.TabIndex = 4;
            //
            // lblFullName
            //
            lblFullName.AutoSize = true;
            lblFullName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblFullName.ForeColor = Color.FromArgb(51, 65, 85);
            lblFullName.Location = new Point(57, 376);
            lblFullName.Margin = new Padding(5, 0, 5, 0);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(158, 36);
            lblFullName.TabIndex = 5;
            lblFullName.Text = "Full Name *";
            //
            // txtFullName
            //
            txtFullName.Font = new Font("Segoe UI", 10.5F);
            txtFullName.Location = new Point(57, 416);
            txtFullName.Margin = new Padding(5);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(826, 45);
            txtFullName.TabIndex = 6;
            //
            // lblRole
            //
            lblRole.AutoSize = true;
            lblRole.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblRole.ForeColor = Color.FromArgb(51, 65, 85);
            lblRole.Location = new Point(57, 496);
            lblRole.Margin = new Padding(5, 0, 5, 0);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(87, 36);
            lblRole.TabIndex = 11;
            lblRole.Text = "Role *";
            //
            // cmbRole
            //
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.Font = new Font("Segoe UI", 10F);
            cmbRole.FormattingEnabled = true;
            cmbRole.Location = new Point(57, 536);
            cmbRole.Margin = new Padding(5, 5, 5, 5);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new Size(388, 45);
            cmbRole.TabIndex = 12;
            //
            // lblDepartment
            //
            lblDepartment.AutoSize = true;
            lblDepartment.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblDepartment.ForeColor = Color.FromArgb(51, 65, 85);
            lblDepartment.Location = new Point(496, 496);
            lblDepartment.Margin = new Padding(5, 0, 5, 0);
            lblDepartment.Name = "lblDepartment";
            lblDepartment.Size = new Size(182, 36);
            lblDepartment.TabIndex = 13;
            lblDepartment.Text = "Department *";
            //
            // cmbDepartment
            //
            cmbDepartment.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDepartment.Font = new Font("Segoe UI", 10F);
            cmbDepartment.FormattingEnabled = true;
            cmbDepartment.Location = new Point(496, 536);
            cmbDepartment.Margin = new Padding(5, 5, 5, 5);
            cmbDepartment.Name = "cmbDepartment";
            cmbDepartment.Size = new Size(388, 45);
            cmbDepartment.TabIndex = 14;
            //
            // lblStatus
            //
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblStatus.ForeColor = Color.FromArgb(51, 65, 85);
            lblStatus.Location = new Point(57, 616);
            lblStatus.Margin = new Padding(5, 0, 5, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(109, 36);
            lblStatus.TabIndex = 15;
            lblStatus.Text = "Status *";
            //
            // cmbStatus
            //
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Font = new Font("Segoe UI", 10F);
            cmbStatus.FormattingEnabled = true;
            cmbStatus.Location = new Point(57, 656);
            cmbStatus.Margin = new Padding(5, 5, 5, 5);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(388, 45);
            cmbStatus.TabIndex = 16;
            //
            // pnlFooter
            //
            pnlFooter.BackColor = Color.FromArgb(34, 40, 49);
            pnlFooter.Controls.Add(btnCancel);
            pnlFooter.Controls.Add(btnSave);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 760);
            pnlFooter.Margin = new Padding(5, 5, 5, 5);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Size = new Size(942, 128);
            pnlFooter.TabIndex = 17;
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
            btnCancel.Margin = new Padding(5);
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
            btnSave.Margin = new Padding(5);
            btnSave.Name = "btnSave";
            btnSave.PressedForeColor = Color.Empty;
            btnSave.Size = new Size(171, 70);
            btnSave.TabIndex = 1;
            btnSave.Text = "Save Changes";
            btnSave.UseVisualStyleBackColor = false;
            //
            // EditUserModalForm
            //
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
            CancelButton = btnCancel;
            ClientSize = new Size(942, 888);
            Controls.Add(pnlFooter);
            Controls.Add(cmbStatus);
            Controls.Add(lblStatus);
            Controls.Add(cmbDepartment);
            Controls.Add(lblDepartment);
            Controls.Add(cmbRole);
            Controls.Add(lblRole);
            Controls.Add(txtFullName);
            Controls.Add(lblFullName);
            Controls.Add(txtCompanyId);
            Controls.Add(lblCompanyId);
            Controls.Add(txtUsername);
            Controls.Add(lblUsername);
            Controls.Add(pnlHeader);
            Controls.Add(pnlCustomHeader);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EditUserModalForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Edit System User";
            pnlCustomHeader.ResumeLayout(false);
            pnlCustomHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconLogo).EndInit();
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlFooter.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblCompanyId;
        private System.Windows.Forms.TextBox txtCompanyId;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.ComboBox cmbRole;
        private System.Windows.Forms.Label lblDepartment;
        private System.Windows.Forms.ComboBox cmbDepartment;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Panel pnlFooter;
        private AssemblyLineTester.Views.Controls.RoundedButton btnCancel;
        private AssemblyLineTester.Views.Controls.RoundedButton btnSave;
        private System.Windows.Forms.Panel pnlCustomHeader;
        private FontAwesome.Sharp.IconPictureBox iconLogo;
        private System.Windows.Forms.Label lblCustomTitle;
    }
}
