namespace SampleScanWebApi.Entities;

public class Zone : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public ICollection<Location> Locations { get; set; } = [];
}