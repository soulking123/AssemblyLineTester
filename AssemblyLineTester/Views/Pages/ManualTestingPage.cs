using AssemblyLineTester.Services;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace AssemblyLineTester.Views.Pages
{
    public partial class ManualTestingPage : UserControl
    {
        private readonly HashSet<int> highlightedRows = new();
        private readonly List<DiagramNode> leftNodes = new();
        private readonly List<DiagramNode> rightNodes = new();
        private readonly List<DiagramConnection> connections = new();

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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode && System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                LoadDefaultData();
                UpdateTotalNetCount();
                UpdateDiagramSize();
                picPreview.Invalidate();
            }
        }

        private void SetupEvents()
        {
            btnAdd.Click += BtnAdd_Click;
            btnRemove.Click += BtnRemove_Click;
            btnReset.Click += BtnReset_Click;
            btnExport.Click += (s, e) => MessageBox.Show("Export feature not implemented yet.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnImport.Click += (s, e) => MessageBox.Show("Import feature not implemented yet.", "Import", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnLearnConnection.Click += (s, e) => MessageBox.Show("Learning connection...", "Learn Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnStart.Click += (s, e) => MessageBox.Show("Starting manual test...", "Start Test", MessageBoxButtons.OK, MessageBoxIcon.Information);

            gridConfig.CellValueChanged += GridConfig_CellValueChanged;
            gridConfig.RowsRemoved += GridConfig_RowsChanged;
            gridConfig.RowsAdded += GridConfig_RowsChanged;
            gridConfig.CellClick += GridConfig_CellClick;

            picPreview.Paint += PicPreview_Paint;
            picPreview.MouseDown += PicPreview_MouseDown;
            picPreview.Resize += (s, e) => picPreview.Invalidate();
            pnlPreviewScroll.Resize += (s, e) => { UpdateDiagramSize(); picPreview.Invalidate(); };
            splitContainerMain.SplitterMoved += (s, e) => { UpdateDiagramSize(); picPreview.Invalidate(); };
        }

        public ManualTestingPage()
        {
            InitializeComponent();
            EnableDoubleBuffering();
            SetupEvents();
        }

        private void EnableDoubleBuffering()
        {
            try
            {
                typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(gridConfig, true, null);
                typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(splitContainerMain, true, null);
                typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(picPreview, true, null);
            }
            catch { /* ignore */ }
        }

        private void LoadDefaultData()
        {
            gridConfig.Rows.Clear();
            highlightedRows.Clear();

            // Default Data: No, From, To, Measurement
            gridConfig.Rows.Add("1", "X1-01", "Y1-01", "Ω");
            gridConfig.Rows.Add("2", "X2-01", "Y2-01", "Ω");
            gridConfig.Rows.Add("3", "X2-02", "Y2-02", "μF");
            gridConfig.Rows.Add("4", "X1-02", "Y1-02", "Ω");

            RenumberRows();
            UpdateTotalNetCount();
            UpdateDiagramSize();
            picPreview.Invalidate();
        }

        private void RenumberRows()
        {
            for (int i = 0; i < gridConfig.Rows.Count; i++)
            {
                gridConfig.Rows[i].Cells["colNo"].Value = (i + 1).ToString();
            }
        }

        private void UpdateTotalNetCount()
        {
            btnTotalNet.Text = $"Total Net: {gridConfig.Rows.Count}";
        }

        private void UpdateDiagramSize()
        {
            int rowCount = gridConfig.Rows.Count;
            float minRowSpacing = 55f;
            int reqHeight = (int)(60 + Math.Max(rowCount, 1) * minRowSpacing + 60);
            int finalHeight = Math.Max(pnlPreviewScroll.ClientSize.Height, reqHeight);
            int finalWidth = Math.Max(pnlPreviewScroll.ClientSize.Width - (reqHeight > pnlPreviewScroll.ClientSize.Height ? 25 : 0), 500);

            if (picPreview.Width != finalWidth || picPreview.Height != finalHeight)
            {
                picPreview.Size = new Size(finalWidth, finalHeight);
            }
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            int newIdx = gridConfig.Rows.Count + 1;
            gridConfig.Rows.Add(newIdx.ToString(), $"X1-{newIdx:D2}", $"Y1-{newIdx:D2}", "Ω");
            RenumberRows();
            UpdateTotalNetCount();
            UpdateDiagramSize();
            picPreview.Invalidate();
        }

        private void BtnRemove_Click(object? sender, EventArgs e)
        {
            if (gridConfig.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in gridConfig.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        highlightedRows.Remove(row.Index);
                        gridConfig.Rows.Remove(row);
                    }
                }
            }
            else if (gridConfig.CurrentRow != null && !gridConfig.CurrentRow.IsNewRow)
            {
                highlightedRows.Remove(gridConfig.CurrentRow.Index);
                gridConfig.Rows.Remove(gridConfig.CurrentRow);
            }

            RenumberRows();
            UpdateTotalNetCount();
            UpdateDiagramSize();
            picPreview.Invalidate();
        }

        private void BtnReset_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete all rows?", "Confirm Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                gridConfig.Rows.Clear();
                highlightedRows.Clear();
                UpdateTotalNetCount();
                UpdateDiagramSize();
                picPreview.Invalidate();
            }
        }

        private void GridConfig_RowsChanged(object? sender, EventArgs e)
        {
            UpdateTotalNetCount();
            UpdateDiagramSize();
            picPreview.Invalidate();
        }

        private void GridConfig_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            picPreview.Invalidate();
        }

        private void GridConfig_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= gridConfig.Rows.Count) return;

            if (e.ColumnIndex == 0 || e.ColumnIndex == -1)
            {
                ToggleHighlight(e.RowIndex);
            }
        }

        private void ToggleHighlight(int rowIndex)
        {
            if (!highlightedRows.Remove(rowIndex))
            {
                highlightedRows.Add(rowIndex);
            }

            UpdateGridHighlights();
            picPreview.Invalidate();
        }

        private void UpdateGridHighlights()
        {
            for (int i = 0; i < gridConfig.Rows.Count; i++)
            {
                DataGridViewRow row = gridConfig.Rows[i];
                if (highlightedRows.Contains(i))
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(254, 249, 195);
                    row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(253, 224, 71);
                    row.DefaultCellStyle.SelectionForeColor = Color.Black;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                    row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(226, 232, 240);
                    row.DefaultCellStyle.SelectionForeColor = Color.Black;
                }
            }
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
        }

        private static bool IsPointInRect(PointF pt, RectangleF rect)
        {
            return pt.X >= rect.Left && pt.X <= rect.Right && pt.Y >= rect.Top && pt.Y <= rect.Bottom;
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

            int rowCount = gridConfig.Rows.Count;
            if (rowCount == 0) return;

            float availHeight = r.Height - 100;
            float startY = 50f;
            float rowSpacing = Math.Max(55f, availHeight / Math.Max(rowCount, 1));

            List<DiagramNode> rawLeftNodes = new List<DiagramNode>();
            List<DiagramNode> rawRightNodes = new List<DiagramNode>();

            for (int i = 0; i < rowCount; i++)
            {
                DataGridViewRow row = gridConfig.Rows[i];
                string fromAddr = row.Cells["colFromAddr"].Value?.ToString() ?? "0";
                string fromLabel = fromAddr; // Use same for label
                string toAddr = row.Cells["colToAddr"].Value?.ToString() ?? "0";
                string toLabel = toAddr; // Use same for label

                DiagramNode lNode = new() { RowIndex = i, Address = fromAddr, Label = fromLabel, IsLeft = true };
                rawLeftNodes.Add(lNode);

                DiagramNode rNode = new() { RowIndex = i, Address = toAddr, Label = toLabel, IsLeft = false };
                rawRightNodes.Add(rNode);
            }

            var sortedLeftNodes = rawLeftNodes.OrderBy(n => int.TryParse(n.Address, out int val) ? val : n.Address.GetHashCode()).ToList();
            for (int idx = 0; idx < sortedLeftNodes.Count; idx++)
            {
                float yPos = startY + idx * rowSpacing;
                var lNode = sortedLeftNodes[idx];
                lNode.NodeRect = new(185, yPos - 22, 50, 44);
                lNode.LabelRect = new(45, yPos - 18, 132, 36);
                leftNodes.Add(lNode);
            }

            var sortedRightNodes = rawRightNodes.OrderBy(n => int.TryParse(n.Address, out int val) ? val : n.Address.GetHashCode()).ToList();
            for (int idx = 0; idx < sortedRightNodes.Count; idx++)
            {
                float yPos = startY + idx * rowSpacing;
                var rNode = sortedRightNodes[idx];
                rNode.NodeRect = new(r.Width - 235, yPos - 22, 50, 44);
                rNode.LabelRect = new(r.Width - 177, yPos - 18, 132, 36);
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

                    float dist = Math.Abs(endPt.X - startPt.X);
                    float ctlXOffset = Math.Max(dist * 0.4f, 40f);
                    PointF c1 = new(startPt.X + ctlXOffset, startPt.Y);
                    PointF c2 = new(endPt.X - ctlXOffset, endPt.Y);

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

            using Pen wirePen = new(Color.FromArgb(148, 163, 184), 2.5f);
            using Pen activeWirePen = new(Color.FromArgb(34, 197, 94), 3.5f);
            foreach (var conn in connections.OrderBy(c => highlightedRows.Contains(c.RowIndex)))
            {
                Pen p = highlightedRows.Contains(conn.RowIndex) ? activeWirePen : wirePen;
                g.DrawBezier(p, conn.StartPoint, conn.Control1, conn.Control2, conn.EndPoint);
            }
            
            using Font fontLabel = new("Segoe UI", 9F, FontStyle.Regular);
            using Font fontAddr = new("Segoe UI", 10.5F, FontStyle.Bold);
            using StringFormat sfLabel = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter };
            using StringFormat sfAddr = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

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
                DrawNodeAndLabel(g, node, highlightedRows.Contains(node.RowIndex), fontLabel, fontAddr, sfLabel, sfAddr);
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
