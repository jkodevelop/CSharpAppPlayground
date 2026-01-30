namespace CSharpAppPlayground.DBClasses
{
    partial class FormDBMaria
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
            btnMariaStatus = new Button();
            btnMariaBasicExample = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnMariaStatus
            // 
            btnMariaStatus.Location = new Point(22, 24);
            btnMariaStatus.Name = "btnMariaStatus";
            btnMariaStatus.Size = new Size(130, 29);
            btnMariaStatus.TabIndex = 0;
            btnMariaStatus.Text = "Status";
            btnMariaStatus.UseVisualStyleBackColor = true;
            btnMariaStatus.Click += btnMariaStatus_Click;
            // 
            // btnMariaBasicExample
            // 
            btnMariaBasicExample.Location = new Point(22, 89);
            btnMariaBasicExample.Name = "btnMariaBasicExample";
            btnMariaBasicExample.Size = new Size(130, 34);
            btnMariaBasicExample.TabIndex = 1;
            btnMariaBasicExample.Text = "Basic examples";
            btnMariaBasicExample.UseVisualStyleBackColor = true;
            btnMariaBasicExample.Click += btnMariaBasicExample_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 71);
            label1.Name = "label1";
            label1.Size = new Size(105, 15);
            label1.TabIndex = 7;
            label1.Text = "table: SqlDBObject";
            // 
            // FormDBMaria
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(btnMariaBasicExample);
            Controls.Add(btnMariaStatus);
            Name = "FormDBMaria";
            Text = "FormDBMaria";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnMariaStatus;
        private Button btnMariaBasicExample;
        private Label label1;
    }
}