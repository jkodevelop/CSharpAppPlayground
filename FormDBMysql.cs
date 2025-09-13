using CSharpAppPlayground.DBClasses.MysqlExamples;
using System.Diagnostics;

namespace CSharpAppPlayground
{
    public partial class FormDBMysql : Form
    {
        _connMysql mysql;
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
    }
}
