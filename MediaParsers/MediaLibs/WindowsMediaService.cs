using System.Diagnostics;
//using Windows.Storage;

// NuGet: Microsoft.Windows.SDK.Contracts => Windows.Media + Windows.Storage
// newer: Microsoft.WindowsAppSDK

namespace CSharpAppPlayground.MediaParsers.MediaLibs
{
    public class WindowsMediaService
    {
        public async Task<(int width, int height, int duration)> GetVideoProperties(string filePath)
        {
            int w = -1;
            int h = -1;
            int duration = -1;

            try
            {
                //var file = await Windows.Storage.StorageFile.GetFileFromPathAsync(filePath);
                //var props = await file.Properties.GetVideoPropertiesAsync();

                //w = (int)props.Width;
                //h = (int)props.Height;
                //duration = (int)props.Duration.TotalSeconds;

                //Debug.Print($"Duration: {props.Duration.TotalSeconds:F2} seconds");
                //Debug.Print($"Width: {props.Width}");
                //Debug.Print($"Height: {props.Height}");
            }
            catch(Exception ex)
            {
                Debug.Print($"WindowsMediaService.GetVideoProperties() failed {ex.Message}");
            }   

            return (w, h, duration);
        }
    }
}
