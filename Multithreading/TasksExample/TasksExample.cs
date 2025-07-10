using System.Diagnostics;

namespace CSharpAppPlayground.Multithreading.TasksExample
{
    /// <summary>
    /// This example uses async/await to demonstrate parallel processing in C# without blocking the UI thread.
    /// </summary>
    public class TasksExample
    {
        public async Task ShowAsync()
        {
            Debug.Print("Processing a collection in parallel with async/await...");
            List<int> workOrderIds = Enumerable.Range(1, 10).ToList();

            var tasks = workOrderIds.Select(workId => ProcessWorkOrderAsync(workId)).ToList();

            var results = await Task.WhenAll(tasks);

            foreach (var result in results)
            {
                Debug.Print(result);
            }
        }

        protected async Task<string> ProcessWorkOrderAsync(int orderId)
        {
            Debug.Print($"      -> Processing order {orderId} on thread {Environment.CurrentManagedThreadId}...");
            await Task.Delay(1000);
            Debug.Print($"      -> Still Processing order {orderId} on thread {Environment.CurrentManagedThreadId}...");
            await Task.Delay(500);
            return $"Result for order {orderId}";
        }
    }
}
