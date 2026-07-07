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

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}