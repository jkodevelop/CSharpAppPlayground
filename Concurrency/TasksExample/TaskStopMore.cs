using CSharpAppPlayground.UIClasses;
using System.Diagnostics;

namespace CSharpAppPlayground.Concurrency.TasksExample
{
    public class TaskStopMore : UIFormRichTextBoxHelper
    {
        // private Form f;
        private Button btnStopT1;
        private Button btnStopT2;
        private Button btnStopT3;
        private Button btnStopAll;

        private int maxTasks = 3;
        private CancellationTokenSource[] cancellationTokenSources;
        private CancellationTokenSource globalCTS;
        private CancellationTokenSource[] linkedTokens;

        Color[] colors = new Color[] { Color.Green, Color.Blue, Color.Purple };

        private bool isTaskRunning = false;

        public TaskStopMore(Form _f, Button _bt1, Button _bt2, Button _bt3, Button _btnAll)
        {
            f = _f;
            btnStopT1 = _bt1;
            btnStopT2 = _bt2;
            btnStopT3 = _bt3;
            btnStopAll = _btnAll;
            btnStopT1.Click += (sender, e) => StopTask(0);
            btnStopT2.Click += (sender, e) => StopTask(1);
            btnStopT3.Click += (sender, e) => StopTask(2);
            btnStopAll.Click += (sender, e) => StopAllTasks();
        }

        public void ToggleButtons()
        {
            btnStopT1.Enabled = !btnStopT1.Enabled;
            btnStopT2.Enabled = !btnStopT2.Enabled;
            btnStopT3.Enabled = !btnStopT3.Enabled;
            btnStopAll.Enabled = !btnStopAll.Enabled;
        }

        private void initializeCancellationTokens()
        {
            globalCTS = new CancellationTokenSource();
            cancellationTokenSources = new CancellationTokenSource[maxTasks];
            linkedTokens = new CancellationTokenSource[maxTasks];
            for (int i = 0; i < maxTasks; i++)
            {
                cancellationTokenSources[i] = new CancellationTokenSource();
                // using linked tokens, if either the Task Token or Global Token is cancelled then the linked token will also be cancelled
                linkedTokens[i] = CancellationTokenSource.CreateLinkedTokenSource(cancellationTokenSources[i].Token, globalCTS.Token);
            }
        }

        private void disposeAllTokens()
        {
            for (int i = 0; i < maxTasks; i++)
            {
                cancellationTokenSources[i].Dispose();
                linkedTokens[i].Dispose();
            }
            globalCTS.Dispose();
        }

        private void StopTask(int taskIndex)
        {
            // Logic to stop the specified task
            this.RichTextbox($"Stopping Task {taskIndex+1}", colors[taskIndex]);
            // Here you would implement the actual stopping logic for the task
            // For example, if you have a CancellationTokenSource for each task, you would call Cancel on it.
            linkedTokens[taskIndex].Cancel();
        }

        private void StopAllTasks()
        {
            // Logic to stop all tasks
            this.RichTextbox("Stopping all tasks");
            globalCTS.Cancel();
        }

        public async Task ShowAsync()
        {
            if (isTaskRunning)
            {
                Debug.Print($"Tasks are already running.");
                return;
            }

            initializeCancellationTokens();
            ToggleButtons(); // starting tasks, so enable stop buttons
            isTaskRunning = true;

            Task[] tasks =
            [
                RunTask(0, 3, linkedTokens[0].Token),
                RunTask(1, 4, linkedTokens[1].Token),
                RunTask(2, 2, linkedTokens[2].Token)
            ];

            IEnumerable<Task> wrappedTasks = tasks.Select(async t =>
            {
                try
                {
                    await t;
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Task was cancelled gracefully.");
                }
            });

            await Task.WhenAll(wrappedTasks);

            this.RichTextbox("All tasks complete or cancelled.");

            // end of tasks, dispose all tokens
            disposeAllTokens();
            ToggleButtons(); // starting tasks, so disable stop buttons
            isTaskRunning = false;
        }

        protected async Task RunTask(int idx, int delaySec, CancellationToken token)
        {
            string name = $"Task {idx+1}";
            try
            {
                this.RichTextbox($"{name} started.", colors[idx]);
                for(int i = 0; i < delaySec; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        this.RichTextbox($"{name} was cancelled.", colors[idx]);
                        return;
                    }
                    this.RichTextbox($"{name} is running... {i}/{delaySec}", colors[idx]);
                    await Task.Delay(1000, token); // Simulate work
                }
                Console.WriteLine($"{name} completed successfully.", colors[idx]);
            }
            catch (OperationCanceledException)
            {
                this.RichTextbox($"{name} was cancelled.", colors[idx]);
            }
            catch (Exception ex)
            {
                Debug.Print($"{name} encountered an error: {ex.Message}");
            }
        }
    }
}
