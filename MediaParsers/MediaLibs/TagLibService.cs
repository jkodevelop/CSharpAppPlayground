using System.Diagnostics;

namespace CSharpAppPlayground.MediaParsers.MediaLibs
{
    public class TagLibService
    {
        protected TagLib.File tFile;

        public void GetFile(string filePath)
        {
            try
            {
                tFile = TagLib.File.Create(filePath);
            }
            catch (Exception ex)
            {
                Debug.Print($"TagLibService.GetFile() ex: {ex.Message}");
            }
        }

        public int GetDuration(string filePath)
        {
            try
            {
                //Debug.Print($"Taglib object:{filePath} -> {tFile.Name}");
                TimeSpan t = tFile.Properties.Duration;
                return (int)t.TotalSeconds;
            }
            catch (Exception ex)
            {
                Debug.Print($"TagLibService.GetDuration() ex: {ex.Message}");
            }
            return -1;
        }

        public (int width, int height) GetMediaDimensions(string filePath)
        {
            try
            {
                if (tFile.Properties != null)
                {
                    // For video files
                    if (tFile.Properties.VideoWidth > 0 && tFile.Properties.VideoHeight > 0)
                    {
                        return (tFile.Properties.VideoWidth, tFile.Properties.VideoHeight);
                    }
                    // For image files
                    if (tFile.Properties.PhotoWidth > 0 && tFile.Properties.PhotoHeight > 0)
                    {
                        return (tFile.Properties.PhotoWidth, tFile.Properties.PhotoHeight);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"TagLibService.GetMediaDimensions() ex: {ex.Message}");
            }
            return (-1, -1); // Return invalid dimensions if unsuccessful
        }

        public void Dispose()
        {
            tFile?.Dispose();
        }
    }
}
