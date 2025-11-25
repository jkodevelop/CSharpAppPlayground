using CSharpAppPlayground.DBClasses.Data;
using CSharpAppPlayground.DBClasses.Data.BSONbenchmark;
using MethodTimer;
using MongoDB.Bson;
using MongoDB.Driver;
using MySqlX.XDevAPI;
using System.Configuration;
using System.Diagnostics;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace CSharpAppPlayground.DBClasses.MongoDBBenchmark
{
    public class MongoDBSearchBenchmarks
    {
        private string connectionStr = string.Empty;
        private MongoClient client;
        private IMongoDatabase database;
        private IMongoCollection<VidsBSONwithId> collection;

        public MongoDBSearchBenchmarks()
        {
            // Get connection string from App.config
            connectionStr = ConfigurationManager.ConnectionStrings["MongoDBConnection"].ConnectionString;

            // Initialize MongoDB client and database
            client = new MongoClient(connectionStr);
            database = client.GetDatabase("testdb");
            collection = database.GetCollection<VidsBSONwithId>("Vids");
        }

        public void RunWordsSearchTest(string[] words)
        {
            Debug.Print("\n/////////////////////////////////////////////////////////" +
                "\nMONGO words search" +
                "\n/////////////////////////////////////////////////////////");
            Test_ContainWordsSearch(words);
            Test_FullTextSearch(string.Join(' ', words));
        }

        [Time("ContainWordsSearch")]
        private void Test_ContainWordsSearch(string[] words)
        {
            Debug.Print("\n--- Method 1: Contain Words Search ---");
            var v = ContainWordsSearch(words);
            Debug.Print($"Found {v.Count} ContainWordsSearch words:{string.Join(", ", words)}");
        }

        [Time("FullTextSearch")]
        private void Test_FullTextSearch(string words)
        {
            Debug.Print("\n--- Method 2: Full Text Search ---");
            var v = FullTextSearch(words);
            Debug.Print($"Found {v.Count} FullTextSearch words:{string.Join(", ", words)}");
        }

        public void RunSimpleSearchTest(string searchTerm)
        {
            Debug.Print("\n/////////////////////////////////////////////////////////" +
                "\nMONGO simple search" +
                "\n/////////////////////////////////////////////////////////");
            Test_SearchContain(searchTerm);
            Test_SearchContainCaseSensitive(searchTerm);
            Test_SearchExact(searchTerm);
        }

        [Time("SearchContain")]
        public void Test_SearchContain(string searchTerm)
        {
            Debug.Print("\n--- Method 1: Search Using Contains + Case Insensitive ---");
            var v = SearchContain(searchTerm);
            Debug.Print($"Found {v.Count} LIKE searchTerm:{searchTerm}");
        }

        [Time("SearchContainCaseSensitive")]
        public void Test_SearchContainCaseSensitive(string searchTerm)
        {
            Debug.Print("\n--- Method 2: Search Using Contains + Case Sensitive ---");
            var v = SearchContainCaseSensitive(searchTerm);
            Debug.Print($"Found {v.Count} LIKE + case searchTerm:{searchTerm}");
        }

        [Time("SearchExact")]
        public void Test_SearchExact(string searchTerm)
        {
            Debug.Print("\n--- Method 3: Search Exact ---");
            var v = SearchExact(searchTerm);
            Debug.Print($"Found {v.Count} EXACT searchTerm:{searchTerm}");
        }

        public List<VidsBSONwithId> SearchContain(string searchTerm)
        {
            var filter = Builders<VidsBSONwithId>.Filter.Regex(
                f => f.filename, 
                new BsonRegularExpression($".*{Regex.Escape(searchTerm)}.*", "i") // "i" makes it case-insensitive
            );
            List<VidsBSONwithId> results = collection.Find(filter).ToList();
            Debug.Print($"found number {results.Count}");
            return results;
        }

        public List<VidsBSONwithId> SearchContainCaseSensitive(string searchTerm)
        {
            var filter = Builders<VidsBSONwithId>.Filter.Regex(
                f => f.filename,
                new BsonRegularExpression($".*{Regex.Escape(searchTerm)}.*") // case-insensitive + contains
            );
            List<VidsBSONwithId> results = collection.Find(filter).ToList();
            Debug.Print($"found number {results.Count}");
            return results;
        }

        public List<VidsBSONwithId> SearchExact(string searchTerm)
        {
            var filter = Builders<VidsBSONwithId>.Filter.Eq(f => f.filename, searchTerm);
            List<VidsBSONwithId> results = collection.Find(filter).ToList();
            Debug.Print($"found number {results.Count}");
            return results;
        }

        public List<VidsBSONwithId> ContainWordsSearch(string[] words)
        {
            if (words == null || words.Length == 0)
                return new List<VidsBSONwithId>();

            var regexFilters = words
                .Where(w => !string.IsNullOrWhiteSpace(w))
                .Select(w => Builders<VidsBSONwithId>.Filter.Regex(
                    f => f.filename, new BsonRegularExpression(w, "i")))
                .ToList();

            var filter = regexFilters.Count == 0 
                ? Builders<VidsBSONwithId>.Filter.Empty 
                : Builders<VidsBSONwithId>.Filter.And(regexFilters);

            var results = collection.Find(filter).ToList();
            return results;
        }

        public List<VidsBSONwithId> FullTextSearch(string words)
        {
            //--create text index
            //db.collection.createIndex({ text: "text" })

            if (words == null || words.Length == 0)
                return new List<VidsBSONwithId>();

            var filter = Builders<VidsBSONwithId>.Filter.Text(words);
            var results = collection.Find(filter).ToList();
            return results;
        }
    }
}
