using LibVLCSharp.Shared;
using System.Diagnostics;

namespace CSharpAppPlayground.MediaParsers.MediaLibs
{
    public class VLCLibSimpleService
    {
        public int GetDuration(string filePath)
        {
            int seconds = -1;
            try
            {
                var libVLC = new LibVLC();
                var media = new Media(libVLC, filePath);
                media.Parse(MediaParseOptions.ParseLocal | MediaParseOptions.ParseNetwork);

                // Wait for parsing to complete
                while (media.ParsedStatus != MediaParsedStatus.Done)
                {
                    System.Threading.Thread.Sleep(10);
                }

                // Duration is now available in milliseconds
                long duration = media.Duration;

                //x--
                var videoTrack = media.Tracks.FirstOrDefault(t => t.TrackType == TrackType.Video);

                if (!videoTrack.Equals(default(MediaTrack)))
                {
                    var width = videoTrack.Data.Video.Width;
                    var height = videoTrack.Data.Video.Height;
                    Debug.Print($"Frame Size: {width}x{height}");
                }
                else
                {
                    Debug.Print("No video track found.");
                }
                //x--

                return (int)(duration / 1000);
            }
            catch(Exception ex)
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
                var libVLC = new LibVLC();
                var media = new Media(libVLC, filePath);
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
                Debug.Print($"VLCLibSimpleService.GetDuration() ex: {ex.Message}");
            }

            return (w, h);
        }
    }
}

