using System.Collections.Generic;

namespace AssemblyLineTester.Models.Dto
{
    public class DashboardTrendResult
    {
        public TrendGranularity Granularity { get; set; }
        public List<ReportTrendPoint> Points { get; set; } = new();
    }
}