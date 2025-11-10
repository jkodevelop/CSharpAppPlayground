using CSharpAppPlayground.Classes.DataGen.Generators;
using CSharpAppPlayground.DBClasses.Data;
using MethodTimer;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Configuration;
using System.Diagnostics;

namespace CSharpAppPlayground.DBClasses.MongoDBBenchmark
{
    public class MongoDBBasicBenchmarks
    {
        private string connectionStr = string.Empty;
        private MongoClient client;
        private IMongoDatabase database;
        private IMongoCollection<Vids> collection;

        public MongoDBBasicBenchmarks()
        {
            // Get connection string from App.config
            connectionStr = ConfigurationManager.ConnectionStrings["MongoDBConnection"].ConnectionString;

            // Initialize MongoDB client and database
            client = new MongoClient(connectionStr);
            database = client.GetDatabase("testdb");
            collection = database.GetCollection<Vids>("Vids");
        }

        public void GenData(int dataSetSize)
        {
            GenerateVids generator = new GenerateVids();
            List<Vids> testData = generator.GenerateData(dataSetSize);
            InsertManyAPI(testData);
        }

        public void FastestCompareBenchmark(int dataSetSize)
        {
            GenerateVids generator = new GenerateVids();
            List<Vids> testData = generator.GenerateData(dataSetSize);
            Test_InsertManyAPI(testData);
        }

        public void RunBulkInsertBenchmark(int dataSetSize)
        {
            Debug.Print("=== MySQL Bulk Insert Examples with VidsSQL ===");

            // Generate test data
            GenerateVids generator = new GenerateVids();
            List<Vids> testData = generator.GenerateData(dataSetSize);
            Debug.Print($"Generated {testData.Count} test records\n");

            //Example 1: Single insert loop(baseline)
            Test_InsertSimpleLoop(testData);

            // Example 2: InsertMany() API
            Test_InsertManyAPI(testData);

        }

        [Time("InsertSimpleLoop:")]
        protected void Test_InsertSimpleLoop(List<Vids> testData)
        {
            Debug.Print("\n--- Method 1: Single Insert Loop ---");
            int insertedCount = InsertSimpleLoop(testData);
            Debug.Print($"Inserted {insertedCount} records using Single Loop\n");
        }

        [Time("InsertManyAPI:")]
        protected void Test_InsertManyAPI(List<Vids> testData)
        {
            Debug.Print("\n--- Method 2: InserMany() ---");
            int insertedCount = InsertManyAPI(testData);
            Debug.Print($"Inserted {insertedCount} records using InsertMany()\n");
        }

        /// <summary>
        /// Insert documents one-by-one using InsertOne.
        /// Returns the elapsed milliseconds taken to insert all documents.
        /// </summary>
        public int InsertSimpleLoop(List<Vids> vids)
        {
            if (vids == null) return 0;

            int insertedCount = 0;
            foreach (var vid in vids)
            {
                try
                {
                    collection.InsertOne(vid);
                    insertedCount++;
                }
                catch (Exception ex)
                {
                    Debug.Print($"InsertOne failed: {ex.Message}");
                }
            }

            return insertedCount;
        }

        /// <summary>
        /// Insert documents in batches using InsertMany. Batch size defaults to 1000.
        /// Returns the elapsed milliseconds taken to insert all documents.
        /// </summary>
        public int InsertManyAPI(List<Vids> vids)
        {
            if (vids == null) return 0;
            try
            {
                collection.InsertMany(vids);
            }
            catch (MongoBulkWriteException mwx)
            {
                Debug.Print($"Bulk insert partial failure: {mwx.Message}");
            }
            catch (Exception ex)
            {
                Debug.Print($"InsertMany failed: {ex.Message}");
            }
            return vids.Count;
        }

        public long DeleteAll()
        {
            // this is slow because its deletes row by row, there is a faster option: Drop()
            // BUT this would also remove all indexes, so don't do that
            try
            {
                // 1. Delete all rows
                DeleteResult result = collection.DeleteMany(_ => true);
                Debug.Print($"Deleted {result.DeletedCount} documents from collection");
                // 2. Truncate all indexed data, free up the spaces and reset
                var command = new BsonDocument
                {
                    { "compact", "Vids" }
                };
                var compactResult = database.RunCommand<BsonDocument>(command);
                Debug.Print("Collection compacted: " + compactResult.ToJson());
                return result.DeletedCount;
            }
            catch (Exception ex)
            {
                Debug.Print($"Error deleting all documents: {ex.Message}");
            }
            return 0;
        }

        public long GetVidsCount()
        {
            try
            {
                long count = collection.CountDocuments(_ => true);
                Debug.Print($"Total documents in collection: {count}");
                return count;
            }
            catch (Exception ex)
            {
                Debug.Print($"Error counting documents: {ex.Message}");
            }
            return -1;
        }
    }
}
