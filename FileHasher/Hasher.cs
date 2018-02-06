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
            _paths = new string[] { @"C:\" };
        }

        public Hasher(string[] paths)
        {
            _paths = paths;
        }

        public List<File> HashSystem()
        {
            List<File> files = new List<File>();
            VirusTotalService.VirusTotalService virusTotalService = new VirusTotalService.VirusTotalService();

            foreach (string path in _paths)
            {
                var outputs = virusTotalService.SigCheckDirectory(path);

                foreach (var output in outputs)
                {
                    var fileInfo = new FileInfoWithVersion(output.Path);
                    
                    files.Add(new File
                    {
                        Name = path.Substring(path.LastIndexOf("\\") + 1),
                        Path = path,
                        Hash = output.SHA256,
                        CreationTimeUtc = fileInfo.FileInfo.CreationTimeUtc,
                        LastAccessedTimeUtc = fileInfo.FileInfo.LastAccessTimeUtc,
                        LastWriteTimeUtc = fileInfo.FileInfo.LastWriteTimeUtc,
                        Length = fileInfo.FileInfo.Length,
                        ReadOnly = fileInfo.FileInfo.IsReadOnly,
                        Version = fileInfo.FileVersionInfo.FileVersion,
                        VTDetection = output.VTDetection,
                        Whitelisted = true
                    }); 
                }
            }

            return files;
        }

        //private Dictionary<string, FileInfoWithVersion> GetFileList(string[] paths)
        //{
        //    List<string> filePaths = new List<string>();
        //    Dictionary<string, FileInfoWithVersion> files = new Dictionary<string, FileInfoWithVersion>();

        //    foreach (string path in paths)
        //    {
        //        filePaths.AddRange(GetFileList(path));
        //    }

        //    foreach (string file in filePaths)
        //    {
        //        files.Add(file, new FileInfoWithVersion(file));
        //    }

        //    return files;
        //}

        //private IEnumerable<string> GetFileList(string rootFolderPath)
        //{
        //    Queue<string> pending = new Queue<string>();
        //    pending.Enqueue(rootFolderPath);
        //    string[] tmp;

        //    while (pending.Count > 0)
        //    {
        //        rootFolderPath = pending.Dequeue();

        //        try
        //        {
        //            tmp = Directory.GetFiles(rootFolderPath);
        //        }
        //        catch (System.Exception)
        //        {
        //            continue;
        //        }

        //        for (int i = 0; i < tmp.Length; i++)
        //        {
        //            yield return tmp[i];
        //        }

        //        tmp = Directory.GetDirectories(rootFolderPath);

        //        for (int i = 0; i < tmp.Length; i++)
        //        {
        //            pending.Enqueue(tmp[i]);
        //        }
        //    }
        //}
    }
}
