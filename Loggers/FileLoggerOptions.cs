using Microsoft.Extensions.Logging;
public class FileLoggerOptions
{
    public string? FilePath { get; set; }
    public LogLevel LogLevel { get; set; } = LogLevel.Information;
}