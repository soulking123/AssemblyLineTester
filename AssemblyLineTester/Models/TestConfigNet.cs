using System.ComponentModel.DataAnnotations;

namespace AssemblyLineTester.Models
{
    // Detail: one row per net/pin pair to be tested within a TestConfiguration.
    public class TestConfigNet
    {
        [Key]
        public int NetConfigId { get; set; }

        public int ConfigId { get; set; }                                   
        public TestConfiguration? Configuration { get; set; }
        public string NetId { get; set; } = string.Empty;                   // Elec-Control-001

        public int SequenceNo { get; set; }                                 // 1,2,3,4,

        public string HarnessGroup { get; set; } = string.Empty;            // Low Voltage Supply

        public int FromAddress { get; set; }                                // 0,1,2,3,4  
        public string FromLabel { get; set; } = string.Empty;               // X1=01

        public int ToAddress { get; set; }                                  // 0,1,2,3,4
        public string ToLabel { get; set; } = string.Empty;                 // Y1=01

        public int TestTypeId { get; set; }                                 // 
        public TestType? TestType { get; set; }

        public double? LowerLimit { get; set; }                             //0
        
        public double? UpperLimit { get; set; }                             //10

        public string MeasurementUnit { get; set; } = string.Empty;         //ohm
        
        public double? ExpectedValue { get; set; }                          //100
        
        public double? Tolerance { get; set; }                              //10
    }
}