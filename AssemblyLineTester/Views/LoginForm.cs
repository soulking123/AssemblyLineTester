using AssemblyLineTester.Data;
using AssemblyLineTester.Services;
using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AssemblyLineTester.Views
{
    public partial class LoginForm : Form
    {
        private readonly InstrumentService _instrument = new();
        public LoginForm()
        {
            //try
            //{
            //    var processes = System.Diagnostics.Process.GetProcessesByName("easywire");
            //    foreach (var process in processes)
            //    {
            //        process.Kill();
            //        process.WaitForExit(3000);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    System.Diagnostics.Debug.WriteLine($"Error terminate easywire process: {ex.Message}");
            //}

            //_instrument.Connect();
            InitializeComponent();

            // Load CAT logo image
            try
            {
                string logoPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "cat_logo.png");
                if (System.IO.File.Exists(logoPath))
                {
                    logoBox.Image = Image.FromFile(logoPath);
                }

                string iconPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "cat_logo.ico");
                if (System.IO.File.Exists(iconPath))
                {
                    this.Icon = new Icon(iconPath);
                }
            }
            catch { /* fallback silently if image/icon missing */ }

            // Disable tab stop on the login button so Tab cycles only between textboxes
            btnLogin.TabStop = false;

            txtUsername.KeyDown += TxtUsername_EnterKeyDown;
            txtPassword.KeyDown += TxtPassword_EnterKeyDown;

            // Wire up focus & hover styling to animate underline and icon colors to Gold without disappearing
            void UpdateUsernameVisuals()
            {
                bool active = txtUsername.Focused || pnlUsernameContainer.ClientRectangle.Contains(pnlUsernameContainer.PointToClient(Cursor.Position));
                pnlUsernameLine.BackColor = active ? System.Drawing.Color.FromArgb(250, 204, 21) : System.Drawing.Color.FromArgb(100, 116, 139);
                iconUsername.IconColor = active ? System.Drawing.Color.FromArgb(250, 204, 21) : System.Drawing.Color.FromArgb(100, 116, 139);
            }

            void UpdatePasswordVisuals()
            {
                bool active = txtPassword.Focused || pnlPasswordContainer.ClientRectangle.Contains(pnlPasswordContainer.PointToClient(Cursor.Position));
                pnlPasswordLine.BackColor = active ? System.Drawing.Color.FromArgb(250, 204, 21) : System.Drawing.Color.FromArgb(100, 116, 139);
                iconPassword.IconColor = active ? System.Drawing.Color.FromArgb(250, 204, 21) : System.Drawing.Color.FromArgb(100, 116, 139);
            }

            txtUsername.Enter += (s, e) => UpdateUsernameVisuals();
            txtUsername.Leave += (s, e) => UpdateUsernameVisuals();
            txtUsername.MouseEnter += (s, e) => UpdateUsernameVisuals();
            txtUsername.MouseLeave += (s, e) => UpdateUsernameVisuals();
            pnlUsernameContainer.MouseEnter += (s, e) => UpdateUsernameVisuals();
            pnlUsernameContainer.MouseLeave += (s, e) => UpdateUsernameVisuals();
            iconUsername.MouseEnter += (s, e) => UpdateUsernameVisuals();
            iconUsername.MouseLeave += (s, e) => UpdateUsernameVisuals();

            txtPassword.Enter += (s, e) => UpdatePasswordVisuals();
            txtPassword.Leave += (s, e) => UpdatePasswordVisuals();
            txtPassword.MouseEnter += (s, e) => UpdatePasswordVisuals();
            txtPassword.MouseLeave += (s, e) => UpdatePasswordVisuals();
            pnlPasswordContainer.MouseEnter += (s, e) => UpdatePasswordVisuals();
            pnlPasswordContainer.MouseLeave += (s, e) => UpdatePasswordVisuals();
            iconPassword.MouseEnter += (s, e) => UpdatePasswordVisuals();
            iconPassword.MouseLeave += (s, e) => UpdatePasswordVisuals();

            // Allow clicking anywhere on the icon or underline row to focus the textbox
            pnlUsernameContainer.Click += (s, e) => txtUsername.Focus();
            iconUsername.Click += (s, e) => txtUsername.Focus();
            pnlUsernameLine.Click += (s, e) => txtUsername.Focus();

            pnlPasswordContainer.Click += (s, e) => txtPassword.Focus();
            iconPassword.Click += (s, e) => txtPassword.Focus();
            pnlPasswordLine.Click += (s, e) => txtPassword.Focus();

            // Wire up close button and form dragging
            btnClose.Click += (s, e) => this.Close();
            btnClose.MouseEnter += (s, e) => { btnClose.BackColor = Color.FromArgb(225, 29, 72); btnClose.IconColor = Color.White; };
            btnClose.MouseLeave += (s, e) => { btnClose.BackColor = Color.Transparent; btnClose.IconColor = Color.FromArgb(203, 213, 225); };

            this.MouseDown += Form_MouseDown;
        }

        
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        private void Form_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        [DllImport("dwmapi.dll", PreserveSig = true)]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        private const int DWMWA_WINDOW_CORNER_PREFERENCE = 33;
        private const int DWMWCP_ROUND = 2;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        private void UpdateUsernameVisuals(object? sender, EventArgs e)
        {
            bool active = txtUsername.Focused || pnlUsernameContainer.ClientRectangle.Contains(pnlUsernameContainer.PointToClient(Cursor.Position));
            pnlUsernameLine.BackColor = active ? System.Drawing.Color.FromArgb(250, 204, 21) : System.Drawing.Color.FromArgb(100, 116, 139);
            iconUsername.IconColor = active ? System.Drawing.Color.FromArgb(250, 204, 21) : System.Drawing.Color.FromArgb(100, 116, 139);

            //pnlPasswordLine.BackColor = System.Drawing.Color.FromArgb(100, 116, 139);
            //iconPassword.IconColor =  System.Drawing.Color.FromArgb(100, 116, 139);
        }
        private void UpdatePasswordVisuals(object? sender, EventArgs e)
        {
            bool active = txtPassword.Focused || pnlPasswordContainer.ClientRectangle.Contains(pnlPasswordContainer.PointToClient(Cursor.Position));
            pnlPasswordLine.BackColor = active ? System.Drawing.Color.FromArgb(250, 204, 21) : System.Drawing.Color.FromArgb(100, 116, 139);
            iconPassword.IconColor = active ? System.Drawing.Color.FromArgb(250, 204, 21) : System.Drawing.Color.FromArgb(100, 116, 139);

            //pnlUsernameLine.BackColor =  System.Drawing.Color.FromArgb(100, 116, 139);
            //iconUsername.IconColor = System.Drawing.Color.FromArgb(100, 116, 139);
        }
        private void FocusUsername(object? sender, EventArgs e)
        {
            txtUsername.Focus();
        }
        private void FocusPassword(object? sender, EventArgs e)
        {
            txtPassword.Focus();
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            int preference = DWMWCP_ROUND;
            if (DwmSetWindowAttribute(Handle, DWMWA_WINDOW_CORNER_PREFERENCE, ref preference, sizeof(int)) != 0)
            {
                ApplyRoundedCorners();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            int preference = DWMWCP_ROUND;
            if (DwmSetWindowAttribute(Handle, DWMWA_WINDOW_CORNER_PREFERENCE, ref preference, sizeof(int)) != 0)
            {
                ApplyRoundedCorners();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.Region != null)
            {
                ApplyRoundedCorners();
            }
        }

        private void ApplyRoundedCorners()
        {
            if (Width > 0 && Height > 0)
            {
                IntPtr handle = CreateRoundRectRgn(0, 0, Width, Height, 32, 32);
                this.Region = System.Drawing.Region.FromHrgn(handle);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
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
                        .Include(u => u.Role) // Eager load Role to set the Session details correctly
                        .FirstOrDefault(u => u.Username.ToLower() == username.ToLower());

                    if (user == null)
                    {
                        MessageBox.Show("Invalid Login");
                        return;
                    }

                    bool isCorrect = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);


                    // update last login timestamp, save the logged-in user to the session, and close login form
                    if (isCorrect)
                    {

                        _instrument.Connect();
                        // Update LastLogin field and persist to SQLite database
                        user.LastLogin = DateTime.Now;
                        db.SaveChanges();

                        //bool isConnected = _instrument.Connect();
                        //if (!isConnected)
                        //{
                        //    MessageBox.Show("Failed to connect to instrument.", "Connection Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //    return;
                        //}
                        //Debug.WriteLine($"Bool : {isConnected}");

                        // Start global instrument connection status monitoring
                        InstrumentService.StartMonitoring();

                        // Save the logged-in user in memory
                        SessionManager.SetSession(user);

                        // Start tracking maintenance and operational hours
                        MaintenanceService.StartTracking();

                        MainForm dashboard = new();
                        
                        // Close login form to terminate the application process when dashboard closes
                        dashboard.FormClosed += (s, args) => 
                        {
                            MaintenanceService.StopTrackingAndSave();
                            InstrumentService.StopMonitoring(); // Stop monitoring
                            if (dashboard.IsLoggingOut)
                            {
                                SessionManager.ClearSession();
                                this.Show();
                            }
                            else
                            {
                                this.Close();
                            }
                        };
                        
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
