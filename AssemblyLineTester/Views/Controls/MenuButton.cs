using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AssemblyLineTester.Views.Controls
{
    public class MenuButton : Button
    {
        private bool isActive = false;
        private bool isHovered = false;
        private bool isMinimized = false;
        private string fullText = "";
        private string emojiIcon = "";

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
        [DefaultValue(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsMinimized
        {
            get => isMinimized;
            set => SetMinimized(value);
        }

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

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string EmojiIcon
        {
            get => emojiIcon;
            set
            {
                emojiIcon = value;
                if (isMinimized)
                {
                    Text = value;
                }
            }
        }

        public MenuButton()
        {
            DoubleBuffered = true;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            FlatAppearance.MouseOverBackColor = Color.Transparent;
            FlatAppearance.MouseDownBackColor = Color.Transparent;
            FlatAppearance.CheckedBackColor = Color.Transparent;
            BackColor = Color.Transparent;
            ForeColor = inactiveForeColor;
            Cursor = Cursors.Hand;
            TextAlign = ContentAlignment.MiddleLeft;
            Padding = new Padding(10, 0, 0, 0);
        }

        public void SetMinimized(bool minimized)
        {
            isMinimized = minimized;
            if (minimized)
            {
                Text = emojiIcon;
                TextAlign = ContentAlignment.MiddleCenter;
                Padding = new Padding(0);
            }
            else
            {
                Text = fullText;
                TextAlign = ContentAlignment.MiddleLeft;
                Padding = new Padding(10, 0, 0, 0);
            }
            Invalidate();
        }

        private void UpdateColors()
        {
            if (isActive)
            {
                ForeColor = activeForeColor;
            }
            else
            {
                ForeColor = inactiveForeColor;
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

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // Clear with parent backcolor for transparent blending
            if (Parent != null && Parent.BackColor != Color.Transparent)
            {
                using (Brush brush = new SolidBrush(Parent.BackColor))
                {
                    pevent.Graphics.FillRectangle(brush, ClientRectangle);
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

            // Ensure background is cleared cleanly before drawing shape
            if (Parent != null && Parent.BackColor != Color.Transparent)
            {
                using (Brush brush = new SolidBrush(Parent.BackColor))
                {
                    pevent.Graphics.FillRectangle(brush, ClientRectangle);
                }
            }

            if (isActive || isHovered)
            {
                Color fillColor = isActive ? activeBackColor : hoverBackColor;

                using (GraphicsPath path = new GraphicsPath())
                {
                    if (isMinimized)
                    {
                        // Centered rounded square when minimized
                        float wBox = 64f;
                        float hBox = 64f;
                        if (wBox > Width - 8f) wBox = Width - 8f;
                        if (hBox > Height - 8f) hBox = Height - 8f;
                        float xBox = (Width - wBox) / 2f;
                        float yBox = (Height - hBox) / 2f;
                        float r = 16f;
                        if (r > hBox / 2f) r = hBox / 2f;

                        path.StartFigure();
                        path.AddArc(xBox, yBox, r * 2f, r * 2f, 180f, 90f);
                        path.AddArc(xBox + wBox - (r * 2f), yBox, r * 2f, r * 2f, 270f, 90f);
                        path.AddArc(xBox + wBox - (r * 2f), yBox + hBox - (r * 2f), r * 2f, r * 2f, 0f, 90f);
                        path.AddArc(xBox, yBox + hBox - (r * 2f), r * 2f, r * 2f, 90f, 90f);
                        path.CloseFigure();
                    }
                    else
                    {
                        // Rounded right end when expanded
                        float w = Width - 16f;
                        if (w < 20f) w = Width - 2f;
                        float h = Height - 1f;
                        float r = 20f;
                        if (r > h / 2f) r = h / 2f;

                        path.StartFigure();
                        path.AddLine(0, 0, w - r, 0);
                        path.AddArc(w - (r * 2f), 0, r * 2f, r * 2f, 270f, 90f);
                        path.AddLine(w, r, w, h - r);
                        path.AddArc(w - (r * 2f), h - (r * 2f), r * 2f, r * 2f, 0f, 90f);
                        path.AddLine(w - r, h, 0, h);
                        path.AddLine(0, h, 0, 0);
                        path.CloseFigure();
                    }

                    using (Brush brush = new SolidBrush(fillColor))
                    {
                        pevent.Graphics.FillPath(brush, path);
                    }
                }
            }

            // Draw text directly without calling base.OnPaint so our custom highlight is preserved
            if (isMinimized)
            {
                TextRenderer.DrawText(
                    pevent.Graphics,
                    Text,
                    Font,
                    ClientRectangle,
                    ForeColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine
                );
            }
            else
            {
                Rectangle textRect = new Rectangle(
                    ClientRectangle.Left + Padding.Left,
                    ClientRectangle.Top,
                    ClientRectangle.Width - Padding.Left - 16,
                    ClientRectangle.Height
                );
                TextRenderer.DrawText(
                    pevent.Graphics,
                    Text,
                    Font,
                    textRect,
                    ForeColor,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine
                );
            }
        }
    }
}
