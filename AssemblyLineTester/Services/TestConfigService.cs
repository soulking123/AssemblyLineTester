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
            int sumPresentTestType = 0;
            foreach (var testType in config.TestTypes)
            {
                sumPresentTestType += testType.TestTypeValue;
            }

            List<string> testTypeCode = ["58D817-6J350",
            "56ACDF-MULTI",
            "56ACDF-MULTI",
            "4344B1-MULTI",
            "56ACDF-MULTI",
            "56ACDF-MULTI",
            "4344B1-MULTI",
            "56ACDF-MULTI",
            "4344B1-MULTI",
            "56ACDF-MULTI",
            "4344B1-MULTI",
            "4344B1-MULTI",
            "56ACDF-MULTI",
            "4344B1-MULTI",
            "4344B1-MULTI",
            "4344B1-MULTI",
            ];

            var sb = new System.Text.StringBuilder();

            sb.AppendLine("* ");
            sb.AppendLine();
            sb.AppendLine("* CRC Signature: YER8C3");
            sb.AppendLine();

            string programName = !string.IsNullOrWhiteSpace(config.ConfigName) ? config.ConfigName : "PROGRAM";
            sb.AppendLine($"({testTypeCode[sumPresentTestType]}");
            sb.AppendLine();
            sb.AppendLine(" J1 D507F1 *[AHED-64,0,0,0,0,0,0,0,0,64] 64 POS. .1\" FEM.");
            sb.AppendLine(" J3 D507F1 *[AHED-64,0,0,0,0,0,0,0,0,64] 64 POS. .1\" FEM.");
            sb.AppendLine();

            double connResist = config.MaxWireResistance > 0 ? config.MaxWireResistance : 10.0;
            double compResist = config.MinComponentResistance > 0 ? config.MinComponentResistance : 10.1;
            double insResist = config.MinInsulationResistance > 0 ? config.MinInsulationResistance : 100000.0;

            string insResStr;
            if (insResist >= 1e6)
                insResStr = (insResist / 1e6).ToString("0.##", System.Globalization.CultureInfo.InvariantCulture) + " M ohm";
            else if (insResist >= 1e3)
                insResStr = (insResist / 1e3).ToString("0.##", System.Globalization.CultureInfo.InvariantCulture) + " K ohm";
            else
                insResStr = insResist.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture) + " ohm";

            string hipotStr = config.HipotVoltage ? "ON" : "OFF";

            string compResStr = compResist == (int)compResist ? ((int)compResist).ToString() : compResist.ToString("F1", System.Globalization.CultureInfo.InvariantCulture);

            
            if (sumPresentTestType != 0)
            {
                sb.AppendLine("WIRE LIST SIGNATURE:80EF1F-8T8M0");
                sb.AppendLine("INSUL TEST PARM SIGNATURE:6F6F0");
            }
            sb.AppendLine($"CONNECTION RESIS  {connResist.ToString("F1", System.Globalization.CultureInfo.InvariantCulture)} ohm");
            if (sumPresentTestType != 0)
            {
                sb.AppendLine($"COMPONENT RESIS >  {compResStr} ohm");
            }
            sb.AppendLine($"LV INSULATION RESIS  {insResStr}");
            sb.AppendLine($"HIPOT VOLTAGE  {hipotStr}");
            sb.AppendLine("** TEST METHOD SINGLE");
            sb.AppendLine();

            var wireNets = new System.Collections.Generic.List<TestConfigNet>();
            var componentNets = new System.Collections.Generic.List<TestConfigNet>();

            foreach (var net in config.Nets.OrderBy(n => n.SequenceNo))
            {
                string typeName = net.TestType?.TestTypeName?.ToUpperInvariant() ?? "";
                if (string.IsNullOrWhiteSpace(typeName) || ContinuityLikeTestTypes.Contains(typeName))
                {
                    wireNets.Add(net);
                }
                else
                {
                    componentNets.Add(net);
                }
            }

            // Group wire nets using Connected Components (Disjoint Set Union) so parallel/connected pins merge into unified net groups
            var parent = new System.Collections.Generic.Dictionary<string, string>();

            string Find(string item)
            {
                if (!parent.ContainsKey(item)) parent[item] = item;
                if (parent[item] != item) parent[item] = Find(parent[item]);
                return parent[item];
            }

            void Union(string a, string b)
            {
                string rootA = Find(a);
                string rootB = Find(b);
                if (rootA != rootB) parent[rootA] = rootB;
            }

            var pinAddresses = new System.Collections.Generic.Dictionary<string, int>();

            foreach (var net in wireNets)
            {
                string fromPin = !string.IsNullOrWhiteSpace(net.FromLabel) ? net.FromLabel : FormatPin(net.FromAddress);
                string toPin = !string.IsNullOrWhiteSpace(net.ToLabel) ? net.ToLabel : FormatPin(net.ToAddress);

                if (!pinAddresses.ContainsKey(fromPin)) pinAddresses[fromPin] = net.FromAddress;
                if (!pinAddresses.ContainsKey(toPin)) pinAddresses[toPin] = net.ToAddress;

                string? groupId = !string.IsNullOrWhiteSpace(net.HarnessGroup) ? net.HarnessGroup : (!string.IsNullOrWhiteSpace(net.NetId) ? net.NetId : null);

                Union(fromPin, toPin);

                if (groupId != null)
                {
                    Union(fromPin, "GROUP_" + groupId);
                    Union(toPin, "GROUP_" + groupId);
                }
            }

            // Group all pins by their root ancestor
            var groupsByRoot = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>>();
            foreach (var pin in pinAddresses.Keys)
            {
                string root = Find(pin);
                if (!groupsByRoot.ContainsKey(root))
                {
                    groupsByRoot[root] = new System.Collections.Generic.List<string>();
                }
                groupsByRoot[root].Add(pin);
            }

            // Order groups by minimum pin address in each group
            var sortedGroups = groupsByRoot.Values
                .OrderBy(grp => grp.Min(p => pinAddresses.TryGetValue(p, out int addr) ? addr : int.MaxValue))
                .ToList();

            int wireSeq = 1;
            foreach (var grp in sortedGroups)
            {
                var pinList = grp.OrderBy(p => pinAddresses.TryGetValue(p, out int addr) ? addr : int.MaxValue)
                                 .ThenBy(p => p)
                                 .ToList();

                for (int pIdx = 0; pIdx < pinList.Count; pIdx += 4)
                {
                    var chunk = pinList.Skip(pIdx).Take(4);
                    string chunkText = string.Join("  ", chunk);

                    if (pIdx == 0)
                    {
                        sb.AppendLine($"{wireSeq,-3} {chunkText}  ");
                    }
                    else
                    {
                        sb.AppendLine($"     {chunkText}  ");
                    }
                }
                wireSeq++;
            }

            if (componentNets.Count > 0)
            {
                sb.AppendLine();
                sb.AppendLine("CHECK COMPONENTS");
                int compSeq = 1;
                foreach (var net in componentNets)
                {
                    string fromPin = !string.IsNullOrWhiteSpace(net.FromLabel) ? net.FromLabel : FormatPin(net.FromAddress);
                    string toPin = !string.IsNullOrWhiteSpace(net.ToLabel) ? net.ToLabel : FormatPin(net.ToAddress);
                    string typeName = net.TestType?.TestTypeName?.ToUpperInvariant() ?? "";
                    
                    string shortType = typeName;
                    if (typeName == "RESISTOR") shortType = "RESIS";
                    else if (typeName == "CAPACITOR") shortType = "CAP";

                    if (shortType == "DIODE")
                    {
                        sb.AppendLine($"{compSeq++} DIODE {fromPin} {toPin}");
                    }
                    else
                    {
                        double val = net.ExpectedValue ?? 0;
                        string valStr = "";
                        if (shortType == "RESIS")
                        {
                            if (val >= 1e6) valStr = (val / 1e6).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + " M ohm";
                            else if (val >= 1e3) valStr = (val / 1e3).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + " K ohm";
                            else valStr = val.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + " ohm";
                        }
                        else if (shortType == "CAP")
                        {
                            if (val >= 1e-3) valStr = (val * 1e3).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + " mF";
                            else if (val >= 1e-6) valStr = (val * 1e6).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + " uF";
                            else if (val >= 1e-9) valStr = (val * 1e9).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + " nF";
                            else if (val >= 1e-12) valStr = (val * 1e12).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + " pF";
                            else valStr = val.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + " F";
                        }
                        else
                        {
                            valStr = val.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + " " + (net.MeasurementUnit ?? "");
                        }

                        string tol = (net.Tolerance ?? 0).ToString("0.##", System.Globalization.CultureInfo.InvariantCulture);
                        sb.AppendLine($"{compSeq++} {shortType} {fromPin} {toPin} {valStr} {tol} %");
                    }
                }
            }

            sb.AppendLine();
            sb.AppendLine(")");

            return sb.ToString();
        }

        private string FormatPin(int address)
        {
            return address <= 64 ? $"J1-{address:D3}" : $"J3-{(address - 64):D3}";
        }

        public TestConfiguration ConvertToTestConfig(string configText)
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

                string firstPin = currentNetPins[0];
                for (int i = 1; i < currentNetPins.Count; i++)
                {
                    string fromPinStr = firstPin;
                    string toPinStr = currentNetPins[i];

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
                        HarnessGroup = currentNetGroupId,
                        NetId = currentNetGroupId,
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
            pin = pin.Trim();
            if (pin.StartsWith("J1-") && pin.Length > 3)
            {
                if (int.TryParse(pin.Substring(3), out int num))
                    return num;
            }
            else if (pin.StartsWith("J3-") && pin.Length > 3)
            {
                if (int.TryParse(pin.Substring(3), out int num))
                    return num + 64;
            }
            return 0;
        }
    }
}