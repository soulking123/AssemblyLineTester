namespace AssemblyLineTester
{
    partial class Form1
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnToggleSidebar = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.btnTestExecution = new System.Windows.Forms.Button();
            this.btnHarnessDatabase = new System.Windows.Forms.Button();
            this.btnTestHistory = new System.Windows.Forms.Button();
            this.btnReports = new System.Windows.Forms.Button();
            this.btnSystemLog = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnUserManagement = new System.Windows.Forms.Button();
            this.lblMenu = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(40)))), ((int)(((byte)(49)))));
            this.splitContainer1.Panel1.Controls.Add(this.btnToggleSidebar);
            this.splitContainer1.Panel1.Controls.Add(this.btnDashboard);
            this.splitContainer1.Panel1.Controls.Add(this.btnTestExecution);
            this.splitContainer1.Panel1.Controls.Add(this.btnHarnessDatabase);
            this.splitContainer1.Panel1.Controls.Add(this.btnTestHistory);
            this.splitContainer1.Panel1.Controls.Add(this.btnReports);
            this.splitContainer1.Panel1.Controls.Add(this.btnSystemLog);
            this.splitContainer1.Panel1.Controls.Add(this.btnSettings);
            this.splitContainer1.Panel1.Controls.Add(this.btnUserManagement);
            this.splitContainer1.Panel1.Controls.Add(this.lblMenu);
            this.splitContainer1.Size = new System.Drawing.Size(2429, 1261);
            this.splitContainer1.SplitterDistance = 380;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnToggleSidebar
            // 
            this.btnToggleSidebar.FlatAppearance.BorderSize = 0;
            this.btnToggleSidebar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleSidebar.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.btnToggleSidebar.ForeColor = System.Drawing.Color.White;
            this.btnToggleSidebar.Location = new System.Drawing.Point(20, 20);
            this.btnToggleSidebar.Name = "btnToggleSidebar";
            this.btnToggleSidebar.Size = new System.Drawing.Size(60, 60);
            this.btnToggleSidebar.TabIndex = 1;
            this.btnToggleSidebar.Text = "☰";
            this.btnToggleSidebar.UseVisualStyleBackColor = true;
            this.btnToggleSidebar.Click += new System.EventHandler(this.BtnToggleSidebar_Click);
            // 
            // lblMenu
            // 
            this.lblMenu.AutoSize = true;
            this.lblMenu.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblMenu.ForeColor = System.Drawing.Color.White;
            this.lblMenu.Location = new System.Drawing.Point(100, 30);
            this.lblMenu.Name = "lblMenu";
            this.lblMenu.Size = new System.Drawing.Size(214, 45);
            this.lblMenu.TabIndex = 0;
            this.lblMenu.Text = "MAIN MENU";
            // 
            // btnDashboard
            // 
            this.btnDashboard.Location = new System.Drawing.Point(20, 100);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(340, 50);
            this.btnDashboard.TabIndex = 2;
            this.btnDashboard.Text = "        Dashboard";
            this.btnDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboard.UseVisualStyleBackColor = true;
            // 
            // btnTestExecution
            // 
            this.btnTestExecution.Location = new System.Drawing.Point(20, 160);
            this.btnTestExecution.Name = "btnTestExecution";
            this.btnTestExecution.Size = new System.Drawing.Size(340, 50);
            this.btnTestExecution.TabIndex = 3;
            this.btnTestExecution.Text = "        Test Execution";
            this.btnTestExecution.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTestExecution.UseVisualStyleBackColor = true;
            // 
            // btnHarnessDatabase
            // 
            this.btnHarnessDatabase.Location = new System.Drawing.Point(20, 220);
            this.btnHarnessDatabase.Name = "btnHarnessDatabase";
            this.btnHarnessDatabase.Size = new System.Drawing.Size(340, 50);
            this.btnHarnessDatabase.TabIndex = 4;
            this.btnHarnessDatabase.Text = "        Harness Database";
            this.btnHarnessDatabase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHarnessDatabase.UseVisualStyleBackColor = true;
            // 
            // btnTestHistory
            // 
            this.btnTestHistory.Location = new System.Drawing.Point(20, 280);
            this.btnTestHistory.Name = "btnTestHistory";
            this.btnTestHistory.Size = new System.Drawing.Size(340, 50);
            this.btnTestHistory.TabIndex = 5;
            this.btnTestHistory.Text = "        Test History";
            this.btnTestHistory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTestHistory.UseVisualStyleBackColor = true;
            this.btnTestHistory.Click += new System.EventHandler(this.BtnMenu_Click);
            // 
            // btnReports
            // 
            this.btnReports.Location = new System.Drawing.Point(20, 340);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(340, 50);
            this.btnReports.TabIndex = 6;
            this.btnReports.Text = "        Reports";
            this.btnReports.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReports.UseVisualStyleBackColor = true;
            // 
            // btnSystemLog
            // 
            this.btnSystemLog.Location = new System.Drawing.Point(20, 400);
            this.btnSystemLog.Name = "btnSystemLog";
            this.btnSystemLog.Size = new System.Drawing.Size(340, 50);
            this.btnSystemLog.TabIndex = 7;
            this.btnSystemLog.Text = "        System Log";
            this.btnSystemLog.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSystemLog.UseVisualStyleBackColor = true;
            this.btnSystemLog.Click += new System.EventHandler(this.BtnMenu_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(20, 460);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(340, 50);
            this.btnSettings.TabIndex = 8;
            this.btnSettings.Text = "        Settings";
            this.btnSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSettings.UseVisualStyleBackColor = true;
            // 
            // btnUserManagement
            // 
            this.btnUserManagement.Location = new System.Drawing.Point(20, 520);
            this.btnUserManagement.Name = "btnUserManagement";
            this.btnUserManagement.Size = new System.Drawing.Size(340, 50);
            this.btnUserManagement.TabIndex = 9;
            this.btnUserManagement.Text = "        User Management";
            this.btnUserManagement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUserManagement.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2429, 1261);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Assembly Line Tester - Dashboard";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnToggleSidebar;
        private System.Windows.Forms.Label lblMenu;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnTestExecution;
        private System.Windows.Forms.Button btnHarnessDatabase;
        private System.Windows.Forms.Button btnTestHistory;
        private System.Windows.Forms.Button btnReports;
        private System.Windows.Forms.Button btnSystemLog;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnUserManagement;
    }
}