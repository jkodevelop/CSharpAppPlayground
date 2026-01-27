using AngleSharp.Dom;
using CSharpAppPlayground.DBClasses.Data;
using CSharpAppPlayground.DBClasses.MariaDBExamples;
using MethodTimer;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Diagnostics;

namespace CSharpAppPlayground.DBClasses.MariaDBBenchmark
{
    public class MariaDBBasicBenchmarks
    {
        private string connectionStr;
        private MariaDBBase mariaBase;
        private DataGenHelper dataGenHelper;

        private int batchLimit = 5000;
        private int overloadLimit = 50000;

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

        #region BENCHMARK METHODS
        [Time("BulkInsertUseCSVOperation:")]
        protected void Test_BulkInsertUseInlineFile(string filePath)
        {
            Debug.Print("\n--- Method ?: Inline file command Example ---");
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

        public void ResetVidsTable()
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
    }
}
