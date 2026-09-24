using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            if (!SessionManager.HasPermission("Action.TestConfig.Edit"))
                throw new UnauthorizedAccessException("You do not have permission to save test configurations.");

            using (var db = new AppDbContext())
            {
                var dbTestTypes = db.TestTypes.ToDictionary(t => t.Id);
                var testTypeNamesById = dbTestTypes.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.TestTypeName);

                foreach (var net in config.Nets)
                {
                    NormalizeNetUnits(net, testTypeNamesById);

                    if (net.TestTypeId != 0 && dbTestTypes.TryGetValue(net.TestTypeId, out var trackedType))
                    {
                        net.TestType = trackedType;
                    }
                    else if (net.TestType != null && dbTestTypes.TryGetValue(net.TestType.Id, out var typeByNav))
                    {
                        net.TestTypeId = typeByNav.Id;
                        net.TestType = typeByNav;
                    }
                    else
                    {
                        net.TestType = null;
                    }
                }

                if (config.TestTypes != null && config.TestTypes.Count > 0)
                {
                    config.TestTypes = config.TestTypes
                        .Where(t => t != null && dbTestTypes.ContainsKey(t.Id))
                        .Select(t => dbTestTypes[t.Id])
                        .GroupBy(t => t.Id)
                        .Select(g => g.First())
                        .ToList();
                }
                else
                {
                    config.TestTypes = config.Nets
                        .Where(n => n.TestType != null)
                        .Select(n => n.TestType!)
                        .GroupBy(t => t.Id)
                        .Select(g => g.First())
                        .ToList();
                }

                var existingProgram = db.TestConfigurations
                    .Include(c => c.Nets)
                    .Include(c => c.TestTypes)
                    .FirstOrDefault(c => c.ConfigName == config.ConfigName);

                if (existingProgram != null)
                {
                    existingProgram.ProductModelName = config.ProductModelName;
                    existingProgram.Description = config.Description;
                    existingProgram.MaxWireResistance = config.MaxWireResistance;
                    existingProgram.MinInsulationResistance = config.MinInsulationResistance;
                    existingProgram.MinComponentResistance = config.MinComponentResistance;
                    existingProgram.HipotVoltage = config.HipotVoltage;
                    existingProgram.ShortTest = config.ShortTest;
                    existingProgram.UpdatedAt = DateTime.UtcNow;
                    existingProgram.TestTypes = config.TestTypes;

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
                    $"Test config '{config.ConfigName}' saved for model {config.ProductModelName} with {config.Nets.Count} nets.");
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

            if (net.ExpectedValue2.HasValue)
                net.ExpectedValue2 = UnitConversionHelper.ConvertToBaseUnit(key, net.ExpectedValue2.Value, enteredUnit);

            if (net.LowerLimit2.HasValue)
                net.LowerLimit2 = UnitConversionHelper.ConvertToBaseUnit(key, net.LowerLimit2.Value, enteredUnit);

            if (net.UpperLimit2.HasValue)
                net.UpperLimit2 = UnitConversionHelper.ConvertToBaseUnit(key, net.UpperLimit2.Value, enteredUnit);

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
                var cfg = db.TestConfigurations
                    .Include(c => c.Nets)
                    .ThenInclude(n => n.TestType)
                    .Include(c => c.TestTypes)
                    .FirstOrDefault(c => c.IsActive);

                EnsureTestTypesPopulated(cfg);
                return cfg;
            }
        }

        public TestConfiguration? GetConfigurationByName(string configName)
        {
            using var db = new AppDbContext();
            var cfg = db.TestConfigurations
                .Include(c => c.Nets)
                .ThenInclude(n => n.TestType)
                .Include(c => c.TestTypes)
                .FirstOrDefault(c => c.ConfigName == configName);

            EnsureTestTypesPopulated(cfg);
            return cfg;
        }

        // Configurations filtered by name, newest first, filtered in the database.
        public List<TestConfiguration> GetConfigurations(string? configNameFilter)
        {
            using var db = new AppDbContext();
            var q = db.TestConfigurations
                .Include(c => c.Nets)
                .ThenInclude(n => n.TestType)
                .Include(c => c.TestTypes)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(configNameFilter))
            {
                string key = configNameFilter.Trim().ToUpperInvariant();
                // ToUpper + Contains is what EF can translate to SQL UPPER(...) LIKE.
                q = q.Where(c => c.ConfigName != null && c.ConfigName.ToUpper().Contains(key));
            }

            q = q.OrderByDescending(c => (c.UpdatedAt ?? c.CreatedAt));

            var list = q.ToList();
            foreach (var cfg in list)
            {
                EnsureTestTypesPopulated(cfg);
            }
            return list;
        }

        private static void EnsureTestTypesPopulated(TestConfiguration? config)
        {
            if (config == null) return;
            if (config.TestTypes == null || config.TestTypes.Count == 0)
            {
                config.TestTypes = config.Nets
                    .Where(n => n.TestType != null)
                    .Select(n => n.TestType!)
                    .GroupBy(t => t.Id)
                    .Select(g => g.First())
                    .ToList();
            }
        }

        public bool DeleteConfiguration(string configName)
        {
            if (!SessionManager.HasPermission("Action.TestConfig.Edit"))
                throw new UnauthorizedAccessException("You do not have permission to delete test configurations.");

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

        public string ConvertToProgramText(TestConfiguration config, int testPoint)
        {
            if (config == null) return string.Empty;

            var sb = new System.Text.StringBuilder();

            sb.AppendLine("TESTDEFAULTS");
            sb.AppendLine("BEGIN");
            sb.AppendLine("    TESTER , 8100, 448, AHED-64");
            sb.AppendLine("    STARTEVENT , ATTACHED");
            sb.AppendLine("    SERIALENTRY , NONE");
            sb.AppendLine("    TESTTYPE , BUILD");
            sb.AppendLine("    SHOWCONNECTORS , On");
            sb.AppendLine("    ERRORDETAILS , On");
            sb.AppendLine("    MEASUREDVALUES , Off");
            sb.AppendLine("    SHOWCURRENTINSTRUCTION , On");
            sb.AppendLine("    SOUND , On");
            sb.AppendLine("    DEBUG , Off");
            sb.AppendLine("    ERRORLOCATION , Off, 10");
            sb.AppendLine("    USECOMPONENTRES , On");
            sb.AppendLine("    USELOTID , Off");
            sb.AppendLine("    FAVENABLED , Off");
            sb.AppendLine("    FIRSTARTICLEVERIFIED , Off");
            sb.AppendLine("    ASSOCIATEDFILE , ");
            sb.AppendLine("    HVIRVALUES , Off");
            sb.AppendLine("    USELABELS , Off");
            sb.AppendLine("    USESSMARTLIGHTS , Off");
            sb.AppendLine("    COMMONHARDWARE_4W_PATTERN_OFFSET , 32");
            sb.AppendLine("    DISPLAYCYCLESASTIME , Off");
            sb.AppendLine("END");
            sb.AppendLine();

            sb.AppendLine("GRAPHIC_CONN_TYPES");
            sb.AppendLine("BEGIN");
            sb.AppendLine("     AHED-64 , 64, 1, 0, 112, 96, PIN_GROUP, 0, 2, 0, 112, 92, PIN_GROUP, 0, 3, 0, 108, 96, PIN_GROUP, 0, 4, 0, 108, 92, PIN_GROUP, 0, 5, 0, 104, 96, PIN_GROUP, 0, 6, 0, 104, 92, PIN_GROUP, 0, 7, 0, 100, 96, PIN_GROUP, 0, 8, 0, 100, 92, PIN_GROUP, 0, 9, 0, 96, 96, PIN_GROUP, 0, 10, 0, 96, 92, PIN_GROUP, 0, 11, 0, 92, 96, PIN_GROUP, 0, 12, 0, 92, 92, PIN_GROUP, 0, 13, 0, 88, 96, PIN_GROUP, 0, 14, 0, 88, 92, PIN_GROUP, 0, 15, 0, 84, 96, PIN_GROUP, 0, 16, 0, 84, 92, PIN_GROUP, 0, 17, 0, 80, 96, PIN_GROUP, 0, 18, 0, 80, 92, PIN_GROUP, 0, 19, 0, 76, 96, PIN_GROUP, 0, 20, 0, 76, 92, PIN_GROUP, 0, 21, 0, 72, 96, PIN_GROUP, 0, 22, 0, 72, 92, PIN_GROUP, 0, 23, 0, 68, 96, PIN_GROUP, 0, 24, 0, 68, 92, PIN_GROUP, 0, 25, 0, 64, 96, PIN_GROUP, 0, 26, 0, 64, 92, PIN_GROUP, 0, 27, 0, 60, 96, PIN_GROUP, 0, 28, 0, 60, 92, PIN_GROUP, 0, 29, 0, 56, 96, PIN_GROUP, 0, 30, 0, 56, 92, PIN_GROUP, 0, 31, 0, 52, 96, PIN_GROUP, 0, 32, 0, 52, 92, PIN_GROUP, 0, 33, 0, 48, 96, PIN_GROUP, 0, 34, 0, 48, 92, PIN_GROUP, 0, 35, 0, 44, 96, PIN_GROUP, 0, 36, 0, 44, 92, PIN_GROUP, 0, 37, 0, 40, 96, PIN_GROUP, 0, 38, 0, 40, 92, PIN_GROUP, 0, 39, 0, 36, 96, PIN_GROUP, 0, 40, 0, 36, 92, PIN_GROUP, 0, 41, 0, 32, 96, PIN_GROUP, 0, 42, 0, 32, 92, PIN_GROUP, 0, 43, 0, 28, 96, PIN_GROUP, 0, 44, 0, 28, 92, PIN_GROUP, 0, 45, 0, 24, 96, PIN_GROUP, 0, 46, 0, 24, 92, PIN_GROUP, 0, 47, 0, 20, 96, PIN_GROUP, 0, 48, 0, 20, 92, PIN_GROUP, 0, 49, 0, 16, 96, PIN_GROUP, 0, 50, 0, 16, 92, PIN_GROUP, 0, 51, 0, 12, 96, PIN_GROUP, 0, 52, 0, 12, 92, PIN_GROUP, 0, 53, 0, 8, 96, PIN_GROUP, 0, 54, 0, 8, 92, PIN_GROUP, 0, 55, 0, 4, 96, PIN_GROUP, 0, 56, 0, 4, 92, PIN_GROUP, 0, 57, 0, 0, 96, PIN_GROUP, 0, 58, 0, 0, 92, PIN_GROUP, 0, 59, 0, -4, 96, PIN_GROUP, 0, 60, 0, -4, 92, PIN_GROUP, 0, 61, 0, -8, 96, PIN_GROUP, 0, 62, 0, -8, 92, PIN_GROUP, 0, 63, 0, -12, 96, PIN_GROUP, 0, 64, 0, -12, 92, PIN_GROUP, 0, STYLE, Standard, , 0, 3, 1");
            sb.AppendLine("    GraphicView, 1");
            sb.AppendLine("    Strapping, 0, 0, 0, 0, 0, 0, 0, 0, 0");
            sb.AppendLine("    HasImageData, TRUE");
            sb.AppendLine("END");
            sb.AppendLine();

            sb.AppendLine("INTERFACING");
            sb.AppendLine("BEGIN");
            testPoint = (testPoint + 63) / 64;
            for (int j = 1; j <= testPoint; j++)
            {
                sb.AppendLine($"     J{j} ,  AHED-64 ");
            }
            sb.AppendLine("END");
            sb.AppendLine();

            sb.AppendLine("ATTACH");
            sb.AppendLine("BEGIN");
            for (int j = 1; j <= testPoint; j++)
            {
                int startPin = (j - 1) * 64 + 1;
                var pins = Enumerable.Range(startPin, 64);
                sb.AppendLine($"     J{j} , {string.Join(", ", pins)}");
            }
            sb.AppendLine("END");
            sb.AppendLine();

            double connResist = config.MaxWireResistance > 0 ? config.MaxWireResistance : 6.0;
            double compResist = config.MinComponentResistance > 0 ? config.MinComponentResistance : 100.0;
            double insResist = config.MinInsulationResistance > 0 ? config.MinInsulationResistance : 500000.0;

            string insResStr;
            if (insResist >= 1e6)
                insResStr = (insResist / 1e6).ToString("0.##", System.Globalization.CultureInfo.InvariantCulture) + " MOhm";
            else if (insResist >= 1e3)
                insResStr = (insResist / 1e3).ToString("0.##", System.Globalization.CultureInfo.InvariantCulture) + " kOhm";
            else
                insResStr = insResist.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture) + " Ohm";

            string compResStr;
            if (compResist >= 1e6)
                compResStr = (compResist / 1e6).ToString("0.##", System.Globalization.CultureInfo.InvariantCulture) + " MOhm";
            else if (compResist >= 1e3)
                compResStr = (compResist / 1e3).ToString("0.##", System.Globalization.CultureInfo.InvariantCulture) + " kOhm";
            else
                compResStr = compResist.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture) + " Ohm";

            sb.AppendLine("PARAMETERS");
            sb.AppendLine("BEGIN");
            sb.AppendLine($"    WIRERES , {connResist.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture)} Ohm");
            sb.AppendLine("    RESISTORRES , 100 Ohm");
            //sb.AppendLine($"    RESISTORRES , {compResStr}");
            sb.AppendLine("    RESISTORTOL ,  10 %");
            sb.AppendLine("    FORWARDV , 700 mV");
            sb.AppendLine("    REVERSEV , > 6.00 V");
            sb.AppendLine("    FORWARDTOL ,  20 %");
            sb.AppendLine("    REVERSETOL ,  20 %");
            sb.AppendLine("    SHORTSRES , 500 kOhm");
            sb.AppendLine("    CAPTOLERANCE ,  20 %");
            sb.AppendLine("    WIRERESTARE , 0.0 Ohm");
            sb.AppendLine("    DELAYRESISTANCERES , 0.1 Ohm");
            sb.AppendLine("    DELAYRESISTANCETOL ,  1 %");
            sb.AppendLine("    COMMENTDELAY , 5.00 s");
            sb.AppendLine("    4W_WIRERES , 1.00 Ohm");
            sb.AppendLine("    4W_WIRERESTARE , 0 mOhm");
            sb.AppendLine("    4W_WIREMIN , 0 mOhm");
            sb.AppendLine("    4W_RESISTORRES , 10.0 Ohm");
            sb.AppendLine("    4W_RESISTORTOL ,  1 %");
            sb.AppendLine("    COMPONENT_RESISTANCE , 0 Ohm");
            sb.AppendLine("    INDUCTANCE , 1.00 uH");
            sb.AppendLine("    INDUCTANCE_TOL ,  10 %");
            sb.AppendLine("    PIN_CAPACITANCE , 5.0 pF");
            sb.AppendLine("    PROBE_R_MIN , 0.0 Ohm");
            sb.AppendLine("    PROBE_R_MAX , 5.0 Ohm");
            sb.AppendLine("    CAPACITANCE , 1.00 nF");
            sb.AppendLine("END");
            sb.AppendLine();

            sb.AppendLine("INSTRUCTIONS");
            sb.AppendLine("BEGIN");

            var nets = config.Nets?.OrderBy(n => n.SequenceNo).ToList() ?? [];
            foreach (var net in nets)
            {
                string typeName = net.TestType?.TestTypeName?.ToUpperInvariant() ?? "";
                string fromPin = FormatPin(net.FromAddress);
                string toPin = FormatPin(net.ToAddress);

                if (string.IsNullOrWhiteSpace(typeName) || ContinuityLikeTestTypes.Contains(typeName))
                {
                    double wireLimit = (net.UpperLimit.HasValue && net.UpperLimit.Value > 0)
                        ? net.UpperLimit.Value
                        : connResist;
                    sb.AppendLine($"    WIRE,   {fromPin},  {toPin}, {wireLimit.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture)} Ohm");
                }
                else if (typeName == "RESISTOR")
                {
                    double val = net.ExpectedValue ?? 100.0;
                    string valFormatted = val >= 1e6
                        ? (val / 1e6).ToString("0.##", System.Globalization.CultureInfo.InvariantCulture) + " MOhm"
                        : val >= 1e3
                            ? (val / 1e3).ToString("0.##", System.Globalization.CultureInfo.InvariantCulture) + " kOhm"
                            : val.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture) + " Ohm";

                    double tol = net.Tolerance ?? 10.0;
                    sb.AppendLine($"    RESISTOR,   {fromPin},  {toPin}, {valFormatted},  {tol.ToString("0", System.Globalization.CultureInfo.InvariantCulture)} %");
                }
                else if (typeName == "DIODE")
                {
                    if (net.ExpectedValue2.HasValue || (net.ExpectedValue.HasValue && net.ExpectedValue.Value > 0))
                    {
                        double fwdV = net.ExpectedValue ?? 0.70;
                        double fwdTol = net.Tolerance ?? 20.0;
                        double revV = net.ExpectedValue2 ?? 6.00;
                        double revTol = net.Tolerance2 ?? 20.0;

                        string fwdStr = fwdV < 1.0
                            ? (fwdV * 1000).ToString("0", System.Globalization.CultureInfo.InvariantCulture) + " mV"
                            : fwdV.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + " V";
                        string revStr = "> " + revV.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + " V";

                        sb.AppendLine($"    DIODE,   {fromPin},  {toPin}, {fwdStr},  {fwdTol.ToString("0", System.Globalization.CultureInfo.InvariantCulture)} %, {revStr},  {revTol.ToString("0", System.Globalization.CultureInfo.InvariantCulture)} %");
                    }
                    else
                    {
                        sb.AppendLine($"    DIODE,   {fromPin},  {toPin}");
                    }
                }
                else if (typeName == "CAPACITOR")
                {
                    double val = net.ExpectedValue ?? 1e-9;
                    string valFormatted = val >= 1e-3
                        ? (val * 1e3).ToString("0.##", System.Globalization.CultureInfo.InvariantCulture) + " mF"
                        : val >= 1e-6
                            ? (val * 1e6).ToString("0.##", System.Globalization.CultureInfo.InvariantCulture) + " uF"
                            : val >= 1e-9
                                ? (val * 1e9).ToString("0.##", System.Globalization.CultureInfo.InvariantCulture) + " nF"
                                : val >= 1e-12
                                    ? (val * 1e12).ToString("0.##", System.Globalization.CultureInfo.InvariantCulture) + " pF"
                                    : val.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture) + " F";

                    double tol = net.Tolerance ?? 20.0;
                    sb.AppendLine($"    CAPACITORTEST,   {fromPin},  {toPin}, {valFormatted},  {tol.ToString("0", System.Globalization.CultureInfo.InvariantCulture)} %");
                }
                else
                {
                    sb.AppendLine($"    {typeName},   {fromPin},  {toPin}");
                }
            }

            sb.AppendLine("END");
            sb.AppendLine();

            sb.AppendLine("NETNAMES");
            sb.AppendLine("BEGIN");
            sb.AppendLine("END");
            sb.AppendLine();

            sb.AppendLine("CUSTOM_LABELS");
            sb.AppendLine("BEGIN");
            sb.AppendLine("END");

            return sb.ToString();
        }

        public static string FormatPin(int address)
        {
            if (address <= 0) return "$J1-1";
            int connIdx = ((address - 1) / 64) + 1;
            int pinIdx = ((address - 1) % 64) + 1;
            return $"J{connIdx}-{pinIdx}";
        }
        //public static string FormatPin(int address)
        //{
        //    if (address <= 0) return "$J1-1";
        //    int connIdx = ((address - 1) / 64) + 1;
        //    int pinIdx = ((address - 1) % 64) + 1;
        //    return $"$J{connIdx}-{pinIdx}";
        //}

        public TestConfiguration ConvertToTestConfig(string configText)
        {
            if (string.IsNullOrWhiteSpace(configText))
                return new TestConfiguration();

            string trimmedStart = configText.TrimStart();
            if (trimmedStart.StartsWith("(") || trimmedStart.StartsWith("*") || configText.Contains("** TEST METHOD", StringComparison.OrdinalIgnoreCase))
            {
                return ConvertToTestConfig1100(configText);
            }

            var config = new TestConfiguration();
            var lines = configText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            string currentSection = string.Empty;
            double connResist = 6.0;
            double defaultForwardV = 0.70;
            double defaultForwardTol = 20.0;
            double defaultReverseV = 6.00;
            double defaultReverseTol = 20.0;
            var wireTestType = GetTestTypeByName("WIRE");
            var resistorTestType = GetTestTypeByName("RESISTOR");
            var diodeTestType = GetTestTypeByName("DIODE");
            var capacitorTestType = GetTestTypeByName("CAPACITOR");

            var netNamesByPin = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (var line in lines)
            {
                string trimmed = line.Trim();
                if (string.IsNullOrWhiteSpace(trimmed)) continue;

                if (trimmed.Equals("BEGIN", StringComparison.OrdinalIgnoreCase)) continue;
                if (trimmed.Equals("END", StringComparison.OrdinalIgnoreCase))
                {
                    currentSection = string.Empty;
                    continue;
                }

                if (trimmed.Equals("TESTDEFAULTS", StringComparison.OrdinalIgnoreCase) ||
                    trimmed.Equals("GRAPHIC_CONN_TYPES", StringComparison.OrdinalIgnoreCase) ||
                    trimmed.Equals("INTERFACING", StringComparison.OrdinalIgnoreCase) ||
                    trimmed.Equals("ATTACH", StringComparison.OrdinalIgnoreCase) ||
                    trimmed.Equals("PARAMETERS", StringComparison.OrdinalIgnoreCase) ||
                    trimmed.Equals("INSTRUCTIONS", StringComparison.OrdinalIgnoreCase) ||
                    trimmed.Equals("NETNAMES", StringComparison.OrdinalIgnoreCase) ||
                    trimmed.Equals("CUSTOM_LABELS", StringComparison.OrdinalIgnoreCase))
                {
                    currentSection = trimmed.ToUpperInvariant();
                    continue;
                }

                if (currentSection == "PARAMETERS")
                {
                    var parts = trimmed.Split(new[] { ',' }, 2);
                    if (parts.Length == 2)
                    {
                        string key = parts[0].Trim().ToUpperInvariant();
                        string val = parts[1].Trim();

                        if (key == "WIRERES")
                        {
                            if (TryParseValueWithUnit(val, out double r))
                            {
                                config.MaxWireResistance = r;
                                connResist = r;
                            }
                        }
                        else if (key == "SHORTSRES")
                        {
                            if (TryParseValueWithUnit(val, out double ir))
                            {
                                config.MinInsulationResistance = ir;
                            }
                        }
                        else if (key == "RESISTORRES" || key == "COMPONENT_RESISTANCE")
                        {
                            if (TryParseValueWithUnit(val, out double cr))
                            {
                                config.MinComponentResistance = cr;
                            }
                        }
                        else if (key == "FORWARDV")
                        {
                            if (TryParseValueWithUnit(val, out double fv))
                            {
                                defaultForwardV = fv;
                            }
                        }
                        else if (key == "REVERSEV")
                        {
                            if (TryParseValueWithUnit(val, out double rv))
                            {
                                defaultReverseV = rv;
                            }
                        }
                        else if (key == "FORWARDTOL")
                        {
                            if (TryParseTolerance(val, out double ft))
                            {
                                defaultForwardTol = ft;
                            }
                        }
                        else if (key == "REVERSETOL")
                        {
                            if (TryParseTolerance(val, out double rt))
                            {
                                defaultReverseTol = rt;
                            }
                        }
                    }
                }
                else if (currentSection == "INSTRUCTIONS")
                {
                    var tokens = trimmed.Split(',').Select(t => t.Trim()).ToArray();
                    if (tokens.Length >= 3)
                    {
                        string instType = tokens[0].ToUpperInvariant();
                        string fromPinStr = tokens[1];
                        string toPinStr = tokens[2];

                        var net = new TestConfigNet
                        {
                            SequenceNo = config.Nets.Count + 1,
                            FromAddress = ParsePin(fromPinStr),
                            FromLabel = fromPinStr,
                            ToAddress = ParsePin(toPinStr),
                            ToLabel = toPinStr,
                            HarnessGroup = "",
                            NetId = ""
                        };

                        if (instType == "WIRE" || ContinuityLikeTestTypes.Contains(instType))
                        {
                            double wireLimit = connResist;
                            if (tokens.Length >= 4 && TryParseValueWithUnit(tokens[3], out double limitVal))
                            {
                                wireLimit = limitVal;
                            }
                            net.LowerLimit = 0.0;
                            net.UpperLimit = wireLimit;
                            net.MeasurementUnit = "Ω";
                            net.TestType = wireTestType;
                            net.TestTypeId = wireTestType?.Id ?? 0;
                        }
                        else if (instType == "RESISTOR")
                        {
                            double expVal = 100.0;
                            double tolVal = 10.0;
                            if (tokens.Length >= 4 && TryParseValueWithUnit(tokens[3], out double parsedExp))
                            {
                                expVal = parsedExp;
                            }
                            if (tokens.Length >= 5 && TryParseTolerance(tokens[4], out double parsedTol))
                            {
                                tolVal = parsedTol;
                            }
                            net.ExpectedValue = expVal;
                            net.Tolerance = tolVal;
                            net.LowerLimit = expVal * (1.0 - tolVal / 100.0);
                            net.UpperLimit = expVal * (1.0 + tolVal / 100.0);
                            net.MeasurementUnit = "Ω";
                            net.TestType = resistorTestType;
                            net.TestTypeId = resistorTestType?.Id ?? 0;
                        }
                        else if (instType == "DIODE")
                        {
                            double fwdV = defaultForwardV;
                            double fwdTol = defaultForwardTol;
                            double revV = defaultReverseV;
                            double revTol = defaultReverseTol;

                            if (tokens.Length >= 4 && TryParseValueWithUnit(tokens[3], out double parsedFwdV))
                            {
                                fwdV = parsedFwdV;
                            }
                            if (tokens.Length >= 5 && TryParseTolerance(tokens[4], out double parsedFwdTol))
                            {
                                fwdTol = parsedFwdTol;
                            }
                            if (tokens.Length >= 6 && TryParseValueWithUnit(tokens[5], out double parsedRevV))
                            {
                                revV = parsedRevV;
                            }
                            if (tokens.Length >= 7 && TryParseTolerance(tokens[6], out double parsedRevTol))
                            {
                                revTol = parsedRevTol;
                            }

                            net.ExpectedValue = fwdV;
                            net.Tolerance = fwdTol;
                            net.LowerLimit = fwdV * (1.0 - fwdTol / 100.0);
                            net.UpperLimit = fwdV * (1.0 + fwdTol / 100.0);

                            net.ExpectedValue2 = revV;
                            net.Tolerance2 = revTol;
                            net.LowerLimit2 = revV * (1.0 - revTol / 100.0);
                            net.UpperLimit2 = revV;

                            net.MeasurementUnit = "V";
                            net.TestType = diodeTestType;
                            net.TestTypeId = diodeTestType?.Id ?? 0;
                        }
                        else if (instType == "CAPACITOR")
                        {
                            if (tokens.Length >= 4 && TryParseValueWithUnit(tokens[3], out double expVal))
                            {
                                net.ExpectedValue = expVal;
                            }
                            if (tokens.Length >= 5 && TryParseTolerance(tokens[4], out double tolVal))
                            {
                                net.Tolerance = tolVal;
                            }
                            net.MeasurementUnit = "F";
                            net.TestType = capacitorTestType;
                            net.TestTypeId = capacitorTestType?.Id ?? 0;
                        }
                        else
                        {
                            var tt = GetTestTypeByName(instType);
                            net.TestType = tt;
                            net.TestTypeId = tt?.Id ?? 0;
                        }

                        config.Nets.Add(net);
                    }
                }
                else if (currentSection == "NETNAMES")
                {
                    var parts = trimmed.Split(new[] { ',' }, 2);
                    if (parts.Length == 2)
                    {
                        string netName = parts[0].Trim();
                        string pin = parts[1].Trim();
                        if (!string.IsNullOrEmpty(pin) && !string.IsNullOrEmpty(netName))
                        {
                            netNamesByPin[pin] = netName;
                        }
                    }
                }
            }

            int netIdx = 1;
            foreach (var net in config.Nets)
            {
                string pinClean = net.FromLabel.TrimStart('$');
                string pinWithDollar = "$" + pinClean;

                if (netNamesByPin.TryGetValue(net.FromLabel, out string? name) ||
                    netNamesByPin.TryGetValue(pinClean, out name) ||
                    netNamesByPin.TryGetValue(pinWithDollar, out name))
                {
                    net.NetId = name;
                }
                else
                {
                    net.NetId = $"Net{netIdx}";
                }
                netIdx++;
            }

            return config;
        }

        public TestConfiguration ConvertToTestConfig1100(string configText)
        {
            var config = new TestConfiguration();
            if (string.IsNullOrWhiteSpace(configText))
                return config;

            var lines = configText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            bool inWireList = false;
            bool inCheckComponents = false;
            double connResist = 5.0; // default

            var currentNetPins = new System.Collections.Generic.List<string>();
            string currentNetGroupId = "";
            var wireTestType = GetTestTypeByName("WIRE");

            void FlushCurrentNetPins()
            {
                if (currentNetPins.Count < 2)
                {
                    currentNetPins.Clear();
                    return;
                }

                for (int i = 0; i < currentNetPins.Count - 1; i++)
                {
                    string fromPinStr = currentNetPins[i];
                    string toPinStr = currentNetPins[i + 1];

                    if (fromPinStr == toPinStr) continue;

                    if (fromPinStr.StartsWith("J3") && toPinStr.StartsWith("J1"))
                    {
                        string temp = fromPinStr;
                        fromPinStr = toPinStr;
                        toPinStr = temp;
                    }

                    var net = new TestConfigNet
                    {
                        SequenceNo = config.Nets.Count + 1,
                        FromAddress = ParsePin(fromPinStr),
                        FromLabel = fromPinStr,
                        ToAddress = ParsePin(toPinStr),
                        ToLabel = toPinStr,
                        HarnessGroup = "",
                        NetId = "",
                        UpperLimit = connResist
                    };

                    if (wireTestType != null)
                    {
                        net.TestTypeId = wireTestType.Id;
                        net.TestType = wireTestType;
                    }

                    config.Nets.Add(net);
                }

                currentNetPins.Clear();
            }

            foreach (var line in lines)
            {
                string trimmed = line.Trim();
                if (string.IsNullOrWhiteSpace(trimmed)) continue;

                if (trimmed == ")")
                {
                    FlushCurrentNetPins();
                    break;
                }

                if (trimmed.StartsWith("CONNECTION RESIS", StringComparison.OrdinalIgnoreCase))
                {
                    var parts = trimmed.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 3 && double.TryParse(parts[2], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double cr))
                    {
                        double mult = GetUnitMultiplier(parts, 3);
                        config.MaxWireResistance = cr * mult;
                        connResist = config.MaxWireResistance;
                    }
                    continue;
                }

                if (trimmed.StartsWith("COMPONENT RESIS", StringComparison.OrdinalIgnoreCase))
                {
                    string cleanLine = trimmed.Replace(">", " ");
                    var parts = cleanLine.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 3 && double.TryParse(parts[2], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double cr))
                    {
                        double mult = GetUnitMultiplier(parts, 3);
                        config.MinComponentResistance = cr * mult;
                    }
                    continue;
                }

                if (trimmed.StartsWith("LV INSULATION", StringComparison.OrdinalIgnoreCase))
                {
                    var parts = trimmed.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    int numIndex = -1;
                    for (int idx = 2; idx < parts.Length; idx++)
                    {
                        if (double.TryParse(parts[idx], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out _))
                        {
                            numIndex = idx;
                            break;
                        }
                    }
                    if (numIndex != -1 && double.TryParse(parts[numIndex], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double cr))
                    {
                        double mult = GetUnitMultiplier(parts, numIndex + 1);
                        config.MinInsulationResistance = cr * mult;
                    }
                    continue;
                }

                if (trimmed.StartsWith("HIPOT VOLTAGE", StringComparison.OrdinalIgnoreCase))
                {
                    var parts = trimmed.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 3)
                    {
                        string valStr = parts[2].Trim().ToUpperInvariant();
                        config.HipotVoltage = valStr != "OFF" && valStr != "0" && valStr != "DISABLED";
                    }
                    continue;
                }

                if (trimmed.StartsWith("** TEST METHOD", StringComparison.OrdinalIgnoreCase))
                {
                    FlushCurrentNetPins();
                    inWireList = true;
                    inCheckComponents = false;
                    continue;
                }

                if (trimmed.StartsWith("CHECK COMPONENTS", StringComparison.OrdinalIgnoreCase))
                {
                    FlushCurrentNetPins();
                    inWireList = false;
                    inCheckComponents = true;
                    continue;
                }

                if (inWireList)
                {
                    var parts = trimmed.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length > 0)
                    {
                        bool isNewNetHeader = int.TryParse(parts[0], out int seqNum);
                        if (isNewNetHeader)
                        {
                            FlushCurrentNetPins();
                            currentNetGroupId = parts[0];
                            for (int pIdx = 1; pIdx < parts.Length; pIdx++)
                            {
                                currentNetPins.Add(parts[pIdx]);
                            }
                        }
                        else
                        {
                            // Continuation line of the current multi-pin net
                            for (int pIdx = 0; pIdx < parts.Length; pIdx++)
                            {
                                currentNetPins.Add(parts[pIdx]);
                            }
                        }
                    }
                }
                else if (inCheckComponents)
                {
                    var parts = trimmed.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 4 && int.TryParse(parts[0], out int seq))
                    {
                        string testTypeName = parts[1];
                        string mappedTestTypeName = testTypeName;
                        if (testTypeName == "RESIS") mappedTestTypeName = "RESISTOR";
                        else if (testTypeName == "CAP") mappedTestTypeName = "CAPACITOR";

                        string fromPinStr = parts[2];
                        string toPinStr = parts[3];

                        if (fromPinStr.StartsWith("J3") && toPinStr.StartsWith("J1"))
                        {
                            string temp = fromPinStr;
                            fromPinStr = toPinStr;
                            toPinStr = temp;
                        }

                        var net = new TestConfigNet
                        {
                            SequenceNo = config.Nets.Count + 1,
                            FromAddress = ParsePin(fromPinStr),
                            FromLabel = fromPinStr,
                            ToAddress = ParsePin(toPinStr),
                            ToLabel = toPinStr
                        };

                        if (parts.Length >= 5)
                        {
                            if (double.TryParse(parts[4], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double exp))
                                net.ExpectedValue = exp;
                            
                            int i = 5;
                            var unitParts = new System.Collections.Generic.List<string>();
                            while (i < parts.Length && !double.TryParse(parts[i], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out _))
                            {
                                unitParts.Add(parts[i]);
                                i++;
                            }
                            
                            double multiplier = 1.0;
                            if (unitParts.Count > 0)
                            {
                                string fullUnit = string.Join("", unitParts);
                                if (fullUnit.StartsWith("K") || fullUnit.StartsWith("k")) multiplier = 1e3;
                                else if (fullUnit.StartsWith("M")) multiplier = 1e6;
                                else if (fullUnit.StartsWith("m")) multiplier = 1e-3;
                                else if (fullUnit.StartsWith("u")) multiplier = 1e-6;
                                else if (fullUnit.StartsWith("n")) multiplier = 1e-9;
                                else if (fullUnit.StartsWith("p")) multiplier = 1e-12;
                            }
                            
                            if (net.ExpectedValue.HasValue)
                            {
                                net.ExpectedValue = net.ExpectedValue.Value * multiplier;
                            }
                            
                            net.MeasurementUnit = "";
                            
                            if (i < parts.Length && double.TryParse(parts[i], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double tol))
                            {
                                net.Tolerance = tol;
                            }
                        }

                        var tt = GetTestTypeByName(mappedTestTypeName);
                        if (tt != null)
                        {
                            net.TestTypeId = tt.Id;
                            net.TestType = tt;
                        }

                        config.Nets.Add(net);
                    }
                }
            }

            FlushCurrentNetPins();

            int sequenceIndex = 1;
            foreach (var n in config.Nets)
            {
                n.SequenceNo = sequenceIndex++;
            }

            return config;
        }

        private static bool TryParseValueWithUnit(string text, out double value)
        {
            value = 0.0;
            if (string.IsNullOrWhiteSpace(text)) return false;
            text = text.Trim();

            var match = System.Text.RegularExpressions.Regex.Match(text, @"^([><\s]*)([-+]?[0-9]*\.?[0-9]+)\s*([a-zA-Z]*)");
            if (!match.Success) return false;

            if (!double.TryParse(match.Groups[2].Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double num))
                return false;

            string unit = match.Groups[3].Value.Trim();
            double multiplier = 1.0;
            if (unit.StartsWith("k", StringComparison.OrdinalIgnoreCase)) multiplier = 1e3;
            else if (unit.StartsWith("M")) multiplier = 1e6;
            else if (unit.StartsWith("m")) multiplier = 1e-3;
            else if (unit.StartsWith("u") || unit.StartsWith("µ")) multiplier = 1e-6;
            else if (unit.StartsWith("n", StringComparison.OrdinalIgnoreCase)) multiplier = 1e-9;
            else if (unit.StartsWith("p", StringComparison.OrdinalIgnoreCase)) multiplier = 1e-12;

            value = num * multiplier;
            return true;
        }

        private static bool TryParseTolerance(string text, out double tolerance)
        {
            tolerance = 0.0;
            if (string.IsNullOrWhiteSpace(text)) return false;
            text = text.Replace("%", "").Trim();
            return double.TryParse(text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out tolerance);
        }

        private static double GetUnitMultiplier(string[] parts, int startIndex)
        {
            if (startIndex >= parts.Length) return 1.0;
            string fullUnit = string.Join("", parts.Skip(startIndex));
            if (fullUnit.StartsWith("K") || fullUnit.StartsWith("k")) return 1e3;
            if (fullUnit.StartsWith("M")) return 1e6;
            if (fullUnit.StartsWith("m")) return 1e-3;
            if (fullUnit.StartsWith("u")) return 1e-6;
            if (fullUnit.StartsWith("n")) return 1e-9;
            if (fullUnit.StartsWith("p")) return 1e-12;
            return 1.0;
        }

        private int ParsePin(string pin)
        {
            if (string.IsNullOrWhiteSpace(pin)) return 0;
            pin = pin.Trim().TrimStart('$');
            if (pin.StartsWith("J", StringComparison.OrdinalIgnoreCase))
            {
                var parts = pin.Substring(1).Split('-');
                if (parts.Length == 2 && int.TryParse(parts[0], out int connNum) && int.TryParse(parts[1], out int pinNum))
                {
                    return (connNum - 1) * 64 + pinNum;
                }
            }
            if (int.TryParse(pin, out int directAddr))
                return directAddr;
            return 0;
        }
    }
}