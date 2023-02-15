using MVC.ViewModels;

namespace MVC.Services.Interfaces
{
    public interface ICatalogService
    {
        public Task<Catalog> GetCatalogItems(int page, int take, int? anomaly, int? type, int? meet);
        public Task<IEnumerable<SelectListItem>> GetAnomalies();
        public Task<IEnumerable<SelectListItem>> GetTypes();
        public Task<IEnumerable<SelectListItem>> GetMeets();
        }
}
