using Travel.Domain.Tools.Logging;

namespace Travel.Api.Middlewares
{
    public class OperationCanceledMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IApiLogger _logger;
        public OperationCanceledMiddleware(RequestDelegate next, IApiLogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (OperationCanceledException)
            {
                _logger.LoggingInformation("Request was cancelled - 409");
                context.Response.StatusCode = 409;
            }
        }
    }
}
