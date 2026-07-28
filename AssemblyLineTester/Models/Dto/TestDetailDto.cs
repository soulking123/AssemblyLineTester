namespace AssemblyLineTester.Models.Dto
{
    public class TestDetailDto
    {
        public string NetId { get; set; } = string.Empty;
        public string HarnessGroup { get; set; } = string.Empty;
        public string FromPin { get; set; } = string.Empty;
        public string ToPin { get; set; } = string.Empty;
        public string TestType { get; set; } = string.Empty;

        // Kept as numbers so no exporter has to parse a formatted limit string back apart.
        public double? LowerLimit { get; set; }
        public double? UpperLimit { get; set; }
        public string Unit { get; set; } = string.Empty;

        // Human-readable limit range, e.g. "1 - 10 Ohm" or "< 10 Ohm".
        public string Limit => FormatLimit(LowerLimit, UpperLimit, Unit);

        // Symmetric tolerance around the midpoint of the band, "-" without a two-sided band.
        public string Tolerance
        {
            get
            {
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
    }
}