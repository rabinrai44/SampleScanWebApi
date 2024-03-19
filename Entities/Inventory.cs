namespace SampleScanWebApi.Entities;

public class Inventory : BaseEntity
{
    public int ItemId { get; set; }
    public Item? Item { get; set; }
    public int LocationId { get; set; }
    public Location? Location { get; set; }
    public int Quantity { get; set; }
    public string? ToteId { get; set; }
    public DateTime LastStockedDate { get; set; }
    public DateTime LastPickedDate { get; set; }
}
