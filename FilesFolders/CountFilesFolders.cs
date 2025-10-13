using MethodTimer;
using System.Diagnostics;
using System.IO.Enumeration;
using System.Text.RegularExpressions;

namespace CSharpAppPlayground.FilesFolders
{
    public class CountFilesFolders
    {
        public void CountFilesAndFolders(string folderPath, out int fileCount, out int folderCount)
        {
            // Initialize output parameters
            fileCount = 0;
            folderCount = 0;
            
            // Check for null or empty folder path
            if (string.IsNullOrWhiteSpace(folderPath))
            {
                Debug.Print("Error: Folder path is null, empty, or whitespace.");
                return;
            }
            if(!Directory.Exists(folderPath))
            {
                Debug.Print($"Error: Folder path does not exist: {folderPath}");
                return;
            }

            // TODO: let's see which is faster, MethodA or MethodB
        }

        // A: using System.IO + loop + recursion
        protected void R_CountMethodA(string folderPath, out int fileCount, out int folderCount)
        {
            fileCount = 0;
            folderCount = 0;
            try
            {
                // Use System.IO to get files and directories
                // Count files in current directory
                var files = Directory.GetFiles(folderPath);
                if (files != null)
                {
                    fileCount += files.Length;
                }
                
                // Count directories in current directory
                var subDirs = Directory.GetDirectories(folderPath);
                if (subDirs != null)
                {
                    folderCount += subDirs.Length;

                    // Recursively count files and folders in subdirectories
                    foreach (var dir in subDirs)
                    {
                        if (!string.IsNullOrWhiteSpace(dir))
                        {
                            R_CountMethodA(dir, out int subFileCount, out int subFolderCount);
                            fileCount += subFileCount;
                            folderCount += subFolderCount;
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Debug.Print($"Access denied to folder: {folderPath}. Exception: {ex.Message}");
            }
            catch (Exception ex)
            {
                Debug.Print($"An error occurred while accessing folder: {folderPath}. Exception: {ex.Message}");
            }
            //finally 
            //{ 
            //    Debug.Print($"CountMethodA({folderPath}): file={fileCount}, folder={folderCount}");
            //}
        }

        // B: using cmd.exe + dir /s
        // dir /s "E:\Downloads" | find "File(s)"
        // dir /s /-c "E:\Downloads"
        /*
            cmd.exe /c = run command and exit
            /-c disables the thousands separator, so 1,024 becomes 1024.
            /s displays files in specified directory and all subdirectories.
        */
        protected void CountMethodB(string folderPath, out int fileCount, out int folderCount)
        {
            fileCount = 0;
            folderCount = 0;
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c dir /s /-c \"{folderPath}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            Debug.Print($"out:: {output}");
            process.WaitForExit();

            Match match = Regex.Match(output, @"Total Files Listed:\s+\d+\s+File\(s\)\s+([\d,]+)");
            long totalSize = match.Success ? long.Parse(match.Groups[1].Value) : 0;
            Debug.Print($"What is total size? {totalSize} bytes, {match}");
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
        protected void CountMethodC(string folderPath, out int fileCount, out int folderCount)
        {
            fileCount = 0;
            folderCount = 0;
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
            Debug.Print($"out:: {output}");
            process.WaitForExit();

            Match match = Regex.Match(output, @"Bytes :\s+(\d+)\s+");
            long totalSize = match.Success ? long.Parse(match.Groups[1].Value) : 0;
            Debug.Print($"What is total size? {totalSize} bytes, {match}");
        }

        public void CountMethodD(string folderPath, out int fileCount, out int folderCount)
        {
            fileCount = 0;
            folderCount = 0;
            long bytes = Directory.EnumerateFiles(folderPath, "*", SearchOption.AllDirectories).Sum(file => new FileInfo(file).Length);
            Debug.Print($"{bytes} bytes");
        }

        public void CountMethodE(string folderPath, out int fileCount, out int folderCount)
        {
            fileCount = 0;
            folderCount = 0;
            var allFiles = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories);
            fileCount = allFiles.Length;
            long bytes = allFiles.Sum(file => new FileInfo(file).Length);
            Debug.Print($"{bytes} bytes");
        }

        public void CountMethodF(string folderPath, out int fileCount, out int folderCount)
        {
            fileCount = 0;
            folderCount = 0;
            var allDirs = Directory.GetDirectories(folderPath, "*", SearchOption.AllDirectories);
            folderCount = allDirs.Length;
            Debug.Print($"Total directories: {folderCount}");
        }

        public void CountMethodG(string folderPath, out int fileCount, out int folderCount)
        {
            fileCount = 0;
            folderCount = 0;
            var allDirs = Directory.EnumerateDirectories(folderPath, "*", SearchOption.AllDirectories);
            folderCount = allDirs.Count();
            Debug.Print($"Total directories: {folderCount}");
        }

        public void CountMethodH(string folderPath, out int fileCount, out int folderCount)
        {
            fileCount = 0;
            folderCount = 0;
            var allDirs = Directory.EnumerateFileSystemEntries(folderPath, "*", SearchOption.AllDirectories);
            // var allEntries = Directory.GetFileSystemEntries(folderPath, "*", SearchOption.AllDirectories);
            foreach (var entry in allDirs)
            {
                if (Directory.Exists(entry))
                {
                    folderCount++;
                }
                else if (File.Exists(entry))
                {
                    fileCount++;
                }
            }
            Debug.Print($"Total directories: {folderCount}, Total files: {fileCount}");
        }

        public void CountMethodI(string folderPath, out int fileCount, out int folderCount)
        {
            fileCount = 0;
            folderCount = 0;
            long bytes = new FileSystemEnumerable<long>(folderPath, (ref FileSystemEntry entry) => entry.Length, new EnumerationOptions { RecurseSubdirectories = true }).Sum();
            Debug.Print($"{bytes} bytes");
        }

        public void TestPerformance(string folderPath)
        {
            int fileCount = 0;  
            int folderCount = 0;

            //Test_CountMethodA(folderPath, out fileCount, out folderCount);
            //Debug.Print($"TOTAL => CountMethodA({folderPath}): file={fileCount}, folder={folderCount}");

            // Test_CountMethodB(folderPath, out fileCount, out folderCount);

            Test_CountMethodC(folderPath, out fileCount, out folderCount);
        }

        [Time("CountMethodA: {folderPath}")]
        protected void Test_CountMethodA(string folderPath, out int fileCount, out int folderCount)
        {
            R_CountMethodA(folderPath, out fileCount, out folderCount);
        }

        [Time("CountMethodB: {folderPath}")]
        protected void Test_CountMethodB(string folderPath, out int fileCount, out int folderCount)
        {
            CountMethodB(folderPath, out fileCount, out folderCount);
        }

        [Time("CountMethodC: {folderPath}")]
        protected void Test_CountMethodC(string folderPath, out int fileCount, out int folderCount)
        {
            CountMethodC(folderPath, out fileCount, out folderCount);
        }
    }
}
