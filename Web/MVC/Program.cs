using MVC.Services;
using MVC.Services.Interfaces;
using MVC.ViewModels;
using Infrastructure.Extensions;

var configuration = GetConfig();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.AddConfiguration();

var identityUrl = configuration.GetValue<string>("IdentityUrl");
var callBackUrl = configuration.GetValue<string>("CallBackUrl");
var redirectUrl = configuration.GetValue<string>("RedirectUrl");
var sessionCookieLifetime = configuration.GetValue("SessionCookieLifetimeMinutes", 60);

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = OpenIdConnectDefaults.AuthenticationScheme;
    })
    .AddCookie(setup => setup.ExpireTimeSpan = TimeSpan.FromMinutes(sessionCookieLifetime))
    .AddOpenIdConnect(options =>
    {
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.Authority = identityUrl;
        options.Events.OnRedirectToIdentityProvider = async n =>
        {
            n.ProtocolMessage.RedirectUri = redirectUrl;
            await Task.FromResult(0);
        };
        options.SignedOutRedirectUri = callBackUrl;
        options.ClientId = "mvc_pkce";
        options.ClientSecret = "secret";
        options.ResponseType = "code";
        options.SaveTokens = true;
        options.GetClaimsFromUserInfoEndpoint = true;
        options.RequireHttpsMetadata = false;
        options.UsePkce = true;
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.Scope.Add("mvc");
        options.Scope.Add("catalog.api.catalogbff");
    });

builder.Services.Configure<AppSettings>(configuration);
builder.Services.AddAuthorization(configuration);

builder.Services.AddHttpClient();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IHttpClientService, HttpClientService>();
builder.Services.AddTransient<ICatalogService, CatalogService>();
builder.Services.AddTransient<IIdentityParser<ApplicationUser>, IdentityParcer>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("default", "{controller=Catalog}/{action=Index}/{id?}");
    endpoints.MapControllerRoute("defaultErrors", "{controller=Error}/{action=Error}");
    endpoints.MapControllers();
});

app.Run();

IConfiguration GetConfig()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

    return builder.Build();
}