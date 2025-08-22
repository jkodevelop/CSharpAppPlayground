using CSharpAppPlayground.Concurrency.ParallelExample;
using CSharpAppPlayground.UIClasses;
using System.Diagnostics;

namespace CSharpAppPlayground
{
    public partial class FormConcurParallel : FormWithRichText
    {
        public FormConcurParallel()
        {
            InitializeComponent();
            pe = new ParallelExample(this);
        }

        protected ParallelExample pe;
        private void btnParallelExample01_Click(object sender, EventArgs e)
        {
            updateRichTextBoxMain("testing richtext box");
            updateLabelMain("testing label");
            Debug.Print("Starting Parallel Example...");
            pe.Run();
            Debug.Print("DONE");
        }

    }
}
