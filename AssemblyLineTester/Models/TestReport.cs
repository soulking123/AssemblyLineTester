using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssemblyLineTester.Models
{
    public enum TestStatus
    {
        Completed,
        InProgress,
        Stopped
    }

    public enum TestResult
    {
        Pass,
        Fail
    }

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

        public TestStatus Status { get; set; } = TestStatus.Completed;
        public TestResult? OverallResult { get; set; }

        public bool IsDraft { get; set; } = true;

        public string? FilePath { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<TestDetail> Details { get; set; } = new();
        public List<TestStep> Steps { get; set; } = new();
    }
}