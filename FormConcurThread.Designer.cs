namespace CSharpAppPlayground
{
    partial class FormConcurThread
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
            btnMTExample = new Button();
            btnMT02Start = new Button();
            groupBox1 = new GroupBox();
            btnMT02Stop = new Button();
            groupBox2 = new GroupBox();
            btnThreadB = new Button();
            btnThreadA = new Button();
            btnMTMultiStatus = new Button();
            btnMTMultiStart = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // btnMTExample
            // 
            btnMTExample.Location = new Point(24, 23);
            btnMTExample.Name = "btnMTExample";
            btnMTExample.Size = new Size(173, 39);
            btnMTExample.TabIndex = 0;
            btnMTExample.Text = "Multithread Example";
            btnMTExample.UseVisualStyleBackColor = true;
            btnMTExample.Click += btnMTExample_Click;
            // 
            // btnMT02Start
            // 
            btnMT02Start.Location = new Point(6, 22);
            btnMT02Start.Name = "btnMT02Start";
            btnMT02Start.Size = new Size(135, 33);
            btnMT02Start.TabIndex = 3;
            btnMT02Start.Text = "Start";
            btnMT02Start.UseVisualStyleBackColor = true;
            btnMT02Start.Click += btnMT02Start_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnMT02Stop);
            groupBox1.Controls.Add(btnMT02Start);
            groupBox1.Location = new Point(24, 78);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(288, 69);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Threading With Stop";
            // 
            // btnMT02Stop
            // 
            btnMT02Stop.Enabled = false;
            btnMT02Stop.Location = new Point(147, 22);
            btnMT02Stop.Name = "btnMT02Stop";
            btnMT02Stop.Size = new Size(135, 33);
            btnMT02Stop.TabIndex = 4;
            btnMT02Stop.Text = "Stop";
            btnMT02Stop.UseVisualStyleBackColor = true;
            btnMT02Stop.Click += btnMT02Stop_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnThreadB);
            groupBox2.Controls.Add(btnThreadA);
            groupBox2.Controls.Add(btnMTMultiStatus);
            groupBox2.Controls.Add(btnMTMultiStart);
            groupBox2.Location = new Point(24, 168);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(288, 130);
            groupBox2.TabIndex = 5;
            groupBox2.TabStop = false;
            groupBox2.Text = "Theading Pause/Resume and Status";
            // 
            // btnThreadB
            // 
            btnThreadB.Location = new Point(147, 85);
            btnThreadB.Name = "btnThreadB";
            btnThreadB.Size = new Size(135, 31);
            btnThreadB.TabIndex = 3;
            btnThreadB.Text = "Pause Thread B";
            btnThreadB.UseVisualStyleBackColor = true;
            // 
            // btnThreadA
            // 
            btnThreadA.Location = new Point(6, 85);
            btnThreadA.Name = "btnThreadA";
            btnThreadA.Size = new Size(135, 31);
            btnThreadA.TabIndex = 2;
            btnThreadA.Text = "Pause Thread A";
            btnThreadA.UseVisualStyleBackColor = true;
            // 
            // btnMTMultiStatus
            // 
            btnMTMultiStatus.Location = new Point(190, 22);
            btnMTMultiStatus.Name = "btnMTMultiStatus";
            btnMTMultiStatus.Size = new Size(92, 39);
            btnMTMultiStatus.TabIndex = 1;
            btnMTMultiStatus.Text = "Status";
            btnMTMultiStatus.UseVisualStyleBackColor = true;
            // 
            // btnMTMultiStart
            // 
            btnMTMultiStart.Location = new Point(6, 22);
            btnMTMultiStart.Name = "btnMTMultiStart";
            btnMTMultiStart.Size = new Size(176, 39);
            btnMTMultiStart.TabIndex = 0;
            btnMTMultiStart.Text = "Start Threads";
            btnMTMultiStart.UseVisualStyleBackColor = true;
            btnMTMultiStart.Click += btnMTMultiStart_Click;
            // 
            // FormConcurThread
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(btnMTExample);
            Name = "FormConcurThread";
            Text = "ConcurrencyThreads";
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button btnMTExample;
        private Button btnMT02Start;
        private GroupBox groupBox1;
        private Button btnMT02Stop;
        private GroupBox groupBox2;
        private Button btnMTMultiStart;
        private Button btnMTMultiStatus;
        private Button btnThreadA;
        private Button btnThreadB;
    }
}