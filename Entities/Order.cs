namespace SampleScanWebApi.Entities;

public class Order : BaseEntity
{
    public required string PoNumber { get; set; }
    public string? ShipmentId { get; set; }
    public DateOnly PoDate { get; set; }
    public int QuantityOrdered { get; set; }
    public int QuantityReceived { get; set; }
    public required string ItemNumber { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = [];
}
