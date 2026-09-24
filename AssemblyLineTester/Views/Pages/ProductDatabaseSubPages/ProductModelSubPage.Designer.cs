namespace AssemblyLineTester.Views.Pages.ProductDatabaseSubPages
{
    partial class ProductModelSubPage
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
            splitContainerMain = new System.Windows.Forms.SplitContainer();
            pnlLeftHeader = new System.Windows.Forms.Panel();
            lblProductModelList = new System.Windows.Forms.Label();
            btnNewProduct = new System.Windows.Forms.Button();
            txtSearch = new System.Windows.Forms.TextBox();
            dgvProductModelList = new System.Windows.Forms.DataGridView();
            colNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colProductModelName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colProductModelPartNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colTotalHarnessAssembly = new System.Windows.Forms.DataGridViewTextBoxColumn();
            pnlRightContainer = new System.Windows.Forms.Panel();
            pnlTopSection = new System.Windows.Forms.Panel();
            pnlGridContainer = new System.Windows.Forms.Panel();
            pnlBottomBar = new System.Windows.Forms.Panel();
            pnlBottomButtons = new System.Windows.Forms.FlowLayoutPanel();
            btnSave = new System.Windows.Forms.Button();
            btnDelete = new System.Windows.Forms.Button();
            dgvAttachedHarness = new System.Windows.Forms.DataGridView();
            colAttNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colAttHarnessName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colAttPartNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colAttTotalConnector = new System.Windows.Forms.DataGridViewTextBoxColumn();
            pnlAttachActions = new System.Windows.Forms.FlowLayoutPanel();
            btnRemove = new System.Windows.Forms.Button();
            btnAttach = new System.Windows.Forms.Button();
            tblTopForm = new System.Windows.Forms.TableLayoutPanel();
            lblName = new System.Windows.Forms.Label();
            lblDescription = new System.Windows.Forms.Label();
            txtName = new System.Windows.Forms.TextBox();
            txtDescription = new System.Windows.Forms.TextBox();
            lblPartNumber = new System.Windows.Forms.Label();
            txtPartNumber = new System.Windows.Forms.TextBox();
            lblPicture = new System.Windows.Forms.Label();
            pnlPictureContainer = new System.Windows.Forms.Panel();
            picBox = new System.Windows.Forms.PictureBox();
            pnlPicButtons = new System.Windows.Forms.FlowLayoutPanel();
            btnAddPicture = new System.Windows.Forms.Button();
            btnDeletePicture = new System.Windows.Forms.Button();
            btnPrevPicture = new System.Windows.Forms.Button();
            btnNextPicture = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
            splitContainerMain.Panel1.SuspendLayout();
            splitContainerMain.Panel2.SuspendLayout();
            splitContainerMain.SuspendLayout();
            pnlLeftHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProductModelList).BeginInit();
            pnlRightContainer.SuspendLayout();
            pnlTopSection.SuspendLayout();
            pnlGridContainer.SuspendLayout();
            pnlBottomBar.SuspendLayout();
            pnlBottomButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAttachedHarness).BeginInit();
            pnlAttachActions.SuspendLayout();
            tblTopForm.SuspendLayout();
            pnlPictureContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picBox).BeginInit();
            pnlPicButtons.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainerMain
            // 
            splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainerMain.Location = new System.Drawing.Point(0, 0);
            splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            splitContainerMain.Panel1.Controls.Add(dgvProductModelList);
            splitContainerMain.Panel1.Controls.Add(txtSearch);
            splitContainerMain.Panel1.Controls.Add(pnlLeftHeader);
            splitContainerMain.Panel1.Padding = new System.Windows.Forms.Padding(12);
            // 
            // splitContainerMain.Panel2
            // 
            splitContainerMain.Panel2.Controls.Add(pnlRightContainer);
            splitContainerMain.Panel2.Padding = new System.Windows.Forms.Padding(12);
            splitContainerMain.Size = new System.Drawing.Size(1100, 680);
            splitContainerMain.SplitterDistance = 490;
            splitContainerMain.SplitterWidth = 2;
            splitContainerMain.TabIndex = 0;
            // 
            // pnlLeftHeader
            // 
            pnlLeftHeader.Controls.Add(btnNewProduct);
            pnlLeftHeader.Controls.Add(lblProductModelList);
            pnlLeftHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlLeftHeader.Location = new System.Drawing.Point(12, 12);
            pnlLeftHeader.Name = "pnlLeftHeader";
            pnlLeftHeader.Size = new System.Drawing.Size(466, 44);
            pnlLeftHeader.TabIndex = 0;
            // 
            // lblProductModelList
            // 
            lblProductModelList.AutoSize = true;
            lblProductModelList.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            lblProductModelList.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblProductModelList.Location = new System.Drawing.Point(0, 8);
            lblProductModelList.Name = "lblProductModelList";
            lblProductModelList.Size = new System.Drawing.Size(167, 25);
            lblProductModelList.TabIndex = 0;
            lblProductModelList.Text = "Product Model List";
            // 
            // btnNewProduct
            // 
            btnNewProduct.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnNewProduct.BackColor = System.Drawing.Color.FromArgb(245, 158, 11);
            btnNewProduct.FlatAppearance.BorderSize = 0;
            btnNewProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnNewProduct.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            btnNewProduct.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            btnNewProduct.Location = new System.Drawing.Point(320, 4);
            btnNewProduct.Name = "btnNewProduct";
            btnNewProduct.Size = new System.Drawing.Size(138, 34);
            btnNewProduct.TabIndex = 1;
            btnNewProduct.Text = "+ New Product";
            btnNewProduct.UseVisualStyleBackColor = false;
            // 
            // txtSearch
            // 
            txtSearch.Dock = System.Windows.Forms.DockStyle.Top;
            txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtSearch.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            txtSearch.Location = new System.Drawing.Point(12, 56);
            txtSearch.Margin = new System.Windows.Forms.Padding(0, 6, 0, 8);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search";
            txtSearch.Size = new System.Drawing.Size(466, 25);
            txtSearch.TabIndex = 1;
            // 
            // dgvProductModelList
            // 
            dgvProductModelList.AllowUserToAddRows = false;
            dgvProductModelList.AllowUserToDeleteRows = false;
            dgvProductModelList.BackgroundColor = System.Drawing.Color.White;
            dgvProductModelList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dgvProductModelList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductModelList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colNo, colProductModelName, colProductModelPartNumber, colTotalHarnessAssembly });
            dgvProductModelList.Dock = System.Windows.Forms.DockStyle.Fill;
            dgvProductModelList.Location = new System.Drawing.Point(12, 89);
            dgvProductModelList.MultiSelect = false;
            dgvProductModelList.Name = "dgvProductModelList";
            dgvProductModelList.ReadOnly = true;
            dgvProductModelList.RowHeadersVisible = false;
            dgvProductModelList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvProductModelList.Size = new System.Drawing.Size(466, 579);
            dgvProductModelList.TabIndex = 2;
            // 
            // colNo
            // 
            colNo.HeaderText = "No";
            colNo.Name = "colNo";
            colNo.ReadOnly = true;
            colNo.Width = 45;
            // 
            // colProductModelName
            // 
            colProductModelName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            colProductModelName.HeaderText = "Product Model Name";
            colProductModelName.Name = "colProductModelName";
            colProductModelName.ReadOnly = true;
            // 
            // colProductModelPartNumber
            // 
            colProductModelPartNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            colProductModelPartNumber.HeaderText = "Product Model Part Number";
            colProductModelPartNumber.Name = "colProductModelPartNumber";
            colProductModelPartNumber.ReadOnly = true;
            // 
            // colTotalHarnessAssembly
            // 
            colTotalHarnessAssembly.HeaderText = "Total Harness Assembly";
            colTotalHarnessAssembly.Name = "colTotalHarnessAssembly";
            colTotalHarnessAssembly.ReadOnly = true;
            colTotalHarnessAssembly.Width = 130;
            // 
            // pnlRightContainer
            // 
            pnlRightContainer.BackColor = System.Drawing.Color.White;
            pnlRightContainer.Controls.Add(pnlTopSection);
            pnlRightContainer.Controls.Add(pnlBottomBar);
            pnlRightContainer.Controls.Add(pnlGridContainer);
            pnlRightContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlRightContainer.Location = new System.Drawing.Point(12, 12);
            pnlRightContainer.Name = "pnlRightContainer";
            pnlRightContainer.Padding = new System.Windows.Forms.Padding(16);
            pnlRightContainer.Size = new System.Drawing.Size(584, 656);
            pnlRightContainer.TabIndex = 0;
            // 
            // pnlTopSection
            // 
            pnlTopSection.BackColor = System.Drawing.Color.Transparent;
            pnlTopSection.Controls.Add(pnlAttachActions);
            pnlTopSection.Controls.Add(pnlPictureContainer);
            pnlTopSection.Controls.Add(lblPicture);
            pnlTopSection.Controls.Add(tblTopForm);
            pnlTopSection.Dock = System.Windows.Forms.DockStyle.Top;
            pnlTopSection.Location = new System.Drawing.Point(16, 16);
            pnlTopSection.Name = "pnlTopSection";
            pnlTopSection.Size = new System.Drawing.Size(552, 326);
            pnlTopSection.TabIndex = 0;
            // 
            // pnlBottomBar
            // 
            pnlBottomBar.BackColor = System.Drawing.Color.Transparent;
            pnlBottomBar.Controls.Add(pnlBottomButtons);
            pnlBottomBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            pnlBottomBar.Location = new System.Drawing.Point(16, 596);
            pnlBottomBar.Name = "pnlBottomBar";
            pnlBottomBar.Size = new System.Drawing.Size(552, 44);
            pnlBottomBar.TabIndex = 2;
            // 
            // pnlBottomButtons
            // 
            pnlBottomButtons.BackColor = System.Drawing.Color.Transparent;
            pnlBottomButtons.Controls.Add(btnSave);
            pnlBottomButtons.Controls.Add(btnDelete);
            pnlBottomButtons.Dock = System.Windows.Forms.DockStyle.Right;
            pnlBottomButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            pnlBottomButtons.Location = new System.Drawing.Point(322, 0);
            pnlBottomButtons.Name = "pnlBottomButtons";
            pnlBottomButtons.Size = new System.Drawing.Size(230, 44);
            pnlBottomButtons.TabIndex = 0;
            pnlBottomButtons.WrapContents = false;
            // 
            // btnSave
            // 
            btnSave.BackColor = System.Drawing.Color.FromArgb(16, 185, 129);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnSave.ForeColor = System.Drawing.Color.White;
            btnSave.Location = new System.Drawing.Point(120, 4);
            btnSave.Margin = new System.Windows.Forms.Padding(6, 4, 0, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(104, 36);
            btnSave.TabIndex = 0;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = System.Drawing.Color.FromArgb(239, 68, 68);
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnDelete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnDelete.ForeColor = System.Drawing.Color.White;
            btnDelete.Location = new System.Drawing.Point(10, 4);
            btnDelete.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new System.Drawing.Size(98, 36);
            btnDelete.TabIndex = 1;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // pnlGridContainer
            // 
            pnlGridContainer.BackColor = System.Drawing.Color.Transparent;
            pnlGridContainer.Controls.Add(dgvAttachedHarness);
            pnlGridContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlGridContainer.Location = new System.Drawing.Point(16, 312);
            pnlGridContainer.Name = "pnlGridContainer";
            pnlGridContainer.Padding = new System.Windows.Forms.Padding(0, 6, 0, 8);
            pnlGridContainer.Size = new System.Drawing.Size(552, 284);
            pnlGridContainer.TabIndex = 1;
            // 
            // dgvAttachedHarness
            // 
            dgvAttachedHarness.AllowUserToAddRows = false;
            dgvAttachedHarness.AllowUserToDeleteRows = false;
            dgvAttachedHarness.BackgroundColor = System.Drawing.Color.White;
            dgvAttachedHarness.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dgvAttachedHarness.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAttachedHarness.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colAttNo, colAttHarnessName, colAttPartNumber, colAttTotalConnector });
            dgvAttachedHarness.Dock = System.Windows.Forms.DockStyle.Fill;
            dgvAttachedHarness.Location = new System.Drawing.Point(0, 6);
            dgvAttachedHarness.MultiSelect = false;
            dgvAttachedHarness.Name = "dgvAttachedHarness";
            dgvAttachedHarness.ReadOnly = true;
            dgvAttachedHarness.RowHeadersVisible = false;
            dgvAttachedHarness.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvAttachedHarness.Size = new System.Drawing.Size(552, 270);
            dgvAttachedHarness.TabIndex = 0;
            // 
            // colAttNo
            // 
            colAttNo.HeaderText = "No";
            colAttNo.Name = "colAttNo";
            colAttNo.ReadOnly = true;
            colAttNo.Width = 45;
            // 
            // colAttHarnessName
            // 
            colAttHarnessName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            colAttHarnessName.HeaderText = "Harness Assembly Name";
            colAttHarnessName.Name = "colAttHarnessName";
            colAttHarnessName.ReadOnly = true;
            // 
            // colAttPartNumber
            // 
            colAttPartNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            colAttPartNumber.HeaderText = "Part Number";
            colAttPartNumber.Name = "colAttPartNumber";
            colAttPartNumber.ReadOnly = true;
            // 
            // colAttTotalConnector
            // 
            colAttTotalConnector.HeaderText = "Total Connector";
            colAttTotalConnector.Name = "colAttTotalConnector";
            colAttTotalConnector.ReadOnly = true;
            colAttTotalConnector.Width = 110;
            // 
            // pnlAttachActions
            // 
            pnlAttachActions.Controls.Add(btnRemove);
            pnlAttachActions.Controls.Add(btnAttach);
            pnlAttachActions.Location = new System.Drawing.Point(0, 286);
            pnlAttachActions.Name = "pnlAttachActions";
            pnlAttachActions.Size = new System.Drawing.Size(220, 36);
            pnlAttachActions.TabIndex = 3;
            // 
            // btnRemove
            // 
            btnRemove.BackColor = System.Drawing.Color.FromArgb(239, 68, 68);
            btnRemove.FlatAppearance.BorderSize = 0;
            btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnRemove.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnRemove.ForeColor = System.Drawing.Color.White;
            btnRemove.Location = new System.Drawing.Point(0, 2);
            btnRemove.Margin = new System.Windows.Forms.Padding(0, 2, 6, 2);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new System.Drawing.Size(88, 30);
            btnRemove.TabIndex = 0;
            btnRemove.Text = "Remove";
            btnRemove.UseVisualStyleBackColor = false;
            // 
            // btnAttach
            // 
            btnAttach.BackColor = System.Drawing.Color.FromArgb(16, 185, 129);
            btnAttach.FlatAppearance.BorderSize = 0;
            btnAttach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnAttach.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnAttach.ForeColor = System.Drawing.Color.White;
            btnAttach.Location = new System.Drawing.Point(94, 2);
            btnAttach.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            btnAttach.Name = "btnAttach";
            btnAttach.Size = new System.Drawing.Size(88, 30);
            btnAttach.TabIndex = 1;
            btnAttach.Text = "Attach";
            btnAttach.UseVisualStyleBackColor = false;
            // 
            // tblTopForm
            // 
            tblTopForm.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tblTopForm.ColumnCount = 2;
            tblTopForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tblTopForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tblTopForm.Controls.Add(lblName, 0, 0);
            tblTopForm.Controls.Add(lblDescription, 1, 0);
            tblTopForm.Controls.Add(txtName, 0, 1);
            tblTopForm.Controls.Add(txtDescription, 1, 1);
            tblTopForm.Controls.Add(lblPartNumber, 0, 2);
            tblTopForm.Controls.Add(txtPartNumber, 0, 3);
            tblTopForm.Location = new System.Drawing.Point(0, 0);
            tblTopForm.Name = "tblTopForm";
            tblTopForm.RowCount = 4;
            tblTopForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            tblTopForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            tblTopForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            tblTopForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            tblTopForm.Size = new System.Drawing.Size(552, 114);
            tblTopForm.TabIndex = 0;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            lblName.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblName.Location = new System.Drawing.Point(3, 0);
            lblName.Name = "lblName";
            lblName.Size = new System.Drawing.Size(43, 17);
            lblName.TabIndex = 0;
            lblName.Text = "Name";
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            lblDescription.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblDescription.Location = new System.Drawing.Point(279, 0);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new System.Drawing.Size(74, 17);
            lblDescription.TabIndex = 1;
            lblDescription.Text = "Description";
            // 
            // txtName
            // 
            txtName.Dock = System.Windows.Forms.DockStyle.Fill;
            txtName.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtName.Location = new System.Drawing.Point(3, 25);
            txtName.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            txtName.Name = "txtName";
            txtName.Size = new System.Drawing.Size(261, 25);
            txtName.TabIndex = 2;
            // 
            // txtDescription
            // 
            txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            txtDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtDescription.Location = new System.Drawing.Point(279, 25);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            tblTopForm.SetRowSpan(txtDescription, 3);
            txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            txtDescription.Size = new System.Drawing.Size(270, 86);
            txtDescription.TabIndex = 3;
            // 
            // lblPartNumber
            // 
            lblPartNumber.AutoSize = true;
            lblPartNumber.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            lblPartNumber.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblPartNumber.Location = new System.Drawing.Point(3, 56);
            lblPartNumber.Name = "lblPartNumber";
            lblPartNumber.Size = new System.Drawing.Size(81, 17);
            lblPartNumber.TabIndex = 4;
            lblPartNumber.Text = "Part Number";
            // 
            // txtPartNumber
            // 
            txtPartNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            txtPartNumber.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtPartNumber.Location = new System.Drawing.Point(3, 81);
            txtPartNumber.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            txtPartNumber.Name = "txtPartNumber";
            txtPartNumber.Size = new System.Drawing.Size(261, 25);
            txtPartNumber.TabIndex = 5;
            // 
            // lblPicture
            // 
            lblPicture.AutoSize = true;
            lblPicture.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            lblPicture.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblPicture.Location = new System.Drawing.Point(0, 122);
            lblPicture.Name = "lblPicture";
            lblPicture.Size = new System.Drawing.Size(47, 17);
            lblPicture.TabIndex = 1;
            lblPicture.Text = "Picture";
            // 
            // pnlPictureContainer
            // 
            pnlPictureContainer.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlPictureContainer.Controls.Add(pnlPicButtons);
            pnlPictureContainer.Controls.Add(picBox);
            pnlPictureContainer.Location = new System.Drawing.Point(0, 144);
            pnlPictureContainer.Name = "pnlPictureContainer";
            pnlPictureContainer.Size = new System.Drawing.Size(552, 134);
            pnlPictureContainer.TabIndex = 2;
            // 
            // picBox
            // 
            picBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            picBox.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            picBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            picBox.Cursor = System.Windows.Forms.Cursors.Hand;
            picBox.Location = new System.Drawing.Point(0, 0);
            picBox.Name = "picBox";
            picBox.Size = new System.Drawing.Size(444, 134);
            picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            picBox.TabIndex = 0;
            picBox.TabStop = false;
            // 
            // pnlPicButtons
            // 
            pnlPicButtons.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            pnlPicButtons.Controls.Add(btnAddPicture);
            pnlPicButtons.Controls.Add(btnDeletePicture);
            pnlPicButtons.Controls.Add(btnPrevPicture);
            pnlPicButtons.Controls.Add(btnNextPicture);
            pnlPicButtons.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            pnlPicButtons.Location = new System.Drawing.Point(450, 0);
            pnlPicButtons.Name = "pnlPicButtons";
            pnlPicButtons.Size = new System.Drawing.Size(102, 134);
            pnlPicButtons.TabIndex = 1;
            pnlPicButtons.WrapContents = false;
            // 
            // btnAddPicture
            // 
            btnAddPicture.BackColor = System.Drawing.Color.FromArgb(16, 185, 129);
            btnAddPicture.FlatAppearance.BorderSize = 0;
            btnAddPicture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnAddPicture.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnAddPicture.ForeColor = System.Drawing.Color.White;
            btnAddPicture.Location = new System.Drawing.Point(0, 1);
            btnAddPicture.Margin = new System.Windows.Forms.Padding(0, 1, 0, 2);
            btnAddPicture.Name = "btnAddPicture";
            btnAddPicture.Size = new System.Drawing.Size(100, 30);
            btnAddPicture.TabIndex = 0;
            btnAddPicture.Text = "+ Add";
            btnAddPicture.UseVisualStyleBackColor = false;
            // 
            // btnDeletePicture
            // 
            btnDeletePicture.BackColor = System.Drawing.Color.FromArgb(239, 68, 68);
            btnDeletePicture.FlatAppearance.BorderSize = 0;
            btnDeletePicture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnDeletePicture.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnDeletePicture.ForeColor = System.Drawing.Color.White;
            btnDeletePicture.Location = new System.Drawing.Point(0, 35);
            btnDeletePicture.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            btnDeletePicture.Name = "btnDeletePicture";
            btnDeletePicture.Size = new System.Drawing.Size(100, 30);
            btnDeletePicture.TabIndex = 1;
            btnDeletePicture.Text = "Delete";
            btnDeletePicture.UseVisualStyleBackColor = false;
            // 
            // btnPrevPicture
            // 
            btnPrevPicture.BackColor = System.Drawing.Color.FromArgb(71, 85, 105);
            btnPrevPicture.FlatAppearance.BorderSize = 0;
            btnPrevPicture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnPrevPicture.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnPrevPicture.ForeColor = System.Drawing.Color.White;
            btnPrevPicture.Location = new System.Drawing.Point(0, 69);
            btnPrevPicture.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            btnPrevPicture.Name = "btnPrevPicture";
            btnPrevPicture.Size = new System.Drawing.Size(100, 30);
            btnPrevPicture.TabIndex = 2;
            btnPrevPicture.Text = "< Prev";
            btnPrevPicture.UseVisualStyleBackColor = false;
            // 
            // btnNextPicture
            // 
            btnNextPicture.BackColor = System.Drawing.Color.FromArgb(71, 85, 105);
            btnNextPicture.FlatAppearance.BorderSize = 0;
            btnNextPicture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnNextPicture.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnNextPicture.ForeColor = System.Drawing.Color.White;
            btnNextPicture.Location = new System.Drawing.Point(0, 103);
            btnNextPicture.Margin = new System.Windows.Forms.Padding(0, 2, 0, 1);
            btnNextPicture.Name = "btnNextPicture";
            btnNextPicture.Size = new System.Drawing.Size(100, 30);
            btnNextPicture.TabIndex = 3;
            btnNextPicture.Text = "Next >";
            btnNextPicture.UseVisualStyleBackColor = false;
            // 
            // ProductModelSubPage
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(244, 245, 247);
            Controls.Add(splitContainerMain);
            Name = "ProductModelSubPage";
            Size = new System.Drawing.Size(1100, 680);
            splitContainerMain.Panel1.ResumeLayout(false);
            splitContainerMain.Panel1.PerformLayout();
            splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).EndInit();
            splitContainerMain.ResumeLayout(false);
            pnlLeftHeader.ResumeLayout(false);
            pnlLeftHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProductModelList).EndInit();
            pnlRightContainer.ResumeLayout(false);
            pnlTopSection.ResumeLayout(false);
            pnlTopSection.PerformLayout();
            pnlBottomBar.ResumeLayout(false);
            pnlBottomButtons.ResumeLayout(false);
            pnlGridContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvAttachedHarness).EndInit();
            pnlAttachActions.ResumeLayout(false);
            tblTopForm.ResumeLayout(false);
            tblTopForm.PerformLayout();
            pnlPictureContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picBox).EndInit();
            pnlPicButtons.ResumeLayout(false);
            pnlGridContainer.BringToFront();
            ResumeLayout(false);
        }

        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.Panel pnlLeftHeader;
        private System.Windows.Forms.Label lblProductModelList;
        private System.Windows.Forms.Button btnNewProduct;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridView dgvProductModelList;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductModelName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductModelPartNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalHarnessAssembly;
        private System.Windows.Forms.Panel pnlRightContainer;
        private System.Windows.Forms.Panel pnlTopSection;
        private System.Windows.Forms.Panel pnlGridContainer;
        private System.Windows.Forms.Panel pnlBottomBar;
        private System.Windows.Forms.TableLayoutPanel tblTopForm;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblPartNumber;
        private System.Windows.Forms.TextBox txtPartNumber;
        private System.Windows.Forms.Label lblPicture;
        private System.Windows.Forms.Panel pnlPictureContainer;
        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.FlowLayoutPanel pnlPicButtons;
        private System.Windows.Forms.Button btnAddPicture;
        private System.Windows.Forms.Button btnDeletePicture;
        private System.Windows.Forms.Button btnPrevPicture;
        private System.Windows.Forms.Button btnNextPicture;
        private System.Windows.Forms.FlowLayoutPanel pnlAttachActions;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAttach;
        private System.Windows.Forms.DataGridView dgvAttachedHarness;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAttNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAttHarnessName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAttPartNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAttTotalConnector;
        private System.Windows.Forms.FlowLayoutPanel pnlBottomButtons;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
    }
}
