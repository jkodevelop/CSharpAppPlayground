using CSharpAppPlayground.DBClasses.MysqlBenchmark;
using System.Diagnostics;

namespace CSharpAppPlayground.DBClasses
{
    public partial class FormDBBenchmark : Form
    {
        public FormDBBenchmark()
        {
            InitializeComponent();
        }

        MysqlBasicBenchmarks mysqlBenchmarks = new MysqlBasicBenchmarks();
        private void btnBenchmarkInserts_Click(object sender, EventArgs e)
        {
            mysqlBenchmarks.RunBulkInsertBenchmark(10);
            int insertedCount = mysqlBenchmarks.GetVidsCount();
            Debug.Print($"Inserted:{insertedCount}");
            // mysqlBenchmarks.ResetVidsTable();
        }
    }
}
