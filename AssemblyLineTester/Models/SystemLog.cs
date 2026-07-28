using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssemblyLineTester.Models
{
    
    public class SystemLog
    {
        [Key]
        public int Id { get; set; }
        
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public LogSeverity SeverityLevel { get; set; } = LogSeverity.Info;

        public string ModuleName { get; set; } = string.Empty;

        public string? UserName { get; set; }

        public string ActionName { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// PC that produced the entry. Tells apart bundles coming from two machines at the same site.
        /// </summary>
        /// <remarks>
        /// Mapped onto the existing <c>IPAddress</c> column. This is a desktop application on a
        /// factory PC, so the IP it was originally meant to hold was always null and the column was
        /// dead weight; reusing it renames the field in code without spending a migration.
        /// ponytail: rename the column for real whenever the next schema migration is written.
        /// </remarks>
        [Column("IPAddress")]
        public string? MachineName { get; set; }

        public enum LogSeverity
        {
            Info,
            Warning,
            Error
        }
    }
}