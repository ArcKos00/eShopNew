using Infrastructure.Services.Interfaces;

namespace Infrastructure.Services
{
    public class DbContextWrapper<T> : IDbContextWrapper<T>
        where T : DbContext
    {
        private readonly T _dbContext;

        public DbContextWrapper(IDbContextFactory<T> context)
        {
            _dbContext = context.CreateDbContext();
        }

        public T DbContext => _dbContext;

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken token)
        {
            return _dbContext.Database.BeginTransactionAsync(token);
        }
    }
}
