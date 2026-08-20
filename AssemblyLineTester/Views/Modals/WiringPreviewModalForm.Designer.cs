using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace AssemblyLineTester.Views.Modals
{
    partial class WiringPreviewModalForm
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
            lblCustomTitle = new Label();
            iconLogo = new IconPictureBox();
            btnMaximize = new IconButton();
            pnlBodyContainer = new Panel();
            pnlMainCard = new Panel();
            pnlPreviewScroll = new Panel();
            picPreview = new AssemblyLineTester.Views.Controls.SchematicDiagramControl();
            pnlHeaderTop = new Panel();
            lblHeaderTop = new Label();
            pnlFooter = new Panel();
            lblInstruction = new Label();
            btnCloseModal = new AssemblyLineTester.Views.Controls.RoundedButton();
            pnlCustomHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconLogo).BeginInit();
            pnlBodyContainer.SuspendLayout();
            pnlMainCard.SuspendLayout();
            pnlPreviewScroll.SuspendLayout();
            pnlHeaderTop.SuspendLayout();
            pnlFooter.SuspendLayout();
            SuspendLayout();
            // 
            // pnlCustomHeader
            // 
            pnlCustomHeader.BackColor = Color.FromArgb(34, 40, 49);
            pnlCustomHeader.Controls.Add(lblCustomTitle);
            pnlCustomHeader.Controls.Add(iconLogo);
            pnlCustomHeader.Controls.Add(btnMaximize);
            pnlCustomHeader.Dock = DockStyle.Top;
            pnlCustomHeader.Location = new Point(1, 1);
            pnlCustomHeader.Margin = new Padding(0);
            pnlCustomHeader.Name = "pnlCustomHeader";
            pnlCustomHeader.Padding = new Padding(20, 0, 13, 0);
            pnlCustomHeader.Size = new Size(1818, 72);
            pnlCustomHeader.TabIndex = 0;
            // 
            // lblCustomTitle
            // 
            lblCustomTitle.AutoSize = true;
            lblCustomTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCustomTitle.ForeColor = SystemColors.ControlLightLight;
            lblCustomTitle.Location = new Point(84, 15);
            lblCustomTitle.Margin = new Padding(4, 0, 4, 0);
            lblCustomTitle.Name = "lblCustomTitle";
            lblCustomTitle.Size = new Size(453, 45);
            lblCustomTitle.TabIndex = 5;
            lblCustomTitle.Text = "WIRING DIAGRAM PREVIEW";
            // 
            // iconLogo
            // 
            iconLogo.BackColor = Color.Transparent;
            iconLogo.Dock = DockStyle.Left;
            iconLogo.ForeColor = Color.FromArgb(250, 204, 21);
            iconLogo.IconChar = IconChar.NetworkWired;
            iconLogo.IconColor = Color.FromArgb(250, 204, 21);
            iconLogo.IconFont = IconFont.Auto;
            iconLogo.IconSize = 58;
            iconLogo.Location = new Point(20, 0);
            iconLogo.Margin = new Padding(0);
            iconLogo.Name = "iconLogo";
            iconLogo.Size = new Size(58, 72);
            iconLogo.SizeMode = PictureBoxSizeMode.CenterImage;
            iconLogo.TabIndex = 0;
            iconLogo.TabStop = false;
            // 
            // btnMaximize
            // 
            btnMaximize.BackColor = Color.Transparent;
            btnMaximize.Dock = DockStyle.Right;
            btnMaximize.FlatAppearance.BorderSize = 0;
            btnMaximize.FlatStyle = FlatStyle.Flat;
            btnMaximize.IconChar = IconChar.WindowMaximize;
            btnMaximize.IconColor = Color.FromArgb(203, 213, 225);
            btnMaximize.IconFont = IconFont.Auto;
            btnMaximize.IconSize = 20;
            btnMaximize.Location = new Point(1659, 0);
            btnMaximize.Margin = new Padding(0);
            btnMaximize.Name = "btnMaximize";
            btnMaximize.Size = new Size(73, 72);
            btnMaximize.TabIndex = 3;
            btnMaximize.UseVisualStyleBackColor = false;
            // 
            // pnlBodyContainer
            // 
            pnlBodyContainer.BackColor = Color.FromArgb(241, 245, 249);
            pnlBodyContainer.Controls.Add(pnlMainCard);
            pnlBodyContainer.Controls.Add(pnlFooter);
            pnlBodyContainer.Dock = DockStyle.Fill;
            pnlBodyContainer.Location = new Point(1, 73);
            pnlBodyContainer.Margin = new Padding(0);
            pnlBodyContainer.Name = "pnlBodyContainer";
            pnlBodyContainer.Padding = new Padding(26, 26, 26, 26);
            pnlBodyContainer.Size = new Size(1818, 1078);
            pnlBodyContainer.TabIndex = 1;
            // 
            // pnlMainCard
            // 
            pnlMainCard.BackColor = Color.White;
            pnlMainCard.Controls.Add(pnlPreviewScroll);
            pnlMainCard.Controls.Add(pnlHeaderTop);
            pnlMainCard.Dock = DockStyle.Fill;
            pnlMainCard.Location = new Point(26, 26);
            pnlMainCard.Margin = new Padding(4, 4, 4, 4);
            pnlMainCard.Name = "pnlMainCard";
            pnlMainCard.Size = new Size(1766, 936);
            pnlMainCard.TabIndex = 0;
            // 
            // pnlPreviewScroll
            // 
            pnlPreviewScroll.AutoScroll = true;
            pnlPreviewScroll.BackColor = Color.FromArgb(248, 250, 252);
            pnlPreviewScroll.Controls.Add(picPreview);
            pnlPreviewScroll.Dock = DockStyle.Fill;
            pnlPreviewScroll.Location = new Point(0, 64);
            pnlPreviewScroll.Margin = new Padding(4, 4, 4, 4);
            pnlPreviewScroll.Name = "pnlPreviewScroll";
            pnlPreviewScroll.Size = new Size(1766, 872);
            pnlPreviewScroll.TabIndex = 1;
            // 
            // picPreview
            // 
            picPreview.BackColor = Color.FromArgb(248, 250, 252);
            picPreview.Location = new Point(0, 0);
            picPreview.Margin = new Padding(4, 4, 4, 4);
            picPreview.Name = "picPreview";
            picPreview.Size = new Size(1765, 1408);
            picPreview.TabIndex = 2;
            // 
            // pnlHeaderTop
            // 
            pnlHeaderTop.BackColor = Color.White;
            pnlHeaderTop.Controls.Add(lblHeaderTop);
            pnlHeaderTop.Dock = DockStyle.Top;
            pnlHeaderTop.Location = new Point(0, 0);
            pnlHeaderTop.Margin = new Padding(4, 4, 4, 4);
            pnlHeaderTop.Name = "pnlHeaderTop";
            pnlHeaderTop.Size = new Size(1766, 64);
            pnlHeaderTop.TabIndex = 0;
            // 
            // lblHeaderTop
            // 
            lblHeaderTop.Dock = DockStyle.Fill;
            lblHeaderTop.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblHeaderTop.ForeColor = Color.FromArgb(30, 41, 59);
            lblHeaderTop.Location = new Point(0, 0);
            lblHeaderTop.Margin = new Padding(4, 0, 4, 0);
            lblHeaderTop.Name = "lblHeaderTop";
            lblHeaderTop.Padding = new Padding(20, 0, 0, 0);
            lblHeaderTop.Size = new Size(1766, 64);
            lblHeaderTop.TabIndex = 0;
            lblHeaderTop.Text = "⚡ Interactive Schematic: Harness Connector (J1) ➔ Test Station (J3)";
            lblHeaderTop.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlFooter
            // 
            pnlFooter.Controls.Add(lblInstruction);
            pnlFooter.Controls.Add(btnCloseModal);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(26, 962);
            pnlFooter.Margin = new Padding(4, 4, 4, 4);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Size = new Size(1766, 90);
            pnlFooter.TabIndex = 1;
            // 
            // lblInstruction
            // 
            lblInstruction.Dock = DockStyle.Left;
            lblInstruction.Font = new Font("Segoe UI", 9.5F);
            lblInstruction.ForeColor = Color.FromArgb(100, 116, 139);
            lblInstruction.Location = new Point(0, 0);
            lblInstruction.Margin = new Padding(4, 0, 4, 0);
            lblInstruction.Name = "lblInstruction";
            lblInstruction.Size = new Size(780, 90);
            lblInstruction.TabIndex = 1;
            lblInstruction.Text = "💡 Tip: Click on any connection line or pin node to highlight and trace its circuit route.";
            lblInstruction.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnCloseModal
            // 
            btnCloseModal.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCloseModal.BackColor = Color.FromArgb(100, 116, 139);
            btnCloseModal.BorderRadius = 10;
            btnCloseModal.FlatAppearance.BorderSize = 0;
            btnCloseModal.FlatStyle = FlatStyle.Flat;
            btnCloseModal.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCloseModal.ForeColor = Color.White;
            btnCloseModal.HoverBackColor = Color.FromArgb(71, 85, 105);
            btnCloseModal.Location = new Point(1584, 19);
            btnCloseModal.Margin = new Padding(4, 4, 4, 4);
            btnCloseModal.Name = "btnCloseModal";
            btnCloseModal.NormalBackColor = Color.FromArgb(100, 116, 139);
            btnCloseModal.PressedBackColor = Color.FromArgb(51, 65, 85);
            btnCloseModal.Size = new Size(182, 51);
            btnCloseModal.TabIndex = 0;
            btnCloseModal.Text = "Close Preview";
            btnCloseModal.UseVisualStyleBackColor = false;
            // 
            // WiringPreviewModalForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(203, 213, 225);
            ClientSize = new Size(1820, 1152);
            Controls.Add(pnlBodyContainer);
            Controls.Add(pnlCustomHeader);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 4, 4, 4);
            MinimumSize = new Size(1235, 768);
            Name = "WiringPreviewModalForm";
            Padding = new Padding(1);
            StartPosition = FormStartPosition.CenterParent;
            Text = "Wiring Diagram Preview";
            pnlCustomHeader.ResumeLayout(false);
            pnlCustomHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconLogo).EndInit();
            pnlBodyContainer.ResumeLayout(false);
            pnlMainCard.ResumeLayout(false);
            pnlPreviewScroll.ResumeLayout(false);
            pnlHeaderTop.ResumeLayout(false);
            pnlFooter.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlCustomHeader;
        private System.Windows.Forms.Label lblCustomTitle;
        private FontAwesome.Sharp.IconPictureBox iconLogo;
        private FontAwesome.Sharp.IconButton btnMaximize;
        private System.Windows.Forms.Panel pnlBodyContainer;
        private System.Windows.Forms.Panel pnlMainCard;
        private System.Windows.Forms.Panel pnlPreviewScroll;
        private AssemblyLineTester.Views.Controls.SchematicDiagramControl picPreview;
        private System.Windows.Forms.Panel pnlHeaderTop;
        private System.Windows.Forms.Label lblHeaderTop;
        private System.Windows.Forms.Panel pnlFooter;
        private AssemblyLineTester.Views.Controls.RoundedButton btnCloseModal;
        private System.Windows.Forms.Label lblInstruction;
    }
}
