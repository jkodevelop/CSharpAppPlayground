using CSharpAppPlayground.UIClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CSharpAppPlayground.Concurrency.TasksExample
{
    /// <summary>
    /// This example uses async/await to demonstrate parallel processing in C# without blocking the UI thread.
    /// </summary>
    public class TaskExample : UIFormRichTextBoxHelper
    {
        // private Form f;

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
                this.RichTextbox(result);
            }
        }

        protected async Task<string> ProcessWorkOrderAsync(int orderId)
        {
            this.RichTextbox($"Processing order {orderId} on thread {Environment.CurrentManagedThreadId}...");
            await Task.Delay(2000);
            this.RichTextbox($"Still Processing order {orderId} on thread {Environment.CurrentManagedThreadId}...");
            await Task.Delay(500);
            return $"Result for order {orderId}";
        }
    }
}
