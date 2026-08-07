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

        // What Reset puts back. Set from the Settings card, so the service interval is whatever
        // this site actually runs to rather than a figure fixed at build time.
        public int IntervalDays { get; set; } = 180;
        public double IntervalHours { get; set; } = 2160.0;
    }
}
