using CSharpAppPlayground.UIClasses;
using MethodTimer;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace CSharpAppPlayground.Concurrency.ParallelExample
{
    public class ParallelTaskScheduler : UIFormRichTextBoxHelper
    {
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

                // This will now execute on the UI thread
                string itemStr = $"{item} - Processed on UI Thread: {Thread.CurrentThread.ManagedThreadId}{Environment.NewLine}";
                // RACE Conidition here if using a regular string
                // result += itemStr;

                results.Add(itemStr);
            });
            result = string.Join(string.Empty, results);
            return result;
        }

        [Time("Parallel TaskScheduler Run()")]
        public void Run()
        {
            List<string> items = new List<string> { "Item 1", "Item 2", "Item 3", "Item 4", "Item 5" };

            // 1. Get the UI thread's TaskScheduler
            //TaskScheduler uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            //ParallelOptions parallelOptions = new ParallelOptions { TaskScheduler = uiScheduler };

            // 2. no TaskScheduler specified, will use ThreadPool threads
            ParallelOptions parallelOptions = new ParallelOptions();

            // 3. Custom TaskScheduler (not recommended for UI updates)
            // SynchronizationContext sc = new SynchronizationContext();

            string result = Process(items, parallelOptions); 

            Label("Processing complete.");
            RichTextbox(result);
        }
    }
}
