using MethodTimer;
using System.Diagnostics;
using System.IO;
using System.IO.Enumeration;
using System.Text.RegularExpressions;

namespace CSharpAppPlayground.FilesFolders
{
    public class CountStorage
    {
        public void CountFolderSize(string folderPath, out int sizeInBytes)
        {
            // Initialize output parameters
            sizeInBytes = 0;

            // Check for null or empty folder path
            if (string.IsNullOrWhiteSpace(folderPath))
            {
                Debug.Print("Error: Folder path is null, empty, or whitespace.");
                return;
            }
            else if (!Directory.Exists(folderPath))
            {
                Debug.Print($"Error: Folder path does not exist: {folderPath}");
                return;
            }

            // TODO: let's see which is faster, MethodA or MethodB
            // Task.Run + CancelletionToken
        }

        protected long CountMethodA(string folderPath)
        {
            long bytes = 0;
            try
            {
                string[] allFiles = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories);
                // int fileCount = allFiles.Length;
                bytes = allFiles.Sum(file => new FileInfo(file).Length);

                // Alt: Sum the sizes of all files (about the same performance)
                //foreach (string file in allFiles)
                //{
                //    FileInfo fileInfo = new FileInfo(file);
                //    bytes += fileInfo.Length;
                //}
            }
            catch (UnauthorizedAccessException ex)
            {
                Debug.Print($"Error: Unauthorized access - {ex.Message}");
            }
            catch (PathTooLongException ex)
            {
                Debug.Print($"Error: Path too long - {ex.Message}");
            }
            catch (Exception ex)
            {
                Debug.Print($"Error: An unexpected error occurred - {ex.Message}");
            }
            return bytes;
        }

        // A: using System.IO + loop + recursion
        //
        // <TODO>
        //

        // B: using cmd.exe + dir /s
        // dir /s "E:\Downloads" | find "File(s)"
        // dir /s /-c "E:\Downloads"
        /*
            cmd.exe /c = run command and exit
            /-c disables the thousands separator, so 1,024 becomes 1024.
            /s displays files in specified directory and all subdirectories.
        */
        protected long CountMethodB(string folderPath)
        {
            long bytes = 0;
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
					Arguments = $"/c dir /a /s /-c \"{folderPath}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            // Debug.Print($"out:: {output}");
            process.WaitForExit();

            Match match = Regex.Match(output, @"Total Files Listed:\s+\d+\s+File\(s\)\s+([\d,]+)");
            bytes = match.Success ? long.Parse(match.Groups[1].Value) : 0;
            // Debug.Print($"What is total size? {bytes} bytes, {Environment.NewLine}{match}");
            return bytes;
        }

        // C: using Robocopy (Windows only)
        // robocopy "C:\Folder" "C:\Folder" /L /S /BYTES
        // robocopy "C:\Folder" "C:\Folder" /E /V /TS /FP /BYTES /LOG:detailed.log
        /*
            /L – Lists files that would be copied without actually copying them. Useful for testing what a command will do.
            /S – Copies all subdirectories except empty ones.
            /E – Copies all subdirectories, including empty ones.
            /V – Enables verbose output, showing skipped files and additional details in the log.
            /TS – Includes the source file’s timestamp in the output log.
            /FP – Includes the full path of each file in the log output.
        */
        protected long CountMethodC(string folderPath)
        {
            long bytes = 0;
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "robocopy",
                    Arguments = $"\"{folderPath}\" \"{folderPath}\" /L /S /BYTES",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            // Debug.Print($"out:: {output}");
            process.WaitForExit();

            Match match = Regex.Match(output, @"Bytes :\s+(\d+)\s+");
            bytes = match.Success ? long.Parse(match.Groups[1].Value) : 0;
            // Debug.Print($"What is total size? {bytes} bytes, {match}");
            return bytes;
        }

        public long CountMethodD(string folderPath)
        {
            long bytes = Directory.EnumerateFiles(folderPath, "*", SearchOption.AllDirectories).Sum(file => new FileInfo(file).Length);
            return bytes;
        }

        //public long CoundMethodD_Parallel(string folderPath)
        //{
        //    long bytes = Directory.EnumerateFiles(folderPath, "*", SearchOption.AllDirectories)
        //                           .AsParallel()
        //                           .Sum(file => new FileInfo(file).Length);
        //    return bytes;
        //}

        public long CountMethodE(string folderPath)
        {
            long bytes = 0;
            return bytes;
        }

        public long CountMethodF(string folderPath)
        {
			var options = new EnumerationOptions
			{
				RecurseSubdirectories = true,
				AttributesToSkip = 0, // include hidden and system
				ReturnSpecialDirectories = false,
				IgnoreInaccessible = true
			};
			long bytes = new FileSystemEnumerable<long>(folderPath, (ref FileSystemEntry entry) => entry.Length, options).Sum();
			return bytes;
        }

        [Time("CountMethodA: {folderPath}")]
        public void Test_CountMethodA(string folderPath)
        {
            long bytes = CountMethodA(folderPath);
            Debug.Print($"CountMethodA: {bytes} bytes");
        }

        [Time("CountMethodB: {folderPath}")]
        public void Test_CountMethodB(string folderPath)
        {
            long bytes = CountMethodB(folderPath);
            Debug.Print($"CountMethodB: {bytes} bytes");
        }

        [Time("CountMethodC: {folderPath}")]
        public void Test_CountMethodC(string folderPath)
        {
            long bytes = CountMethodC(folderPath);
            Debug.Print($"CountMethodC: {bytes} bytes");
        }

        [Time("CountMethodD: {folderPath}")]
        public void Test_CountMethodD(string folderPath)
        {
            long bytes = CountMethodD(folderPath);
            Debug.Print($"CountMethodD: {bytes} bytes");
        }

        [Time("CountMethodE: {folderPath}")]
        public void Test_CountMethodE(string folderPath)
        {
            long bytes = CountMethodE(folderPath);
            Debug.Print($"CountMethodE: {bytes} bytes");
        }

        [Time("CountMethodF: {folderPath}")]
        public void Test_CountMethodF(string folderPath)
        {
            long bytes = CountMethodF(folderPath);
            Debug.Print($"CountMethodF: {bytes} bytes");
        }

        public void TestPerformance(string folderPath)
        {
            Test_CountMethodA(folderPath);
            Test_CountMethodB(folderPath);
            Test_CountMethodC(folderPath);
            Test_CountMethodD(folderPath);
            Test_CountMethodE(folderPath);
            Test_CountMethodF(folderPath);
        }
    }
}
