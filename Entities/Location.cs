namespace SampleScanWebApi.Entities;

public class Location : BaseEntity
{
    public int ZoneId { get; set; }
    public Zone? Zone { get; set; }
    public required string Aisle { get; set; }
    public required string Rack { get; set; }
    public required string Shelf { get; set; }
    public required string Tote { get; set; }
    public string? Description { get; set; }
}
