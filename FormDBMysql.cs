using CSharpAppPlayground.DBClasses.MysqlExamples;
using System.Diagnostics;

namespace CSharpAppPlayground
{
    public partial class FormDBMysql : Form
    {
        _connMysql mysql;

        MysqlBaseExamples baseExamples = new MysqlBaseExamples();
        public FormDBMysql()
        {
            InitializeComponent();
            Init();
        }

        protected void Init()
        {
            mysql = new _connMysql();
            // mysql.connect();
        }

        private void btnMysqlStatus_Click(object sender, EventArgs e)
        {
            string serverVersion = mysql.getServerVersion();
            Debug.Print($"{serverVersion}");
        }

        private void btnMysqlBasicExample_Click(object sender, EventArgs e)
        {
            // running basic example: insert, select, update, delete
            baseExamples.RunBasicExample();
        }
    }
}
