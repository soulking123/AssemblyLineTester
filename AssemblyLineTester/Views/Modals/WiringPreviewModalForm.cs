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
using AssemblyLineTester.Models.Dto;
using AssemblyLineTester.Helpers;

namespace AssemblyLineTester.Views.Modals
{
    public partial class WiringPreviewModalForm : Form
    {
        private readonly HashSet<int> highlightedRows = [];
        private readonly HashSet<int> errorRows = [];
        private readonly List<AssemblyLineTester.Views.Controls.DiagramNetDefinition> netDefinitions = [];
        private readonly List<LiveTestDetailRowDto>? testResults;

        private readonly bool _hideErrorColors;

        public WiringPreviewModalForm(List<LiveTestDetailRowDto>? results = null, bool hideErrorColors = false)
        {
            _hideErrorColors = hideErrorColors;
            testResults = results;
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
            picPreview.DiagramRowClicked += PicPreview_DiagramRowClicked;
            pnlPreviewScroll.Resize += (s, e) => { UpdateDiagramSize(); UpdateDiagram(); };

            btnCloseModal.Click += (s, e) => Close();
            btnMaximize.Click += (s, e) => ToggleMaximize();

            btnMaximize.MouseEnter += (s, e) => { btnMaximize.BackColor = Color.FromArgb(30, 41, 59); btnMaximize.IconColor = Color.White; };
            btnMaximize.MouseLeave += (s, e) => { btnMaximize.BackColor = Color.Transparent; btnMaximize.IconColor = Color.FromArgb(203, 213, 225); };

            pnlCustomHeader.MouseDown += TitleBar_MouseDown;
            iconLogo.MouseDown += TitleBar_MouseDown;
            lblCustomTitle.MouseDown += TitleBar_MouseDown;

            pnlCustomHeader.DoubleClick += (s, e) => ToggleMaximize();
            iconLogo.DoubleClick += (s, e) => ToggleMaximize();
            lblCustomTitle.DoubleClick += (s, e) => ToggleMaximize();
        }


        private void TitleBar_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                ActiveControl = pnlCustomHeader;
                ValidateChildren();

            NativeMethods.DragWindow(Handle);
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
            errorRows.Clear();

            var configService = new TestConfigService();
            var activeConfig = configService.GetActiveConfig();

            if (activeConfig != null && activeConfig.Nets != null && activeConfig.Nets.Count > 0)
            {
                int i = 0;
                foreach (var net in activeConfig.Nets)
                {
                    netDefinitions.Add(new AssemblyLineTester.Views.Controls.DiagramNetDefinition
                    {
                        RowIndex = i++,
                        FromLabel = net.FromLabel ?? "",
                        FromAddress = net.FromAddress.ToString(),
                        ToLabel = net.ToLabel ?? "",
                        ToAddress = net.ToAddress.ToString(),
                        IsError = false
                    });
                }
            }
            else
            {
                LoadDefaultWiringData();
            }

            int nextRowIndex = netDefinitions.Count > 0 ? netDefinitions.Max(n => n.RowIndex) + 1 : 0;
            if (testResults != null)
            {
                foreach (var result in testResults)
                {
                    if (result.Status == LiveTestStatus.Fail)
                    {
                        string ExtractNumber(string point)
                        {
                            if (string.IsNullOrEmpty(point)) return point;
                            var parts = point.Split('-');
                            if (parts.Length == 2 && int.TryParse(parts[1], out int num))
                            {
                                return num.ToString();
                            }
                            return point;
                        }

                        string resFrom = ExtractNumber(result.FromPoint ?? "");
                        string resTo = ExtractNumber(result.ToPoint ?? "");

                        var existingNet = netDefinitions.FirstOrDefault(n => 
                            (n.FromAddress == resFrom && n.ToAddress == resTo) || 
                            (n.FromAddress == resTo && n.ToAddress == resFrom));
                        
                        if (existingNet != null)
                        {
                            errorRows.Add(existingNet.RowIndex);
                        }
                        else
                        {
                            var newNet = new AssemblyLineTester.Views.Controls.DiagramNetDefinition
                            {
                                RowIndex = nextRowIndex++,
                                FromLabel = string.IsNullOrEmpty(result.NetId) ? "Unexpected" : result.NetId,
                                FromAddress = resFrom,
                                ToLabel = string.IsNullOrEmpty(result.NetId) ? "Unexpected" : result.NetId,
                                ToAddress = resTo,
                                IsError = true
                            };
                            netDefinitions.Add(newNet);
                            errorRows.Add(newNet.RowIndex);
                        }
                    }
                }
            }

            UpdateDiagramSize();
            UpdateDiagram();
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
                netDefinitions.Add(new AssemblyLineTester.Views.Controls.DiagramNetDefinition
                {
                    RowIndex = i,
                    FromLabel = parts[0],
                    FromAddress = parts[1],
                    ToLabel = parts[0],
                    ToAddress = parts[2],
                    IsError = false
                });
            }

            UpdateDiagramSize();
            UpdateDiagram();
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

        private void UpdateDiagram()
        {
            // Sync error rows with netDefinitions
            foreach (var net in netDefinitions)
            {
                net.IsError = _hideErrorColors ? false : errorRows.Contains(net.RowIndex);
            }

            picPreview.RenderDiagram(netDefinitions, TestSettingService.GetDefaults().TotalTestPointsAvailable);
            picPreview.SetHighlightedRows(highlightedRows);
        }

        private void PicPreview_DiagramRowClicked(int rowIndex)
        {
            if (!highlightedRows.Remove(rowIndex))
            {
                highlightedRows.Add(rowIndex);
            }
            picPreview.SetHighlightedRows(highlightedRows);
        }
    }
}
