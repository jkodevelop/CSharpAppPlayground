using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
