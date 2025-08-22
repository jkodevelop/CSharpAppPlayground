using CSharpAppPlayground.UIClasses;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace CSharpAppPlayground.Concurrency.TasksExample
{
    public class TaskSimpleExample : UIFormRichTextBoxHelper
    {
        public BlockingCollection<string> cakeOrders;

        // private Form f;

        public TaskSimpleExample(Form _f)
        {
            f = _f;
        }

        public void Baker()
        {
            foreach (string order in cakeOrders.GetConsumingEnumerable())
            {
                this.RichTextbox($"Baker is baking {order}...", Color.Red);
                Thread.Sleep(2000); // Simulate baking time, can use Thread.Sleep because Task.Run() put this function in a separate thread
                // Debug.Print($"Baker has finished baking {order}");
                this.RichTextbox($"Baker has finished baking {order}", Color.Green);
            }
            Debug.Print("Baker loop ended");
        }

        public async Task ShowAsync()
        {
            cakeOrders = new BlockingCollection<string>(boundedCapacity: 3); // Initialize the collection with a bounded capacity

            Task.Run(Baker); // Start the baker task
            Debug.Print("Baker() function called");

            // Simulate placing orders
            for (int i = 1; i <= 10; i++)
            {
                string order = $"Cake {i}";
                this.RichTextbox($"Placing order for {order}", Color.Turquoise);
                cakeOrders.Add(order);
                // Thread.Sleep(1000); // Simulate time between orders, This is called by UI so Thread = UI = blocking
                await Task.Delay(1000); // Use Task.Delay for non-blocking delay
            }
            // Signal that no more orders will be placed
            cakeOrders.CompleteAdding();
            // Wait for the baker to finish processing all orders
            this.Label("CompleteAdding() called. All orders placed. Waiting for baker to finish...", true);
        }
    }
}

// source: https://medium.com/@codechuckle/exploring-blockingcollection-in-c-a-comprehensive-guide-c735ada150f3
