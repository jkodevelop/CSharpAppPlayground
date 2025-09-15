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

        protected FormFactory _formPostgres = new FormFactory("CSharpAppPlayground.FormDBPostgres, CSharpAppPlayground");
        private void btnPostgres_Click(object sender, EventArgs e)
        {
            _formPostgres.Open();
        }

        protected FormFactory _formMongo = new FormFactory("CSharpAppPlayground.FormDBMongo, CSharpAppPlayground");
        private void btnMongoDB_Click(object sender, EventArgs e)
        {
            _formMongo.Open();
        }
    }
}
