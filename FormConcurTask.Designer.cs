namespace CSharpAppPlayground
{
    partial class FormConcurTask
    {
        private Button btnBack;
        private Button btnStartTaskSimple;
        private Button btnTaskExample01;
        private Button btnTaskExample02;
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
            btnBack = new Button();
            btnStartTaskSimple = new Button();
            btnTaskExample01 = new Button();
            btnTaskExample02 = new Button();
            richTBoxMain = new RichTextBox();
            SuspendLayout();
            // 
            // btnBack
            // 
            btnBack.Location = new Point(687, 335);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(111, 42);
            btnBack.TabIndex = 0;
            btnBack.Text = "Close";
            btnBack.Click += BtnBack_Click;
            // 
            // btnStartTaskSimple
            // 
            btnStartTaskSimple.Location = new Point(12, 24);
            btnStartTaskSimple.Name = "btnStartTaskSimple";
            btnStartTaskSimple.Size = new Size(166, 29);
            btnStartTaskSimple.TabIndex = 3;
            btnStartTaskSimple.Text = "Start Task Simple";
            btnStartTaskSimple.UseVisualStyleBackColor = true;
            btnStartTaskSimple.Click += btnStartTaskSimple_Click;
            // 
            // btnTaskExample01
            // 
            btnTaskExample01.Location = new Point(12, 72);
            btnTaskExample01.Name = "btnTaskExample01";
            btnTaskExample01.Size = new Size(166, 32);
            btnTaskExample01.TabIndex = 4;
            btnTaskExample01.Text = "Task Example";
            btnTaskExample01.UseVisualStyleBackColor = true;
            btnTaskExample01.Click += btnTaskExample01_Click;
            // 
            // btnTaskExample02
            // 
            btnTaskExample02.Location = new Point(12, 124);
            btnTaskExample02.Name = "btnTaskExample02";
            btnTaskExample02.Size = new Size(164, 30);
            btnTaskExample02.TabIndex = 5;
            btnTaskExample02.Text = "Task Example 2";
            btnTaskExample02.UseVisualStyleBackColor = true;
            btnTaskExample02.Click += btnTaskExample02_Click;
            // 
            // richTBoxMain
            // 
            richTBoxMain.Location = new Point(532, 24);
            richTBoxMain.Name = "richTBoxMain";
            richTBoxMain.ScrollBars = RichTextBoxScrollBars.Vertical;
            richTBoxMain.Size = new Size(266, 293);
            richTBoxMain.TabIndex = 6;
            richTBoxMain.Text = "";
            // 
            // FormConcurTask
            // 
            ClientSize = new Size(810, 389);
            Controls.Add(richTBoxMain);
            Controls.Add(btnTaskExample02);
            Controls.Add(btnTaskExample01);
            Controls.Add(btnStartTaskSimple);
            Controls.Add(btnBack);
            Name = "FormConcurTask";
            Text = "Other Concurrency Examples";
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox richTBoxMain;
    }
}