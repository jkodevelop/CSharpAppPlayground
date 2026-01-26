-- MySQL Schema Creation Script for SqlDBObject
-- This script creates the table structure for the SqlDBObject class

-- Create database if it doesn't exist (uncomment if needed)
-- CREATE DATABASE IF NOT EXISTS testdb;
USE testdb;

-- Drop table if it exists (for clean recreation)
DROP TABLE IF EXISTS SqlDBObject;

-- Create the SqlDBObject table
CREATE TABLE SqlDBObject (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    
    -- Add indexes for better performance
    INDEX idx_name (Name),
    INDEX idx_created_at (CreatedAt)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Insert sample data (optional)
INSERT INTO SqlDBObject (Name, CreatedAt) VALUES 
('Sample Object 1', NOW()),
('Sample Object 2', NOW()),
('Test Object', NOW());

-- Verify table creation
DESCRIBE SqlDBObject;

-- Show sample data
SELECT * FROM SqlDBObject;
