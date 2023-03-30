namespace Travel.Domain.Tools.Logging
{
    public interface IApiLogger
    {
        public void LoggingWarning(string message);
        public void LoggingInformation(string message);
        public void LoggingError(Exception ex, string message);
    }
}