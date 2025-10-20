using NReco.VideoInfo;
using System.Diagnostics;

// NuGet Package: NReco.VideoInfo

namespace CSharpAppPlayground.MediaParsers.MediaLibs
{
    public class NRecoFFProbeService
    {
        protected FFProbe ffProbe;
        protected NReco.VideoInfo.MediaInfo videoInfo;

        public NRecoFFProbeService()
        {
            ffProbe = new FFProbe();
            // ffProbe = new FFProbe { ToolPath = @"C:\ffmpeg\bin" };
        }

        public void GetFile(string filePath)
        {
            // TODO: validations here
            try
            {
                videoInfo = ffProbe.GetMediaInfo(filePath);
            }
            catch (Exception ex)
            {
                Debug.Print($"NRecoFFProbeService.GetMediaInfo() ex: {ex.Message}");
                throw new Exception($"NRecoFFProbeService.GetFile() failed to get media info for {filePath}");
            }
        }

        public void ParseWithFFProbe(string filePath)
        {
            GetFile(filePath);
            try
            {
                // Debug.Print($"VideoInfo: {videoInfo}");
                Debug.Print($"ffprobe duration: {videoInfo.Duration} seconds: {videoInfo.Duration.TotalSeconds}");

                int w = videoInfo.Streams[0].Width;
                int h = videoInfo.Streams[0].Height;
                Debug.Print($"ffprobe width:{w}, height:{h}");

                string type = videoInfo.Streams[0].CodecType.ToLower();
                Debug.Print($"ffprobe codectype check: {type}");
            }
            catch (Exception ex)
            {
                Debug.Print("NRecoFFProbeService.parseWithFFProbe() ffProbe, this is not a media file this library supports");
            }
        }

        public int GetDuration(string filePath)
        {
            GetFile(filePath);
            try
            {               
                return (int)videoInfo.Duration.TotalSeconds;
            }
            catch (Exception ex)
            {
                Debug.Print("NRecoFFProbeService.GetDuration() ex: " + ex.Message);
            }
            return -1;
        }

        public (int width, int height) GetMediaDimensions(string filePath)
        {
            GetFile(filePath);
            int w = -1;
            int h = -1;
            try
            {
                w = videoInfo.Streams[0].Width;
                h = videoInfo.Streams[0].Height;
            }
            catch (Exception ex)
            {
                Debug.Print($"NRecoFFProbeService.GetMediaDimensions() ex: {ex.Message}");
            }
            return (w, h); // Return invalid dimensions if unsuccessful
        }
    }
}
