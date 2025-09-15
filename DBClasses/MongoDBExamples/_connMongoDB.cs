using System.Configuration;
using System.Diagnostics;
using MongoDB.Driver;
using MongoDB.Bson;

/// <summary>
/// Required library for this MongoDB connection example
/// 
/// MongoDB.Driver
/// 
/// </summary>
namespace CSharpAppPlayground.DBClasses.MongoDBExamples
{
    public class _connMongoDB
    {
        public _connMongoDB()
        {
            connect();
        }

        public void connect()
        {
            // Connection string to connect to MongoDB
            string connectionStr = ConfigurationManager.ConnectionStrings["MongoDBConnection"].ConnectionString;

            try
            {
                // Create a new MongoDB client
                var client = new MongoClient(connectionStr);
                
                // Test the connection by pinging the server
                var adminDatabase = client.GetDatabase("testdb");
                var pingCommand = new BsonDocument("ping", 1);
                var result = adminDatabase.RunCommand<BsonDocument>(pingCommand);

                Debug.Print($"Successfully connected to MongoDB.");
                Debug.Print($"MongoDB Server Version: {client.Settings.Server}");
                
                // Get server info
                var serverInfo = client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("buildInfo", 1));
                if (serverInfo.Contains("version"))
                {
                    Debug.Print($"MongoDB Version: {serverInfo["version"]}");
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"Error connecting to MongoDB: {ex.Message}");
            }
        }
    }
}
