using System.ComponentModel.DataAnnotations;

// Join entity; Position records attach order ("J1", "J2", ...), which the FK alone cannot recover.
public class HarnessConnector
{
    [Key]
    public int HarnessConnectorId { get; set; }

    public int ProductHarnessId { get; set; }
    public ProductHarness? ProductHarness { get; set; }

    public int ConnectorId { get; set; }
    public Connector? Connector { get; set; }

    public int Position { get; set; }
}
