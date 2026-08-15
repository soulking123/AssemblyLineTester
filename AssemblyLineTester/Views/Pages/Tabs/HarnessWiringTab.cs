using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace AssemblyLineTester.Views.Pages.Tabs
{
    public partial class HarnessWiringTab : UserControl
    {
        private HashSet<int> highlightedRows = new HashSet<int>();
        private List<DiagramNode> leftNodes = new List<DiagramNode>();
        private List<DiagramNode> rightNodes = new List<DiagramNode>();
        private List<DiagramConnection> connections = new List<DiagramConnection>();

        public HarnessWiringTab()
        {
            InitializeComponent();

            picDiagram.Paint += PicDiagram_Paint;
            gridWiring.SelectionChanged += GridWiring_SelectionChanged;
            gridWiring.CellValueChanged += GridWiring_CellValueChanged;
            gridWiring.RowsAdded += GridWiring_RowsChanged;
            gridWiring.RowsRemoved += GridWiring_RowsChanged;
            gridWiring.DataError += GridWiring_DataError;
            
            // Add valid items to the ComboBox column before adding rows
            colTestType.Items.Add("Continuity");
            colTestType.Items.Add("Resistance");
            colTestType.Items.Add("Hipot");
            colTestType.Items.Add("IR");

            // Add some dummy data to preview
            gridWiring.Rows.Add("NEW_NET", "1", "2", "Continuity", "Pass");
            gridWiring.Rows.Add("NET_2", "3", "4", "Resistance", "0.5");
        }

        private void GridWiring_DataError(object? sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void GridWiring_SelectionChanged(object? sender, EventArgs e)
        {
            highlightedRows.Clear();
            foreach (DataGridViewRow row in gridWiring.SelectedRows)
            {
                if (!row.IsNewRow)
                    highlightedRows.Add(row.Index);
            }
            picDiagram.Invalidate();
        }

        private void GridWiring_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            picDiagram.Invalidate();
        }

        private void GridWiring_RowsChanged(object? sender, DataGridViewRowsAddedEventArgs e)
        {
            picDiagram.Invalidate();
        }

        private void GridWiring_RowsChanged(object? sender, DataGridViewRowsRemovedEventArgs e)
        {
            picDiagram.Invalidate();
        }

        private void PicDiagram_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Rectangle r = picDiagram.ClientRectangle;

            using SolidBrush bgBrush = new SolidBrush(Color.FromArgb(248, 250, 252));
            g.FillRectangle(bgBrush, r);

            using SolidBrush dotBrush = new SolidBrush(Color.FromArgb(226, 232, 240));
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

            int rowCount = gridWiring.Rows.Count;
            if (rowCount == 0) return;

            float availHeight = r.Height - 100;
            float startY = 50f;
            int validRows = gridWiring.Rows.Cast<DataGridViewRow>().Count(row => !row.IsNewRow);
            if (validRows == 0) return;
            float rowSpacing = Math.Max(55f, availHeight / Math.Max(validRows, 1));

            int maxPin = 0;
            var allNodesDict = new Dictionary<string, DiagramNode>();

            for (int i = 0; i < rowCount; i++)
            {
                DataGridViewRow row = gridWiring.Rows[i];
                if (row.IsNewRow) continue;

                string fromAddr = row.Cells["colFromAddr"].Value?.ToString() ?? "0";
                string fromLabel = fromAddr; 
                string toAddr = row.Cells["colToAddr"].Value?.ToString() ?? "0";
                string toLabel = toAddr; 

                void ProcessNode(string addr, string label)
                {
                    if (string.IsNullOrEmpty(addr) || addr == "0") return;

                    if (!allNodesDict.TryGetValue(addr, out DiagramNode n))
                    {
                        n = new DiagramNode { RowIndices = [i], Address = addr, Label = label };
                        allNodesDict[addr] = n;
                    }
                    else
                    {
                        if (!n.RowIndices.Contains(i)) n.RowIndices.Add(i);
                        if ((string.IsNullOrEmpty(n.Label) || n.Label == "NC" || n.Label == "Unexpected") &&
                            !string.IsNullOrEmpty(label) && label != "NC" && label != "Unexpected")
                        {
                            n.Label = label;
                        }
                    }

                    if (int.TryParse(addr, out int num) && num > maxPin)
                        maxPin = num;
                }

                ProcessNode(fromAddr, fromLabel);
                ProcessNode(toAddr, toLabel);
            }

            int splitPoint = 32;
            if (maxPin > 128) splitPoint = 128;
            else if (maxPin > 64) splitPoint = 64;

            var sortedUniqueNodes = allNodesDict.Values.OrderBy(n => int.TryParse(n.Address, out int val) ? val : n.Address.GetHashCode()).ToList();

            List<DiagramNode> rawLeftNodes = new List<DiagramNode>();
            List<DiagramNode> rawRightNodes = new List<DiagramNode>();

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

            float rowSpacingLeft = Math.Max(55f, availHeight / Math.Max(rawLeftNodes.Count, 1));
            for (int idx = 0; idx < rawLeftNodes.Count; idx++)
            {
                float yPos = startY + idx * rowSpacingLeft;
                var lNode = rawLeftNodes[idx];
                lNode.NodeRect = new RectangleF(100, yPos - 22, 50, 44);
                lNode.LabelRect = new RectangleF(20, yPos - 18, 70, 36);
                leftNodes.Add(lNode);
            }

            float rowSpacingRight = Math.Max(55f, availHeight / Math.Max(rawRightNodes.Count, 1));
            for (int idx = 0; idx < rawRightNodes.Count; idx++)
            {
                float yPos = startY + idx * rowSpacingRight;
                var rNode = rawRightNodes[idx];
                rNode.NodeRect = new RectangleF(r.Width - 150, yPos - 22, 50, 44);
                rNode.LabelRect = new RectangleF(r.Width - 90, yPos - 18, 70, 36);
                rightNodes.Add(rNode);
            }

            for (int i = 0; i < rowCount; i++)
            {
                DataGridViewRow row = gridWiring.Rows[i];
                if (row.IsNewRow) continue;

                string fromAddr = row.Cells["colFromAddr"].Value?.ToString() ?? "0";
                string toAddr = row.Cells["colToAddr"].Value?.ToString() ?? "0";

                var lNode = leftNodes.Concat(rightNodes).FirstOrDefault(n => n.Address == fromAddr);
                var rNode = leftNodes.Concat(rightNodes).FirstOrDefault(n => n.Address == toAddr);

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
                        float offset = Math.Max(Math.Abs(endPt.Y - startPt.Y) * 0.8f, 60f);
                        c1 = new PointF(startPt.X + offset, startPt.Y);
                        c2 = new PointF(endPt.X + offset, endPt.Y);
                    }
                    else if (!lNode.IsLeft && !rNode.IsLeft)
                    {
                        startPt = new PointF(lNode.NodeRect.Left, lNode.NodeRect.Top + lNode.NodeRect.Height / 2);
                        endPt = new PointF(rNode.NodeRect.Left, rNode.NodeRect.Top + rNode.NodeRect.Height / 2);
                        float offset = Math.Max(Math.Abs(endPt.Y - startPt.Y) * 0.8f, 60f);
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

                    connections.Add(new DiagramConnection
                    {
                        RowIndex = i,
                        StartPoint = startPt,
                        EndPoint = endPt,
                        Control1 = c1,
                        Control2 = c2,
                        LeftNode = lNode,
                        RightNode = rNode
                    });
                }
            }

            using Pen wirePen = new Pen(Color.FromArgb(148, 163, 184), 2.5f);
            using Pen activeWirePen = new Pen(Color.FromArgb(34, 197, 94), 3.5f);
            foreach (var conn in connections.OrderBy(c => highlightedRows.Contains(c.RowIndex)))
            {
                Pen p = highlightedRows.Contains(conn.RowIndex) ? activeWirePen : wirePen;
                g.DrawBezier(p, conn.StartPoint, conn.Control1, conn.Control2, conn.EndPoint);
            }

            using Font fontLabel = new Font("Segoe UI", 9F, FontStyle.Regular);
            using Font fontAddr = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            using StringFormat sfLabel = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter };
            using StringFormat sfAddr = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

            using Pen glowPen = new Pen(Color.FromArgb(254, 240, 138), 11f) { StartCap = LineCap.Round, EndCap = LineCap.Round };
            using Pen innerPen = new Pen(Color.FromArgb(13, 110, 253), 3.5f) { StartCap = LineCap.Round, EndCap = LineCap.Round };
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
                DrawNodeAndLabel(g, node, node.RowIndices.Any(idx => highlightedRows.Contains(idx)), fontLabel, fontAddr, sfLabel, sfAddr);
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

            if (!string.IsNullOrEmpty(node.Label) && node.Label != "NC" && node.Label != "Unexpected" && node.Label.Trim() != "")
            {
                using SolidBrush labelBg = new SolidBrush(isHighlight ? Color.FromArgb(254, 249, 195) : Color.FromArgb(226, 232, 240));
                using Pen labelBorder = new Pen(isHighlight ? Color.FromArgb(245, 158, 11) : Color.FromArgb(203, 213, 225), 1.5f);
                using SolidBrush labelText = new SolidBrush(Color.FromArgb(30, 41, 59));
                DrawRoundedRectangle(g, labelBg, labelBorder, node.LabelRect, 8f);
                g.DrawString(node.Label, fontLabel, labelText, node.LabelRect, sfLabel);
            }

            Color nodeColor = node.IsLeft ? Color.FromArgb(0, 180, 216) : Color.FromArgb(74, 222, 128);
            using SolidBrush nodeFill = new SolidBrush(nodeColor);
            using Pen nodeBorder = new Pen(Color.FromArgb(15, 23, 42), 2f);
            using SolidBrush addrText = new SolidBrush(Color.FromArgb(15, 23, 42));
            DrawRoundedRectangle(g, nodeFill, nodeBorder, node.NodeRect, 20f);

            float dynamicSize = node.Address.Length >= 4 ? 7.5F : (node.Address.Length == 3 ? 8.5F : (node.Address.Length == 2 ? 9.5F : 10.5F));
            using Font dynamicAddrFont = new Font("Segoe UI", dynamicSize, FontStyle.Bold);
            g.DrawString(node.Address, dynamicAddrFont, addrText, node.NodeRect, sfAddr);
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
        }
    }
}
