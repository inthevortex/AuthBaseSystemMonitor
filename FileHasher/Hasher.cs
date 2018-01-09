using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace FileHasher
{
    public class Hasher
    {
        private string[] _paths;
        private string _fileSearchPattern;

        public Hasher()
        {
            _paths = new string[] { @"C:\Windows", @"C:\Program Files", @"C:\Program Files (x86)", @"C:\Users" };
            _fileSearchPattern = "*";
        }

        public Hasher(string[] paths, string fileSearchPattern)
        {
            _paths = paths;
            _fileSearchPattern = fileSearchPattern;
        }

        public void CreateJsonFile(List<File> files)
        {
            System.IO.File.WriteAllText(@"C:\Users\AuthBase\Documents\hashes.json", JsonConvert.SerializeObject(files, Formatting.Indented));
        }

        public List<File> HashSystem()
        {
            Dictionary<string, FileInfoWithVersion> filePathsWithInfo = GetFileList(_paths, _fileSearchPattern);
            List<File> files = new List<File>();

            foreach (string path in filePathsWithInfo.Keys)
            {
                try
                {
                    files.Add(new File
                    {
                        Name = path.Substring(path.LastIndexOf("\\") + 1),
                        Path = path,
                        Hash = GetHash(path),
                        CreationTimeUtc = filePathsWithInfo[path].FileInfo.CreationTimeUtc,
                        LastAccessedTimeUtc = filePathsWithInfo[path].FileInfo.LastAccessTimeUtc,
                        LastWriteTimeUtc = filePathsWithInfo[path].FileInfo.LastWriteTimeUtc,
                        Length = filePathsWithInfo[path].FileInfo.Length,
                        ReadOnly = filePathsWithInfo[path].FileInfo.IsReadOnly,
                        Version = filePathsWithInfo[path].FileVersionInfo.FileVersion
                    });
                }
                catch (IOException)
                {
                    continue;
                }
            }

            //Console.WriteLine("Number of files hashes is {0}.", files.Count);

            return files;
        }

        //private string GetHash<T>(Stream stream) where T : HashAlgorithm
        //{
        //    StringBuilder sb = new StringBuilder();

        //    MethodInfo create = typeof(T).GetMethod("Create", new Type[] { });
        //    using (T crypt = (T)create.Invoke(null, null))
        //    {
        //        byte[] hashBytes = crypt.ComputeHash(stream);
        //        foreach (byte bt in hashBytes)
        //        {
        //            sb.Append(bt.ToString("x2"));
        //        }
        //    }
        //    return sb.ToString();
        //}

        private string GetHash(string filename, string hashAlgorithm = "SHA256")
        {
            string output = "";
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                UseShellExecute = false,
                CreateNoWindow = false,
                RedirectStandardOutput = true,
                FileName = @"C:\Users\AuthBase\source\repos\AuthBaseSystemIOMonitor\FileHasher\Resources\checksum.exe",
                Arguments = "/a:" + hashAlgorithm + " " + filename
            };

            using (Process process = Process.Start(startInfo))
            {
                output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
            }

            return output.Substring(output.IndexOf("Hash:") + 5).Trim();
        }

        private Dictionary<string, FileInfoWithVersion> GetFileList(string[] paths, string fileSearchPattern)
        {
            List<string> filePaths = new List<string>();
            Dictionary<string, FileInfoWithVersion> files = new Dictionary<string, FileInfoWithVersion>();

            foreach (string path in paths)
            {
                filePaths.AddRange(GetFileList(path, fileSearchPattern));
            }

            foreach (string file in filePaths)
            {
                files.Add(file, new FileInfoWithVersion(file));
            }

            //Console.WriteLine("FileList has {0} files.", filePaths.Count);

            return files;
        }

        private IEnumerable<string> GetFileList(string rootFolderPath, string fileSearchPattern)
        {
            Queue<string> pending = new Queue<string>();
            pending.Enqueue(rootFolderPath);
            string[] tmp;

            while (pending.Count > 0)
            {
                rootFolderPath = pending.Dequeue();

                try
                {
                    tmp = Directory.GetFiles(rootFolderPath, fileSearchPattern);
                }
                catch (System.UnauthorizedAccessException)
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
