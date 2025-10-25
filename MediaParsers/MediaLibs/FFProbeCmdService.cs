using System.Diagnostics;

// REQUIRED: ffmpeg and ffprobe must be installed and accessible in the system PATH for this code to work.
// command line is more sensitive to quotes and media types than using ffmpeg libraries directly.

/*
ffprobe -v error -show_entries stream=width,height,duration -of default ".\file.opus"
ffprobe -v error -show_entries stream=width,height,duration -of default ".\file.mp4"

example output for .mp4 file:
[STREAM]
width=640
height=360
duration=8505.363533
[/STREAM]
[STREAM]
duration=8505.422948
[/STREAM]

example output for .opus file:
[STREAM]
duration=10341.207500
[/STREAM]

ffprobe -v error -select_streams v:0 -show_entries stream=duration,width,height -of  default=noprint_wrappers=1 ".\file.mp4"
ffprobe -v error -show_entries stream=duration,width,height -of  default=noprint_wrappers=1 ".\file.opus"

example output for .mp4 file:
width=640
height=360
duration=8505.363533

example output for .opus file:
duration=10341.207500
 */

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

            var lines = output.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            if (lines.Length >= 2 &&
                int.TryParse(lines[0].Trim(), out int w) &&
                int.TryParse(lines[1].Trim(), out int h))
            {
                return (w, h);
            }

            return (-1, -1);
        }

        // FIX for audio files that do not have width/height
        public (int width, int height, int duration) GetVideoProperties(string filePath)
        {
            int w = -1;
            int h = -1;
            int duration = -1;

            // TODO: validate filePath
            var psi = new ProcessStartInfo
            {
                FileName = "ffprobe",
                Arguments = $"-v error -show_entries stream=width,height,duration -of default=noprint_wrappers=1 \"{filePath}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var proc = Process.Start(psi);
            string output = proc!.StandardOutput.ReadToEnd();
            proc.WaitForExit();

            // FFProbe output format: width=640\nheight=360\nduration=8505.363533
            // For audio files, only duration may be present
            var lines = output.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var trimmedLine = line.Trim();
                if (trimmedLine.StartsWith("width="))
                {
                    var widthValue = trimmedLine.Substring(6); // Remove "width="
                    if (int.TryParse(widthValue, out int parsedW))
                        w = parsedW;
                }
                else if (trimmedLine.StartsWith("height="))
                {
                    var heightValue = trimmedLine.Substring(7); // Remove "height="
                    if (int.TryParse(heightValue, out int parsedH))
                        h = parsedH;
                }
                else if (trimmedLine.StartsWith("duration="))
                {
                    var durationValue = trimmedLine.Substring(9); // Remove "duration="
                    if (double.TryParse(durationValue, System.Globalization.CultureInfo.InvariantCulture, out double seconds))
                        duration = (int)seconds;
                }
            }

            return (w, h, duration);
        }
    }
}
