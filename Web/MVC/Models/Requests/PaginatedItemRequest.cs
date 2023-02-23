namespace MVC.Models.Requests
{
    public class PaginatedItemRequest<T> : PaginatedBaseRequest
        where T : notnull
    {
        public Dictionary<T, int>? Filter { get; set; }
    }
}
