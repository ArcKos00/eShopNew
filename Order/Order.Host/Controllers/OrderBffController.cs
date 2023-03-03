using Order.Host.Models.Dto;
using Order.Host.Models.Request.Update;
using Order.Host.Models.Response;

namespace Order.Host.Controllers
{
    [ApiController]
    [AllowAnonymous]
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
        public async Task<IActionResult> GetUserOrders(PaginatedRequest request)
        {
            var userSub = User.Claims.FirstOrDefault(f => f.Type == "sub")?.Value;
            var result = await _service.GetUserOrders(userSub!, request.PageIndex, request.PageSize);
            return Ok(result);
        }
    }
}
