using CSharpAppPlayground.Classes;
using CSharpAppPlayground.DBClasses.MongoDBExamples;
using CSharpAppPlayground.DBClasses.MysqlExamples;
using CSharpAppPlayground.DBClasses.PostgresExamples;
using System.Diagnostics;

namespace CSharpAppPlayground
{
    public partial class FormDBsMenu
    {
        private _connMysql mysql;
        private _connPostgres postgres;
        private _connMongoDB mongo;

        public FormDBsMenu()
        {
            InitializeComponent();

            mysql = new _connMysql();
            postgres = new _connPostgres();
            mongo = new _connMongoDB();
        }

        protected FormFactory _formMysql = new FormFactory("CSharpAppPlayground.FormDBMysql, CSharpAppPlayground");
        private void btnMySQL_Click(object sender, EventArgs e)
        {
            _formMysql.Open();
        }
        private void btnMySQLStatus_Click(object sender, EventArgs e)
        {
            string serverVersion = mysql.getServerVersion();
            Debug.Print($"{serverVersion}");
        }

        protected FormFactory _formPostgres = new FormFactory("CSharpAppPlayground.FormDBPostgres, CSharpAppPlayground");
        private void btnPostgres_Click(object sender, EventArgs e)
        {
            _formPostgres.Open();
        }
        private void btnPostgresStatus_Click(object sender, EventArgs e)
        {
            string serverVersion = postgres.getServerVersion();
            Debug.Print($"{serverVersion}");
        }

        protected FormFactory _formMongo = new FormFactory("CSharpAppPlayground.FormDBMongo, CSharpAppPlayground");
        private void btnMongoDB_Click(object sender, EventArgs e)
        {
            _formMongo.Open();
        }

        private void btnMongoDBStatus_Click(object sender, EventArgs e)
        {
            string serverVersion = mongo.getServerVersion();
            Debug.Print($"{serverVersion}");
        }

        protected FormFactory _formDBBenchmark = new FormFactory("CSharpAppPlayground.DBClasses.FormDBBenchmark, CSharpAppPlayground");
        private void btnDBBenchmark_Click(object sender, EventArgs e)
        {
            _formDBBenchmark.Open();
        }
    }
}
