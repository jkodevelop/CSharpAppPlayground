// usage: 
// mongosh mongodb://localhost:27017 --file cleanup.js   
// mongosh --file cleanup.js

// alt usage, if already inside mongosh
// load('cleanup.js');

use testdb
db.dropUser('testuser')
db.dropDatabase()

// for replica sets and sharded clusters, ensure the dropUser command is acknowledged by a majority of nodes
//
//db.runCommand({
//    dropUser: "username",
//    writeConcern: { w: "majority", wtimeout: 5000 }
//})