using MVC.ViewModels.Pagination;

namespace MVC.ViewModels.CatalogViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<CatalogItem> CatalogItems { get; set; } = null!;
        public IEnumerable<SelectListItem> Anomalies { get; set; } = null!;
        public IEnumerable<SelectListItem> Types { get; set; } = null!;
        public IEnumerable<SelectListItem> Meets { get; set; } = null!;
        public int? AnomalyFilterApplied { get; set; }
        public int? TypeFilterApplied { get; set; }
        public int? MeetFilterApplied { get; set; }
        public PaginationInfo PaginationInfo { get; set; } = null!;
        public Basket Basket { get; set; } = null!;
        public IEnumerable<Order> Orders { get; set; } = null!;
    }
}
