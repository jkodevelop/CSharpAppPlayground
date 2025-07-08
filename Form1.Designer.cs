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
            groupBox1 = new GroupBox();
            textboxMTE = new TextBox();
            btnThread2 = new Button();
            btnThread1 = new Button();
            groupBox1.SuspendLayout();
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
            btnStartThreads.Location = new Point(6, 33);
            btnStartThreads.Name = "btnStartThreads";
            btnStartThreads.Size = new Size(252, 23);
            btnStartThreads.TabIndex = 8;
            btnStartThreads.Text = "Start Threads";
            btnStartThreads.UseVisualStyleBackColor = true;
            btnStartThreads.Click += btnStartThreads_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(textboxMTE);
            groupBox1.Controls.Add(btnThread2);
            groupBox1.Controls.Add(btnThread1);
            groupBox1.Controls.Add(btnStartThreads);
            groupBox1.Location = new Point(508, 32);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(264, 391);
            groupBox1.TabIndex = 9;
            groupBox1.TabStop = false;
            groupBox1.Text = "More MultiThreading Examples";
            // 
            // textboxMTE
            // 
            textboxMTE.Location = new Point(6, 112);
            textboxMTE.Multiline = true;
            textboxMTE.Name = "textboxMTE";
            textboxMTE.ScrollBars = ScrollBars.Both;
            textboxMTE.Size = new Size(252, 273);
            textboxMTE.TabIndex = 11;
            // 
            // btnThread2
            // 
            btnThread2.Enabled = false;
            btnThread2.Location = new Point(133, 71);
            btnThread2.Name = "btnThread2";
            btnThread2.Size = new Size(125, 23);
            btnThread2.TabIndex = 10;
            btnThread2.Text = "Pause Thread 2";
            btnThread2.UseVisualStyleBackColor = true;
            // 
            // btnThread1
            // 
            btnThread1.Enabled = false;
            btnThread1.Location = new Point(6, 71);
            btnThread1.Name = "btnThread1";
            btnThread1.Size = new Size(121, 23);
            btnThread1.TabIndex = 9;
            btnThread1.Text = "Pause Thread 1";
            btnThread1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblMain);
            Controls.Add(textboxMain);
            Controls.Add(btnThreads);
            Controls.Add(btnGeneric);
            Controls.Add(btnDI);
            Controls.Add(btnFoo);
            Controls.Add(btnRun);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
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
        private GroupBox groupBox1;
        public Button btnThread2;
        public Button btnThread1;
        public TextBox textboxMTE;
        private TextBox textBox1;
    }
}
