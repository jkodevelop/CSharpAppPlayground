using CSharpAppPlayground.Classes;
using CSharpAppPlayground.DIExample.advance;
using CSharpAppPlayground.DIExample.median;
using CSharpAppPlayground.DIExample.medianB;
using CSharpAppPlayground.GenericTypeExample;
using CSharpAppPlayground.Loggers;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CSharpAppPlayground
{
    public partial class Form1 : Form
    {

        private Foo f = new Foo("Hello From Bar();");
        public Form1()
        {
            InitializeComponent();
            // ILogger logger = new FileLogger("CSharpAppPlayground", new FileLoggerOptions { FilePath = "log.txt", LogLevel = LogLevel.Information });
            // logger.LogInformation("Application started.");
            GlobalLogger.Instance.LogInformation("Application started.");
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            //Debug.Print("Hello, World!"); // outputs to console, not suitable for WinForms
            Debug.Print("Hello, World!"); // outputs to the Output window in Visual Studio

            GlobalLogger.Instance.LogInformation("Run Clicked");
        }

        private void btnFoo_Click(object sender, EventArgs e)
        {
            f.PrintBar("The Bar.");
        }

        private void btnDI_Click(object sender, EventArgs e)
        {
            DIExampleSampleApp dieExample = new DIExampleSampleApp();
            dieExample.Run();

            ReportApp reportApp = new ReportApp();
            reportApp.Run();

            ProcessOrderApp processOrderApp = new ProcessOrderApp();
            processOrderApp.Run();
        }

        private void btnGeneric_Click(object sender, EventArgs e)
        {
            GenericsDemo.Show();
            GenericsReturnDemo.Show();
        }
    }
}
