﻿using System;
using System.Diagnostics;

namespace ProcessMonitor
{
    public class ProcessModuleObject
    {
        public string ModuleName { get; set; }
        public string FileName { get; set; }
        public IntPtr BaseAddress { get; set; }
        public int ModuleMemorySize { get; set; }
        public IntPtr EntryPointAddress { get; set; }
        public FileVersionInfo FileVersionInfo { get; set; }
    }
}