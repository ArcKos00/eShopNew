using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Infrastructure.Services.Interfaces;

namespace Catalog.Host.Repositories
{
    public class ArtefactRepository : IArtefactRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ArtefactRepository> _logger;

        public ArtefactRepository(
            IDbContextWrapper<ApplicationDbContext> wrapper,
            ILogger<ArtefactRepository> logger)
        {
            _context = wrapper.DbContext;
            _logger = logger;
        }

        public async Task<int?> Add(string name, decimal cost, string img, string nature, int anomalyId, int abnormalTypeId, int frequencyId, int characteristicId)
        {
            var entity = await _context.Artefact.AddAsync(new ArtefactEntity()
            {
                Name = name,
                Cost = cost,
                ImagePath = img,
                Nature = nature,
                AnomalyId = anomalyId,
                AbnormalTypeId = abnormalTypeId,
                FrequencyId = frequencyId,
                CharacteristicId = characteristicId
            });

            await _context.SaveChangesAsync();
            return entity.Entity.Id;
        }

        public async Task<ArtefactEntity?> Get(int id)
        {
            return await _context.Artefact.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<ArtefactEntity?> GetWithContent(int id)
        {
            return await _context.Artefact
                .Include(i => i.Anomaly)
                .Include(i => i.Type)
                .Include(i => i.Meets)
                .Include(i => i.Values)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<PaginatedItems<ArtefactEntity>> GetPage(int pageIndex, int pageSize, int? anomalyFilter, int? abnormalFilter, int? meetsFilter)
        {
            IQueryable<ArtefactEntity> query = _context.Artefact;

            if (anomalyFilter.HasValue)
            {
                query = query.Where(w => w.AnomalyId == anomalyFilter.Value);
            }

            if (abnormalFilter.HasValue)
            {
                query = query.Where(w => w.AbnormalTypeId == abnormalFilter.Value);
            }

            if (meetsFilter.HasValue)
            {
                query = query.Where(w => w.FrequencyId == meetsFilter.Value);
            }

            var totalItems = await query.LongCountAsync();

            var itemsOnPage = await query.OrderBy(o => o.Name)
                .Include(i => i.Anomaly)
                .Include(i => i.Type)
                .Include(i => i.Meets)
                .Include(i => i.Values)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedItems<ArtefactEntity>() { TotalCount = totalItems, Data = itemsOnPage };
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

        public async Task<bool> UpdateNature(int id, string nature)
        {
            var entity = await Get(id);
            if (entity == null)
            {
                _logger.LogError(LoggerDefaultResponse.FailedUpdate);
                return false;
            }

            entity.Nature = nature;
            _context.Entry(entity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCost(int id, decimal cost)
        {
            var entity = await Get(id);
            if (entity == null)
            {
                _logger.LogError(LoggerDefaultResponse.FailedUpdate);
                return false;
            }

            entity.Cost = cost;
            _context.Entry(entity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateImage(int id, string image)
        {
            var entity = await Get(id);
            if (entity == null)
            {
                _logger.LogError(LoggerDefaultResponse.FailedUpdate);
                return false;
            }

            entity.ImagePath = image;
            _context.Entry(entity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = Get(id);
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
