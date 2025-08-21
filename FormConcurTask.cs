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
            tbasic = new TaskBasic();
            tse = new TaskSimpleExample(this);
            te = new TaskExample(this);
            ted = new TaskExampleDeadlock(this);

            ts = new TaskStoppable(this, btnCancel);
            tsm = new TaskStopMore(this, btnCancel1, btnCancel2, btnCancel3, btnCancelAll);

            tp = new TaskPausible(this, btnTask1Pause, btnTask2Pause);
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

        protected TaskSimpleExample tse;
        private void btnStartTaskSimple_Click(object sender, EventArgs e)
        {
            tse.ShowAsync();
        }

        protected TaskExample te;
        private async void btnTaskExampleA_Click(object sender, EventArgs e)
        {
            await te.ShowAsync();
            updateRichTextBoxMain($"All tasks from example 1 completed successfully.");
        }

        protected TaskExampleDeadlock ted;
        private async void btnTaskExampleB_Click(object sender, EventArgs e)
        {
            await ted.ShowAsync();
            updateRichTextBoxMain($"All tasks from example 2 completed successfully.");
        }

        protected TaskStoppable ts;
        private async void btnTasksStartStop_Click(object sender, EventArgs e)
        {
            await ts.ShowAsync();
            updateRichTextBoxMain($"All tasks from stoppable example completed successfully.");
        }

        protected TaskStopMore tsm;
        private async void btnTasksCancellable_Click(object sender, EventArgs e)
        {
            await tsm.ShowAsync();
            updateRichTextBoxMain($"All tasks from multi-stoppable example completed successfully.");
        }

        protected TaskPausible tp;
        private async void btnTasksStartPause_Click(object sender, EventArgs e)
        {
            await tp.ShowAsync();
            updateRichTextBoxMain($"All tasks from pausible example completed successfully.");
        }
    }
}