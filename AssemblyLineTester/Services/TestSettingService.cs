using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AssemblyLineTester.Data;
using AssemblyLineTester.Models;
using Microsoft.EntityFrameworkCore;

namespace AssemblyLineTester.Services
{
    // Reads and writes the three tick-list tables behind the Default Test Settings card.
    public static class TestSettingService
    {
        public static List<TestTypeCheck> GetTestTypeChecks()
        {
            using var db = new AppDbContext();
            return db.TestTypeChecks.AsNoTracking().OrderBy(c => c.Id).ToList();
        }

        public static List<LearnConnection> GetLearnConnections()
        {
            using var db = new AppDbContext();
            return db.LearnConnections.AsNoTracking().Include(c => c.TestType).OrderBy(c => c.Id).ToList();
        }

        // The column order of the Test Parameters grid, which the live run and the exports follow so
        // one net reads the same way on every page. Seed ids track insertion, not position, so the
        // order is stated here rather than by renumbering rows an operator has already ticked.
        private static readonly string[] ResultColumnOrder =
        {
            "Harness Group", "Color", "Gauge", "Net-ID", "From", "Net From Label", "Net From Connector", "To",
            "Net To Label", "Net To Connector", "Test Type", "Lower Limit", "Upper Limit", "Expected Value",
            "Tolerance", "Measurement"
        };

        public static List<TestResultCheck> GetTestResultChecks()
        {
            using var db = new AppDbContext();
            var checks = db.TestResultChecks.AsNoTracking().ToList();

            int virtualId = 1000;
            foreach (var col in ResultColumnOrder)
            {
                if (!checks.Any(c => string.Equals(c.TestResultName, col, StringComparison.OrdinalIgnoreCase)))
                {
                    checks.Add(new TestResultCheck { Id = virtualId++, TestResultName = col, IsChecked = true });
                }
            }

            // A column this list has never heard of sorts to the end instead of disappearing.
            return checks
                .OrderBy(c => Array.IndexOf(ResultColumnOrder, c.TestResultName) is int i && i >= 0 ? i : int.MaxValue)
                .ThenBy(c => c.Id)
                .ToList();
        }

        // Names of the result columns the operator left ticked, in their seeded display order.
        public static List<string> GetEnabledResultColumns()
        {
            return GetTestResultChecks().Where(c => c.IsChecked).Select(c => c.TestResultName).ToList();
        }

        // Names of the test types the operator left ticked, for the Test Parameter type dropdown.
        public static List<string> GetEnabledTestTypeNames()
        {
            return GetTestTypeChecks().Where(c => c.IsChecked).Select(c => c.TestTypeName).OrderBy(n => n).ToList();
        }

        public static List<TestType> GetTestTypes()
        {
            using var db = new AppDbContext();
            return db.TestTypes.AsNoTracking().OrderBy(t => t.TestTypeName).ToList();
        }

        /// <summary>
        /// The single row behind the Default Test Settings card, created empty on first use.
        /// </summary>
        /// <remarks>
        /// One row, not one per product model: the entity carries no product model column, so a
        /// second row could never be told apart from the first.
        /// </remarks>
        public static DefaultTestSetting GetDefaults()
        {
            using var db = new AppDbContext();
            var existing = db.DefaultTestSettings.AsNoTracking().OrderBy(d => d.Id).FirstOrDefault();
            if (existing != null) return existing;

            // DefaultTestTypeId is a required foreign key, so the zero a new object starts with
            // points at a TestType that cannot exist and the insert is refused. The first seeded
            // type stands in until the operator picks one on the Settings card.
            int firstTypeId = db.TestTypes.AsNoTracking().OrderBy(t => t.Id).Select(t => t.Id).FirstOrDefault();
            if (firstTypeId == 0) return new DefaultTestSetting();

            var seeded = new DefaultTestSetting { DefaultTestTypeId = firstTypeId };
            db.DefaultTestSettings.Add(seeded);
            db.SaveChanges();
            return seeded;
        }

        // Name of the test type a new Test Parameter row should start on.
        public static string GetDefaultTestTypeName()
        {
            int id = GetDefaults().DefaultTestTypeId;
            if (id <= 0) return string.Empty;

            using var db = new AppDbContext();
            return db.TestTypes.AsNoTracking().FirstOrDefault(t => t.Id == id)?.TestTypeName ?? string.Empty;
        }

        // Writes the whole card in one go, so a half-saved set of defaults cannot be left behind.
        public static bool SaveDefaults(DefaultTestSetting values)
        {
            if (!SessionManager.HasPermission("Action.Settings.Edit")) return false;

            try
            {
                using var db = new AppDbContext();
                var row = db.DefaultTestSettings.OrderBy(d => d.Id).FirstOrDefault();
                if (row == null)
                {
                    row = new DefaultTestSetting
                    {
                        DefaultTestTypeId = db.TestTypes.AsNoTracking().OrderBy(t => t.Id).Select(t => t.Id).FirstOrDefault()
                    };
                    db.DefaultTestSettings.Add(row);
                }

                row.TotalTestPointsAvailable = values.TotalTestPointsAvailable;

                // Zero means nothing was selected, and the foreign key has no row to point at.
                // Keeping the stored type is better than refusing the whole save over it.
                if (values.DefaultTestTypeId > 0) row.DefaultTestTypeId = values.DefaultTestTypeId;
                row.DefaultMaxWireResistance = values.DefaultMaxWireResistance;
                row.DefaultMinInulationResistance = values.DefaultMinInulationResistance;
                row.DefaultResistorValue = values.DefaultResistorValue;
                row.DefaultResistorTolerance = values.DefaultResistorTolerance;
                row.DefaultCapacitorValue = values.DefaultCapacitorValue;
                row.DefaultCapacitorTolerance = values.DefaultCapacitorTolerance;

                // Asked before SaveChanges, while the tracker still knows the old values, so
                // pressing Save without editing anything leaves no entry.
                bool changed = db.ChangeTracker.HasChanges();
                db.SaveChanges();
                if (!changed) return true;

                string typeName = db.TestTypes.AsNoTracking()
                    .FirstOrDefault(t => t.Id == row.DefaultTestTypeId)?.TestTypeName ?? "none";

                new SystemLogService().Log(
                    Models.SystemLog.LogSeverity.Info, "Settings", "Update Default Test Settings",
                    $"Default test type set to {typeName}, {row.TotalTestPointsAvailable} test points available.");

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"TestSettingService.SaveDefaults failed: {ex}");
                return false;
            }
        }

        public static bool SaveTestTypeChecks(IEnumerable<(int Id, bool Checked)> states) =>
            SaveTicks(states, "Update Test Type Availability", "test types",
                db => db.TestTypeChecks, c => c.Id, c => c.IsChecked, (c, v) => c.IsChecked = v, c => c.TestTypeName);

        public static bool SaveLearnConnectionChecks(IEnumerable<(int Id, bool Checked)> states) =>
            SaveTicks(states, "Update Learn Connections", "learn connections",
                db => db.LearnConnections, c => c.Id, c => c.IsChecked, (c, v) => c.IsChecked = v, c => c.DisplayName);

        public static bool SaveTestResultChecks(IEnumerable<(int Id, bool Checked)> states) =>
            SaveTicks(states, "Update Result Columns", "result columns",
                db => db.TestResultChecks, c => c.Id, c => c.IsChecked, (c, v) => c.IsChecked = v, c => c.TestResultName);

        /// <summary>
        /// Applies a whole tick list in one transaction and records what actually changed.
        /// </summary>
        /// <remarks>
        /// One entry per list, not per tick: a single Save touches every box on the card, and
        /// logging each one would bury the day's real events under thirty rows. Nothing is written
        /// to the log when nothing moved, so an operator who opens Edit and changes their mind
        /// leaves no entry behind.
        /// </remarks>
        private static bool SaveTicks<T>(
            IEnumerable<(int Id, bool Checked)> states,
            string actionName,
            string listLabel,
            Func<AppDbContext, DbSet<T>> set,
            Func<T, int> id,
            Func<T, bool> isChecked,
            Action<T, bool> apply,
            Func<T, string> name) where T : class
        {
            if (!SessionManager.HasPermission("Action.Settings.Edit")) return false;

            try
            {
                using var db = new AppDbContext();
                var rows = set(db).ToList();

                var turnedOn = new List<string>();
                var turnedOff = new List<string>();

                foreach (var (rowId, wanted) in states)
                {
                    var row = rows.FirstOrDefault(r => id(r) == rowId);
                    if (row == null || isChecked(row) == wanted) continue;

                    (wanted ? turnedOn : turnedOff).Add(name(row));
                    apply(row, wanted);
                }

                if (turnedOn.Count == 0 && turnedOff.Count == 0) return true;

                db.SaveChanges();

                new SystemLogService().Log(
                    Models.SystemLog.LogSeverity.Info, "Settings", actionName,
                    BuildTickMessage(listLabel, turnedOn, turnedOff, rows.Count(isChecked), rows.Count));

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"TestSettingService.SaveTicks({actionName}) failed: {ex}");
                return false;
            }
        }

        // Names what moved and where the list stands afterwards, in one sentence.
        internal static string BuildTickMessage(string listLabel, List<string> turnedOn, List<string> turnedOff, int nowEnabled, int total)
        {
            var parts = new List<string>();
            if (turnedOn.Count > 0) parts.Add($"enabled {string.Join(", ", turnedOn)}");
            if (turnedOff.Count > 0) parts.Add($"disabled {string.Join(", ", turnedOff)}");

            string subject = $"{char.ToUpperInvariant(listLabel[0])}{listLabel[1..]}";
            return $"{subject}: {string.Join("; ", parts)}. {nowEnabled} of {total} now enabled.";
        }

        /// <summary>Debug-only guard on the tick-list log message.</summary>
        [Conditional("DEBUG")]
        public static void SelfCheck()
        {
            string both = BuildTickMessage("result columns", new List<string> { "Tolerance" }, new List<string> { "Net-ID" }, 6, 12);
            Debug.Assert(both == "Result columns: enabled Tolerance; disabled Net-ID. 6 of 12 now enabled.", both);

            string onlyOff = BuildTickMessage("test types", new List<string>(), new List<string> { "DIODE", "OPEN" }, 4, 15);
            Debug.Assert(onlyOff == "Test types: disabled DIODE, OPEN. 4 of 15 now enabled.", onlyOff);
            Debug.Assert(!onlyOff.Contains("enabled DIODE"), "an empty side must not be announced");
        }
    }
}
