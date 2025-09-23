using CSharpAppPlayground.DBClasses.MongoDBExamples.Collections;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace CSharpAppPlayground.DBClasses.MongoDBExamples
{
    public class _connMongoDBStandard
    {
        private static bool connected = false;
        private static string connectionStr = string.Empty;

        public static readonly string dbName = "testdb";

        private static IServiceCollection services { get; } = new ServiceCollection();
        private static ServiceProvider? provider;

        public _connMongoDBStandard()
        {
            // Connection string to connect to MongoDB
            connectionStr = System.Configuration.ConfigurationManager.ConnectionStrings["MongoDBConnection"].ConnectionString;
        }

        public ServiceProvider GetServiceProvider()
        {
            if (!connected)
                Setup();
            return provider!;
        }

        private static void RegisterConvention()
        {
            // Register a convention to use camelCase for element names in MongoDB
            ConventionPack conventionPack = new ConventionPack
            {
                new CamelCaseElementNameConvention()
            };
            ConventionRegistry.Register(
                "CamelCase",
                conventionPack,
                _ => true // Applies to all types
            );

            //
            // other conventions examples: Enum to string
            //
            //ConventionRegistry.Register("EnumStringConvention", 
            //    new ConventionPack {
            //    new EnumRepresentationConvention(BsonType.String)
            //}, _ => true);
        }

        private static void Setup()
        {
            RegisterConvention(); // this needs to be called before any MongoDB setup to make sure rules are applied

            services.AddSingleton<IMongoClient>(sp =>
            {
                MongoClientSettings settings = MongoClientSettings.FromConnectionString(connectionStr);
                //settings.ConnectTimeout = TimeSpan.FromSeconds(2);
                //settings.ServerSelectionTimeout = TimeSpan.FromSeconds(2);
                
                return new MongoClient(settings);
            });

            services.AddSingleton<IMongoDatabase>(sp =>
            {
                IMongoClient client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase(dbName);
            });

            services.AddSingleton<CollectionsManager>();

            // this creates the service provider, with all resources loaded
            provider = services.BuildServiceProvider(); 

            connected = true;
        }
    }
}
