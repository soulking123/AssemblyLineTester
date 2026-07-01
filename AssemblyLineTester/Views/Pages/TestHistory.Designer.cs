namespace AssemblyLineTester.Views.Pages
{
    partial class TestHistory
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
            pnlTop = new Panel();
            logoBox = new PictureBox();
            lblTitle = new Label();
            lblDateRange = new Label();
            dtpStart = new DateTimePicker();
            lblDash = new Label();
            dtpEnd = new DateTimePicker();
            lblProductModel = new Label();
            cmbProductModel = new ComboBox();
            lblProductType = new Label();
            cmbProductType = new ComboBox();
            lblArea = new Label();
            cmbArea = new ComboBox();
            lblResult = new Label();
            cmbResult = new ComboBox();
            lblOperator = new Label();
            cmbOperator = new ComboBox();
            btnSearch = new Button();
            btnReset = new Button();
            mainLayout = new TableLayoutPanel();
            pnlLeft = new Panel();
            btnExport = new Button();
            btnRefresh = new Button();
            gridData = new DataGridView();
            lblTableTitle = new Label();
            rightLayout = new TableLayoutPanel();
            pnlDetail = new Panel();
            lblDetailTitle = new Label();
            pnlSummary = new Panel();
            lblSummaryTitle = new Label();
            pnlSteps = new Panel();
            lblStepsTitle = new Label();
            pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)logoBox).BeginInit();
            mainLayout.SuspendLayout();
            pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridData).BeginInit();
            rightLayout.SuspendLayout();
            pnlDetail.SuspendLayout();
            pnlSummary.SuspendLayout();
            pnlSteps.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.White;
            pnlTop.Controls.Add(logoBox);
            pnlTop.Controls.Add(lblTitle);
            pnlTop.Controls.Add(lblDateRange);
            pnlTop.Controls.Add(dtpStart);
            pnlTop.Controls.Add(lblDash);
            pnlTop.Controls.Add(dtpEnd);
            pnlTop.Controls.Add(lblProductModel);
            pnlTop.Controls.Add(cmbProductModel);
            pnlTop.Controls.Add(lblProductType);
            pnlTop.Controls.Add(cmbProductType);
            pnlTop.Controls.Add(lblArea);
            pnlTop.Controls.Add(cmbArea);
            pnlTop.Controls.Add(lblResult);
            pnlTop.Controls.Add(cmbResult);
            pnlTop.Controls.Add(lblOperator);
            pnlTop.Controls.Add(cmbOperator);
            pnlTop.Controls.Add(btnSearch);
            pnlTop.Controls.Add(btnReset);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(2210, 212);
            pnlTop.TabIndex = 0;
            // 
            // logoBox
            // 
            logoBox.BackColor = Color.LightGray;
            logoBox.Location = new Point(40, 20);
            logoBox.Name = "logoBox";
            logoBox.Size = new Size(45, 45);
            logoBox.TabIndex = 0;
            logoBox.TabStop = false;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(100, 23);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(315, 59);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "TEST HISTORY";
            // 
            // lblDateRange
            // 
            lblDateRange.AutoSize = true;
            lblDateRange.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDateRange.Location = new Point(40, 96);
            lblDateRange.Name = "lblDateRange";
            lblDateRange.Size = new Size(146, 32);
            lblDateRange.TabIndex = 2;
            lblDateRange.Text = "Date Range";
            // 
            // dtpStart
            // 
            dtpStart.Format = DateTimePickerFormat.Short;
            dtpStart.Location = new Point(40, 138);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(217, 39);
            dtpStart.TabIndex = 3;
            // 
            // lblDash
            // 
            lblDash.AutoSize = true;
            lblDash.Location = new Point(275, 137);
            lblDash.Name = "lblDash";
            lblDash.Size = new Size(24, 32);
            lblDash.TabIndex = 4;
            lblDash.Text = "-";
            // 
            // dtpEnd
            // 
            dtpEnd.Format = DateTimePickerFormat.Short;
            dtpEnd.Location = new Point(316, 137);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(228, 39);
            dtpEnd.TabIndex = 5;
            // 
            // lblProductModel
            // 
            lblProductModel.AutoSize = true;
            lblProductModel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblProductModel.Location = new Point(595, 96);
            lblProductModel.Name = "lblProductModel";
            lblProductModel.Size = new Size(185, 32);
            lblProductModel.TabIndex = 6;
            lblProductModel.Text = "Product Model";
            // 
            // cmbProductModel
            // 
            cmbProductModel.FormattingEnabled = true;
            cmbProductModel.Items.AddRange(new object[] { "All", "6060" });
            cmbProductModel.Location = new Point(595, 137);
            cmbProductModel.Name = "cmbProductModel";
            cmbProductModel.Size = new Size(180, 40);
            cmbProductModel.TabIndex = 7;
            cmbProductModel.Text = "All";
            // 
            // lblProductType
            // 
            lblProductType.AutoSize = true;
            lblProductType.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblProductType.Location = new Point(815, 96);
            lblProductType.Name = "lblProductType";
            lblProductType.Size = new Size(166, 32);
            lblProductType.TabIndex = 8;
            lblProductType.Text = "Product Type";
            // 
            // cmbProductType
            // 
            cmbProductType.FormattingEnabled = true;
            cmbProductType.Items.AddRange(new object[] { "All", "AC" });
            cmbProductType.Location = new Point(815, 137);
            cmbProductType.Name = "cmbProductType";
            cmbProductType.Size = new Size(180, 40);
            cmbProductType.TabIndex = 9;
            cmbProductType.Text = "All";
            // 
            // lblArea
            // 
            lblArea.AutoSize = true;
            lblArea.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblArea.Location = new Point(1035, 96);
            lblArea.Name = "lblArea";
            lblArea.Size = new Size(67, 32);
            lblArea.TabIndex = 10;
            lblArea.Text = "Area";
            // 
            // cmbArea
            // 
            cmbArea.FormattingEnabled = true;
            cmbArea.Items.AddRange(new object[] { "All", "Engine Control" });
            cmbArea.Location = new Point(1035, 136);
            cmbArea.Name = "cmbArea";
            cmbArea.Size = new Size(190, 40);
            cmbArea.TabIndex = 11;
            cmbArea.Text = "All";
            // 
            // lblResult
            // 
            lblResult.AutoSize = true;
            lblResult.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblResult.Location = new Point(1255, 96);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(84, 32);
            lblResult.TabIndex = 12;
            lblResult.Text = "Result";
            // 
            // cmbResult
            // 
            cmbResult.FormattingEnabled = true;
            cmbResult.Items.AddRange(new object[] { "All", "PASS", "FAIL" });
            cmbResult.Location = new Point(1255, 137);
            cmbResult.Name = "cmbResult";
            cmbResult.Size = new Size(180, 40);
            cmbResult.TabIndex = 13;
            cmbResult.Text = "All";
            // 
            // lblOperator
            // 
            lblOperator.AutoSize = true;
            lblOperator.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblOperator.Location = new Point(1454, 96);
            lblOperator.Name = "lblOperator";
            lblOperator.Size = new Size(117, 32);
            lblOperator.TabIndex = 14;
            lblOperator.Text = "Operator";
            // 
            // cmbOperator
            // 
            cmbOperator.FormattingEnabled = true;
            cmbOperator.Items.AddRange(new object[] { "All", "Fally" });
            cmbOperator.Location = new Point(1454, 136);
            cmbOperator.Name = "cmbOperator";
            cmbOperator.Size = new Size(180, 40);
            cmbOperator.TabIndex = 15;
            cmbOperator.Text = "All";
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(255, 193, 7);
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSearch.Location = new Point(1906, 131);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(140, 50);
            btnSearch.TabIndex = 16;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = false;
            // 
            // btnReset
            // 
            btnReset.BackColor = Color.White;
            btnReset.FlatAppearance.BorderColor = Color.LightGray;
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnReset.Location = new Point(2061, 129);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(120, 50);
            btnReset.TabIndex = 17;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = false;
            // 
            // mainLayout
            // 
            mainLayout.ColumnCount = 2;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            mainLayout.Controls.Add(pnlLeft, 0, 0);
            mainLayout.Controls.Add(rightLayout, 1, 0);
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Location = new Point(0, 212);
            mainLayout.Name = "mainLayout";
            mainLayout.Padding = new Padding(32, 37, 32, 37);
            mainLayout.RowCount = 1;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.Size = new Size(2210, 946);
            mainLayout.TabIndex = 1;
            // 
            // pnlLeft
            // 
            pnlLeft.BackColor = Color.White;
            pnlLeft.Controls.Add(btnExport);
            pnlLeft.Controls.Add(btnRefresh);
            pnlLeft.Controls.Add(gridData);
            pnlLeft.Controls.Add(lblTableTitle);
            pnlLeft.Dock = DockStyle.Fill;
            pnlLeft.Location = new Point(43, 49);
            pnlLeft.Margin = new Padding(11, 12, 11, 12);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Size = new Size(1480, 848);
            pnlLeft.TabIndex = 0;
            // 
            // btnExport
            // 
            btnExport.BackColor = Color.White;
            btnExport.FlatAppearance.BorderColor = Color.LightGray;
            btnExport.FlatStyle = FlatStyle.Flat;
            btnExport.Location = new Point(1177, 40);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(130, 45);
            btnExport.TabIndex = 0;
            btnExport.Text = "Export";
            btnExport.UseVisualStyleBackColor = false;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.White;
            btnRefresh.FlatAppearance.BorderColor = Color.LightGray;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Location = new Point(1313, 40);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(130, 45);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            // 
            // gridData
            // 
            gridData.AllowUserToAddRows = false;
            gridData.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridData.BackgroundColor = Color.White;
            gridData.BorderStyle = BorderStyle.None;
            gridData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridData.Location = new Point(43, 110);
            gridData.Name = "gridData";
            gridData.RowHeadersVisible = false;
            gridData.RowHeadersWidth = 82;
            gridData.Size = new Size(1400, 705);
            gridData.TabIndex = 1;
            // 
            // lblTableTitle
            // 
            lblTableTitle.AutoSize = true;
            lblTableTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTableTitle.Location = new Point(43, 49);
            lblTableTitle.Name = "lblTableTitle";
            lblTableTitle.Size = new Size(488, 45);
            lblTableTitle.TabIndex = 2;
            lblTableTitle.Text = "TEST HISTORY LIST (Total: 215)";
            // 
            // rightLayout
            // 
            rightLayout.ColumnCount = 1;
            rightLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rightLayout.Controls.Add(pnlDetail, 0, 0);
            rightLayout.Controls.Add(pnlSummary, 0, 1);
            rightLayout.Controls.Add(pnlSteps, 0, 2);
            rightLayout.Dock = DockStyle.Fill;
            rightLayout.Location = new Point(1545, 49);
            rightLayout.Margin = new Padding(11, 12, 11, 12);
            rightLayout.Name = "rightLayout";
            rightLayout.RowCount = 3;
            rightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            rightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            rightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 35F));
            rightLayout.Size = new Size(622, 848);
            rightLayout.TabIndex = 1;
            // 
            // pnlDetail
            // 
            pnlDetail.BackColor = Color.White;
            pnlDetail.Controls.Add(lblDetailTitle);
            pnlDetail.Dock = DockStyle.Fill;
            pnlDetail.Location = new Point(0, 0);
            pnlDetail.Margin = new Padding(0, 0, 0, 25);
            pnlDetail.Name = "pnlDetail";
            pnlDetail.Size = new Size(622, 314);
            pnlDetail.TabIndex = 0;
            // 
            // lblDetailTitle
            // 
            lblDetailTitle.AutoSize = true;
            lblDetailTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDetailTitle.Location = new Point(20, 20);
            lblDetailTitle.Name = "lblDetailTitle";
            lblDetailTitle.Size = new Size(175, 37);
            lblDetailTitle.TabIndex = 0;
            lblDetailTitle.Text = "TEST DETAIL";
            // 
            // pnlSummary
            // 
            pnlSummary.BackColor = Color.White;
            pnlSummary.Controls.Add(lblSummaryTitle);
            pnlSummary.Dock = DockStyle.Fill;
            pnlSummary.Location = new Point(0, 339);
            pnlSummary.Margin = new Padding(0, 0, 0, 25);
            pnlSummary.Name = "pnlSummary";
            pnlSummary.Size = new Size(622, 187);
            pnlSummary.TabIndex = 1;
            // 
            // lblSummaryTitle
            // 
            lblSummaryTitle.AutoSize = true;
            lblSummaryTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSummaryTitle.Location = new Point(20, 20);
            lblSummaryTitle.Name = "lblSummaryTitle";
            lblSummaryTitle.Size = new Size(225, 37);
            lblSummaryTitle.TabIndex = 0;
            lblSummaryTitle.Text = "TEST SUMMARY";
            // 
            // pnlSteps
            // 
            pnlSteps.BackColor = Color.White;
            pnlSteps.Controls.Add(lblStepsTitle);
            pnlSteps.Dock = DockStyle.Fill;
            pnlSteps.Location = new Point(0, 551);
            pnlSteps.Margin = new Padding(0, 0, 0, 25);
            pnlSteps.Name = "pnlSteps";
            pnlSteps.Size = new Size(622, 272);
            pnlSteps.TabIndex = 2;
            // 
            // lblStepsTitle
            // 
            lblStepsTitle.AutoSize = true;
            lblStepsTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblStepsTitle.Location = new Point(20, 12);
            lblStepsTitle.Name = "lblStepsTitle";
            lblStepsTitle.Size = new Size(264, 37);
            lblStepsTitle.TabIndex = 0;
            lblStepsTitle.Text = "TEST STEPS RESULT";
            // 
            // TestHistory
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 245, 247);
            Controls.Add(mainLayout);
            Controls.Add(pnlTop);
            Margin = new Padding(6, 7, 6, 7);
            Name = "TestHistory";
            Size = new Size(2210, 1158);
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)logoBox).EndInit();
            mainLayout.ResumeLayout(false);
            pnlLeft.ResumeLayout(false);
            pnlLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridData).EndInit();
            rightLayout.ResumeLayout(false);
            pnlDetail.ResumeLayout(false);
            pnlDetail.PerformLayout();
            pnlSummary.ResumeLayout(false);
            pnlSummary.PerformLayout();
            pnlSteps.ResumeLayout(false);
            pnlSteps.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.PictureBox logoBox;
        private System.Windows.Forms.Label lblTitle;

        // Deklarasi Komponen Filter
        private System.Windows.Forms.Label lblDateRange;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label lblDash;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label lblProductModel;
        private System.Windows.Forms.ComboBox cmbProductModel;
        private System.Windows.Forms.Label lblProductType;
        private System.Windows.Forms.ComboBox cmbProductType;
        private System.Windows.Forms.Label lblArea;
        private System.Windows.Forms.ComboBox cmbArea;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.ComboBox cmbResult;
        private System.Windows.Forms.Label lblOperator;
        private System.Windows.Forms.ComboBox cmbOperator;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnReset;

        // Deklarasi Tambahan (Panel Kiri & Kanan)
        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblTableTitle;
        private System.Windows.Forms.DataGridView gridData;
        private System.Windows.Forms.TableLayoutPanel rightLayout;
        private System.Windows.Forms.Panel pnlDetail;
        private System.Windows.Forms.Label lblDetailTitle;
        private System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.Label lblSummaryTitle;
        private System.Windows.Forms.Panel pnlSteps;
        private System.Windows.Forms.Label lblStepsTitle;
    }
}