using System;
using System.Globalization;

namespace AssemblyLineTester.Models.Dto
{
    public class TestDetailDto
    {
        public string NetId { get; set; } = string.Empty;
        public string HarnessGroup { get; set; } = string.Empty;
        public string FromPin { get; set; } = string.Empty;
        public string ToPin { get; set; } = string.Empty;
        public string FromLabel { get; set; } = string.Empty;
        public string ToLabel { get; set; } = string.Empty;
        public string TestType { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Gauge { get; set; } = string.Empty;

        // Kept as numbers so no exporter has to parse a formatted limit string back apart.
        public double? LowerLimit { get; set; }
        public double? UpperLimit { get; set; }
        public double? ExpectedValue { get; set; }
        public double? LowerLimit2 { get; set; }
        public double? UpperLimit2 { get; set; }
        public double? ExpectedValue2 { get; set; }
        public string Unit { get; set; } = string.Empty;

        // The tolerance recorded with the test step. Null falls back to the band-derived figure below.
        public double? ToleranceValue { get; set; }
        public double? ToleranceValue2 { get; set; }

        public double? MeasuredValue { get; set; }
        public string? MeasurementPrefix { get; set; }
        public double? MeasuredValue2 { get; set; }
        public string? MeasurementPrefix2 { get; set; }

        public string Measured { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
        public string? ErrorDetail { get; set; }

        // Human-readable limit range, e.g. "1 - 10 Ohm" or "< 10 Ohm".
        public string Limit => FormatLimit(LowerLimit, UpperLimit, Unit);

        // Symmetric tolerance around the midpoint of the band, "-" without a two-sided band.
        // Wire is plain continuity, not a component with a rated value - tolerance means nothing here.
        public string Tolerance
        {
            get
            {
                if (string.Equals(TestType, "WIRE", StringComparison.OrdinalIgnoreCase)) return "-";

                bool isDiode = string.Equals(TestType, "DIODE", StringComparison.OrdinalIgnoreCase) ||
                               string.Equals(TestType, "SIGNATURE DIODE", StringComparison.OrdinalIgnoreCase);

                if (isDiode && (ToleranceValue2.HasValue || ToleranceValue.HasValue))
                {
                    string t1 = ToleranceValue?.ToString("0.##", CultureInfo.InvariantCulture) ?? "20";
                    string t2 = ToleranceValue2?.ToString("0.##", CultureInfo.InvariantCulture) ?? "20";
                    return $"±{t1}%, ±{t2}%";
                }

                if (ToleranceValue is double recorded) return $"±{recorded.ToString("0.##", CultureInfo.InvariantCulture)}%";
                if (LowerLimit is not double lower || UpperLimit is not double upper) return "-";

                double mid = (lower + upper) / 2.0;
                if (Math.Abs(mid) < 1e-9) return "-";

                return $"±{Math.Abs((upper - mid) / mid * 100.0).ToString("0.##", CultureInfo.InvariantCulture)}%";
            }
        }

        private static string FormatLimit(double? lower, double? upper, string unit)
        {
            if (lower.HasValue && upper.HasValue) return $"{lower.Value.ToString(CultureInfo.InvariantCulture)} - {upper.Value.ToString(CultureInfo.InvariantCulture)} {unit}";
            if (upper.HasValue) return $"< {upper.Value.ToString(CultureInfo.InvariantCulture)} {unit}";
            if (lower.HasValue) return $"{lower.Value.ToString(CultureInfo.InvariantCulture)} {unit} min";
            return "-";
        }

        public static string FormatLimitValue(double? val, string testType)
        {
            if (!val.HasValue) return "";
            double v = val.Value;
            if (string.Equals(testType, "CAPACITOR", StringComparison.OrdinalIgnoreCase) || (Math.Abs(v) > 0 && Math.Abs(v) < 0.01))
            {
                return v.ToString("0.##########", CultureInfo.InvariantCulture);
            }
            if (string.Equals(testType, "DIODE", StringComparison.OrdinalIgnoreCase) || string.Equals(testType, "SIGNATURE DIODE", StringComparison.OrdinalIgnoreCase))
            {
                string s = v.ToString("0.####", CultureInfo.InvariantCulture);
                if (!s.Contains('.')) return s + ".00";
                var parts = s.Split('.');
                if (parts[1].Length < 2) return parts[0] + "." + parts[1].PadRight(2, '0');
                return s;
            }
            return v.ToString("0.##", CultureInfo.InvariantCulture);
        }

        public static string FormatMeasuredValue(double? val, string? prefix = null)
        {
            if (!val.HasValue) return "-";
            double v = val.Value;
            string numStr;
            if (Math.Abs(v) < 1e-12) numStr = "0.00";
            else if (Math.Abs(v) >= 0.01) numStr = v.ToString("F2", CultureInfo.InvariantCulture);
            else numStr = v.ToString("0.##########", CultureInfo.InvariantCulture);

            return (prefix ?? "") + numStr;
        }

        public static string FormatMeasuredRow(double? val1, string? prefix1, double? val2, string? prefix2, string testType)
        {
            bool isDiode = string.Equals(testType, "DIODE", StringComparison.OrdinalIgnoreCase) ||
                           string.Equals(testType, "SIGNATURE DIODE", StringComparison.OrdinalIgnoreCase);

            if (isDiode && (val2.HasValue || prefix2 != null))
            {
                string fwdStr = FormatMeasuredValue(val1, prefix1);
                string revStr = FormatMeasuredValue(val2, prefix2);
                return $"{fwdStr}, {revStr}";
            }

            return FormatMeasuredValue(val1, prefix1);
        }

        // Renders one configurable result column by the name Settings stores it under.
        public string Column(string name)
        {
            bool isDiode = string.Equals(TestType, "DIODE", StringComparison.OrdinalIgnoreCase) ||
                           string.Equals(TestType, "SIGNATURE DIODE", StringComparison.OrdinalIgnoreCase);

            return name switch
            {
                "Net-ID" => NetId,
                "Harness Group" => HarnessGroup,
                "From" => FromPin,
                "Net From Label" => FromLabel,
                "To" => ToPin,
                "Net To Label" => ToLabel,
                "Test Type" => TestType,
                "Color" => Color,
                "Gauge" => Gauge,
                "Lower Limit" => (isDiode && LowerLimit2.HasValue)
                    ? $"{FormatLimitValue(LowerLimit, TestType)}, > {FormatLimitValue(LowerLimit2, TestType)}"
                    : (LowerLimit.HasValue ? FormatLimitValue(LowerLimit, TestType) : "-"),
                "Upper Limit" => (isDiode && UpperLimit2.HasValue)
                    ? $"{FormatLimitValue(UpperLimit, TestType)}, > {FormatLimitValue(UpperLimit2, TestType)}"
                    : (UpperLimit.HasValue ? FormatLimitValue(UpperLimit, TestType) : "-"),
                "Expected Value" => (isDiode && ExpectedValue2.HasValue)
                    ? $"{FormatLimitValue(ExpectedValue, TestType)}, > {FormatLimitValue(ExpectedValue2, TestType)}"
                    : (ExpectedValue.HasValue ? FormatLimitValue(ExpectedValue, TestType) : "-"),
                "Tolerance" => Tolerance,
                "Measurement" => !string.IsNullOrEmpty(Measured) ? Measured : FormatMeasuredRow(MeasuredValue, MeasurementPrefix, MeasuredValue2, MeasurementPrefix2, TestType),
                "Measured Value" => !string.IsNullOrEmpty(Measured) ? Measured : FormatMeasuredRow(MeasuredValue, MeasurementPrefix, MeasuredValue2, MeasurementPrefix2, TestType),
                "Measured" => !string.IsNullOrEmpty(Measured) ? Measured : FormatMeasuredRow(MeasuredValue, MeasurementPrefix, MeasuredValue2, MeasurementPrefix2, TestType),
                _ => ""
            };
        }
    }
}