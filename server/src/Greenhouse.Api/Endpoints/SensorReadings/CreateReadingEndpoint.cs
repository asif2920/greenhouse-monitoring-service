namespace Greenhouse.Api.Endpoints.SensorReadings;
using Greenhouse.Api.Contracts.SensorReadings;
using Greenhouse.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Greenhouse.Domain.Entities;

public static class CreateReadingEndpoint
{
    public static void MapCreateReading(this WebApplication app)
    {
        app.MapPost("/api/readings", async (
            [FromBody] CreateReadingRequest request,
            [FromServices] SensorReadingService sensorReadingService) =>
        {
            var reading = new SensorReading
            {
                Id = Guid.NewGuid(),
                SequenceNumber = request.SequenceNumber,
                Timestamp = request.Timestamp,
                Temperature = request.Temperature,
                Humidity = request.Humidity,
                Co2Ppm = request.Co2Ppm
            };

            await sensorReadingService.AddReadingAsync(reading);

            var response = new CreateReadingResponse(
                reading.Id,
                reading.SequenceNumber,
                reading.Timestamp,
                reading.Temperature,
                reading.Humidity,
                reading.Co2Ppm
            );

            return Results.Created($"/api/readings/{reading.Id}", response);
        });
    }
}