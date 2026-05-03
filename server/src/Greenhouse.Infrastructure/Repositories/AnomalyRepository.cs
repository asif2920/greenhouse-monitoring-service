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

    public async Task AddAsync(Anomaly anomaly, CancellationToken cancellationToken = default)
    {
        await _db.Anomaly.AddAsync(anomaly, cancellationToken);

        var all = await _db.Anomaly
            .OrderByDescending(a => a.DetectedAt)
            .ToListAsync(cancellationToken);

        if (all.Count > 20)
        {
            var toRemove = all.Skip(20).ToList();
            _db.Anomaly.RemoveRange(toRemove);
        }

        await _db.SaveChangesAsync(cancellationToken);
    }
}
