using Catalog.Host.Models.Enums;

namespace Catalog.Host.Models.Request
{
    public class PaginatedItemRequest
    {
        [Range(0, double.MaxValue)]
        public int PageIndex { get; set; }

        [Range(0, double.MaxValue)]
        public int PageSize { get; set; }

        public Dictionary<TypeFilter, int>? Filter { get; set; }
    }
}
