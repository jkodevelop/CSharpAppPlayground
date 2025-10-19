using MediaToolkit;
using MediaToolkit.Model;
using System.Diagnostics;
using System.Text.RegularExpressions;

// NuGet Package: MediaToolkit

namespace CSharpAppPlayground.MediaParsers.MediaLibs
{
    public class MediaToolkitService
    {
        static readonly Regex FrameSizeRx = new Regex(@"(?<w>\d+)\s*[xX,]\s*(?<h>\d+)", RegexOptions.Compiled);

        public int GetDuration(string filePath)
        {
            try
            {
                using (var engine = new Engine())
                {
                    var mediaFile = new MediaFile { Filename = filePath };
                    engine.GetMetadata(mediaFile);

                    var metadata = mediaFile.Metadata;
                    var frameSize = metadata.VideoData?.FrameSize;


                    return (int)metadata.Duration.TotalSeconds;
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"MediaToolkitService.GetDuration() ex: {ex.Message}");
            }
            return -1;
        }


        /// <summary>
        /// Tries to extract width/height from MediaFile.Metadata.VideoData.FrameSize.
        /// Handles System.Drawing.Size, string like "1920x1080", or numeric tuples.
        /// </summary>
        public static bool TryGetWidthHeight(MediaFile file, out int width, out int height)
        {
            width = 0; height = 0;
            if (file?.Metadata?.VideoData == null) return false;

            var fs = file.Metadata.VideoData.FrameSize;
            if (fs == null) return false;

            // Case 1: System.Drawing.Size or similar struct with Width/Height properties
            var type = fs.GetType();
            var widthProp = type.GetProperty("Width");
            var heightProp = type.GetProperty("Height");
            if (widthProp != null && heightProp != null)
            {
                try
                {
                    width = Convert.ToInt32(widthProp.GetValue(fs));
                    height = Convert.ToInt32(heightProp.GetValue(fs));
                    return width > 0 && height > 0;
                }
                catch { /* fallthrough to string parsing */ }
            }

            // Case 2: string like "1920x1080" or " 1920 x 1080 "
            if (fs is string s)
            {
                var m = FrameSizeRx.Match(s);
                if (m.Success &&
                    int.TryParse(m.Groups["w"].Value, out width) &&
                    int.TryParse(m.Groups["h"].Value, out height))
                {
                    return true;
                }
            }

            // Case 3: fallback - try ToString() and parse
            var tstr = fs.ToString();
            if (!string.IsNullOrEmpty(tstr))
            {
                var m = FrameSizeRx.Match(tstr);
                if (m.Success &&
                    int.TryParse(m.Groups["w"].Value, out width) &&
                    int.TryParse(m.Groups["h"].Value, out height))
                {
                    return true;
                }
            }

            return false;
        }
    }
}

/*
 * Organize Later
 * 

var inputFile = new MediaFile { Filename = @"C:\videos\sample.mp4" };
using (var engine = new Engine())
{
    engine.GetMetadata(inputFile);
}

if (MediaToolkitHelpers.TryGetWidthHeight(inputFile, out int w, out int h))
{
    Console.WriteLine($"Resolution: {w}x{h}");
}
else
{
    Console.WriteLine("Resolution not available from MediaToolkit metadata.");

}
*/

//class Program
//{
//    static void Main()
//    {
//        var ffProbe = new FFProbe { ToolPath = @"C:\ffmpeg\bin" }; // optional if ffprobe is on PATH
//        var mediaInfo = ffProbe.GetMediaInfo(@"C:\videos\sample.mp4");

//        Console.WriteLine($"Duration : {mediaInfo.Duration}");

//        var videoStream = Array.Find(mediaInfo.Streams, s => s.CodecType == "video");
//        if (videoStream != null)
//        {
//            Console.WriteLine($"Width    : {videoStream.Width}");
//            Console.WriteLine($"Height   : {videoStream.Height}");
//        }
//    }
//}