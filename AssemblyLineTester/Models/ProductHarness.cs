using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ProductHarness
{
    [Key]
    public int ProductHarnessId { get; set; }

    public string Name { get; set; } = string.Empty;
    public string ProductModel { get; set; } = string.Empty;
    public string ProductType { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public DateTime LastUpdate { get; set; } = DateTime.UtcNow;

    public List<string> ImagePaths { get; set; } = new();

    // "J1", "J2", ... - one label per entry in ConnectorList, in the order each connector was
    // attached to this harness (not the connector's part number).
    public List<String> ConnectorListName { get; set; } = new();

    // The join rows backing the many-to-many relationship with Connector (a connector can belong to
    // several harnesses). Query/order this via HarnessConnectorId->Position; consumers should use
    // ConnectorList below instead of touching this directly.
    public List<HarnessConnector> HarnessConnectors { get; set; } = new();

    // Not EF-mapped: populated by HarnessService (Position-ordered) when a harness is loaded, and
    // used as a plain DTO when building a ProductHarness to hand to HarnessService.AddHarness/UpdateHarness.
    [NotMapped]
    public List<Connector> ConnectorList { get; set; } = new();
}