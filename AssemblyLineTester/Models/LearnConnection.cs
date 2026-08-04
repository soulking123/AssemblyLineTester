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

        // The bit the instrument uses for this connection type. Wire is 0: it is the plain
        // continuity case the others are measured on top of, so it carries no bit of its own.
        public int TestTypeValue { get; set; }
    }
}
