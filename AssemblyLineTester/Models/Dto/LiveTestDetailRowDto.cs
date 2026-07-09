namespace AssemblyLineTester.Models.Dto
{
    public enum LiveTestStatus
    {
        Pending,
        Testing,
        Pass,
        Fail
    }

    // Row for the Live Test Detail grid. MeasuredValue/Status stay empty until
    // the execution team's logic fills them in during a real test run.
    public class LiveTestDetailRowDto
    {
        public int StepNo { get; set; }
        public string NetId { get; set; } = string.Empty;
        public string TestPoint { get; set; } = string.Empty;
        public string TestType { get; set; } = string.Empty;
        public string ExpectedValue { get; set; } = string.Empty;
        public double? MeasuredValue { get; set; }
        public LiveTestStatus Status { get; set; } = LiveTestStatus.Pending;
    }
}