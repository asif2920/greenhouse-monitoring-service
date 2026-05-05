namespace Greenhouse.Api.Endpoints.SensorReadings;

using Greenhouse.Api.Contracts.SensorReadings;
using Microsoft.AspNetCore.Mvc;
using Greenhouse.Application.Services;


public static class GetLatestSensorReadingEndpoint
{
    public static void MapGetLatestReading(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/readings/latest", async ([FromServices] SensorReadingService sensorReadingService) =>
        {
            var latestReading = await sensorReadingService.GetLatestReadingAsync();

            if (latestReading == null)
            {
                return Results.Ok(null);
            }

            var response = new ReadingResponse(
                latestReading.Id,
                (int)latestReading.SequenceNumber,
                latestReading.Timestamp,
                latestReading.Temperature,
                latestReading.Humidity,
                latestReading.Co2Ppm
            );

            return Results.Ok(response);
        })
        .WithTags("SensorReadings")
        .WithSummary("Get the latest sensor reading")
        .WithDescription("Retrieves the most recent sensor reading from the database.");
    }
}