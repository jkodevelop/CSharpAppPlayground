using CSharpAppPlayground.Classes.DataGen.Generators;
using CSharpAppPlayground.DBClasses.Data;
using CSharpAppPlayground.DBClasses.Data.SQLbenchmark;
using CSharpAppPlayground.DBClasses.PostgresExamples;
using MethodTimer;
using MongoDB.Driver;
using RepoDb;
using Npgsql;
using NpgsqlTypes;
using PgPartner;
using System.Configuration;
using System.Diagnostics;
using System.Text;

// Required NuGet: PgPartner + Npgsql

namespace CSharpAppPlayground.DBClasses.PostgresBenchmark
{
    public class PostgresBasicBenchmarks
    {
        private string connectionStr;
        private PostgresBase postgresBase;
        private DataGenHelper dataGenHelper;

        private int batchLimit = 5000;
        private int overloadLimit = 50000;

        private int repoDBBatchLimit = 500;
        public bool repoDBTestEnabled { set; get; } = false;

        private string csvFilePath = @".\testdata\postgres_vids_bulk_insert.csv";

        public PostgresBasicBenchmarks()
        {
            connectionStr = ConfigurationManager.ConnectionStrings["PostgreSqlConnection"].ConnectionString;
            postgresBase = new PostgresBase();
            dataGenHelper = new DataGenHelper();
        }

        public void FastestCompareBenchmark(int dataSetSize)
        {
            // only test with fastest APIs for big data, Note: if its less then 10000 records the benchmark is kinda pointless
            GenerateVidsSQL generator = new GenerateVidsSQL();
            List<VidsSQL> testData = generator.GenerateData(dataSetSize);
            Test_BulkInsertUseBinaryImport(testData);
            if (dataGenHelper.GenCSVfileWithData(testData, csvFilePath))
                Test_BulkInsertUseCopyCommand(csvFilePath);
            else
                Debug.Print("Failed to generate CSV file for bulk insert, cannot run Test_BulkInsertUseCopyCommand");
        }

        /// <summary>
        /// Runs all bulk insert benchmarks with VidsSQL objects
        /// </summary>
        public void RunBulkInsertBenchmark(int dataSetSize)
        {
            Debug.Print("=== PostgreSQL Bulk Insert Examples with VidsSQL ===");

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

            // Example 6: PostgreSQL COPY command (native bulk insert) [Second FASTEST OPTION]
            if (dataGenHelper.GenCSVfileWithData(testData, csvFilePath))
                Test_BulkInsertUseCopyCommand(csvFilePath);
            else
                Debug.Print("Failed to generate CSV file for bulk insert, cannot run Test_BulkInsertUseCopyCommand");

            // Example 7: Npgsql Binary Import [FASTEST OPTION]
            Test_BulkInsertUseBinaryImport(testData);

            if (repoDBTestEnabled)
            {
                // Example 8: RepoDB InsertAll()
                List<RepoVidInsert> fixedData = dataGenHelper.ConvertListVidsDataPostgresAndRepoDBList(testData);
                Test_BulkInsertWithRepoDBInsertAll(fixedData);
            }
            else
                Debug.Print("skipping RepoDB InsertAll() test Postgres");
            
            //// Example 9: PgPartner BulkAdd()
            List<RepoVidInsert> convertedData = dataGenHelper.ConvertListVidsData(testData);
            Test_BulkAddWithPgPartner(convertedData);

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

        [Time("BulkInsertUseCopyCommand:")]
        protected void Test_BulkInsertUseCopyCommand(string filePath)
        {
            Debug.Print("\n--- Method 6: PostgreSQL COPY command Example ---");
            int insertedCount = BulkInsertUseCopyCommand(filePath);
            Debug.Print($"Inserted {insertedCount} records using COPY command\n");
        }

        [Time("BulkInsertUseBinaryImport:")]
        protected void Test_BulkInsertUseBinaryImport(List<VidsSQL> testData)
        {
            Debug.Print("\n--- Method 7: Npgsql Binary Import Example ---");
            int insertedCount = BulkInsertUseBinaryImport(testData);
            Debug.Print($"Inserted {insertedCount} records using Binary Import\n");
        }

        [Time("BulkInsertWithRepoDBInsertAll:")]
        protected void Test_BulkInsertWithRepoDBInsertAll(List<RepoVidInsert> convertedData)
        {
            Debug.Print("\n--- Method 8: RepoDB InsertAll() Example ---");
            // int insertedCount = BulkAddWithPgPartner(convertedData).GetAwaiter().GetResult(); // this doesn't work
            int insertedCount = BulkInsertWithRepoDBInsertAll(convertedData);
            Debug.Print($"Inserted {insertedCount} records using RepoDB InsertAll()\n");
        }

        [Time("BulkAddWithPgPartner:")]
        protected async void Test_BulkAddWithPgPartner(List<RepoVidInsert> convertedData)
        {
            Debug.Print("\n--- Method 9: PgPartner BulkAdd() Example ---");
            // int insertedCount = BulkAddWithPgPartner(convertedData).GetAwaiter().GetResult(); // this doesn't work
            int insertedCount = await BulkAddWithPgPartner(convertedData);
            Debug.Print($"Inserted {insertedCount} records using PgPartner BulkAdd()\n");
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
                using (var connection = new NpgsqlConnection(connectionStr))
                {
                    connection.Open();
                    foreach (var vid in vids)
                    {
                        string query = "INSERT INTO \"Vids\" (filename, filesizebyte, duration, metadatetime, width, height) " +
                                      "VALUES (@filename, @filesizebyte, @duration, @metadatetime, @width, @height)";

                        using (var command = new NpgsqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@filename", vid.filename);
                            command.Parameters.AddWithValue("@filesizebyte", DataGenHelper.ConvertBigIntegerToDbValue(vid.filesizebyte));
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
        /// Cons: Can hit PostgreSQL query size limits for very large datasets
        /// </summary>
        private int BulkInsertMultiValue(List<VidsSQL> vids)
        {
            int insertedCount = 0;
            int batchSize = batchLimit; // Insert in batches to avoid query size limits

            if (vids.Count >= overloadLimit)
            {
                Debug.Print($"Dataset size {vids.Count} exceeds overload limit {overloadLimit}, skipping Multi-Value Insert to avoid query size issues.");
                return -1;
            }

            try
            {
                using (var connection = new NpgsqlConnection(connectionStr))
                {
                    connection.Open();

                    for (int i = 0; i < vids.Count; i += batchSize)
                    {
                        var batch = vids.Skip(i).Take(batchSize).ToList();
                        var query = new StringBuilder();

                        query.Append("INSERT INTO \"Vids\" (filename, filesizebyte, duration, metadatetime, width, height) VALUES ");

                        for (int j = 0; j < batch.Count; j++)
                        {
                            var vid = batch[j];
                            // For multi-value SQL we inline numeric values (or NULL) — BigInteger.ToString() is safe here.
                            string filesizeSql = vid.filesizebyte.HasValue ? vid.filesizebyte.Value.ToString() : "NULL";
                            string durationSql = vid.duration.HasValue ? vid.duration.Value.ToString() : "NULL";
                            string widthSql = vid.width.HasValue ? vid.width.Value.ToString() : "NULL";
                            string heightSql = vid.height.HasValue ? vid.height.Value.ToString() : "NULL";
                            string datetimeSql = vid.metadatetime.HasValue ? $"'{vid.metadatetime.Value:yyyy-MM-dd HH:mm:ss}'::timestamp" : "NULL";

                            // Escape single quotes in filename for PostgreSQL
                            string escapedFilename = vid.filename.Replace("'", "''");
                            query.Append($"('{escapedFilename}', " +
                                        $"{filesizeSql}, " +
                                        $"{durationSql}, " +
                                        $"{datetimeSql}, " +
                                        $"{widthSql}, " +
                                        $"{heightSql})");

                            if (j < batch.Count - 1)
                                query.Append(", ");
                        }

                        using (var command = new NpgsqlCommand(query.ToString(), connection))
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
            int batchSize = batchLimit;

            try
            {
                using (var connection = new NpgsqlConnection(connectionStr))
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
                                    string query = "INSERT INTO \"Vids\" (filename, filesizebyte, duration, metadatetime, width, height) " +
                                                  "VALUES (@filename, @filesizebyte, @duration, @metadatetime, @width, @height)";

                                    using (var command = new NpgsqlCommand(query, connection, transaction))
                                    {
                                        command.Parameters.AddWithValue("@filename", vid.filename);
                                        command.Parameters.AddWithValue("@filesizebyte", DataGenHelper.ConvertBigIntegerToDbValue(vid.filesizebyte));
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
        /// Pros: Very fast, secure against SQL injection, optimized for PostgreSQL
        /// Cons: More complex
        /// </summary>
        private int BulkInsertWithPreparedStatement(List<VidsSQL> vids)
        {
            int insertedCount = 0;
            int batchSize = batchLimit;

            try
            {
                using (var connection = new NpgsqlConnection(connectionStr))
                {
                    connection.Open();

                    // Prepare the statement
                    string query = "INSERT INTO \"Vids\" (filename, filesizebyte, duration, metadatetime, width, height) " +
                                  "VALUES (@filename, @filesizebyte, @duration, @metadatetime, @width, @height)";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        // Add parameters once
                        command.Parameters.Add("@filename", NpgsqlTypes.NpgsqlDbType.Varchar);
                        command.Parameters.Add("@filesizebyte", NpgsqlTypes.NpgsqlDbType.Bigint);
                        command.Parameters.Add("@duration", NpgsqlTypes.NpgsqlDbType.Integer);
                        command.Parameters.Add("@metadatetime", NpgsqlTypes.NpgsqlDbType.Timestamp);
                        command.Parameters.Add("@width", NpgsqlTypes.NpgsqlDbType.Integer);
                        command.Parameters.Add("@height", NpgsqlTypes.NpgsqlDbType.Integer);

                        // Prepare the command
                        command.Prepare();

                        // Execute in batches
                        for (int i = 0; i < vids.Count; i += batchSize)
                        {
                            var batch = vids.Skip(i).Take(batchSize).ToList();

                            foreach (var vid in batch)
                            {
                                command.Parameters["@filename"].Value = vid.filename;
                                command.Parameters["@filesizebyte"].Value = DataGenHelper.ConvertBigIntegerToDbValue(vid.filesizebyte);
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

        /// <summary>
        /// Method 5: Prepared statement with batching and transaction
        /// Pros: Fastest for medium datasets, all-or-nothing, optimized
        /// Cons: More complex
        /// </summary>
        private int BulkInsertWithPreparedStatementAndTransaction(List<VidsSQL> vids)
        {
            int insertedCount = 0;
            int batchSize = batchLimit;

            try
            {
                using (var connection = new NpgsqlConnection(connectionStr))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        // Prepare the statement
                        string query = "INSERT INTO \"Vids\" (filename, filesizebyte, duration, metadatetime, width, height) " +
                                      "VALUES (@filename, @filesizebyte, @duration, @metadatetime, @width, @height)";

                        using (var command = new NpgsqlCommand(query, connection, transaction))
                        {
                            // Add parameters once
                            command.Parameters.Add("@filename", NpgsqlTypes.NpgsqlDbType.Varchar);
                            command.Parameters.Add("@filesizebyte", NpgsqlTypes.NpgsqlDbType.Bigint);
                            command.Parameters.Add("@duration", NpgsqlTypes.NpgsqlDbType.Integer);
                            command.Parameters.Add("@metadatetime", NpgsqlTypes.NpgsqlDbType.Timestamp);
                            command.Parameters.Add("@width", NpgsqlTypes.NpgsqlDbType.Integer);
                            command.Parameters.Add("@height", NpgsqlTypes.NpgsqlDbType.Integer);

                            // Prepare the command
                            command.Prepare();

                            // Execute in batches
                            for (int i = 0; i < vids.Count; i += batchSize)
                            {
                                var batch = vids.Skip(i).Take(batchSize).ToList();

                                foreach (var vid in batch)
                                {
                                    command.Parameters["@filename"].Value = vid.filename;
                                    command.Parameters["@filesizebyte"].Value = DataGenHelper.ConvertBigIntegerToDbValue(vid.filesizebyte);
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
        /// Method 6: PostgreSQL COPY command
        /// Uses PostgreSQL's native COPY command to bulk load data from CSV
        /// Pros: Very fast, native PostgreSQL feature
        /// Cons: Requires CSV file generation
        /// </summary>
        private int BulkInsertUseCopyCommand(string filePath)
        {
            int insertedCount = 0;
            try
            {
                using (var connection = new NpgsqlConnection(connectionStr))
                {
                    connection.Open();

                    // PostgreSQL COPY command
                    string copyCommand = $"COPY \"Vids\" (filename, filesizebyte, duration, metadatetime, width, height) FROM STDIN WITH (FORMAT csv, HEADER true, DELIMITER ':')";

                    using (var writer = connection.BeginTextImport(copyCommand))
                    {
                        // Read the CSV file and write to the COPY stream
                        using (var reader = new StreamReader(filePath))
                        {
                            string? line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                writer.WriteLine(line);
                                insertedCount++;
                            }
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Debug.Print($"Error during COPY command: {ex.Message}");
            }
            catch (Exception ex)
            {
                Debug.Print($"Error in BulkInsertUseCopyCommand: {ex.Message}");
            }
            insertedCount--; // removing the HEADER line from being counted
            return insertedCount;
        }

        /// <summary>
        /// Method 7: Npgsql Binary Import
        /// Uses Npgsql's binary import feature for fastest bulk insert
        /// Pros: Fastest option, direct binary transfer
        /// Cons: More complex setup
        /// </summary>
        private int BulkInsertUseBinaryImport(List<VidsSQL> vids)
        {
            int insertedCount = 0;
            try
            {
                using (var connection = new NpgsqlConnection(connectionStr))
                {
                    connection.Open();

                    // Start binary COPY
                    using (var writer = connection.BeginBinaryImport("COPY \"Vids\" (filename, filesizebyte, duration, metadatetime, width, height) FROM STDIN (FORMAT BINARY)"))
                    {
                        foreach (var vid in vids)
                        {
                            writer.StartRow();
                            
                            writer.Write(vid.filename);
                            
                            // Handle nullable BigInteger
                            var filesizeValue = DataGenHelper.ConvertBigIntegerToNullableLong(vid.filesizebyte);
                            if (filesizeValue.HasValue)
                                writer.Write(filesizeValue.Value, NpgsqlTypes.NpgsqlDbType.Bigint);
                            else
                                writer.WriteNull();
                            
                            // Handle nullable duration
                            if (vid.duration.HasValue)
                                writer.Write(vid.duration.Value, NpgsqlTypes.NpgsqlDbType.Integer);
                            else
                                writer.WriteNull();
                            
                            // Handle nullable metadatetime
                            if (vid.metadatetime.HasValue)
                                writer.Write(vid.metadatetime.Value, NpgsqlTypes.NpgsqlDbType.Timestamp);
                            else
                                writer.WriteNull();
                            
                            // Handle nullable width
                            if (vid.width.HasValue)
                                writer.Write(vid.width.Value, NpgsqlTypes.NpgsqlDbType.Integer);
                            else
                                writer.WriteNull();
                            
                            // Handle nullable height
                            if (vid.height.HasValue)
                                writer.Write(vid.height.Value, NpgsqlTypes.NpgsqlDbType.Integer);
                            else
                                writer.WriteNull();
                            
                            insertedCount++;
                        }

                        writer.Complete();
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Debug.Print($"Error during binary import: {ex.Message}");
            }
            catch (Exception ex)
            {
                Debug.Print($"Error in BulkInsertUseBinaryImport: {ex.Message}");
            }
            return insertedCount;
        }

        private int BulkInsertWithRepoDBInsertAll(List<RepoVidInsert> vids)
        {
            int insertedCount = 0;
            try
            {
                using (var connection = new NpgsqlConnection(connectionStr))
                {
                    connection.Open();

                    if (vids.Count > 0)
                    {
                        connection.InsertAll("Vids", vids, batchSize: repoDBBatchLimit);
                        insertedCount = vids.Count;
                        //vids.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                // Note: StackOverflowException is not catchable/recoverable in .NET; avoid relying on catching it.
                Debug.Print($"Error in RepoDB_InsertAll: {ex.Message}");
            }
            return insertedCount;
        }

        /// <summary>
        /// There is something wrong with PgPartner.BulkAdd()
        /// </summary>
        /// <param name="vids"></param>
        /// <returns></returns>
        private async Task<int> BulkAddWithPgPartner(List<RepoVidInsert> vids)
        {
            int insertedCount = vids.Count;
            try
            {
                using (var connection = new NpgsqlConnection(connectionStr))
                {
                    connection.Open();

                    // IMPORTANT ERROR: This does not work, PgPartner.BulkAdd does not return to context.
                    //   even though the data are inserted, the program is frozen.
                    //   There might an internal issue where BulkAdd does not return to Context so its in a deadlock state
                    //
                    //connection.BulkAdd(vids, (mapper, v) =>
                    //    {
                    //        // mapper.Map("id", v.id, NpgsqlDbType.Uuid);
                    //        mapper.Map("filename", v.filename, NpgsqlDbType.Varchar);
                    //        // mapper.Map("filesizebyte", v.filesizebyte, NpgsqlDbType.Bigint); // cannot remap C#.BigInteger to postgres.Bigint
                    //        // FIX: Convert BigInteger? into a DB-friendly boxed Int64 or DBNull.
                    //        // ALT FIX: using RepoVidInsert class instead of VidsSQL class
                    //        mapper.Map("filesizebyte", v.filesizebyte, NpgsqlDbType.Bigint);
                    //        mapper.Map("duration", v.duration, NpgsqlDbType.Integer);
                    //        mapper.Map("metadatetime", v.metadatetime, NpgsqlDbType.Timestamp);
                    //        mapper.Map("width", v.width, NpgsqlDbType.Integer);
                    //        mapper.Map("height", v.height, NpgsqlDbType.Integer);
                    //    },
                    //    "public",
                    //    "\"Vids\"");

                    // IMPORTANT: This works, it continues properly by wrapping in Task threads.
                    //   But it creates race condition on final step in test on grabbing number of records in the Postgres DB.
                    //   The records are up to date because its counting before the Task is done.
                    //
                    await Task.Run(() =>
                    {
                        connection.BulkAdd(vids, (mapper, v) =>
                            {
                                // mapper.Map("id", v.id, NpgsqlDbType.Uuid);
                                mapper.Map("filename", v.filename, NpgsqlDbType.Varchar);

                                // mapper.Map("filesizebyte", v.filesizebyte, NpgsqlDbType.Bigint); // cannot remap C#.BigInteger to postgres.Bigint
                                // FIX: Convert BigInteger? into a DB-friendly boxed Int64 or DBNull.
                                // DataGenHelper.ConvertBigIntegerToDbValue returns either a boxed long or DBNull.Value.
                                mapper.Map("filesizebyte", DataGenHelper.ConvertBigIntegerToDbValue(v.filesizebyte), NpgsqlDbType.Bigint);
                                mapper.Map("duration", v.duration, NpgsqlDbType.Integer);
                                mapper.Map("metadatetime", v.metadatetime, NpgsqlDbType.Timestamp);
                                mapper.Map("width", v.width, NpgsqlDbType.Integer);
                                mapper.Map("height", v.height, NpgsqlDbType.Integer);
                            },
                            "public",
                            "\"Vids\"");
                        Debug.Print("PgPartner.BulkAdd() Task Run() complete");
                    });

                    // IMPORTANT: This also works.
                    //
                    //connection.BulkAddAsync(vids, (mapper, v) =>
                    //    {
                    //        // mapper.Map("id", v.id, NpgsqlDbType.Uuid);
                    //        mapper.Map("filename", v.filename, NpgsqlDbType.Varchar);

                    //        // mapper.Map("filesizebyte", v.filesizebyte, NpgsqlDbType.Bigint); // cannot remap C#.BigInteger to postgres.Bigint
                    //        // FIX: Convert BigInteger? into a DB-friendly boxed Int64 or DBNull.
                    //        // DataGenHelper.ConvertBigIntegerToDbValue returns either a boxed long or DBNull.Value.
                    //        mapper.Map("filesizebyte", DataGenHelper.ConvertBigIntegerToDbValue(v.filesizebyte), NpgsqlDbType.Bigint);
                    //        mapper.Map("duration", v.duration, NpgsqlDbType.Integer);
                    //        mapper.Map("metadatetime", v.metadatetime, NpgsqlDbType.Timestamp);
                    //        mapper.Map("width", v.width, NpgsqlDbType.Integer);
                    //        mapper.Map("height", v.height, NpgsqlDbType.Integer);
                    //    },
                    //    "public",
                    //    "\"Vids\"");
                    // .GetAwaiter().GetResult(); // this line doesn't work either

                    Debug.Print("Inserted with BulkAddWithPgPartner complete");
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"Error in BulkAddWithPgPartner: {ex.Message}");
            }
            return insertedCount;
        }

        /// <summary>
        /// Get record count from Vids table
        /// </summary>
        public int GetVidsCount()
        {
            try
            {
                return postgresBase.WithSqlCommand(command =>
                {
                    return Convert.ToInt32(command.ExecuteScalar());
                }, "SELECT COUNT(*) FROM \"Vids\"");
            }
            catch (Exception ex)
            {
                Debug.Print($"Error in GetVidsCount: {ex.Message}");
                return -1;
            }
        }

        /// <summary>
        /// Cleanup method to clear the Vids table
        /// </summary>
        public void CleanupVidsTable()
        {
            try
            {
                postgresBase.WithConnection(connection =>
                {
                    using (var command = new NpgsqlCommand("DELETE FROM \"Vids\"", connection))
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
        /// Reset the Vids table using TRUNCATE for faster, non-logged removal.
        /// Set cascade = true to include dependent tables (TRUNCATE ... CASCADE).
        /// 
        /// TRUNCATE TABLE \"Vids\";
        /// TRUNCATE TABLE \"Vids\" CASCADE;
        /// TRUNCATE TABLE \"Vids\" RESTART IDENTITY;
        /// 
        /// </summary>
        public void ResetVidsTable()
        {
            try
            {
                postgresBase.WithConnection(connection =>
                {
                    string sql = "TRUNCATE TABLE \"Vids\" RESTART IDENTITY;";
                    using (var command = new NpgsqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                        Debug.Print("TRUNCATE TABLE \"Vids\" RESTART IDENTITY; done");
                        return true;
                    }
                });
            }
            catch (Exception ex)
            {
                Debug.Print($"Error in Postgres ResetVidsTable: {ex.Message}");
            }
        }
    }
}
