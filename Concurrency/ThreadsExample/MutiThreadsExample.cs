using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAppPlayground.Concurrency.ThreadsExample
{
    /// <summary>
    /// This example uses Threads() to demonstrate multithreading in C#.
    /// 
    /// Lots of INVOKE functions because only UI thread can update UI controls.
    /// So update UI controls from other threads, we need to use Invoke or BeginInvoke.
    /// </summary>
    public class MutiThreadsExample
    {
        private Thread[] threads = new Thread[2];
        private ThreadStart[] threadProcess = new ThreadStart[2];

        private ManualResetEvent[] pauseEvents = new ManualResetEvent[2];
        private bool[] isPaused = new bool[2] { false, false };

        private int[] counters = new int[2] { 0, 0 };
        private int maxCount = 10; // default max count for each thread

        private int isRunning = 0; // Flag to control the running state of the threads

        protected Form f;
        protected Button btnThreadA, btnThreadB, btnStatus;

        public MutiThreadsExample(Form _f, Button _btnThreadA, Button _btnThreadB, Button _btnStatus)
        {
            f = _f;
            btnThreadA = _btnThreadA;
            btnThreadB = _btnThreadB;
            btnStatus = _btnStatus;

            btnThreadA.Click += (sender, e) => PausePressedThread(sender, 0);
            btnThreadB.Click += (sender, e) => PausePressedThread(sender, 1);
            btnStatus.Click += (sender, e) => GetThreadsStatus(sender);

            pauseEvents[0] = new ManualResetEvent(true); // Thread A
            pauseEvents[1] = new ManualResetEvent(true); // Thread B
        }

        protected void PausePressedThread(object sender, int threadIndex)
        {
            if (threadIndex < 0 || threadIndex >= pauseEvents.Length)
                throw new ArgumentOutOfRangeException(nameof(threadIndex), "Invalid thread index.");

            string threadName = threadIndex == 0 ? "A" : "B";
            if (isPaused[threadIndex])
            {
                pauseEvents[threadIndex].Set(); // Resume the thread
                isPaused[threadIndex] = false;
                PrintMsg($"Thread {threadName} resumed.");
                string btnText = (sender as Button)?.Text ?? string.Empty;
                string newText = btnText.Replace("Resume", "Pause");
                (sender as Button).Text = newText; // Replace 'Resume' with 'Pause'

                /// alt style to change button text
                // string btnText = sender.GetType().GetProperty("Text").GetValue(sender)?.ToString() ?? string.Empty;
                // sender.GetType().GetProperty("Text").SetValue(sender, newText, null); // Replace 'Resume' with 'Pause'
            }
            else
            {
                isPaused[threadIndex] = true;
                pauseEvents[threadIndex].Reset(); // Pause the thread
                PrintMsg($"Thread {threadName} paused.");
                string btnText = (sender as Button)?.Text ?? string.Empty;
                string newText = btnText.Replace("Pause", "Resume");
                (sender as Button).Text = newText; // Replace 'Resume' with 'Pause'
            }
        }

        protected void GetThreadsStatus(object sender) {
            string threadStatusSummary = "";
            if(threads[0] is not null)
            {
                threadStatusSummary += (threads[0].IsAlive ? "Thread A is alive. " : "Thread A is done. ");
            }
            if(threads[1] is not null)
            {
                threadStatusSummary += (threads[1].IsAlive ? "Thread B is alive." : "Thread B is done.");
            }
            string summary = $"Status: {threadStatusSummary}";
            Debug.Print(summary + Environment.NewLine);
            UpdateStatusLabel(summary);
        }

        protected void UpdateStatusLabel(string msg)
        {
            Debug.Print(msg);
            (f as FormConcurThread).updateLabelMain(msg);
        }
        protected void PrintMsg(string msg, Color c = default)
        {
            Debug.Print(msg);
            (f as FormConcurThread).updateRichTextBoxMain(msg, c);
        }

        private void EnableButtons(Button btn, bool enable)
        {
            if (f.IsDisposed || f.Disposing)
                return; // Prevent invoking on disposed form

            f.Invoke((MethodInvoker)(() => {
                btn.Enabled = enable;
            }));
        }

        private void UpdateStatusButtonState(Button btn)
        {
            if (f.IsDisposed || f.Disposing)
                return; // Prevent invoking on disposed form

            bool enable = isRunning > 0; // Enable the button only if no threads are running
            f.Invoke((MethodInvoker)(() => {
                btn.Enabled = enable;
            }));
            if (!enable)
                UpdateStatusLabel("Status: ");
        }

        protected void ThreadMethodOne()
        {
            isRunning++;
            EnableButtons(btnThreadA, true);
            UpdateStatusButtonState(btnStatus);
            while (counters[0] < maxCount)
            {
                pauseEvents[0].WaitOne(); // Wait until the thread is not paused
                counters[0]++;
                string msg = $"Thread A: id {Thread.CurrentThread.ManagedThreadId} - Count: {counters[0]}/{maxCount}";
                PrintMsg(msg, Color.Red);
                Thread.Sleep(500); // Simulate work
            }
            PrintMsg("Thread A finished.", Color.Red);
            counters[0] = 0; // Reset counter for next run
            isRunning--;
            EnableButtons(btnThreadA, false);
            UpdateStatusButtonState(btnStatus);
        }

        protected void ThreadMethodTwo()
        {
            isRunning++;
            EnableButtons(btnThreadB, true);
            UpdateStatusButtonState(btnStatus);
            while (counters[1] < maxCount)
            {
                pauseEvents[1].WaitOne(); // Wait until the thread is not paused
                counters[1]++;
                string msg = $"Thread B: id {Thread.CurrentThread.ManagedThreadId} - Count: {counters[1]}/{maxCount}";
                PrintMsg(msg, Color.Green);
                Thread.Sleep(500); // Simulate work
            }
            PrintMsg("Thread B finished.", Color.Green);
            counters[1] = 0; // Reset counter for next run
            isRunning--;
            EnableButtons(btnThreadB, false);
            UpdateStatusButtonState(btnStatus);
        }

        public void Run()
        {
            Debug.Print("Multithreading other example started.");
            if(isRunning > 0)
            {
                Debug.Print("Threads Examples are already running.");
                return;
            }
            PrintMsg("starting ...");
            if (threadProcess[0] == null)
                threadProcess[0] = new ThreadStart(ThreadMethodOne);
            if (threadProcess[1] == null)
                threadProcess[1] = new ThreadStart(ThreadMethodTwo);

            // restart always require a new Thread instance and recreation
            threads[0] = new Thread(threadProcess[0]);
            threads[0].IsBackground = true;
            
            threads[1] = new Thread(threadProcess[1]);
            threads[1].IsBackground = true;
            
            threads[0].Start();
            threads[1].Start();
        }
    }
}
