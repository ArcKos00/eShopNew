namespace Infrastructure.Extensions.Middleware
{
        public class RateMiddleware
        {
            private readonly RequestDelegate _next;

            public RateMiddleware(
                RequestDelegate next)
            {
                _next = next;
            }

            public long RateLimit { get; set; } = 10;

            public async Task InvokeAsync(HttpContext context)
            {
                var ip = context.Connection?.RemoteIpAddress?.ToString().Split("ff:")[1];
                var key = $"Ip:{ip}-Path:{context.Request.Path}";

                var connect = ConnectionMultiplexer.Connect(ConfigurationOptions.Parse("www.alevelwebsite.com:6380"));
                var database = connect.GetDatabase();
                var count = database.SetLength(key);

                if (count >= RateLimit)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                    return;
                }

                database.SetAdd(key, count);
                database.KeyExpire(key, TimeSpan.FromMinutes(1));

                await _next(context);
            }
        }
}
