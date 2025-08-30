using CSharpAppPlayground.UIClasses;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace CSharpAppPlayground.Concurrency.ParallelExample
{
    /// <summary>
    /// This example uses Parallel to demonstrate parallel processing in C#.
    /// </summary>
    public class ParallelPauseCancel : UIFormRichTextBoxHelper
    {
        Button btnPause, btnStop;

        private ManualResetEventSlim pauser;
        private CancellationTokenSource cancelToken;

        int maxParallelism = 4;
        int totalItems = 15;
        int simulatedWorkTime = 500; // milliseconds

        bool isRunning = false;
        bool isPaused = false;

        public ParallelPauseCancel(Form _f, Button _btnPause, Button _btnStop)
        {
            // Constructor logic if needed
            f = _f;
            btnPause = _btnPause;
            btnStop = _btnStop;
            btnPause.Click += (sender, e) => PausePressed(sender);
            btnStop.Click += (sender, e) => StopPressed(sender);
        }

        public void ToggleButtons(bool reset)
        {
            if (f.InvokeRequired)
            {
                f.BeginInvoke(() =>
                {
                    btnPause.Enabled = !btnPause.Enabled;
                    btnStop.Enabled = !btnStop.Enabled;
                    if (reset)
                    {
                        btnPause.Text = "Pause";
                    }
                });
            }
            else
            {
                btnPause.Enabled = !btnPause.Enabled;
                btnStop.Enabled = !btnStop.Enabled;
            }
        }

        private void init()
        {
            pauser = new ManualResetEventSlim(true); // true = not paused
            cancelToken = new CancellationTokenSource();
            ToggleButtons(false);
        }

        public void Run()
        {
            if (isRunning)
            {
                Debug.Print("Already running, please wait...");
                return; 
            }

            isRunning = true;
            init();

            ConcurrentBag<int> items = new ConcurrentBag<int>();
            for (int i = 1; i <= totalItems; i++)
                items.Add(i);

            Task.Run(() =>
            {
                try
                {
                    Parallel.ForEach(items, new ParallelOptions
                    {
                        MaxDegreeOfParallelism = maxParallelism,
                        CancellationToken = cancelToken.Token
                    },
                    (item) =>
                    {
                        // Wait if paused
                        pauser.Wait(cancelToken.Token);

                        Debug.Print($"[{Task.CurrentId}] Processing item {item}, threadId: {Thread.CurrentThread.ManagedThreadId}");
                        Thread.Sleep(simulatedWorkTime); // Simulate work
                    });
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
                finally
                {
                    Debug.Print("Processing complete or cancelled.");
                    isRunning = false;
                    ToggleButtons(true);
                }
            });
        }

        protected void PausePressed(object sender)
        {
            if (isPaused)
            {
                pauser.Set();
                isPaused = false;
                btnPause.Text = "Pause";
            }
            else
            {
                pauser.Reset();
                isPaused = true;
                btnPause.Text = "Resume";
            }
        }

        protected void StopPressed(object sender)
        {
            cancelToken.Cancel();
            pauser.Set(); // In case it's paused
            isPaused = false;
            isRunning = false;
        }

    }
}
