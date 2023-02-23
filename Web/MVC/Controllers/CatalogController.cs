using MVC.Services.Interfaces;
using MVC.ViewModels.CatalogViewModels;
using MVC.ViewModels.Pagination;

namespace MVC.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        public async Task<IActionResult> Index(int? anomalyFilterApplied, int? typeFilterApplied, int? meetFilterApplied, int? page, int? itemsPage)
        {
            page ??= 0;
            itemsPage ??= 6;

             var catalog = await _catalogService.GetCatalogItems(
                page.Value,
                itemsPage.Value,
                anomalyFilterApplied,
                typeFilterApplied,
                meetFilterApplied);

            if (catalog == null)
            {
                return View("Error");
            }

            var info = new PaginationInfo()
            {
                ActualPage = page.Value,
                ItemsPerPage = itemsPage.Value,
                TotalItems = catalog.Count,
                TotalPages = (int)Math.Ceiling((decimal)catalog.Count / itemsPage.Value)
            };

            var vm = new IndexViewModel()
            {
                CatalogItems = catalog.Data,
                Anomalies = await _catalogService.GetAnomalies(),
                Types = await _catalogService.GetTypes(),
                Meets = await _catalogService.GetMeets(),
                PaginationInfo = info,
            };

            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : string.Empty;
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : string.Empty;

            return View(vm);
        }
    }
}