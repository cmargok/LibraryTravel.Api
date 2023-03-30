using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;
using Travel.Domain.Enums;
using Travel.Domain.Tools;
using System.Text.Json;
using Travel.Domain.Tools.Logging;

namespace Travel.Api.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IApiLogger _logger;
        public ExceptionHandlerMiddleware(RequestDelegate next, IApiLogger logger)
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
            catch (Exception ex)
            {
                _logger.LoggingError(ex, ex.Message);
                _logger.LoggingInformation($"{ex.Source} - ERROR");                
                await HandleExceptionAsync(context, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception error)
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var problem = new Problem()
            {
                DataCount = 0,
                Result = Result.InternalServerError.GetDescription(),
                Source = error.Source!,
                StatusCode = 500,
                Message = error.Message
            };

            if(error.InnerException != null)
            {
                problem.Source = error.InnerException.Source!;
            }

            var json = JsonSerializer.Serialize(problem);
            return context.Response.WriteAsync(json);
        }



        private class Problem : TailMessage
        {
            public int StatusCode { get; set; } = 500;
            public string Source { get; set; }
            public string Message { get; set; }
        }
    }

  
}
