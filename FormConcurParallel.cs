using CSharpAppPlayground.Multithreading.ParallelExample;
using CSharpAppPlayground.UIClasses;
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
    public partial class FormConcurParallel : FormWithRichText
    {
        public FormConcurParallel()
        {
            InitializeComponent();
        }

        protected ParallelExample pe = new ParallelExample();
        private void btnParallelExample01_Click(object sender, EventArgs e)
        {
            updateRichTextBoxMain("testing richtext box");
            updateLabelMain("testing label");
            pe.Run();
        }

    }
}
