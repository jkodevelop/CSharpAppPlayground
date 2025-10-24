using System.Diagnostics;

namespace CSharpAppPlayground.MediaParsers
{
    public class MediaFolderParser
    {
        private MediaFileParser mediaFileParser;

        public MediaFolderParser()
        {
            mediaFileParser = new MediaFileParser();
        }

        public void ParseFolder(string folderPath)
        {
            var mediaFiles = Directory.EnumerateFiles(folderPath, "*.*", SearchOption.AllDirectories)
                                      .Where(file => MediaChecker.IsMediaFile(file));
            foreach (var file in mediaFiles)
            {
                MediaMeta metaData = mediaFileParser.GetFileMetaData(file);
                Debug.Print(metaData.ToString());
            }
        }
    }
}
