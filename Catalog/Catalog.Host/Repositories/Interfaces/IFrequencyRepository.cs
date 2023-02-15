using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface IFrequencyRepository
    {
        public Task<int?> Add(string meet);
        public Task<FrequencyEntity?> Get(int id);
        public Task<IEnumerable<FrequencyEntity>?> GetMeets();
        public Task<bool> UpdateMeet(int id, string name);
        public Task<bool> Delete(int id);
    }
}
