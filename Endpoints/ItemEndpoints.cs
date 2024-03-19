using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SampleScanWebApi.Data;
using SampleScanWebApi.Entities;

namespace SampleScanWebApi.Endpoints;

public static class ItemEndpoints
{
    public static void MapItemEndpoints(this WebApplication app)
    {
        // /items endpoint
        app.MapGet("/items", async Task<IResult> (AppDbContext context) =>
        {
            var items = await context.Items.ToListAsync();
            return TypedResults.Ok(items);
        })
         .WithName("GetItems")
         .WithOpenApi();


        // /items/{id} endpoint
        app.MapGet("/items/{id}", async Task<Results<Ok<Item>, NotFound>> (AppDbContext context, int id) =>
        {
            var item = await context.Items.FindAsync(id);
            if (item is null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(item);
        })
        .WithName("GetItem")
        .WithOpenApi();

        // POST /items endpoint
        app.MapPost("/items", async (AppDbContext context, Item item) =>
        {
            context.Items.Add(item);
            await context.SaveChangesAsync();
            return Results.Created($"/items/{item.Id}", item);
        })
        .WithName("CreateItem")
        .WithOpenApi();
    }
}
