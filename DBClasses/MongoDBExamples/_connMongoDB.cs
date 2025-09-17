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
        private MongoClient client;
        private bool connected = false;
        private string connectionStr = string.Empty;

        public _connMongoDB()
        {
           
        }

        public MongoClient getClient()
        {
            if(connected && client != null)
                return client;

            try
            {
                // Connection string to connect to MongoDB
                connectionStr = ConfigurationManager.ConnectionStrings["MongoDBConnection"].ConnectionString;
                // how many seconds to wait before a timeout occurs, default is 3 seconds in mongo db driver
                //MongoClientSettings settings = MongoClientSettings.FromConnectionString(connectionStr);
                //settings.ConnectTimeout = TimeSpan.FromSeconds(2);
                //settings.ServerSelectionTimeout = TimeSpan.FromSeconds(2);
                //MongoClient client = new MongoClient(settings);

                // Create a new MongoDB client, without settings
                client = new MongoClient(connectionStr);
                connected = true;
            }
            catch (Exception ex)
            {
                Debug.Print($"Error creating MongoDB client: {ex.Message}");
            }
            return client;
        }

        public string getServerVersion()
        {
            string serverVersion = "N/A";

            getClient();
            if (client != null)
            {
                try
                {
                    // Test the connection by pinging the server
                    IMongoDatabase adminDatabase = client.GetDatabase("testdb");
                    BsonDocument pingCommand = new BsonDocument("ping", 1);
                    BsonDocument result = adminDatabase.RunCommand<BsonDocument>(pingCommand);

                    Debug.Print($"Successfully connected to MongoDB.");
                    Debug.Print($"MongoDB Server Version: {client.Settings.Server}");

                    // Get server info
                    BsonDocument serverInfo = client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("buildInfo", 1));
                    if (serverInfo.Contains("version"))
                    {
                        serverVersion = serverInfo["version"].AsString;
                        // Debug.Print($"MongoDB Version: {serverInfo["version"]}");
                    }
                }
                catch (Exception ex)
                {
                    Debug.Print($"Error connecting to MongoDB: {ex.Message}");
                }
            }

            return $"MongoDB server version: {serverVersion}"; ;
        }
    }
}
