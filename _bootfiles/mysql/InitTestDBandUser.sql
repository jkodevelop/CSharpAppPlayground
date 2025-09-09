CREATE DATABASE IF NOT EXISTS testdb;

CREATE USER IF NOT EXISTS 'testuser'@'localhost' IDENTIFIED BY 'testpassword';

GRANT ALL PRIVILEGES ON testdb.* TO 'testuser'@'localhost';

FLUSH PRIVILEGES;
