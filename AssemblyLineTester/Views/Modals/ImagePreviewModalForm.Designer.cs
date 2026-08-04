using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace AssemblyLineTester.Views.Modals
{
    partial class ImagePreviewModalForm
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
            picPreview = new PictureBox();
            pnlFooter = new Panel();
            btnPrev = new AssemblyLineTester.Views.Controls.RoundedButton();
            lblImageCounter = new Label();
            btnNext = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnCloseModal = new AssemblyLineTester.Views.Controls.RoundedButton();
            pnlCustomHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconLogo).BeginInit();
            pnlBodyContainer.SuspendLayout();
            pnlMainCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picPreview).BeginInit();
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
            pnlCustomHeader.Size = new Size(1198, 72);
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
            lblCustomTitle.Size = new Size(272, 45);
            lblCustomTitle.TabIndex = 5;
            lblCustomTitle.Text = "IMAGE PREVIEW";
            // 
            // iconLogo
            // 
            iconLogo.BackColor = Color.Transparent;
            iconLogo.Dock = DockStyle.Left;
            iconLogo.ForeColor = Color.FromArgb(250, 204, 21);
            iconLogo.IconChar = IconChar.Image;
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
            btnMaximize.Location = new Point(1039, 0);
            btnMaximize.Margin = new Padding(4, 5, 4, 5);
            btnMaximize.Name = "btnMaximize";
            btnMaximize.Size = new Size(146, 72);
            btnMaximize.TabIndex = 4;
            btnMaximize.UseVisualStyleBackColor = false;
            // 
            // pnlBodyContainer
            // 
            pnlBodyContainer.BackColor = Color.FromArgb(244, 245, 247);
            pnlBodyContainer.Controls.Add(pnlMainCard);
            pnlBodyContainer.Controls.Add(pnlFooter);
            pnlBodyContainer.Dock = DockStyle.Fill;
            pnlBodyContainer.Location = new Point(1, 73);
            pnlBodyContainer.Margin = new Padding(4, 5, 4, 5);
            pnlBodyContainer.Name = "pnlBodyContainer";
            pnlBodyContainer.Padding = new Padding(40);
            pnlBodyContainer.Size = new Size(1198, 876);
            pnlBodyContainer.TabIndex = 1;
            // 
            // pnlMainCard
            // 
            pnlMainCard.BackColor = Color.White;
            pnlMainCard.Controls.Add(picPreview);
            pnlMainCard.Dock = DockStyle.Fill;
            pnlMainCard.Location = new Point(40, 40);
            pnlMainCard.Margin = new Padding(4, 5, 4, 5);
            pnlMainCard.Name = "pnlMainCard";
            pnlMainCard.Padding = new Padding(10);
            pnlMainCard.Size = new Size(1118, 696);
            pnlMainCard.TabIndex = 0;
            // 
            // picPreview
            // 
            picPreview.BackColor = Color.FromArgb(248, 250, 252);
            picPreview.Dock = DockStyle.Fill;
            picPreview.Location = new Point(10, 10);
            picPreview.Name = "picPreview";
            picPreview.Size = new Size(1098, 676);
            picPreview.SizeMode = PictureBoxSizeMode.Zoom;
            picPreview.TabIndex = 0;
            picPreview.TabStop = false;
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.Transparent;
            pnlFooter.Controls.Add(btnPrev);
            pnlFooter.Controls.Add(lblImageCounter);
            pnlFooter.Controls.Add(btnNext);
            pnlFooter.Controls.Add(btnCloseModal);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(40, 736);
            pnlFooter.Margin = new Padding(4, 5, 4, 5);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new Padding(0, 25, 0, 0);
            pnlFooter.Size = new Size(1118, 100);
            pnlFooter.TabIndex = 1;
            // 
            // btnPrev
            // 
            btnPrev.BackColor = Color.FromArgb(226, 232, 240);
            btnPrev.Dock = DockStyle.Left;
            btnPrev.FlatStyle = FlatStyle.Flat;
            btnPrev.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnPrev.ForeColor = Color.FromArgb(15, 23, 42);
            btnPrev.HoverForeColor = Color.Empty;
            btnPrev.Location = new Point(0, 25);
            btnPrev.Name = "btnPrev";
            btnPrev.NormalBackColor = Color.FromArgb(226, 232, 240);
            btnPrev.PressedForeColor = Color.Empty;
            btnPrev.Size = new Size(120, 75);
            btnPrev.TabIndex = 0;
            btnPrev.Text = "Previous";
            btnPrev.UseVisualStyleBackColor = false;
            // 
            // lblImageCounter
            // 
            lblImageCounter.Dock = DockStyle.Fill;
            lblImageCounter.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblImageCounter.ForeColor = Color.FromArgb(71, 85, 105);
            lblImageCounter.Location = new Point(0, 25);
            lblImageCounter.Name = "lblImageCounter";
            lblImageCounter.Size = new Size(798, 75);
            lblImageCounter.TabIndex = 1;
            lblImageCounter.Text = "1 of 1";
            lblImageCounter.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnNext
            // 
            btnNext.BackColor = Color.FromArgb(226, 232, 240);
            btnNext.Dock = DockStyle.Right;
            btnNext.FlatStyle = FlatStyle.Flat;
            btnNext.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnNext.ForeColor = Color.FromArgb(15, 23, 42);
            btnNext.HoverForeColor = Color.Empty;
            btnNext.Location = new Point(798, 25);
            btnNext.Name = "btnNext";
            btnNext.NormalBackColor = Color.FromArgb(226, 232, 240);
            btnNext.PressedForeColor = Color.Empty;
            btnNext.Size = new Size(120, 75);
            btnNext.TabIndex = 2;
            btnNext.Text = "Next";
            btnNext.UseVisualStyleBackColor = false;
            // 
            // btnCloseModal
            // 
            btnCloseModal.BackColor = Color.FromArgb(14, 165, 233);
            btnCloseModal.Dock = DockStyle.Right;
            btnCloseModal.FlatStyle = FlatStyle.Flat;
            btnCloseModal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCloseModal.ForeColor = Color.White;
            btnCloseModal.HoverForeColor = Color.Empty;
            btnCloseModal.Location = new Point(918, 25);
            btnCloseModal.Margin = new Padding(4, 5, 4, 5);
            btnCloseModal.Name = "btnCloseModal";
            btnCloseModal.NormalBackColor = Color.FromArgb(14, 165, 233);
            btnCloseModal.PressedForeColor = Color.Empty;
            btnCloseModal.Size = new Size(200, 75);
            btnCloseModal.TabIndex = 3;
            btnCloseModal.Text = "Close";
            btnCloseModal.UseVisualStyleBackColor = false;
            // 
            // ImagePreviewModalForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(34, 40, 49);
            ClientSize = new Size(1200, 950);
            Controls.Add(pnlBodyContainer);
            Controls.Add(pnlCustomHeader);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "ImagePreviewModalForm";
            Padding = new Padding(1);
            StartPosition = FormStartPosition.CenterParent;
            Text = "Image Preview";
            pnlCustomHeader.ResumeLayout(false);
            pnlCustomHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconLogo).EndInit();
            pnlBodyContainer.ResumeLayout(false);
            pnlMainCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picPreview).EndInit();
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
        private System.Windows.Forms.Panel pnlFooter;
        private AssemblyLineTester.Views.Controls.RoundedButton btnCloseModal;
        private AssemblyLineTester.Views.Controls.RoundedButton btnPrev;
        private AssemblyLineTester.Views.Controls.RoundedButton btnNext;
        private System.Windows.Forms.Label lblImageCounter;
        private System.Windows.Forms.PictureBox picPreview;
    }
}
