using Travel.Domain.Tools.Logging;

namespace Travel.Api.Middlewares
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IApiLogger _logger;
        public RequestMiddleware(RequestDelegate next, IApiLogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LoggingInformation($"Received {context.Request.Method} request for {context.Request.Path}");
            await _next(context);
          
        }
    }
}
