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

        public static List<TestResultCheck> GetTestResultChecks()
        {
            using var db = new AppDbContext();
            return db.TestResultChecks.AsNoTracking().OrderBy(c => c.Id).ToList();
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

        public static bool SetTestTypeChecked(int id, bool isChecked) =>
            Update(db => db.TestTypeChecks.FirstOrDefault(c => c.Id == id), e => e.IsChecked = isChecked);

        public static bool SetLearnConnectionChecked(int id, bool isChecked) =>
            Update(db => db.LearnConnections.FirstOrDefault(c => c.Id == id), e => e.IsChecked = isChecked);

        public static bool SetTestResultChecked(int id, bool isChecked) =>
            Update(db => db.TestResultChecks.FirstOrDefault(c => c.Id == id), e => e.IsChecked = isChecked);

        // A failed tick must not take the Settings page down with it, so the write is reported, not thrown.
        private static bool Update<T>(Func<AppDbContext, T?> find, Action<T> apply) where T : class
        {
            try
            {
                using var db = new AppDbContext();
                var entity = find(db);
                if (entity == null) return false;

                apply(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"TestSettingService.Update failed: {ex}");
                return false;
            }
        }
    }
}
