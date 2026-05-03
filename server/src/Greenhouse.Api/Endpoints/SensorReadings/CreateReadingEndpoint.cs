namespace Greenhouse.Api.Endpoints.SensorReadings;

using System.Text.Json;
using Greenhouse.Api.Contracts.SensorReadings;
using Greenhouse.Application.Services;
using Greenhouse.Domain.Entities;
using Greenhouse.Api.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

public static class CreateReadingEndpoint
{
    public static void MapCreateReading(this WebApplication app)
    {
        app.MapPost("/api/readings", async (
            [FromBody] JsonElement payload,
            [FromServices] SensorReadingService sensorReadingService,
            [FromServices] AnomalyDetectionOrchestrator anomalyOrchestrator,
            [FromServices] IHubContext<GreenhouseHub> hub,
            CancellationToken cancellationToken) =>
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

                    await sensorReadingService.AddReadingAsync(reading, cancellationToken);

                    var anomaly = await anomalyOrchestrator.ProcessAsync(reading, cancellationToken);

                    await hub.Clients.All.SendAsync("NewReading", reading, cancellationToken);

                    if (anomaly != null)
                        await hub.Clients.All.SendAsync("AnomalyDetected", anomaly, cancellationToken);

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

            await sensorReadingService.AddReadingAsync(singleReading, cancellationToken);

            var singleAnomaly = await anomalyOrchestrator.ProcessAsync(singleReading, cancellationToken);

            await hub.Clients.All.SendAsync("NewReading", singleReading, cancellationToken);

            if (singleAnomaly != null)
                await hub.Clients.All.SendAsync("AnomalyDetected", singleAnomaly, cancellationToken);

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
