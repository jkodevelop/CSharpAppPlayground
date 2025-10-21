using LibVLCSharp.Shared;
using System.Diagnostics;

// // NuGet Packages required: LibVLCSharp + VideoLAN.LibVLC.Windows

namespace CSharpAppPlayground.MediaParsers.MediaLibs
{
    public class VLCLibSimpleService : IDisposable
    {
        private readonly LibVLC _libVLC;
        private bool _disposed;

        public VLCLibSimpleService()
        {
            _libVLC = new LibVLC();
        }

        public int GetDuration(string filePath)
        {
            int seconds = -1;
            try
            {
                var media = new Media(_libVLC, filePath);
                media.Parse(MediaParseOptions.ParseLocal | MediaParseOptions.ParseNetwork);

                // Wait for parsing to complete
                while (media.ParsedStatus != MediaParsedStatus.Done)
                {
                    System.Threading.Thread.Sleep(10);
                }

                // Duration is now available in milliseconds
                long duration = media.Duration;
                return (int)(duration / 1000);
            }
            catch (Exception ex)
            {
                Debug.Print($"VLCLibSimpleService.GetDuration() ex: {ex.Message}");
            }
            return seconds;
        }

        public (int width, int height) GetMediaDimensions(string filePath)
        {
            int w = -1;
            int h = -1;
            try
            {
                var media = new Media(_libVLC, filePath);
                media.Parse(MediaParseOptions.ParseLocal | MediaParseOptions.ParseNetwork);

                // Wait for parsing to complete
                while (media.ParsedStatus != MediaParsedStatus.Done)
                {
                    System.Threading.Thread.Sleep(10);
                }
                var videoTrack = media.Tracks.FirstOrDefault(t => t.TrackType == TrackType.Video);

                if (!videoTrack.Equals(default(MediaTrack)))
                {
                    w = (int)videoTrack.Data.Video.Width;
                    h = (int)videoTrack.Data.Video.Height;
                    Debug.Print($"Frame Size: {w}x{h}");
                }
                else
                {
                    Debug.Print("No video track found.");
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"VLCLibSimpleService.GetMediaDimensions() ex: {ex.Message}");
            }
            return (w, h);
        }

        public (int width, int height, int duration) GetVideoProperties(string filePath)
        {
            int seconds = -1;
            int w = -1;
            int h = -1;
            try
            {
                var media = new Media(_libVLC, filePath);
                media.Parse(MediaParseOptions.ParseLocal | MediaParseOptions.ParseNetwork);

                // Wait for parsing to complete
                while (media.ParsedStatus != MediaParsedStatus.Done)
                {
                    System.Threading.Thread.Sleep(10);
                }

                // Duration is now available in milliseconds
                long duration = media.Duration;
                seconds = (int)(duration / 1000);

                // Framesize
                var videoTrack = media.Tracks.FirstOrDefault(t => t.TrackType == TrackType.Video);
                if (!videoTrack.Equals(default(MediaTrack)))
                {
                    w = (int)videoTrack.Data.Video.Width;
                    h = (int)videoTrack.Data.Video.Height;
                    //Debug.Print($"Frame Size: {w}x{h}");
                }
                else
                {
                    Debug.Print("No video track found.");
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"VLCLibSimpleService.GetVideoProperties() ex: {ex.Message}");
            }
            return (w, h, seconds);
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
using (var vlcService = new VLCLibSimpleService())
{
    // Parse multiple videos using the same service instance
    vlcService.GetDuration("video1.mp4");
    vlcService.GetDuration("video2.mp4");
    vlcService.GetDuration("video3.mp4");
}
*/