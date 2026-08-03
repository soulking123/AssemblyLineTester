namespace AssemblyLineTester.Views.Pages.Tabs
{
    partial class HarnessConnectorTab
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
            tableLayoutPanelConnector = new TableLayoutPanel();
            panelConnectorList = new Panel();
            tblConnectorListLayout = new TableLayoutPanel();
            panelConnectorTop = new TableLayoutPanel();
            btnAddConnector = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnEditConnector = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnDeleteConnector = new AssemblyLineTester.Views.Controls.RoundedButton();
            tblConnectorSearch = new TableLayoutPanel();
            txtConnectorSearch = new TextBox();
            btnConnectorSearch = new AssemblyLineTester.Views.Controls.RoundedButton();
            dgvConnectors = new DataGridView();
            tblConnectorPagination = new TableLayoutPanel();
            flpConnectorPages = new FlowLayoutPanel();
            flpConnectorShowEntries = new FlowLayoutPanel();
            lblConnectorShowEntries2 = new Label();
            cmbConnectorShowEntries = new ComboBox();
            lblConnectorShowEntries1 = new Label();
            panelConnectorDetail = new Panel();
            tableLayoutPanelConnectorDetail = new TableLayoutPanel();
            panelConnectorInfo = new Panel();
            tblConnectorInfo = new TableLayoutPanel();
            panelConnectorImage = new Panel();
            btnChangeConnectorImage = new AssemblyLineTester.Views.Controls.RoundedButton();
            tblConnectorImages = new TableLayoutPanel();
            lblConnectorImage = new Label();
            picConnectorImage = new PictureBox();
            tableLayoutPanelConnector.SuspendLayout();
            panelConnectorList.SuspendLayout();
            tblConnectorListLayout.SuspendLayout();
            panelConnectorTop.SuspendLayout();
            tblConnectorSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvConnectors).BeginInit();
            tblConnectorPagination.SuspendLayout();
            flpConnectorShowEntries.SuspendLayout();
            panelConnectorDetail.SuspendLayout();
            tableLayoutPanelConnectorDetail.SuspendLayout();
            panelConnectorInfo.SuspendLayout();
            panelConnectorImage.SuspendLayout();
            tblConnectorImages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picConnectorImage).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanelConnector
            // 
            tableLayoutPanelConnector.ColumnCount = 2;
            tableLayoutPanelConnector.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelConnector.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelConnector.Controls.Add(panelConnectorList, 0, 0);
            tableLayoutPanelConnector.Controls.Add(panelConnectorDetail, 1, 0);
            tableLayoutPanelConnector.Dock = DockStyle.Fill;
            tableLayoutPanelConnector.Location = new Point(0, 0);
            tableLayoutPanelConnector.Margin = new Padding(0);
            tableLayoutPanelConnector.Name = "tableLayoutPanelConnector";
            tableLayoutPanelConnector.RowCount = 1;
            tableLayoutPanelConnector.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelConnector.Size = new Size(1625, 960);
            tableLayoutPanelConnector.TabIndex = 0;
            // 
            // panelConnectorList
            // 
            panelConnectorList.Controls.Add(tblConnectorListLayout);
            panelConnectorList.Dock = DockStyle.Fill;
            panelConnectorList.Location = new Point(0, 0);
            panelConnectorList.Margin = new Padding(0, 0, 26, 0);
            panelConnectorList.Name = "panelConnectorList";
            panelConnectorList.Size = new Size(786, 960);
            panelConnectorList.TabIndex = 0;
            // 
            // tblConnectorListLayout
            // 
            tblConnectorListLayout.ColumnCount = 1;
            tblConnectorListLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblConnectorListLayout.Controls.Add(panelConnectorTop, 0, 0);
            tblConnectorListLayout.Controls.Add(tblConnectorSearch, 0, 1);
            tblConnectorListLayout.Controls.Add(dgvConnectors, 0, 2);
            tblConnectorListLayout.Controls.Add(tblConnectorPagination, 0, 3);
            tblConnectorListLayout.Dock = DockStyle.Fill;
            tblConnectorListLayout.Location = new Point(0, 0);
            tblConnectorListLayout.Margin = new Padding(0);
            tblConnectorListLayout.Name = "tblConnectorListLayout";
            tblConnectorListLayout.RowCount = 4;
            tblConnectorListLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            tblConnectorListLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 74F));
            tblConnectorListLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblConnectorListLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 114F));
            tblConnectorListLayout.Size = new Size(786, 960);
            tblConnectorListLayout.TabIndex = 0;
            // 
            // panelConnectorTop
            // 
            panelConnectorTop.ColumnCount = 3;
            panelConnectorTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            panelConnectorTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            panelConnectorTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            panelConnectorTop.Controls.Add(btnAddConnector, 0, 0);
            panelConnectorTop.Controls.Add(btnEditConnector, 1, 0);
            panelConnectorTop.Controls.Add(btnDeleteConnector, 2, 0);
            panelConnectorTop.Dock = DockStyle.Fill;
            panelConnectorTop.Location = new Point(0, 0);
            panelConnectorTop.Margin = new Padding(0);
            panelConnectorTop.Name = "panelConnectorTop";
            panelConnectorTop.RowCount = 1;
            panelConnectorTop.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            panelConnectorTop.Size = new Size(786, 70);
            panelConnectorTop.TabIndex = 0;
            // 
            // btnAddConnector
            // 
            btnAddConnector.NormalBackColor = Color.FromArgb(250, 204, 21);
            btnAddConnector.HoverBackColor = Color.FromArgb(254, 216, 53);
            btnAddConnector.PressedBackColor = Color.FromArgb(234, 186, 10);
            btnAddConnector.BorderRadius = 8;
            btnAddConnector.Dock = DockStyle.Fill;
            btnAddConnector.FlatAppearance.BorderSize = 0;
            btnAddConnector.FlatStyle = FlatStyle.Flat;
            btnAddConnector.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAddConnector.Location = new Point(3, 3);
            btnAddConnector.Margin = new Padding(3, 3, 5, 3);
            btnAddConnector.Name = "btnAddConnector";
            btnAddConnector.Size = new Size(254, 64);
            btnAddConnector.TabIndex = 0;
            btnAddConnector.Text = "➕ Add";
            btnAddConnector.UseVisualStyleBackColor = false;
            // 
            // btnEditConnector
            // 
            btnEditConnector.NormalBackColor = Color.FromArgb(241, 245, 249);
            btnEditConnector.HoverBackColor = Color.FromArgb(241, 245, 249);
            btnEditConnector.PressedBackColor = Color.FromArgb(226, 232, 240);
            btnEditConnector.BorderRadius = 8;
            btnEditConnector.Dock = DockStyle.Fill;
            btnEditConnector.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnEditConnector.FlatStyle = FlatStyle.Flat;
            btnEditConnector.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEditConnector.Location = new Point(267, 3);
            btnEditConnector.Margin = new Padding(5, 3, 5, 3);
            btnEditConnector.Name = "btnEditConnector";
            btnEditConnector.Size = new Size(252, 64);
            btnEditConnector.TabIndex = 1;
            btnEditConnector.Text = "✏ Edit";
            btnEditConnector.UseVisualStyleBackColor = false;
            // 
            // btnDeleteConnector
            // 
            btnDeleteConnector.NormalBackColor = Color.FromArgb(254, 242, 242);
            btnDeleteConnector.HoverBackColor = Color.FromArgb(254, 226, 226);
            btnDeleteConnector.PressedBackColor = Color.FromArgb(252, 165, 165);
            btnDeleteConnector.BorderRadius = 8;
            btnDeleteConnector.Dock = DockStyle.Fill;
            btnDeleteConnector.FlatAppearance.BorderColor = Color.FromArgb(254, 202, 202);
            btnDeleteConnector.FlatStyle = FlatStyle.Flat;
            btnDeleteConnector.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDeleteConnector.ForeColor = Color.FromArgb(239, 68, 68);
            btnDeleteConnector.Location = new Point(529, 3);
            btnDeleteConnector.Margin = new Padding(5, 3, 3, 3);
            btnDeleteConnector.Name = "btnDeleteConnector";
            btnDeleteConnector.Size = new Size(254, 64);
            btnDeleteConnector.TabIndex = 2;
            btnDeleteConnector.Text = "🗑 Delete";
            btnDeleteConnector.UseVisualStyleBackColor = false;
            // 
            // tblConnectorSearch
            // 
            tblConnectorSearch.ColumnCount = 2;
            tblConnectorSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblConnectorSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            tblConnectorSearch.Controls.Add(txtConnectorSearch, 0, 0);
            tblConnectorSearch.Controls.Add(btnConnectorSearch, 1, 0);
            tblConnectorSearch.Dock = DockStyle.Fill;
            tblConnectorSearch.Location = new Point(0, 70);
            tblConnectorSearch.Margin = new Padding(0);
            tblConnectorSearch.Name = "tblConnectorSearch";
            tblConnectorSearch.RowCount = 1;
            tblConnectorSearch.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblConnectorSearch.Size = new Size(786, 74);
            tblConnectorSearch.TabIndex = 1;
            // 
            // txtConnectorSearch
            // 
            txtConnectorSearch.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtConnectorSearch.Font = new Font("Segoe UI", 9.5F);
            txtConnectorSearch.Location = new Point(3, 16);
            txtConnectorSearch.Name = "txtConnectorSearch";
            txtConnectorSearch.PlaceholderText = "Search by Part Number...";
            txtConnectorSearch.Size = new Size(620, 41);
            txtConnectorSearch.TabIndex = 0;
            // 
            // btnConnectorSearch
            // 
            btnConnectorSearch.Anchor = AnchorStyles.Right;
            btnConnectorSearch.NormalBackColor = Color.FromArgb(241, 245, 249);
            btnConnectorSearch.HoverBackColor = Color.FromArgb(241, 245, 249);
            btnConnectorSearch.PressedBackColor = Color.FromArgb(226, 232, 240);
            btnConnectorSearch.BorderRadius = 8;
            btnConnectorSearch.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnConnectorSearch.FlatStyle = FlatStyle.Flat;
            btnConnectorSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnConnectorSearch.Location = new Point(629, 16);
            btnConnectorSearch.Name = "btnConnectorSearch";
            btnConnectorSearch.Size = new Size(154, 42);
            btnConnectorSearch.TabIndex = 1;
            btnConnectorSearch.Text = "🔍 Search";
            btnConnectorSearch.UseVisualStyleBackColor = false;
            // 
            // dgvConnectors
            // 
            dgvConnectors.BackgroundColor = Color.White;
            dgvConnectors.BorderStyle = BorderStyle.None;
            dgvConnectors.ColumnHeadersHeight = 46;
            dgvConnectors.Dock = DockStyle.Fill;
            dgvConnectors.Location = new Point(0, 144);
            dgvConnectors.Margin = new Padding(0);
            dgvConnectors.Name = "dgvConnectors";
            dgvConnectors.RowHeadersWidth = 82;
            dgvConnectors.Size = new Size(786, 702);
            dgvConnectors.TabIndex = 2;
            // 
            // tblConnectorPagination
            // 
            tblConnectorPagination.ColumnCount = 2;
            tblConnectorPagination.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tblConnectorPagination.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tblConnectorPagination.Controls.Add(flpConnectorPages, 0, 0);
            tblConnectorPagination.Controls.Add(flpConnectorShowEntries, 1, 0);
            tblConnectorPagination.Dock = DockStyle.Fill;
            tblConnectorPagination.Location = new Point(0, 846);
            tblConnectorPagination.Margin = new Padding(0);
            tblConnectorPagination.Name = "tblConnectorPagination";
            tblConnectorPagination.RowCount = 1;
            tblConnectorPagination.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblConnectorPagination.Size = new Size(786, 114);
            tblConnectorPagination.TabIndex = 3;
            // 
            // flpConnectorPages
            // 
            flpConnectorPages.Dock = DockStyle.Fill;
            flpConnectorPages.Location = new Point(0, 0);
            flpConnectorPages.Margin = new Padding(0);
            flpConnectorPages.Name = "flpConnectorPages";
            flpConnectorPages.Size = new Size(353, 114);
            flpConnectorPages.TabIndex = 0;
            // 
            // flpConnectorShowEntries
            // 
            flpConnectorShowEntries.Controls.Add(lblConnectorShowEntries2);
            flpConnectorShowEntries.Controls.Add(cmbConnectorShowEntries);
            flpConnectorShowEntries.Controls.Add(lblConnectorShowEntries1);
            flpConnectorShowEntries.Dock = DockStyle.Fill;
            flpConnectorShowEntries.FlowDirection = FlowDirection.RightToLeft;
            flpConnectorShowEntries.Location = new Point(353, 0);
            flpConnectorShowEntries.Margin = new Padding(0);
            flpConnectorShowEntries.Name = "flpConnectorShowEntries";
            flpConnectorShowEntries.Size = new Size(433, 114);
            flpConnectorShowEntries.TabIndex = 1;
            flpConnectorShowEntries.WrapContents = false;
            // 
            // lblConnectorShowEntries2
            // 
            lblConnectorShowEntries2.AutoSize = true;
            lblConnectorShowEntries2.Font = new Font("Segoe UI", 9F);
            lblConnectorShowEntries2.ForeColor = Color.FromArgb(100, 116, 139);
            lblConnectorShowEntries2.Location = new Point(347, 19);
            lblConnectorShowEntries2.Margin = new Padding(6, 19, 0, 0);
            lblConnectorShowEntries2.Name = "lblConnectorShowEntries2";
            lblConnectorShowEntries2.Size = new Size(86, 32);
            lblConnectorShowEntries2.TabIndex = 0;
            lblConnectorShowEntries2.Text = "entries";
            // 
            // cmbConnectorShowEntries
            // 
            cmbConnectorShowEntries.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbConnectorShowEntries.Font = new Font("Segoe UI", 9F);
            cmbConnectorShowEntries.FormattingEnabled = true;
            cmbConnectorShowEntries.Items.AddRange(new object[] { "10", "25", "50" });
            cmbConnectorShowEntries.Location = new Point(266, 13);
            cmbConnectorShowEntries.Margin = new Padding(6, 13, 6, 0);
            cmbConnectorShowEntries.Name = "cmbConnectorShowEntries";
            cmbConnectorShowEntries.Size = new Size(69, 40);
            cmbConnectorShowEntries.TabIndex = 1;
            // 
            // lblConnectorShowEntries1
            // 
            lblConnectorShowEntries1.AutoSize = true;
            lblConnectorShowEntries1.Font = new Font("Segoe UI", 9F);
            lblConnectorShowEntries1.ForeColor = Color.FromArgb(100, 116, 139);
            lblConnectorShowEntries1.Location = new Point(182, 19);
            lblConnectorShowEntries1.Margin = new Padding(0, 19, 6, 0);
            lblConnectorShowEntries1.Name = "lblConnectorShowEntries1";
            lblConnectorShowEntries1.Size = new Size(72, 32);
            lblConnectorShowEntries1.TabIndex = 2;
            lblConnectorShowEntries1.Text = "Show";
            // 
            // panelConnectorDetail
            // 
            panelConnectorDetail.Controls.Add(tableLayoutPanelConnectorDetail);
            panelConnectorDetail.Dock = DockStyle.Fill;
            panelConnectorDetail.Location = new Point(812, 0);
            panelConnectorDetail.Margin = new Padding(0);
            panelConnectorDetail.Name = "panelConnectorDetail";
            panelConnectorDetail.Size = new Size(813, 960);
            panelConnectorDetail.TabIndex = 1;
            // 
            // tableLayoutPanelConnectorDetail
            // 
            tableLayoutPanelConnectorDetail.ColumnCount = 1;
            tableLayoutPanelConnectorDetail.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelConnectorDetail.Controls.Add(panelConnectorInfo, 0, 0);
            tableLayoutPanelConnectorDetail.Controls.Add(panelConnectorImage, 0, 1);
            tableLayoutPanelConnectorDetail.Dock = DockStyle.Fill;
            tableLayoutPanelConnectorDetail.Location = new Point(0, 0);
            tableLayoutPanelConnectorDetail.Margin = new Padding(0);
            tableLayoutPanelConnectorDetail.Name = "tableLayoutPanelConnectorDetail";
            tableLayoutPanelConnectorDetail.RowCount = 2;
            tableLayoutPanelConnectorDetail.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelConnectorDetail.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelConnectorDetail.Size = new Size(813, 960);
            tableLayoutPanelConnectorDetail.TabIndex = 0;
            // 
            // panelConnectorInfo
            // 
            panelConnectorInfo.Controls.Add(tblConnectorInfo);
            panelConnectorInfo.Dock = DockStyle.Fill;
            panelConnectorInfo.Location = new Point(0, 0);
            panelConnectorInfo.Margin = new Padding(0, 0, 0, 13);
            panelConnectorInfo.Name = "panelConnectorInfo";
            panelConnectorInfo.Size = new Size(813, 467);
            panelConnectorInfo.TabIndex = 0;
            // 
            // tblConnectorInfo
            // 
            tblConnectorInfo.AutoScroll = true;
            tblConnectorInfo.ColumnCount = 2;
            tblConnectorInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 274F));
            tblConnectorInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblConnectorInfo.Dock = DockStyle.Fill;
            tblConnectorInfo.Location = new Point(0, 0);
            tblConnectorInfo.Margin = new Padding(0);
            tblConnectorInfo.Name = "tblConnectorInfo";
            tblConnectorInfo.RowCount = 6;
            tblConnectorInfo.RowStyles.Add(new RowStyle());
            tblConnectorInfo.RowStyles.Add(new RowStyle());
            tblConnectorInfo.RowStyles.Add(new RowStyle());
            tblConnectorInfo.RowStyles.Add(new RowStyle());
            tblConnectorInfo.RowStyles.Add(new RowStyle());
            tblConnectorInfo.RowStyles.Add(new RowStyle());
            tblConnectorInfo.RowStyles.Add(new RowStyle());
            tblConnectorInfo.RowStyles.Add(new RowStyle());
            tblConnectorInfo.Size = new Size(813, 467);
            tblConnectorInfo.TabIndex = 0;
            // 
            // panelConnectorImage
            // 
            panelConnectorImage.Controls.Add(btnChangeConnectorImage);
            panelConnectorImage.Controls.Add(tblConnectorImages);
            panelConnectorImage.Dock = DockStyle.Fill;
            panelConnectorImage.Location = new Point(0, 493);
            panelConnectorImage.Margin = new Padding(0, 13, 0, 0);
            panelConnectorImage.Name = "panelConnectorImage";
            panelConnectorImage.Size = new Size(813, 467);
            panelConnectorImage.TabIndex = 1;
            // 
            // btnChangeConnectorImage
            // 
            btnChangeConnectorImage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnChangeConnectorImage.NormalBackColor = Color.FromArgb(248, 250, 252);
            btnChangeConnectorImage.HoverBackColor = Color.FromArgb(241, 245, 249);
            btnChangeConnectorImage.PressedBackColor = Color.FromArgb(226, 232, 240);
            btnChangeConnectorImage.BorderRadius = 8;
            btnChangeConnectorImage.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnChangeConnectorImage.FlatStyle = FlatStyle.Flat;
            btnChangeConnectorImage.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnChangeConnectorImage.Location = new Point(2239, 10);
            btnChangeConnectorImage.Name = "btnChangeConnectorImage";
            btnChangeConnectorImage.Size = new Size(10, 10);
            btnChangeConnectorImage.TabIndex = 0;
            btnChangeConnectorImage.Text = "📷 Change Image";
            btnChangeConnectorImage.UseVisualStyleBackColor = false;
            btnChangeConnectorImage.Visible = false;
            // 
            // tblConnectorImages
            // 
            tblConnectorImages.ColumnCount = 1;
            tblConnectorImages.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblConnectorImages.Controls.Add(lblConnectorImage, 0, 0);
            tblConnectorImages.Controls.Add(picConnectorImage, 0, 1);
            tblConnectorImages.Dock = DockStyle.Fill;
            tblConnectorImages.Location = new Point(0, 0);
            tblConnectorImages.Margin = new Padding(0);
            tblConnectorImages.Name = "tblConnectorImages";
            tblConnectorImages.RowCount = 2;
            tblConnectorImages.RowStyles.Add(new RowStyle(SizeType.Absolute, 51F));
            tblConnectorImages.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblConnectorImages.Size = new Size(813, 467);
            tblConnectorImages.TabIndex = 0;
            // 
            // lblConnectorImage
            // 
            lblConnectorImage.AutoSize = true;
            lblConnectorImage.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblConnectorImage.ForeColor = Color.FromArgb(51, 65, 85);
            lblConnectorImage.Location = new Point(0, 0);
            lblConnectorImage.Margin = new Padding(0);
            lblConnectorImage.Name = "lblConnectorImage";
            lblConnectorImage.Size = new Size(264, 36);
            lblConnectorImage.TabIndex = 1;
            lblConnectorImage.Text = "CONNECTOR IMAGE";
            // 
            // picConnectorImage
            // 
            picConnectorImage.BackColor = Color.FromArgb(248, 250, 252);
            picConnectorImage.Dock = DockStyle.Fill;
            picConnectorImage.Location = new Point(0, 51);
            picConnectorImage.Margin = new Padding(0);
            picConnectorImage.Name = "picConnectorImage";
            picConnectorImage.Size = new Size(813, 416);
            picConnectorImage.SizeMode = PictureBoxSizeMode.CenterImage;
            picConnectorImage.TabIndex = 2;
            picConnectorImage.TabStop = false;
            // 
            // HarnessConnectorTab
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanelConnector);
            Name = "HarnessConnectorTab";
            Size = new Size(1625, 960);
            tableLayoutPanelConnector.ResumeLayout(false);
            panelConnectorList.ResumeLayout(false);
            tblConnectorListLayout.ResumeLayout(false);
            panelConnectorTop.ResumeLayout(false);
            tblConnectorSearch.ResumeLayout(false);
            tblConnectorSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvConnectors).EndInit();
            tblConnectorPagination.ResumeLayout(false);
            flpConnectorShowEntries.ResumeLayout(false);
            flpConnectorShowEntries.PerformLayout();
            panelConnectorDetail.ResumeLayout(false);
            tableLayoutPanelConnectorDetail.ResumeLayout(false);
            panelConnectorInfo.ResumeLayout(false);
            panelConnectorImage.ResumeLayout(false);
            tblConnectorImages.ResumeLayout(false);
            tblConnectorImages.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picConnectorImage).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.TableLayoutPanel panelConnectorTop;
        private AssemblyLineTester.Views.Controls.RoundedButton btnAddConnector;
        private AssemblyLineTester.Views.Controls.RoundedButton btnEditConnector;
        private AssemblyLineTester.Views.Controls.RoundedButton btnDeleteConnector;
        private System.Windows.Forms.DataGridView dgvConnectors;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelConnector;
        private System.Windows.Forms.Panel panelConnectorList;
        private System.Windows.Forms.TableLayoutPanel tblConnectorListLayout;
        private System.Windows.Forms.TableLayoutPanel tblConnectorSearch;
        private System.Windows.Forms.TextBox txtConnectorSearch;
        private AssemblyLineTester.Views.Controls.RoundedButton btnConnectorSearch;
        private System.Windows.Forms.TableLayoutPanel tblConnectorPagination;
        private System.Windows.Forms.FlowLayoutPanel flpConnectorPages;
        private System.Windows.Forms.FlowLayoutPanel flpConnectorShowEntries;
        private System.Windows.Forms.Label lblConnectorShowEntries1;
        private System.Windows.Forms.ComboBox cmbConnectorShowEntries;
        private System.Windows.Forms.Label lblConnectorShowEntries2;
        private System.Windows.Forms.Panel panelConnectorDetail;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelConnectorDetail;
        private System.Windows.Forms.Panel panelConnectorInfo;
        private System.Windows.Forms.TableLayoutPanel tblConnectorInfo;
        private System.Windows.Forms.Panel panelConnectorImage;
        private System.Windows.Forms.TableLayoutPanel tblConnectorImages;
        private System.Windows.Forms.Label lblConnectorImage;
        private System.Windows.Forms.PictureBox picConnectorImage;
        private AssemblyLineTester.Views.Controls.RoundedButton btnChangeConnectorImage;
    }
}
