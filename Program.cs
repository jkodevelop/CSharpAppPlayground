using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CSharpAppPlayground
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"), optional: false, reloadOnChange: true)
                .Build();

            string logLevelString = configuration["Logging:LogLevel:Default"] ?? "Information";
            LogLevel logLevel = Enum.TryParse<LogLevel>(logLevelString, out LogLevel loglevel) ? loglevel : LogLevel.Information;

            // Pass logLevel to your FileLogger
            CSharpAppPlayground.Loggers.GlobalLogger.Initialize(logLevel);
            Application.Run(new Form1());
        }
    }
}