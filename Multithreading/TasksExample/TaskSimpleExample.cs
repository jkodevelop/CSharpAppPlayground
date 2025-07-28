using CSharpAppPlayground.Classes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAppPlayground.Multithreading.TasksExample
{
    public class TaskSimpleExample
    {
        public BlockingCollection<string> cakeOrders;

        private Form f;

        public TaskSimpleExample(Form _f)
        {
            f = _f;
        }

        public void Baker()
        {
            foreach (var order in cakeOrders.GetConsumingEnumerable())
            {
                Debug.Print($"Baker is baking {order}...");
                Thread.Sleep(2000); // Simulate baking time
                Debug.Print($"Baker has finished baking {order}");
                (f as FormConcurTask).updateRichTextBoxMain($"Baker has finished baking {order}", System.Drawing.Color.Green);
            }
        }

        public async void Run()
        {
            cakeOrders = new BlockingCollection<string>(boundedCapacity: 5); // Initialize the collection with a bounded capacity

            Task.Run(Baker); // Start the baker task
            
            // Simulate placing orders
            for (int i = 1; i <= 10; i++)
            {
                string order = $"Cake {i}";
                Debug.Print($"Placing order for {order}");
                cakeOrders.Add(order);
                // Thread.Sleep(1000); // Simulate time between orders, This is called by UI so Thread = UI = blocking
                await Task.Delay(1000); // Use Task.Delay for non-blocking delay
            }
            // Signal that no more orders will be placed
            cakeOrders.CompleteAdding();
            // Wait for the baker to finish processing all orders
            Debug.Print("All orders placed. Waiting for baker to finish...");
        }
    }
}

// source: https://medium.com/@codechuckle/exploring-blockingcollection-in-c-a-comprehensive-guide-c735ada150f3
