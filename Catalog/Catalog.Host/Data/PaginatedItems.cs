namespace Catalog.Host.Data
{
    public class PaginatedItems<T>
    {
        public long TotalCount { get; set; }
        public List<T> Data { get; set; } = new List<T>();
    }
}
