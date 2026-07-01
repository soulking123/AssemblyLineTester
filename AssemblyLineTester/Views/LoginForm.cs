using System;
using System.Windows.Forms;

namespace AssemblyLineTester.Views
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            // Memasangkan event klik tombol
            btnLogin.Click += BtnLogin_Click;
        }

        // CS8622 FIX: Mengubah 'object sender' menjadi 'object? sender' (tambahkan tanda tanya)
        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            // IDE0090 FIX: Mengubah 'new Form1()' menjadi 'new()' yang lebih ringkas dan modern
            MainForm dashboard = new();
            dashboard.Show();

            this.Hide();
        }
    }
}