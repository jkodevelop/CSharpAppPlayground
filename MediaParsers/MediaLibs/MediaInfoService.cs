using CSharpAppPlayground.Loggers;
using MediaInfo;

// NuGet Package: MediaInfo.Core.Native + MediaInfo.Wrapper.Core

namespace CSharpAppPlayground.MediaParsers.MediaLibs
{
    public class MediaInfoService
    {
        protected MediaInfo.MediaInfoWrapper mediaInf;
        protected DebugLogger logger = new DebugLogger("MediaInfoService");

        public void GetFile(string filePath)
        {
            mediaInf = new MediaInfo.MediaInfoWrapper(filePath, logger);
            if (!mediaInf.Success)
            {
                throw new Exception($"Failed to get MediaInfo {filePath}");
            }
        }

        public int GetDuration(string filePath)
        {
            GetFile(filePath);
            return (int)(mediaInf.Duration/1000);
        }

        public (int width, int height) GetMediaDimensions(string filePath)
        {
            GetFile(filePath);
            int w = mediaInf.Width;
            int h = mediaInf.Height;
            return (w, h);
        }
    }
}
