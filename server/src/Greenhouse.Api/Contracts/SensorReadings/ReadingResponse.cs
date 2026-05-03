namespace Greenhouse.Api.Contracts.SensorReadings;
public record ReadingResponse
(
    Guid Id,
    long SequenceNumber,
    DateTime Timestamp,
    decimal Temperature,
    decimal Humidity,
    int Co2Ppm
);