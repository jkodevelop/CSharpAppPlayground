using CSharpAppPlayground.DBClasses.MongoDBExamples;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
namespace CSharpAppPlayground
{
    public partial class FormDBMongo : Form
    {
        public _connMongoDB mongo;
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
            mongo.connect();
        }
    }
}
