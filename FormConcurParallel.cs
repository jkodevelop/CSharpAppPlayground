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
            ppn = new ParallelPrimeNumbers(this);
            ppnCompare = new ParallelPrimeNumbers(this);
            pvi = new ParallelInvoke(this);
            pts = new ParallelTaskScheduler(this);
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

        protected ParallelPrimeNumbers ppn;
        private void btnParallelPrimeNumbers_Click(object sender, EventArgs e)
        {
            updateLabelMain("Using Parallel to get prime numbers...");
            ppn.Run();
            updateLabelMain("DONE");
        }

        protected ParallelPrimeNumbers ppnCompare;
        private void btnParallelPrimeNumCompare_Click(object sender, EventArgs e)
        {
            updateLabelMain("NOT! using Parallel to get prime numbers...");
            ppnCompare.Compare();
            updateLabelMain("DONE");
        }

        protected ParallelInvoke pvi;
        private void btnParallelInvoke_Click(object sender, EventArgs e)
        {
            pvi.Run();
        }

        protected ParallelTaskScheduler pts;
        private void btnParallelTaskScheduler_Click(object sender, EventArgs e)
        {
            updateLabelMain("Using Parallel with Task Scheduler...");
            pts.Run();
            updateLabelMain("DONE");
        }
    }
}
