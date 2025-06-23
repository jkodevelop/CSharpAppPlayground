using Microsoft.Extensions.Logging;

namespace CSharpAppPlayground.Loggers
{
    public static class GlobalLogger
    {
        public static readonly ILogger Instance = new FileLogger(
            "CSharpAppPlayground",
            new FileLoggerOptions
            {
                FilePath = "log.txt",
                LogLevel = LogLevel.Information
            }
        );
    }
}