using CSharpAppPlayground.Multithreading.TasksExample;
using System;
using System.Windows.Forms;

namespace CSharpAppPlayground
{
    public class Form2 : Form
    {
        private Button btnBack;
        private Button btnStartTaskSimple;
        private TextBox textboxConcur;

        public Form2()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            btnBack = new Button();
            textboxConcur = new TextBox();
            btnStartTaskSimple = new Button();
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
            // Form2
            // 
            ClientSize = new Size(810, 389);
            Controls.Add(btnStartTaskSimple);
            Controls.Add(textboxConcur);
            Controls.Add(btnBack);
            Name = "Form2";
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
    }
} 