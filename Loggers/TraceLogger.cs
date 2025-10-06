using System.Diagnostics;

namespace CSharpAppPlayground.Loggers
{
    /// <summary>
    /// This is an example of a "Trace Logger".
    /// this is older way of logging in .NET, 
    /// the current 2025+ recommended way is to use Microsoft.Extensions.Logging
    /// ILogger interface and related abstractions.
    /// 
    /// This is still viable for quick and dirty logging, for smaller projects
    /// 
    /// Notes About this implementation:
    /// - This implementation creates or overwrites the log file each time the TraceLogger is instantiated
    /// - Trace is thread-safe, so no additional synchronization is needed for writing log entries
    /// - Trace has a Defualt option DefaultTraceListener that writes to the Output
    /// - Adding TextWriterTraceListener to write to a file as well
    /// 
    /// </summary>

    public class TraceLogger
    {
        public TraceLogger(bool useAppSettings = true, string? logPath = "./logs/tracelogs.txt")
        {
            try
            {
                if (useAppSettings)
                {
                    string folderPath = Program.Configuration["Logging:LogFileSettings:Folder"] ?? "";
                    string fileName = Program.Configuration["Logging:LogFileSettings:TraceLogFilename"] ?? "";
                    if (!string.IsNullOrWhiteSpace(folderPath) && !string.IsNullOrWhiteSpace(fileName))
                    {
                        logPath = Path.Combine(folderPath, fileName);
                    }
                }

                DirectoryInfo logsFolder = Directory.CreateDirectory(Path.GetDirectoryName(logPath)); // example relative path: "./logs"
                Stream myFile = File.Create(logPath); // example relative path: "./logs/trace.log"
                                                                     // Create a new text writer using the output stream, and add it to the trace listeners. 
                TextWriterTraceListener myTextListener = new TextWriterTraceListener(myFile);
                Trace.Listeners.Add(myTextListener);
                Trace.AutoFlush = true;
            }
            catch (Exception ex)
            {
                Debug.Print($"Failed to create log file: {ex.Message}");
                throw new Exception("Failed to create log file, Path or Log Name issues");
            }
        }

        public void Error(string message, string module)
        {
            WriteEntry(message, "[error]", module);
        }

        public void Error(Exception ex, string module)
        {
            WriteEntry(ex.Message, "[error;ex]", module);
        }

        public void Warning(string message, string module)
        {
            WriteEntry(message, "[warning]", module);
        }

        public void Info(string message, string module)
        {
            WriteEntry(message, "[info]", module);
        }

        private string FormattedMessage(string message, string type, string module)
        {
            Thread t = Thread.CurrentThread;
            return string.Format("{0} {1}-{2} {3}::{4}",
                                  DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                  t.ManagedThreadId,
                                  type,
                                  module,
                                  message);
        }

        private void WriteEntry(string message, string type, string module)
        {
            Trace.WriteLine(FormattedMessage(message, type, module));
            // Trace.Flush();
        }

        public void trace(string line)
        {
            Trace.WriteLine(line);
            // Trace.Flush();
        }
    }
}
