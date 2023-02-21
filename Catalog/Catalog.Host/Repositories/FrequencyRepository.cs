using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Infrastructure.Services.Interfaces;

namespace Catalog.Host.Repositories
{
    public class FrequencyRepository : IFrequencyRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FrequencyRepository> _logger;

        public FrequencyRepository(IDbContextWrapper<ApplicationDbContext> wrapper, ILogger<FrequencyRepository> logger)
        {
            _context = wrapper.DbContext;
            _logger = logger;
        }

        public async Task<int?> Add(string meets)
        {
            var entity = await _context.Frequency.AddAsync(new FrequencyEntity()
            {
                Meets = meets
            });

            await _context.SaveChangesAsync();
            return entity.Entity.Id;
        }

        public async Task<FrequencyEntity?> Get(int id)
        {
            return await _context.Frequency.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<FrequencyEntity>?> GetMeets()
        {
            return await _context.Frequency
                .Distinct()
                .OrderBy(o => o.Meets)
                .ToListAsync();
        }

        public async Task<bool> UpdateMeet(int id, string meet)
        {
            var entity = await Get(id);
            if (entity == null)
            {
                _logger.LogError(LoggerDefaultResponse.FailedUpdate);
                return false;
            }

            entity.Meets = meet;
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
