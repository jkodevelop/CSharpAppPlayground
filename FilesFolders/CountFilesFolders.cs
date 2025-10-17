using MethodTimer;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO.Enumeration;
using System.Text.RegularExpressions;
using static CSharpAppPlayground.FilesFolders.CountFilesFoldersStorage;

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
            else if(!Directory.Exists(folderPath))
            {
                Debug.Print($"Error: Folder path does not exist: {folderPath}");
                return;
            }

            // TODO: let's see which is faster, MethodA or MethodB
        }

        // using System.IO + loop + recursion
        protected void CountMethodA_Recur(string folderPath, out int fileCount, out int folderCount)
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
                            CountMethodA_Recur(dir, out int subFileCount, out int subFolderCount);
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

        public void CountMethodB(string folderPath, out int fileCount, out int folderCount)
        {
            fileCount = 0;
            folderCount = 0;
            var allFiles = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories);
            fileCount = allFiles.Length;
            var allDirs = Directory.GetDirectories(folderPath, "*", SearchOption.AllDirectories);
            folderCount = allDirs.Length;
        }

        // using cmd.exe + dir /s (Windows only)
        // dir /s "E:\Downloads" | find "File(s)"
        // dir /s /-c "E:\Downloads"
        /*
            cmd.exe /c = run command and exit
            /-c disables the thousands separator, so 1,024 becomes 1024.
            /s displays files in specified directory and all subdirectories.
        */
        protected void CountMethodC(string folderPath, out int fileCount, out int folderCount)
        {
            Debug.Print($"\n!! using cmd.exe to cound file and folders, the folders count will not be corrected because of the way dir counts !!\n");
            fileCount = 0;
            folderCount = 0;
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c dir /a /s /-c \"{folderPath}\"",
                    // Arguments = $"/c dir /s /-c \"{folderPath}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            // Debug.Print($"out:: {output}");
            process.WaitForExit();

			// Extract totals from the end summary. With /-c there are no commas, but handle both.
			// Take the last occurrence to ensure we capture the grand totals, not per-directory lines.
			var fileMatches = Regex.Matches(output, @"^\s*(\d+)\s+File\(s\)", RegexOptions.Multiline);
			if (fileMatches.Count > 0)
				fileCount = int.Parse(fileMatches[fileMatches.Count - 1].Groups[1].Value);

			var dirMatches = Regex.Matches(output, @"^\s*(\d+)\s+Dir\(s\)", RegexOptions.Multiline);
			if (dirMatches.Count > 0)
				folderCount = int.Parse(dirMatches[dirMatches.Count - 1].Groups[1].Value);
            
            // Match sizeMatch = Regex.Match(output, @"Total Files Listed:\s+\d+\s+File\(s\)\s+([\d,]+)");
			// long totalSize = sizeMatch.Success ? long.Parse(sizeMatch.Groups[1].Value.Replace(",", string.Empty)) : 0;
			
            // Debug.Print($"Totals => files={fileCount}, dirs={folderCount}, bytes={totalSize}");
        }

        // using Robocopy (Windows only)
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
        /* Example output, end summary part
         * 
                           Total    Copied       Skipped  Mismatch    FAILED    Extras
            Dirs :         33454         0         33454         0         0         0
           Files :         92482         0         92482         0         0         0
           Bytes : 2741560126524         0 2741560126524         0         0         0
           Times :       0:00:42   0:00:00                           0:00:00   0:00:42
           Ended : October 16, 2025 2:11:27 PM   
        */
        protected void CountMethodD(string folderPath, out int fileCount, out int folderCount)
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
            // Debug.Print($"out:: {output}");
            process.WaitForExit();

            // -- getting bytes
            //Match bytesMatch = Regex.Match(output, @"Bytes :\s+(\d+)\s+");
            //long totalSize = bytesMatch.Success ? long.Parse(bytesMatch.Groups[1].Value) : 0;
            //Debug.Print($"What is total size? {totalSize} bytes, {bytesMatch}");

            // -- getting total files
            Match filesMatch = Regex.Match(output, @"Files :\s+(\d+)\s+");
            fileCount = filesMatch.Success ? int.Parse(filesMatch.Groups[1].Value) : 0;
            Debug.Print($"What is total fileCount? {fileCount}, {filesMatch}");

            // -- getting total dirs
            Match dirsMatch = Regex.Match(output, @"Dirs :\s+(\d+)\s+");
            folderCount = dirsMatch.Success ? int.Parse(dirsMatch.Groups[1].Value) : 0;
            folderCount -= 1; // subtract 1 to exclude the root folder itself
            Debug.Print($"What is total dirCount? {folderCount}, {dirsMatch}");
        }

        public void CountMethodE(string folderPath, out int fileCount, out int folderCount)
        {
            fileCount = 0;
            folderCount = 0;

            fileCount = Directory.EnumerateFiles(folderPath, "*", SearchOption.AllDirectories).Count();
            folderCount = Directory.EnumerateDirectories(folderPath, "*", SearchOption.AllDirectories).Count();
        }

        // EnumerateFileSystemInfos
        public void CountMethodF(string folderPath, out int fileCount, out int folderCount)
        {
            fileCount = 0;
            folderCount = 0;
            var allEntries = new DirectoryInfo(folderPath).EnumerateFileSystemInfos("*", SearchOption.AllDirectories);
            foreach (var entry in allEntries)
            {
                if (entry is DirectoryInfo)
                    folderCount++;
                else if (entry is FileInfo)
                    fileCount++;
            }
            // Debug.Print($"Total directories: {folderCount}, Total files: {fileCount}");
        }

        public void CountMethodG(string folderPath, out int fileCount, out int folderCount)
        {
            fileCount = 0;
            folderCount = 0;
            var allEntries = new DirectoryInfo(folderPath).GetFileSystemInfos("*", SearchOption.AllDirectories);
            foreach (var entry in allEntries)
            {
                if (entry is DirectoryInfo)
                    folderCount++;
                else if (entry is FileInfo)
                    fileCount++;
            }
            // Debug.Print($"Total directories: {folderCount}, Total files: {fileCount}");
        }

        public void CountMethodH(string folderPath, out int fileCount, out int folderCount)
        {
            fileCount = 0;
            folderCount = 0;
            var allEntries = Directory.EnumerateFileSystemEntries(folderPath, "*", SearchOption.AllDirectories);
            foreach (var entry in allEntries)
            {
                if (Directory.Exists(entry))
                    folderCount++;
                else if (File.Exists(entry))
                    fileCount++;
            }
            // Debug.Print($"Total directories: {folderCount}, Total files: {fileCount}");
        }

        public void CountMethodI(string folderPath, out int fileCount, out int folderCount)
        {
            fileCount = 0;
            folderCount = 0;
            var allEntries = Directory.GetFileSystemEntries(folderPath, "*", SearchOption.AllDirectories);
            foreach (var entry in allEntries)
            {
                if (Directory.Exists(entry))
                    folderCount++;
                else if (File.Exists(entry))
                    fileCount++;
            }
            // Debug.Print($"Total directories: {folderCount}, Total files: {fileCount}");
        }

        public void CountMethodJ(string folderPath, out int fileCount, out int folderCount)
        {
            fileCount = 0;
            folderCount = 0;
            var options = new EnumerationOptions
            {
				RecurseSubdirectories = true,
				AttributesToSkip = 0, // include hidden and system
				ReturnSpecialDirectories = false,
				IgnoreInaccessible = true
			};

            var enumerable = new FileSystemEnumerable<byte>(
                folderPath,
                (ref FileSystemEntry entry) =>
                {
                    if (entry.IsDirectory)
                        return (byte)1; // folder
                    else
                        return (byte)2; // file
                },
                options);

            foreach (var type in enumerable)
            {
                if (type == 1)
                    folderCount++;
                else if (type == 2)
                    fileCount++;
            }
        }

        public void CountMethodK(string folderPath, out int fileCount, out int folderCount)
        {
            int _fileCount = 0;
            int _folderCount = 0;
            var directoriesToProcess = new ConcurrentQueue<string>();
            directoriesToProcess.Enqueue(folderPath);

            while (directoriesToProcess.TryDequeue(out var currentDir))
            {
                // Process files in parallel for the current directory
                var files = Directory.EnumerateFiles(currentDir, "*");
                Parallel.ForEach(files, file => Interlocked.Increment(ref _fileCount));

                // Enqueue subdirectories for later processing
                var subDirs = Directory.EnumerateDirectories(currentDir, "*");
                foreach (var subDir in subDirs)
                {
                    Interlocked.Increment(ref _folderCount);
                    directoriesToProcess.Enqueue(subDir);
                }   
            }
            fileCount = _fileCount;
            folderCount = _folderCount;
        }

        // Delegate matching the out-parameter counting methods
        private delegate void CountOutMethod(string folderPath, out int fileCount, out int folderCount);

        // Generic tester to avoid duplicated Test_Count logic.
        private void Test_Count(string folderPath, string methodName, CountOutMethod countMethod)
        {
            int fileCount = 0, folderCount = 0;
            Debug.Print($"\nTesting {methodName} on folder: {folderPath}");
            countMethod(folderPath, out fileCount, out folderCount);
            Debug.Print($"TOTAL => {methodName}({folderPath}): file={fileCount}, folder={folderCount}");
        }

        [Time("CountMethodA: {folderPath}")]
        protected void Test_CountMethodA(string folderPath)
        {
            // attribute kept for MethodTimer; delegate body to generic tester
            Test_Count(folderPath, nameof(CountMethodA_Recur), CountMethodA_Recur);
        }

        [Time("CountMethodB: {folderPath}")]
        protected void Test_CountMethodB(string folderPath)
        {
            Test_Count(folderPath, nameof(CountMethodB), CountMethodB);
        }

        [Time("CountMethodC: {folderPath}")]
        protected void Test_CountMethodC(string folderPath)
        {
            Test_Count(folderPath, nameof(CountMethodC), CountMethodC);
        }

        [Time("CountMethodD: {folderPath}")]
        protected void Test_CountMethodD(string folderPath)
        {
            Test_Count(folderPath, nameof(CountMethodD), CountMethodD);
        }

        [Time("CountMethodE: {folderPath}")]
        protected void Test_CountMethodE(string folderPath)
        {
            Test_Count(folderPath, nameof(CountMethodE), CountMethodE);
        }

        [Time("CountMethodF: {folderPath}")]
        protected void Test_CountMethodF(string folderPath)
        {
            Test_Count(folderPath, nameof(CountMethodF), CountMethodF);
        }

        [Time("CountMethodG: {folderPath}")]
        protected void Test_CountMethodG(string folderPath)
        {
            Test_Count(folderPath, nameof(CountMethodG), CountMethodG);
        }

        [Time("CountMethodH: {folderPath}")]
        protected void Test_CountMethodH(string folderPath)
        {
            Test_Count(folderPath, nameof(CountMethodH), CountMethodH);
        }

        [Time("CountMethodI: {folderPath}")]
        protected void Test_CountMethodI(string folderPath)
        {
            Test_Count(folderPath, nameof(CountMethodI), CountMethodI);
        }

        [Time("CountMethodJ: {folderPath}")]
        protected void Test_CountMethodJ(string folderPath)
        {
            Test_Count(folderPath, nameof(CountMethodJ), CountMethodJ);
        }

        [Time("CountMethodK: {folderPath}")]
        protected void Test_CountMethodK(string folderPath)
        {
            Test_Count(folderPath, nameof(CountMethodK), CountMethodK);
        }

        public async Task TestPerformanceAsync(string folderPath, CancellationToken cancellationToken)
        {
            try
            {
                // one only
                // await Task.Run(() => Test_CountMethodC(folderPath), cancellationToken);

                // one after another
                await Task.Run(() => Test_CountMethodA(folderPath), cancellationToken)
                    .ContinueWith(t => Test_CountMethodB(folderPath), cancellationToken)
                    .ContinueWith(t => Test_CountMethodC(folderPath), cancellationToken)
                    .ContinueWith(t => Test_CountMethodD(folderPath), cancellationToken)
                    .ContinueWith(t => Test_CountMethodE(folderPath), cancellationToken)
                    .ContinueWith(t => Test_CountMethodF(folderPath), cancellationToken)
                    .ContinueWith(t => Test_CountMethodG(folderPath), cancellationToken)
                    .ContinueWith(t => Test_CountMethodH(folderPath), cancellationToken)
                    .ContinueWith(t => Test_CountMethodI(folderPath), cancellationToken)
                    .ContinueWith(t => Test_CountMethodJ(folderPath), cancellationToken)
                    .ContinueWith(t => Test_CountMethodK(folderPath), cancellationToken);

                cancellationToken.ThrowIfCancellationRequested();
            }
            catch (OperationCanceledException)
            {
                Debug.Print("Cancellation requested: The operation was cancelled by the user.");
            }
        }
    }
}
