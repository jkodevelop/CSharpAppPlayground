using MethodTimer;
using System.Diagnostics;

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

        [Time("CountMethodA: {folderPath}")]
        protected void Test_CountMethodA(string folderPath)
        {
            Debug.Print($"\nTesting CountMethodA on folder: {folderPath}");
            FileFolderStorageCount o = CountMethodA(folderPath);
            Debug.Print($"TOTAL => CountMethodA({folderPath}): file={o.FileCount}, folder={o.FolderCount}, bytes={o.TotalBytes}");
        }

        public async Task TestPerformanceAsync(string folderPath, CancellationToken cancellationToken)
        {
            try
            {
                // one only
                await Task.Run(() => Test_CountMethodA(folderPath), cancellationToken);

                // one after another
                //await Task.Run(() => Test_CountMethodF(folderPath), cancellationToken)
                //    .ContinueWith(t => Test_CountMethodB(folderPath), cancellationToken)
                //    .ContinueWith(t => Test_CountMethodC(folderPath), cancellationToken)
                //    .ContinueWith(t => Test_CountMethodD(folderPath), cancellationToken)
                //    .ContinueWith(t => Test_CountMethodA(folderPath), cancellationToken)
                //    .ContinueWith(t => Test_CountMethodA_Recur(folderPath), cancellationToken);

                cancellationToken.ThrowIfCancellationRequested();
            }
            catch (OperationCanceledException)
            {
                Debug.Print("Cancellation requested: The operation was cancelled by the user.");
            }
        }
    }
}
