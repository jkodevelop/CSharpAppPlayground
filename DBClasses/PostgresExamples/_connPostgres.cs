using System.Configuration;
using System.Diagnostics;
using Npgsql;

/// <summary>
/// Required library for this Postgres connection example
/// 
/// Npgsql
/// 
/// </summary>
namespace CSharpAppPlayground.DBClasses.PostgresExamples
{
    public class _connPostgres
    {
        public _connPostgres()
        {
            connect();
        }

        public void connect()
        {
            // Connection string to connect to PostgreSQL
            string connectionStr = ConfigurationManager.ConnectionStrings["PostgreSqlConnection"].ConnectionString;

            // Create a new connection object
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionStr))
            {
                try
                {
                    conn.Open();

                    Debug.Print($"Successfully connected to PostgreSQL.");
                    Debug.Print($"PostgreSQL Version: {conn.PostgreSqlVersion}");
                }
                catch (Exception ex)
                {
                    Debug.Print($"Error connecting to PostgreSQL: {ex.Message}");
                }
            }
        }
    }
}
