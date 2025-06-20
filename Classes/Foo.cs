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

        public void PrintBar()
        {
            Debug.Print(Bar); // Outputs to the Output window in Visual Studio
        }

    }
}
