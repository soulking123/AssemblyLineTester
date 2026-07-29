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

        // Types judged against one max-resistance threshold rather than an expected value band.
        public static readonly string[] ContinuityLikeTestTypes =
            { "WIRE", "4WIRE", "TWISTED PAIR", "SIGNATURE WIRE" };

        public void SaveConfig(TestConfiguration config)
        {
            using (var db = new AppDbContext())
            {
                // Looked up once for the whole batch, not per net.
                var testTypeNamesById = db.TestTypes.ToDictionary(t => t.Id, t => t.TestTypeName);
                foreach (var net in config.Nets)
                {
                    NormalizeNetUnits(net, testTypeNamesById);
                }

                var existingProgram = db.TestConfigurations
                    .Include(c => c.Nets)
                    .FirstOrDefault(c => c.ConfigName == config.ConfigName);

                if (existingProgram != null)
                {
                    existingProgram.ProductModel = config.ProductModel;
                    existingProgram.Description = config.Description;
                    existingProgram.UpdatedAt = DateTime.UtcNow;

                    // Delete is committed before the replacements attach, or EF reconciles both at once.
                    db.TestConfigNets.RemoveRange(existingProgram.Nets);
                    db.SaveChanges();

                    foreach (var net in config.Nets)
                    {
                        net.NetConfigId = 0;
                        net.ConfigId = existingProgram.ConfigId;
                    }

                    db.TestConfigNets.AddRange(config.Nets);
                }
                else
                {
                    config.CreatedAt = DateTime.UtcNow;
                    config.UpdatedAt = DateTime.UtcNow;

                    db.TestConfigurations.Add(config);
                }

                db.SaveChanges();

                new SystemLogService().Log(
                    Models.SystemLog.LogSeverity.Info, "Test Configuration",
                    existingProgram != null ? "Update Test Config" : "Create Test Config",
                    $"Test config '{config.ConfigName}' saved for model {config.ProductModel} with {config.Nets.Count} nets.");
            }
        }

        // Rewrites a net's values into the base unit for its test type; Tolerance is a percentage.
        private static void NormalizeNetUnits(TestConfigNet net, IReadOnlyDictionary<int, string> testTypeNamesById)
        {
            string? testTypeName = net.TestType?.TestTypeName;
            if (string.IsNullOrWhiteSpace(testTypeName) && net.TestTypeId != 0)
            {
                testTypeNamesById.TryGetValue(net.TestTypeId, out testTypeName);
            }

            string key = string.IsNullOrWhiteSpace(testTypeName) ? string.Empty : testTypeName.Trim().ToUpperInvariant();
            string enteredUnit = net.MeasurementUnit;

            // No unit means it is already in base units, and the converter would now throw.
            if (string.IsNullOrWhiteSpace(enteredUnit))
            {
                net.MeasurementUnit = UnitConversionHelper.GetBaseUnit(key);
                return;
            }

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
            using var db = new AppDbContext();

            // Two statements, no rows pulled across.
            db.TestConfigurations.Where(c => c.IsActive && c.ConfigName != configName)
                .ExecuteUpdate(s => s.SetProperty(c => c.IsActive, false));

            db.TestConfigurations.Where(c => c.ConfigName == configName)
                .ExecuteUpdate(s => s.SetProperty(c => c.IsActive, true));
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

        // Configurations filtered by name, newest first, filtered in the database.
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
                // ToUpper + Contains is what EF can translate to SQL UPPER(...) LIKE.
                q = q.Where(c => c.ConfigName != null && c.ConfigName.ToUpper().Contains(key));
            }

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

            int netCount = cfg.Nets.Count;

            // Nets cascade now that ConfigId is a real foreign key.
            db.TestConfigurations.Remove(cfg);
            db.SaveChanges();

            new SystemLogService().Log(
                Models.SystemLog.LogSeverity.Warning, "Test Configuration", "Delete Test Config",
                $"Test config '{configName}' deleted along with {netCount} nets.");

            return true;
        }

        public TestType? GetTestTypeByName(string testTypeName)
        {
            if (string.IsNullOrWhiteSpace(testTypeName)) return null;
            using var db = new AppDbContext();
            string key = testTypeName.Trim().ToUpperInvariant();
            return db.TestTypes.FirstOrDefault(t => t.TestTypeName != null && t.TestTypeName.ToUpper() == key);
        }

        // Dropdown items for colTestType, read live from the TestTypes table.
        public List<string> GetTestTypeNames()
        {
            using var db = new AppDbContext();
            return db.TestTypes.OrderBy(t => t.TestTypeName).Select(t => t.TestTypeName).ToList();
        }

        // Parses a flat JSON array of nets from disk.
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