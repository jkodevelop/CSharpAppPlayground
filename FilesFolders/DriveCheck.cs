using System.Diagnostics;
using System.IO;
using System.Management;

namespace CSharpAppPlayground.FilesFolders
{
    public class DriveCheck
    {
        public static void CheckAllDriveInfo()
        {   
            var drives = System.IO.DriveInfo.GetDrives();
            foreach (var drive in drives)
            {
                PrintDriveInfo(drive);
            }
        }

        public static void CheckDriveInfo(string driveName)
        {
            DriveInfo drive = new DriveInfo(driveName);
            PrintDriveInfo(drive);
        }

        public static void PrintDriveInfo(DriveInfo drive)
        {
            Debug.Print($"Drive Name: {drive.Name}");
            Debug.Print($"  Drive Type: {drive.DriveType}");
            if (drive.IsReady)
            {
                Debug.Print($"  Volume Label: {drive.VolumeLabel}");
                Debug.Print($"  File System: {drive.DriveFormat}");
                Debug.Print($"  Available Free Space: {drive.AvailableFreeSpace} bytes");
                Debug.Print($"  Total Free Space: {drive.TotalFreeSpace} bytes");
                Debug.Print($"  Total Size: {drive.TotalSize} bytes");
            }
            else
            {
                Debug.Print("  Drive is not ready.");
            }
        }

        public static bool isHDD(string driveName)
        {
            try
            {
                // Extract the drive letter (e.g., "C:")
                // string driveLetter = Path.GetPathRoot(path)?.TrimEnd('\\');

                if (string.IsNullOrEmpty(driveName))
                    return false;

                // Find the physical disk corresponding to that logical drive
                string query = "ASSOCIATORS OF {Win32_LogicalDisk.DeviceID='" + driveName +
                               "'} WHERE AssocClass=Win32_LogicalDiskToPartition";
                using var partitionSearcher = new ManagementObjectSearcher(query);

                foreach (ManagementObject partition in partitionSearcher.Get())
                {
                    string diskQuery = "ASSOCIATORS OF {Win32_DiskPartition.DeviceID='" +
                                       partition["DeviceID"] +
                                       "'} WHERE AssocClass=Win32_DiskDriveToDiskPartition";

                    using var driveSearcher = new ManagementObjectSearcher(diskQuery);
                    foreach (ManagementObject drive in driveSearcher.Get())
                    {
                        // Primary properties
                        var rotationRate = drive["RotationRate"];
                        var iface = drive["InterfaceType"]?.ToString()?.ToUpperInvariant();

                        if (rotationRate is uint rate && rate > 0)
                            return true; // HDD
                        if (iface == "USB")
                            return true; // Assume external USB = HDD
                    }
                }
            }
            catch(Exception ex) 
            {
                Debug.Print($"isHDD Exception: {ex.Message}");
            }
            return false; // Default to SSD if unknown

            //try
            //{
            //    var driveLetter = Path.GetPathRoot(path)?.TrimEnd('\\');
            //    using var searcher = new System.Management.ManagementObjectSearcher(
            //        "SELECT RotationRate, InterfaceType FROM Win32_DiskDrive");

            //    foreach (ManagementObject drive in searcher.Get())
            //    {
            //        var rotation = drive["RotationRate"];
            //        var iface = drive["InterfaceType"]?.ToString()?.ToUpperInvariant();

            //        if (rotation is uint rate && rate > 0)
            //            return true; // HDD
            //        if (iface == "USB")
            //            return true; // External USB HDD assumed
            //    }
            //}
            //catch { }
            //return false; // default to SSD
        }
    }
}