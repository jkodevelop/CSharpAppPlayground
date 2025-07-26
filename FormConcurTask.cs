using CSharpAppPlayground.Multithreading.TasksExample;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace CSharpAppPlayground
{
    public class FormConcurTask : Form
    {
        private Button btnBack;
        private Button btnStartTaskSimple;
        private Button btnTaskExample01;
        private Button btnTaskExample02;
        private TextBox textboxConcur;

        public FormConcurTask()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            btnBack = new Button();
            textboxConcur = new TextBox();
            btnStartTaskSimple = new Button();
            btnTaskExample01 = new Button();
            btnTaskExample02 = new Button();
            SuspendLayout();
            // 
            // btnBack
            // 
            btnBack.Location = new Point(687, 335);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(111, 42);
            btnBack.TabIndex = 0;
            btnBack.Text = "Close";
            btnBack.Click += BtnBack_Click;
            // 
            // textboxConcur
            // 
            textboxConcur.Location = new Point(511, 24);
            textboxConcur.Multiline = true;
            textboxConcur.Name = "textboxConcur";
            textboxConcur.ScrollBars = ScrollBars.Vertical;
            textboxConcur.Size = new Size(287, 305);
            textboxConcur.TabIndex = 2;
            // 
            // btnStartTaskSimple
            // 
            btnStartTaskSimple.Location = new Point(12, 24);
            btnStartTaskSimple.Name = "btnStartTaskSimple";
            btnStartTaskSimple.Size = new Size(166, 29);
            btnStartTaskSimple.TabIndex = 3;
            btnStartTaskSimple.Text = "Start Task Simple";
            btnStartTaskSimple.UseVisualStyleBackColor = true;
            btnStartTaskSimple.Click += btnStartTaskSimple_Click;
            // 
            // btnTaskExample01
            // 
            btnTaskExample01.Location = new Point(12, 72);
            btnTaskExample01.Name = "btnTaskExample01";
            btnTaskExample01.Size = new Size(166, 32);
            btnTaskExample01.TabIndex = 4;
            btnTaskExample01.Text = "Task Example";
            btnTaskExample01.UseVisualStyleBackColor = true;
            btnTaskExample01.Click += btnTaskExample01_Click;
            // 
            // btnTaskExample02
            // 
            btnTaskExample02.Location = new Point(12, 124);
            btnTaskExample02.Name = "btnTaskExample02";
            btnTaskExample02.Size = new Size(164, 30);
            btnTaskExample02.TabIndex = 5;
            btnTaskExample02.Text = "Task Example 2";
            btnTaskExample02.UseVisualStyleBackColor = true;
            btnTaskExample02.Click += btnTaskExample02_Click;
            // 
            // FormConcurTask
            // 
            ClientSize = new Size(810, 389);
            Controls.Add(btnTaskExample02);
            Controls.Add(btnTaskExample01);
            Controls.Add(btnStartTaskSimple);
            Controls.Add(textboxConcur);
            Controls.Add(btnBack);
            Name = "FormConcurTask";
            Text = "Other Concurrency Examples";
            ResumeLayout(false);
            PerformLayout();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected TaskSimpleExample tse = new TaskSimpleExample();
        private void btnStartTaskSimple_Click(object sender, EventArgs e)
        {
            tse.Run();
        }

        protected TaskExample te = new TaskExample();
        private void btnTaskExample01_Click(object sender, EventArgs e)
        {
            te.ShowAsync().ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    Debug.Print($"Error: {t.Exception?.Message}");
                    // updateTextBox($"Error: {t.Exception?.Message}");
                }
                else
                {
                    Debug.Print("All tasks from example 1 completed successfully.");
                    // updateTextBox("All tasks completed successfully.");
                }
            });
        }

        protected TaskExampleDeadlock ted = new TaskExampleDeadlock();
        private void btnTaskExample02_Click(object sender, EventArgs e)
        {
            ted.ShowAsync().ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    Debug.Print($"Error: {t.Exception?.Message}");
                    // updateTextBox($"Error: {t.Exception?.Message}");
                }
                else
                {
                    Debug.Print("All tasks from example 2 completed successfully.");
                    // updateTextBox("All tasks completed successfully.");
                }
            });
        }
    }
} 