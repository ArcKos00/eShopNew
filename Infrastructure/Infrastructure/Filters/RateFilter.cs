namespace Infrastructure.Filters
{
    public class RateFilter : ActionFilterAttribute
    {
        public long RateLimit { get; set; } = 10;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var ip = context.HttpContext.Connection?.RemoteIpAddress?.ToString().Split("ff:")[1];
            var key = $"Ip:{ip}-Path:{context.HttpContext.Request.Path}";

            var connect = ConnectionMultiplexer.Connect(ConfigurationOptions.Parse("www.alevelwebsite.com:6380"));
            var database = connect.GetDatabase();

            var count = database.SetLength(key);
            if (count >= RateLimit)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                context.Result = new ContentResult()
                {
                    Content = "Too Many Request",
                    StatusCode = (int)HttpStatusCode.TooManyRequests
                };
                return;
            }

            Console.WriteLine(count);
            database.SetAdd(key, count);
            database.KeyExpire(key, TimeSpan.FromMinutes(1));
        }
    }
}
