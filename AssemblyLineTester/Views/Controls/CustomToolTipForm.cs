using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AssemblyLineTester.Views.Controls
{
    public class CustomToolTipForm : Form
    {
        private string _text = "";
        private int _tailWidth = 8;
        private int _tailHeight = 16;
        private int _radius = 8;

        public CustomToolTipForm()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            Color bgMatch = Color.FromArgb(244, 245, 247);
            this.BackColor = bgMatch;
            this.TransparencyKey = bgMatch;
            this.DoubleBuffered = true;
            this.StartPosition = FormStartPosition.Manual;
        }

        protected override bool ShowWithoutActivation => true;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT (Click-through)
                return cp;
            }
        }

        public void ShowToolTip(string text, Point controlScreenLocation, int controlHeight)
        {
            _text = text;
            
            using (Font f = new Font("Segoe UI", 10F, FontStyle.Bold))
            {
                Size textSize = TextRenderer.MeasureText(text, f);
                // Padding of 12 left/right, 8 top/bottom, plus tail width
                this.Size = new Size(textSize.Width + 24 + _tailWidth, textSize.Height + 16);
            }
            
            int targetY = controlScreenLocation.Y + (controlHeight - this.Height) / 2;
            
            // X offset: place strictly adjacent + small gap for aesthetics (2px)
            this.Location = new Point(controlScreenLocation.X + 2, targetY);
            
            if (!this.Visible) this.Show();
            this.Invalidate();
        }

        public void HideToolTip()
        {
            if (this.Visible) this.Hide();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.Clear(this.BackColor);

            Color bgColor = Color.FromArgb(250, 204, 21);
            using (SolidBrush brush = new SolidBrush(bgColor))
            using (GraphicsPath path = new GraphicsPath())
            {
                int rectX = _tailWidth;
                int rectY = 0;
                int rectWidth = this.Width - _tailWidth - 1;
                int rectHeight = this.Height - 1;

                // Build rounded rectangle
                path.StartFigure();
                path.AddArc(rectX, rectY, _radius * 2, _radius * 2, 180, 90);
                path.AddArc(rectX + rectWidth - _radius * 2, rectY, _radius * 2, _radius * 2, 270, 90);
                path.AddArc(rectX + rectWidth - _radius * 2, rectY + rectHeight - _radius * 2, _radius * 2, _radius * 2, 0, 90);
                path.AddArc(rectX, rectY + rectHeight - _radius * 2, _radius * 2, _radius * 2, 90, 90);
                path.CloseFigure();
                
                e.Graphics.FillPath(brush, path);
                
                // Draw the left-pointing tail
                Point[] tailPoints = new Point[]
                {
                    new Point(rectX + 1, (this.Height - _tailHeight) / 2),
                    new Point(0, this.Height / 2),
                    new Point(rectX + 1, (this.Height + _tailHeight) / 2)
                };
                e.Graphics.FillPolygon(brush, tailPoints);
                
            }

            // Draw Text
            using (Font f = new Font("Segoe UI", 10F, FontStyle.Bold))
            {
                Rectangle textRect = new Rectangle(_tailWidth, 0, this.Width - _tailWidth, this.Height);
                TextRenderer.DrawText(e.Graphics, _text, f, textRect, Color.Black, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }
    }
}
