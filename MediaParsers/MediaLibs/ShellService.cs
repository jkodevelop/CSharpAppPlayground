using System.Diagnostics;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using Microsoft.WindowsAPICodePack.Shell;

namespace CSharpAppPlayground.MediaParsers.MediaLibs
{
    public class ShellService
    {
        public int getDurationOfMedia(string filePath)
        {
            try
            {
                ShellFile fileProp = ShellFile.FromFilePath(filePath);
                ShellProperty<ulong?> mediaDuraion = fileProp.Properties.System.Media.Duration;
                if (mediaDuraion != null && mediaDuraion.Value.HasValue)
                {
                    TimeSpan t = TimeSpan.FromTicks((long)mediaDuraion.Value.Value);
                    // Debug.Print($"file: {filePath} duration seconds: {t.TotalSeconds}, dataValue: {mediaDuraion.Value.Value}");
                    return (int)t.TotalSeconds; // duration = t.Seconds; - Seconds is wrong, TotalSeconds is correct 
                }
            }
            catch (Exception ex)
            {
                Debug.Print("getDurationOfMedia() ex: " + ex.Message);
            }
            return -1;
        }
    }
}
