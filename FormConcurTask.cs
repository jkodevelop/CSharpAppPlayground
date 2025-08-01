using CSharpAppPlayground.Multithreading.TasksExample;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace CSharpAppPlayground
{
    public partial class FormConcurTask : Form
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
            tp = new TaskPausible(this);
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void updateRichTextBoxMain(string msg, Color lineColor = default)
        {
            if (this.IsDisposed || this.Disposing)
            {
                // Form is disposed or disposing, do not attempt to update UI
                Debug.Print("Form is disposed or disposing, skipping updateRichTextBoxMain.");
                return;
            }
            if (InvokeRequired)
            {
                try
                {
                    Debug.Print("InvokeRequired for updateRichTextBoxMain().");
                    Invoke(new Action<string, Color>(updateRichTextBoxMain), msg, lineColor);
                }
                catch (ObjectDisposedException)
                {
                    Debug.Print("Invoke failed: Form is disposed.");
                }
                catch (InvalidOperationException)
                {
                    Debug.Print("Invoke failed: Form is disposed or handle is invalid.");
                }
            }
            else
            {
                if (richTBoxMain != null && !richTBoxMain.IsDisposed)
                {
                    richTBoxMain.SelectionColor = lineColor;
                    richTBoxMain.AppendText(msg + Environment.NewLine);
                    richTBoxMain.Refresh(); // Force the rich text box to refresh immediately
                }
                else
                {
                    Debug.Print(msg);
                }
            }
        }

        protected TaskSimpleExample tse;
        private void btnStartTaskSimple_Click(object sender, EventArgs e)
        {
            tse.Run();
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

        }
    }
}