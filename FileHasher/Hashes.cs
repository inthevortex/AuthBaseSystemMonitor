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
    }
}
