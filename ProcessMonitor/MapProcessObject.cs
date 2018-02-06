using FileHasher;
using FileHasher.VirusTotalService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProcessMonitor
{
    public class MapProcessObject
    {
        public ProcessObject TransformToProcessObject(Process process, Dictionary<string, float> otherResources)
        {
            ProcessObject processObject = new ProcessObject();
            VirusTotalService hasher = new VirusTotalService();

            try
            {
                string hash = hasher.GetHash(process.MainModule.FileName);
                processObject.HashMatched = CheckHash(hash);
                processObject.CPUUsage = otherResources["CPUUsage"];
                processObject.RAMUsage = otherResources["RAMUsage"];
                processObject.Handles = otherResources["Handles"];
                processObject.Threads = otherResources["Threads"];
                processObject.ReadBytes = otherResources["ReadBytes"];
                processObject.WriteBytes = otherResources["WriteBytes"];
                processObject.ReadOps = otherResources["ReadOps"];
                processObject.WriteOps = otherResources["WriteOps"];
                processObject.ProcessPriorityClass = process.PriorityClass;
                processObject.PrivilegedProcessorTime = process.PrivilegedProcessorTime;
                processObject.ProcessName = process.ProcessName;
                processObject.TotalProcessorTime = process.TotalProcessorTime;
                processObject.SessionId = process.SessionId;
                processObject.Responding = process.Responding;
                processObject.MainModule = new ProcessModuleObject
                {
                    ModuleName = process.MainModule.ModuleName,
                    FileName = process.MainModule.FileName,
                    ModuleMemorySize = process.MainModule.ModuleMemorySize,
                    Version = process.MainModule.FileVersionInfo.FileVersion
                };
                processObject.MainWindowTitle = process.MainWindowTitle;
                processObject.MachineName = process.MachineName;
                processObject.HandleCount = process.HandleCount;
                processObject.HasExited = process.HasExited;
            }
            catch (Exception ex)
            {
                return null;
            }

            return processObject;
        }

        public ProcessObject TransformToProcessObject(Process process)
        {
            ProcessObject processObject = new ProcessObject();
            VirusTotalService hasher = new VirusTotalService();

            try
            {
                processObject.Hash = hasher.GetHash(process.MainModule.FileName);
                processObject.HashMatched = CheckHash(processObject.Hash);
                processObject.ProcessPriorityClass = process.PriorityClass;
                processObject.PrivilegedProcessorTime = process.PrivilegedProcessorTime;
                processObject.ProcessName = process.ProcessName;
                processObject.TotalProcessorTime = process.TotalProcessorTime;
                processObject.SessionId = process.SessionId;
                processObject.Responding = process.Responding;
                processObject.MainModule = new ProcessModuleObject
                {
                    ModuleName = process.MainModule.ModuleName,
                    FileName = process.MainModule.FileName,
                    ModuleMemorySize = process.MainModule.ModuleMemorySize,
                    Version = process.MainModule.FileVersionInfo.FileVersion
                };
                processObject.MainWindowTitle = process.MainWindowTitle;
                processObject.MachineName = process.MachineName;
                processObject.HandleCount = process.HandleCount;
                processObject.HasExited = process.HasExited;
            }
            catch (Exception)
            {
                return null;
            }

            return processObject;
        }

        private bool CheckHash(string hash)
        {
            FileHasherContext context = new FileHasherContext();
            FileRepository repository = new FileRepository(context);

            return repository.CheckHash(hash);
        }

        public void WriteToJsonFile(ProcessObject[] processObjects)
        {
            string json = JsonConvert.SerializeObject(processObjects, Formatting.Indented);
            System.IO.File.WriteAllText(string.Format(@"C:\Users\AuthBase\Documents\ProcessStats\ProcessStat.json"), json);
        }
    }
}
