using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace CSharpAppPlayground.Loggers
{
    public static class GlobalLogger
    {
        public static LogLevel currentLevel = LogLevel.Information; // Default log level
        public static ILogger Instance { get; private set; }

        public static void Initialize(string? logPath = "globallogs.txt")
        {
            if (Instance != null) { return; }

            // Load configuration from appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"), optional: false, reloadOnChange: true)
                .Build();

            string logLevelString = configuration["Logging:LogLevel:Default"] ?? "Information";
            LogLevel logLevel = Enum.TryParse<LogLevel>(logLevelString, out LogLevel loglevel) ? loglevel : LogLevel.Information;
            
            Instance = new FileLogger( "CSharpAppPlayground",
                new FileLoggerOptions
                {
                    FilePath = logPath,
                    LogLevel = logLevel
                }
            );
        }
    }
}