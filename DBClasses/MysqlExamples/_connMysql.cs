using MySql.Data.MySqlClient;
using Npgsql;
using System.Configuration;
using System.Diagnostics;

/// <summary>
/// Required library for this MySQL connection example
/// 
/// MySql.Data.MysqlClient - NuGet package
/// 
/// </summary>
namespace CSharpAppPlayground.DBClasses.MysqlExamples
{
    public class _connMysql
    {
        private MySqlConnection conn;
        private bool connected = false;
        private string connectionStr = string.Empty;

        public _connMysql()
        {
            // Get connection string from App.config
            connectionStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            conn = new MySqlConnection(connectionStr);

            /*
            using (MySqlConnection connection = new MySqlConnection(connectionStr))
            {
                try
                {
                    connection.Open();
                    Debug.WriteLine("MySQL Connection State: " + connection.State);
                }
                catch (MySqlException ex)
                {
                    Debug.WriteLine("MySQL Connection Error: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                    Debug.WriteLine("MySQL Connection State after close: " + connection.State);
                }
            }
            */
        }

        public MySqlConnection getConn()
        {
            if (!connected)
                connect();
            
            return conn;
        }

        public bool connect()
        {
            try
            {
                conn.Open();
                connected = true;
            }
            catch (MySqlException ex)
            {
                Debug.Print($"mysql.Connect() ex: {ex.Message}");
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
            catch (MySqlException ex)
            {
                Debug.Print($"mysql closeConnection() ex: {ex.Message}");
            }
            connected = false;
        }

        // this also helps confirm mysql connection works
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
            catch (MySqlException ex)
            {
                Debug.Print($"mysql closeConnection() ex: {ex.Message}");
            }
            */

            using (MySqlConnection c = new MySqlConnection(connectionStr))
            {
                try
                {
                    c.Open();
                    serverVersion = c.ServerVersion;
                    // Debug.Print($"Mysql Version: {serverVersion}");
                }
                catch (Exception ex)
                {
                    Debug.Print($"Error retrieving Mysql version: {ex.Message}");
                }
            }
            return $"Mysql server version: {serverVersion}";
        }

    }
}
