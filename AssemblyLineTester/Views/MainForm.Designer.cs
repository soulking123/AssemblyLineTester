namespace AssemblyLineTester.Views
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            splitContainer1 = new SplitContainer();
            btnToggleSidebar = new Button();
            btnDashboard = new AssemblyLineTester.Views.Controls.MenuButton();
            btnTestExecution = new AssemblyLineTester.Views.Controls.MenuButton();
            btnManualTesting = new AssemblyLineTester.Views.Controls.MenuButton();
            btnHarnessDatabase = new AssemblyLineTester.Views.Controls.MenuButton();
            btnTestHistory = new AssemblyLineTester.Views.Controls.MenuButton();
            btnReports = new AssemblyLineTester.Views.Controls.MenuButton();
            btnSystemLog = new AssemblyLineTester.Views.Controls.MenuButton();
            btnSettings = new AssemblyLineTester.Views.Controls.MenuButton();
            btnUserManagement = new AssemblyLineTester.Views.Controls.MenuButton();
            pnlLogoHeader = new AssemblyLineTester.Views.Controls.LogoHeaderPanel();
            lblMenu = new PictureBox();
            pnlContent = new Panel();
            pnlTopHeader = new Panel();
            pnlRight = new FlowLayoutPanel();
            pnlWindowControls = new FlowLayoutPanel();
            btnMinimize = new Button();
            btnMaximize = new Button();
            btnClose = new Button();
            pnlSep2 = new Panel();
            pnlDateTime = new Panel();
            lblDateIcon = new Label();
            lblDateText = new Label();
            lblTimeText = new Label();
            pnlSep = new Panel();
            pnlUser = new Panel();
            lblUserIcon = new Label();
            lblUserName = new Label();
            lblUserRole = new Label();
            lblArrow = new Label();
            lblHeaderTitle = new Label();
            pnlUserDropdown = new Panel();
            btnLogOut = new Button();
            clockTimer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            pnlLogoHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)lblMenu).BeginInit();
            pnlTopHeader.SuspendLayout();
            pnlRight.SuspendLayout();
            pnlWindowControls.SuspendLayout();
            pnlDateTime.SuspendLayout();
            pnlUser.SuspendLayout();
            pnlUserDropdown.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Margin = new Padding(5);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.BackColor = Color.FromArgb(34, 40, 49);
            splitContainer1.Panel1.Controls.Add(btnToggleSidebar);
            splitContainer1.Panel1.Controls.Add(btnDashboard);
            splitContainer1.Panel1.Controls.Add(btnTestExecution);
            splitContainer1.Panel1.Controls.Add(btnManualTesting);
            splitContainer1.Panel1.Controls.Add(btnHarnessDatabase);
            splitContainer1.Panel1.Controls.Add(btnTestHistory);
            splitContainer1.Panel1.Controls.Add(btnReports);
            splitContainer1.Panel1.Controls.Add(btnSystemLog);
            splitContainer1.Panel1.Controls.Add(btnSettings);
            splitContainer1.Panel1.Controls.Add(btnUserManagement);
            splitContainer1.Panel1.Controls.Add(btnToggleSidebar);
            splitContainer1.Panel1.Controls.Add(pnlLogoHeader);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(pnlContent);
            splitContainer1.Panel2.Controls.Add(pnlTopHeader);
            splitContainer1.Size = new Size(2884, 1759);
            splitContainer1.SplitterDistance = 432;
            splitContainer1.SplitterWidth = 5;
            splitContainer1.TabIndex = 0;
            // 
            // btnToggleSidebar
            // 
            btnToggleSidebar.FlatAppearance.BorderSize = 0;
            btnToggleSidebar.FlatStyle = FlatStyle.Flat;
            btnToggleSidebar.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            btnToggleSidebar.ForeColor = Color.White;
            btnToggleSidebar.Location = new Point(320, 24);
            btnToggleSidebar.Margin = new Padding(5);
            btnToggleSidebar.Name = "btnToggleSidebar";
            btnToggleSidebar.Size = new Size(88, 88);
            btnToggleSidebar.TabIndex = 1;
            btnToggleSidebar.Text = "☰";
            btnToggleSidebar.UseVisualStyleBackColor = true;
            btnToggleSidebar.Click += BtnToggleSidebar_Click;
            // 
            // pnlLogoHeader
            // 
            pnlLogoHeader.BackColor = Color.Transparent;
            pnlLogoHeader.Controls.Add(lblMenu);
            pnlLogoHeader.CornerRadius = 24F;
            pnlLogoHeader.Location = new Point(0, 0);
            pnlLogoHeader.Margin = new Padding(0);
            pnlLogoHeader.Name = "pnlLogoHeader";
            pnlLogoHeader.Size = new Size(300, 130);
            pnlLogoHeader.SlopeOffset = 50F;
            pnlLogoHeader.TabColor = Color.FromArgb(250, 204, 21);
            pnlLogoHeader.TabIndex = 0;
            // 
            // lblMenu
            // 
            lblMenu.BackColor = Color.Transparent;
            lblMenu.Location = new Point(16, 20);
            lblMenu.Margin = new Padding(5, 0, 5, 0);
            lblMenu.Name = "lblMenu";
            lblMenu.Size = new Size(200, 90);
            lblMenu.SizeMode = PictureBoxSizeMode.Zoom;
            lblMenu.TabIndex = 0;
            lblMenu.TabStop = false;
            // 
            // btnDashboard
            // 
            btnDashboard.Font = new Font("Segoe UI", 11F);
            btnDashboard.Location = new Point(32, 160);
            btnDashboard.Margin = new Padding(5);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Size = new Size(377, 80);
            btnDashboard.TabIndex = 2;
            btnDashboard.Text = "        Dashboard";
            btnDashboard.TextAlign = ContentAlignment.MiddleLeft;
            btnDashboard.UseVisualStyleBackColor = true;
            btnDashboard.Click += BtnMenu_Click;
            // 
            // btnTestExecution
            // 
            btnTestExecution.Font = new Font("Segoe UI", 11F);
            btnTestExecution.Location = new Point(32, 256);
            btnTestExecution.Margin = new Padding(5);
            btnTestExecution.Name = "btnTestExecution";
            btnTestExecution.Size = new Size(377, 80);
            btnTestExecution.TabIndex = 3;
            btnTestExecution.Text = "        Test Execution";
            btnTestExecution.TextAlign = ContentAlignment.MiddleLeft;
            btnTestExecution.UseVisualStyleBackColor = true;
            btnTestExecution.Click += BtnMenu_Click;
            // 
            // btnManualTesting
            // 
            btnManualTesting.Font = new Font("Segoe UI", 11F);
            btnManualTesting.Location = new Point(32, 352);
            btnManualTesting.Margin = new Padding(5);
            btnManualTesting.Name = "btnManualTesting";
            btnManualTesting.Size = new Size(377, 80);
            btnManualTesting.TabIndex = 4;
            btnManualTesting.Text = "        Manual Testing";
            btnManualTesting.TextAlign = ContentAlignment.MiddleLeft;
            btnManualTesting.UseVisualStyleBackColor = true;
            btnManualTesting.Click += BtnMenu_Click;
            // 
            // btnHarnessDatabase
            // 
            btnHarnessDatabase.Font = new Font("Segoe UI", 11F);
            btnHarnessDatabase.Location = new Point(32, 448);
            btnHarnessDatabase.Margin = new Padding(5);
            btnHarnessDatabase.Name = "btnHarnessDatabase";
            btnHarnessDatabase.Size = new Size(377, 80);
            btnHarnessDatabase.TabIndex = 4;
            btnHarnessDatabase.Text = "        Harness Database";
            btnHarnessDatabase.TextAlign = ContentAlignment.MiddleLeft;
            btnHarnessDatabase.UseVisualStyleBackColor = true;
            btnHarnessDatabase.Click += BtnMenu_Click;
            // 
            // btnTestHistory
            // 
            btnTestHistory.Font = new Font("Segoe UI", 11F);
            btnTestHistory.Location = new Point(32, 544);
            btnTestHistory.Margin = new Padding(5);
            btnTestHistory.Name = "btnTestHistory";
            btnTestHistory.Size = new Size(377, 80);
            btnTestHistory.TabIndex = 5;
            btnTestHistory.Text = "        Test History";
            btnTestHistory.TextAlign = ContentAlignment.MiddleLeft;
            btnTestHistory.UseVisualStyleBackColor = true;
            btnTestHistory.Click += BtnMenu_Click;
            // 
            // btnReports
            // 
            btnReports.Font = new Font("Segoe UI", 11F);
            btnReports.Location = new Point(32, 640);
            btnReports.Margin = new Padding(5);
            btnReports.Name = "btnReports";
            btnReports.Size = new Size(377, 80);
            btnReports.TabIndex = 6;
            btnReports.Text = "        Reports";
            btnReports.TextAlign = ContentAlignment.MiddleLeft;
            btnReports.UseVisualStyleBackColor = true;
            btnReports.Click += BtnMenu_Click;
            // 
            // btnSystemLog
            // 
            btnSystemLog.Font = new Font("Segoe UI", 11F);
            btnSystemLog.Location = new Point(32, 736);
            btnSystemLog.Margin = new Padding(5);
            btnSystemLog.Name = "btnSystemLog";
            btnSystemLog.Size = new Size(377, 80);
            btnSystemLog.TabIndex = 7;
            btnSystemLog.Text = "        System Log";
            btnSystemLog.TextAlign = ContentAlignment.MiddleLeft;
            btnSystemLog.UseVisualStyleBackColor = true;
            btnSystemLog.Click += BtnMenu_Click;
            // 
            // btnSettings
            // 
            btnSettings.Font = new Font("Segoe UI", 11F);
            btnSettings.Location = new Point(32, 832);
            btnSettings.Margin = new Padding(5);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(377, 80);
            btnSettings.TabIndex = 8;
            btnSettings.Text = "        Settings";
            btnSettings.TextAlign = ContentAlignment.MiddleLeft;
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += BtnMenu_Click;
            // 
            // btnUserManagement
            // 
            btnUserManagement.Font = new Font("Segoe UI", 11F);
            btnUserManagement.Location = new Point(32, 928);
            btnUserManagement.Margin = new Padding(5);
            btnUserManagement.Name = "btnUserManagement";
            btnUserManagement.Size = new Size(377, 80);
            btnUserManagement.TabIndex = 9;
            btnUserManagement.Text = "        User Management";
            btnUserManagement.TextAlign = ContentAlignment.MiddleLeft;
            btnUserManagement.UseVisualStyleBackColor = true;
            btnUserManagement.Click += BtnMenu_Click;
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.FromArgb(244, 245, 247);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 122);
            pnlContent.Margin = new Padding(5);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(2447, 1637);
            pnlContent.TabIndex = 1;
            // 
            // pnlTopHeader
            // 
            pnlTopHeader.BackColor = Color.FromArgb(248, 250, 252);
            pnlTopHeader.Controls.Add(pnlRight);
            pnlTopHeader.Controls.Add(lblHeaderTitle);
            pnlTopHeader.Dock = DockStyle.Top;
            pnlTopHeader.Location = new Point(0, 0);
            pnlTopHeader.Margin = new Padding(5);
            pnlTopHeader.Name = "pnlTopHeader";
            pnlTopHeader.Size = new Size(2447, 122);
            pnlTopHeader.TabIndex = 0;
            pnlTopHeader.Paint += PnlTopHeader_Paint;
            // 
            // pnlRight
            // 
            pnlRight.AutoSize = true;
            pnlRight.BackColor = Color.Transparent;
            pnlRight.Controls.Add(pnlWindowControls);
            pnlRight.Controls.Add(pnlSep2);
            pnlRight.Controls.Add(pnlDateTime);
            pnlRight.Controls.Add(pnlSep);
            pnlRight.Controls.Add(pnlUser);
            pnlRight.Dock = DockStyle.Right;
            pnlRight.FlowDirection = FlowDirection.RightToLeft;
            pnlRight.Location = new Point(1312, 0);
            pnlRight.Margin = new Padding(5);
            pnlRight.MinimumSize = new Size(1100, 122);
            pnlRight.Name = "pnlRight";
            pnlRight.Padding = new Padding(0, 24, 25, 0);
            pnlRight.Size = new Size(1135, 122);
            pnlRight.TabIndex = 1;
            pnlRight.WrapContents = false;
            // 
            // pnlWindowControls
            // 
            pnlWindowControls.AutoSize = true;
            pnlWindowControls.BackColor = Color.Transparent;
            pnlWindowControls.Controls.Add(btnMinimize);
            pnlWindowControls.Controls.Add(btnMaximize);
            pnlWindowControls.Controls.Add(btnClose);
            pnlWindowControls.Location = new Point(942, 35);
            pnlWindowControls.Margin = new Padding(0, 11, 0, 0);
            pnlWindowControls.Name = "pnlWindowControls";
            pnlWindowControls.Size = new Size(168, 52);
            pnlWindowControls.TabIndex = 4;
            pnlWindowControls.WrapContents = false;
            // 
            // btnMinimize
            // 
            btnMinimize.Cursor = Cursors.Hand;
            btnMinimize.FlatAppearance.BorderSize = 0;
            btnMinimize.FlatStyle = FlatStyle.Flat;
            btnMinimize.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnMinimize.ForeColor = Color.FromArgb(100, 116, 139);
            btnMinimize.Location = new Point(2, 0);
            btnMinimize.Margin = new Padding(2, 0, 2, 0);
            btnMinimize.Name = "btnMinimize";
            btnMinimize.Size = new Size(52, 52);
            btnMinimize.TabIndex = 0;
            btnMinimize.Text = "—";
            btnMinimize.UseVisualStyleBackColor = true;
            // 
            // btnMaximize
            // 
            btnMaximize.Cursor = Cursors.Hand;
            btnMaximize.FlatAppearance.BorderSize = 0;
            btnMaximize.FlatStyle = FlatStyle.Flat;
            btnMaximize.Font = new Font("Segoe UI", 13F);
            btnMaximize.ForeColor = Color.FromArgb(100, 116, 139);
            btnMaximize.Location = new Point(58, 0);
            btnMaximize.Margin = new Padding(2, 0, 2, 0);
            btnMaximize.Name = "btnMaximize";
            btnMaximize.Size = new Size(52, 52);
            btnMaximize.TabIndex = 1;
            btnMaximize.Text = "❐";
            btnMaximize.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            btnClose.Cursor = Cursors.Hand;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnClose.ForeColor = Color.FromArgb(100, 116, 139);
            btnClose.Location = new Point(114, 0);
            btnClose.Margin = new Padding(2, 0, 2, 0);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(52, 52);
            btnClose.TabIndex = 2;
            btnClose.Text = "✕";
            btnClose.UseVisualStyleBackColor = true;
            // 
            // pnlSep2
            // 
            pnlSep2.BackColor = Color.FromArgb(203, 213, 225);
            pnlSep2.Location = new Point(908, 32);
            pnlSep2.Margin = new Padding(32, 8, 32, 0);
            pnlSep2.Name = "pnlSep2";
            pnlSep2.Size = new Size(2, 58);
            pnlSep2.TabIndex = 3;
            // 
            // pnlDateTime
            // 
            pnlDateTime.BackColor = Color.Transparent;
            pnlDateTime.Controls.Add(lblDateIcon);
            pnlDateTime.Controls.Add(lblDateText);
            pnlDateTime.Controls.Add(lblTimeText);
            pnlDateTime.Location = new Point(526, 24);
            pnlDateTime.Margin = new Padding(0);
            pnlDateTime.Name = "pnlDateTime";
            pnlDateTime.Size = new Size(350, 74);
            pnlDateTime.TabIndex = 0;
            // 
            // lblDateIcon
            // 
            lblDateIcon.Font = new Font("Segoe UI", 18F);
            lblDateIcon.Location = new Point(-17, 3);
            lblDateIcon.Margin = new Padding(5, 0, 5, 0);
            lblDateIcon.Name = "lblDateIcon";
            lblDateIcon.Size = new Size(75, 61);
            lblDateIcon.TabIndex = 0;
            lblDateIcon.Text = "📅";
            // 
            // lblDateText
            // 
            lblDateText.AutoSize = true;
            lblDateText.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblDateText.ForeColor = Color.FromArgb(30, 41, 59);
            lblDateText.Location = new Point(65, 3);
            lblDateText.Margin = new Padding(5, 0, 5, 0);
            lblDateText.Name = "lblDateText";
            lblDateText.Size = new Size(168, 36);
            lblDateText.TabIndex = 1;
            lblDateText.Text = "15 Mar 2026";
            // 
            // lblTimeText
            // 
            lblTimeText.AutoSize = true;
            lblTimeText.Font = new Font("Segoe UI", 8.5F);
            lblTimeText.ForeColor = Color.FromArgb(100, 116, 139);
            lblTimeText.Location = new Point(65, 35);
            lblTimeText.Margin = new Padding(5, 0, 5, 0);
            lblTimeText.Name = "lblTimeText";
            lblTimeText.Size = new Size(138, 31);
            lblTimeText.TabIndex = 2;
            lblTimeText.Text = "10:30:45 AM";
            // 
            // pnlSep
            // 
            pnlSep.BackColor = Color.FromArgb(203, 213, 225);
            pnlSep.Location = new Point(492, 32);
            pnlSep.Margin = new Padding(32, 8, 32, 0);
            pnlSep.Name = "pnlSep";
            pnlSep.Size = new Size(2, 58);
            pnlSep.TabIndex = 1;
            // 
            // pnlUser
            // 
            pnlUser.BackColor = Color.Transparent;
            pnlUser.Controls.Add(lblUserIcon);
            pnlUser.Controls.Add(lblUserName);
            pnlUser.Controls.Add(lblUserRole);
            pnlUser.Controls.Add(lblArrow);
            pnlUser.Cursor = Cursors.Hand;
            pnlUser.Location = new Point(0, 24);
            pnlUser.Margin = new Padding(0);
            pnlUser.Name = "pnlUser";
            pnlUser.Size = new Size(460, 74);
            pnlUser.TabIndex = 2;
            pnlUser.Click += ToggleUserDropdown;
            // 
            // lblUserIcon
            // 
            lblUserIcon.Cursor = Cursors.Hand;
            lblUserIcon.Font = new Font("Segoe UI", 18F);
            lblUserIcon.Location = new Point(-14, 0);
            lblUserIcon.Margin = new Padding(5, 0, 5, 0);
            lblUserIcon.Name = "lblUserIcon";
            lblUserIcon.Size = new Size(72, 64);
            lblUserIcon.TabIndex = 0;
            lblUserIcon.Text = "👤";
            lblUserIcon.Click += ToggleUserDropdown;
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Cursor = Cursors.Hand;
            lblUserName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblUserName.ForeColor = Color.FromArgb(30, 41, 59);
            lblUserName.Location = new Point(65, 3);
            lblUserName.Margin = new Padding(5, 0, 5, 0);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(175, 36);
            lblUserName.TabIndex = 1;
            lblUserName.Text = "Fally Kusuma";
            lblUserName.Click += ToggleUserDropdown;
            // 
            // lblUserRole
            // 
            lblUserRole.AutoSize = true;
            lblUserRole.Cursor = Cursors.Hand;
            lblUserRole.Font = new Font("Segoe UI", 8.5F);
            lblUserRole.ForeColor = Color.FromArgb(100, 116, 139);
            lblUserRole.Location = new Point(65, 35);
            lblUserRole.Margin = new Padding(5, 0, 5, 0);
            lblUserRole.Name = "lblUserRole";
            lblUserRole.Size = new Size(155, 31);
            lblUserRole.TabIndex = 2;
            lblUserRole.Text = "Administrator";
            lblUserRole.Click += ToggleUserDropdown;
            // 
            // lblArrow
            // 
            lblArrow.Cursor = Cursors.Hand;
            lblArrow.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblArrow.ForeColor = Color.FromArgb(100, 116, 139);
            lblArrow.Location = new Point(400, 13);
            lblArrow.Margin = new Padding(5, 0, 5, 0);
            lblArrow.Name = "lblArrow";
            lblArrow.Size = new Size(39, 38);
            lblArrow.TabIndex = 3;
            lblArrow.Text = "⌄";
            lblArrow.Click += ToggleUserDropdown;
            // 
            // lblHeaderTitle
            // 
            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblHeaderTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblHeaderTitle.Location = new Point(41, 32);
            lblHeaderTitle.Margin = new Padding(5, 0, 5, 0);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new Size(616, 59);
            lblHeaderTitle.TabIndex = 0;
            lblHeaderTitle.Text = "Assembly Line Harness Tester";
            // 
            // pnlUserDropdown
            // 
            pnlUserDropdown.BackColor = Color.White;
            pnlUserDropdown.Controls.Add(btnLogOut);
            pnlUserDropdown.Location = new Point(1538, 3);
            pnlUserDropdown.Name = "pnlUserDropdown";
            pnlUserDropdown.Size = new Size(220, 56);
            pnlUserDropdown.TabIndex = 10;
            pnlUserDropdown.Visible = false;
            pnlUserDropdown.Paint += PnlUserDropdown_Paint;
            // 
            // btnLogOut
            // 
            btnLogOut.BackColor = Color.Transparent;
            btnLogOut.Cursor = Cursors.Hand;
            btnLogOut.FlatAppearance.BorderSize = 0;
            btnLogOut.FlatStyle = FlatStyle.Flat;
            btnLogOut.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLogOut.ForeColor = Color.FromArgb(220, 38, 38);
            btnLogOut.Location = new Point(8, 6);
            btnLogOut.Name = "btnLogOut";
            btnLogOut.Size = new Size(204, 44);
            btnLogOut.TabIndex = 2;
            btnLogOut.Text = "\u23fb   Log Out";
            btnLogOut.TextAlign = ContentAlignment.MiddleLeft;
            btnLogOut.UseVisualStyleBackColor = false;
            btnLogOut.Click += BtnLogOut_Click;
            btnLogOut.MouseEnter += BtnLogOut_MouseEnter;
            btnLogOut.MouseLeave += BtnLogOut_MouseLeave;
            // 
            // clockTimer
            // 
            clockTimer.Enabled = true;
            clockTimer.Interval = 1000;
            clockTimer.Tick += ClockTimer_Tick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2884, 1759);
            Controls.Add(pnlUserDropdown);
            Controls.Add(splitContainer1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(5);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Assembly Line Tester - Dashboard";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            pnlLogoHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)lblMenu).EndInit();
            pnlTopHeader.ResumeLayout(false);
            pnlTopHeader.PerformLayout();
            pnlRight.ResumeLayout(false);
            pnlRight.PerformLayout();
            pnlWindowControls.ResumeLayout(false);
            pnlDateTime.ResumeLayout(false);
            pnlDateTime.PerformLayout();
            pnlUser.ResumeLayout(false);
            pnlUser.PerformLayout();
            pnlUserDropdown.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnToggleSidebar;
        private AssemblyLineTester.Views.Controls.LogoHeaderPanel pnlLogoHeader;
        private System.Windows.Forms.PictureBox lblMenu;
        private AssemblyLineTester.Views.Controls.MenuButton btnDashboard;
        private AssemblyLineTester.Views.Controls.MenuButton btnTestExecution;
        private AssemblyLineTester.Views.Controls.MenuButton btnManualTesting;
        private AssemblyLineTester.Views.Controls.MenuButton btnHarnessDatabase;
        private AssemblyLineTester.Views.Controls.MenuButton btnTestHistory;
        private AssemblyLineTester.Views.Controls.MenuButton btnReports;
        private AssemblyLineTester.Views.Controls.MenuButton btnSystemLog;
        private AssemblyLineTester.Views.Controls.MenuButton btnSettings;
        private AssemblyLineTester.Views.Controls.MenuButton btnUserManagement;
        private System.Windows.Forms.Panel pnlTopHeader;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.FlowLayoutPanel pnlRight;
        private System.Windows.Forms.Panel pnlDateTime;
        private System.Windows.Forms.Label lblDateIcon;
        private System.Windows.Forms.Label lblDateText;
        private System.Windows.Forms.Label lblTimeText;
        private System.Windows.Forms.Panel pnlSep;
        private System.Windows.Forms.Panel pnlUser;
        private System.Windows.Forms.Label lblUserIcon;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblUserRole;
        private System.Windows.Forms.Label lblArrow;
        private System.Windows.Forms.Timer clockTimer;
        private System.Windows.Forms.Panel pnlUserDropdown;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.FlowLayoutPanel pnlWindowControls;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Button btnMaximize;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlSep2;
    }
}
