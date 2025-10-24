using CSharpAppPlayground.MediaParsers.MediaLibs;
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

    public class MediaFileParser
    {
        Mp4ParserService mp4FastParse;
        TagLibService tagLibService;
        ShellService shellService;
        FFProbeCmdService ffProbeCmdService;

        public MediaFileParser()
        {
            mp4FastParse = new Mp4ParserService();
            tagLibService = new TagLibService();
            shellService = new ShellService();
            ffProbeCmdService = new FFProbeCmdService();
        }

        private void CheckParseStatus(ref MediaMeta meta)
        {
            if(meta.Width > 0 && meta.Height > 0 && meta.DurationSeconds > 0)
            {
                meta.Result = parseResults.Success;
            }
            else if(meta.Width > 0 || meta.Height > 0 || meta.DurationSeconds > 0)
            {
                meta.Result = parseResults.Partial;
            }
            else
            {
                meta.Result = parseResults.Failed;
            }
        }

        private void ParseWithMp4Parser(ref MediaMeta meta)
        {
            (int h, int w, int duration) = mp4FastParse.GetVideoProperties(meta.filePath);
            meta.Width = w;
            meta.Height = h;
            meta.DurationSeconds = duration;
            CheckParseStatus(ref meta);
        }

        private void ParseWithTagLib(ref MediaMeta meta)
        {
            tagLibService.GetFile(meta.filePath);
            (int h, int w, int duration) = tagLibService.GetVideoProperties(meta.filePath);
            meta.Width = w;
            meta.Height = h;
            meta.DurationSeconds = duration;
            CheckParseStatus(ref meta);
        }

        private void ParseWithShell(ref MediaMeta meta)
        {
            (int h, int w, int duration) = shellService.GetVideoProperties(meta.filePath);
            meta.Width = w;
            meta.Height = h;
            meta.DurationSeconds = duration;
            CheckParseStatus(ref meta);
        }

        private void ParseWithFFProbeCmd(ref MediaMeta meta)
        {
            (int h, int w, int duration) = ffProbeCmdService.GetVideoProperties(meta.filePath);
            meta.Width = w;
            meta.Height = h;
            meta.DurationSeconds = duration;
            CheckParseStatus(ref meta);
        }

        public MediaMeta GetFileMetaData(string filePath)
        {
            MediaMeta meta = new MediaMeta()
            {
                Width = -1,
                Height = -1,
                DurationSeconds = -1,
                Result = parseResults.Failed,
                filePath = filePath
            };

            // use the top 4 libraries to get the metadata
            // UseMp4Parser / UseTagLibService / UseShellService / UseFFProbeCmd
            try
            {
                if (MediaChecker.IsMediaFile(filePath))
                {
                    meta.Result = parseResults.Working;
                    if (MediaChecker.IsFastParseFile(filePath))
                    {
                        ParseWithMp4Parser(ref meta);
                        if (meta.Result == parseResults.Success)
                        {
                            meta.notes = "MP4FastParse";
                            Debug.Print($"[MP4FastParse done]:{filePath}");
                            return meta;
                        }
                    }

                    ParseWithTagLib(ref meta);
                    if (meta.Result == parseResults.Success)
                    {
                        meta.notes = "TagLib";
                        Debug.Print($"[TagLib done]:{filePath}");
                        return meta;
                    }

                    ParseWithShell(ref meta);
                    if (meta.Result == parseResults.Success)
                    {
                        meta.notes = "Shell";
                        Debug.Print($"[Shell done]:{filePath}");
                        return meta;
                    }

                    ParseWithFFProbeCmd(ref meta);
                    if (meta.Result == parseResults.Success)
                    {
                        meta.notes = "FFProbeCmd";
                        Debug.Print($"[FFProbeCmd done]:{filePath}");
                        return meta;
                    }
                }
                else
                {
                    Debug.Print($"[ERR:notMedia]:{filePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting metadata: {ex.Message}");
                meta.Result = parseResults.Failed;
            }
            return meta;
        }
    }
}
