namespace Greenhouse.Domain.Entities;

public class Anomaly
{
    public Guid Id { get; set; }
    public DateTime DetectedAt { get; set; }
    public required string SensorType { get; set; }
    public decimal Value { get; set; }
    public decimal ZScore { get; set; }
    public required string Reason { get; set; }
}

