using Greenhouse.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Greenhouse.Api.Endpoints.SensorReadings;
using Greenhouse.Infrastructure.Repositories;
using Greenhouse.Application.Services;
using Greenhouse.Domain.Interfaces;
using Greenhouse.Api.Endpoints.Anomalies;
using System.Text.Json;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<GreenhouseDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<ISensorReadingRepository, SensorReadingRepository>();
builder.Services.AddScoped<SensorReadingService>();

builder.Services.AddScoped<IAnomalyRepository, AnomalyRepository>();
builder.Services.AddScoped<AnomalyService>();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.PropertyNameCaseInsensitive = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGetLatestReading();
app.MapCreateReading();

app.MapGetRecentAnomalies();

app.Run();

