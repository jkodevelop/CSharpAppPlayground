using CSharpAppPlayground.UIClasses;
using System.Collections.Concurrent;

namespace CSharpAppPlayground.Concurrency.ParallelExample
{
    public class ParallelLimit : UIFormRichTextBoxHelper
    {
        //List<string> items = new List<string> { 
        //    "Item 1", "Item 2", "Item 3", "Item 4", "Item 5",
        //    "Item 6", "Item 7", "Item 8", "Item 9", "Item 10"
        //};
        bool isRunning = false;
        public ParallelLimit(Form _f)
        {
            f = _f;
        }

        public void Run()
        {
            if(isRunning)
            {
                Label("Already running, please wait...", true);
                return;
            }
            isRunning = true;
            string result = string.Empty;
            ConcurrentBag<string> results = new ConcurrentBag<string>();

            ParallelOptions parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = 2 };
            // Example usage with Parallel.For
            Parallel.For(0, 10, parallelOptions, i =>
            {
                results.Add($"Task {i} started on thread {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(500); // Simulate some work
                results.Add($"Task {i} completed on thread {Thread.CurrentThread.ManagedThreadId}");
            });

            result = string.Join(Environment.NewLine, results);
            RichTextbox(result);
            Label("Processing complete.");
            isRunning = false;
        }

    }
}
