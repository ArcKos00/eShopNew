using MVC.Services.Interfaces;
using MVC.ViewModels;

namespace MVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IIdentityParser<ApplicationUser> _identityParser;

        public AccountController(ILogger<AccountController> logger,
            IIdentityParser<ApplicationUser> parser)
        {
            _logger = logger;
            _identityParser = parser;
        }

        public IActionResult SignIn()
        {
            var user = _identityParser.Parse(User);

            _logger.LogInformation($"User {user.Name} authenticated");
            return RedirectToAction(nameof(CatalogController.Index), "Catalog");
        }

        public async Task<IActionResult> Signout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);

            var homeUrl = Url.Action(nameof(CatalogController.Index), "Catalog");

            return new SignOutResult(OpenIdConnectDefaults.AuthenticationScheme,
                new AuthenticationProperties { RedirectUri = homeUrl });
        }
    }
}
