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
        protected Form1 f;
        public MutiThreadsExample(Form1 _f)
        {
            f = _f;
        }

        public void PrintMsg(string msg)
        {
            if(f != null)
            {
                // make sure f.textboxMain is not null and (public, default is private)
                f.textboxMain.Text += msg + Environment.NewLine;
            }
            else
            {
                Debug.Print(msg);
            }
        }

        public void Show()
        {
            Debug.Print("Multithreading example started.");
            PrintMsg("ok");
        }
    }
}
