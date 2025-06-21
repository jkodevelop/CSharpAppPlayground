using System.Diagnostics;
using System.Reflection;

namespace CSharpAppPlayground.Loggers;

public static class MethodTimeLogger
{
    public static void Log(MethodBase methodBase, TimeSpan timeSpan, string message)
    {
        Debug.Print($"Method '{methodBase.Name}' executed in {timeSpan} ms, {message}");
    }

    public static void Log(MethodBase methodBase, long milliseconds, string message)
    {
        Debug.Print($"Method '{methodBase.Name}' completed in {milliseconds} ms, {message}");
    }
}

