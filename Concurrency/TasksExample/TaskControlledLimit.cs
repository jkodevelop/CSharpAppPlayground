using CSharpAppPlayground.UIClasses;
using System.Diagnostics;

namespace CSharpAppPlayground.Concurrency.TasksExample
{
    public class TaskControlledLimit : UIFormRichTextBoxHelper
    {
        private static SemaphoreSlim semaphore;
        // A padding interval to make the output more orderly.
        private static int padding;

        private int maxParallelism = 3;
        private int totalTasks = 5;

        bool isRunning = false;

        public TaskControlledLimit(Form _f)
        {
            f = _f;
        }

        public async Task ShowAsync()
        {
            if(isRunning)
            {
                Debug.Print("TaskControlledLimit is already running, please wait.");
                return;
            }
            isRunning = true;

            // Create the semaphore.
            semaphore = new SemaphoreSlim(0, maxParallelism);
            RichTextbox($"{semaphore.CurrentCount} tasks can enter the semaphore.");
            Task[] tasks = new Task[totalTasks];

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
                        Interlocked.Add(ref padding, 100);

                        RichTextbox($"Task {Task.CurrentId} enters the semaphore.");

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

            // Wait for half a second, to allow all the tasks to start and block.
            Thread.Sleep(500);

            // Restore the semaphore count to its maximum value.
            RichTextbox($"Main thread calls Release({maxParallelism}) --> ");
            semaphore.Release(maxParallelism);
            RichTextbox($"{semaphore.CurrentCount} tasks can enter the semaphore.");
            // Main thread waits for the tasks to complete.
            Task.WaitAll(tasks);

            Label("Main thread exits.");
            isRunning = false;
        }

    }
}

// source: https://learn.microsoft.com/en-us/dotnet/api/system.threading.semaphoreslim?view=net-9.0
