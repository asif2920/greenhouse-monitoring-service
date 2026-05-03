namespace Greenhouse.Application.Services
{
    using Greenhouse.Domain.Entities;
    using Greenhouse.Domain.Interfaces;

    public class SensorReadingService
    {
        private readonly ISensorReadingRepository _sensorReadingRepository;

        public SensorReadingService(ISensorReadingRepository sensorReadingRepository)
        {
            _sensorReadingRepository = sensorReadingRepository;
        }

        public async Task<SensorReading?> GetLatestReadingAsync(CancellationToken cancellationToken = default)
        {
            return await _sensorReadingRepository.GetLatestAsync(cancellationToken);
        }

        public async Task AddReadingAsync(SensorReading reading, CancellationToken cancellationToken = default)
        {
            await _sensorReadingRepository.AddAsync(reading, cancellationToken);
        }
    }
}