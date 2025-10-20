using LibVLCSharp.Shared;
using System.Diagnostics;

namespace CSharpAppPlayground.MediaParsers.MediaLibs
{
    public class VLCLibService : IDisposable
    {
        private readonly LibVLC _libVLC;
        private bool _disposed;

        public VLCLibService()
        {
            // Initialize with common VLC options for media parsing
            _libVLC = new LibVLC("--quiet", "--no-video", "--no-audio");
        }

        public async Task<int> GetDuration(string filePath)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(VLCLibService));

            try
            {
                using (var media = new Media(_libVLC, new Uri(filePath)))
                {
                    Debug.Print($"Parsing media file: {filePath}, {media.ToString()}");
                    // Parse the media synchronously first
                    await media.Parse(MediaParseOptions.ParseNetwork);

                    //// Get duration in milliseconds and convert to seconds
                    long durationMs = media.Duration;
                    return (int)(durationMs / 1000);
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"VLC error parsing {filePath}: {ex.Message}");
                return -1;
            }
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
