using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Services.Interfaces
{
    public interface IAnomalyService
    {
        public Task<int?> Add(string name, int abnormalTypeId, int locationPlaceId, int frequenceId);
        public Task<Anomaly> Get(int id);
        public Task<List<Anomaly>> GetAnomaly();
        public Task<bool> UpdateName(int id, string name);
        public Task<bool> Delete(int id);
    }
}
