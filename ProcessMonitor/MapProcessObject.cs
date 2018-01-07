using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace ProcessMonitor
{
    public class MapProcessObject
    {
        public ProcessObject TransformToProcessObject(Process process)
        {
            ProcessObject processObject = new ProcessObject();

            try
            {
                processObject.ProcessPriorityClass = process.PriorityClass;
                processObject.PriorityBoostEnabled = process.PriorityBoostEnabled;
                processObject.PeakVirtualMemorySize64 = process.PeakVirtualMemorySize64;
                processObject.PeakWorkingSet64 = process.PeakWorkingSet64;
                processObject.PeakPagedMemorySize64 = process.PeakPagedMemorySize64;
                processObject.PagedMemorySize64 = process.PagedMemorySize64;
                processObject.NonpagedSystemMemorySize64 = process.NonpagedSystemMemorySize64;
                //processObject.Modules = process.Modules;
                processObject.MinWorkingSet = process.MinWorkingSet;
                processObject.PagedSystemMemorySize64 = process.PagedSystemMemorySize64;
                processObject.PrivateMemorySize64 = process.PrivateMemorySize64;
                processObject.PrivilegedProcessorTime = process.PrivilegedProcessorTime;
                processObject.ProcessName = process.ProcessName;
                processObject.WorkingSet64 = process.WorkingSet64;
                processObject.EnableRaisingEvents = process.EnableRaisingEvents;
                processObject.VirtualMemorySize64 = process.VirtualMemorySize64;
                processObject.UserProcessorTime = process.UserProcessorTime;
                processObject.TotalProcessorTime = process.TotalProcessorTime;
                //processObject.Threads = process.Threads;
                processObject.StartInfo = new ProcessStartInfoObject
                {
                    ErrorDialog = process.StartInfo.ErrorDialog,
                    WorkingDirectory = process.StartInfo.WorkingDirectory,
                    FileName = process.StartInfo.FileName,
                    LoadUserProfile = process.StartInfo.LoadUserProfile,
                    Domain = process.StartInfo.Domain,
                    PasswordInClearText = process.StartInfo.PasswordInClearText,
                    Password = process.StartInfo.Password,
                    UserName = process.StartInfo.UserName,
                    Verbs = process.StartInfo.Verbs,
                    UseShellExecute = process.StartInfo.UseShellExecute,
                    RedirectStandardError = process.StartInfo.RedirectStandardError,
                    RedirectStandardOutput = process.StartInfo.RedirectStandardOutput,
                    RedirectStandardInput = process.StartInfo.RedirectStandardInput,
                    EnvironmentVariables = process.StartInfo.EnvironmentVariables,
                    CreateNoWindow = process.StartInfo.CreateNoWindow,
                    Arguments = process.StartInfo.Arguments,
                    Verb = process.StartInfo.Verb,
                    ErrorDialogParentHandle = process.StartInfo.ErrorDialogParentHandle,
                    WindowStyle = process.StartInfo.WindowStyle
                };
                processObject.SessionId = process.SessionId;
                processObject.Responding = process.Responding;
                processObject.ProcessorAffinity = process.ProcessorAffinity;
                processObject.MaxWorkingSet = process.MaxWorkingSet;
                processObject.MainModule = new ProcessModuleObject
                {
                    ModuleName = process.MainModule.ModuleName,
                    FileName = process.MainModule.FileName,
                    BaseAddress = process.MainModule.BaseAddress,
                    ModuleMemorySize = process.MainModule.ModuleMemorySize,
                    EntryPointAddress = process.MainModule.EntryPointAddress,
                    FileVersionInfo = process.MainModule.FileVersionInfo
                };
                processObject.MainWindowTitle = process.MainWindowTitle;
                processObject.MachineName = process.MachineName;
                processObject.Id = process.Id;
                processObject.HandleCount = process.HandleCount;
                processObject.Handle = process.Handle;
                //processObject.ExitTime = process.ExitTime;
                processObject.HasExited = process.HasExited;
                //processObject.ExitCode = process.ExitCode;
                processObject.BasePriority = process.BasePriority;
                processObject.MainWindowHandle = process.MainWindowHandle;
            }
            catch (Exception)
            {
                return null;
            }

            return processObject;
        }

        public void WriteToJsonFile(ProcessObject[] processObjects)
        {
            string json = JsonConvert.SerializeObject(processObjects, Formatting.Indented);
            System.IO.File.WriteAllText(string.Format(@"C:\Users\AuthBase\Documents\ProcessStats\ProcessStat_{0}.json", DateTime.Now), json);
        }
    }
}
