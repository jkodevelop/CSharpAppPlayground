using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CSharpAppPlayground.DIExample.medianB
{
    public class ReportApp
    {
        private readonly ReportService _reportService;
        public ReportApp()
        {
            IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // --- Register our services ---
                    // Here we choose which implementation to use for each interface.

                    // Example 1: Use CSV data to Generate Report.
                    services.AddSingleton<IDataSource, CSVDataSource>();
                    // services.AddSingleton<IDataSource, DBDataSource>();

                    // Example 2: Use DocReportSummary with (parameters) to Generate Report Summary.
                    services.AddSingleton<IReportSummary>(provider => new DocReportSummary("THE_report.txt")); ;

                    // Register the main application class. The DI container will see it needs
                    // the interfaces above and inject them automatically.
                    //
                    /// Explanation:
                    /// in this example, ReportService(IDataSource dataSource, IReportSummary reportSummary) constructor
                    /// The services.AddTransient<ReportService>() line registers the CSVDataSource/DocReportSummary
                    services.AddTransient<ReportService>();
                })
                .Build();
            _reportService = host.Services.GetRequiredService<ReportService>();
        }
        public void Run()
        {
            Debug.Print("ReportApp.Run called.");
            _reportService.CreateReport();
        }
    }
}
