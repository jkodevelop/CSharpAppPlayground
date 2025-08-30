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
            groupBox2 = new GroupBox();
            btnParallelStop = new Button();
            btnParallelPause = new Button();
            btnParallelRun = new Button();
            groupBox3 = new GroupBox();
            btnParallelStopAlt = new Button();
            btnParallelPauseAlt = new Button();
            btnParallelRunAlt = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // btnParallelExample01
            // 
            btnParallelExample01.Location = new Point(12, 12);
            btnParallelExample01.Name = "btnParallelExample01";
            btnParallelExample01.Size = new Size(170, 33);
            btnParallelExample01.TabIndex = 0;
            btnParallelExample01.Text = "Parallel Example";
            btnParallelExample01.UseVisualStyleBackColor = true;
            btnParallelExample01.Click += btnParallelExample01_Click;
            // 
            // btnParallelPrimeNumbers
            // 
            btnParallelPrimeNumbers.Location = new Point(12, 62);
            btnParallelPrimeNumbers.Name = "btnParallelPrimeNumbers";
            btnParallelPrimeNumbers.Size = new Size(170, 34);
            btnParallelPrimeNumbers.TabIndex = 3;
            btnParallelPrimeNumbers.Text = "Prime Numbers (Parallel)";
            btnParallelPrimeNumbers.UseVisualStyleBackColor = true;
            btnParallelPrimeNumbers.Click += btnParallelPrimeNumbers_Click;
            // 
            // btnParallelPrimeNumCompare
            // 
            btnParallelPrimeNumCompare.Location = new Point(12, 114);
            btnParallelPrimeNumCompare.Name = "btnParallelPrimeNumCompare";
            btnParallelPrimeNumCompare.Size = new Size(170, 35);
            btnParallelPrimeNumCompare.TabIndex = 4;
            btnParallelPrimeNumCompare.Text = "Prime Numbers (linear)";
            btnParallelPrimeNumCompare.UseVisualStyleBackColor = true;
            btnParallelPrimeNumCompare.Click += btnParallelPrimeNumCompare_Click;
            // 
            // btnParallelInvoke
            // 
            btnParallelInvoke.Location = new Point(12, 167);
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
            groupBox1.Location = new Point(188, 12);
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
            btnParallelLimit.Location = new Point(12, 223);
            btnParallelLimit.Name = "btnParallelLimit";
            btnParallelLimit.Size = new Size(167, 33);
            btnParallelLimit.TabIndex = 8;
            btnParallelLimit.Text = "Limited Concurrency";
            btnParallelLimit.UseVisualStyleBackColor = true;
            btnParallelLimit.Click += btnParallelLimit_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnParallelStop);
            groupBox2.Controls.Add(btnParallelPause);
            groupBox2.Controls.Add(btnParallelRun);
            groupBox2.Location = new Point(361, 176);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(167, 149);
            groupBox2.TabIndex = 9;
            groupBox2.TabStop = false;
            groupBox2.Text = "Pause/Stop Example";
            // 
            // btnParallelStop
            // 
            btnParallelStop.Enabled = false;
            btnParallelStop.Location = new Point(6, 109);
            btnParallelStop.Name = "btnParallelStop";
            btnParallelStop.Size = new Size(155, 35);
            btnParallelStop.TabIndex = 2;
            btnParallelStop.Text = "Stop";
            btnParallelStop.UseVisualStyleBackColor = true;
            // 
            // btnParallelPause
            // 
            btnParallelPause.Enabled = false;
            btnParallelPause.Location = new Point(6, 64);
            btnParallelPause.Name = "btnParallelPause";
            btnParallelPause.Size = new Size(155, 30);
            btnParallelPause.TabIndex = 1;
            btnParallelPause.Text = "Pause";
            btnParallelPause.UseVisualStyleBackColor = true;
            // 
            // btnParallelRun
            // 
            btnParallelRun.Location = new Point(6, 22);
            btnParallelRun.Name = "btnParallelRun";
            btnParallelRun.Size = new Size(155, 33);
            btnParallelRun.TabIndex = 0;
            btnParallelRun.Text = "Run";
            btnParallelRun.UseVisualStyleBackColor = true;
            btnParallelRun.Click += btnParallelRun_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(btnParallelStopAlt);
            groupBox3.Controls.Add(btnParallelPauseAlt);
            groupBox3.Controls.Add(btnParallelRunAlt);
            groupBox3.Location = new Point(361, 12);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(167, 149);
            groupBox3.TabIndex = 10;
            groupBox3.TabStop = false;
            groupBox3.Text = "Pause/Stop Alt Example";
            // 
            // btnParallelStopAlt
            // 
            btnParallelStopAlt.Enabled = false;
            btnParallelStopAlt.Location = new Point(6, 109);
            btnParallelStopAlt.Name = "btnParallelStopAlt";
            btnParallelStopAlt.Size = new Size(155, 35);
            btnParallelStopAlt.TabIndex = 2;
            btnParallelStopAlt.Text = "Stop";
            btnParallelStopAlt.UseVisualStyleBackColor = true;
            // 
            // btnParallelPauseAlt
            // 
            btnParallelPauseAlt.Enabled = false;
            btnParallelPauseAlt.Location = new Point(6, 64);
            btnParallelPauseAlt.Name = "btnParallelPauseAlt";
            btnParallelPauseAlt.Size = new Size(155, 30);
            btnParallelPauseAlt.TabIndex = 1;
            btnParallelPauseAlt.Text = "Pause";
            btnParallelPauseAlt.UseVisualStyleBackColor = true;
            // 
            // btnParallelRunAlt
            // 
            btnParallelRunAlt.Location = new Point(6, 22);
            btnParallelRunAlt.Name = "btnParallelRunAlt";
            btnParallelRunAlt.Size = new Size(155, 33);
            btnParallelRunAlt.TabIndex = 0;
            btnParallelRunAlt.Text = "Run";
            btnParallelRunAlt.UseVisualStyleBackColor = true;
            btnParallelRunAlt.Click += btnParallelRunAlt_Click;
            // 
            // FormConcurParallel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
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
            Controls.SetChildIndex(groupBox2, 0);
            Controls.SetChildIndex(groupBox3, 0);
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
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
        private GroupBox groupBox2;
        private Button btnParallelStop;
        private Button btnParallelPause;
        private Button btnParallelRun;
        private GroupBox groupBox3;
        private Button btnParallelStopAlt;
        private Button btnParallelPauseAlt;
        private Button btnParallelRunAlt;
    }
}