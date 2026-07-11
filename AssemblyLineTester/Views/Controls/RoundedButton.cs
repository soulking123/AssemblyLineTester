using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AssemblyLineTester.Views.Controls
{
    public class RoundedButton : Button
    {
        private int borderRadius = 24;
        private Color normalBackColor = Color.FromArgb(250, 204, 21);
        private Color hoverBackColor = Color.FromArgb(254, 216, 53);
        private Color pressedBackColor = Color.FromArgb(234, 186, 10);
        private bool isHovered = false;
        private bool isPressed = false;

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

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color NormalBackColor
        {
            get => normalBackColor;
            set
            {
                normalBackColor = value;
                BackColor = value;
                Invalidate();
            }
        }
        private bool ShouldSerializeNormalBackColor() => normalBackColor != Color.FromArgb(250, 204, 21);
        private void ResetNormalBackColor() => NormalBackColor = Color.FromArgb(250, 204, 21);

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color HoverBackColor
        {
            get => hoverBackColor;
            set { hoverBackColor = value; Invalidate(); }
        }
        private bool ShouldSerializeHoverBackColor() => hoverBackColor != Color.FromArgb(254, 216, 53);
        private void ResetHoverBackColor() => HoverBackColor = Color.FromArgb(254, 216, 53);

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color PressedBackColor
        {
            get => pressedBackColor;
            set { pressedBackColor = value; Invalidate(); }
        }
        private bool ShouldSerializePressedBackColor() => pressedBackColor != Color.FromArgb(234, 186, 10);
        private void ResetPressedBackColor() => PressedBackColor = Color.FromArgb(234, 186, 10);

        public RoundedButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Size = new Size(200, 48);
            BackColor = normalBackColor;
            ForeColor = Color.FromArgb(15, 23, 42);
            Cursor = Cursors.Hand;
            DoubleBuffered = true;
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

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            if (mevent.Button == MouseButtons.Left)
            {
                isPressed = true;
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            if (mevent.Button == MouseButtons.Left)
            {
                isPressed = false;
                Invalidate();
            }
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

            RectangleF rectSurface = new RectangleF(0.5f, 0.5f, Width - 1f, Height - 1f);

            Color currentColor = normalBackColor;
            if (isPressed)
            {
                currentColor = pressedBackColor;
            }
            else if (isHovered)
            {
                currentColor = hoverBackColor;
            }
            else if (!Enabled)
            {
                currentColor = Color.Gray;
            }

            // Clear background with parent's backcolor for smooth anti-aliased rounded corners
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

            // Draw rounded pill surface
            using (GraphicsPath pathSurface = GetFigurePath(rectSurface, borderRadius))
            using (Brush surfaceBrush = new SolidBrush(currentColor))
            {
                pevent.Graphics.FillPath(surfaceBrush, pathSurface);
            }

            // Draw button text
            TextRenderer.DrawText(
                pevent.Graphics,
                Text,
                Font,
                ClientRectangle,
                ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis
            );
        }
    }
}
