using System;
using System.Diagnostics;
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

        /// <summary>
        /// Flags for text centred in a button or badge.
        /// </summary>
        /// <remarks>
        /// <c>SingleLine</c> is not optional: GDI honours DT_VCENTER only together with
        /// DT_SINGLELINE, so pairing <c>VerticalCenter</c> with <c>WordBreak</c> - as this helper
        /// used to - silently drops the vertical centring and leaves the caption sitting against
        /// the top edge. <c>NoPadding</c> drops the few pixels GDI otherwise adds on the left and
        /// right, which pull horizontally centred text off centre on narrow controls.
        /// <c>EndEllipsis</c> trims a caption that will not fit instead of slicing a glyph in half.
        /// </remarks>
        private const TextFormatFlags CenteredText =
            TextFormatFlags.HorizontalCenter
            | TextFormatFlags.VerticalCenter
            | TextFormatFlags.SingleLine
            | TextFormatFlags.NoPadding
            | TextFormatFlags.EndEllipsis;

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
            // Row and header metrics are absolute pixels, so they do not follow the grid when
            // WinForms scales it. Left alone they leave the text floating in an oversized row on
            // one machine and clipped in a cramped one on the next.
            float scale = GetHostScaleFactor(grid);

            grid.ColumnHeadersDefaultCellStyle.Padding = ScalePadding(new Padding(10, 4, 10, 4), scale);
            grid.ColumnHeadersHeight = (int)Math.Round(46 * scale);

            // Row Style
            grid.DefaultCellStyle.BackColor = Color.White;
            grid.DefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(241, 245, 249);
            grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);
            grid.DefaultCellStyle.Padding = ScalePadding(new Padding(8, 4, 8, 4), scale);
            grid.RowTemplate.Height = (int)Math.Round(42 * scale);

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

                int r = ScaleRadius(card, radius);

                Color parentBg = card.Parent?.BackColor != null && card.Parent.BackColor != Color.Transparent
                    ? card.Parent.BackColor
                    : Color.FromArgb(244, 245, 247);

                using (var parentBrush = new SolidBrush(parentBg))
                {
                    e.Graphics.FillRectangle(parentBrush, 0, 0, card.Width, card.Height);
                }

                var rectOuter = new Rectangle(0, 0, card.Width, card.Height);
                using var pathOuter = GetRoundedPath(rectOuter, r);
                using (var cardBrush = new SolidBrush(card.BackColor))
                {
                    e.Graphics.FillPath(cardBrush, pathOuter);
                }

                if (borderColor != Color.Transparent && borderWidth > 0)
                {
                    var rectBorder = new Rectangle(1, 1, Math.Max(1, card.Width - 3), Math.Max(1, card.Height - 3));
                    using var pathBorder = GetRoundedPath(rectBorder, r);
                    using var pen = new Pen(borderColor, borderWidth);
                    e.Graphics.DrawPath(pen, pathBorder);
                }
            };
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

                int r = ScaleRadius(btn, radius);

                Color parentBg = btn.Parent?.BackColor != null && btn.Parent.BackColor != Color.Transparent
                    ? btn.Parent.BackColor
                    : Color.White;

                using (var parentBrush = new SolidBrush(parentBg))
                {
                    e.Graphics.FillRectangle(parentBrush, 0, 0, btn.Width, btn.Height);
                }

                var rectOuter = new Rectangle(0, 0, btn.Width, btn.Height);
                using var pathOuter = GetRoundedPath(rectOuter, r);
                using (var brush = new SolidBrush(btn.BackColor))
                {
                    e.Graphics.FillPath(brush, pathOuter);
                }

                if (borderColor != Color.Transparent)
                {
                    var rectBorder = new Rectangle(1, 1, Math.Max(1, btn.Width - 3), Math.Max(1, btn.Height - 3));
                    using var pathBorder = GetRoundedPath(rectBorder, r);
                    using var pen = new Pen(borderColor, 1.5f);
                    e.Graphics.DrawPath(pen, pathBorder);
                }

                TextRenderer.DrawText(
                    e.Graphics,
                    btn.Text,
                    btn.Font,
                    rectOuter,
                    btn.ForeColor,
                    CenteredText
                );
            };
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

                int r = ScaleRadius(badge, radius);

                Color parentBg = badge.Parent?.BackColor != null && badge.Parent.BackColor != Color.Transparent
                    ? badge.Parent.BackColor
                    : Color.White;

                using (var parentBrush = new SolidBrush(parentBg))
                {
                    e.Graphics.FillRectangle(parentBrush, 0, 0, badge.Width, badge.Height);
                }

                var rectOuter = new Rectangle(0, 0, badge.Width, badge.Height);
                using var pathOuter = GetRoundedPath(rectOuter, r);
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
                    CenteredText
                );
            };
        }

        /// <summary>
        /// The factor the host form's AutoScale applied to this control's bounds at runtime.
        /// </summary>
        /// <remarks>
        /// Forms in this project were authored on machines with different display scaling, so
        /// their designer <c>AutoScaleDimensions</c> disagree (7x15, 8x20, 13x32). WinForms
        /// scales Bounds, Padding and Margin by <c>CurrentAutoScaleDimensions / AutoScaleDimensions</c>
        /// but does <b>not</b> touch a font that was assigned explicitly in the designer or in code.
        /// On a machine whose scaling differs from the authoring machine the containers shrink
        /// while those fonts stay put, and the text overflows and clips.
        /// Returns 1 when there is nothing to correct.
        /// </remarks>
        public static float GetHostScaleFactor(Control control)
        {
            Form? host = control?.FindForm();
            if (host is null) return 1f;

            float authored = host.AutoScaleDimensions.Height;
            float actual = host.CurrentAutoScaleDimensions.Height;
            if (authored <= 0f || actual <= 0f) return 1f;

            return actual / authored;
        }

        /// <summary>
        /// Corner radius in the units the caller authored it in, converted to the pixels this
        /// machine is actually drawing at.
        /// </summary>
        /// <remarks>
        /// The 138 <c>Setup*RoundedEdges</c> calls pass a literal 8, 10 or 12. Those numbers were
        /// picked against the authoring machine's scaling; the control they decorate is scaled at
        /// runtime but a literal is not, so without this the corner is a fixed pixel count on a
        /// control of varying size and the rounding reads differently on every machine.
        /// </remarks>
        public static int ScaleRadius(Control control, int radius)
        {
            if (radius <= 0) return 0;
            return Math.Max(1, (int)Math.Round(radius * GetHostScaleFactor(control)));
        }

        /// <summary>
        /// Scales a hard-coded <see cref="Padding"/> the same way. A non-zero side never rounds
        /// away to nothing, or the gap it was holding open disappears.
        /// </summary>
        public static Padding ScalePadding(Padding padding, float factor)
        {
            if (factor <= 0f || Math.Abs(factor - 1f) < 0.01f) return padding;

            static int Side(int value, float f) => value <= 0 ? value : Math.Max(1, (int)Math.Round(value * f));

            return new Padding(
                Side(padding.Left, factor),
                Side(padding.Top, factor),
                Side(padding.Right, factor),
                Side(padding.Bottom, factor));
        }

        /// <summary>
        /// Returns <paramref name="font"/> resized by <paramref name="factor"/>. The original is
        /// never disposed: designer fonts are frequently the shared statics above, shared by many
        /// controls.
        /// </summary>
        public static Font ScaleFont(Font font, float factor)
        {
            if (font is null || factor <= 0f) return font!;
            return new Font(font.FontFamily, font.Size * factor, font.Style, font.Unit);
        }

        /// <summary>
        /// Multiplies every explicitly-set font under <paramref name="root"/> by <paramref name="factor"/>,
        /// so text tracks the containers WinForms already scaled. See <see cref="GetHostScaleFactor"/>.
        /// Fonts inherited from a parent are skipped - those already followed the parent chain, and
        /// scaling them here would scale them a second time.
        /// </summary>
        /// <remarks>
        /// Call once per control tree, after the control has a parent and has been laid out (its
        /// <c>Load</c> handler). Calling it twice on the same tree compounds the factor.
        /// </remarks>
        public static void ScaleExplicitFonts(Control root, float factor)
        {
            if (root is null || factor <= 0f) return;
            if (Math.Abs(factor - 1f) < 0.01f) return;

            foreach (Control control in Descendants(root))
            {
                // Control.Font hands back the parent's own instance when no local font was set,
                // so reference equality is what separates "explicit" from "inherited".
                bool inherited = control.Parent is not null && ReferenceEquals(control.Font, control.Parent.Font);
                if (!inherited)
                {
                    control.Font = ScaleFont(control.Font, factor);
                }

                // Grid text lives in cell styles, not in Control.Font.
                if (control is DataGridView grid)
                {
                    grid.DefaultCellStyle.Font = ScaleFont(grid.DefaultCellStyle.Font ?? grid.Font, factor);
                    grid.ColumnHeadersDefaultCellStyle.Font = ScaleFont(grid.ColumnHeadersDefaultCellStyle.Font ?? grid.Font, factor);
                    grid.RowsDefaultCellStyle.Font = ScaleFont(grid.RowsDefaultCellStyle.Font ?? grid.Font, factor);
                }
            }
        }

        private static IEnumerable<Control> Descendants(Control root)
        {
            foreach (Control child in root.Controls)
            {
                yield return child;
                foreach (Control descendant in Descendants(child))
                {
                    yield return descendant;
                }
            }
        }

        /// <summary>
        /// Guards the one rule that is easy to get wrong: explicit fonts scale, inherited ones
        /// must not. Debug builds only.
        /// </summary>
        [Conditional("DEBUG")]
        public static void SelfCheckFontScaling()
        {
            using Panel root = new() { Font = new Font("Segoe UI", 10F) };
            Label inherited = new();
            Label explicitFont = new() { Font = new Font("Segoe UI", 10F) };
            root.Controls.Add(inherited);
            root.Controls.Add(explicitFont);

            ScaleExplicitFonts(root, 0.5f);

            Debug.Assert(Math.Abs(root.Font.Size - 10F) < 0.01f, "root font must be left alone");
            Debug.Assert(Math.Abs(inherited.Font.Size - 10F) < 0.01f, "inherited font must not be scaled twice");
            Debug.Assert(Math.Abs(explicitFont.Font.Size - 5F) < 0.01f, "explicit font must be scaled");

            ScaleExplicitFonts(root, 1f);
            Debug.Assert(Math.Abs(explicitFont.Font.Size - 5F) < 0.01f, "factor 1 must be a no-op");

            // An unparented control has no host form, so the factor is 1 and the radius passes through.
            using Button orphan = new();
            Debug.Assert(ScaleRadius(orphan, 12) == 12, "no host form means no scaling");
            Debug.Assert(ScaleRadius(orphan, 0) == 0, "square stays square");

            // A rounded shape must never collapse to a rectangle just because the factor is small.
            Debug.Assert(ScaleRadius(orphan, 1) >= 1, "radius must survive rounding down");

            Padding squeezed = ScalePadding(new Padding(10, 4, 10, 0), 0.1f);
            Debug.Assert(squeezed.Left >= 1 && squeezed.Top >= 1, "a real gap must not round away to zero");
            Debug.Assert(squeezed.Bottom == 0, "a zero side must stay zero");

            // GDI ignores DT_VCENTER unless DT_SINGLELINE rides along - the whole point of this set.
            Debug.Assert(CenteredText.HasFlag(TextFormatFlags.SingleLine), "VerticalCenter needs SingleLine");
        }
    }
}
