using System.Runtime.InteropServices;

namespace CSharpAppPlayground.FilesFolders
{
    public static class FileCacheManager
    {
        [Flags]
        public enum File_Cache_Flags : uint
        {
            MAX_HARD_ENABLE = 0x00000001,
            MAX_HARD_DISABLE = 0x00000002,
            MIN_HARD_ENABLE = 0x00000004,
            MIN_HARD_DISABLE = 0x00000008
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetSystemFileCacheSize(IntPtr MinimumFileCacheSize, IntPtr MaximumFileCacheSize, File_Cache_Flags Flags);

        /// <summary>
        /// Flushes the system file cache by setting its size to unlimited minimum and maximum.
        /// </summary>
        public static bool FlushFileCache()
        {
            const long UNLIMITED = -1;
            IntPtr ptr = new IntPtr(UNLIMITED);
            return SetSystemFileCacheSize(ptr, ptr, 0);
        }
    }
}
