using MySql.Data.MySqlClient;
using Npgsql;
using System.Configuration;
using System.Diagnostics;

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
        private NpgsqlConnection conn;
        private bool connected = false;
        private string connectionStr = string.Empty;

        public _connPostgres()
        {
            // Get connection string from App.config
            connectionStr = ConfigurationManager.ConnectionStrings["PostgreSqlConnection"].ConnectionString;
            conn = new NpgsqlConnection(connectionStr);
        }

        public NpgsqlConnection getConn()
        {
            if (!connected)
                connect();

            return conn;
        }

        public bool connect()
        {
            /*
            // Connection string to connect to PostgreSQL
            string connectionStr = ConfigurationManager.ConnectionStrings["PostgreSqlConnection"].ConnectionString;

            // Create a new connection object
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionStr))
            {
                try
                {
                    conn.Open();
                    connected = true;

                    Debug.Print($"Successfully connected to PostgreSQL.");
                    Debug.Print($"PostgreSQL Version: {conn.PostgreSqlVersion}");
                }
                catch (Exception ex)
                {
                    Debug.Print($"Error connecting to PostgreSQL: {ex.Message}");
                }
            }
            */

            try
            {
                conn.Open();
                connected = true;
            }
            catch (NpgsqlException ex)
            {
                Debug.Print($"postgres.Connect() ex: {ex.Message}");
                connected = false;
            }
            return connected;
        }

        public void closeConnection()
        {
            try
            {
                conn.Close();
                conn.Dispose();
            }
            catch (NpgsqlException ex)
            {
                Debug.Print($"postgres closeConnection() ex: {ex.Message}");
            }
            connected = false;
        }

        public string getServerVersion()
        {
            string serverVersion = "N/A";

            /*
            try
            {
                if (connect())
                    serverVersion = this.conn.ServerVersion;
                
                closeConnection();
            }
            catch (NpgsqlConnection ex)
            {
                Debug.Print($"postgres closeConnection() ex: {ex.Message}");
            }
            */

            try
            {
                using (NpgsqlConnection c = new NpgsqlConnection(connectionStr))
                {
                    c.Open();
                    serverVersion = c.PostgreSqlVersion.ToString();
                    // Debug.Print($"PostgreSQL Version: {conn.PostgreSqlVersion}");
                }
            }
            catch (NpgsqlException pex)
            {
                Debug.Print($"Error PostgreSQL NpgsqlException: {pex.Message}");
            }
            catch (Exception ex)
            {
                Debug.Print($"Error PostgreSQL Exception: {ex.Message}");
            }
            return $"Postgres server version: {serverVersion}";
        }
    }
}
