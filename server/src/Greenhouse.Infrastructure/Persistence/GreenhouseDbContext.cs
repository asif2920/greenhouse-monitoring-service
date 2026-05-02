using Microsoft.EntityFrameworkCore;
using Greenhouse.Domain.Entities;

namespace Greenhouse.Infrastructure.Persistence;

public class GreenhouseDbContext : DbContext
{
    public GreenhouseDbContext(DbContextOptions<GreenhouseDbContext> options) : base(options)
    {
    }

    public DbSet<SensorReading> SensorReading => Set<SensorReading>();
    public DbSet<Anomaly> Anomaly => Set<Anomaly>();
}
