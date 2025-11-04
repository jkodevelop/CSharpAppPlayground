using CSharpAppPlayground.DBClasses.Data;
using System.Diagnostics;

namespace CSharpAppPlayground.DBClasses.PostgresExamples
{
	public class PostgresBaseExamples
	{
		//private string connectionStr = string.Empty;
		//private SqlDBObject? dbObject;

		private PostgresBase psqlBase;

        public PostgresBaseExamples()
		{
            //connectionStr = ConfigurationManager.ConnectionStrings["PostgreSqlConnection"].ConnectionString;
            psqlBase = new PostgresBase();
        }

		#region INSERT Operations

		public int InsertSqlDBObject(SqlDBObject obj)
		{
			try
			{
                string query = "INSERT INTO \"SqlDBObject\" (\"Name\", \"CreatedAt\") VALUES (@Name, @CreatedAt) RETURNING \"Id\";";
				return psqlBase.WithSqlCommand(command => {

                    command.Parameters.AddWithValue("@Name", obj.Name);
                    command.Parameters.AddWithValue("@CreatedAt", obj.CreatedAt);

                    var result = command.ExecuteScalar();
                    return Convert.ToInt32(result);

                }, query);
			}
			catch (Exception ex)
			{
				Debug.Print($"InsertSqlDBObject(): {ex.Message}");
			}
			return -1;
		}

		public async Task<int> InsertSqlDBObjectAsync(SqlDBObject obj)
		{
			try
			{
                string query = "INSERT INTO \"SqlDBObject\" (\"Name\", \"CreatedAt\") VALUES (@Name, @CreatedAt) RETURNING \"Id\";";
				return await psqlBase.WithSqlCommandAsync(async command => {
					command.Parameters.AddWithValue("@Name", obj.Name);
					command.Parameters.AddWithValue("@CreatedAt", obj.CreatedAt);

					object? result = await command.ExecuteScalarAsync();
					return Convert.ToInt32(result);

				}, query);
			}
			catch (Exception ex)
			{
				Debug.Print($"InsertSqlDBObjectAsync(): {ex.Message}");
			}
			return -1;
        }

		#endregion

		#region SELECT Operations

		public SqlDBObject? GetById(int id)
		{
			try
			{
                string query = "SELECT \"Id\", \"Name\", \"CreatedAt\" FROM \"SqlDBObject\" WHERE \"Id\" = @Id";
				return psqlBase.WithSqlCommand(command =>
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
						return null;
                    }
                },query);
			}
			catch (Exception ex)
			{
				Debug.Print($"GetById({id}): {ex.Message}");
			}
			return null;
		}

		public List<SqlDBObject> GetAll()
		{
			var objects = new List<SqlDBObject>();
			try
			{
                string query = "SELECT \"Id\", \"Name\", \"CreatedAt\" FROM \"SqlDBObject\" ORDER BY \"CreatedAt\" DESC";
				psqlBase.WithSqlCommand<object>(command =>
				{
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
                    return true; // doesn't matter cause objects is populated by reference
                },query);
			}
			catch (Exception ex)
			{
				Debug.Print($"GetAll(): Error retrieving all SqlDBObject: {ex.Message}");
			}
			return objects;
		}

		public List<SqlDBObject> GetByName(string name)
		{
			var objects = new List<SqlDBObject>();

			try
			{
                string query = "SELECT \"Id\", \"Name\", \"CreatedAt\" FROM \"SqlDBObject\" WHERE \"Name\" ILIKE @Name ORDER BY \"CreatedAt\" DESC";
                psqlBase.WithSqlCommand<object>(command =>
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
                    return true; // doesn't matter cause objects is populated by reference
                }, query);
			}
			catch (Exception ex)
			{
				Debug.Print($"GetByName({name}): Error retrieving SqlDBObject by name: {ex.Message}");
			}
			return objects;
		}

		public async Task<List<SqlDBObject>> GetAllAsync()
		{
			var objects = new List<SqlDBObject>();

			try
			{
                string query = "SELECT \"Id\", \"Name\", \"CreatedAt\" FROM \"SqlDBObject\" ORDER BY \"CreatedAt\" DESC";
				await psqlBase.WithSqlCommandAsync<object>(async command =>
				{
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
					return true; // doesn't matter cause objects is populated by reference
				}, query);
			}
			catch (Exception ex)
			{
				Debug.Print($"GetAllAsync(): Error retrieving all SqlDBObject: {ex.Message}");
			}

			return objects;
		}

		#endregion

		#region UPDATE Operations

		public bool UpdateSqlDBObject(SqlDBObject obj)
		{
			try
			{
                string query = "UPDATE \"SqlDBObject\" SET \"Name\" = @Name, \"CreatedAt\" = @CreatedAt WHERE \"Id\" = @Id";
				return psqlBase.WithSqlCommand(command =>
				{
                    command.Parameters.AddWithValue("@Id", obj.Id);
                    command.Parameters.AddWithValue("@Name", obj.Name);
                    command.Parameters.AddWithValue("@CreatedAt", obj.CreatedAt);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
				}, query); // just to validate the query
			}
			catch (Exception ex)
			{
				Debug.Print($"Error updating SqlDBObject: {ex.Message}", ex);
			}
			return false;
        }

		public async Task<bool> UpdateSqlDBObjectAsync(SqlDBObject obj)
		{
			try
			{
                string query = "UPDATE \"SqlDBObject\" SET \"Name\" = @Name, \"CreatedAt\" = @CreatedAt WHERE \"Id\" = @Id";
				return await psqlBase.WithSqlCommandAsync(async command =>
				{
                    command.Parameters.AddWithValue("@Id", obj.Id);
                    command.Parameters.AddWithValue("@Name", obj.Name);
                    command.Parameters.AddWithValue("@CreatedAt", obj.CreatedAt);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }, query);
			}
			catch (Exception ex)
			{
				Debug.Print($"UpdateSqlDBObjectAsync(): {ex.Message}");
			}
			return false;
        }

		#endregion

		#region DELETE Operations

		public bool DeleteById(int id)
		{
			try
			{
                string query = "DELETE FROM \"SqlDBObject\" WHERE \"Id\" = @Id";
				return psqlBase.WithSqlCommand(command =>
				{
                    command.Parameters.AddWithValue("@Id", id);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }, query);
			}
			catch (Exception ex)
			{
				Debug.Print($"DeleteById({id}): {ex.Message}");
			}
			return false;
		}

		public bool DeleteByName(string name)
		{
			try
			{
                string query = "DELETE FROM \"SqlDBObject\" WHERE \"Name\" = @Name";
                return psqlBase.WithSqlCommand(command =>
                {
                    command.Parameters.AddWithValue("@Name", name);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }, query);
			}
			catch (Exception ex)
			{
				Debug.Print($"DeleteByName({name}): {ex.Message}");
			}
			return false;
        }

		public async Task<bool> DeleteByIdAsync(int id)
		{
			try
			{
                string query = "DELETE FROM \"SqlDBObject\" WHERE \"Id\" = @Id";
                return await psqlBase.WithSqlCommandAsync(async command =>
                {
                    command.Parameters.AddWithValue("@Id", id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }, query);
			}
			catch (Exception ex)
			{ 
				Debug.Print($"DeleteByIdAsync({id}): {ex.Message}");
			}
			return false;
        }

		#endregion

		#region Utility Methods

		public int GetRecordCount()
		{
			try
			{
                string query = "SELECT COUNT(*) FROM \"SqlDBObject\"";
				return psqlBase.WithSqlCommand(command =>
				{
                    return Convert.ToInt32(command.ExecuteScalar());
                }, query);
			}
			catch (Exception ex)
			{
				Debug.Print($"GetRecordCount(): {ex.Message}");
			}
			return -1;
		}

		public bool TableExists()
		{
			try
			{
                string query = "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = 'public' AND table_name = 'SqlDBObject'";
                return psqlBase.WithSqlCommand(command =>
                {
                    return Convert.ToInt32(command.ExecuteScalar()) > 0;
                }, query);
			}
			catch (Exception ex)
			{
                Debug.Print($"TableExists(): {ex.Message}");
            }
			return false;
		}

		#endregion
	
		public void RunBasicExample()
		{
            Debug.Print("Postgre Basic Example Started");
            try
			{	
				// Check if table exists
				if (!TableExists())
				{
					Debug.Print("Table 'SqlDBObject' does not exist. Please create the table before running this example.");
					return;
				}
				// Insert a new record
				var newObj = new SqlDBObject
				{
					Name = "Test Object",
					CreatedAt = DateTime.Now
				};
				int newId = InsertSqlDBObject(newObj);
				Debug.Print($"Inserted new SqlDBObject with Id: {newId}");
				// Retrieve the inserted record
				var retrievedObj = GetById(newId);
				if (retrievedObj != null)
				{
					Debug.Print($"Retrieved SqlDBObject: Id={retrievedObj.Id}, Name={retrievedObj.Name}, CreatedAt={retrievedObj.CreatedAt}");
				}
				// Update the record
				if (retrievedObj != null)
				{
					retrievedObj.Name = "Updated Test Object";
					bool updateResult = UpdateSqlDBObject(retrievedObj);
					Debug.Print($"Update result: {updateResult}");
				}
				// Retrieve all records
				//var allObjects = GetAll();
				//Debug.Print($"Total SqlDBObject records: {allObjects.Count}");
				// Delete the inserted record
				bool deleteResult = DeleteById(newId);
				Debug.Print($"Delete result for Id {newId}: {deleteResult}");
				Debug.Print("PostgreSQL Basic Example Completed");
			}
			catch (Exception ex)
			{
				Debug.Print($"RunBasicExample(): {ex.Message}");
			}
        }
    }
}