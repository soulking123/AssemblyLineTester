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

    // "J1", "J2", ... one per connector in attach order. Derived, never stored.
    [NotMapped]
    public List<string> ConnectorListName => Enumerable.Range(1, ConnectorList.Count).Select(i => $"J{i}").ToList();

    // The join rows; consumers should read ConnectorList below instead of these.
    public List<HarnessConnector> HarnessConnectors { get; set; } = new();

    // Not EF-mapped; HarnessService fills it in Position order when a harness is loaded.
    [NotMapped]
    public List<Connector> ConnectorList { get; set; } = new();
}