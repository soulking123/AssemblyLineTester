using AssemblyLineTester.Services;
using System.Drawing;
using System.Windows.Forms;

namespace AssemblyLineTester.Views.Modals
{
    partial class SaveTestReportModalForm
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
            iconLogo = new Label();
            lblHeaderTitle = new Label();
            btnClose = new Button();
            pnlMainContainer = new Panel();
            pnlGenInfoBox = new Panel();
            tblGenGrid = new TableLayoutPanel();
            lblGenModelKey = new Label();
            lblGenModelVal = new Label();
            lblGenEcnKey = new Label();
            lblGenEcnVal = new Label();
            lblGenTypeKey = new Label();
            lblGenTypeVal = new Label();
            lblGenRevKey = new Label();
            lblGenRevVal = new Label();
            lblGenAreaKey = new Label();
            lblGenAreaVal = new Label();
            lblGenOpKey = new Label();
            txtGenOpVal = new TextBox();
            lblGenPartKey = new Label();
            lblGenPartVal = new Label();
            lblGenTimeKey = new Label();
            lblGenTimeVal = new Label();
            lblGenSnKey = new Label();
            txtGenSnVal = new TextBox();
            lblGenResKey = new Label();
            lblGenResVal = new Label();
            lblGenTitle = new Label();
            lblInstruction = new Label();
            pnlFooter = new Panel();
            btnConfirmSave = new Button();
            btnCancel = new Button();
            pnlCustomHeader.SuspendLayout();
            pnlMainContainer.SuspendLayout();
            pnlGenInfoBox.SuspendLayout();
            tblGenGrid.SuspendLayout();
            pnlFooter.SuspendLayout();
            SuspendLayout();
            // 
            // pnlCustomHeader
            // 
            pnlCustomHeader.BackColor = Color.FromArgb(34, 40, 49);
            pnlCustomHeader.Controls.Add(iconLogo);
            pnlCustomHeader.Controls.Add(lblHeaderTitle);
            pnlCustomHeader.Controls.Add(btnClose);
            pnlCustomHeader.Dock = DockStyle.Top;
            pnlCustomHeader.Location = new Point(0, 0);
            pnlCustomHeader.Margin = new Padding(6);
            pnlCustomHeader.Name = "pnlCustomHeader";
            pnlCustomHeader.Size = new Size(1783, 119);
            pnlCustomHeader.TabIndex = 0;
            // 
            // iconLogo
            // 
            iconLogo.AutoSize = true;
            iconLogo.Font = new Font("Segoe UI", 16F);
            iconLogo.ForeColor = Color.White;
            iconLogo.Location = new Point(30, 26);
            iconLogo.Margin = new Padding(6, 0, 6, 0);
            iconLogo.Name = "iconLogo";
            iconLogo.Size = new Size(84, 59);
            iconLogo.TabIndex = 0;
            iconLogo.Text = "💾";
            // 
            // lblHeaderTitle
            // 
            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblHeaderTitle.ForeColor = Color.FromArgb(241, 245, 249);
            lblHeaderTitle.Location = new Point(108, 38);
            lblHeaderTitle.Margin = new Padding(6, 0, 6, 0);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new Size(386, 41);
            lblHeaderTitle.TabIndex = 1;
            lblHeaderTitle.Text = "Save Test Execution Report";
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnClose.ForeColor = Color.FromArgb(203, 213, 225);
            btnClose.Location = new Point(1694, 21);
            btnClose.Margin = new Padding(6);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(67, 77);
            btnClose.TabIndex = 2;
            btnClose.Text = "✕";
            btnClose.UseVisualStyleBackColor = true;
            // 
            // pnlMainContainer
            // 
            pnlMainContainer.BackColor = Color.White;
            pnlMainContainer.Controls.Add(pnlGenInfoBox);
            pnlMainContainer.Controls.Add(lblInstruction);
            pnlMainContainer.Dock = DockStyle.Fill;
            pnlMainContainer.Location = new Point(0, 119);
            pnlMainContainer.Margin = new Padding(6);
            pnlMainContainer.Name = "pnlMainContainer";
            pnlMainContainer.Padding = new Padding(45, 43, 45, 21);
            pnlMainContainer.Size = new Size(1783, 841);
            pnlMainContainer.TabIndex = 1;
            // 
            // pnlGenInfoBox
            // 
            pnlGenInfoBox.BackColor = Color.FromArgb(248, 250, 252);
            pnlGenInfoBox.Controls.Add(tblGenGrid);
            pnlGenInfoBox.Controls.Add(lblGenTitle);
            pnlGenInfoBox.Dock = DockStyle.Fill;
            pnlGenInfoBox.Location = new Point(45, 116);
            pnlGenInfoBox.Margin = new Padding(0);
            pnlGenInfoBox.Name = "pnlGenInfoBox";
            pnlGenInfoBox.Padding = new Padding(30, 34, 30, 34);
            pnlGenInfoBox.Size = new Size(1693, 704);
            pnlGenInfoBox.TabIndex = 1;
            // 
            // tblGenGrid
            // 
            tblGenGrid.ColumnCount = 4;
            tblGenGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22F));
            tblGenGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
            tblGenGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22F));
            tblGenGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
            tblGenGrid.Controls.Add(lblGenModelKey, 0, 0);
            tblGenGrid.Controls.Add(lblGenModelVal, 1, 0);
            tblGenGrid.Controls.Add(lblGenEcnKey, 2, 0);
            tblGenGrid.Controls.Add(lblGenEcnVal, 3, 0);
            tblGenGrid.Controls.Add(lblGenTypeKey, 0, 1);
            tblGenGrid.Controls.Add(lblGenTypeVal, 1, 1);
            tblGenGrid.Controls.Add(lblGenRevKey, 2, 1);
            tblGenGrid.Controls.Add(lblGenRevVal, 3, 1);
            tblGenGrid.Controls.Add(lblGenAreaKey, 0, 2);
            tblGenGrid.Controls.Add(lblGenAreaVal, 1, 2);
            tblGenGrid.Controls.Add(lblGenOpKey, 2, 2);
            tblGenGrid.Controls.Add(txtGenOpVal, 3, 2);
            tblGenGrid.Controls.Add(lblGenPartKey, 0, 3);
            tblGenGrid.Controls.Add(lblGenPartVal, 1, 3);
            tblGenGrid.Controls.Add(lblGenTimeKey, 2, 3);
            tblGenGrid.Controls.Add(lblGenTimeVal, 3, 3);
            tblGenGrid.Controls.Add(lblGenSnKey, 0, 4);
            tblGenGrid.Controls.Add(txtGenSnVal, 1, 4);
            tblGenGrid.Controls.Add(lblGenResKey, 2, 4);
            tblGenGrid.Controls.Add(lblGenResVal, 3, 4);
            tblGenGrid.Dock = DockStyle.Fill;
            tblGenGrid.Location = new Point(30, 102);
            tblGenGrid.Margin = new Padding(0);
            tblGenGrid.Name = "tblGenGrid";
            tblGenGrid.RowCount = 5;
            tblGenGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tblGenGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tblGenGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tblGenGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tblGenGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tblGenGrid.Size = new Size(1633, 568);
            tblGenGrid.TabIndex = 1;
            // 
            // lblGenModelKey
            // 
            lblGenModelKey.Dock = DockStyle.Fill;
            lblGenModelKey.Font = new Font("Segoe UI", 9F);
            lblGenModelKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblGenModelKey.Location = new Point(6, 0);
            lblGenModelKey.Margin = new Padding(6, 0, 6, 0);
            lblGenModelKey.Name = "lblGenModelKey";
            lblGenModelKey.Size = new Size(347, 113);
            lblGenModelKey.TabIndex = 0;
            lblGenModelKey.Text = "Product Model:";
            lblGenModelKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGenModelVal
            // 
            lblGenModelVal.Dock = DockStyle.Fill;
            lblGenModelVal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblGenModelVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblGenModelVal.Location = new Point(365, 0);
            lblGenModelVal.Margin = new Padding(6, 0, 6, 0);
            lblGenModelVal.Name = "lblGenModelVal";
            lblGenModelVal.Size = new Size(445, 113);
            lblGenModelVal.TabIndex = 1;
            lblGenModelVal.Text = "-";
            lblGenModelVal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGenEcnKey
            // 
            lblGenEcnKey.Dock = DockStyle.Fill;
            lblGenEcnKey.Font = new Font("Segoe UI", 9F);
            lblGenEcnKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblGenEcnKey.Location = new Point(822, 0);
            lblGenEcnKey.Margin = new Padding(6, 0, 6, 0);
            lblGenEcnKey.Name = "lblGenEcnKey";
            lblGenEcnKey.Size = new Size(347, 113);
            lblGenEcnKey.TabIndex = 2;
            lblGenEcnKey.Text = "ECN No:";
            lblGenEcnKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGenEcnVal
            // 
            lblGenEcnVal.Dock = DockStyle.Fill;
            lblGenEcnVal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblGenEcnVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblGenEcnVal.Location = new Point(1181, 0);
            lblGenEcnVal.Margin = new Padding(6, 0, 6, 0);
            lblGenEcnVal.Name = "lblGenEcnVal";
            lblGenEcnVal.Size = new Size(446, 113);
            lblGenEcnVal.TabIndex = 3;
            lblGenEcnVal.Text = "-";
            lblGenEcnVal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGenTypeKey
            // 
            lblGenTypeKey.Dock = DockStyle.Fill;
            lblGenTypeKey.Font = new Font("Segoe UI", 9F);
            lblGenTypeKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblGenTypeKey.Location = new Point(6, 113);
            lblGenTypeKey.Margin = new Padding(6, 0, 6, 0);
            lblGenTypeKey.Name = "lblGenTypeKey";
            lblGenTypeKey.Size = new Size(347, 113);
            lblGenTypeKey.TabIndex = 4;
            lblGenTypeKey.Text = "Product Type:";
            lblGenTypeKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGenTypeVal
            // 
            lblGenTypeVal.Dock = DockStyle.Fill;
            lblGenTypeVal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblGenTypeVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblGenTypeVal.Location = new Point(365, 113);
            lblGenTypeVal.Margin = new Padding(6, 0, 6, 0);
            lblGenTypeVal.Name = "lblGenTypeVal";
            lblGenTypeVal.Size = new Size(445, 113);
            lblGenTypeVal.TabIndex = 5;
            lblGenTypeVal.Text = "-";
            lblGenTypeVal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGenRevKey
            // 
            lblGenRevKey.Dock = DockStyle.Fill;
            lblGenRevKey.Font = new Font("Segoe UI", 9F);
            lblGenRevKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblGenRevKey.Location = new Point(822, 113);
            lblGenRevKey.Margin = new Padding(6, 0, 6, 0);
            lblGenRevKey.Name = "lblGenRevKey";
            lblGenRevKey.Size = new Size(347, 113);
            lblGenRevKey.TabIndex = 6;
            lblGenRevKey.Text = "Revision:";
            lblGenRevKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGenRevVal
            // 
            lblGenRevVal.Dock = DockStyle.Fill;
            lblGenRevVal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblGenRevVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblGenRevVal.Location = new Point(1181, 113);
            lblGenRevVal.Margin = new Padding(6, 0, 6, 0);
            lblGenRevVal.Name = "lblGenRevVal";
            lblGenRevVal.Size = new Size(446, 113);
            lblGenRevVal.TabIndex = 7;
            lblGenRevVal.Text = "-";
            lblGenRevVal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGenAreaKey
            // 
            lblGenAreaKey.Dock = DockStyle.Fill;
            lblGenAreaKey.Font = new Font("Segoe UI", 9F);
            lblGenAreaKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblGenAreaKey.Location = new Point(6, 226);
            lblGenAreaKey.Margin = new Padding(6, 0, 6, 0);
            lblGenAreaKey.Name = "lblGenAreaKey";
            lblGenAreaKey.Size = new Size(347, 113);
            lblGenAreaKey.TabIndex = 8;
            lblGenAreaKey.Text = "Area:";
            lblGenAreaKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGenAreaVal
            // 
            lblGenAreaVal.Dock = DockStyle.Fill;
            lblGenAreaVal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblGenAreaVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblGenAreaVal.Location = new Point(365, 226);
            lblGenAreaVal.Margin = new Padding(6, 0, 6, 0);
            lblGenAreaVal.Name = "lblGenAreaVal";
            lblGenAreaVal.Size = new Size(445, 113);
            lblGenAreaVal.TabIndex = 9;
            lblGenAreaVal.Text = "-";
            lblGenAreaVal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGenOpKey
            // 
            lblGenOpKey.Dock = DockStyle.Fill;
            lblGenOpKey.Font = new Font("Segoe UI", 9F);
            lblGenOpKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblGenOpKey.Location = new Point(822, 226);
            lblGenOpKey.Margin = new Padding(6, 0, 6, 0);
            lblGenOpKey.Name = "lblGenOpKey";
            lblGenOpKey.Size = new Size(347, 113);
            lblGenOpKey.TabIndex = 10;
            lblGenOpKey.Text = "Operator:";
            lblGenOpKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtGenOpVal
            // 
            txtGenOpVal.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtGenOpVal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            txtGenOpVal.ForeColor = Color.FromArgb(30, 41, 59);
            txtGenOpVal.Location = new Point(1181, 263);
            txtGenOpVal.Margin = new Padding(6);
            txtGenOpVal.Name = "txtGenOpVal";
            txtGenOpVal.Size = new Size(446, 39);
            txtGenOpVal.TabIndex = 11;
            // 
            // lblGenPartKey
            // 
            lblGenPartKey.Dock = DockStyle.Fill;
            lblGenPartKey.Font = new Font("Segoe UI", 9F);
            lblGenPartKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblGenPartKey.Location = new Point(6, 339);
            lblGenPartKey.Margin = new Padding(6, 0, 6, 0);
            lblGenPartKey.Name = "lblGenPartKey";
            lblGenPartKey.Size = new Size(347, 113);
            lblGenPartKey.TabIndex = 12;
            lblGenPartKey.Text = "Part Number:";
            lblGenPartKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGenPartVal
            // 
            lblGenPartVal.Dock = DockStyle.Fill;
            lblGenPartVal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblGenPartVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblGenPartVal.Location = new Point(365, 339);
            lblGenPartVal.Margin = new Padding(6, 0, 6, 0);
            lblGenPartVal.Name = "lblGenPartVal";
            lblGenPartVal.Size = new Size(445, 113);
            lblGenPartVal.TabIndex = 13;
            lblGenPartVal.Text = "-";
            lblGenPartVal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGenTimeKey
            // 
            lblGenTimeKey.Dock = DockStyle.Fill;
            lblGenTimeKey.Font = new Font("Segoe UI", 9F);
            lblGenTimeKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblGenTimeKey.Location = new Point(822, 339);
            lblGenTimeKey.Margin = new Padding(6, 0, 6, 0);
            lblGenTimeKey.Name = "lblGenTimeKey";
            lblGenTimeKey.Size = new Size(347, 113);
            lblGenTimeKey.TabIndex = 14;
            lblGenTimeKey.Text = "Test Time:";
            lblGenTimeKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGenTimeVal
            // 
            lblGenTimeVal.Dock = DockStyle.Fill;
            lblGenTimeVal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblGenTimeVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblGenTimeVal.Location = new Point(1181, 339);
            lblGenTimeVal.Margin = new Padding(6, 0, 6, 0);
            lblGenTimeVal.Name = "lblGenTimeVal";
            lblGenTimeVal.Size = new Size(446, 113);
            lblGenTimeVal.TabIndex = 15;
            lblGenTimeVal.Text = "-";
            lblGenTimeVal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGenSnKey
            // 
            lblGenSnKey.Dock = DockStyle.Fill;
            lblGenSnKey.Font = new Font("Segoe UI", 9F);
            lblGenSnKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblGenSnKey.Location = new Point(6, 452);
            lblGenSnKey.Margin = new Padding(6, 0, 6, 0);
            lblGenSnKey.Name = "lblGenSnKey";
            lblGenSnKey.Size = new Size(347, 116);
            lblGenSnKey.TabIndex = 16;
            lblGenSnKey.Text = "Serial Number:";
            lblGenSnKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtGenSnVal
            // 
            txtGenSnVal.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtGenSnVal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            txtGenSnVal.ForeColor = Color.FromArgb(30, 41, 59);
            txtGenSnVal.Location = new Point(365, 490);
            txtGenSnVal.Margin = new Padding(6);
            txtGenSnVal.Name = "txtGenSnVal";
            txtGenSnVal.Size = new Size(445, 39);
            txtGenSnVal.TabIndex = 17;
            // 
            // lblGenResKey
            // 
            lblGenResKey.Dock = DockStyle.Fill;
            lblGenResKey.Font = new Font("Segoe UI", 9F);
            lblGenResKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblGenResKey.Location = new Point(822, 452);
            lblGenResKey.Margin = new Padding(6, 0, 6, 0);
            lblGenResKey.Name = "lblGenResKey";
            lblGenResKey.Size = new Size(347, 116);
            lblGenResKey.TabIndex = 18;
            lblGenResKey.Text = "Overall Result:";
            lblGenResKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGenResVal
            // 
            lblGenResVal.Dock = DockStyle.Fill;
            lblGenResVal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblGenResVal.ForeColor = Color.FromArgb(4, 120, 87);
            lblGenResVal.Location = new Point(1181, 452);
            lblGenResVal.Margin = new Padding(6, 0, 6, 0);
            lblGenResVal.Name = "lblGenResVal";
            lblGenResVal.Size = new Size(446, 116);
            lblGenResVal.TabIndex = 19;
            lblGenResVal.Text = "PASS";
            lblGenResVal.TextAlign = ContentAlignment.MiddleLeft;
            // lblGenTitle
            // 
            lblGenTitle.Dock = DockStyle.Top;
            lblGenTitle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblGenTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblGenTitle.Location = new Point(30, 34);
            lblGenTitle.Margin = new Padding(6, 0, 6, 0);
            lblGenTitle.Name = "lblGenTitle";
            lblGenTitle.Size = new Size(1633, 68);
            lblGenTitle.TabIndex = 0;
            lblGenTitle.Text = "GENERAL INFORMATION";
            // 
            // lblInstruction
            // 
            lblInstruction.Dock = DockStyle.Top;
            lblInstruction.Font = new Font("Segoe UI", 9.5F);
            lblInstruction.ForeColor = Color.FromArgb(71, 85, 105);
            lblInstruction.Location = new Point(45, 43);
            lblInstruction.Margin = new Padding(6, 0, 6, 0);
            lblInstruction.Name = "lblInstruction";
            lblInstruction.Size = new Size(1693, 73);
            lblInstruction.TabIndex = 0;
            lblInstruction.Text = "Please verify the general information for this test run below. You may adjust the Operator or Serial Number before saving.";
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.FromArgb(34, 40, 49);
            pnlFooter.Controls.Add(btnConfirmSave);
            pnlFooter.Controls.Add(btnCancel);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 960);
            pnlFooter.Margin = new Padding(6);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new Padding(45, 30, 45, 30);
            pnlFooter.Size = new Size(1783, 149);
            pnlFooter.TabIndex = 2;
            // 
            // btnConfirmSave
            // 
            btnConfirmSave.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            btnConfirmSave.BackColor = Color.FromArgb(22, 163, 74);
            btnConfirmSave.FlatAppearance.BorderSize = 0;
            btnConfirmSave.FlatStyle = FlatStyle.Flat;
            btnConfirmSave.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnConfirmSave.ForeColor = Color.White;
            btnConfirmSave.Location = new Point(1330, 34);
            btnConfirmSave.Margin = new Padding(6);
            btnConfirmSave.Name = "btnConfirmSave";
            btnConfirmSave.Size = new Size(409, 81);
            btnConfirmSave.TabIndex = 0;
            btnConfirmSave.Text = "✔ Save Report";
            btnConfirmSave.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            btnCancel.BackColor = Color.FromArgb(226, 232, 240);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCancel.ForeColor = Color.FromArgb(71, 85, 105);
            btnCancel.Location = new Point(1107, 34);
            btnCancel.Margin = new Padding(6);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(204, 81);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // SaveTestReportModalForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1783, 1109);
            Controls.Add(pnlMainContainer);
            Controls.Add(pnlFooter);
            Controls.Add(pnlCustomHeader);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(6);
            Name = "SaveTestReportModalForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Save Test Report";
            pnlCustomHeader.ResumeLayout(false);
            pnlCustomHeader.PerformLayout();
            pnlMainContainer.ResumeLayout(false);
            pnlGenInfoBox.ResumeLayout(false);
            tblGenGrid.ResumeLayout(false);
            tblGenGrid.PerformLayout();
            pnlFooter.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlCustomHeader;
        private Label iconLogo;
        private Label lblHeaderTitle;
        private Button btnClose;
        private Panel pnlMainContainer;
        private Label lblInstruction;
        private Panel pnlGenInfoBox;
        private Label lblGenTitle;
        private TableLayoutPanel tblGenGrid;
        private Label lblGenModelKey;
        private Label lblGenModelVal;
        private Label lblGenEcnKey;
        private Label lblGenEcnVal;
        private Label lblGenTypeKey;
        private Label lblGenTypeVal;
        private Label lblGenRevKey;
        private Label lblGenRevVal;
        private Label lblGenAreaKey;
        private Label lblGenAreaVal;
        private Label lblGenOpKey;
        private TextBox txtGenOpVal;
        private Label lblGenPartKey;
        private Label lblGenPartVal;
        private Label lblGenTimeKey;
        private Label lblGenTimeVal;
        private Label lblGenSnKey;
        private TextBox txtGenSnVal;
        private Label lblGenResKey;
        private Label lblGenResVal;
        private Panel pnlFooter;
        private Button btnConfirmSave;
        private Button btnCancel;
    }
}
