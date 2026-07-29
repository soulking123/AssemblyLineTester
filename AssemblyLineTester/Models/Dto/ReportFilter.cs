using System;

namespace AssemblyLineTester.Models.Dto
{
    public class ReportFilter
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string? ProductModel { get; set; }
        public string? ProductType { get; set; }
        public string? Area { get; set; }
        public string? Result { get; set; }   // PASS / FAIL / All
        public string? Operator { get; set; }
        public string? PartNumberKeyword { get; set; }

        // Clamped the same way SystemLogFilter is. An unclamped Page of 0 turned the paging maths
        // into a negative Skip, which is not a query anyone meant to run.
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