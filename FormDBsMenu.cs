using CSharpAppPlayground.Classes;
using CSharpAppPlayground.DBClasses.MariaDBExamples;
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
        private _connMariaDB mariadb;

        public FormDBsMenu()
        {
            InitializeComponent();

            mysql = new _connMysql();
            postgres = new _connPostgres();
            mongo = new _connMongoDB();
            mariadb = new _connMariaDB();
        }

        // check all DB connections status
        private void btnStatus_Click(object sender, EventArgs e)
        {
            string mysqlStat = mysql.getServerVersion();
            Debug.Print($"  MySQL: {mysqlStat}");
            string pgStat = postgres.getServerVersion();
            Debug.Print($"  PostgreSQL: {pgStat}");
            string mariaStat = mariadb.getServerVersion();
            Debug.Print($"  MariaDB: {mariaStat}");
            string mongoStat = mongo.getServerVersion();
            Debug.Print($"  MongoDB: {mongoStat}");
        }

        protected FormFactory _formMysql = new FormFactory("CSharpAppPlayground.DBClasses.FormDBMysql, CSharpAppPlayground");

        private void btnMySQL_Click(object sender, EventArgs e)
        {
            _formMysql.Open();
        }
        private void btnMySQLStatus_Click(object sender, EventArgs e)
        {
            string serverVersion = mysql.getServerVersion();
            Debug.Print($"{serverVersion}");
        }

        protected FormFactory _formDBMaria = new FormFactory("CSharpAppPlayground.DBClasses.FormDBMaria, CSharpAppPlayground");
        private void btnMariaDB_Click(object sender, EventArgs e)
        {
            _formDBMaria.Open();
        }

        protected FormFactory _formPostgres = new FormFactory("CSharpAppPlayground.DBClasses.FormDBPostgres, CSharpAppPlayground");
        private void btnPostgres_Click(object sender, EventArgs e)
        {
            _formPostgres.Open();
        }
        private void btnPostgresStatus_Click(object sender, EventArgs e)
        {
            string serverVersion = postgres.getServerVersion();
            Debug.Print($"{serverVersion}");
        }

        protected FormFactory _formMongo = new FormFactory("CSharpAppPlayground.DBClasses.FormDBMongo, CSharpAppPlayground");
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

        private void btnMariaDBStatus_Click(object sender, EventArgs e)
        {
            string serverVersion = mariadb.getServerVersion();
            Debug.Print($"{serverVersion}");
        }
    }
}
