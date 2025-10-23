using System.Diagnostics;

// REQUIRED: ffmpeg and ffprobe must be installed and accessible in the system PATH for this code to work.

namespace CSharpAppPlayground.MediaParsers.MediaLibs
{
    // command example to get video duration, width, and height:
    // 1.
    // ffprobe -v error -select_streams v:0 -show_entries stream=duration,width,height -of default=noprint_wrappers=1:nokey=1 input.mp4
    public class FFProbeCmdService
    {
        public int GetDuration(string filePath)
        {
            //TODO: validate filePath
            var psi = new ProcessStartInfo
            {
                FileName = "ffprobe",
                Arguments = $"-v error -show_entries format=duration -of default=noprint_wrappers=1:nokey=1 \"{filePath}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var proc = Process.Start(psi);
            string output = proc!.StandardOutput.ReadToEnd();
            proc.WaitForExit();

            if (double.TryParse(output.Trim(), System.Globalization.CultureInfo.InvariantCulture, out double seconds))
                return (int)seconds;

            return -1;
        }

        public (int width, int height) GetMediaDimensions(string filePath)
        {
            int w = -1;
            int h = -1;

            //TODO: validate filePath
            var psi = new ProcessStartInfo
            {
                FileName = "ffprobe",
                Arguments = $"-v error -select_streams v:0 -show_entries stream=width,height -of default=noprint_wrappers=1:nokey=1 \"{filePath}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var proc = Process.Start(psi);
            string output = proc!.StandardOutput.ReadToEnd();
            proc.WaitForExit();

            if (double.TryParse(output.Trim(), System.Globalization.CultureInfo.InvariantCulture, out double seconds))
                return (-1, -1);

            return (w, h);
        }

        public (int width, int height, int duration) GetVideoProperties(string filePath)
        {
            int w = -1;
            int h = -1;
            int duration = -1;

            //TODO: validate filePath
            var psi = new ProcessStartInfo
            {
                FileName = "ffprobe",
                Arguments = $"-v error -select_streams v:0 -show_entries stream=width,height,duration -of default=noprint_wrappers=1:nokey=1 \"{filePath}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var proc = Process.Start(psi);
            string output = proc!.StandardOutput.ReadToEnd();
            proc.WaitForExit();

            if (double.TryParse(output.Trim(), System.Globalization.CultureInfo.InvariantCulture, out double seconds))
                return (-1, -1, -1);

            return (w, h, duration); // Return invalid dimensions if unsuccessful
        }
    }
}
