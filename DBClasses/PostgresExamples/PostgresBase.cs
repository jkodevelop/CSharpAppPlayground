using Npgsql;
using System.Configuration;

namespace CSharpAppPlayground.DBClasses.PostgresExamples
{
    public class PostgresBase
    {
        private string connectionStr = string.Empty;

        public PostgresBase()
        {
            connectionStr = ConfigurationManager.ConnectionStrings["PostgreSqlConnection"].ConnectionString;

            // This is replaced, with GlobalConfiguration.Setup().UseMySql().UsePostgreSql(); // to support both DBs
            // GlobalConfiguration.Setup().UsePostgreSql(); // configuring "RepoDb", add more supported API to postgres
        }

        public T WithConnection<T>(Func<NpgsqlConnection, T> func)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionStr))
                {
                    connection.Open();
                    return func(connection);
                }
            }
            catch (NpgsqlException sqlEx)
            {
                throw new Exception($"Postgres error {sqlEx.ErrorCode}: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Postgres operation failed: {ex.Message}", ex);
            }
        }

        public async Task<T> WithConnectionAsync<T>(Func<NpgsqlConnection, Task<T>> func)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionStr))
                {
                    await connection.OpenAsync();
                    return await func(connection);
                }
            }
            catch (NpgsqlException sqlEx)
            {
                throw new Exception($"Postgres error {sqlEx.ErrorCode}: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Postgres operation failed: {ex.Message}", ex);
            }
        }

        public T WithSqlCommand<T>(Func<NpgsqlCommand, T> func, string query)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionStr))
                {
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        connection.Open();
                        return func(command);
                    }
                }
            }
            catch (NpgsqlException sqlEx)
            {
                throw new Exception($"Postgres error {sqlEx.ErrorCode}: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Postgres operation failed: {ex.Message}", ex);
            }
        }

        public async Task<T> WithSqlCommandAsync<T>(Func<NpgsqlCommand, Task<T>> func, string query)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionStr))
                {
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        await connection.OpenAsync();
                        return await func(command);
                    }
                }
            }
            catch (NpgsqlException sqlEx)
            {
                throw new Exception($"Postgres error {sqlEx.ErrorCode}: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Postgres operation failed: {ex.Message}", ex);
            }
        }
    }
}
