using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Schedule.Shared.Middleware
{
    public class AppStatusMiddleware
    {
        private readonly RequestDelegate _next;

        public AppStatusMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context, ILogger<AppStatusMiddleware> logger)
        {
            logger.LogInformation($"Request started for username = {context.User?.Identity.Name}");
            logger.LogInformation(
                $"Http Request Information:{Environment.NewLine}" +
                $"Schema:{context.Request.Scheme} " +
                $"Host: {context.Request.Host} " +
                $"Path: {context.Request.Path} " +
                $"QueryString: {context.Request.QueryString}{Environment.NewLine}" +
                $"Full url = {context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}");

            return _next(context);
        }
    }
}
