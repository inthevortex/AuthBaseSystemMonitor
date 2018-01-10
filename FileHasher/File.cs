using System;

namespace FileHasher
{
    public class File
    {
        public int FId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Hash { get; set; }
        public DateTime CreationTimeUtc { get; set; }
        public DateTime LastAccessedTimeUtc { get; set; }
        public DateTime LastWriteTimeUtc { get; set; }
        public long Length { get; set; }
        public bool ReadOnly { get; set; }
        public string Version { get; set; }
    }
}
