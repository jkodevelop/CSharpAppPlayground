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
            btnConcurTask = new Button();
            btnUIForm = new Button();
            btnConcurThread = new Button();
            btnConcurParallel = new Button();
            btnDBs = new Button();
            btnLogging = new Button();
            btnDataGenQuickTest = new Button();
            SuspendLayout();
            // 
            // btnRun
            // 
            btnRun.Location = new Point(12, 12);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(183, 77);
            btnRun.TabIndex = 0;
            btnRun.Text = "RUN";
            btnRun.UseVisualStyleBackColor = true;
            btnRun.Click += btnRun_Click;
            // 
            // btnFoo
            // 
            btnFoo.Location = new Point(12, 108);
            btnFoo.Name = "btnFoo";
            btnFoo.Size = new Size(183, 35);
            btnFoo.TabIndex = 1;
            btnFoo.Text = "Foo";
            btnFoo.UseVisualStyleBackColor = true;
            btnFoo.Click += btnFoo_Click;
            // 
            // btnDI
            // 
            btnDI.Location = new Point(12, 149);
            btnDI.Name = "btnDI";
            btnDI.Size = new Size(183, 40);
            btnDI.TabIndex = 2;
            btnDI.Text = "Dependency Injection";
            btnDI.UseVisualStyleBackColor = true;
            btnDI.Click += btnDI_Click;
            // 
            // btnGeneric
            // 
            btnGeneric.Location = new Point(12, 195);
            btnGeneric.Name = "btnGeneric";
            btnGeneric.Size = new Size(183, 33);
            btnGeneric.TabIndex = 3;
            btnGeneric.Text = "Generic Types";
            btnGeneric.UseVisualStyleBackColor = true;
            btnGeneric.Click += btnGeneric_Click;
            // 
            // btnConcurTask
            // 
            btnConcurTask.Location = new Point(212, 55);
            btnConcurTask.Name = "btnConcurTask";
            btnConcurTask.Size = new Size(183, 33);
            btnConcurTask.TabIndex = 16;
            btnConcurTask.Text = "Task()";
            btnConcurTask.UseVisualStyleBackColor = true;
            btnConcurTask.Click += btnConcurTask_Click;
            // 
            // btnUIForm
            // 
            btnUIForm.Location = new Point(12, 234);
            btnUIForm.Name = "btnUIForm";
            btnUIForm.Size = new Size(183, 35);
            btnUIForm.TabIndex = 18;
            btnUIForm.Text = "WinForms UI";
            btnUIForm.UseVisualStyleBackColor = true;
            btnUIForm.Click += btnUIForm_Click;
            // 
            // btnConcurThread
            // 
            btnConcurThread.Location = new Point(212, 12);
            btnConcurThread.Name = "btnConcurThread";
            btnConcurThread.Size = new Size(183, 35);
            btnConcurThread.TabIndex = 19;
            btnConcurThread.Text = "Thread()";
            btnConcurThread.UseVisualStyleBackColor = true;
            btnConcurThread.Click += btnConcurThread_Click;
            // 
            // btnConcurParallel
            // 
            btnConcurParallel.Location = new Point(212, 96);
            btnConcurParallel.Name = "btnConcurParallel";
            btnConcurParallel.Size = new Size(183, 33);
            btnConcurParallel.TabIndex = 20;
            btnConcurParallel.Text = "Parallel()";
            btnConcurParallel.UseVisualStyleBackColor = true;
            btnConcurParallel.Click += btnConcurParallel_Click;
            // 
            // btnDBs
            // 
            btnDBs.Location = new Point(12, 370);
            btnDBs.Name = "btnDBs";
            btnDBs.Size = new Size(183, 44);
            btnDBs.TabIndex = 21;
            btnDBs.Text = "Databases";
            btnDBs.UseVisualStyleBackColor = true;
            btnDBs.Click += btnDBs_Click;
            // 
            // btnLogging
            // 
            btnLogging.Location = new Point(12, 275);
            btnLogging.Name = "btnLogging";
            btnLogging.Size = new Size(183, 23);
            btnLogging.TabIndex = 22;
            btnLogging.Text = "Logging";
            btnLogging.UseVisualStyleBackColor = true;
            btnLogging.Click += btnLogging_Click;
            // 
            // btnDataGenQuickTest
            // 
            btnDataGenQuickTest.Location = new Point(12, 306);
            btnDataGenQuickTest.Name = "btnDataGenQuickTest";
            btnDataGenQuickTest.Size = new Size(183, 23);
            btnDataGenQuickTest.TabIndex = 23;
            btnDataGenQuickTest.Text = "Data Generation QuickTest";
            btnDataGenQuickTest.UseVisualStyleBackColor = true;
            btnDataGenQuickTest.Click += btnDataGenQuickTest_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 426);
            Controls.Add(btnDataGenQuickTest);
            Controls.Add(btnLogging);
            Controls.Add(btnDBs);
            Controls.Add(btnConcurParallel);
            Controls.Add(btnConcurThread);
            Controls.Add(btnUIForm);
            Controls.Add(btnConcurTask);
            Controls.Add(btnGeneric);
            Controls.Add(btnDI);
            Controls.Add(btnFoo);
            Controls.Add(btnRun);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button btnRun;
        private Button btnFoo;
        private Button btnDI;
        private Button btnGeneric;
        private Button btnConcurTask;
        private Button btnUIForm;
        private Button btnConcurThread;
        private Button btnConcurParallel;
        private Button btnDBs;
        private Button btnLogging;
        private Button btnDataGenQuickTest;
    }
}
