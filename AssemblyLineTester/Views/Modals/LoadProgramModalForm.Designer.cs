using System.Drawing;
using System.Windows.Forms;

namespace AssemblyLineTester.Views.Modals
{
    partial class LoadProgramModalForm
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
            iconLogo = new FontAwesome.Sharp.IconPictureBox();
            lblHeaderTitle = new Label();
            pnlFooter = new Panel();
            btnLoad = new Button();
            btnBrowseFile = new Button();
            btnCancel = new Button();
            pnlMainContainer = new Panel();
            pnlGridCard = new Panel();
            gridConfigs = new DataGridView();
            pnlSearch = new Panel();
            lblSearch = new Label();
            txtSearch = new TextBox();
            btnSearch = new Button();
            btnClear = new Button();
            pnlCustomHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconLogo).BeginInit();
            pnlFooter.SuspendLayout();
            pnlMainContainer.SuspendLayout();
            pnlGridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridConfigs).BeginInit();
            pnlSearch.SuspendLayout();
            SuspendLayout();
            // 
            // pnlCustomHeader
            // 
            pnlCustomHeader.BackColor = Color.FromArgb(34, 40, 49);
            pnlCustomHeader.Controls.Add(iconLogo);
            pnlCustomHeader.Controls.Add(lblHeaderTitle);
            pnlCustomHeader.Dock = DockStyle.Top;
            pnlCustomHeader.Location = new Point(0, 0);
            pnlCustomHeader.Margin = new Padding(6);
            pnlCustomHeader.Name = "pnlCustomHeader";
            pnlCustomHeader.Size = new Size(1495, 128);
            pnlCustomHeader.TabIndex = 0;
            // 
            // iconLogo
            // 
            iconLogo.BackColor = Color.Transparent;
            iconLogo.ForeColor = Color.FromArgb(56, 189, 248);
            iconLogo.IconChar = FontAwesome.Sharp.IconChar.FolderOpen;
            iconLogo.IconColor = Color.FromArgb(56, 189, 248);
            iconLogo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconLogo.IconSize = 64;
            iconLogo.Location = new Point(39, 32);
            iconLogo.Margin = new Padding(6);
            iconLogo.Name = "iconLogo";
            iconLogo.Size = new Size(65, 64);
            iconLogo.SizeMode = PictureBoxSizeMode.CenterImage;
            iconLogo.TabIndex = 0;
            iconLogo.TabStop = false;
            // 
            // lblHeaderTitle
            // 
            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblHeaderTitle.ForeColor = Color.White;
            lblHeaderTitle.Location = new Point(120, 38);
            lblHeaderTitle.Margin = new Padding(6, 0, 6, 0);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new Size(394, 47);
            lblHeaderTitle.TabIndex = 1;
            lblHeaderTitle.Text = "LOAD TEST PROGRAM";
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.FromArgb(34, 40, 49);
            pnlFooter.Controls.Add(btnLoad);
            pnlFooter.Controls.Add(btnBrowseFile);
            pnlFooter.Controls.Add(btnCancel);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 832);
            pnlFooter.Margin = new Padding(6);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Size = new Size(1495, 128);
            pnlFooter.TabIndex = 1;
            // 
            // btnLoad
            // 
            btnLoad.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLoad.BackColor = Color.FromArgb(22, 163, 74);
            btnLoad.FlatAppearance.BorderSize = 0;
            btnLoad.FlatStyle = FlatStyle.Flat;
            btnLoad.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLoad.ForeColor = Color.White;
            btnLoad.Location = new Point(725, 29);
            btnLoad.Margin = new Padding(6);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(234, 70);
            btnLoad.TabIndex = 0;
            btnLoad.Text = "Load";
            btnLoad.UseVisualStyleBackColor = false;
            // 
            // btnBrowseFile
            // 
            btnBrowseFile.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBrowseFile.BackColor = Color.FromArgb(14, 165, 233);
            btnBrowseFile.FlatAppearance.BorderSize = 0;
            btnBrowseFile.FlatStyle = FlatStyle.Flat;
            btnBrowseFile.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnBrowseFile.ForeColor = Color.White;
            btnBrowseFile.Location = new Point(978, 29);
            btnBrowseFile.Margin = new Padding(6);
            btnBrowseFile.Name = "btnBrowseFile";
            btnBrowseFile.Size = new Size(250, 70);
            btnBrowseFile.TabIndex = 1;
            btnBrowseFile.Text = "Browse File...";
            btnBrowseFile.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancel.BackColor = Color.FromArgb(226, 232, 240);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancel.ForeColor = Color.FromArgb(51, 65, 85);
            btnCancel.Location = new Point(1248, 29);
            btnCancel.Margin = new Padding(6);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(205, 70);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Close";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // pnlMainContainer
            // 
            pnlMainContainer.BackColor = Color.White;
            pnlMainContainer.Controls.Add(pnlGridCard);
            pnlMainContainer.Controls.Add(pnlSearch);
            pnlMainContainer.Dock = DockStyle.Fill;
            pnlMainContainer.Location = new Point(0, 128);
            pnlMainContainer.Margin = new Padding(6);
            pnlMainContainer.Name = "pnlMainContainer";
            pnlMainContainer.Padding = new Padding(32);
            pnlMainContainer.Size = new Size(1495, 704);
            pnlMainContainer.TabIndex = 2;
            // 
            // pnlGridCard
            // 
            pnlGridCard.BackColor = Color.FromArgb(248, 250, 252);
            pnlGridCard.Controls.Add(gridConfigs);
            pnlGridCard.Dock = DockStyle.Fill;
            pnlGridCard.Location = new Point(32, 122);
            pnlGridCard.Margin = new Padding(6);
            pnlGridCard.Name = "pnlGridCard";
            pnlGridCard.Padding = new Padding(20, 19, 20, 19);
            pnlGridCard.Size = new Size(1431, 550);
            pnlGridCard.TabIndex = 1;
            // 
            // gridConfigs
            // 
            gridConfigs.AllowUserToAddRows = false;
            gridConfigs.AllowUserToDeleteRows = false;
            gridConfigs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridConfigs.BackgroundColor = Color.White;
            gridConfigs.BorderStyle = BorderStyle.None;
            gridConfigs.ColumnHeadersHeight = 40;
            gridConfigs.Dock = DockStyle.Fill;
            gridConfigs.Location = new Point(20, 19);
            gridConfigs.Margin = new Padding(6);
            gridConfigs.Name = "gridConfigs";
            gridConfigs.ReadOnly = true;
            gridConfigs.RowHeadersVisible = false;
            gridConfigs.RowHeadersWidth = 51;
            gridConfigs.RowTemplate.Height = 36;
            gridConfigs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridConfigs.Size = new Size(1391, 512);
            gridConfigs.TabIndex = 0;
            // 
            // pnlSearch
            // 
            pnlSearch.Controls.Add(lblSearch);
            pnlSearch.Controls.Add(txtSearch);
            pnlSearch.Controls.Add(btnSearch);
            pnlSearch.Controls.Add(btnClear);
            pnlSearch.Dock = DockStyle.Top;
            pnlSearch.Location = new Point(32, 32);
            pnlSearch.Margin = new Padding(6);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Size = new Size(1431, 90);
            pnlSearch.TabIndex = 0;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSearch.ForeColor = Color.FromArgb(51, 65, 85);
            lblSearch.Location = new Point(6, 26);
            lblSearch.Margin = new Padding(6, 0, 6, 0);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(308, 36);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "Search by Config Name:";
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Segoe UI", 10.5F);
            txtSearch.Location = new Point(322, 19);
            txtSearch.Margin = new Padding(6);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(615, 45);
            txtSearch.TabIndex = 1;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(15, 23, 42);
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(959, 16);
            btnSearch.Margin = new Padding(6);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(162, 56);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = false;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.FromArgb(226, 232, 240);
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClear.ForeColor = Color.FromArgb(51, 65, 85);
            btnClear.Location = new Point(1138, 16);
            btnClear.Margin = new Padding(6);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(162, 56);
            btnClear.TabIndex = 3;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = false;
            // 
            // LoadProgramModalForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1495, 960);
            Controls.Add(pnlMainContainer);
            Controls.Add(pnlFooter);
            Controls.Add(pnlCustomHeader);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(6);
            Name = "LoadProgramModalForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Load Test Program";
            pnlCustomHeader.ResumeLayout(false);
            pnlCustomHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconLogo).EndInit();
            pnlFooter.ResumeLayout(false);
            pnlMainContainer.ResumeLayout(false);
            pnlGridCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridConfigs).EndInit();
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlCustomHeader;
        private FontAwesome.Sharp.IconPictureBox iconLogo;
        private Label lblHeaderTitle;
        private Panel pnlFooter;
        private Button btnLoad;
        private Button btnBrowseFile;
        private Button btnCancel;
        private Panel pnlMainContainer;
        private Panel pnlSearch;
        private Label lblSearch;
        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnClear;
        private Panel pnlGridCard;
        private DataGridView gridConfigs;
    }
}
