using Windows.Storage;
using System.Diagnostics;

// Required: [TO DOCUMENT]
// 1. Install the NuGet package "Microsoft.WindowsAppSDK" version 1.4.6 or later
// Microsoft.WindowsAppSDK => Windows.Media + Windows.Storage
//
// 2. update your project file (.csproj) to include the following property group:
// <PropertyGroup>
//   ...
//   <TargetFramework>net8.0-windows10.0.19041.0</TargetFramework>
// </PropertyGroup>
//
// older package
// NuGet: Microsoft.Windows.SDK.Contracts => Windows.Media + Windows.Storage (obsolete)

namespace CSharpAppPlayground.MediaParsers.MediaLibs
{
    public class WindowsMediaService
    {
        public async Task<int> GetDuration(string filePath)
        {
            int duration = -1;

            try
            {
                var file = await Windows.Storage.StorageFile.GetFileFromPathAsync(filePath);
                var props = await file.Properties.GetVideoPropertiesAsync();
                duration = (int)props.Duration.TotalSeconds;
                //Debug.Print($"Duration: {props.Duration.TotalSeconds:F2} seconds");
            }
            catch (Exception ex)
            {
                Debug.Print($"WindowsMediaService.GetVideoProperties() failed {ex.Message}");
            }

            return duration;
        }

        public async Task<(int width, int height, int duration)> GetVideoProperties(string filePath)
        {
            int w = -1;
            int h = -1;
            int duration = -1;

            try
            {
                var file = await Windows.Storage.StorageFile.GetFileFromPathAsync(filePath);
                var props = await file.Properties.GetVideoPropertiesAsync();

                w = (int)props.Width;
                h = (int)props.Height;
                duration = (int)props.Duration.TotalSeconds;

                Debug.Print($"Duration: {props.Duration.TotalSeconds:F2} seconds");
                Debug.Print($"Width: {props.Width}");
                Debug.Print($"Height: {props.Height}");
            }
            catch(Exception ex)
            {
                Debug.Print($"WindowsMediaService.GetVideoProperties() failed {ex.Message}");
            }   

            return (w, h, duration);
        }
    }
}
