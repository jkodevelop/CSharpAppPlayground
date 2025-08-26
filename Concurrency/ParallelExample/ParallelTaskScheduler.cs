using CSharpAppPlayground.UIClasses;
using MethodTimer;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace CSharpAppPlayground.Concurrency.ParallelExample
{
    public class ParallelTaskScheduler : UIFormRichTextBoxHelper
    {
        private List<string> items = new List<string> { "Item 1", "Item 2", "Item 3", "Item 4", "Item 5" };

        bool isRunning = false;

        public ParallelTaskScheduler(Form _f)
        {
            f = _f;
        }

        private string Process(List<string> items, ParallelOptions parallelOptions)
        {
            string result = string.Empty;
            ConcurrentBag<string> results = new ConcurrentBag<string>();

            Parallel.ForEach(items, parallelOptions, item =>
            {
                // Simulate some work
                Thread.Sleep(1000);

                string itemStr = $"{item} - Processed on UI Thread: {Thread.CurrentThread.ManagedThreadId}{Environment.NewLine}";
                //---- RACE Conidition here if using a regular string
                // result += itemStr;

                results.Add(itemStr);
            });
            result = string.Join(string.Empty, results);
            return result;
        }

        [Time("Parallel Run()")]
        public void Run()
        {
            if(isRunning)
            {
                Label("Already running, please wait...", true);
                return;
            }
            isRunning = true;
            // no TaskScheduler specified, will use ThreadPool threads
            // using additional threads from the pool so it should finish in about 1 second
            ParallelOptions parallelOptions = new ParallelOptions();
            string result = Process(items, parallelOptions); 
            Label("Processing complete.");
            RichTextbox(result);
            isRunning = false;
        }

        // This will run the parallel tasks on the UI thread (thread 1)
        [Time("Parallel RunWithTaskScheduler()")]
        public void RunWithTaskScheduler()
        {
            if (isRunning)
            {
                Label("Already running, please wait...", true);
                return;
            }
            isRunning = true;
            // Get the UI thread's TaskScheduler
            // since its queuing to the UI thread, it will take about 5 seconds to complete (5items x 1second)
            TaskScheduler uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            ParallelOptions parallelOptions = new ParallelOptions { TaskScheduler = uiScheduler };
            string result = Process(items, parallelOptions);
            Label("Processing complete.");
            RichTextbox(result);
            isRunning = false;
        }

        [Time("Parallel ProcessOnSpecifiedThread(with TaskScheduler: {separateSyncContext})")]
        private void ProcessOnSpecifiedThread(bool separateSyncContext)
        {
            ParallelOptions parallelOptions = new ParallelOptions();

            if (separateSyncContext)
            {
                SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
                // Get a TaskScheduler associated with this SynchronizationContext
                TaskScheduler customScheduler = TaskScheduler.FromCurrentSynchronizationContext();
                parallelOptions = new ParallelOptions { TaskScheduler = customScheduler };
            }

            string result = Process(items, parallelOptions);
            Debug.Print(result);

            Label("Processing complete.");
            RichTextbox(result);
            isRunning = false;
        }

        public void RunOnSpecifiedThread(bool separateSyncContext)
        {
            if (isRunning)
            {
                Label("Already running, please wait...", true);
                return;
            }
            isRunning = true;
            ThreadStart tprocess = new ThreadStart(() => { ProcessOnSpecifiedThread(separateSyncContext); });
            Thread thread = new Thread(tprocess);

            thread.IsBackground = true;
            thread.Start();
        }
    }
}
