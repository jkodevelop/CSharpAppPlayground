using System.Diagnostics;

namespace CSharpAppPlayground.Multithreading.ParallelExample
{
    /// <summary>
    /// This example uses Parallel to demonstrate parallel processing in C#.
    /// </summary>
    public class ParallelExample2
    {
        public async Task ShowAsync()
        {
            Debug.Print("Processing a collection in parallel with Parallel.ForEach...");
            List<int> workOrderIds = Enumerable.Range(1, 10).ToList();

            // Use Task.WhenAll with LINQ to process items in parallel without blocking
            var tasks = workOrderIds.Select(async workId =>
            {
                // This lambda will be executed on multiple threads concurrently.
                string processingResult = await ProcessWorkOrder(workId).ConfigureAwait(false);
                Debug.Print(processingResult);
                return processingResult;
            });

            // Wait for all tasks to complete
            await Task.WhenAll(tasks).ConfigureAwait(false);
        }

        // Keep the original synchronous method for backward compatibility
        public async void Show()
        {
            // Call the async version and wait for it to complete
            // ShowAsync().Wait();
            ShowAsync();
        }

        protected async Task<string> ProcessWorkOrder(int orderId)
        {
            // Simulate work for this specific item
            Debug.Print($"      -> Processing order {orderId} on thread {Environment.CurrentManagedThreadId}...");
            await Task.Delay(1000).ConfigureAwait(false); // Use Task.Delay instead of Thread.Sleep for non-blocking
            // Thread.Sleep(1000); // Simulate work, 1000 milliseconds
            Debug.Print($"      -> Still Processing order {orderId} on thread {Environment.CurrentManagedThreadId}...");
            await Task.Delay(500).ConfigureAwait(false); // Use Task.Delay instead of Thread.Sleep for non-blocking
            // Thread.Sleep(500); // Simulate work, 500 milliseconds
            return $"Result for order {orderId}";
        }
    }
}
