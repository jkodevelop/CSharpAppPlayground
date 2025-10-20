using LibVLCSharp.Shared;
using System.Diagnostics;

// NuGet Packages required: LibVLCSharp + VideoLAN.LibVLC.Windows

namespace CSharpAppPlayground.MediaParsers.MediaLibs
{
    public class VLCLibService : IDisposable
    {
        private readonly LibVLC _libVLC;
        private bool _disposed;

        public VLCLibService()
        {
            // 1. Initialize LibVLCSharp core.
            // This only needs to be called once in your application's lifetime.
            Core.Initialize();
            
            // Create a single LibVLC instance to be reused
            _libVLC = new LibVLC();
        }

        public async Task<int> GetDuration(string filePath)
        {
            // Create a Media object for the video file using the shared LibVLC instance
            using var media = new Media(_libVLC, filePath, FromType.FromPath);

            // Parse the media asynchronously to get metadata
            var parseResult = await media.Parse(MediaParseOptions.ParseNetwork);
            Debug.Print($"Parse result: {parseResult}");

            if (parseResult != MediaParsedStatus.Done)
            {
                Console.WriteLine($"Failed to parse media. Status: {parseResult}");
                return -1;
            }

            // Access the video's properties
            var duration = media.Duration;
            var videoTrack = media.Tracks.FirstOrDefault(t => t.TrackType == TrackType.Video);

            if (!videoTrack.Equals(default(MediaTrack)))
            {
                var width = videoTrack.Data.Video.Width;
                var height = videoTrack.Data.Video.Height;

                Console.WriteLine($"Video Duration: {TimeSpan.FromMilliseconds(duration)}");
                Console.WriteLine($"Frame Size: {width}x{height}");
            }
            else
            {
                Console.WriteLine("No video track found.");
            }
            return -1;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _libVLC?.Dispose();
                }
                _disposed = true;
            }
        }
    }
}

/* Example usage:
using (var vlcService = new VLCLibService())
{
    // Parse multiple videos using the same service instance
    await vlcService.GetDuration("video1.mp4");
    await vlcService.GetDuration("video2.mp4");
    await vlcService.GetDuration("video3.mp4");
}
*/