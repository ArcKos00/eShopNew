using MVC.Services.Interfaces;
using MVC.ViewModels;

namespace MVC.Controllers
{
    public class BasketController : Controller
    {
        private readonly ILogger<BasketController> _logger;
        private readonly IIdentityParser<ApplicationUser> _identityParser;
        private readonly IBasketService _service;

        public BasketController(
            ILogger<BasketController> logger,
            IIdentityParser<ApplicationUser> parcer,
            IBasketService service)
        {
            _logger = logger;
            _identityParser = parcer;
            _service = service;
        }


    }
}
