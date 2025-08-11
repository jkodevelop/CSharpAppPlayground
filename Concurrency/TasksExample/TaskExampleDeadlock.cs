using CSharpAppPlayground.UIClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAppPlayground.Concurrency.TasksExample
{
    // TO DOCUMENT! deadlock
    // example: calling ShowAsync().Wait() or ShowAsync().GetAwaiter().GetResult() can cause a deadlock in some contexts, especially in UI applications.
    // example: using Thread.Sleep will block cause this is using UI thread, its not a parallel processing

    public class TaskExampleDeadlock
    {
        private Form f;

        public TaskExampleDeadlock(Form _f)
        {
            f = _f;
        }

        public async Task ShowAsync()
        {
            Debug.Print("Processing a collection in parallel with Parallel.ForEach...");
            List<int> workOrderIds = Enumerable.Range(1, 10).ToList();

            // Use Task.WhenAll with LINQ to process items in parallel without blocking
            var tasks = workOrderIds.Select(async workId =>
            {
                // This lambda will be executed on multiple threads concurrently.
                string processingResult = await ProcessWorkOrder(workId).ConfigureAwait(false);
                (f as FormWithRichText).updateRichTextBoxMain(processingResult);
                return processingResult;
            });

            // Wait for all tasks to complete
            await Task.WhenAll(tasks).ConfigureAwait(false);
        }

        // Keep the original synchronous method for backward compatibility
        public async void Show()
        {
            // Call the async version and wait for it to complete
            // ShowAsync().Wait(); // this is not recommended as it can cause DEADLOCK in some contexts
            ShowAsync();
        }

        protected async Task<string> ProcessWorkOrder(int orderId)
        {
            // Simulate work for this specific item
            (f as FormWithRichText).updateRichTextBoxMain($"Another Example: Processing order {orderId} on thread {Environment.CurrentManagedThreadId}...");
            await Task.Delay(1000).ConfigureAwait(false); // Use Task.Delay instead of Thread.Sleep for non-blocking
            // Thread.Sleep(1000); // Simulate work, 1000 milliseconds
            (f as FormWithRichText).updateRichTextBoxMain($"Another Example: Still Processing order {orderId} on thread {Environment.CurrentManagedThreadId}...");
            await Task.Delay(500).ConfigureAwait(false); // Use Task.Delay instead of Thread.Sleep for non-blocking
            // Thread.Sleep(500); // Simulate work, 500 milliseconds
            return $"Result for order {orderId}";
        }
    }
}
