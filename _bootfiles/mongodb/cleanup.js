// usage: 
// mongosh mongodb://localhost:27017 --file cleanup.js   
// mongosh --file cleanup.js

// alt usage, if already inside mongosh
// load('cleanup.js');

use testdb
db.dropUser('testuser')
db.dropDatabase()
