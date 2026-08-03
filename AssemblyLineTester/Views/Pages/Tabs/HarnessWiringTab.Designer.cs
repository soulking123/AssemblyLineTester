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
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelLeft = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelToolbar = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddTestPoint = new AssemblyLineTester.Views.Controls.RoundedButton();
            this.btnEdit = new AssemblyLineTester.Views.Controls.RoundedButton();
            this.btnDelete = new AssemblyLineTester.Views.Controls.RoundedButton();
            this.btnImportCsv = new AssemblyLineTester.Views.Controls.RoundedButton();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new AssemblyLineTester.Views.Controls.RoundedButton();
            this.btnSave = new AssemblyLineTester.Views.Controls.RoundedButton();
            this.gridWiring = new System.Windows.Forms.DataGridView();
            this.colNetId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFromAddr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colToAddr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTestType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colExpected = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanelRight = new System.Windows.Forms.TableLayoutPanel();
            this.lblTestParameterDetails = new System.Windows.Forms.Label();
            this.picDiagram = new System.Windows.Forms.PictureBox();
            
            this.tableLayoutPanelMain.SuspendLayout();
            this.tableLayoutPanelLeft.SuspendLayout();
            this.tableLayoutPanelToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridWiring)).BeginInit();
            this.tableLayoutPanelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDiagram)).BeginInit();
            this.SuspendLayout();
            
            // tableLayoutPanelMain
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelLeft, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelRight, 1, 0);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 1;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(1000, 600);
            this.tableLayoutPanelMain.TabIndex = 0;
            
            // tableLayoutPanelLeft
            this.tableLayoutPanelLeft.ColumnCount = 1;
            this.tableLayoutPanelLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelLeft.Controls.Add(this.tableLayoutPanelToolbar, 0, 0);
            this.tableLayoutPanelLeft.Controls.Add(this.gridWiring, 0, 1);
            this.tableLayoutPanelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelLeft.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanelLeft.Margin = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanelLeft.Name = "tableLayoutPanelLeft";
            this.tableLayoutPanelLeft.RowCount = 2;
            this.tableLayoutPanelLeft.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelLeft.Size = new System.Drawing.Size(680, 580);
            this.tableLayoutPanelLeft.TabIndex = 0;
            
            // tableLayoutPanelToolbar
            this.tableLayoutPanelToolbar.AutoSize = true;
            this.tableLayoutPanelToolbar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanelToolbar.ColumnCount = 4;
            this.tableLayoutPanelToolbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelToolbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelToolbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelToolbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelToolbar.Controls.Add(this.btnAddTestPoint, 0, 0);
            this.tableLayoutPanelToolbar.Controls.Add(this.btnEdit, 1, 0);
            this.tableLayoutPanelToolbar.Controls.Add(this.btnDelete, 2, 0);
            this.tableLayoutPanelToolbar.Controls.Add(this.btnImportCsv, 3, 0);
            this.tableLayoutPanelToolbar.Controls.Add(this.txtSearch, 0, 1);
            this.tableLayoutPanelToolbar.Controls.Add(this.btnSearch, 2, 1);
            this.tableLayoutPanelToolbar.Controls.Add(this.btnSave, 3, 1);
            this.tableLayoutPanelToolbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelToolbar.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelToolbar.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.tableLayoutPanelToolbar.Name = "tableLayoutPanelToolbar";
            this.tableLayoutPanelToolbar.RowCount = 2;
            this.tableLayoutPanelToolbar.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelToolbar.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelToolbar.Size = new System.Drawing.Size(680, 84);
            this.tableLayoutPanelToolbar.TabIndex = 1;
            
            // btnAddTestPoint
            this.btnAddTestPoint.NormalBackColor = System.Drawing.Color.FromArgb(250, 204, 21);
            this.btnAddTestPoint.HoverBackColor = System.Drawing.Color.FromArgb(254, 216, 53);
            this.btnAddTestPoint.PressedBackColor = System.Drawing.Color.FromArgb(234, 186, 10);
            this.btnAddTestPoint.BorderRadius = 8;
            this.btnAddTestPoint.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddTestPoint.Location = new System.Drawing.Point(3, 3);
            this.btnAddTestPoint.Name = "btnAddTestPoint";
            this.btnAddTestPoint.Size = new System.Drawing.Size(140, 35);
            this.btnAddTestPoint.TabIndex = 0;
            this.btnAddTestPoint.Text = "+ Add Test Point";
            
            // btnEdit
            this.btnEdit.NormalBackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.btnEdit.HoverBackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.btnEdit.PressedBackColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.btnEdit.BorderRadius = 8;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEdit.Location = new System.Drawing.Point(149, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(60, 35);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Edit";
            
            // btnDelete
            this.btnDelete.NormalBackColor = System.Drawing.Color.FromArgb(254, 226, 226);
            this.btnDelete.HoverBackColor = System.Drawing.Color.FromArgb(254, 242, 242);
            this.btnDelete.PressedBackColor = System.Drawing.Color.FromArgb(252, 165, 165);
            this.btnDelete.BorderRadius = 8;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDelete.Location = new System.Drawing.Point(215, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(70, 35);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            
            // btnImportCsv
            this.btnImportCsv.NormalBackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.btnImportCsv.HoverBackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.btnImportCsv.PressedBackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.btnImportCsv.BorderRadius = 8;
            this.btnImportCsv.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnImportCsv.Location = new System.Drawing.Point(291, 3);
            this.btnImportCsv.Name = "btnImportCsv";
            this.btnImportCsv.Size = new System.Drawing.Size(100, 35);
            this.btnImportCsv.TabIndex = 3;
            this.btnImportCsv.Text = "Import CSV";
            
            // txtSearch
            this.txtSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanelToolbar.SetColumnSpan(this.txtSearch, 2);
            this.txtSearch.Location = new System.Drawing.Point(3, 49);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(206, 27);
            this.txtSearch.TabIndex = 4;
            
            // btnSearch
            this.btnSearch.NormalBackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.btnSearch.HoverBackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.btnSearch.PressedBackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.btnSearch.BorderRadius = 8;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSearch.Location = new System.Drawing.Point(215, 44);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(70, 35);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "Search";
            
            // btnSave
            this.btnSave.NormalBackColor = System.Drawing.Color.FromArgb(74, 222, 128);
            this.btnSave.HoverBackColor = System.Drawing.Color.FromArgb(134, 239, 172);
            this.btnSave.PressedBackColor = System.Drawing.Color.FromArgb(34, 197, 94);
            this.btnSave.BorderRadius = 8;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.Location = new System.Drawing.Point(291, 44);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 35);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            
            // gridWiring
            this.gridWiring.AllowUserToAddRows = true;
            this.gridWiring.AllowUserToDeleteRows = true;
            this.gridWiring.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridWiring.BackgroundColor = System.Drawing.Color.White;
            this.gridWiring.ColumnHeadersHeight = 45;
            this.gridWiring.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNetId,
            this.colFromAddr,
            this.colToAddr,
            this.colTestType,
            this.colExpected});
            this.gridWiring.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridWiring.Location = new System.Drawing.Point(0, 94);
            this.gridWiring.Margin = new System.Windows.Forms.Padding(0);
            this.gridWiring.Name = "gridWiring";
            this.gridWiring.RowHeadersVisible = false;
            this.gridWiring.RowTemplate.Height = 35;
            this.gridWiring.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridWiring.Size = new System.Drawing.Size(680, 486);
            this.gridWiring.TabIndex = 2;
            
            // colNetId
            this.colNetId.HeaderText = "Net ID";
            this.colNetId.Name = "colNetId";
            
            // colFromAddr
            this.colFromAddr.HeaderText = "From";
            this.colFromAddr.Name = "colFromAddr";
            
            // colToAddr
            this.colToAddr.HeaderText = "To";
            this.colToAddr.Name = "colToAddr";
            
            // colTestType
            this.colTestType.HeaderText = "Type";
            this.colTestType.Name = "colTestType";
            
            // colExpected
            this.colExpected.HeaderText = "Expected";
            this.colExpected.Name = "colExpected";
            
            // tableLayoutPanelRight
            this.tableLayoutPanelRight.ColumnCount = 1;
            this.tableLayoutPanelRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelRight.Controls.Add(this.lblTestParameterDetails, 0, 0);
            this.tableLayoutPanelRight.Controls.Add(this.picDiagram, 0, 1);
            this.tableLayoutPanelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelRight.Location = new System.Drawing.Point(710, 10);
            this.tableLayoutPanelRight.Margin = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanelRight.Name = "tableLayoutPanelRight";
            this.tableLayoutPanelRight.RowCount = 2;
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelRight.Size = new System.Drawing.Size(280, 580);
            this.tableLayoutPanelRight.TabIndex = 1;
            
            // lblTestParameterDetails
            this.lblTestParameterDetails.AutoSize = true;
            this.lblTestParameterDetails.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTestParameterDetails.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblTestParameterDetails.Location = new System.Drawing.Point(0, 0);
            this.lblTestParameterDetails.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.lblTestParameterDetails.Name = "lblTestParameterDetails";
            this.lblTestParameterDetails.Size = new System.Drawing.Size(200, 23);
            this.lblTestParameterDetails.TabIndex = 0;
            this.lblTestParameterDetails.Text = "TEST PARAMETER DETAILS";
            
            // picDiagram
            this.picDiagram.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.picDiagram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picDiagram.Location = new System.Drawing.Point(0, 33);
            this.picDiagram.Margin = new System.Windows.Forms.Padding(0);
            this.picDiagram.Name = "picDiagram";
            this.picDiagram.Size = new System.Drawing.Size(280, 547);
            this.picDiagram.TabIndex = 1;
            this.picDiagram.TabStop = false;
            
            // HarnessWiringTab
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Name = "HarnessWiringTab";
            this.Size = new System.Drawing.Size(1000, 600);
            
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelLeft.ResumeLayout(false);
            this.tableLayoutPanelLeft.PerformLayout();
            this.tableLayoutPanelToolbar.ResumeLayout(false);
            this.tableLayoutPanelToolbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridWiring)).EndInit();
            this.tableLayoutPanelRight.ResumeLayout(false);
            this.tableLayoutPanelRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDiagram)).EndInit();
            this.ResumeLayout(false);
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
