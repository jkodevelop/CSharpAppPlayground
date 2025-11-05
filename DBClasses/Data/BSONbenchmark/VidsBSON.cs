using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CSharpAppPlayground.DBClasses.Data.BSONbenchmark
{
    // BSON representation of Vids for MongoDB + Vids object attribute
    // For insert benchmarking better to not have ObjectId in the object to allow for Mongodb to auto generate it internally
    // Else there is going to be duplicate key errors on inserts on multiple methods tests
    public class VidsBSON : Vids
    {
        private string collection = "Vids";

        [BsonElement("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }

        public override string ToString()
        {
            return $"Mongo Collection: {collection}";
        }
    }
}
