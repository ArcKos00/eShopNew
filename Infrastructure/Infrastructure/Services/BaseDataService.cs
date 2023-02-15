using Infrastructure.CommonValues;
using Infrastructure.Services.Interfaces;

namespace Infrastructure.Services
{
    public abstract class BaseDataService<T>
        where T : DbContext
    {
        private readonly IDbContextWrapper<T> _dbContextWrapper;
        private readonly ILogger<BaseDataService<T>> _logger;

        public BaseDataService(
            IDbContextWrapper<T> wrapper,
            ILogger<BaseDataService<T>> logger)
        {
            _dbContextWrapper = wrapper;
            _logger = logger;
        }

        protected Task ExecuteSafeAsync(Func<Task> action, CancellationToken token = default) => ExecuteSafeAsync(cancellationToken => action(), token);

        protected Task<TResult> ExecuteSafeAsync<TResult>(Func<Task<TResult>> action, CancellationToken token = default) => ExecuteSafeAsync<TResult>(cancellationToken => action(), token);

        private async Task ExecuteSafeAsync(Func<CancellationToken, Task> action, CancellationToken token = default)
        {
            await using var transaction = await _dbContextWrapper.BeginTransactionAsync(token);

            try
            {
                await action(token);
                await transaction.CommitAsync(token);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(token);
                _logger.LogError(ex, LoggerDefaultResponse.Rollbacked);
            }
        }

        private async Task<TResult> ExecuteSafeAsync<TResult>(Func<CancellationToken, Task<TResult>> action, CancellationToken token = default)
        {
            await using var transaction = await _dbContextWrapper.BeginTransactionAsync(token);

            try
            {
                var result = await action(token);
                await transaction.CommitAsync(token);

                return result;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(token);
                _logger.LogError(ex, LoggerDefaultResponse.Rollbacked);
            }

            return default(TResult)!;
        }
    }
}
