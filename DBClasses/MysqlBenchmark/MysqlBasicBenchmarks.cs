using CSharpAppPlayground.Classes.DataGen.Generators;
using CSharpAppPlayground.DBClasses.Data.SQLbenchmark;
using CSharpAppPlayground.DBClasses.MysqlExamples;
using MethodTimer;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Ocsp;
using System.Configuration;
using System.Diagnostics;
using System.Numerics;
using System.Text;

namespace CSharpAppPlayground.DBClasses.MysqlBenchmark
{
    public class MysqlBasicBenchmarks
    {
        private string connectionStr;
        private MysqlBase mysqlBase;

        public MysqlBasicBenchmarks()
        {
            connectionStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            mysqlBase = new MysqlBase();
        }

        /// <summary>
        /// Simple loop-based single inserts (baseline for comparison)
        /// </summary>
        public void RunBulkInsertBenchmark(int dataSetSize)
        {
            Debug.Print("=== MySQL Bulk Insert Examples with VidsSQL ===");

            // Generate test data
            GenerateVidsSQL generator = new GenerateVidsSQL();
            List<VidsSQL> testData = generator.GenerateData(dataSetSize);
            Debug.Print($"Generated {testData.Count} test records\n");

            // Example 1: Single insert loop (baseline)
            Test_InsertSimpleLoop(testData);

            // Example 2: Multi-value INSERT statement
            Test_InsertMultiValue(testData);

            // Example 3: Transaction with batched inserts
            Test_InsertWithTransaction(testData);

            // Example 4: Prepared statement with batching
            Test_InsertWithPreparedStatement(testData);

            // Example 5: Prepared statement with batching and transaction
            Test_BulkInsertWithPreparedStatementAndTransaction(testData);

            Debug.Print("\n=== Benchmark Complete ===");
        }

        [Time("BulkInsertSingleLoop:")]
        protected void Test_InsertSimpleLoop(List<VidsSQL> testData)
        {
            Debug.Print("\n--- Method 1: Single Insert Loop ---");
            int insertedCount = BulkInsertSingleLoop(testData);
            Debug.Print($"Inserted {insertedCount} records using Single Loop\n");
        }

        [Time("BulkInsertMultiValue:")]
        protected void Test_InsertMultiValue(List<VidsSQL> testData)
        {
            Debug.Print("\n--- Method 2: Multi-Value INSERT Statement ---");
            int insertedCount = BulkInsertMultiValue(testData);
            Debug.Print($"Inserted {insertedCount} records using Multi-Value\n");
        }

        [Time("BulkInsertWithTransaction:")]
        protected void Test_InsertWithTransaction(List<VidsSQL> testData)
        {
            Debug.Print("\n--- Method 3: Transaction with Batched Inserts ---");
            int insertedCount = BulkInsertWithTransaction(testData);
            Debug.Print($"Inserted {insertedCount} records using Transaction with Batching\n");
        }

        [Time("BulkInsertWithPreparedStatement:")]
        protected void Test_InsertWithPreparedStatement(List<VidsSQL> testData)
        {
            Debug.Print("\n--- Method 4: Prepared Statement with Batching ---");
            int insertedCount = BulkInsertWithPreparedStatement(testData);
            Debug.Print($"Inserted {insertedCount} records using Prepared Statement with Batching\n");
        }

        [Time("BulkInsertWithPreparedStatementAndTransaction:")]
        protected void Test_BulkInsertWithPreparedStatementAndTransaction(List<VidsSQL> testData)
        {
            Debug.Print("\n--- Method 5: Prepared Statement with Batching + transaction ---");
            int insertedCount = BulkInsertWithPreparedStatementAndTransaction(testData);
            Debug.Print($"Inserted {insertedCount} records using Prepared Statement with Batching\n");
        }

        /// <summary>
        /// Method 1: Simple loop with individual INSERT statements
        /// Pros: Simple, safe for large datasets
        /// Cons: Slowest due to many round trips
        /// </summary>
        private int BulkInsertSingleLoop(List<VidsSQL> vids)
        {
            int insertedCount = 0;
            try
            {
                using (var connection = new MySqlConnection(connectionStr))
                {
                    connection.Open();
                    foreach (var vid in vids)
                    {
                        string query = "INSERT INTO Vids (filename, filesizebyte, duration, metadatetime, width, height) " +
                                      "VALUES (@filename, @filesizebyte, @duration, @metadatetime, @width, @height)";
                        
                        using (var command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@filename", vid.filename);
                            command.Parameters.AddWithValue("@filesizebyte", ConvertBigIntegerToDbValue(vid.filesizebyte));
                            command.Parameters.AddWithValue("@duration", vid.duration.HasValue ? vid.duration.Value : DBNull.Value);
                            command.Parameters.AddWithValue("@metadatetime", vid.metadatetime.HasValue ? vid.metadatetime.Value : DBNull.Value);
                            command.Parameters.AddWithValue("@width", vid.width.HasValue ? vid.width.Value : DBNull.Value);
                            command.Parameters.AddWithValue("@height", vid.height.HasValue ? vid.height.Value : DBNull.Value);
                            
                            command.ExecuteNonQuery();
                            insertedCount++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"Error in BulkInsertSingleLoop: {ex.Message}");
            }
            return insertedCount;
        }

        /// <summary>
        /// Method 2: Multi-value INSERT statement (INSERT INTO ... VALUES (val1, val2), (val3, val4), ...)
        /// Pros: Fast for smaller datasets
        /// Cons: Can hit MySQL packet size limits for very large datasets
        /// </summary>
        private int BulkInsertMultiValue(List<VidsSQL> vids)
        {
            int insertedCount = 0;
            const int batchSize = 1000; // Insert in batches to avoid packet size limits

            try
            {
                using (var connection = new MySqlConnection(connectionStr))
                {
                    connection.Open();
                    
                    for (int i = 0; i < vids.Count; i += batchSize)
                    {
                        var batch = vids.Skip(i).Take(batchSize).ToList();
                        var query = new StringBuilder();
                        
                        query.Append("INSERT INTO Vids (filename, filesizebyte, duration, metadatetime, width, height) VALUES ");
                        
                        for (int j = 0; j < batch.Count; j++)
                        {
                            var vid = batch[j];
                            // For multi-value SQL we inline numeric values (or NULL) — BigInteger.ToString() is safe here.
                            string filesizeSql = vid.filesizebyte.HasValue ? vid.filesizebyte.Value.ToString() : "NULL";
                            query.Append($"('{MySqlHelper.EscapeString(vid.filename)}', " +
                                        $"{filesizeSql}, " +
                                        $"{(vid.duration.HasValue ? vid.duration.Value.ToString() : "NULL")}, " +
                                        $"{(vid.metadatetime.HasValue ? $"'{vid.metadatetime.Value:yyyy-MM-dd HH:mm:ss}'" : "NULL")}, " +
                                        $"{(vid.width.HasValue ? vid.width.Value.ToString() : "NULL")}, " +
                                        $"{(vid.height.HasValue ? vid.height.Value.ToString() : "NULL")})");
                            
                            if (j < batch.Count - 1)
                                query.Append(", ");
                        }

                        using (var command = new MySqlCommand(query.ToString(), connection))
                        {
                            insertedCount += command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"Error in BulkInsertMultiValue: {ex.Message}");
            }
            return insertedCount;
        }

        /// <summary>
        /// Method 3: Transaction with batched inserts
        /// Pros: All-or-nothing, fast
        /// Cons: Requires careful memory management
        /// </summary>
        private int BulkInsertWithTransaction(List<VidsSQL> vids)
        {
            int insertedCount = 0;
            const int batchSize = 500;

            try
            {
                using (var connection = new MySqlConnection(connectionStr))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            for (int i = 0; i < vids.Count; i += batchSize)
                            {
                                var batch = vids.Skip(i).Take(batchSize).ToList();
                                
                                foreach (var vid in batch)
                                {
                                    string query = "INSERT INTO Vids (filename, filesizebyte, duration, metadatetime, width, height) " +
                                                  "VALUES (@filename, @filesizebyte, @duration, @metadatetime, @width, @height)";
                                    
                                    using (var command = new MySqlCommand(query, connection, transaction))
                                    {
                                        command.Parameters.AddWithValue("@filename", vid.filename);
                                        // BigInteger handling, only limited long is supported directly
                                        // command.Parameters.AddWithValue("@filesizebyte", vid.filesizebyte.HasValue ? vid.filesizebyte.Value : DBNull.Value);
                                        command.Parameters.AddWithValue("@filesizebyte", ConvertBigIntegerToDbValue(vid.filesizebyte));
                                        command.Parameters.AddWithValue("@duration", vid.duration.HasValue ? vid.duration.Value : DBNull.Value);
                                        command.Parameters.AddWithValue("@metadatetime", vid.metadatetime.HasValue ? vid.metadatetime.Value : DBNull.Value);
                                        command.Parameters.AddWithValue("@width", vid.width.HasValue ? vid.width.Value : DBNull.Value);
                                        command.Parameters.AddWithValue("@height", vid.height.HasValue ? vid.height.Value : DBNull.Value);
                                        
                                        command.ExecuteNonQuery();
                                        insertedCount++;
                                    }
                                }
                            }
                            
                            transaction.Commit();
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"Error in BulkInsertWithTransaction: {ex.Message}");
            }
            return insertedCount;
        }

        /// <summary>
        /// Method 4: Prepared statement with batching
        /// Pros: Very fast, secure against SQL injection, optimized for MySQL
        /// Cons: More complex
        /// </summary>
        private int BulkInsertWithPreparedStatement(List<VidsSQL> vids)
        {
            int insertedCount = 0;
            const int batchSize = 1000;

            try
            {
                using (var connection = new MySqlConnection(connectionStr))
                {
                    connection.Open();
                    
                    // Prepare the statement
                    string query = "INSERT INTO Vids (filename, filesizebyte, duration, metadatetime, width, height) " +
                                  "VALUES (@filename, @filesizebyte, @duration, @metadatetime, @width, @height)";
                    
                    using (var command = new MySqlCommand(query, connection))
                    {
                        // Add parameters once
                        command.Parameters.Add("@filename", MySqlDbType.VarChar);
                        command.Parameters.Add("@filesizebyte", MySqlDbType.Int64);
                        command.Parameters.Add("@duration", MySqlDbType.Int32);
                        command.Parameters.Add("@metadatetime", MySqlDbType.DateTime);
                        command.Parameters.Add("@width", MySqlDbType.Int32);
                        command.Parameters.Add("@height", MySqlDbType.Int32);
                        
                        // Prepare the command
                        command.Prepare();
                        
                        // Execute in batches
                        for (int i = 0; i < vids.Count; i += batchSize)
                        {
                            var batch = vids.Skip(i).Take(batchSize).ToList();
                            
                            foreach (var vid in batch)
                            {
                                command.Parameters["@filename"].Value = vid.filename;
                                // BigInteger handling, only limited long is supported directly
                                // command.Parameters["@filesizebyte"].Value = vid.filesizebyte.HasValue ? vid.filesizebyte.Value : DBNull.Value;
                                command.Parameters["@filesizebyte"].Value = ConvertBigIntegerToDbValue(vid.filesizebyte);
                                command.Parameters["@duration"].Value = vid.duration.HasValue ? vid.duration.Value : DBNull.Value;
                                command.Parameters["@metadatetime"].Value = vid.metadatetime.HasValue ? vid.metadatetime.Value : DBNull.Value;
                                command.Parameters["@width"].Value = vid.width.HasValue ? vid.width.Value : DBNull.Value;
                                command.Parameters["@height"].Value = vid.height.HasValue ? vid.height.Value : DBNull.Value;
                                
                                command.ExecuteNonQuery();
                                insertedCount++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"Error in BulkInsertWithPreparedStatement: {ex.Message}");
            }
            return insertedCount;
        }

        private int BulkInsertWithPreparedStatementAndTransaction(List<VidsSQL> vids)
        {
            int insertedCount = 0;
            const int batchSize = 1000;

            try
            {
                using (var connection = new MySqlConnection(connectionStr))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {

                        // Prepare the statement
                        string query = "INSERT INTO Vids (filename, filesizebyte, duration, metadatetime, width, height) " +
                                  "VALUES (@filename, @filesizebyte, @duration, @metadatetime, @width, @height)";

                        using (var command = new MySqlCommand(query, connection))
                        {
                            // Add parameters once
                            command.Parameters.Add("@filename", MySqlDbType.VarChar);
                            command.Parameters.Add("@filesizebyte", MySqlDbType.Int64);
                            command.Parameters.Add("@duration", MySqlDbType.Int32);
                            command.Parameters.Add("@metadatetime", MySqlDbType.DateTime);
                            command.Parameters.Add("@width", MySqlDbType.Int32);
                            command.Parameters.Add("@height", MySqlDbType.Int32);

                            // Prepare the command
                            command.Prepare();

                            // Execute in batches
                            for (int i = 0; i < vids.Count; i += batchSize)
                            {
                                var batch = vids.Skip(i).Take(batchSize).ToList();

                                foreach (var vid in batch)
                                {
                                    command.Parameters["@filename"].Value = vid.filename;
                                    // BigInteger handling, only limited long is supported directly
                                    // command.Parameters["@filesizebyte"].Value = vid.filesizebyte.HasValue ? vid.filesizebyte.Value : DBNull.Value;
                                    command.Parameters["@filesizebyte"].Value = ConvertBigIntegerToDbValue(vid.filesizebyte);
                                    command.Parameters["@duration"].Value = vid.duration.HasValue ? vid.duration.Value : DBNull.Value;
                                    command.Parameters["@metadatetime"].Value = vid.metadatetime.HasValue ? vid.metadatetime.Value : DBNull.Value;
                                    command.Parameters["@width"].Value = vid.width.HasValue ? vid.width.Value : DBNull.Value;
                                    command.Parameters["@height"].Value = vid.height.HasValue ? vid.height.Value : DBNull.Value;

                                    command.ExecuteNonQuery();
                                    insertedCount++;
                                }
                            }
                        }
                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"Error in BulkInsertWithPreparedStatementAndTransaction: {ex.Message}");
            }
            return insertedCount;
        }

        /// <summary>
        /// Cleanup method to clear the Vids table
        /// </summary>
        public void CleanupVidsTable()
        {
            try
            {
                mysqlBase.WithConnection(connection =>
                {
                    using (var command = new MySqlCommand("DELETE FROM Vids", connection))
                    {
                        int rowsDeleted = command.ExecuteNonQuery();
                        Debug.Print($"Cleaned up {rowsDeleted} records from Vids table");
                        return true;
                    }
                });
            }
            catch (Exception ex)
            {
                Debug.Print($"Error in CleanupVidsTable: {ex.Message}");
            }
        }

        /// <summary>
        /// Clears the Vids table and resets the AUTO_INCREMENT counter so IDs start from 1 on next insert.
        /// Uses TRUNCATE when possible (fast and resets auto-increment), falls back to DELETE + ALTER if TRUNCATE fails
        /// (e.g. when foreign key constraints prevent TRUNCATE).
        /// </summary>
        public void ResetVidsTable()
        {
            try
            {
                mysqlBase.WithConnection(connection =>
                {
                    // Prefer TRUNCATE (fast, resets AUTO_INCREMENT). If it fails (FK constraints or insufficient privileges),
                    // fall back to DELETE + ALTER.
                    try
                    {
                        using (var cmd = new MySqlCommand("TRUNCATE TABLE Vids", connection))
                        {
                            cmd.ExecuteNonQuery();
                            Debug.Print("Truncated Vids table; AUTO_INCREMENT reset automatically.");
                            return true;
                        }
                    }
                    catch (MySqlException trxEx)
                    {
                        Debug.Print($"TRUNCATE failed ({trxEx.Message}), falling back to DELETE + ALTER TABLE.");
                        // Fallback: delete rows then reset auto increment
                        using (var delCmd = new MySqlCommand("DELETE FROM Vids", connection))
                        {
                            int rowsDeleted = delCmd.ExecuteNonQuery();
                            Debug.Print($"Deleted {rowsDeleted} rows from Vids table (fallback).");
                        }

                        using (var alterCmd = new MySqlCommand("ALTER TABLE Vids AUTO_INCREMENT = 1", connection))
                        {
                            alterCmd.ExecuteNonQuery();
                            Debug.Print("Reset AUTO_INCREMENT for Vids to 1 (fallback).");
                        }

                        return true;
                    }
                });
            }
            catch (Exception ex)
            {
                Debug.Print($"Error in ResetVidsTable: {ex.Message}");
            }
        }

        /// <summary>
        /// Get record count from Vids table
        /// </summary>
        public int GetVidsCount()
        {
            try
            {
                return mysqlBase.WithSqlCommand(command =>
                {
                    return Convert.ToInt32(command.ExecuteScalar());
                }, "SELECT COUNT(*) FROM Vids");
            }
            catch (Exception ex)
            {
                Debug.Print($"Error in GetVidsCount: {ex.Message}");
                return -1;
            }
        }

        /// <summary>
        /// Convert BigInteger? into a DB-friendly value.
        /// Returns DBNull.Value when null or when the BigInteger cannot be represented as Int64.
        /// When in-range, returns a boxed Int64 (long) which is IConvertible and accepted by MySqlParameter.
        /// </summary>
        private static object ConvertBigIntegerToDbValue(BigInteger? value)
        {
            if (!value.HasValue)
                return DBNull.Value;

            BigInteger v = value.Value;
            var max = new BigInteger(long.MaxValue);
            var min = new BigInteger(long.MinValue);

            if (v <= max && v >= min)
            {
                return (long)v;
            }
            else
            {
                // Out of range for BIGINT in MySQL. Log and return NULL to avoid casting exceptions.
                Debug.Print($"BigInteger value {v} is outside Int64 range; inserting NULL instead.");
                return DBNull.Value;
            }
        }
    }
}
