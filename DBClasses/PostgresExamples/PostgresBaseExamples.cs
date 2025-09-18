using Npgsql;
using CSharpAppPlayground.DBClasses.Data;
using System.Configuration;
using System.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSharpAppPlayground.DBClasses.PostgresExamples
{
	public class PostgresBaseExamples
	{
		private string connectionStr = string.Empty;

		private SqlDBObject? dbObject;

		public PostgresBaseExamples()
		{
			connectionStr = ConfigurationManager.ConnectionStrings["PostgreSqlConnection"].ConnectionString;
		}

		#region INSERT Operations

		public int InsertSqlDBObject(SqlDBObject obj)
		{
			try
			{
				using (var connection = new NpgsqlConnection(connectionStr))
				{
					connection.Open();
					string query = "INSERT INTO \"SqlDBObjects\" (\"Name\", \"CreatedAt\") VALUES (@Name, @CreatedAt) RETURNING \"Id\";";

					using (var command = new NpgsqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@Name", obj.Name);
						command.Parameters.AddWithValue("@CreatedAt", obj.CreatedAt);

						var result = command.ExecuteScalar();
						return Convert.ToInt32(result);
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"Error inserting SqlDBObject: {ex.Message}", ex);
			}
		}

		public async Task<int> InsertSqlDBObjectAsync(SqlDBObject obj)
		{
			try
			{
				using (var connection = new NpgsqlConnection(connectionStr))
				{
					await connection.OpenAsync();
					string query = "INSERT INTO \"SqlDBObjects\" (\"Name\", \"CreatedAt\") VALUES (@Name, @CreatedAt) RETURNING \"Id\";";

					using (var command = new NpgsqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@Name", obj.Name);
						command.Parameters.AddWithValue("@CreatedAt", obj.CreatedAt);

						var result = await command.ExecuteScalarAsync();
						return Convert.ToInt32(result);
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"Error inserting SqlDBObject: {ex.Message}", ex);
			}
		}

		#endregion

		#region SELECT Operations

		public SqlDBObject? GetById(int id)
		{
			try
			{
				using (var connection = new NpgsqlConnection(connectionStr))
				{
					connection.Open();
					string query = "SELECT \"Id\", \"Name\", \"CreatedAt\" FROM \"SqlDBObjects\" WHERE \"Id\" = @Id";

					using (var command = new NpgsqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@Id", id);

						using (var reader = command.ExecuteReader())
						{
							if (reader.Read())
							{
								return new SqlDBObject
								{
									Id = reader.GetInt32(reader.GetOrdinal("Id")),
									Name = reader.GetString(reader.GetOrdinal("Name")),
									CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
								};
							}
						}
					}
				}
				return null;
			}
			catch (Exception ex)
			{
				throw new Exception($"Error retrieving SqlDBObject by ID: {ex.Message}", ex);
			}
		}

		public List<SqlDBObject> GetAll()
		{
			var objects = new List<SqlDBObject>();

			try
			{
				using (var connection = new NpgsqlConnection(connectionStr))
				{
					connection.Open();
					string query = "SELECT \"Id\", \"Name\", \"CreatedAt\" FROM \"SqlDBObjects\" ORDER BY \"CreatedAt\" DESC";

					using (var command = new NpgsqlCommand(query, connection))
					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							objects.Add(new SqlDBObject
							{
								Id = reader.GetInt32(reader.GetOrdinal("Id")),
								Name = reader.GetString(reader.GetOrdinal("Name")),
								CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
							});
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"Error retrieving all SqlDBObjects: {ex.Message}", ex);
			}

			return objects;
		}

		public List<SqlDBObject> GetByName(string name)
		{
			var objects = new List<SqlDBObject>();

			try
			{
				using (var connection = new NpgsqlConnection(connectionStr))
				{
					connection.Open();
					string query = "SELECT \"Id\", \"Name\", \"CreatedAt\" FROM \"SqlDBObjects\" WHERE \"Name\" ILIKE @Name ORDER BY \"CreatedAt\" DESC";

					using (var command = new NpgsqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@Name", $"%{name}%");

						using (var reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								objects.Add(new SqlDBObject
								{
									Id = reader.GetInt32(reader.GetOrdinal("Id")),
									Name = reader.GetString(reader.GetOrdinal("Name")),
									CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
								});
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"Error retrieving SqlDBObjects by name: {ex.Message}", ex);
			}

			return objects;
		}

		public async Task<List<SqlDBObject>> GetAllAsync()
		{
			var objects = new List<SqlDBObject>();

			try
			{
				using (var connection = new NpgsqlConnection(connectionStr))
				{
					await connection.OpenAsync();
					string query = "SELECT \"Id\", \"Name\", \"CreatedAt\" FROM \"SqlDBObjects\" ORDER BY \"CreatedAt\" DESC";

					using (var command = new NpgsqlCommand(query, connection))
					using (var reader = await command.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							objects.Add(new SqlDBObject
							{
								Id = reader.GetInt32(reader.GetOrdinal("Id")),
								Name = reader.GetString(reader.GetOrdinal("Name")),
								CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
							});
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"Error retrieving all SqlDBObjects: {ex.Message}", ex);
			}

			return objects;
		}

		#endregion

		#region UPDATE Operations

		public bool UpdateSqlDBObject(SqlDBObject obj)
		{
			try
			{
				using (var connection = new NpgsqlConnection(connectionStr))
				{
					connection.Open();
					string query = "UPDATE \"SqlDBObjects\" SET \"Name\" = @Name, \"CreatedAt\" = @CreatedAt WHERE \"Id\" = @Id";

					using (var command = new NpgsqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@Id", obj.Id);
						command.Parameters.AddWithValue("@Name", obj.Name);
						command.Parameters.AddWithValue("@CreatedAt", obj.CreatedAt);

						int rowsAffected = command.ExecuteNonQuery();
						return rowsAffected > 0;
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"Error updating SqlDBObject: {ex.Message}", ex);
			}
		}

		public async Task<bool> UpdateSqlDBObjectAsync(SqlDBObject obj)
		{
			try
			{
				using (var connection = new NpgsqlConnection(connectionStr))
				{
					await connection.OpenAsync();
					string query = "UPDATE \"SqlDBObjects\" SET \"Name\" = @Name, \"CreatedAt\" = @CreatedAt WHERE \"Id\" = @Id";

					using (var command = new NpgsqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@Id", obj.Id);
						command.Parameters.AddWithValue("@Name", obj.Name);
						command.Parameters.AddWithValue("@CreatedAt", obj.CreatedAt);

						int rowsAffected = await command.ExecuteNonQueryAsync();
						return rowsAffected > 0;
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"Error updating SqlDBObject: {ex.Message}", ex);
			}
		}

		#endregion

		#region DELETE Operations

		public bool DeleteById(int id)
		{
			try
			{
				using (var connection = new NpgsqlConnection(connectionStr))
				{
					connection.Open();
					string query = "DELETE FROM \"SqlDBObjects\" WHERE \"Id\" = @Id";

					using (var command = new NpgsqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@Id", id);

						int rowsAffected = command.ExecuteNonQuery();
						return rowsAffected > 0;
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"Error deleting SqlDBObject by ID: {ex.Message}", ex);
			}
		}

		public bool DeleteByName(string name)
		{
			try
			{
				using (var connection = new NpgsqlConnection(connectionStr))
				{
					connection.Open();
					string query = "DELETE FROM \"SqlDBObjects\" WHERE \"Name\" = @Name";

					using (var command = new NpgsqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@Name", name);

						int rowsAffected = command.ExecuteNonQuery();
						return rowsAffected > 0;
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"Error deleting SqlDBObject by name: {ex.Message}", ex);
			}
		}

		public async Task<bool> DeleteByIdAsync(int id)
		{
			try
			{
				using (var connection = new NpgsqlConnection(connectionStr))
				{
					await connection.OpenAsync();
					string query = "DELETE FROM \"SqlDBObjects\" WHERE \"Id\" = @Id";

					using (var command = new NpgsqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@Id", id);

						int rowsAffected = await command.ExecuteNonQueryAsync();
						return rowsAffected > 0;
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"Error deleting SqlDBObject by ID: {ex.Message}", ex);
			}
		}

		#endregion

		#region Utility Methods

		public int GetRecordCount()
		{
			try
			{
				using (var connection = new NpgsqlConnection(connectionStr))
				{
					connection.Open();
					string query = "SELECT COUNT(*) FROM \"SqlDBObjects\"";

					using (var command = new NpgsqlCommand(query, connection))
					{
						return Convert.ToInt32(command.ExecuteScalar());
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"Error getting record count: {ex.Message}", ex);
			}
		}

		public bool TableExists()
		{
			try
			{
				using (var connection = new NpgsqlConnection(connectionStr))
				{
					connection.Open();
					string query = "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = 'public' AND table_name = 'SqlDBObjects'";

					using (var command = new NpgsqlCommand(query, connection))
					{
						return Convert.ToInt32(command.ExecuteScalar()) > 0;
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"Error checking if table exists: {ex.Message}", ex);
			}
		}

		#endregion
	}
}