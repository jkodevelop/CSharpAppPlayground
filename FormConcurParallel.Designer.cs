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
            button1 = new Button();
            SuspendLayout();
            // 
            // btnParallelExample01
            // 
            btnParallelExample01.Location = new Point(25, 30);
            btnParallelExample01.Name = "btnParallelExample01";
            btnParallelExample01.Size = new Size(170, 33);
            btnParallelExample01.TabIndex = 0;
            btnParallelExample01.Text = "Parallel Example";
            btnParallelExample01.UseVisualStyleBackColor = true;
            btnParallelExample01.Click += btnParallelExample01_Click;
            // 
            // btnParallelPrimeNumbers
            // 
            btnParallelPrimeNumbers.Location = new Point(25, 83);
            btnParallelPrimeNumbers.Name = "btnParallelPrimeNumbers";
            btnParallelPrimeNumbers.Size = new Size(170, 34);
            btnParallelPrimeNumbers.TabIndex = 3;
            btnParallelPrimeNumbers.Text = "Prime Numbers (Parallel)";
            btnParallelPrimeNumbers.UseVisualStyleBackColor = true;
            btnParallelPrimeNumbers.Click += btnParallelPrimeNumbers_Click;
            // 
            // button1
            // 
            button1.Location = new Point(25, 138);
            button1.Name = "button1";
            button1.Size = new Size(170, 35);
            button1.TabIndex = 4;
            button1.Text = "Prime Numbers (linear)";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // FormConcurParallel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(btnParallelPrimeNumbers);
            Controls.Add(btnParallelExample01);
            Name = "FormConcurParallel";
            Text = "FormConcurParallel";
            Controls.SetChildIndex(btnParallelExample01, 0);
            Controls.SetChildIndex(btnParallelPrimeNumbers, 0);
            Controls.SetChildIndex(button1, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnParallelExample01;
        private Button btnParallelPrimeNumbers;
        private Button button1;
    }
}