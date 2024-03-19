namespace SampleScanWebApi.Entities;

public class Tote : BaseEntity
{
    public required string ToteId { get; set; }
    public int Size { get; set; }

}
