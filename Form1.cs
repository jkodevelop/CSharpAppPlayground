using CSharpAppPlayground.Classes;
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
            //Console.WriteLine("Hello, World!"); // outputs to console, not suitable for WinForms
            Debug.Print("Hello, World!"); // outputs to the Output window in Visual Studio

            GlobalLogger.Instance.LogInformation("Run Clicked");
        }

        private void btnFoo_Click(object sender, EventArgs e)
        {
            f.PrintBar("The Bar.");
        }
    }
}
