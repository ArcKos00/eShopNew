using Catalog.Host.Data.Entities;
using Catalog.Host.Data;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface IArtefactRepository
    {
        public Task<int?> Add(string name, decimal cost, string img, string nature, int anomalyId, int abnormalTypeId, int frequencyId, int characteristicId);
        public Task<ArtefactEntity?> Get(int id);
        public Task<ArtefactEntity?> GetWithContent(int id);
        public Task<PaginatedItems<ArtefactEntity>> GetPage(int pageIndex, int pageSize, int? anomalyFilter, int? abnormalTypeFilter, int? meetFilter);
        public Task<bool> UpdateName(int id, string name);
        public Task<bool> UpdateNature(int id, string name);
        public Task<bool> UpdateCost(int id, decimal cost);
        public Task<bool> UpdateImage(int id, string image);
        public Task<bool> Delete(int id);
    }
}
