using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CSharpAppPlayground.Loggers
{
    public class DebugLogger : ILogger
    {
        private readonly string _name;
        private readonly LogLevel _minLogLevel;

        public DebugLogger(string name, LogLevel minLogLevel = LogLevel.Information)
        {
            _name = name;
            _minLogLevel = minLogLevel;
        }

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= _minLogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            Debug.Print($"{DateTime.Now} [{logLevel}] {_name}: {formatter(state, exception)}");

            if (exception != null)
            {
                Debug.Print($"Exception:{exception}");
            }
        }
    }
}
