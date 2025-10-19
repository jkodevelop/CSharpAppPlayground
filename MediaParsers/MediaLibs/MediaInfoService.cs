using MediaInfo;

// NuGet Package: MediaInfo.Core.Native + MediaInfo.Wrapper.Core

namespace CSharpAppPlayground.MediaParsers.MediaLibs
{
    public class MediaInfoService
    {
        protected MediaInfo.MediaInfoWrapper mediaInf;

        public MediaInfoService(string filePath)
        {
            // supports ILogger
            // example: mediaInf = new MediaInfo.MediaInfoWrapper(filePath, logger);
            
            mediaInf = new MediaInfo.MediaInfoWrapper(filePath);
            if (!mediaInf.Success)
            {
                throw new Exception($"Failed to get MediaInfo {filePath}");
            }
        }

        public int GetDuration()
        {
            return mediaInf.Duration;
        }

        public (int width, int height) GetMediaDimensions()
        {
            int w = mediaInf.Width;
            int h = mediaInf.Height;
            return (w, h);
        }
    }
}
