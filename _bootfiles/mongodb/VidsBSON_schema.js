use testdb;

var collectionName = "Vids";

db[collectionName].drop();

var indexSpecs = [
    {
        "key": { "_id": 1.0 },
        "name": "_id_",
        // 'v' is an internal field and should not be specified in createIndex
    },
    {
        "key": {
            "filename": "text" // Simplified key for the createIndex command
        },
        "name": "idx_filename",
        "weights": { "filename": 1.0 },
        "default_language": "english",
        "language_override": "language",
        // 'v', '_fts', '_ftsx', 'textIndexVersion' are internal fields
    },
    {
        "key": { "filesizebyte": 1.0 },
        "name": "idx_filesize"
    },
    {
        "key": { "duration": 1.0 },
        "name": "idx_duration"
    },
    {
        "key": { "metadatetime": 1.0 },
        "name": "idx_metadatetime"
    },
    {
        "key": { "width": 1.0, "height": 1.0 },
        "name": "idx_dimensions"
    }
];

// Iterate over the array and create each index
indexSpecs.forEach(function (spec) {
    // Separate the key (index definition) from the options
    var indexKey = spec.key;
    var indexOptions = Object.assign({}, spec); // Copy all original fields to options
    delete indexOptions.key; // Remove 'key' from options

    // Note: For text indexes, we must handle the simplified key correctly for createIndex.
    // The input JSON format is from a metadata dump, not a direct createIndex input.
    // The corrected indexSpecs array above already handles this by simplifying the 'key' field.

    console.log("Creating index: " + spec.name);
    try {
        db[collectionName].createIndex(indexKey, indexOptions);
    } catch (e) {
        console.error("Error creating index " + spec.name + ": " + e);
    }
});

print("Collection + Indexes creation process finished.");


// Insert sample data
db[collectionName].insertMany([
{
    "filename" : "top amazing list blender",
    "duration" : NumberInt(2927),
    "metadatetime" : ISODate("2018-09-01T04:00:00.000+0000"),
    "width" : NumberInt(640),
    "height" : NumberInt(720),
    "filesizebyte" : NumberLong(4236804352)
},{
    "filename" : "top values blender",
    "duration" : NumberInt(2927),
    "metadatetime" : ISODate("2015-06-12T04:00:00.000+0000"),
    "width" : NumberInt(480),
    "height" : NumberInt(720),
    "filesizebyte" : NumberLong(4369729281)
},{
    "filename" : "top parameters blender",
    "duration" : NumberInt(2927),
    "metadatetime" : ISODate("2016-06-12T04:00:00.000+0000"),
    "width" : NumberInt(640),
    "height" : NumberInt(720),
    "filesizebyte" : NumberLong(4369729281)
},{
    "filename" : "list of shirts",
    "duration" : NumberInt(3000),
    "metadatetime" : ISODate("2018-06-12T04:00:00.000+0000"),
    "width" : NumberInt(640),
    "height" : NumberInt(720),
    "filesizebyte" : NumberLong(4369729281)
}
]);
