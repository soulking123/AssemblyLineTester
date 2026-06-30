using System;
using System.Windows.Forms;

namespace AssemblyLineTester
{
    public partial class LoginPage : Form
    {
        public LoginPage()
        {
            InitializeComponent();

            // Memasangkan event klik tombol
            btnLogin.Click += BtnLogin_Click;
        }

        // CS8622 FIX: Mengubah 'object sender' menjadi 'object? sender' (tambahkan tanda tanya)
        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            // IDE0090 FIX: Mengubah 'new Form1()' menjadi 'new()' yang lebih ringkas dan modern
            Form1 dashboard = new();
            dashboard.Show();

            this.Hide();
        }
    }
}