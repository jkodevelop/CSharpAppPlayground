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

        private void btnProgress0_Click(object sender, EventArgs e)
        {
            progressBar.Value = 0;
        }

        private void btnProgress100_Click(object sender, EventArgs e)
        {
            progressBar.Value = 100;
        }
    }
}
