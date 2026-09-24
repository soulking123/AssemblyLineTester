using System.ComponentModel.DataAnnotations;

// Join entity; Position records attach order ("J1", "J2", ...), which the FK alone cannot recover.
public class HarnessAssembly
{
    //[Key]
    //public int HarnessConnectorId { get; set; }

    //public int ProductHarnessId { get; set; }
    //public ProductHarness? ProductHarness { get; set; }

    //public int ConnectorId { get; set; }
    //public Connector? Connector { get; set; }

    //public int Position { get; set; }
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PartNumber { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> ImagePath { get; set; } = new();
    public List<Connector> Connectors { get; set; } = new();
}
