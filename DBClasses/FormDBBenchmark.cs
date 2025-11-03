using CSharpAppPlayground.DBClasses.MysqlBenchmark;
using CSharpAppPlayground.DBClasses.PostgresBenchmark;
using CSharpAppPlayground.UIClasses;
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
        private void btnBenchmarkInserts_Click(object sender, EventArgs e)
        {
            int amount = (int)numAmount.Value;

            mysqlBenchmarks.RunBulkInsertBenchmark(amount);
            int mysqlInsertedCount = mysqlBenchmarks.GetVidsCount();
            Debug.Print($"** MySQL Inserted:{mysqlInsertedCount}\n");

            pgsBenchmarks.RunBulkInsertBenchmark(amount);
            int pgsInsertedCount = pgsBenchmarks.GetVidsCount();
            Debug.Print($"** PostgreSQL Inserted:{pgsInsertedCount}\n");
        }

        private void btnResetTables_Click(object sender, EventArgs e)
        {
            mysqlBenchmarks.ResetVidsTable();
            pgsBenchmarks.ResetVidsTable();
        }
    }
}
