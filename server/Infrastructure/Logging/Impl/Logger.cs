using IExternalLogger = NLog.ILogger;

namespace Infrastructure.Logging.Impl
{
    public class Logger : ILogger
    {
        private readonly IExternalLogger _logger;

        public Logger(IExternalLogger logger) => _logger = logger;

        public void LogDebug(string message) => _logger.Debug(message);

        public void LogError(string message) => _logger.Error(message);

        public void LogInfo(string message) => _logger.Info(message);

        public void LogWarn(string message) => _logger.Warn(message);
    }
}