using CSharpAppPlayground.UIClasses;
using System.Diagnostics;

namespace CSharpAppPlayground.Concurrency.ParallelExample
{
    /// <summary>
    /// This example uses Parallel to demonstrate parallel processing in C#.
    /// </summary>
    public class ParallelExample
    {
        private Form f;

        public ParallelExample(Form _f)
        {
            f = _f;
        }

        private void updateRichTextBoxMain(string txt)
        {
            // (f as FormWithRichText).updateRichTextBoxMain(txt);
            if (f.InvokeRequired)
            {
                // Invoke vs BeginInvoke
                f.BeginInvoke(new Action(() => (f as FormWithRichText).updateRichTextBoxMain(txt)));
            }
            else
            {
                (f as FormWithRichText).updateRichTextBoxMain(txt);
            }
        }

        private void updateLabelMain(string txt)
        {
            (f as FormWithRichText).updateLabelMain(txt);
        }

        public async void Run()
        {
            Debug.Print("Processing a collection in parallel with Parallel.ForEach...");
            // List<int> workOrderIds = Enumerable.Range(1, 10).ToList();
            int[] workerIds = Enumerable.Range(1, 5).ToArray();

            Parallel.ForEach(workerIds, workId =>
            {
                // This lambda will be executed on multiple threads concurrently.
                Task<string> processingResult = ProcessWorkOrder(workId);
                Debug.Print(processingResult.ToString());
            });
        }

        protected async Task<string> ProcessWorkOrder(int orderId)
        {
            // Simulate work for this specific item
            //updateRichTextBoxMain($"Processing order {orderId} on thread {Environment.CurrentManagedThreadId}...");
            // Thread.Sleep(100);
            //await Task.Delay(1000); // Simulate async work
            
            // Thread.Sleep(50);
            //await Task.Delay(500); // Simulate async work
            updateRichTextBoxMain($"Still Processing order {orderId} on thread {Environment.CurrentManagedThreadId}...");
            await Task.Delay(500); // Simulate async work

            Debug.Print($"Result for order {orderId}");
            return $"Result for order {orderId}";
        }
    }
}
