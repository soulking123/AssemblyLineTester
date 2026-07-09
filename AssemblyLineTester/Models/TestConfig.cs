using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssemblyLineTester.Models
{
    public class TestConfiguration
    {
        [Key]
        public int ConfigId { get; set; }
        public string ConfigCode { get; set; } = string.Empty;

        public string ConfigName { get; set; } = string.Empty;
        public string ProductModel { get; set; } = string.Empty;
        public string? Description { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public bool IsActive { get; set; } = true;
        public List<TestConfigNet> Nets { get; set; } = new();
    }
}