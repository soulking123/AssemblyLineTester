namespace AssemblyLineTester.Models.Dto
{
    public class FailReasonSliceDto
    {
        public string TestTypeName { get; set; } = string.Empty;
        public int FailCount { get; set; }
        public double PercentOfTotalFail { get; set; }
    }
}