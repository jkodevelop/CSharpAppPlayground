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
            btnPostgresBasicExample.Location = new Point(21, 70);
            btnPostgresBasicExample.Name = "btnPostgresBasicExample";
            btnPostgresBasicExample.Size = new Size(132, 32);
            btnPostgresBasicExample.TabIndex = 2;
            btnPostgresBasicExample.Text = "Basic examples";
            btnPostgresBasicExample.UseVisualStyleBackColor = true;
            btnPostgresBasicExample.Click += btnPostgresBasicExample_Click;
            // 
            // btnEnumExample
            // 
            btnEnumExample.Location = new Point(21, 122);
            btnEnumExample.Name = "btnEnumExample";
            btnEnumExample.Size = new Size(132, 32);
            btnEnumExample.TabIndex = 3;
            btnEnumExample.Text = "Enum Example";
            btnEnumExample.UseVisualStyleBackColor = true;
            btnEnumExample.Click += btnEnumExample_Click;
            // 
            // btnBenchBulkUpdate
            // 
            btnBenchBulkUpdate.Location = new Point(170, 70);
            btnBenchBulkUpdate.Name = "btnBenchBulkUpdate";
            btnBenchBulkUpdate.Size = new Size(156, 32);
            btnBenchBulkUpdate.TabIndex = 4;
            btnBenchBulkUpdate.Text = "Benchmark Bulk Update";
            btnBenchBulkUpdate.UseVisualStyleBackColor = true;
            btnBenchBulkUpdate.Click += btnBenchBulkUpdate_Click;
            // 
            // FormDBPostgres
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnBenchBulkUpdate);
            Controls.Add(btnEnumExample);
            Controls.Add(btnPostgresBasicExample);
            Controls.Add(btnPostgresStatus);
            Name = "FormDBPostgres";
            Text = "FormDBPostgres";
            ResumeLayout(false);
        }

        #endregion

        private Button btnPostgresStatus;
        private Button btnPostgresBasicExample;
        private Button btnEnumExample;
        private Button btnBenchBulkUpdate;
    }
}