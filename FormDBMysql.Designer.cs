namespace CSharpAppPlayground
{
    partial class FormDBMysql
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
            btnMysqlStatus = new Button();
            btnMysqlBasicExample = new Button();
            SuspendLayout();
            // 
            // btnMysqlStatus
            // 
            btnMysqlStatus.Location = new Point(21, 23);
            btnMysqlStatus.Name = "btnMysqlStatus";
            btnMysqlStatus.Size = new Size(132, 29);
            btnMysqlStatus.TabIndex = 0;
            btnMysqlStatus.Text = "Status";
            btnMysqlStatus.UseVisualStyleBackColor = true;
            btnMysqlStatus.Click += btnMysqlStatus_Click;
            // 
            // btnMysqlBasicExample
            // 
            btnMysqlBasicExample.Location = new Point(21, 70);
            btnMysqlBasicExample.Name = "btnMysqlBasicExample";
            btnMysqlBasicExample.Size = new Size(132, 29);
            btnMysqlBasicExample.TabIndex = 1;
            btnMysqlBasicExample.Text = "Basic examples";
            btnMysqlBasicExample.UseVisualStyleBackColor = true;
            btnMysqlBasicExample.Click += btnMysqlBasicExample_Click;
            // 
            // FormDBMysql
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnMysqlBasicExample);
            Controls.Add(btnMysqlStatus);
            Name = "FormDBMysql";
            Text = "FormDBMysql";
            ResumeLayout(false);
        }

        #endregion

        private Button btnMysqlStatus;
        private Button btnMysqlBasicExample;
    }
}