using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssemblyLineTester.Models
{
    public class TestConfiguration
    {
        [Key]
        public int ConfigId { get; set; }

        public string ConfigName { get; set; } = string.Empty;

        [Column("ProductModel")]
        public string ProductModelName { get; set; } = string.Empty;
        public string? Description { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public bool IsActive { get; set; } = false;
        public List<TestConfigNet> Nets { get; set; } = new();

        public List<TestType> TestTypes { get; set; } = new();

        public double MaxWireResistance { get; set; }
        public double MinComponentResistance { get; set; }
        public double MinInsulationResistance { get; set; }

        public bool HipotVoltage { get; set; }

        public bool ShortTest { get; set; }
    }
}