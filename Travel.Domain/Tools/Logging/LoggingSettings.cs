namespace Travel.Domain.Tools.Logging
{
    public class LoggingSettings
    {
        public bool IsFileConsoleActivated { get; set; }
        public string FileConsoleLoggerName { get; set; } = string.Empty;
        public bool IsSeqActivated { get; set; }
        public string SeqLoggerName { get; set; } = string.Empty;
    }

}
