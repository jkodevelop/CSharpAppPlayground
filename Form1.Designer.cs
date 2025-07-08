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
            button1 = new Button();
            groupBox1 = new GroupBox();
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
            // button1
            // 
            button1.Location = new Point(15, 54);
            button1.Name = "button1";
            button1.Size = new Size(194, 23);
            button1.TabIndex = 8;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button1);
            groupBox1.Location = new Point(508, 32);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(264, 391);
            groupBox1.TabIndex = 9;
            groupBox1.TabStop = false;
            groupBox1.Text = "More MultiThreading Examples";
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
        private Button button1;
        private GroupBox groupBox1;
    }
}
