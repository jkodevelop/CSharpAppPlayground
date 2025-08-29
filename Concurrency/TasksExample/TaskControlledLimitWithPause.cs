using CSharpAppPlayground.UIClasses;
using System.Diagnostics;
using System.Collections.Concurrent;
using MethodTimer;

namespace CSharpAppPlayground.Concurrency.ParallelExample
{
    /// <summary>
    /// This example uses Parallel to demonstrate parallel processing in C#.
    /// </summary>
    public class TaskControlledLimitWithPause : UIFormRichTextBoxHelper
    {
        // private Form f;

        private ConcurrentDictionary<int, TaskInfo> taskInfos = new ConcurrentDictionary<int, TaskInfo>();
        private SemaphoreSlim executionSemaphore;
        
        public class TaskInfo
        {
            public int TaskId { get; set; }
            public int ThreadId { get; set; }
            public TaskState State { get; set; }
            public ManualResetEvent PauseEvent { get; set; } = new ManualResetEvent(true);
            public CancellationTokenSource CancellationTokenSource { get; set; } = new CancellationTokenSource();
            public bool IsExecuting { get; set; } = false;
        }
        
        public enum TaskState
        {
            Queued,
            Running,
            Paused,
            Completed,
            Cancelled
        }

        int totalTasks = 10;
        int maxParallelism = 3;
        int secondPerTask = 5;

        public TaskControlledLimitWithPause(Form _f)
        {
            f = _f;
        }

        [Time("RunParallelOperation::")]
        private void RunParallelOperation(ConcurrentBag<string> results, CancellationToken cancellationToken)
        {
            try
            {
                // Use SemaphoreSlim instead of ParallelOptions.MaxDegreeOfParallelism
                executionSemaphore = new SemaphoreSlim(maxParallelism, maxParallelism);
                
                // Create all tasks but let them wait for execution slots
                var tasks = new List<Task>();
                
                for (int i = 0; i < totalTasks; i++)
                {
                    var taskId = i;
                    var taskInfo = new TaskInfo 
                    { 
                        TaskId = taskId, 
                        State = TaskState.Queued
                    };
                    
                    taskInfos.TryAdd(taskId, taskInfo);
                    
                    var task = Task.Run(async () =>
                    {
                        await ProcessTask(taskId, results, cancellationToken);
                    }, cancellationToken);
                    
                    tasks.Add(task);
                }
                
                // Wait for all tasks to complete
                Task.WaitAll(tasks.ToArray(), cancellationToken);

                
            }catch(Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    Label("Operation was cancelled.", true);
                }
                else
                {
                    Label($"An error occurred: {ex.Message}", true);
                }
            }
            finally
            {
                executionSemaphore?.Dispose();
            }
            // ... existing catch blocks ...
        }

        private async Task ProcessTask(int taskId, ConcurrentBag<string> results, CancellationToken cancellationToken)
        {
            try
            {
                // Wait for an execution slot
                await executionSemaphore.WaitAsync(cancellationToken);
                
                var taskInfo = taskInfos[taskId];
                taskInfo.State = TaskState.Running;
                taskInfo.IsExecuting = true;
                taskInfo.ThreadId = Thread.CurrentThread.ManagedThreadId;
                
                try
                {
                    // Check for cancellation
                    cancellationToken.ThrowIfCancellationRequested();

                    string line = $"Task {taskId} started on thread {Thread.CurrentThread.ManagedThreadId}";
                    Debug.Print(line);
                    results.Add(line);

                    // Simulate work with individual pause checks
                    for (int j = 0; j < secondPerTask; j++)
                    {
                        // Check for cancellation
                        cancellationToken.ThrowIfCancellationRequested();
                        
                        // Wait if this specific task is paused
                        taskInfo.PauseEvent.WaitOne();
                        
                        Thread.Sleep(1000); // Simulate some work
                    }

                    line = $"Task {taskId} completed on thread {Thread.CurrentThread.ManagedThreadId}";
                    Debug.Print(line);
                    results.Add(line);
                    
                    taskInfo.State = TaskState.Completed;
                }
                finally
                {
                    // Always release the execution slot
                    taskInfo.IsExecuting = false;
                    executionSemaphore.Release();
                }
            }
            catch (OperationCanceledException)
            {
                if (taskInfos.TryGetValue(taskId, out var taskInfo))
                {
                    taskInfo.State = TaskState.Cancelled;
                    taskInfo.IsExecuting = false;
                    executionSemaphore.Release();
                }
                throw;
            }
        }

        // Individual task control methods
        public void PauseTask(int taskId)
        {
            if (taskInfos.TryGetValue(taskId, out var taskInfo))
            {
                if (taskInfo.State == TaskState.Running)
                {
                    taskInfo.State = TaskState.Paused;
                    taskInfo.PauseEvent.Reset();
                    Label($"Task {taskId} paused...", true);
                }
            }
        }

        public void ResumeTask(int taskId)
        {
            if (taskInfos.TryGetValue(taskId, out var taskInfo))
            {
                if (taskInfo.State == TaskState.Paused)
                {
                    taskInfo.State = TaskState.Running;
                    taskInfo.PauseEvent.Set();
                    Label($"Task {taskId} resumed...", true);
                }
            }
        }

        public void StopTask(int taskId)
        {
            if (taskInfos.TryGetValue(taskId, out var taskInfo))
            {
                taskInfo.State = TaskState.Cancelled;
                taskInfo.CancellationTokenSource.Cancel();
                taskInfo.PauseEvent.Set(); // Unblock if paused
                Label($"Task {taskId} stopped...", true);
            }
        }

        // Get current task status
        public Dictionary<int, TaskState> GetTaskStatus()
        {
            return taskInfos.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.State);
        }

        // Get execution statistics
        public (int Running, int Paused, int Queued, int Completed) GetExecutionStats()
        {
            var running = taskInfos.Values.Count(t => t.State == TaskState.Running);
            var paused = taskInfos.Values.Count(t => t.State == TaskState.Paused);
            var queued = taskInfos.Values.Count(t => t.State == TaskState.Queued);
            var completed = taskInfos.Values.Count(t => t.State == TaskState.Completed);
            
            return (running, paused, queued, completed);
        }

        public async void Run()
        {
           // TODO
        }

    }
}
