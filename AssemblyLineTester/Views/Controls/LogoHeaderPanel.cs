using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AssemblyLineTester.Views.Controls
{
    public class LogoHeaderPanel : Panel
    {
        private Color tabColor = Color.FromArgb(250, 204, 21);
        private float slopeOffset = 45f;
        private float cornerRadius = 24f;

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color TabColor
        {
            get => tabColor;
            set { tabColor = value; Invalidate(); }
        }

        [Category("Appearance")]
        [DefaultValue(45f)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float SlopeOffset
        {
            get => slopeOffset;
            set { slopeOffset = value; Invalidate(); }
        }

        [Category("Appearance")]
        [DefaultValue(24f)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float CornerRadius
        {
            get => cornerRadius;
            set { cornerRadius = value; Invalidate(); }
        }

        private bool isMinimized = false;

        [Category("Appearance")]
        [DefaultValue(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsMinimized
        {
            get => isMinimized;
            set { isMinimized = value; Invalidate(); }
        }

        public LogoHeaderPanel()
        {
            DoubleBuffered = true;
            BackColor = Color.Transparent;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (Parent != null && Parent.BackColor != Color.Transparent)
            {
                using (Brush parentBrush = new SolidBrush(Parent.BackColor))
                {
                    e.Graphics.FillRectangle(parentBrush, ClientRectangle);
                }
            }
            else
            {
                base.OnPaintBackground(e);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            if (Parent != null && Parent.BackColor != Color.Transparent)
            {
                using (Brush parentBrush = new SolidBrush(Parent.BackColor))
                {
                    e.Graphics.FillRectangle(parentBrush, 0, 0, Width, Height);
                }
            }
            else
            {
                e.Graphics.Clear(Color.Transparent);
            }

            if (Width <= 0 || Height <= 0) return;

            using (GraphicsPath path = new GraphicsPath())
            {
                float w = Width - 1f;
                float h = Height - 1f;
                float slope = slopeOffset;
                float r = cornerRadius;
                if (r > h / 2f) r = h / 2f;
                if (r > w / 2f) r = w / 2f;

                if (isMinimized)
                {
                    path.StartFigure();
                    path.AddLine(0, 0, w, 0);
                    path.AddLine(w, 0, w, h - r);
                    path.AddArc(w - (r * 2f), h - (r * 2f), r * 2f, r * 2f, 0f, 90f);
                    path.AddLine(w - r, h, r, h);
                    path.AddArc(0, h - (r * 2f), r * 2f, r * 2f, 90f, 90f);
                    path.AddLine(0, h - r, 0, 0);
                    path.CloseFigure();
                }
                else
                {
                    if (slope > w / 2f) slope = w / 2f;

                    path.StartFigure();
                    path.AddLine(0, 0, w - slope, 0);

                    path.AddLine(w - slope, 0, w - slope - (r * 0.5f), h - r);

                    path.AddArc(w - slope - (r * 1.5f), h - (r * 2f), r * 2f, r * 2f, 25f, 65f);

                    path.AddLine(w - slope - (r * 1.5f), h, 0, h);

                    path.AddLine(0, h, 0, 0);
                    path.CloseFigure();
                }

                using (Brush tabBrush = new SolidBrush(tabColor))
                {
                    e.Graphics.FillPath(tabBrush, path);
                }
            }

            base.OnPaint(e);
        }
    }
}
