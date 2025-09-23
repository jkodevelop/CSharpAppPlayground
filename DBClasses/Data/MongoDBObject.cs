using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CSharpAppPlayground.DBClasses.Data
{
    public class MongoDBObject
    {
        // public ObjectId Id { get; set; } // one option for ID, not the best option

        // better option for ID, string representation of ObjectId, allows for Mongo to auto create the IDs
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("Name")]
        [BsonRequired]
        public string Name { get; set; } = string.Empty;

        [BsonElement("CreatedAt")]
        [BsonRequired]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public MongoDBObject()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public MongoDBObject(string name) : this()
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"MongoDBObject: Id={Id}, Name={Name}, CreatedAt={CreatedAt:yyyy-MM-dd HH:mm:ss}";
        }
    }
}
