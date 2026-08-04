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

        // Kept as numbers so no exporter has to parse a formatted limit string back apart.
        public double? LowerLimit { get; set; }
        public double? UpperLimit { get; set; }
        public double? ExpectedValue { get; set; }
        public string Unit { get; set; } = string.Empty;

        // The tolerance recorded with the test step. Null falls back to the band-derived figure below.
        public double? ToleranceValue { get; set; }

        // Human-readable limit range, e.g. "1 - 10 Ohm" or "< 10 Ohm".
        public string Limit => FormatLimit(LowerLimit, UpperLimit, Unit);

        // Symmetric tolerance around the midpoint of the band, "-" without a two-sided band.
        public string Tolerance
        {
            get
            {
                if (ToleranceValue is double recorded) return $"±{recorded:0.##}%";
                if (LowerLimit is not double lower || UpperLimit is not double upper) return "-";

                double mid = (lower + upper) / 2.0;
                if (Math.Abs(mid) < 1e-9) return "-";

                return $"±{Math.Abs((upper - mid) / mid * 100.0):0.##}%";
            }
        }

        private static string FormatLimit(double? lower, double? upper, string unit)
        {
            if (lower.HasValue && upper.HasValue) return $"{lower} - {upper} {unit}";
            if (upper.HasValue) return $"< {upper} {unit}";
            if (lower.HasValue) return $"{lower} {unit} min";
            return "-";
        }

        public string Measured { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
        public string? ErrorDetail { get; set; }

        // Renders one configurable result column by the name Settings stores it under.
        public string Column(string name) => name switch
        {
            "Net-ID" => NetId,
            "Harness Group" => HarnessGroup,
            "From" => FromPin,
            "Net From Label" => FromLabel,
            "To" => ToPin,
            "Net To Label" => ToLabel,
            "Test Type" => TestType,
            "Lower Limit" => LowerLimit?.ToString() ?? "-",
            "Upper Limit" => UpperLimit?.ToString() ?? "-",
            "Expected Value" => ExpectedValue?.ToString() ?? "-",
            "Tolerance" => Tolerance,
            "Measurement" => Measured,
            _ => ""
        };
    }
}