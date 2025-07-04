using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CSharpAppPlayground.DIExample.advance
{
    public class ProcessOrderApp
    {
        private IHost host;
        public ProcessOrderApp()
        {
            host = Host.CreateDefaultBuilder().ConfigureServices((context, services) => {

                // setting up log for any ILogger constructor dependencies for DI
                /// 
                /// IMPORTANT: this log automatically resolves to T:type ILogger<ProcessOrderApp>
                /// SO any constructor that has ILogger object must use ILogger<ProcessOrderApp>, it needs to resolve to this
                /// DO NOT use generic ILogger in the other classes, it won't resolve correctly
                services.AddLogging(builder => {
                    builder.ClearProviders();

                    ///
                    /// The advantage of using IHostBuilder is default configuration is already set up for us
                    /// like appsettings.json is auto loaded if the project has this file
                    /// 
                    string _logLevel = context.Configuration["Logging:LogLevel:Default"] ?? "Information";
                    LogLevel logLevel;
                    if (!Enum.TryParse(_logLevel, out logLevel))
                        logLevel = LogLevel.Information;

                    var options = new FileLoggerOptions { FilePath = "processOrderlogs.txt", LogLevel = logLevel };
                    builder.AddProvider(new FileLoggerProvider(options)); ;
                });

                services.AddSingleton<ICreditCardValidator, FakeCardValidator>();
                services.AddTransient<IPaymentService, FakeCardPayment>();
                services.AddTransient<IShippingService, FakeShipping>();

                // All services that are registered in the DI container can be resolved by the IHost
                // we just need to make sure all Interfaces are added to the DI container
                services.AddTransient<ProcessOrderService>();

            }).Build();
        }

        public void Run()
        {
            ProcessOrderService proc = host.Services.GetRequiredService<ProcessOrderService>();
            proc.ProcessOrder("1234567890123456", 100.00m);
            proc.ProcessOrder("x234567890123456", 200.00m);
        }
    }
}
