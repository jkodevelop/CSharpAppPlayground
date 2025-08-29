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
            pl = new ParallelLimit(this);
            pwp = new ParallelWithPause(this, btnParallelPause, btnParallelStop);
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
            updateLabelMain("Using Parallel with not using Task Scheduler...");
            pts.Run();
            updateLabelMain("DONE");
        }

        private void btnParallelTaskSchedulerA_Click(object sender, EventArgs e)
        {
            updateLabelMain("Using Parallel with Task Scheduler...");
            pts.RunWithTaskScheduler();
            updateLabelMain("DONE");
        }

        private void btnParallelTaskSchedulerB_Click(object sender, EventArgs e)
        {
            updateLabelMain("Using Parallel with a Separate Thread + Task Scheduler...");
            pts.RunOnSpecifiedThread(true);
            updateLabelMain("DONE");
        }

        private void btnParallelTaskSchedulerC_Click(object sender, EventArgs e)
        {
            updateLabelMain("Using Parallel with a Separate Thread without Task Scheduler...");
            pts.RunOnSpecifiedThread(false);
            updateLabelMain("DONE");
        }


        protected ParallelLimit pl;
        private void btnParallelLimit_Click(object sender, EventArgs e)
        {
            pl.Run();
        }

        protected ParallelWithPause pwp;
        private void btnParallelRun_Click(object sender, EventArgs e)
        {
            pwp.Run();
        }

        private void btnParallelControlLimit_Click(object sender, EventArgs e)
        {

        }
    }
}
