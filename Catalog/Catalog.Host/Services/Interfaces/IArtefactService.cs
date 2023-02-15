using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces
{
    public interface IArtefactService
    {
        public Task<int?> Add(string name, decimal cost, string img, string nature, int anomalyId, int abnormalTypeId, int frequencyId, int characteristicId);
        public Task<Artefact> Get(int id);
        public Task<Artefact> GetWithContent(int id);
        public Task<PaginatedItemsResponse<Artefact>> GetPage(int pageIndex, int pageSize, Dictionary<TypeFilter, int>? filter);
        public Task<bool> UpdateName(int id, string name);
        public Task<bool> UpdateNature(int id, string name);
        public Task<bool> UpdateCost(int id, decimal cost);
        public Task<bool> UpdateImage(int id, string image);
        public Task<bool> Delete(int id);
    }
}
