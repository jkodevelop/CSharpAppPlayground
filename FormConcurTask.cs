using CSharpAppPlayground.Concurrency.TasksExample;
using CSharpAppPlayground.UIClasses;

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
            tsimp = new TaskSimple();
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
            await tsm.ShowAsync().ContinueWith(t =>
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

        protected TaskSimple tsimp;
        private async void btnTaskSimpleAlt_Click(object sender, EventArgs e)
        {
            updateLabelMain("Starting simple alt task");

            // 1
            // Task<string> result = tsimp.ProcessOrderAsync(1); // DOES NOT DELAY

            // 2
            // string result = tsimp.ProcessOrderAsync(1).Result; // DEADLOCK

            // 3 
            string result = await tsimp.ProcessOrderAsync(1); // Use await to avoid deadlock and get the result

            updateRichTextBoxMain($"Simple alt task completed with result: {result}");
        }
    }
}