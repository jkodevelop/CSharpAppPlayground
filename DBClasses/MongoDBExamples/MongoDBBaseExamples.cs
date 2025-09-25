using CSharpAppPlayground.DBClasses.Data;
using CSharpAppPlayground.DBClasses.MongoDBExamples.Collections;
using CSharpAppPlayground.DIExample.median;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Configuration;
using System.Diagnostics;

// source:
// https://antondevtips.com/blog/best-practices-when-working-with-mongodb-in-dotnet

namespace CSharpAppPlayground.DBClasses.MongoDBExamples
{
    public class MongoDBBaseExamples
    {
        // SIMPLER VERSION WITHOUT DEPENDENCY INJECTION
        //
        /*
        private string connectionStr = string.Empty;
        private MongoClient client;
        private IMongoDatabase database;
        private IMongoCollection<MongoDBObject> collection;

        public MongoDBBaseExamples()
        {
            // Get connection string from App.config
            connectionStr = ConfigurationManager.ConnectionStrings["MongoDBConnection"].ConnectionString;
            
            // Initialize MongoDB client and database
            client = new MongoClient(connectionStr);
            database = client.GetDatabase("testdb");
            collection = database.GetCollection<MongoDBObject>("testcollection");
        }
        */

        // VERSION WITH DEPENDENCY INJECTION
        private _connMongoDBStandard _conn = new _connMongoDBStandard();
        private IMongoDatabase database;
        private IMongoCollection<MongoDBObject> collection;

        public MongoDBBaseExamples()
        {
            ServiceProvider serviceProvider = _conn.GetServiceProvider();

            database = serviceProvider
                .GetRequiredService<IMongoClient>()
                .GetDatabase(_connMongoDBStandard.dbName);

            // Option 1. get collection example basic
            // 
            //collection = serviceProvider
            //    .GetRequiredService<IMongoClient>()
            //    .GetDatabase(_connMongoDBStandard.dbName)
            //    .GetCollection<MongoDBObject>("MongoDBObject");

            // Option 2. get collection using CollectionsManagerm - DI example
            //
            collection = serviceProvider.GetRequiredService<CollectionsManager>().MongoDBObjectCollection; // implicitly requesting the service via interface
        }

        #region Insert Operations

        public async Task<string> InsertSingleAsync(string name)
        {
            try
            {
                MongoDBObject document = new MongoDBObject(name);
                await collection.InsertOneAsync(document);
                Debug.Print($"Inserted document with ID: {document.Id}");
                return document.Id;
            }
            catch (Exception ex)
            {
                Debug.Print($"Error inserting document: {ex.Message}");
                throw;
            }
        }

        public async Task<long> InsertMultipleAsync(List<string> names)
        {
            try
            {
                List<MongoDBObject> documents = names.Select(name => new MongoDBObject(name)).ToList();
                await collection.InsertManyAsync(documents);
                Debug.Print($"Inserted {documents.Count} documents");
                return documents.Count;
            }
            catch (Exception ex)
            {
                Debug.Print($"Error inserting multiple documents: {ex.Message}");
                throw;
            }
        }

        public string InsertSingle(string name)
        {
            try
            {
                MongoDBObject document = new MongoDBObject(name);
                collection.InsertOne(document);
                Debug.Print($"Inserted document with ID: {document.Id}");
                return document.Id;
            }
            catch (Exception ex)
            {
                Debug.Print($"Error inserting document: {ex.Message}");
                throw;
            }
        }

        #endregion

        #region Select Operations

        public async Task<MongoDBObject?> FindByIdAsync(string id)
        {
            try
            {
                FilterDefinition<MongoDBObject> filter = Builders<MongoDBObject>.Filter.Eq(x => x.Id, id);
                MongoDBObject? document = await collection.Find(filter).FirstOrDefaultAsync();
                Debug.Print(document != null ? $"Found document: {document}" : "Document not found");
                return document;
            }
            catch (Exception ex)
            {
                Debug.Print($"Error finding document by ID: {ex.Message}");
                throw;
            }
        }

        public async Task<List<MongoDBObject>> FindByNameAsync(string name)
        {
            try
            {
                FilterDefinition<MongoDBObject> filter = Builders<MongoDBObject>.Filter.Eq(x => x.Name, name);
                List<MongoDBObject> documents = await collection.Find(filter).ToListAsync();
                Debug.Print($"Found {documents.Count} documents with name '{name}'");
                return documents;
            }
            catch (Exception ex)
            {
                Debug.Print($"Error finding documents by name: {ex.Message}");
                throw;
            }
        }

        public async Task<List<MongoDBObject>> FindAllAsync()
        {
            try
            {
                List<MongoDBObject> documents = await collection.Find(_ => true).ToListAsync();
                Debug.Print($"Found {documents.Count} documents in collection");
                return documents;
            }
            catch (Exception ex)
            {
                Debug.Print($"Error finding all documents: {ex.Message}");
                throw;
            }
        }

        public async Task<List<MongoDBObject>> FindWithPaginationAsync(int pageNumber, int pageSize)
        {
            try
            {
                int skip = (pageNumber - 1) * pageSize;
                List<MongoDBObject> documents = await collection.Find(_ => true)
                    .Skip(skip)
                    .Limit(pageSize)
                    .ToListAsync();
                Debug.Print($"Found {documents.Count} documents on page {pageNumber}");
                return documents;
            }
            catch (Exception ex)
            {
                Debug.Print($"Error finding documents with pagination: {ex.Message}");
                throw;
            }
        }

        public async Task<List<MongoDBObject>> FindByDateAfterAsync(DateTime date)
        {
            try
            {
                FilterDefinition<MongoDBObject> filter = Builders<MongoDBObject>.Filter.Gt(x => x.CreatedAt, date);
                List<MongoDBObject> documents = await collection.Find(filter).ToListAsync();
                Debug.Print($"Found {documents.Count} documents created after {date:yyyy-MM-dd HH:mm:ss}");
                return documents;
            }
            catch (Exception ex)
            {
                Debug.Print($"Error finding documents by date: {ex.Message}");
                throw;
            }
        }

        public async Task<long> CountDocumentsAsync()
        {
            try
            {
                long count = await collection.CountDocumentsAsync(_ => true);
                Debug.Print($"Total documents in collection: {count}");
                return count;
            }
            catch (Exception ex)
            {
                Debug.Print($"Error counting documents: {ex.Message}");
                throw;
            }
        }

        #endregion

        #region Update Operations

        public async Task<bool> UpdateByIdAsync(string id, string newName)
        {
            try
            {
                FilterDefinition<MongoDBObject> filter = Builders<MongoDBObject>.Filter.Eq(x => x.Id, id);
                UpdateDefinition<MongoDBObject> update = Builders<MongoDBObject>.Update.Set(x => x.Name, newName);
                UpdateResult result = await collection.UpdateOneAsync(filter, update);
                
                bool updated = result.ModifiedCount > 0;
                Debug.Print(updated ? $"Updated document with ID: {id}" : $"Document with ID {id} not found");
                return updated;
            }
            catch (Exception ex)
            {
                Debug.Print($"Error updating document: {ex.Message}");
                throw;
            }
        }

        public async Task<long> UpdateByNameAsync(string oldName, string newName)
        {
            try
            {
                FilterDefinition<MongoDBObject> filter = Builders<MongoDBObject>.Filter.Eq(x => x.Name, oldName);
                UpdateDefinition<MongoDBObject> update = Builders<MongoDBObject>.Update.Set(x => x.Name, newName);
                UpdateResult result = await collection.UpdateManyAsync(filter, update);
                
                Debug.Print($"Update    d {result.ModifiedCount} documents from '{oldName}' to '{newName}'");
                return result.ModifiedCount;
            }
            catch (Exception ex)
            {
                Debug.Print($"Error updating documents by name: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> ReplaceDocumentAsync(string id, MongoDBObject newDocument)
        {
            try
            {
                FilterDefinition<MongoDBObject> filter = Builders<MongoDBObject>.Filter.Eq(x => x.Id, id);
                ReplaceOneResult result = await collection.ReplaceOneAsync(filter, newDocument);
                
                bool replaced = result.ModifiedCount > 0;
                Debug.Print(replaced ? $"Replaced document with ID: {id}" : $"Document with ID {id} not found");
                return replaced;
            }
            catch (Exception ex)
            {
                Debug.Print($"Error replacing document: {ex.Message}");
                throw;
            }
        }

        public bool UpdateById(string id, string newName)
        {
            try
            {
                FilterDefinition<MongoDBObject> filter = Builders<MongoDBObject>.Filter.Eq(x => x.Id, id);
                UpdateDefinition<MongoDBObject> update = Builders<MongoDBObject>.Update.Set(x => x.Name, newName);
                UpdateResult result = collection.UpdateOne(filter, update);
                
                bool updated = result.ModifiedCount > 0;
                Debug.Print(updated ? $"Updated document with ID: {id}" : $"Document with ID {id} not found");
                return updated;
            }
            catch (Exception ex)
            {
                Debug.Print($"Error updating document: {ex.Message}");
                throw;
            }
        }

        #endregion

        #region Delete Operations

        public async Task<bool> DeleteByIdAsync(string id)
        {
            try
            {
                FilterDefinition<MongoDBObject> filter = Builders<MongoDBObject>.Filter.Eq(x => x.Id, id);
                DeleteResult result = await collection.DeleteOneAsync(filter);
                
                bool deleted = result.DeletedCount > 0;
                Debug.Print(deleted ? $"Deleted document with ID: {id}" : $"Document with ID {id} not found");
                return deleted;
            }
            catch (Exception ex)
            {
                Debug.Print($"Error deleting document: {ex.Message}");
                throw;
            }
        }

        public async Task<long> DeleteByNameAsync(string name)
        {
            try
            {
                FilterDefinition<MongoDBObject> filter = Builders<MongoDBObject>.Filter.Eq(x => x.Name, name);
                DeleteResult result = await collection.DeleteManyAsync(filter);
                
                Debug.Print($"Deleted {result.DeletedCount} documents with name '{name}'");
                return result.DeletedCount;
            }
            catch (Exception ex)
            {
                Debug.Print($"Error deleting documents by name: {ex.Message}");
                throw;
            }
        }

        public async Task<long> DeleteAllAsync()
        {
            try
            {
                DeleteResult result = await collection.DeleteManyAsync(_ => true);
                Debug.Print($"Deleted {result.DeletedCount} documents from collection");
                return result.DeletedCount;
            }
            catch (Exception ex)
            {
                Debug.Print($"Error deleting all documents: {ex.Message}");
                throw;
            }
        }

        public async Task<long> DeleteByDateBeforeAsync(DateTime date)
        {
            try
            {
                FilterDefinition<MongoDBObject> filter = Builders<MongoDBObject>.Filter.Lt(x => x.CreatedAt, date);
                DeleteResult result = await collection.DeleteManyAsync(filter);
                Debug.Print($"Deleted {result.DeletedCount} documents created before {date:yyyy-MM-dd HH:mm:ss}");
                return result.DeletedCount;
            }
            catch (Exception ex)
            {
                Debug.Print($"Error deleting documents by date: {ex.Message}");
                throw;
            }
        }

        public bool DeleteById(string id)
        {
            try
            {
                FilterDefinition<MongoDBObject> filter = Builders<MongoDBObject>.Filter.Eq(x => x.Id, id);
                DeleteResult result = collection.DeleteOne(filter);
                
                bool deleted = result.DeletedCount > 0;
                Debug.Print(deleted ? $"Deleted document with ID: {id}" : $"Document with ID {id} not found");
                return deleted;
            }
            catch (Exception ex)
            {
                Debug.Print($"Error deleting document: {ex.Message}");
                throw;
            }
        }

        #endregion

        #region Utility Methods

        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                await database.RunCommandAsync((Command<BsonDocument>)"{ping:1}");
                Debug.Print("MongoDB connection test successful");
                return true;
            }
            catch (Exception ex)
            {
                Debug.Print($"MongoDB connection test failed: {ex.Message}");
                return false;
            }
        }

        public async Task<string> GetCollectionStatsAsync()
        {
            try
            {
                long count = await collection.CountDocumentsAsync(_ => true);
                BsonDocument stats = await database.RunCommandAsync<BsonDocument>("{collStats: 'testcollection'}");
                
                string result = @$"Collection Statistics:\n
                              - Document Count: {count}\n
                              - Collection Size: {stats.GetValue("size", 0)} bytes\n
                              - Average Document Size: {stats.GetValue("avgObjSize", 0)} bytes";
                
                Debug.Print(result);
                return result;
            }
            catch (Exception ex)
            {
                Debug.Print($"Error getting collection stats: {ex.Message}");
                return "Error retrieving collection statistics";
            }
        }

        #endregion

        public void RunBasicExample()
        {
            Debug.Print("Running MongoDB Basic Example...");
            try
            {
                // 1. INSERT: Create a new MongoDBObject and insert it into the collection
                var newObj = new MongoDBObject { Name = "Inserted Object", CreatedAt = DateTime.UtcNow };
                collection.InsertOne(newObj);
                Debug.Print($"Inserted document with Id: {newObj.Id}");

                // 2. SELECT: Find the inserted object by name
                var filter = Builders<MongoDBObject>.Filter.Eq(x => x.Name, "Inserted Object");
                var foundObj = collection.Find(filter).FirstOrDefault();
                if (foundObj != null)
                {
                    // Debug.Print($"Found document: Id={foundObj.Id}, Name={foundObj.Name}, CreatedAt={foundObj.CreatedAt}");
                    Debug.Print($"Found document: {foundObj.ToString()}");
                }

                // 3. UPDATE: Update the Name of the inserted object
                var update = Builders<MongoDBObject>.Update.Set(x => x.Name, "Updated Object");
                var updateResult = collection.UpdateOne(filter, update);
                Debug.Print($"Matched {updateResult.MatchedCount} document(s), Modified {updateResult.ModifiedCount} document(s)");

                // 4. SELECT: Verify the update
                var updatedFilter = Builders<MongoDBObject>.Filter.Eq(x => x.Name, "Updated Object");
                var updatedObj = collection.Find(updatedFilter).FirstOrDefault();
                if (updatedObj != null)
                {
                    Debug.Print($"Updated document: {updatedObj.ToString()}");
                }

                // 5. DELETE: Delete the updated object
                var deleteFilter = Builders<MongoDBObject>.Filter.Eq(x => x.Id, updatedObj.Id);
                var deleteResult = collection.DeleteOne(deleteFilter);
                Debug.Print($"Deleted {deleteResult.DeletedCount} document(s)");

                // 6. SELECT: Verify deletion
                var deletedObj = collection.Find(deleteFilter).FirstOrDefault();
                if (deletedObj == null)
                    Debug.Print("Document successfully deleted.");
                else
                    Debug.Print("Document still exists after delete.");
            }
            catch (Exception ex)
            {
                Debug.Print($"Error in MongoDB Basic Example: {ex.Message}");
            }
        }
    }
}
