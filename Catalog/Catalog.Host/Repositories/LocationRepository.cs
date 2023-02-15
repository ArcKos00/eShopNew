using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Infrastructure.Services.Interfaces;
using Infrastructure.CommonValues;

namespace Catalog.Host.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<LocationRepository> _logger;

        public LocationRepository(
            IDbContextWrapper<ApplicationDbContext> wrapper,
            ILogger<LocationRepository> logger)
        {
            _context = wrapper.DbContext;
            _logger = logger;
        }

        public async Task<int?> Add(string name)
        {
            var entity = await _context.Location.AddAsync(new LocationEntity()
            {
                Place = name
            });

            await _context.SaveChangesAsync();
            return entity.Entity.Id;
        }

        public async Task<LocationEntity?> Get(int id)
        {
            return await _context.Location.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<LocationEntity>?> GetLocations()
        {
            return await _context.Location
                .Distinct()
                .OrderBy(o => o.Place)
                .ToListAsync();
        }

        public async Task<bool> UpdatePlace(int id, string place)
        {
            var entity = await Get(id);
            if (entity == null)
            {
                _logger.LogError(LoggerDefaultResponse.FailedUpdate);
                return false;
            }

            entity.Place = place;
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
