using System;
using System.Collections.Generic;

namespace AssemblyLineTester.Models.Dto
{
    public class ReportDetailDto
    {
        public string ReportNumber { get; set; } = string.Empty;
        public DateTime TestDateTime { get; set; }
        public string ProductModel { get; set; } = string.Empty;
        public string ProductType { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
        public string ECNNumber { get; set; } = string.Empty;
        public string Revision { get; set; } = string.Empty;
        public string PartNumber { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public string Operator { get; set; } = string.Empty;
        public string TestTime { get; set; } = string.Empty; // formatted duration HH:mm:ss
        public string OverallResult { get; set; } = string.Empty;

        public List<TestTypeSummaryDto> TypeSummaries { get; set; } = new();
        public List<TestDetailDto> Details { get; set; } = new();
    }
}