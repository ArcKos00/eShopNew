using Basket.Host.Models.Basket;
using Basket.Host.Models.Request;
using Basket.Host.Services.Interfaces;

namespace Basket.Host.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route(ComponentDefaults.DefaultRoute)]
    [Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
    public class BasketBffController : ControllerBase
    {
        private readonly IBasketService _service;
        private readonly ILogger<BasketBffController> _logger;

        public BasketBffController(
            IBasketService service,
            ILogger<BasketBffController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddToBasket(WithItemRequest request)
        {
            var userSub = User.Claims.FirstOrDefault(f => f.Type == "sub")?.Value ?? "babka";
            _logger.LogInformation(userSub);
            var result = await _service.AddToBasket(userSub!, request.Id, request.Name, request.Cost);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RemoveFromBasket(WithItemIdRequest request)
        {
            var userSub = User.Claims.FirstOrDefault(f => f.Type == "sub")?.Value ?? "babka";
            _logger.LogInformation(userSub);
            var result = await _service.RemoveFromBasket(userSub!, request.Id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> MakeAnOrder()
        {
            var userSub = User.Claims.FirstOrDefault(f => f.Type == "sub")?.Value ?? "babka";
            _logger.LogInformation(userSub);
            var result = await _service.MakeAnOrder(userSub!);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(BasketModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBasket()
        {
            var userSub = User.Claims.FirstOrDefault(f => f.Type == "sub")?.Value ?? "babka";
            _logger.LogInformation(userSub);
            var result = await _service.GetBasket(userSub!);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ClearBasket()
        {
            var userSub = User.Claims.FirstOrDefault(f => f.Type == "sub")?.Value ?? "babka";
            _logger.LogInformation(userSub);
            var result = await _service.Clear(userSub!);
            return Ok(result);
        }
    }
}
