namespace SampleScanWebApi.Entities;

public class TransactionLog : BaseEntity
{
    public required string UserId { get; set; }
    public required TransactionAction Action { get; set; }
    public int ItemId { get; set; }
    public int LocationId { get; set; }
    public int Quantity { get; set; }
    public DateTime Timestamp { get; set; }
}
