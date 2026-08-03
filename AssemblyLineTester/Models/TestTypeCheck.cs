using System.ComponentModel.DataAnnotations;

namespace AssemblyLineTester.Models
{
    public class TestTypeCheck
    {
        [Key]
        public int Id { get; set; }

        public string TestTypeName { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public bool IsChecked { get; set; }
    }
}
