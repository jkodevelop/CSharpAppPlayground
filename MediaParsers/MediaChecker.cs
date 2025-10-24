namespace CSharpAppPlayground.MediaParsers
{
    public static class MediaChecker
    {
        static string[] audioExtensions = { ".mp3", ".wav", ".aac", ".flac", ".ogg", ".opus" };
        static string[] videoExtensions = { ".mp4", ".avi", ".mov", ".mkv", ".wmv", ".rmvb", ".mpg", ".flv" };
        static string[] fastExtensions = { ".mp4", ".mov", ".m4v" }; // this allows for quick MP4 parsing option

        public static bool IsMediaFile(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath).ToLower();

            return Array.Exists(audioExtensions, ext => ext == fileExtension) ||
                    Array.Exists(videoExtensions, ext => ext == fileExtension);
        }

        public static bool IsFastParseFile(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath).ToLower();
            return Array.Exists(fastExtensions, ext => ext == fileExtension);
        }
    }
}
