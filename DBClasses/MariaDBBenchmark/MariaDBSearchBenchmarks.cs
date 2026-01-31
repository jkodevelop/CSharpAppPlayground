using CSharpAppPlayground.DBClasses.Data.SQLbenchmark;
using CSharpAppPlayground.DBClasses.MariaDBExamples;
using CSharpAppPlayground.DBClasses.MariaDBExamples;
using MethodTimer;
using System.Diagnostics;

namespace CSharpAppPlayground.DBClasses.MariaDBBenchmark
{
    public class MariaDBSearchBenchmarks
    {
        private MariaDBBase mariaDB;

        public MariaDBSearchBenchmarks()
        {
            mariaDB = new MariaDBBase();
        }

        public void RunWordsSearchTest(string[] words)
        {
            Debug.Print("\n/////////////////////////////////////////////////////////" +
                "\nMariaDB words search" +
                "\n/////////////////////////////////////////////////////////");
            Test_ContainWordsSearch(words);
            Test_FullTextSearch(words);
        }

        [Time("MariaDB.ContainWordsSearch")]
        private void Test_ContainWordsSearch(string[] words)
        {
            Debug.Print("\n--- Method 1: Contain Words Search ---");
            var v = ContainWordsSearch(words);
            Debug.Print($"Found {v.Count} ContainWordsSearch for words:{string.Join(',', words)}");
        }

        [Time("MariaDB.FullTextSearch")]
        private void Test_FullTextSearch(string[] words)
        {
            Debug.Print("\n--- Method 2: Full Text Search ---");
            var v = FullTextSearch(words);
            Debug.Print($"Found {v.Count} FullTextSearch for words:{string.Join(',', words)}");
        }

        public void RunSimpleSearchTest(string searchTerm)
        {
            Debug.Print("\n/////////////////////////////////////////////////////////" +
                "\nMariaDB simple search" +
                "\n/////////////////////////////////////////////////////////");
            Test_GetFilenameByLike(searchTerm);
            Test_GetFilenameByLikeCaseSensitive(searchTerm);
            Test_GetFilename(searchTerm);
        }

        [Time("MariaDB.GetFilenameByLike")]
        public void Test_GetFilenameByLike(string searchTerm)
        {
            Debug.Print("\n--- Method 1: Search Using LIKE ---");
            var v = GetFilenameByLike(searchTerm);
            Debug.Print($"Found {v.Count} LIKE searchTerm:{searchTerm}");
        }

        [Time("MariaDB.GetFilenameByLikeCaseSensitive")]
        public void Test_GetFilenameByLikeCaseSensitive(string searchTerm)
        {
            Debug.Print("\n--- Method 2: Search Using LIKE casesensitive ---");
            var v = GetFilenameByLikeCaseSensitive(searchTerm);
            Debug.Print($"Found {v.Count} LIKE + case searchTerm:{searchTerm}");
        }

        [Time("MariaDB.GetFilename")]
        public void Test_GetFilename(string searchTerm)
        {
            Debug.Print("\n--- Method 3: Search Exact ---");
            var v = GetFilename(searchTerm);
            Debug.Print($"Found {v.Count} EXACT searchTerm:{searchTerm}");
        }

        public List<VidsSQL> GetFilenameByLike(string searchTerm)
        {
            var objects = new List<VidsSQL>();
            var vidSQL = new VidsSQL();
            try
            {
                string query = "SELECT * FROM Vids WHERE filename LIKE @filename";
                mariaDB.WithSqlCommand<object>(command =>
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
                mariaDB.WithSqlCommand<object>(command =>
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
                mariaDB.WithSqlCommand<object>(command =>
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

        public List<VidsSQL> ContainWordsSearch(string[] words)
        {
            /* -- case insensitive example
                SELECT * FROM table_name WHERE 
                LOWER(column_name) LIKE '% word1 %' 
                AND LOWER(column_name) LIKE '% word2 %';  
            */
            var objects = new List<VidsSQL>();
            var vidSQL = new VidsSQL();
            try
            {
                string query = "SELECT * FROM Vids WHERE ";
                for (int i = 0; i < words.Length; i++)
                {
                    if (i > 0) { query += " AND "; }
                    var paramName = $"@word{i}";
                    query += $"LOWER(filename) LIKE {paramName}"; // case insensitive
                }

                mariaDB.WithSqlCommand<object>(command =>
                {
                    for (int i = 0; i < words.Length; i++)
                    {
                        var paramName = $"@word{i}";
                        command.Parameters.AddWithValue(paramName, $"%{words[i]}%");
                    }
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
                Debug.Print($"[MariaDB] ContainWordsSearch: Error retrieving Vids by words: {ex.Message}");
            }
            return objects;
        }

        public List<VidsSQL> FullTextSearch(string[] words)
        {
            //--create fulltext index:
            // CREATE FULLTEXT INDEX `fulltext_index_name` ON `table_name` (column_name);
            // CREATE FULLTEXT INDEX `idx_fulltext_filename` ON `testdb`.`vids` (filename) COMMENT '' ALGORITHM DEFAULT LOCK DEFAULT
            var objects = new List<VidsSQL>();
            try
            {
                /* MariaDB only ---------------------------
                SELECT * FROM table_name
                WHERE MATCH(column_name) AGAINST('+word1 +word2' IN BOOLEAN MODE);
                */
                if (words == null || words.Length == 0)
                    return new List<VidsSQL>();

                string searchString = string.Join(" ", words.Select(w => $"+{w}"));
                Debug.Print($"MariaDB.FullTextSearch: searchString: {searchString}");

                string query = "SELECT * FROM Vids WHERE MATCH(filename) AGAINST(@search IN BOOLEAN MODE);";
                mariaDB.WithSqlCommand<object>(command =>
                {
                    command.Parameters.AddWithValue("@search", searchString);
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
                Debug.Print($"[MariaDB] FullTextSearch: Error retrieving Vids by words: {ex.Message}");
            }
            return objects;
        }
    }
}
