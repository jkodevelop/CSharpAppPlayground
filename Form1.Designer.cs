namespace CSharpAppPlayground
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnRun = new Button();
            btnFoo = new Button();
            btnDI = new Button();
            btnGeneric = new Button();
            btnThreads = new Button();
            textboxMain = new TextBox();
            lblMain = new Label();
            btnStartThreads = new Button();
            grpBox1 = new GroupBox();
            lblThreads = new Label();
            btnStatus = new Button();
            textboxMTE = new TextBox();
            btnThread2 = new Button();
            btnThread1 = new Button();
            btnConcurTask = new Button();
            btnUIForm = new Button();
            btnConcurThread = new Button();
            pageSetupDialog1 = new PageSetupDialog();
            btnConcurParallel = new Button();
            grpBox1.SuspendLayout();
            SuspendLayout();
            // 
            // btnRun
            // 
            btnRun.Location = new Point(26, 28);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(197, 104);
            btnRun.TabIndex = 0;
            btnRun.Text = "RUN";
            btnRun.UseVisualStyleBackColor = true;
            btnRun.Click += btnRun_Click;
            // 
            // btnFoo
            // 
            btnFoo.Location = new Point(26, 163);
            btnFoo.Name = "btnFoo";
            btnFoo.Size = new Size(197, 35);
            btnFoo.TabIndex = 1;
            btnFoo.Text = "Foo";
            btnFoo.UseVisualStyleBackColor = true;
            btnFoo.Click += btnFoo_Click;
            // 
            // btnDI
            // 
            btnDI.Location = new Point(26, 204);
            btnDI.Name = "btnDI";
            btnDI.Size = new Size(197, 40);
            btnDI.TabIndex = 2;
            btnDI.Text = "Dependency Injection Examples";
            btnDI.UseVisualStyleBackColor = true;
            btnDI.Click += btnDI_Click;
            // 
            // btnGeneric
            // 
            btnGeneric.Location = new Point(26, 250);
            btnGeneric.Name = "btnGeneric";
            btnGeneric.Size = new Size(197, 33);
            btnGeneric.TabIndex = 3;
            btnGeneric.Text = "Generic Types";
            btnGeneric.UseVisualStyleBackColor = true;
            btnGeneric.Click += btnGeneric_Click;
            // 
            // btnThreads
            // 
            btnThreads.Location = new Point(247, 28);
            btnThreads.Name = "btnThreads";
            btnThreads.Size = new Size(237, 34);
            btnThreads.TabIndex = 4;
            btnThreads.Text = "Multi-Threading Simple";
            btnThreads.UseVisualStyleBackColor = true;
            btnThreads.Click += btnThreads_Click;
            // 
            // textboxMain
            // 
            textboxMain.Location = new Point(247, 87);
            textboxMain.Multiline = true;
            textboxMain.Name = "textboxMain";
            textboxMain.ScrollBars = ScrollBars.Both;
            textboxMain.Size = new Size(237, 128);
            textboxMain.TabIndex = 6;
            // 
            // lblMain
            // 
            lblMain.AutoSize = true;
            lblMain.Location = new Point(247, 69);
            lblMain.Name = "lblMain";
            lblMain.Size = new Size(32, 15);
            lblMain.TabIndex = 7;
            lblMain.Text = "label";
            // 
            // btnStartThreads
            // 
            btnStartThreads.Location = new Point(6, 22);
            btnStartThreads.Name = "btnStartThreads";
            btnStartThreads.Size = new Size(172, 34);
            btnStartThreads.TabIndex = 8;
            btnStartThreads.Text = "1. Start Threads";
            btnStartThreads.UseVisualStyleBackColor = true;
            btnStartThreads.Click += btnStartThreads_Click;
            // 
            // grpBox1
            // 
            grpBox1.Controls.Add(lblThreads);
            grpBox1.Controls.Add(btnStatus);
            grpBox1.Controls.Add(textboxMTE);
            grpBox1.Controls.Add(btnThread2);
            grpBox1.Controls.Add(btnThread1);
            grpBox1.Controls.Add(btnStartThreads);
            grpBox1.Location = new Point(524, 28);
            grpBox1.Name = "grpBox1";
            grpBox1.Size = new Size(264, 391);
            grpBox1.TabIndex = 9;
            grpBox1.TabStop = false;
            grpBox1.Text = "More MultiThreading Examples";
            // 
            // lblThreads
            // 
            lblThreads.AutoSize = true;
            lblThreads.Location = new Point(10, 65);
            lblThreads.Name = "lblThreads";
            lblThreads.Size = new Size(42, 15);
            lblThreads.TabIndex = 13;
            lblThreads.Text = "Status:";
            // 
            // btnStatus
            // 
            btnStatus.Enabled = false;
            btnStatus.Location = new Point(184, 22);
            btnStatus.Name = "btnStatus";
            btnStatus.Size = new Size(74, 34);
            btnStatus.TabIndex = 12;
            btnStatus.Text = "status";
            btnStatus.UseVisualStyleBackColor = true;
            // 
            // textboxMTE
            // 
            textboxMTE.Location = new Point(6, 129);
            textboxMTE.Multiline = true;
            textboxMTE.Name = "textboxMTE";
            textboxMTE.ScrollBars = ScrollBars.Both;
            textboxMTE.Size = new Size(252, 122);
            textboxMTE.TabIndex = 11;
            // 
            // btnThread2
            // 
            btnThread2.Enabled = false;
            btnThread2.Location = new Point(133, 88);
            btnThread2.Name = "btnThread2";
            btnThread2.Size = new Size(125, 23);
            btnThread2.TabIndex = 10;
            btnThread2.Text = "Pause Thread 2";
            btnThread2.UseVisualStyleBackColor = true;
            // 
            // btnThread1
            // 
            btnThread1.Enabled = false;
            btnThread1.Location = new Point(6, 88);
            btnThread1.Name = "btnThread1";
            btnThread1.Size = new Size(121, 23);
            btnThread1.TabIndex = 9;
            btnThread1.Text = "Pause Thread 1";
            btnThread1.UseVisualStyleBackColor = true;
            // 
            // btnConcurTask
            // 
            btnConcurTask.Location = new Point(26, 367);
            btnConcurTask.Name = "btnConcurTask";
            btnConcurTask.Size = new Size(197, 33);
            btnConcurTask.TabIndex = 16;
            btnConcurTask.Text = "Task()";
            btnConcurTask.UseVisualStyleBackColor = true;
            btnConcurTask.Click += btnConcurTask_Click;
            // 
            // btnUIForm
            // 
            btnUIForm.Location = new Point(26, 289);
            btnUIForm.Name = "btnUIForm";
            btnUIForm.Size = new Size(197, 35);
            btnUIForm.TabIndex = 18;
            btnUIForm.Text = "WinForms UI";
            btnUIForm.UseVisualStyleBackColor = true;
            btnUIForm.Click += btnUIForm_Click;
            // 
            // btnConcurThread
            // 
            btnConcurThread.Location = new Point(26, 331);
            btnConcurThread.Name = "btnConcurThread";
            btnConcurThread.Size = new Size(197, 30);
            btnConcurThread.TabIndex = 19;
            btnConcurThread.Text = "Thread()";
            btnConcurThread.UseVisualStyleBackColor = true;
            btnConcurThread.Click += btnConcurThread_Click;
            // 
            // btnConcurParallel
            // 
            btnConcurParallel.Location = new Point(247, 367);
            btnConcurParallel.Name = "btnConcurParallel";
            btnConcurParallel.Size = new Size(197, 33);
            btnConcurParallel.TabIndex = 20;
            btnConcurParallel.Text = "Parallel()";
            btnConcurParallel.UseVisualStyleBackColor = true;
            btnConcurParallel.Click += btnConcurParallel_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnConcurParallel);
            Controls.Add(btnConcurThread);
            Controls.Add(btnUIForm);
            Controls.Add(btnConcurTask);
            Controls.Add(lblMain);
            Controls.Add(textboxMain);
            Controls.Add(btnThreads);
            Controls.Add(btnGeneric);
            Controls.Add(btnDI);
            Controls.Add(btnFoo);
            Controls.Add(btnRun);
            Controls.Add(grpBox1);
            Name = "Form1";
            Text = "Form1";
            grpBox1.ResumeLayout(false);
            grpBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnRun;
        private Button btnFoo;
        private Button btnDI;
        private Button btnGeneric;
        private Button btnThreads;
        public TextBox textboxMain;
        private Label lblMain;
        private Button btnStartThreads;
        private GroupBox grpBox1;
        public Button btnThread2;
        public Button btnThread1;
        public TextBox textboxMTE;
        public Button btnStatus;
        public Label lblThreads;
        private Button btnConcurTask;
        private Button btnUIForm;
        private Button btnConcurThread;
        private PageSetupDialog pageSetupDialog1;
        private Button btnConcurParallel;
    }
}
