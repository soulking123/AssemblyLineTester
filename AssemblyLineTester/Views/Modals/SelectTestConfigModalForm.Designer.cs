using System.Drawing;
using System.Windows.Forms;

namespace AssemblyLineTester.Views.Modals
{
    partial class SelectTestConfigModalForm
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
            gridConfigs = new DataGridView();
            pnlFooter = new Panel();
            btnCancel = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnSelect = new AssemblyLineTester.Views.Controls.RoundedButton();
            pnlCustomHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconLogo).BeginInit();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridConfigs).BeginInit();
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
            pnlCustomHeader.Margin = new Padding(6);
            pnlCustomHeader.Name = "pnlCustomHeader";
            pnlCustomHeader.Size = new Size(1207, 107);
            pnlCustomHeader.TabIndex = 0;
            // 
            // lblCustomTitle
            // 
            lblCustomTitle.AutoSize = true;
            lblCustomTitle.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            lblCustomTitle.ForeColor = Color.White;
            lblCustomTitle.Location = new Point(102, 30);
            lblCustomTitle.Margin = new Padding(6, 0, 6, 0);
            lblCustomTitle.Name = "lblCustomTitle";
            lblCustomTitle.Size = new Size(367, 42);
            lblCustomTitle.TabIndex = 1;
            lblCustomTitle.Text = "SELECT TEST PROGRAM";
            // 
            // iconLogo
            // 
            iconLogo.BackColor = Color.Transparent;
            iconLogo.ForeColor = Color.FromArgb(250, 204, 21);
            iconLogo.IconChar = FontAwesome.Sharp.IconChar.Tasks;
            iconLogo.IconColor = Color.FromArgb(250, 204, 21);
            iconLogo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconLogo.IconSize = 59;
            iconLogo.Location = new Point(28, 19);
            iconLogo.Margin = new Padding(6);
            iconLogo.Name = "iconLogo";
            iconLogo.Size = new Size(59, 68);
            iconLogo.SizeMode = PictureBoxSizeMode.CenterImage;
            iconLogo.TabIndex = 0;
            iconLogo.TabStop = false;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(241, 245, 249);
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 107);
            pnlHeader.Margin = new Padding(6);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1207, 149);
            pnlHeader.TabIndex = 1;
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 9F);
            lblSubtitle.ForeColor = Color.FromArgb(148, 163, 184);
            lblSubtitle.Location = new Point(37, 90);
            lblSubtitle.Margin = new Padding(6, 0, 6, 0);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(685, 32);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Choose an active harness wiring program to load on the tester.";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.DimGray;
            lblTitle.Location = new Point(33, 32);
            lblTitle.Margin = new Padding(6, 0, 6, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(346, 51);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Harness Programs";
            // 
            // gridConfigs
            // 
            gridConfigs.AllowUserToAddRows = false;
            gridConfigs.AllowUserToDeleteRows = false;
            gridConfigs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridConfigs.BackgroundColor = Color.White;
            gridConfigs.BorderStyle = BorderStyle.None;
            gridConfigs.ColumnHeadersHeight = 35;
            gridConfigs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            gridConfigs.Dock = DockStyle.Fill;
            gridConfigs.Location = new Point(0, 256);
            gridConfigs.Margin = new Padding(6);
            gridConfigs.Name = "gridConfigs";
            gridConfigs.ReadOnly = true;
            gridConfigs.RowHeadersVisible = false;
            gridConfigs.RowHeadersWidth = 82;
            gridConfigs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridConfigs.Size = new Size(1207, 662);
            gridConfigs.TabIndex = 2;
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.FromArgb(34, 40, 49);
            pnlFooter.Controls.Add(btnCancel);
            pnlFooter.Controls.Add(btnSelect);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 918);
            pnlFooter.Margin = new Padding(6);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Size = new Size(1207, 149);
            pnlFooter.TabIndex = 3;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.BackColor = Color.FromArgb(241, 245, 249);
            btnCancel.BorderRadius = 16;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCancel.ForeColor = Color.Black;
            btnCancel.HoverBackColor = Color.FromArgb(226, 232, 240);
            btnCancel.HoverForeColor = Color.Empty;
            btnCancel.Location = new Point(728, 32);
            btnCancel.Margin = new Padding(6);
            btnCancel.Name = "btnCancel";
            btnCancel.NormalBackColor = Color.FromArgb(241, 245, 249);
            btnCancel.PressedBackColor = Color.FromArgb(203, 213, 225);
            btnCancel.PressedForeColor = Color.Empty;
            btnCancel.Size = new Size(204, 85);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnSelect
            // 
            btnSelect.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSelect.BackColor = Color.FromArgb(250, 204, 21);
            btnSelect.BorderRadius = 16;
            btnSelect.FlatAppearance.BorderSize = 0;
            btnSelect.FlatStyle = FlatStyle.Flat;
            btnSelect.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSelect.ForeColor = Color.FromArgb(30, 41, 59);
            btnSelect.HoverForeColor = Color.Empty;
            btnSelect.Location = new Point(944, 32);
            btnSelect.Margin = new Padding(6);
            btnSelect.Name = "btnSelect";
            btnSelect.PressedForeColor = Color.Empty;
            btnSelect.Size = new Size(222, 85);
            btnSelect.TabIndex = 0;
            btnSelect.Text = "Select Program";
            btnSelect.UseVisualStyleBackColor = false;
            // 
            // SelectTestConfigModalForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(1207, 1067);
            Controls.Add(gridConfigs);
            Controls.Add(pnlHeader);
            Controls.Add(pnlCustomHeader);
            Controls.Add(pnlFooter);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(6);
            Name = "SelectTestConfigModalForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Select Test Program";
            pnlCustomHeader.ResumeLayout(false);
            pnlCustomHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconLogo).EndInit();
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridConfigs).EndInit();
            pnlFooter.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlCustomHeader;
        private System.Windows.Forms.Label lblCustomTitle;
        private FontAwesome.Sharp.IconPictureBox iconLogo;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView gridConfigs;
        private System.Windows.Forms.Panel pnlFooter;
        private AssemblyLineTester.Views.Controls.RoundedButton btnCancel;
        private AssemblyLineTester.Views.Controls.RoundedButton btnSelect;
    }
}
