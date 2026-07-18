using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AssemblyLineTester.Helpers
{
    public static class UIStyleHelper
    {
        public static readonly Font Font85Regular = new("Segoe UI", 8.5F, FontStyle.Regular);
        public static readonly Font Font85Bold = new("Segoe UI", 8.5F, FontStyle.Bold);
        public static readonly Font Font85 = Font85Regular;
        public static readonly Font Font9Regular = new("Segoe UI", 9F, FontStyle.Regular);
        public static readonly Font Font9Bold = new("Segoe UI", 9F, FontStyle.Bold);
        public static readonly Font Font9 = Font9Regular;
        public static readonly Font Font95Regular = new("Segoe UI", 9.5F, FontStyle.Regular);
        public static readonly Font Font95Bold = new("Segoe UI", 9.5F, FontStyle.Bold);
        public static readonly Font Font95 = Font95Regular;
        public static readonly Font Font10Bold = new("Segoe UI", 10F, FontStyle.Bold);
        public static readonly Font Font105Regular = new("Segoe UI", 10.5F, FontStyle.Regular);
        public static readonly Font Font105Bold = new("Segoe UI", 10.5F, FontStyle.Bold);
        public static readonly Font Font11Regular = new("Segoe UI", 11F, FontStyle.Regular);
        public static readonly Font Font11Bold = new("Segoe UI", 11F, FontStyle.Bold);
        public static readonly Font Font12Bold = new("Segoe UI", 12F, FontStyle.Bold);
        public static readonly Font Font13Bold = new("Segoe UI", 13F, FontStyle.Bold);
        public static readonly Font Font16Bold = new("Segoe UI", 16F, FontStyle.Bold);
        public static readonly Font Font18Bold = new("Segoe UI", 18F, FontStyle.Bold);
        public static readonly Font Font22Bold = new("Segoe UI", 22F, FontStyle.Bold);
        public static readonly Font Font48Regular = new("Segoe UI", 48F, FontStyle.Regular);

        public static GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new();
            int d = radius * 2;
            if (rect.Width <= 0 || rect.Height <= 0)
            {
                path.AddRectangle(rect);
                return path;
            }
            if (d > rect.Width) d = rect.Width;
            if (d > rect.Height) d = rect.Height;

            if (d <= 0)
            {
                path.AddRectangle(rect);
                return path;
            }

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        public static GraphicsPath GetRoundedRectPath(Rectangle bounds, int radius)
        {
            return GetRoundedPath(bounds, radius);
        }

        public static void StyleDataGridView(DataGridView grid)
        {
            if (grid == null) return;

            // Enable DoubleBuffered via reflection to prevent visual flicker during updates/scrolling
            typeof(DataGridView).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                                ?.SetValue(grid, true);

            grid.BorderStyle = BorderStyle.FixedSingle;
            grid.BackgroundColor = Color.White;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            grid.GridColor = Color.FromArgb(203, 213, 225);
            grid.RowHeadersVisible = false;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.ReadOnly = true;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.EnableHeadersVisualStyles = false;

            // Header Style
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(71, 85, 105);
            grid.ColumnHeadersDefaultCellStyle.Font = Font95Bold;
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(248, 250, 252);
            grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(71, 85, 105);
            grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 4, 10, 4);
            grid.ColumnHeadersHeight = 46;

            // Row Style
            grid.DefaultCellStyle.BackColor = Color.White;
            grid.DefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(241, 245, 249);
            grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);
            grid.DefaultCellStyle.Padding = new Padding(8, 4, 8, 4);
            grid.RowTemplate.Height = 42;

            foreach (DataGridViewRow row in grid.Rows)
            {
                row.Height = grid.RowTemplate.Height;
            }
        }

        public static void SetupCardRoundedEdges(Control card, int radius, Color borderColor, float borderWidth = 2.5f)
        {
            if (card == null) return;
            if (card.Tag is string tag && tag.Contains("STYLED_CARD")) return;
            card.Tag = (card.Tag?.ToString() ?? "") + " STYLED_CARD";

            // Enable double buffering on the card control
            typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                           ?.SetValue(card, true);

            if (card is TableLayoutPanel || card.Padding.Left < 4 || card.Padding.Top < 4 || card.Padding.Right < 4 || card.Padding.Bottom < 4)
            {
                card.Padding = new Padding(
                    Math.Max(card.Padding.Left, 4),
                    Math.Max(card.Padding.Top, 4),
                    Math.Max(card.Padding.Right, 4),
                    Math.Max(card.Padding.Bottom, 4)
                );
            }

            card.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                Color parentBg = card.Parent?.BackColor != null && card.Parent.BackColor != Color.Transparent
                    ? card.Parent.BackColor
                    : Color.FromArgb(244, 245, 247);

                using (var parentBrush = new SolidBrush(parentBg))
                {
                    e.Graphics.FillRectangle(parentBrush, 0, 0, card.Width, card.Height);
                }

                var rectOuter = new Rectangle(0, 0, card.Width, card.Height);
                using var pathOuter = GetRoundedPath(rectOuter, radius);
                using (var cardBrush = new SolidBrush(card.BackColor))
                {
                    e.Graphics.FillPath(cardBrush, pathOuter);
                }

                if (borderColor != Color.Transparent && borderWidth > 0)
                {
                    var rectBorder = new Rectangle(1, 1, Math.Max(1, card.Width - 3), Math.Max(1, card.Height - 3));
                    using var pathBorder = GetRoundedPath(rectBorder, radius);
                    using var pen = new Pen(borderColor, borderWidth);
                    e.Graphics.DrawPath(pen, pathBorder);
                }
            };

            Size lastSize = Size.Empty;
            card.Resize += (s, e) =>
            {
                if (card.Width > 0 && card.Height > 0 && card.Size != lastSize)
                {
                    lastSize = card.Size;
                    var oldRegion = card.Region;
                    card.Region = new Region(GetRoundedPath(new Rectangle(0, 0, card.Width, card.Height), radius));
                    oldRegion?.Dispose();
                    card.Invalidate();
                }
            };

            if (card.Width > 0 && card.Height > 0)
            {
                lastSize = card.Size;
                card.Region = new Region(GetRoundedPath(new Rectangle(0, 0, card.Width, card.Height), radius));
            }
        }

        public static void SetupButtonRoundedEdges(Button btn, int radius, Color normColor, Color hoverColor, Color pressColor, Color borderColor)
        {
            if (btn == null) return;
            if (btn.Tag is string tag && tag.Contains("STYLED_BTN")) return;
            btn.Tag = (btn.Tag?.ToString() ?? "") + " STYLED_BTN";

            typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                           ?.SetValue(btn, true);

            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = normColor;
            btn.Cursor = Cursors.Hand;

            btn.MouseEnter += (s, e) => { btn.BackColor = hoverColor; btn.Invalidate(); };
            btn.MouseLeave += (s, e) => { btn.BackColor = normColor; btn.Invalidate(); };
            btn.MouseDown += (s, e) => { btn.BackColor = pressColor; btn.Invalidate(); };
            btn.MouseUp += (s, e) => { btn.BackColor = hoverColor; btn.Invalidate(); };

            btn.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                Color parentBg = btn.Parent?.BackColor != null && btn.Parent.BackColor != Color.Transparent
                    ? btn.Parent.BackColor
                    : Color.White;

                using (var parentBrush = new SolidBrush(parentBg))
                {
                    e.Graphics.FillRectangle(parentBrush, 0, 0, btn.Width, btn.Height);
                }

                var rectOuter = new Rectangle(0, 0, btn.Width, btn.Height);
                using var pathOuter = GetRoundedPath(rectOuter, radius);
                using (var brush = new SolidBrush(btn.BackColor))
                {
                    e.Graphics.FillPath(brush, pathOuter);
                }

                if (borderColor != Color.Transparent)
                {
                    var rectBorder = new Rectangle(1, 1, Math.Max(1, btn.Width - 3), Math.Max(1, btn.Height - 3));
                    using var pathBorder = GetRoundedPath(rectBorder, radius);
                    using var pen = new Pen(borderColor, 1.5f);
                    e.Graphics.DrawPath(pen, pathBorder);
                }

                TextRenderer.DrawText(
                    e.Graphics,
                    btn.Text,
                    btn.Font,
                    rectOuter,
                    btn.ForeColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak
                );
            };

            Size lastSize = Size.Empty;
            btn.Resize += (s, e) =>
            {
                if (btn.Width > 0 && btn.Height > 0 && btn.Size != lastSize)
                {
                    lastSize = btn.Size;
                    var oldRegion = btn.Region;
                    btn.Region = new Region(GetRoundedPath(new Rectangle(0, 0, btn.Width, btn.Height), radius));
                    oldRegion?.Dispose();
                    btn.Invalidate();
                }
            };

            if (btn.Width > 0 && btn.Height > 0)
            {
                lastSize = btn.Size;
                btn.Region = new Region(GetRoundedPath(new Rectangle(0, 0, btn.Width, btn.Height), radius));
            }
        }

        public static void SetupBadgeRoundedEdges(Label badge, int radius)
        {
            if (badge == null) return;
            if (badge.Tag is string tag && tag.Contains("STYLED_BADGE")) return;
            badge.Tag = (badge.Tag?.ToString() ?? "") + " STYLED_BADGE";

            typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                           ?.SetValue(badge, true);

            badge.Paint += (s, e) =>
            {
                if (s is not Control ctrl) return;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                Color parentBg = badge.Parent?.BackColor != null && badge.Parent.BackColor != Color.Transparent
                    ? badge.Parent.BackColor
                    : Color.White;

                using (var parentBrush = new SolidBrush(parentBg))
                {
                    e.Graphics.FillRectangle(parentBrush, 0, 0, badge.Width, badge.Height);
                }

                var rectOuter = new Rectangle(0, 0, badge.Width, badge.Height);
                using var pathOuter = GetRoundedPath(rectOuter, radius);
                using (var brush = new SolidBrush(badge.BackColor))
                {
                    e.Graphics.FillPath(brush, pathOuter);
                }

                TextRenderer.DrawText(
                    e.Graphics,
                    badge.Text,
                    badge.Font,
                    new Rectangle(0, 0, badge.Width, badge.Height),
                    badge.ForeColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                );
            };

            Size lastSize = Size.Empty;
            badge.Resize += (s, e) =>
            {
                if (badge.Width > 0 && badge.Height > 0 && badge.Size != lastSize)
                {
                    lastSize = badge.Size;
                    var oldRegion = badge.Region;
                    badge.Region = new Region(GetRoundedPath(new Rectangle(0, 0, badge.Width, badge.Height), radius));
                    oldRegion?.Dispose();
                    badge.Invalidate();
                }
            };

            if (badge.Width > 0 && badge.Height > 0)
            {
                lastSize = badge.Size;
                badge.Region = new Region(GetRoundedPath(new Rectangle(0, 0, badge.Width, badge.Height), radius));
            }
        }
    }
}
