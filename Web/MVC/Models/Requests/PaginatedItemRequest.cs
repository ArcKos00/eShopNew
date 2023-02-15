namespace MVC.Dtos
{
    public class PaginatedItemRequest<T>
        where T : notnull
    {
        [Range(0, double.MaxValue)]
        public int PageIndex { get; set; }

        [Range(0, double.MaxValue)]
        public int PageSize { get; set; }

        public Dictionary<T, int>? Filter { get; set; }
    }
}
