using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CSharpAppPlayground.DBClasses.Data.BSONbenchmark
{
    // This is used for Reading the MongoDB object, need to map _id from database to the class

    // BSON representation of Vids for MongoDB + Vids object attribute
    // For insert benchmarking better to not have ObjectId in the object to allow for Mongodb to auto generate it internally
    // Else there is going to be duplicate key errors on inserts on multiple methods tests
    public class VidsBSONwithId : Vids
    {
        public static string collection = "Vids";

        [BsonElement("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }

        public long? filesizebyte { get; set; }

        public override string ToString()
        {
            return $"Mongo Collection: {collection}";
        }
    }
}
