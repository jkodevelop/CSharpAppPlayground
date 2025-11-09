using CSharpAppPlayground.DBClasses.Data.SQLbenchmark;
using CSharpAppPlayground.DBClasses.MysqlExamples;
using MethodTimer;
using MySql.Data.MySqlClient;
using System.Diagnostics;

// [TO DOCUMENT] LIKE BINARY
// only mysql have `BINARY` keyword, postgres does not have this, but postgres supports COLLATE
/*

SELECT * FROM users 
WHERE name LIKE '%John%' COLLATE utf8mb4_bin;

SELECT * FROM users 
WHERE name COLLATE utf8mb4_bin LIKE '%John%';

(slow, indexes affected, because COLLATE applied directly to a column)
WHERE BINARY name = 'John'
-- or
WHERE name COLLATE utf8mb4_bin = 'John'

(best practice, keeps usage of indexes)
WHERE name = BINARY 'John'
-- or
WHERE name = 'John' COLLATE utf8mb4_bin
*/

namespace CSharpAppPlayground.DBClasses.MysqlBenchmark
{
    public class MysqlSearchBenchmarks
    {
        private MysqlBase mysqlBase;

        public MysqlSearchBenchmarks()
        {
            mysqlBase = new MysqlBase();
        }

        public void RunTest(string searchTerm)
        {
            Test_GetFilenameByLike(searchTerm);
        }

        [Time("GetFilenameByLike")]
        public void Test_GetFilenameByLike(string searchTerm)
        {
            Debug.Print("\n--- Method 1: Search Using LIKE ---");
            GetFilenameByLike(searchTerm);
        }

        [Time("GetFilename")]
        public void Test_GetFilename(string searchTerm)
        {
            Debug.Print("\n--- Method 2: Search Exact ---");
            GetFilename(searchTerm);
        }

        public List<VidsSQL> GetFilenameByLike(string searchTerm)
        {
            var objects = new List<VidsSQL>();
            var vidSQL = new VidsSQL();
            try
            {
                string query = "SELECT * FROM Vids WHERE filename LIKE @filename";
                mysqlBase.WithSqlCommand<object>(command =>
                {
                    command.Parameters.AddWithValue("@filename", $"%{searchTerm}%");
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            objects.Add(vidSQL.MapReaderToObject(reader));
                        }
                    }
                    return true; // doesn't matter cause objects is populated by reference
                }, query);
            }
            catch (Exception ex)
            {
                Debug.Print($"GetFilenameByLike({searchTerm}): Error retrieving Vids by filename: {ex.Message}");
            }
            return objects;
        }

        // NOTE: limitations for `LIKE BINARY` the searchTerm encoding has to be the same latin1 != UTF8 mapping
        public List<VidsSQL> GetFilenameByLikeCaseSensitive(string searchTerm)
        {
            var objects = new List<VidsSQL>();
            var vidSQL = new VidsSQL();
            try
            {
                string query = "SELECT * FROM Vids WHERE filename LIKE BINARY @filename";
                mysqlBase.WithSqlCommand<object>(command =>
                {
                    command.Parameters.AddWithValue("@filename", $"%{searchTerm}%");
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            objects.Add(vidSQL.MapReaderToObject(reader));
                        }
                    }
                    return true; // doesn't matter cause objects is populated by reference
                }, query);
            }
            catch (Exception ex)
            {
                Debug.Print($"GetFilenameByLikeCaseSensitive({searchTerm}): Error retrieving Vids by filename: {ex.Message}");
            }
            return objects;
        }

        public List<VidsSQL> GetFilename(string searchTerm)
        {
            var objects = new List<VidsSQL>();
            var vidSQL = new VidsSQL();
            try
            {
                string query = "SELECT * FROM Vids WHERE filename=@filename";
                mysqlBase.WithSqlCommand<object>(command =>
                {
                    command.Parameters.AddWithValue("@filename", searchTerm);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            objects.Add(vidSQL.MapReaderToObject(reader));
                        }
                    }
                    return true; // doesn't matter cause objects is populated by reference
                }, query);
            }
            catch (Exception ex)
            {
                Debug.Print($"GetFilename({searchTerm}): Error retrieving Vids by filename: {ex.Message}");
            }
            return objects;
        }
    }
}