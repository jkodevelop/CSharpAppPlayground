using System.Diagnostics;
using System.Threading;

namespace CSharpAppPlayground
{
    public partial class FormConcurThread : Form
    {
        public FormConcurThread()
        {
            InitializeComponent();
        }

        // invoking the main UI thread to do it if it is called from another thread
        // This is a workaround to avoid the error:
        // "Cross-thread operation not valid: Control 'tboxMain' accessed from a thread other than the thread it was created on."
        public void updateTextBoxMain(string msg)
        {
            if (InvokeRequired)
            {
                // If this is called from another thread compared to UI thread
                Debug.Print("InvokeRequired for updateTextBox().");
                Invoke(new Action<string>(updateTextBoxMain), msg);
            }
            else
            {
                //tboxMain.Text += Environment.NewLine;
                tboxMain.AppendText(msg + Environment.NewLine);
            }
        }

        public void updateLabelMain(string msg)
        {
            lblMain.Text = msg;
            lblMain.Refresh(); // Force the label to refresh immediately
            // WITHOUT refresh the label might not redraw immediately
            // GUI.Label doesn't update/redraw as aggressively as GUI.TextBox
        }

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

        private void threadProcess(int limit = 10)
        {
            // Invoke((MethodInvoker)(() => updateLabelMain("starting now...")));
            for (int i = 0; i < limit; i++)
            {
                string msg = $"Thread {Thread.CurrentThread.ManagedThreadId} - Count: {i}";
                updateTextBoxMain(msg);
                Thread.Sleep(500); // Simulate work, 200 milliseconds
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
                threadProcess(4);
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
    }
}
