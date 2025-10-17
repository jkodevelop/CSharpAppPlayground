using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CSharpAppPlayground.FilesFolders
{
    class DriveHelper
    {
        [StructLayout(LayoutKind.Sequential)]
        struct STORAGE_PROPERTY_QUERY
        {
            public int PropertyId;
            public int QueryType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] AdditionalParameters;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct DEVICE_SEEK_PENALTY_DESCRIPTOR
        {
            public int Version;
            public int Size;
            [MarshalAs(UnmanagedType.U1)]
            public bool IncursSeekPenalty;
        }

        const uint IOCTL_STORAGE_QUERY_PROPERTY = 0x002D1400;
        const int StorageDeviceSeekPenaltyProperty = 7; // identifies SSD vs HDD
        const int PropertyStandardQuery = 0;

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern IntPtr CreateFile(
            string lpFileName,
            uint dwDesiredAccess,
            uint dwShareMode,
            IntPtr lpSecurityAttributes,
            uint dwCreationDisposition,
            uint dwFlagsAndAttributes,
            IntPtr hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool DeviceIoControl(
            IntPtr hDevice,
            uint dwIoControlCode,
            ref STORAGE_PROPERTY_QUERY lpInBuffer,
            int nInBufferSize,
            out DEVICE_SEEK_PENALTY_DESCRIPTOR lpOutBuffer,
            int nOutBufferSize,
            out int lpBytesReturned,
            IntPtr lpOverlapped);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool CloseHandle(IntPtr hObject);

        public static bool IsDriveSSD(string driveLetter)
        {
            string path = @"\\.\" + driveLetter.TrimEnd('\\');

            IntPtr hDevice = CreateFile(
                path,
                0, // no access, we just query metadata
                3, // FILE_SHARE_READ | FILE_SHARE_WRITE
                IntPtr.Zero,
                3, // OPEN_EXISTING
                0,
                IntPtr.Zero);

            if (hDevice.ToInt64() == -1)
                throw new IOException("Failed to open handle to drive.");

            STORAGE_PROPERTY_QUERY query = new()
            {
                PropertyId = StorageDeviceSeekPenaltyProperty,
                QueryType = PropertyStandardQuery,
                AdditionalParameters = new byte[1]
            };

            bool result = DeviceIoControl(
                hDevice,
                IOCTL_STORAGE_QUERY_PROPERTY,
                ref query,
                Marshal.SizeOf(query),
                out DEVICE_SEEK_PENALTY_DESCRIPTOR descriptor,
                Marshal.SizeOf(typeof(DEVICE_SEEK_PENALTY_DESCRIPTOR)),
                out int bytesReturned,
                IntPtr.Zero);

            CloseHandle(hDevice);

            if (!result)
            {
                // throw new IOException("DeviceIoControl failed.");
                return false; // fallback to assume HDD
            }

            // HDD -> true (incurs penalty), SSD -> false
            return !descriptor.IncursSeekPenalty;
        }

        public static ParallelOptions GetParallelOptions(string path)
        {
            string drive = Path.GetPathRoot(path);
            bool isSSD = false;
            try
            {
                isSSD = IsDriveSSD(drive);
            }
            catch (Exception ex)
            {
                Debug.Print($"GetParallelOptions Exception: {ex.Message}");
            }

            return new ParallelOptions
            {
                MaxDegreeOfParallelism = isSSD ? Environment.ProcessorCount : 1
            };
        }
    }

}
