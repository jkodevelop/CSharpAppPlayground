using CSharpAppPlayground.Classes;
using CSharpAppPlayground.Classes.AppSettings;
using CSharpAppPlayground.Classes.DataGen.Generators;
using CSharpAppPlayground.DBClasses.Data;
using CSharpAppPlayground.DBClasses.Data.SQLbenchmark;
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
        private string bulkVidsCSVFilePath = @".\testdata\vids_bulk_inserts.csv";
        CsvManager csvMan;

        public FormDBBenchmark()
        {
            InitializeComponent();
            whichRepoDBSelect.Items.Add("disable RepoDB tests");
            whichRepoDBSelect.Items.Add("enable Mysql RepoDB tests");
            whichRepoDBSelect.Items.Add("enable Postgres RepoDB tests");

            if (GlobalState.GetRepoDBGlobalConfigState())
            {
                int choice = GlobalState.GetRepoDBGlobalConfigChoice();
                whichRepoDBSelect.SelectedIndex = choice;
                whichRepoDBSelect.Enabled = false;
            }
            else
            {
                whichRepoDBSelect.SelectedIndex = 0; // default to disabled
            }
            csvMan = new CsvManager(bulkVidsCSVFilePath);
        }

        PostgresBasicBenchmarks pgsBenchmarks = new PostgresBasicBenchmarks();
        MysqlBasicBenchmarks mysqlBenchmarks = new MysqlBasicBenchmarks();
        MongoDBBasicBenchmarks mongoDBBenchmarks = new MongoDBBasicBenchmarks();

        private void btnResetTables_Click(object sender, EventArgs e)
        {
            mysqlBenchmarks.ResetVidsTable();
            pgsBenchmarks.ResetVidsTable();
            mongoDBBenchmarks.DeleteAll();
        }

        private void btnBenchmarkInserts_Click(object sender, EventArgs e)
        {
            Debug.Print($"select index {whichRepoDBSelect.SelectedIndex}");
            int amount = (int)numAmount.Value;

            //////////////////////////////////////////////////////////////////////////////////////////////////////
            /// SQL DB
            //////////////////////////////////////////////////////////////////////////////////////////////////////

            // !!IMPORTANT NOTE: in order to test RepoDB with Mysql and Postgres, this can only be down one at a time
            // SO to test mysql with RepoInsertAll option then comment out postgres benchmark: pgsBenchmarks
            // Vice Versa for Postgres testing

            if (whichRepoDBSelect.SelectedIndex == 1)
            {
                GlobalConfiguration.Setup().UseMySql(); // RepoDb.MySqlBootstrap.Initialize(); [deprecated]
                mysqlBenchmarks.repoDBTestEnabled = true;
                whichRepoDBSelect.Enabled = false;
                GlobalState.RepoDBGlobalConfigActivated(1);
            }
            mysqlBenchmarks.RunBulkInsertBenchmark(amount);
            int mysqlInsertedCount = mysqlBenchmarks.GetVidsCount();
            Debug.Print($"** MySQL Inserted:{mysqlInsertedCount}\n");

            if (whichRepoDBSelect.SelectedIndex == 2)
            {
                GlobalConfiguration.Setup().UsePostgreSql(); // RepoDb.PostgreSqlBootstrap.Initialize(); [deprecated]
                pgsBenchmarks.repoDBTestEnabled = true;
                whichRepoDBSelect.Enabled = false;
                GlobalState.RepoDBGlobalConfigActivated(2);
            }
            pgsBenchmarks.RunBulkInsertBenchmark(amount);
            int pgsInsertedCount = pgsBenchmarks.GetVidsCount();
            Debug.Print($"** PostgreSQL Inserted:{pgsInsertedCount}\n");

            //////////////////////////////////////////////////////////////////////////////////////////////////////
            /// MongoDB
            //////////////////////////////////////////////////////////////////////////////////////////////////////

            mongoDBBenchmarks.RunBulkInsertBenchmark(amount);
            long mongoInsertCount = mongoDBBenchmarks.GetVidsCount();
            Debug.Print($"** MongoDB Inserted:{mongoInsertCount}\n");
        }

        private void btnBenchmarkInsertsFast_Click(object sender, EventArgs e)
        {
            int amount = (int)numAmount.Value;
            mysqlBenchmarks.FastestCompareBenchmark(amount);
            pgsBenchmarks.FastestCompareBenchmark(amount);
            mongoDBBenchmarks.FastestCompareBenchmark(amount);
            Debug.Print("*** Fastest Bulk Insert Benchmark Complete ***");
        }

        GenerateVidsBSON generator = new GenerateVidsBSON();
        private void btnGenData_Click(object sender, EventArgs e)
        {
            int amount = (int)numAmount.Value;

            // 1. This will generate separate data
            mysqlBenchmarks.GenData(amount);
            pgsBenchmarks.GenData(amount);
            mongoDBBenchmarks.GenData(amount);

            // 2. This will keep them all the same, good for benchmark search etc..
            // create the CSV file
            //List<Vids> testData = generator.GenerateData(amount);
            //csvMan.WriteToCSV(testData);
            //List<VidsSQL> vids = csvMan.ReadFromCSV<VidsSQL>();
            //mysqlBenchmarks.ImportCSV(bulkVidsCSVFilePath);
            //pgsBenchmarks.ImportData(vids);
            //mongoDBBenchmarks.ImportData(testData);

            Debug.Print($"Generated Data: {amount}");
        }


        MongoDBSearchBenchmarks mongoSearch = new MongoDBSearchBenchmarks();
        MysqlSearchBenchmarks mysqlSearch = new MysqlSearchBenchmarks();
        PostgresSearchBenchmarks postgresSearch = new PostgresSearchBenchmarks();
        private void btnSearchText_Click(object sender, EventArgs e)
        {
            string searchTerm = tbSearchText.Text;
            mongoSearch.RunSimpleSearchTest(searchTerm);
            mysqlSearch.RunSimpleSearchTest(searchTerm);
            postgresSearch.RunSimpleSearchTest(searchTerm);
        }
    }
}
