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
            groupBox1 = new GroupBox();
            btnParallelTaskSchedulerB = new Button();
            btnParallelTaskSchedulerC = new Button();
            btnParallelTaskSchedulerA = new Button();
            btnParallelLimit = new Button();
            groupBox1.SuspendLayout();
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
            btnParallelPrimeNumbers.Location = new Point(25, 80);
            btnParallelPrimeNumbers.Name = "btnParallelPrimeNumbers";
            btnParallelPrimeNumbers.Size = new Size(170, 34);
            btnParallelPrimeNumbers.TabIndex = 3;
            btnParallelPrimeNumbers.Text = "Prime Numbers (Parallel)";
            btnParallelPrimeNumbers.UseVisualStyleBackColor = true;
            btnParallelPrimeNumbers.Click += btnParallelPrimeNumbers_Click;
            // 
            // btnParallelPrimeNumCompare
            // 
            btnParallelPrimeNumCompare.Location = new Point(25, 132);
            btnParallelPrimeNumCompare.Name = "btnParallelPrimeNumCompare";
            btnParallelPrimeNumCompare.Size = new Size(170, 35);
            btnParallelPrimeNumCompare.TabIndex = 4;
            btnParallelPrimeNumCompare.Text = "Prime Numbers (linear)";
            btnParallelPrimeNumCompare.UseVisualStyleBackColor = true;
            btnParallelPrimeNumCompare.Click += btnParallelPrimeNumCompare_Click;
            // 
            // btnParallelInvoke
            // 
            btnParallelInvoke.Location = new Point(25, 185);
            btnParallelInvoke.Name = "btnParallelInvoke";
            btnParallelInvoke.Size = new Size(167, 34);
            btnParallelInvoke.TabIndex = 5;
            btnParallelInvoke.Text = "Parallel Invoke";
            btnParallelInvoke.UseVisualStyleBackColor = true;
            btnParallelInvoke.Click += btnParallelInvoke_Click;
            // 
            // btnParallelTaskScheduler
            // 
            btnParallelTaskScheduler.Location = new Point(6, 22);
            btnParallelTaskScheduler.Name = "btnParallelTaskScheduler";
            btnParallelTaskScheduler.Size = new Size(155, 34);
            btnParallelTaskScheduler.TabIndex = 6;
            btnParallelTaskScheduler.Text = "no TaskScheduler ";
            btnParallelTaskScheduler.UseVisualStyleBackColor = true;
            btnParallelTaskScheduler.Click += btnParallelTaskScheduler_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnParallelTaskSchedulerB);
            groupBox1.Controls.Add(btnParallelTaskSchedulerC);
            groupBox1.Controls.Add(btnParallelTaskSchedulerA);
            groupBox1.Controls.Add(btnParallelTaskScheduler);
            groupBox1.Location = new Point(25, 234);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(167, 204);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "SynchronizationContext";
            // 
            // btnParallelTaskSchedulerB
            // 
            btnParallelTaskSchedulerB.Location = new Point(6, 124);
            btnParallelTaskSchedulerB.Name = "btnParallelTaskSchedulerB";
            btnParallelTaskSchedulerB.Size = new Size(155, 34);
            btnParallelTaskSchedulerB.TabIndex = 9;
            btnParallelTaskSchedulerB.Text = "Thread + TaskScheduler ";
            btnParallelTaskSchedulerB.UseVisualStyleBackColor = true;
            btnParallelTaskSchedulerB.Click += btnParallelTaskSchedulerB_Click;
            // 
            // btnParallelTaskSchedulerC
            // 
            btnParallelTaskSchedulerC.Location = new Point(6, 164);
            btnParallelTaskSchedulerC.Name = "btnParallelTaskSchedulerC";
            btnParallelTaskSchedulerC.Size = new Size(155, 34);
            btnParallelTaskSchedulerC.TabIndex = 8;
            btnParallelTaskSchedulerC.Text = "New Thread Only";
            btnParallelTaskSchedulerC.UseVisualStyleBackColor = true;
            btnParallelTaskSchedulerC.Click += btnParallelTaskSchedulerC_Click;
            // 
            // btnParallelTaskSchedulerA
            // 
            btnParallelTaskSchedulerA.Location = new Point(6, 62);
            btnParallelTaskSchedulerA.Name = "btnParallelTaskSchedulerA";
            btnParallelTaskSchedulerA.Size = new Size(155, 34);
            btnParallelTaskSchedulerA.TabIndex = 7;
            btnParallelTaskSchedulerA.Text = "WITH TaskScheduler ";
            btnParallelTaskSchedulerA.UseVisualStyleBackColor = true;
            btnParallelTaskSchedulerA.Click += btnParallelTaskSchedulerA_Click;
            // 
            // btnParallelLimit
            // 
            btnParallelLimit.Location = new Point(213, 30);
            btnParallelLimit.Name = "btnParallelLimit";
            btnParallelLimit.Size = new Size(155, 33);
            btnParallelLimit.TabIndex = 8;
            btnParallelLimit.Text = "Limited Concurrency";
            btnParallelLimit.UseVisualStyleBackColor = true;
            btnParallelLimit.Click += btnParallelLimit_Click;
            // 
            // FormConcurParallel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnParallelLimit);
            Controls.Add(groupBox1);
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
            Controls.SetChildIndex(groupBox1, 0);
            Controls.SetChildIndex(btnParallelLimit, 0);
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnParallelExample01;
        private Button btnParallelPrimeNumbers;
        private Button btnParallelPrimeNumCompare;
        private Button btnParallelInvoke;
        private Button btnParallelTaskScheduler;
        private GroupBox groupBox1;
        private Button btnParallelTaskSchedulerA;
        private Button btnParallelTaskSchedulerB;
        private Button btnParallelTaskSchedulerC;
        private Button btnParallelLimit;
    }
}