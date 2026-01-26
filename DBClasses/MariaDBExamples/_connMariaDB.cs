using MySql.Data.MySqlClient;
using System.Configuration;
using System.Diagnostics;

namespace CSharpAppPlayground.DBClasses.MariaDBExamples
{
    public class _connMariaDB
    {
        private MySqlConnection conn;
        private bool connected = false;
        private string connectionStr = string.Empty;

        public _connMariaDB()
        {
            // Get connection string from App.config
            connectionStr = ConfigurationManager.ConnectionStrings["MariaDBConnection"].ConnectionString;
            conn = new MySqlConnection(connectionStr);
        }

        public string getServerVersion()
        {
            string serverVersion = "N/A";
            try
            {
                using (MySqlConnection c = new MySqlConnection(connectionStr))
                {
                    c.Open();
                    serverVersion = c.ServerVersion;
                    // Debug.Print($"Mysql Version: {serverVersion}");   
                }
            }
            catch (MySqlException myex)
            {
                Debug.Print($"Error with MariaDB connectionm, MariaDBException: {myex.Message}");
            }
            catch (Exception ex)
            {
                Debug.Print($"Error with MariaDB connection, Exception: {ex.Message}");
            }
            return $"MariaDB server version: {serverVersion}";
        }
    }
}
