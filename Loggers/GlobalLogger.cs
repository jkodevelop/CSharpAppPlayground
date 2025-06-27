using Microsoft.Extensions.Logging;

namespace CSharpAppPlayground.Loggers
{
    public static class GlobalLogger
    {
        public static LogLevel currentLevel = LogLevel.Information; // Default log level
        public static ILogger Instance { get; private set; }

        public static void Initialize(LogLevel logLevel)
        {
            if (Instance != null && currentLevel.Equals(logLevel)) { return; }   
            Instance = new FileLogger( "CSharpAppPlayground",
                new FileLoggerOptions
                {
                    FilePath = "log.txt",
                    LogLevel = logLevel
                }
            );
        }
    }
}