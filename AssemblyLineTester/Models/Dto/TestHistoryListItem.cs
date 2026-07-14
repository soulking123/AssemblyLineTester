using System;

namespace AssemblyLineTester.Models.Dto
{
    public class TestHistoryListItem
    {
        public int ReportId { get; set; }
        public DateTime TestTime { get; set; }
        public string ProductModel { get; set; } = string.Empty;
        public string ProductType { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
        public string PartNumber { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string? Result { get; set; }
        public string TotalTime { get; set; } = "-"; // formatted HH:mm:ss, "-" if still running/stopped early
        public string Operator { get; set; } = string.Empty;
    }
}