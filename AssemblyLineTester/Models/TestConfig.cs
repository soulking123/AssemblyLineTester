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
        // Named apart from the ProductModel entity it holds the name of; the column keeps its old
        // name so the rename costs no migration.
        [Column("ProductModel")]
        public string ProductModelName { get; set; } = string.Empty;
        public string? Description { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public bool IsActive { get; set; } = false;
        public List<TestConfigNet> Nets { get; set; } = new();

        // The test types this configuration runs, as a many-to-many: a type belongs to many
        // configurations and a configuration runs many types.
        public List<TestType> TestTypes { get; set; } = new();

        public double MaxWireResistance { get; set; }
        public double MinComponentResistance { get; set; }
        public double MinInsulationResistance { get; set; }

        // Whether the hipot step runs at all; the voltage it uses is an instrument setting.
        public bool HipotVoltage { get; set; }
    }
}