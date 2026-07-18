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
            this.pnlCustomHeader = new System.Windows.Forms.Panel();
            this.lblCustomTitle = new System.Windows.Forms.Label();
            this.iconLogo = new FontAwesome.Sharp.IconPictureBox();
            this.btnClose = new FontAwesome.Sharp.IconButton();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.gridConfigs = new System.Windows.Forms.DataGridView();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnCancel = new AssemblyLineTester.Views.Controls.RoundedButton();
            this.btnSelect = new AssemblyLineTester.Views.Controls.RoundedButton();
            this.pnlCustomHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconLogo)).BeginInit();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridConfigs)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCustomHeader
            // 
            this.pnlCustomHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(40)))), ((int)(((byte)(49)))));
            this.pnlCustomHeader.Controls.Add(this.lblCustomTitle);
            this.pnlCustomHeader.Controls.Add(this.iconLogo);
            this.pnlCustomHeader.Controls.Add(this.btnClose);
            this.pnlCustomHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCustomHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlCustomHeader.Name = "pnlCustomHeader";
            this.pnlCustomHeader.Size = new System.Drawing.Size(650, 50);
            this.pnlCustomHeader.TabIndex = 0;
            // 
            // lblCustomTitle
            // 
            this.lblCustomTitle.AutoSize = true;
            this.lblCustomTitle.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.lblCustomTitle.ForeColor = System.Drawing.Color.White;
            this.lblCustomTitle.Location = new System.Drawing.Point(55, 14);
            this.lblCustomTitle.Name = "lblCustomTitle";
            this.lblCustomTitle.Size = new System.Drawing.Size(200, 21);
            this.lblCustomTitle.TabIndex = 1;
            this.lblCustomTitle.Text = "SELECT TEST PROGRAM";
            // 
            // iconLogo
            // 
            this.iconLogo.BackColor = System.Drawing.Color.Transparent;
            this.iconLogo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(204)))), ((int)(((byte)(21)))));
            this.iconLogo.IconChar = FontAwesome.Sharp.IconChar.Tasks;
            this.iconLogo.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(204)))), ((int)(((byte)(21)))));
            this.iconLogo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconLogo.IconSize = 32;
            this.iconLogo.Location = new System.Drawing.Point(15, 9);
            this.iconLogo.Name = "iconLogo";
            this.iconLogo.Size = new System.Drawing.Size(32, 32);
            this.iconLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.iconLogo.TabIndex = 0;
            this.iconLogo.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.IconChar = FontAwesome.Sharp.IconChar.Times;
            this.btnClose.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnClose.IconSize = 20;
            this.btnClose.Location = new System.Drawing.Point(600, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(40, 40);
            this.btnClose.TabIndex = 2;
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 50);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(650, 70);
            this.pnlHeader.TabIndex = 1;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblSubtitle.Location = new System.Drawing.Point(20, 42);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(326, 15);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Choose an active harness wiring program to load on the tester.";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(18, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(176, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Harness Programs";
            // 
            // gridConfigs
            // 
            this.gridConfigs.AllowUserToAddRows = false;
            this.gridConfigs.AllowUserToDeleteRows = false;
            this.gridConfigs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridConfigs.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.gridConfigs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridConfigs.ColumnHeadersHeight = 35;
            this.gridConfigs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridConfigs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridConfigs.Location = new System.Drawing.Point(0, 120);
            this.gridConfigs.Name = "gridConfigs";
            this.gridConfigs.ReadOnly = true;
            this.gridConfigs.RowHeadersVisible = false;
            this.gridConfigs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridConfigs.Size = new System.Drawing.Size(650, 310);
            this.gridConfigs.TabIndex = 2;
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlFooter.Controls.Add(this.btnCancel);
            this.pnlFooter.Controls.Add(this.btnSelect);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 430);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(650, 70);
            this.pnlFooter.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnCancel.BorderRadius = 16;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.HoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnCancel.Location = new System.Drawing.Point(392, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.NormalBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnCancel.PressedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnCancel.Size = new System.Drawing.Size(110, 40);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(204)))), ((int)(((byte)(21)))));
            this.btnSelect.BorderRadius = 16;
            this.btnSelect.FlatAppearance.BorderSize = 0;
            this.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelect.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSelect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnSelect.HoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(216)))), ((int)(((byte)(53)))));
            this.btnSelect.Location = new System.Drawing.Point(518, 15);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.NormalBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(204)))), ((int)(((byte)(21)))));
            this.btnSelect.PressedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(186)))), ((int)(((byte)(10)))));
            this.btnSelect.Size = new System.Drawing.Size(110, 40);
            this.btnSelect.TabIndex = 0;
            this.btnSelect.Text = "Select Program";
            this.btnSelect.UseVisualStyleBackColor = false;
            // 
            // SelectTestConfigModalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.ClientSize = new System.Drawing.Size(650, 500);
            this.Controls.Add(this.gridConfigs);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlCustomHeader);
            this.Controls.Add(this.pnlFooter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SelectTestConfigModalForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Test Program";
            this.pnlCustomHeader.ResumeLayout(false);
            this.pnlCustomHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconLogo)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridConfigs)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlCustomHeader;
        private System.Windows.Forms.Label lblCustomTitle;
        private FontAwesome.Sharp.IconPictureBox iconLogo;
        private FontAwesome.Sharp.IconButton btnClose;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView gridConfigs;
        private System.Windows.Forms.Panel pnlFooter;
        private AssemblyLineTester.Views.Controls.RoundedButton btnCancel;
        private AssemblyLineTester.Views.Controls.RoundedButton btnSelect;
    }
}
