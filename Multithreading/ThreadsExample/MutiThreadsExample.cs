using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAppPlayground.Multithreading.ThreadsExample
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

        protected Form1 f;

        public MutiThreadsExample(Form1 _f)
        {
            f = _f;
            pauseEvents[0] = new ManualResetEvent(true); // Thread 1
            pauseEvents[1] = new ManualResetEvent(true); // Thread 2
            f.btnThread1.Click += (sender, e) => PausePressedThread(sender, 0);
            f.btnThread2.Click += (sender, e) => PausePressedThread(sender, 1);
            f.btnStatus.Click += (sender, e) => GetThreadsStatus(sender);
        }

        protected void PausePressedThread(object sender, int threadIndex)
        {
            if (threadIndex < 0 || threadIndex >= pauseEvents.Length)
                throw new ArgumentOutOfRangeException(nameof(threadIndex), "Invalid thread index.");

            if (isPaused[threadIndex])
            {
                pauseEvents[threadIndex].Set(); // Resume the thread
                isPaused[threadIndex] = false;
                PrintMsg($"Thread {threadIndex + 1} resumed.");
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
                PrintMsg($"Thread {threadIndex + 1} paused.");
                string btnText = (sender as Button)?.Text ?? string.Empty;
                string newText = btnText.Replace("Pause", "Resume");
                (sender as Button).Text = newText; // Replace 'Resume' with 'Pause'
            }
        }

        protected void GetThreadsStatus(object sender) {
            string threadStatusSummary = "";
            if(threads[0] is not null)
            {
                threadStatusSummary += (threads[0].IsAlive ? "Thread 1 is alive. " : "Thread 1 is done. ");
            }
            if(threads[1] is not null)
            {
                threadStatusSummary += (threads[1].IsAlive ? "Thread 2 is alive." : "Thread 2 is done.");
            }
            string summary = $"Status: {threadStatusSummary}";
            Debug.Print(summary + Environment.NewLine);
            UpdateStatusLabel(summary);
        }

        protected void UpdateStatusLabel(string msg)
        {
            // make sure f.lblThreads is not null and (public, default is private)
            if (f != null)
                f.Invoke((MethodInvoker)(() => { f.lblThreads.Text = msg; }));
            else
                Debug.Print(msg);
        }
        protected void PrintMsg(string msg)
        {
            // make sure f.textboxMTE is not null and (public, default is private)
            if (f != null)
                f.Invoke((MethodInvoker)(() => { f.textboxMTE.Text += msg + Environment.NewLine; }));
            else
                Debug.Print(msg);
        }

        private void EnableButtons(Button btn, bool enable)
        {
            f.Invoke((MethodInvoker)(() => {
                btn.Enabled = enable;
            }));
        }

        private void UpdateStatusButtonState(Button btn)
        {
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
            EnableButtons(f.btnThread1, true);
            UpdateStatusButtonState(f.btnStatus);
            while (counters[0] < maxCount)
            {
                pauseEvents[0].WaitOne(); // Wait until the thread is not paused
                counters[0]++;
                string msg = $"Thead 1: id {Thread.CurrentThread.ManagedThreadId} - Count: {counters[0]}/{maxCount}";
                PrintMsg(msg);
                Thread.Sleep(500); // Simulate work
            }
            PrintMsg("Thread 1 finished.");
            counters[0] = 0; // Reset counter for next run
            isRunning--;
            EnableButtons(f.btnThread1, false);
            UpdateStatusButtonState(f.btnStatus);
        }

        protected void ThreadMethodTwo()
        {
            isRunning++;
            EnableButtons(f.btnThread2, true);
            UpdateStatusButtonState(f.btnStatus);
            while (counters[1] < maxCount)
            {
                pauseEvents[1].WaitOne(); // Wait until the thread is not paused
                counters[1]++;
                string msg = $"Thread 2: id {Thread.CurrentThread.ManagedThreadId} - Count: {counters[1]}/{maxCount}";
                PrintMsg(msg);
                Thread.Sleep(500); // Simulate work
            }
            PrintMsg("Thread 2 finished.");
            counters[1] = 0; // Reset counter for next run
            isRunning--;
            EnableButtons(f.btnThread2, false);
            UpdateStatusButtonState(f.btnStatus);
        }

        public void Show()
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
