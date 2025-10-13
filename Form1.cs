using CSharpAppPlayground.Classes;
using CSharpAppPlayground.Classes.DataGen.Generators;
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

        //public void updateTextBox(string msg)
        //{
        //    if (InvokeRequired)
        //    {
        //        // If this is called from another thread compared to UI thread
        //        Debug.Print("InvokeRequired for updateTextBox().");
        //        Invoke(new Action<string>(updateTextBox), msg);
        //    }
        //    else
        //    {
        //        //textboxMain.Text += Environment.NewLine;
        //        textboxMain.AppendText(msg + Environment.NewLine);
        //    }
        //}

        //private void updateLabel(string msg)
        //{
        //    lblMain.Text = msg;
        //    lblMain.Refresh(); // Force the label to refresh immediately
        //                       // WITHOUT refresh the label might not redraw immediately
        //                       // GUI.Label doesn't update/redraw as aggressively as GUI.TextBox
        //    /// THREADING supported ways to call methods on the UI thread
        //    // Invoke((MethodInvoker)(() => updateLabel("running thread processing...")));
        //    // BeginInvoke((MethodInvoker) delegate() { updateLabel("running thread processing...") });
        //    // Invoke(new Action<string>(updateLabel), "running thread processing...");
        //}

        public Form1()
        {
            InitializeComponent();
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

        protected FormFactory _formUIs = new FormFactory("CSharpAppPlayground.FormUIs, CSharpAppPlayground");
        private void btnUIForm_Click(object sender, EventArgs e)
        {
            _formUIs.Open();
        }

        protected FormFactory _formConcurTask = new FormFactory("CSharpAppPlayground.FormConcurTask, CSharpAppPlayground");
        private void btnConcurTask_Click(object sender, EventArgs e)
        {
            _formConcurTask.Open();
        }

        protected FormFactory _formConcurThread = new FormFactory("CSharpAppPlayground.FormConcurThread, CSharpAppPlayground");
        private void btnConcurThread_Click(object sender, EventArgs e)
        {
            _formConcurThread.Open();
        }

        protected FormFactory _formConcurParallel = new FormFactory("CSharpAppPlayground.FormConcurParallel, CSharpAppPlayground");
        private void btnConcurParallel_Click(object sender, EventArgs e)
        {
            _formConcurParallel.Open();
        }

        protected FormFactory _formDBs = new FormFactory("CSharpAppPlayground.FormDBsMenu, CSharpAppPlayground");
        private void btnDBs_Click(object sender, EventArgs e)
        {
            _formDBs.Open();
        }

        TraceLogger tLogger = new TraceLogger();
        private void btnLogging_Click(object sender, EventArgs e)
        {
            // show examples from TraceLogger, GlobalLogger(ILogger) from .Loggers Namespace
            GlobalLogger.Instance.LogInformation("Hi From GlobalLogger<ILogger>");
            tLogger.Info("Hi From TraceLogger", "Form1.btnLogging_Click");
        }

        /// <summary>
        /// 
        /// This button is good for quick testing of data generation performance
        /// For any new data generation class, add a TestPerformance() method
        /// Then hook up here for quick test. 
        /// Change to new object when needed.
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDataGenQuickTest_Click(object sender, EventArgs e)
        {
            GenerateVidsSQL gen = new GenerateVidsSQL();
            gen.TestPerformance();
        }

        protected FormFactory _formFilesFolders = new FormFactory("CSharpAppPlayground.FilesFolders.FormFilesFolders, CSharpAppPlayground");
        private void btnFileIO_Click(object sender, EventArgs e)
        {
            _formFilesFolders.Open();
        }
    }
}
