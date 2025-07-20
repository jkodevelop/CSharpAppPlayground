using CSharpAppPlayground;
using CSharpAppPlayground.Classes;
using CSharpAppPlayground.DIExample.advance;
using CSharpAppPlayground.DIExample.median;
using CSharpAppPlayground.DIExample.medianB;
using CSharpAppPlayground.GenericTypeExample;
using CSharpAppPlayground.Multithreading.ThreadsExample;
using CSharpAppPlayground.Loggers;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using CSharpAppPlayground.Multithreading.ParallelExample;
using CSharpAppPlayground.Multithreading.TasksExample;
using System.Threading.Tasks;

namespace CSharpAppPlayground
{
    public partial class Form1 : Form
    {
        private Foo f = new Foo("Hello From Bar();");

        // Multi-threading examples
        protected MutiThreadsExample mte;
        protected ParallelExample2 pe;
        protected TasksExample te;

        public void updateTextBox(string msg)
        {
            if (InvokeRequired)
            {
                // If this is called from another thread compared to UI thread
                Debug.Print("InvokeRequired for updateTextBox().");
                Invoke(new Action<string>(updateTextBox), msg);
            }
            else
            {
                //textboxMain.Text += Environment.NewLine;
                textboxMain.AppendText(msg + Environment.NewLine);
            }
        }

        private void updateLabel(string msg)
        {
            lblMain.Text = msg;
            lblMain.Refresh(); // Force the label to refresh immediately
                               // WITHOUT refresh the label might not redraw immediately
                               // GUI.Label doesn't update/redraw as aggressively as GUI.TextBox
            /// THREADING supported ways to call methods on the UI thread
            // Invoke((MethodInvoker)(() => updateLabel("running thread processing...")));
            // BeginInvoke((MethodInvoker) delegate() { updateLabel("running thread processing...") });
            // Invoke(new Action<string>(updateLabel), "running thread processing...");
        }

        public Form1()
        {
            InitializeComponent();
            GlobalLogger.Instance.LogInformation("Application started.");

            // multithreading examples
            mte = new MutiThreadsExample(this);
            pe = new ParallelExample2();
            te = new TasksExample();
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

        private void btnThreads_Click(object sender, EventArgs e)
        {
            int processors = 1;
            string processorsStr = System.Environment.GetEnvironmentVariable("NUMBER_OF_PROCESSORS") ?? "1"; // alt: Environment.ProcessorCount.ToString();
            if (processorsStr != null)
                processors = int.Parse(processorsStr);
            string resultsStr = $"Number of processors: {processors}";
            Debug.Print(resultsStr);
            lblMain.Text = resultsStr;

            updateTextBox(resultsStr);
            updateTextBox("running threads...");

            SimpleThreadExampleWithInvokeUsage();
        }

        ////////////////////////////////////////////////////////////////////////////////////////
        // MULTI-THREADING examples
        ////////////////////////////////////////////////////////////////////////////////////////
        private void threadProcess(int limit = 10)
        {
            Invoke((MethodInvoker)(() => updateLabel("starting now...")));
            for (int i = 0; i < limit; i++)
            {
                string msg = $"Thread {System.Threading.Thread.CurrentThread.ManagedThreadId} - Count: {i}";
                updateTextBox(msg);
                System.Threading.Thread.Sleep(500); // Simulate work, 200 milliseconds
            }
        }

        bool simpleThreadRunning = false;
        /// <summary>
        /// This is simple example showing a simple threading implementation
        /// </summary>
        private void SimpleThreadExampleWithInvokeUsage()
        {
            if (simpleThreadRunning)
            {
                Debug.Print("!! simple thread example already running right now !!");
                return;
            }

            // ThreadStart() defines what to run
            ThreadStart simpleThreadStart = new ThreadStart(() =>
            {
                simpleThreadRunning = true;
                threadProcess(4);
                simpleThreadRunning = false;
            });

            /// option 1: run the process defined in same/main thread
            simpleThreadStart.Invoke(); // This make the process run on the UI thread, blocking it until done

            /// option 2: run the process in a new thread
            Thread thread = new Thread(simpleThreadStart);
            thread.IsBackground = true; // Setting this true allows for application to exit even if it's running
                                        // all threads are foreground when created, IsBackground = true sends it to the background
            thread.Start();
        }

        private void btnStartThreads_Click(object sender, EventArgs e)
        {
            mte.Show();
        }

        private async void btnStartParallel_Click(object sender, EventArgs e)
        {
            //pe.Show();

            //pe.ShowAsync().ContinueWith(t =>
            //{
            //    if (t.IsFaulted)
            //    {
            //        Debug.Print($"Error: {t.Exception?.Message}");
            //        updateTextBox($"Error: {t.Exception?.Message}");
            //    }
            //    else
            //    {
            //        Debug.Print("Parallel2 processing completed successfully.");
            //        updateTextBox("Parallel2 processing completed successfully.");
            //    }
            //});

            await pe.ShowAsync();
        }

        private void btnStartTasks_Click(object sender, EventArgs e)
        {
            te.ShowAsync().ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    Debug.Print($"Error: {t.Exception?.Message}");
                    // updateTextBox($"Error: {t.Exception?.Message}");
                }
                else
                {
                    Debug.Print("All tasks completed successfully.");
                    // updateTextBox("All tasks completed successfully.");
                }
            });
        }

        private void btnNewView_Click(object sender, EventArgs e)
        {
            var form2 = new Form2();
            form2.ShowDialog();
        }

        private void btnSwitchPageConcurrency_Click(object sender, EventArgs e)
        {
            // Hide main controls except panelPage2
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl != panelPage2)
                {
                    ctrl.Visible = false;
                }
            }
            panelPage2.Visible = true;
        }

        private void btnBackFromPage2_Click(object sender, EventArgs e)
        {
            // Show main controls except panelPage2
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl != panelPage2)
                {
                    ctrl.Visible = true;
                }
            }
            panelPage2.Visible = false;
        }

        //// TO DOCUMENT
        private bool IsUIFormOpen = false;
        private void FormUIs_FormClosed(object sender, FormClosedEventArgs e)
        {
            IsUIFormOpen = false; // Optionally reset your flag here
        }
        private void btnUIForm_Click(object sender, EventArgs e)
        {
            // Create a new instance of FormUIs every time the button is clicked
            // Because Closing the form will dispose the form and we need a new instance
            // ALTERNATIVELY, you can keep a single instance of FormUIs and just hide it instead of closing it
            // BUT to do this, you need to subscribe to the FormClosing event and set e.Cancel = true; to prevent it from closing
            //
            // EXAMPLE:
            // private void FormUIs_FormClosing(object sender, FormClosingEventArgs e)
            // {
            //    e.Cancel = true;      // Cancel the close
            //    ((Form)sender).Hide(); // Hide the form instead
            // }
            //

            //Form formUIs = new FormUIs();
            FormUIs formUIs = (FormUIs)FormHelpers.GetInstance("CSharpAppPlayground.FormUIs"); // "CSharpAppPlayground.FormUIs, CSharpAppPlayground" <- this also works
            //FormUIs formUIs = (FormUIs)FormHelpers.GetInstance("FormUIs"); // does not work, need full namespace

            if (IsUIFormOpen)
            {
                Debug.Print("FormUIs is already open.");
                return; // Prevent opening the form again if it's already open
            }
            else
            {
                IsUIFormOpen = true; // Set the flag to true when opening the form
                formUIs.Show(); // this will NOT block MAIN Form, it will run in parallel with the main form
                // formUIs.ShowDialog(); // this will block MAIN Form until the form is closed
            }
            formUIs.FormClosed -= FormUIs_FormClosed; // Unsubscribe if already attached
            Delegate[] subscribers = FormHelpers.GetFormClosedSubscribers(formUIs);
            if (subscribers.Length > 0)
            {
                foreach (var subscriber in subscribers)
                {
                    Debug.Print($"Subscriber: {subscriber.Method.Name}");
                }
            }
            else
            {
                Debug.Print("no subs");
            }
            formUIs.FormClosed += FormUIs_FormClosed; // Subscribe
            subscribers = FormHelpers.GetFormClosedSubscribers(formUIs);
            if (subscribers.Length > 0)
            {
                foreach (var subscriber in subscribers)
                {
                    Debug.Print($"Checked again Subscriber: {subscriber.Method.Name}");
                }
            }
            else
            {
                Debug.Print("no subs");
            }
        }
       
    }
}
