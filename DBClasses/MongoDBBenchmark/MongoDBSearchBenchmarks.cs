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
    public class MongoDBSearchBenchmarks
    {
        private string connectionStr = string.Empty;
        private MongoClient client;
        private IMongoDatabase database;
        private IMongoCollection<VidsBSON> collection;

        public MongoDBSearchBenchmarks()
        {
            // Get connection string from App.config
            connectionStr = ConfigurationManager.ConnectionStrings["MongoDBConnection"].ConnectionString;

            // Initialize MongoDB client and database
            client = new MongoClient(connectionStr);
            database = client.GetDatabase("testdb");
            collection = database.GetCollection<VidsBSON>("Vids");
        }

        public void RunTest(string searchTerm)
        {
            Test_SearchContain(searchTerm);
            Test_SearchContainCaseSensitive(searchTerm);
            Test_SearchExact(searchTerm);
        }

        [Time("SearchContain")]
        public void Test_SearchContain(string searchTerm)
        {
            Debug.Print("\n--- Method 1: Search Using Contains + Case Insensitive ---");
            SearchContain(searchTerm);
        }

        [Time("SearchContainCaseSensitive")]
        public void Test_SearchContainCaseSensitive(string searchTerm)
        {
            Debug.Print("\n--- Method 2: Search Using Contains + Case Sensitive ---");
            SearchContainCaseSensitive(searchTerm);
        }

        [Time("SearchExact")]
        public void Test_SearchExact(string searchTerm)
        {
            Debug.Print("\n--- Method 3: Search Exact ---");
            SearchExact(searchTerm);
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

        public List<VidsBSON> SearchContainCaseSensitive(string searchTerm)
        {
            var filter = Builders<VidsBSON>.Filter.Regex(
                f => f.filename,
                new BsonRegularExpression($".*{Regex.Escape(searchTerm)}.*") // case-insensitive + contains
            );
            List<VidsBSON> results = collection.Find(filter).ToList();
            Debug.Print($"found number {results.Count}");
            return results;
        }

        public List<VidsBSON> SearchExact(string searchTerm)
        {
            var filter = Builders<VidsBSON>.Filter.Eq(f => f.filename, searchTerm);
            List<VidsBSON> results = collection.Find(filter).ToList();
            Debug.Print($"found number {results.Count}");
            return results;
        }
    }
}
