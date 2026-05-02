namespace Greenhouse.Infrastructure.Repositories;

using Greenhouse.Domain.Entities;
using Greenhouse.Domain.Interfaces;
using Greenhouse.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class SensorReadingRepository : ISensorReadingRepository
{
    private readonly GreenhouseDbContext _context;

    public SensorReadingRepository(GreenhouseDbContext context)
    {
        _context = context;
    }

    public async Task<SensorReading?> GetLatestAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SensorReading
            .OrderByDescending(r => r.SequenceNumber)
            .FirstOrDefaultAsync(cancellationToken);
    }
}