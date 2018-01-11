using System;
using System.Diagnostics;

namespace ProcessMonitor
{
    public class ProcessModuleObject
    {
        public string ModuleName { get; set; }
        public string FileName { get; set; }
        public int ModuleMemorySize { get; set; }
        public string Version { get; set; }
    }
}
