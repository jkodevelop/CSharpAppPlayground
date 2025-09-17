using CSharpAppPlayground.DBClasses.PostgresExamples;
using System.Diagnostics;

namespace CSharpAppPlayground
{
    public partial class FormDBPostgres : Form
    {
        _connPostgres connPostgres;
        public FormDBPostgres()
        {
            InitializeComponent();
            init();
        }

        public void init()
        {
            connPostgres = new _connPostgres();
        }

        private void btnPostgresStatus_Click(object sender, EventArgs e)
        {
            string serverVersion = connPostgres.getServerVersion();
            Debug.Print($"{serverVersion}");
        }
    }
}
