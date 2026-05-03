namespace Greenhouse.Domain.Interfaces;

using Greenhouse.Domain.Entities;

public interface IAnomalyDetectionService
{
    List<Anomaly> Detect(SensorReading newReading, List<SensorReading> recentReadings);
}
