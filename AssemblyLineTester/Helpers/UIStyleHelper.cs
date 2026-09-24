using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
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

        // RULE: rebuilding a control list must go through here. Controls.Clear() only unparents,
        // and Control has no finaliser that frees the window handle, so the USER objects of every
        // discarded pagination button and card leak for the life of the process. Painting degrades
        // as the process nears the 10,000 handle cap, which is the "flickers after a long shift".
        public static void ClearAndDispose(Control.ControlCollection controls)
        {
            while (controls.Count > 0)
            {
                Control child = controls[0];
                controls.RemoveAt(0);
                child.Dispose();
            }
        }

        // Looked up once. This walks a whole page - Reports alone is 113 controls - and resolving
        // the property per control put a reflection lookup on the navigation path.
        private static readonly PropertyInfo? DoubleBufferedProperty =
            typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);

        // Panels and grids created after MainForm ran its own pass never got double buffering,
        // so anything custom-painted on a page flickered on every repaint.
        public static void EnableDoubleBuffering(Control root)
        {
            if (root is Panel or TableLayoutPanel or FlowLayoutPanel or SplitContainer or SplitterPanel or DataGridView or UserControl or PictureBox or TabControl)
            {
                DoubleBufferedProperty?.SetValue(root, true);
            }

            foreach (Control child in root.Controls)
            {
                EnableDoubleBuffering(child);
            }
        }

        // RULE: SingleLine is mandatory - GDI honours DT_VCENTER only alongside DT_SINGLELINE.
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

            // DoubleBuffered is protected, so reflection is the only way in.
            DoubleBufferedProperty?.SetValue(grid, true);

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

            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(71, 85, 105);
            grid.ColumnHeadersDefaultCellStyle.Font = Font95Bold;
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(248, 250, 252);
            grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(71, 85, 105);
            // Absolute pixels do not follow the grid when WinForms scales it, so they are scaled here.
            float scale = GetHostScaleFactor(grid);

            grid.ColumnHeadersDefaultCellStyle.Padding = ScalePadding(new Padding(10, 4, 10, 4), scale);
            grid.ColumnHeadersHeight = (int)Math.Round(46 * scale);

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

            DoubleBufferedProperty?.SetValue(card, true);

            // Calculate required padding to keep child controls safely inside the rounded corners
            int requiredPadding = (int)Math.Ceiling(radius - (radius - borderWidth / 2f) / 1.4142f) + 1;

            if (card is TableLayoutPanel || card.Padding.Left < requiredPadding || card.Padding.Top < requiredPadding || card.Padding.Right < requiredPadding || card.Padding.Bottom < requiredPadding)
            {
                card.Padding = new Padding(
                    Math.Max(card.Padding.Left, requiredPadding),
                    Math.Max(card.Padding.Top, requiredPadding),
                    Math.Max(card.Padding.Right, requiredPadding),
                    Math.Max(card.Padding.Bottom, requiredPadding)
                );
            }

            int? cachedRadius = null;

            card.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                if (cachedRadius == null)
                {
                    if (card.FindForm() != null) cachedRadius = ScaleRadius(card, radius);
                    else cachedRadius = radius; // Fallback if not yet parented to a form
                }
                int r = cachedRadius.Value;

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

                if (card is PictureBox pb && pb.Image != null)
                {
                    var g = e.Graphics;
                    var state = g.Save();
                    g.SetClip(pathOuter);

                    var img = pb.Image;
                    int pad = (int)Math.Ceiling(borderWidth) + 1;
                    int availW = Math.Max(1, card.Width - pad * 2);
                    int availH = Math.Max(1, card.Height - pad * 2);

                    if (pb.SizeMode == PictureBoxSizeMode.Zoom && img.Width > 0 && img.Height > 0)
                    {
                        float imgAspect = (float)img.Width / img.Height;
                        float boxAspect = (float)availW / availH;
                        Rectangle destRect;
                        if (imgAspect > boxAspect)
                        {
                            int h = Math.Max(1, (int)Math.Round(availW / imgAspect));
                            int y = pad + (availH - h) / 2;
                            destRect = new Rectangle(pad, y, availW, h);
                        }
                        else
                        {
                            int w = Math.Max(1, (int)Math.Round(availH * imgAspect));
                            int x = pad + (availW - w) / 2;
                            destRect = new Rectangle(x, pad, w, availH);
                        }
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.DrawImage(img, destRect);
                    }
                    else if (pb.SizeMode == PictureBoxSizeMode.CenterImage)
                    {
                        int x = (card.Width - img.Width) / 2;
                        int y = (card.Height - img.Height) / 2;
                        g.DrawImage(img, x, y);
                    }
                    else if (pb.SizeMode == PictureBoxSizeMode.StretchImage)
                    {
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.DrawImage(img, pad, pad, availW, availH);
                    }
                    else
                    {
                        g.DrawImage(img, pad, pad);
                    }

                    g.Restore(state);
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

            DoubleBufferedProperty?.SetValue(btn, true);

            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = normColor;
            btn.Cursor = Cursors.Hand;

            btn.MouseEnter += (s, e) => { btn.BackColor = hoverColor; btn.Invalidate(); };
            btn.MouseLeave += (s, e) => { btn.BackColor = normColor; btn.Invalidate(); };
            btn.MouseDown += (s, e) => { btn.BackColor = pressColor; btn.Invalidate(); };
            btn.MouseUp += (s, e) => { btn.BackColor = hoverColor; btn.Invalidate(); };

            int? cachedRadius = null;

            btn.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                if (cachedRadius == null)
                {
                    if (btn.FindForm() != null) cachedRadius = ScaleRadius(btn, radius);
                    else cachedRadius = radius;
                }
                int r = cachedRadius.Value;

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

            DoubleBufferedProperty?.SetValue(badge, true);

            int? cachedRadius = null;

            badge.Paint += (s, e) =>
            {
                if (s is not Control ctrl) return;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                if (cachedRadius == null)
                {
                    if (badge.FindForm() != null) cachedRadius = ScaleRadius(badge, radius);
                    else cachedRadius = radius;
                }
                int r = cachedRadius.Value;

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

        // RULE: designer AutoScaleDimensions disagree (7x15, 8x20, 13x32) because each form was
        // authored at a different Windows scaling. WinForms scales Bounds/Padding/Margin by this
        // factor but never an explicitly-set Font, which is why text overflows its container on a
        // machine that did not author it. Set Windows scaling to 100% before opening a Designer,
        // or Visual Studio rewrites AutoScaleDimensions and the divergence comes back.
        public static float GetHostScaleFactor(Control control)
        {
            Form? host = control?.FindForm();
            if (host is null) return 1f;

            float authored = host.AutoScaleDimensions.Height;
            float actual = host.CurrentAutoScaleDimensions.Height;
            if (authored <= 0f || actual <= 0f) return 1f;

            return actual / authored;
        }

        // The literal 8/10/12 passed by every Setup*RoundedEdges call was picked at one scaling.
        public static int ScaleRadius(Control control, int radius)
        {
            if (radius <= 0) return 0;
            return Math.Max(1, (int)Math.Round(radius * GetHostScaleFactor(control)));
        }

        // A non-zero side never rounds away to nothing, or the gap it held open vanishes.
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

        // Memoised: pagination rebuilds call this per button, and a Font is a GDI handle that
        // Control.Dispose does not release, so every page change used to leak one per button.
        private static readonly Dictionary<(string Family, float Size, FontStyle Style, GraphicsUnit Unit), Font> ScaledFonts = new();

        // The original is never disposed; designer fonts are often the shared statics above.
        public static Font ScaleFont(Font font, float factor)
        {
            if (font is null || factor <= 0f) return font!;

            var key = (font.FontFamily.Name, font.Size * factor, font.Style, font.Unit);
            if (!ScaledFonts.TryGetValue(key, out Font? scaled))
            {
                scaled = new Font(key.Item1, key.Item2, key.Style, key.Unit);
                ScaledFonts[key] = scaled;
            }

            return scaled;
        }

        // RULE: call once per tree, from Load. Twice compounds the factor; inherited fonts are skipped.
        public static void ScaleExplicitFonts(Control root, float factor)
        {
            if (root is null || factor <= 0f) return;
            if (Math.Abs(factor - 1f) < 0.01f) return;

            root.SuspendLayout();
            try
            {

                foreach (Control control in Descendants(root))
                {
                // Control.Font returns the parent's instance when unset, so identity means inherited.
                    bool inherited = control.Parent is not null && ReferenceEquals(control.Font, control.Parent.Font);
                    if (!inherited)
                    {
                        control.Font = ScaleFont(control.Font, factor);
                    }

                    if (control is DataGridView grid)
                    {
                        grid.DefaultCellStyle.Font = ScaleFont(grid.DefaultCellStyle.Font ?? grid.Font, factor);
                        grid.ColumnHeadersDefaultCellStyle.Font = ScaleFont(grid.ColumnHeadersDefaultCellStyle.Font ?? grid.Font, factor);
                        grid.RowsDefaultCellStyle.Font = ScaleFont(grid.RowsDefaultCellStyle.Font ?? grid.Font, factor);
                    }
                }
            }
            finally
            {
                root.ResumeLayout(false);
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

        // Debug-only guard: explicit fonts scale, inherited ones must not.
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

            using Button orphan = new();
            Debug.Assert(ScaleRadius(orphan, 12) == 12, "no host form means no scaling");
            Debug.Assert(ScaleRadius(orphan, 0) == 0, "square stays square");

            Debug.Assert(ScaleRadius(orphan, 1) >= 1, "radius must survive rounding down");

            Padding squeezed = ScalePadding(new Padding(10, 4, 10, 0), 0.1f);
            Debug.Assert(squeezed.Left >= 1 && squeezed.Top >= 1, "a real gap must not round away to zero");
            Debug.Assert(squeezed.Bottom == 0, "a zero side must stay zero");

            Debug.Assert(CenteredText.HasFlag(TextFormatFlags.SingleLine), "VerticalCenter needs SingleLine");
        }
    }
}
