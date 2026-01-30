namespace CSharpAppPlayground.DBClasses
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
            label1 = new Label();
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
            btnMysqlBasicExample.Location = new Point(21, 87);
            btnMysqlBasicExample.Name = "btnMysqlBasicExample";
            btnMysqlBasicExample.Size = new Size(132, 31);
            btnMysqlBasicExample.TabIndex = 1;
            btnMysqlBasicExample.Text = "Basic examples";
            btnMysqlBasicExample.UseVisualStyleBackColor = true;
            btnMysqlBasicExample.Click += btnMysqlBasicExample_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(21, 69);
            label1.Name = "label1";
            label1.Size = new Size(105, 15);
            label1.TabIndex = 6;
            label1.Text = "table: SqlDBObject";
            // 
            // FormDBMysql
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(btnMysqlBasicExample);
            Controls.Add(btnMysqlStatus);
            Name = "FormDBMysql";
            Text = "FormDBMysql";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnMysqlStatus;
        private Button btnMysqlBasicExample;
        private Label label1;
    }
}