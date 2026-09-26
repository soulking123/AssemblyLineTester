using AssemblyLineTester.Data;
using AssemblyLineTester.Models;
using AssemblyLineTester.Services;
using AssemblyLineTester.Views.Modals;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static AssemblyLineTester.Models.SystemLog;
using AssemblyLineTester.Helpers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AssemblyLineTester.Views
{
    public partial class LoginForm : Form
    {
        private readonly InstrumentService _instrument = new();
        private readonly SystemLogService logService = new ();

        public LoginForm()
        {
            var processes = System.Diagnostics.Process.GetProcessesByName("easywire");
            foreach (var process in processes)
            {
                try
                {
                    process.Kill();
                    process.WaitForExit(3000);
                    logService.Log(LogSeverity.Info, "Instrument", "Cirris Tester API Terminated",
                        $"Cirris Tester API Already Running, terminating Cirris Tester API / Easy Wire for App Initialization ", "-");
                }
                catch (Exception ex)
                {
                    logService.Log(LogSeverity.Info, "Instrument", "Failed Terminating Cirris Tester API",
                        $"Failed to terminate running Cirris Tester API with error message : {ex.Message}\n Please Terminate Cirris Tester API / Easy Wire manually", "-");
                }     
            }

            _instrument.Connect(); //run the first Connect, it will initiate Cirris Tester API, but will return faise (first initialization)
            

            InitializeComponent();

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

            btnLogin.TabStop = false;

            // KeyDown is already wired in the designer; subscribing again doubled every keystroke.

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

            pnlUsernameContainer.Click += (s, e) => txtUsername.Focus();
            iconUsername.Click += (s, e) => txtUsername.Focus();
            pnlUsernameLine.Click += (s, e) => txtUsername.Focus();

            pnlPasswordContainer.Click += (s, e) => txtPassword.Focus();
            iconPassword.Click += (s, e) => txtPassword.Focus();
            pnlPasswordLine.Click += (s, e) => txtPassword.Focus();

            btnClose.Click += (s, e) => this.Close();
            btnClose.MouseEnter += (s, e) => { btnClose.BackColor = Color.FromArgb(225, 29, 72); btnClose.IconColor = Color.White; };
            btnClose.MouseLeave += (s, e) => { btnClose.BackColor = Color.Transparent; btnClose.IconColor = Color.FromArgb(203, 213, 225); };

            this.MouseDown += Form_MouseDown;
        }

        

        private void Form_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
            NativeMethods.DragWindow(Handle);
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

        }
        private void UpdatePasswordVisuals(object? sender, EventArgs e)
        {
            bool active = txtPassword.Focused || pnlPasswordContainer.ClientRectangle.Contains(pnlPasswordContainer.PointToClient(Cursor.Position));
            pnlPasswordLine.BackColor = active ? System.Drawing.Color.FromArgb(250, 204, 21) : System.Drawing.Color.FromArgb(100, 116, 139);
            iconPassword.IconColor = active ? System.Drawing.Color.FromArgb(250, 204, 21) : System.Drawing.Color.FromArgb(100, 116, 139);

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
            if (!DesignMode)
            {
                AssemblyLineTester.Helpers.UIStyleHelper.ScaleExplicitFonts(this, AssemblyLineTester.Helpers.UIStyleHelper.GetHostScaleFactor(this));
            }
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

        private void TxtUsername_EnterKeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void TxtPassword_EnterKeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            // Consumed before the click, or auto-repeat queues a second Enter behind the modal.
            e.Handled = true;
            e.SuppressKeyPress = true;

            btnLogin.PerformClick();
        }

        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            if (_instrument.Connect())
            {
                logService.Log(LogSeverity.Info, "Instrument", "Cirris Tester API Connected",
                    $"Cirris Tester API Connected Successfully", "-");
            }
            else
            {
                logService.Log(LogSeverity.Error, "Instrument", "Cirris Tester API Connection Failed",
                    $"Failed to connect to Cirris Tester API, Check whether Cirris Tester API / Easy Wire is installed", "-");
            }

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
                        .Include(u => u.Role)
                        .Include(u => u.Status)
                        .FirstOrDefault(u => u.Username.ToLower() == username.ToLower());

                    

                    if (user == null)
                    {
                        // Logged under the typed name, to show runs against names that do not exist.
                        logService.Log(LogSeverity.Warning, "Authentication", "Login Failed",
                            $"Failed sign-in for unknown username '{username}'.", username);
                        MessageBox.Show("Invalid Login");
                        return;
                    }

                    bool isCorrect = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

                    if (!isCorrect)
                    {
                        logService.Log(LogSeverity.Warning, "Authentication", "Login Failed",
                            $"Failed sign-in for '{user.Username}': incorrect password.", user.Username);
                        MessageBox.Show("Invalid Login");
                        return;
                    }

                    // The count catches consecutive guessing, not typos spread over months.
                    user.FailedLoginCount = 0;
                    user.LockoutUntil = null;

                    // Committed before the status check below, which returns early.
                    db.SaveChanges();

                    // After the password check, so a wrong guess learns nothing about the account.
                    UserStatus.SelfCheck();
                    if (!UserStatus.CanSignIn(user.StatusId))
                    {
                        string statusName = user.Status?.Name ?? "not active";
                        logService.Log(LogSeverity.Warning, "Authentication", "Login Blocked",
                            $"Sign-in blocked for '{user.Username}': account is {statusName}.", user.Username);

                        MessageBox.Show(
                            $"This account is {statusName}. Contact your administrator.",
                            "Sign-in blocked",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    logService.Log(LogSeverity.Info, "Authentication", "Login",
                        $"User '{user.Username}' signed in.", user.Username);

                    // A required password change is handled by MainForm once the user is inside.
                    _instrument.Connect();
                    user.LastLogin = AppTime.UtcNow;
                    db.SaveChanges();

                    InstrumentService.StartMonitoring();

                    SessionManager.SetSession(user);

                    MaintenanceService.StartTracking();

                    MainForm dashboard = new();

                    // Closing the dashboard ends the process unless the user logged out.
                    dashboard.FormClosed += (s, args) =>
                    {
                        MaintenanceService.StopTrackingAndSave();
                        InstrumentService.StopMonitoring();
                        if (dashboard.IsLoggingOut)
                        {
                            SessionManager.ClearSession();
                            txtUsername.Clear();
                            txtPassword.Clear();
                            this.Show();
                            txtUsername.Focus();
                        }
                        else
                        {
                            InstrumentService.Shutdown();
                            this.Close();
                        }
                    };

                    dashboard.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                FileLogService.Error("LoginForm.BtnLogin_Click", $"Sign-in failed for '{username}'.", ex);
                MessageBox.Show($"Authentication error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
