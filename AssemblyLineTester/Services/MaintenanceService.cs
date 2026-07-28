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
            // Stop any existing timer to prevent duplicates
            if (_sessionTimer != null)
            {
                _sessionTimer.Stop();
                _sessionTimer.Dispose();
                _sessionTimer = null;
            }

            using (var db = new AppDbContext())
            {
                var state = db.MaintenanceStates.First(s => s.Id == 1);

                // Check if calendar day rolled over since last session
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

            // Start the background tracking timer (ticks every second)
            _sessionTimer = new System.Windows.Forms.Timer();
            _sessionTimer.Interval = 1000;
            _sessionTimer.Tick += OnTimerTick;
            _sessionTimer.Start();
        }

        private static void OnTimerTick(object? sender, EventArgs e)
        {
            ActiveOperatingSeconds++;
            _unsavedSeconds++;

            // Auto-save progress once a minute to prevent data loss on crashes
            if (_unsavedSeconds >= 60)
            {
                SaveProgress();
            }

            // Detect if day rolls over while application is running (Midnight transition)
            if (DateTime.Today != LastUsageDate.Date)
            {
                using (var db = new AppDbContext())
                {
                    var state = db.MaintenanceStates.First(s => s.Id == 1);
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
                // Runs during shutdown, so the database may already be closing. Not worth stopping
                // the exit for, but silently losing the operating hours would make the maintenance
                // counter drift with nothing to explain it.
                FileLogService.Warn("MaintenanceService.StopTrackingAndSave",
                    $"Operating hours could not be saved: {ex.Message}");
            }
            _unsavedSeconds = 0;
        }

        private static void PerformDayRollover(MaintenanceState state, AppDbContext db)
        {
            int daysPassed = (DateTime.Today - state.LastUsageDate.Date).Days;
            if (daysPassed < 1) daysPassed = 1; // Safeguard for edge cases

            // Decrease remaining days
            state.RemainingMaintDays = Math.Max(0, state.RemainingMaintDays - daysPassed);

            // Convert today's operating seconds to hours
            double operatingHoursToday = state.OperatingSecondsToday / 3600.0;

            // Decrease remaining hours by daily usage
            state.RemainingMaintHours = Math.Max(0, state.RemainingMaintHours - operatingHoursToday);

            // Reset operating time for the new day
            state.OperatingSecondsToday = 0;
            state.LastUsageDate = DateTime.Today;

            db.SaveChanges();
        }
    }
}
