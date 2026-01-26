using CSharpAppPlayground.DBClasses.MongoDBExamples;
using System.Diagnostics;

namespace CSharpAppPlayground.DBClasses
{
    public partial class FormDBMongo : Form
    {
        public _connMongoDB mongo;

        protected MongoDBBaseExamples basicExamples = new MongoDBBaseExamples();
        public FormDBMongo()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            mongo = new _connMongoDB();
        }

        private void btnMongoStatus_Click(object sender, EventArgs e)
        {
            string serverVersion = mongo.getServerVersion();
            Debug.Print($"{serverVersion}");
        }

        private void btnMongoBasicExample_Click(object sender, EventArgs e)
        {
            basicExamples.RunBasicExample();
        }
    }
}
