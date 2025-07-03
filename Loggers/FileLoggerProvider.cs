using Microsoft.Extensions.Logging;

public class FileLoggerProvider : ILoggerProvider
{
    private readonly FileLoggerOptions _options;

    public FileLoggerProvider(FileLoggerOptions options)
    {
        _options = options;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new FileLogger(categoryName, _options);
    }

    public void Dispose()
    {
        // Nothing to dispose
    }
}