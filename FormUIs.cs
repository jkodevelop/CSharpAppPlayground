using System;
using System.Windows.Forms;

namespace CSharpAppPlayground
{
    public partial class FormUIs : Form
    {
        public FormUIs()
        {
            InitializeComponent();
        }

        private void btnHidePanel01_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void btnShowPanel01_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel1.BringToFront();
        }
    }
}
