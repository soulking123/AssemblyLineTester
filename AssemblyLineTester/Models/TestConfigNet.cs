using System.ComponentModel.DataAnnotations;

namespace AssemblyLineTester.Models
{
    // One row per net or pin pair inside a TestConfiguration.
    public class TestConfigNet
    {
        [Key]
        public int NetConfigId { get; set; }

        public int ConfigId { get; set; }                                   
        public TestConfiguration? Configuration { get; set; }
        public string NetId { get; set; } = string.Empty;

        public int SequenceNo { get; set; }                                 // 1,2,3,4,

        public string HarnessGroup { get; set; } = string.Empty;

        public int FromAddress { get; set; }                                // 0,1,2,3,4  
        public string FromLabel { get; set; } = string.Empty;

        public int ToAddress { get; set; }                                  // 0,1,2,3,4
        public string ToLabel { get; set; } = string.Empty;
        public int TestTypeId { get; set; }                                 // 

        public TestType? TestType { get; set; }

        public double? LowerLimit { get; set; }                             //0
        
        public double? UpperLimit { get; set; }                             //10

        public string MeasurementUnit { get; set; } = string.Empty;
        
        public double? ExpectedValue { get; set; }                          //100

        public double? Tolerance { get; set; }                              //10

        public string Color { get; set; } = string.Empty;                   // BLK, RED, BLU

        public string Gauge { get; set; } = string.Empty;                   // 18 AWG, 2.5 mm2
    }
}