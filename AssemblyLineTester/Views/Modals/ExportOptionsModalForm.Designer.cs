namespace AssemblyLineTester.Views.Modals
{
    partial class ExportOptionsModalForm
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
            pnlCustomHeader = new System.Windows.Forms.Panel();
            lblTitle = new System.Windows.Forms.Label();
            iconLogo = new FontAwesome.Sharp.IconPictureBox();
            pnlContent = new System.Windows.Forms.Panel();
            flowOptions = new System.Windows.Forms.FlowLayoutPanel();
            chkDatabase = new System.Windows.Forms.CheckBox();
            chkExcel = new System.Windows.Forms.CheckBox();
            pnlBottom = new System.Windows.Forms.Panel();
            btnCancel = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnProceed = new AssemblyLineTester.Views.Controls.RoundedButton();
            pnlCustomHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconLogo).BeginInit();
            pnlContent.SuspendLayout();
            flowOptions.SuspendLayout();
            pnlBottom.SuspendLayout();
            SuspendLayout();
            // 
            // pnlCustomHeader
            // 
            pnlCustomHeader.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            pnlCustomHeader.Controls.Add(lblTitle);
            pnlCustomHeader.Controls.Add(iconLogo);
            pnlCustomHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlCustomHeader.Location = new System.Drawing.Point(0, 0);
            pnlCustomHeader.Margin = new System.Windows.Forms.Padding(6);
            pnlCustomHeader.Name = "pnlCustomHeader";
            pnlCustomHeader.Size = new System.Drawing.Size(743, 85);
            pnlCustomHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.FromArgb(241, 245, 249);
            lblTitle.Location = new System.Drawing.Point(72, 17);
            lblTitle.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new System.Drawing.Size(326, 47);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Save Configuration";
            // 
            // iconLogo
            // 
            iconLogo.BackColor = System.Drawing.Color.Transparent;
            iconLogo.ForeColor = System.Drawing.Color.FromArgb(250, 204, 21);
            iconLogo.IconChar = FontAwesome.Sharp.IconChar.Save;
            iconLogo.IconColor = System.Drawing.Color.FromArgb(250, 204, 21);
            iconLogo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconLogo.IconSize = 56;
            iconLogo.Location = new System.Drawing.Point(15, 6);
            iconLogo.Margin = new System.Windows.Forms.Padding(6);
            iconLogo.Name = "iconLogo";
            iconLogo.Size = new System.Drawing.Size(56, 73);
            iconLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            iconLogo.TabIndex = 0;
            iconLogo.TabStop = false;
            // 
            // pnlContent
            // 
            pnlContent.BackColor = System.Drawing.Color.White;
            pnlContent.Controls.Add(flowOptions);
            pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlContent.Location = new System.Drawing.Point(0, 85);
            pnlContent.Margin = new System.Windows.Forms.Padding(6);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new System.Drawing.Size(743, 427);
            pnlContent.TabIndex = 1;
            // 
            // flowOptions
            // 
            flowOptions.AutoScroll = true;
            flowOptions.BackColor = System.Drawing.Color.Transparent;
            flowOptions.Controls.Add(chkDatabase);
            flowOptions.Controls.Add(chkExcel);
            flowOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            flowOptions.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            flowOptions.Location = new System.Drawing.Point(0, 0);
            flowOptions.Margin = new System.Windows.Forms.Padding(6);
            flowOptions.Name = "flowOptions";
            flowOptions.Padding = new System.Windows.Forms.Padding(56, 16, 12, 12);
            flowOptions.Size = new System.Drawing.Size(743, 427);
            flowOptions.TabIndex = 0;
            flowOptions.WrapContents = false;
            // 
            // chkDatabase
            // 
            chkDatabase.AutoSize = true;
            chkDatabase.BackColor = System.Drawing.Color.Transparent;
            chkDatabase.Checked = true;
            chkDatabase.CheckState = System.Windows.Forms.CheckState.Checked;
            chkDatabase.Font = new System.Drawing.Font("Segoe UI", 12F);
            chkDatabase.ForeColor = System.Drawing.Color.Black;
            chkDatabase.Location = new System.Drawing.Point(62, 49);
            chkDatabase.Margin = new System.Windows.Forms.Padding(6);
            chkDatabase.Name = "chkDatabase";
            chkDatabase.Size = new System.Drawing.Size(306, 49);
            chkDatabase.TabIndex = 0;
            chkDatabase.Text = "Save to Database";
            chkDatabase.UseVisualStyleBackColor = false;
            // 
            // chkExcel
            // 
            chkExcel.AutoSize = true;
            chkExcel.BackColor = System.Drawing.Color.Transparent;
            chkExcel.Checked = true;
            chkExcel.CheckState = System.Windows.Forms.CheckState.Checked;
            chkExcel.Font = new System.Drawing.Font("Segoe UI", 12F);
            chkExcel.ForeColor = System.Drawing.Color.Black;
            chkExcel.Location = new System.Drawing.Point(62, 110);
            chkExcel.Margin = new System.Windows.Forms.Padding(6);
            chkExcel.Name = "chkExcel";
            chkExcel.Size = new System.Drawing.Size(341, 49);
            chkExcel.TabIndex = 1;
            chkExcel.Text = "Export to Excel file";
            chkExcel.UseVisualStyleBackColor = false;
            // 
            // pnlBottom
            // 
            pnlBottom.BackColor = System.Drawing.Color.FromArgb(34, 40, 49);
            pnlBottom.Controls.Add(btnCancel);
            pnlBottom.Controls.Add(btnProceed);
            pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            pnlBottom.Location = new System.Drawing.Point(0, 512);
            pnlBottom.Margin = new System.Windows.Forms.Padding(6);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Size = new System.Drawing.Size(743, 128);
            pnlBottom.TabIndex = 2;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnCancel.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            btnCancel.BorderRadius = 6;
            btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnCancel.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            btnCancel.HoverBackColor = System.Drawing.Color.FromArgb(203, 213, 225);
            btnCancel.HoverForeColor = System.Drawing.Color.Empty;
            btnCancel.Location = new System.Drawing.Point(357, 26);
            btnCancel.Margin = new System.Windows.Forms.Padding(6);
            btnCancel.Name = "btnCancel";
            btnCancel.NormalBackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            btnCancel.PressedBackColor = System.Drawing.Color.FromArgb(203, 213, 225);
            btnCancel.PressedForeColor = System.Drawing.Color.Empty;
            btnCancel.Size = new System.Drawing.Size(167, 68);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnProceed
            // 
            btnProceed.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnProceed.BackColor = System.Drawing.Color.FromArgb(14, 165, 233);
            btnProceed.BorderRadius = 6;
            btnProceed.Cursor = System.Windows.Forms.Cursors.Hand;
            btnProceed.FlatAppearance.BorderSize = 0;
            btnProceed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnProceed.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnProceed.ForeColor = System.Drawing.Color.Black;
            btnProceed.HoverBackColor = System.Drawing.Color.FromArgb(2, 132, 199);
            btnProceed.HoverForeColor = System.Drawing.Color.Empty;
            btnProceed.Location = new System.Drawing.Point(535, 26);
            btnProceed.Margin = new System.Windows.Forms.Padding(6);
            btnProceed.Name = "btnProceed";
            btnProceed.NormalBackColor = System.Drawing.Color.FromArgb(14, 165, 233);
            btnProceed.PressedBackColor = System.Drawing.Color.FromArgb(2, 132, 199);
            btnProceed.PressedForeColor = System.Drawing.Color.Empty;
            btnProceed.Size = new System.Drawing.Size(167, 68);
            btnProceed.TabIndex = 0;
            btnProceed.Text = "Proceed";
            btnProceed.UseVisualStyleBackColor = false;
            // 
            // ExportOptionsModalForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            ClientSize = new System.Drawing.Size(743, 640);
            Controls.Add(pnlContent);
            Controls.Add(pnlBottom);
            Controls.Add(pnlCustomHeader);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Margin = new System.Windows.Forms.Padding(6);
            Name = "ExportOptionsModalForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Save Configuration";
            pnlCustomHeader.ResumeLayout(false);
            pnlCustomHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconLogo).EndInit();
            pnlContent.ResumeLayout(false);
            flowOptions.ResumeLayout(false);
            flowOptions.PerformLayout();
            pnlBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlCustomHeader;
        private System.Windows.Forms.Label lblTitle;
        private FontAwesome.Sharp.IconPictureBox iconLogo;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.FlowLayoutPanel flowOptions;
        private System.Windows.Forms.CheckBox chkDatabase;
        private System.Windows.Forms.CheckBox chkExcel;
        private System.Windows.Forms.Panel pnlBottom;
        private AssemblyLineTester.Views.Controls.RoundedButton btnCancel;
        private AssemblyLineTester.Views.Controls.RoundedButton btnProceed;
    }
}
