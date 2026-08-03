namespace AssemblyLineTester.Views.Modals
{
    partial class LearnConnectionModalForm
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
            pnlCustomHeader = new Panel();
            lblTitle = new Label();
            iconLogo = new FontAwesome.Sharp.IconPictureBox();
            pnlContent = new Panel();
            chkTwistedPair = new CheckBox();
            chkDiode = new CheckBox();
            chkCapacitor = new CheckBox();
            chkResistor = new CheckBox();
            chkWire = new CheckBox();
            pnlBottom = new Panel();
            btnCancel = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnProceed = new AssemblyLineTester.Views.Controls.RoundedButton();
            pnlCustomHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconLogo).BeginInit();
            pnlContent.SuspendLayout();
            pnlBottom.SuspendLayout();
            SuspendLayout();
            // 
            // pnlCustomHeader
            // 
            pnlCustomHeader.BackColor = Color.FromArgb(30, 41, 59);
            pnlCustomHeader.Controls.Add(lblTitle);
            pnlCustomHeader.Controls.Add(iconLogo);
            pnlCustomHeader.Dock = DockStyle.Top;
            pnlCustomHeader.Location = new Point(0, 0);
            pnlCustomHeader.Margin = new Padding(6);
            pnlCustomHeader.Name = "pnlCustomHeader";
            pnlCustomHeader.Size = new Size(743, 85);
            pnlCustomHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(241, 245, 249);
            lblTitle.Location = new Point(72, 17);
            lblTitle.Margin = new Padding(6, 0, 6, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(433, 47);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Learn Connection Options";
            // 
            // iconLogo
            // 
            iconLogo.BackColor = Color.Transparent;
            iconLogo.ForeColor = Color.FromArgb(250, 204, 21);
            iconLogo.IconChar = FontAwesome.Sharp.IconChar.Chain;
            iconLogo.IconColor = Color.FromArgb(250, 204, 21);
            iconLogo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconLogo.IconSize = 56;
            iconLogo.Location = new Point(15, 6);
            iconLogo.Margin = new Padding(6);
            iconLogo.Name = "iconLogo";
            iconLogo.Size = new Size(56, 73);
            iconLogo.SizeMode = PictureBoxSizeMode.CenterImage;
            iconLogo.TabIndex = 0;
            iconLogo.TabStop = false;
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.White;
            pnlContent.Controls.Add(chkTwistedPair);
            pnlContent.Controls.Add(chkDiode);
            pnlContent.Controls.Add(chkCapacitor);
            pnlContent.Controls.Add(chkResistor);
            pnlContent.Controls.Add(chkWire);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 85);
            pnlContent.Margin = new Padding(6);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(743, 427);
            pnlContent.TabIndex = 1;
            // 
            // chkTwistedPair
            // 
            chkTwistedPair.AutoSize = true;
            chkTwistedPair.BackColor = Color.Transparent;
            chkTwistedPair.Font = new Font("Segoe UI", 12F);
            chkTwistedPair.ForeColor = Color.Black;
            chkTwistedPair.Location = new Point(56, 299);
            chkTwistedPair.Margin = new Padding(6);
            chkTwistedPair.Name = "chkTwistedPair";
            chkTwistedPair.Size = new Size(220, 49);
            chkTwistedPair.TabIndex = 4;
            chkTwistedPair.Text = "Twisted Pair";
            chkTwistedPair.UseVisualStyleBackColor = false;
            // 
            // chkDiode
            // 
            chkDiode.AutoSize = true;
            chkDiode.BackColor = Color.Transparent;
            chkDiode.Font = new Font("Segoe UI", 12F);
            chkDiode.ForeColor = Color.Black;
            chkDiode.Location = new Point(56, 235);
            chkDiode.Margin = new Padding(6);
            chkDiode.Name = "chkDiode";
            chkDiode.Size = new Size(137, 49);
            chkDiode.TabIndex = 3;
            chkDiode.Text = "Diode";
            chkDiode.UseVisualStyleBackColor = false;
            // 
            // chkCapacitor
            // 
            chkCapacitor.AutoSize = true;
            chkCapacitor.BackColor = Color.Transparent;
            chkCapacitor.Font = new Font("Segoe UI", 12F);
            chkCapacitor.ForeColor = Color.Black;
            chkCapacitor.Location = new Point(56, 171);
            chkCapacitor.Margin = new Padding(6);
            chkCapacitor.Name = "chkCapacitor";
            chkCapacitor.Size = new Size(187, 49);
            chkCapacitor.TabIndex = 2;
            chkCapacitor.Text = "Capacitor";
            chkCapacitor.UseVisualStyleBackColor = false;
            // 
            // chkResistor
            // 
            chkResistor.AutoSize = true;
            chkResistor.BackColor = Color.Transparent;
            chkResistor.Font = new Font("Segoe UI", 12F);
            chkResistor.ForeColor = Color.Black;
            chkResistor.Location = new Point(56, 107);
            chkResistor.Margin = new Padding(6);
            chkResistor.Name = "chkResistor";
            chkResistor.Size = new Size(164, 49);
            chkResistor.TabIndex = 1;
            chkResistor.Text = "Resistor";
            chkResistor.UseVisualStyleBackColor = false;
            // 
            // chkWire
            // 
            chkWire.AutoSize = true;
            chkWire.BackColor = Color.Transparent;
            chkWire.Checked = true;
            chkWire.CheckState = CheckState.Checked;
            chkWire.Font = new Font("Segoe UI", 12F);
            chkWire.ForeColor = Color.Black;
            chkWire.Location = new Point(56, 43);
            chkWire.Margin = new Padding(6);
            chkWire.Name = "chkWire";
            chkWire.Size = new Size(118, 49);
            chkWire.TabIndex = 0;
            chkWire.Text = "Wire";
            chkWire.UseVisualStyleBackColor = false;
            // 
            // pnlBottom
            // 
            pnlBottom.BackColor = Color.FromArgb(34, 40, 49);
            pnlBottom.Controls.Add(btnCancel);
            pnlBottom.Controls.Add(btnProceed);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.Location = new Point(0, 512);
            pnlBottom.Margin = new Padding(6);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Size = new Size(743, 128);
            pnlBottom.TabIndex = 2;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.BackColor = Color.FromArgb(241, 245, 249);
            btnCancel.BorderRadius = 6;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancel.ForeColor = Color.FromArgb(71, 85, 105);
            btnCancel.HoverBackColor = Color.FromArgb(203, 213, 225);
            btnCancel.HoverForeColor = Color.Empty;
            btnCancel.Location = new Point(357, 26);
            btnCancel.Margin = new Padding(6);
            btnCancel.Name = "btnCancel";
            btnCancel.NormalBackColor = Color.FromArgb(241, 245, 249);
            btnCancel.PressedBackColor = Color.FromArgb(203, 213, 225);
            btnCancel.PressedForeColor = Color.Empty;
            btnCancel.Size = new Size(167, 68);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnProceed
            // 
            btnProceed.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnProceed.BackColor = Color.FromArgb(14, 165, 233);
            btnProceed.BorderRadius = 6;
            btnProceed.Cursor = Cursors.Hand;
            btnProceed.FlatAppearance.BorderSize = 0;
            btnProceed.FlatStyle = FlatStyle.Flat;
            btnProceed.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnProceed.ForeColor = Color.Black;
            btnProceed.HoverBackColor = Color.FromArgb(2, 132, 199);
            btnProceed.HoverForeColor = Color.Empty;
            btnProceed.Location = new Point(535, 26);
            btnProceed.Margin = new Padding(6);
            btnProceed.Name = "btnProceed";
            btnProceed.PressedForeColor = Color.Empty;
            btnProceed.Size = new Size(167, 68);
            btnProceed.TabIndex = 0;
            btnProceed.Text = "Proceed";
            btnProceed.UseVisualStyleBackColor = false;
            // 
            // LearnConnectionModalForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(743, 640);
            Controls.Add(pnlContent);
            Controls.Add(pnlBottom);
            Controls.Add(pnlCustomHeader);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(6);
            Name = "LearnConnectionModalForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Learn Connection Options";
            pnlCustomHeader.ResumeLayout(false);
            pnlCustomHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconLogo).EndInit();
            pnlContent.ResumeLayout(false);
            pnlContent.PerformLayout();
            pnlBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlCustomHeader;
        private System.Windows.Forms.Label lblTitle;
        private FontAwesome.Sharp.IconPictureBox iconLogo;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.CheckBox chkTwistedPair;
        private System.Windows.Forms.CheckBox chkDiode;
        private System.Windows.Forms.CheckBox chkCapacitor;
        private System.Windows.Forms.CheckBox chkResistor;
        private System.Windows.Forms.CheckBox chkWire;
        private System.Windows.Forms.Panel pnlBottom;
        private AssemblyLineTester.Views.Controls.RoundedButton btnCancel;
        private AssemblyLineTester.Views.Controls.RoundedButton btnProceed;
    }
}
