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

        private ManualResetEventSlim[] mres = new ManualResetEventSlim[2];

        bool isRunning = false; // Flag to control the running state of the task    

        public TaskPausible(Form _f)
        {
            f = _f;
            init();
        }

        public void init()
        {
            for(int i = 0; i < mres.Length; i++)
            {
                mres[i] = new ManualResetEventSlim(false); // Initialize each ManualResetEventSlim
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
        }

        protected async Task<string> ProcessWorkOrderAsync(int orderId)
        {
            mres[orderId - 1] = new ManualResetEventSlim(true); // Initialize the ManualResetEventSlim for each order
            Debug.Print($"      -> Processing order {orderId} on thread {Environment.CurrentManagedThreadId}...");
            await Task.Delay(5000);
            Debug.Print($"      -> Still Processing order {orderId} on thread {Environment.CurrentManagedThreadId}...");
            await Task.Delay(500);
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
