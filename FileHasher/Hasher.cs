﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

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

        public void CreateJsonFile(Hashes hashes)
        {
            System.IO.File.WriteAllText(@"C:\Users\AuthBase\Documents\hashes.json", JsonConvert.SerializeObject(hashes, Formatting.Indented));
        }

        public void ExecuteChecksum()
        {
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.FileName = @"C:\Users\AuthBase\source\repos\AuthBaseSystemIOMonitor\FileHasher\Resources\checksum.exe";
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
        }

        public Hashes GetHashes()
        {
            Hashes hashes = new Hashes();
            List<string> files = GetFileList(_paths, _fileSearchPattern);

            foreach (string file in files)
            {
                try
                {
                    using (FileStream fStream = System.IO.File.OpenRead(file))
                    {
                        hashes.Files.Add(new File
                        {
                            Name = file.Substring(file.LastIndexOf("\\") + 1),
                            Path = file,
                            Hash = GetHash<MD5>(fStream)
                        });
                    }
                }
                catch (IOException)
                {
                    continue;
                }
            }

            Console.WriteLine("Number of files hashes is {0}.", hashes.Files.Count);

            return hashes;
        }

        private string GetHash<T>(Stream stream) where T : HashAlgorithm
        {
            StringBuilder sb = new StringBuilder();

            MethodInfo create = typeof(T).GetMethod("Create", new Type[] { });
            using (T crypt = (T)create.Invoke(null, null))
            {
                byte[] hashBytes = crypt.ComputeHash(stream);
                foreach (byte bt in hashBytes)
                {
                    sb.Append(bt.ToString("x2"));
                }
            }
            return sb.ToString();
        }

        private List<string> GetFileList(string[] paths, string fileSearchPattern)
        {
            List<string> files = new List<string>();

            foreach (string path in paths)
            {
                files.AddRange(GetFileList(path, fileSearchPattern));
            }

            Console.WriteLine("FileList has {0} files.", files.Count);

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
                catch (UnauthorizedAccessException)
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