namespace CSharpAppPlayground.MediaParsers
{
    public class MediaLibCheck
    {
        static string[] audioExtensions = { ".mp3", ".wav", ".aac", ".flac", ".ogg", ".opus" };
        static string[] videoExtensions = { ".mp4", ".avi", ".mov", ".mkv", ".wmv", ".rmvb", ".mpg", ".flv" };

        public static bool IsMediaFile(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath).ToLower();

            return Array.Exists(audioExtensions, ext => ext == fileExtension) ||
                    Array.Exists(videoExtensions, ext => ext == fileExtension);
        }   
    }
}
