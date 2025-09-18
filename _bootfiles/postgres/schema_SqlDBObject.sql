-- PostgreSQL Schema Creation Script for "SqlDBObject"
-- This script creates the table structure for the "SqlDBObject" class

-- Create database if it doesn't exist (uncomment if needed)
-- CREATE DATABASE testdb;
\c testdb;

-- Drop table if it exists (for clean recreation)
DROP TABLE IF EXISTS "SqlDBObject" CASCADE;

-- Create the "SqlDBObject" table
CREATE TABLE "SqlDBObject" (
    "Id" SERIAL PRIMARY KEY,
    "Name" VARCHAR(255) NOT NULL,
    "CreatedAt" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Add indexes for better performance
CREATE INDEX idx_sqldbobject_name ON "SqlDBObject" ("Name");
CREATE INDEX idx_sqldbobject_created_at ON "SqlDBObject" ("CreatedAt");

-- Add comments to the table and columns for documentation, (optional)
-- COMMENT ON TABLE "SqlDBObject" IS 'Table representing "SqlDBObject" entities from C# application';
-- COMMENT ON COLUMN "SqlDBObject"."Id" IS 'Primary key, auto-incrementing integer';
-- COMMENT ON COLUMN "SqlDBObject"."Name" IS 'Name of the object, required field';
-- COMMENT ON COLUMN "SqlDBObject"."CreatedAt" IS 'Timestamp when the record was created';

-- show comment on TABLE
--
-- SELECT 
--     obj_description(c.oid) as table_comment
-- FROM pg_class c
-- JOIN pg_namespace n ON n.oid = c.relnamespace
-- WHERE c.relname = 'SqlDBObject'
-- AND n.nspname = 'public';

-- show comment on COLUMN
-- 
-- SELECT 
--     col_description(c.oid, a.attnum) as column_comment
-- FROM pg_class c
-- JOIN pg_namespace n ON n.oid = c.relnamespace
-- JOIN pg_attribute a ON a.attrelid = c.oid
-- WHERE c.relname = 'SqlDBObject'
-- AND n.nspname = 'public'
-- AND a.attname = 'Name'
-- AND a.attnum > 0
-- AND NOT a.attisdropped;


-- Insert sample data (optional)
INSERT INTO "SqlDBObject" ("Name", "CreatedAt") VALUES 
('Sample Object 1', CURRENT_TIMESTAMP),
('Sample Object 2', CURRENT_TIMESTAMP),
('Test Object', CURRENT_TIMESTAMP);

-- Verify table creation
\d "SqlDBObject";

-- Show sample data
SELECT * FROM "SqlDBObject";
