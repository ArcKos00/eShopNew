using Basket.Host.Configurations;
using Basket.Host.Services;
using Basket.Host.Services.Interfaces;
using FluentAssertions;
using Infrastructure.CommonValues;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using StackExchange.Redis;

namespace Basket.UnitTests.Services
{
    public class CacheServiceTest
    {

        private readonly ICacheService _service;

        private readonly Mock<IOptions<RedisConfig>> _config;
        private readonly Mock<ILogger<CacheService>> _logger;
        private readonly Mock<IRedisCacheConnectionService> _redis;
        private readonly Mock<IJsonSerializer> _serializer;
        private readonly Mock<IConnectionMultiplexer> _connectionMultiplexer;
        private readonly Mock<IDatabase> _database;

        public CacheServiceTest()
        {
            _config = new Mock<IOptions<RedisConfig>>();
            _logger = new Mock<ILogger<CacheService>>();
            _redis = new Mock<IRedisCacheConnectionService>();
            _connectionMultiplexer = new Mock<IConnectionMultiplexer>();
            _database = new Mock<IDatabase>();
            _serializer = new Mock<IJsonSerializer>();

            _config.Setup(s => s.Value).Returns(new RedisConfig() { CacheTimeout = TimeSpan.Zero});
            _connectionMultiplexer.Setup(s => s.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(_database.Object);
            _redis.Setup(s => s.Connection).Returns(_connectionMultiplexer.Object);

            _service = new CacheService(
                _logger.Object,
                _redis.Object,
                _serializer.Object,
                _config.Object);
        }

        [Fact]
        public async Task AddOrUpdate_Successful_Add()
        {
            // arrange
            var testEntity = new
            {
                userId = "Test",
                Data = "data"
            };

            _database.Setup(expression: s => s.StringSetAsync(
                It.IsAny<RedisKey>(),
                It.IsAny<RedisValue>(),
                It.IsAny<TimeSpan>(),
                It.IsAny<When>(),
                It.IsAny<CommandFlags>()))
                .ReturnsAsync(false);

            // act
            await _service.AddOrUpdateAsync(testEntity.userId, testEntity.Data);

            // assert
            _logger.Verify(
                v => v.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString()!
                    .Contains(LoggerDefaultResponse.ValueCached)),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
                Times.AtMostOnce);
        }

        [Fact]
        public async Task AddOrUpdate_Successful_Update()
        {
            // arrange
            var testEntity = new
            {
                userId = "Test",
                Data = "data"
            };

            _database.Setup(expression: s => s.StringSetAsync(
                It.IsAny<RedisKey>(),
                It.IsAny<RedisValue>(),
                It.IsAny<TimeSpan>(),
                It.IsAny<When>(),
                It.IsAny<CommandFlags>()))
                .ReturnsAsync(false);

            // act
            await _service.AddOrUpdateAsync(testEntity.userId, testEntity.Data);
            await _service.AddOrUpdateAsync(testEntity.userId, testEntity.Data);

            // assert
            _logger.Verify(
                v => v.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString()!
                    .Contains(LoggerDefaultResponse.ValueUpdated)),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
                Times.AtLeastOnce);
        }

        [Fact]
        public async Task AddOrUpdate_Failed()
        {
            // arrange
            var testEntity = new
            {
                userId = "Test",
                Data = "data"
            };

            _database.Setup(expression: s => s.StringSetAsync(
                It.IsAny<RedisKey>(),
                It.IsAny<RedisValue>(),
                It.IsAny<TimeSpan>(),
                It.IsAny<When>(),
                It.IsAny<CommandFlags>()))
                .ReturnsAsync(false);
            // act
            await _service.AddOrUpdateAsync(testEntity.userId, testEntity.Data);
            await _service.AddOrUpdateAsync(testEntity.userId, testEntity.Data);

            // assert
            _logger.Verify(
                v => v.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString()!.Contains(LoggerDefaultResponse.ValueCached)),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
                Times.Never);
        }

        [Fact]
        public async Task GetAsync_Successful()
        {
            // arrange
            var data = "Test";

            _serializer.Setup(s => s.Deserialize<string>(It.IsAny<string>())).Returns(data);
            _database.Setup(expression: s => s.StringGetAsync(
                It.IsAny<RedisKey>(),
                It.IsAny<CommandFlags>())).ReturnsAsync(data);

            // act
            var result = await _service.GetAsync<string>(data);

            // assert
            result.Should().Be(data);
        }

        [Fact]
        public async Task GetAsync_Failed()
        {
            // arrange
            var testName = "Test";

            // act
            var result = await _service.GetAsync<string>(testName);

            // assert
            result.Should().BeNull();
        }
    }
}