using System.Collections.Concurrent;
using System.Diagnostics;

namespace CSharpAppPlayground.Concurrency.ParallelExample
{
    /// <summary>
    /// This example uses Parallel to demonstrate parallel processing in C#.
    /// </summary>
    public class ParallelPauseCancel
    {
        private ManualResetEventSlim pauser;
        private CancellationTokenSource cancelToken;

        public ParallelPauseCancel()
        {
            // Constructor logic if needed
        }

        private void init()
        {
            pauser = new ManualResetEventSlim(true); // true = not paused
            cancelToken = new CancellationTokenSource();
        }

        public void Run()
        {
            init();

            ConcurrentBag<int> items = new ConcurrentBag<int>();
            for (int i = 1; i <= 20; i++)
                items.Add(i);

            Task.Run(() =>
            {
                Parallel.ForEach(items, new ParallelOptions
                {
                    MaxDegreeOfParallelism = 4,
                    CancellationToken = cancelToken.Token
                },
                (item) =>
                {
                    // Wait if paused
                    pauser.Wait(cancelToken.Token);

                    Debug.Print($"[{Task.CurrentId}] Processing item {item}");
                    Thread.Sleep(500); // Simulate work
                });
            });

            // Control loop
            while (true)
            {
                Debug.Print("\nCommands: p = pause, r = resume, s = stop");
                
                pauser.Reset();
                Debug.Print("Paused...");
                
                pauser.Set();
                Debug.Print("Resumed...");
                
                cancelToken.Cancel();
                Debug.Print("Stopping...");

                break;
            }
            Debug.Print("Main finished.");
        }

    }
}
