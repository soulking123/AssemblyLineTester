using System;

namespace AssemblyLineTester.Models.Dto
{
    public class ReportListItem
    {
        public int ReportId { get; set; }
        public string ReportName { get; set; } = string.Empty;
        public DateTime DateGenerated { get; set; }
        public string GeneratedBy { get; set; } = string.Empty;
        public string OverallResult { get; set; } = string.Empty;
    }
}