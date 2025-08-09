using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using CSharpAppPlayground.UIClasses;
using System.DirectoryServices.ActiveDirectory;

namespace CSharpAppPlayground.Concurrency.TasksExample
{
    public class TaskStoppable
    {
        private Form f;
        private Button btnStopT1;

        private CancellationTokenSource cts;

        private int max = 5;
        public TaskStoppable(Form _f, Button _btnStopT1)
        {
            f = _f;
            btnStopT1 = _btnStopT1;
            btnStopT1.Click += (sender, e) => StopPressedTask(sender, 0);
        }

        protected void StopPressedTask(object sender, int taskIndex)
        {
            if(cts == null)
            {
                Debug.Print("CancellationTokenSource is not initialized.");
                return;
            }
            cts.Cancel();

            Debug.Print("Stop button pressed for task.");
            // Logic to stop the task
            // This could involve setting a cancellation token or flag that the task checks periodically
            // For example, if using CancellationToken:
            // cancellationTokenSource.Cancel();
            (f as FormWithRichText).updateRichTextBoxMain("Task has been requested to stop.");
        }

        public async Task ShowAsync()
        {
            btnStopT1.Enabled = true;
            
            cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            Task[] tasks = new[]
            {
                Task.Run(() =>
                {
                    for (int i = 1; i <= max; i++)
                    {
                        (f as FormWithRichText).updateRichTextBoxMain($"A task is running... {i}/{max}");
                        token.ThrowIfCancellationRequested();
                        // Simulate work
                        Task.Delay(2000, token).Wait();
                    }
                }, token),
                Task.Run(() =>
                {
                    token.WaitHandle.WaitOne(5000);
                    (f as FormWithRichText).updateRichTextBoxMain($"A secret task is running... ", Color.DarkBlue);
                    if (token.IsCancellationRequested)
                        throw new OperationCanceledException(token);
                }, token)
            };

            try
            {
                await Task.WhenAll(tasks);
                (f as FormWithRichText).updateRichTextBoxMain("A tasks are done.");
            }
            catch (AggregateException ae)
            {
                foreach (var ee in ae.InnerExceptions)
                {
                    if (ee is TaskCanceledException)
                        (f as FormWithRichText).updateRichTextBoxMain("A task was canceled.");
                    else
                        (f as FormWithRichText).updateRichTextBoxMain($"Task faulted: {ee.Message}");
                }
            }
            finally
            {
                cts.Dispose();
                btnStopT1.Enabled = false;
            }
           
        }
    }
}
