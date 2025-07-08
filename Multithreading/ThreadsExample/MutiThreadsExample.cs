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
            isRunning++;
            while (counters[0] < maxCount)
            {
                pauseEvents[0].WaitOne(); // Wait until the thread is not paused
                counters[0]++;
                string msg = $"Thread {Thread.CurrentThread.ManagedThreadId} - Count: {counters[0]}/{maxCount}";
                PrintMsg(msg);
                Thread.Sleep(500); // Simulate work
            }
            PrintMsg("Thread 1 finished.");
            isRunning--;
            counters[0] = 0; // Reset counter for next run
        }

        protected void ThreadMethodTwo()
        {
            isRunning++;
            while (counters[1] < maxCount)
            {
                pauseEvents[1].WaitOne(); // Wait until the thread is not paused
                counters[1]++;
                string msg = $"Thread {Thread.CurrentThread.ManagedThreadId} - Count: {counters[1]}/{maxCount}";
                PrintMsg(msg);
                Thread.Sleep(500); // Simulate work
            }
            PrintMsg("Thread 2 finished.");
            isRunning--;
            counters[1] = 0; // Reset counter for next run
        }

        public void Show()
        {
            Debug.Print("Multithreading other example started.");
            PrintMsg("starting ...");
            if(isRunning > 0)
            {
                Debug.Print("Threads Examples are already running.");
                return;
            }
            if (threadProcess[0] == null)
                threadProcess[0] = new ThreadStart(ThreadMethodOne);
            if (threadProcess[1] == null)
                threadProcess[1] = new ThreadStart(ThreadMethodTwo);
            if (threads[0] == null)
            {
                threads[0] = new Thread(threadProcess[0]);
                threads[0].IsBackground = true;
            }
            if(threads[1] == null)
            {
                threads[1] = new Thread(threadProcess[1]);
                threads[1].IsBackground = true;
            }
            threads[0].Start();
            threads[1].Start();
        }

    }
}
