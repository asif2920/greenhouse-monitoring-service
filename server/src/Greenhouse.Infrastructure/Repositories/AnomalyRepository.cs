using Greenhouse.Domain.Entities;
using Greenhouse.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Greenhouse.Infrastructure.Persistence;

namespace Greenhouse.Infrastructure.Repositories;

public class AnomalyRepository : IAnomalyRepository
{
    private readonly GreenhouseDbContext _db;

    public AnomalyRepository(GreenhouseDbContext db)
    {
        _db = db;
    }

    public async Task<List<Anomaly>> GetRecentAsync(int count = 20, CancellationToken cancellationToken = default)
    {
        return await _db.Anomaly
            .OrderByDescending(a => a.DetectedAt)
            .Take(count)
            .ToListAsync(cancellationToken);
    }
}
