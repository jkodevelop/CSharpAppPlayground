using CSharpAppPlayground.UIClasses;
using System.Diagnostics;

namespace CSharpAppPlayground.Concurrency.ParallelExample
{
    public class ParallelInvoke : UIFormRichTextBoxHelper
    {
        public ParallelInvoke(Form _f) { f = _f; }

        public void Run() 
        {
            Parallel.Invoke(
                () => PerformingAnAction("SendDataToA", 1000),
                () => PerformingAnAction("SendDataToB", 500),
                () => PerformingAnAction("SendDataToC", 2000)
            );
            Debug.Print("All tasks are finished.");
        }

        private void PerformingAnAction(string name, int delay)
        {
            Debug.Print($"Starting {name} on thread {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(delay);
            Debug.Print($"Ending {name} on thread {Environment.CurrentManagedThreadId}");
        }
    }
}
