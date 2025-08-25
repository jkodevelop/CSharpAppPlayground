namespace CSharpAppPlayground
{
    partial class FormConcurParallel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnParallelExample01 = new Button();
            btnParallelPrimeNumbers = new Button();
            btnParallelPrimeNumCompare = new Button();
            btnParallelInvoke = new Button();
            btnParallelTaskScheduler = new Button();
            SuspendLayout();
            // 
            // btnParallelExample01
            // 
            btnParallelExample01.Location = new Point(25, 30);
            btnParallelExample01.Name = "btnParallelExample01";
            btnParallelExample01.Size = new Size(170, 33);
            btnParallelExample01.TabIndex = 0;
            btnParallelExample01.Text = "Parallel Example";
            btnParallelExample01.UseVisualStyleBackColor = true;
            btnParallelExample01.Click += btnParallelExample01_Click;
            // 
            // btnParallelPrimeNumbers
            // 
            btnParallelPrimeNumbers.Location = new Point(25, 83);
            btnParallelPrimeNumbers.Name = "btnParallelPrimeNumbers";
            btnParallelPrimeNumbers.Size = new Size(170, 34);
            btnParallelPrimeNumbers.TabIndex = 3;
            btnParallelPrimeNumbers.Text = "Prime Numbers (Parallel)";
            btnParallelPrimeNumbers.UseVisualStyleBackColor = true;
            btnParallelPrimeNumbers.Click += btnParallelPrimeNumbers_Click;
            // 
            // btnParallelPrimeNumCompare
            // 
            btnParallelPrimeNumCompare.Location = new Point(25, 138);
            btnParallelPrimeNumCompare.Name = "btnParallelPrimeNumCompare";
            btnParallelPrimeNumCompare.Size = new Size(170, 35);
            btnParallelPrimeNumCompare.TabIndex = 4;
            btnParallelPrimeNumCompare.Text = "Prime Numbers (linear)";
            btnParallelPrimeNumCompare.UseVisualStyleBackColor = true;
            btnParallelPrimeNumCompare.Click += btnParallelPrimeNumCompare_Click;
            // 
            // btnParallelInvoke
            // 
            btnParallelInvoke.Location = new Point(25, 195);
            btnParallelInvoke.Name = "btnParallelInvoke";
            btnParallelInvoke.Size = new Size(167, 34);
            btnParallelInvoke.TabIndex = 5;
            btnParallelInvoke.Text = "Parallel Invoke";
            btnParallelInvoke.UseVisualStyleBackColor = true;
            btnParallelInvoke.Click += btnParallelInvoke_Click;
            // 
            // btnParallelTaskScheduler
            // 
            btnParallelTaskScheduler.Location = new Point(25, 251);
            btnParallelTaskScheduler.Name = "btnParallelTaskScheduler";
            btnParallelTaskScheduler.Size = new Size(167, 34);
            btnParallelTaskScheduler.TabIndex = 6;
            btnParallelTaskScheduler.Text = "TaskScheduler ";
            btnParallelTaskScheduler.UseVisualStyleBackColor = true;
            btnParallelTaskScheduler.Click += btnParallelTaskScheduler_Click;
            // 
            // FormConcurParallel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnParallelTaskScheduler);
            Controls.Add(btnParallelInvoke);
            Controls.Add(btnParallelPrimeNumCompare);
            Controls.Add(btnParallelPrimeNumbers);
            Controls.Add(btnParallelExample01);
            Name = "FormConcurParallel";
            Text = "FormConcurParallel";
            Controls.SetChildIndex(btnParallelExample01, 0);
            Controls.SetChildIndex(btnParallelPrimeNumbers, 0);
            Controls.SetChildIndex(btnParallelPrimeNumCompare, 0);
            Controls.SetChildIndex(btnParallelInvoke, 0);
            Controls.SetChildIndex(btnParallelTaskScheduler, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnParallelExample01;
        private Button btnParallelPrimeNumbers;
        private Button btnParallelPrimeNumCompare;
        private Button btnParallelInvoke;
        private Button btnParallelTaskScheduler;
    }
}