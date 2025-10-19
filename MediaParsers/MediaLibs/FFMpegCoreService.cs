using FFMpegCore;
using System.Diagnostics;

// Nuget Package: FFMpegCore

namespace CSharpAppPlayground.MediaParsers.MediaLibs
{
    public class FFMpegCoreService
    {
        public int GetDuration(string filePath)
        {
            var info = FFProbe.Analyse(filePath);

            //var width = info.PrimaryVideoStream.Width;
            //var height = info.PrimaryVideoStream.Height;

            Debug.Print(info.Duration.ToString());
            return (int) info.Duration.TotalSeconds;
        }
    }
}

/* Organize Later

// multi-stream example
foreach (var videoStream in mediaInfo.VideoStreams)
{
    Console.WriteLine($"Video: {videoStream.Width}x{videoStream.Height}, Codec: {videoStream.CodecName}");
}

// FIRST RUN THIS TO SET THE FFMPEG BINARY PATH, if needed
FFMpegOptions.Configure(new FFMpegOptions
{
    RootDirectory = @"C:\ffmpeg\bin"  // where ffmpeg.exe and ffprobe.exe live
});

///////////////// PARALEL USAGE EXAMPLE /////////////////

var videoFiles = Directory.GetFiles(@"C:\videos", "*.mp4", SearchOption.AllDirectories);

Parallel.ForEach(videoFiles, file =>
{
    var info = FFProbe.Analyse(file);
    Console.WriteLine($"{Path.GetFileName(file)}: {info.PrimaryVideoStream.Width}x{info.PrimaryVideoStream.Height}");
});

// isHDD

bool isHDD = FFProbe.IsFileOnHDD(filePath);
var options = new ParallelOptions { MaxDegreeOfParallelism = 2 };
Parallel.ForEach(videoFiles, options, file => { <TODO> });

*/

//class Program
//{
//    static void Main()
//    {
//        // Configure path only if ffmpeg/ffprobe aren’t on the system PATH
//        // FFMpegOptions.Configure(new FFMpegOptions { RootDirectory = @"C:\ffmpeg\bin" });

//        var info = FFProbe.Analyse(@"C:\videos\sample.mp4");

//        Console.WriteLine($"Duration : {info.Duration}");
//        Console.WriteLine($"Width    : {info.PrimaryVideoStream?.Width}");
//        Console.WriteLine($"Height   : {info.PrimaryVideoStream?.Height}");
//    }
//}
