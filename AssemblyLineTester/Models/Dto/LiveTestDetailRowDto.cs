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

        // Empty for an instrument-driven run, which knows nothing about harness groups.
        public string HarnessGroup { get; set; } = string.Empty;
    }
}