namespace CSharpAppPlayground.DBClasses
{
    partial class FormDBMongo
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
            btnMongoStatus = new Button();
            btnMongoBasicExample = new Button();
            SuspendLayout();
            // 
            // btnMongoStatus
            // 
            btnMongoStatus.Location = new Point(21, 23);
            btnMongoStatus.Name = "btnMongoStatus";
            btnMongoStatus.Size = new Size(132, 29);
            btnMongoStatus.TabIndex = 1;
            btnMongoStatus.Text = "Status";
            btnMongoStatus.UseVisualStyleBackColor = true;
            btnMongoStatus.Click += btnMongoStatus_Click;
            // 
            // btnMongoBasicExample
            // 
            btnMongoBasicExample.Location = new Point(21, 69);
            btnMongoBasicExample.Name = "btnMongoBasicExample";
            btnMongoBasicExample.Size = new Size(132, 33);
            btnMongoBasicExample.TabIndex = 2;
            btnMongoBasicExample.Text = "Basic examples";
            btnMongoBasicExample.UseVisualStyleBackColor = true;
            btnMongoBasicExample.Click += btnMongoBasicExample_Click;
            // 
            // FormDBMongo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnMongoBasicExample);
            Controls.Add(btnMongoStatus);
            Name = "FormDBMongo";
            Text = "FormDBMongo";
            ResumeLayout(false);
        }

        #endregion

        private Button btnMongoStatus;
        private Button btnMongoBasicExample;
    }
}