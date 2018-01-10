using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHasher
{
    public class DynamicFile
    {
        public int DFId { get; set; }
        public int FId { get; set; }
        public float CPUUsage { get; set; }
        public float CPUTime { get; set; }
        public float RAMUsage { get; set; }
        public float Handles { get; set; }
        public float Threads { get; set; }
        public float ReadBytes { get; set; }
        public float WriteBytes { get; set; }
        public float ReadOps { get; set; }
        public float WriteOps { get; set; }
    }
}
