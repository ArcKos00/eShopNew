using Infrastructure.Extensions.Middleware;

namespace Infrastructure.Extensions
{
    public static class RateExtension
    {
        public static void UseRateMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<RateMiddleware>();
        }
    }
}
