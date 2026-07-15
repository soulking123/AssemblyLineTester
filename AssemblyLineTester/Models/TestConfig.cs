using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssemblyLineTester.Models
{
    public class TestConfiguration
    {
        [Key]
        public int ConfigId { get; set; }
        public string ConfigCode { get; set; } = string.Empty; // 

        public string ConfigName { get; set; } = string.Empty; // CAT-PROGRAM-001
        public string ProductModel { get; set; } = string.Empty; // 6060
        public string? Description { get; set; }

        public string CreatedBy { get; set; } = string.Empty;  // Admin, Engineer, Techinician
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public bool IsActive { get; set; } = false;
        public List<TestConfigNet> Nets { get; set; } = new();
    }
}