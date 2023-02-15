using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Services.Interfaces
{
    public interface IAbnormalTypeService
    {
        public Task<int?> Add(string name);
        public Task<AbnormalType> Get(int id);
        public Task<List<AbnormalType>> GetTypes();
        public Task<bool> UpdateName(int id, string name);
        public Task<bool> Delete(int id);
    }
}
