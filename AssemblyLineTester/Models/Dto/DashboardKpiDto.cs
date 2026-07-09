namespace AssemblyLineTester.Models.Dto
{
    public class DashboardKpiDto
    {
        public int TotalTested { get; set; }
        public int PassCount { get; set; }
        public int FailCount { get; set; }
        public double YieldPercent { get; set; }
        public double AvgCycleTimeSeconds { get; set; }
        public string AvgCycleTimeFormatted { get; set; } = "-";
    }
}