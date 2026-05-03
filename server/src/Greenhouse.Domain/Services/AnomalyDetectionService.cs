namespace Greenhouse.Domain.Services;

using Greenhouse.Domain.Entities;
using Greenhouse.Domain.Interfaces;

public class AnomalyDetectionService : IAnomalyDetectionService
{
    private const decimal Threshold = 2.5m;

    public List<Anomaly> Detect(SensorReading newReading, List<SensorReading> recent)
    {
        var anomalies = new List<Anomaly>();

        if (recent.Count < 5)
            return anomalies;

        var tempAnomaly = DetectForMetric(
            newReading.Temperature,
            recent.Select(r => r.Temperature).ToList(),
            "temperature"
        );
        if (tempAnomaly != null)
            anomalies.Add(tempAnomaly);

        var humidityAnomaly = DetectForMetric(
            newReading.Humidity,
            recent.Select(r => r.Humidity).ToList(),
            "humidity"
        );
        if (humidityAnomaly != null)
            anomalies.Add(humidityAnomaly);

        var co2Anomaly = DetectForMetric(
            newReading.Co2Ppm,
            recent.Select(r => (decimal)r.Co2Ppm).ToList(),
            "co2"
        );
        if (co2Anomaly != null)
            anomalies.Add(co2Anomaly);

        return anomalies;
    }

    private Anomaly? DetectForMetric(decimal newValue, List<decimal> values, string sensorType)
    {
        var mean = values.Average();
        var variance = values.Sum(v => (v - mean) * (v - mean)) / values.Count;
        var stdDev = (decimal)Math.Sqrt((double)variance);

        if (stdDev == 0)
            return null;

        var z = (newValue - mean) / stdDev;

        if (Math.Abs(z) <= Threshold)
            return null;

        return new Anomaly
        {
            Id = Guid.NewGuid(),
            DetectedAt = DateTime.UtcNow,
            SensorType = sensorType,
            Value = newValue,
            ZScore = z,
            Reason = $"Z-score {z:F2} exceeded threshold for {sensorType}"
        };
    }
}
