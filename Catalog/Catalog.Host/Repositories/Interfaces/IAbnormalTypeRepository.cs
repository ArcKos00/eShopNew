using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface IAbnormalTypeRepository
    {
        public Task<int?> Add(string name);
        public Task<AbnormalTypeEntity?> Get(int id);
        public Task<IEnumerable<AbnormalTypeEntity>?> GetTypes();
        public Task<bool> UpdateName(int id, string name);
        public Task<bool> Delete(int id);
    }
}
