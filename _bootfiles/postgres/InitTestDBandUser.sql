-- to run this in postgres cmd
-- \i 'PATH with \\ or / and single quotes, double quotes an \ slash will not work'

CREATE DATABASE testdb;

CREATE USER testuser WITH ENCRYPTED PASSWORD 'testpassword';

-- remove PUBLIC access to testdb then enforce explicit grants, superuser and dbowner still have access
REVOKE CONNECT ON DATABASE testdb FROM PUBLIC;

-- 1. Grant connection and temporary table creation
GRANT CONNECT, TEMPORARY ON DATABASE testdb TO testuser;

-- 2. Connect to testdb and run the following in that database:
GRANT USAGE, CREATE ON SCHEMA public TO testuser;

GRANT ALL ON SCHEMA public TO testuser;

GRANT CREATE ON DATABASE testdb TO testuser;

-- 3. Grant privileges on existing objects
GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO testuser;
GRANT ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA public TO testuser;
GRANT ALL PRIVILEGES ON ALL FUNCTIONS IN SCHEMA public TO testuser;

-- 4. Grant default privileges for future objects
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT ALL PRIVILEGES ON TABLES TO testuser;
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT ALL PRIVILEGES ON SEQUENCES TO testuser;
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT ALL PRIVILEGES ON FUNCTIONS TO testuser;

-- check permissions, TODO add more

-- Can this user create in the public schema?
SELECT has_schema_privilege(current_user, 'public', 'CREATE') as can_create_in_public;
-- if (f)false then do this
-- 1. login as root user, with admin permissions
-- 2. switch to testdb -> \c testdb
-- 3. run: GRANT ALL ON SCHEMA public TO testuser;
