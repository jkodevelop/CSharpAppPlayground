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
                var btnText = sender.GetType().GetProperty("Text").GetValue(sender)?.ToString() ?? string.Empty;
                var newText = btnText.Replace("Resume", "Pause");
                sender.GetType().GetProperty("Text").SetValue(sender, newText, null); // Replace 'Resume' with 'Pause'
            }
            else
            {
                isPaused[threadIndex] = true;
                pauseEvents[threadIndex].Reset(); // Pause the thread
                PrintMsg($"Thread {threadIndex + 1} paused.");
                var btnText = sender.GetType().GetProperty("Text").GetValue(sender)?.ToString() ?? string.Empty;
                var newText = btnText.Replace("Pause", "Resume");
                sender.GetType().GetProperty("Text").SetValue(sender, newText, null); // Replace 'Pause' with 'Resume'
            }
        }

        protected void PrintMsg(string msg)
        {
            if (f != null)
            {
                f.Invoke((MethodInvoker)(() => {
                    f.textboxMTE.Text += msg + Environment.NewLine; // make sure f.textboxMTE is not null and (public, default is private)
                }));
            }
            else
            {
                Debug.Print(msg);
            }
        }

        protected void ThreadMethodOne()
        {
            f.Invoke((MethodInvoker)(() => { f.btnThread1.Enabled = true; }));
            isRunning++;
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
            f.Invoke((MethodInvoker)(() => { f.btnThread1.Enabled = false; }));
        }

        protected void ThreadMethodTwo()
        {
            f.Invoke((MethodInvoker)(() => { f.btnThread2.Enabled = true; }));
            isRunning++;
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
            f.Invoke((MethodInvoker)(() => { f.btnThread2.Enabled = false; }));
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
