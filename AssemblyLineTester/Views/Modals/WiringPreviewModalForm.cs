using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FontAwesome.Sharp;
using AssemblyLineTester.Services;
using AssemblyLineTester.Models;

namespace AssemblyLineTester.Views.Modals
{
    public partial class WiringPreviewModalForm : Form
    {
        private readonly HashSet<int> highlightedRows = [];
        private readonly List<DiagramNode> leftNodes = [];
        private readonly List<DiagramNode> rightNodes = [];
        private readonly List<DiagramConnection> connections = [];
        private readonly List<WiringNetInfo> netDefinitions = [];

        private class WiringNetInfo
        {
            public int RowIndex { get; set; }
            public string FromAddr { get; set; } = "";
            public string FromLabel { get; set; } = "";
            public string ToAddr { get; set; } = "";
            public string ToLabel { get; set; } = "";
        }

        private class DiagramNode
        {
            public int RowIndex { get; set; }
            public string Address { get; set; } = "";
            public string Label { get; set; } = "";
            public RectangleF NodeRect { get; set; }
            public RectangleF LabelRect { get; set; }
            public bool IsLeft { get; set; }
        }

        private class DiagramConnection
        {
            public int RowIndex { get; set; }
            public PointF StartPoint { get; set; }
            public PointF EndPoint { get; set; }
            public PointF Control1 { get; set; }
            public PointF Control2 { get; set; }
            public DiagramNode LeftNode { get; set; } = null!;
            public DiagramNode RightNode { get; set; } = null!;
        }

        public WiringPreviewModalForm()
        {
            InitializeComponent();
            SetupEvents();
            LoadActiveWiringData();
            UpdateDiagramSize();
            ApplyFormRounding();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplyFormRounding();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                AssemblyLineTester.Helpers.UIStyleHelper.ScaleExplicitFonts(this, AssemblyLineTester.Helpers.UIStyleHelper.GetHostScaleFactor(this));
            }
        }

        private void ApplyFormRounding()
        {
            if (Width <= 0 || Height <= 0) return;
            int radius = 24;
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            using GraphicsPath path = new();
            int d = radius * 2;
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            this.Region = new Region(path);
        }

        private void SetupEvents()
        {
            picPreview.Paint += PicPreview_Paint;
            picPreview.MouseDown += PicPreview_MouseDown;
            picPreview.Resize += (s, e) => picPreview.Invalidate();
            pnlPreviewScroll.Resize += (s, e) => { UpdateDiagramSize(); picPreview.Invalidate(); };

            btnClose.Click += (s, e) => Close();
            btnCloseModal.Click += (s, e) => Close();
            btnMaximize.Click += (s, e) => ToggleMaximize();

            btnClose.MouseEnter += (s, e) => { btnClose.BackColor = Color.FromArgb(239, 68, 68); btnClose.IconColor = Color.White; };
            btnClose.MouseLeave += (s, e) => { btnClose.BackColor = Color.Transparent; btnClose.IconColor = Color.FromArgb(203, 213, 225); };

            btnMaximize.MouseEnter += (s, e) => { btnMaximize.BackColor = Color.FromArgb(30, 41, 59); btnMaximize.IconColor = Color.White; };
            btnMaximize.MouseLeave += (s, e) => { btnMaximize.BackColor = Color.Transparent; btnMaximize.IconColor = Color.FromArgb(203, 213, 225); };

            pnlCustomHeader.MouseDown += TitleBar_MouseDown;
            iconLogo.MouseDown += TitleBar_MouseDown;
            lblCustomTitle.MouseDown += TitleBar_MouseDown;

            pnlCustomHeader.DoubleClick += (s, e) => ToggleMaximize();
            iconLogo.DoubleClick += (s, e) => ToggleMaximize();
            lblCustomTitle.DoubleClick += (s, e) => ToggleMaximize();
        }

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        private void TitleBar_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                ActiveControl = pnlCustomHeader;
                ValidateChildren();

                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        private void ToggleMaximize()
        {
            ActiveControl = pnlCustomHeader;
            ValidateChildren();

            if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
                btnMaximize.IconChar = IconChar.WindowMaximize;
                Padding = new Padding(1);
            }
            else
            {
                MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;
                WindowState = FormWindowState.Maximized;
                btnMaximize.IconChar = IconChar.WindowRestore;
                Padding = new Padding(0);
            }
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x0084;
            const int HTLEFT = 10;
            const int HTRIGHT = 11;
            const int HTTOP = 12;
            const int HTTOPLEFT = 13;
            const int HTTOPRIGHT = 14;
            const int HTBOTTOM = 15;
            const int HTBOTTOMLEFT = 16;
            const int HTBOTTOMRIGHT = 17;

            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST && (int)m.Result == 1 && WindowState == FormWindowState.Normal)
            {
                Point pt = PointToClient(Cursor.Position);
                int border = 6;
                bool overButtons = pt.X >= btnMaximize.Left && pt.Y <= pnlCustomHeader.Height;

                if (overButtons) return;

                if (pt.X <= border && pt.Y <= border) m.Result = (IntPtr)HTTOPLEFT;
                else if (pt.X >= ClientSize.Width - border && pt.Y <= border) m.Result = (IntPtr)HTTOPRIGHT;
                else if (pt.X <= border && pt.Y >= ClientSize.Height - border) m.Result = (IntPtr)HTBOTTOMLEFT;
                else if (pt.X >= ClientSize.Width - border && pt.Y >= ClientSize.Height - border) m.Result = (IntPtr)HTBOTTOMRIGHT;
                else if (pt.X <= border) m.Result = (IntPtr)HTLEFT;
                else if (pt.X >= ClientSize.Width - border) m.Result = (IntPtr)HTRIGHT;
                else if (pt.Y <= border) m.Result = (IntPtr)HTTOP;
                else if (pt.Y >= ClientSize.Height - border) m.Result = (IntPtr)HTBOTTOM;
            }
        }

        private void LoadActiveWiringData()
        {
            netDefinitions.Clear();
            highlightedRows.Clear();

            var configService = new TestConfigService();
            var activeConfig = configService.GetActiveConfig();

            if (activeConfig != null && activeConfig.Nets != null && activeConfig.Nets.Count > 0)
            {
                int i = 0;
                foreach (var net in activeConfig.Nets)
                {
                    netDefinitions.Add(new WiringNetInfo
                    {
                        RowIndex = i++,
                        FromLabel = net.FromLabel ?? "",
                        FromAddr = net.FromAddress.ToString(),
                        ToLabel = net.ToLabel ?? "",
                        ToAddr = net.ToAddress.ToString()
                    });
                }
            }
            else
            {
                // Fallback to sample data if no active config
                LoadDefaultWiringData();
            }

            UpdateDiagramSize();
            picPreview.Invalidate();
        }

        private void LoadDefaultWiringData()
        {
            netDefinitions.Clear();
            highlightedRows.Clear();

            string[] sampleNets =
            [
                "Low Voltage Supply|X1-01|Y1-01",
                "ECM-Main Connector|X2-01|Y2-01",
                "ECM-Main Connector|X2-02|Y2-02",
                "Low Voltage Supply|X1-02|Y1-02",
                "Power Bus Line 12V|X1-03|Y1-03",
                "Chassis Ground A|X1-04|Y1-04",
                "CAN High Bus Data|X2-03|Y2-03",
                "CAN Low Bus Data|X2-04|Y2-04",
                "Sensor Supply 5V|X1-05|Y1-05",
                "Sensor Return Gnd|X1-06|Y1-06",
                "Ignition Trigger Line|X2-05|Y2-05",
                "Fuel Injector A|X2-06|Y2-06",
                "Fuel Injector B|X2-07|Y2-07",
                "Safety Interlock Loop|X1-07|Y1-07",
                "Auxiliary Shield Gnd|X1-08|Y1-08"
            ];

            for (int i = 0; i < sampleNets.Length; i++)
            {
                string[] parts = sampleNets[i].Split('|');
                netDefinitions.Add(new WiringNetInfo
                {
                    RowIndex = i,
                    FromLabel = parts[0],
                    FromAddr = parts[1],
                    ToLabel = parts[0],
                    ToAddr = parts[2]
                });
            }

            UpdateDiagramSize();
            picPreview.Invalidate();
        }

        private void UpdateDiagramSize()
        {
            if (pnlPreviewScroll.Width <= 0 || pnlPreviewScroll.Height <= 0) return;

            int netCount = netDefinitions.Count;
            int neededHeight = netCount * 65 + 120;
            int targetHeight = Math.Max(pnlPreviewScroll.ClientSize.Height, neededHeight);
            int targetWidth = Math.Max(pnlPreviewScroll.ClientSize.Width, 900);

            if (picPreview.Width != targetWidth || picPreview.Height != targetHeight)
            {
                picPreview.Size = new Size(targetWidth, targetHeight);
            }
        }

        private void ToggleHighlight(int rowIndex)
        {
            if (!highlightedRows.Remove(rowIndex))
            {
                highlightedRows.Add(rowIndex);
            }
            picPreview.Invalidate();
        }

        private void PicPreview_MouseDown(object? sender, MouseEventArgs e)
        {
            PointF pt = e.Location;

            foreach (var node in leftNodes.Concat(rightNodes).OrderByDescending(n => highlightedRows.Contains(n.RowIndex)))
            {
                if (IsPointInRect(pt, node.NodeRect) || IsPointInRect(pt, node.LabelRect))
                {
                    ToggleHighlight(node.RowIndex);
                    return;
                }
            }

            foreach (var conn in connections.OrderByDescending(c => highlightedRows.Contains(c.RowIndex)))
            {
                if (IsPointNearBezier(pt, conn.StartPoint, conn.Control1, conn.Control2, conn.EndPoint, 12f))
                {
                    ToggleHighlight(conn.RowIndex);
                    return;
                }
            }
        }

        private static bool IsPointInRect(PointF pt, RectangleF rect)
        {
            return pt.X >= rect.Left && pt.X <= rect.Right && pt.Y >= rect.Top && pt.Y <= rect.Bottom;
        }

        private static bool IsPointNearBezier(PointF pt, PointF p0, PointF p1, PointF p2, PointF p3, float threshold)
        {
            int steps = 40;
            for (int i = 0; i <= steps; i++)
            {
                float t = i / (float)steps;
                float u = 1 - t;
                float tt = t * t;
                float uu = u * u;
                float uuu = uu * u;
                float ttt = tt * t;

                float x = uuu * p0.X + 3 * uu * t * p1.X + 3 * u * tt * p2.X + ttt * p3.X;
                float y = uuu * p0.Y + 3 * uu * t * p1.Y + 3 * u * tt * p2.Y + ttt * p3.Y;

                float dx = pt.X - x;
                float dy = pt.Y - y;
                if (Math.Sqrt(dx * dx + dy * dy) <= threshold)
                {
                    return true;
                }
            }
            return false;
        }

        private void PicPreview_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Rectangle r = picPreview.ClientRectangle;

            using SolidBrush bgBrush = new(Color.FromArgb(248, 250, 252));
            g.FillRectangle(bgBrush, r);

            using SolidBrush dotBrush = new(Color.FromArgb(226, 232, 240));
            for (int x = 20; x < r.Width; x += 25)
            {
                for (int y = 20; y < r.Height; y += 25)
                {
                    g.FillEllipse(dotBrush, x - 1, y - 1, 2, 2);
                }
            }

            leftNodes.Clear();
            rightNodes.Clear();
            connections.Clear();

            int rowCount = netDefinitions.Count;
            if (rowCount == 0) return;

            float availHeight = r.Height - 100;
            float startY = 50f;
            float rowSpacing = Math.Max(55f, availHeight / Math.Max(rowCount, 1));

            List<DiagramNode> rawLeftNodes = [];
            List<DiagramNode> rawRightNodes = [];

            for (int i = 0; i < rowCount; i++)
            {
                var net = netDefinitions[i];

                DiagramNode lNode = new()
                {
                    RowIndex = i,
                    Address = net.FromAddr,
                    Label = net.FromLabel,
                    IsLeft = true
                };
                rawLeftNodes.Add(lNode);

                DiagramNode rNode = new()
                {
                    RowIndex = i,
                    Address = net.ToAddr,
                    Label = net.ToLabel,
                    IsLeft = false
                };
                rawRightNodes.Add(rNode);
            }

            var sortedLeftNodes = rawLeftNodes.OrderBy(n => int.TryParse(n.Address, out int val) ? val : n.Address.GetHashCode()).ToList();
            for (int idx = 0; idx < sortedLeftNodes.Count; idx++)
            {
                float yPos = startY + idx * rowSpacing;
                var lNode = sortedLeftNodes[idx];
                lNode.NodeRect = new(185, yPos - 22, 60, 44);
                lNode.LabelRect = new(35, yPos - 18, 142, 36);
                leftNodes.Add(lNode);
            }

            var sortedRightNodes = rawRightNodes.OrderBy(n => int.TryParse(n.Address, out int val) ? val : n.Address.GetHashCode()).ToList();
            for (int idx = 0; idx < sortedRightNodes.Count; idx++)
            {
                float yPos = startY + idx * rowSpacing;
                var rNode = sortedRightNodes[idx];
                rNode.NodeRect = new(r.Width - 245, yPos - 22, 60, 44);
                rNode.LabelRect = new(r.Width - 177, yPos - 18, 142, 36);
                rightNodes.Add(rNode);
            }

            for (int i = 0; i < rowCount; i++)
            {
                var lNode = leftNodes.FirstOrDefault(n => n.RowIndex == i);
                var rNode = rightNodes.FirstOrDefault(n => n.RowIndex == i);

                if (lNode != null && rNode != null)
                {
                    PointF startPt = new(lNode.NodeRect.Right, lNode.NodeRect.Top + lNode.NodeRect.Height / 2);
                    PointF endPt = new(rNode.NodeRect.Left, rNode.NodeRect.Top + rNode.NodeRect.Height / 2);
                    float offset = (endPt.X - startPt.X) * 0.45f;

                    connections.Add(new()
                    {
                        RowIndex = i,
                        StartPoint = startPt,
                        EndPoint = endPt,
                        Control1 = new(startPt.X + offset, startPt.Y),
                        Control2 = new(endPt.X - offset, endPt.Y),
                        LeftNode = lNode,
                        RightNode = rNode
                    });
                }
            }

            using Pen normalPen = new(Color.FromArgb(14, 165, 233), 3f) { StartCap = LineCap.Round, EndCap = LineCap.Round };
            using Pen darkPen = new(Color.FromArgb(15, 23, 42), 3f) { StartCap = LineCap.Round, EndCap = LineCap.Round };
            foreach (var conn in connections)
            {
                if (!highlightedRows.Contains(conn.RowIndex))
                {
                    Pen p = (conn.RowIndex % 2 == 0) ? normalPen : darkPen;
                    g.DrawBezier(p, conn.StartPoint, conn.Control1, conn.Control2, conn.EndPoint);
                }
            }

            using Font fontLabel = new("Segoe UI", 9.5F, FontStyle.Bold);
            using Font fontAddr = new("Segoe UI", 10.5F, FontStyle.Bold);
            using StringFormat sfLabel = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.NoWrap, Trimming = StringTrimming.EllipsisCharacter };
            using StringFormat sfAddr = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.NoClip };

            foreach (var node in leftNodes.Concat(rightNodes))
            {
                if (!highlightedRows.Contains(node.RowIndex))
                {
                    DrawNodeAndLabel(g, node, false, fontLabel, fontAddr, sfLabel, sfAddr);
                }
            }

            using Pen glowPen = new(Color.FromArgb(254, 240, 138), 11f) { StartCap = LineCap.Round, EndCap = LineCap.Round };
            using Pen innerPen = new(Color.FromArgb(13, 110, 253), 3.5f) { StartCap = LineCap.Round, EndCap = LineCap.Round };
            foreach (var conn in connections)
            {
                if (highlightedRows.Contains(conn.RowIndex))
                {
                    g.DrawBezier(glowPen, conn.StartPoint, conn.Control1, conn.Control2, conn.EndPoint);
                    g.DrawBezier(innerPen, conn.StartPoint, conn.Control1, conn.Control2, conn.EndPoint);
                }
            }

            foreach (var node in leftNodes.Concat(rightNodes))
            {
                if (highlightedRows.Contains(node.RowIndex))
                {
                    DrawNodeAndLabel(g, node, true, fontLabel, fontAddr, sfLabel, sfAddr);
                }
            }
        }

        private static void DrawNodeAndLabel(Graphics g, DiagramNode node, bool isHighlight, Font fontLabel, Font fontAddr, StringFormat sfLabel, StringFormat sfAddr)
        {
            if (isHighlight)
            {
                RectangleF haloRect = RectangleF.Inflate(node.NodeRect, 6f, 6f);
                using SolidBrush haloFill = new(Color.FromArgb(254, 240, 138));
                using Pen haloBorder = new(Color.FromArgb(245, 158, 11), 3f);
                DrawRoundedRectangle(g, haloFill, haloBorder, haloRect, 24f);
            }

            using SolidBrush labelBg = new(isHighlight ? Color.FromArgb(254, 249, 195) : Color.FromArgb(226, 232, 240));
            using Pen labelBorder = new(isHighlight ? Color.FromArgb(245, 158, 11) : Color.FromArgb(203, 213, 225), 1.5f);
            using SolidBrush labelText = new(Color.FromArgb(30, 41, 59));
            DrawRoundedRectangle(g, labelBg, labelBorder, node.LabelRect, 8f);
            g.DrawString(node.Label, fontLabel, labelText, node.LabelRect, sfLabel);

            Color nodeColor = node.IsLeft ? Color.FromArgb(0, 180, 216) : Color.FromArgb(74, 222, 128);
            using SolidBrush nodeFill = new(nodeColor);
            using Pen nodeBorder = new(Color.FromArgb(15, 23, 42), 2f);
            using SolidBrush addrText = new(Color.FromArgb(15, 23, 42));
            DrawRoundedRectangle(g, nodeFill, nodeBorder, node.NodeRect, 20f);

            float dynamicSize = node.Address.Length >= 4 ? 7.5F : (node.Address.Length == 3 ? 8.5F : (node.Address.Length == 2 ? 9.5F : 10.5F));
            using Font dynamicAddrFont = new("Segoe UI", dynamicSize, FontStyle.Bold);
            g.DrawString(node.Address, dynamicAddrFont, addrText, node.NodeRect, sfAddr);
        }

        private static void DrawRoundedRectangle(Graphics g, Brush brush, Pen pen, RectangleF rect, float radius)
        {
            using GraphicsPath path = new();
            float d = radius * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            g.FillPath(brush, path);
            g.DrawPath(pen, path);
        }
    }
}
