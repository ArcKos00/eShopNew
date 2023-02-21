using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Infrastructure.Services.Interfaces;

namespace Catalog.Host.Repositories
{
    public class AnomalyRepository : IAnomalyRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AnomalyRepository> _logger;

        public AnomalyRepository(
            IDbContextWrapper<ApplicationDbContext> wrapper,
            ILogger<AnomalyRepository> logger)
        {
            _context = wrapper.DbContext;
            _logger = logger;
        }

        public async Task<int?> Add(string name, int abnormalTypeId, int locationPlaceId, int frequenceId)
        {
            var entity = await _context.Anomaly.AddAsync(new AnomalyEntity()
            {
                Name = name
            });

            await _context.SaveChangesAsync();

            return entity?.Entity.Id;
        }

        public async Task<AnomalyEntity?> Get(int id)
        {
            return await _context.Anomaly.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<AnomalyEntity>?> GetAnomaly()
        {
            var result = await _context.Anomaly
                .Distinct()
                .OrderBy(o => o.Name)
                .ToListAsync();

            return result;
        }

        public async Task<bool> UpdateName(int id, string name)
        {
            var entity = await Get(id);
            if (entity == null)
            {
                _logger.LogError(LoggerDefaultResponse.FailedUpdate);
                return false;
            }

            entity.Name = name;
            _context.Entry(entity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await Get(id);
            if (entity == null)
            {
                _logger.LogError(LoggerDefaultResponse.FailedDelete);
                return false;
            }

            _context.Entry(entity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
