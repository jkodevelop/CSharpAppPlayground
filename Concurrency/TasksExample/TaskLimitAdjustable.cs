using CSharpAppPlayground.UIClasses;
using System.Diagnostics;

namespace CSharpAppPlayground.Concurrency.TasksExample
{
    public class TaskLimitAdjustable : UIFormRichTextBoxHelper
    {
        NumericUpDown procNumbers;

        int maxParallelism = 5;
        int totalTasks = 10;
        int previousNumericValue = 0;
        SemaphoreSlim semaphore;

        bool isRunning = false;

        public TaskLimitAdjustable(Form _f, NumericUpDown _procNumbers)
        {
            f = _f;
            procNumbers = _procNumbers;

            procNumbers.ValueChanged += (sender, e) => ProcNumbers_ValueChanged(sender, e);
        }

        private void init()
        {
            previousNumericValue = 0;
            procNumbers.Value = 0;
            semaphore = new SemaphoreSlim(0, maxParallelism);
        }

        private void ReleaseMoreSlots(int count)
        {
            if (semaphore.CurrentCount >= maxParallelism)
            {
                Debug.Print("Semaphore is already at max capacity.");
                return;
            }
            int toRelease = semaphore.CurrentCount + count;
            if (toRelease > maxParallelism)
            {
                toRelease = maxParallelism;
            }
            toRelease -= semaphore.CurrentCount; // Calculate how many to actually release
            semaphore.Release(toRelease);
        }

        protected void ProcNumbers_ValueChanged(object? sender, EventArgs e)
        {
            int newValue = (int)procNumbers.Value;
            if (newValue < previousNumericValue)
            {
                procNumbers.Value = previousNumericValue;
                return;
            }
            
            int numToRelease = newValue - previousNumericValue;
            previousNumericValue = newValue;

            ReleaseMoreSlots(numToRelease);
        }

        public async Task ShowAsync()
        {
            if(isRunning)
            {
                Debug.Print("Task is already running. Please wait until it completes.");
                return;
            }
            isRunning = true;

            init();
            Label($"Starting with 0 tasks, waiting for task to be increased to Max of {maxParallelism}");
            RichTextbox($"{semaphore.CurrentCount} tasks can enter the semaphore.");

            Task[] tasks = new Task[totalTasks];
            int padding = 0;
            for (int i = 0; i < totalTasks; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    // Each task begins by requesting the semaphore.
                    RichTextbox($"Task {Task.CurrentId} begins and waits for the semaphore.");
                    int semaphoreCount;
                    semaphore.Wait();
                    try
                    {
                        Interlocked.Add(ref padding, 50);
                        RichTextbox($"Task {Task.CurrentId} enters the semaphore.", Color.Green);
                        // The task just sleeps for 1+ seconds.
                        Thread.Sleep(1000 + padding);
                    }
                    finally
                    {
                        semaphoreCount = semaphore.Release();
                    }
                    RichTextbox($"Task {Task.CurrentId} releases the semaphore; previous count: {semaphoreCount}.");
                });
            }
            // Wait for all tasks to complete.
            await Task.WhenAll(tasks);
            Label("All tasks complete.");
            isRunning = false;
        }
    }
}
