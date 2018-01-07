using System;
using System.Diagnostics;

namespace ProcessMonitor
{
    public class ProcessObject
    {
        public ProcessPriorityClass ProcessPriorityClass { get; set; }
        public bool PriorityBoostEnabled { get; set; }
        public long PeakVirtualMemorySize64 { get; set; }
        public long PeakWorkingSet64 { get; set; }
        public long PeakPagedMemorySize64 { get; set; }
        public long PagedMemorySize64 { get; set; }
        public long NonpagedSystemMemorySize64 { get; set; }
        //public ProcessModuleCollection Modules { get; set; }
        public IntPtr MinWorkingSet { get; set; }
        public long PagedSystemMemorySize64 { get; set; }
        public long PrivateMemorySize64 { get; set; }
        public TimeSpan PrivilegedProcessorTime { get; set; }
        public string ProcessName { get; set; }
        public long WorkingSet64 { get; set; }
        public bool EnableRaisingEvents { get; set; }
        public long VirtualMemorySize64 { get; set; }
        public TimeSpan UserProcessorTime { get; set; }
        public TimeSpan TotalProcessorTime { get; set; }
        //public ProcessThreadCollection Threads { get; set; }
        public ProcessStartInfoObject StartInfo { get; set; }
        public int SessionId { get; set; }
        public bool Responding { get; set; }
        public IntPtr ProcessorAffinity { get; set; }
        public IntPtr MaxWorkingSet { get; set; }
        public ProcessModuleObject MainModule { get; set; }
        public string MainWindowTitle { get; set; }
        public string MachineName { get; set; }
        public int Id { get; set; }
        public int HandleCount { get; set; }
        //public SafeProcessHandle SafeHandle { get; }
        public IntPtr Handle { get; set; }
        //public DateTime ExitTime { get; set; }
        public bool HasExited { get; set; }
        //public int ExitCode { get; set; }
        public int BasePriority { get; set; }
        public IntPtr MainWindowHandle { get; set; }
    }
}
