using System.Diagnostics;
using System.IO;

namespace FileHasher
{
    // TODO: Replace with custom object
    public class FileInfoWithVersion
    {
        public FileInfoWithVersion(string filePath)
        {
            FileInfo = new FileInfo(filePath);
            FileVersionInfo = FileVersionInfo.GetVersionInfo(filePath);
        }

        public FileInfo FileInfo { get; set; }
        public FileVersionInfo FileVersionInfo { get; set; }
    }
}
