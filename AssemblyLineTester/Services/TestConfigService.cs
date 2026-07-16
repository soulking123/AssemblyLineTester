using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using AssemblyLineTester.Data;
using AssemblyLineTester.Models;
using AssemblyLineTester.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace AssemblyLineTester.Services
{
    public class TestConfigService
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };

        // Test types that behave like "continuity" checks: pass/fail against a single
        // max-resistance threshold, rather than an Expected Value ± Tolerance band.

        public static readonly string[] ContinuityLikeTestTypes =
            { "WIRE", "4WIRE", "TWISTED PAIR", "SIGNATURE WIRE" };

        public void SaveConfig(TestConfiguration config)
        {
            using (var db = new AppDbContext())
            {
                // Convert every net's Expected/Lower/Upper values into the canonical base unit
                // for its test type BEFORE persisting (e.g. 1 "kΩ" entered -> stored as 1000 "Ω").
                foreach (var net in config.Nets)
                {
                    NormalizeNetUnits(net);
                }
                var existingProgram = db.TestConfigurations
                    .Include(c => c.Nets)
                    .FirstOrDefault(c => c.ConfigName == config.ConfigName);

                if (existingProgram != null)
                {
                    existingProgram.ProductModel = config.ProductModel;
                    existingProgram.Description = config.Description;
                    existingProgram.UpdatedAt = DateTime.UtcNow;

                    db.TestConfigNets.RemoveRange(existingProgram.Nets);
                    existingProgram.Nets = config.Nets;
                }
                else
                {
                    // Ensure CreatedAt/UpdatedAt are set for new configs
                    config.CreatedAt = DateTime.UtcNow;
                    config.UpdatedAt = DateTime.UtcNow;
                    db.TestConfigurations.Add(config);
                }

                db.SaveChanges();
            }
        }

        // Converts a net's ExpectedValue/LowerLimit/UpperLimit from whatever unit the user
        // picked in the grid into the canonical base unit for that test type, and overwrites
        // the Unit field to the base unit. Tolerance is a percentage and is left untouched.

        private static void NormalizeNetUnits(TestConfigNet net)
        {
            // Determine the test type name to use for unit conversion.
            string testTypeName = net.TestType?.TestTypeName;
            if (string.IsNullOrWhiteSpace(testTypeName) && net.TestTypeId != 0)
            {
                using var db = new AppDbContext();
                var tt = db.TestTypes.Find(net.TestTypeId);
                if (tt != null) testTypeName = tt.TestTypeName;
            }

            // Normalize to map keys (UnitConversionHelper expects keys like "CAPACITOR", "4WIRE", etc.)
            string key = string.IsNullOrWhiteSpace(testTypeName) ? string.Empty : testTypeName.Trim().ToUpperInvariant();
            string enteredUnit = net.MeasurementUnit;

            if (net.ExpectedValue.HasValue)
                net.ExpectedValue = UnitConversionHelper.ConvertToBaseUnit(key, net.ExpectedValue.Value, enteredUnit);

            if (net.LowerLimit.HasValue)
                net.LowerLimit = UnitConversionHelper.ConvertToBaseUnit(key, net.LowerLimit.Value, enteredUnit);

            if (net.UpperLimit.HasValue)
                net.UpperLimit = UnitConversionHelper.ConvertToBaseUnit(key, net.UpperLimit.Value, enteredUnit);

            net.MeasurementUnit = UnitConversionHelper.GetBaseUnit(key);
        }

        public void SetActiveConfig(string configName)
        {
            using (var db = new AppDbContext())
            {
                var allPrograms = db.TestConfigurations.ToList();
                foreach (var p in allPrograms)
                {
                    p.IsActive = p.ConfigName == configName;
                }
                db.SaveChanges();
            }
        }

        public TestConfiguration? GetActiveConfig()
        {
            using (var db = new AppDbContext())
            {
                return db.TestConfigurations
                    .Include(c => c.Nets)
                    .FirstOrDefault(c => c.IsActive);
            }
        }

        public TestConfiguration? GetConfigurationByName(string configName)
        {
            using var db = new AppDbContext();
            return db.TestConfigurations
                .Include(c => c.Nets)
                .ThenInclude(n => n.TestType)
                .FirstOrDefault(c => c.ConfigName == configName);
        }

        public List<TestConfiguration> GetAllConfigurations()
        {
            return GetConfigurations(null);
        }

        /// <summary>
        /// Retrieve configurations optionally filtered by config name (case-insensitive)
        /// and ordered by most-recently updated (or created) first.
        /// This performs filtering at the database level for efficiency.
        /// </summary>
        public List<TestConfiguration> GetConfigurations(string? configNameFilter)
        {
            using var db = new AppDbContext();
            var q = db.TestConfigurations
                .Include(c => c.Nets)
                .ThenInclude(n => n.TestType)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(configNameFilter))
            {
                string key = configNameFilter.Trim().ToUpperInvariant();
                // Use ToUpper + Contains so EF Core can translate to SQL UPPER(...) LIKE ...
                q = q.Where(c => c.ConfigName != null && c.ConfigName.ToUpper().Contains(key));
            }

            // Order by UpdatedAt if available, otherwise by CreatedAt. Newest first.
            q = q.OrderByDescending(c => (c.UpdatedAt ?? c.CreatedAt));

            return q.ToList();
        }

        public bool DeleteConfiguration(string configName)
        {
            if (string.IsNullOrWhiteSpace(configName)) return false;

            using var db = new AppDbContext();
            var cfg = db.TestConfigurations
                .Include(c => c.Nets)
                .FirstOrDefault(c => c.ConfigName == configName);

            if (cfg == null) return false;

            db.TestConfigNets.RemoveRange(cfg.Nets);
            db.TestConfigurations.Remove(cfg);
            db.SaveChanges();
            return true;
        }

        // TestType helpers
        public TestType? GetTestTypeByName(string testTypeName)
        {
            if (string.IsNullOrWhiteSpace(testTypeName)) return null;
            using var db = new AppDbContext();
            string key = testTypeName.Trim().ToUpperInvariant();
            return db.TestTypes.FirstOrDefault(t => t.TestTypeName != null && t.TestTypeName.ToUpper() == key);
        }

        public string? GetTestTypeNameById(int id)
        {
            if (id <= 0) return null;
            using var db = new AppDbContext();
            var tt = db.TestTypes.Find(id);
            return tt?.TestTypeName;
        }

        // Populates colTestType's dropdown items - reads live from the TestTypes table.
        public List<string> GetTestTypeNames()
        {
            using var db = new AppDbContext();
            return db.TestTypes.OrderBy(t => t.TestTypeName).Select(t => t.TestTypeName).ToList();
        }

        // Reads and parses a test config file from disk (flat JSON array of nets),
        // e.g. AutomatedTestConfig3.json / AutomatedTestConfig4.json.
        public List<TestConfigNetDto> LoadConfigFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path is required.", nameof(filePath));
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Config file not found.", filePath);

            string json = File.ReadAllText(filePath);
            var nets = JsonSerializer.Deserialize<List<TestConfigNetDto>>(json, JsonOptions);

            if (nets is null || nets.Count == 0)
                throw new InvalidDataException("Config file is empty or could not be parsed.");

            return nets;
        }

        public string ConvertToProgramText(TestConfiguration config)
        {
            var sb = new System.Text.StringBuilder();

            sb.AppendLine("* ");
            sb.AppendLine();
            sb.AppendLine("* CRC Signature: Z9S34P");
            sb.AppendLine();

            string programName = !string.IsNullOrWhiteSpace(config.ConfigName) ? config.ConfigName : "PROGRAM";
            sb.AppendLine($"({programName}");
            sb.AppendLine();
            sb.AppendLine(" J1 D507F1 *[AHED-64,0,0,0,0,0,0,0,0,64] 64 POS. .1\" FEM.");
            sb.AppendLine(" J3 D507F1 *[AHED-64,0,0,0,0,0,0,0,0,64] 64 POS. .1\" FEM.");
            sb.AppendLine();

            double connResist = 5;
            var resNet = config.Nets.FirstOrDefault(n => n.UpperLimit.HasValue);
            if (resNet != null && resNet.UpperLimit.HasValue)
            {
                connResist = resNet.UpperLimit.Value;
            }

            sb.AppendLine($"CONNECTION RESIS  {connResist} ohm");
            sb.AppendLine("LV INSULATION RESIS  100 K ohm");
            sb.AppendLine("HIPOT VOLTAGE  OFF");
            sb.AppendLine("** TEST METHOD CONTINUOUS");
            sb.AppendLine();

            foreach (var net in config.Nets.OrderBy(n => n.SequenceNo))
            {
                string fromPin = FormatPin(net.FromAddress);
                string toPin = FormatPin(net.ToAddress);

                sb.AppendLine($"{net.SequenceNo}   {fromPin}  {toPin}  ");
            }

            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine(")");

            return sb.ToString();
        }

        private string FormatPin(int address)
        {
            return address < 64 ? $"J1-{address:D3}" : $"J3-{(address - 64):D3}";
        }
    }
}