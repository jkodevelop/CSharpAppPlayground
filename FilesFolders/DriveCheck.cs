using System.Diagnostics;
using System.Management;

namespace CSharpAppPlayground.FilesFolders
{
    public class DriveCheck
    {
        public static void CheckAllDriveInfo()
        {   
            var drives = DriveInfo.GetDrives();
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

        // Usage: pass driveName = "C:" or "D:" etc. mind the colon
        public static bool isHDD(string driveName)
        {
            try
            {
                if (string.IsNullOrEmpty(driveName))
                    return false;

                // Ensure drive name has proper format (e.g., "C:")
                if (!driveName.EndsWith(":"))
                    driveName += ":";

                Debug.Print($"Checking drive: {driveName}");

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
                        // Get all relevant properties for debugging
                        var rotationRate = drive["RotationRate"];
                        var iface = drive["InterfaceType"]?.ToString()?.ToUpperInvariant();
                        var model = drive["Model"]?.ToString();
                        var mediaType = drive["MediaType"]?.ToString();

                        Debug.Print($"Drive Model: {model}");
                        Debug.Print($"Interface Type: {iface}");
                        Debug.Print($"Media Type: {mediaType}");
                        Debug.Print($"Rotation Rate: {rotationRate}");

                        // Check rotation rate (HDDs have > 0, SSDs typically have 0 or null)
                        if (rotationRate is uint rate && rate > 0)
                        {
                            Debug.Print($"HDD detected - Rotation Rate: {rate} RPM");
                            return true; // HDD
                        }

                        // Check interface type for external drives
                        if (iface == "USB")
                        {
                            Debug.Print("External USB drive detected - assuming HDD");
                            return true; // Assume external USB = HDD
                        }

                        // Check media type as additional indicator
                        if (!string.IsNullOrEmpty(mediaType) && 
                            (mediaType.Contains("Fixed") || mediaType.Contains("Rotational")))
                        {
                            Debug.Print($"HDD detected by Media Type: {mediaType}");
                            return true;
                        }
                    }
                }
            }
            catch(Exception ex) 
            {
                Debug.Print($"isHDD Exception: {ex.Message}");
            }
            return false; // Default to SSD if unknown
        }

        /// <summary>
        /// Gets detailed information about all physical drives in the system
        /// Useful for debugging and understanding drive properties
        /// </summary>
        public static void PrintAllDriveDetails()
        {
            try
            {
                using var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
                
                Debug.Print("=== All Physical Drives ===");
                foreach (ManagementObject drive in searcher.Get())
                {
                    Debug.Print($"Model: {drive["Model"]}");
                    Debug.Print($"  DeviceID: {drive["DeviceID"]}");
                    Debug.Print($"  InterfaceType: {drive["InterfaceType"]}");
                    Debug.Print($"  MediaType: {drive["MediaType"]}");
                    Debug.Print($"  RotationRate: {drive["RotationRate"]} RPM");
                    Debug.Print($"  Size: {drive["Size"]} bytes");
                    Debug.Print($"  Status: {drive["Status"]}");
                    Debug.Print("---");
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"PrintAllDriveDetails Exception: {ex.Message}");
            }
        }

        /// <summary>
        /// Alternative method to check if a drive is HDD using direct WMI query
        /// This is simpler but less precise for specific logical drives
        /// 
        /// Usage: pass driveName = "C:" or "D:" etc. mind the colon
        /// </summary>
        public static bool isHDD_Alternative(string driveName)
        {
            try
            {
                if (string.IsNullOrEmpty(driveName))
                    return false;

                // Ensure drive name has proper format
                if (!driveName.EndsWith(":"))
                    driveName += ":";

                // Get the drive letter without colon for WMI query
                string driveLetter = driveName.TrimEnd(':');

                // Direct query to get drive information
                string query = $"SELECT * FROM Win32_LogicalDisk WHERE DeviceID = '{driveName}'";
                using var searcher = new ManagementObjectSearcher(query);

                foreach (ManagementObject logicalDisk in searcher.Get())
                {
                    // Get associated physical drive
                    string assocQuery = $"ASSOCIATORS OF {{Win32_LogicalDisk.DeviceID='{driveName}'}} WHERE AssocClass=Win32_LogicalDiskToPartition";
                    using var assocSearcher = new ManagementObjectSearcher(assocQuery);
                    
                    foreach (ManagementObject partition in assocSearcher.Get())
                    {
                        string diskQuery = $"ASSOCIATORS OF {{Win32_DiskPartition.DeviceID='{partition["DeviceID"]}'}} WHERE AssocClass=Win32_DiskDriveToDiskPartition";
                        using var diskSearcher = new ManagementObjectSearcher(diskQuery);
                        
                        foreach (ManagementObject disk in diskSearcher.Get())
                        {
                            var rotationRate = disk["RotationRate"];
                            var iface = disk["InterfaceType"]?.ToString()?.ToUpperInvariant();
                            
                            if (rotationRate is uint rate && rate > 0)
                                return true;
                            if (iface == "USB")
                                return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"isHDD_Alternative Exception: {ex.Message}");
            }
            return false;
        }
    }
}