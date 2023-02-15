using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ILocationRepository
    {
        public Task<int?> Add(string name);
        public Task<LocationEntity?> Get(int id);
        public Task<IEnumerable<LocationEntity>?> GetLocations();
        public Task<bool> UpdatePlace(int id, string place);
        public Task<bool> Delete(int id);
    }
}
