using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAppPlayground.Concurrency.TasksExample
{
    public class TaskSimple
    {
        public async Task<string> ProcessOrderAsync(int orderId)
        {
            await Task.Delay(1000); // Simulate work
            Debug.Print($"Delayed done: order {orderId} on thread {Environment.CurrentManagedThreadId}...");
            return $"Order {orderId} processed on thread {Environment.CurrentManagedThreadId}";
        }
    }
}
