using System.Diagnostics;

namespace CSharpAppPlayground.MediaParsers
{
    public struct MediaMeta
    {
        public int Width;
        public int Height;
        public int DurationSeconds;
        public parseResults Result;
        public string filePath;
        public string notes;

        public override string ToString()
        {
            return $"   Width: {Width}, Height: {Height}, DurationSeconds: {DurationSeconds}, {notes}, File: {filePath}, Result:{Result}";
        }
    }

    public enum parseResults
    {
        Success,
        Partial,
        Failed,
        Working
    }

    public static class MediaChecker
    {
        static string[] audioExtensions = { ".mp3", ".wav", ".aac", ".flac", ".ogg", ".opus" };
        static string[] videoExtensions = { ".mp4", ".avi", ".mov", ".mkv", ".wmv", ".rmvb", ".mpg", ".flv", ".webm", ".vob" };
        static string[] fastExtensions = { ".mp4", ".mov", ".m4v" }; // this allows for quick MP4 parsing option

        public static bool IsMediaFile(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath).ToLower();

            return Array.Exists(audioExtensions, ext => ext == fileExtension) ||
                    Array.Exists(videoExtensions, ext => ext == fileExtension);
        }

        public static bool IsAudioFile(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath).ToLower();
            return Array.Exists(audioExtensions, ext => ext == fileExtension);
        }

        public static bool IsFastParseFile(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath).ToLower();
            return Array.Exists(fastExtensions, ext => ext == fileExtension);
        }
    }
}
