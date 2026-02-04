using CSharpAppPlayground.Classes.DataGen.Generators;
using CSharpAppPlayground.DBClasses.Data;
using CSharpAppPlayground.DBClasses.Data.SQLbenchmark;
using CSharpAppPlayground.DBClasses.MariaDBExamples;
using CSharpAppPlayground.DBClasses.MysqlBenchmark;
using MethodTimer;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Diagnostics;

namespace CSharpAppPlayground.DBClasses.MariaDBBenchmark
{
    public class MariaDBBasicBenchmarks: MysqlBasicBenchmarks
    {
        private string connectionStr;
        private MariaDBBase mariaBase;
        private DataGenHelper dataGenHelper;

        protected int batchLimit = 5000;
        protected int overloadLimit = 50000;

        private string csvFilePath = @".\testdata\mysql_vids_bulk_insert.csv";

        public MariaDBBasicBenchmarks()
        {
            connectionStr = ConfigurationManager.ConnectionStrings["MariaDBConnection"].ConnectionString; ;
            mariaBase = new MariaDBBase();
            dataGenHelper = new DataGenHelper();
        }

        public void ImportCSV(string csvFilePath)
        {
            BulkInsertUseInlineFile(csvFilePath);
        }

        public new void FastestCompareBenchmark(int dataSetSize)
        {
            // only test with fastest APIs for big data, Note: if its less then 10000 records the benchmark is kinda pointless
            GenerateVidsCSV gen = new GenerateVidsCSV();
            List<VidsCSV> testData = gen.GenerateData(dataSetSize);
            if (dataGenHelper.GenCSVfileWithData(testData, csvFilePath))
                Test_BulkInsertUseInlineFile(csvFilePath);
            else
                Debug.Print("Failed to generate CSV file for bulk insert, cannot run Test_BulkInsertUseInlineFile");
        }

        public new void RunBulkInsertBenchmark(int dataSetSize)
        {
            Debug.Print("=== MariaDB Bulk Insert Examples with VidsSQL ===");

            // Generate test data
            GenerateVidsSQL generator = new GenerateVidsSQL();
            List<VidsSQL> testData = generator.GenerateData(dataSetSize);
            Debug.Print($"Generated {testData.Count} test records\n");

            //Example 1: Single insert loop(baseline)
            Test_InsertSimpleLoop(testData);

            // Example 2: Multi-value INSERT statement
            Test_InsertMultiValue(testData);

            // Example 3: Transaction with batched inserts
            Test_InsertWithTransaction(testData);

            // Example 4: Prepared statement with batching
            Test_InsertWithPreparedStatement(testData);

            // Example 5: Prepared statement with batching and transaction
            Test_BulkInsertWithPreparedStatementAndTransaction(testData);

            // EXAMPLE 7: Bulk Insert using Inline File Command
            GenerateVidsCSV gen = new GenerateVidsCSV();
            List<VidsCSV> csvData = gen.GenerateData(dataSetSize);
            if (dataGenHelper.GenCSVfileWithData(csvData, csvFilePath))
                Test_BulkInsertUseInlineFile(csvFilePath);
            else
                Debug.Print("Failed to generate CSV file for bulk insert, cannot run Test_BulkInsertUseInlineFile");

            Debug.Print("\n=== Benchmark Complete ===");
        }

        #region BENCHMARK METHODS
        [Time("MariaDB")]
        protected void Test_BulkInsertUseInlineFile(string filePath)
        {
            Debug.Print("\n--- Method 6: Inline file command Example ---");
            int insertedCount = BulkInsertUseInlineFile(filePath);
            Debug.Print($"Inserted {insertedCount} records using CSV operation\n");
        }
        #endregion BENCHMARK METHODS

        private int BulkInsertUseInlineFile(string filePath)
        {
            if(File.Exists(filePath) == false)
            {
                throw new FileNotFoundException($"file not found at path: {filePath}");
            }
            int insertedCount = 0;
            try
            {
                using (var connection = new MySqlConnection(connectionStr))
                {
                    // LOAD DATA LOCAL INFILE ' %PATH TO FILE% ' INTO TABLE vids FIELDS TERMINATED BY ':' OPTIONALLY ENCLOSED BY '"' LINES TERMINATED BY '\n' IGNORE 1 LINES (duration, filename, filesizebyte, height, id, metadatetime, width);
                    // NOTE: If file has a header row use IGNORE 1 LINES
                    connection.Open();
                    string sql = $@"LOAD DATA LOCAL INFILE '{filePath.Replace("\\", "\\\\")}'
                            INTO TABLE Vids
                            FIELDS TERMINATED BY ':'
                            OPTIONALLY ENCLOSED BY '""'
                            LINES TERMINATED BY '\n'
                            IGNORE 1 LINES
                            (duration, filename, filesizebyte, height, id, metadatetime, width);"; 
                    var command = new MySqlCommand(sql, connection);
                    insertedCount = command.ExecuteNonQuery();
                    Debug.Print($"{insertedCount} rows were inserted into the 'Vids' table.");
                }
            }
            catch (MySqlException ex)
            {
                Debug.Print($"Error during bulk insert: {ex.Message}");
            }
            catch (Exception ex)
            {
                Debug.Print($"Error in BulkInsertUseCSVOperation: {ex.Message}");
            }
            return insertedCount;
        }

        public new void ResetVidsTable()
        {
            try
            {
                mariaBase.WithConnection(connection =>
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

        public new int GetVidsCount()
        {
            try
            {
                return mariaBase.WithSqlCommand(command =>
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
    }
}
