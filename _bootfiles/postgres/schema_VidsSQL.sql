-- SQL Schema Creation Script for VidsSQL Object
-- This script creates the Vids table based on the VidsSQL C# class properties
-- PostgreSQL version with case-sensitive identifiers

-- Drop table if exists (for clean setup)
DROP TABLE IF EXISTS "Vids";

-- Create Vids table
CREATE TABLE "Vids" (
    "id" SERIAL PRIMARY KEY,                                    -- Primary key, auto-increment
    "filename" VARCHAR(500) NOT NULL,                          -- Video filename (required)
    "filesizebyte" BIGINT NULL,                                -- File size in bytes (nullable)
    "duration" INTEGER NULL,                                   -- Duration in seconds (nullable)
    "metadatetime" TIMESTAMP NULL,                             -- Metadata datetime (nullable)
    "width" INTEGER NULL,                                      -- Video width in pixels (nullable)
    "height" INTEGER NULL                                     -- Video height in pixels (nullable)
);

-- Create indexes for common queries
CREATE INDEX idx_filename ON "Vids" ("filename");
CREATE INDEX idx_filesize ON "Vids" ("filesizebyte");
CREATE INDEX idx_duration ON "Vids" ("duration");
CREATE INDEX idx_metadatetime ON "Vids" ("metadatetime");
CREATE INDEX idx_dimensions ON "Vids" ("width", "height");

-- better full text search, enable pg_trgm
-- check what is enabled
----- SELECT name, installed_version FROM pg_available_extensions;
----- SELECT name, installed_version FROM pg_available_extensions WHERE name = 'pg_trgm';

-- enable pg_trgm extension
CREATE EXTENSION IF NOT EXISTS pg_trgm;

-- case sensitive
CREATE INDEX idx_filename_trgm ON "Vids" USING GIN (filename gin_trgm_ops);
-- SELECT * FROM vids WHERE filename LIKE '%Example%';

-- case insensitive
CREATE INDEX idx_filename_trgm_nocase ON "Vids" USING GIN (lower(filename) gin_trgm_ops);   
-- SELECT * FROM vids WHERE lower(filename) LIKE '%example%';
-- or with ILIKE (though LIKE with lower() is more explicit)

-- other GIN indexes, might not be as useful as trgm*
CREATE INDEX fulltext_filename_gin ON "Vids" USING GIN (to_tsvector('simple', filename));


-- Add table comment
COMMENT ON TABLE "Vids" IS 'Video metadata storage table';
 
-- Add column comments for documentation
COMMENT ON COLUMN "Vids"."id" IS 'Primary key identifier';
COMMENT ON COLUMN "Vids"."filename" IS 'Video filename';
COMMENT ON COLUMN "Vids"."filesizebyte" IS 'File size in bytes';
COMMENT ON COLUMN "Vids"."duration" IS 'Video duration in seconds';
COMMENT ON COLUMN "Vids"."metadatetime" IS 'Metadata creation/modification datetime';
COMMENT ON COLUMN "Vids"."width" IS 'Video width in pixels';
COMMENT ON COLUMN "Vids"."height" IS 'Video height in pixels';


-- Insert sample data
INSERT INTO "Vids" ("filename","filesizebyte","duration","metadatetime","width","height") VALUES 
('top amazing list blender',1000,5000,'2008-10-09 00:00:00',480,320),
('top values blender',1000,5000,'2008-11-09 00:00:00',480,320),
('top parameters blender',1000,5000,'2009-10-09 00:00:00',480,320),
('list of shirts',1000,5000,'2012-10-09 00:00:00',480,320);