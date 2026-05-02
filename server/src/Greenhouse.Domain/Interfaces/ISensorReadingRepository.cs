namespace Greenhouse.Domain.Interfaces;
using Greenhouse.Domain.Entities;

public interface ISensorReadingRepository
{
     Task<SensorReading?> GetLatestAsync(CancellationToken cancellationToken = default);
}
