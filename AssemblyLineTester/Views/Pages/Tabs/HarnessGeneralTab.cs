using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AssemblyLineTester.Helpers;
using AssemblyLineTester.Models;
using AssemblyLineTester.Services;

namespace AssemblyLineTester.Views.Pages.Tabs
{
    public partial class HarnessGeneralTab : UserControl
    {
        private ProductHarness? _selectedHarness;
        private Connector? _selectedConnector;
        private int _genInfoRowIndex;
        
        private List<string> _harnessImagePaths = new();
        private int _harnessImageIndex = 0;
        private List<string> _genConnectorImagePaths = new();
        private int _genConnectorImageIndex = 0;
        
        private int _currentHarnessSlide = 0;
        private int _currentMachineSlide = 0;
        private const int TotalSlides = 3;
        
        private Label? _lblConnPartNumber, _lblConnContactType, _lblConnContactQty, _lblConnWireSize, _lblConnCurrentRating, _lblConnMountingStyle;
        private Image? _customHarnessImage = null;
        private Image? _customMachineImage = null;

        public HarnessGeneralTab()
        {
            InitializeComponent();
            picHarnessImage.Paint += PicHarnessImage_Paint;
            picMachineArea.Paint += PicMachineArea_Paint;
            picHarnessImage.MouseClick += PicHarnessImage_MouseClick;
            picMachineArea.MouseClick += PicMachineArea_MouseClick;
        }

        public void Initialize(EventHandler onEditHarness, EventHandler onDeleteHarness)
        {
            btnEditGeneral.Click += onEditHarness;
            btnDeleteHarness.Click += onDeleteHarness;
        }

        public void ShowNoHarnessSelectedState()
        {
            _selectedHarness = null;
            _selectedConnector = null;

            UIStyleHelper.ClearAndDispose(tblGenInfo.Controls);
            tblGenInfo.RowStyles.Clear();
            tblGenInfo.RowCount = 1;
            tblGenInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));

            var lbl = new Label
            {
                Text = "Select a harness from the list on the left to view its details.",
                Font = AssemblyLineTester.Helpers.UIStyleHelper.Font9,
                ForeColor = Color.FromArgb(100, 116, 139),
                AutoSize = true,
                Anchor = AnchorStyles.Left | AnchorStyles.Top,
                Margin = new Padding(4, 4, 4, 0)
            };
            tblGenInfo.SetColumnSpan(lbl, 2);
            tblGenInfo.Controls.Add(lbl, 0, 0);

            SetHarnessImage(new List<string>());
            SetConnectorImage(new List<string>());
        }

        public void RenderHarnessSelected(ProductHarness harness)
        {
            _selectedHarness = harness;
            _selectedConnector = null;
            _genInfoRowIndex = 0;

            UIStyleHelper.ClearAndDispose(tblGenInfo.Controls);
            tblGenInfo.RowStyles.Clear();
            tblGenInfo.RowCount = 0;

            AddGenInfoValueRow("Harness Name", harness.Name);
            AddGenInfoValueRow("Product Model", harness.ProductModel);
            AddGenInfoValueRow("Product Type", harness.ProductType);
            AddGenInfoValueRow("Area", harness.Area);
            AddGenInfoValueRow("Last Updated", harness.LastUpdate.ToString("dd MMM yyyy HH:mm"));

            AddConnectorDropdownRow(harness.ConnectorListName, harness.ConnectorList);

            _lblConnPartNumber = AddGenInfoValueRow("Part Number", "-");
            _lblConnContactType = AddGenInfoValueRow("Contact Type", "-");
            _lblConnContactQty = AddGenInfoValueRow("Contact Quantity", "-");
            _lblConnWireSize = AddGenInfoValueRow("Wire Size", "-");
            _lblConnCurrentRating = AddGenInfoValueRow("Current Rating", "-");
            _lblConnMountingStyle = AddGenInfoValueRow("Mounting Style", "-");

            SetHarnessImage(harness.ImagePaths);
            SetConnectorImage(new List<string>());
        }

        private Label AddGenInfoValueRow(string key, string value)
        {
            tblGenInfo.RowCount++;
            tblGenInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));

            var lblKey = new Label
            {
                Text = key,
                Font = AssemblyLineTester.Helpers.UIStyleHelper.Font9Bold,
                ForeColor = Color.FromArgb(71, 85, 105),
                AutoSize = true,
                Anchor = AnchorStyles.Left | AnchorStyles.Top,
                Margin = new Padding(4, 4, 4, 0)
            };
            var lblVal = new Label
            {
                Text = value,
                Font = AssemblyLineTester.Helpers.UIStyleHelper.Font9,
                ForeColor = Color.FromArgb(15, 23, 42),
                AutoSize = true,
                Anchor = AnchorStyles.Left | AnchorStyles.Top,
                Margin = new Padding(0, 4, 4, 0)
            };
            tblGenInfo.Controls.Add(lblKey, 0, _genInfoRowIndex);
            tblGenInfo.Controls.Add(lblVal, 1, _genInfoRowIndex);
            _genInfoRowIndex++;
            return lblVal;
        }

        private sealed record ConnectorOption(string Label, Connector Connector)
        {
            public override string ToString() => Label;
        }

        private void AddConnectorDropdownRow(List<string> connectorNames, List<Connector> connectors)
        {
            tblGenInfo.RowCount++;
            tblGenInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));

            var lblKey = new Label
            {
                Text = "Connector",
                Font = AssemblyLineTester.Helpers.UIStyleHelper.Font9Bold,
                ForeColor = Color.FromArgb(71, 85, 105),
                AutoSize = true,
                Anchor = AnchorStyles.Left | AnchorStyles.Top,
                Margin = new Padding(4, 4, 4, 0)
            };
            tblGenInfo.Controls.Add(lblKey, 0, _genInfoRowIndex);

            var cmb = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = AssemblyLineTester.Helpers.UIStyleHelper.Font9,
                Width = 260,
                Anchor = AnchorStyles.Left | AnchorStyles.Top,
                Margin = new Padding(0, 0, 4, 0)
            };

            if (connectors.Count == 0)
            {
                cmb.Items.Add("No connector found for this harness");
                cmb.SelectedIndex = 0;
                cmb.Enabled = false;
            }
            else
            {
                for (int i = 0; i < connectors.Count; i++)
                    cmb.Items.Add(new ConnectorOption(connectorNames[i], connectors[i]));

                cmb.SelectedIndexChanged += (s, e) =>
                {
                    if (s is ComboBox combo && combo.SelectedItem is ConnectorOption opt)
                        OnConnectorSelected(opt.Connector);
                };
            }

            tblGenInfo.Controls.Add(cmb, 1, _genInfoRowIndex);
            _genInfoRowIndex++;
        }

        private void OnConnectorSelected(Connector? conn)
        {
            _selectedConnector = conn;
            if (conn != null)
            {
                _lblConnPartNumber!.Text = conn.PartNumber;
                _lblConnContactType!.Text = conn.ContactType;
                _lblConnContactQty!.Text = conn.ContactQuantity.ToString();
                _lblConnWireSize!.Text = conn.WireSize;
                _lblConnCurrentRating!.Text = $"{conn.CurrentRating} A";
                _lblConnMountingStyle!.Text = conn.MountingStyle;
                SetConnectorImage(ConnectorService.GetConnectorImagePaths(conn));
            }
            else
            {
                _lblConnPartNumber!.Text = "-";
                _lblConnContactType!.Text = "-";
                _lblConnContactQty!.Text = "-";
                _lblConnWireSize!.Text = "-";
                _lblConnCurrentRating!.Text = "-";
                _lblConnMountingStyle!.Text = "-";
                SetConnectorImage(new List<string>());
            }
        }

        private void SetHarnessImage(List<string> relativePaths)
        {
            _harnessImagePaths = relativePaths.Select(ResolveHarnessImagePath).Where(p => p != null).Select(p => p!).ToList();
            _harnessImageIndex = 0;
            LoadGalleryImage(picHarnessImage, _harnessImagePaths, _harnessImageIndex);
        }

        private void SetConnectorImage(List<string> fullPaths)
        {
            _genConnectorImagePaths = fullPaths;
            _genConnectorImageIndex = 0;
            LoadGalleryImage(picMachineArea, _genConnectorImagePaths, _genConnectorImageIndex);
        }

        private static string? ResolveHarnessImagePath(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath)) return null;

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string candidateBase = Path.Combine(baseDir, relativePath);
            if (File.Exists(candidateBase)) return candidateBase;

            if (File.Exists(relativePath)) return Path.GetFullPath(relativePath);

            string imagesFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "AssemblyLineTester", "Images");
            string candidateAppData = Path.Combine(imagesFolder, relativePath);
            return File.Exists(candidateAppData) ? candidateAppData : null;
        }

        private static void LoadGalleryImage(PictureBox pic, List<string> paths, int index)
        {
            var oldImage = pic.Image;
            pic.Image = null;
            oldImage?.Dispose();

            if (paths.Count > 0 && index >= 0 && index < paths.Count)
            {
                try
                {
                    pic.SizeMode = PictureBoxSizeMode.Zoom;
                    pic.Image = Image.FromFile(paths[index]);
                }
                catch (Exception ex)
                {
                    FileLogService.Warn("HarnessDatabase.ShowImage", $"Image '{paths[index]}' could not be loaded: {ex.Message}");
                }
            }

            pic.Invalidate();
        }

        private static bool HandleGalleryClick(Point location, int controlWidth, int count, ref int index)
        {
            const int arrowZoneWidth = 50;
            if (location.X <= arrowZoneWidth)
            {
                index = (index - 1 + count) % count;
                return true;
            }
            if (location.X >= controlWidth - arrowZoneWidth)
            {
                index = (index + 1) % count;
                return true;
            }
            return false;
        }

        private static void DrawGalleryUI(Graphics g, Rectangle rect, int index, int count)
        {
            int arrowZoneWidth = 50;
            using var arrowBgBrush = new SolidBrush(Color.FromArgb(10, 0, 0, 0));
            g.FillRectangle(arrowBgBrush, 0, 0, arrowZoneWidth, rect.Height);
            g.FillRectangle(arrowBgBrush, rect.Width - arrowZoneWidth, 0, arrowZoneWidth, rect.Height);

            using var arrowPen = new Pen(Color.FromArgb(100, 116, 139), 3);
            arrowPen.StartCap = LineCap.Round;
            arrowPen.EndCap = LineCap.Round;
            arrowPen.LineJoin = LineJoin.Round;

            int cy = rect.Height / 2;
            int arrowSize = 12;

            int leftX = 20;
            g.DrawLines(arrowPen, new Point[] { new Point(leftX + arrowSize, cy - arrowSize), new Point(leftX, cy), new Point(leftX + arrowSize, cy + arrowSize) });

            int rightX = rect.Width - 20 - arrowSize;
            g.DrawLines(arrowPen, new Point[] { new Point(rightX, cy - arrowSize), new Point(rightX + arrowSize, cy), new Point(rightX, cy + arrowSize) });

            string counter = $"{index + 1} / {count}";
            var counterFont = AssemblyLineTester.Helpers.UIStyleHelper.Font9Bold;
            var counterSize = g.MeasureString(counter, counterFont);
            var counterRect = new RectangleF(rect.Width - counterSize.Width - 22, rect.Height - counterSize.Height - 16, counterSize.Width + 12, counterSize.Height + 6);
            using var counterBg = new SolidBrush(Color.FromArgb(160, 15, 23, 42));
            g.FillRectangle(counterBg, counterRect);
            g.DrawString(counter, counterFont, Brushes.White, counterRect.X + 6, counterRect.Y + 3);
        }

        private void PicHarnessImage_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Control ctrl || e.Graphics == null) return;
            if (ctrl is PictureBox pic && pic.Image != null)
            {
                if (_harnessImagePaths.Count > 1)
                    DrawGalleryUI(e.Graphics, ctrl.ClientRectangle, _harnessImageIndex, _harnessImagePaths.Count);
                return;
            }
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = ctrl.ClientRectangle;
            if (rect.Width <= 1 || rect.Height <= 1) return;

            using var penBorder = new Pen(Color.FromArgb(148, 163, 184), 1.5f);
            e.Graphics.DrawRectangle(penBorder, 0, 0, rect.Width - 1, rect.Height - 1);

            if (_customHarnessImage != null)
            {
                var ratio = Math.Min((float)rect.Width / _customHarnessImage.Width, (float)rect.Height / _customHarnessImage.Height);
                var w = _customHarnessImage.Width * ratio;
                var h = _customHarnessImage.Height * ratio;
                var x = (rect.Width - w) / 2;
                var y = (rect.Height - h) / 2;
                e.Graphics.DrawImage(_customHarnessImage, x, y, w, h);
                return;
            }

            int cy = rect.Height / 2;
            using var brushConn = new SolidBrush(Color.FromArgb(30, 41, 59));

            if (_currentHarnessSlide == 0)
            {
                using var penCable = new Pen(Color.FromArgb(51, 65, 85), 4);
                using var penBranch1 = new Pen(Color.FromArgb(239, 68, 68), 3);
                using var penBranch2 = new Pen(Color.FromArgb(59, 130, 246), 3);

                e.Graphics.DrawLine(penCable, rect.Width * 0.15f, cy, rect.Width * 0.7f, cy);
                e.Graphics.DrawLine(penBranch1, rect.Width * 0.45f, cy, rect.Width * 0.65f, cy - (rect.Height * 0.25f));
                e.Graphics.DrawLine(penBranch2, rect.Width * 0.55f, cy, rect.Width * 0.75f, cy + (rect.Height * 0.25f));

                e.Graphics.FillRectangle(brushConn, rect.Width * 0.1f, cy - 12, 36, 24);
                e.Graphics.FillRectangle(brushConn, rect.Width * 0.68f, cy - 10, 28, 20);
                e.Graphics.FillRectangle(brushConn, rect.Width * 0.63f, cy - (rect.Height * 0.25f) - 10, 24, 18);
                e.Graphics.FillRectangle(brushConn, rect.Width * 0.73f, cy + (rect.Height * 0.25f) - 8, 24, 18);
            }
            else if (_currentHarnessSlide == 1)
            {
                using var penCable = new Pen(Color.FromArgb(51, 65, 85), 4);
                using var penBranch = new Pen(Color.FromArgb(22, 163, 74), 3);

                e.Graphics.DrawLine(penCable, rect.Width * 0.2f, cy - 50, rect.Width * 0.8f, cy - 50);
                e.Graphics.DrawLine(penBranch, rect.Width * 0.5f, cy - 50, rect.Width * 0.5f, cy + 80);

                e.Graphics.FillRectangle(brushConn, rect.Width * 0.15f, cy - 62, 36, 24);
                e.Graphics.FillRectangle(brushConn, rect.Width * 0.78f, cy - 62, 36, 24);
                e.Graphics.FillRectangle(brushConn, rect.Width * 0.47f, cy + 80, 24, 30);
            }
            else
            {
                using var penCable = new Pen(Color.FromArgb(51, 65, 85), 4);
                e.Graphics.DrawLine(penCable, rect.Width * 0.25f, cy, rect.Width * 0.75f, cy);
                e.Graphics.FillRectangle(brushConn, rect.Width * 0.2f, cy - 20, 40, 40);
                e.Graphics.FillRectangle(brushConn, rect.Width * 0.72f, cy - 20, 40, 40);
            }

            DrawCarouselUI(e.Graphics, rect, _currentHarnessSlide);
        }

        private void PicMachineArea_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Control ctrl || e.Graphics == null) return;
            if (ctrl is PictureBox pic && pic.Image != null)
            {
                if (_genConnectorImagePaths.Count > 1)
                    DrawGalleryUI(e.Graphics, ctrl.ClientRectangle, _genConnectorImageIndex, _genConnectorImagePaths.Count);
                return;
            }
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = ctrl.ClientRectangle;
            if (rect.Width <= 1 || rect.Height <= 1) return;

            using var penBorder = new Pen(Color.FromArgb(148, 163, 184), 1.5f);
            e.Graphics.DrawRectangle(penBorder, 0, 0, rect.Width - 1, rect.Height - 1);

            if (_customMachineImage != null)
            {
                var ratio = Math.Min((float)rect.Width / _customMachineImage.Width, (float)rect.Height / _customMachineImage.Height);
                var w = _customMachineImage.Width * ratio;
                var h = _customMachineImage.Height * ratio;
                var x = (rect.Width - w) / 2;
                var y = (rect.Height - h) / 2;
                e.Graphics.DrawImage(_customMachineImage, x, y, w, h);
                return;
            }

            using var penBox = new Pen(Color.FromArgb(148, 163, 184), 2) { DashStyle = DashStyle.Dash };
            var font = AssemblyLineTester.Helpers.UIStyleHelper.Font9Bold;

            if (_currentMachineSlide == 0)
            {
                var areaRect = new Rectangle((int)(rect.Width * 0.2f), (int)(rect.Height * 0.2f), (int)(rect.Width * 0.6f), (int)(rect.Height * 0.6f));
                e.Graphics.DrawRectangle(penBox, areaRect);

                using var brushHighlight = new SolidBrush(Color.FromArgb(60, 250, 204, 21));
                e.Graphics.FillRectangle(brushHighlight, (int)(rect.Width * 0.4f), (int)(rect.Height * 0.35f), (int)(rect.Width * 0.25f), (int)(rect.Height * 0.3f));

                TextRenderer.DrawText(e.Graphics, "ENGINE ROOM - ZONE A2", font, areaRect, Color.FromArgb(71, 85, 105), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
            else if (_currentMachineSlide == 1)
            {
                var areaRect = new Rectangle((int)(rect.Width * 0.1f), (int)(rect.Height * 0.1f), (int)(rect.Width * 0.4f), (int)(rect.Height * 0.8f));
                e.Graphics.DrawRectangle(penBox, areaRect);

                using var brushHighlight = new SolidBrush(Color.FromArgb(60, 59, 130, 246));
                e.Graphics.FillRectangle(brushHighlight, (int)(rect.Width * 0.15f), (int)(rect.Height * 0.2f), (int)(rect.Width * 0.3f), (int)(rect.Height * 0.2f));

                TextRenderer.DrawText(e.Graphics, "CABIN MAIN CONTROL", font, areaRect, Color.FromArgb(71, 85, 105), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
            else
            {
                var areaRect = new Rectangle((int)(rect.Width * 0.5f), (int)(rect.Height * 0.3f), (int)(rect.Width * 0.4f), (int)(rect.Height * 0.4f));
                e.Graphics.DrawRectangle(penBox, areaRect);

                using var brushHighlight = new SolidBrush(Color.FromArgb(60, 239, 68, 68));
                e.Graphics.FillRectangle(brushHighlight, (int)(rect.Width * 0.55f), (int)(rect.Height * 0.4f), (int)(rect.Width * 0.3f), (int)(rect.Height * 0.2f));

                TextRenderer.DrawText(e.Graphics, "HYDRAULIC PUMP C", font, areaRect, Color.FromArgb(71, 85, 105), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }

            DrawCarouselUI(e.Graphics, rect, _currentMachineSlide);
        }

        private void DrawCarouselUI(Graphics g, Rectangle rect, int currentSlide)
        {
            int arrowZoneWidth = 50;
            using var arrowBgBrush = new SolidBrush(Color.FromArgb(10, 0, 0, 0));
            g.FillRectangle(arrowBgBrush, 0, 0, arrowZoneWidth, rect.Height);
            g.FillRectangle(arrowBgBrush, rect.Width - arrowZoneWidth, 0, arrowZoneWidth, rect.Height);

            using var arrowPen = new Pen(Color.FromArgb(100, 116, 139), 3);
            arrowPen.StartCap = LineCap.Round;
            arrowPen.EndCap = LineCap.Round;
            arrowPen.LineJoin = LineJoin.Round;

            int cy = rect.Height / 2;
            int arrowSize = 12;

            int leftX = 20;
            g.DrawLines(arrowPen, new Point[] { new Point(leftX + arrowSize, cy - arrowSize), new Point(leftX, cy), new Point(leftX + arrowSize, cy + arrowSize) });

            int rightX = rect.Width - 20 - arrowSize;
            g.DrawLines(arrowPen, new Point[] { new Point(rightX, cy - arrowSize), new Point(rightX + arrowSize, cy), new Point(rightX, cy + arrowSize) });

            int dotSize = 8;
            int dotSpacing = 8;
            int totalDotsWidth = (TotalSlides * dotSize) + ((TotalSlides - 1) * dotSpacing);
            int startX = (rect.Width - totalDotsWidth) / 2;
            int dotY = rect.Height - 20;

            for (int i = 0; i < TotalSlides; i++)
            {
                var dotRect = new Rectangle(startX + (i * (dotSize + dotSpacing)), dotY, dotSize, dotSize);
                if (i == currentSlide)
                {
                    using var activeBrush = new SolidBrush(Color.FromArgb(250, 204, 21));
                    g.FillEllipse(activeBrush, dotRect);
                }
                else
                {
                    using var inactiveBrush = new SolidBrush(Color.FromArgb(203, 213, 225));
                    g.FillEllipse(inactiveBrush, dotRect);
                }
            }
        }

        private void HandleCarouselClick(Point clickLocation, int controlWidth, ref int currentSlide)
        {
            int arrowZoneWidth = 50;
            if (clickLocation.X <= arrowZoneWidth)
                currentSlide = (currentSlide - 1 + TotalSlides) % TotalSlides;
            else if (clickLocation.X >= controlWidth - arrowZoneWidth)
                currentSlide = (currentSlide + 1) % TotalSlides;
        }

        private void PicHarnessImage_MouseClick(object? sender, MouseEventArgs e)
        {
            if (sender is not Control ctrl) return;
            if (picHarnessImage.Image != null)
            {
                if (_harnessImagePaths.Count <= 1) return;
                if (!HandleGalleryClick(e.Location, ctrl.Width, _harnessImagePaths.Count, ref _harnessImageIndex)) return;
                LoadGalleryImage(picHarnessImage, _harnessImagePaths, _harnessImageIndex);
                return;
            }
            HandleCarouselClick(e.Location, ctrl.Width, ref _currentHarnessSlide);
            ctrl.Invalidate();
        }

        private void PicMachineArea_MouseClick(object? sender, MouseEventArgs e)
        {
            if (sender is not Control ctrl) return;
            if (picMachineArea.Image != null)
            {
                if (_genConnectorImagePaths.Count <= 1) return;
                if (!HandleGalleryClick(e.Location, ctrl.Width, _genConnectorImagePaths.Count, ref _genConnectorImageIndex)) return;
                LoadGalleryImage(picMachineArea, _genConnectorImagePaths, _genConnectorImageIndex);
                return;
            }
            HandleCarouselClick(e.Location, ctrl.Width, ref _currentMachineSlide);
            ctrl.Invalidate();
        }
    }
}
