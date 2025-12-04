using Npgsql;
using NpgsqlTypes;
using System.Configuration;
using System.Diagnostics;

// TO DOCUMENT
// problem ENUM POSTGRESQL EXAMPLE: case sensitive problems with enum values
// example:
// 1. postgres enum defined as 'Todo', 'Pending', 'Done' - must match exactly in C#
// 2. .net ActionStatus enum defined as Todo, Pending, Done
// PROBLEM: sending enum:Pending -> postgres expects 'Pending' but Npgsql sends 'pending' (lowercase) -> error
// WHY? Npgsql driver, by default, uses a snake-case name translator that converts C# PascalCase names to lower-case for PostgreSQL
// SOLUTION a:
//   use [PgName("ExactName")] attribute on enum values to specify exact mapping
// SOLUTION b: call this before dataSourceBuilder.MapEnum<T>
//   dataSourceBuilder.DefaultNameTranslator = new Npgsql.NameTranslation.NpgsqlIdentityNameTranslator();


namespace CSharpAppPlayground.DBClasses.PostgresExamples
{
    // Example enum to represent status values - ensure this matches the enum type defined in your PostgreSQL database
    public enum ActionStatus
    {
        // [PgName("Todo")] // example of solution (a) the postgres driver Npsql auto lowercase conversion, this forces exact value passing
        Todo,
        //[PgName("Pending")]
        Pending,
        //[PgName("Done")]
        Done,
    }

    public class ExampleEnum
    {
        // tablename: "ExampleEnum"
        public int? id { get; set; }
        public string title { get; set; } = "";
        public ActionStatus currentstatus { get; set; } // Property uses the C# enum type
    }

    public class PostgresEnumExample
    {
        private string connectionStr = string.Empty;
        private static NpgsqlDataSource dataSource;

        public PostgresEnumExample()
        {
            connectionStr = ConfigurationManager.ConnectionStrings["PostgreSqlConnection"].ConnectionString;

            // This is replaced, with GlobalConfiguration.Setup().UseMySql().UsePostgreSql(); // to support both DBs
            // GlobalConfiguration.Setup().UsePostgreSql(); // configuring "RepoDb", add more supported API to postgres

            // 1.Create an NpgsqlDataSourceBuilder instance with the connection string
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionStr);

            // 2a. Set the name translator to avoid case conversion issues, just send name as-is, no case change
            // note by default Npgsql uses NpgsqlSnakeCaseNameTranslator() which converts PascalCase to snake_case (lowercase with underscores)
            // MUST BE CALLED BEFORE MapEnum<T>()
            // example of solution (b)
            dataSourceBuilder.DefaultNameTranslator = new Npgsql.NameTranslation.NpgsqlNullNameTranslator();

            // 2. (Optional) Configure type mappings or other options
            // For example, to map a custom enum or composite type:
            // Map the PostgreSQL enum type name without extra quotes
            // dataSourceBuilder.MapComposite<MyCompositeType>();
            dataSourceBuilder.MapEnum<ActionStatus>("ActionStatus"); // "ActionStatus" is the name of ENUM type defined in postgres

            // 3. Build the NpgsqlDataSource
            dataSource = dataSourceBuilder.Build();
        }

        public int Insert(ExampleEnum obj)
        {
            try
            {
                string query = "";
                using (NpgsqlCommand command = dataSource.CreateCommand(query)) { 
                    // Prepare SQL query to insert the row and get the new id back
                    query = "INSERT INTO \"ExampleEnum\" (title, currentstatus) VALUES (@title, @currentstatus) RETURNING id;";
                    command.CommandText = query;

                    // Add parameters
                    command.Parameters.AddWithValue("@title", obj.title);
                    command.Parameters.AddWithValue("@currentstatus", obj.currentstatus);

                    // For enum parameters explicitly set NpgsqlDbType and DataTypeName so Npgsql knows the DB enum type
                    //var statusParam = new NpgsqlParameter("@currentstatus", obj.currentstatus)
                    //{
                    //    DataTypeName = "ActionStatus"
                    //};
                    //command.Parameters.Add(statusParam);

                    var result = command.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"InsertSqlDBObject(): {ex.Message}");
            }
            return -1;
        }

        public List<ExampleEnum> GetByCurrentStatus(ActionStatus status)
        {
            var objects = new List<ExampleEnum>();

            try
            {
                string query = "SELECT id, title, currentstatus FROM \"ExampleEnum\" WHERE currentstatus = @currentstatus;";
                using (NpgsqlCommand command = dataSource.CreateCommand(query))
                {
                    command.CommandText = query;
                    // Create enum parameter with explicit type, alt example of adding Parameter and Value using ENUM
                    var statusParam = new NpgsqlParameter("@currentstatus", status)
                    {
                        DataTypeName = "ActionStatus"
                    };
                    command.Parameters.Add(statusParam);
                    
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            objects.Add(new ExampleEnum
                            {
                                id = reader.GetInt32(0),
                                title = reader.GetString(1),
                                currentstatus = (ActionStatus)reader.GetValue(2) // Npgsql will return the enum value
                                
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"GetByCurrentStatus({status}): Error retrieving ExampleEnum by status: {ex.Message}");
            }
            return objects;
        }

        public void DemoInsertAndSelect()
        {
            var insertObj = new ExampleEnum
            {
                title = "Sample Task",
                currentstatus = ActionStatus.Pending
            };
            int resId = Insert(insertObj);
            Debug.Print($"Inserted ExampleEnum with ID: {resId}");

            List<ExampleEnum> pendingItems = GetByCurrentStatus(ActionStatus.Pending);
            Debug.Print($"Retrieved {pendingItems.Count} ExampleEnum items with status 'Pending'");
        }
    }
}

/* Example using Entity Framework Core with Npgsql and PostgreSQL ENUM type

using Microsoft.EntityFrameworkCore;

var newItem = new WorkItem { Title = "Review code", CurrentStatus = Status.Pending };
context.WorkItems.Add(newItem);
context.SaveChanges(); // Saves successfully using the native ENUM type

// Query items: Npgsql reads 'Done' from the DB and maps it to Status.Done in C#
var doneItems = context.WorkItems.Where(w => w.CurrentStatus == Status.Done).ToList();

*/

// [TO DOCUMENT]
/*

var dataSourceBuilder = new NpgsqlDataSourceBuilder("Host=localhost;Database=mydb;Username=myuser;Password=mypass");
dataSourceBuilder.MapEnum<Status>();
var dataSource = dataSourceBuilder.Build();

builder.HasPostgresEnum<ActionStatus>(name: "status"); // This tells Npgsql to create the 'status' enum type in PostgreSQL via migrations

*/