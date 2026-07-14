using System;

namespace AssemblyLineTester.Models.Dto
{
    public class ReportTrendPoint
    {
        public DateTime Date { get; set; }
        public int PassCount { get; set; }
        public int FailCount { get; set; }
        public int TotalCount { get; set; }
    }
}