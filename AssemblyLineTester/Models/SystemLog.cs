using System;
using System.ComponentModel.DataAnnotations;

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

        public string? IPAddress { get; set; }

        public enum LogSeverity
        {
            Info,
            Warning,
            Error
        }
    }
}