using System;
using System.Collections.Generic;

namespace AssemblyLineTester.Models.Dto
{
    public class TestHistoryDetailDto
    {
        public string ReportNumber { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string TotalTime { get; set; } = "-";

        public string ProductModel { get; set; } = string.Empty;
        public string ProductType { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
        public string PartNumber { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public string ECNNumber { get; set; } = string.Empty;
        public string Operator { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
        public string? OverallResult { get; set; }

        public List<TestStepResultDto> Steps { get; set; } = new();
    }
}