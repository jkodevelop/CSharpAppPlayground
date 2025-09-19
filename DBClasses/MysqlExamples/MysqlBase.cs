using CSharpAppPlayground.DBClasses.Data;
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
                throw new Exception($"MySQL operation failed: {ex.Message}", ex);
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
                throw new Exception($"MySQL operation failed: {ex.Message}", ex);
            }
        }

        // USAGE EXAMPLE:
        // 
        //public int InsertSqlDBObject(SqlDBObject obj)
        //{
        //    return mysqlBase.WithConnection(connection =>
        //    {
        //        string query = "INSERT INTO SqlDBObjects (Name, CreatedAt) VALUES (@Name, @CreatedAt); SELECT LAST_INSERT_ID();";
        //        using (var command = new MySqlCommand(query, connection))
        //        {
        //            command.Parameters.AddWithValue("@Name", obj.Name);
        //            command.Parameters.AddWithValue("@CreatedAt", obj.CreatedAt);
        //            var result = command.ExecuteScalar();
        //            return Convert.ToInt32(result);
        //        }
        //    });
        //}

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
                throw new Exception($"MySQL error {sqlEx.Number}: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"MySQL operation failed: {ex.Message}", ex);
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
                throw new Exception($"MySQL error {sqlEx.Number}: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"MySQL operation failed: {ex.Message}", ex);
            }
        }
    }
}
