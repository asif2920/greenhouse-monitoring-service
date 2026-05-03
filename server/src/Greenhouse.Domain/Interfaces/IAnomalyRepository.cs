namespace Greenhouse.Domain.Interfaces;

using Greenhouse.Domain.Entities;

public interface IAnomalyRepository
{
    Task<List<Anomaly>> GetRecentAsync(int count = 20, CancellationToken cancellationToken = default);
    Task AddAsync(Anomaly anomaly, CancellationToken cancellationToken = default);

}
