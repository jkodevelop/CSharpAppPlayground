namespace CSharpAppPlayground
{
    partial class FormUIs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUIs));
            btnShowPanel01 = new Button();
            panel1 = new Panel();
            btnHidePanel01 = new Button();
            lblPanel1 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnShowPanel01
            // 
            btnShowPanel01.Location = new Point(25, 25);
            btnShowPanel01.Name = "btnShowPanel01";
            btnShowPanel01.Size = new Size(177, 60);
            btnShowPanel01.TabIndex = 0;
            btnShowPanel01.Text = "Show Panel";
            btnShowPanel01.UseVisualStyleBackColor = true;
            btnShowPanel01.Click += btnShowPanel01_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.AppWorkspace;
            panel1.Controls.Add(btnHidePanel01);
            panel1.Controls.Add(lblPanel1);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(776, 126);
            panel1.TabIndex = 1;
            panel1.Visible = false;
            // 
            // btnHidePanel01
            // 
            btnHidePanel01.Location = new Point(614, 68);
            btnHidePanel01.Name = "btnHidePanel01";
            btnHidePanel01.Size = new Size(148, 46);
            btnHidePanel01.TabIndex = 1;
            btnHidePanel01.Text = "Hide Panel";
            btnHidePanel01.UseVisualStyleBackColor = true;
            btnHidePanel01.Click += btnHidePanel01_Click;
            // 
            // lblPanel1
            // 
            lblPanel1.AutoSize = true;
            lblPanel1.Location = new Point(214, 13);
            lblPanel1.Name = "lblPanel1";
            lblPanel1.Size = new Size(358, 135);
            lblPanel1.TabIndex = 0;
            lblPanel1.Text = resources.GetString("lblPanel1.Text");
            // 
            // FormUIs
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnShowPanel01);
            Controls.Add(panel1);
            Name = "FormUIs";
            Text = "OtherUIs";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnShowPanel01;
        private Panel panel1;
        private Label lblPanel1;
        private Button btnHidePanel01;
    }
}