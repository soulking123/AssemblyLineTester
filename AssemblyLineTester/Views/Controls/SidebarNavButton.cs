using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace AssemblyLineTester.Views.Controls
{
    public class SidebarNavButton : IconButton
    {
        private bool isActive = false;
        private bool isHovered = false;
        private int borderRadius = 16;
        private Color activeBackColor = Color.FromArgb(250, 204, 21);
        private Color activeForeColor = Color.FromArgb(15, 23, 42);
        private Color hoverBackColor = Color.FromArgb(50, 60, 75);
        private Color inactiveForeColor = Color.White;

        [Category("Appearance")]
        [DefaultValue(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsActive
        {
            get => isActive;
            set
            {
                isActive = value;
                UpdateColors();
                Invalidate();
            }
        }

        [Category("Appearance")]
        [DefaultValue(16)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int BorderRadius
        {
            get => borderRadius;
            set
            {
                borderRadius = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color ActiveBackColor
        {
            get => activeBackColor;
            set { activeBackColor = value; UpdateColors(); Invalidate(); }
        }

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color ActiveForeColor
        {
            get => activeForeColor;
            set { activeForeColor = value; UpdateColors(); Invalidate(); }
        }

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color HoverBackColor
        {
            get => hoverBackColor;
            set { hoverBackColor = value; Invalidate(); }
        }

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color InactiveForeColor
        {
            get => inactiveForeColor;
            set { inactiveForeColor = value; UpdateColors(); Invalidate(); }
        }

        public SidebarNavButton()
        {
            DoubleBuffered = true;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            BackColor = Color.Transparent;
            ForeColor = inactiveForeColor;
            IconColor = inactiveForeColor;
            Cursor = Cursors.Hand;
            TextImageRelation = TextImageRelation.ImageBeforeText;
            ImageAlign = ContentAlignment.MiddleLeft;
            TextAlign = ContentAlignment.MiddleLeft;
            Padding = new Padding(16, 0, 0, 0);
        }

        private string fullText = "";
        private bool isMinimized = false;

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string FullText
        {
            get => fullText;
            set
            {
                fullText = value;
                if (!isMinimized)
                {
                    Text = value;
                }
            }
        }

        public void SetMinimized(bool minimized)
        {
            isMinimized = minimized;
            if (minimized)
            {
                Text = "";
                ImageAlign = ContentAlignment.MiddleCenter;
                Padding = new Padding(0);
                Margin = new Padding(10, 6, 10, 6);
            }
            else
            {
                Text = fullText;
                ImageAlign = ContentAlignment.MiddleLeft;
                Padding = new Padding(20, 0, 0, 0);
                Margin = new Padding(16, 6, 16, 6);
            }
            Invalidate();
        }

        private void UpdateColors()
        {
            if (isActive)
            {
                ForeColor = activeForeColor;
                IconColor = activeForeColor;
            }
            else
            {
                ForeColor = inactiveForeColor;
                IconColor = inactiveForeColor;
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            isHovered = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            isHovered = false;
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

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            if (Parent != null && Parent.BackColor != Color.Transparent)
            {
                using (Brush parentBrush = new SolidBrush(Parent.BackColor))
                {
                    pevent.Graphics.FillRectangle(parentBrush, ClientRectangle);
                }
            }
            else
            {
                base.OnPaintBackground(pevent);
            }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            pevent.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            if (Parent != null && Parent.BackColor != Color.Transparent)
            {
                using (Brush parentBrush = new SolidBrush(Parent.BackColor))
                {
                    pevent.Graphics.FillRectangle(parentBrush, 0, 0, Width, Height);
                }
            }
            else
            {
                pevent.Graphics.Clear(Color.Transparent);
            }

            RectangleF rectSurface = new RectangleF(0.5f, 0.5f, Width - 1f, Height - 1f);

            if (isActive)
            {
                using (GraphicsPath pathSurface = GetFigurePath(rectSurface, borderRadius))
                using (Brush activeBrush = new SolidBrush(activeBackColor))
                {
                    pevent.Graphics.FillPath(activeBrush, pathSurface);
                }
            }
            else if (isHovered)
            {
                using (GraphicsPath pathSurface = GetFigurePath(rectSurface, borderRadius))
                using (Brush hoverBrush = new SolidBrush(hoverBackColor))
                {
                    pevent.Graphics.FillPath(hoverBrush, pathSurface);
                }
            }

            base.OnPaint(pevent);
        }
    }
}
