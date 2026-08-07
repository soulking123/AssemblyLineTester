using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using AssemblyLineTester.Data;
using AssemblyLineTester.Models;

namespace AssemblyLineTester.Services
{
    public static class MaintenanceService
    {
        private static System.Windows.Forms.Timer? _sessionTimer;
        private static int _unsavedSeconds = 0;

        private static bool _dueAlreadyLogged;

        public static double ActiveOperatingSeconds { get; private set; }
        public static int RemainingDays { get; private set; }
        public static double RemainingHours { get; private set; }
        public static DateTime LastUsageDate { get; private set; }

        /// <summary>
        /// Hours left counting the time already used today.
        /// </summary>
        /// <remarks>
        /// The stored figure is only charged at the day rollover, so a rig that burns its whole
        /// allowance in one shift would otherwise not be flagged until the next morning.
        /// </remarks>
        public static double RemainingHoursLive =>
            Math.Max(0, RemainingHours - (ActiveOperatingSeconds / 3600.0));

        // Two independent countdowns; whichever reaches zero first is what makes maintenance due.
        public static bool IsDue => RemainingDays <= 0 || RemainingHoursLive <= 0;

        public static string DueReason
        {
            get
            {
                bool daysGone = RemainingDays <= 0;
                bool hoursGone = RemainingHoursLive <= 0;

                if (daysGone && hoursGone) return "Both the scheduled days and the operating hours have run out.";
                if (daysGone) return "The scheduled maintenance days have run out.";
                if (hoursGone) return "The allotted operating hours have run out.";
                return "";
            }
        }

        /// <summary>
        /// Hours as HH:MM:SS, counting past 24 rather than rolling into days.
        /// </summary>
        /// <remarks>
        /// A decimal like 2.4 hours reads as two hours and forty minutes to some operators and two
        /// hours and four to others; the colon form cannot be misread. Hours are not wrapped at 24
        /// because the figure is an allowance, not a time of day.
        /// </remarks>
        public static string FormatHours(double hours)
        {
            if (hours < 0) hours = 0;

            int totalSeconds = (int)Math.Round(hours * 3600.0);
            int h = totalSeconds / 3600;
            int m = totalSeconds % 3600 / 60;
            int s = totalSeconds % 60;

            return $"{h:D2}:{m:D2}:{s:D2}";
        }

        /// <summary>
        /// The same value padded to four hour digits, for the fixed-width masked input.
        /// </summary>
        /// <remarks>
        /// ponytail: four digits caps the field at 9999 hours, about 416 days of continuous
        /// running. Widen the mask and this method together if an interval ever needs more.
        /// </remarks>
        public static string FormatHoursPadded(double hours)
        {
            if (hours < 0) hours = 0;

            int totalSeconds = (int)Math.Round(hours * 3600.0);
            int h = Math.Min(9999, totalSeconds / 3600);

            return $"{h:D4}:{totalSeconds % 3600 / 60:D2}:{totalSeconds % 60:D2}";
        }

        // Accepts HH:MM:SS as written by FormatHours, and a plain number of hours typed by hand.
        public static bool TryParseHours(string? text, out double hours)
        {
            hours = 0;
            if (string.IsNullOrWhiteSpace(text)) return false;

            string trimmed = text.Trim();

            if (!trimmed.Contains(':'))
            {
                return double.TryParse(trimmed, NumberStyles.Any, CultureInfo.InvariantCulture, out hours) && hours >= 0;
            }

            var parts = trimmed.Split(':');
            if (parts.Length is < 2 or > 3) return false;

            if (!int.TryParse(parts[0], out int h) || h < 0) return false;
            if (!int.TryParse(parts[1], out int m) || m is < 0 or > 59) return false;

            int s = 0;
            if (parts.Length == 3 && (!int.TryParse(parts[2], out s) || s is < 0 or > 59)) return false;

            hours = h + (m / 60.0) + (s / 3600.0);
            return true;
        }

        /// <summary>Debug-only guard on the hour format and its parser.</summary>
        [Conditional("DEBUG")]
        public static void SelfCheck()
        {
            Debug.Assert(FormatHours(2.5) == "02:30:00", FormatHours(2.5));
            Debug.Assert(FormatHours(2148.32) == "2148:19:12", FormatHours(2148.32));
            Debug.Assert(FormatHours(0) == "00:00:00", "an empty allowance must not read as blank");
            Debug.Assert(FormatHours(-5) == "00:00:00", "a negative allowance must clamp, not print a minus");

            Debug.Assert(TryParseHours("02:30:00", out double a) && Math.Abs(a - 2.5) < 1e-9, "round trip must hold");
            Debug.Assert(TryParseHours("10", out double b) && Math.Abs(b - 10) < 1e-9, "a plain number is still accepted");
            Debug.Assert(TryParseHours("01:30", out double c) && Math.Abs(c - 1.5) < 1e-9, "seconds may be left off");
            Debug.Assert(!TryParseHours("01:75:00", out _), "minutes past 59 must be refused");
            Debug.Assert(!TryParseHours("abc", out _), "text must be refused");

            Debug.Assert(FormatHoursPadded(2.5) == "0002:30:00", FormatHoursPadded(2.5));
            Debug.Assert(FormatHoursPadded(2148.32) == "2148:19:12", FormatHoursPadded(2148.32));
            Debug.Assert(FormatHoursPadded(20000) == "9999:00:00", "the mask cannot show five hour digits");
            Debug.Assert(TryParseHours(FormatHoursPadded(7.25), out double d) && Math.Abs(d - 7.25) < 1e-9,
                "what the masked field shows must parse back");
        }

        public static int IntervalDays { get; private set; }
        public static double IntervalHours { get; private set; }

        /// <summary>
        /// Sets the service interval and starts the countdown from it.
        /// </summary>
        /// <remarks>
        /// The figures entered here become what Reset restores. Keeping the interval alongside the
        /// remainder is what lets a drained countdown be put back to the value this site runs to,
        /// instead of a number chosen when the application was built.
        /// </remarks>
        public static bool SetRemaining(int days, double hours)
        {
            if (days < 0 || hours < 0) return false;
            return Apply(days, hours, storeAsInterval: true, action: "Set Maintenance Interval",
                message: $"Maintenance interval set to {days} day(s) or {FormatHours(hours)} of operating time.");
        }

        // Puts the countdown back to the interval held in Settings.
        public static bool ResetToInterval()
        {
            int days = IntervalDays;
            double hours = IntervalHours;

            return Apply(days, hours, storeAsInterval: false, action: "Reset Maintenance",
                message: $"Maintenance countdown reset to its interval of {days} day(s) or {FormatHours(hours)} of operating time.");
        }

        private static bool Apply(int days, double hours, bool storeAsInterval, string action, string message)
        {
            try
            {
                using var db = new AppDbContext();
                var state = LoadOrCreateState(db);

                state.RemainingMaintDays = days;
                state.RemainingMaintHours = hours;
                state.OperatingSecondsToday = 0;
                state.LastUsageDate = DateTime.Today;

                if (storeAsInterval)
                {
                    state.IntervalDays = days;
                    state.IntervalHours = hours;
                }

                db.SaveChanges();

                RemainingDays = days;
                RemainingHours = hours;
                IntervalDays = state.IntervalDays;
                IntervalHours = state.IntervalHours;
                ActiveOperatingSeconds = 0;
                LastUsageDate = DateTime.Today;
                _dueAlreadyLogged = false;

                new SystemLogService().Log(
                    Models.SystemLog.LogSeverity.Warning, "Maintenance", action, message);

                return true;
            }
            catch (Exception ex)
            {
                FileLogService.Error("MaintenanceService.Apply", "Could not save the maintenance countdown.", ex);
                return false;
            }
        }

        // Written once per crossing, not once per tick, or a due rig fills the log by itself.
        private static void LogIfNewlyDue()
        {
            if (!IsDue)
            {
                _dueAlreadyLogged = false;
                return;
            }

            if (_dueAlreadyLogged) return;
            _dueAlreadyLogged = true;

            new SystemLogService().Log(
                Models.SystemLog.LogSeverity.Warning, "Maintenance", "Maintenance Due",
                $"{DueReason} {RemainingDays} day(s) and {FormatHours(RemainingHoursLive)} remain.");
        }

        public static void StartTracking()
        {
            if (_sessionTimer != null)
            {
                _sessionTimer.Stop();
                _sessionTimer.Dispose();
                _sessionTimer = null;
            }

            using (var db = new AppDbContext())
            {
                var state = LoadOrCreateState(db);

                if (DateTime.Today != state.LastUsageDate.Date)
                {
                    PerformDayRollover(state, db);
                }

                ActiveOperatingSeconds = state.OperatingSecondsToday;
                RemainingDays = state.RemainingMaintDays;
                RemainingHours = state.RemainingMaintHours;
                IntervalDays = state.IntervalDays;
                IntervalHours = state.IntervalHours;
                LastUsageDate = state.LastUsageDate;
            }

            _dueAlreadyLogged = false;
            LogIfNewlyDue();
            _unsavedSeconds = 0;

            _sessionTimer = new System.Windows.Forms.Timer();
            _sessionTimer.Interval = 1000;
            _sessionTimer.Tick += OnTimerTick;
            _sessionTimer.Start();
        }

        private static void OnTimerTick(object? sender, EventArgs e)
        {
            ActiveOperatingSeconds++;
            _unsavedSeconds++;
            LogIfNewlyDue();

            // Saved once a minute, so a crash costs at most that.
            if (_unsavedSeconds >= 60)
            {
                SaveProgress();
            }

            // Midnight can arrive while the application is still running.
            if (DateTime.Today != LastUsageDate.Date)
            {
                // Flushed before the rollover reads the stored value, or the last minute is lost.
                SaveProgress();

                using (var db = new AppDbContext())
                {
                    var state = LoadOrCreateState(db);
                    PerformDayRollover(state, db);

                    ActiveOperatingSeconds = state.OperatingSecondsToday;
                    RemainingDays = state.RemainingMaintDays;
                    RemainingHours = state.RemainingMaintHours;
                    LastUsageDate = state.LastUsageDate;
                }
            }
        }

        public static void StopTrackingAndSave()
        {
            if (_sessionTimer != null)
            {
                _sessionTimer.Stop();
                _sessionTimer.Dispose();
                _sessionTimer = null;
            }
            SaveProgress();
        }

        // Recreated rather than thrown on: the caller is on the sign-in path.
        private static MaintenanceState LoadOrCreateState(AppDbContext db)
        {
            var state = db.MaintenanceStates.FirstOrDefault(s => s.Id == 1);
            if (state is not null) return state;

            FileLogService.Warn("MaintenanceService.LoadOrCreateState",
                "Maintenance state row was missing and has been recreated at its default interval.");

            state = new MaintenanceState { Id = 1, LastUsageDate = DateTime.Today };
            db.MaintenanceStates.Add(state);
            db.SaveChanges();
            return state;
        }

        private static void SaveProgress()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var state = db.MaintenanceStates.FirstOrDefault(s => s.Id == 1);
                    if (state != null)
                    {
                        state.OperatingSecondsToday = ActiveOperatingSeconds;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                // Runs during shutdown, so the database may already be closing.
                FileLogService.Warn("MaintenanceService.StopTrackingAndSave",
                    $"Operating hours could not be saved: {ex.Message}");
            }
            _unsavedSeconds = 0;
        }

        private static void PerformDayRollover(MaintenanceState state, AppDbContext db)
        {
            int daysPassed = (DateTime.Today - state.LastUsageDate.Date).Days;

            // A clock that moved backwards must not charge a maintenance day that never passed.
            if (daysPassed < 0) daysPassed = 0;

            state.RemainingMaintDays = Math.Max(0, state.RemainingMaintDays - daysPassed);

            double operatingHoursToday = state.OperatingSecondsToday / 3600.0;

            state.RemainingMaintHours = Math.Max(0, state.RemainingMaintHours - operatingHoursToday);

            state.OperatingSecondsToday = 0;
            state.LastUsageDate = DateTime.Today;

            db.SaveChanges();
        }
    }
}
