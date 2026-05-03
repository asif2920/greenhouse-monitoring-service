namespace Greenhouse.Domain.Interfaces;
using Greenhouse.Domain.Entities;

public interface ISensorReadingRepository
{
     Task<SensorReading?> GetLatestAsync(CancellationToken cancellationToken = default);
     Task AddAsync(SensorReading reading, CancellationToken cancellationToken = default);
     Task<List<SensorReading>> GetRecentAsync(int count = 20, CancellationToken cancellationToken = default);

}
