using CSharpAppPlayground.DBClasses.Data;
using MongoDB.Driver;

namespace CSharpAppPlayground.DBClasses.MongoDBExamples.Collections
{
    // DOCUMENT usage: inject IMongoClient via DI, then use this class to get collections
    public class CollectionsManager(IMongoClient mongoClient)
    {
        // private static string dbName = "testdb"; // Example

        private readonly IMongoDatabase _database = mongoClient.GetDatabase(_connMongoDBStandard.dbName);

        public IMongoCollection<MongoDBObject> MongoDBObjectCollection 
            => _database.GetCollection<MongoDBObject>("MongoDBObject");
    }
}
