using NReco.VideoInfo;
using System.Diagnostics;

namespace CSharpAppPlayground.MediaParsers.MediaLibs
{
    public class NRecoFFProbeService
    {
        protected FFProbe ffProbe;
        protected NReco.VideoInfo.MediaInfo videoInfo;

        public NRecoFFProbeService(string? filePath)
        {
            ffProbe = new FFProbe();
            if(filePath != null)
                GetFile(filePath);
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
                Debug.Print("NRecoFFProbeService.GetMediaInfo() ex: " + ex.Message);
            }
        }

        public void CheckFileIsSet()
        {
            if (videoInfo == null)
            {
                throw new Exception("videoInfo is null, use GetFile() to set the file");
            }
        }

        public void ParseWithFFProbe()
        {
            CheckFileIsSet();
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

        public int GetDuration()
        {
            CheckFileIsSet();
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
            CheckFileIsSet();
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
