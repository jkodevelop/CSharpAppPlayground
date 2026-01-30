using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Reflection;

namespace CSharpAppPlayground.Loggers;

public static class MethodTimeLogger
{
    public static void Log(MethodBase methodBase, TimeSpan timeSpan, string message)
    {
        //string msg = $"[benchmark] Method '{methodBase.Name}' executed in {timeSpan} ms, {message}";
        string msg = $",[benchmark],{message},Method:{methodBase.Name},{timeSpan} ms";
        Debug.Print(msg);
        GlobalLogger.Instance.LogInformation(msg);
    }

    public static void Log(MethodBase methodBase, long milliseconds, string message)
    {
        //string msg = $"[benchmark] Method '{methodBase.Name}' completed in {milliseconds} ms, {message}";
        string msg = $",[benchmark],{message},Method:{methodBase.Name},{milliseconds} ms";
        Debug.Print(msg);
        GlobalLogger.Instance.LogInformation(msg);
    }
}

