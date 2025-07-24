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
            lblMain = new Label();
            tboxMain = new TextBox();
            btnMT02Start = new Button();
            groupBox1 = new GroupBox();
            btnMT02Stop = new Button();
            groupBox1.SuspendLayout();
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
            // lblMain
            // 
            lblMain.AutoSize = true;
            lblMain.Location = new Point(509, 23);
            lblMain.Name = "lblMain";
            lblMain.Size = new Size(32, 15);
            lblMain.TabIndex = 1;
            lblMain.Text = "label";
            // 
            // tboxMain
            // 
            tboxMain.Location = new Point(499, 97);
            tboxMain.Multiline = true;
            tboxMain.Name = "tboxMain";
            tboxMain.ScrollBars = ScrollBars.Vertical;
            tboxMain.Size = new Size(258, 316);
            tboxMain.TabIndex = 2;
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
            // FormConcurThread
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox1);
            Controls.Add(tboxMain);
            Controls.Add(lblMain);
            Controls.Add(btnMTExample);
            Name = "FormConcurThread";
            Text = "ConcurrencyThreads";
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnMTExample;
        private Label lblMain;
        private TextBox tboxMain;
        private Button btnMT02Start;
        private GroupBox groupBox1;
        private Button btnMT02Stop;
    }
}