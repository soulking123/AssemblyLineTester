using System.ComponentModel.DataAnnotations;

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
    public List<String> ConnectorListName { get; set; } = new();
    public List<Connector> ConnectorList { get; set; } = new();
}