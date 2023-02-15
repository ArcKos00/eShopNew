using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Infrastructure.CommonValues;
using Infrastructure.Services.Interfaces;

namespace Catalog.Host.Repositories
{
    public class AbnormalTypeRepository : IAbnormalTypeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AbnormalTypeRepository> _logger;

        public AbnormalTypeRepository(IDbContextWrapper<ApplicationDbContext> wrapper, ILogger<AbnormalTypeRepository> logger)
        {
            _context = wrapper.DbContext;
            _logger = logger;
        }

        public async Task<int?> Add(string name)
        {
            var entity = await _context.AbnormalType.AddAsync(new AbnormalTypeEntity()
            {
                Name = name,
            });

            await _context.SaveChangesAsync();
            return entity?.Entity.Id;
        }

        public async Task<AbnormalTypeEntity?> Get(int id)
        {
            return await _context.AbnormalType
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<AbnormalTypeEntity>?> GetTypes()
        {
            var result = await _context.AbnormalType
                .Distinct()
                .OrderBy(f => f.Name)
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
