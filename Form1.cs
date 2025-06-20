using CSharpAppPlayground.Classes;
using System.Diagnostics;

namespace CSharpAppPlayground
{
    public partial class Form1 : Form
    {

        private Foo f = new Foo("Hello From Bar();");
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("Hello, World!"); // outputs to console, not suitable for WinForms
            Debug.Print("Hello, World!"); // outputs to the Output window in Visual Studio
        }

        private void btnFoo_Click(object sender, EventArgs e)
        {
            f.PrintBar();
        }
    }
}
