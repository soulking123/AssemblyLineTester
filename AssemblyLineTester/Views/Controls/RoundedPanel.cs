using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AssemblyLineTester.Views.Controls
{
    public class RoundedPanel : Panel
    {
        private int borderRadius = 24;

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(24)]
        public int BorderRadius
        {
            get => borderRadius;
            set
            {
                borderRadius = value;
                Invalidate();
            }
        }

        private Color borderColor = Color.Transparent;
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(typeof(Color), "Transparent")]
        public Color BorderColor
        {
            get => borderColor;
            set
            {
                borderColor = value;
                Invalidate();
            }
        }

        private float borderSize = 0f;
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(0f)]
        public float BorderSize
        {
            get => borderSize;
            set
            {
                borderSize = value;
                Invalidate();
            }
        }

        public RoundedPanel()
        {
            DoubleBuffered = true;
            BackColor = Color.White;
        }

        protected override void OnResize(EventArgs eventArgs)
        {
            base.OnResize(eventArgs);
            Invalidate();
        }

        private GraphicsPath GetFigurePath(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            if (radius <= 0)
            {
                path.AddRectangle(rect);
                return path;
            }

            float d = radius * 2f;
            if (d > rect.Height) d = rect.Height;
            if (d > rect.Width) d = rect.Width;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
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
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            RectangleF rectSurface = new RectangleF(0.5f, 0.5f, Width - 1f, Height - 1f);

            // Clear background with parent's backcolor for smooth anti-aliased rounded corners
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

            // Draw rounded card surface
            using (GraphicsPath pathSurface = GetFigurePath(rectSurface, borderRadius))
            using (Brush surfaceBrush = new SolidBrush(BackColor))
            {
                e.Graphics.FillPath(surfaceBrush, pathSurface);
                
                if (borderSize > 0 && borderColor != Color.Transparent)
                {
                    using (Pen borderPen = new Pen(borderColor, borderSize))
                    {
                        borderPen.Alignment = PenAlignment.Inset;
                        e.Graphics.DrawPath(borderPen, pathSurface);
                    }
                }
            }
        }
    }
}
