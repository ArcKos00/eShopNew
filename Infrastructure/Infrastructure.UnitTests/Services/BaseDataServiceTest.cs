using Infrastructure.CommonValues;

namespace Infrastructure.UnitTests.Services
{
    public class BaseDataServiceTest
    {
        private readonly Mock<IDbContextTransaction> _dbContextTransaction;
        private readonly Mock<ILogger<MockService>> _logger;
        private readonly MockService _mockService;

        public BaseDataServiceTest()
        {
            var dbContextWrapper = new Mock<IDbContextWrapper<MockDbContex>>();
            _dbContextTransaction = new Mock<IDbContextTransaction>();
            _logger = new Mock<ILogger<MockService>>();

            dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(_dbContextTransaction.Object);
            _mockService = new MockService(dbContextWrapper.Object, _logger.Object);
        }

        [Fact]
        public async Task ExecuteSafe_Success()
        {
            // arrange

            // act
            await _mockService.RunWithoutException();

            // assert
            _dbContextTransaction.Verify(v => v.CommitAsync(CancellationToken.None), Times.Once);
            _dbContextTransaction.Verify(v => v.RollbackAsync(CancellationToken.None), Times.Never);
        }

        [Fact]
        public async Task ExecuteSafe_Failed()
        {
            // arrange

            // act
            await _mockService.RunWithException();

            // assert
            _dbContextTransaction.Verify(v => v.CommitAsync(CancellationToken.None), Times.Never);
            _dbContextTransaction.Verify(v => v.RollbackAsync(CancellationToken.None), Times.Once);

            _logger.Verify(
                v => v.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString() !
                        .Contains(LoggerDefaultResponse.Rollbacked)),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>() !),
                Times.Once);
        }
    }
}