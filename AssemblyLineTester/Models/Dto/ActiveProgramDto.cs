using System;

namespace AssemblyLineTester.Models.Dto
{
    public class ActiveProgramDto
    {
        public string PartNumber { get; set; } = string.Empty;
        public string ProductModel { get; set; } = string.Empty;
        public string StatusLabel { get; set; } = string.Empty; // "Running", "Idle", "Completed"
        public DateTime LastRunAt { get; set; }
    }
}