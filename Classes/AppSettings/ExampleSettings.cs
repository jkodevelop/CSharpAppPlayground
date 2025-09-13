namespace CSharpAppPlayground.Classes.AppSettings
{
    // this is used to show a strongly typed example of appsettings.json
    // this maps to the "ExampleSettings" section in appsettings.json
    //
    //"ExampleSettings": {
    //  "ExampleString": "exampleValue",
    //  "ExampleInt": 42
    //}

    public class ExampleSettings
    {
        public string ExampleString { get; set; } = "defaultString";
        public int ExampleInt { get; set; } = 0;
    }
}
