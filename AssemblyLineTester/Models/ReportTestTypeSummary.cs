using System.ComponentModel.DataAnnotations;

namespace AssemblyLineTester.Models
{
    public class ReportTestTypeSummary
    {
        [Key]
        public int SummaryId { get; set; }

        public int ReportId { get; set; }
        public TestReport Report { get; set; } = null!;

        public string TestTypeName { get; set; } = string.Empty;
        public TestType? TestTypeInfo { get; set; }

        public int TotalCount { get; set; }
        public int PassCount { get; set; }
        public int FailCount { get; set; }
    }
}