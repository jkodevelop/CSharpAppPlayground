using CSharpAppPlayground.Multithreading.TasksExample;
using CSharpAppPlayground.UIClasses;
using System;
using System.Diagnostics;
using System.Windows.Forms;

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
    }
}