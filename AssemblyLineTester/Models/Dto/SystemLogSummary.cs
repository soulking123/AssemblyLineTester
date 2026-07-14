using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblyLineTester.Models.Dto
{
    public class SystemLogSummary
    {
        public int Total { get; set; }
        public int InfoCount { get; set; }
        public int WarningCount { get; set; }
        public int ErrorCount { get; set; }
        public Dictionary<string, int> CountByModule { get; set; } = new();
    }
}
