namespace CSharpAppPlayground.Classes.AppSettings
{
    public class LogSettings
    {
        public LogLevelSettings LogLevel { get; set; }
    }

    // this maps to the subsection "Logging:LogLevel" in appsettings.json
    public class LogLevelSettings
    {
        public string Default { get; set; } = "Information";
        public string Microsoft { get; set; } = "Warning";
        public string CritOnly { get; set; } = "Critical";
    }

    public class LoggingSettingsMap
    {
        public Dictionary<string, string> LogLevel { get; set; }
    }
}
