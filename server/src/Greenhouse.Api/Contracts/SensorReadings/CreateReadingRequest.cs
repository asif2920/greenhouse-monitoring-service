namespace Greenhouse.Api.Contracts.SensorReadings;
public record CreateReadingRequest(
    long SequenceNumber,
    DateTime Timestamp,
    decimal Temperature,
    decimal Humidity,
    int Co2Ppm
);
