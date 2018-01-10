using System;
using System.Diagnostics;

namespace ProcessMonitor
{
    public class ProcessObject
    {
        public string POId { get; set; }
        public int FId { get; set; }
        public bool HashMatched { get; set; }
        public float CPUUsage { get; set; }
        //public float CPUTime { get; set; }
        public float RAMUsage { get; set; }
        public float Handles { get; set; }
        public float Threads { get; set; }
        public float ReadBytes { get; set; }
        public float WriteBytes { get; set; }
        public float ReadOps { get; set; }
        public float WriteOps { get; set; }
        public ProcessPriorityClass ProcessPriorityClass { get; set; }
        //public long PeakVirtualMemorySize64 { get; set; }
        //public long PeakWorkingSet64 { get; set; }
        //public long PeakPagedMemorySize64 { get; set; }
        //public long PagedMemorySize64 { get; set; }
        //public long NonpagedSystemMemorySize64 { get; set; }
        //public ProcessModuleCollection Modules { get; set; }
        //public long PagedSystemMemorySize64 { get; set; }
        //public long PrivateMemorySize64 { get; set; }
        public TimeSpan PrivilegedProcessorTime { get; set; }
        public string ProcessName { get; set; }
        //public long WorkingSet64 { get; set; }
        //public bool EnableRaisingEvents { get; set; }
        //public long VirtualMemorySize64 { get; set; }
        //public TimeSpan UserProcessorTime { get; set; }
        public TimeSpan TotalProcessorTime { get; set; }
        //public ProcessThreadCollection Threads { get; set; }
        public int SessionId { get; set; }
        public bool Responding { get; set; }
        public ProcessModuleObject MainModule { get; set; }
        public string MainWindowTitle { get; set; }
        public string MachineName { get; set; }
        public int HandleCount { get; set; }
        public DateTime ExitTime { get; set; }
        public bool HasExited { get; set; }
        public int ExitCode { get; set; }
        //public int BasePriority { get; set; }
    }
}
