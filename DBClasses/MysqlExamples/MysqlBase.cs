using MySql.Data.MySqlClient;
using System.Configuration;

namespace CSharpAppPlayground.DBClasses.MysqlExamples
{
    public class MysqlBase
    {
        // Add these helper methods to your class
        private string connectionStr = string.Empty;

        public MysqlBase()
        {
            connectionStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        }

        public T WithConnection<T>(Func<MySqlConnection, T> func)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionStr))
                {
                    connection.Open();
                    return func(connection);
                }
            }
            catch(MySqlException sqlEx)
            {
                throw new Exception($"MySQL error {sqlEx.Number}: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Database operation failed: {ex.Message}", ex);
            }
        }

        public async Task<T> WithConnectionAsync<T>(Func<MySqlConnection, Task<T>> func)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionStr))
                {
                    await connection.OpenAsync();
                    return await func(connection);
                }
            }
            catch (MySqlException sqlEx)
            {
                throw new Exception($"MySQL error {sqlEx.Number}: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Database operation failed: {ex.Message}", ex);
            }
        }
    }
}
