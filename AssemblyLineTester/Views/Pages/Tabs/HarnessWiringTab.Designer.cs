namespace AssemblyLineTester.Views.Pages.Tabs
{
    partial class HarnessWiringTab
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
            tableLayoutPanelMain = new TableLayoutPanel();
            tableLayoutPanelLeft = new TableLayoutPanel();
            tableLayoutPanelToolbar = new TableLayoutPanel();
            btnAddTestPoint = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnEdit = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnDelete = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnImportCsv = new AssemblyLineTester.Views.Controls.RoundedButton();
            txtSearch = new TextBox();
            btnSearch = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnSave = new AssemblyLineTester.Views.Controls.RoundedButton();
            gridWiring = new DataGridView();
            colNetId = new DataGridViewTextBoxColumn();
            colFromAddr = new DataGridViewTextBoxColumn();
            colToAddr = new DataGridViewTextBoxColumn();
            colTestType = new DataGridViewComboBoxColumn();
            colExpected = new DataGridViewTextBoxColumn();
            tableLayoutPanelRight = new TableLayoutPanel();
            lblTestParameterDetails = new Label();
            picDiagram = new PictureBox();
            tableLayoutPanelMain.SuspendLayout();
            tableLayoutPanelLeft.SuspendLayout();
            tableLayoutPanelToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridWiring).BeginInit();
            tableLayoutPanelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picDiagram).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.ColumnCount = 2;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 51.6571426F));
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48.3428574F));
            tableLayoutPanelMain.Controls.Add(tableLayoutPanelLeft, 0, 0);
            tableLayoutPanelMain.Controls.Add(tableLayoutPanelRight, 1, 0);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.Location = new Point(0, 0);
            tableLayoutPanelMain.Margin = new Padding(3, 2, 3, 2);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 1;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Size = new Size(875, 450);
            tableLayoutPanelMain.TabIndex = 0;
            // 
            // tableLayoutPanelLeft
            // 
            tableLayoutPanelLeft.ColumnCount = 1;
            tableLayoutPanelLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelLeft.Controls.Add(tableLayoutPanelToolbar, 0, 0);
            tableLayoutPanelLeft.Controls.Add(gridWiring, 0, 1);
            tableLayoutPanelLeft.Dock = DockStyle.Fill;
            tableLayoutPanelLeft.Location = new Point(9, 8);
            tableLayoutPanelLeft.Margin = new Padding(9, 8, 9, 8);
            tableLayoutPanelLeft.Name = "tableLayoutPanelLeft";
            tableLayoutPanelLeft.RowCount = 2;
            tableLayoutPanelLeft.RowStyles.Add(new RowStyle());
            tableLayoutPanelLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelLeft.Size = new Size(434, 434);
            tableLayoutPanelLeft.TabIndex = 0;
            // 
            // tableLayoutPanelToolbar
            // 
            tableLayoutPanelToolbar.AutoSize = true;
            tableLayoutPanelToolbar.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanelToolbar.ColumnCount = 4;
            tableLayoutPanelToolbar.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanelToolbar.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanelToolbar.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanelToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelToolbar.Controls.Add(btnAddTestPoint, 0, 0);
            tableLayoutPanelToolbar.Controls.Add(btnEdit, 1, 0);
            tableLayoutPanelToolbar.Controls.Add(btnDelete, 2, 0);
            tableLayoutPanelToolbar.Controls.Add(btnImportCsv, 3, 0);
            tableLayoutPanelToolbar.Controls.Add(txtSearch, 0, 1);
            tableLayoutPanelToolbar.Controls.Add(btnSearch, 2, 1);
            tableLayoutPanelToolbar.Controls.Add(btnSave, 3, 1);
            tableLayoutPanelToolbar.Dock = DockStyle.Fill;
            tableLayoutPanelToolbar.Location = new Point(0, 0);
            tableLayoutPanelToolbar.Margin = new Padding(0, 0, 0, 8);
            tableLayoutPanelToolbar.Name = "tableLayoutPanelToolbar";
            tableLayoutPanelToolbar.RowCount = 2;
            tableLayoutPanelToolbar.RowStyles.Add(new RowStyle());
            tableLayoutPanelToolbar.RowStyles.Add(new RowStyle());
            tableLayoutPanelToolbar.Size = new Size(434, 60);
            tableLayoutPanelToolbar.TabIndex = 1;
            // 
            // btnAddTestPoint
            // 
            btnAddTestPoint.BackColor = Color.FromArgb(250, 204, 21);
            btnAddTestPoint.BorderRadius = 8;
            btnAddTestPoint.FlatStyle = FlatStyle.Flat;
            btnAddTestPoint.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAddTestPoint.ForeColor = Color.FromArgb(15, 23, 42);
            btnAddTestPoint.HoverForeColor = Color.Empty;
            btnAddTestPoint.Location = new Point(3, 2);
            btnAddTestPoint.Margin = new Padding(3, 2, 3, 2);
            btnAddTestPoint.Name = "btnAddTestPoint";
            btnAddTestPoint.PressedForeColor = Color.Empty;
            btnAddTestPoint.Size = new Size(122, 26);
            btnAddTestPoint.TabIndex = 0;
            btnAddTestPoint.Text = "+ Add Test Point";
            btnAddTestPoint.UseVisualStyleBackColor = false;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.FromArgb(226, 232, 240);
            btnEdit.BorderRadius = 8;
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEdit.ForeColor = Color.FromArgb(15, 23, 42);
            btnEdit.HoverBackColor = Color.FromArgb(241, 245, 249);
            btnEdit.HoverForeColor = Color.Empty;
            btnEdit.Location = new Point(131, 2);
            btnEdit.Margin = new Padding(3, 2, 3, 2);
            btnEdit.Name = "btnEdit";
            btnEdit.NormalBackColor = Color.FromArgb(226, 232, 240);
            btnEdit.PressedBackColor = Color.FromArgb(203, 213, 225);
            btnEdit.PressedForeColor = Color.Empty;
            btnEdit.Size = new Size(52, 26);
            btnEdit.TabIndex = 1;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(254, 226, 226);
            btnDelete.BorderRadius = 8;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDelete.ForeColor = Color.FromArgb(15, 23, 42);
            btnDelete.HoverBackColor = Color.FromArgb(254, 242, 242);
            btnDelete.HoverForeColor = Color.Empty;
            btnDelete.Location = new Point(190, 2);
            btnDelete.Margin = new Padding(3, 2, 3, 2);
            btnDelete.Name = "btnDelete";
            btnDelete.NormalBackColor = Color.FromArgb(254, 226, 226);
            btnDelete.PressedBackColor = Color.FromArgb(252, 165, 165);
            btnDelete.PressedForeColor = Color.Empty;
            btnDelete.Size = new Size(61, 26);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnImportCsv
            // 
            btnImportCsv.BackColor = Color.FromArgb(241, 245, 249);
            btnImportCsv.BorderRadius = 8;
            btnImportCsv.FlatStyle = FlatStyle.Flat;
            btnImportCsv.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnImportCsv.ForeColor = Color.FromArgb(15, 23, 42);
            btnImportCsv.HoverBackColor = Color.FromArgb(248, 250, 252);
            btnImportCsv.HoverForeColor = Color.Empty;
            btnImportCsv.Location = new Point(257, 2);
            btnImportCsv.Margin = new Padding(3, 2, 3, 2);
            btnImportCsv.Name = "btnImportCsv";
            btnImportCsv.NormalBackColor = Color.FromArgb(241, 245, 249);
            btnImportCsv.PressedBackColor = Color.FromArgb(226, 232, 240);
            btnImportCsv.PressedForeColor = Color.Empty;
            btnImportCsv.Size = new Size(88, 26);
            btnImportCsv.TabIndex = 3;
            btnImportCsv.Text = "Import CSV";
            btnImportCsv.UseVisualStyleBackColor = false;
            // 
            // txtSearch
            // 
            txtSearch.Anchor = AnchorStyles.Left;
            tableLayoutPanelToolbar.SetColumnSpan(txtSearch, 2);
            txtSearch.Location = new Point(3, 33);
            txtSearch.Margin = new Padding(3, 2, 3, 2);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(181, 23);
            txtSearch.TabIndex = 4;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(241, 245, 249);
            btnSearch.BorderRadius = 8;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSearch.ForeColor = Color.FromArgb(15, 23, 42);
            btnSearch.HoverBackColor = Color.FromArgb(248, 250, 252);
            btnSearch.HoverForeColor = Color.Empty;
            btnSearch.Location = new Point(190, 32);
            btnSearch.Margin = new Padding(3, 2, 3, 2);
            btnSearch.Name = "btnSearch";
            btnSearch.NormalBackColor = Color.FromArgb(241, 245, 249);
            btnSearch.PressedBackColor = Color.FromArgb(226, 232, 240);
            btnSearch.PressedForeColor = Color.Empty;
            btnSearch.Size = new Size(61, 26);
            btnSearch.TabIndex = 5;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(74, 222, 128);
            btnSave.BorderRadius = 8;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSave.ForeColor = Color.FromArgb(15, 23, 42);
            btnSave.HoverBackColor = Color.FromArgb(134, 239, 172);
            btnSave.HoverForeColor = Color.Empty;
            btnSave.Location = new Point(257, 32);
            btnSave.Margin = new Padding(3, 2, 3, 2);
            btnSave.Name = "btnSave";
            btnSave.NormalBackColor = Color.FromArgb(74, 222, 128);
            btnSave.PressedBackColor = Color.FromArgb(34, 197, 94);
            btnSave.PressedForeColor = Color.Empty;
            btnSave.Size = new Size(52, 26);
            btnSave.TabIndex = 6;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // gridWiring
            // 
            gridWiring.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridWiring.BackgroundColor = Color.White;
            gridWiring.ColumnHeadersHeight = 45;
            gridWiring.Columns.AddRange(new DataGridViewColumn[] { colNetId, colFromAddr, colToAddr, colTestType, colExpected });
            gridWiring.Dock = DockStyle.Fill;
            gridWiring.Location = new Point(0, 68);
            gridWiring.Margin = new Padding(0);
            gridWiring.Name = "gridWiring";
            gridWiring.RowHeadersVisible = false;
            gridWiring.RowTemplate.Height = 35;
            gridWiring.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridWiring.Size = new Size(434, 366);
            gridWiring.TabIndex = 2;
            // 
            // colNetId
            // 
            colNetId.HeaderText = "Net ID";
            colNetId.Name = "colNetId";
            // 
            // colFromAddr
            // 
            colFromAddr.HeaderText = "From";
            colFromAddr.Name = "colFromAddr";
            // 
            // colToAddr
            // 
            colToAddr.HeaderText = "To";
            colToAddr.Name = "colToAddr";
            // 
            // colTestType
            // 
            colTestType.HeaderText = "Type";
            colTestType.Name = "colTestType";
            // 
            // colExpected
            // 
            colExpected.HeaderText = "Expected";
            colExpected.Name = "colExpected";
            // 
            // tableLayoutPanelRight
            // 
            tableLayoutPanelRight.ColumnCount = 1;
            tableLayoutPanelRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelRight.Controls.Add(lblTestParameterDetails, 0, 0);
            tableLayoutPanelRight.Controls.Add(picDiagram, 0, 1);
            tableLayoutPanelRight.Dock = DockStyle.Fill;
            tableLayoutPanelRight.Location = new Point(461, 8);
            tableLayoutPanelRight.Margin = new Padding(9, 8, 9, 8);
            tableLayoutPanelRight.Name = "tableLayoutPanelRight";
            tableLayoutPanelRight.RowCount = 2;
            tableLayoutPanelRight.RowStyles.Add(new RowStyle());
            tableLayoutPanelRight.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelRight.Size = new Size(405, 434);
            tableLayoutPanelRight.TabIndex = 1;
            // 
            // lblTestParameterDetails
            // 
            lblTestParameterDetails.AutoSize = true;
            lblTestParameterDetails.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTestParameterDetails.ForeColor = Color.FromArgb(15, 23, 42);
            lblTestParameterDetails.Location = new Point(0, 0);
            lblTestParameterDetails.Margin = new Padding(0, 0, 0, 8);
            lblTestParameterDetails.Name = "lblTestParameterDetails";
            lblTestParameterDetails.Size = new Size(182, 19);
            lblTestParameterDetails.TabIndex = 0;
            lblTestParameterDetails.Text = "TEST PARAMETER DETAILS";
            // 
            // picDiagram
            // 
            picDiagram.BackColor = Color.FromArgb(248, 250, 252);
            picDiagram.Dock = DockStyle.Fill;
            picDiagram.Location = new Point(0, 27);
            picDiagram.Margin = new Padding(0);
            picDiagram.Name = "picDiagram";
            picDiagram.Size = new Size(405, 407);
            picDiagram.TabIndex = 1;
            picDiagram.TabStop = false;
            // 
            // HarnessWiringTab
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(tableLayoutPanelMain);
            Margin = new Padding(3, 2, 3, 2);
            Name = "HarnessWiringTab";
            Size = new Size(875, 450);
            tableLayoutPanelMain.ResumeLayout(false);
            tableLayoutPanelLeft.ResumeLayout(false);
            tableLayoutPanelLeft.PerformLayout();
            tableLayoutPanelToolbar.ResumeLayout(false);
            tableLayoutPanelToolbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridWiring).EndInit();
            tableLayoutPanelRight.ResumeLayout(false);
            tableLayoutPanelRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picDiagram).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelLeft;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelRight;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelToolbar;
        private AssemblyLineTester.Views.Controls.RoundedButton btnAddTestPoint;
        private AssemblyLineTester.Views.Controls.RoundedButton btnEdit;
        private AssemblyLineTester.Views.Controls.RoundedButton btnDelete;
        private AssemblyLineTester.Views.Controls.RoundedButton btnImportCsv;
        private System.Windows.Forms.TextBox txtSearch;
        private AssemblyLineTester.Views.Controls.RoundedButton btnSearch;
        private AssemblyLineTester.Views.Controls.RoundedButton btnSave;
        private System.Windows.Forms.DataGridView gridWiring;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNetId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFromAddr;
        private System.Windows.Forms.DataGridViewTextBoxColumn colToAddr;
        private System.Windows.Forms.DataGridViewComboBoxColumn colTestType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExpected;
        private System.Windows.Forms.Label lblTestParameterDetails;
        private System.Windows.Forms.PictureBox picDiagram;
    }
}
