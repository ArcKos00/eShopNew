using MVC.Services.Interfaces;
using MVC.Models.Requests;
using MVC.ViewModels;

namespace MVC.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOptions<AppSettings> _options;
        private readonly ILogger<OrderService> _logger;
        private readonly IHttpClientService _httpClient;

        public OrderService(
            IOptions<AppSettings> options,
            ILogger<OrderService> logger,
            IHttpClientService httpClient)
        {
            _options = options;
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task GetOrders(string userId, int? pageindex = null!, int? pageSize = null!)
        {
            pageindex = pageindex ?? 0;
            pageSize = pageSize ?? 50;

            var result = await _httpClient.SendAsync<UserOrder, PaginatedOrderRequest>(
                $"{_options.Value.OrderUrl}/getuserorders",
                HttpMethod.Post,
                new PaginatedOrderRequest()
                {
                    PageIndex = (int)pageindex,
                    PageSize = (int)pageSize,
                    UserId = userId
                });
        }
    }
}
