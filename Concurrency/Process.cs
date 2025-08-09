using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAppPlayground.Concurrency
{
    public class Process
    {
        public void Run()
        {
            for (int i = 0; i < 10; i++)
            {
                string msg = $"Thread {Thread.CurrentThread.ManagedThreadId} - Count: {i}";
                Thread.Sleep(500); // Simulate work, 500 milliseconds
            }
        }
    }
}
