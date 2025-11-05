using CSharpAppPlayground.DBClasses.MongoDBBenchmark;
using CSharpAppPlayground.DBClasses.MysqlBenchmark;
using CSharpAppPlayground.DBClasses.PostgresBenchmark;
using CSharpAppPlayground.UIClasses;
using RepoDb;
using System.Diagnostics;

namespace CSharpAppPlayground.DBClasses
{
    public partial class FormDBBenchmark : FormWithRichText
    {
        public FormDBBenchmark()
        {
            InitializeComponent();
        }

        PostgresBasicBenchmarks pgsBenchmarks = new PostgresBasicBenchmarks();
        MysqlBasicBenchmarks mysqlBenchmarks = new MysqlBasicBenchmarks();
        MongoDBBasicBenchmarks mongoDBBenchmark = new MongoDBBasicBenchmarks();
        private void btnBenchmarkInserts_Click(object sender, EventArgs e)
        {
            int amount = (int)numAmount.Value;

            //////////////////////////////////////////////////////////////////////////////////////////////////////
            /// SQL DB
            //////////////////////////////////////////////////////////////////////////////////////////////////////

            // !!IMPORTANT NOTE: in order to test RepoDB with Mysql and Postgres, this can only be down one at a time
            // SO to test mysql with RepoInsertAll option then comment out postgres benchmark: pgsBenchmarks
            // Vice Versa for Postgres testing

            //GlobalConfiguration.Setup().UseMySql(); // RepoDb.MySqlBootstrap.Initialize(); [deprecated]
            //mysqlBenchmarks.RunBulkInsertBenchmark(amount);
            //int mysqlInsertedCount = mysqlBenchmarks.GetVidsCount();
            //Debug.Print($"** MySQL Inserted:{mysqlInsertedCount}\n");

            //GlobalConfiguration.Setup().UsePostgreSql(); // RepoDb.PostgreSqlBootstrap.Initialize(); [deprecated]
            //pgsBenchmarks.RunBulkInsertBenchmark(amount);
            //int pgsInsertedCount = pgsBenchmarks.GetVidsCount();
            //Debug.Print($"** PostgreSQL Inserted:{pgsInsertedCount}\n");

            //////////////////////////////////////////////////////////////////////////////////////////////////////
            /// MongoDB
            //////////////////////////////////////////////////////////////////////////////////////////////////////
            
            mongoDBBenchmark.RunBulkInsertBenchmark(amount);
            long mongoInsertCount = mongoDBBenchmark.GetVidsCount();
            Debug.Print($"** MongoDB Inserted:{mongoInsertCount}\n");
        }

        private void btnResetTables_Click(object sender, EventArgs e)
        {
            //mysqlBenchmarks.ResetVidsTable();
            //pgsBenchmarks.ResetVidsTable();
            mongoDBBenchmark.DeleteAll();
        }
    }
}
