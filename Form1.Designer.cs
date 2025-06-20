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
            RUNbtn = new Button();
            SuspendLayout();
            // 
            // RUNbtn
            // 
            RUNbtn.Location = new Point(26, 28);
            RUNbtn.Name = "RUNbtn";
            RUNbtn.Size = new Size(197, 104);
            RUNbtn.TabIndex = 0;
            RUNbtn.Text = "RUN";
            RUNbtn.UseVisualStyleBackColor = true;
            RUNbtn.Click += RUNbtn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(RUNbtn);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button RUNbtn;
    }
}
