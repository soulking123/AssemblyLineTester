using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssemblyLineTester.Models
{
    public class TestReport
    {
        [Key]
        public int ReportId { get; set; }

        public string ReportNumber { get; set; } = string.Empty;

        public string ProductModel { get; set; } = string.Empty;
        public string ProductType { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
        public string ECNNumber { get; set; } = string.Empty;
        public string Revision { get; set; } = string.Empty;

        public string PartNumber { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public string Operator { get; set; } = string.Empty;
        public string TesterModel { get; set; } = string.Empty;

        public DateTime TestDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }

        public int TotalNets { get; set; }
        public int PassCount { get; set; }
        public int FailCount { get; set; }

        public string OverallResult { get; set; } = string.Empty; // PASS / FAIL / IN_PROGRESS
        public string? FilePath { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<TestDetail> Details { get; set; } = new();
    }
}