CREATE DATABASE IF NOT EXISTS testdb;

CREATE USER IF NOT EXISTS 'testuser'@'localhost' IDENTIFIED BY 'testpassword';

GRANT ALL PRIVILEGES ON testdb.* TO 'testuser'@'localhost';

-- this is required for LOAD DATA INFILE, ALL covers this permission
-- GRANT FILE ON *.* TO 'testuser'@'localhost';


FLUSH PRIVILEGES;
