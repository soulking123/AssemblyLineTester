namespace AssemblyLineTester.Views.Pages.ProductDatabaseSubPages
{
    partial class ConnectorSubPage
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
            lblConnectorList = new System.Windows.Forms.Label();
            btnNewConnector = new System.Windows.Forms.Button();
            txtSearch = new System.Windows.Forms.TextBox();
            dgvConnectorList = new System.Windows.Forms.DataGridView();
            colNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colConnectorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colPartNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colTotalPin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            pnlRightContainer = new System.Windows.Forms.Panel();
            tblForm = new System.Windows.Forms.TableLayoutPanel();
            lblName = new System.Windows.Forms.Label();
            txtName = new System.Windows.Forms.TextBox();
            lblTotalPins = new System.Windows.Forms.Label();
            txtTotalPins = new System.Windows.Forms.TextBox();
            lblPartNumber = new System.Windows.Forms.Label();
            txtPartNumber = new System.Windows.Forms.TextBox();
            lblType = new System.Windows.Forms.Label();
            txtType = new System.Windows.Forms.TextBox();
            lblDescription = new System.Windows.Forms.Label();
            txtDescription = new System.Windows.Forms.TextBox();
            lblPicture = new System.Windows.Forms.Label();
            pnlPictureContainer = new System.Windows.Forms.Panel();
            picBox = new System.Windows.Forms.PictureBox();
            pnlPicButtons = new System.Windows.Forms.FlowLayoutPanel();
            btnAddPicture = new System.Windows.Forms.Button();
            btnDeletePicture = new System.Windows.Forms.Button();
            pnlBottomBar = new System.Windows.Forms.Panel();
            pnlBottomButtons = new System.Windows.Forms.FlowLayoutPanel();
            btnSave = new System.Windows.Forms.Button();
            btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
            splitContainerMain.Panel1.SuspendLayout();
            splitContainerMain.Panel2.SuspendLayout();
            splitContainerMain.SuspendLayout();
            pnlLeftHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvConnectorList).BeginInit();
            pnlRightContainer.SuspendLayout();
            tblForm.SuspendLayout();
            pnlPictureContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picBox).BeginInit();
            pnlPicButtons.SuspendLayout();
            pnlBottomBar.SuspendLayout();
            pnlBottomButtons.SuspendLayout();
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
            splitContainerMain.Panel1.Controls.Add(dgvConnectorList);
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
            pnlLeftHeader.Controls.Add(btnNewConnector);
            pnlLeftHeader.Controls.Add(lblConnectorList);
            pnlLeftHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlLeftHeader.Location = new System.Drawing.Point(12, 12);
            pnlLeftHeader.Name = "pnlLeftHeader";
            pnlLeftHeader.Size = new System.Drawing.Size(466, 44);
            pnlLeftHeader.TabIndex = 0;
            // 
            // lblConnectorList
            // 
            lblConnectorList.AutoSize = true;
            lblConnectorList.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            lblConnectorList.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblConnectorList.Location = new System.Drawing.Point(0, 8);
            lblConnectorList.Name = "lblConnectorList";
            lblConnectorList.Size = new System.Drawing.Size(142, 25);
            lblConnectorList.TabIndex = 0;
            lblConnectorList.Text = "Connector List";
            // 
            // btnNewConnector
            // 
            btnNewConnector.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnNewConnector.BackColor = System.Drawing.Color.FromArgb(245, 158, 11);
            btnNewConnector.FlatAppearance.BorderSize = 0;
            btnNewConnector.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnNewConnector.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            btnNewConnector.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            btnNewConnector.Location = new System.Drawing.Point(310, 4);
            btnNewConnector.Name = "btnNewConnector";
            btnNewConnector.Size = new System.Drawing.Size(148, 34);
            btnNewConnector.TabIndex = 1;
            btnNewConnector.Text = "+ New Connector";
            btnNewConnector.UseVisualStyleBackColor = false;
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
            // dgvConnectorList
            // 
            dgvConnectorList.AllowUserToAddRows = false;
            dgvConnectorList.AllowUserToDeleteRows = false;
            dgvConnectorList.BackgroundColor = System.Drawing.Color.White;
            dgvConnectorList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dgvConnectorList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvConnectorList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colNo, colConnectorName, colPartNumber, colTotalPin, colType });
            dgvConnectorList.Dock = System.Windows.Forms.DockStyle.Fill;
            dgvConnectorList.Location = new System.Drawing.Point(12, 89);
            dgvConnectorList.MultiSelect = false;
            dgvConnectorList.Name = "dgvConnectorList";
            dgvConnectorList.ReadOnly = true;
            dgvConnectorList.RowHeadersVisible = false;
            dgvConnectorList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvConnectorList.Size = new System.Drawing.Size(466, 579);
            dgvConnectorList.TabIndex = 2;
            // 
            // colNo
            // 
            colNo.HeaderText = "No";
            colNo.Name = "colNo";
            colNo.ReadOnly = true;
            colNo.Width = 45;
            // 
            // colConnectorName
            // 
            colConnectorName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            colConnectorName.HeaderText = "Connector Name";
            colConnectorName.Name = "colConnectorName";
            colConnectorName.ReadOnly = true;
            // 
            // colPartNumber
            // 
            colPartNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            colPartNumber.HeaderText = "Part Number";
            colPartNumber.Name = "colPartNumber";
            colPartNumber.ReadOnly = true;
            // 
            // colTotalPin
            // 
            colTotalPin.HeaderText = "Total Pin";
            colTotalPin.Name = "colTotalPin";
            colTotalPin.ReadOnly = true;
            colTotalPin.Width = 75;
            // 
            // colType
            // 
            colType.HeaderText = "Type";
            colType.Name = "colType";
            colType.ReadOnly = true;
            colType.Width = 75;
            // 
            // pnlRightContainer
            // 
            pnlRightContainer.BackColor = System.Drawing.Color.White;
            pnlRightContainer.Controls.Add(tblForm);
            pnlRightContainer.Controls.Add(pnlBottomBar);
            pnlRightContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlRightContainer.Location = new System.Drawing.Point(12, 12);
            pnlRightContainer.Name = "pnlRightContainer";
            pnlRightContainer.Padding = new System.Windows.Forms.Padding(16);
            pnlRightContainer.Size = new System.Drawing.Size(584, 656);
            pnlRightContainer.TabIndex = 0;
            // 
            // tblForm
            // 
            tblForm.ColumnCount = 2;
            tblForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tblForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tblForm.Controls.Add(lblName, 0, 0);
            tblForm.Controls.Add(lblTotalPins, 1, 0);
            tblForm.Controls.Add(txtName, 0, 1);
            tblForm.Controls.Add(txtTotalPins, 1, 1);
            tblForm.Controls.Add(lblPartNumber, 0, 2);
            tblForm.Controls.Add(lblType, 1, 2);
            tblForm.Controls.Add(txtPartNumber, 0, 3);
            tblForm.Controls.Add(txtType, 1, 3);
            tblForm.Controls.Add(lblDescription, 0, 4);
            tblForm.Controls.Add(txtDescription, 0, 5);
            tblForm.Controls.Add(lblPicture, 0, 6);
            tblForm.Controls.Add(pnlPictureContainer, 0, 7);
            tblForm.Dock = System.Windows.Forms.DockStyle.Top;
            tblForm.Location = new System.Drawing.Point(16, 16);
            tblForm.Name = "tblForm";
            tblForm.RowCount = 8;
            tblForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            tblForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            tblForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            tblForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            tblForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            tblForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            tblForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            tblForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            tblForm.Size = new System.Drawing.Size(552, 340);
            tblForm.TabIndex = 0;
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
            // lblTotalPins
            // 
            lblTotalPins.AutoSize = true;
            lblTotalPins.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            lblTotalPins.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblTotalPins.Location = new System.Drawing.Point(279, 0);
            lblTotalPins.Name = "lblTotalPins";
            lblTotalPins.Size = new System.Drawing.Size(65, 17);
            lblTotalPins.TabIndex = 1;
            lblTotalPins.Text = "Total Pins";
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
            // txtTotalPins
            // 
            txtTotalPins.Dock = System.Windows.Forms.DockStyle.Fill;
            txtTotalPins.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtTotalPins.Location = new System.Drawing.Point(279, 25);
            txtTotalPins.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            txtTotalPins.Name = "txtTotalPins";
            txtTotalPins.Size = new System.Drawing.Size(270, 25);
            txtTotalPins.TabIndex = 3;
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
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            lblType.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblType.Location = new System.Drawing.Point(279, 56);
            lblType.Name = "lblType";
            lblType.Size = new System.Drawing.Size(35, 17);
            lblType.TabIndex = 5;
            lblType.Text = "Type";
            // 
            // txtPartNumber
            // 
            txtPartNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            txtPartNumber.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtPartNumber.Location = new System.Drawing.Point(3, 81);
            txtPartNumber.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            txtPartNumber.Name = "txtPartNumber";
            txtPartNumber.Size = new System.Drawing.Size(261, 25);
            txtPartNumber.TabIndex = 6;
            // 
            // txtType
            // 
            txtType.Dock = System.Windows.Forms.DockStyle.Fill;
            txtType.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtType.Location = new System.Drawing.Point(279, 81);
            txtType.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            txtType.Name = "txtType";
            txtType.Size = new System.Drawing.Size(270, 25);
            txtType.TabIndex = 7;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            tblForm.SetColumnSpan(lblDescription, 2);
            lblDescription.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            lblDescription.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblDescription.Location = new System.Drawing.Point(3, 112);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new System.Drawing.Size(74, 17);
            lblDescription.TabIndex = 8;
            lblDescription.Text = "Description";
            // 
            // txtDescription
            // 
            tblForm.SetColumnSpan(txtDescription, 2);
            txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            txtDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtDescription.Location = new System.Drawing.Point(3, 137);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            txtDescription.Size = new System.Drawing.Size(546, 68);
            txtDescription.TabIndex = 9;
            // 
            // lblPicture
            // 
            lblPicture.AutoSize = true;
            tblForm.SetColumnSpan(lblPicture, 2);
            lblPicture.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            lblPicture.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblPicture.Location = new System.Drawing.Point(3, 208);
            lblPicture.Name = "lblPicture";
            lblPicture.Size = new System.Drawing.Size(47, 17);
            lblPicture.TabIndex = 10;
            lblPicture.Text = "Picture";
            // 
            // pnlPictureContainer
            // 
            tblForm.SetColumnSpan(pnlPictureContainer, 2);
            pnlPictureContainer.Controls.Add(pnlPicButtons);
            pnlPictureContainer.Controls.Add(picBox);
            pnlPictureContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlPictureContainer.Location = new System.Drawing.Point(3, 233);
            pnlPictureContainer.Name = "pnlPictureContainer";
            pnlPictureContainer.Size = new System.Drawing.Size(546, 104);
            pnlPictureContainer.TabIndex = 11;
            // 
            // picBox
            // 
            picBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            picBox.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            picBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            picBox.Location = new System.Drawing.Point(0, 0);
            picBox.Name = "picBox";
            picBox.Size = new System.Drawing.Size(438, 104);
            picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            picBox.TabIndex = 0;
            picBox.TabStop = false;
            // 
            // pnlPicButtons
            // 
            pnlPicButtons.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            pnlPicButtons.Controls.Add(btnAddPicture);
            pnlPicButtons.Controls.Add(btnDeletePicture);
            pnlPicButtons.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            pnlPicButtons.Location = new System.Drawing.Point(444, 0);
            pnlPicButtons.Name = "pnlPicButtons";
            pnlPicButtons.Size = new System.Drawing.Size(102, 104);
            pnlPicButtons.TabIndex = 1;
            // 
            // btnAddPicture
            // 
            btnAddPicture.BackColor = System.Drawing.Color.FromArgb(16, 185, 129);
            btnAddPicture.FlatAppearance.BorderSize = 0;
            btnAddPicture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnAddPicture.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnAddPicture.ForeColor = System.Drawing.Color.White;
            btnAddPicture.Location = new System.Drawing.Point(0, 2);
            btnAddPicture.Margin = new System.Windows.Forms.Padding(0, 2, 0, 4);
            btnAddPicture.Name = "btnAddPicture";
            btnAddPicture.Size = new System.Drawing.Size(100, 32);
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
            btnDeletePicture.Location = new System.Drawing.Point(0, 42);
            btnDeletePicture.Margin = new System.Windows.Forms.Padding(0, 4, 0, 2);
            btnDeletePicture.Name = "btnDeletePicture";
            btnDeletePicture.Size = new System.Drawing.Size(100, 32);
            btnDeletePicture.TabIndex = 1;
            btnDeletePicture.Text = "+ Delete";
            btnDeletePicture.UseVisualStyleBackColor = false;
            // 
            // pnlBottomBar
            // 
            pnlBottomBar.BackColor = System.Drawing.Color.Transparent;
            pnlBottomBar.Controls.Add(pnlBottomButtons);
            pnlBottomBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            pnlBottomBar.Location = new System.Drawing.Point(16, 596);
            pnlBottomBar.Name = "pnlBottomBar";
            pnlBottomBar.Size = new System.Drawing.Size(552, 44);
            pnlBottomBar.TabIndex = 1;
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
            // ConnectorSubPage
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(244, 245, 247);
            Controls.Add(splitContainerMain);
            Name = "ConnectorSubPage";
            Size = new System.Drawing.Size(1100, 680);
            splitContainerMain.Panel1.ResumeLayout(false);
            splitContainerMain.Panel1.PerformLayout();
            splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).EndInit();
            splitContainerMain.ResumeLayout(false);
            pnlLeftHeader.ResumeLayout(false);
            pnlLeftHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvConnectorList).EndInit();
            pnlRightContainer.ResumeLayout(false);
            tblForm.ResumeLayout(false);
            tblForm.PerformLayout();
            pnlPictureContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picBox).EndInit();
            pnlPicButtons.ResumeLayout(false);
            pnlBottomBar.ResumeLayout(false);
            pnlBottomButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.Panel pnlLeftHeader;
        private System.Windows.Forms.Label lblConnectorList;
        private System.Windows.Forms.Button btnNewConnector;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridView dgvConnectorList;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colConnectorName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPartNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalPin;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.Panel pnlRightContainer;
        private System.Windows.Forms.Panel pnlBottomBar;
        private System.Windows.Forms.TableLayoutPanel tblForm;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblTotalPins;
        private System.Windows.Forms.TextBox txtTotalPins;
        private System.Windows.Forms.Label lblPartNumber;
        private System.Windows.Forms.TextBox txtPartNumber;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblPicture;
        private System.Windows.Forms.Panel pnlPictureContainer;
        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.FlowLayoutPanel pnlPicButtons;
        private System.Windows.Forms.Button btnAddPicture;
        private System.Windows.Forms.Button btnDeletePicture;
        private System.Windows.Forms.FlowLayoutPanel pnlBottomButtons;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
    }
}
