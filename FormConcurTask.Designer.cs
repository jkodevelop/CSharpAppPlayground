namespace CSharpAppPlayground
{
    partial class FormConcurTask
    {
        private Button btnStartTaskSimple;
        private Button btnTaskExample01;
        private Button btnTaskExample02;
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
            btnStartTaskSimple = new Button();
            btnTaskExample01 = new Button();
            btnTaskExample02 = new Button();
            groupBox1 = new GroupBox();
            btnTask2Pause = new Button();
            btnTask1Pause = new Button();
            btnTasksStartPause = new Button();
            groupBox2 = new GroupBox();
            btnCancel = new Button();
            btnTasksStartStop = new Button();
            groupBox3 = new GroupBox();
            btnCancelAll = new Button();
            btnCancel3 = new Button();
            btnCancel2 = new Button();
            btnCancel1 = new Button();
            btnTasksCancellable = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
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
            // groupBox1
            // 
            groupBox1.Controls.Add(btnTask2Pause);
            groupBox1.Controls.Add(btnTask1Pause);
            groupBox1.Controls.Add(btnTasksStartPause);
            groupBox1.Location = new Point(365, 24);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(169, 115);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Tasks With Pause";
            // 
            // btnTask2Pause
            // 
            btnTask2Pause.Location = new Point(89, 57);
            btnTask2Pause.Name = "btnTask2Pause";
            btnTask2Pause.Size = new Size(74, 43);
            btnTask2Pause.TabIndex = 2;
            btnTask2Pause.Text = "Pause Task 2";
            btnTask2Pause.UseVisualStyleBackColor = true;
            // 
            // btnTask1Pause
            // 
            btnTask1Pause.Location = new Point(6, 57);
            btnTask1Pause.Name = "btnTask1Pause";
            btnTask1Pause.Size = new Size(77, 43);
            btnTask1Pause.TabIndex = 1;
            btnTask1Pause.Text = "Pause Task 1";
            btnTask1Pause.UseVisualStyleBackColor = true;
            // 
            // btnTasksStartPause
            // 
            btnTasksStartPause.Location = new Point(6, 22);
            btnTasksStartPause.Name = "btnTasksStartPause";
            btnTasksStartPause.Size = new Size(157, 23);
            btnTasksStartPause.TabIndex = 0;
            btnTasksStartPause.Text = "Start Tasks";
            btnTasksStartPause.UseVisualStyleBackColor = true;
            btnTasksStartPause.Click += btnTasksStartPause_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnCancel);
            groupBox2.Controls.Add(btnTasksStartStop);
            groupBox2.Location = new Point(190, 24);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(169, 102);
            groupBox2.TabIndex = 8;
            groupBox2.TabStop = false;
            groupBox2.Text = "Task with cancel";
            // 
            // btnCancel
            // 
            btnCancel.Enabled = false;
            btnCancel.Location = new Point(6, 65);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(157, 29);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel Task";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnTasksStartStop
            // 
            btnTasksStartStop.Location = new Point(6, 20);
            btnTasksStartStop.Name = "btnTasksStartStop";
            btnTasksStartStop.Size = new Size(157, 35);
            btnTasksStartStop.TabIndex = 0;
            btnTasksStartStop.Text = "Start";
            btnTasksStartStop.UseVisualStyleBackColor = true;
            btnTasksStartStop.Click += btnTasksStartStop_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(btnCancelAll);
            groupBox3.Controls.Add(btnCancel3);
            groupBox3.Controls.Add(btnCancel2);
            groupBox3.Controls.Add(btnCancel1);
            groupBox3.Controls.Add(btnTasksCancellable);
            groupBox3.Location = new Point(190, 132);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(169, 215);
            groupBox3.TabIndex = 9;
            groupBox3.TabStop = false;
            groupBox3.Text = "Tasks with Cancel";
            // 
            // btnCancelAll
            // 
            btnCancelAll.Enabled = false;
            btnCancelAll.Location = new Point(5, 171);
            btnCancelAll.Name = "btnCancelAll";
            btnCancelAll.Size = new Size(158, 38);
            btnCancelAll.TabIndex = 4;
            btnCancelAll.Text = "Cancel All";
            btnCancelAll.UseVisualStyleBackColor = true;
            // 
            // btnCancel3
            // 
            btnCancel3.Enabled = false;
            btnCancel3.Location = new Point(5, 131);
            btnCancel3.Name = "btnCancel3";
            btnCancel3.Size = new Size(158, 23);
            btnCancel3.TabIndex = 3;
            btnCancel3.Text = "Cancel Task 3";
            btnCancel3.UseVisualStyleBackColor = true;
            // 
            // btnCancel2
            // 
            btnCancel2.Enabled = false;
            btnCancel2.Location = new Point(5, 102);
            btnCancel2.Name = "btnCancel2";
            btnCancel2.Size = new Size(158, 23);
            btnCancel2.TabIndex = 2;
            btnCancel2.Text = "Cancel Task 2";
            btnCancel2.UseVisualStyleBackColor = true;
            // 
            // btnCancel1
            // 
            btnCancel1.Enabled = false;
            btnCancel1.Location = new Point(5, 73);
            btnCancel1.Name = "btnCancel1";
            btnCancel1.Size = new Size(158, 23);
            btnCancel1.TabIndex = 1;
            btnCancel1.Text = "Cancel Task 1";
            btnCancel1.UseVisualStyleBackColor = true;
            // 
            // btnTasksCancellable
            // 
            btnTasksCancellable.Location = new Point(6, 22);
            btnTasksCancellable.Name = "btnTasksCancellable";
            btnTasksCancellable.Size = new Size(157, 37);
            btnTasksCancellable.TabIndex = 0;
            btnTasksCancellable.Text = "Start Tasks";
            btnTasksCancellable.UseVisualStyleBackColor = true;
            btnTasksCancellable.Click += btnTasksCancellable_Click;
            // 
            // FormConcurTask
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            ClientSize = new Size(810, 462);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(btnTaskExample02);
            Controls.Add(btnTaskExample01);
            Controls.Add(btnStartTaskSimple);
            Name = "FormConcurTask";
            Text = "Other Concurrency Examples";
            Controls.SetChildIndex(btnStartTaskSimple, 0);
            Controls.SetChildIndex(btnTaskExample01, 0);
            Controls.SetChildIndex(btnTaskExample02, 0);
            Controls.SetChildIndex(groupBox1, 0);
            Controls.SetChildIndex(groupBox2, 0);
            Controls.SetChildIndex(groupBox3, 0);
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private GroupBox groupBox1;
        private Button btnTasksStartPause;
        private Button btnTask2Pause;
        private Button btnTask1Pause;
        private GroupBox groupBox2;
        private Button btnTasksStartStop;
        public Button btnCancel;
        private GroupBox groupBox3;
        private Button btnTasksCancellable;
        public Button btnCancelAll;
        public Button btnCancel3;
        public Button btnCancel2;
        public Button btnCancel1;
    }
}