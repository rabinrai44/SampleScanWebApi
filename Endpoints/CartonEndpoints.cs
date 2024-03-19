using SampleScanWebApi.Data;
using SampleScanWebApi.DTO;
using SampleScanWebApi.Entities;

namespace SampleScanWebApi.Endpoints;

public static class CartonEndpoints
{
    public static void MapCartonEndpoints(this WebApplication app)
    {
        // Cartons endpoints

        // Cartons endpoints

        // POST /cartons - Create Carton
        app.MapPost("/cartons", async (AppDbContext context, CreateCartonDto cartonDto) =>
        {
            List<Carton> cartons = [];
            foreach (CartonDto item in cartonDto.Cartons)
            {
                var carton = new Carton
                {
                    CartonId = item.CartonId,
                    CartonQuantity = item.CartonQuantity,
                    DeliveryId = item.DeliveryId,
                    TrackingNumber = item.TrackingNumber
                };
            }

            context.Cartons.AddRange(cartons);
            await context.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("CreateCarton")
        .WithOpenApi();
    }
}
