using Greenhouse.Domain.Entities;
using Greenhouse.Domain.Interfaces;

namespace Greenhouse.Application.Services;

public class AnomalyService
{
    private readonly IAnomalyRepository _repo;

    public AnomalyService(IAnomalyRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Anomaly>> GetRecentAsync(int count = 20, CancellationToken cancellationToken = default)
    {
        return await _repo.GetRecentAsync(count, cancellationToken);
    }
}
