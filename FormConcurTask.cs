using CSharpAppPlayground.Concurrency.TasksExample;
using CSharpAppPlayground.UIClasses;
using System.Windows.Forms.Design;

namespace CSharpAppPlayground
{
    public partial class FormConcurTask : FormWithRichText
    {
        public FormConcurTask()
        {
            InitializeComponent();
            InitObjects();
        }

        private void InitObjects()
        {
            tse = new TaskSimpleExample(this);
            te = new TaskExample(this);
            ted = new TaskExampleDeadlock(this);
            tp = new TaskPausible(this, btnTask1Pause, btnTask2Pause);
            ts = new TaskStoppable(this, btnCancel);
            tsm = new TaskStopMore(this, btnCancel1, btnCancel2, btnCancel3, btnCancelAll);
            tbasic = new TaskBasic();
        }

        protected TaskSimpleExample tse;
        private void btnStartTaskSimple_Click(object sender, EventArgs e)
        {
            tse.ShowAsync();
        }

        protected TaskExample te;
        private void btnTaskExample01_Click(object sender, EventArgs e)
        {
            te.ShowAsync().ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    updateRichTextBoxMain($"Error: {t.Exception?.Message}");
                }
                else
                {
                    updateRichTextBoxMain($"All tasks from example 1 completed successfully.");
                }
            });
        }

        protected TaskExampleDeadlock ted;
        private void btnTaskExample02_Click(object sender, EventArgs e)
        {
            ted.ShowAsync().ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    updateRichTextBoxMain($"Error: {t.Exception?.Message}");
                }
                else
                {
                    updateRichTextBoxMain($"All tasks from example 2 completed successfully.");
                }
            });
        }

        protected TaskPausible tp;
        private void btnTasksStartPause_Click(object sender, EventArgs e)
        {
            tp.ShowAsync().ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    updateRichTextBoxMain($"Error: {t.Exception?.Message}");
                }
                else
                {
                    updateRichTextBoxMain($"All tasks from example 3 completed successfully.");
                }
            });
        }

        protected TaskStoppable ts;
        private async void btnTasksStartStop_Click(object sender, EventArgs e)
        {
            await ts.ShowAsync().ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    updateRichTextBoxMain($"Error: {t.Exception?.Message}");
                }
                else
                {
                    updateRichTextBoxMain($"All tasks from example 3 completed successfully.");
                }
            });
        }

        protected TaskStopMore tsm;
        private async void btnTasksCancellable_Click(object sender, EventArgs e)
        {
            await tsm.ShowAsync();
            updateRichTextBoxMain($"All tasks from example 3 completed successfully.");
        }

        protected TaskBasic tbasic;
        private async void btnTaskBasic_Click(object sender, EventArgs e)
        {
            updateLabelMain("Starting simple alt task");

            // 1 - DOES NOT DELAY
            // Task<string> result = tsimp.ShowAsync(); // DOES NOT DELAY

            // 2 - DEADLOCK, because ShowAsync() has an await Task.Delay() inside
            // string result = tsimp.ShowAsync().Result; // DEADLOCK

            // 3 - await all the way down
            string result = await tbasic.ShowAsync(); // Use await to avoid deadlock and get the result

            updateRichTextBoxMain($"Simple alt task completed with result: {result}");

            // Example of handling a faulted task
            await tbasic.SimulateFaultedTask().ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    updateRichTextBoxMain($"Error in faulted task: {t.Exception?.Message}");
                }
                else
                {
                    updateRichTextBoxMain("Faulted task completed successfully.");
                }
            });
        }
    }
}