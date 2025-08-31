using CSharpAppPlayground.Classes;
using CSharpAppPlayground.UIClasses;
using MethodTimer;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace CSharpAppPlayground.Concurrency.ParallelExample
{
    public class ParallelWithPause : UIFormRichTextBoxHelper
    {

        Button btnPause, btnStop;

        bool isRunning = false;
        bool isPaused = false;
        CancellationTokenSource cancellationTokenSource;
        ManualResetEvent pauseEvent = new ManualResetEvent(true);

        int maxParallelism = 10;
        int totalTasks = 10;
        int secondPerTask = 5; // seconds

        public ParallelWithPause(Form _f, Button _btnPause, Button _btnStop)
        {
            f = _f;
            btnPause = _btnPause;
            btnStop = _btnStop;
            btnPause.Click += (sender, e) => PausePressed(sender);
            btnStop.Click += (sender, e) => StopPressed(sender);
        }

        public void ToggleButtons(bool reset)
        {
            FormHelpers.ToggleButtonsPauseAndStop(reset, f, btnPause, btnStop);
        }

        public void Run()
        {
            if (isRunning)
            {
                Label("Already running, please wait...", true);
                return;
            }

            isRunning = true;
            isPaused = false;

            ToggleButtons(false);
            btnPause.Text = "Pause";
            
            cancellationTokenSource = new CancellationTokenSource();
            pauseEvent.Set(); // Allow execution to proceed
            
            string result = string.Empty;
            ConcurrentBag<string> results = new ConcurrentBag<string>();

            // Run the parallel operation in a separate task so we can control it
            Task.Run(() => RunParallelOperation(results, cancellationTokenSource.Token));

        }

        [Time("RunParallelOperation::")]
        private void RunParallelOperation(ConcurrentBag<string> results, CancellationToken cancellationToken)
        {
            try
            {
                ParallelOptions parallelOptions = new ParallelOptions 
                { 
                    MaxDegreeOfParallelism = maxParallelism,
                    CancellationToken = cancellationToken
                };

                // Example usage with Parallel.For and pause functionality
                Parallel.For(0, totalTasks, parallelOptions, i =>
                {
                    // Check for cancellation
                    cancellationToken.ThrowIfCancellationRequested();

                    string line = $"Task {i} started on thread {Thread.CurrentThread.ManagedThreadId}";
                    RichTextbox(line);
                    results.Add(line);

                    // Simulate work with pause checks, j < 5 = 5 seconds because of Thread.Sleep(1000) = 1 second x 5
                    for (int j = 0; j < secondPerTask; j++)
                    {
                        // Check for cancellation
                        cancellationToken.ThrowIfCancellationRequested();
                        
                        // Wait if paused
                        pauseEvent.WaitOne();
                        
                        Thread.Sleep(1000); // Simulate some work
                    }

                    line = $"Task {i} completed on thread {Thread.CurrentThread.ManagedThreadId}";
                    RichTextbox(line);
                    results.Add(line);
                });

                // Update UI on completion
                
                string result = string.Join(Environment.NewLine, results);
                Debug.Print(result);
                Label("Processing complete.");
                isRunning = false;
            }
            catch (OperationCanceledException)
            {
                RichTextbox("Operation was cancelled.");
                Label("Processing cancelled.");
                isRunning = false;
            }
            catch (Exception ex)
            {
                RichTextbox($"Error occurred: {ex.Message}");
                Label("Processing failed.");
                isRunning = false;
            }
            ToggleButtons(true);
            cancellationTokenSource.Dispose();
        }

        protected void PausePressed(object sender)
        {
            if (isPaused)
            {
                Resume();
                btnPause.Text = "Pause";
            }
            else
            {
                Pause();
                btnPause.Text = "Resume";
            }
        }   

        protected void Pause()
        {
            if (isRunning && !isPaused)
            {
                isPaused = true;
                pauseEvent.Reset(); // Block execution
                Label("Processing paused...", true);
            }
        }

        protected void Resume()
        {
            if (isRunning && isPaused)
            {
                isPaused = false;
                pauseEvent.Set(); // Allow execution to continue
                Label("Processing resumed...", true);
            }
        }

        protected void StopPressed(object sender)
        {
            Stop();
        }

        protected void Stop()
        {
            if (isRunning)
            {
                cancellationTokenSource?.Cancel();
                pauseEvent.Set(); // Unblock any waiting threads
                Label("Stopping processing...", true);
            }
        }

        public bool IsRunning => isRunning;
        public bool IsPaused => isPaused;
    }
}
