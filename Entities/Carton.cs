namespace SampleScanWebApi.Entities;

public class Carton
{
    public int Id { get; set; }
    public required string CartonId { get; set; }
    public int CartonQuantity { get; set; }
    public required string DeliveryId { get; set; }
    public required string TrackingNumber { get; set; }

    public Carton()
    {

    }

    public Carton(string cartonId, int cartonQuantity, string deliveryId, string trackingNumber)
    {
        CartonId = cartonId;
        CartonQuantity = cartonQuantity;
        DeliveryId = deliveryId;
        TrackingNumber = trackingNumber;
    }
}