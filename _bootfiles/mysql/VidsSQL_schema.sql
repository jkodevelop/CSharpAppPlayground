-- SQL Schema Creation Script for VidsSQL Object
-- This script creates the Vids table based on the VidsSQL C# class properties
-- Mysql DEFAULT engine is ENGINE=InnoDB

-- Drop table if exists (for clean setup)
DROP TABLE IF EXISTS vids;

-- Create Vids table
CREATE TABLE Vids (
    id INT PRIMARY KEY AUTO_INCREMENT,                    -- Primary key, auto-increment
    filename VARCHAR(500) NOT NULL,                        -- Video filename (required)
    filesizebyte BIGINT NULL,                             -- File size in bytes (nullable)
    duration INT NULL,                                    -- Duration in seconds (nullable)
    metadatetime DATETIME NULL,                           -- Metadata datetime (nullable)
    width INT NULL,                                       -- Video width in pixels (nullable)
    height INT NULL,                                      -- Video height in pixels (nullable)
    
    -- Add indexes for common queries
    INDEX idx_filename (filename),
    INDEX idx_filesize (filesizebyte),
    INDEX idx_duration (duration),
    INDEX idx_metadatetime (metadatetime),
    INDEX idx_dimensions (width, height)
);

-- Add table comment
ALTER TABLE Vids COMMENT = 'Video metadata storage table';
 
-- Add column comments for documentation
ALTER TABLE Vids MODIFY COLUMN id INT AUTO_INCREMENT COMMENT 'Primary key identifier';
ALTER TABLE Vids MODIFY COLUMN filename VARCHAR(500) NOT NULL COMMENT 'Video filename';
ALTER TABLE Vids MODIFY COLUMN filesizebyte BIGINT NULL COMMENT 'File size in bytes';
ALTER TABLE Vids MODIFY COLUMN duration INT NULL COMMENT 'Video duration in seconds';
ALTER TABLE Vids MODIFY COLUMN metadatetime DATETIME NULL COMMENT 'Metadata creation/modification datetime';
ALTER TABLE Vids MODIFY COLUMN width INT NULL COMMENT 'Video width in pixels';
ALTER TABLE Vids MODIFY COLUMN height INT NULL COMMENT 'Video height in pixels';
