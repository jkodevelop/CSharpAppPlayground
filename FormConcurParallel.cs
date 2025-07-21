using CSharpAppPlayground.Multithreading.ParallelExample;
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
    public partial class FormConcurParallel : Form
    {
        public FormConcurParallel()
        {
            InitializeComponent();
        }

        protected ParallelExample pe = new ParallelExample();
        private void btnParallelExample01_Click(object sender, EventArgs e)
        {
            pe.Run();
        }
    }
}
