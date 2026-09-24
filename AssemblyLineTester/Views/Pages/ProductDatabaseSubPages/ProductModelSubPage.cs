using System;
using System.Drawing;
using System.Windows.Forms;
using AssemblyLineTester.Helpers;

namespace AssemblyLineTester.Views.Pages.ProductDatabaseSubPages
{
    public partial class ProductModelSubPage : UserControl
    {
        public ProductModelSubPage()
        {
            InitializeComponent();
            ApplyCustomStyles();
        }

        private void ApplyCustomStyles()
        {
            UIStyleHelper.StyleDataGridView(dgvProductModelList);
            UIStyleHelper.StyleDataGridView(dgvAttachedHarness);

            UIStyleHelper.SetupButtonRoundedEdges(btnNewProduct, 8,
                Color.FromArgb(245, 158, 11), Color.FromArgb(217, 119, 6), Color.FromArgb(180, 83, 9), Color.Transparent);

            UIStyleHelper.SetupButtonRoundedEdges(btnAddPicture, 6,
                Color.FromArgb(16, 185, 129), Color.FromArgb(5, 150, 105), Color.FromArgb(4, 120, 87), Color.Transparent);

            UIStyleHelper.SetupButtonRoundedEdges(btnDeletePicture, 6,
                Color.FromArgb(239, 68, 68), Color.FromArgb(220, 38, 38), Color.FromArgb(185, 28, 28), Color.Transparent);

            UIStyleHelper.SetupButtonRoundedEdges(btnAttach, 6,
                Color.FromArgb(16, 185, 129), Color.FromArgb(5, 150, 105), Color.FromArgb(4, 120, 87), Color.Transparent);

            UIStyleHelper.SetupButtonRoundedEdges(btnRemove, 6,
                Color.FromArgb(239, 68, 68), Color.FromArgb(220, 38, 38), Color.FromArgb(185, 28, 28), Color.Transparent);

            UIStyleHelper.SetupButtonRoundedEdges(btnSave, 8,
                Color.FromArgb(16, 185, 129), Color.FromArgb(5, 150, 105), Color.FromArgb(4, 120, 87), Color.Transparent);

            UIStyleHelper.SetupButtonRoundedEdges(btnDelete, 8,
                Color.FromArgb(239, 68, 68), Color.FromArgb(220, 38, 38), Color.FromArgb(185, 28, 28), Color.Transparent);

            UIStyleHelper.SetupCardRoundedEdges(pnlRightContainer, 10, Color.FromArgb(226, 232, 240), 1f);
        }
    }
}
