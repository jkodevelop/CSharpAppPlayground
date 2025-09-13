namespace CSharpAppPlayground
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
            // FormDBPostgres
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnPostgresStatus);
            Name = "FormDBPostgres";
            Text = "FormDBPostgres";
            ResumeLayout(false);
        }

        #endregion

        private Button btnPostgresStatus;
    }
}