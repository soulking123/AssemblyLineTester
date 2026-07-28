using System;
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

        public static double ActiveOperatingSeconds { get; private set; }
        public static int RemainingDays { get; private set; }
        public static double RemainingHours { get; private set; }
        public static DateTime LastUsageDate { get; private set; }

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
                LastUsageDate = state.LastUsageDate;
            }

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
