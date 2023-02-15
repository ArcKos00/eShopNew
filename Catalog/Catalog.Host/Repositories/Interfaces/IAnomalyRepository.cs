using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface IAnomalyRepository
    {
        public Task<int?> Add(string name, int abnormalTypeId, int locationPlaceId, int frequenceId);
        public Task<AnomalyEntity?> Get(int id);
        public Task<IEnumerable<AnomalyEntity>?> GetAnomaly();
        public Task<bool> UpdateName(int id, string name);
        public Task<bool> Delete(int id);
    }
}
