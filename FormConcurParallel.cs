using CSharpAppPlayground.Concurrency.ParallelExample;
using CSharpAppPlayground.UIClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
            pe = new ParallelExample(this);
        }

        protected ParallelExample pe;
        private void btnParallelExample01_Click(object sender, EventArgs e)
        {
            updateRichTextBoxMain("testing richtext box");
            updateLabelMain("testing label");
            Debug.Print("Starting Parallel Example...");
            pe.Run();
            Debug.Print("DONE");
        }

    }
}
