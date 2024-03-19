using Microsoft.EntityFrameworkCore;
using SampleScanWebApi.Data;
using SampleScanWebApi.DTO;

namespace SampleScanWebApi.Endpoints;

public static class ValidationEndpoints
{
    public static void MapValidationEndpoints(this WebApplication app)
    {
        // GET /validatePOSID endpoint
        app.MapGet("/validatePOSID/{posId}", async Task<IResult> (AppDbContext context, string posId) =>
        {
            var item = await context.Items.FirstOrDefaultAsync(i => i.ItemNumber == posId);
            if (item is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(item);
        })
        .WithName("ValidatePOSID")
        .WithOpenApi();


        // GET /validateUPC endpoint
        app.MapGet("/validateUPC/{upc}", async Task<IResult> (AppDbContext context, string upc) =>
        {
            var item = await context.Items.FirstOrDefaultAsync(i => i.ItemNumber == upc);
            if (item is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(item);
        })
        .WithName("ValidateUPC")
        .WithOpenApi();

        // CodeValues endpoints
        // POST /codeValues/retrieve
        app.MapPost("/codeValues/retrieve", async Task<IResult> (AppDbContext context, CodeValueRequest codeValue) =>
        {
            // Mocking the response
            var resp = new
            {
                codeString1 = "123456",
                codeString2 = "654321",
            };
            return Results.Ok(resp);
        })
        .WithName("RetrieveCodeValues")
        .WithOpenApi();
    }
}
