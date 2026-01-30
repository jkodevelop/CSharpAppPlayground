namespace CSharpAppPlayground.DBClasses
{
    partial class FormDBPostgres
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
            btnPostgresStatus = new Button();
            btnPostgresBasicExample = new Button();
            btnEnumExample = new Button();
            btnBenchBulkUpdate = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // btnPostgresStatus
            // 
            btnPostgresStatus.Location = new Point(21, 23);
            btnPostgresStatus.Name = "btnPostgresStatus";
            btnPostgresStatus.Size = new Size(132, 29);
            btnPostgresStatus.TabIndex = 1;
            btnPostgresStatus.Text = "Status";
            btnPostgresStatus.UseVisualStyleBackColor = true;
            btnPostgresStatus.Click += btnPostgresStatus_Click;
            // 
            // btnPostgresBasicExample
            // 
            btnPostgresBasicExample.Location = new Point(21, 87);
            btnPostgresBasicExample.Name = "btnPostgresBasicExample";
            btnPostgresBasicExample.Size = new Size(132, 32);
            btnPostgresBasicExample.TabIndex = 2;
            btnPostgresBasicExample.Text = "Basic examples";
            btnPostgresBasicExample.UseVisualStyleBackColor = true;
            btnPostgresBasicExample.Click += btnPostgresBasicExample_Click;
            // 
            // btnEnumExample
            // 
            btnEnumExample.Location = new Point(21, 151);
            btnEnumExample.Name = "btnEnumExample";
            btnEnumExample.Size = new Size(132, 32);
            btnEnumExample.TabIndex = 3;
            btnEnumExample.Text = "Enum Example";
            btnEnumExample.UseVisualStyleBackColor = true;
            btnEnumExample.Click += btnEnumExample_Click;
            // 
            // btnBenchBulkUpdate
            // 
            btnBenchBulkUpdate.Location = new Point(170, 87);
            btnBenchBulkUpdate.Name = "btnBenchBulkUpdate";
            btnBenchBulkUpdate.Size = new Size(156, 32);
            btnBenchBulkUpdate.TabIndex = 4;
            btnBenchBulkUpdate.Text = "Benchmark Bulk Update";
            btnBenchBulkUpdate.UseVisualStyleBackColor = true;
            btnBenchBulkUpdate.Click += btnBenchBulkUpdate_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(21, 69);
            label1.Name = "label1";
            label1.Size = new Size(105, 15);
            label1.TabIndex = 5;
            label1.Text = "table: SqlDBObject";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(21, 133);
            label2.Name = "label2";
            label2.Size = new Size(114, 15);
            label2.TabIndex = 6;
            label2.Text = "table: ExampleEnum";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(170, 69);
            label3.Name = "label3";
            label3.Size = new Size(39, 15);
            label3.TabIndex = 7;
            label3.Text = "TODO";
            // 
            // FormDBPostgres
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnBenchBulkUpdate);
            Controls.Add(btnEnumExample);
            Controls.Add(btnPostgresBasicExample);
            Controls.Add(btnPostgresStatus);
            Name = "FormDBPostgres";
            Text = "FormDBPostgres";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnPostgresStatus;
        private Button btnPostgresBasicExample;
        private Button btnEnumExample;
        private Button btnBenchBulkUpdate;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}