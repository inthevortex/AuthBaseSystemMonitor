using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace FileHasher
{
    public class Hasher
    {
        private string[] _paths;

        public Hasher()
        {
            _paths = new string[] { "" };
        }

        public Hasher(string[] paths)
        {
            _paths = paths;
        }

        public List<File> HashSystem()
        {
            Dictionary<string, FileInfoWithVersion> filePathsWithInfo = GetFileList(_paths);
            List<File> files = new List<File>();
            VirusTotal.VirusTotalService virusTotalService = new VirusTotal.VirusTotalService();

            foreach (string path in filePathsWithInfo.Keys)
            {
                var output = virusTotalService.SigCheckFile(path);

                files.Add(new File
                {
                    Name = path.Substring(path.LastIndexOf("\\") + 1),
                    Path = path,
                    Hash = output.SHA256,
                    CreationTimeUtc = filePathsWithInfo[path].FileInfo.CreationTimeUtc,
                    LastAccessedTimeUtc = filePathsWithInfo[path].FileInfo.LastAccessTimeUtc,
                    LastWriteTimeUtc = filePathsWithInfo[path].FileInfo.LastWriteTimeUtc,
                    Length = filePathsWithInfo[path].FileInfo.Length,
                    ReadOnly = filePathsWithInfo[path].FileInfo.IsReadOnly,
                    Version = filePathsWithInfo[path].FileVersionInfo.FileVersion,
                    VTDetection = output.VTDetection,
                    Whitelisted = true
                });
            }

            return files;
        }

        public string GetHash(string filename, string hashAlgorithm = "SHA256")
        {
            string output = "";
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                UseShellExecute = false,
                CreateNoWindow = false,
                RedirectStandardOutput = true,
                FileName = @"C:\Users\AuthBase\source\repos\AuthBaseSystemIOMonitor\FileHasher\Resources\checksum.exe",
                Arguments = "/a:" + hashAlgorithm + " \"" + filename + "\""
            };

            using (Process process = Process.Start(startInfo))
            {
                output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
            }

            return output.Substring(output.IndexOf("Hash:") + 5).Trim();
        }

        private Dictionary<string, FileInfoWithVersion> GetFileList(string[] paths)
        {
            List<string> filePaths = new List<string>();
            Dictionary<string, FileInfoWithVersion> files = new Dictionary<string, FileInfoWithVersion>();

            foreach (string path in paths)
            {
                filePaths.AddRange(GetFileList(path));
            }

            foreach (string file in filePaths)
            {
                files.Add(file, new FileInfoWithVersion(file));
            }

            return files;
        }

        private IEnumerable<string> GetFileList(string rootFolderPath)
        {
            Queue<string> pending = new Queue<string>();
            pending.Enqueue(rootFolderPath);
            string[] tmp;

            while (pending.Count > 0)
            {
                rootFolderPath = pending.Dequeue();

                try
                {
                    tmp = Directory.GetFiles(rootFolderPath);
                }
                catch (System.Exception)
                {
                    continue;
                }

                for (int i = 0; i < tmp.Length; i++)
                {
                    yield return tmp[i];
                }

                tmp = Directory.GetDirectories(rootFolderPath);

                for (int i = 0; i < tmp.Length; i++)
                {
                    pending.Enqueue(tmp[i]);
                }
            }
        }
    }
}
