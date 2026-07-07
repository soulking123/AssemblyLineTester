using System;
using static AssemblyLineTester.Models.SystemLog;

namespace AssemblyLineTester.Models.Dto
{
    public class SystemLogFilter
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public LogSeverity? SeverityLevel { get; set; }
        public string? ModuleName { get; set; }
        public string? UserName { get; set; }
        public string? ActionName { get; set; }
        public string? Keyword { get; set; }

        private int _page = 1;
        public int Page
        {
            get => _page;
            set => _page = value < 1 ? 1 : value;
        }

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value < 1 ? 10 : Math.Min(value, 200);
        }
    }
}