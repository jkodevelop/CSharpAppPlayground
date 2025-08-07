using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpAppPlayground.UIClasses
{
    public partial class FormWithRichText : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public FormWithRichText()
        {
            InitializeComponent();
        }

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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        protected void InitializeComponent()
        {
            lblMain = new Label();
            richTBoxMain = new RichTextBox();
            btnClose = new Button();
            SuspendLayout();
            // 
            // lblMain
            // 
            lblMain.AutoSize = true;
            lblMain.Location = new Point(540, 9);
            lblMain.Name = "lblMain";
            lblMain.Size = new Size(65, 15);
            lblMain.TabIndex = 0;
            lblMain.Text = "Main Label";
            // 
            // richTBoxMain
            // 
            richTBoxMain.Location = new Point(540, 62);
            richTBoxMain.Name = "richTBoxMain";
            richTBoxMain.Size = new Size(248, 321);
            richTBoxMain.TabIndex = 1;
            richTBoxMain.Text = "";
            // 
            // btnClose
            // 
            btnClose.Location = new Point(589, 403);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(199, 35);
            btnClose.TabIndex = 2;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // FormWithRichText
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnClose);
            Controls.Add(richTBoxMain);
            Controls.Add(lblMain);
            Name = "FormWithRichText";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }
        private Label lblMain;
        private RichTextBox richTBoxMain;
        private Button btnClose;

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
