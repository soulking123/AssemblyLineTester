using System;

namespace AssemblyLineTester.Models.Dto
{
    public class ReportListItem
    {
        public int ReportId { get; set; }
        public string ReportName { get; set; } = string.Empty;
        public string ReportType { get; set; } = "Test Report";
        public DateTime DateGenerated { get; set; }
        public string GeneratedBy { get; set; } = string.Empty;
        public double SizeKb { get; set; }
        public string OverallResult { get; set; } = string.Empty;
    }
}