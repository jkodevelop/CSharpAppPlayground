
-- This SQL script creates an enumerated type called 'status' with three possible values: 'Todo', 'Pending', and 'Done'.
-- note postgres enum values are case-sensitive and stored as strings internally
CREATE TYPE "ActionStatus" AS ENUM ('Todo', 'Pending', 'Done');

DROP TABLE IF EXISTS ExampleEnum CASCADE;

CREATE TABLE "ExampleEnum" (
    "id" SERIAL PRIMARY KEY,
    "title" VARCHAR(255) NOT NULL,
    "currentstatus" "ActionStatus" NOT NULL DEFAULT 'Todo'
);
