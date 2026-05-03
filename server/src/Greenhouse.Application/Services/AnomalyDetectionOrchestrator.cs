namespace Greenhouse.Application.Services;

using Greenhouse.Domain.Entities;
using Greenhouse.Domain.Interfaces;

public class AnomalyDetectionOrchestrator
{
    private readonly ISensorReadingRepository _readingRepo;
    private readonly IAnomalyRepository _anomalyRepo;
    private readonly IAnomalyDetectionService _detector;

    public AnomalyDetectionOrchestrator(
        ISensorReadingRepository readingRepo,
        IAnomalyRepository anomalyRepo,
        IAnomalyDetectionService detector)
    {
        _readingRepo = readingRepo;
        _anomalyRepo = anomalyRepo;
        _detector = detector;
    }

    public async Task<List<Anomaly>> ProcessAsync(
        SensorReading newReading,
        CancellationToken cancellationToken = default)
    {
        var recent = await _readingRepo.GetRecentAsync(20, cancellationToken);

        var anomalies = _detector.Detect(newReading, recent);

        foreach (var anomaly in anomalies)
        {
            await _anomalyRepo.AddAsync(anomaly);
        }

        return anomalies;
    }
}
