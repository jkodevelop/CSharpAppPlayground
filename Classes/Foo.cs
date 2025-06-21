using MethodTimer;
using System.Diagnostics;

namespace CSharpAppPlayground.Classes
{
    public class Foo
    {

        public string Bar { get; set; }

        public Foo(string? bar = null)
        {
            Bar = bar ?? "Hello, World!";
        }

        [Time("Message passed: {msg}")]
        public void PrintBar(string? msg = "")
        {
            if(msg == "") msg = Bar;
            Debug.Print(Bar); // Outputs to the Output window in Visual Studio
        }

    }
}
