using CSharpAppPlayground.DBClasses.Data.SQLbenchmark;
using CSharpAppPlayground.DBClasses.PostgresExamples;
using MethodTimer;
using System.Diagnostics;

namespace CSharpAppPlayground.DBClasses.PostgresBenchmark
{
    public class PostgresSearchBenchmarks
    {
        private PostgresBase psqlBase;

        public PostgresSearchBenchmarks()
        {
            psqlBase = new PostgresBase();
        }

        public void RunSimpleSearchTest(string searchTerm)
        {
            Debug.Print("\n/////////////////////////////////////////////////////////" +
                "\nPOSTGRES simple search" +
                "\n/////////////////////////////////////////////////////////");
            Test_GetFilenameByILike(searchTerm);
            Test_GetFilenameByLikeCaseSensitive(searchTerm);
            Test_GetFilename(searchTerm);
        }

        [Time("GetFilenameByILike")]
        public void Test_GetFilenameByILike(string searchTerm)
        {
            Debug.Print("\n--- Method 1: Search Using ILIKE ---");
            var v = GetFilenameByILike(searchTerm);
            Debug.Print($"Found {v.Count} LIKE searchTerm:{searchTerm}");
        }

        [Time("GetFilenameByLikeCaseSensitive")]
        public void Test_GetFilenameByLikeCaseSensitive(string searchTerm)
        {
            Debug.Print("\n--- Method 2: Search Using LIKE + case sensitive ---");
            var v = GetFilenameByLikeCaseSensitive(searchTerm);
            Debug.Print($"Found {v.Count} LIKE + case searchTerm:{searchTerm}");
        }

        [Time("GetFilename")]
        public void Test_GetFilename(string searchTerm)
        {
            Debug.Print("\n--- Method 3: Search Exact ---");
            var v = GetFilename(searchTerm);
            Debug.Print($"Found {v.Count} EXACT searchTerm:{searchTerm}");
        }

        public List<VidsSQL> GetFilenameByILike(string searchTerm)
        {
            var objects = new List<VidsSQL>();
            var vidSQL = new VidsSQL();
            try
            {
                string query = "SELECT * FROM \"Vids\" WHERE filename ILIKE @filename;";
                psqlBase.WithSqlCommand<object>(command =>
                {
                    command.Parameters.AddWithValue("@filename", $"%{searchTerm}%");
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            while (reader.Read())
                            {
                                objects.Add(vidSQL.MapReaderToObject(reader));
                            }
                        }
                    }
                    return true; // doesn't matter cause objects is populated by reference
                }, query);
            }
            catch (Exception ex)
            {
                Debug.Print($"Postgres.GetFilenameByLike({searchTerm}): {ex.Message}");
            }
            return objects;
        }

        public List<VidsSQL> GetFilenameByLikeCaseSensitive(string searchTerm)
        {
            var objects = new List<VidsSQL>();
            var vidSQL = new VidsSQL();
            try
            {
                string query = "SELECT * FROM \"Vids\" WHERE filename LIKE @filename COLLATE \"C\";";
                // "SELECT * FROM \"Vids\" WHERE filename LIKE @filename;";
                psqlBase.WithSqlCommand<object>(command =>
                {
                    command.Parameters.AddWithValue("@filename", $"%{searchTerm}%");
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            while (reader.Read())
                            {
                                objects.Add(vidSQL.MapReaderToObject(reader));
                            }
                        }
                    }
                    return true; // doesn't matter cause objects is populated by reference
                }, query);
            }
            catch (Exception ex)
            {
                Debug.Print($"Postgres.GetFilenameByLikeCaseSensitive({searchTerm}): {ex.Message}");
            }
            return objects;
        }

        public List<VidsSQL> GetFilename(string searchTerm)
        {
            var objects = new List<VidsSQL>();
            var vidSQL = new VidsSQL();
            try
            {
                string query = "SELECT * FROM \"Vids\" WHERE filename=@filename;";
                psqlBase.WithSqlCommand<object>(command =>
                {
                    command.Parameters.AddWithValue("@filename", searchTerm);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            while (reader.Read())
                            {
                                objects.Add(vidSQL.MapReaderToObject(reader));
                            }
                        }
                    }
                    return true; // doesn't matter cause objects is populated by reference
                }, query);
            }
            catch (Exception ex)
            {
                Debug.Print($"Postgres.GetFilename({searchTerm}): {ex.Message}");
            }
            return objects;
        }
    }
}
