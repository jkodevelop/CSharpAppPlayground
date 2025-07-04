using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace CSharpAppPlayground.DIExample.median
{
    public class DIExampleSampleApp
    {
        protected ServiceCollection Services { get; set; } 

        public DIExampleSampleApp()
        {
            Services = new ServiceCollection();
            // Services.AddTransient<FileLoggerOptions>(provider => new FileLoggerOptions { FilePath = "log.txt", LogLevel = LogLevel.Information });

            // Register the FileLoggerProvider and configure logging
            Services.AddLogging(builder =>
            {
                var options = new FileLoggerOptions { FilePath = "logs.txt", LogLevel = LogLevel.Information };
                builder.ClearProviders();
                builder.AddProvider(new FileLoggerProvider(options));
                // builder.SetMinimumLevel(options.LogLevel); // this is redundant, the FileLoggerProvider + FileLoggerOptions already done this
            });

            // Fix: Ensure TesterCaseAService implements ITesterService
            Services.AddTransient<ITesterService, TesterCaseAService>();
        }

        public void Run()
        {
            using ServiceProvider serviceProvider = Services.BuildServiceProvider();
            // TesterCaseAService testCase = serviceProvider.GetRequiredService<TesterCaseAService>();
            ITesterService testCase = serviceProvider.GetRequiredService<ITesterService>();
            testCase.RunTestA();
            testCase.RunTestB();
        }

    }
    
}
