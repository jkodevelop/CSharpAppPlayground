using WMPLib;

// To fix CS0246, you need to add a reference to the Windows Media Player COM library.
// In Visual Studio: Right-click on Project > Add > COM Reference... > "Windows Media Player" (wmp.dll).
// After adding the reference, the WMPLib namespace will be available.

namespace CSharpAppPlayground.MediaParsers.MediaLibs
{
    public class WindowsMediaPlayerService
    {
        public int GetDuration(string filePath)
        {
            var player = new WindowsMediaPlayer();
            var clip = player.newMedia(filePath);

            int duration = (int)clip.duration;

            // option A get width/height
            string widthStr = clip.getItemInfo("WM/VideoWidth");
            string heightStr = clip.getItemInfo("WM/VideoHeight");
            int.TryParse(widthStr, out int width);
            int.TryParse(heightStr, out int height);

            // option B should be faster
            int w = clip.imageSourceWidth;
            int h = clip.imageSourceHeight;

            return duration;
        }
    }
}
