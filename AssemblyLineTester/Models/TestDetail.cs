using System.ComponentModel.DataAnnotations;

namespace AssemblyLineTester.Models
{
    public class TestDetail
    {
        [Key]
        public int DetailId { get; set; }

        public int ReportId { get; set; }
        public TestReport Report { get; set; } = null!;

        public string NetId { get; set; } = string.Empty;
        public string ConnectorGroup { get; set; } = string.Empty;
        public string HarnessGroup { get; set; } = string.Empty;
        public string FromPin { get; set; } = string.Empty;
        public string ToPin { get; set; } = string.Empty;
        public string FromLabel { get; set; } = string.Empty;
        public string ToLabel { get; set; } = string.Empty;
        public string FromConnector { get; set; } = string.Empty;
        public string ToConnector { get; set; } = string.Empty;

        public string Color { get; set; } = string.Empty;
        public string Gauge { get; set; } = string.Empty;

        public string TestTypeName { get; set; } = string.Empty;
        public TestType? TestTypeInfo { get; set; }

        public double? MeasuredValue { get; set; }
        public string? MeasurementPrefix { get; set; }
        public double? LowerLimit { get; set; }
        public double? UpperLimit { get; set; }
        public double? ExpectedValue { get; set; }
        public double? Tolerance { get; set; }

        public double? MeasuredValue2 { get; set; }
        public string? MeasurementPrefix2 { get; set; }
        public double? LowerLimit2 { get; set; }
        public double? UpperLimit2 { get; set; }
        public double? ExpectedValue2 { get; set; }
        public double? Tolerance2 { get; set; }

        public string Unit { get; set; } = string.Empty;

        public bool Result { get; set; }
        public string? ErrorDetail { get; set; }
    }
}