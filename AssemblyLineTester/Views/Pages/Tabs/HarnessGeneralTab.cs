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

        private Label? _lblConnPartNumber, _lblConnContactType, _lblConnContactQty, _lblConnWireSize, _lblConnCurrentRating, _lblConnMountingStyle;

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

            DrawNoImageState(e.Graphics, ctrl.ClientRectangle, "No images uploaded for this harness.");
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

            string message = _selectedConnector == null
                ? "Select a connector to view its images."
                : "No images uploaded for this connector.";
            DrawNoImageState(e.Graphics, ctrl.ClientRectangle, message);
        }

        // What both galleries fall back to once there is nothing to page through - the honest
        // state, not the placeholder diagrams the mockup shipped with.
        private static void DrawNoImageState(Graphics g, Rectangle rect, string message)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            if (rect.Width <= 1 || rect.Height <= 1) return;

            using var penBorder = new Pen(Color.FromArgb(148, 163, 184), 1.5f);
            g.DrawRectangle(penBorder, 0, 0, rect.Width - 1, rect.Height - 1);

            using var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g.DrawString(message, AssemblyLineTester.Helpers.UIStyleHelper.Font10Bold, Brushes.Gray, rect, sf);
        }

        private void PicHarnessImage_MouseClick(object? sender, MouseEventArgs e)
        {
            if (sender is not Control ctrl) return;
            if (picHarnessImage.Image == null || _harnessImagePaths.Count <= 1) return;
            if (!HandleGalleryClick(e.Location, ctrl.Width, _harnessImagePaths.Count, ref _harnessImageIndex)) return;
            LoadGalleryImage(picHarnessImage, _harnessImagePaths, _harnessImageIndex);
        }

        private void PicMachineArea_MouseClick(object? sender, MouseEventArgs e)
        {
            if (sender is not Control ctrl) return;
            if (picMachineArea.Image == null || _genConnectorImagePaths.Count <= 1) return;
            if (!HandleGalleryClick(e.Location, ctrl.Width, _genConnectorImagePaths.Count, ref _genConnectorImageIndex)) return;
            LoadGalleryImage(picMachineArea, _genConnectorImagePaths, _genConnectorImageIndex);
        }
    }
}
