using System.Diagnostics;

namespace CSharpAppPlayground.Multithreading.ParallelExample
{
    /// <summary>
    /// This example uses Parallel to demonstrate parallel processing in C#.
    /// </summary>
    public class ParallelExample
    {
        public void Show()
        {
            Debug.Print("Processing a collection in parallel with Parallel.ForEach...");
            // List<int> workOrderIds = Enumerable.Range(1, 10).ToList();
            int[] workerIds = Enumerable.Range(1, 10).ToArray();

            Parallel.ForEach(workerIds, workId =>
            {
                // This lambda will be executed on multiple threads concurrently.
                Task<string> processingResult = ProcessWorkOrder(workId);
                // processedResults.Add(processingResult);
                Debug.Print(processingResult.ToString());
            });
        }

        protected async Task<string> ProcessWorkOrder(int orderId)
        {
            // Simulate work for this specific item
            Debug.Print($"      -> Processing order {orderId} on thread {Environment.CurrentManagedThreadId}...");
            Thread.Sleep(1000);
            Debug.Print($"      -> Still Processing order {orderId} on thread {Environment.CurrentManagedThreadId}...");
            Thread.Sleep(500);
            return $"Result for order {orderId}";
        }
    }
}
