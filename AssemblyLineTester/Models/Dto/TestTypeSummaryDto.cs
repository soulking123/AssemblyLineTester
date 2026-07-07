namespace AssemblyLineTester.Models.Dto
{
    public class TestTypeSummaryDto
    {
        public string TestType { get; set; } = string.Empty;
        public int Total { get; set; }
        public int Pass { get; set; }
        public int Fail { get; set; }
        public double PassRatePercent { get; set; }
    }
}