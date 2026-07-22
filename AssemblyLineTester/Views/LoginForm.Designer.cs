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
            pnlCard.Location = new Point(33, 41);
            pnlCard.Margin = new Padding(4);
            pnlCard.Name = "pnlCard";
            pnlCard.Size = new Size(431, 485);
            pnlCard.TabIndex = 0;
            pnlCard.MouseDown += Form_MouseDown;
            // 
            // logoBox
            // 
            logoBox.BackColor = Color.Transparent;
            logoBox.Image = Properties.Resources.cat_logo;
            logoBox.Location = new Point(51, 17);
            logoBox.Margin = new Padding(4);
            logoBox.Name = "logoBox";
            logoBox.Size = new Size(327, 96);
            logoBox.SizeMode = PictureBoxSizeMode.Zoom;
            logoBox.TabIndex = 0;
            logoBox.TabStop = false;
            logoBox.MouseDown += Form_MouseDown;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblTitle.Location = new Point(105, 116);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(226, 37);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "WELCOME BACK";
            lblTitle.MouseDown += Form_MouseDown;
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblSubtitle.Location = new Point(72, 153);
            lblSubtitle.Margin = new Padding(4, 0, 4, 0);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(297, 23);
            lblSubtitle.TabIndex = 2;
            lblSubtitle.Text = "Please enter your credentials to login.";
            // 
            // lblUserId
            // 
            lblUserId.AutoSize = true;
            lblUserId.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUserId.ForeColor = Color.FromArgb(100, 116, 139);
            lblUserId.Location = new Point(49, 203);
            lblUserId.Margin = new Padding(4, 0, 4, 0);
            lblUserId.Name = "lblUserId";
            lblUserId.Size = new Size(89, 23);
            lblUserId.TabIndex = 3;
            lblUserId.Text = "Username";
            // 
            // pnlUsernameContainer
            // 
            pnlUsernameContainer.BackColor = Color.White;
            pnlUsernameContainer.Controls.Add(txtUsername);
            pnlUsernameContainer.Controls.Add(iconUsername);
            pnlUsernameContainer.Controls.Add(pnlUsernameLine);
            pnlUsernameContainer.Location = new Point(49, 230);
            pnlUsernameContainer.Margin = new Padding(4);
            pnlUsernameContainer.Name = "pnlUsernameContainer";
            pnlUsernameContainer.Size = new Size(333, 42);
            pnlUsernameContainer.TabIndex = 4;
            pnlUsernameContainer.Click += FocusUsername;
            pnlUsernameContainer.MouseEnter += UpdateUsernameVisuals;
            pnlUsernameContainer.MouseLeave += UpdateUsernameVisuals;
            // 
            // txtUsername
            // 
            txtUsername.BackColor = Color.White;
            txtUsername.BorderStyle = BorderStyle.None;
            txtUsername.Font = new Font("Segoe UI", 12F);
            txtUsername.ForeColor = Color.Black;
            txtUsername.Location = new Point(34, 8);
            txtUsername.Margin = new Padding(4);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(295, 27);
            txtUsername.TabIndex = 1;
            txtUsername.Enter += UpdateUsernameVisuals;
            txtUsername.KeyDown += TxtUsername_EnterKeyDown;
            // 
            // iconUsername
            // 
            iconUsername.BackColor = Color.Transparent;
            iconUsername.ForeColor = Color.FromArgb(100, 116, 139);
            iconUsername.IconChar = FontAwesome.Sharp.IconChar.User;
            iconUsername.IconColor = Color.FromArgb(100, 116, 139);
            iconUsername.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconUsername.IconSize = 24;
            iconUsername.Location = new Point(2, 8);
            iconUsername.Margin = new Padding(4);
            iconUsername.Name = "iconUsername";
            iconUsername.Size = new Size(24, 24);
            iconUsername.SizeMode = PictureBoxSizeMode.CenterImage;
            iconUsername.TabIndex = 0;
            iconUsername.TabStop = false;
            iconUsername.Click += FocusUsername;
            iconUsername.MouseEnter += UpdateUsernameVisuals;
            iconUsername.MouseLeave += UpdateUsernameVisuals;
            // 
            // pnlUsernameLine
            // 
            pnlUsernameLine.BackColor = Color.FromArgb(100, 116, 139);
            pnlUsernameLine.Dock = DockStyle.Bottom;
            pnlUsernameLine.Location = new Point(0, 40);
            pnlUsernameLine.Margin = new Padding(0);
            pnlUsernameLine.Name = "pnlUsernameLine";
            pnlUsernameLine.Size = new Size(333, 2);
            pnlUsernameLine.TabIndex = 2;
            pnlUsernameLine.Click += FocusUsername;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPassword.ForeColor = Color.FromArgb(100, 116, 139);
            lblPassword.Location = new Point(49, 286);
            lblPassword.Margin = new Padding(4, 0, 4, 0);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(85, 23);
            lblPassword.TabIndex = 5;
            lblPassword.Text = "Password";
            // 
            // pnlPasswordContainer
            // 
            pnlPasswordContainer.BackColor = Color.White;
            pnlPasswordContainer.Controls.Add(txtPassword);
            pnlPasswordContainer.Controls.Add(iconPassword);
            pnlPasswordContainer.Controls.Add(pnlPasswordLine);
            pnlPasswordContainer.Location = new Point(49, 313);
            pnlPasswordContainer.Margin = new Padding(4);
            pnlPasswordContainer.Name = "pnlPasswordContainer";
            pnlPasswordContainer.Size = new Size(333, 42);
            pnlPasswordContainer.TabIndex = 6;
            pnlPasswordContainer.Click += FocusPassword;
            pnlPasswordContainer.Enter += UpdatePasswordVisuals;
            pnlPasswordContainer.Leave += UpdatePasswordVisuals;
            pnlPasswordContainer.MouseEnter += UpdatePasswordVisuals;
            pnlPasswordContainer.MouseLeave += UpdatePasswordVisuals;
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.White;
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Font = new Font("Segoe UI", 12F);
            txtPassword.ForeColor = Color.Black;
            txtPassword.Location = new Point(34, 8);
            txtPassword.Margin = new Padding(4);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(295, 27);
            txtPassword.TabIndex = 1;
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.KeyDown += TxtPassword_EnterKeyDown;
            // 
            // iconPassword
            // 
            iconPassword.BackColor = Color.Transparent;
            iconPassword.ForeColor = Color.FromArgb(100, 116, 139);
            iconPassword.IconChar = FontAwesome.Sharp.IconChar.Key;
            iconPassword.IconColor = Color.FromArgb(100, 116, 139);
            iconPassword.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPassword.IconSize = 24;
            iconPassword.Location = new Point(2, 8);
            iconPassword.Margin = new Padding(4);
            iconPassword.Name = "iconPassword";
            iconPassword.Size = new Size(24, 24);
            iconPassword.SizeMode = PictureBoxSizeMode.CenterImage;
            iconPassword.TabIndex = 0;
            iconPassword.TabStop = false;
            iconPassword.Click += FocusPassword;
            iconPassword.MouseEnter += UpdatePasswordVisuals;
            iconPassword.MouseLeave += UpdatePasswordVisuals;
            // 
            // pnlPasswordLine
            // 
            pnlPasswordLine.BackColor = Color.FromArgb(100, 116, 139);
            pnlPasswordLine.Dock = DockStyle.Bottom;
            pnlPasswordLine.Location = new Point(0, 40);
            pnlPasswordLine.Margin = new Padding(0);
            pnlPasswordLine.Name = "pnlPasswordLine";
            pnlPasswordLine.Size = new Size(333, 2);
            pnlPasswordLine.TabIndex = 2;
            pnlPasswordLine.Click += FocusPassword;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(250, 204, 21);
            btnLogin.BorderRadius = 25;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnLogin.ForeColor = Color.FromArgb(15, 23, 42);
            btnLogin.HoverForeColor = Color.Empty;
            btnLogin.Location = new Point(49, 388);
            btnLogin.Margin = new Padding(4);
            btnLogin.Name = "btnLogin";
            btnLogin.PressedForeColor = Color.Empty;
            btnLogin.Size = new Size(333, 50);
            btnLogin.TabIndex = 7;
            btnLogin.TabStop = false;
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
            btnClose.Location = new Point(458, 5);
            btnClose.Margin = new Padding(0);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(33, 31);
            btnClose.TabIndex = 8;
            btnClose.UseVisualStyleBackColor = false;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(34, 40, 49);
            ClientSize = new Size(496, 571);
            Controls.Add(btnClose);
            Controls.Add(pnlCard);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4);
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