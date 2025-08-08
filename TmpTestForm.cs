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
    public partial class TmpTestForm : FormWithRichText
    {
        public TmpTestForm()
        {
            // base.InitializeComponent(); // I don't need to call this as it is already called in the constructor of FormWithRichText
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            updateRichTextBoxMain("testing richtext box");
            updateLabelMain("testing label");
        }
    }
}
