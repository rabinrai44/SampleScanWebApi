namespace SampleScanWebApi.Entities;

public class Item
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int Quantity { get; set; }
    public required string Model { get; set; }
    public string? Description { get; set; }

}