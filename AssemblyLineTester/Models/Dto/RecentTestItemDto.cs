using System;

namespace AssemblyLineTester.Models.Dto
{
    // Maps 1:1 to gridRecent's columns: No, Test Time, Part Number, Serial Number, Result, Operator.
    public class RecentTestItemDto
    {
        public int ReportId { get; set; }
        public DateTime TestTime { get; set; }
        public string PartNumber { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public string? Result { get; set; } // "PASS" / "FAIL" / null if not completed
        public string Operator { get; set; } = string.Empty;
    }
}