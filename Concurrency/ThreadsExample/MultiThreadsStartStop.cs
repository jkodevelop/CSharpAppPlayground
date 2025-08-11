using CSharpAppPlayground.Classes;
using CSharpAppPlayground.UIClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAppPlayground.Concurrency.ThreadsExample
{
    /// <summary>
    /// Example with CancellationTokenSource and CancellationToken. Shows how to start and stop a thread
    /// </summary>
    // sources:
    // https://www.codeproject.com/Tips/5267935/Use-CancellationToken-not-Thread-Sleep
    // https://josipmisko.com/posts/c-sharp-stop-thread
    // CancellationTokenSource tokenSource = new(); // Create a token source. shorthand: new CancellationTokenSource();
    public class MultiThreadsStartStop
    {
        CancellationTokenSource tokenSource;
        bool threadStartStopRunning = false;

        protected Form f;

        public MultiThreadsStartStop(Form _f)
        {
            f = _f;
        }

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
                (f as FormWithRichText).updateRichTextBoxMain(msg);
                threadStartStopRunning = false;
            });
        }

        private void threadStartStopProcess(CancellationToken token, int limit = 10)
        {
            // Invoke((MethodInvoker)(() => updateLabelMain("starting now...")));
            for (int i = 1; i <= limit && !token.IsCancellationRequested; i++)
            {
                string msg = $"Start/Stop Example: Thread {Thread.CurrentThread.ManagedThreadId} - Count: {i}/{limit}";
                (f as FormWithRichText).updateRichTextBoxMain(msg);
                bool cancellationTriggered = token.WaitHandle.WaitOne(500); // Wait for 500 milliseconds or until cancellation is requested
            }
            if (!token.IsCancellationRequested)
            {
                // the thread completed without cancellation
                (f as FormConcurThread).ExampleStopped();
            }
        }

        public void Run()
        {
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

        public void Stop()
        {
            tokenSource.Cancel(); // Request cancellation of the token
        }
    }
}
