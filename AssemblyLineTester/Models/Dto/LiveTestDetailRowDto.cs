namespace AssemblyLineTester.Models.Dto
{
    public enum LiveTestStatus
    {
        Pending,
        Testing,
        Pass,
        Fail
    }

    // One row of the Live Test Detail grid; MeasuredValue and Status fill in during a run.
    public class LiveTestDetailRowDto
    {
        public int StepNo { get; set; }
        public string NetId { get; set; } = string.Empty;
        public string TestPoint { get; set; } = string.Empty;
        public string FromPoint { get; set; } = string.Empty;
        public string ToPoint { get; set; } = string.Empty;
        public string TestType { get; set; } = string.Empty;
        public string ExpectedValue { get; set; } = string.Empty;
        public double? MeasuredValue { get; set; }
        public LiveTestStatus Status { get; set; } = LiveTestStatus.Pending;

        // The threshold this row's verdict was decided against, so the report can state it.
        public double? UpperLimit { get; set; }

        // Carried over from the loaded TestConfigNet; blank on a run with no matching config row.
        public string FromLabel { get; set; } = string.Empty;
        public string ToLabel { get; set; } = string.Empty;
        public double? LowerLimit { get; set; }
        public double? Tolerance { get; set; }
        public string Unit { get; set; } = string.Empty;

        // Empty for an instrument-driven run, which knows nothing about harness groups.
        public string HarnessGroup { get; set; } = string.Empty;
    }
}