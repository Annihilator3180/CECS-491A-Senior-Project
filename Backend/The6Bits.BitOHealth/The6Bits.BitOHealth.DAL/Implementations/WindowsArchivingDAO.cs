using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.IO.Compression;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;

namespace The6Bits.BitOHealth.DAL.Implementations
{
    public class WindowsArchivingDAO : IArchivingPC
    {
        private string localPath = System.Environment.GetEnvironmentVariable("USERPROFILE")+"\\Documents\\";
        private string fileName = $"Logs-{DateTime.Now.Month}.csv";
        private string zipName = $"Logs-{DateTime.Now.Month}.zip";
        private string folderName = $"Logs-{DateTime.Now.Month}";
        public bool Archive(IList<string> logs)
        {
            var csv = new StringBuilder();
            foreach (string log in logs)
            {
                csv.AppendLine(log);
            }
            System.Diagnostics.Debug.WriteLine(System.Environment.GetEnvironmentVariable("USERPROFILE"));
            System.IO.Directory.CreateDirectory($"{localPath}{folderName}");
            string csvpath = $"{localPath}\\{folderName}\\{fileName}";
            File.WriteAllText(csvpath, csv.ToString());
            return true;
        }
        //passin file
        public bool Compress()
        {
            try
            {
                ZipFile.CreateFromDirectory($"{localPath}\\{folderName}", $"{localPath}\\{folderName}\\{zipName}");
                File.Delete($"{localPath}\\{folderName}\\{fileName}");
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

    }
}
