using System.ComponentModel.DataAnnotations;

namespace AssemblyLineTester.Models
{
    // Default test parameters for a product model.
    public class DefaultTestSetting
    {
        [Key]
        public int Id { get; set; }

        [Range(0, int.MaxValue)]
        public int TotalTestPointsAvailable { get; set; }

        public int DefaultTestTypeId { get; set; }
        public TestType? DefaultTestType { get; set; }

        [Range(0, double.MaxValue)]
        public double DefaultMaxWireResistance { get; set; }

        [Range(0, double.MaxValue)]
        public double DefaultWireInstallationResistance { get; set; }

        [Range(0, double.MaxValue)]
        public double DefaultResistorValue { get; set; }

        [Range(1, 100)]
        public int DefaultResistorTolerance { get; set; }

        [Range(0, double.MaxValue)]
        public double DefaultCapacitorValue { get; set; }

        [Range(1, 100)]
        public int DefaultCapacitorTolerance { get; set; }
    }
}
