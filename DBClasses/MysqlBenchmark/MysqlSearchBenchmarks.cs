using CSharpAppPlayground.DBClasses.Data.SQLbenchmark;
using CSharpAppPlayground.DBClasses.MysqlExamples;
using MethodTimer;
using System.Diagnostics;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

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

        public void RunWordsSearchTest(string[] words)
        {
            Debug.Print("\n/////////////////////////////////////////////////////////" +
                "\nMYSQL words search" +
                "\n/////////////////////////////////////////////////////////");
            Test_ContainWordsSearch(words);
            Test_FullTextSearch(words);
        }

        [Time("ContainWordsSearch")]
        private void Test_ContainWordsSearch(string[] words)
        {
            Debug.Print("\n--- Method 1: Contain Words Search ---");
            var v = ContainWordsSearch(words);
            Debug.Print($"Found {v.Count} ContainWordsSearch for words:{String.Join(',', words)}");
        }

        [Time("FullTextSearch")]
        private void Test_FullTextSearch(string[] words)
        {
            Debug.Print("\n--- Method 2: Full Text Search ---");
            var v = FullTextSearch(words);
            Debug.Print($"Found {v.Count} FullTextSearch for words:{String.Join(',', words)}");
        }   

        public void RunSimpleSearchTest(string searchTerm)
        {
            Debug.Print("\n/////////////////////////////////////////////////////////" +
                "\nMYSQL simple search" +
                "\n/////////////////////////////////////////////////////////");
            Test_GetFilenameByLike(searchTerm);
            Test_GetFilenameByLikeCaseSensitive(searchTerm);
            Test_GetFilename(searchTerm);
        }

        [Time("GetFilenameByLike")]
        public void Test_GetFilenameByLike(string searchTerm)
        {
            Debug.Print("\n--- Method 1: Search Using LIKE ---");
            var v = GetFilenameByLike(searchTerm);
            Debug.Print($"Found {v.Count} LIKE searchTerm:{searchTerm}");
        }

        [Time("GetFilenameByLikeCaseSensitive")]
        public void Test_GetFilenameByLikeCaseSensitive(string searchTerm)
        {
            Debug.Print("\n--- Method 2: Search Using LIKE casesensitive ---");
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

        public List<VidsSQL> ContainWordsSearch(string[] words)
        {
            var objects = new List<VidsSQL>();
            try
            {
                /* -- case insensitive example
                SELECT * FROM table_name WHERE 
                LOWER(column_name) LIKE '% word1 %' 
                AND LOWER(column_name) LIKE '% word2 %';  
                */
                string query = "SELECT * FROM Vids WHERE ";
                mysqlBase.WithSqlCommand<object>(command =>
                {
                    for (int i = 0; i < words.Length; i++)
                    {
                        if (i > 0)
                            query += " AND ";
                        query += $"LOWWER(filename) LIKE @word{i} ";
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
                Debug.Print($"[Mysql] ContainWordsSearch: Error retrieving Vids by words: {ex.Message}");
            }
            return objects;
        }

        public List<VidsSQL> FullTextSearch(string[] words)
        {
            //--create fulltext index:
            //CREATE FULLTEXT INDEX ON table_name(column_name) KEY INDEX index_name;
            try
            {
                /* MYSQL only ---------------------------
                SELECT * FROM table_name
                WHERE MATCH(column_name) AGAINST('+word1 +word2' IN BOOLEAN MODE);
                */
            }
            catch (Exception ex)
            {
                Debug.Print($"[Mysql] FullTextSearch: Error retrieving Vids by words: {ex.Message}");
            }
            return null;
        }
    }
}

/*
 * 
 * -- Other SQL DB full text search examples, these are not part of MYSQL or POSTGRES

SELECT * FROM table_name
WHERE CONTAINS(column_name, 'top AND blender');

SELECT * FROM table_name
WHERE FREETEXT(column_name, 'top blender');  
 
*/