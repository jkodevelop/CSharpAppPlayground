using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CSharpAppPlayground.Loggers
{
    public static class GlobalLogger
    {
        public static LogLevel currentLevel = LogLevel.Information; // Default log level
        public static ILogger Instance { get; private set; }

        public static void Initialize(bool useAppSettings=true, string? logPath="./logs/globallogs.txt")
        {
            if (Instance != null) { return; }

            // Load configuration from appsettings.json
            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //    .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"), optional: false, reloadOnChange: true)
            //    .Build();
            // string logLevelString = configuration["Logging:LogLevel:Default"] ?? "Information";

            try { 
                if (useAppSettings)
                {
                    string folderPath = Program.Configuration["Logging:LogFileSettings:Folder"] ?? "";
                    string fileName = Program.Configuration["Logging:LogFileSettings:GlobalLogFilename"] ?? "";
                    if (!string.IsNullOrWhiteSpace(folderPath) && !string.IsNullOrWhiteSpace(fileName))
                    {
                        logPath = Path.Combine(folderPath, fileName);
                    }
                }

                string logLevelString = Program.Configuration["Logging:LogLevel:Default"] ?? "Information";
                LogLevel logLevel = Enum.TryParse<LogLevel>(logLevelString, out LogLevel loglevel) ? loglevel : LogLevel.Information;

                Instance = new FileLogger("CSharpAppPlayground",
                    new FileLoggerOptions
                    {
                        FilePath = logPath,
                        LogLevel = logLevel
                    }
                );
            }
            catch (Exception ex)
            {
                Debug.Print($"Failed to create log file: {ex.Message}");
                throw new Exception("Failed to create log file, Path or Log Name issues");
            }
        }
    }
}