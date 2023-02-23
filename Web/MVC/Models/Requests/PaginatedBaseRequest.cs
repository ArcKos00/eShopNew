namespace MVC.Models.Requests
{
    public class PaginatedBaseRequest
    {
        [Range(0, double.MaxValue)]
        public int PageIndex { get; set; }

        [Range(0, double.MaxValue)]
        public int PageSize { get; set; }
    }
}
