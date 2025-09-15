// usage: 
// mongosh mongodb://localhost:27017 --file InitTestDBandUser.js   
// mongosh --file InitTestDBandUser.js

// alt usage, if already inside mongosh
// load('InitTestDBandUser.js');

use testdb

db.createUser(
   {
     user: "testuser",
     pwd: "testpassword",
     roles: [ { role: "readWrite", db: "testdb" } ]
   }
);

/*
db.createUser(
   {
     user: "testuser",
     pwd: passwordPrompt(), // or specify the password directly
     roles: [ { role: "readWrite", db: "testdb" }, { role: "read", db: "reporting" } ]
   }
);
*/
