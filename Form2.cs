using System;
using System.Windows.Forms;

namespace CSharpAppPlayground
{
    public class Form2 : Form
    {
        private Button btnBack;
        public Form2()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            btnBack = new Button();
            btnBack.Text = "Back";
            btnBack.Location = new System.Drawing.Point(20, 20);
            btnBack.Size = new System.Drawing.Size(100, 30);
            btnBack.Click += BtnBack_Click;
            Controls.Add(btnBack);
            this.Text = "Form2";
            this.ClientSize = new System.Drawing.Size(300, 200);
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
} 