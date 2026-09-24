using System;
using System.Drawing;
using System.Windows.Forms;
using AssemblyLineTester.Views.Pages.ProductDatabaseSubPages;

namespace AssemblyLineTester.Views.Pages
{
    public partial class ProductDatabase : UserControl
    {
        private ProductModelSubPage _productModelPage = null!;
        private HarnessAssemblySubPage _harnessAssemblyPage = null!;
        private ConnectorSubPage _connectorPage = null!;
        private Label _activeTabLabel = null!;

        public ProductDatabase()
        {
            InitializeComponent();
            InitializeSubPages();
            SetupInteractiveTabs();
            DoubleBuffered = true;
        }

        private void InitializeSubPages()
        {
            _productModelPage = new ProductModelSubPage { Dock = DockStyle.Fill };
            _harnessAssemblyPage = new HarnessAssemblySubPage { Dock = DockStyle.Fill };
            _connectorPage = new ConnectorSubPage { Dock = DockStyle.Fill };

            pnlContentContainer.Controls.Add(_productModelPage);
            pnlContentContainer.Controls.Add(_harnessAssemblyPage);
            pnlContentContainer.Controls.Add(_connectorPage);

            SwitchSubPage(lblTabProductModel, _productModelPage);
        }

        private void SetupInteractiveTabs()
        {
            Label[] tabLabels = [lblTabProductModel, lblTabHarnessAssembly, lblTabConnector];

            foreach (var lbl in tabLabels)
            {
                lbl.Cursor = Cursors.Hand;
                lbl.Click += (s, e) =>
                {
                    if (lbl == lblTabProductModel) SwitchSubPage(lblTabProductModel, _productModelPage);
                    else if (lbl == lblTabHarnessAssembly) SwitchSubPage(lblTabHarnessAssembly, _harnessAssemblyPage);
                    else if (lbl == lblTabConnector) SwitchSubPage(lblTabConnector, _connectorPage);
                };

                lbl.Paint += (s, e) =>
                {
                    if (lbl == _activeTabLabel)
                    {
                        using var brush = new SolidBrush(Color.FromArgb(245, 158, 11));
                        e.Graphics.FillRectangle(brush, 0, lbl.Height - 4, lbl.Width, 4);
                    }
                };
            }
        }

        private void SwitchSubPage(Label activeTab, UserControl activePage)
        {
            _activeTabLabel = activeTab;

            lblTabProductModel.ForeColor = (activeTab == lblTabProductModel) ? Color.FromArgb(15, 23, 42) : Color.FromArgb(100, 116, 139);
            lblTabHarnessAssembly.ForeColor = (activeTab == lblTabHarnessAssembly) ? Color.FromArgb(15, 23, 42) : Color.FromArgb(100, 116, 139);
            lblTabConnector.ForeColor = (activeTab == lblTabConnector) ? Color.FromArgb(15, 23, 42) : Color.FromArgb(100, 116, 139);

            flpTabs.Invalidate();

            _productModelPage.Visible = (activePage == _productModelPage);
            _harnessAssemblyPage.Visible = (activePage == _harnessAssemblyPage);
            _connectorPage.Visible = (activePage == _connectorPage);

            activePage.BringToFront();
        }
    }
}
