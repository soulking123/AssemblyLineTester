using System.ComponentModel.DataAnnotations;

namespace AssemblyLineTester.Models
{
    public class TestResultCheck
    {
        [Key]
        public int Id { get; set; }

        public string TestResultName { get; set; } = string.Empty;
        public bool IsChecked { get; set; }
    }
}
