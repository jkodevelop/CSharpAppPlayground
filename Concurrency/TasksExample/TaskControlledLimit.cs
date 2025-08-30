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

        public TaskControlledLimit(Form _f)
        {
            f = _f;
        }

        public async Task ShowAsync()
        {
            // Create the semaphore.
            semaphore = new SemaphoreSlim(0, maxParallelism);
            Debug.Print("{0} tasks can enter the semaphore.",
                              semaphore.CurrentCount);
            Task[] tasks = new Task[totalTasks];

            // Create and start five numbered tasks.
            for (int i = 0; i <= 4; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    // Each task begins by requesting the semaphore.
                    Debug.Print("Task {0} begins and waits for the semaphore.",
                                      Task.CurrentId);

                    int semaphoreCount;
                    semaphore.Wait();
                    try
                    {
                        Interlocked.Add(ref padding, 100);

                        Debug.Print("Task {0} enters the semaphore.", Task.CurrentId);

                        // The task just sleeps for 1+ seconds.
                        Thread.Sleep(1000 + padding);
                    }
                    finally
                    {
                        semaphoreCount = semaphore.Release();
                    }
                    Debug.Print("Task {0} releases the semaphore; previous count: {1}.",
                                      Task.CurrentId, semaphoreCount);
                });
            }

            // Wait for half a second, to allow all the tasks to start and block.
            Thread.Sleep(500);

            // Restore the semaphore count to its maximum value.
            Console.Write("Main thread calls Release(3) --> ");
            semaphore.Release(maxParallelism);
            Debug.Print("{0} tasks can enter the semaphore.",
                              semaphore.CurrentCount);
            // Main thread waits for the tasks to complete.
            Task.WaitAll(tasks);

            Debug.Print("Main thread exits.");
        }

    }
}

// source: https://learn.microsoft.com/en-us/dotnet/api/system.threading.semaphoreslim?view=net-9.0
