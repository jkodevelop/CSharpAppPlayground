using CSharpAppPlayground.DBClasses.Data;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace CSharpAppPlayground.DBClasses.MariaDBExamples
{
    public class MariaDBBaseExamples
    {
        private MariaDBBase mariaBase;

        public MariaDBBaseExamples()
        {
            mariaBase = new MariaDBBase();
        }

        #region INSERT Operations
        public int InsertSqlDBObject(SqlDBObject obj)
        {
            try
            {
                string query = "INSERT INTO SqlDBObject (Name, CreatedAt) VALUES (@Name, @CreatedAt); SELECT LAST_INSERT_ID();";
                return mariaBase.WithSqlCommand((command =>
                {
                    command.Parameters.AddWithValue("@Name", obj.Name);
                    command.Parameters.AddWithValue("@CreatedAt", obj.CreatedAt);
                    var result = command.ExecuteScalar();
                    return Convert.ToInt32(result);
                }), query);
            }
            catch (Exception ex)
            {
                Debug.Print($"InsertSqlDBObject(): {ex.Message}");
            }
            return -1;
        }

        public Task<int> InsertSqlDBObjectAsync(SqlDBObject obj)
        {
            try
            {
                string query = "INSERT INTO SqlDBObject (Name, CreatedAt) VALUES (@Name, @CreatedAt); SELECT LAST_INSERT_ID();";
                return mariaBase.WithSqlCommandAsync(async command =>
                {
                    command.Parameters.AddWithValue("@Name", obj.Name);
                    command.Parameters.AddWithValue("@CreatedAt", obj.CreatedAt);
                    var result = await command.ExecuteScalarAsync();
                    return Convert.ToInt32(result);
                }, query);
            }
            catch (Exception ex)
            {
                Debug.Print($"InsertSqlDBObjectAsync(): {ex.Message}");
            }
            return Task.FromResult(-1);
        }
        #endregion INSERT Operations

        #region SELECT Operations
        public SqlDBObject? GetById(int id)
        {
            SqlDBObject? obj = null;
            try
            {
                string query = "SELECT Id, Name, CreatedAt FROM SqlDBObject WHERE Id = @Id";
                obj = mariaBase.WithSqlCommand(command =>
                {
                    SqlDBObject? res = null;
                    command.Parameters.AddWithValue("@Id", id);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            res = new SqlDBObject
                            {
                                Id = reader.GetInt32("Id"),
                                Name = reader.GetString("Name"),
                                CreatedAt = reader.GetDateTime("CreatedAt")
                            };
                        }
                    }
                    return res;
                }, query);
            }
            catch (Exception ex)
            {
                Debug.Print($"GetById({id}): {ex.Message}");
            }
            return obj;
        }

        public SqlDBObject? GetLastInserted()
        {
            try
            {
                string query = "SELECT Id, Name, CreatedAt FROM SqlDBObject ORDER BY Id DESC LIMIT 1";
                return mariaBase.WithSqlCommand(command =>
                {
                    SqlDBObject? res = null;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            res = new SqlDBObject
                            {
                                Id = reader.GetInt32("Id"),
                                Name = reader.GetString("Name"),
                                CreatedAt = reader.GetDateTime("CreatedAt")
                            };
                        }
                    }
                    return res;
                }, query);
            }
            catch (Exception ex)
            {
                Debug.Print($"GetLastInserted(): {ex.Message}");
            }
            return null;
        }

        public List<SqlDBObject> GetAll()
        {
            List<SqlDBObject> objects = new List<SqlDBObject>();
            try
            {
                string query = "SELECT Id, Name, CreatedAt FROM SqlDBObject ORDER BY CreatedAt DESC";
                mariaBase.WithSqlCommand<object>(command =>
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            objects.Add(new SqlDBObject
                            {
                                Id = reader.GetInt32("Id"),
                                Name = reader.GetString("Name"),
                                CreatedAt = reader.GetDateTime("CreatedAt")
                            });
                        }
                    }
                    return true; // doesn't matter cause objects is populated by reference
                }, query);
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
                string query = "SELECT Id, Name, CreatedAt FROM SqlDBObject WHERE Name LIKE @Name ORDER BY CreatedAt DESC";
                mariaBase.WithSqlCommand<object>(command =>
                {
                    command.Parameters.AddWithValue("@Name", $"%{name}%");
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            objects.Add(new SqlDBObject
                            {
                                Id = reader.GetInt32("Id"),
                                Name = reader.GetString("Name"),
                                CreatedAt = reader.GetDateTime("CreatedAt")
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
            List<SqlDBObject> objects = new List<SqlDBObject>();
            try
            {
                string query = "SELECT Id, Name, CreatedAt FROM SqlDBObject ORDER BY CreatedAt DESC";
                await mariaBase.WithSqlCommandAsync<object>(async command =>
                {
                    using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            objects.Add(new SqlDBObject
                            {
                                Id = reader.GetInt32("Id"),
                                Name = reader.GetString("Name"),
                                CreatedAt = reader.GetDateTime("CreatedAt")
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
        #endregion SELECT Operations

        #region UPDATE Operations

        public bool UpdateSqlDBObject(SqlDBObject obj)
        {
            try
            {
                string query = "UPDATE SqlDBObject SET Name = @Name, CreatedAt = @CreatedAt WHERE Id = @Id";
                return mariaBase.WithSqlCommand(command =>
                {
                    command.Parameters.AddWithValue("@Id", obj.Id);
                    command.Parameters.AddWithValue("@Name", obj.Name);
                    command.Parameters.AddWithValue("@CreatedAt", obj.CreatedAt);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }, query);
            }
            catch (Exception ex)
            {
                Debug.Print($"UpdateSqlDBObject(): {ex.Message}");
            }
            return false;
        }

        public async Task<bool> UpdateSqlDBObjectAsync(SqlDBObject obj)
        {
            try
            {
                string query = "UPDATE SqlDBObject SET Name = @Name, CreatedAt = @CreatedAt WHERE Id = @Id";
                return await mariaBase.WithSqlCommandAsync(async command =>
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

        #endregion UPDATE Operations

        #region DELETE Operations

        public bool DeleteById(int id)
        {
            try
            {
                string query = "DELETE FROM SqlDBObject WHERE Id = @Id";
                return mariaBase.WithSqlCommand(command =>
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
                string query = "DELETE FROM SqlDBObject WHERE Name = @Name";
                return mariaBase.WithSqlCommand(command =>
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
                string query = "DELETE FROM SqlDBObject WHERE Id = @Id";
                return await mariaBase.WithSqlCommandAsync(async command =>
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
        #endregion DELETE Operations

        #region Utility Methods

        public int GetRecordCount()
        {
            try
            {
                string query = "SELECT COUNT(*) FROM SqlDBObject";
                return mariaBase.WithSqlCommand(command =>
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
                string query = "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = DATABASE() AND table_name = 'SqlDBObject'";
                return mariaBase.WithSqlCommand(command =>
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

        #endregion Utility Methods

        public void RunBasicExample()
        {
            // this run a basic example of inserting, retrieving, updating, and deleting a SqlDBObject
            Debug.Print("MariaDB Basic Example Started");
            try
            {
                // Insert
                var newObj = new SqlDBObject { Name = "Test Object", CreatedAt = DateTime.Now };
                int newId = InsertSqlDBObject(newObj);
                Debug.Print($"Inserted new SqlDBObject with Id: {newId}");
                // Retrieve
                var retrievedObj = GetById(newId);
                if (retrievedObj != null)
                {
                    Debug.Print($"Retrieved SqlDBObject: {retrievedObj.ToString()}");
                }
                // Update
                if (retrievedObj != null)
                {
                    retrievedObj.Name = "Updated Test Object";
                    bool updateSuccess = UpdateSqlDBObject(retrievedObj);
                    Debug.Print($"Update success: {updateSuccess}");
                }
                // Delete
                bool deleteSuccess = DeleteById(newId);
                Debug.Print($"Delete success byId({newId}): {deleteSuccess}");
            }
            catch (Exception ex)
            {
                Debug.Print($"RunBasicExample(): {ex.Message}");
            }
        }
    }
}
