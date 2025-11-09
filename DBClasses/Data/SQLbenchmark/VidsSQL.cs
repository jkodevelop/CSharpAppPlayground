using System.Data.Common;

namespace CSharpAppPlayground.DBClasses.Data.SQLbenchmark
{
    public class VidsSQL : Vids
    {
        public static string table = "Vids";

        public int id { get; set; }
        
        public override string ToString()
        {
            return $"SQL Table: {table}";
        }

        public VidsSQL MapReaderToObject(DbDataReader reader) // MysqlDataReader | NpgsqlDataReader
        {
            return new VidsSQL
            {
                id = reader["id"] != DBNull.Value ? reader.GetOrdinal("id") : 0,
                filename = reader["filename"] != DBNull.Value ? reader.GetString(reader.GetOrdinal("filename")) : null,
                filesizebyte = reader["filesizebyte"] != DBNull.Value ? (System.Numerics.BigInteger)reader.GetInt64(reader.GetOrdinal("filesizebyte")) : (System.Numerics.BigInteger?)null,
                duration = reader["duration"] != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("duration")) : (int?)null,
                metadatetime = reader["metadatetime"] != DBNull.Value ? reader.GetDateTime(reader.GetOrdinal("metadatetime")) : (DateTime?)null,
                width = reader["width"] != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("width")) : (int?)null,
                height = reader["height"] != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("height")) : (int?)null
            };
        }
    }
}
