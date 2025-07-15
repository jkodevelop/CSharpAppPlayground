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
            btnStartParallel = new Button();
            lblThreads = new Label();
            btnStatus = new Button();
            textboxMTE = new TextBox();
            btnThread2 = new Button();
            btnThread1 = new Button();
            btnStartTasks = new Button();
            btnNewView = new Button();
            btnSwitchPageConcurrency = new Button();
            panelPage2 = new Panel();
            lblPanel2_01 = new Label();
            btnBackFromPage2 = new Button();
            grpBox1.SuspendLayout();
            panelPage2.SuspendLayout();
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
            grpBox1.Controls.Add(btnStartParallel);
            grpBox1.Controls.Add(lblThreads);
            grpBox1.Controls.Add(btnStatus);
            grpBox1.Controls.Add(textboxMTE);
            grpBox1.Controls.Add(btnThread2);
            grpBox1.Controls.Add(btnThread1);
            grpBox1.Controls.Add(btnStartThreads);
            grpBox1.Controls.Add(btnStartTasks);
            grpBox1.Location = new Point(508, 32);
            grpBox1.Name = "grpBox1";
            grpBox1.Size = new Size(264, 391);
            grpBox1.TabIndex = 9;
            grpBox1.TabStop = false;
            grpBox1.Text = "More MultiThreading Examples";
            // 
            // btnStartParallel
            // 
            btnStartParallel.Location = new Point(6, 356);
            btnStartParallel.Name = "btnStartParallel";
            btnStartParallel.Size = new Size(252, 29);
            btnStartParallel.TabIndex = 14;
            btnStartParallel.Text = "3. Start Parallel";
            btnStartParallel.UseVisualStyleBackColor = true;
            btnStartParallel.Click += btnStartParallel_Click;
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
            // btnStartTasks
            // 
            btnStartTasks.Location = new Point(6, 309);
            btnStartTasks.Name = "btnStartTasks";
            btnStartTasks.Size = new Size(252, 31);
            btnStartTasks.TabIndex = 15;
            btnStartTasks.Text = "2. Start Tasks";
            btnStartTasks.UseVisualStyleBackColor = true;
            btnStartTasks.Click += btnStartTasks_Click;
            // 
            // btnNewView
            // 
            btnNewView.Location = new Point(26, 290);
            btnNewView.Name = "btnNewView";
            btnNewView.Size = new Size(197, 33);
            btnNewView.TabIndex = 16;
            btnNewView.Text = "Open Dialog";
            btnNewView.UseVisualStyleBackColor = true;
            btnNewView.Click += btnNewView_Click;
            // 
            // btnSwitchPageConcurrency
            // 
            btnSwitchPageConcurrency.Location = new Point(26, 330);
            btnSwitchPageConcurrency.Name = "btnSwitchPageConcurrency";
            btnSwitchPageConcurrency.Size = new Size(197, 33);
            btnSwitchPageConcurrency.TabIndex = 17;
            btnSwitchPageConcurrency.Text = "Switch Panel";
            btnSwitchPageConcurrency.UseVisualStyleBackColor = true;
            btnSwitchPageConcurrency.Click += btnSwitchPageConcurrency_Click;
            // 
            // panelPage2
            // 
            panelPage2.Controls.Add(lblPanel2_01);
            panelPage2.Controls.Add(btnBackFromPage2);
            panelPage2.Location = new Point(0, 0);
            panelPage2.Name = "panelPage2";
            panelPage2.Size = new Size(800, 450);
            panelPage2.TabIndex = 18;
            panelPage2.Visible = false;
            // 
            // lblPanel2_01
            // 
            lblPanel2_01.AutoSize = true;
            lblPanel2_01.Location = new Point(12, 28);
            lblPanel2_01.Name = "lblPanel2_01";
            lblPanel2_01.Size = new Size(235, 15);
            lblPanel2_01.TabIndex = 1;
            lblPanel2_01.Text = "Example for creating another panel; Panel 2";
            // 
            // btnBackFromPage2
            // 
            btnBackFromPage2.Location = new Point(12, 378);
            btnBackFromPage2.Name = "btnBackFromPage2";
            btnBackFromPage2.Size = new Size(776, 60);
            btnBackFromPage2.TabIndex = 0;
            btnBackFromPage2.Text = "Switch Panel Back";
            btnBackFromPage2.UseVisualStyleBackColor = true;
            btnBackFromPage2.Click += btnBackFromPage2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSwitchPageConcurrency);
            Controls.Add(btnNewView);
            Controls.Add(lblMain);
            Controls.Add(textboxMain);
            Controls.Add(btnThreads);
            Controls.Add(btnGeneric);
            Controls.Add(btnDI);
            Controls.Add(btnFoo);
            Controls.Add(btnRun);
            Controls.Add(grpBox1);
            Controls.Add(panelPage2);
            Name = "Form1";
            Text = "Form1";
            grpBox1.ResumeLayout(false);
            grpBox1.PerformLayout();
            panelPage2.ResumeLayout(false);
            panelPage2.PerformLayout();
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
        private Button btnStartParallel;
        private Button btnNewView;
        private Button btnSwitchPageConcurrency;
        private Panel panelPage2;
        private Button btnBackFromPage2;
        private Button btnStartTasks;
        private Label lblPanel2_01;
    }
}
