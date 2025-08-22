using System.Diagnostics;

namespace CSharpAppPlayground.DIExample.medianB
{
    public class DBDataSource: IDataSource
    {
        /// <summary>
        /// Faking it for example purposes
        /// </summary>
        //private readonly string _connectionString;
        //public DBDataSource(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}
        public string[] ReadData()
        {
            Debug.Print("DBDataSource.ReadData called.");
            // Simulate reading from a database
            return new[]
            {
                "Name,Value",
                "Z,10",
                "Y,20",
                "X,30",
                "W,40"
            };
            // In a real implementation, you would connect to the database and retrieve data here.
        }
    }
}
