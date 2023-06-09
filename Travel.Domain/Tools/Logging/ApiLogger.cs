﻿using Microsoft.Extensions.Logging;
using System.ComponentModel.Design;
using System.Runtime;

namespace Travel.Domain.Tools.Logging
{
    public class ApiLogger : IApiLogger
    {
        private readonly ILogger _logger;

        public ApiLogger(ILoggerFactory loggerFactory, string NameLogger)
        {
            _logger = loggerFactory.CreateLogger(NameLogger);
        }

        public void LoggingInformation(string message)
        {
            _logger.LogInformation(message);
        }

        public void LoggingWarning(string message)
        {
            _logger.LogWarning(message);
        }

        public void LoggingError(Exception ex, string message)
        {
            _logger.LogError(ex, message);
        }
    }

}
