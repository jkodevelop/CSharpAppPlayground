using CSharpAppPlayground.DBClasses.Data;
using CSharpAppPlayground.DBClasses.Data.BSONbenchmark;
using MethodTimer;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Configuration;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CSharpAppPlayground.DBClasses.MongoDBBenchmark
{
    public class MongoDBSearchBenchmark
    {
        private string connectionStr = string.Empty;
        private MongoClient client;
        private IMongoDatabase database;
        private IMongoCollection<VidsBSON> collection;

        public MongoDBSearchBenchmark()
        {
            // Get connection string from App.config
            connectionStr = ConfigurationManager.ConnectionStrings["MongoDBConnection"].ConnectionString;

            // Initialize MongoDB client and database
            client = new MongoClient(connectionStr);
            database = client.GetDatabase("testdb");
            collection = database.GetCollection<VidsBSON>("Vids");
        }

        public List<VidsBSON> SearchContain(string searchTerm)
        {
            var filter = Builders<VidsBSON>.Filter.Regex(
                f => f.filename, 
                new BsonRegularExpression($".*{Regex.Escape(searchTerm)}.*", "i") // "i" makes it case-insensitive
            );
            List<VidsBSON> results = collection.Find(filter).ToList();
            Debug.Print($"found number {results.Count}");
            return results;
        }
    }
}
