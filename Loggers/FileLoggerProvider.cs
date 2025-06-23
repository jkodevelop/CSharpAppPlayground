using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

public class FileLoggerProvider : ILoggerProvider
{
    private readonly FileLoggerOptions _options;

    public FileLoggerProvider(IOptions<FileLoggerOptions> options)
    {
        _options = options.Value;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new FileLogger(categoryName, _options);
    }

    public void Dispose()
    {
    }
}