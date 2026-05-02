namespace Greenhouse.Api.Contracts.Anomalies;

public record AnomalyResponse(
    Guid Id,
    DateTime DetectedAt,
    string SensorType,
    decimal Value,
    decimal ZScore,
    string Reason
);
