using System.Diagnostics;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using Microsoft.WindowsAPICodePack.Shell;

// NuGet Package: Microsoft-WindowsAPICodePack-Shell

namespace CSharpAppPlayground.MediaParsers.MediaLibs
{
    public class ShellService
    {
        protected ShellFile sFile;

        public int GetDuration(string filePath)
        {
            try
            {
                sFile = ShellFile.FromFilePath(filePath);
                ShellProperty<ulong?> mediaDuraion = sFile.Properties.System.Media.Duration;
                if (mediaDuraion != null && mediaDuraion.Value.HasValue)
                {
                    TimeSpan t = TimeSpan.FromTicks((long)mediaDuraion.Value.Value);
                    // Debug.Print($"file: {filePath} duration seconds: {t.TotalSeconds}, dataValue: {mediaDuraion.Value.Value}");
                    return (int)t.TotalSeconds; // duration = t.Seconds; - Seconds is wrong, TotalSeconds is correct 
                }
            }
            catch (Exception ex)
            {
                Debug.Print("ShellService.GetDuration() ex: " + ex.Message);
            }
            return -1;
        }

        public (int width, int height) GetMediaDimensions(string filePath)
        {
            int w = -1;
            int h = -1;
            try
            {
                sFile = ShellFile.FromFilePath(filePath);
                ShellProperty<uint?> fwidth = sFile.Properties.System.Image.HorizontalSize;
                if (fwidth != null && fwidth.Value.HasValue)
                    w = (int)fwidth.Value.Value;
                
                ShellProperty<uint?> fheight = sFile.Properties.System.Image.VerticalSize;
                if (fheight != null && fheight.Value.HasValue)
                    h = (int)fheight.Value.Value;
            }
            catch (Exception ex)
            {
                Debug.Print($"ShellService.GetMediaDimensions() ex: {ex.Message}");
            }
            return (w, h); // Return invalid dimensions if unsuccessful
        }

        public void Dispose()
        {
            sFile?.Dispose();
        }

        public (int width, int height ,int duration) GetVideoProperties(string filePath)
        {
            int w = -1;
            int h = -1;
            int duration = -1;
            try
            {
                using (var fileProp = ShellFile.FromFilePath(filePath)) { 
                    if (fileProp != null) {
                        ShellProperty<uint?> fwidth = fileProp.Properties.System.Video.FrameWidth;
                        if (fwidth != null && fwidth.Value.HasValue)
                            w = (int)fwidth.Value.Value;
                        
                        ShellProperty<uint?> fheight = fileProp.Properties.System.Video.FrameHeight;
                        if (fheight != null && fheight.Value.HasValue)
                            h = (int)fheight.Value.Value;
                        
                        ShellProperty<ulong?> mediaDuraion = fileProp.Properties.System.Media.Duration;
                        if (mediaDuraion != null && mediaDuraion.Value.HasValue)
                        {
                            TimeSpan t = TimeSpan.FromTicks((long)mediaDuraion.Value.Value);
                            duration = (int)t.TotalSeconds; // duration = t.Seconds; - Seconds is wrong, TotalSeconds is correct 
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                Debug.Print($"ShellService.GetVideoProperties() ex: {ex.Message}");
            }
            return (w, h, duration); // Return invalid dimensions if unsuccessful
        }
    }
}
