namespace SampleScanWebApi.Entities;

public class Item : BaseEntity
{
    public required string ItemNumber { get; set; }
    public required string Name { get; set; }
    public string? Sku { get; set; }
    public string? Description { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
}