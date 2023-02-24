using MVC.Services.Interfaces;
using MVC.ViewModels;

namespace MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _service;
        private readonly IIdentityParser<ApplicationUser> _parcer;

        public OrderController(IOrderService service,
            IIdentityParser<ApplicationUser> parcer)
        {
            _service = service;
            _parcer = parcer;
        }

        public async Task<IActionResult> Index(int? page, int? itemsOnPage)
        {
            page ??= 0;
            itemsOnPage ??= 50;
            var user = _parcer.Parse(User);

            var orders = await _service.GetOrders(user.Id, page, itemsOnPage);


        }
    }
}
