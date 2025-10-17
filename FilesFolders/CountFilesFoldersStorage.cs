using MethodTimer;
using System.Diagnostics;
using System.IO.Enumeration;
using System.Text.RegularExpressions;

namespace CSharpAppPlayground.FilesFolders
{
    public class CountFilesFoldersStorage
    {
        public struct FileFolderStorageCount
        {
            public int FileCount;
            public int FolderCount;
            public long TotalBytes;
        }

        public FileFolderStorageCount CountFilesFoldersAndStorage(string folderPath)
        {
            int fileCount = 0;
            int folderCount = 0;    
            long bytes = 0;

            FileFolderStorageCount resultObj = new FileFolderStorageCount
            {
                FileCount = fileCount,
                FolderCount = folderCount,
                TotalBytes = bytes
            };

            if (string.IsNullOrWhiteSpace(folderPath))
            {
                Debug.Print("Error: Folder path is null, empty, or whitespace.");
                return resultObj;
            }
            else if (!Directory.Exists(folderPath))
            {
                Debug.Print($"Error: Folder path does not exist: {folderPath}");
                return resultObj;
            }

            // TODO check different functions

            return resultObj;       
        }

        public FileFolderStorageCount CountMethodA(string folderPath)
        {
            int fileCount = 0;
            int folderCount = 0;
            long bytes = 0;

            try
            {
                string[] allDirs = Directory.GetDirectories(folderPath, "*", SearchOption.AllDirectories);
                folderCount = allDirs.Length;
                string[] allFiles = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories);
                fileCount = allFiles.Length;
                bytes = allFiles.Sum(file => new FileInfo(file).Length);
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

            return new FileFolderStorageCount
            {
                FileCount = fileCount,
                FolderCount = folderCount,
                TotalBytes = bytes
            };
        }

        public FileFolderStorageCount CountMethodB(string folderPath)
        {
            int fileCount = 0;
            int folderCount = 0;
            long bytes = 0;

            var options = new EnumerationOptions
            {
                RecurseSubdirectories = true,
                AttributesToSkip = 0, // include hidden and system
                ReturnSpecialDirectories = false,
                IgnoreInaccessible = true
            };

            // Count files and folders using FileSystemEnumerable
            foreach (var entry in new FileSystemEnumerable<bool>(folderPath, (ref FileSystemEntry entry) => 
            {
                if (entry.IsDirectory)
                {
                    folderCount++;
                }
                else
                {
                    fileCount++;
                    bytes += entry.Length;
                }
                return true;
            }, options))
            {
                // Processing done in the selector
                // Debug.Print("DONE: Processing done in the selector");
            }
            
            return new FileFolderStorageCount
            {
                FileCount = fileCount,
                FolderCount = folderCount,
                TotalBytes = bytes
            };
        }

        protected FileFolderStorageCount CountMethodC(string folderPath)
        {
            int fileCount = 0;
            int folderCount = 0;
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

            // -- getting bytes
            Match bytesMatch = Regex.Match(output, @"Bytes :\s+(\d+)\s+");
            bytes = bytesMatch.Success ? long.Parse(bytesMatch.Groups[1].Value) : 0;
            Debug.Print($"What is total size? {bytes} bytes, {bytesMatch}");

            // -- getting total files
            Match filesMatch = Regex.Match(output, @"Files :\s+(\d+)\s+");
            fileCount = filesMatch.Success ? int.Parse(filesMatch.Groups[1].Value) : 0;
            Debug.Print($"What is total fileCount? {fileCount}, {filesMatch}");

            // -- getting total dirs
            Match dirsMatch = Regex.Match(output, @"Dirs :\s+(\d+)\s+");
            folderCount = dirsMatch.Success ? int.Parse(dirsMatch.Groups[1].Value) : 0;
            folderCount -= 1; // subtract 1 to exclude the root folder itself
            Debug.Print($"What is total dirCount? {folderCount}, {dirsMatch}");

            return new FileFolderStorageCount
            {
                FileCount = fileCount,
                FolderCount = folderCount,
                TotalBytes = bytes
            };
        }

        protected FileFolderStorageCount CountMethodD(string folderPath)
        {
            int fileCount = 0;
            int folderCount = 0;
            long bytes = 0;

            var files = Directory.EnumerateFiles(folderPath, "*", SearchOption.AllDirectories);
            fileCount = 0;
            bytes = 0;
            foreach (var file in files)
            {
                fileCount++;
                try
                {
                    bytes += new FileInfo(file).Length;
                }
                catch (Exception)
                {
                    Debug.Print($"Error: Could not get file size - {file}"); // Could not get file size, ignore or handle as needed.
                }
            }
            folderCount = Directory.EnumerateDirectories(folderPath, "*", SearchOption.AllDirectories).Count();

            return new FileFolderStorageCount
            {
                FileCount = fileCount,
                FolderCount = folderCount,
                TotalBytes = bytes
            };
        }

        protected FileFolderStorageCount CountMethodE(string folderPath)
        {
            int fileCount = 0;
            int folderCount = 0;
            long bytes = 0;

            var allEntries = new DirectoryInfo(folderPath).EnumerateFileSystemInfos("*", SearchOption.AllDirectories);
            foreach (var entry in allEntries)
            {
                if (entry is DirectoryInfo)
                {
                    folderCount++;
                }
                else if (entry is FileInfo)
                {
                    fileCount++;
                    bytes += ((FileInfo)entry).Length;
                }
            }

            return new FileFolderStorageCount
            {
                FileCount = fileCount,
                FolderCount = folderCount,
                TotalBytes = bytes
            };
        }

        protected FileFolderStorageCount CountMethodF(string folderPath)
        {
            int fileCount = 0;
            int folderCount = 0;
            long bytes = 0;

            var allEntries = new DirectoryInfo(folderPath).GetFileSystemInfos("*", SearchOption.AllDirectories);
            foreach (var entry in allEntries)
            {
                if (entry is DirectoryInfo)
                {
                    folderCount++;
                }
                else if (entry is FileInfo)
                {
                    fileCount++;
                    bytes += ((FileInfo)entry).Length;
                }
            }

            return new FileFolderStorageCount
            {
                FileCount = fileCount,
                FolderCount = folderCount,
                TotalBytes = bytes
            };
        }

        protected FileFolderStorageCount CountMethodG(string folderPath)
        {
            int fileCount = 0;
            int folderCount = 0;
            long bytes = 0;

            var allEntries = Directory.EnumerateFileSystemEntries(folderPath, "*", SearchOption.AllDirectories);
            foreach (var entry in allEntries)
            {
                if (Directory.Exists(entry))
                {
                    folderCount++;
                }
                else if (File.Exists(entry))
                {
                    fileCount++;
                    bytes += new FileInfo(entry).Length;
                }
            }

            return new FileFolderStorageCount
            {
                FileCount = fileCount,
                FolderCount = folderCount,
                TotalBytes = bytes
            };
        }

        protected FileFolderStorageCount CountMethodH(string folderPath)
        {
            int fileCount = 0;
            int folderCount = 0;
            long bytes = 0;

            var allEntries = Directory.GetFileSystemEntries(folderPath, "*", SearchOption.AllDirectories);
            foreach (var entry in allEntries)
            {
                if (Directory.Exists(entry))
                {
                    folderCount++;
                }
                else if (File.Exists(entry))
                {
                    fileCount++;
                    bytes += new FileInfo(entry).Length;
                }
            }

            return new FileFolderStorageCount
            {
                FileCount = fileCount,
                FolderCount = folderCount,
                TotalBytes = bytes
            };
        }

        // Generic tester to avoid duplicated Test_Count logic.
        private void Test_Count(string folderPath, string methodName, Func<string, FileFolderStorageCount> countMethod)
        {
            Debug.Print($"\nTesting {methodName} on folder: {folderPath}");
            FileFolderStorageCount o = countMethod(folderPath);
            Debug.Print($"TOTAL => {methodName}({folderPath}): file={o.FileCount}, folder={o.FolderCount}, bytes={o.TotalBytes}");
        }

        [Time("CountMethodA: {folderPath}")]
        protected void Test_CountMethodA(string folderPath)
        {
            Test_Count(folderPath, nameof(CountMethodA), CountMethodA);
        }

        [Time("CountMethodB: {folderPath}")]
        protected void Test_CountMethodB(string folderPath)
        {
            Test_Count(folderPath, "CountMethodB", CountMethodB);
        }

        [Time("CountMethodC: {folderPath}")]
        protected void Test_CountMethodC(string folderPath)
        {
            Test_Count(folderPath, "CountMethodC", CountMethodC);
        }

        [Time("CountMethodD: {folderPath}")]
        protected void Test_CountMethodD(string folderPath)
        {
            Test_Count(folderPath, "CountMethodD", CountMethodD);
        }

        [Time("CountMethodE: {folderPath}")]
        protected void Test_CountMethodE(string folderPath)
        {
            Test_Count(folderPath, "CountMethodE", CountMethodE);
        }

        [Time("CountMethodF: {folderPath}")]
        protected void Test_CountMethodF(string folderPath)
        {
            Test_Count(folderPath, "CountMethodF", CountMethodF);
        }

        [Time("CountMethodG: {folderPath}")]
        protected void Test_CountMethodG(string folderPath)
        {
            Test_Count(folderPath, "CountMethodG", CountMethodG);
        }

        [Time("CountMethodH: {folderPath}")]
        protected void Test_CountMethodH(string folderPath)
        {
            Test_Count(folderPath, "CountMethodH", CountMethodH);
        }

        public async Task TestPerformanceAsync(string folderPath, CancellationToken cancellationToken)
        {
            try
            {
                // one only
                // await Task.Run(() => Test_CountMethodA(folderPath), cancellationToken);

                // one after another
                await Task.Run(() => Test_CountMethodA(folderPath), cancellationToken)
                    .ContinueWith(t => Test_CountMethodB(folderPath), cancellationToken)
                    .ContinueWith(t => Test_CountMethodC(folderPath), cancellationToken)
                    .ContinueWith(t => Test_CountMethodD(folderPath), cancellationToken)
                    .ContinueWith(t => Test_CountMethodE(folderPath), cancellationToken)
                    .ContinueWith(t => Test_CountMethodF(folderPath), cancellationToken)
                    .ContinueWith(t => Test_CountMethodG(folderPath), cancellationToken)
                    .ContinueWith(t => Test_CountMethodH(folderPath), cancellationToken);

                cancellationToken.ThrowIfCancellationRequested();
            }
            catch (OperationCanceledException)
            {
                Debug.Print("Cancellation requested: The operation was cancelled by the user.");
            }
        }
    }
}
