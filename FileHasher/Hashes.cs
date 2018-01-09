using System;
using System.Collections.Generic;

namespace FileHasher
{
    public class Hashes
    {
        public Hashes()
        {
            Files = new List<File>();
        }

        public List<File> Files { get; set; }
    }

    public class File
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Hash { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastAccessedTime { get; set; }
        public DateTime LastModifiedTime { get; set; }
        public long Size { get; set; }
        public bool ReadOnly { get; set; }
        //public string Version { get; set; }
        public AccessPriviledge AccessPriviledge { get; set; }
    }

    public class AccessPriviledge
    {
    }
}
