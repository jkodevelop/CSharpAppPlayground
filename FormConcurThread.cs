using CSharpAppPlayground.Classes;
using System.Diagnostics;
using System.Threading;

namespace CSharpAppPlayground
{
    public partial class FormConcurThread : Form
    {
        public FormConcurThread()
        {
            InitializeComponent();
            startstopExampleInit();
        }

        // invoking the main UI thread to do it if it is called from another thread
        // This is a workaround to avoid the error:
        // "Cross-thread operation not valid: Control 'tboxMain' accessed from a thread other than the thread it was created on."
        public void updateTextBoxMain(string msg)
        {
            if (this.IsDisposed || this.Disposing)
            {
                // Form is disposed or disposing, do not attempt to update UI
                Debug.Print("Form is disposed or disposing, skipping updateTextBoxMain.");
                return;
            }
            if (InvokeRequired)
            {
                try
                {
                    Debug.Print("InvokeRequired for updateTextBox().");
                    Invoke(new Action<string>(updateTextBoxMain), msg);
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
                if (tboxMain != null && !tboxMain.IsDisposed)
                {
                    tboxMain.AppendText(msg + Environment.NewLine);
                }
            }
        }

        public void updateLabelMain(string msg)
        {
            lblMain.Text = msg;
            lblMain.Refresh(); // Force the label to refresh immediately
            // WITHOUT refresh the label might not redraw immediately
            // GUI.Label doesn't update/redraw as aggressively as GUI.TextBox
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

            updateTextBoxMain(resultsStr);
            updateTextBoxMain("running threads...");

            SimpleThreadExampleWithInvokeUsage();
        }

        private void threadSimpleProcess(int limit = 10)
        {
            // Invoke((MethodInvoker)(() => updateLabelMain("starting now...")));
            for (int i = 0; i < limit; i++)
            {
                string msg = $"Simple Example: Thread {Thread.CurrentThread.ManagedThreadId} - Count: {i}/{limit}";
                updateTextBoxMain(msg);
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

        // sources:
        // https://www.codeproject.com/Tips/5267935/Use-CancellationToken-not-Thread-Sleep
        // https://josipmisko.com/posts/c-sharp-stop-thread
        CancellationTokenSource tokenSource = new(); // Create a token source. shorthand: new CancellationTokenSource();
        bool threadStartStopRunning = false;
        private void startstopExampleInit(){
            // Register post-cancellation logic ONCE, method called from constructor
            // This is called when the token is cancelled
            if (tokenSource == null) {
                tokenSource = new CancellationTokenSource();
            }
            tokenSource.Token.Register(() =>
            {
                string msg = "Cancellation was requested. Performing cleanup...";
                if (this.InvokeRequired) {
                    this.Invoke(new Action(() => updateTextBoxMain(msg)));
                } else {
                    updateTextBoxMain(msg);
                }
                threadStartStopRunning = false;
            });
        }

        private void threadStartStopProcess(CancellationToken token, int limit = 10)
        {
            // Invoke((MethodInvoker)(() => updateLabelMain("starting now...")));
            for (int i = 0; i < limit && !token.IsCancellationRequested; i++)
            {
                string msg = $"Start/Stop Example: Thread {Thread.CurrentThread.ManagedThreadId} - Count: {i}/{limit}";
                updateTextBoxMain(msg);
                // Thread.Sleep(500); // Simulate work, 200 milliseconds
                bool cancellationTriggered = token.WaitHandle.WaitOne(500); // Wait for 500 milliseconds or until cancellation is requested
            }
        }

        private void btnMT02Start_Click(object sender, EventArgs e)
        {
            FormHelpers.FlipButtons(btnMT02Start, btnMT02Stop);
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
    }
}
