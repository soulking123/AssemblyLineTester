namespace AssemblyLineTester.Views.Pages
{
    partial class ProductDatabase
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
            pnlHeader = new System.Windows.Forms.Panel();
            lblTitle = new System.Windows.Forms.Label();
            flpTabs = new System.Windows.Forms.FlowLayoutPanel();
            lblTabProductModel = new System.Windows.Forms.Label();
            lblTabHarnessAssembly = new System.Windows.Forms.Label();
            lblTabConnector = new System.Windows.Forms.Label();
            pnlContentContainer = new System.Windows.Forms.Panel();
            pnlHeader.SuspendLayout();
            flpTabs.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = System.Drawing.Color.FromArgb(244, 245, 247);
            pnlHeader.Controls.Add(flpTabs);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader.Location = new System.Drawing.Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new System.Windows.Forms.Padding(20, 16, 20, 0);
            pnlHeader.Size = new System.Drawing.Size(1200, 88);
            pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblTitle.Location = new System.Drawing.Point(20, 16);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new System.Drawing.Size(217, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Product Database";
            // 
            // flpTabs
            // 
            flpTabs.Controls.Add(lblTabProductModel);
            flpTabs.Controls.Add(lblTabHarnessAssembly);
            flpTabs.Controls.Add(lblTabConnector);
            flpTabs.Dock = System.Windows.Forms.DockStyle.Bottom;
            flpTabs.Location = new System.Drawing.Point(20, 50);
            flpTabs.Name = "flpTabs";
            flpTabs.Size = new System.Drawing.Size(1160, 38);
            flpTabs.TabIndex = 1;
            // 
            // lblTabProductModel
            // 
            lblTabProductModel.AutoSize = true;
            lblTabProductModel.Cursor = System.Windows.Forms.Cursors.Hand;
            lblTabProductModel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            lblTabProductModel.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblTabProductModel.Location = new System.Drawing.Point(0, 0);
            lblTabProductModel.Margin = new System.Windows.Forms.Padding(0, 0, 24, 0);
            lblTabProductModel.Name = "lblTabProductModel";
            lblTabProductModel.Padding = new System.Windows.Forms.Padding(4, 4, 4, 8);
            lblTabProductModel.Size = new System.Drawing.Size(120, 32);
            lblTabProductModel.TabIndex = 0;
            lblTabProductModel.Text = "Product Model";
            // 
            // lblTabHarnessAssembly
            // 
            lblTabHarnessAssembly.AutoSize = true;
            lblTabHarnessAssembly.Cursor = System.Windows.Forms.Cursors.Hand;
            lblTabHarnessAssembly.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            lblTabHarnessAssembly.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblTabHarnessAssembly.Location = new System.Drawing.Point(144, 0);
            lblTabHarnessAssembly.Margin = new System.Windows.Forms.Padding(0, 0, 24, 0);
            lblTabHarnessAssembly.Name = "lblTabHarnessAssembly";
            lblTabHarnessAssembly.Padding = new System.Windows.Forms.Padding(4, 4, 4, 8);
            lblTabHarnessAssembly.Size = new System.Drawing.Size(147, 32);
            lblTabHarnessAssembly.TabIndex = 1;
            lblTabHarnessAssembly.Text = "Harness Assembly";
            // 
            // lblTabConnector
            // 
            lblTabConnector.AutoSize = true;
            lblTabConnector.Cursor = System.Windows.Forms.Cursors.Hand;
            lblTabConnector.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            lblTabConnector.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblTabConnector.Location = new System.Drawing.Point(315, 0);
            lblTabConnector.Margin = new System.Windows.Forms.Padding(0, 0, 24, 0);
            lblTabConnector.Name = "lblTabConnector";
            lblTabConnector.Padding = new System.Windows.Forms.Padding(4, 4, 4, 8);
            lblTabConnector.Size = new System.Drawing.Size(90, 32);
            lblTabConnector.TabIndex = 2;
            lblTabConnector.Text = "Connector";
            // 
            // pnlContentContainer
            // 
            pnlContentContainer.BackColor = System.Drawing.Color.FromArgb(244, 245, 247);
            pnlContentContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlContentContainer.Location = new System.Drawing.Point(0, 88);
            pnlContentContainer.Name = "pnlContentContainer";
            pnlContentContainer.Padding = new System.Windows.Forms.Padding(8, 0, 8, 8);
            pnlContentContainer.Size = new System.Drawing.Size(1200, 712);
            pnlContentContainer.TabIndex = 1;
            // 
            // ProductDatabase
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(244, 245, 247);
            Controls.Add(pnlContentContainer);
            Controls.Add(pnlHeader);
            Name = "ProductDatabase";
            Size = new System.Drawing.Size(1200, 800);
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            flpTabs.ResumeLayout(false);
            flpTabs.PerformLayout();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.FlowLayoutPanel flpTabs;
        private System.Windows.Forms.Label lblTabProductModel;
        private System.Windows.Forms.Label lblTabHarnessAssembly;
        private System.Windows.Forms.Label lblTabConnector;
        private System.Windows.Forms.Panel pnlContentContainer;
    }
}
