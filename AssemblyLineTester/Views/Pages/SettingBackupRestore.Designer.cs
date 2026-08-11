namespace AssemblyLineTester.Views
{
    partial class SettingBackupRestore
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            tblMainLayout = new TableLayoutPanel();
            cardBackupRestore = new Panel();
            tblBackupRestore = new TableLayoutPanel();
            lblTitle = new Label();
            lblAutoRecap = new Label();
            cboAutoRecap = new ComboBox();
            btnResetDefault = new AssemblyLineTester.Views.Controls.RoundedButton();
            lblManualRecap = new Label();
            txtManualRecapPath = new TextBox();
            flpRecapBtns = new FlowLayoutPanel();
            btnBrowseRecap = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnRecap = new AssemblyLineTester.Views.Controls.RoundedButton();
            lblRollbackTo = new Label();
            txtRollbackPath = new TextBox();
            flpRollbackBtns = new FlowLayoutPanel();
            btnBrowseRollback = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnRollback = new AssemblyLineTester.Views.Controls.RoundedButton();
            tblMainLayout.SuspendLayout();
            cardBackupRestore.SuspendLayout();
            tblBackupRestore.SuspendLayout();
            flpRecapBtns.SuspendLayout();
            flpRollbackBtns.SuspendLayout();
            SuspendLayout();
            // 
            // tblMainLayout
            // 
            tblMainLayout.AutoSize = true;
            tblMainLayout.BackColor = Color.FromArgb(244, 245, 247);
            tblMainLayout.ColumnCount = 1;
            tblMainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblMainLayout.Controls.Add(cardBackupRestore, 0, 0);
            tblMainLayout.Dock = DockStyle.Fill;
            tblMainLayout.Location = new Point(0, 0);
            tblMainLayout.Margin = new Padding(0);
            tblMainLayout.Name = "tblMainLayout";
            tblMainLayout.Padding = new Padding(7, 6, 7, 6);
            tblMainLayout.RowCount = 2;
            tblMainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 47.3333321F));
            tblMainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 52.6666679F));
            tblMainLayout.Size = new Size(933, 462);
            tblMainLayout.TabIndex = 0;
            // 
            // cardBackupRestore
            // 
            cardBackupRestore.AutoSize = true;
            cardBackupRestore.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            cardBackupRestore.BackColor = Color.White;
            cardBackupRestore.Controls.Add(tblBackupRestore);
            cardBackupRestore.Dock = DockStyle.Fill;
            cardBackupRestore.Location = new Point(7, 6);
            cardBackupRestore.Margin = new Padding(0, 0, 0, 9);
            cardBackupRestore.Name = "cardBackupRestore";
            cardBackupRestore.Size = new Size(919, 204);
            cardBackupRestore.TabIndex = 1;
            // 
            // tblBackupRestore
            // 
            tblBackupRestore.AutoSize = true;
            tblBackupRestore.ColumnCount = 6;
            tblBackupRestore.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666F));
            tblBackupRestore.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666F));
            tblBackupRestore.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666F));
            tblBackupRestore.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666F));
            tblBackupRestore.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666F));
            tblBackupRestore.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.67F));
            tblBackupRestore.Controls.Add(lblTitle, 0, 0);
            tblBackupRestore.Controls.Add(lblAutoRecap, 0, 1);
            tblBackupRestore.Controls.Add(cboAutoRecap, 0, 2);
            tblBackupRestore.Controls.Add(btnResetDefault, 3, 2);
            tblBackupRestore.Controls.Add(lblManualRecap, 0, 3);
            tblBackupRestore.Controls.Add(txtManualRecapPath, 0, 4);
            tblBackupRestore.Controls.Add(flpRecapBtns, 4, 4);
            tblBackupRestore.Controls.Add(lblRollbackTo, 0, 5);
            tblBackupRestore.Controls.Add(txtRollbackPath, 0, 6);
            tblBackupRestore.Controls.Add(flpRollbackBtns, 4, 6);
            tblBackupRestore.Dock = DockStyle.Top;
            tblBackupRestore.Location = new Point(0, 0);
            tblBackupRestore.Name = "tblBackupRestore";
            tblBackupRestore.Padding = new Padding(8, 0, 8, 7);
            tblBackupRestore.RowCount = 7;
            tblBackupRestore.RowStyles.Add(new RowStyle(SizeType.Absolute, 23F));
            tblBackupRestore.RowStyles.Add(new RowStyle());
            tblBackupRestore.RowStyles.Add(new RowStyle());
            tblBackupRestore.RowStyles.Add(new RowStyle());
            tblBackupRestore.RowStyles.Add(new RowStyle());
            tblBackupRestore.RowStyles.Add(new RowStyle());
            tblBackupRestore.RowStyles.Add(new RowStyle());
            tblBackupRestore.Size = new Size(919, 192);
            tblBackupRestore.TabIndex = 0;
            // 
            // lblTitle
            // 
            tblBackupRestore.SetColumnSpan(lblTitle, 6);
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTitle.Location = new Point(8, 0);
            lblTitle.Margin = new Padding(0, 0, 0, 7);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(903, 16);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "BACKUP & RESTORE";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblAutoRecap
            // 
            lblAutoRecap.AutoSize = true;
            tblBackupRestore.SetColumnSpan(lblAutoRecap, 3);
            lblAutoRecap.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAutoRecap.ForeColor = Color.FromArgb(51, 65, 85);
            lblAutoRecap.Location = new Point(8, 28);
            lblAutoRecap.Margin = new Padding(0, 5, 0, 2);
            lblAutoRecap.Name = "lblAutoRecap";
            lblAutoRecap.Size = new Size(108, 15);
            lblAutoRecap.TabIndex = 1;
            lblAutoRecap.Text = "Auto Recap Every:";
            // 
            // cboAutoRecap
            // 
            cboAutoRecap.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tblBackupRestore.SetColumnSpan(cboAutoRecap, 3);
            cboAutoRecap.DropDownStyle = ComboBoxStyle.DropDownList;
            cboAutoRecap.FlatStyle = FlatStyle.Flat;
            cboAutoRecap.Font = new Font("Segoe UI", 9.5F);
            cboAutoRecap.FormattingEnabled = true;
            cboAutoRecap.Items.AddRange(new object[] { "Everyday", "7 days", "2 weeks", "1 month" });
            cboAutoRecap.Location = new Point(8, 45);
            cboAutoRecap.Margin = new Padding(0, 0, 8, 7);
            cboAutoRecap.Name = "cboAutoRecap";
            cboAutoRecap.Size = new Size(442, 25);
            cboAutoRecap.TabIndex = 2;
            // 
            // btnResetDefault
            // 
            btnResetDefault.BackColor = Color.FromArgb(250, 204, 21);
            tblBackupRestore.SetColumnSpan(btnResetDefault, 3);
            btnResetDefault.FlatStyle = FlatStyle.Flat;
            btnResetDefault.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnResetDefault.ForeColor = Color.FromArgb(15, 23, 42);
            btnResetDefault.HoverForeColor = Color.Empty;
            btnResetDefault.Location = new Point(466, 45);
            btnResetDefault.Margin = new Padding(8, 0, 0, 7);
            btnResetDefault.Name = "btnResetDefault";
            btnResetDefault.PressedForeColor = Color.Empty;
            btnResetDefault.Size = new Size(134, 25);
            btnResetDefault.TabIndex = 3;
            btnResetDefault.Text = "Reset to default";
            btnResetDefault.UseVisualStyleBackColor = true;
            btnResetDefault.Click += btnResetDefault_Click;
            // 
            // lblManualRecap
            // 
            lblManualRecap.AutoSize = true;
            tblBackupRestore.SetColumnSpan(lblManualRecap, 6);
            lblManualRecap.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblManualRecap.ForeColor = Color.FromArgb(51, 65, 85);
            lblManualRecap.Location = new Point(8, 82);
            lblManualRecap.Margin = new Padding(0, 5, 0, 2);
            lblManualRecap.Name = "lblManualRecap";
            lblManualRecap.Size = new Size(87, 15);
            lblManualRecap.TabIndex = 4;
            lblManualRecap.Text = "Manual Recap:";
            // 
            // txtManualRecapPath
            // 
            txtManualRecapPath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tblBackupRestore.SetColumnSpan(txtManualRecapPath, 4);
            txtManualRecapPath.Font = new Font("Segoe UI", 9.5F);
            txtManualRecapPath.Location = new Point(8, 99);
            txtManualRecapPath.Margin = new Padding(0, 0, 8, 7);
            txtManualRecapPath.Name = "txtManualRecapPath";
            txtManualRecapPath.ReadOnly = true;
            txtManualRecapPath.Size = new Size(592, 24);
            txtManualRecapPath.TabIndex = 5;
            // 
            // flpRecapBtns
            // 
            flpRecapBtns.AutoSize = true;
            tblBackupRestore.SetColumnSpan(flpRecapBtns, 2);
            flpRecapBtns.Controls.Add(btnBrowseRecap);
            flpRecapBtns.Controls.Add(btnRecap);
            flpRecapBtns.Location = new Point(616, 99);
            flpRecapBtns.Margin = new Padding(8, 0, 0, 7);
            flpRecapBtns.Name = "flpRecapBtns";
            flpRecapBtns.Size = new Size(186, 25);
            flpRecapBtns.TabIndex = 6;
            // 
            // btnBrowseRecap
            // 
            btnBrowseRecap.BackColor = Color.FromArgb(250, 204, 21);
            btnBrowseRecap.FlatStyle = FlatStyle.Flat;
            btnBrowseRecap.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnBrowseRecap.ForeColor = Color.FromArgb(15, 23, 42);
            btnBrowseRecap.HoverForeColor = Color.Empty;
            btnBrowseRecap.Location = new Point(0, 0);
            btnBrowseRecap.Margin = new Padding(0, 0, 12, 0);
            btnBrowseRecap.Name = "btnBrowseRecap";
            btnBrowseRecap.PressedForeColor = Color.Empty;
            btnBrowseRecap.Size = new Size(87, 25);
            btnBrowseRecap.TabIndex = 0;
            btnBrowseRecap.Text = "Browse";
            btnBrowseRecap.UseVisualStyleBackColor = true;
            btnBrowseRecap.Click += btnBrowseRecap_Click;
            // 
            // btnRecap
            // 
            btnRecap.BackColor = Color.FromArgb(250, 204, 21);
            btnRecap.FlatStyle = FlatStyle.Flat;
            btnRecap.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRecap.ForeColor = Color.FromArgb(15, 23, 42);
            btnRecap.HoverForeColor = Color.Empty;
            btnRecap.Location = new Point(99, 0);
            btnRecap.Margin = new Padding(0);
            btnRecap.Name = "btnRecap";
            btnRecap.PressedForeColor = Color.Empty;
            btnRecap.Size = new Size(87, 25);
            btnRecap.TabIndex = 1;
            btnRecap.Text = "Recap";
            btnRecap.UseVisualStyleBackColor = true;
            btnRecap.Click += btnRecap_Click;
            // 
            // lblRollbackTo
            // 
            lblRollbackTo.AutoSize = true;
            tblBackupRestore.SetColumnSpan(lblRollbackTo, 6);
            lblRollbackTo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblRollbackTo.ForeColor = Color.FromArgb(51, 65, 85);
            lblRollbackTo.Location = new Point(8, 136);
            lblRollbackTo.Margin = new Padding(0, 5, 0, 2);
            lblRollbackTo.Name = "lblRollbackTo";
            lblRollbackTo.Size = new Size(73, 15);
            lblRollbackTo.TabIndex = 7;
            lblRollbackTo.Text = "Rollback To:";
            // 
            // txtRollbackPath
            // 
            txtRollbackPath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tblBackupRestore.SetColumnSpan(txtRollbackPath, 4);
            txtRollbackPath.Font = new Font("Segoe UI", 9.5F);
            txtRollbackPath.Location = new Point(8, 153);
            txtRollbackPath.Margin = new Padding(0, 0, 8, 7);
            txtRollbackPath.Name = "txtRollbackPath";
            txtRollbackPath.ReadOnly = true;
            txtRollbackPath.Size = new Size(592, 24);
            txtRollbackPath.TabIndex = 8;
            // 
            // flpRollbackBtns
            // 
            flpRollbackBtns.AutoSize = true;
            tblBackupRestore.SetColumnSpan(flpRollbackBtns, 2);
            flpRollbackBtns.Controls.Add(btnBrowseRollback);
            flpRollbackBtns.Controls.Add(btnRollback);
            flpRollbackBtns.Location = new Point(616, 153);
            flpRollbackBtns.Margin = new Padding(8, 0, 0, 7);
            flpRollbackBtns.Name = "flpRollbackBtns";
            flpRollbackBtns.Size = new Size(186, 25);
            flpRollbackBtns.TabIndex = 9;
            // 
            // btnBrowseRollback
            // 
            btnBrowseRollback.BackColor = Color.FromArgb(250, 204, 21);
            btnBrowseRollback.FlatStyle = FlatStyle.Flat;
            btnBrowseRollback.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnBrowseRollback.ForeColor = Color.FromArgb(15, 23, 42);
            btnBrowseRollback.HoverForeColor = Color.Empty;
            btnBrowseRollback.Location = new Point(0, 0);
            btnBrowseRollback.Margin = new Padding(0, 0, 12, 0);
            btnBrowseRollback.Name = "btnBrowseRollback";
            btnBrowseRollback.PressedForeColor = Color.Empty;
            btnBrowseRollback.Size = new Size(87, 25);
            btnBrowseRollback.TabIndex = 0;
            btnBrowseRollback.Text = "Browse";
            btnBrowseRollback.UseVisualStyleBackColor = true;
            btnBrowseRollback.Click += btnBrowseRollback_Click;
            // 
            // btnRollback
            // 
            btnRollback.BackColor = Color.FromArgb(250, 204, 21);
            btnRollback.FlatStyle = FlatStyle.Flat;
            btnRollback.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRollback.ForeColor = Color.FromArgb(15, 23, 42);
            btnRollback.HoverForeColor = Color.Empty;
            btnRollback.Location = new Point(99, 0);
            btnRollback.Margin = new Padding(0);
            btnRollback.Name = "btnRollback";
            btnRollback.PressedForeColor = Color.Empty;
            btnRollback.Size = new Size(87, 25);
            btnRollback.TabIndex = 1;
            btnRollback.Text = "Rollback";
            btnRollback.UseVisualStyleBackColor = true;
            btnRollback.Click += btnRollback_Click;
            // 
            // SettingBackupRestore
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(tblMainLayout);
            Name = "SettingBackupRestore";
            Size = new Size(933, 462);
            tblMainLayout.ResumeLayout(false);
            tblMainLayout.PerformLayout();
            cardBackupRestore.ResumeLayout(false);
            cardBackupRestore.PerformLayout();
            tblBackupRestore.ResumeLayout(false);
            tblBackupRestore.PerformLayout();
            flpRecapBtns.ResumeLayout(false);
            flpRollbackBtns.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblMainLayout;
        private System.Windows.Forms.Panel cardBackupRestore;
        private System.Windows.Forms.TableLayoutPanel tblBackupRestore;
        private Controls.RoundedButton btnResetDefault;
        private System.Windows.Forms.FlowLayoutPanel flpRecapBtns;
        private Controls.RoundedButton btnBrowseRecap;
        private Controls.RoundedButton btnRecap;
        private System.Windows.Forms.FlowLayoutPanel flpRollbackBtns;
        private Controls.RoundedButton btnBrowseRollback;
        private Controls.RoundedButton btnRollback;
        private Label lblTitle;
        private Label lblAutoRecap;
        private ComboBox cboAutoRecap;
        private Label lblManualRecap;
        private TextBox txtManualRecapPath;
        private Label lblRollbackTo;
        private TextBox txtRollbackPath;
    }
}
