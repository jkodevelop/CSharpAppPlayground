using CSharpAppPlayground.FilesFolders;
using CSharpAppPlayground.MediaParsers.MediaLibs;
using MethodTimer;
using System.Diagnostics;

namespace CSharpAppPlayground.MediaParsers
{
    public class MediaLibChecker
    {
        static string[] audioExtensions = { ".mp3", ".wav", ".aac", ".flac", ".ogg", ".opus" };
        static string[] videoExtensions = { ".mp4", ".avi", ".mov", ".mkv", ".wmv", ".rmvb", ".mpg", ".flv" };

        public static bool IsMediaFile(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath).ToLower();

            return Array.Exists(audioExtensions, ext => ext == fileExtension) ||
                    Array.Exists(videoExtensions, ext => ext == fileExtension);
        }

        public void CheckFile(string filePath)
        {
            if (IsMediaFile(filePath))
            {
                // Perform media file checks, [TODO: pick a good option]
            }
            else
                Debug.Print($"Unsupported media file {filePath}");
        }

        // [TODO]
        public void CheckFolder(string folderPath)
        {
            var mediaFiles = Directory.EnumerateFiles(folderPath, "*.*", SearchOption.AllDirectories)
                                      .Where(file => IsMediaFile(file));
            foreach (var file in mediaFiles)
            {
                CheckFile(file);
            }
        }

        protected FFMpegCoreService ffMpegCoreService = new FFMpegCoreService();
        public int UseFFMpegCore(string filePath)
        {
            return ffMpegCoreService.GetDuration(filePath);
        }

        protected FFProbeCmdService ffProbeCmdService = new FFProbeCmdService();
        public int UseFFProbeCmd(string filePath)
        {
            return ffProbeCmdService.GetDuration(filePath);
        }

        protected MediaInfoService mediaInfoService = new MediaInfoService();
        public int UseMediaInfo(string filePath)
        {
            return mediaInfoService.GetDuration(filePath);
        }

        protected MediaToolkitService mediaToolkitService = new MediaToolkitService();
        public int UseMediaToolkit(string filePath)
        {
            return mediaToolkitService.GetDuration(filePath);
        }

        protected Mp4ParserService mp4ParserService = new Mp4ParserService();
        public int UseMp4Parser(string filePath)
        {
            // [TODO]: change class to ADD get duration only
            return mp4ParserService.GetDuration(filePath);
        }

        protected NRecoFFProbeService nRecoFFProbeService = new NRecoFFProbeService();
        public int UseNRecoFFProbe(string filePath)
        {
            return nRecoFFProbeService.GetDuration(filePath);
        }

        protected ShellService shellService = new ShellService();
        public int UseShellService(string filePath)
        {
            return shellService.GetDuration(filePath);
        }

        protected TagLibService tagLibService = new TagLibService();
        public int UseTagLibService(string filePath)
        {
            return tagLibService.GetDuration(filePath);
        }

        protected VLCLibService vlcLibService = new VLCLibService();
        public async Task<int> UseVLCLibService(string filePath)
        {
            return await vlcLibService.GetDuration(filePath);
        }

        protected WindowsMediaPlayerService wmpService = new WindowsMediaPlayerService();
        public int UseWMPService(string filePath)
        {
            return wmpService.GetDuration(filePath);
        }

        protected WindowsMediaService wmService = new WindowsMediaService();
        public async Task<int> UseWMService(string filePath)
        {
            return await wmService.GetDuration(filePath);
        }

        protected void TestDuration(string filePath, string nameOfMethod, Func<string, int> f)
        {
            int seconds = f(filePath);
            Debug.Print($"{nameOfMethod}: {seconds} seconds");
        }

        protected async Task TestDurationAsync(string filePath, string nameOfMethod, Func<string, Task<int>> f)
        {
            Debug.Print($"\nStarting async {nameOfMethod}...");
            int seconds = await f(filePath);
            Debug.Print($"async {nameOfMethod}: {seconds} seconds");
        }

        [Time("UseFFMpegCore: {filePath}")]
        protected void Test_UseFFMpegCore(string filePath)
        {
            TestDuration(filePath, "UseFFMpegCore", UseFFMpegCore);
        }

        [Time("UseFFProbeCmd: {filePath}")]
        protected void Test_UseFFProbeCmd(string filePath)
        {
            TestDuration(filePath, "UseFFProbeCmd", UseFFProbeCmd);
        }

        [Time("UseMediaInfo: {filePath}")]
        protected void Test_UseMediaInfo(string filePath)
        {
            TestDuration(filePath, "UseMediaInfo", UseMediaInfo);
        }

        [Time("UseMediaToolkit: {filePath}")]
        protected void Test_UseMediaToolkit(string filePath)
        {
            TestDuration(filePath, "UseMediaToolkit", UseMediaToolkit);
        }

        [Time("UseMp4Parser: {filePath}")]
        protected void Test_UseMp4Parser(string filePath)
        {
            TestDuration(filePath, "UseMp4Parser", UseMp4Parser);
        }

        [Time("UseNRecoFFProbe: {filePath}")]
        protected void Test_UseNRecoFFProbe(string filePath)
        {
            TestDuration(filePath, "UseNRecoFFProbe", UseNRecoFFProbe);
        }

        [Time("UseShellService: {filePath}")]
        protected void Test_UseShellService(string filePath)
        {
            TestDuration(filePath, "UseShellService", UseShellService);
        }

        [Time("UseTagLibService: {filePath}")]
        protected void Test_UseTagLibService(string filePath)
        {
            TestDuration(filePath, "UseTagLibService", UseTagLibService);
        }

        [Time("UseVLCLibService: {filePath}")]
        protected async void Test_UseVLCLibService(string filePath)
        {
            await TestDurationAsync(filePath, "UseVLCLibService", UseVLCLibService);
        }

        [Time("UseWMPService: {filePath}")]
        protected void Test_UseWMPService(string filePath)
        {
            TestDuration(filePath, "UseWMPService", UseWMPService);
        }

        [Time("UseWMService: {filePath}")]
        protected async void Test_UseWMService(string filePath)
        {
            await TestDurationAsync(filePath, "UseWMService", UseWMService);
        }

        public async Task TestPerformanceAsync_File(string filePath, CancellationToken cancellationToken)
        {
            try
            {
                FileCacheManager.FlushFileCache();

                //await Task.Run(() => Test_UseFFMpegCore(filePath), cancellationToken);

                // one after another
                await Task.Run(() => Test_UseFFMpegCore(filePath), cancellationToken)
                    .ContinueWith(t => Test_UseFFProbeCmd(filePath), cancellationToken)
                    .ContinueWith(t => Test_UseMediaInfo(filePath), cancellationToken)
                    .ContinueWith(t => Test_UseMediaToolkit(filePath), cancellationToken)
                    .ContinueWith(t => Test_UseMp4Parser(filePath), cancellationToken)
                    .ContinueWith(t => Test_UseNRecoFFProbe(filePath), cancellationToken)
                    .ContinueWith(t => Test_UseShellService(filePath), cancellationToken)
                    .ContinueWith(t => Test_UseTagLibService(filePath), cancellationToken)
                    .ContinueWith(t => Test_UseVLCLibService(filePath), cancellationToken)
                    .ContinueWith(t => Test_UseWMPService(filePath), cancellationToken)
                    .ContinueWith(t => Test_UseWMService(filePath), cancellationToken);

                cancellationToken.ThrowIfCancellationRequested();
            }
            catch (OperationCanceledException)
            {
                Debug.Print("Cancellation requested: The operation was cancelled by the user.");
            }
            catch (Exception ex)
            {
                Debug.Print($"An error occurred during performance testing: {ex.Message}");
            }
        }
    }
}
