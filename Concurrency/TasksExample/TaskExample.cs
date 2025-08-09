using System.Diagnostics;

namespace CSharpAppPlayground.Concurrency.TasksExample
{
    /// <summary>
    /// This example uses async/await to demonstrate parallel processing in C# without blocking the UI thread.
    /// </summary>
    public class TaskExample
    {
        private Form f;

        public TaskExample(Form _f)
        {
            f = _f;
        }
        public async Task ShowAsync()
        {
            Debug.Print("Processing a collection in parallel with async/await...");
            List<int> workOrderIds = Enumerable.Range(1, 10).ToList();

            List<Task<string>> tasks = workOrderIds.Select(workId => ProcessWorkOrderAsync(workId)).ToList();

            string[] results = await Task.WhenAll(tasks);

            foreach (string result in results)
            {
                (f as FormConcurTask).updateRichTextBoxMain(result);
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
