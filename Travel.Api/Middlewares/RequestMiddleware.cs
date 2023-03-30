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
            var traceId = context.TraceIdentifier;
            if (string.IsNullOrEmpty(traceId))
            {
                traceId = Guid.NewGuid().ToString();
                context.TraceIdentifier = traceId;
            }
            _logger.LoggingInformation($"Received {context.Request.Method} request for {context.Request.Path} - Trace ID: {traceId}");
            await _next(context);
          
        }
    }
}
