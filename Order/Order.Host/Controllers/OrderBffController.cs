using Order.Host.Models.Dto;
using Order.Host.Models.Request;
using Order.Host.Models.Request.Add;
using Order.Host.Models.Request.Update;
using Order.Host.Models.Response;

namespace Order.Host.Controllers
{
    [ApiController]
    [Scope("order.orderbff.api")]
    [Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
    [Route(ComponentDefaults.DefaultRoute)]
    public class OrderBffController : ControllerBase
    {
        private readonly ILogger<OrderBffController> _logger;
        private readonly IOrderService _service;

        public OrderBffController(
            ILogger<OrderBffController> logger,
            IOrderService orderService)
        {
            _logger = logger;
            _service = orderService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserOrders<Orders>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUserOrders(PaginatedUserOrdersRequest request)
        {
            var result = await _service.GetUserOrders(request.UserId, request.PageIndex, request.PageSize);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserOrders<Orders>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateStatus(UpdateStatusRequest request)
        {
            var result = await _service.UpdateStatus(request.Id, request.Status);
            return Ok(result);
        }
    }
}
