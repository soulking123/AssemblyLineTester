using System.ComponentModel.DataAnnotations;

namespace AssemblyLineTester.Models
{
    public class LearnConnection
    {
        [Key]
        public int Id { get; set; }

        public int TestTypeId { get; set; }
        public TestType? TestType { get; set; }

        public bool IsChecked { get; set; }
        public string DisplayName { get; set; } = string.Empty;
    }
}
