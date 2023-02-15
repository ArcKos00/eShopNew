namespace Catalog.Host.Models.Response
{
    public class PaginatedItemsResponse<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public long Count { get; set; }
        public List<T> Data { get; set; } = new List<T>();
    }
}
