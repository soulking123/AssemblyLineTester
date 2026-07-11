using System.Drawing;
using System.Windows.Forms;

namespace AssemblyLineTester.Views
{
    partial class LoginForm
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
            pnlCard = new AssemblyLineTester.Views.Controls.RoundedPanel();
            logoBox = new PictureBox();
            lblTitle = new Label();
            lblSubtitle = new Label();
            lblUserId = new Label();
            pnlUsernameContainer = new Panel();
            txtUsername = new TextBox();
            iconUsername = new FontAwesome.Sharp.IconPictureBox();
            pnlUsernameLine = new Panel();
            lblPassword = new Label();
            pnlPasswordContainer = new Panel();
            txtPassword = new TextBox();
            iconPassword = new FontAwesome.Sharp.IconPictureBox();
            pnlPasswordLine = new Panel();
            btnLogin = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnClose = new FontAwesome.Sharp.IconButton();
            pnlCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)logoBox).BeginInit();
            pnlUsernameContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconUsername).BeginInit();
            pnlPasswordContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPassword).BeginInit();
            SuspendLayout();
            // 
            // pnlCard
            // 
            pnlCard.BackColor = Color.White;
            pnlCard.Controls.Add(logoBox);
            pnlCard.Controls.Add(lblTitle);
            pnlCard.Controls.Add(lblSubtitle);
            pnlCard.Controls.Add(lblUserId);
            pnlCard.Controls.Add(pnlUsernameContainer);
            pnlCard.Controls.Add(lblPassword);
            pnlCard.Controls.Add(pnlPasswordContainer);
            pnlCard.Controls.Add(btnLogin);
            pnlCard.Location = new Point(54, 66);
            pnlCard.Margin = new Padding(6);
            pnlCard.Name = "pnlCard";
            pnlCard.Size = new Size(700, 776);
            pnlCard.TabIndex = 0;
            // 
            // logoBox
            // 
            logoBox.BackColor = Color.Transparent;
            logoBox.Location = new Point(83, 27);
            logoBox.Margin = new Padding(6);
            logoBox.Name = "logoBox";
            logoBox.Size = new Size(531, 153);
            logoBox.SizeMode = PictureBoxSizeMode.Zoom;
            logoBox.TabIndex = 0;
            logoBox.TabStop = false;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblTitle.Location = new Point(171, 186);
            lblTitle.Margin = new Padding(6, 0, 6, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(359, 59);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "WELCOME BACK";
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblSubtitle.Location = new Point(117, 245);
            lblSubtitle.Margin = new Padding(6, 0, 6, 0);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(461, 37);
            lblSubtitle.TabIndex = 2;
            lblSubtitle.Text = "Please enter your credentials to login.";
            // 
            // lblUserId
            // 
            lblUserId.AutoSize = true;
            lblUserId.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUserId.ForeColor = Color.FromArgb(100, 116, 139);
            lblUserId.Location = new Point(80, 325);
            lblUserId.Margin = new Padding(6, 0, 6, 0);
            lblUserId.Name = "lblUserId";
            lblUserId.Size = new Size(146, 37);
            lblUserId.TabIndex = 3;
            lblUserId.Text = "Username";
            // 
            // pnlUsernameContainer
            // 
            pnlUsernameContainer.BackColor = Color.White;
            pnlUsernameContainer.Controls.Add(txtUsername);
            pnlUsernameContainer.Controls.Add(iconUsername);
            pnlUsernameContainer.Controls.Add(pnlUsernameLine);
            pnlUsernameContainer.Location = new Point(80, 368);
            pnlUsernameContainer.Margin = new Padding(6);
            pnlUsernameContainer.Name = "pnlUsernameContainer";
            pnlUsernameContainer.Size = new Size(541, 67);
            pnlUsernameContainer.TabIndex = 4;
            // 
            // txtUsername
            // 
            txtUsername.BackColor = Color.White;
            txtUsername.BorderStyle = BorderStyle.None;
            txtUsername.Font = new Font("Segoe UI", 12F);
            txtUsername.ForeColor = Color.Black;
            txtUsername.Location = new Point(55, 13);
            txtUsername.Margin = new Padding(6);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(479, 43);
            txtUsername.TabIndex = 1;
            // 
            // iconUsername
            // 
            iconUsername.BackColor = Color.Transparent;
            iconUsername.ForeColor = Color.FromArgb(100, 116, 139);
            iconUsername.IconChar = FontAwesome.Sharp.IconChar.User;
            iconUsername.IconColor = Color.FromArgb(100, 116, 139);
            iconUsername.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconUsername.IconSize = 38;
            iconUsername.Location = new Point(3, 13);
            iconUsername.Margin = new Padding(6);
            iconUsername.Name = "iconUsername";
            iconUsername.Size = new Size(39, 38);
            iconUsername.SizeMode = PictureBoxSizeMode.CenterImage;
            iconUsername.TabIndex = 0;
            iconUsername.TabStop = false;
            // 
            // pnlUsernameLine
            // 
            pnlUsernameLine.BackColor = Color.FromArgb(100, 116, 139);
            pnlUsernameLine.Dock = DockStyle.Bottom;
            pnlUsernameLine.Location = new Point(0, 64);
            pnlUsernameLine.Margin = new Padding(0);
            pnlUsernameLine.Name = "pnlUsernameLine";
            pnlUsernameLine.Size = new Size(541, 3);
            pnlUsernameLine.TabIndex = 2;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPassword.ForeColor = Color.FromArgb(100, 116, 139);
            lblPassword.Location = new Point(80, 458);
            lblPassword.Margin = new Padding(6, 0, 6, 0);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(139, 37);
            lblPassword.TabIndex = 5;
            lblPassword.Text = "Password";
            // 
            // pnlPasswordContainer
            // 
            pnlPasswordContainer.BackColor = Color.White;
            pnlPasswordContainer.Controls.Add(txtPassword);
            pnlPasswordContainer.Controls.Add(iconPassword);
            pnlPasswordContainer.Controls.Add(pnlPasswordLine);
            pnlPasswordContainer.Location = new Point(80, 501);
            pnlPasswordContainer.Margin = new Padding(6);
            pnlPasswordContainer.Name = "pnlPasswordContainer";
            pnlPasswordContainer.Size = new Size(541, 67);
            pnlPasswordContainer.TabIndex = 6;
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.White;
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Font = new Font("Segoe UI", 12F);
            txtPassword.ForeColor = Color.Black;
            txtPassword.Location = new Point(55, 13);
            txtPassword.Margin = new Padding(6);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(479, 43);
            txtPassword.TabIndex = 1;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // iconPassword
            // 
            iconPassword.BackColor = Color.Transparent;
            iconPassword.ForeColor = Color.FromArgb(100, 116, 139);
            iconPassword.IconChar = FontAwesome.Sharp.IconChar.Key;
            iconPassword.IconColor = Color.FromArgb(100, 116, 139);
            iconPassword.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPassword.IconSize = 38;
            iconPassword.Location = new Point(3, 13);
            iconPassword.Margin = new Padding(6);
            iconPassword.Name = "iconPassword";
            iconPassword.Size = new Size(39, 38);
            iconPassword.SizeMode = PictureBoxSizeMode.CenterImage;
            iconPassword.TabIndex = 0;
            iconPassword.TabStop = false;
            // 
            // pnlPasswordLine
            // 
            pnlPasswordLine.BackColor = Color.FromArgb(100, 116, 139);
            pnlPasswordLine.Dock = DockStyle.Bottom;
            pnlPasswordLine.Location = new Point(0, 64);
            pnlPasswordLine.Margin = new Padding(0);
            pnlPasswordLine.Name = "pnlPasswordLine";
            pnlPasswordLine.Size = new Size(541, 3);
            pnlPasswordLine.TabIndex = 2;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(250, 204, 21);
            btnLogin.BorderRadius = 25;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnLogin.ForeColor = Color.FromArgb(15, 23, 42);
            btnLogin.Location = new Point(80, 621);
            btnLogin.Margin = new Padding(6);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(541, 80);
            btnLogin.TabIndex = 7;
            btnLogin.Text = "LOG IN";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += BtnLogin_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.BackColor = Color.Transparent;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(225, 29, 72);
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.ForeColor = Color.White;
            btnClose.IconChar = FontAwesome.Sharp.IconChar.Close;
            btnClose.IconColor = Color.FromArgb(203, 213, 225);
            btnClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnClose.IconSize = 28;
            btnClose.Location = new Point(744, 8);
            btnClose.Margin = new Padding(0);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(54, 50);
            btnClose.TabIndex = 8;
            btnClose.UseVisualStyleBackColor = false;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(34, 40, 49);
            ClientSize = new Size(806, 914);
            Controls.Add(btnClose);
            Controls.Add(pnlCard);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(6);
            MaximizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Assembly Line Tester - Login";
            pnlCard.ResumeLayout(false);
            pnlCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)logoBox).EndInit();
            pnlUsernameContainer.ResumeLayout(false);
            pnlUsernameContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconUsername).EndInit();
            pnlPasswordContainer.ResumeLayout(false);
            pnlPasswordContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPassword).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private Controls.RoundedPanel pnlCard;
        private System.Windows.Forms.PictureBox logoBox;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblUserId;
        private System.Windows.Forms.Panel pnlUsernameContainer;
        private FontAwesome.Sharp.IconPictureBox iconUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Panel pnlUsernameLine;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Panel pnlPasswordContainer;
        private FontAwesome.Sharp.IconPictureBox iconPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Panel pnlPasswordLine;
        private Controls.RoundedButton btnLogin;
        private FontAwesome.Sharp.IconButton btnClose;
    }
}