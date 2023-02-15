using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Services.Interfaces
{
    public interface ILocationService
    {
        public Task<int?> Add(string name);
        public Task<Location> Get(int id);
        public Task<List<Location>> GetLocations();
        public Task<bool> UpdatePlace(int id, string place);
        public Task<bool> Delete(int id);
    }
}
