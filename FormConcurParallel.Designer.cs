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
            richTextBox1 = new RichTextBox();
            btnBack = new Button();
            SuspendLayout();
            // 
            // btnParallelExample01
            // 
            btnParallelExample01.Location = new Point(25, 30);
            btnParallelExample01.Name = "btnParallelExample01";
            btnParallelExample01.Size = new Size(158, 33);
            btnParallelExample01.TabIndex = 0;
            btnParallelExample01.Text = "Parallel Example";
            btnParallelExample01.UseVisualStyleBackColor = true;
            btnParallelExample01.Click += btnParallelExample01_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(523, 30);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(246, 333);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "";
            // 
            // btnBack
            // 
            btnBack.Location = new Point(662, 369);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(107, 54);
            btnBack.TabIndex = 2;
            btnBack.Text = "Close";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // FormConcurParallel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnBack);
            Controls.Add(richTextBox1);
            Controls.Add(btnParallelExample01);
            Name = "FormConcurParallel";
            Text = "FormConcurParallel";
            ResumeLayout(false);
        }

        #endregion

        private Button btnParallelExample01;
        private RichTextBox richTextBox1;
        private Button btnBack;
    }
}