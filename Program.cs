using CSharpAppPlayground.Classes.AppSettings;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace CSharpAppPlayground
{
    internal static class Program
    {
        public static IConfiguration Configuration { get; private set; }
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // alternative example to set up configuration
            // Load configuration from appsettings.json
            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //    .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"), optional: false, reloadOnChange: true)
            //    .Build();
            // string logLevelString = configuration["Logging:LogLevel:Default"] ?? "Information";

            // Build configuration
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // example: basic
            string logLevelDefault = Configuration["Logging:LogLevel:Default"] ?? "Information";

            // example: extracting appsettings using custom type class
            ExampleSettings exSettings = Configuration.GetSection("ExampleSettings").Get<ExampleSettings>();

            // example: getting ConnectionString, this is built-in, same as .GetSection("ConnectionString")
            string dbStrSettings = Configuration.GetConnectionString("MySqlConnection");

            // example: deeper hierarchy example 
            LoggingSettingsMap logSettingsMap = Configuration.GetSection("Logging").Get<LoggingSettingsMap>();
            LogSettings logSettings = Configuration.GetSection("Logging").Get<LogSettings>();


            Debug.Print($@"
            ExampleSettings.ExampleString: {exSettings.ExampleString}
            ExampleSettings.ExampleInt: {exSettings.ExampleInt}
            ConnectionStrings.Str: {dbStrSettings}
            Logging.LogLevel.Default: {logLevelDefault}
            Logging.LogLevel.Microsoft: {logSettingsMap.LogLevel["Microsoft"]}
            Logging.LogLevel.Crit: {logSettings.LogLevel.CritOnly}
            ");

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Create a logger instance
            Loggers.GlobalLogger.Initialize("globallogs.txt");

            Application.Run(new Form1());
        }
    }
}