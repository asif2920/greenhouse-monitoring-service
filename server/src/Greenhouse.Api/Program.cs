using Greenhouse.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Greenhouse.Api.Endpoints.SensorReadings;
using Greenhouse.Infrastructure.Repositories;
using Greenhouse.Application.Services;
using Greenhouse.Domain.Interfaces;
using Greenhouse.Api.Endpoints.Anomalies;
using System.Text.Json;
using Greenhouse.Api.Hubs;
using Microsoft.AspNetCore.SignalR;
using Greenhouse.Domain.Services;


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

builder.Services.AddScoped<IAnomalyDetectionService, AnomalyDetectionService>();
builder.Services.AddScoped<AnomalyDetectionOrchestrator>();
builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(
            "http://localhost:4200",
            "https://localhost:4200"
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<GreenhouseDbContext>();
    db.Database.EnsureCreated();
}

app.MapGetLatestReading();
app.MapCreateReading();

app.MapGetRecentAnomalies();

app.MapHub<GreenhouseHub>("/hubs/greenhouse");

app.Run();

