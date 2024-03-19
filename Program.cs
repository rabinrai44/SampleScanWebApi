using SampleScanWebApi.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SampleScanWebApi.DTO;
using SampleScanWebApi.Entities;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("SampleScanWebApi") ?? "Data Source=SampleScanWebApi.db";

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

// /items endpoint
app.MapGet("/items", async (AppDbContext context) =>
{
    var items = await context.Items.ToListAsync();
    return items;
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

// GET /validatePOSID endpoint
app.MapGet("/validatePOSID/{posId}", async (AppDbContext context, string posId) =>
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
app.MapGet("/validateUPC/{upc}", async (AppDbContext context, string upc) =>
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
app.MapPost("/codeValues/retrieve", (AppDbContext context, CodeValueRequest codeValue) =>
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

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
