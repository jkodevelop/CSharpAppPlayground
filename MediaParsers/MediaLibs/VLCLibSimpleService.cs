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
                return (int)(duration / 1000);
            }
            catch(Exception ex)
            {
                Debug.Print($"VLCLibSimpleService.GetDuration() ex: {ex.Message}");
            }
            return seconds;
        }
    }
}

