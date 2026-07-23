using System.ComponentModel.DataAnnotations;

// Join entity for the many-to-many relationship between ProductHarness and Connector - a Connector
// is a standalone catalog item that can be reused by several harnesses. Position records the order a
// connector was attached to a given harness (1st attached = "J1", 2nd = "J2", ...), since that order
// can't be recovered from the FK relationship alone.
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
