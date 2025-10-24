using CSharpAppPlayground.FilesFolders;
using CSharpAppPlayground.MediaParsers.MediaLibs;
using MethodTimer;
using System.Diagnostics;

namespace CSharpAppPlayground.MediaParsers
{
    public class MediaLibChecker
    {
        // [TODO]
        public void CheckLibsUsingFile(string filePath)
        {
            if (MediaChecker.IsMediaFile(filePath))
            {
                // Perform media file checks, [TODO: pick a good option]
            }
            else
                Debug.Print($"Unsupported media file {filePath}");
        }

        // [TODO]
        public void CheckLibsUsingFolder(string folderPath)
        {
            var mediaFiles = Directory.EnumerateFiles(folderPath, "*.*", SearchOption.AllDirectories)
                                      .Where(file => MediaChecker.IsMediaFile(file));
            foreach (var file in mediaFiles)
            {
                CheckLibsUsingFile(file);
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

        protected VLCLibSimpleService vLCLibSimpleService = new VLCLibSimpleService();
        public int UseVLCLibSimpleService(string filePath)
        {
            return vLCLibSimpleService.GetDuration(filePath);
        }

        public int UseVLCLibSimpleService_IDispose(string filePath)
        {
            using (var vlcService = new VLCLibSimpleService())
            {
                return vlcService.GetDuration(filePath);
            }
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
            Debug.Print($"\nStarting {nameOfMethod}...");
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

        [Time("UseVLCLibSimpleService: {filePath}")]
        protected void Test_UseVLCLibSimpleService(string filePath)
        {
            TestDuration(filePath, "UseVLCLibSimpleService", UseVLCLibSimpleService);
        }

        [Time("UseVLCLibSimpleService_IDispose: {filePath}")]
        protected void Test_UseVLCLibSimpleService_IDispose(string filePath)
        {
            TestDuration(filePath, "UseVLCLibSimpleService_IDispose", UseVLCLibSimpleService_IDispose);
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

                //await Task.Run(() => Test_UseVLCLibSimpleService(filePath), cancellationToken);
                //await Task.Run(() => Test_UseMp4Parser(filePath), cancellationToken);
                //await Task.Run(() => Test_UseVLCLibSimpleService_IDispose(filePath), cancellationToken);

                // one after another
                await Task.Run(() => Test_UseFFMpegCore(filePath), cancellationToken)
                    .ContinueWith(t => Test_UseFFProbeCmd(filePath), cancellationToken)
                    .ContinueWith(t => Test_UseMediaInfo(filePath), cancellationToken)
                    .ContinueWith(t => Test_UseMediaToolkit(filePath), cancellationToken)
                    .ContinueWith(t => Test_UseMp4Parser(filePath), cancellationToken)
                    .ContinueWith(t => Test_UseNRecoFFProbe(filePath), cancellationToken)
                    .ContinueWith(t => Test_UseShellService(filePath), cancellationToken)
                    .ContinueWith(t => Test_UseTagLibService(filePath), cancellationToken)
                    .ContinueWith(t => Test_UseVLCLibSimpleService(filePath), cancellationToken)
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
