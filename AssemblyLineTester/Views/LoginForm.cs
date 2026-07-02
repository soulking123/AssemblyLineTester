using AssemblyLineTester.Data;
using System;
using System.Windows.Forms;

namespace AssemblyLineTester.Views
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            // Disable tab stop on the login button so Tab cycles only between textboxes
            btnLogin.TabStop = false;

            txtUsername.KeyDown += TxtUsername_EnterKeyDown;
            txtPassword.KeyDown += TxtPassword_EnterKeyDown;
        }

        /// Moves focus to the password textbox when the Enter key is pressed.
        private void TxtUsername_EnterKeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
                e.SuppressKeyPress = true; // Prevents the Windows beep sound
            }
        }

        /// Programmatically triggers the login button click when the Enter key is pressed.
        private void TxtPassword_EnterKeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
                e.SuppressKeyPress = true; // Prevents the Windows beep sound
            }
        }

        /// Authenticates user credentials against the local SQLite database.
        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Fill Username and Password");
                return;
            }

            try
            {
                using (var db = new AppDbContext())
                {
                    var user = db.Users
                        .FirstOrDefault(u => u.Username.ToLower() == username.ToLower());

                    if (user == null)
                    {
                        MessageBox.Show("Invalid Login");
                        return;
                    }

                    bool isCorrect = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

                    if (isCorrect)
                    {
                        MainForm dashboard = new();
                        
                        // Close login form to terminate the application process when dashboard closes
                        dashboard.FormClosed += (s, args) => this.Close(); 
                        
                        dashboard.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Login");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Authentication error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}