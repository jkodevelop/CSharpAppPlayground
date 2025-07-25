using CSharpAppPlayground.Classes;
using CSharpAppPlayground.Multithreading.ThreadsExample;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace CSharpAppPlayground
{
    // TO DOCUMENT: richtextbox needs to be force refreshed() compared to textbox,
    // textbox updates/redraws immediately, richtextbox doesn't => like the label component
    public partial class FormConcurThread : Form
    {
        public FormConcurThread()
        {
            InitializeComponent();
            MultiThreadExampleInit();
        }

        // invoking the main UI thread to do it if it is called from another thread
        // This is a workaround to avoid the error:
        // "Cross-thread operation not valid: Control 'richTBoxMain' accessed from a thread other than the thread it was created on."
        public void updateRichTextBoxMain(string msg, Color lineColor = default)
        {
            if (this.IsDisposed || this.Disposing)
            {
                // Form is disposed or disposing, do not attempt to update UI
                Debug.Print("Form is disposed or disposing, skipping updateRichTextBoxMain.");
                return;
            }
            if (InvokeRequired)
            {
                try
                {
                    Debug.Print("InvokeRequired for updateRichTextBoxMain().");
                    Invoke(new Action<string, Color>(updateRichTextBoxMain), msg, lineColor);
                }
                catch (ObjectDisposedException)
                {
                    Debug.Print("Invoke failed: Form is disposed.");
                }
                catch (InvalidOperationException)
                {
                    Debug.Print("Invoke failed: Form is disposed or handle is invalid.");
                }
            }
            else
            {
                if (richTBoxMain != null && !richTBoxMain.IsDisposed)
                {
                    richTBoxMain.SelectionColor = lineColor;
                    richTBoxMain.AppendText(msg + Environment.NewLine);
                    richTBoxMain.Refresh(); // Force the rich text box to refresh immediately
                }
            }
        }

        public void updateLabelMain(string msg)
        {
            if (this.IsDisposed || this.Disposing)
            {
                // Form is disposed or disposing, do not attempt to update UI
                Debug.Print("Form is disposed or disposing, skipping updateLabelMain.");
                return;
            }
            if (InvokeRequired)
            {
                try
                {
                    Debug.Print("InvokeRequired for updateLabelMain().");
                    Invoke(new Action<string>(updateLabelMain));
                }
                catch (ObjectDisposedException)
                {
                    Debug.Print("Invoke failed: Form is disposed.");
                }
                catch (InvalidOperationException)
                {
                    Debug.Print("Invoke failed: Form is disposed or handle is invalid.");
                }
            }
            else
            {
                if (lblMain != null && !lblMain.IsDisposed)
                {
                    lblMain.Text = msg;
                    lblMain.Refresh(); // Force the label to refresh immediately
                                       // WITHOUT refresh the label might not redraw immediately
                                       // GUI.Label doesn't update/redraw as aggressively as GUI.TextBox
                }
            }
        }

        /// <summary>
        /// Simple Thread Example showing blocking(no thread) and non-blocking(threaded) execution.
        /// Example usage of Invoke() / ThreadStart / Thread.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMTExample_Click(object sender, EventArgs e)
        {
            int processors = 1;
            string processorsStr = System.Environment.GetEnvironmentVariable("NUMBER_OF_PROCESSORS") ?? "1"; // alt: Environment.ProcessorCount.ToString();
            if (processorsStr != null)
                processors = int.Parse(processorsStr);
            string resultsStr = $"Number of processors: {processors}";
            Debug.Print(resultsStr);
            lblMain.Text = resultsStr;

            updateRichTextBoxMain(resultsStr);
            updateRichTextBoxMain("running threads...");

            SimpleThreadExampleWithInvokeUsage();
        }

        private void threadSimpleProcess(int limit = 10)
        {
            // Invoke((MethodInvoker)(() => updateLabelMain("starting now...")));
            for (int i = 1; i <= limit; i++)
            {
                string msg = $"Simple Example: Thread {Thread.CurrentThread.ManagedThreadId} - Count: {i}/{limit}";
                updateRichTextBoxMain(msg);
                Thread.Sleep(500); // Simulate work, 500 milliseconds
            }
        }

        bool simpleThreadRunning = false;
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
                threadSimpleProcess(4);
                simpleThreadRunning = false;
            });

            updateLabelMain("blocking thread started");
            /// option 1: run the process defined in same/main thread
            simpleThreadStart.Invoke(); // This make the process run on the UI thread, blocking it until done

            lblMain.Text = "non-blocking thread started";
            /// option 2: run the process in a new thread
            Thread thread = new Thread(simpleThreadStart);
            thread.IsBackground = true; // Setting this true allows for application to exit even if it's running
                                        // all threads are foreground when created, IsBackground = true sends it to the background
            thread.Start();
        }

        /// <summary>
        /// Example with CancellationTokenSource and CancellationToken. Shows how to start and stop a thread
        /// </summary>
        // sources:
        // https://www.codeproject.com/Tips/5267935/Use-CancellationToken-not-Thread-Sleep
        // https://josipmisko.com/posts/c-sharp-stop-thread
        // CancellationTokenSource tokenSource = new(); // Create a token source. shorthand: new CancellationTokenSource();
        CancellationTokenSource tokenSource;
        bool threadStartStopRunning = false;
        private void startstopExampleInitToken()
        {
            // -----------------------------------------------------------------------------
            // TO DOCUMENT, token usage + limitation of having to reset everytime
            // -----------------------------------------------------------------------------
            // Initialize the CancellationTokenSource, This needs to be done everytime
            // BECAUSE the once a token is cancelled, it cannot be reused.
            // SO we need to create a new CancellationTokenSource for each new operation.
            tokenSource = new CancellationTokenSource();

            // Register post-cancellation logic ONCE
            // This is called when the token is cancelled
            tokenSource.Token.Register(() =>
            {
                string msg = "Cancellation was requested. Performing cleanup...";
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => updateRichTextBoxMain(msg)));
                }
                else
                {
                    updateRichTextBoxMain(msg);
                }
                threadStartStopRunning = false;
            });
        }

        private void threadStartStopProcess(CancellationToken token, int limit = 10)
        {
            // Invoke((MethodInvoker)(() => updateLabelMain("starting now...")));
            for (int i = 1; i <= limit && !token.IsCancellationRequested; i++)
            {
                string msg = $"Start/Stop Example: Thread {Thread.CurrentThread.ManagedThreadId} - Count: {i}/{limit}";
                updateRichTextBoxMain(msg);
                bool cancellationTriggered = token.WaitHandle.WaitOne(500); // Wait for 500 milliseconds or until cancellation is requested
            }
        }

        private void btnMT02Start_Click(object sender, EventArgs e)
        {
            FormHelpers.FlipButtons(btnMT02Start, btnMT02Stop);
            startstopExampleInitToken();

            if (threadStartStopRunning)
            {
                Debug.Print("!! start/stop thread example already running right now !!");
                return;
            }
            ThreadStart startstopThreadStart = new ThreadStart(() =>
            {
                threadStartStopRunning = true;
                threadStartStopProcess(tokenSource.Token, 5);
                threadStartStopRunning = false;
            });
            Thread thread = new Thread(startstopThreadStart);
            thread.IsBackground = true;
            thread.Start();
        }

        private void btnMT02Stop_Click(object sender, EventArgs e)
        {
            FormHelpers.FlipButtons(btnMT02Start, btnMT02Stop);
            tokenSource.Cancel(); // Request cancellation of the token
        }

        /// <summary>
        /// Example of threads A and B running concurrently. Allow for pausing and resuming.
        /// Also shows the threads status
        /// </summary>
        protected MutiThreadsExample mte;
        private void MultiThreadExampleInit()
        {
            if (mte == null)
                mte = new MutiThreadsExample(this, btnThreadA, btnThreadB, btnMTMultiStatus, lblMain, richTBoxMain);
        }

        private void btnMTMultiStart_Click(object sender, EventArgs e)
        {
            mte.Run();
        }
    }
}
