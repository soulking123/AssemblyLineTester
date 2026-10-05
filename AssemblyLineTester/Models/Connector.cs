using System.ComponentModel.DataAnnotations;

public class Connector
{
    //[Key]
    //public int ConnectorId { get; set; }

    //public string Perf { get; set; } = string.Empty;
    //public string PartNumber { get; set; } = string.Empty;

    //public string ContactType { get; set; } = string.Empty;
    //public int ContactQuantity { get; set; }
    //public string WireSize { get; set; } = string.Empty;
    //public double CurrentRating { get; set; }
    //public string MountingStyle { get; set; } = string.Empty;

    //public string? ImagePath { get; set; }

    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PartNumber { get; set; } = string.Empty;
    public int TotalPin { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> ImagePath { get; set; } = new();
    public List<bool> IncludedForReport { get; set; } = new();
}