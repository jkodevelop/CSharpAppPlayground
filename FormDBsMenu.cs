using CSharpAppPlayground.Classes;

namespace CSharpAppPlayground
{
    public partial class FormDBsMenu : Form
    {
        public FormDBsMenu()
        {
            InitializeComponent();
        }

        protected FormFactory _formMysql = new FormFactory("CSharpAppPlayground.FormDBMysql, CSharpAppPlayground");
        private void btnMySQL_Click(object sender, EventArgs e)
        {
            _formMysql.Open();
        }

        private void btnPostgres_Click(object sender, EventArgs e)
        {

        }

        private void btnMongoDB_Click(object sender, EventArgs e)
        {

        }
    }
}
