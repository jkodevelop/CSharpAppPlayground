using CSharpAppPlayground.DBClasses.PostgresExamples;
using System.Diagnostics;

namespace CSharpAppPlayground
{
    public partial class FormDBPostgres : Form
    {
        _connPostgres connPostgres;

        PostgresBaseExamples baseExamples = new PostgresBaseExamples();
        PostgresEnumExample enumExample = new PostgresEnumExample();

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

        private void btnPostgresBasicExample_Click(object sender, EventArgs e)
        {
            // running basic example: insert, select, update, delete
            baseExamples.RunBasicExample();
        }

        private void btnEnumExample_Click(object sender, EventArgs e)
        {
            enumExample.DemoInsertAndSelect();
        }
    }
}
