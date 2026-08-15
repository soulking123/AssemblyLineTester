using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace AssemblyLineTester.Views.Controls
{
    public class DiagramNetDefinition
    {
        public int RowIndex { get; set; }
        public string FromAddress { get; set; } = "";
        public string FromLabel { get; set; } = "";
        public string ToAddress { get; set; } = "";
        public string ToLabel { get; set; } = "";
        public bool IsError { get; set; }
    }

    public class SchematicDiagramControl : UserControl
    {
        private HashSet<int> _highlightedRows = new HashSet<int>();
        private List<DiagramNode> _leftNodes = new List<DiagramNode>();
        private List<DiagramNode> _rightNodes = new List<DiagramNode>();
        private List<DiagramConnection> _connections = new List<DiagramConnection>();
        private List<DiagramNetDefinition> _netDefinitions = new List<DiagramNetDefinition>();
        
        private int _totalTestPointsAvailable = 64;

        public event Action<int>? DiagramRowClicked;

        public SchematicDiagramControl()
        {
            DoubleBuffered = true;
            BackColor = Color.FromArgb(248, 250, 252);
        }

        public void RenderDiagram(IEnumerable<DiagramNetDefinition> nets, int totalTestPoints)
        {
            _netDefinitions = nets.ToList();
            _totalTestPointsAvailable = totalTestPoints;
            CalculateLayout();
            Invalidate();
        }

        public void SetHighlightedRows(HashSet<int> rows)
        {
            _highlightedRows = new HashSet<int>(rows);
            Invalidate();
        }

        public void AddHighlight(int rowIndex)
        {
            _highlightedRows.Add(rowIndex);
            Invalidate();
        }

        public void RemoveHighlight(int rowIndex)
        {
            _highlightedRows.Remove(rowIndex);
            Invalidate();
        }

        public void ToggleHighlight(int rowIndex)
        {
            if (!_highlightedRows.Remove(rowIndex))
            {
                _highlightedRows.Add(rowIndex);
            }
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            CalculateLayout();
            Invalidate();
        }

        private void CalculateLayout()
        {
            _leftNodes.Clear();
            _rightNodes.Clear();
            _connections.Clear();

            int rowCount = _netDefinitions.Count;
            if (rowCount == 0) return;

            var allNodesDict = new Dictionary<string, DiagramNode>();

            foreach (var net in _netDefinitions)
            {
                void ProcessNode(string addr, string label)
                {
                    if (string.IsNullOrEmpty(addr) || addr == "0") return;

                    if (!allNodesDict.TryGetValue(addr, out DiagramNode? n))
                    {
                        n = new DiagramNode { RowIndices = [net.RowIndex], Address = addr, Label = label };
                        allNodesDict[addr] = n;
                    }
                    else
                    {
                        if (!n.RowIndices.Contains(net.RowIndex)) n.RowIndices.Add(net.RowIndex);
                        if ((string.IsNullOrEmpty(n.Label) || n.Label == "NC" || n.Label == "Unexpected") &&
                            !string.IsNullOrEmpty(label) && label != "NC" && label != "Unexpected")
                        {
                            n.Label = label;
                        }
                    }
                }

                ProcessNode(net.FromAddress, net.FromLabel);
                ProcessNode(net.ToAddress, net.ToLabel);
            }

            int splitPoint = _totalTestPointsAvailable / 2;

            var sortedUniqueNodes = allNodesDict.Values.OrderBy(n => int.TryParse(n.Address, out int val) ? val : n.Address.GetHashCode()).ToList();

            List<DiagramNode> rawLeftNodes = [];
            List<DiagramNode> rawRightNodes = [];

            foreach (var node in sortedUniqueNodes)
            {
                if (int.TryParse(node.Address, out int val))
                {
                    if (val <= splitPoint)
                    {
                        node.IsLeft = true;
                        rawLeftNodes.Add(node);
                    }
                    else
                    {
                        node.IsLeft = false;
                        rawRightNodes.Add(node);
                    }
                }
                else
                {
                    node.IsLeft = false;
                    rawRightNodes.Add(node);
                }
            }

            float availHeight = this.Height - 100;
            float startY = 50f;
            
            // Check if we need to resize height to accommodate all nodes
            int maxNodes = Math.Max(rawLeftNodes.Count, rawRightNodes.Count);
            if (maxNodes * 55f > availHeight)
            {
                int reqHeight = (int)(maxNodes * 55f + 100f);
                if (reqHeight > this.Height)
                {
                    this.Height = reqHeight;
                    availHeight = this.Height - 100;
                }
            }

            float rowSpacingLeft = Math.Max(55f, availHeight / Math.Max(rawLeftNodes.Count, 1));
            for (int idx = 0; idx < rawLeftNodes.Count; idx++)
            {
                float yPos = startY + idx * rowSpacingLeft;
                var lNode = rawLeftNodes[idx];
                lNode.NodeRect = new RectangleF(100, yPos - 22, 50, 44);
                lNode.LabelRect = new RectangleF(20, yPos - 18, 70, 36);
                _leftNodes.Add(lNode);
            }

            float rowSpacingRight = Math.Max(55f, availHeight / Math.Max(rawRightNodes.Count, 1));
            for (int idx = 0; idx < rawRightNodes.Count; idx++)
            {
                float yPos = startY + idx * rowSpacingRight;
                var rNode = rawRightNodes[idx];
                rNode.NodeRect = new RectangleF(this.Width - 150, yPos - 22, 50, 44);
                rNode.LabelRect = new RectangleF(this.Width - 90, yPos - 18, 70, 36);
                _rightNodes.Add(rNode);
            }

            foreach (var net in _netDefinitions)
            {
                var lNode = _leftNodes.Concat(_rightNodes).FirstOrDefault(n => n.Address == net.FromAddress);
                var rNode = _leftNodes.Concat(_rightNodes).FirstOrDefault(n => n.Address == net.ToAddress);

                if (lNode != null && rNode != null)
                {
                    PointF startPt;
                    PointF endPt;
                    PointF c1;
                    PointF c2;

                    if (lNode.IsLeft && rNode.IsLeft)
                    {
                        startPt = new PointF(lNode.NodeRect.Right, lNode.NodeRect.Top + lNode.NodeRect.Height / 2);
                        endPt = new PointF(rNode.NodeRect.Right, rNode.NodeRect.Top + rNode.NodeRect.Height / 2);
                        float offset = Math.Max(Math.Min(Math.Abs(endPt.Y - startPt.Y) * 0.8f, 150f), 60f);
                        c1 = new PointF(startPt.X + offset, startPt.Y);
                        c2 = new PointF(endPt.X + offset, endPt.Y);
                    }
                    else if (!lNode.IsLeft && !rNode.IsLeft)
                    {
                        startPt = new PointF(lNode.NodeRect.Left, lNode.NodeRect.Top + lNode.NodeRect.Height / 2);
                        endPt = new PointF(rNode.NodeRect.Left, rNode.NodeRect.Top + rNode.NodeRect.Height / 2);
                        float offset = Math.Max(Math.Min(Math.Abs(endPt.Y - startPt.Y) * 0.8f, 150f), 60f);
                        c1 = new PointF(startPt.X - offset, startPt.Y);
                        c2 = new PointF(endPt.X - offset, endPt.Y);
                    }
                    else
                    {
                        var leftSideNode = lNode.IsLeft ? lNode : rNode;
                        var rightSideNode = lNode.IsLeft ? rNode : lNode;

                        startPt = new PointF(leftSideNode.NodeRect.Right, leftSideNode.NodeRect.Top + leftSideNode.NodeRect.Height / 2);
                        endPt = new PointF(rightSideNode.NodeRect.Left, rightSideNode.NodeRect.Top + rightSideNode.NodeRect.Height / 2);
                        float offset = (endPt.X - startPt.X) * 0.45f;
                        c1 = new PointF(startPt.X + offset, startPt.Y);
                        c2 = new PointF(endPt.X - offset, endPt.Y);
                    }

                    _connections.Add(new DiagramConnection
                    {
                        RowIndex = net.RowIndex,
                        StartPoint = startPt,
                        EndPoint = endPt,
                        Control1 = c1,
                        Control2 = c2,
                        LeftNode = lNode,
                        RightNode = rNode,
                        IsError = net.IsError
                    });
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            using SolidBrush dotBrush = new SolidBrush(Color.FromArgb(226, 232, 240));
            for (int x = 20; x < this.Width; x += 25)
            {
                for (int y = 20; y < this.Height; y += 25)
                {
                    g.FillEllipse(dotBrush, x - 1, y - 1, 2, 2);
                }
            }

            using Pen wirePen = new Pen(Color.Black, 2.5f);
            using Pen activeWirePen = new Pen(Color.Black, 3.5f);
            using Pen errorWirePen = new Pen(Color.FromArgb(244, 63, 94), 4f); // High contrast Pink/Red
            
            using Pen glowPen = new Pen(Color.FromArgb(254, 240, 138), 11f) { StartCap = LineCap.Round, EndCap = LineCap.Round };

            // First draw the glows for highlighted rows
            foreach (var conn in _connections.Where(c => _highlightedRows.Contains(c.RowIndex)))
            {
                g.DrawBezier(glowPen, conn.StartPoint, conn.Control1, conn.Control2, conn.EndPoint);
            }

            foreach (var conn in _connections.OrderBy(c => _highlightedRows.Contains(c.RowIndex) ? 1 : 0))
            {
                bool isHighlight = _highlightedRows.Contains(conn.RowIndex);
                Pen p;
                if (conn.IsError) p = errorWirePen;
                else if (isHighlight) p = activeWirePen;
                else p = wirePen;

                g.DrawBezier(p, conn.StartPoint, conn.Control1, conn.Control2, conn.EndPoint);
            }

            using Font fontLabel = new Font("Segoe UI", 9F, FontStyle.Regular);
            using Font fontAddr = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            using StringFormat sfLabel = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter };
            using StringFormat sfAddr = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

            foreach (var node in _leftNodes.Concat(_rightNodes))
            {
                DrawNodeAndLabel(g, node, node.RowIndices.Any(idx => _highlightedRows.Contains(idx)), fontLabel, fontAddr, sfLabel, sfAddr);
            }
        }

        private static void DrawNodeAndLabel(Graphics g, DiagramNode node, bool isHighlight, Font fontLabel, Font fontAddr, StringFormat sfLabel, StringFormat sfAddr)
        {
            if (isHighlight)
            {
                RectangleF haloRect = RectangleF.Inflate(node.NodeRect, 6f, 6f);
                using SolidBrush haloFill = new SolidBrush(Color.FromArgb(254, 240, 138));
                using Pen haloBorder = new Pen(Color.FromArgb(245, 158, 11), 3f);
                DrawRoundedRectangle(g, haloFill, haloBorder, haloRect, 24f);
            }

            Color nodeColor = node.IsLeft ? Color.FromArgb(0, 180, 216) : Color.FromArgb(74, 222, 128);
            using SolidBrush nodeFill = new SolidBrush(nodeColor);
            using Pen nodeBorder = new Pen(Color.FromArgb(15, 23, 42), 2f);
            using SolidBrush addrText = new SolidBrush(Color.FromArgb(15, 23, 42));
            DrawRoundedRectangle(g, nodeFill, nodeBorder, node.NodeRect, 20f);

            float dynamicSize = node.Address.Length >= 4 ? 7.5F : (node.Address.Length == 3 ? 8.5F : (node.Address.Length == 2 ? 9.5F : 10.5F));
            using Font dynamicAddrFont = new Font("Segoe UI", dynamicSize, FontStyle.Bold);
            g.DrawString(node.Address, dynamicAddrFont, addrText, node.NodeRect, sfAddr);

            if (!string.IsNullOrEmpty(node.Label) && node.Label != "NC" && node.Label != "Unexpected" && node.Label.Trim() != "")
            {
                using SolidBrush labelBg = new SolidBrush(isHighlight ? Color.FromArgb(254, 249, 195) : Color.FromArgb(226, 232, 240));
                using Pen labelBorder = new Pen(isHighlight ? Color.FromArgb(245, 158, 11) : Color.FromArgb(203, 213, 225), 1.5f);
                using SolidBrush labelText = new SolidBrush(Color.FromArgb(30, 41, 59));
                DrawRoundedRectangle(g, labelBg, labelBorder, node.LabelRect, 8f);
                g.DrawString(node.Label, fontLabel, labelText, node.LabelRect, sfLabel);
            }
        }

        private static void DrawRoundedRectangle(Graphics g, Brush brush, Pen pen, RectangleF rect, float radius)
        {
            using GraphicsPath path = new GraphicsPath();
            float d = radius * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            
            g.FillPath(brush, path);
            g.DrawPath(pen, path);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            PointF pt = e.Location;

            foreach (var node in _leftNodes.Concat(_rightNodes).OrderByDescending(n => n.RowIndices.Any(idx => _highlightedRows.Contains(idx))))
            {
                if (IsPointInRect(pt, node.NodeRect) || IsPointInRect(pt, node.LabelRect))
                {
                    bool isHighlighted = node.RowIndices.Any(idx => _highlightedRows.Contains(idx));
                    if (node.RowIndices.Count > 0)
                    {
                        int firstIdx = node.RowIndices.First();
                        ToggleHighlight(firstIdx);
                        DiagramRowClicked?.Invoke(firstIdx);
                    }
                    return;
                }
            }

            foreach (var conn in _connections.OrderByDescending(c => _highlightedRows.Contains(c.RowIndex)))
            {
                if (IsPointNearBezier(pt, conn.StartPoint, conn.Control1, conn.Control2, conn.EndPoint, 12f))
                {
                    ToggleHighlight(conn.RowIndex);
                    DiagramRowClicked?.Invoke(conn.RowIndex);
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

        private class DiagramNode
        {
            public List<int> RowIndices { get; set; } = new List<int>();
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
            public DiagramNode? LeftNode { get; set; }
            public DiagramNode? RightNode { get; set; }
            public bool IsError { get; set; }
        }
    }
}
