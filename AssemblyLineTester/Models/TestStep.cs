using System.ComponentModel.DataAnnotations;

namespace AssemblyLineTester.Models
{
    public class TestStep
    {
        [Key]
        public int StepId { get; set; }

        public int ReportId { get; set; }
        public TestReport Report { get; set; } = null!;

        public int StepNo { get; set; }
        public string TestTypeName { get; set; } = string.Empty;
        public TestType? TestTypeInfo { get; set; }

        public bool Result { get; set; }
        public int DurationSeconds { get; set; }
    }
}