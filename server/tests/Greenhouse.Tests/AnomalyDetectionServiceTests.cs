using Greenhouse.Domain.Entities;
using Greenhouse.Domain.Services;
using Xunit;

namespace Greenhouse.Tests
{
    public class AnomalyDetectionServiceTests
    {
        [Fact]
        public void Detect_WithTemperatureSpike_ReturnsTemperatureAnomaly()
        {
            var detector = new AnomalyDetectionService();

            var recent = new List<SensorReading>
            {
                new() { Temperature = 20, Humidity = 50, Co2Ppm = 500 },
                new() { Temperature = 21, Humidity = 51, Co2Ppm = 505 },
                new() { Temperature = 19, Humidity = 49, Co2Ppm = 498 },
                new() { Temperature = 20, Humidity = 50, Co2Ppm = 502 },
                new() { Temperature = 21, Humidity = 52, Co2Ppm = 501 }
            };

            var currentReading = new SensorReading
            {
                Temperature = 100,
                Humidity = 50,
                Co2Ppm = 500
            };

            var result = detector.Detect(currentReading, recent);

            Assert.Single(result);
            Assert.Equal("temperature", result[0].SensorType);
        }

        [Fact]
        public void Detect_WithNormalReading_ReturnsNoAnomalies()
        {
            var detector = new AnomalyDetectionService();

            var recent = new List<SensorReading>
            {
                new() { Temperature = 20, Humidity = 50, Co2Ppm = 500 },
                new() { Temperature = 21, Humidity = 51, Co2Ppm = 505 },
                new() { Temperature = 19, Humidity = 49, Co2Ppm = 498 },
                new() { Temperature = 20, Humidity = 50, Co2Ppm = 502 },
                new() { Temperature = 21, Humidity = 52, Co2Ppm = 501 }
            };

            var currentReading = new SensorReading
            {
                Temperature = 20,
                Humidity = 50,
                Co2Ppm = 500
            };

            var result = detector.Detect(currentReading, recent);

            Assert.Empty(result);
        }

        [Fact]
        public void Detect_WithMultiSensorSpike_ReturnsThreeAnomalies()
        {
            var detector = new AnomalyDetectionService();

            var recent = new List<SensorReading>
            {
                new() { Temperature = 20, Humidity = 50, Co2Ppm = 500 },
                new() { Temperature = 21, Humidity = 51, Co2Ppm = 505 },
                new() { Temperature = 19, Humidity = 49, Co2Ppm = 498 },
                new() { Temperature = 20, Humidity = 50, Co2Ppm = 502 },
                new() { Temperature = 21, Humidity = 52, Co2Ppm = 501 }
            };

            var currentReading = new SensorReading
            {
                Temperature = 100,
                Humidity = 5,
                Co2Ppm = 2000
            };

            var result = detector.Detect(currentReading, recent);

            Assert.Equal(3, result.Count);
            Assert.Contains(result, a => a.SensorType == "temperature");
            Assert.Contains(result, a => a.SensorType == "humidity");
            Assert.Contains(result, a => a.SensorType == "co2");
        }
    }
}
