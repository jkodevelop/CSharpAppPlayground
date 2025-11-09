using CSharpAppPlayground.DBClasses.Data;
using CSharpAppPlayground.DBClasses.Data.SQLbenchmark;
using CSharpAppPlayground.DBClasses.PostgresExamples;
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

        public List<VidsSQL> GetFilenameByLike(string searchTerm)
        {
            var objects = new List<VidsSQL>();
            var vidSQL = new VidsSQL();
            try
            {
                string query = "SELECT * FROM \"Vids\" WHERE filename LIKE @filename";
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
    }
}
