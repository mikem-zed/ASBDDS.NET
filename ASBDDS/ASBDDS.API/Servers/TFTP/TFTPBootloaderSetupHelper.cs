using ASBDDS.Shared.Models.Database.DataDb;
using System;
using System.IO;

namespace ASBDDS.API.Servers.TFTP
{
    public static class TFTPBootloaderSetupHelper
    {
        public static string TftpDirectory = Path.Combine(Environment.CurrentDirectory, "tftp_root");
        public static string ImagesDirectory = Path.Combine(Environment.CurrentDirectory, "images");

        private static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            foreach (FileInfo fi in source.GetFiles())
            {
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        public static void MakeFirmware(Device device)
        {
            var firmwarePath = Path.Combine(ImagesDirectory, device.BaseModel, "firmware");
            var outPath = Path.Combine(TftpDirectory, device.Serial);
            CopyAll(new DirectoryInfo(firmwarePath), new DirectoryInfo(outPath));
        }

        public static void MakeUboot(Device device, string variant = "normal")
        {
            var ubootPath = Path.Combine(ImagesDirectory, device.BaseModel, "u-boot", variant);
            var outPath = Path.Combine(TftpDirectory, device.Serial);
            CopyAll(new DirectoryInfo(ubootPath), new DirectoryInfo(outPath));
        }

        public static void RemoveDeviceDirectory(Device device)
        {
            Directory.Delete(Path.Combine(TftpDirectory, device.Serial), true);
        }
    }
}
