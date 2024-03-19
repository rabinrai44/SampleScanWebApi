using Microsoft.EntityFrameworkCore;
using SampleScanWebApi.Data;
using SampleScanWebApi.DTO;

namespace SampleScanWebApi.Endpoints;

public static class TrackingEndpoints
{
    public static void MapTrackingEndpoints(this WebApplication app)
    {
        // TrackingNumbers endpoints

        // GET /trackingNumbers
        app.MapGet("/trackingNumbers/{trackingNumber}/validate", async (AppDbContext context, string trackingNumber) =>
        {
            var item = await context.Items.FirstOrDefaultAsync(i => i.ItemNumber == trackingNumber);
            if (item is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(item);
        })
        .WithName("ValidateTrackingNumber")
        .WithOpenApi();
    }
}
