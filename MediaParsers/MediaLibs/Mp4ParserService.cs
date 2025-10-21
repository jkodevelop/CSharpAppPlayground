using System.Diagnostics;
using System.Text;

// EXPERIMENTAL: A simple MP4 parser to extract width, height, and duration without external libraries
// SUPPORTS: mp4/mov/m4v

namespace CSharpAppPlayground.MediaParsers.MediaLibs
{
    public class Mp4ParserService
    {
        protected Mp4Info info;

        public struct Mp4Info
        {
            public int Width;
            public int Height;
            public int DurationSeconds;
        }

        public Mp4ParserService()
        {
            info = new Mp4Info()
            {
                Width = -1,
                Height = -1,
                DurationSeconds = -1
            };
        }

        public int GetDuration(string filePath)
        {
            ParseMp4Header(filePath);
            Debug.Print($"what? {info.DurationSeconds}");
            return info.DurationSeconds;
        }

        //////////////////////////////////////////////////////////////////////////////////////
        // This section parses the MP4 file structure to extract width, height, and duration
        //////////////////////////////////////////////////////////////////////////////////////
        public Mp4Info ParseMp4Header(string filePath)
        {
            using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            using var br = new BinaryReader(fs);

            while (fs.Position < fs.Length)
            {
                long boxStart = fs.Position;
                uint boxSize = ReadUInt32(br);
                string boxType = Encoding.ASCII.GetString(br.ReadBytes(4));

                if (boxSize == 1)
                    boxSize = (uint)(br.ReadUInt64() - 16); // large size (rare)

                if (boxType == "moov")
                {
                    ParseMoovBox(br, boxStart, boxSize);
                    break;
                }
                else
                {
                    fs.Position = boxStart + boxSize;
                }
            }

            return info;
        }

        private void ParseMoovBox(BinaryReader br, long moovStart, uint moovSize)
        {
            long moovEnd = moovStart + moovSize;

            while (br.BaseStream.Position < moovEnd)
            {
                long boxStart = br.BaseStream.Position;
                uint boxSize = ReadUInt32(br);
                string boxType = Encoding.ASCII.GetString(br.ReadBytes(4));

                if (boxType == "mvhd")
                    ParseMvhd(br);
                else if (boxType == "trak")
                    ParseTrak(br, boxStart, boxSize);
                else
                    br.BaseStream.Position = boxStart + boxSize;
            }
        }

        private void ParseMvhd(BinaryReader br)
        {
            long start = br.BaseStream.Position;
            byte version = br.ReadByte();
            br.BaseStream.Position += 3; // flags

            if (version == 1)
            {
                br.BaseStream.Position += 16; // creation/modification time (64-bit)
                uint timescale = ReadUInt32(br);
                ulong duration = ReadUInt64(br);
                info.DurationSeconds = (int)(duration / (double)timescale);
            }
            else
            {
                br.BaseStream.Position += 8; // creation/modification time
                uint timescale = ReadUInt32(br);
                uint duration = ReadUInt32(br);
                Debug.Print($"duration: {duration}");
                info.DurationSeconds = (int)(duration / (double)timescale);
                Debug.Print($"info.duration: {info.DurationSeconds}");
            }

            br.BaseStream.Position = start + 100; // skip rest of mvhd
        }

        private void ParseTrak(BinaryReader br, long trakStart, uint trakSize)
        {
            long trakEnd = trakStart + trakSize;

            while (br.BaseStream.Position < trakEnd)
            {
                long boxStart = br.BaseStream.Position;
                uint boxSize = ReadUInt32(br);
                string boxType = Encoding.ASCII.GetString(br.ReadBytes(4));

                if (boxType == "tkhd")
                {
                    ParseTkhd(br);
                    break;
                }
                else
                {
                    br.BaseStream.Position = boxStart + boxSize;
                }
            }
        }

        private void ParseTkhd(BinaryReader br)
        {
            long start = br.BaseStream.Position;
            byte version = br.ReadByte();
            br.BaseStream.Position += 3; // flags

            if (version == 1)
                br.BaseStream.Position += 32; // creation/modification/reserved
            else
                br.BaseStream.Position += 20;

            br.BaseStream.Position += 52; // skip matrix etc.

            uint widthFixed = ReadUInt32(br);
            uint heightFixed = ReadUInt32(br);

            info.Width = (int)(widthFixed >> 16);
            info.Height = (int)(heightFixed >> 16);
        }

        private uint ReadUInt32(BinaryReader br)
        {
            var bytes = br.ReadBytes(4);
            if (bytes.Length < 4) throw new EndOfStreamException();
            Array.Reverse(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }

        private ulong ReadUInt64(BinaryReader br)
        {
            var bytes = br.ReadBytes(8);
            if (bytes.Length < 8) throw new EndOfStreamException();
            Array.Reverse(bytes);
            return BitConverter.ToUInt64(bytes, 0);
        }
    }
}
