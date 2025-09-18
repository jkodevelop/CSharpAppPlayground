// MongoDB Schema Setup Script for MongoDBObject
// This script creates the collection and indexes for the MongoDBObject class

// Switch to the database (create if it doesn't exist)
use testdb;

var colName = "MongoDBObject";

// Drop the collection if it exists (for clean recreation)
// collection selection options:
//
// db.ExampleDBObject
// db[colName]
// db.getCollection(colName)

db[colName].drop();

// Create the ExampleDBObject collection
// Note: MongoDB creates collections implicitly when first document is inserted
// But we can create it explicitly and add validation rules

// Create collection with validation schema
db.createCollection(colName, {
    validator: {
        $jsonSchema: {
            bsonType: "object",
            required: ["Name", "CreatedAt"],
            properties: {
                _id: {
                    bsonType: "objectId",
                    description: "MongoDB ObjectId, auto-generated"
                },
                Name: {
                    bsonType: "string",
                    minLength: 1,
                    maxLength: 255,
                    description: "Name of the object, required field"
                },
                CreatedAt: {
                    bsonType: "date",
                    description: "Timestamp when the record was created"
                }
            }
        }
    }
});

// Create indexes for better performance
db[colName].createIndex({ "Name": 1 });
db[colName].createIndex({ "CreatedAt": 1 });

// Insert sample data
db[colName].insertMany([
    {
        Name: "Sample Object 1",
        CreatedAt: new Date()
    },
    {
        Name: "Sample Object 2", 
        CreatedAt: new Date()
    },
    {
        Name: "Test Object",
        CreatedAt: new Date()
    }
]);

// Verify collection creation and indexes
print("Collection created: " + db[colName].getName());
print("Indexes:");
db[colName].getIndexes().forEach(printjson);

// Show sample data
print("Sample documents:");
db[colName].find().forEach(printjson);

// Show collection statistics
print("Collection stats:");
printjson(db[colName].stats());
