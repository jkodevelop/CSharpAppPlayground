using CSharpAppPlayground.DBClasses.MysqlExamples;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
