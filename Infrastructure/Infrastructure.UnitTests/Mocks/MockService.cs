using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infrastructure.UnitTests.Mocks
{
    public class MockService : BaseDataService<MockDbContex>
    {
        public MockService(
            IDbContextWrapper<MockDbContex> wrapper,
            ILogger<MockService> logger)
            : base(wrapper, logger)
        {
        }

        public async Task RunWithException()
        {
            await ExecuteSafeAsync(() => throw new Exception());
        }

        public async Task RunWithoutException()
        {
            await ExecuteSafeAsync(() => Task.CompletedTask);
        }
    }
}
