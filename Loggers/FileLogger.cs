using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;

public class FileLogger : ILogger
{
    private readonly string _name;
    private readonly string _filePath;
    private readonly LogLevel _minLevel;

    public FileLogger(string name, FileLoggerOptions options)
    {
        _name = name;
        _filePath = options.FilePath ?? "log.txt";
        _minLevel = options.LogLevel;
    }

    public IDisposable? BeginScope<TState>(TState state) => null;

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel >= _minLevel;
    }

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        string message = formatter(state, exception);
        string logEntry = $"[{DateTime.Now}] [{logLevel}] [{_name}] {message}{Environment.NewLine}";

        try
        {
            File.AppendAllText(_filePath, logEntry);
        }
        catch (Exception ex)
        {
            // Handle file writing errors gracefully.  Consider logging to a fallback mechanism.
            Debug.Print($"Error writing to log file: {ex.Message}");
        }
    }
}