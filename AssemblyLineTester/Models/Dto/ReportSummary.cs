namespace AssemblyLineTester.Models.Dto
{
    public class ReportSummary
    {
        public int TotalTested { get; set; }
        public int PassCount { get; set; }
        public int FailCount { get; set; }
        public int InProgressCount { get; set; }
        public double PassRatePercent { get; set; }
    }
}