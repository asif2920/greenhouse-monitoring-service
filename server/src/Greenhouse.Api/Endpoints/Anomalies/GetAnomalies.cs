namespace Greenhouse.Api.Endpoints.Anomalies;

using Microsoft.AspNetCore.Mvc;
using Greenhouse.Application.Services;
using Greenhouse.Api.Contracts.Anomalies;

public static class GetRecentAnomaliesEndpoint
{
    public static void MapGetRecentAnomalies(this WebApplication app)
    {
        app.MapGet("/api/anomalies", async (
            [FromServices] AnomalyService service) =>
        {
            var anomalies = await service.GetRecentAsync(20);

            var response = anomalies.Select(a => new AnomalyResponse(
                a.Id,
                a.DetectedAt,
                a.SensorType,
                a.Value,
                a.ZScore,
                a.Reason
            ));

            return Results.Ok(response);
        });
    }
}
