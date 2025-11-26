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

        public void RunWordsSearchTest(string[] words)
        {
            Debug.Print("\n/////////////////////////////////////////////////////////" +
                "\nPOSTGRES words search" +
                "\n/////////////////////////////////////////////////////////");
            Test_ContainWordsSearch(words);
            Test_FullTextSearch(words);
        }

        [Time("Postgres.ContainWordsSearch")]
        private void Test_ContainWordsSearch(string[] words)
        {
            Debug.Print("\n--- Method 1: Contain Words Search ---");
            var v = ContainWordsSearch(words);
            Debug.Print($"Found {v.Count} ContainWordsSearch for words:{string.Join(',', words)}");
        }

        [Time("Postgres.FullTextSearch")]
        private void Test_FullTextSearch(string[] words)
        {
            Debug.Print("\n--- Method 2: Full Text Search ---");
            var v = FullTextSearch(words);
            Debug.Print($"Found {v.Count} FullTextSearch for words:{string.Join(',', words)}");
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

        [Time("Postgres.GetFilenameByILike")]
        public void Test_GetFilenameByILike(string searchTerm)
        {
            Debug.Print("\n--- Method 1: Search Using ILIKE ---");
            var v = GetFilenameByILike(searchTerm);
            Debug.Print($"Found {v.Count} LIKE searchTerm:{searchTerm}");
        }

        [Time("Postgres.GetFilenameByLikeCaseSensitive")]
        public void Test_GetFilenameByLikeCaseSensitive(string searchTerm)
        {
            Debug.Print("\n--- Method 2: Search Using LIKE + case sensitive ---");
            var v = GetFilenameByLikeCaseSensitive(searchTerm);
            Debug.Print($"Found {v.Count} LIKE + case searchTerm:{searchTerm}");
        }

        [Time("Postgres.GetFilename")]
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
                Debug.Print($"Postgres.GetFilename({searchTerm}): {ex.Message}");
            }
            return objects;
        }

        public List<VidsSQL> ContainWordsSearch(string[] words)
        {
            var objects = new List<VidsSQL>();
            try
            {
                /* -- case insensitive example
                SELECT * FROM table_name WHERE 
                column_name ILIKE '% word1 %' 
                AND column_name ILIKE '% word2 %';  
                */
                string query = "SELECT * FROM \"Vids\" WHERE ";
                for (int i = 0; i < words.Length; i++)
                {
                    if (i > 0)
                        query += " AND ";
                    query += $"filename ILIKE @word{i} "; // case insensitive
                }

                psqlBase.WithSqlCommand<object>(command =>
                {
                    for (int i = 0; i < words.Length; i++)
                    {
                        command.Parameters.AddWithValue($"@word{i}", $"%{words[i]}%");
                    }
                    using (var reader = command.ExecuteReader())
                    {
                        var vidSQL = new VidsSQL();
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
                Debug.Print($"[postgres] ContainWordsSearch: Error retrieving Vids by words: {ex.Message}");             
            }
            return objects;
        }

        public List<VidsSQL> FullTextSearch(string[] words)
        {
            //--create GIN fulltext index:
            // CREATE INDEX <index_name> ON <table_name> USING GIN (to_tsvector('simple', <column_name>));
            // CREATE INDEX fulltext_filename_gin ON "Vids" USING GIN (to_tsvector('simple', filename));
            var objects = new List<VidsSQL>();
            try
            {
                /*
                POSTGRES only ------------------------
                SELECT * FROM table_name
                WHERE to_tsvector(column_name) @@ to_tsquery('word1 & word2');
                */
                if (words == null || words.Length == 0)
                    return objects;

                // create tsquery string for Postgres text search
                // For example, "word1 & word2"
                string tsquery = string.Join(" & ", words
                    .Where(w => !string.IsNullOrWhiteSpace(w))
                    .Select(w => w.Replace("'", "''"))); // escape single quotes for SQL
                Debug.Print($"Postgres.FullTextSearch: tsquery: {tsquery}");

                string query = "SELECT * FROM \"Vids\" WHERE to_tsvector(\"filename\") @@ to_tsquery(@tsquery);";
                psqlBase.WithSqlCommand<object>(command =>
                {
                    command.Parameters.AddWithValue("@tsquery", tsquery);
                    using (var reader = command.ExecuteReader())
                    {
                        var vidSQL = new VidsSQL();
                        while (reader.Read())
                        {
                            objects.Add(vidSQL.MapReaderToObject(reader));
                        }
                    }
                    return true;
                }, query);

            }
            catch (Exception ex)
            {
                Debug.Print($"[postgres] FullTextSearch: Error retrieving Vids by words: {ex.Message}");
            }
            return objects;
        }
    }
}
