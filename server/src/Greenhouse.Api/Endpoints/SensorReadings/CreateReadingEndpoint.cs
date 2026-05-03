namespace Greenhouse.Api.Endpoints.SensorReadings;

using System.Text.Json;
using Greenhouse.Api.Contracts.SensorReadings;
using Greenhouse.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Greenhouse.Domain.Entities;

public static class CreateReadingEndpoint
{
    public static void MapCreateReading(this WebApplication app)
    {
        app.MapPost("/api/readings", async (
            [FromBody] JsonElement payload,
            [FromServices] SensorReadingService sensorReadingService) =>
        {
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };

            if (payload.ValueKind == JsonValueKind.Array)
            {
                var requests = JsonSerializer.Deserialize<List<CreateReadingRequest>>(
                    payload.GetRawText(),
                    jsonOptions
                )!;

                var responses = new List<CreateReadingResponse>();

                foreach (var req in requests)
                {
                    var reading = new SensorReading
                    {
                        Id = Guid.NewGuid(),
                        SequenceNumber = req.SequenceNumber,
                        Timestamp = req.Timestamp,
                        Temperature = req.Temperature,
                        Humidity = req.Humidity,
                        Co2Ppm = req.Co2Ppm
                    };

                    await sensorReadingService.AddReadingAsync(reading);

                    responses.Add(new CreateReadingResponse(
                        reading.Id,
                        reading.SequenceNumber,
                        reading.Timestamp,
                        reading.Temperature,
                        reading.Humidity,
                        reading.Co2Ppm
                    ));
                }

                return Results.Ok(responses);
            }

            var request = JsonSerializer.Deserialize<CreateReadingRequest>(
                payload.GetRawText(),
                jsonOptions
            )!;

            var singleReading = new SensorReading
            {
                Id = Guid.NewGuid(),
                SequenceNumber = request.SequenceNumber,
                Timestamp = request.Timestamp,
                Temperature = request.Temperature,
                Humidity = request.Humidity,
                Co2Ppm = request.Co2Ppm
            };

            await sensorReadingService.AddReadingAsync(singleReading);

            var singleResponse = new CreateReadingResponse(
                singleReading.Id,
                singleReading.SequenceNumber,
                singleReading.Timestamp,
                singleReading.Temperature,
                singleReading.Humidity,
                singleReading.Co2Ppm
            );

            return Results.Created($"/api/readings/{singleReading.Id}", singleResponse);
        });
    }
}