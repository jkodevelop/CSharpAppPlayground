using MySql.Data.MySqlClient;
using System.Configuration;

namespace CSharpAppPlayground.DBClasses.MariaDBExamples
{
    public class MariaDBBase
    {
        private string connectionStr = string.Empty;

        public MariaDBBase()
        {
            connectionStr = ConfigurationManager.ConnectionStrings["MariaDBConnection"].ConnectionString;
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
            catch (MySqlException sqlEx)
            {
                throw new Exception($"MariaDB error {sqlEx.Number}: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"MariaDB operation failed: {ex.Message}", ex);
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
                throw new Exception($"MariaDB error {sqlEx.Number}: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"MariaDB operation failed: {ex.Message}", ex);
            }
        }

        public T WithSqlCommand<T>(Func<MySqlCommand, T> func, string query)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionStr))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        connection.Open();
                        return func(command);
                    }
                }
            }
            catch (MySqlException sqlEx)
            {
                throw new Exception($"MariaDB error {sqlEx.Number}: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"MariaDB operation failed: {ex.Message}", ex);
            }
        }

        public async Task<T> WithSqlCommandAsync<T>(Func<MySqlCommand, Task<T>> func, string query)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionStr))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        await connection.OpenAsync();
                        return await func(command);
                    }
                }
            }
            catch (MySqlException sqlEx)
            {
                throw new Exception($"MariaDB error {sqlEx.Number}: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"MariaDB operation failed: {ex.Message}", ex);
            }
        }
    }
}
