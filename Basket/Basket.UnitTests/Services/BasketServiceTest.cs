using Basket.Host.Configurations;
using Basket.Host.Models.Basket;
using Basket.Host.Services;
using Basket.Host.Services.Interfaces;
using FluentAssertions;
using Infrastructure.CommonValues;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Basket.UnitTests.Services
{
    public class BasketServiceTest
    {
        private readonly IBasketService _service;

        private readonly Mock<ILogger<BasketService>> _logger;
        private readonly Mock<ICacheService> _cache;
        private readonly Mock<IInternalHttpClientService> _internalHttp;
        private readonly Mock<IOptions<Config>> _config;

        public BasketServiceTest()
        {
            _logger = new Mock<ILogger<BasketService>>();
            _cache = new Mock<ICacheService>();
            _internalHttp = new Mock<IInternalHttpClientService>();
            _config = new Mock<IOptions<Config>>();

            _config.Setup(s => s.Value).Returns(new Config());

            _service = new BasketService(
                _cache.Object,
                _logger.Object,
                _internalHttp.Object,
                _config.Object);
        }

        [Fact]
        public async Task AddToBasket_Successful()
        {
            // arrange
            var testId = 1;
            var testName = "Test";
            var testCost = 1m;
            var testBasket = new BasketModel()
            {
                BasketList = new List<BasketItem>
                {
                    new BasketItem()
                    {
                        Id = testId,
                        Name= testName,
                        Cost = testCost,
                    }
                },
                TotalCost = testCost,
            };
            _cache.Setup(s => s.GetAsync<BasketModel>(It.IsAny<string>())).ReturnsAsync(testBasket);

            // act
            await _service.AddToBasket(It.IsAny<string>(), testId, testName, testCost);

            // assert
            _logger.Verify(
                v => v.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString()!.Contains(LoggerDefaultResponse.SuccessfulUpdate)),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
                Times.Once);
        }

        [Fact]
        public async Task AddToBasket_Failed()
        {
            // arrange
            var testId = 1;
            var testName = "Test";
            var testCost = 1m;
            BasketModel test = null!;
            _cache.Setup(s => s.GetAsync<BasketModel>(It.IsAny<string>())).ReturnsAsync(test);

            // act
            await _service.AddToBasket(It.IsAny<string>(), testId, testName, testCost);

            // assert
            _logger.Verify(
                v => v.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString()!.Contains("Basket is empty")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
                Times.Once);
        }

        [Fact]
        public async Task RemoveFromBasket_Failed_FindCache()
        {
            // arrange
            var testUser = "Test";
            var testItemId = 1;
            BasketModel testBasket = null!;
            _cache.Setup(s => s.GetAsync<BasketModel>(testUser)).ReturnsAsync(testBasket);

            // act
            await _service.RemoveFromBasket(testUser, testItemId);

            // assert
            _logger.Verify(
                v => v.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString()!.Contains($"Value with key: {testUser} — {LoggerDefaultResponse.NotFound}")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
                Times.Once);
        }

        [Fact]
        public async Task RemoveFromBasket_Failed_FindItem()
        {
            // arrange
            var testUser = "Test";
            var testId = 3;
            var testName = "";
            var testCost = 0;
            var testBasket = new BasketModel()
            {
                BasketList = new List<BasketItem>
                {
                    new BasketItem()
                    {
                        Name= testName,
                        Cost = testCost,
                    }
                },
                TotalCost = 0,
            };
            _cache.Setup(s => s.GetAsync<BasketModel>(testUser)).ReturnsAsync(testBasket);

            // act
            await _service.RemoveFromBasket(testUser, testId);

            // assert
            _logger.Verify(
                v => v.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString()!.Contains($"Value with key: {testUser} ItemId: {testId} — {LoggerDefaultResponse.NotFound}")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
                Times.Once);
        }

        [Fact]
        public async Task RemoveFromBasket_Successful()
        {
            // arrange
            var testUser = "Test";
            var testId = 2;
            var testName = "Test";
            var testCost = 1;
            var testBasket = new BasketModel()
            {
                BasketList = new List<BasketItem>
                {
                    new BasketItem()
                    {
                        Id = testId,
                        Name= testName,
                        Cost = testCost,
                    },
                    new BasketItem()
                    {
                        Id = testId,
                        Name= testName,
                        Cost = testCost,
                    }
                },
                TotalCost = 2,
            };
            _cache.Setup(s => s.GetAsync<BasketModel>(testUser)).ReturnsAsync(testBasket);

            // act
            await _service.RemoveFromBasket(testUser, testId);

            // assert
            _logger.Verify(
                v => v.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString()!.Contains(LoggerDefaultResponse.SuccessfulDelete)),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
                Times.Once);
        }

        [Fact]
        public async Task GetBasket_Successful()
        {
            // arrange
            var testUser = "Test";
            var testId = 2;
            var testName = "Test";
            var testCost = 1;
            var testBasket = new BasketModel()
            {
                BasketList = new List<BasketItem>
                {
                    new BasketItem()
                    {
                        Id = testId,
                        Name= testName,
                        Cost = testCost,
                    },
                    new BasketItem()
                    {
                        Id = testId,
                        Name= testName,
                        Cost = testCost,
                    }
                },
                TotalCost = 2,
            };
            _cache.Setup(s => s.GetAsync<BasketModel>(It.IsAny<string>())).ReturnsAsync(testBasket);

            // act
            var result = await _service.GetBasket(It.IsAny<string>());

            // assert
            result.Should().NotBeNull();
            result?.BasketList.Should().NotBeNullOrEmpty();
            result.Should().Be(testBasket);
        }

        [Fact]
        public async Task GetBasket_Failed()
        {
            // arrange
            BasketModel testBasket = null!;
            _cache.Setup(s => s.GetAsync<BasketModel>(It.IsAny<string>())).ReturnsAsync(testBasket);

            // act
            var result = await _service.GetBasket(It.IsAny<string>());

            // assert
            result.Should().NotBeNull();
            result?.BasketList.Should().BeNullOrEmpty();
            _logger.Verify(v => v.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString()!.Contains(LoggerDefaultResponse.NotFound)),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()!), Times.Once);
        }

        [Fact]
        public async Task MakeAnOrder_Successful()
        {
            // arrange
            var testUser = "Test";
            var testId = 2;
            var testName = "Test";
            var testCost = 1;
            var testBasket = new BasketModel()
            {
                BasketList = new List<BasketItem>
                {
                    new BasketItem()
                    {
                        Id = testId,
                        Name= testName,
                        Cost = testCost,
                    },
                    new BasketItem()
                    {
                        Id = testId,
                        Name= testName,
                        Cost = testCost,
                    }
                },
                TotalCost = 2,
            };
            _cache.Setup(s => s.GetAsync<BasketModel>(It.IsAny<string>())).ReturnsAsync(testBasket);
            // act
            await _service.MakeAnOrder(It.IsAny<string>());

            // assert
            _logger.Verify(
                v => v.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString()!.Contains("the Сache has been cleared")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()!), Times.Once);
        }

        [Fact]
        public async Task MakeAnOrder_Failed()
        {
            // arrange
            BasketModel testBasket = null!;
            _cache.Setup(s => s.GetAsync<BasketModel>(It.IsAny<string>())).ReturnsAsync(testBasket);

            // act
            await _service.MakeAnOrder(It.IsAny<string>());

            // assert
            _logger.Verify(v => v.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString()!.Contains($"the Order was {LoggerDefaultResponse.NotFound}")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()!), Times.Once);
        }
    }
}
