using CSharpAppPlayground.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAppPlayground.Multithreading.TasksExample
{
    public class TaskPausible
    {
        private Form f;
        private Button btnPauseT1, btnPauseT2;

        // cannot use ManualResetEvent because it is not awaitable, this will block the UI thread
        // since this example "Tasks" are run by async/await so it is not running on a separate thread
        // private ManualResetEventSlim[] mres = new ManualResetEventSlim[2];
        // instead use AsyncManualResetEvent
        private AsyncManualResetEvent[] mres = new AsyncManualResetEvent[2];

        bool isRunning = false; // Flag to control the running state of the task    
        int maxCount = 5;

        public TaskPausible(Form _f, Button _btnPauseT1, Button _btnPauseT2)
        {
            f = _f;
            btnPauseT1 = _btnPauseT1;
            btnPauseT2 = _btnPauseT2;
            btnPauseT1.Click += (sender, e) => PausePressedTask(sender, 0);
            btnPauseT2.Click += (sender, e) => PausePressedTask(sender, 1);
            init();
        }

        public void init()
        {
            for(int i = 0; i < mres.Length; i++)
            {
                // mres[i] = new ManualResetEventSlim(true); // Initialize each ManualResetEventSlim, true means it's unpaused
                mres[i] = new AsyncManualResetEvent(true); // true = unpaused
            }
        }
        public async Task ShowAsync()
        {
            if (isRunning)
            {
                Debug.Print("Task is already running.");
                return;
            }
            isRunning = true; // Set the running flag to true

            // what is a signaled ManualResetEventSlim?
            // A ManualResetEventSlim that is in a signaled state allows threads to proceed.
            // to unsignal it, you call Reset() on it, which blocks threads that are waiting on it.

            Debug.Print("Processing a collection in parallel with async/await...");
            List<int> workOrderIds = Enumerable.Range(1, 2).ToList();

            List<Task<string>> tasks = workOrderIds.Select(workId => ProcessWorkOrderAsync(workId)).ToList();

            string[] results = await Task.WhenAll(tasks);

            foreach (string result in results)
            {
                (f as FormConcurTask).updateRichTextBoxMain(result);
            }
            isRunning = false; // Reset the running flag
        }

        protected async Task<string> ProcessWorkOrderAsync(int orderId)
        {
            int taskIdx = orderId - 1;
            Debug.Print($"      -> Processing order {orderId} on thread {Environment.CurrentManagedThreadId}...");
            for (int i=0; i<maxCount; i++)
            {
                Debug.Print($"      -> Still Processing order {orderId} on thread {Environment.CurrentManagedThreadId}...");
                // mres[taskIdx].Wait(); // UI thread blocking, this will turn into Deadlock
                await mres[taskIdx].WaitAsync();
                await Task.Delay(500);
            }
            return $"Result for order {orderId}";
        }

        protected void PausePressedTask(object sender, int taskIndex)
        {
            if (taskIndex < 0 || taskIndex >= mres.Length)
                throw new ArgumentOutOfRangeException(nameof(taskIndex), "Invalid task index.");
            string taskName = taskIndex == 0 ? "1" : "2";
            if (mres[taskIndex].IsSet)
            {
                mres[taskIndex].Reset(); // Pause the task
                Debug.Print($"Task {taskName} paused.");
                string btnText = (sender as Button)?.Text ?? string.Empty;
                string newText = btnText.Replace("Pause", "Resume");
                (sender as Button).Text = newText; // Replace 'Pause' with 'Resume'
            }
            else
            {
                mres[taskIndex].Set(); // Resume the task
                Debug.Print($"Task {taskName} resumed.");
                string btnText = (sender as Button)?.Text ?? string.Empty;
                string newText = btnText.Replace("Resume", "Pause");
                (sender as Button).Text = newText; // Replace 'Resume' with 'Pause'
            }
        }
    }

}
