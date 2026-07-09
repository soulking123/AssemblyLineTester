using System.ComponentModel.DataAnnotations;

namespace AssemblyLineTester.Models
{
    // Detail: one row per net/pin pair to be tested within a TestConfiguration.
    public class TestConfigNet
    {
        [Key]
        public int NetConfigId { get; set; }

        public int ConfigId { get; set; }
        public TestConfiguration Configuration { get; set; } = null!;

        public int SequenceNo { get; set; }

        public string HarnessGroup { get; set; } = string.Empty;

        public int FromAddress { get; set; }
        public string FromLabel { get; set; } = string.Empty;

        public int ToAddress { get; set; }
        public string ToLabel { get; set; } = string.Empty;

        public int TestTypeId { get; set; }
        public TestType TestType { get; set; } = null!;

        public double? LowerLimit { get; set; }
        public double? UpperLimit { get; set; }
        public string MeasurementUnit { get; set; } = string.Empty; 
    }
}