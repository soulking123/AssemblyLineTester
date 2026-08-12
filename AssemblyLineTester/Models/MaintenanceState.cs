using System;
using System.ComponentModel.DataAnnotations;

namespace AssemblyLineTester.Models
{
    public class MaintenanceState
    {
        [Key]
        public int Id { get; set; }
        public DateTime LastUsageDate { get; set; }
        public double OperatingSecondsToday { get; set; }
        public int RemainingMaintDays { get; set; } = 180;
        public double RemainingMaintHours { get; set; } = 2160.0;

        public int IntervalDays { get; set; } = 180;
        public double IntervalHours { get; set; } = 2160.0;
    }
}
