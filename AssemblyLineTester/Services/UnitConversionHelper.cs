using System.Collections.Generic;
using System.Linq;

namespace AssemblyLineTester.Services
{
    // ASSUMPTION: vendor unit mapping, unconfirmed. Non-measurement types carry no unit.
    public static class UnitConversionHelper
    {
        private static readonly Dictionary<string, (string BaseUnit, Dictionary<string, double> Multipliers)> Map = new()
        {
            ["RESISTOR"] = ("Ω", new() { ["Ω"] = 1, ["mΩ"] = 0.001, ["kΩ"] = 1_000, ["MΩ"] = 1_000_000, ["GΩ"] = 1_000_000_000 }),
            ["4RESISTOR"] = ("Ω", new() { ["Ω"] = 1, ["mΩ"] = 0.001, ["kΩ"] = 1_000, ["MΩ"] = 1_000_000, ["GΩ"] = 1_000_000_000 }),
            ["WIRE"] = ("Ω", new() { ["Ω"] = 1, ["mΩ"] = 0.001, ["kΩ"] = 1_000 }),
            ["4WIRE"] = ("Ω", new() { ["Ω"] = 1, ["mΩ"] = 0.001, ["kΩ"] = 1_000 }),
            ["TWISTED PAIR"] = ("Ω", new() { ["Ω"] = 1, ["mΩ"] = 0.001, ["kΩ"] = 1_000 }),
            ["SIGNATURE WIRE"] = ("Ω", new() { ["Ω"] = 1, ["mΩ"] = 0.001, ["kΩ"] = 1_000 }),
            ["CAPACITOR"] = ("F", new() { ["F"] = 1, ["mF"] = 0.001, ["μF"] = 0.000001, ["nF"] = 0.000000001, ["pF"] = 0.000000000001 }),
            ["DIODE"] = ("V", new() { ["V"] = 1, ["mV"] = 0.001, ["kV"] = 1_000 }),
            ["SIGNATURE DIODE"] = ("V", new() { ["V"] = 1, ["mV"] = 0.001, ["kV"] = 1_000 }),
        };

        private static readonly (string BaseUnit, Dictionary<string, double> Multipliers) NoMeasurement =
            ("-", new() { ["-"] = 1 });

        public static string[] GetUnitsForTestType(string testTypeName)
        {
            return Map.TryGetValue(testTypeName, out var info)
                ? info.Multipliers.Keys.ToArray()
                : NoMeasurement.Multipliers.Keys.ToArray();
        }

        public static string GetBaseUnit(string testTypeName)
        {
            return Map.TryGetValue(testTypeName, out var info) ? info.BaseUnit : NoMeasurement.BaseUnit;
        }

        // Throws on a unit that does not belong to the test type, rather than silently not converting.
        public static double ConvertToBaseUnit(string testTypeName, double value, string fromUnit)
        {
            var info = Map.TryGetValue(testTypeName, out var found) ? found : NoMeasurement;

            if (!info.Multipliers.TryGetValue(fromUnit, out double multiplier))
            {
                throw new ArgumentException(
                    $"'{fromUnit}' is not a valid unit for test type '{testTypeName}'. " +
                    $"Expected one of: {string.Join(", ", info.Multipliers.Keys)}.",
                    nameof(fromUnit));
            }

            return value * multiplier;
        }
    }
}