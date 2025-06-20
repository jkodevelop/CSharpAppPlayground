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
            btnFoo.Size = new Size(197, 45);
            btnFoo.TabIndex = 1;
            btnFoo.Text = "Foo";
            btnFoo.UseVisualStyleBackColor = true;
            btnFoo.Click += btnFoo_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnFoo);
            Controls.Add(btnRun);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button btnRun;
        private Button btnFoo;
    }
}
