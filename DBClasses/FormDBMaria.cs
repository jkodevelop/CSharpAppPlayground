using CSharpAppPlayground.DBClasses.MariaDBExamples;
using System.Diagnostics;

namespace CSharpAppPlayground.DBClasses
{
    public partial class FormDBMaria : Form
    {
        _connMariaDB maria;

        MariaDBBaseExamples basicExamples = new MariaDBBaseExamples();
        public FormDBMaria()
        {
            InitializeComponent();
            Init();
        }

        protected void Init()
        {
            maria = new _connMariaDB();
        }

        private void btnMariaStatus_Click(object sender, EventArgs e)
        {
            string serverVersion = maria.getServerVersion();
            Debug.Print($"{serverVersion}");
        }

        private void btnMariaBasicExample_Click(object sender, EventArgs e)
        {
            basicExamples.RunBasicExample();
        }
    }
}
