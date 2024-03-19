using SampleScanWebApi.Data;
using Microsoft.EntityFrameworkCore;
using SampleScanWebApi.Endpoints;

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

// Map the endpoints
app.MapItemEndpoints();
app.MapCartonEndpoints();
app.MapValidationEndpoints();
app.MapTrackingEndpoints();
app.MapWeatherForecastEndpoints();

app.Run();
