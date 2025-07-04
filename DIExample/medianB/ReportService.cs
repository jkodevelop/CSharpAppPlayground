using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAppPlayground.DIExample.medianB
{
    public class ReportService
    {
        private readonly IDataSource _dataSource;
        private readonly IReportSummary _reportSummary;
        public ReportService(IDataSource dataSource, IReportSummary reportSummary)
        {
            _dataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
            _reportSummary = reportSummary ?? throw new ArgumentNullException(nameof(reportSummary));
        }
        public void CreateReport()
        {
            string[] data = _dataSource.ReadData();
            string summary = _reportSummary.GenerateSummary(data);
            Debug.Print("Generating Report .............................");
            Debug.Print(summary);
        }
    }
}
