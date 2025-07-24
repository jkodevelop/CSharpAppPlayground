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
            // FormConcurThread
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tboxMain);
            Controls.Add(lblMain);
            Controls.Add(btnMTExample);
            Name = "FormConcurThread";
            Text = "ConcurrencyThreads";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnMTExample;
        private Label lblMain;
        private TextBox tboxMain;
    }
}