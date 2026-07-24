using System.ComponentModel.DataAnnotations;

public class Connector
{
    [Key]
    public int ConnectorId { get; set; }

    public string Perf { get; set; } = string.Empty;
    public string PartNumber { get; set; } = string.Empty;

    public string ContactType { get; set; } = string.Empty;
    public int ContactQuantity { get; set; }
    public string WireSize { get; set; } = string.Empty;
    public double CurrentRating { get; set; }
    public string MountingStyle { get; set; } = string.Empty;

    public string? ImagePath { get; set; }
}